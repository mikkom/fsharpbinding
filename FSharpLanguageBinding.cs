//  FSharpLanguageBinding.cs
//
//  Copyright (C) 2008   Art Wild <wildart@gmail.com>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Reflection;
using System.Resources;
using System.Xml;
using System.CodeDom.Compiler;
using System.Threading;
using Microsoft.CSharp;

using MonoDevelop.Projects;
using MonoDevelop.Projects.Parser;
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
		
		public BuildResult Compile (ProjectFileCollection projectFiles, ProjectReferenceCollection references, DotNetProjectConfiguration configuration, IProgressMonitor monitor)
		{
			Debug.Assert(compilerManager != null);
			return compilerManager.Compile (projectFiles, references, configuration, monitor);
		}
		
		public ICloneable CreateCompilationParameters (XmlElement projectOptions)
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
