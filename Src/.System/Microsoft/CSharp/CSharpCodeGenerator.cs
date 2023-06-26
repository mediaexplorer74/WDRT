using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.CSharp
{
	// Token: 0x0200000D RID: 13
	internal class CSharpCodeGenerator : ICodeCompiler, ICodeGenerator
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00006E14 File Offset: 0x00005014
		internal CSharpCodeGenerator()
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006E1C File Offset: 0x0000501C
		internal CSharpCodeGenerator(IDictionary<string, string> providerOptions)
		{
			this.provOptions = providerOptions;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00006E2B File Offset: 0x0000502B
		private string FileExtension
		{
			get
			{
				return ".cs";
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00006E32 File Offset: 0x00005032
		private string CompilerName
		{
			get
			{
				return "csc.exe";
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00006E39 File Offset: 0x00005039
		private string CurrentTypeName
		{
			get
			{
				if (this.currentClass != null)
				{
					return this.currentClass.Name;
				}
				return "<% unknown %>";
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00006E54 File Offset: 0x00005054
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00006E61 File Offset: 0x00005061
		private int Indent
		{
			get
			{
				return this.output.Indent;
			}
			set
			{
				this.output.Indent = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00006E6F File Offset: 0x0000506F
		private bool IsCurrentInterface
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsInterface;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00006E93 File Offset: 0x00005093
		private bool IsCurrentClass
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsClass;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00006EB7 File Offset: 0x000050B7
		private bool IsCurrentStruct
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsStruct;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00006EDB File Offset: 0x000050DB
		private bool IsCurrentEnum
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsEnum;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00006EFF File Offset: 0x000050FF
		private bool IsCurrentDelegate
		{
			get
			{
				return this.currentClass != null && this.currentClass is CodeTypeDelegate;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00006F19 File Offset: 0x00005119
		private string NullToken
		{
			get
			{
				return "null";
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00006F20 File Offset: 0x00005120
		private CodeGeneratorOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00006F28 File Offset: 0x00005128
		private TextWriter Output
		{
			get
			{
				return this.output;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006F30 File Offset: 0x00005130
		private string QuoteSnippetStringCStyle(string value)
		{
			StringBuilder stringBuilder = new StringBuilder(value.Length + 5);
			Indentation indentation = new Indentation((IndentedTextWriter)this.Output, this.Indent + 1);
			stringBuilder.Append("\"");
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c <= '"')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							stringBuilder.Append("\\t");
							break;
						case '\n':
							stringBuilder.Append("\\n");
							break;
						case '\v':
						case '\f':
							goto IL_10C;
						case '\r':
							stringBuilder.Append("\\r");
							break;
						default:
							if (c != '"')
							{
								goto IL_10C;
							}
							stringBuilder.Append("\\\"");
							break;
						}
					}
					else
					{
						stringBuilder.Append("\\0");
					}
				}
				else if (c <= '\\')
				{
					if (c != '\'')
					{
						if (c != '\\')
						{
							goto IL_10C;
						}
						stringBuilder.Append("\\\\");
					}
					else
					{
						stringBuilder.Append("\\'");
					}
				}
				else
				{
					if (c != '\u2028' && c != '\u2029')
					{
						goto IL_10C;
					}
					this.AppendEscapedChar(stringBuilder, value[i]);
				}
				IL_11A:
				if (i > 0 && i % 80 == 0)
				{
					if (char.IsHighSurrogate(value[i]) && i < value.Length - 1 && char.IsLowSurrogate(value[i + 1]))
					{
						stringBuilder.Append(value[++i]);
					}
					stringBuilder.Append("\" +");
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(indentation.IndentationString);
					stringBuilder.Append('"');
				}
				i++;
				continue;
				IL_10C:
				stringBuilder.Append(value[i]);
				goto IL_11A;
			}
			stringBuilder.Append("\"");
			return stringBuilder.ToString();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000070EC File Offset: 0x000052EC
		private string QuoteSnippetStringVerbatimStyle(string value)
		{
			StringBuilder stringBuilder = new StringBuilder(value.Length + 5);
			stringBuilder.Append("@\"");
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == '"')
				{
					stringBuilder.Append("\"\"");
				}
				else
				{
					stringBuilder.Append(value[i]);
				}
			}
			stringBuilder.Append("\"");
			return stringBuilder.ToString();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000715D File Offset: 0x0000535D
		private string QuoteSnippetString(string value)
		{
			if (value.Length < 256 || value.Length > 1500 || value.IndexOf('\0') != -1)
			{
				return this.QuoteSnippetStringCStyle(value);
			}
			return this.QuoteSnippetStringVerbatimStyle(value);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007194 File Offset: 0x00005394
		private void ProcessCompilerOutputLine(CompilerResults results, string line)
		{
			if (CSharpCodeGenerator.outputRegSimple == null)
			{
				CSharpCodeGenerator.outputRegWithFileAndLine = new Regex("(^(.*)(\\(([0-9]+),([0-9]+)\\)): )(error|warning) ([A-Z]+[0-9]+) ?: (.*)");
				CSharpCodeGenerator.outputRegSimple = new Regex("(error|warning) ([A-Z]+[0-9]+) ?: (.*)");
			}
			Match match = CSharpCodeGenerator.outputRegWithFileAndLine.Match(line);
			bool flag;
			if (match.Success)
			{
				flag = true;
			}
			else
			{
				match = CSharpCodeGenerator.outputRegSimple.Match(line);
				flag = false;
			}
			if (match.Success)
			{
				CompilerError compilerError = new CompilerError();
				if (flag)
				{
					compilerError.FileName = match.Groups[2].Value;
					compilerError.Line = int.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture);
					compilerError.Column = int.Parse(match.Groups[5].Value, CultureInfo.InvariantCulture);
				}
				if (string.Compare(match.Groups[flag ? 6 : 1].Value, "warning", StringComparison.OrdinalIgnoreCase) == 0)
				{
					compilerError.IsWarning = true;
				}
				compilerError.ErrorNumber = match.Groups[flag ? 7 : 2].Value;
				compilerError.ErrorText = match.Groups[flag ? 8 : 3].Value;
				results.Errors.Add(compilerError);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000072D8 File Offset: 0x000054D8
		private string CmdArgsFromParameters(CompilerParameters options)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			if (options.GenerateExecutable)
			{
				stringBuilder.Append("/t:exe ");
				if (options.MainClass != null && options.MainClass.Length > 0)
				{
					stringBuilder.Append("/main:");
					stringBuilder.Append(options.MainClass);
					stringBuilder.Append(" ");
				}
			}
			else
			{
				stringBuilder.Append("/t:library ");
			}
			stringBuilder.Append("/utf8output ");
			if (FileIntegrity.IsEnabled)
			{
				stringBuilder.Append("/EnforceCodeIntegrity ");
			}
			string text = options.CoreAssemblyFileName;
			string text2;
			if (string.IsNullOrWhiteSpace(options.CoreAssemblyFileName) && CodeDomProvider.TryGetProbableCoreAssemblyFilePath(options, out text2))
			{
				text = text2;
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				stringBuilder.Append("/nostdlib+ ");
				stringBuilder.Append("/R:\"").Append(text.Trim()).Append("\" ");
			}
			foreach (string text3 in options.ReferencedAssemblies)
			{
				stringBuilder.Append("/R:");
				stringBuilder.Append("\"");
				stringBuilder.Append(text3);
				stringBuilder.Append("\"");
				stringBuilder.Append(" ");
			}
			stringBuilder.Append("/out:");
			stringBuilder.Append("\"");
			stringBuilder.Append(options.OutputAssembly);
			stringBuilder.Append("\"");
			stringBuilder.Append(" ");
			if (options.IncludeDebugInformation)
			{
				stringBuilder.Append("/D:DEBUG ");
				stringBuilder.Append("/debug+ ");
				stringBuilder.Append("/optimize- ");
			}
			else
			{
				stringBuilder.Append("/debug- ");
				stringBuilder.Append("/optimize+ ");
			}
			if (options.Win32Resource != null)
			{
				stringBuilder.Append("/win32res:\"" + options.Win32Resource + "\" ");
			}
			foreach (string text4 in options.EmbeddedResources)
			{
				stringBuilder.Append("/res:\"");
				stringBuilder.Append(text4);
				stringBuilder.Append("\" ");
			}
			foreach (string text5 in options.LinkedResources)
			{
				stringBuilder.Append("/linkres:\"");
				stringBuilder.Append(text5);
				stringBuilder.Append("\" ");
			}
			if (options.TreatWarningsAsErrors)
			{
				stringBuilder.Append("/warnaserror ");
			}
			if (options.WarningLevel >= 0)
			{
				stringBuilder.Append("/w:" + options.WarningLevel.ToString() + " ");
			}
			if (options.CompilerOptions != null)
			{
				stringBuilder.Append(options.CompilerOptions + " ");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00007618 File Offset: 0x00005818
		private void ContinueOnNewLine(string st)
		{
			this.Output.WriteLine(st);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00007628 File Offset: 0x00005828
		private string GetResponseFileCmdArgs(CompilerParameters options, string cmdArgs)
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
			return "/noconfig /fullpaths @\"" + text + "\"";
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000076A8 File Offset: 0x000058A8
		private void OutputIdentifier(string ident)
		{
			this.Output.Write(this.CreateEscapedIdentifier(ident));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000076BC File Offset: 0x000058BC
		private void OutputType(CodeTypeReference typeRef)
		{
			this.Output.Write(this.GetTypeOutput(typeRef));
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000076D0 File Offset: 0x000058D0
		private void GenerateArrayCreateExpression(CodeArrayCreateExpression e)
		{
			this.Output.Write("new ");
			CodeExpressionCollection initializers = e.Initializers;
			if (initializers.Count > 0)
			{
				this.OutputType(e.CreateType);
				if (e.CreateType.ArrayRank == 0)
				{
					this.Output.Write("[]");
				}
				this.Output.WriteLine(" {");
				int num = this.Indent;
				this.Indent = num + 1;
				this.OutputExpressionList(initializers, true);
				num = this.Indent;
				this.Indent = num - 1;
				this.Output.Write("}");
				return;
			}
			this.Output.Write(this.GetBaseTypeOutput(e.CreateType));
			this.Output.Write("[");
			if (e.SizeExpression != null)
			{
				this.GenerateExpression(e.SizeExpression);
			}
			else
			{
				this.Output.Write(e.Size);
			}
			this.Output.Write("]");
			int nestedArrayDepth = e.CreateType.NestedArrayDepth;
			for (int i = 0; i < nestedArrayDepth - 1; i++)
			{
				this.Output.Write("[]");
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000077F7 File Offset: 0x000059F7
		private void GenerateBaseReferenceExpression(CodeBaseReferenceExpression e)
		{
			this.Output.Write("base");
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000780C File Offset: 0x00005A0C
		private void GenerateBinaryOperatorExpression(CodeBinaryOperatorExpression e)
		{
			bool flag = false;
			this.Output.Write("(");
			this.GenerateExpression(e.Left);
			this.Output.Write(" ");
			if (e.Left is CodeBinaryOperatorExpression || e.Right is CodeBinaryOperatorExpression)
			{
				if (!this.inNestedBinary)
				{
					flag = true;
					this.inNestedBinary = true;
					this.Indent += 3;
				}
				this.ContinueOnNewLine("");
			}
			this.OutputOperator(e.Operator);
			this.Output.Write(" ");
			this.GenerateExpression(e.Right);
			this.Output.Write(")");
			if (flag)
			{
				this.Indent -= 3;
				this.inNestedBinary = false;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000078DC File Offset: 0x00005ADC
		private void GenerateCastExpression(CodeCastExpression e)
		{
			this.Output.Write("((");
			this.OutputType(e.TargetType);
			this.Output.Write(")(");
			this.GenerateExpression(e.Expression);
			this.Output.Write("))");
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00007934 File Offset: 0x00005B34
		public void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			if (this.output != null)
			{
				throw new InvalidOperationException(SR.GetString("CodeGenReentrance"));
			}
			this.options = ((options == null) ? new CodeGeneratorOptions() : options);
			this.output = new IndentedTextWriter(writer, this.options.IndentString);
			try
			{
				CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
				this.currentClass = codeTypeDeclaration;
				this.GenerateTypeMember(member, codeTypeDeclaration);
			}
			finally
			{
				this.currentClass = null;
				this.output = null;
				this.options = null;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000079C0 File Offset: 0x00005BC0
		private void GenerateDefaultValueExpression(CodeDefaultValueExpression e)
		{
			this.Output.Write("default(");
			this.OutputType(e.Type);
			this.Output.Write(")");
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000079F0 File Offset: 0x00005BF0
		private void GenerateDelegateCreateExpression(CodeDelegateCreateExpression e)
		{
			this.Output.Write("new ");
			this.OutputType(e.DelegateType);
			this.Output.Write("(");
			this.GenerateExpression(e.TargetObject);
			this.Output.Write(".");
			this.OutputIdentifier(e.MethodName);
			this.Output.Write(")");
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007A64 File Offset: 0x00005C64
		private void GenerateEvents(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeMemberEvent)
				{
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeMemberEvent codeMemberEvent = (CodeMemberEvent)enumerator.Current;
					if (codeMemberEvent.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberEvent.LinePragma);
					}
					this.GenerateEvent(codeMemberEvent, e);
					if (codeMemberEvent.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberEvent.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00007B5C File Offset: 0x00005D5C
		private void GenerateFields(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeMemberField)
				{
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeMemberField codeMemberField = (CodeMemberField)enumerator.Current;
					if (codeMemberField.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberField.LinePragma);
					}
					this.GenerateField(codeMemberField);
					if (codeMemberField.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberField.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00007C52 File Offset: 0x00005E52
		private void GenerateFieldReferenceExpression(CodeFieldReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.GenerateExpression(e.TargetObject);
				this.Output.Write(".");
			}
			this.OutputIdentifier(e.FieldName);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00007C84 File Offset: 0x00005E84
		private void GenerateArgumentReferenceExpression(CodeArgumentReferenceExpression e)
		{
			this.OutputIdentifier(e.ParameterName);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007C92 File Offset: 0x00005E92
		private void GenerateVariableReferenceExpression(CodeVariableReferenceExpression e)
		{
			this.OutputIdentifier(e.VariableName);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007CA0 File Offset: 0x00005EA0
		private void GenerateIndexerExpression(CodeIndexerExpression e)
		{
			this.GenerateExpression(e.TargetObject);
			this.Output.Write("[");
			bool flag = true;
			foreach (object obj in e.Indices)
			{
				CodeExpression codeExpression = (CodeExpression)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				this.GenerateExpression(codeExpression);
			}
			this.Output.Write("]");
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007D40 File Offset: 0x00005F40
		private void GenerateArrayIndexerExpression(CodeArrayIndexerExpression e)
		{
			this.GenerateExpression(e.TargetObject);
			this.Output.Write("[");
			bool flag = true;
			foreach (object obj in e.Indices)
			{
				CodeExpression codeExpression = (CodeExpression)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				this.GenerateExpression(codeExpression);
			}
			this.Output.Write("]");
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007DE0 File Offset: 0x00005FE0
		private void GenerateSnippetCompileUnit(CodeSnippetCompileUnit e)
		{
			this.GenerateDirectives(e.StartDirectives);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaStart(e.LinePragma);
			}
			this.Output.WriteLine(e.Value);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(e.LinePragma);
			}
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007E4C File Offset: 0x0000604C
		private void GenerateSnippetExpression(CodeSnippetExpression e)
		{
			this.Output.Write(e.Value);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00007E5F File Offset: 0x0000605F
		private void GenerateMethodInvokeExpression(CodeMethodInvokeExpression e)
		{
			this.GenerateMethodReferenceExpression(e.Method);
			this.Output.Write("(");
			this.OutputExpressionList(e.Parameters);
			this.Output.Write(")");
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007E9C File Offset: 0x0000609C
		private void GenerateMethodReferenceExpression(CodeMethodReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				if (e.TargetObject is CodeBinaryOperatorExpression)
				{
					this.Output.Write("(");
					this.GenerateExpression(e.TargetObject);
					this.Output.Write(")");
				}
				else
				{
					this.GenerateExpression(e.TargetObject);
				}
				this.Output.Write(".");
			}
			this.OutputIdentifier(e.MethodName);
			if (e.TypeArguments.Count > 0)
			{
				this.Output.Write(this.GetTypeArgumentsOutput(e.TypeArguments));
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007F3C File Offset: 0x0000613C
		private bool GetUserData(CodeObject e, string property, bool defaultValue)
		{
			object obj = e.UserData[property];
			if (obj != null && obj is bool)
			{
				return (bool)obj;
			}
			return defaultValue;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007F6C File Offset: 0x0000616C
		private void GenerateNamespace(CodeNamespace e)
		{
			this.GenerateCommentStatements(e.Comments);
			this.GenerateNamespaceStart(e);
			if (this.GetUserData(e, "GenerateImports", true))
			{
				this.GenerateNamespaceImports(e);
			}
			this.Output.WriteLine("");
			this.GenerateTypes(e);
			this.GenerateNamespaceEnd(e);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007FC0 File Offset: 0x000061C0
		private void GenerateStatement(CodeStatement e)
		{
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaStart(e.LinePragma);
			}
			if (e is CodeCommentStatement)
			{
				this.GenerateCommentStatement((CodeCommentStatement)e);
			}
			else if (e is CodeMethodReturnStatement)
			{
				this.GenerateMethodReturnStatement((CodeMethodReturnStatement)e);
			}
			else if (e is CodeConditionStatement)
			{
				this.GenerateConditionStatement((CodeConditionStatement)e);
			}
			else if (e is CodeTryCatchFinallyStatement)
			{
				this.GenerateTryCatchFinallyStatement((CodeTryCatchFinallyStatement)e);
			}
			else if (e is CodeAssignStatement)
			{
				this.GenerateAssignStatement((CodeAssignStatement)e);
			}
			else if (e is CodeExpressionStatement)
			{
				this.GenerateExpressionStatement((CodeExpressionStatement)e);
			}
			else if (e is CodeIterationStatement)
			{
				this.GenerateIterationStatement((CodeIterationStatement)e);
			}
			else if (e is CodeThrowExceptionStatement)
			{
				this.GenerateThrowExceptionStatement((CodeThrowExceptionStatement)e);
			}
			else if (e is CodeSnippetStatement)
			{
				int indent = this.Indent;
				this.Indent = 0;
				this.GenerateSnippetStatement((CodeSnippetStatement)e);
				this.Indent = indent;
			}
			else if (e is CodeVariableDeclarationStatement)
			{
				this.GenerateVariableDeclarationStatement((CodeVariableDeclarationStatement)e);
			}
			else if (e is CodeAttachEventStatement)
			{
				this.GenerateAttachEventStatement((CodeAttachEventStatement)e);
			}
			else if (e is CodeRemoveEventStatement)
			{
				this.GenerateRemoveEventStatement((CodeRemoveEventStatement)e);
			}
			else if (e is CodeGotoStatement)
			{
				this.GenerateGotoStatement((CodeGotoStatement)e);
			}
			else
			{
				if (!(e is CodeLabeledStatement))
				{
					throw new ArgumentException(SR.GetString("InvalidElementType", new object[] { e.GetType().FullName }), "e");
				}
				this.GenerateLabeledStatement((CodeLabeledStatement)e);
			}
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(e.LinePragma);
			}
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000081BC File Offset: 0x000063BC
		private void GenerateStatements(CodeStatementCollection stms)
		{
			foreach (object obj in stms)
			{
				((ICodeGenerator)this).GenerateCodeFromStatement((CodeStatement)obj, this.output.InnerWriter, this.options);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000081FC File Offset: 0x000063FC
		private void GenerateNamespaceImports(CodeNamespace e)
		{
			foreach (object obj in e.Imports)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				if (codeNamespaceImport.LinePragma != null)
				{
					this.GenerateLinePragmaStart(codeNamespaceImport.LinePragma);
				}
				this.GenerateNamespaceImport(codeNamespaceImport);
				if (codeNamespaceImport.LinePragma != null)
				{
					this.GenerateLinePragmaEnd(codeNamespaceImport.LinePragma);
				}
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000825A File Offset: 0x0000645A
		private void GenerateEventReferenceExpression(CodeEventReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.GenerateExpression(e.TargetObject);
				this.Output.Write(".");
			}
			this.OutputIdentifier(e.EventName);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000828C File Offset: 0x0000648C
		private void GenerateDelegateInvokeExpression(CodeDelegateInvokeExpression e)
		{
			if (e.TargetObject != null)
			{
				this.GenerateExpression(e.TargetObject);
			}
			this.Output.Write("(");
			this.OutputExpressionList(e.Parameters);
			this.Output.Write(")");
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000082DC File Offset: 0x000064DC
		private void GenerateObjectCreateExpression(CodeObjectCreateExpression e)
		{
			this.Output.Write("new ");
			this.OutputType(e.CreateType);
			this.Output.Write("(");
			this.OutputExpressionList(e.Parameters);
			this.Output.Write(")");
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00008334 File Offset: 0x00006534
		private void GeneratePrimitiveExpression(CodePrimitiveExpression e)
		{
			if (e.Value is char)
			{
				this.GeneratePrimitiveChar((char)e.Value);
				return;
			}
			if (e.Value is sbyte)
			{
				this.Output.Write(((sbyte)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is ushort)
			{
				this.Output.Write(((ushort)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is uint)
			{
				this.Output.Write(((uint)e.Value).ToString(CultureInfo.InvariantCulture));
				this.Output.Write("u");
				return;
			}
			if (e.Value is ulong)
			{
				this.Output.Write(((ulong)e.Value).ToString(CultureInfo.InvariantCulture));
				this.Output.Write("ul");
				return;
			}
			this.GeneratePrimitiveExpressionBase(e);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000844C File Offset: 0x0000664C
		private void GeneratePrimitiveExpressionBase(CodePrimitiveExpression e)
		{
			if (e.Value == null)
			{
				this.Output.Write(this.NullToken);
				return;
			}
			if (e.Value is string)
			{
				this.Output.Write(this.QuoteSnippetString((string)e.Value));
				return;
			}
			if (e.Value is char)
			{
				this.Output.Write("'" + e.Value.ToString() + "'");
				return;
			}
			if (e.Value is byte)
			{
				this.Output.Write(((byte)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is short)
			{
				this.Output.Write(((short)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is int)
			{
				this.Output.Write(((int)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is long)
			{
				this.Output.Write(((long)e.Value).ToString(CultureInfo.InvariantCulture));
				return;
			}
			if (e.Value is float)
			{
				this.GenerateSingleFloatValue((float)e.Value);
				return;
			}
			if (e.Value is double)
			{
				this.GenerateDoubleValue((double)e.Value);
				return;
			}
			if (e.Value is decimal)
			{
				this.GenerateDecimalValue((decimal)e.Value);
				return;
			}
			if (!(e.Value is bool))
			{
				throw new ArgumentException(SR.GetString("InvalidPrimitiveType", new object[] { e.Value.GetType().ToString() }));
			}
			if ((bool)e.Value)
			{
				this.Output.Write("true");
				return;
			}
			this.Output.Write("false");
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00008658 File Offset: 0x00006858
		private void GeneratePrimitiveChar(char c)
		{
			this.Output.Write('\'');
			if (c > '\'')
			{
				if (c <= '\u0084')
				{
					if (c == '\\')
					{
						this.Output.Write("\\\\");
						goto IL_143;
					}
					if (c != '\u0084')
					{
						goto IL_125;
					}
				}
				else if (c != '\u0085' && c != '\u2028' && c != '\u2029')
				{
					goto IL_125;
				}
				this.AppendEscapedChar(null, c);
				goto IL_143;
			}
			if (c <= '\r')
			{
				if (c == '\0')
				{
					this.Output.Write("\\0");
					goto IL_143;
				}
				switch (c)
				{
				case '\t':
					this.Output.Write("\\t");
					goto IL_143;
				case '\n':
					this.Output.Write("\\n");
					goto IL_143;
				case '\r':
					this.Output.Write("\\r");
					goto IL_143;
				}
			}
			else
			{
				if (c == '"')
				{
					this.Output.Write("\\\"");
					goto IL_143;
				}
				if (c == '\'')
				{
					this.Output.Write("\\'");
					goto IL_143;
				}
			}
			IL_125:
			if (char.IsSurrogate(c))
			{
				this.AppendEscapedChar(null, c);
			}
			else
			{
				this.Output.Write(c);
			}
			IL_143:
			this.Output.Write('\'');
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000087B8 File Offset: 0x000069B8
		private void AppendEscapedChar(StringBuilder b, char value)
		{
			int num;
			if (b == null)
			{
				this.Output.Write("\\u");
				TextWriter textWriter = this.Output;
				num = (int)value;
				textWriter.Write(num.ToString("X4", CultureInfo.InvariantCulture));
				return;
			}
			b.Append("\\u");
			num = (int)value;
			b.Append(num.ToString("X4", CultureInfo.InvariantCulture));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000881D File Offset: 0x00006A1D
		private void GeneratePropertySetValueReferenceExpression(CodePropertySetValueReferenceExpression e)
		{
			this.Output.Write("value");
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000882F File Offset: 0x00006A2F
		private void GenerateThisReferenceExpression(CodeThisReferenceExpression e)
		{
			this.Output.Write("this");
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00008841 File Offset: 0x00006A41
		private void GenerateExpressionStatement(CodeExpressionStatement e)
		{
			this.GenerateExpression(e.Expression);
			if (!this.generatingForLoop)
			{
				this.Output.WriteLine(";");
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00008868 File Offset: 0x00006A68
		private void GenerateIterationStatement(CodeIterationStatement e)
		{
			this.generatingForLoop = true;
			this.Output.Write("for (");
			this.GenerateStatement(e.InitStatement);
			this.Output.Write("; ");
			this.GenerateExpression(e.TestExpression);
			this.Output.Write("; ");
			this.GenerateStatement(e.IncrementStatement);
			this.Output.Write(")");
			this.OutputStartingBrace();
			this.generatingForLoop = false;
			int num = this.Indent;
			this.Indent = num + 1;
			this.GenerateStatements(e.Statements);
			num = this.Indent;
			this.Indent = num - 1;
			this.Output.WriteLine("}");
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000892C File Offset: 0x00006B2C
		private void GenerateThrowExceptionStatement(CodeThrowExceptionStatement e)
		{
			this.Output.Write("throw");
			if (e.ToThrow != null)
			{
				this.Output.Write(" ");
				this.GenerateExpression(e.ToThrow);
			}
			this.Output.WriteLine(";");
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00008980 File Offset: 0x00006B80
		private void GenerateComment(CodeComment e)
		{
			string text = (e.DocComment ? "///" : "//");
			this.Output.Write(text);
			this.Output.Write(" ");
			string text2 = e.Text;
			for (int i = 0; i < text2.Length; i++)
			{
				if (text2[i] != '\0')
				{
					this.Output.Write(text2[i]);
					if (text2[i] == '\r')
					{
						if (i < text2.Length - 1 && text2[i + 1] == '\n')
						{
							this.Output.Write('\n');
							i++;
						}
						((IndentedTextWriter)this.Output).InternalOutputTabs();
						this.Output.Write(text);
					}
					else if (text2[i] == '\n')
					{
						((IndentedTextWriter)this.Output).InternalOutputTabs();
						this.Output.Write(text);
					}
					else if (text2[i] == '\u2028' || text2[i] == '\u2029' || text2[i] == '\u0085')
					{
						this.Output.Write(text);
					}
				}
			}
			this.Output.WriteLine();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00008AB6 File Offset: 0x00006CB6
		private void GenerateCommentStatement(CodeCommentStatement e)
		{
			if (e.Comment == null)
			{
				throw new ArgumentException(SR.GetString("Argument_NullComment", new object[] { "e" }), "e");
			}
			this.GenerateComment(e.Comment);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00008AF0 File Offset: 0x00006CF0
		private void GenerateCommentStatements(CodeCommentStatementCollection e)
		{
			foreach (object obj in e)
			{
				CodeCommentStatement codeCommentStatement = (CodeCommentStatement)obj;
				this.GenerateCommentStatement(codeCommentStatement);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00008B44 File Offset: 0x00006D44
		private void GenerateMethodReturnStatement(CodeMethodReturnStatement e)
		{
			this.Output.Write("return");
			if (e.Expression != null)
			{
				this.Output.Write(" ");
				this.GenerateExpression(e.Expression);
			}
			this.Output.WriteLine(";");
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00008B98 File Offset: 0x00006D98
		private void GenerateConditionStatement(CodeConditionStatement e)
		{
			this.Output.Write("if (");
			this.GenerateExpression(e.Condition);
			this.Output.Write(")");
			this.OutputStartingBrace();
			int num = this.Indent;
			this.Indent = num + 1;
			this.GenerateStatements(e.TrueStatements);
			num = this.Indent;
			this.Indent = num - 1;
			CodeStatementCollection falseStatements = e.FalseStatements;
			if (falseStatements.Count > 0)
			{
				this.Output.Write("}");
				if (this.Options.ElseOnClosing)
				{
					this.Output.Write(" ");
				}
				else
				{
					this.Output.WriteLine("");
				}
				this.Output.Write("else");
				this.OutputStartingBrace();
				num = this.Indent;
				this.Indent = num + 1;
				this.GenerateStatements(e.FalseStatements);
				num = this.Indent;
				this.Indent = num - 1;
			}
			this.Output.WriteLine("}");
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00008CA8 File Offset: 0x00006EA8
		private void GenerateTryCatchFinallyStatement(CodeTryCatchFinallyStatement e)
		{
			this.Output.Write("try");
			this.OutputStartingBrace();
			int num = this.Indent;
			this.Indent = num + 1;
			this.GenerateStatements(e.TryStatements);
			num = this.Indent;
			this.Indent = num - 1;
			CodeCatchClauseCollection catchClauses = e.CatchClauses;
			if (catchClauses.Count > 0)
			{
				IEnumerator enumerator = catchClauses.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this.Output.Write("}");
					if (this.Options.ElseOnClosing)
					{
						this.Output.Write(" ");
					}
					else
					{
						this.Output.WriteLine("");
					}
					CodeCatchClause codeCatchClause = (CodeCatchClause)enumerator.Current;
					this.Output.Write("catch (");
					this.OutputType(codeCatchClause.CatchExceptionType);
					this.Output.Write(" ");
					this.OutputIdentifier(codeCatchClause.LocalName);
					this.Output.Write(")");
					this.OutputStartingBrace();
					num = this.Indent;
					this.Indent = num + 1;
					this.GenerateStatements(codeCatchClause.Statements);
					num = this.Indent;
					this.Indent = num - 1;
				}
			}
			CodeStatementCollection finallyStatements = e.FinallyStatements;
			if (finallyStatements.Count > 0)
			{
				this.Output.Write("}");
				if (this.Options.ElseOnClosing)
				{
					this.Output.Write(" ");
				}
				else
				{
					this.Output.WriteLine("");
				}
				this.Output.Write("finally");
				this.OutputStartingBrace();
				num = this.Indent;
				this.Indent = num + 1;
				this.GenerateStatements(finallyStatements);
				num = this.Indent;
				this.Indent = num - 1;
			}
			this.Output.WriteLine("}");
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00008E88 File Offset: 0x00007088
		private void GenerateAssignStatement(CodeAssignStatement e)
		{
			this.GenerateExpression(e.Left);
			this.Output.Write(" = ");
			this.GenerateExpression(e.Right);
			if (!this.generatingForLoop)
			{
				this.Output.WriteLine(";");
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008ED5 File Offset: 0x000070D5
		private void GenerateAttachEventStatement(CodeAttachEventStatement e)
		{
			this.GenerateEventReferenceExpression(e.Event);
			this.Output.Write(" += ");
			this.GenerateExpression(e.Listener);
			this.Output.WriteLine(";");
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008F0F File Offset: 0x0000710F
		private void GenerateRemoveEventStatement(CodeRemoveEventStatement e)
		{
			this.GenerateEventReferenceExpression(e.Event);
			this.Output.Write(" -= ");
			this.GenerateExpression(e.Listener);
			this.Output.WriteLine(";");
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00008F49 File Offset: 0x00007149
		private void GenerateSnippetStatement(CodeSnippetStatement e)
		{
			this.Output.WriteLine(e.Value);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00008F5C File Offset: 0x0000715C
		private void GenerateGotoStatement(CodeGotoStatement e)
		{
			this.Output.Write("goto ");
			this.Output.Write(e.Label);
			this.Output.WriteLine(";");
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00008F90 File Offset: 0x00007190
		private void GenerateLabeledStatement(CodeLabeledStatement e)
		{
			int num = this.Indent;
			this.Indent = num - 1;
			this.Output.Write(e.Label);
			this.Output.WriteLine(":");
			num = this.Indent;
			this.Indent = num + 1;
			if (e.Statement != null)
			{
				this.GenerateStatement(e.Statement);
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00008FF4 File Offset: 0x000071F4
		private void GenerateVariableDeclarationStatement(CodeVariableDeclarationStatement e)
		{
			this.OutputTypeNamePair(e.Type, e.Name);
			if (e.InitExpression != null)
			{
				this.Output.Write(" = ");
				this.GenerateExpression(e.InitExpression);
			}
			if (!this.generatingForLoop)
			{
				this.Output.WriteLine(";");
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00009050 File Offset: 0x00007250
		private void GenerateLinePragmaStart(CodeLinePragma e)
		{
			this.Output.WriteLine("");
			this.Output.Write("#line ");
			this.Output.Write(e.LineNumber);
			this.Output.Write(" \"");
			this.Output.Write(e.FileName);
			this.Output.Write("\"");
			this.Output.WriteLine("");
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000090CF File Offset: 0x000072CF
		private void GenerateLinePragmaEnd(CodeLinePragma e)
		{
			this.Output.WriteLine();
			this.Output.WriteLine("#line default");
			this.Output.WriteLine("#line hidden");
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000090FC File Offset: 0x000072FC
		private void GenerateEvent(CodeMemberEvent e, CodeTypeDeclaration c)
		{
			if (this.IsCurrentDelegate || this.IsCurrentEnum)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			if (e.PrivateImplementationType == null)
			{
				this.OutputMemberAccessModifier(e.Attributes);
			}
			this.Output.Write("event ");
			string text = e.Name;
			if (e.PrivateImplementationType != null)
			{
				text = this.GetBaseTypeOutput(e.PrivateImplementationType) + "." + text;
			}
			this.OutputTypeNamePair(e.Type, text);
			this.Output.WriteLine(";");
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000919C File Offset: 0x0000739C
		private void GenerateExpression(CodeExpression e)
		{
			if (e is CodeArrayCreateExpression)
			{
				this.GenerateArrayCreateExpression((CodeArrayCreateExpression)e);
				return;
			}
			if (e is CodeBaseReferenceExpression)
			{
				this.GenerateBaseReferenceExpression((CodeBaseReferenceExpression)e);
				return;
			}
			if (e is CodeBinaryOperatorExpression)
			{
				this.GenerateBinaryOperatorExpression((CodeBinaryOperatorExpression)e);
				return;
			}
			if (e is CodeCastExpression)
			{
				this.GenerateCastExpression((CodeCastExpression)e);
				return;
			}
			if (e is CodeDelegateCreateExpression)
			{
				this.GenerateDelegateCreateExpression((CodeDelegateCreateExpression)e);
				return;
			}
			if (e is CodeFieldReferenceExpression)
			{
				this.GenerateFieldReferenceExpression((CodeFieldReferenceExpression)e);
				return;
			}
			if (e is CodeArgumentReferenceExpression)
			{
				this.GenerateArgumentReferenceExpression((CodeArgumentReferenceExpression)e);
				return;
			}
			if (e is CodeVariableReferenceExpression)
			{
				this.GenerateVariableReferenceExpression((CodeVariableReferenceExpression)e);
				return;
			}
			if (e is CodeIndexerExpression)
			{
				this.GenerateIndexerExpression((CodeIndexerExpression)e);
				return;
			}
			if (e is CodeArrayIndexerExpression)
			{
				this.GenerateArrayIndexerExpression((CodeArrayIndexerExpression)e);
				return;
			}
			if (e is CodeSnippetExpression)
			{
				this.GenerateSnippetExpression((CodeSnippetExpression)e);
				return;
			}
			if (e is CodeMethodInvokeExpression)
			{
				this.GenerateMethodInvokeExpression((CodeMethodInvokeExpression)e);
				return;
			}
			if (e is CodeMethodReferenceExpression)
			{
				this.GenerateMethodReferenceExpression((CodeMethodReferenceExpression)e);
				return;
			}
			if (e is CodeEventReferenceExpression)
			{
				this.GenerateEventReferenceExpression((CodeEventReferenceExpression)e);
				return;
			}
			if (e is CodeDelegateInvokeExpression)
			{
				this.GenerateDelegateInvokeExpression((CodeDelegateInvokeExpression)e);
				return;
			}
			if (e is CodeObjectCreateExpression)
			{
				this.GenerateObjectCreateExpression((CodeObjectCreateExpression)e);
				return;
			}
			if (e is CodeParameterDeclarationExpression)
			{
				this.GenerateParameterDeclarationExpression((CodeParameterDeclarationExpression)e);
				return;
			}
			if (e is CodeDirectionExpression)
			{
				this.GenerateDirectionExpression((CodeDirectionExpression)e);
				return;
			}
			if (e is CodePrimitiveExpression)
			{
				this.GeneratePrimitiveExpression((CodePrimitiveExpression)e);
				return;
			}
			if (e is CodePropertyReferenceExpression)
			{
				this.GeneratePropertyReferenceExpression((CodePropertyReferenceExpression)e);
				return;
			}
			if (e is CodePropertySetValueReferenceExpression)
			{
				this.GeneratePropertySetValueReferenceExpression((CodePropertySetValueReferenceExpression)e);
				return;
			}
			if (e is CodeThisReferenceExpression)
			{
				this.GenerateThisReferenceExpression((CodeThisReferenceExpression)e);
				return;
			}
			if (e is CodeTypeReferenceExpression)
			{
				this.GenerateTypeReferenceExpression((CodeTypeReferenceExpression)e);
				return;
			}
			if (e is CodeTypeOfExpression)
			{
				this.GenerateTypeOfExpression((CodeTypeOfExpression)e);
				return;
			}
			if (e is CodeDefaultValueExpression)
			{
				this.GenerateDefaultValueExpression((CodeDefaultValueExpression)e);
				return;
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			throw new ArgumentException(SR.GetString("InvalidElementType", new object[] { e.GetType().FullName }), "e");
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000093EC File Offset: 0x000075EC
		private void GenerateField(CodeMemberField e)
		{
			if (this.IsCurrentDelegate || this.IsCurrentInterface)
			{
				return;
			}
			if (this.IsCurrentEnum)
			{
				if (e.CustomAttributes.Count > 0)
				{
					this.GenerateAttributes(e.CustomAttributes);
				}
				this.OutputIdentifier(e.Name);
				if (e.InitExpression != null)
				{
					this.Output.Write(" = ");
					this.GenerateExpression(e.InitExpression);
				}
				this.Output.WriteLine(",");
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			this.OutputMemberAccessModifier(e.Attributes);
			this.OutputVTableModifier(e.Attributes);
			this.OutputFieldScopeModifier(e.Attributes);
			this.OutputTypeNamePair(e.Type, e.Name);
			if (e.InitExpression != null)
			{
				this.Output.Write(" = ");
				this.GenerateExpression(e.InitExpression);
			}
			this.Output.WriteLine(";");
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000094F1 File Offset: 0x000076F1
		private void GenerateSnippetMember(CodeSnippetTypeMember e)
		{
			this.Output.Write(e.Text);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00009504 File Offset: 0x00007704
		private void GenerateParameterDeclarationExpression(CodeParameterDeclarationExpression e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes, null, true);
			}
			this.OutputDirection(e.Direction);
			this.OutputTypeNamePair(e.Type, e.Name);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00009540 File Offset: 0x00007740
		private void GenerateEntryPointMethod(CodeEntryPointMethod e, CodeTypeDeclaration c)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			this.Output.Write("public static ");
			this.OutputType(e.ReturnType);
			this.Output.Write(" Main()");
			this.OutputStartingBrace();
			int num = this.Indent;
			this.Indent = num + 1;
			this.GenerateStatements(e.Statements);
			num = this.Indent;
			this.Indent = num - 1;
			this.Output.WriteLine("}");
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000095D8 File Offset: 0x000077D8
		private void GenerateMethods(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeMemberMethod && !(enumerator.Current is CodeTypeConstructor) && !(enumerator.Current is CodeConstructor))
				{
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeMemberMethod codeMemberMethod = (CodeMemberMethod)enumerator.Current;
					if (codeMemberMethod.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberMethod.LinePragma);
					}
					if (enumerator.Current is CodeEntryPointMethod)
					{
						this.GenerateEntryPointMethod((CodeEntryPointMethod)enumerator.Current, e);
					}
					else
					{
						this.GenerateMethod(codeMemberMethod, e);
					}
					if (codeMemberMethod.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberMethod.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00009710 File Offset: 0x00007910
		private void GenerateMethod(CodeMemberMethod e, CodeTypeDeclaration c)
		{
			if (!this.IsCurrentClass && !this.IsCurrentStruct && !this.IsCurrentInterface)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			if (e.ReturnTypeCustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.ReturnTypeCustomAttributes, "return: ");
			}
			if (!this.IsCurrentInterface)
			{
				if (e.PrivateImplementationType == null)
				{
					this.OutputMemberAccessModifier(e.Attributes);
					this.OutputVTableModifier(e.Attributes);
					this.OutputMemberScopeModifier(e.Attributes);
				}
			}
			else
			{
				this.OutputVTableModifier(e.Attributes);
			}
			this.OutputType(e.ReturnType);
			this.Output.Write(" ");
			if (e.PrivateImplementationType != null)
			{
				this.Output.Write(this.GetBaseTypeOutput(e.PrivateImplementationType));
				this.Output.Write(".");
			}
			this.OutputIdentifier(e.Name);
			this.OutputTypeParameters(e.TypeParameters);
			this.Output.Write("(");
			this.OutputParameters(e.Parameters);
			this.Output.Write(")");
			this.OutputTypeParameterConstraints(e.TypeParameters);
			if (!this.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				this.OutputStartingBrace();
				int num = this.Indent;
				this.Indent = num + 1;
				this.GenerateStatements(e.Statements);
				num = this.Indent;
				this.Indent = num - 1;
				this.Output.WriteLine("}");
				return;
			}
			this.Output.WriteLine(";");
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000098B4 File Offset: 0x00007AB4
		private void GenerateProperties(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeMemberProperty)
				{
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeMemberProperty codeMemberProperty = (CodeMemberProperty)enumerator.Current;
					if (codeMemberProperty.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeMemberProperty.LinePragma);
					}
					this.GenerateProperty(codeMemberProperty, e);
					if (codeMemberProperty.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeMemberProperty.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000099AC File Offset: 0x00007BAC
		private void GenerateProperty(CodeMemberProperty e, CodeTypeDeclaration c)
		{
			if (!this.IsCurrentClass && !this.IsCurrentStruct && !this.IsCurrentInterface)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			if (!this.IsCurrentInterface)
			{
				if (e.PrivateImplementationType == null)
				{
					this.OutputMemberAccessModifier(e.Attributes);
					this.OutputVTableModifier(e.Attributes);
					this.OutputMemberScopeModifier(e.Attributes);
				}
			}
			else
			{
				this.OutputVTableModifier(e.Attributes);
			}
			this.OutputType(e.Type);
			this.Output.Write(" ");
			if (e.PrivateImplementationType != null && !this.IsCurrentInterface)
			{
				this.Output.Write(this.GetBaseTypeOutput(e.PrivateImplementationType));
				this.Output.Write(".");
			}
			if (e.Parameters.Count > 0 && string.Compare(e.Name, "Item", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.Output.Write("this[");
				this.OutputParameters(e.Parameters);
				this.Output.Write("]");
			}
			else
			{
				this.OutputIdentifier(e.Name);
			}
			this.OutputStartingBrace();
			int num = this.Indent;
			this.Indent = num + 1;
			if (e.HasGet)
			{
				if (this.IsCurrentInterface || (e.Attributes & MemberAttributes.ScopeMask) == MemberAttributes.Abstract)
				{
					this.Output.WriteLine("get;");
				}
				else
				{
					this.Output.Write("get");
					this.OutputStartingBrace();
					num = this.Indent;
					this.Indent = num + 1;
					this.GenerateStatements(e.GetStatements);
					num = this.Indent;
					this.Indent = num - 1;
					this.Output.WriteLine("}");
				}
			}
			if (e.HasSet)
			{
				if (this.IsCurrentInterface || (e.Attributes & MemberAttributes.ScopeMask) == MemberAttributes.Abstract)
				{
					this.Output.WriteLine("set;");
				}
				else
				{
					this.Output.Write("set");
					this.OutputStartingBrace();
					num = this.Indent;
					this.Indent = num + 1;
					this.GenerateStatements(e.SetStatements);
					num = this.Indent;
					this.Indent = num - 1;
					this.Output.WriteLine("}");
				}
			}
			num = this.Indent;
			this.Indent = num - 1;
			this.Output.WriteLine("}");
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00009C14 File Offset: 0x00007E14
		private void GenerateSingleFloatValue(float s)
		{
			if (float.IsNaN(s))
			{
				this.Output.Write("float.NaN");
				return;
			}
			if (float.IsNegativeInfinity(s))
			{
				this.Output.Write("float.NegativeInfinity");
				return;
			}
			if (float.IsPositiveInfinity(s))
			{
				this.Output.Write("float.PositiveInfinity");
				return;
			}
			this.Output.Write(s.ToString(CultureInfo.InvariantCulture));
			this.Output.Write('F');
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00009C90 File Offset: 0x00007E90
		private void GenerateDoubleValue(double d)
		{
			if (double.IsNaN(d))
			{
				this.Output.Write("double.NaN");
				return;
			}
			if (double.IsNegativeInfinity(d))
			{
				this.Output.Write("double.NegativeInfinity");
				return;
			}
			if (double.IsPositiveInfinity(d))
			{
				this.Output.Write("double.PositiveInfinity");
				return;
			}
			this.Output.Write(d.ToString("R", CultureInfo.InvariantCulture));
			this.Output.Write("D");
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00009D14 File Offset: 0x00007F14
		private void GenerateDecimalValue(decimal d)
		{
			this.Output.Write(d.ToString(CultureInfo.InvariantCulture));
			this.Output.Write('m');
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00009D3C File Offset: 0x00007F3C
		private void OutputVTableModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.VTableMask;
			if (memberAttributes == MemberAttributes.New)
			{
				this.Output.Write("new ");
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00009D68 File Offset: 0x00007F68
		private void OutputMemberAccessModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.AccessMask;
			if (memberAttributes <= MemberAttributes.Family)
			{
				if (memberAttributes == MemberAttributes.Assembly)
				{
					this.Output.Write("internal ");
					return;
				}
				if (memberAttributes == MemberAttributes.FamilyAndAssembly)
				{
					this.Output.Write("internal ");
					return;
				}
				if (memberAttributes != MemberAttributes.Family)
				{
					return;
				}
				this.Output.Write("protected ");
				return;
			}
			else
			{
				if (memberAttributes == MemberAttributes.FamilyOrAssembly)
				{
					this.Output.Write("protected internal ");
					return;
				}
				if (memberAttributes == MemberAttributes.Private)
				{
					this.Output.Write("private ");
					return;
				}
				if (memberAttributes != MemberAttributes.Public)
				{
					return;
				}
				this.Output.Write("public ");
				return;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00009E1C File Offset: 0x0000801C
		private void OutputMemberScopeModifier(MemberAttributes attributes)
		{
			switch (attributes & MemberAttributes.ScopeMask)
			{
			case MemberAttributes.Abstract:
				this.Output.Write("abstract ");
				return;
			case MemberAttributes.Final:
				this.Output.Write("");
				return;
			case MemberAttributes.Static:
				this.Output.Write("static ");
				return;
			case MemberAttributes.Override:
				this.Output.Write("override ");
				return;
			default:
			{
				MemberAttributes memberAttributes = attributes & MemberAttributes.AccessMask;
				if (memberAttributes == MemberAttributes.Assembly || memberAttributes == MemberAttributes.Family || memberAttributes == MemberAttributes.Public)
				{
					this.Output.Write("virtual ");
				}
				return;
			}
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00009EBC File Offset: 0x000080BC
		private void OutputOperator(CodeBinaryOperatorType op)
		{
			switch (op)
			{
			case CodeBinaryOperatorType.Add:
				this.Output.Write("+");
				return;
			case CodeBinaryOperatorType.Subtract:
				this.Output.Write("-");
				return;
			case CodeBinaryOperatorType.Multiply:
				this.Output.Write("*");
				return;
			case CodeBinaryOperatorType.Divide:
				this.Output.Write("/");
				return;
			case CodeBinaryOperatorType.Modulus:
				this.Output.Write("%");
				return;
			case CodeBinaryOperatorType.Assign:
				this.Output.Write("=");
				return;
			case CodeBinaryOperatorType.IdentityInequality:
				this.Output.Write("!=");
				return;
			case CodeBinaryOperatorType.IdentityEquality:
				this.Output.Write("==");
				return;
			case CodeBinaryOperatorType.ValueEquality:
				this.Output.Write("==");
				return;
			case CodeBinaryOperatorType.BitwiseOr:
				this.Output.Write("|");
				return;
			case CodeBinaryOperatorType.BitwiseAnd:
				this.Output.Write("&");
				return;
			case CodeBinaryOperatorType.BooleanOr:
				this.Output.Write("||");
				return;
			case CodeBinaryOperatorType.BooleanAnd:
				this.Output.Write("&&");
				return;
			case CodeBinaryOperatorType.LessThan:
				this.Output.Write("<");
				return;
			case CodeBinaryOperatorType.LessThanOrEqual:
				this.Output.Write("<=");
				return;
			case CodeBinaryOperatorType.GreaterThan:
				this.Output.Write(">");
				return;
			case CodeBinaryOperatorType.GreaterThanOrEqual:
				this.Output.Write(">=");
				return;
			default:
				return;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000A034 File Offset: 0x00008234
		private void OutputFieldScopeModifier(MemberAttributes attributes)
		{
			switch (attributes & MemberAttributes.ScopeMask)
			{
			case MemberAttributes.Final:
			case MemberAttributes.Override:
				break;
			case MemberAttributes.Static:
				this.Output.Write("static ");
				return;
			case MemberAttributes.Const:
				this.Output.Write("const ");
				break;
			default:
				return;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000A080 File Offset: 0x00008280
		private void GeneratePropertyReferenceExpression(CodePropertyReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.GenerateExpression(e.TargetObject);
				this.Output.Write(".");
			}
			this.OutputIdentifier(e.PropertyName);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000A0B4 File Offset: 0x000082B4
		private void GenerateConstructors(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeConstructor)
				{
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeConstructor codeConstructor = (CodeConstructor)enumerator.Current;
					if (codeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeConstructor.LinePragma);
					}
					this.GenerateConstructor(codeConstructor, e);
					if (codeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeConstructor.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000A1AC File Offset: 0x000083AC
		private void GenerateConstructor(CodeConstructor e, CodeTypeDeclaration c)
		{
			if (!this.IsCurrentClass && !this.IsCurrentStruct)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			this.OutputMemberAccessModifier(e.Attributes);
			this.OutputIdentifier(this.CurrentTypeName);
			this.Output.Write("(");
			this.OutputParameters(e.Parameters);
			this.Output.Write(")");
			CodeExpressionCollection baseConstructorArgs = e.BaseConstructorArgs;
			CodeExpressionCollection chainedConstructorArgs = e.ChainedConstructorArgs;
			int num;
			if (baseConstructorArgs.Count > 0)
			{
				this.Output.WriteLine(" : ");
				num = this.Indent;
				this.Indent = num + 1;
				num = this.Indent;
				this.Indent = num + 1;
				this.Output.Write("base(");
				this.OutputExpressionList(baseConstructorArgs);
				this.Output.Write(")");
				num = this.Indent;
				this.Indent = num - 1;
				num = this.Indent;
				this.Indent = num - 1;
			}
			if (chainedConstructorArgs.Count > 0)
			{
				this.Output.WriteLine(" : ");
				num = this.Indent;
				this.Indent = num + 1;
				num = this.Indent;
				this.Indent = num + 1;
				this.Output.Write("this(");
				this.OutputExpressionList(chainedConstructorArgs);
				this.Output.Write(")");
				num = this.Indent;
				this.Indent = num - 1;
				num = this.Indent;
				this.Indent = num - 1;
			}
			this.OutputStartingBrace();
			num = this.Indent;
			this.Indent = num + 1;
			this.GenerateStatements(e.Statements);
			num = this.Indent;
			this.Indent = num - 1;
			this.Output.WriteLine("}");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000A378 File Offset: 0x00008578
		private void GenerateTypeConstructor(CodeTypeConstructor e)
		{
			if (!this.IsCurrentClass && !this.IsCurrentStruct)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			this.Output.Write("static ");
			this.Output.Write(this.CurrentTypeName);
			this.Output.Write("()");
			this.OutputStartingBrace();
			int num = this.Indent;
			this.Indent = num + 1;
			this.GenerateStatements(e.Statements);
			num = this.Indent;
			this.Indent = num - 1;
			this.Output.WriteLine("}");
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000A423 File Offset: 0x00008623
		private void GenerateTypeReferenceExpression(CodeTypeReferenceExpression e)
		{
			this.OutputType(e.Type);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000A431 File Offset: 0x00008631
		private void GenerateTypeOfExpression(CodeTypeOfExpression e)
		{
			this.Output.Write("typeof(");
			this.OutputType(e.Type);
			this.Output.Write(")");
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000A460 File Offset: 0x00008660
		private void GenerateType(CodeTypeDeclaration e)
		{
			this.currentClass = e;
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
			this.GenerateCommentStatements(e.Comments);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaStart(e.LinePragma);
			}
			this.GenerateTypeStart(e);
			if (this.Options.VerbatimOrder)
			{
				using (IEnumerator enumerator = e.Members.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
						this.GenerateTypeMember(codeTypeMember, e);
					}
					goto IL_CA;
				}
			}
			this.GenerateFields(e);
			this.GenerateSnippetMembers(e);
			this.GenerateTypeConstructors(e);
			this.GenerateConstructors(e);
			this.GenerateProperties(e);
			this.GenerateEvents(e);
			this.GenerateMethods(e);
			this.GenerateNestedTypes(e);
			IL_CA:
			this.currentClass = e;
			this.GenerateTypeEnd(e);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(e.LinePragma);
			}
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000A584 File Offset: 0x00008784
		private void GenerateTypes(CodeNamespace e)
		{
			foreach (object obj in e.Types)
			{
				CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj;
				if (this.options.BlankLinesBetweenMembers)
				{
					this.Output.WriteLine();
				}
				((ICodeGenerator)this).GenerateCodeFromType(codeTypeDeclaration, this.output.InnerWriter, this.options);
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000A608 File Offset: 0x00008808
		private void GenerateTypeStart(CodeTypeDeclaration e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.CustomAttributes);
			}
			if (this.IsCurrentDelegate)
			{
				TypeAttributes typeAttributes = e.TypeAttributes & TypeAttributes.VisibilityMask;
				if (typeAttributes != TypeAttributes.NotPublic && typeAttributes == TypeAttributes.Public)
				{
					this.Output.Write("public ");
				}
				CodeTypeDelegate codeTypeDelegate = (CodeTypeDelegate)e;
				this.Output.Write("delegate ");
				this.OutputType(codeTypeDelegate.ReturnType);
				this.Output.Write(" ");
				this.OutputIdentifier(e.Name);
				this.Output.Write("(");
				this.OutputParameters(codeTypeDelegate.Parameters);
				this.Output.WriteLine(");");
				return;
			}
			this.OutputTypeAttributes(e);
			this.OutputIdentifier(e.Name);
			this.OutputTypeParameters(e.TypeParameters);
			bool flag = true;
			foreach (object obj in e.BaseTypes)
			{
				CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
				if (flag)
				{
					this.Output.Write(" : ");
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				this.OutputType(codeTypeReference);
			}
			this.OutputTypeParameterConstraints(e.TypeParameters);
			this.OutputStartingBrace();
			int indent = this.Indent;
			this.Indent = indent + 1;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000A784 File Offset: 0x00008984
		private void GenerateTypeMember(CodeTypeMember member, CodeTypeDeclaration declaredType)
		{
			if (this.options.BlankLinesBetweenMembers)
			{
				this.Output.WriteLine();
			}
			if (member is CodeTypeDeclaration)
			{
				((ICodeGenerator)this).GenerateCodeFromType((CodeTypeDeclaration)member, this.output.InnerWriter, this.options);
				this.currentClass = declaredType;
				return;
			}
			if (member.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(member.StartDirectives);
			}
			this.GenerateCommentStatements(member.Comments);
			if (member.LinePragma != null)
			{
				this.GenerateLinePragmaStart(member.LinePragma);
			}
			if (member is CodeMemberField)
			{
				this.GenerateField((CodeMemberField)member);
			}
			else if (member is CodeMemberProperty)
			{
				this.GenerateProperty((CodeMemberProperty)member, declaredType);
			}
			else if (member is CodeMemberMethod)
			{
				if (member is CodeConstructor)
				{
					this.GenerateConstructor((CodeConstructor)member, declaredType);
				}
				else if (member is CodeTypeConstructor)
				{
					this.GenerateTypeConstructor((CodeTypeConstructor)member);
				}
				else if (member is CodeEntryPointMethod)
				{
					this.GenerateEntryPointMethod((CodeEntryPointMethod)member, declaredType);
				}
				else
				{
					this.GenerateMethod((CodeMemberMethod)member, declaredType);
				}
			}
			else if (member is CodeMemberEvent)
			{
				this.GenerateEvent((CodeMemberEvent)member, declaredType);
			}
			else if (member is CodeSnippetTypeMember)
			{
				int indent = this.Indent;
				this.Indent = 0;
				this.GenerateSnippetMember((CodeSnippetTypeMember)member);
				this.Indent = indent;
				this.Output.WriteLine();
			}
			if (member.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(member.LinePragma);
			}
			if (member.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(member.EndDirectives);
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000A91C File Offset: 0x00008B1C
		private void GenerateTypeConstructors(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeTypeConstructor)
				{
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeTypeConstructor codeTypeConstructor = (CodeTypeConstructor)enumerator.Current;
					if (codeTypeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeTypeConstructor.LinePragma);
					}
					this.GenerateTypeConstructor(codeTypeConstructor);
					if (codeTypeConstructor.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeTypeConstructor.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000AA14 File Offset: 0x00008C14
		private void GenerateSnippetMembers(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			bool flag = false;
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeSnippetTypeMember)
				{
					flag = true;
					this.currentMember = (CodeTypeMember)enumerator.Current;
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					this.GenerateCommentStatements(this.currentMember.Comments);
					CodeSnippetTypeMember codeSnippetTypeMember = (CodeSnippetTypeMember)enumerator.Current;
					if (codeSnippetTypeMember.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeSnippetTypeMember.LinePragma);
					}
					int indent = this.Indent;
					this.Indent = 0;
					this.GenerateSnippetMember(codeSnippetTypeMember);
					this.Indent = indent;
					if (codeSnippetTypeMember.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeSnippetTypeMember.LinePragma);
					}
					if (this.currentMember.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.EndDirectives);
					}
				}
			}
			if (flag)
			{
				this.Output.WriteLine();
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000AB34 File Offset: 0x00008D34
		private void GenerateNestedTypes(CodeTypeDeclaration e)
		{
			IEnumerator enumerator = e.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeTypeDeclaration)
				{
					if (this.options.BlankLinesBetweenMembers)
					{
						this.Output.WriteLine();
					}
					CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)enumerator.Current;
					((ICodeGenerator)this).GenerateCodeFromType(codeTypeDeclaration, this.output.InnerWriter, this.options);
				}
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		private void GenerateNamespaces(CodeCompileUnit e)
		{
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				((ICodeGenerator)this).GenerateCodeFromNamespace(codeNamespace, this.output.InnerWriter, this.options);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000AC0C File Offset: 0x00008E0C
		private void OutputAttributeArgument(CodeAttributeArgument arg)
		{
			if (arg.Name != null && arg.Name.Length > 0)
			{
				this.OutputIdentifier(arg.Name);
				this.Output.Write("=");
			}
			((ICodeGenerator)this).GenerateCodeFromExpression(arg.Value, this.output.InnerWriter, this.options);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000AC68 File Offset: 0x00008E68
		private void OutputDirection(FieldDirection dir)
		{
			switch (dir)
			{
			case FieldDirection.In:
				break;
			case FieldDirection.Out:
				this.Output.Write("out ");
				return;
			case FieldDirection.Ref:
				this.Output.Write("ref ");
				break;
			default:
				return;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000AC9E File Offset: 0x00008E9E
		private void OutputExpressionList(CodeExpressionCollection expressions)
		{
			this.OutputExpressionList(expressions, false);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		private void OutputExpressionList(CodeExpressionCollection expressions, bool newlineBetweenItems)
		{
			bool flag = true;
			IEnumerator enumerator = expressions.GetEnumerator();
			int num = this.Indent;
			this.Indent = num + 1;
			while (enumerator.MoveNext())
			{
				if (flag)
				{
					flag = false;
				}
				else if (newlineBetweenItems)
				{
					this.ContinueOnNewLine(",");
				}
				else
				{
					this.Output.Write(", ");
				}
				((ICodeGenerator)this).GenerateCodeFromExpression((CodeExpression)enumerator.Current, this.output.InnerWriter, this.options);
			}
			num = this.Indent;
			this.Indent = num - 1;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000AD34 File Offset: 0x00008F34
		private void OutputParameters(CodeParameterDeclarationExpressionCollection parameters)
		{
			bool flag = true;
			bool flag2 = parameters.Count > 15;
			if (flag2)
			{
				this.Indent += 3;
			}
			foreach (object obj in parameters)
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = (CodeParameterDeclarationExpression)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				if (flag2)
				{
					this.ContinueOnNewLine("");
				}
				this.GenerateExpression(codeParameterDeclarationExpression);
			}
			if (flag2)
			{
				this.Indent -= 3;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000ADB9 File Offset: 0x00008FB9
		private void OutputTypeNamePair(CodeTypeReference typeRef, string name)
		{
			this.OutputType(typeRef);
			this.Output.Write(" ");
			this.OutputIdentifier(name);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000ADDC File Offset: 0x00008FDC
		private void OutputTypeParameters(CodeTypeParameterCollection typeParameters)
		{
			if (typeParameters.Count == 0)
			{
				return;
			}
			this.Output.Write('<');
			bool flag = true;
			for (int i = 0; i < typeParameters.Count; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Output.Write(", ");
				}
				if (typeParameters[i].CustomAttributes.Count > 0)
				{
					this.GenerateAttributes(typeParameters[i].CustomAttributes, null, true);
					this.Output.Write(' ');
				}
				this.Output.Write(typeParameters[i].Name);
			}
			this.Output.Write('>');
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000AE84 File Offset: 0x00009084
		private void OutputTypeParameterConstraints(CodeTypeParameterCollection typeParameters)
		{
			if (typeParameters.Count == 0)
			{
				return;
			}
			for (int i = 0; i < typeParameters.Count; i++)
			{
				this.Output.WriteLine();
				int num = this.Indent;
				this.Indent = num + 1;
				bool flag = true;
				if (typeParameters[i].Constraints.Count > 0)
				{
					foreach (object obj in typeParameters[i].Constraints)
					{
						CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
						if (flag)
						{
							this.Output.Write("where ");
							this.Output.Write(typeParameters[i].Name);
							this.Output.Write(" : ");
							flag = false;
						}
						else
						{
							this.Output.Write(", ");
						}
						this.OutputType(codeTypeReference);
					}
				}
				if (typeParameters[i].HasConstructorConstraint)
				{
					if (flag)
					{
						this.Output.Write("where ");
						this.Output.Write(typeParameters[i].Name);
						this.Output.Write(" : new()");
					}
					else
					{
						this.Output.Write(", new ()");
					}
				}
				num = this.Indent;
				this.Indent = num - 1;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000AFF4 File Offset: 0x000091F4
		private void OutputTypeAttributes(CodeTypeDeclaration e)
		{
			if ((e.Attributes & MemberAttributes.New) != (MemberAttributes)0)
			{
				this.Output.Write("new ");
			}
			TypeAttributes typeAttributes = e.TypeAttributes;
			switch (typeAttributes & TypeAttributes.VisibilityMask)
			{
			case TypeAttributes.NotPublic:
			case TypeAttributes.NestedAssembly:
			case TypeAttributes.NestedFamANDAssem:
				this.Output.Write("internal ");
				break;
			case TypeAttributes.Public:
			case TypeAttributes.NestedPublic:
				this.Output.Write("public ");
				break;
			case TypeAttributes.NestedPrivate:
				this.Output.Write("private ");
				break;
			case TypeAttributes.NestedFamily:
				this.Output.Write("protected ");
				break;
			case TypeAttributes.VisibilityMask:
				this.Output.Write("protected internal ");
				break;
			}
			if (e.IsStruct)
			{
				if (e.IsPartial)
				{
					this.Output.Write("partial ");
				}
				this.Output.Write("struct ");
				return;
			}
			if (e.IsEnum)
			{
				this.Output.Write("enum ");
				return;
			}
			TypeAttributes typeAttributes2 = typeAttributes & TypeAttributes.ClassSemanticsMask;
			if (typeAttributes2 == TypeAttributes.NotPublic)
			{
				if ((typeAttributes & TypeAttributes.Sealed) == TypeAttributes.Sealed)
				{
					this.Output.Write("sealed ");
				}
				if ((typeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract)
				{
					this.Output.Write("abstract ");
				}
				if (e.IsPartial)
				{
					this.Output.Write("partial ");
				}
				this.Output.Write("class ");
				return;
			}
			if (typeAttributes2 != TypeAttributes.ClassSemanticsMask)
			{
				return;
			}
			if (e.IsPartial)
			{
				this.Output.Write("partial ");
			}
			this.Output.Write("interface ");
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000B18C File Offset: 0x0000938C
		private void GenerateTypeEnd(CodeTypeDeclaration e)
		{
			if (!this.IsCurrentDelegate)
			{
				int indent = this.Indent;
				this.Indent = indent - 1;
				this.Output.WriteLine("}");
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000B1C4 File Offset: 0x000093C4
		private void GenerateNamespaceStart(CodeNamespace e)
		{
			if (e.Name != null && e.Name.Length > 0)
			{
				this.Output.Write("namespace ");
				string[] array = e.Name.Split(new char[] { '.' });
				this.OutputIdentifier(array[0]);
				for (int i = 1; i < array.Length; i++)
				{
					this.Output.Write(".");
					this.OutputIdentifier(array[i]);
				}
				this.OutputStartingBrace();
				int indent = this.Indent;
				this.Indent = indent + 1;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000B254 File Offset: 0x00009454
		private void GenerateCompileUnit(CodeCompileUnit e)
		{
			this.GenerateCompileUnitStart(e);
			this.GenerateNamespaces(e);
			this.GenerateCompileUnitEnd(e);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000B26C File Offset: 0x0000946C
		private void GenerateCompileUnitStart(CodeCompileUnit e)
		{
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
			this.Output.WriteLine("//------------------------------------------------------------------------------");
			this.Output.Write("// <");
			this.Output.WriteLine(SR.GetString("AutoGen_Comment_Line1"));
			this.Output.Write("//     ");
			this.Output.WriteLine(SR.GetString("AutoGen_Comment_Line2"));
			this.Output.Write("//     ");
			this.Output.Write(SR.GetString("AutoGen_Comment_Line3"));
			this.Output.WriteLine(Environment.Version.ToString());
			this.Output.WriteLine("//");
			this.Output.Write("//     ");
			this.Output.WriteLine(SR.GetString("AutoGen_Comment_Line4"));
			this.Output.Write("//     ");
			this.Output.WriteLine(SR.GetString("AutoGen_Comment_Line5"));
			this.Output.Write("// </");
			this.Output.WriteLine(SR.GetString("AutoGen_Comment_Line1"));
			this.Output.WriteLine("//------------------------------------------------------------------------------");
			this.Output.WriteLine("");
			SortedList sortedList = new SortedList(StringComparer.Ordinal);
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				if (string.IsNullOrEmpty(codeNamespace.Name))
				{
					codeNamespace.UserData["GenerateImports"] = false;
					foreach (object obj2 in codeNamespace.Imports)
					{
						CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj2;
						if (!sortedList.Contains(codeNamespaceImport.Namespace))
						{
							sortedList.Add(codeNamespaceImport.Namespace, codeNamespaceImport.Namespace);
						}
					}
				}
			}
			foreach (object obj3 in sortedList.Keys)
			{
				string text = (string)obj3;
				this.Output.Write("using ");
				this.OutputIdentifier(text);
				this.Output.WriteLine(";");
			}
			if (sortedList.Keys.Count > 0)
			{
				this.Output.WriteLine("");
			}
			if (e.AssemblyCustomAttributes.Count > 0)
			{
				this.GenerateAttributes(e.AssemblyCustomAttributes, "assembly: ");
				this.Output.WriteLine("");
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000B568 File Offset: 0x00009768
		private void GenerateCompileUnitEnd(CodeCompileUnit e)
		{
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000B584 File Offset: 0x00009784
		private void GenerateDirectionExpression(CodeDirectionExpression e)
		{
			this.OutputDirection(e.Direction);
			this.GenerateExpression(e.Expression);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000B5A0 File Offset: 0x000097A0
		private void GenerateDirectives(CodeDirectiveCollection directives)
		{
			for (int i = 0; i < directives.Count; i++)
			{
				CodeDirective codeDirective = directives[i];
				if (codeDirective is CodeChecksumPragma)
				{
					this.GenerateChecksumPragma((CodeChecksumPragma)codeDirective);
				}
				else if (codeDirective is CodeRegionDirective)
				{
					this.GenerateCodeRegionDirective((CodeRegionDirective)codeDirective);
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000B5F0 File Offset: 0x000097F0
		private void GenerateChecksumPragma(CodeChecksumPragma checksumPragma)
		{
			this.Output.Write("#pragma checksum \"");
			this.Output.Write(checksumPragma.FileName);
			this.Output.Write("\" \"");
			this.Output.Write(checksumPragma.ChecksumAlgorithmId.ToString("B", CultureInfo.InvariantCulture));
			this.Output.Write("\" \"");
			if (checksumPragma.ChecksumData != null)
			{
				foreach (byte b in checksumPragma.ChecksumData)
				{
					this.Output.Write(b.ToString("X2", CultureInfo.InvariantCulture));
				}
			}
			this.Output.WriteLine("\"");
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000B6B0 File Offset: 0x000098B0
		private void GenerateCodeRegionDirective(CodeRegionDirective regionDirective)
		{
			if (regionDirective.RegionMode == CodeRegionMode.Start)
			{
				this.Output.Write("#region ");
				this.Output.WriteLine(regionDirective.RegionText);
				return;
			}
			if (regionDirective.RegionMode == CodeRegionMode.End)
			{
				this.Output.WriteLine("#endregion");
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000B704 File Offset: 0x00009904
		private void GenerateNamespaceEnd(CodeNamespace e)
		{
			if (e.Name != null && e.Name.Length > 0)
			{
				int indent = this.Indent;
				this.Indent = indent - 1;
				this.Output.WriteLine("}");
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000B747 File Offset: 0x00009947
		private void GenerateNamespaceImport(CodeNamespaceImport e)
		{
			this.Output.Write("using ");
			this.OutputIdentifier(e.Namespace);
			this.Output.WriteLine(";");
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000B775 File Offset: 0x00009975
		private void GenerateAttributeDeclarationsStart(CodeAttributeDeclarationCollection attributes)
		{
			this.Output.Write("[");
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000B787 File Offset: 0x00009987
		private void GenerateAttributeDeclarationsEnd(CodeAttributeDeclarationCollection attributes)
		{
			this.Output.Write("]");
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000B799 File Offset: 0x00009999
		private void GenerateAttributes(CodeAttributeDeclarationCollection attributes)
		{
			this.GenerateAttributes(attributes, null, false);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000B7A4 File Offset: 0x000099A4
		private void GenerateAttributes(CodeAttributeDeclarationCollection attributes, string prefix)
		{
			this.GenerateAttributes(attributes, prefix, false);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000B7B0 File Offset: 0x000099B0
		private void GenerateAttributes(CodeAttributeDeclarationCollection attributes, string prefix, bool inLine)
		{
			if (attributes.Count == 0)
			{
				return;
			}
			IEnumerator enumerator = attributes.GetEnumerator();
			bool flag = false;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (codeAttributeDeclaration.Name.Equals("system.paramarrayattribute", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
				}
				else
				{
					this.GenerateAttributeDeclarationsStart(attributes);
					if (prefix != null)
					{
						this.Output.Write(prefix);
					}
					if (codeAttributeDeclaration.AttributeType != null)
					{
						this.Output.Write(this.GetTypeOutput(codeAttributeDeclaration.AttributeType));
					}
					this.Output.Write("(");
					bool flag2 = true;
					foreach (object obj2 in codeAttributeDeclaration.Arguments)
					{
						CodeAttributeArgument codeAttributeArgument = (CodeAttributeArgument)obj2;
						if (flag2)
						{
							flag2 = false;
						}
						else
						{
							this.Output.Write(", ");
						}
						this.OutputAttributeArgument(codeAttributeArgument);
					}
					this.Output.Write(")");
					this.GenerateAttributeDeclarationsEnd(attributes);
					if (inLine)
					{
						this.Output.Write(" ");
					}
					else
					{
						this.Output.WriteLine();
					}
				}
			}
			if (flag)
			{
				if (prefix != null)
				{
					this.Output.Write(prefix);
				}
				this.Output.Write("params");
				if (inLine)
				{
					this.Output.Write(" ");
					return;
				}
				this.Output.WriteLine();
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000B930 File Offset: 0x00009B30
		private static bool IsKeyword(string value)
		{
			return FixedStringLookup.Contains(CSharpCodeGenerator.keywords, value, false);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000B93E File Offset: 0x00009B3E
		private static bool IsPrefixTwoUnderscore(string value)
		{
			return value.Length >= 3 && (value[0] == '_' && value[1] == '_') && value[2] != '_';
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000B971 File Offset: 0x00009B71
		public bool Supports(GeneratorSupport support)
		{
			return (support & (GeneratorSupport.ArraysOfArrays | GeneratorSupport.EntryPointMethod | GeneratorSupport.GotoStatements | GeneratorSupport.MultidimensionalArrays | GeneratorSupport.StaticConstructors | GeneratorSupport.TryCatchStatements | GeneratorSupport.ReturnTypeAttributes | GeneratorSupport.DeclareValueTypes | GeneratorSupport.DeclareEnums | GeneratorSupport.DeclareDelegates | GeneratorSupport.DeclareInterfaces | GeneratorSupport.DeclareEvents | GeneratorSupport.AssemblyAttributes | GeneratorSupport.ParameterAttributes | GeneratorSupport.ReferenceParameters | GeneratorSupport.ChainedConstructorArguments | GeneratorSupport.NestedTypes | GeneratorSupport.MultipleInterfaceMembers | GeneratorSupport.PublicStaticMembers | GeneratorSupport.ComplexExpressions | GeneratorSupport.Win32Resources | GeneratorSupport.Resources | GeneratorSupport.PartialTypes | GeneratorSupport.GenericTypeReference | GeneratorSupport.GenericTypeDeclaration | GeneratorSupport.DeclareIndexerProperties)) == support;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000B980 File Offset: 0x00009B80
		public bool IsValidIdentifier(string value)
		{
			if (value == null || value.Length == 0)
			{
				return false;
			}
			if (value.Length > 512)
			{
				return false;
			}
			if (value[0] != '@')
			{
				if (CSharpCodeGenerator.IsKeyword(value))
				{
					return false;
				}
			}
			else
			{
				value = value.Substring(1);
			}
			return CodeGenerator.IsValidLanguageIndependentIdentifier(value);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000B9CD File Offset: 0x00009BCD
		public void ValidateIdentifier(string value)
		{
			if (!this.IsValidIdentifier(value))
			{
				throw new ArgumentException(SR.GetString("InvalidIdentifier", new object[] { value }));
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000B9F2 File Offset: 0x00009BF2
		public string CreateValidIdentifier(string name)
		{
			if (CSharpCodeGenerator.IsPrefixTwoUnderscore(name))
			{
				name = "_" + name;
			}
			while (CSharpCodeGenerator.IsKeyword(name))
			{
				name = "_" + name;
			}
			return name;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000BA21 File Offset: 0x00009C21
		public string CreateEscapedIdentifier(string name)
		{
			if (CSharpCodeGenerator.IsKeyword(name) || CSharpCodeGenerator.IsPrefixTwoUnderscore(name))
			{
				return "@" + name;
			}
			return name;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000BA40 File Offset: 0x00009C40
		private string GetBaseTypeOutput(CodeTypeReference typeRef)
		{
			string baseType = typeRef.BaseType;
			if (baseType.Length == 0)
			{
				return "void";
			}
			string text = baseType.ToLower(CultureInfo.InvariantCulture).Trim();
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2218649502U)
			{
				if (num <= 574663925U)
				{
					if (num <= 503664103U)
					{
						if (num != 425110298U)
						{
							if (num == 503664103U)
							{
								if (text == "system.string")
								{
									return "string";
								}
							}
						}
						else if (text == "system.char")
						{
							return "char";
						}
					}
					else if (num != 507700544U)
					{
						if (num == 574663925U)
						{
							if (text == "system.uint16")
							{
								return "ushort";
							}
						}
					}
					else if (text == "system.uint64")
					{
						return "ulong";
					}
				}
				else if (num <= 872348156U)
				{
					if (num != 801448826U)
					{
						if (num == 872348156U)
						{
							if (text == "system.byte")
							{
								return "byte";
							}
						}
					}
					else if (text == "system.int32")
					{
						return "int";
					}
				}
				else if (num != 1487069339U)
				{
					if (num == 2218649502U)
					{
						if (text == "system.boolean")
						{
							return "bool";
						}
					}
				}
				else if (text == "system.double")
				{
					return "double";
				}
			}
			else if (num <= 2679997701U)
			{
				if (num <= 2613725868U)
				{
					if (num != 2446023237U)
					{
						if (num == 2613725868U)
						{
							if (text == "system.int16")
							{
								return "short";
							}
						}
					}
					else if (text == "system.decimal")
					{
						return "decimal";
					}
				}
				else if (num != 2647511797U)
				{
					if (num == 2679997701U)
					{
						if (text == "system.int64")
						{
							return "long";
						}
					}
				}
				else if (text == "system.object")
				{
					return "object";
				}
			}
			else if (num <= 2923133227U)
			{
				if (num != 2790997960U)
				{
					if (num == 2923133227U)
					{
						if (text == "system.uint32")
						{
							return "uint";
						}
					}
				}
				else if (text == "system.void")
				{
					return "void";
				}
			}
			else if (num != 3248684926U)
			{
				if (num == 3680803037U)
				{
					if (text == "system.sbyte")
					{
						return "sbyte";
					}
				}
			}
			else if (text == "system.single")
			{
				return "float";
			}
			StringBuilder stringBuilder = new StringBuilder(baseType.Length + 10);
			if ((typeRef.Options & CodeTypeReferenceOptions.GlobalReference) != (CodeTypeReferenceOptions)0)
			{
				stringBuilder.Append("global::");
			}
			string baseType2 = typeRef.BaseType;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < baseType2.Length; i++)
			{
				char c = baseType2[i];
				if (c != '+' && c != '.')
				{
					if (c == '`')
					{
						stringBuilder.Append(this.CreateEscapedIdentifier(baseType2.Substring(num2, i - num2)));
						i++;
						int num4 = 0;
						while (i < baseType2.Length && baseType2[i] >= '0' && baseType2[i] <= '9')
						{
							num4 = num4 * 10 + (int)(baseType2[i] - '0');
							i++;
						}
						this.GetTypeArgumentsOutput(typeRef.TypeArguments, num3, num4, stringBuilder);
						num3 += num4;
						if (i < baseType2.Length && (baseType2[i] == '+' || baseType2[i] == '.'))
						{
							stringBuilder.Append('.');
							i++;
						}
						num2 = i;
					}
				}
				else
				{
					stringBuilder.Append(this.CreateEscapedIdentifier(baseType2.Substring(num2, i - num2)));
					stringBuilder.Append('.');
					i++;
					num2 = i;
				}
			}
			if (num2 < baseType2.Length)
			{
				stringBuilder.Append(this.CreateEscapedIdentifier(baseType2.Substring(num2)));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000BF18 File Offset: 0x0000A118
		private string GetTypeArgumentsOutput(CodeTypeReferenceCollection typeArguments)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			this.GetTypeArgumentsOutput(typeArguments, 0, typeArguments.Count, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000BF48 File Offset: 0x0000A148
		private void GetTypeArgumentsOutput(CodeTypeReferenceCollection typeArguments, int start, int length, StringBuilder sb)
		{
			sb.Append('<');
			bool flag = true;
			for (int i = start; i < start + length; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					sb.Append(", ");
				}
				if (i < typeArguments.Count)
				{
					sb.Append(this.GetTypeOutput(typeArguments[i]));
				}
			}
			sb.Append('>');
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		public string GetTypeOutput(CodeTypeReference typeRef)
		{
			string text = string.Empty;
			CodeTypeReference codeTypeReference = typeRef;
			while (codeTypeReference.ArrayElementType != null)
			{
				codeTypeReference = codeTypeReference.ArrayElementType;
			}
			text += this.GetBaseTypeOutput(codeTypeReference);
			while (typeRef != null && typeRef.ArrayRank > 0)
			{
				char[] array = new char[typeRef.ArrayRank + 1];
				array[0] = '[';
				array[typeRef.ArrayRank] = ']';
				for (int i = 1; i < typeRef.ArrayRank; i++)
				{
					array[i] = ',';
				}
				text += new string(array);
				typeRef = typeRef.ArrayElementType;
			}
			return text;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000C038 File Offset: 0x0000A238
		private void OutputStartingBrace()
		{
			if (this.Options.BracingStyle == "C")
			{
				this.Output.WriteLine("");
				this.Output.WriteLine("{");
				return;
			}
			this.Output.WriteLine(" {");
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000C090 File Offset: 0x0000A290
		private CompilerResults FromFileBatch(CompilerParameters options, string[] fileNames)
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
			string text3 = "pdb";
			if (options.CompilerOptions != null && -1 != CultureInfo.InvariantCulture.CompareInfo.IndexOf(options.CompilerOptions, "/debug:pdbonly", CompareOptions.IgnoreCase))
			{
				compilerResults.TempFiles.AddExtension(text3, true);
			}
			else
			{
				compilerResults.TempFiles.AddExtension(text3);
			}
			string text4 = this.CmdArgsFromParameters(options) + " " + CSharpCodeGenerator.JoinStringArray(fileNames, " ");
			string responseFileCmdArgs = this.GetResponseFileCmdArgs(options, text4);
			string text5 = null;
			if (responseFileCmdArgs != null)
			{
				text5 = text4;
				text4 = responseFileCmdArgs;
			}
			this.Compile(options, RedistVersionInfo.GetCompilerPath(this.provOptions, this.CompilerName), this.CompilerName, text4, ref text, ref num, text5);
			compilerResults.NativeCompilerReturnValue = num;
			if (num != 0 || options.WarningLevel > 0)
			{
				string[] array = CSharpCodeGenerator.ReadAllLines(text, Encoding.UTF8, FileShare.ReadWrite);
				foreach (string text6 in array)
				{
					compilerResults.Output.Add(text6);
					this.ProcessCompilerOutputLine(compilerResults, text6);
				}
				if (num != 0 && flag)
				{
					File.Delete(options.OutputAssembly);
				}
			}
			if (compilerResults.Errors.HasErrors || !options.GenerateInMemory)
			{
				compilerResults.PathToAssembly = options.OutputAssembly;
				return compilerResults;
			}
			byte[] array3;
			if (!FileIntegrity.IsEnabled)
			{
				array3 = File.ReadAllBytes(options.OutputAssembly);
			}
			else
			{
				using (FileStream fileStream = new FileStream(options.OutputAssembly, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					if (!FileIntegrity.IsTrusted(fileStream.SafeFileHandle))
					{
						throw new IOException(SR.GetString("FileIntegrityCheckFailed", new object[] { options.OutputAssembly }));
					}
					int num2 = (int)fileStream.Length;
					array3 = new byte[num2];
					fileStream.Read(array3, 0, num2);
				}
			}
			byte[] array4 = null;
			try
			{
				string text7 = options.TempFiles.BasePath + "." + text3;
				if (File.Exists(text7))
				{
					array4 = File.ReadAllBytes(text7);
				}
			}
			catch
			{
				array4 = null;
			}
			SecurityPermission securityPermission2 = new SecurityPermission(SecurityPermissionFlag.ControlEvidence);
			securityPermission2.Assert();
			try
			{
				if (!FileIntegrity.IsEnabled)
				{
					compilerResults.CompiledAssembly = Assembly.Load(array3, array4, options.Evidence);
				}
				else
				{
					compilerResults.CompiledAssembly = CodeCompiler.LoadImageSkipIntegrityCheck(array3, array4, options.Evidence);
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return compilerResults;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		private static string[] ReadAllLines(string file, Encoding encoding, FileShare share)
		{
			string[] array;
			using (FileStream fileStream = File.Open(file, FileMode.Open, FileAccess.Read, share))
			{
				List<string> list = new List<string>();
				using (StreamReader streamReader = new StreamReader(fileStream, encoding))
				{
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						list.Add(text);
					}
				}
				array = list.ToArray();
			}
			return array;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000C43C File Offset: 0x0000A63C
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

		// Token: 0x0600012D RID: 301 RVA: 0x0000C480 File Offset: 0x0000A680
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

		// Token: 0x0600012E RID: 302 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
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

		// Token: 0x0600012F RID: 303 RVA: 0x0000C508 File Offset: 0x0000A708
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

		// Token: 0x06000130 RID: 304 RVA: 0x0000C54C File Offset: 0x0000A74C
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

		// Token: 0x06000131 RID: 305 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
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

		// Token: 0x06000132 RID: 306 RVA: 0x0000C618 File Offset: 0x0000A818
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

		// Token: 0x06000133 RID: 307 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
		private CompilerResults FromDom(CompilerParameters options, CodeCompileUnit e)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			return this.FromDomBatch(options, new CodeCompileUnit[] { e });
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		private CompilerResults FromFile(CompilerParameters options, string fileName)
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

		// Token: 0x06000135 RID: 309 RVA: 0x0000C758 File Offset: 0x0000A958
		private CompilerResults FromSource(CompilerParameters options, string source)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			return this.FromSourceBatch(options, new string[] { source });
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000C794 File Offset: 0x0000A994
		private CompilerResults FromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
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
									((ICodeGenerator)this).GenerateCodeFromCompileUnit(ea[i], streamWriter, this.Options);
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

		// Token: 0x06000137 RID: 311 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
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

		// Token: 0x06000138 RID: 312 RVA: 0x0000C928 File Offset: 0x0000AB28
		private CompilerResults FromSourceBatch(CompilerParameters options, string[] sources)
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

		// Token: 0x06000139 RID: 313 RVA: 0x0000CA78 File Offset: 0x0000AC78
		private static string JoinStringArray(string[] sa, string separator)
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

		// Token: 0x0600013A RID: 314 RVA: 0x0000CB18 File Offset: 0x0000AD18
		void ICodeGenerator.GenerateCodeFromType(CodeTypeDeclaration e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this.output != null && w != this.output.InnerWriter)
			{
				throw new InvalidOperationException(SR.GetString("CodeGenOutputWriter"));
			}
			if (this.output == null)
			{
				flag = true;
				this.options = ((o == null) ? new CodeGeneratorOptions() : o);
				this.output = new IndentedTextWriter(w, this.options.IndentString);
			}
			try
			{
				this.GenerateType(e);
			}
			finally
			{
				if (flag)
				{
					this.output = null;
					this.options = null;
				}
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000CBAC File Offset: 0x0000ADAC
		void ICodeGenerator.GenerateCodeFromExpression(CodeExpression e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this.output != null && w != this.output.InnerWriter)
			{
				throw new InvalidOperationException(SR.GetString("CodeGenOutputWriter"));
			}
			if (this.output == null)
			{
				flag = true;
				this.options = ((o == null) ? new CodeGeneratorOptions() : o);
				this.output = new IndentedTextWriter(w, this.options.IndentString);
			}
			try
			{
				this.GenerateExpression(e);
			}
			finally
			{
				if (flag)
				{
					this.output = null;
					this.options = null;
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000CC40 File Offset: 0x0000AE40
		void ICodeGenerator.GenerateCodeFromCompileUnit(CodeCompileUnit e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this.output != null && w != this.output.InnerWriter)
			{
				throw new InvalidOperationException(SR.GetString("CodeGenOutputWriter"));
			}
			if (this.output == null)
			{
				flag = true;
				this.options = ((o == null) ? new CodeGeneratorOptions() : o);
				this.output = new IndentedTextWriter(w, this.options.IndentString);
			}
			try
			{
				if (e is CodeSnippetCompileUnit)
				{
					this.GenerateSnippetCompileUnit((CodeSnippetCompileUnit)e);
				}
				else
				{
					this.GenerateCompileUnit(e);
				}
			}
			finally
			{
				if (flag)
				{
					this.output = null;
					this.options = null;
				}
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000CCEC File Offset: 0x0000AEEC
		void ICodeGenerator.GenerateCodeFromNamespace(CodeNamespace e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this.output != null && w != this.output.InnerWriter)
			{
				throw new InvalidOperationException(SR.GetString("CodeGenOutputWriter"));
			}
			if (this.output == null)
			{
				flag = true;
				this.options = ((o == null) ? new CodeGeneratorOptions() : o);
				this.output = new IndentedTextWriter(w, this.options.IndentString);
			}
			try
			{
				this.GenerateNamespace(e);
			}
			finally
			{
				if (flag)
				{
					this.output = null;
					this.options = null;
				}
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000CD80 File Offset: 0x0000AF80
		void ICodeGenerator.GenerateCodeFromStatement(CodeStatement e, TextWriter w, CodeGeneratorOptions o)
		{
			bool flag = false;
			if (this.output != null && w != this.output.InnerWriter)
			{
				throw new InvalidOperationException(SR.GetString("CodeGenOutputWriter"));
			}
			if (this.output == null)
			{
				flag = true;
				this.options = ((o == null) ? new CodeGeneratorOptions() : o);
				this.output = new IndentedTextWriter(w, this.options.IndentString);
			}
			try
			{
				this.GenerateStatement(e);
			}
			finally
			{
				if (flag)
				{
					this.output = null;
					this.options = null;
				}
			}
		}

		// Token: 0x04000067 RID: 103
		private IndentedTextWriter output;

		// Token: 0x04000068 RID: 104
		private CodeGeneratorOptions options;

		// Token: 0x04000069 RID: 105
		private CodeTypeDeclaration currentClass;

		// Token: 0x0400006A RID: 106
		private CodeTypeMember currentMember;

		// Token: 0x0400006B RID: 107
		private bool inNestedBinary;

		// Token: 0x0400006C RID: 108
		private IDictionary<string, string> provOptions;

		// Token: 0x0400006D RID: 109
		private const int ParameterMultilineThreshold = 15;

		// Token: 0x0400006E RID: 110
		private const int MaxLineLength = 80;

		// Token: 0x0400006F RID: 111
		private const GeneratorSupport LanguageSupport = GeneratorSupport.ArraysOfArrays | GeneratorSupport.EntryPointMethod | GeneratorSupport.GotoStatements | GeneratorSupport.MultidimensionalArrays | GeneratorSupport.StaticConstructors | GeneratorSupport.TryCatchStatements | GeneratorSupport.ReturnTypeAttributes | GeneratorSupport.DeclareValueTypes | GeneratorSupport.DeclareEnums | GeneratorSupport.DeclareDelegates | GeneratorSupport.DeclareInterfaces | GeneratorSupport.DeclareEvents | GeneratorSupport.AssemblyAttributes | GeneratorSupport.ParameterAttributes | GeneratorSupport.ReferenceParameters | GeneratorSupport.ChainedConstructorArguments | GeneratorSupport.NestedTypes | GeneratorSupport.MultipleInterfaceMembers | GeneratorSupport.PublicStaticMembers | GeneratorSupport.ComplexExpressions | GeneratorSupport.Win32Resources | GeneratorSupport.Resources | GeneratorSupport.PartialTypes | GeneratorSupport.GenericTypeReference | GeneratorSupport.GenericTypeDeclaration | GeneratorSupport.DeclareIndexerProperties;

		// Token: 0x04000070 RID: 112
		private static volatile Regex outputRegWithFileAndLine;

		// Token: 0x04000071 RID: 113
		private static volatile Regex outputRegSimple;

		// Token: 0x04000072 RID: 114
		private static readonly string[][] keywords = new string[][]
		{
			null,
			new string[] { "as", "do", "if", "in", "is" },
			new string[] { "for", "int", "new", "out", "ref", "try" },
			new string[]
			{
				"base", "bool", "byte", "case", "char", "else", "enum", "goto", "lock", "long",
				"null", "this", "true", "uint", "void"
			},
			new string[]
			{
				"break", "catch", "class", "const", "event", "false", "fixed", "float", "sbyte", "short",
				"throw", "ulong", "using", "while"
			},
			new string[]
			{
				"double", "extern", "object", "params", "public", "return", "sealed", "sizeof", "static", "string",
				"struct", "switch", "typeof", "unsafe", "ushort"
			},
			new string[] { "checked", "decimal", "default", "finally", "foreach", "private", "virtual" },
			new string[] { "abstract", "continue", "delegate", "explicit", "implicit", "internal", "operator", "override", "readonly", "volatile" },
			new string[] { "__arglist", "__makeref", "__reftype", "interface", "namespace", "protected", "unchecked" },
			new string[] { "__refvalue", "stackalloc" }
		};

		// Token: 0x04000073 RID: 115
		private bool generatingForLoop;
	}
}
