// GlobalOptionsPanel.cs
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
