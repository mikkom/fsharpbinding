
using System;

using MonoDevelop.Projects.Gui.Completion;
using MonoDevelop.Projects.Parser;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects.Text;
using MonoDevelop.Projects.Ambience;

using FSharpBinding.FormattingStrategy;

namespace FSharpBinding
{
	public class FSharpParameterDataProvider : MethodParameterDataProvider
	{
		TextEditor editor;
		FSharpAmbience ambience = new FSharpAmbience ();
		
		public FSharpParameterDataProvider (TextEditor editor, MethodParameterDataProvider.Scope scope, IClass cls, string methodName): base (cls, methodName, scope)
		{
			this.editor = editor;
		}
		
		public FSharpParameterDataProvider (TextEditor editor, MethodParameterDataProvider.Scope scope, IClass cls): base (cls, scope)
		{
			this.editor = editor;
		}
		
		public override string GetParameterMarkup (int overload, int paramIndex)
		{
			IParameter p = GetParameter (overload, paramIndex);
			return ambience.Convert (p, ConversionFlags.ShowParameterNames | ConversionFlags.ShowGenericParameters | ConversionFlags.IncludePangoMarkup | ConversionFlags.UseIntrinsicTypeNames);
		}
		
		public override string GetMethodMarkup (int overload, string[] parameters)
		{
			IMethod met = GetMethod (overload);
			
			string paramTxt = string.Join (", ", parameters);
			if (met.IsConstructor)
				return "<b>" + GLib.Markup.EscapeText (ambience.Convert (met.DeclaringType, ConversionFlags.UseIntrinsicTypeNames)) + "</b> (" + paramTxt + ")";
			else
				return GLib.Markup.EscapeText (ambience.Convert (met.ReturnType, ConversionFlags.ShowGenericParameters|ConversionFlags.UseIntrinsicTypeNames)) + " <b>" + met.Name + "</b> (" + paramTxt + ")";
		}
		
		public override int GetCurrentParameterIndex (ICodeCompletionContext ctx)
		{
			return GetCurrentParameterIndex (editor, ctx.TriggerOffset);
		}
		
		public static int GetCurrentParameterIndex (TextEditor editor, int triggerOffset)
		{
			int cursor = editor.CursorPosition;
			int i = triggerOffset;
			
			if (i > cursor)
				return -1;
			else if (i == cursor)
				return 0;
			
			FSharpIndentEngine engine = new FSharpIndentEngine ();
			int index = 1;
			
			do {
				char c = editor.GetCharAt (i - 1);
				
				engine.Push (c);
				
				if (c == ',' && engine.StackDepth == 1)
					index++;
				
				i++;
			} while (i <= cursor && engine.StackDepth > 0);
			
			if (engine.StackDepth == 0)
				return -1;
			else
				return index;
		}
	}
}
