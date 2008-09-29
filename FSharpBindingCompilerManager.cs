//  FSharpBindingCompilerManager.cs
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
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.CodeDom.Compiler;
using System.Threading;

using MonoDevelop.Core;
using MonoDevelop.Core.Execution;
using MonoDevelop.Projects;

namespace FSharpBinding
{
	/// <summary>
	/// This class controls the compilation of C Sharp files and C Sharp projects
	/// </summary>
	public class FSharpBindingCompilerManager
	{	
		public bool CanCompile(string fileName)
		{
            string ext = Path.GetExtension(fileName);
			return (ext.Equals(".fs", StringComparison.CurrentCultureIgnoreCase) ||
			        ext.Equals(".fsi", StringComparison.CurrentCultureIgnoreCase));
		}
        
        private void AddOption(StringWriter writer, string option)
        {
            writer.Write(option + " ");
        }
        

		public BuildResult Compile (ProjectFileCollection projectFiles, ProjectReferenceCollection references, DotNetProjectConfiguration configuration, IProgressMonitor monitor)
		{
			FSharpCompilerParameters compilerparameters = (FSharpCompilerParameters) configuration.CompilationParameters;
			if (compilerparameters == null) compilerparameters = new FSharpCompilerParameters ();
			
            	string responseFileName = Path.GetTempFileName();
			StringWriter writer = new StringWriter ();
			ArrayList gacRoots = new ArrayList ();
            
            AddOption(writer, "--fullpaths");
            AddOption(writer, "--utf8output");
                    
			if (references != null) {
				foreach (ProjectReference lib in references) {
					if ((lib.ReferenceType == ReferenceType.Project) &&
					    (!(lib.OwnerProject.ParentSolution.FindProjectByName (lib.Reference) is DotNetProject)))
						continue;
					foreach (string fileName in lib.GetReferencedFileNames (configuration.Id)) {
						switch (lib.ReferenceType) {
						case ReferenceType.Gac:
							SystemPackage pkg = Runtime.SystemAssemblyService.GetPackageFromFullName (lib.Reference);
							if (pkg == null) {
								string msg = String.Format (GettextCatalog.GetString ("{0} could not be found or is invalid."), lib.Reference);
								monitor.ReportWarning (msg);
								continue;
							}
                            else if (pkg.IsInternalPackage) {
								AddOption(writer,"-r \"" + fileName + "\"");
							} 
							if (pkg.GacRoot != null && !gacRoots.Contains (pkg.GacRoot))
								gacRoots.Add (pkg.GacRoot);
							break;
						default:
							AddOption(writer,"-r \"" + fileName + "\"");
							break;
						}
					}
				}
			}
            
            string exe = configuration.CompiledOutputName;
			AddOption(writer,"--out \"" + exe + '"');
				
			if (configuration.SignAssembly) {
				if (File.Exists (configuration.AssemblyKeyFile))
					AddOption(writer,"--keyfile \"" + configuration.AssemblyKeyFile + '"');
			}
			
			if (configuration.DebugMode) {
				AddOption(writer,"--debug");				
			}
            else
				if (compilerparameters.Optimize)
                    AddOption(writer,"--optimize");
			
			if (compilerparameters.CodePage != 0) {
				AddOption(writer,"--codepage " + compilerparameters.CodePage);
			}						
			
			if (compilerparameters.TreatWarningsAsErrors) {
				AddOption(writer,"--warnaserror");
			}
			
			if (compilerparameters.DefineSymbols.Length > 0) {
				AddOption(writer,"--define " + compilerparameters.DefineSymbols);
			}			
			
			switch (configuration.CompileTarget) {
				case CompileTarget.Exe:
					AddOption(writer,"--target exe");
					break;
				case CompileTarget.WinExe:
					AddOption(writer,"--target winexe");
					break;
				case CompileTarget.Library:
					AddOption(writer,"--target library");
					break;
                case CompileTarget.Module:
					AddOption(writer,"--target module");
					break;
			}
            
            if (compilerparameters.GenerateXmlDocumentation) {
				AddOption(writer,"-doc \"" + Path.ChangeExtension(exe, ".xml") + '"');
			}
			
			if (!string.IsNullOrEmpty (compilerparameters.AdditionalArguments)) {
				AddOption(writer,compilerparameters.AdditionalArguments);
			}
			
			if (!string.IsNullOrEmpty (compilerparameters.NoWarnings)) {
				AddOption(writer, String.Format("--nowarn {0}", compilerparameters.NoWarnings));
			}
			
			foreach (ProjectFile finfo in projectFiles) {
				if (finfo.Subtype == Subtype.Directory)
					continue;

				switch (finfo.BuildAction) {
					case BuildAction.Compile:
						AddOption(writer,'"' + finfo.Name + '"');
						break;
//					case BuildAction.EmbedAsResource:
//						string fname = finfo.Name;
//						if (String.Compare (Path.GetExtension (fname), ".resx", true) == 0)
//							fname = Path.ChangeExtension (fname, ".resources");
//
//						AddOption(writer,@"""/res:{0},{1}""", fname, finfo.ResourceId);
//						break;
					default:
						continue;
				}
			}
            
			writer.Close();

			string output = String.Empty;
			string error  = String.Empty;
			
			File.WriteAllText (responseFileName, writer.ToString ());
			
			string compilerName;
			try {
				compilerName = GetCompilerName (configuration.ClrVersion);
			} catch (Exception e) {
				string message = "Could not obtain F# compiler";
				monitor.ReportError (message, e);
				return null;
			}
			
			//string outstr = compilerName + " @" + responseFileName;
            string outstr = compilerName + " "+ writer.ToString ();
			TempFileCollection tf = new TempFileCollection();

			string workingDir = ".";
			if (projectFiles != null && projectFiles.Count > 0) {
				workingDir = projectFiles [0].Project.BaseDirectory;
				if (workingDir == null)
					// Dummy projects created for single files have no filename
					// and so no BaseDirectory.
					// This is a workaround for a bug in 
					// ProcessStartInfo.WorkingDirectory - not able to handle null
					workingDir = ".";
			}

			LoggingService.LogInfo (compilerName + " " + writer.ToString ());
			
			int exitCode = DoCompilation (outstr, tf, workingDir, gacRoots, ref output, ref error);
			
			BuildResult result = ParseOutput(tf, output, error);
			if (result.CompilerOutput.Trim ().Length != 0)
				monitor.Log.WriteLine (result.CompilerOutput);
			
			//if compiler crashes, output entire error string
			if (result.ErrorCount == 0 && exitCode != 0) {
				if (!string.IsNullOrEmpty (error))
					result.AddError (error);
				else
					result.AddError ("The compiler appears to have crashed without any error output.");
			}
			
			FileService.DeleteFile (responseFileName);
			FileService.DeleteFile (output);
			FileService.DeleteFile (error);
			return result;
		}

