// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace FSharpBinding {
    
    
    internal partial class GlobalOptionsPanelWidget {
        
        private Gtk.VBox vbox70;
        
        private Gtk.Label label96;
        
        private Gtk.HBox hbox63;
        
        private Gtk.Label label100;
        
        private Gtk.Table table9;
        
        private MonoDevelop.Components.FolderEntry fscPathEntry;
        
        private Gtk.Label label98;
        
        private Gtk.HBox hbox3;
        
        private Gtk.Label label2;
        
        private Gtk.Label label1;
        
        private Gtk.Entry fscCompilerFileName;
        
        private Gtk.HBox hbox4;
        
        private Gtk.CheckButton monoAddToCall;
        
        private Gtk.HBox hbox5;
        
        private Gtk.CheckButton fscProgress;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget FSharpBinding.GlobalOptionsPanelWidget
            Stetic.BinContainer.Attach(this);
            this.Name = "FSharpBinding.GlobalOptionsPanelWidget";
            // Container child FSharpBinding.GlobalOptionsPanelWidget.Gtk.Container+ContainerChild
            this.vbox70 = new Gtk.VBox();
            this.vbox70.Name = "vbox70";
            this.vbox70.Spacing = 12;
            // Container child vbox70.Gtk.Box+BoxChild
            this.label96 = new Gtk.Label();
            this.label96.Name = "label96";
            this.label96.Xalign = 0F;
            this.label96.LabelProp = Mono.Unix.Catalog.GetString("<b>Compiler</b>");
            this.label96.UseMarkup = true;
            this.vbox70.Add(this.label96);
            Gtk.Box.BoxChild w1 = ((Gtk.Box.BoxChild)(this.vbox70[this.label96]));
            w1.Position = 0;
            w1.Expand = false;
            w1.Fill = false;
            // Container child vbox70.Gtk.Box+BoxChild
            this.hbox63 = new Gtk.HBox();
            this.hbox63.Name = "hbox63";
            // Container child hbox63.Gtk.Box+BoxChild
            this.label100 = new Gtk.Label();
            this.label100.WidthRequest = 18;
            this.label100.Name = "label100";
            this.hbox63.Add(this.label100);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.hbox63[this.label100]));
            w2.Position = 0;
            w2.Expand = false;
            w2.Fill = false;
            // Container child hbox63.Gtk.Box+BoxChild
            this.table9 = new Gtk.Table(((uint)(1)), ((uint)(2)), false);
            this.table9.Name = "table9";
            this.table9.RowSpacing = ((uint)(6));
            this.table9.ColumnSpacing = ((uint)(6));
            // Container child table9.Gtk.Table+TableChild
            this.fscPathEntry = new MonoDevelop.Components.FolderEntry();
            this.fscPathEntry.Name = "fscPathEntry";
            this.table9.Add(this.fscPathEntry);
            Gtk.Table.TableChild w3 = ((Gtk.Table.TableChild)(this.table9[this.fscPathEntry]));
            w3.LeftAttach = ((uint)(1));
            w3.RightAttach = ((uint)(2));
            w3.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table9.Gtk.Table+TableChild
            this.label98 = new Gtk.Label();
            this.label98.Name = "label98";
            this.label98.Xalign = 0F;
            this.label98.LabelProp = Mono.Unix.Catalog.GetString("F# compiler path:");
            this.table9.Add(this.label98);
            Gtk.Table.TableChild w4 = ((Gtk.Table.TableChild)(this.table9[this.label98]));
            w4.XOptions = ((Gtk.AttachOptions)(4));
            w4.YOptions = ((Gtk.AttachOptions)(0));
            this.hbox63.Add(this.table9);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.hbox63[this.table9]));
            w5.Position = 1;
            this.vbox70.Add(this.hbox63);
            Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.vbox70[this.hbox63]));
            w6.Position = 1;
            w6.Expand = false;
            w6.Fill = false;
            // Container child vbox70.Gtk.Box+BoxChild
            this.hbox3 = new Gtk.HBox();
            this.hbox3.Name = "hbox3";
            this.hbox3.Spacing = 6;
            // Container child hbox3.Gtk.Box+BoxChild
            this.label2 = new Gtk.Label();
            this.label2.Name = "label2";
            this.hbox3.Add(this.label2);
            Gtk.Box.BoxChild w7 = ((Gtk.Box.BoxChild)(this.hbox3[this.label2]));
            w7.Position = 0;
            w7.Expand = false;
            w7.Fill = false;
            w7.Padding = ((uint)(7));
            // Container child hbox3.Gtk.Box+BoxChild
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.LabelProp = Mono.Unix.Catalog.GetString("F# compiler file name:");
            this.hbox3.Add(this.label1);
            Gtk.Box.BoxChild w8 = ((Gtk.Box.BoxChild)(this.hbox3[this.label1]));
            w8.Position = 1;
            w8.Expand = false;
            w8.Fill = false;
            // Container child hbox3.Gtk.Box+BoxChild
            this.fscCompilerFileName = new Gtk.Entry();
            this.fscCompilerFileName.CanFocus = true;
            this.fscCompilerFileName.Name = "fscCompilerFileName";
            this.fscCompilerFileName.IsEditable = true;
            this.fscCompilerFileName.InvisibleChar = '●';
            this.hbox3.Add(this.fscCompilerFileName);
            Gtk.Box.BoxChild w9 = ((Gtk.Box.BoxChild)(this.hbox3[this.fscCompilerFileName]));
            w9.Position = 2;
            this.vbox70.Add(this.hbox3);
            Gtk.Box.BoxChild w10 = ((Gtk.Box.BoxChild)(this.vbox70[this.hbox3]));
            w10.Position = 2;
            w10.Expand = false;
            w10.Fill = false;
            // Container child vbox70.Gtk.Box+BoxChild
            this.hbox4 = new Gtk.HBox();
            this.hbox4.Name = "hbox4";
            this.hbox4.Spacing = 6;
            // Container child hbox4.Gtk.Box+BoxChild
            this.monoAddToCall = new Gtk.CheckButton();
            this.monoAddToCall.CanFocus = true;
            this.monoAddToCall.Name = "monoAddToCall";
            this.monoAddToCall.Label = Mono.Unix.Catalog.GetString("Add 'mono' to compiler call");
            this.monoAddToCall.Active = true;
            this.monoAddToCall.DrawIndicator = true;
            this.monoAddToCall.UseUnderline = true;
            this.hbox4.Add(this.monoAddToCall);
            Gtk.Box.BoxChild w11 = ((Gtk.Box.BoxChild)(this.hbox4[this.monoAddToCall]));
            w11.Position = 0;
            w11.Padding = ((uint)(15));
            this.vbox70.Add(this.hbox4);
            Gtk.Box.BoxChild w12 = ((Gtk.Box.BoxChild)(this.vbox70[this.hbox4]));
            w12.Position = 3;
            w12.Expand = false;
            w12.Fill = false;
            // Container child vbox70.Gtk.Box+BoxChild
            this.hbox5 = new Gtk.HBox();
            this.hbox5.Name = "hbox5";
            this.hbox5.Spacing = 6;
            // Container child hbox5.Gtk.Box+BoxChild
            this.fscProgress = new Gtk.CheckButton();
            this.fscProgress.CanFocus = true;
            this.fscProgress.Name = "fscProgress";
            this.fscProgress.Label = Mono.Unix.Catalog.GetString("Show compilation progress");
            this.fscProgress.DrawIndicator = true;
            this.fscProgress.UseUnderline = true;
            this.hbox5.Add(this.fscProgress);
            Gtk.Box.BoxChild w13 = ((Gtk.Box.BoxChild)(this.hbox5[this.fscProgress]));
            w13.Position = 0;
            w13.Padding = ((uint)(15));
            this.vbox70.Add(this.hbox5);
            Gtk.Box.BoxChild w14 = ((Gtk.Box.BoxChild)(this.vbox70[this.hbox5]));
            w14.Position = 4;
            w14.Expand = false;
            w14.Fill = false;
            this.Add(this.vbox70);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.Show();
        }
    }
}
