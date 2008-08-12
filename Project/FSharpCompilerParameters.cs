//  FSharpCompilerParameters.cs
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
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;
using MonoDevelop.Core.Gui.Components;
using MonoDevelop.Projects;
using MonoDevelop.Core.Serialization;

namespace FSharpBinding
{
	/// <summary>
	/// This class handles project specific compiler parameters
	/// </summary>
	public class FSharpCompilerParameters: ICloneable
	{
		// Configuration parameters
		
		[ItemProperty ("Warnings", DefaultValue = false)]
		bool warnings = false;
		
		[ItemProperty ("NoWarn", DefaultValue = "")]
		string noWarnings = String.Empty;
		
		[ItemProperty ("Optimize")]
		bool optimize;
			
		[ItemProperty ("DefineConstants", DefaultValue = "")]
		string definesymbols = String.Empty;
		
		[ItemProperty ("GenerateDocumentation", DefaultValue = false)]
		bool generateXmlDocumentation = false;

		[ProjectPathItemProperty ("Win32Resource", DefaultValue = "")]
		string win32Resource = String.Empty;
	
		[ItemProperty ("CodePage", DefaultValue = 0)]
		int codePage;
		
		[ItemProperty ("additionalargs", DefaultValue = "")]
		string additionalArgs = string.Empty;
				
		[ItemProperty ("TreatWarningsAsErrors", DefaultValue = false)]
		bool treatWarningsAsErrors;
	
		public object Clone ()
		{
			return MemberwiseClone ();
		}
		
		public int CodePage {
			get {
				return codePage;
			}
			set {
				codePage = value;
			}
		}
		
		[Browsable(false)]
		public string Win32Resource {
			get {
				return win32Resource;
			}
			set {
				win32Resource = value;
			}
		}
		
		public string AdditionalArguments {
			get { return additionalArgs; }
			set { additionalArgs = value; }
		}
		

#region Code Generation
		public string DefineSymbols {
			get {
				return definesymbols;
			}
			set {
				definesymbols = value;
			}
		}
		
		public bool Optimize {
			get {
				return optimize;
			}
			set {
				optimize = value;
			}
		}		
		
		public bool GenerateXmlDocumentation {
			get {
				return generateXmlDocumentation;
			}
			set {
				generateXmlDocumentation = value;
			}
		}
		
#endregion

#region Errors and Warnings 
		public bool Warnings {
			get {
				return warnings;
			}
			set {
				warnings = value;
			}
		}
		
		public string NoWarnings {
			get {
				return noWarnings;
			}
			set {
				noWarnings = value;
			}
		}

		public bool TreatWarningsAsErrors {
			get {
				return treatWarningsAsErrors;
			}
			set {
				treatWarningsAsErrors = value;
			}
		}
#endregion
	}
}
