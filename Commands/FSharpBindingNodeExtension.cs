//  FSharpBindingNodeExtension.cs
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
using System.Collections;
using System.IO;

using MonoDevelop.Core;
using MonoDevelop.Core.Gui;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.Gui.Pads;
using MonoDevelop.Ide.Gui.Pads.ProjectPad;
using MonoDevelop.Projects;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide.Gui.Components;    

namespace FSharpBinding
{
    public enum Commands {MoveUp, MoveDown}
    
	class FSharpBindingNodeExtension : NodeBuilderExtension
	{			
        private bool isFSharpProject = false;
        
		public override bool CanBuildNode (Type dataType)
		{
			return typeof(ProjectFile).IsAssignableFrom (dataType);     
		}
		
		public FSharpBindingNodeExtension ()
		{ 
		}	
		
        public override void BuildNode (ITreeBuilder builder, object dataObject, ref string label, ref Gdk.Pixbuf icon, ref Gdk.Pixbuf closedIcon)
		{
			if (!builder.Options["ShowFileOrder"])
				return;                                   
		
			// Add order status								
			if (dataObject is ProjectFile) {
				ProjectFile pfile = (ProjectFile) dataObject;
				Project prj = pfile.Project;
                                
                foreach (string lng in prj.SupportedLanguages){
                    if (lng.Equals("F#")) isFSharpProject = true;
                }
                if (isFSharpProject){                
                    int index = prj.Files.IndexOf(pfile)+1;                
                    if (index > 0)
                           label += " <b>["+index.ToString()+"]</b>";
                }
			} 
		}
        
		public override Type CommandHandlerType {
			get { return typeof(AddinCommandHandler); }
		}
	}
	
	class AddinCommandHandler : NodeCommandHandler 
	{
		[CommandHandler (Commands.MoveUp)]
		protected void OnMoveUp() {
			RunCommand(Commands.MoveUp);
		}
        
        [CommandUpdateHandler (Commands.MoveUp)]
		protected void UpdatenMoveUp(CommandInfo item) {            
            item.Visible = TestCommand();            
		}
		
        [CommandHandler (Commands.MoveDown)]
		protected void OnMoveDown() {
			RunCommand(Commands.MoveDown);
		}        
        
        [CommandUpdateHandler (Commands.MoveDown)]
		protected void UpdatenMoveDown(CommandInfo item) {            
            item.Visible = TestCommand();            
		}
        
        private bool TestCommand(){
            bool test = false;
            Project prj = ((ProjectFile)CurrentNode.DataItem).Project;
            
            foreach (string lng in prj.SupportedLanguages){
                if (lng.Equals("F#")) test = true;
            }
            return test;
        }
        
        private IProgressMonitor GetStatusMonitor ()
		{
			return IdeApp.Workbench.ProgressMonitors.GetStatusProgressMonitor("Saving new order of files in project","",true);
		}
        
		private void RunCommand(Commands cmd)
		{			
			if (CurrentNode.DataItem is ProjectFile) {
				ProjectFile file = (ProjectFile)CurrentNode.DataItem;
                Project prj = file.Project;
                ProjectFileCollection pfc = prj.Files;
                int currIndex = pfc.IndexOf(file); 
    			try {
    				switch (cmd) {
    					case Commands.MoveUp:
                            if (currIndex > 0) {
                                pfc.RemoveAt(currIndex);
                                pfc.Insert(currIndex-1, file); 
                            }    						
    						break;
    					case Commands.MoveDown:
    						if (currIndex < pfc.Count-1 ) {
                                pfc.RemoveAt(currIndex);
                                pfc.Insert(currIndex+1, file); 
                            }    						
    						break;
                        }
                    using (IProgressMonitor monitor = GetStatusMonitor()){ 
                        prj.Save(monitor); 
                    }
    			}
    			catch (Exception ex) {
    				LoggingService.LogError (ex.ToString ());    				
    			}  
            }
		}
	}
}
