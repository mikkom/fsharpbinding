// GlobalOptionsPanel.cs
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

using MonoDevelop.Projects;
using MonoDevelop.Core.Gui.Dialogs;
using MonoDevelop.Components;
using MonoDevelop.Core;


namespace FSharpBinding
{
	public partial class GlobalOptionsPanelPanel : OptionsPanel
	{
		GlobalOptionsPanelWidget widget;
		
		public override Widget CreatePanelWidget ()
		{
			widget = new GlobalOptionsPanelWidget();
			return widget;
		}
		
		public override void ApplyChanges ()
		{
			widget.Store ();
		}
	}

	partial class GlobalOptionsPanelWidget : Gtk.Bin 
	{
		public GlobalOptionsPanelWidget ()
		{
			Build ();
						
			fscPathEntry.Path = FSharpLanguageBinding.Properties.fscPath;			
            fscCompilerFileName.Text = FSharpLanguageBinding.Properties.fscFileName;
			monoAddToCall.Active = FSharpLanguageBinding.Properties.addMonoToFSCcall;			
            fscProgress.Active = FSharpLanguageBinding.Properties.compilationProgress;			                                    
		}
		
		public bool Store ()
		{
			FSharpLanguageBinding.Properties.fscPath = fscPathEntry.Path;
			FSharpLanguageBinding.Properties.fscFileName = fscCompilerFileName.Text;
            FSharpLanguageBinding.Properties.addMonoToFSCcall = monoAddToCall.Active;
            FSharpLanguageBinding.Properties.compilationProgress = fscProgress.Active;	
            		
			return true;
		}
	}
}
