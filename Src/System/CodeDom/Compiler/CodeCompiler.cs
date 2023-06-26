using System;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides an example implementation of the <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> interface.</summary>
	// Token: 0x0200066B RID: 1643
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class CodeCompiler : CodeGenerator, ICodeCompiler
	{
		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromDom(System.CodeDom.Compiler.CompilerParameters,System.CodeDom.CodeCompileUnit)" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that indicates the source to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.</exception>
		// Token: 0x06003B71 RID: 15217 RVA: 0x000F5298 File Offset: 0x000F3498
		CompilerResults ICodeCompiler.CompileAssemblyFromDom(CompilerParameters options, CodeCompileUnit e)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromDom(options, e);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromFile(System.CodeDom.Compiler.CompilerParameters,System.String)" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="fileName">The file name to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.</exception>
		// Token: 0x06003B72 RID: 15218 RVA: 0x000F52DC File Offset: 0x000F34DC
		CompilerResults ICodeCompiler.CompileAssemblyFromFile(CompilerParameters options, string fileName)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromFile(options, fileName);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromSource(System.CodeDom.Compiler.CompilerParameters,System.String)" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="source">A string that indicates the source code to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.</exception>
		// Token: 0x06003B73 RID: 15219 RVA: 0x000F5320 File Offset: 0x000F3520
		CompilerResults ICodeCompiler.CompileAssemblyFromSource(CompilerParameters options, string source)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromSource(options, source);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromSourceBatch(System.CodeDom.Compiler.CompilerParameters,System.String[])" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="sources">An array of strings that indicates the source code to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.</exception>
		// Token: 0x06003B74 RID: 15220 RVA: 0x000F5364 File Offset: 0x000F3564
		CompilerResults ICodeCompiler.CompileAssemblyFromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromSourceBatch(options, sources);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromFileBatch(System.CodeDom.Compiler.CompilerParameters,System.String[])" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="fileNames">An array of strings that indicates the file names to compile.</param>
		/// <returns>The results of compilation.</returns>
		// Token: 0x06003B75 RID: 15221 RVA: 0x000F53A8 File Offset: 0x000F35A8
		CompilerResults ICodeCompiler.CompileAssemblyFromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			CompilerResults compilerResults;
			try
			{
				foreach (string text in fileNames)
				{
					using (File.OpenRead(text))
					{
					}
				}
				compilerResults = this.FromFileBatch(options, fileNames);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromDomBatch(System.CodeDom.Compiler.CompilerParameters,System.CodeDom.CodeCompileUnit[])" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="ea">An array of <see cref="T:System.CodeDom.CodeCompileUnit" /> objects that indicates the source to compile.</param>
		/// <returns>The results of compilation.</returns>
		// Token: 0x06003B76 RID: 15222 RVA: 0x000F5430 File Offset: 0x000F3630
		CompilerResults ICodeCompiler.CompileAssemblyFromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromDomBatch(options, ea);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		/// <summary>Gets the file name extension to use for source files.</summary>
		/// <returns>The file name extension to use for source files.</returns>
		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06003B77 RID: 15223
		protected abstract string FileExtension { get; }

		/// <summary>Gets the name of the compiler executable.</summary>
		/// <returns>The name of the compiler executable.</returns>
		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06003B78 RID: 15224
		protected abstract string CompilerName { get; }

		// Token: 0x06003B79 RID: 15225 RVA: 0x000F5474 File Offset: 0x000F3674
		internal void Compile(CompilerParameters options, string compilerDirectory, string compilerExe, string arguments, ref string outputFile, ref int nativeReturnValue, string trueArgs)
		{
			string text = null;
			outputFile = options.TempFiles.AddExtension("out");
			string text2 = Path.Combine(compilerDirectory, compilerExe);
			if (File.Exists(text2))
			{
				string text3 = null;
				if (trueArgs != null)
				{
					text3 = "\"" + text2 + "\" " + trueArgs;
				}
				nativeReturnValue = Executor.ExecWaitWithCapture(options.SafeUserToken, "\"" + text2 + "\" " + arguments, Environment.CurrentDirectory, options.TempFiles, ref outputFile, ref text, text3);
				return;
			}
			throw new InvalidOperationException(SR.GetString("CompilerNotFound", new object[] { text2 }));
		}

		/// <summary>Compiles the specified compile unit using the specified options, and returns the results from the compilation.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> object that indicates the source to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.</exception>
		// Token: 0x06003B7A RID: 15226 RVA: 0x000F550C File Offset: 0x000F370C
		protected virtual CompilerResults FromDom(CompilerParameters options, CodeCompileUnit e)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			return this.FromDomBatch(options, new CodeCompileUnit[] { e });
		}

		/// <summary>Compiles the specified file using the specified options, and returns the results from the compilation.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="fileName">The file name to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="fileName" /> is <see langword="null" />.</exception>
		// Token: 0x06003B7B RID: 15227 RVA: 0x000F5548 File Offset: 0x000F3748
		protected virtual CompilerResults FromFile(CompilerParameters options, string fileName)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			using (File.OpenRead(fileName))
			{
			}
			return this.FromFileBatch(options, new string[] { fileName });
		}

		/// <summary>Compiles the specified source code string using the specified options, and returns the results from the compilation.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="source">The source code string to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.</exception>
		// Token: 0x06003B7C RID: 15228 RVA: 0x000F55B4 File Offset: 0x000F37B4
		protected virtual CompilerResults FromSource(CompilerParameters options, string source)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			return this.FromSourceBatch(options, new string[] { source });
		}

		/// <summary>Compiles the specified compile units using the specified options, and returns the results from the compilation.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="ea">An array of <see cref="T:System.CodeDom.CodeCompileUnit" /> objects that indicates the source to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="ea" /> is <see langword="null" />.</exception>
		// Token: 0x06003B7D RID: 15229 RVA: 0x000F55F0 File Offset: 0x000F37F0
		protected virtual CompilerResults FromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (ea == null)
			{
				throw new ArgumentNullException("ea");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			string[] array = new string[ea.Length];
			CompilerResults compilerResults = null;
			try
			{
				WindowsImpersonationContext windowsImpersonationContext = Executor.RevertImpersonation();
				try
				{
					for (int i = 0; i < ea.Length; i++)
					{
						if (ea[i] != null)
						{
							this.ResolveReferencedAssemblies(options, ea[i]);
							array[i] = options.TempFiles.AddExtension(i.ToString() + this.FileExtension);
							Stream stream = new FileStream(array[i], FileMode.Create, FileAccess.Write, FileShare.Read);
							try
							{
								using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
								{
									((ICodeGenerator)this).GenerateCodeFromCompileUnit(ea[i], streamWriter, base.Options);
									streamWriter.Flush();
								}
							}
							finally
							{
								stream.Close();
							}
						}
					}
					compilerResults = this.FromFileBatch(options, array);
				}
				finally
				{
					Executor.ReImpersonate(windowsImpersonationContext);
				}
			}
			catch
			{
				throw;
			}
			return compilerResults;
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x000F570C File Offset: 0x000F390C
		private void ResolveReferencedAssemblies(CompilerParameters options, CodeCompileUnit e)
		{
			if (e.ReferencedAssemblies.Count > 0)
			{
				foreach (string text in e.ReferencedAssemblies)
				{
					if (!options.ReferencedAssemblies.Contains(text))
					{
						options.ReferencedAssemblies.Add(text);
					}
				}
			}
		}

		/// <summary>Compiles the specified files using the specified options, and returns the results from the compilation.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="fileNames">An array of strings that indicates the file names of the files to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="fileNames" /> is <see langword="null" />.</exception>
		// Token: 0x06003B7F RID: 15231 RVA: 0x000F5784 File Offset: 0x000F3984
		protected virtual CompilerResults FromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			string text = null;
			int num = 0;
			CompilerResults compilerResults = new CompilerResults(options.TempFiles);
			SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.ControlEvidence);
			securityPermission.Assert();
			try
			{
				compilerResults.Evidence = options.Evidence;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			bool flag = false;
			if (options.OutputAssembly == null || options.OutputAssembly.Length == 0)
			{
				string text2 = (options.GenerateExecutable ? "exe" : "dll");
				options.OutputAssembly = compilerResults.TempFiles.AddExtension(text2, !options.GenerateInMemory);
				new FileStream(options.OutputAssembly, FileMode.Create, FileAccess.ReadWrite).Close();
				flag = true;
			}
			compilerResults.TempFiles.AddExtension("pdb");
			string text3 = this.CmdArgsFromParameters(options) + " " + CodeCompiler.JoinStringArray(fileNames, " ");
			string responseFileCmdArgs = this.GetResponseFileCmdArgs(options, text3);
			string text4 = null;
			if (responseFileCmdArgs != null)
			{
				text4 = text3;
				text3 = responseFileCmdArgs;
			}
			this.Compile(options, Executor.GetRuntimeInstallDirectory(), this.CompilerName, text3, ref text, ref num, text4);
			compilerResults.NativeCompilerReturnValue = num;
			if (num != 0 || options.WarningLevel > 0)
			{
				FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				try
				{
					if (fileStream.Length > 0L)
					{
						StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8);
						string text5;
						do
						{
							text5 = streamReader.ReadLine();
							if (text5 != null)
							{
								compilerResults.Output.Add(text5);
								this.ProcessCompilerOutputLine(compilerResults, text5);
							}
						}
						while (text5 != null);
					}
				}
				finally
				{
					fileStream.Close();
				}
				if (num != 0 && flag)
				{
					File.Delete(options.OutputAssembly);
				}
			}
			if (!compilerResults.Errors.HasErrors && options.GenerateInMemory)
			{
				FileStream fileStream2 = new FileStream(options.OutputAssembly, FileMode.Open, FileAccess.Read, FileShare.Read);
				try
				{
					int num2 = (int)fileStream2.Length;
					byte[] array = new byte[num2];
					fileStream2.Read(array, 0, num2);
					SecurityPermission securityPermission2 = new SecurityPermission(SecurityPermissionFlag.ControlEvidence);
					securityPermission2.Assert();
					try
					{
						if (!FileIntegrity.IsEnabled)
						{
							compilerResults.CompiledAssembly = Assembly.Load(array, null, options.Evidence);
							return compilerResults;
						}
						if (!FileIntegrity.IsTrusted(fileStream2.SafeFileHandle))
						{
							throw new IOException(SR.GetString("FileIntegrityCheckFailed", new object[] { options.OutputAssembly }));
						}
						compilerResults.CompiledAssembly = CodeCompiler.LoadImageSkipIntegrityCheck(array, null, options.Evidence);
						return compilerResults;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				finally
				{
					fileStream2.Close();
				}
			}
			compilerResults.PathToAssembly = options.OutputAssembly;
			return compilerResults;
		}

		/// <summary>Processes the specified line from the specified <see cref="T:System.CodeDom.Compiler.CompilerResults" />.</summary>
		/// <param name="results">A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> that indicates the results of compilation.</param>
		/// <param name="line">The line to process.</param>
		// Token: 0x06003B80 RID: 15232
		protected abstract void ProcessCompilerOutputLine(CompilerResults results, string line);

		/// <summary>Gets the command arguments to be passed to the compiler from the specified <see cref="T:System.CodeDom.Compiler.CompilerParameters" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> that indicates the compiler options.</param>
		/// <returns>The command arguments.</returns>
		// Token: 0x06003B81 RID: 15233
		protected abstract string CmdArgsFromParameters(CompilerParameters options);

		/// <summary>Gets the command arguments to use when invoking the compiler to generate a response file.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="cmdArgs">A command arguments string.</param>
		/// <returns>The command arguments to use to generate a response file, or <see langword="null" /> if there are no response file arguments.</returns>
		// Token: 0x06003B82 RID: 15234 RVA: 0x000F5A34 File Offset: 0x000F3C34
		protected virtual string GetResponseFileCmdArgs(CompilerParameters options, string cmdArgs)
		{
			string text = options.TempFiles.AddExtension("cmdline");
			Stream stream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.Read);
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
				{
					streamWriter.Write(cmdArgs);
					streamWriter.Flush();
				}
			}
			finally
			{
				stream.Close();
			}
			return "@\"" + text + "\"";
		}

		/// <summary>Compiles the specified source code strings using the specified options, and returns the results from the compilation.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="sources">An array of strings containing the source code to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="sources" /> is <see langword="null" />.</exception>
		// Token: 0x06003B83 RID: 15235 RVA: 0x000F5AB4 File Offset: 0x000F3CB4
		protected virtual CompilerResults FromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			string[] array = new string[sources.Length];
			FileStream[] array2 = new FileStream[sources.Length];
			CompilerResults compilerResults = null;
			try
			{
				WindowsImpersonationContext windowsImpersonationContext = Executor.RevertImpersonation();
				try
				{
					try
					{
						bool isEnabled = FileIntegrity.IsEnabled;
						for (int i = 0; i < sources.Length; i++)
						{
							string text = options.TempFiles.AddExtension(i.ToString() + this.FileExtension);
							FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
							array2[i] = fileStream;
							using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
							{
								streamWriter.Write(sources[i]);
								streamWriter.Flush();
								if (isEnabled)
								{
									FileIntegrity.MarkAsTrusted(fileStream.SafeFileHandle);
								}
							}
							array[i] = text;
						}
						compilerResults = this.FromFileBatch(options, array);
					}
					finally
					{
						int num = 0;
						while (num < array2.Length && array2[num] != null)
						{
							array2[num].Close();
							num++;
						}
					}
				}
				finally
				{
					Executor.ReImpersonate(windowsImpersonationContext);
				}
			}
			catch
			{
				throw;
			}
			return compilerResults;
		}

		/// <summary>Joins the specified string arrays.</summary>
		/// <param name="sa">The array of strings to join.</param>
		/// <param name="separator">The separator to use.</param>
		/// <returns>The concatenated string.</returns>
		// Token: 0x06003B84 RID: 15236 RVA: 0x000F5C04 File Offset: 0x000F3E04
		protected static string JoinStringArray(string[] sa, string separator)
		{
			if (sa == null || sa.Length == 0)
			{
				return string.Empty;
			}
			if (sa.Length == 1)
			{
				return "\"" + sa[0] + "\"";
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < sa.Length - 1; i++)
			{
				stringBuilder.Append("\"");
				stringBuilder.Append(sa[i]);
				stringBuilder.Append("\"");
				stringBuilder.Append(separator);
			}
			stringBuilder.Append("\"");
			stringBuilder.Append(sa[sa.Length - 1]);
			stringBuilder.Append("\"");
			return stringBuilder.ToString();
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x000F5CA4 File Offset: 0x000F3EA4
		internal static Assembly LoadImageSkipIntegrityCheck(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			MethodInfo method = typeof(Assembly).GetMethod("LoadImageSkipIntegrityCheck", BindingFlags.Static | BindingFlags.NonPublic);
			return (method != null) ? ((Assembly)method.Invoke(null, new object[] { rawAssembly, rawSymbolStore, securityEvidence })) : Assembly.Load(rawAssembly, rawSymbolStore, securityEvidence);
		}
	}
}
