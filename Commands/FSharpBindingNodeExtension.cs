//  FSharpBindingNodeExtension.cs
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
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

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
