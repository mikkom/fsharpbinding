
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
