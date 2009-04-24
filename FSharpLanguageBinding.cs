//  FSharpLanguageBinding.cs
//
//  Authors:
//  Art Wild <wildart@gmail.com>
// 
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//  
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA

using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Reflection;
using System.Resources;
using System.Xml;
using System.CodeDom.Compiler;
using System.Threading;

using MonoDevelop.Projects;
using MonoDevelop.Projects.Dom;
using MonoDevelop.Projects.Dom.Parser;
using MonoDevelop.Projects.CodeGeneration;
using MonoDevelop.Core;

namespace FSharpBinding
{
	public class FSharpLanguageBinding : IDotNetLanguageBinding
	{
		public const string LanguageName = "F#";
		
        
		FSharpBindingCompilerManager   compilerManager  = new FSharpBindingCompilerManager();
		//FSharpEnhancedCodeProvider provider;
        static GlobalProperties props = new GlobalProperties ();
        
        public static GlobalProperties Properties {
			get { return props; }
		}
		
		public string Language {
			get {
				return LanguageName;
			}
		}
		
		public bool IsSourceCodeFile (string fileName)
		{
			Debug.Assert(compilerManager != null);
			return compilerManager.CanCompile(fileName);
		}
		
		public BuildResult Compile(ProjectItemCollection items, DotNetProjectConfiguration configuration, IProgressMonitor monitor)
		{
			Debug.Assert(compilerManager != null);
			return compilerManager.Compile(items, configuration, monitor);
		}

		public ProjectParameters CreateProjectParameters(XmlElement projectOptions)
		{
		  // FIXME: Dummy implementation
		  return new ProjectParameters();
		}

		public ConfigurationParameters CreateCompilationParameters (XmlElement projectOptions)
		{
			FSharpCompilerParameters pars = new FSharpCompilerParameters ();
			if (projectOptions != null) {
				string debugAtt = projectOptions.GetAttribute ("DefineDebug");
				if (string.Compare ("True", debugAtt, true) == 0)
					pars.DefineSymbols = "DEBUG";
			}
			return pars;
		}
		
		public string CommentTag
		{
			get { return "//"; }
		}
		
		public CodeDomProvider GetCodeDomProvider ()
		{
//			if (provider == null)
//				provider = new FSharpEnhancedCodeProvider ();
//			return provider;
            return null;
		}
		
		public string GetFileName (string baseName)
		{
			return baseName + ".fs";
		}
		
		public IParser Parser {
			get { return null;	 }
		}
		
		public IRefactorer Refactorer {
			get { return null;	}
		}
		
		public ClrVersion[] GetSupportedClrVersions ()
		{
			return new ClrVersion[] { ClrVersion.Net_1_1, ClrVersion.Net_2_0 };
		}
	}
    
    	public class GlobalProperties
	{
		Properties props = (Properties) PropertyService.Get ("FSharpBingins.GlobalProperties", new Properties ());
		
		public string fscPath {
			get { return props.Get ("fscPath", ""); }
			set { props.Set ("fscPath", value.Equals(null) ? "" : value); }
		}
        
        public string fscFileName {
			get { return props.Get("fscFileName", ""); }
			set { props.Set ("fscFileName", value.Equals(null) ? "" : value); }
		}

		public bool addMonoToFSCcall {
			get { return props.Get ("monoCall", true); }
			set { props.Set ("monoCall", value.Equals(null) ? true : value); }
		}
        
        public bool compilationProgress {
			get { return props.Get("fscProgress", false); }
			set { props.Set ("fscProgress", value.Equals(null) ? false : value); }
		}

	}
}
