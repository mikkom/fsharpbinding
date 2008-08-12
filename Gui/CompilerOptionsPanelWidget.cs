// CompilerOptionsPanelWidget.cs
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
using Gtk;

using MonoDevelop.Ide.Gui;
using MonoDevelop.Core;
using MonoDevelop.Core.Gui;
using MonoDevelop.Projects;
using MonoDevelop.Projects.Parser;
using MonoDevelop.Projects.Text;
using MonoDevelop.Projects.Gui.Dialogs;

namespace FSharpBinding
{	
	[System.ComponentModel.Category("FSharpBinding")]
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CompilerOptionsPanelWidget : Gtk.Bin
	{
		DotNetProject project;
		
		public CompilerOptionsPanelWidget (DotNetProject project)
		{
			this.Build();
			this.project = project;
			DotNetProjectConfiguration configuration = (DotNetProjectConfiguration) project.GetActiveConfiguration (IdeApp.Workspace.ActiveConfiguration);
			FSharpCompilerParameters compilerParameters = (FSharpCompilerParameters) configuration.CompilationParameters;
			
			ListStore store = new ListStore (typeof (string));
			store.AppendValues (GettextCatalog.GetString ("Executable"));
			store.AppendValues (GettextCatalog.GetString ("Library"));
			store.AppendValues (GettextCatalog.GetString ("Executable with GUI"));
			store.AppendValues (GettextCatalog.GetString ("Module"));
			compileTargetCombo.Model = store;
			CellRendererText cr = new CellRendererText ();
			compileTargetCombo.PackStart (cr, true);
			compileTargetCombo.AddAttribute (cr, "text", 0);
			compileTargetCombo.Active = (int) configuration.CompileTarget;
			compileTargetCombo.Changed += new EventHandler (OnTargetChanged);
			
			
			// Load the codepage. If it matches any of the supported encodigs, use the encoding name 			
			string foundEncoding = null;
			foreach (TextEncoding e in TextEncoding.SupportedEncodings) {
				if (e.CodePage == -1)
					continue;
				if (e.CodePage == compilerParameters.CodePage)
					foundEncoding = e.Id;
				codepageEntry.AppendText (e.Id);
			}
			if (foundEncoding != null)
				codepageEntry.Entry.Text = foundEncoding;
			else if (compilerParameters.CodePage != 0)
				codepageEntry.Entry.Text = compilerParameters.CodePage.ToString ();
											
		}
		
		public bool ValidateChanges ()
		{
			if (codepageEntry.Entry.Text.Length > 0) {
				// Get the codepage. If the user specified an encoding name, find it.
				int trialCodePage = -1;
				foreach (TextEncoding e in TextEncoding.SupportedEncodings) {
					if (e.Id == codepageEntry.Entry.Text) {
						trialCodePage = e.CodePage;
						break;
					}
				}
			
				if (trialCodePage == -1) {
					if (!int.TryParse (codepageEntry.Entry.Text, out trialCodePage)) {
						MessageService.ShowError (GettextCatalog.GetString ("Invalid code page number."));
						return false;
					}
				}
			}
			return true;
		}
		
		public void Store ()
		{
			int codePage;
			CompileTarget compileTarget =  (CompileTarget) compileTargetCombo.Active;			
			
			if (codepageEntry.Entry.Text.Length > 0) {
				// Get the codepage. If the user specified an encoding name, find it.
				int trialCodePage = -1;
				foreach (TextEncoding e in TextEncoding.SupportedEncodings) {
					if (e.Id == codepageEntry.Entry.Text) {
						trialCodePage = e.CodePage;
						break;
					}
				}
			
				if (trialCodePage != -1)
					codePage = trialCodePage;
				else {
					if (!int.TryParse (codepageEntry.Entry.Text, out trialCodePage)) {
						return;
					}
					codePage = trialCodePage;
				}
			} else
				codePage = 0;
			
			project.CompileTarget = compileTarget;
			foreach (DotNetProjectConfiguration configuration in project.Configurations) {
				FSharpCompilerParameters compilerParameters = (FSharpCompilerParameters) configuration.CompilationParameters; 
				
				compilerParameters.CodePage = codePage;												
			}
		}
		
		void OnTargetChanged (object s, EventArgs a)
		{
			//UpdateTarget ();
		}		
		
	}
	
	public class CompilerOptionsPanel : ItemOptionsPanel
	{
		CompilerOptionsPanelWidget widget;
		
		public override Widget CreatePanelWidget ()
		{
			return (widget = new CompilerOptionsPanelWidget ((DotNetProject) ConfiguredProject));
		}
		
		public override bool ValidateChanges ()
		{
			return widget.ValidateChanges ();
		}
		
		public override void ApplyChanges ()
		{
			widget.Store ();
		}
	}
}