		string GetCompilerName (ClrVersion version)
		{
			string fsc;
            if (FSharpLanguageBinding.Properties.fscFileName.Length>0)
                fsc = FSharpLanguageBinding.Properties.fscFileName;
            else {
    			switch (version) {
    			case ClrVersion.Net_1_1:
    			case ClrVersion.Net_2_0:
    				fsc = "fsc.exe";
    				break;
    			default:                
    				string message = "Cannot handle unknown runtime version ClrVersion.'" + version.ToString () + "'.";
    				LoggingService.LogError (message);
    				throw new Exception (message);                        				
    			}
            }
			
			string compilerName = "";
			if (FSharpLanguageBinding.Properties.addMonoToFSCcall)
				compilerName = "mono ";
			compilerName += Path.Combine (FSharpLanguageBinding.Properties.fscPath, fsc);			
			return compilerName;
		}
		
		BuildResult ParseOutput(TempFileCollection tf, string stdout, string stderr)
		{
			StringBuilder compilerOutput = new StringBuilder();
			
			CompilerResults cr = new CompilerResults(tf);
			
			// we have 2 formats for the error output the csc gives :
			//Regex normalError  = new Regex(@"(?<file>.*)\((?<line>\d+),(?<column>\d+)\):\s+(?<error>\w+)\s+(?<number>[\d\w]+):\s+(?<message>.*)", RegexOptions.Compiled);
			//Regex generalError = new Regex(@"(?<error>.+)\s+(?<number>[\d\w]+):\s+(?<message>.*)", RegexOptions.Compiled);
			
			bool typeLoadException = false;
			
			foreach (string s in new string[] { stdout, stderr }) {
				StreamReader sr = File.OpenText (s);
				while (true) {
					if (typeLoadException) {
						compilerOutput.Append (sr.ReadToEnd ());
						break;
					}
					string curLine = sr.ReadLine();
					compilerOutput.Append(curLine);
					compilerOutput.Append('\n');
					if (curLine == null) {
						break;
					}
					curLine = curLine.Trim();
					if (curLine.Length == 0) {
						continue;
					}

					if (curLine.StartsWith ("Unhandled Exception: System.TypeLoadException")) {
						cr.Errors.Clear ();
						typeLoadException = true;
					}
					
					CompilerError error = CreateErrorFromString (curLine);
					
					if (error != null)
						cr.Errors.Add (error);
				}
				sr.Close();
			}
			if (typeLoadException) {
				Regex reg  = new Regex(@".*WARNING.*used in (mscorlib|System),.*", RegexOptions.Multiline);
				if (reg.Match (compilerOutput.ToString ()).Success)
					cr.Errors.Add (new CompilerError (String.Empty, 0, 0, String.Empty, "Error: A referenced assembly may be built with an incompatible CLR version. See the compilation output for more details."));
				else
					cr.Errors.Add (new CompilerError (String.Empty, 0, 0, String.Empty, "Error: A dependency of a referenced assembly may be missing, or you may be referencing an assembly created with a newer CLR version. See the compilation output for more details."));
			}
			
			return new BuildResult(cr, compilerOutput.ToString());
		}
		
