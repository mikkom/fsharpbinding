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
    
    
    public partial class CompilerOptionsPanelWidget {
        
        private Gtk.VBox vbox1;
        
        private Gtk.Label label82;
        
        private Gtk.HBox hbox5;
        
        private Gtk.Label label76;
        
        private Gtk.VBox vbox2;
        
        private Gtk.Table table7;
        
        private Gtk.HBox hbox57;
        
        private Gtk.ComboBox compileTargetCombo;
        
        private Gtk.HBox hbox6;
        
        private Gtk.ComboBoxEntry codepageEntry;
        
        private Gtk.Label label1;
        
        private Gtk.Label label86;
        
        private Gtk.Label label75;
        
        private Gtk.HBox hbox7;
        
        private Gtk.Label label74;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget FSharpBinding.CompilerOptionsPanelWidget
            Stetic.BinContainer.Attach(this);
            this.Name = "FSharpBinding.CompilerOptionsPanelWidget";
            // Container child FSharpBinding.CompilerOptionsPanelWidget.Gtk.Container+ContainerChild
            this.vbox1 = new Gtk.VBox();
            this.vbox1.Name = "vbox1";
            this.vbox1.Spacing = 6;
            // Container child vbox1.Gtk.Box+BoxChild
            this.label82 = new Gtk.Label();
            this.label82.Name = "label82";
            this.label82.Xalign = 0F;
            this.label82.LabelProp = Mono.Unix.Catalog.GetString("<b>Code Generation</b>");
            this.label82.UseMarkup = true;
            this.vbox1.Add(this.label82);
            Gtk.Box.BoxChild w1 = ((Gtk.Box.BoxChild)(this.vbox1[this.label82]));
            w1.Position = 0;
            w1.Expand = false;
            w1.Fill = false;
            // Container child vbox1.Gtk.Box+BoxChild
            this.hbox5 = new Gtk.HBox();
            this.hbox5.Name = "hbox5";
            this.hbox5.Spacing = 6;
            // Container child hbox5.Gtk.Box+BoxChild
            this.label76 = new Gtk.Label();
            this.label76.WidthRequest = 18;
            this.label76.Name = "label76";
            this.hbox5.Add(this.label76);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.hbox5[this.label76]));
            w2.Position = 0;
            w2.Expand = false;
            w2.Fill = false;
            // Container child hbox5.Gtk.Box+BoxChild
            this.vbox2 = new Gtk.VBox();
            this.vbox2.Name = "vbox2";
            this.vbox2.Spacing = 6;
            // Container child vbox2.Gtk.Box+BoxChild
            this.table7 = new Gtk.Table(((uint)(2)), ((uint)(2)), false);
            this.table7.Name = "table7";
            this.table7.RowSpacing = ((uint)(6));
            this.table7.ColumnSpacing = ((uint)(6));
            // Container child table7.Gtk.Table+TableChild
            this.hbox57 = new Gtk.HBox();
            this.hbox57.Name = "hbox57";
            // Container child hbox57.Gtk.Box+BoxChild
            this.compileTargetCombo = new Gtk.ComboBox();
            this.compileTargetCombo.Name = "compileTargetCombo";
            this.hbox57.Add(this.compileTargetCombo);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.hbox57[this.compileTargetCombo]));
            w3.Position = 0;
            w3.Expand = false;
            w3.Fill = false;
            this.table7.Add(this.hbox57);
            Gtk.Table.TableChild w4 = ((Gtk.Table.TableChild)(this.table7[this.hbox57]));
            w4.LeftAttach = ((uint)(1));
            w4.RightAttach = ((uint)(2));
            w4.XOptions = ((Gtk.AttachOptions)(4));
            w4.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table7.Gtk.Table+TableChild
            this.hbox6 = new Gtk.HBox();
            this.hbox6.Name = "hbox6";
            this.hbox6.Spacing = 6;
            // Container child hbox6.Gtk.Box+BoxChild
            this.codepageEntry = Gtk.ComboBoxEntry.NewText();
            this.codepageEntry.Name = "codepageEntry";
            this.hbox6.Add(this.codepageEntry);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.hbox6[this.codepageEntry]));
            w5.Position = 0;
            w5.Expand = false;
            w5.Fill = false;
            this.table7.Add(this.hbox6);
            Gtk.Table.TableChild w6 = ((Gtk.Table.TableChild)(this.table7[this.hbox6]));
            w6.TopAttach = ((uint)(1));
            w6.BottomAttach = ((uint)(2));
            w6.LeftAttach = ((uint)(1));
            w6.RightAttach = ((uint)(2));
            w6.XOptions = ((Gtk.AttachOptions)(4));
            w6.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table7.Gtk.Table+TableChild
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.Xalign = 0F;
            this.label1.LabelProp = Mono.Unix.Catalog.GetString("Compiler Code Page:");
            this.table7.Add(this.label1);
            Gtk.Table.TableChild w7 = ((Gtk.Table.TableChild)(this.table7[this.label1]));
            w7.TopAttach = ((uint)(1));
            w7.BottomAttach = ((uint)(2));
            w7.XOptions = ((Gtk.AttachOptions)(4));
            w7.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table7.Gtk.Table+TableChild
            this.label86 = new Gtk.Label();
            this.label86.Name = "label86";
            this.label86.Xalign = 0F;
            this.label86.LabelProp = Mono.Unix.Catalog.GetString("Compile _Target:");
            this.label86.UseUnderline = true;
            this.table7.Add(this.label86);
            Gtk.Table.TableChild w8 = ((Gtk.Table.TableChild)(this.table7[this.label86]));
            w8.XOptions = ((Gtk.AttachOptions)(4));
            w8.YOptions = ((Gtk.AttachOptions)(0));
            this.vbox2.Add(this.table7);
            Gtk.Box.BoxChild w9 = ((Gtk.Box.BoxChild)(this.vbox2[this.table7]));
            w9.Position = 0;
            this.hbox5.Add(this.vbox2);
            Gtk.Box.BoxChild w10 = ((Gtk.Box.BoxChild)(this.hbox5[this.vbox2]));
            w10.Position = 1;
            this.vbox1.Add(this.hbox5);
            Gtk.Box.BoxChild w11 = ((Gtk.Box.BoxChild)(this.vbox1[this.hbox5]));
            w11.Position = 1;
            w11.Expand = false;
            // Container child vbox1.Gtk.Box+BoxChild
            this.label75 = new Gtk.Label();
            this.label75.WidthRequest = 18;
            this.label75.Name = "label75";
            this.vbox1.Add(this.label75);
            Gtk.Box.BoxChild w12 = ((Gtk.Box.BoxChild)(this.vbox1[this.label75]));
            w12.PackType = ((Gtk.PackType)(1));
            w12.Position = 2;
            // Container child vbox1.Gtk.Box+BoxChild
            this.hbox7 = new Gtk.HBox();
            this.hbox7.Name = "hbox7";
            this.hbox7.Spacing = 6;
            // Container child hbox7.Gtk.Box+BoxChild
            this.label74 = new Gtk.Label();
            this.label74.WidthRequest = 18;
            this.label74.Name = "label74";
            this.hbox7.Add(this.label74);
            Gtk.Box.BoxChild w13 = ((Gtk.Box.BoxChild)(this.hbox7[this.label74]));
            w13.Position = 0;
            w13.Expand = false;
            w13.Fill = false;
            this.vbox1.Add(this.hbox7);
            Gtk.Box.BoxChild w14 = ((Gtk.Box.BoxChild)(this.vbox1[this.hbox7]));
            w14.PackType = ((Gtk.PackType)(1));
            w14.Position = 3;
            w14.Expand = false;
            w14.Fill = false;
            this.Add(this.vbox1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.Show();
        }
    }
}