		private int DoCompilation (string outstr, TempFileCollection tf, string working_dir, ArrayList gacRoots, ref string output, ref string error) {
			output = Path.GetTempFileName();
			error = Path.GetTempFileName();
			
			StreamWriter outwr = new StreamWriter(output);
			StreamWriter errwr = new StreamWriter(error);
			string[] tokens = outstr.Split(' ');
			
			outstr = outstr.Substring(tokens[0].Length+1);

			ProcessStartInfo pinfo = new ProcessStartInfo (tokens[0], "\"" + outstr + "\"");
			pinfo.WorkingDirectory = working_dir;
			
			if (gacRoots.Count > 0) {
				// Create the gac prefix string
				string gacPrefix = string.Join ("" + Path.PathSeparator, (string[])gacRoots.ToArray (typeof(string)));
				string oldGacVar = Environment.GetEnvironmentVariable ("MONO_GAC_PREFIX");
				if (!string.IsNullOrEmpty (oldGacVar))
					gacPrefix += Path.PathSeparator + oldGacVar;
				pinfo.EnvironmentVariables ["MONO_GAC_PREFIX"] = gacPrefix;
			}
			pinfo.UseShellExecute = false;
			pinfo.RedirectStandardOutput = true;
			pinfo.RedirectStandardError = true;
			
			ProcessWrapper pw = Runtime.ProcessService.StartProcess (pinfo, outwr, errwr, null);
			pw.WaitForOutput();
			int exitCode = pw.ExitCode;
			outwr.Close();
			errwr.Close();
			pw.Dispose ();
			return exitCode;
		}


		// Snatched from our codedom code :-).
		static Regex regexError = new Regex (@"^(\s*(?<file>.*)\((?<line>\d*)(,(?<column>\d*[\+]*))?\)(:|)\s+)*(?<level>\w+)\s*(?<number>.*\d):\s*(?<message>.*)",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);

		private static CompilerError CreateErrorFromString(string error_string)
		{
			// When IncludeDebugInformation is true, prevents the debug symbols stats from braeking this.
			if (error_string.StartsWith ("WROTE SYMFILE") ||
			    error_string.StartsWith ("OffsetTable") ||
			    error_string.StartsWith ("Compilation succeeded") ||
			    error_string.StartsWith ("Compilation failed"))
				return null;

			CompilerError error = new CompilerError();

			Match match=regexError.Match(error_string);
			if (!match.Success) return null;
			if (String.Empty != match.Result("${file}"))
				error.FileName=match.Result("${file}");
			if (String.Empty != match.Result("${line}"))
				error.Line=Int32.Parse(match.Result("${line}"));
			if (String.Empty != match.Result("${column}")) {
				if (match.Result("${column}") == "255+")
					error.Column = -1;
				else
					error.Column=Int32.Parse(match.Result("${column}"));
			}
			if (match.Result("${level}")=="warning")
				error.IsWarning=true;
			error.ErrorNumber=match.Result("${number}");
			error.ErrorText=match.Result("${message}");
			return error;
		}

	}
}
