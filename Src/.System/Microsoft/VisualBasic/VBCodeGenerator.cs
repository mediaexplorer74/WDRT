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
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.VisualBasic
{
	// Token: 0x02000008 RID: 8
	internal class VBCodeGenerator : CodeCompiler
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020E8 File Offset: 0x000002E8
		internal VBCodeGenerator()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020F0 File Offset: 0x000002F0
		internal VBCodeGenerator(IDictionary<string, string> providerOptions)
		{
			this.provOptions = providerOptions;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020FF File Offset: 0x000002FF
		protected override string FileExtension
		{
			get
			{
				return ".vb";
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002106 File Offset: 0x00000306
		protected override string CompilerName
		{
			get
			{
				return "vbc.exe";
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000210D File Offset: 0x0000030D
		private bool IsCurrentModule
		{
			get
			{
				return base.IsCurrentClass && this.GetUserData(base.CurrentClass, "Module", false);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000212B File Offset: 0x0000032B
		protected override string NullToken
		{
			get
			{
				return "Nothing";
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002132 File Offset: 0x00000332
		private void EnsureInDoubleQuotes(ref bool fInDoubleQuotes, StringBuilder b)
		{
			if (fInDoubleQuotes)
			{
				return;
			}
			b.Append("&\"");
			fInDoubleQuotes = true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002148 File Offset: 0x00000348
		private void EnsureNotInDoubleQuotes(ref bool fInDoubleQuotes, StringBuilder b)
		{
			if (!fInDoubleQuotes)
			{
				return;
			}
			b.Append("\"");
			fInDoubleQuotes = false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002160 File Offset: 0x00000360
		protected override string QuoteSnippetString(string value)
		{
			StringBuilder stringBuilder = new StringBuilder(value.Length + 5);
			bool flag = true;
			Indentation indentation = new Indentation((IndentedTextWriter)base.Output, base.Indent + 1);
			stringBuilder.Append("\"");
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c <= '“')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								this.EnsureNotInDoubleQuotes(ref flag, stringBuilder);
								stringBuilder.Append("&Global.Microsoft.VisualBasic.ChrW(9)");
								break;
							case '\n':
								this.EnsureNotInDoubleQuotes(ref flag, stringBuilder);
								stringBuilder.Append("&Global.Microsoft.VisualBasic.ChrW(10)");
								break;
							case '\v':
							case '\f':
								goto IL_186;
							case '\r':
								this.EnsureNotInDoubleQuotes(ref flag, stringBuilder);
								if (i < value.Length - 1 && value[i + 1] == '\n')
								{
									stringBuilder.Append("&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)");
									i++;
								}
								else
								{
									stringBuilder.Append("&Global.Microsoft.VisualBasic.ChrW(13)");
								}
								break;
							default:
								goto IL_186;
							}
						}
						else
						{
							this.EnsureNotInDoubleQuotes(ref flag, stringBuilder);
							stringBuilder.Append("&Global.Microsoft.VisualBasic.ChrW(0)");
						}
					}
					else
					{
						if (c != '"' && c != '“')
						{
							goto IL_186;
						}
						goto IL_CD;
					}
				}
				else
				{
					if (c <= '\u2028')
					{
						if (c == '”')
						{
							goto IL_CD;
						}
						if (c != '\u2028')
						{
							goto IL_186;
						}
					}
					else if (c != '\u2029')
					{
						if (c == '＂')
						{
							goto IL_CD;
						}
						goto IL_186;
					}
					this.EnsureNotInDoubleQuotes(ref flag, stringBuilder);
					VBCodeGenerator.AppendEscapedChar(stringBuilder, c);
				}
				IL_19D:
				if (i > 0 && i % 80 == 0)
				{
					if (char.IsHighSurrogate(value[i]) && i < value.Length - 1 && char.IsLowSurrogate(value[i + 1]))
					{
						stringBuilder.Append(value[++i]);
					}
					if (flag)
					{
						stringBuilder.Append("\"");
					}
					flag = true;
					stringBuilder.Append("& _ ");
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(indentation.IndentationString);
					stringBuilder.Append('"');
				}
				i++;
				continue;
				IL_CD:
				this.EnsureInDoubleQuotes(ref flag, stringBuilder);
				stringBuilder.Append(c);
				stringBuilder.Append(c);
				goto IL_19D;
				IL_186:
				this.EnsureInDoubleQuotes(ref flag, stringBuilder);
				stringBuilder.Append(value[i]);
				goto IL_19D;
			}
			if (flag)
			{
				stringBuilder.Append("\"");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023B8 File Offset: 0x000005B8
		private static void AppendEscapedChar(StringBuilder b, char value)
		{
			b.Append("&Global.Microsoft.VisualBasic.ChrW(");
			int num = (int)value;
			b.Append(num.ToString(CultureInfo.InvariantCulture));
			b.Append(")");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023F4 File Offset: 0x000005F4
		protected override void ProcessCompilerOutputLine(CompilerResults results, string line)
		{
			if (VBCodeGenerator.outputReg == null)
			{
				VBCodeGenerator.outputReg = new Regex("^([^(]*)\\(?([0-9]*)\\)? ?:? ?(error|warning) ([A-Z]+[0-9]+) ?: ((.|\\n)*)");
			}
			Match match = VBCodeGenerator.outputReg.Match(line);
			if (match.Success)
			{
				CompilerError compilerError = new CompilerError();
				compilerError.FileName = match.Groups[1].Value;
				string value = match.Groups[2].Value;
				if (value != null && value.Length > 0)
				{
					compilerError.Line = int.Parse(value, CultureInfo.InvariantCulture);
				}
				if (string.Compare(match.Groups[3].Value, "warning", StringComparison.OrdinalIgnoreCase) == 0)
				{
					compilerError.IsWarning = true;
				}
				compilerError.ErrorNumber = match.Groups[4].Value;
				compilerError.ErrorText = match.Groups[5].Value;
				results.Errors.Add(compilerError);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024E0 File Offset: 0x000006E0
		protected override string CmdArgsFromParameters(CompilerParameters options)
		{
			foreach (string text in options.ReferencedAssemblies)
			{
				if (string.IsNullOrEmpty(text))
				{
					throw new ArgumentException(SR.GetString("NullOrEmpty_Value_in_Property", new object[] { "ReferencedAssemblies" }), "options");
				}
			}
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
			string text2 = options.CoreAssemblyFileName;
			string text3;
			if (string.IsNullOrWhiteSpace(options.CoreAssemblyFileName) && CodeDomProvider.TryGetProbableCoreAssemblyFilePath(options, out text3))
			{
				text2 = text3;
			}
			if (!string.IsNullOrWhiteSpace(text2))
			{
				string text4 = text2.Trim();
				string directoryName = Path.GetDirectoryName(text4);
				stringBuilder.Append("/nostdlib ");
				stringBuilder.Append("/sdkpath:\"").Append(directoryName).Append("\" ");
				stringBuilder.Append("/R:\"").Append(text4).Append("\" ");
			}
			foreach (string text5 in options.ReferencedAssemblies)
			{
				string fileName = Path.GetFileName(text5);
				if (string.Compare(fileName, "Microsoft.VisualBasic.dll", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(fileName, "mscorlib.dll", StringComparison.OrdinalIgnoreCase) != 0)
				{
					stringBuilder.Append("/R:");
					stringBuilder.Append("\"");
					stringBuilder.Append(text5);
					stringBuilder.Append("\"");
					stringBuilder.Append(" ");
				}
			}
			stringBuilder.Append("/out:");
			stringBuilder.Append("\"");
			stringBuilder.Append(options.OutputAssembly);
			stringBuilder.Append("\"");
			stringBuilder.Append(" ");
			if (options.IncludeDebugInformation)
			{
				stringBuilder.Append("/D:DEBUG=1 ");
				stringBuilder.Append("/debug+ ");
			}
			else
			{
				stringBuilder.Append("/debug- ");
			}
			if (options.Win32Resource != null)
			{
				stringBuilder.Append("/win32resource:\"" + options.Win32Resource + "\" ");
			}
			foreach (string text6 in options.EmbeddedResources)
			{
				stringBuilder.Append("/res:\"");
				stringBuilder.Append(text6);
				stringBuilder.Append("\" ");
			}
			foreach (string text7 in options.LinkedResources)
			{
				stringBuilder.Append("/linkres:\"");
				stringBuilder.Append(text7);
				stringBuilder.Append("\" ");
			}
			if (options.TreatWarningsAsErrors)
			{
				stringBuilder.Append("/warnaserror+ ");
			}
			if (options.CompilerOptions != null)
			{
				stringBuilder.Append(options.CompilerOptions + " ");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002888 File Offset: 0x00000A88
		protected override void OutputAttributeArgument(CodeAttributeArgument arg)
		{
			if (arg.Name != null && arg.Name.Length > 0)
			{
				this.OutputIdentifier(arg.Name);
				base.Output.Write(":=");
			}
			((ICodeGenerator)this).GenerateCodeFromExpression(arg.Value, ((IndentedTextWriter)base.Output).InnerWriter, base.Options);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000028E9 File Offset: 0x00000AE9
		private void OutputAttributes(CodeAttributeDeclarationCollection attributes, bool inLine)
		{
			this.OutputAttributes(attributes, inLine, null, false);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000028F8 File Offset: 0x00000AF8
		private void OutputAttributes(CodeAttributeDeclarationCollection attributes, bool inLine, string prefix, bool closingLine)
		{
			if (attributes.Count == 0)
			{
				return;
			}
			IEnumerator enumerator = attributes.GetEnumerator();
			bool flag = true;
			this.GenerateAttributeDeclarationsStart(attributes);
			while (enumerator.MoveNext())
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					base.Output.Write(", ");
					if (!inLine)
					{
						this.ContinueOnNewLine("");
						base.Output.Write(" ");
					}
				}
				if (prefix != null && prefix.Length > 0)
				{
					base.Output.Write(prefix);
				}
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)enumerator.Current;
				if (codeAttributeDeclaration.AttributeType != null)
				{
					base.Output.Write(this.GetTypeOutput(codeAttributeDeclaration.AttributeType));
				}
				base.Output.Write("(");
				bool flag2 = true;
				foreach (object obj in codeAttributeDeclaration.Arguments)
				{
					CodeAttributeArgument codeAttributeArgument = (CodeAttributeArgument)obj;
					if (flag2)
					{
						flag2 = false;
					}
					else
					{
						base.Output.Write(", ");
					}
					this.OutputAttributeArgument(codeAttributeArgument);
				}
				base.Output.Write(")");
			}
			this.GenerateAttributeDeclarationsEnd(attributes);
			base.Output.Write(" ");
			if (!inLine)
			{
				if (closingLine)
				{
					base.Output.WriteLine();
					return;
				}
				this.ContinueOnNewLine("");
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002A6C File Offset: 0x00000C6C
		protected override void OutputDirection(FieldDirection dir)
		{
			if (dir == FieldDirection.In)
			{
				base.Output.Write("ByVal ");
				return;
			}
			if (dir - FieldDirection.Out > 1)
			{
				return;
			}
			base.Output.Write("ByRef ");
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002A99 File Offset: 0x00000C99
		protected override void GenerateDefaultValueExpression(CodeDefaultValueExpression e)
		{
			base.Output.Write("CType(Nothing, " + this.GetTypeOutput(e.Type) + ")");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002AC1 File Offset: 0x00000CC1
		protected override void GenerateDirectionExpression(CodeDirectionExpression e)
		{
			base.GenerateExpression(e.Expression);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002AD0 File Offset: 0x00000CD0
		protected override void OutputFieldScopeModifier(MemberAttributes attributes)
		{
			switch (attributes & MemberAttributes.ScopeMask)
			{
			case MemberAttributes.Final:
				base.Output.Write("");
				return;
			case MemberAttributes.Static:
				if (!this.IsCurrentModule)
				{
					base.Output.Write("Shared ");
					return;
				}
				return;
			case MemberAttributes.Const:
				base.Output.Write("Const ");
				return;
			}
			base.Output.Write("");
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B48 File Offset: 0x00000D48
		protected override void OutputMemberAccessModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.AccessMask;
			if (memberAttributes <= MemberAttributes.Family)
			{
				if (memberAttributes == MemberAttributes.Assembly)
				{
					base.Output.Write("Friend ");
					return;
				}
				if (memberAttributes == MemberAttributes.FamilyAndAssembly)
				{
					base.Output.Write("Friend ");
					return;
				}
				if (memberAttributes != MemberAttributes.Family)
				{
					return;
				}
				base.Output.Write("Protected ");
				return;
			}
			else
			{
				if (memberAttributes == MemberAttributes.FamilyOrAssembly)
				{
					base.Output.Write("Protected Friend ");
					return;
				}
				if (memberAttributes == MemberAttributes.Private)
				{
					base.Output.Write("Private ");
					return;
				}
				if (memberAttributes != MemberAttributes.Public)
				{
					return;
				}
				base.Output.Write("Public ");
				return;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002BFC File Offset: 0x00000DFC
		private void OutputVTableModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.VTableMask;
			if (memberAttributes == MemberAttributes.New)
			{
				base.Output.Write("Shadows ");
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002C28 File Offset: 0x00000E28
		protected override void OutputMemberScopeModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.ScopeMask;
			switch (memberAttributes)
			{
			case MemberAttributes.Abstract:
				base.Output.Write("MustOverride ");
				return;
			case MemberAttributes.Final:
				base.Output.Write("");
				return;
			case MemberAttributes.Static:
				if (!this.IsCurrentModule)
				{
					base.Output.Write("Shared ");
					return;
				}
				break;
			case MemberAttributes.Override:
				base.Output.Write("Overrides ");
				return;
			default:
			{
				if (memberAttributes == MemberAttributes.Private)
				{
					base.Output.Write("Private ");
					return;
				}
				MemberAttributes memberAttributes2 = attributes & MemberAttributes.AccessMask;
				if (memberAttributes2 == MemberAttributes.Assembly || memberAttributes2 == MemberAttributes.Family || memberAttributes2 == MemberAttributes.Public)
				{
					base.Output.Write("Overridable ");
				}
				break;
			}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002CEC File Offset: 0x00000EEC
		protected override void OutputOperator(CodeBinaryOperatorType op)
		{
			switch (op)
			{
			case CodeBinaryOperatorType.Modulus:
				base.Output.Write("Mod");
				return;
			case CodeBinaryOperatorType.IdentityInequality:
				base.Output.Write("<>");
				return;
			case CodeBinaryOperatorType.IdentityEquality:
				base.Output.Write("Is");
				return;
			case CodeBinaryOperatorType.ValueEquality:
				base.Output.Write("=");
				return;
			case CodeBinaryOperatorType.BitwiseOr:
				base.Output.Write("Or");
				return;
			case CodeBinaryOperatorType.BitwiseAnd:
				base.Output.Write("And");
				return;
			case CodeBinaryOperatorType.BooleanOr:
				base.Output.Write("OrElse");
				return;
			case CodeBinaryOperatorType.BooleanAnd:
				base.Output.Write("AndAlso");
				return;
			}
			base.OutputOperator(op);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002DBC File Offset: 0x00000FBC
		private void GenerateNotIsNullExpression(CodeExpression e)
		{
			base.Output.Write("(Not (");
			base.GenerateExpression(e);
			base.Output.Write(") Is ");
			base.Output.Write(this.NullToken);
			base.Output.Write(")");
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E14 File Offset: 0x00001014
		protected override void GenerateBinaryOperatorExpression(CodeBinaryOperatorExpression e)
		{
			if (e.Operator != CodeBinaryOperatorType.IdentityInequality)
			{
				base.GenerateBinaryOperatorExpression(e);
				return;
			}
			if (e.Right is CodePrimitiveExpression && ((CodePrimitiveExpression)e.Right).Value == null)
			{
				this.GenerateNotIsNullExpression(e.Left);
				return;
			}
			if (e.Left is CodePrimitiveExpression && ((CodePrimitiveExpression)e.Left).Value == null)
			{
				this.GenerateNotIsNullExpression(e.Right);
				return;
			}
			base.GenerateBinaryOperatorExpression(e);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002E91 File Offset: 0x00001091
		protected override string GetResponseFileCmdArgs(CompilerParameters options, string cmdArgs)
		{
			return "/noconfig " + base.GetResponseFileCmdArgs(options, cmdArgs);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002EA5 File Offset: 0x000010A5
		protected override void OutputIdentifier(string ident)
		{
			base.Output.Write(this.CreateEscapedIdentifier(ident));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002EB9 File Offset: 0x000010B9
		protected override void OutputType(CodeTypeReference typeRef)
		{
			base.Output.Write(this.GetTypeOutputWithoutArrayPostFix(typeRef));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002ED0 File Offset: 0x000010D0
		private void OutputTypeAttributes(CodeTypeDeclaration e)
		{
			if ((e.Attributes & MemberAttributes.New) != (MemberAttributes)0)
			{
				base.Output.Write("Shadows ");
			}
			TypeAttributes typeAttributes = e.TypeAttributes;
			if (e.IsPartial)
			{
				base.Output.Write("Partial ");
			}
			switch (typeAttributes & TypeAttributes.VisibilityMask)
			{
			case TypeAttributes.NotPublic:
			case TypeAttributes.NestedAssembly:
			case TypeAttributes.NestedFamANDAssem:
				base.Output.Write("Friend ");
				break;
			case TypeAttributes.Public:
			case TypeAttributes.NestedPublic:
				base.Output.Write("Public ");
				break;
			case TypeAttributes.NestedPrivate:
				base.Output.Write("Private ");
				break;
			case TypeAttributes.NestedFamily:
				base.Output.Write("Protected ");
				break;
			case TypeAttributes.VisibilityMask:
				base.Output.Write("Protected Friend ");
				break;
			}
			if (e.IsStruct)
			{
				base.Output.Write("Structure ");
				return;
			}
			if (e.IsEnum)
			{
				base.Output.Write("Enum ");
				return;
			}
			TypeAttributes typeAttributes2 = typeAttributes & TypeAttributes.ClassSemanticsMask;
			if (typeAttributes2 != TypeAttributes.NotPublic)
			{
				if (typeAttributes2 != TypeAttributes.ClassSemanticsMask)
				{
					return;
				}
				base.Output.Write("Interface ");
				return;
			}
			else
			{
				if (this.IsCurrentModule)
				{
					base.Output.Write("Module ");
					return;
				}
				if ((typeAttributes & TypeAttributes.Sealed) == TypeAttributes.Sealed)
				{
					base.Output.Write("NotInheritable ");
				}
				if ((typeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract)
				{
					base.Output.Write("MustInherit ");
				}
				base.Output.Write("Class ");
				return;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003051 File Offset: 0x00001251
		protected override void OutputTypeNamePair(CodeTypeReference typeRef, string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = "__exception";
			}
			this.OutputIdentifier(name);
			this.OutputArrayPostfix(typeRef);
			base.Output.Write(" As ");
			this.OutputType(typeRef);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003088 File Offset: 0x00001288
		private string GetArrayPostfix(CodeTypeReference typeRef)
		{
			string text = "";
			if (typeRef.ArrayElementType != null)
			{
				text = this.GetArrayPostfix(typeRef.ArrayElementType);
			}
			if (typeRef.ArrayRank > 0)
			{
				char[] array = new char[typeRef.ArrayRank + 1];
				array[0] = '(';
				array[typeRef.ArrayRank] = ')';
				for (int i = 1; i < typeRef.ArrayRank; i++)
				{
					array[i] = ',';
				}
				text = new string(array) + text;
			}
			return text;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000030FA File Offset: 0x000012FA
		private void OutputArrayPostfix(CodeTypeReference typeRef)
		{
			if (typeRef.ArrayRank > 0)
			{
				base.Output.Write(this.GetArrayPostfix(typeRef));
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003118 File Offset: 0x00001318
		protected override void GenerateIterationStatement(CodeIterationStatement e)
		{
			base.GenerateStatement(e.InitStatement);
			base.Output.Write("Do While ");
			base.GenerateExpression(e.TestExpression);
			base.Output.WriteLine("");
			int num = base.Indent;
			base.Indent = num + 1;
			this.GenerateVBStatements(e.Statements);
			base.GenerateStatement(e.IncrementStatement);
			num = base.Indent;
			base.Indent = num - 1;
			base.Output.WriteLine("Loop");
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000031A8 File Offset: 0x000013A8
		protected override void GeneratePrimitiveExpression(CodePrimitiveExpression e)
		{
			if (e.Value is char)
			{
				base.Output.Write("Global.Microsoft.VisualBasic.ChrW(" + ((IConvertible)e.Value).ToInt32(CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture) + ")");
				return;
			}
			if (e.Value is sbyte)
			{
				base.Output.Write("CSByte(");
				base.Output.Write(((sbyte)e.Value).ToString(CultureInfo.InvariantCulture));
				base.Output.Write(")");
				return;
			}
			if (e.Value is ushort)
			{
				base.Output.Write(((ushort)e.Value).ToString(CultureInfo.InvariantCulture));
				base.Output.Write("US");
				return;
			}
			if (e.Value is uint)
			{
				base.Output.Write(((uint)e.Value).ToString(CultureInfo.InvariantCulture));
				base.Output.Write("UI");
				return;
			}
			if (e.Value is ulong)
			{
				base.Output.Write(((ulong)e.Value).ToString(CultureInfo.InvariantCulture));
				base.Output.Write("UL");
				return;
			}
			base.GeneratePrimitiveExpression(e);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000331C File Offset: 0x0000151C
		protected override void GenerateThrowExceptionStatement(CodeThrowExceptionStatement e)
		{
			base.Output.Write("Throw");
			if (e.ToThrow != null)
			{
				base.Output.Write(" ");
				base.GenerateExpression(e.ToThrow);
			}
			base.Output.WriteLine("");
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003370 File Offset: 0x00001570
		protected override void GenerateArrayCreateExpression(CodeArrayCreateExpression e)
		{
			base.Output.Write("New ");
			CodeExpressionCollection initializers = e.Initializers;
			if (initializers.Count > 0)
			{
				string typeOutput = this.GetTypeOutput(e.CreateType);
				base.Output.Write(typeOutput);
				if (typeOutput.IndexOf('(') == -1)
				{
					base.Output.Write("()");
				}
				base.Output.Write(" {");
				int num = base.Indent;
				base.Indent = num + 1;
				this.OutputExpressionList(initializers);
				num = base.Indent;
				base.Indent = num - 1;
				base.Output.Write("}");
				return;
			}
			string typeOutput2 = this.GetTypeOutput(e.CreateType);
			int num2 = typeOutput2.IndexOf('(');
			if (num2 == -1)
			{
				base.Output.Write(typeOutput2);
				base.Output.Write('(');
			}
			else
			{
				base.Output.Write(typeOutput2.Substring(0, num2 + 1));
			}
			if (e.SizeExpression != null)
			{
				base.Output.Write("(");
				base.GenerateExpression(e.SizeExpression);
				base.Output.Write(") - 1");
			}
			else
			{
				base.Output.Write(e.Size - 1);
			}
			if (num2 == -1)
			{
				base.Output.Write(')');
			}
			else
			{
				base.Output.Write(typeOutput2.Substring(num2 + 1));
			}
			base.Output.Write(" {}");
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000034E8 File Offset: 0x000016E8
		protected override void GenerateBaseReferenceExpression(CodeBaseReferenceExpression e)
		{
			base.Output.Write("MyBase");
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000034FC File Offset: 0x000016FC
		protected override void GenerateCastExpression(CodeCastExpression e)
		{
			base.Output.Write("CType(");
			base.GenerateExpression(e.Expression);
			base.Output.Write(",");
			this.OutputType(e.TargetType);
			this.OutputArrayPostfix(e.TargetType);
			base.Output.Write(")");
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000355D File Offset: 0x0000175D
		protected override void GenerateDelegateCreateExpression(CodeDelegateCreateExpression e)
		{
			base.Output.Write("AddressOf ");
			base.GenerateExpression(e.TargetObject);
			base.Output.Write(".");
			this.OutputIdentifier(e.MethodName);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003597 File Offset: 0x00001797
		protected override void GenerateFieldReferenceExpression(CodeFieldReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				base.GenerateExpression(e.TargetObject);
				base.Output.Write(".");
			}
			this.OutputIdentifier(e.FieldName);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000035CC File Offset: 0x000017CC
		protected override void GenerateSingleFloatValue(float s)
		{
			if (float.IsNaN(s))
			{
				base.Output.Write("Single.NaN");
				return;
			}
			if (float.IsNegativeInfinity(s))
			{
				base.Output.Write("Single.NegativeInfinity");
				return;
			}
			if (float.IsPositiveInfinity(s))
			{
				base.Output.Write("Single.PositiveInfinity");
				return;
			}
			base.Output.Write(s.ToString(CultureInfo.InvariantCulture));
			base.Output.Write('!');
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003648 File Offset: 0x00001848
		protected override void GenerateDoubleValue(double d)
		{
			if (double.IsNaN(d))
			{
				base.Output.Write("Double.NaN");
				return;
			}
			if (double.IsNegativeInfinity(d))
			{
				base.Output.Write("Double.NegativeInfinity");
				return;
			}
			if (double.IsPositiveInfinity(d))
			{
				base.Output.Write("Double.PositiveInfinity");
				return;
			}
			base.Output.Write(d.ToString("R", CultureInfo.InvariantCulture));
			base.Output.Write("R");
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000036CC File Offset: 0x000018CC
		protected override void GenerateDecimalValue(decimal d)
		{
			base.Output.Write(d.ToString(CultureInfo.InvariantCulture));
			base.Output.Write('D');
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000036F2 File Offset: 0x000018F2
		protected override void GenerateArgumentReferenceExpression(CodeArgumentReferenceExpression e)
		{
			this.OutputIdentifier(e.ParameterName);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003700 File Offset: 0x00001900
		protected override void GenerateVariableReferenceExpression(CodeVariableReferenceExpression e)
		{
			this.OutputIdentifier(e.VariableName);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003710 File Offset: 0x00001910
		protected override void GenerateIndexerExpression(CodeIndexerExpression e)
		{
			base.GenerateExpression(e.TargetObject);
			if (e.TargetObject is CodeBaseReferenceExpression)
			{
				base.Output.Write(".Item");
			}
			base.Output.Write("(");
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
					base.Output.Write(", ");
				}
				base.GenerateExpression(codeExpression);
			}
			base.Output.Write(")");
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000037CC File Offset: 0x000019CC
		protected override void GenerateArrayIndexerExpression(CodeArrayIndexerExpression e)
		{
			base.GenerateExpression(e.TargetObject);
			base.Output.Write("(");
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
					base.Output.Write(", ");
				}
				base.GenerateExpression(codeExpression);
			}
			base.Output.Write(")");
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000386C File Offset: 0x00001A6C
		protected override void GenerateSnippetExpression(CodeSnippetExpression e)
		{
			base.Output.Write(e.Value);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003880 File Offset: 0x00001A80
		protected override void GenerateMethodInvokeExpression(CodeMethodInvokeExpression e)
		{
			this.GenerateMethodReferenceExpression(e.Method);
			CodeExpressionCollection parameters = e.Parameters;
			if (parameters.Count > 0)
			{
				base.Output.Write("(");
				this.OutputExpressionList(e.Parameters);
				base.Output.Write(")");
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000038D8 File Offset: 0x00001AD8
		protected override void GenerateMethodReferenceExpression(CodeMethodReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				base.GenerateExpression(e.TargetObject);
				base.Output.Write(".");
				base.Output.Write(e.MethodName);
			}
			else
			{
				this.OutputIdentifier(e.MethodName);
			}
			if (e.TypeArguments.Count > 0)
			{
				base.Output.Write(this.GetTypeArgumentsOutput(e.TypeArguments));
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003950 File Offset: 0x00001B50
		protected override void GenerateEventReferenceExpression(CodeEventReferenceExpression e)
		{
			if (e.TargetObject == null)
			{
				this.OutputIdentifier(e.EventName + "Event");
				return;
			}
			bool flag = e.TargetObject is CodeThisReferenceExpression;
			base.GenerateExpression(e.TargetObject);
			base.Output.Write(".");
			if (flag)
			{
				base.Output.Write(e.EventName + "Event");
				return;
			}
			base.Output.Write(e.EventName);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000039D7 File Offset: 0x00001BD7
		private void GenerateFormalEventReferenceExpression(CodeEventReferenceExpression e)
		{
			if (e.TargetObject != null && !(e.TargetObject is CodeThisReferenceExpression))
			{
				base.GenerateExpression(e.TargetObject);
				base.Output.Write(".");
			}
			this.OutputIdentifier(e.EventName);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003A18 File Offset: 0x00001C18
		protected override void GenerateDelegateInvokeExpression(CodeDelegateInvokeExpression e)
		{
			if (e.TargetObject != null)
			{
				if (e.TargetObject is CodeEventReferenceExpression)
				{
					base.Output.Write("RaiseEvent ");
					this.GenerateFormalEventReferenceExpression((CodeEventReferenceExpression)e.TargetObject);
				}
				else
				{
					base.GenerateExpression(e.TargetObject);
				}
			}
			CodeExpressionCollection parameters = e.Parameters;
			if (parameters.Count > 0)
			{
				base.Output.Write("(");
				this.OutputExpressionList(e.Parameters);
				base.Output.Write(")");
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003AA8 File Offset: 0x00001CA8
		protected override void GenerateObjectCreateExpression(CodeObjectCreateExpression e)
		{
			base.Output.Write("New ");
			this.OutputType(e.CreateType);
			base.Output.Write("(");
			this.OutputExpressionList(e.Parameters);
			base.Output.Write(")");
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003AFD File Offset: 0x00001CFD
		protected override void GenerateParameterDeclarationExpression(CodeParameterDeclarationExpression e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, true);
			}
			this.OutputDirection(e.Direction);
			this.OutputTypeNamePair(e.Type, e.Name);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003B38 File Offset: 0x00001D38
		protected override void GeneratePropertySetValueReferenceExpression(CodePropertySetValueReferenceExpression e)
		{
			base.Output.Write("value");
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003B4A File Offset: 0x00001D4A
		protected override void GenerateThisReferenceExpression(CodeThisReferenceExpression e)
		{
			base.Output.Write("Me");
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003B5C File Offset: 0x00001D5C
		protected override void GenerateExpressionStatement(CodeExpressionStatement e)
		{
			base.GenerateExpression(e.Expression);
			base.Output.WriteLine("");
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003B7A File Offset: 0x00001D7A
		private bool IsDocComment(CodeCommentStatement comment)
		{
			return comment != null && comment.Comment != null && comment.Comment.DocComment;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003B94 File Offset: 0x00001D94
		protected override void GenerateCommentStatements(CodeCommentStatementCollection e)
		{
			foreach (object obj in e)
			{
				CodeCommentStatement codeCommentStatement = (CodeCommentStatement)obj;
				if (!this.IsDocComment(codeCommentStatement))
				{
					this.GenerateCommentStatement(codeCommentStatement);
				}
			}
			foreach (object obj2 in e)
			{
				CodeCommentStatement codeCommentStatement2 = (CodeCommentStatement)obj2;
				if (this.IsDocComment(codeCommentStatement2))
				{
					this.GenerateCommentStatement(codeCommentStatement2);
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003C40 File Offset: 0x00001E40
		protected override void GenerateComment(CodeComment e)
		{
			string text = (e.DocComment ? "'''" : "'");
			base.Output.Write(text);
			string text2 = e.Text;
			for (int i = 0; i < text2.Length; i++)
			{
				base.Output.Write(text2[i]);
				if (text2[i] == '\r')
				{
					if (i < text2.Length - 1 && text2[i + 1] == '\n')
					{
						base.Output.Write('\n');
						i++;
					}
					((IndentedTextWriter)base.Output).InternalOutputTabs();
					base.Output.Write(text);
				}
				else if (text2[i] == '\n')
				{
					((IndentedTextWriter)base.Output).InternalOutputTabs();
					base.Output.Write(text);
				}
				else if (text2[i] == '\u2028' || text2[i] == '\u2029' || text2[i] == '\u0085')
				{
					base.Output.Write(text);
				}
			}
			base.Output.WriteLine();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003D5C File Offset: 0x00001F5C
		protected override void GenerateMethodReturnStatement(CodeMethodReturnStatement e)
		{
			if (e.Expression != null)
			{
				base.Output.Write("Return ");
				base.GenerateExpression(e.Expression);
				base.Output.WriteLine("");
				return;
			}
			base.Output.WriteLine("Return");
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003DB0 File Offset: 0x00001FB0
		protected override void GenerateConditionStatement(CodeConditionStatement e)
		{
			base.Output.Write("If ");
			base.GenerateExpression(e.Condition);
			base.Output.WriteLine(" Then");
			int num = base.Indent;
			base.Indent = num + 1;
			this.GenerateVBStatements(e.TrueStatements);
			num = base.Indent;
			base.Indent = num - 1;
			CodeStatementCollection falseStatements = e.FalseStatements;
			if (falseStatements.Count > 0)
			{
				base.Output.Write("Else");
				base.Output.WriteLine("");
				num = base.Indent;
				base.Indent = num + 1;
				this.GenerateVBStatements(e.FalseStatements);
				num = base.Indent;
				base.Indent = num - 1;
			}
			base.Output.WriteLine("End If");
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003E84 File Offset: 0x00002084
		protected override void GenerateTryCatchFinallyStatement(CodeTryCatchFinallyStatement e)
		{
			base.Output.WriteLine("Try ");
			int num = base.Indent;
			base.Indent = num + 1;
			this.GenerateVBStatements(e.TryStatements);
			num = base.Indent;
			base.Indent = num - 1;
			CodeCatchClauseCollection catchClauses = e.CatchClauses;
			if (catchClauses.Count > 0)
			{
				foreach (object obj in catchClauses)
				{
					CodeCatchClause codeCatchClause = (CodeCatchClause)obj;
					base.Output.Write("Catch ");
					this.OutputTypeNamePair(codeCatchClause.CatchExceptionType, codeCatchClause.LocalName);
					base.Output.WriteLine("");
					num = base.Indent;
					base.Indent = num + 1;
					this.GenerateVBStatements(codeCatchClause.Statements);
					num = base.Indent;
					base.Indent = num - 1;
				}
			}
			CodeStatementCollection finallyStatements = e.FinallyStatements;
			if (finallyStatements.Count > 0)
			{
				base.Output.WriteLine("Finally");
				num = base.Indent;
				base.Indent = num + 1;
				this.GenerateVBStatements(finallyStatements);
				num = base.Indent;
				base.Indent = num - 1;
			}
			base.Output.WriteLine("End Try");
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003FB3 File Offset: 0x000021B3
		protected override void GenerateAssignStatement(CodeAssignStatement e)
		{
			base.GenerateExpression(e.Left);
			base.Output.Write(" = ");
			base.GenerateExpression(e.Right);
			base.Output.WriteLine("");
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003FF0 File Offset: 0x000021F0
		protected override void GenerateAttachEventStatement(CodeAttachEventStatement e)
		{
			base.Output.Write("AddHandler ");
			this.GenerateFormalEventReferenceExpression(e.Event);
			base.Output.Write(", ");
			base.GenerateExpression(e.Listener);
			base.Output.WriteLine("");
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004048 File Offset: 0x00002248
		protected override void GenerateRemoveEventStatement(CodeRemoveEventStatement e)
		{
			base.Output.Write("RemoveHandler ");
			this.GenerateFormalEventReferenceExpression(e.Event);
			base.Output.Write(", ");
			base.GenerateExpression(e.Listener);
			base.Output.WriteLine("");
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000409D File Offset: 0x0000229D
		protected override void GenerateSnippetStatement(CodeSnippetStatement e)
		{
			base.Output.WriteLine(e.Value);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000040B0 File Offset: 0x000022B0
		protected override void GenerateGotoStatement(CodeGotoStatement e)
		{
			base.Output.Write("goto ");
			base.Output.WriteLine(e.Label);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000040D4 File Offset: 0x000022D4
		protected override void GenerateLabeledStatement(CodeLabeledStatement e)
		{
			int num = base.Indent;
			base.Indent = num - 1;
			base.Output.Write(e.Label);
			base.Output.WriteLine(":");
			num = base.Indent;
			base.Indent = num + 1;
			if (e.Statement != null)
			{
				base.GenerateStatement(e.Statement);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004138 File Offset: 0x00002338
		protected override void GenerateVariableDeclarationStatement(CodeVariableDeclarationStatement e)
		{
			bool flag = true;
			base.Output.Write("Dim ");
			CodeTypeReference type = e.Type;
			if (type.ArrayRank == 1 && e.InitExpression != null)
			{
				CodeArrayCreateExpression codeArrayCreateExpression = e.InitExpression as CodeArrayCreateExpression;
				if (codeArrayCreateExpression != null && codeArrayCreateExpression.Initializers.Count == 0)
				{
					flag = false;
					this.OutputIdentifier(e.Name);
					base.Output.Write("(");
					if (codeArrayCreateExpression.SizeExpression != null)
					{
						base.Output.Write("(");
						base.GenerateExpression(codeArrayCreateExpression.SizeExpression);
						base.Output.Write(") - 1");
					}
					else
					{
						base.Output.Write(codeArrayCreateExpression.Size - 1);
					}
					base.Output.Write(")");
					if (type.ArrayElementType != null)
					{
						this.OutputArrayPostfix(type.ArrayElementType);
					}
					base.Output.Write(" As ");
					this.OutputType(type);
				}
				else
				{
					this.OutputTypeNamePair(e.Type, e.Name);
				}
			}
			else
			{
				this.OutputTypeNamePair(e.Type, e.Name);
			}
			if (flag && e.InitExpression != null)
			{
				base.Output.Write(" = ");
				base.GenerateExpression(e.InitExpression);
			}
			base.Output.WriteLine("");
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004298 File Offset: 0x00002498
		protected override void GenerateLinePragmaStart(CodeLinePragma e)
		{
			base.Output.WriteLine("");
			base.Output.Write("#ExternalSource(\"");
			base.Output.Write(e.FileName);
			base.Output.Write("\",");
			base.Output.Write(e.LineNumber);
			base.Output.WriteLine(")");
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004307 File Offset: 0x00002507
		protected override void GenerateLinePragmaEnd(CodeLinePragma e)
		{
			base.Output.WriteLine("");
			base.Output.WriteLine("#End ExternalSource");
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000432C File Offset: 0x0000252C
		protected override void GenerateEvent(CodeMemberEvent e, CodeTypeDeclaration c)
		{
			if (base.IsCurrentDelegate || base.IsCurrentEnum)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			string name = e.Name;
			if (e.PrivateImplementationType != null)
			{
				string text = this.GetBaseTypeOutput(e.PrivateImplementationType);
				text = text.Replace('.', '_');
				e.Name = text + "_" + e.Name;
			}
			this.OutputMemberAccessModifier(e.Attributes);
			base.Output.Write("Event ");
			this.OutputTypeNamePair(e.Type, e.Name);
			if (e.ImplementationTypes.Count > 0)
			{
				base.Output.Write(" Implements ");
				bool flag = true;
				using (IEnumerator enumerator = e.ImplementationTypes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
						if (flag)
						{
							flag = false;
						}
						else
						{
							base.Output.Write(" , ");
						}
						this.OutputType(codeTypeReference);
						base.Output.Write(".");
						this.OutputIdentifier(name);
					}
					goto IL_165;
				}
			}
			if (e.PrivateImplementationType != null)
			{
				base.Output.Write(" Implements ");
				this.OutputType(e.PrivateImplementationType);
				base.Output.Write(".");
				this.OutputIdentifier(name);
			}
			IL_165:
			base.Output.WriteLine("");
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000044C0 File Offset: 0x000026C0
		protected override void GenerateField(CodeMemberField e)
		{
			if (base.IsCurrentDelegate || base.IsCurrentInterface)
			{
				return;
			}
			if (base.IsCurrentEnum)
			{
				if (e.CustomAttributes.Count > 0)
				{
					this.OutputAttributes(e.CustomAttributes, false);
				}
				this.OutputIdentifier(e.Name);
				if (e.InitExpression != null)
				{
					base.Output.Write(" = ");
					base.GenerateExpression(e.InitExpression);
				}
				base.Output.WriteLine("");
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			this.OutputMemberAccessModifier(e.Attributes);
			this.OutputVTableModifier(e.Attributes);
			this.OutputFieldScopeModifier(e.Attributes);
			if (this.GetUserData(e, "WithEvents", false))
			{
				base.Output.Write("WithEvents ");
			}
			this.OutputTypeNamePair(e.Type, e.Name);
			if (e.InitExpression != null)
			{
				base.Output.Write(" = ");
				base.GenerateExpression(e.InitExpression);
			}
			base.Output.WriteLine("");
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000045E8 File Offset: 0x000027E8
		private bool MethodIsOverloaded(CodeMemberMethod e, CodeTypeDeclaration c)
		{
			if ((e.Attributes & MemberAttributes.Overloaded) != (MemberAttributes)0)
			{
				return true;
			}
			IEnumerator enumerator = c.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeMemberMethod)
				{
					CodeMemberMethod codeMemberMethod = (CodeMemberMethod)enumerator.Current;
					if (!(enumerator.Current is CodeTypeConstructor) && !(enumerator.Current is CodeConstructor) && codeMemberMethod != e && codeMemberMethod.Name.Equals(e.Name, StringComparison.OrdinalIgnoreCase) && codeMemberMethod.PrivateImplementationType == null)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004671 File Offset: 0x00002871
		protected override void GenerateSnippetMember(CodeSnippetTypeMember e)
		{
			base.Output.Write(e.Text);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004684 File Offset: 0x00002884
		protected override void GenerateMethod(CodeMemberMethod e, CodeTypeDeclaration c)
		{
			if (!base.IsCurrentClass && !base.IsCurrentStruct && !base.IsCurrentInterface)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			string name = e.Name;
			if (e.PrivateImplementationType != null)
			{
				string text = this.GetBaseTypeOutput(e.PrivateImplementationType);
				text = text.Replace('.', '_');
				e.Name = text + "_" + e.Name;
			}
			if (!base.IsCurrentInterface)
			{
				if (e.PrivateImplementationType == null)
				{
					this.OutputMemberAccessModifier(e.Attributes);
					if (this.MethodIsOverloaded(e, c))
					{
						base.Output.Write("Overloads ");
					}
				}
				this.OutputVTableModifier(e.Attributes);
				this.OutputMemberScopeModifier(e.Attributes);
			}
			else
			{
				this.OutputVTableModifier(e.Attributes);
			}
			bool flag = false;
			if (e.ReturnType.BaseType.Length == 0 || string.Compare(e.ReturnType.BaseType, typeof(void).FullName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				flag = true;
			}
			if (flag)
			{
				base.Output.Write("Sub ");
			}
			else
			{
				base.Output.Write("Function ");
			}
			this.OutputIdentifier(e.Name);
			this.OutputTypeParameters(e.TypeParameters);
			base.Output.Write("(");
			this.OutputParameters(e.Parameters);
			base.Output.Write(")");
			if (!flag)
			{
				base.Output.Write(" As ");
				if (e.ReturnTypeCustomAttributes.Count > 0)
				{
					this.OutputAttributes(e.ReturnTypeCustomAttributes, true);
				}
				this.OutputType(e.ReturnType);
				this.OutputArrayPostfix(e.ReturnType);
			}
			if (e.ImplementationTypes.Count > 0)
			{
				base.Output.Write(" Implements ");
				bool flag2 = true;
				using (IEnumerator enumerator = e.ImplementationTypes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
						if (flag2)
						{
							flag2 = false;
						}
						else
						{
							base.Output.Write(" , ");
						}
						this.OutputType(codeTypeReference);
						base.Output.Write(".");
						this.OutputIdentifier(name);
					}
					goto IL_286;
				}
			}
			if (e.PrivateImplementationType != null)
			{
				base.Output.Write(" Implements ");
				this.OutputType(e.PrivateImplementationType);
				base.Output.Write(".");
				this.OutputIdentifier(name);
			}
			IL_286:
			base.Output.WriteLine("");
			if (!base.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				int num = base.Indent;
				base.Indent = num + 1;
				this.GenerateVBStatements(e.Statements);
				num = base.Indent;
				base.Indent = num - 1;
				if (flag)
				{
					base.Output.WriteLine("End Sub");
				}
				else
				{
					base.Output.WriteLine("End Function");
				}
			}
			e.Name = name;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000049A8 File Offset: 0x00002BA8
		protected override void GenerateEntryPointMethod(CodeEntryPointMethod e, CodeTypeDeclaration c)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			base.Output.WriteLine("Public Shared Sub Main()");
			int num = base.Indent;
			base.Indent = num + 1;
			this.GenerateVBStatements(e.Statements);
			num = base.Indent;
			base.Indent = num - 1;
			base.Output.WriteLine("End Sub");
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004A1C File Offset: 0x00002C1C
		private bool PropertyIsOverloaded(CodeMemberProperty e, CodeTypeDeclaration c)
		{
			if ((e.Attributes & MemberAttributes.Overloaded) != (MemberAttributes)0)
			{
				return true;
			}
			IEnumerator enumerator = c.Members.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is CodeMemberProperty)
				{
					CodeMemberProperty codeMemberProperty = (CodeMemberProperty)enumerator.Current;
					if (codeMemberProperty != e && codeMemberProperty.Name.Equals(e.Name, StringComparison.OrdinalIgnoreCase) && codeMemberProperty.PrivateImplementationType == null)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004A8C File Offset: 0x00002C8C
		protected override void GenerateProperty(CodeMemberProperty e, CodeTypeDeclaration c)
		{
			if (!base.IsCurrentClass && !base.IsCurrentStruct && !base.IsCurrentInterface)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			string name = e.Name;
			if (e.PrivateImplementationType != null)
			{
				string text = this.GetBaseTypeOutput(e.PrivateImplementationType);
				text = text.Replace('.', '_');
				e.Name = text + "_" + e.Name;
			}
			if (!base.IsCurrentInterface)
			{
				if (e.PrivateImplementationType == null)
				{
					this.OutputMemberAccessModifier(e.Attributes);
					if (this.PropertyIsOverloaded(e, c))
					{
						base.Output.Write("Overloads ");
					}
				}
				this.OutputVTableModifier(e.Attributes);
				this.OutputMemberScopeModifier(e.Attributes);
			}
			else
			{
				this.OutputVTableModifier(e.Attributes);
			}
			if (e.Parameters.Count > 0 && string.Compare(e.Name, "Item", StringComparison.OrdinalIgnoreCase) == 0)
			{
				base.Output.Write("Default ");
			}
			if (e.HasGet)
			{
				if (!e.HasSet)
				{
					base.Output.Write("ReadOnly ");
				}
			}
			else if (e.HasSet)
			{
				base.Output.Write("WriteOnly ");
			}
			base.Output.Write("Property ");
			this.OutputIdentifier(e.Name);
			base.Output.Write("(");
			if (e.Parameters.Count > 0)
			{
				this.OutputParameters(e.Parameters);
			}
			base.Output.Write(")");
			base.Output.Write(" As ");
			this.OutputType(e.Type);
			this.OutputArrayPostfix(e.Type);
			if (e.ImplementationTypes.Count > 0)
			{
				base.Output.Write(" Implements ");
				bool flag = true;
				using (IEnumerator enumerator = e.ImplementationTypes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
						if (flag)
						{
							flag = false;
						}
						else
						{
							base.Output.Write(" , ");
						}
						this.OutputType(codeTypeReference);
						base.Output.Write(".");
						this.OutputIdentifier(name);
					}
					goto IL_284;
				}
			}
			if (e.PrivateImplementationType != null)
			{
				base.Output.Write(" Implements ");
				this.OutputType(e.PrivateImplementationType);
				base.Output.Write(".");
				this.OutputIdentifier(name);
			}
			IL_284:
			base.Output.WriteLine("");
			if (!c.IsInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				int num = base.Indent;
				base.Indent = num + 1;
				if (e.HasGet)
				{
					base.Output.WriteLine("Get");
					if (!base.IsCurrentInterface)
					{
						num = base.Indent;
						base.Indent = num + 1;
						this.GenerateVBStatements(e.GetStatements);
						e.Name = name;
						num = base.Indent;
						base.Indent = num - 1;
						base.Output.WriteLine("End Get");
					}
				}
				if (e.HasSet)
				{
					base.Output.WriteLine("Set");
					if (!base.IsCurrentInterface)
					{
						num = base.Indent;
						base.Indent = num + 1;
						this.GenerateVBStatements(e.SetStatements);
						num = base.Indent;
						base.Indent = num - 1;
						base.Output.WriteLine("End Set");
					}
				}
				num = base.Indent;
				base.Indent = num - 1;
				base.Output.WriteLine("End Property");
			}
			e.Name = name;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004E5C File Offset: 0x0000305C
		protected override void GeneratePropertyReferenceExpression(CodePropertyReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				base.GenerateExpression(e.TargetObject);
				base.Output.Write(".");
				base.Output.Write(e.PropertyName);
				return;
			}
			this.OutputIdentifier(e.PropertyName);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004EAC File Offset: 0x000030AC
		protected override void GenerateConstructor(CodeConstructor e, CodeTypeDeclaration c)
		{
			if (!base.IsCurrentClass && !base.IsCurrentStruct)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			this.OutputMemberAccessModifier(e.Attributes);
			base.Output.Write("Sub New(");
			this.OutputParameters(e.Parameters);
			base.Output.WriteLine(")");
			int num = base.Indent;
			base.Indent = num + 1;
			CodeExpressionCollection baseConstructorArgs = e.BaseConstructorArgs;
			CodeExpressionCollection chainedConstructorArgs = e.ChainedConstructorArgs;
			if (chainedConstructorArgs.Count > 0)
			{
				base.Output.Write("Me.New(");
				this.OutputExpressionList(chainedConstructorArgs);
				base.Output.Write(")");
				base.Output.WriteLine("");
			}
			else if (baseConstructorArgs.Count > 0)
			{
				base.Output.Write("MyBase.New(");
				this.OutputExpressionList(baseConstructorArgs);
				base.Output.Write(")");
				base.Output.WriteLine("");
			}
			else if (base.IsCurrentClass)
			{
				base.Output.WriteLine("MyBase.New");
			}
			this.GenerateVBStatements(e.Statements);
			num = base.Indent;
			base.Indent = num - 1;
			base.Output.WriteLine("End Sub");
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005004 File Offset: 0x00003204
		protected override void GenerateTypeConstructor(CodeTypeConstructor e)
		{
			if (!base.IsCurrentClass && !base.IsCurrentStruct)
			{
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			base.Output.WriteLine("Shared Sub New()");
			int num = base.Indent;
			base.Indent = num + 1;
			this.GenerateVBStatements(e.Statements);
			num = base.Indent;
			base.Indent = num - 1;
			base.Output.WriteLine("End Sub");
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00005089 File Offset: 0x00003289
		protected override void GenerateTypeOfExpression(CodeTypeOfExpression e)
		{
			base.Output.Write("GetType(");
			base.Output.Write(this.GetTypeOutput(e.Type));
			base.Output.Write(")");
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000050C4 File Offset: 0x000032C4
		protected override void GenerateTypeStart(CodeTypeDeclaration e)
		{
			if (base.IsCurrentDelegate)
			{
				if (e.CustomAttributes.Count > 0)
				{
					this.OutputAttributes(e.CustomAttributes, false);
				}
				TypeAttributes typeAttributes = e.TypeAttributes & TypeAttributes.VisibilityMask;
				if (typeAttributes != TypeAttributes.NotPublic && typeAttributes == TypeAttributes.Public)
				{
					base.Output.Write("Public ");
				}
				CodeTypeDelegate codeTypeDelegate = (CodeTypeDelegate)e;
				if (codeTypeDelegate.ReturnType.BaseType.Length > 0 && string.Compare(codeTypeDelegate.ReturnType.BaseType, "System.Void", StringComparison.OrdinalIgnoreCase) != 0)
				{
					base.Output.Write("Delegate Function ");
				}
				else
				{
					base.Output.Write("Delegate Sub ");
				}
				this.OutputIdentifier(e.Name);
				base.Output.Write("(");
				this.OutputParameters(codeTypeDelegate.Parameters);
				base.Output.Write(")");
				if (codeTypeDelegate.ReturnType.BaseType.Length > 0 && string.Compare(codeTypeDelegate.ReturnType.BaseType, "System.Void", StringComparison.OrdinalIgnoreCase) != 0)
				{
					base.Output.Write(" As ");
					this.OutputType(codeTypeDelegate.ReturnType);
					this.OutputArrayPostfix(codeTypeDelegate.ReturnType);
				}
				base.Output.WriteLine("");
				return;
			}
			int num;
			if (e.IsEnum)
			{
				if (e.CustomAttributes.Count > 0)
				{
					this.OutputAttributes(e.CustomAttributes, false);
				}
				this.OutputTypeAttributes(e);
				this.OutputIdentifier(e.Name);
				if (e.BaseTypes.Count > 0)
				{
					base.Output.Write(" As ");
					this.OutputType(e.BaseTypes[0]);
				}
				base.Output.WriteLine("");
				num = base.Indent;
				base.Indent = num + 1;
				return;
			}
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.CustomAttributes, false);
			}
			this.OutputTypeAttributes(e);
			this.OutputIdentifier(e.Name);
			this.OutputTypeParameters(e.TypeParameters);
			bool flag = false;
			bool flag2 = false;
			if (e.IsStruct)
			{
				flag = true;
			}
			if (e.IsInterface)
			{
				flag2 = true;
			}
			num = base.Indent;
			base.Indent = num + 1;
			foreach (object obj in e.BaseTypes)
			{
				CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
				if (!flag && (e.IsInterface || !codeTypeReference.IsInterface))
				{
					base.Output.WriteLine("");
					base.Output.Write("Inherits ");
					flag = true;
				}
				else if (!flag2)
				{
					base.Output.WriteLine("");
					base.Output.Write("Implements ");
					flag2 = true;
				}
				else
				{
					base.Output.Write(", ");
				}
				this.OutputType(codeTypeReference);
			}
			base.Output.WriteLine("");
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000053D0 File Offset: 0x000035D0
		private void OutputTypeParameters(CodeTypeParameterCollection typeParameters)
		{
			if (typeParameters.Count == 0)
			{
				return;
			}
			base.Output.Write("(Of ");
			bool flag = true;
			for (int i = 0; i < typeParameters.Count; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					base.Output.Write(", ");
				}
				base.Output.Write(typeParameters[i].Name);
				this.OutputTypeParameterConstraints(typeParameters[i]);
			}
			base.Output.Write(')');
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005454 File Offset: 0x00003654
		private void OutputTypeParameterConstraints(CodeTypeParameter typeParameter)
		{
			CodeTypeReferenceCollection constraints = typeParameter.Constraints;
			int num = constraints.Count;
			if (typeParameter.HasConstructorConstraint)
			{
				num++;
			}
			if (num == 0)
			{
				return;
			}
			base.Output.Write(" As ");
			if (num > 1)
			{
				base.Output.Write(" {");
			}
			bool flag = true;
			foreach (object obj in constraints)
			{
				CodeTypeReference codeTypeReference = (CodeTypeReference)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					base.Output.Write(", ");
				}
				base.Output.Write(this.GetTypeOutput(codeTypeReference));
			}
			if (typeParameter.HasConstructorConstraint)
			{
				if (!flag)
				{
					base.Output.Write(", ");
				}
				base.Output.Write("New");
			}
			if (num > 1)
			{
				base.Output.Write('}');
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005550 File Offset: 0x00003750
		protected override void GenerateTypeEnd(CodeTypeDeclaration e)
		{
			if (!base.IsCurrentDelegate)
			{
				int indent = base.Indent;
				base.Indent = indent - 1;
				string text;
				if (e.IsEnum)
				{
					text = "End Enum";
				}
				else if (e.IsInterface)
				{
					text = "End Interface";
				}
				else if (e.IsStruct)
				{
					text = "End Structure";
				}
				else if (this.IsCurrentModule)
				{
					text = "End Module";
				}
				else
				{
					text = "End Class";
				}
				base.Output.WriteLine(text);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000055C8 File Offset: 0x000037C8
		protected override void GenerateNamespace(CodeNamespace e)
		{
			if (this.GetUserData(e, "GenerateImports", true))
			{
				base.GenerateNamespaceImports(e);
			}
			base.Output.WriteLine();
			this.GenerateCommentStatements(e.Comments);
			this.GenerateNamespaceStart(e);
			base.GenerateTypes(e);
			this.GenerateNamespaceEnd(e);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005618 File Offset: 0x00003818
		protected bool AllowLateBound(CodeCompileUnit e)
		{
			object obj = e.UserData["AllowLateBound"];
			return obj == null || !(obj is bool) || (bool)obj;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000564C File Offset: 0x0000384C
		protected bool RequireVariableDeclaration(CodeCompileUnit e)
		{
			object obj = e.UserData["RequireVariableDeclaration"];
			return obj == null || !(obj is bool) || (bool)obj;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00005680 File Offset: 0x00003880
		private bool GetUserData(CodeObject e, string property, bool defaultValue)
		{
			object obj = e.UserData[property];
			if (obj != null && obj is bool)
			{
				return (bool)obj;
			}
			return defaultValue;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000056B0 File Offset: 0x000038B0
		protected override void GenerateCompileUnitStart(CodeCompileUnit e)
		{
			base.GenerateCompileUnitStart(e);
			base.Output.WriteLine("'------------------------------------------------------------------------------");
			base.Output.Write("' <");
			base.Output.WriteLine(SR.GetString("AutoGen_Comment_Line1"));
			base.Output.Write("'     ");
			base.Output.WriteLine(SR.GetString("AutoGen_Comment_Line2"));
			base.Output.Write("'     ");
			base.Output.Write(SR.GetString("AutoGen_Comment_Line3"));
			base.Output.WriteLine(Environment.Version.ToString());
			base.Output.WriteLine("'");
			base.Output.Write("'     ");
			base.Output.WriteLine(SR.GetString("AutoGen_Comment_Line4"));
			base.Output.Write("'     ");
			base.Output.WriteLine(SR.GetString("AutoGen_Comment_Line5"));
			base.Output.Write("' </");
			base.Output.WriteLine(SR.GetString("AutoGen_Comment_Line1"));
			base.Output.WriteLine("'------------------------------------------------------------------------------");
			base.Output.WriteLine("");
			if (this.AllowLateBound(e))
			{
				base.Output.WriteLine("Option Strict Off");
			}
			else
			{
				base.Output.WriteLine("Option Strict On");
			}
			if (!this.RequireVariableDeclaration(e))
			{
				base.Output.WriteLine("Option Explicit Off");
			}
			else
			{
				base.Output.WriteLine("Option Explicit On");
			}
			base.Output.WriteLine();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00005858 File Offset: 0x00003A58
		protected override void GenerateCompileUnit(CodeCompileUnit e)
		{
			this.GenerateCompileUnitStart(e);
			SortedList sortedList = new SortedList(StringComparer.OrdinalIgnoreCase);
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
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
			foreach (object obj3 in sortedList.Keys)
			{
				string text = (string)obj3;
				base.Output.Write("Imports ");
				this.OutputIdentifier(text);
				base.Output.WriteLine("");
			}
			if (e.AssemblyCustomAttributes.Count > 0)
			{
				this.OutputAttributes(e.AssemblyCustomAttributes, false, "Assembly: ", true);
			}
			base.GenerateNamespaces(e);
			this.GenerateCompileUnitEnd(e);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000059E0 File Offset: 0x00003BE0
		protected override void GenerateDirectives(CodeDirectiveCollection directives)
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

		// Token: 0x06000069 RID: 105 RVA: 0x00005A30 File Offset: 0x00003C30
		private void GenerateChecksumPragma(CodeChecksumPragma checksumPragma)
		{
			base.Output.Write("#ExternalChecksum(\"");
			base.Output.Write(checksumPragma.FileName);
			base.Output.Write("\",\"");
			base.Output.Write(checksumPragma.ChecksumAlgorithmId.ToString("B", CultureInfo.InvariantCulture));
			base.Output.Write("\",\"");
			if (checksumPragma.ChecksumData != null)
			{
				foreach (byte b in checksumPragma.ChecksumData)
				{
					base.Output.Write(b.ToString("X2", CultureInfo.InvariantCulture));
				}
			}
			base.Output.WriteLine("\")");
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00005AF0 File Offset: 0x00003CF0
		private void GenerateCodeRegionDirective(CodeRegionDirective regionDirective)
		{
			if (this.IsGeneratingStatements())
			{
				return;
			}
			if (regionDirective.RegionMode == CodeRegionMode.Start)
			{
				base.Output.Write("#Region \"");
				base.Output.Write(regionDirective.RegionText);
				base.Output.WriteLine("\"");
				return;
			}
			if (regionDirective.RegionMode == CodeRegionMode.End)
			{
				base.Output.WriteLine("#End Region");
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00005B5C File Offset: 0x00003D5C
		protected override void GenerateNamespaceStart(CodeNamespace e)
		{
			if (e.Name != null && e.Name.Length > 0)
			{
				base.Output.Write("Namespace ");
				string[] array = e.Name.Split(new char[] { '.' });
				this.OutputIdentifier(array[0]);
				for (int i = 1; i < array.Length; i++)
				{
					base.Output.Write(".");
					this.OutputIdentifier(array[i]);
				}
				base.Output.WriteLine();
				int indent = base.Indent;
				base.Indent = indent + 1;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005BF4 File Offset: 0x00003DF4
		protected override void GenerateNamespaceEnd(CodeNamespace e)
		{
			if (e.Name != null && e.Name.Length > 0)
			{
				int indent = base.Indent;
				base.Indent = indent - 1;
				base.Output.WriteLine("End Namespace");
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005C37 File Offset: 0x00003E37
		protected override void GenerateNamespaceImport(CodeNamespaceImport e)
		{
			base.Output.Write("Imports ");
			this.OutputIdentifier(e.Namespace);
			base.Output.WriteLine("");
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005C65 File Offset: 0x00003E65
		protected override void GenerateAttributeDeclarationsStart(CodeAttributeDeclarationCollection attributes)
		{
			base.Output.Write("<");
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005C77 File Offset: 0x00003E77
		protected override void GenerateAttributeDeclarationsEnd(CodeAttributeDeclarationCollection attributes)
		{
			base.Output.Write(">");
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005C89 File Offset: 0x00003E89
		public static bool IsKeyword(string value)
		{
			return FixedStringLookup.Contains(VBCodeGenerator.keywords, value, true);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005C97 File Offset: 0x00003E97
		protected override bool Supports(GeneratorSupport support)
		{
			return (support & (GeneratorSupport.ArraysOfArrays | GeneratorSupport.EntryPointMethod | GeneratorSupport.GotoStatements | GeneratorSupport.MultidimensionalArrays | GeneratorSupport.StaticConstructors | GeneratorSupport.TryCatchStatements | GeneratorSupport.ReturnTypeAttributes | GeneratorSupport.DeclareValueTypes | GeneratorSupport.DeclareEnums | GeneratorSupport.DeclareDelegates | GeneratorSupport.DeclareInterfaces | GeneratorSupport.DeclareEvents | GeneratorSupport.AssemblyAttributes | GeneratorSupport.ParameterAttributes | GeneratorSupport.ReferenceParameters | GeneratorSupport.ChainedConstructorArguments | GeneratorSupport.NestedTypes | GeneratorSupport.MultipleInterfaceMembers | GeneratorSupport.PublicStaticMembers | GeneratorSupport.ComplexExpressions | GeneratorSupport.Win32Resources | GeneratorSupport.Resources | GeneratorSupport.PartialTypes | GeneratorSupport.GenericTypeReference | GeneratorSupport.GenericTypeDeclaration | GeneratorSupport.DeclareIndexerProperties)) == support;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005CA4 File Offset: 0x00003EA4
		protected override bool IsValidIdentifier(string value)
		{
			if (value == null || value.Length == 0)
			{
				return false;
			}
			if (value.Length > 1023)
			{
				return false;
			}
			if (value[0] != '[' || value[value.Length - 1] != ']')
			{
				if (VBCodeGenerator.IsKeyword(value))
				{
					return false;
				}
			}
			else
			{
				value = value.Substring(1, value.Length - 2);
			}
			return (value.Length != 1 || value[0] != '_') && CodeGenerator.IsValidLanguageIndependentIdentifier(value);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005D21 File Offset: 0x00003F21
		protected override string CreateValidIdentifier(string name)
		{
			if (VBCodeGenerator.IsKeyword(name))
			{
				return "_" + name;
			}
			return name;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005D38 File Offset: 0x00003F38
		protected override string CreateEscapedIdentifier(string name)
		{
			if (VBCodeGenerator.IsKeyword(name))
			{
				return "[" + name + "]";
			}
			return name;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005D54 File Offset: 0x00003F54
		private string GetBaseTypeOutput(CodeTypeReference typeRef)
		{
			string baseType = typeRef.BaseType;
			if (baseType.Length == 0)
			{
				return "Void";
			}
			if (string.Compare(baseType, "System.Byte", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "Byte";
			}
			if (string.Compare(baseType, "System.SByte", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "SByte";
			}
			if (string.Compare(baseType, "System.Int16", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "Short";
			}
			if (string.Compare(baseType, "System.Int32", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "Integer";
			}
			if (string.Compare(baseType, "System.Int64", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "Long";
			}
			if (string.Compare(baseType, "System.UInt16", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "UShort";
			}
			if (string.Compare(baseType, "System.UInt32", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "UInteger";
			}
			if (string.Compare(baseType, "System.UInt64", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "ULong";
			}
			if (string.Compare(baseType, "System.String", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "String";
			}
			if (string.Compare(baseType, "System.DateTime", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "Date";
			}
			if (string.Compare(baseType, "System.Decimal", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "Decimal";
			}
			if (string.Compare(baseType, "System.Single", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "Single";
			}
			if (string.Compare(baseType, "System.Double", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "Double";
			}
			if (string.Compare(baseType, "System.Boolean", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "Boolean";
			}
			if (string.Compare(baseType, "System.Char", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "Char";
			}
			if (string.Compare(baseType, "System.Object", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "Object";
			}
			StringBuilder stringBuilder = new StringBuilder(baseType.Length + 10);
			if ((typeRef.Options & CodeTypeReferenceOptions.GlobalReference) != (CodeTypeReferenceOptions)0)
			{
				stringBuilder.Append("Global.");
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < baseType.Length; i++)
			{
				char c = baseType[i];
				if (c != '+' && c != '.')
				{
					if (c == '`')
					{
						stringBuilder.Append(this.CreateEscapedIdentifier(baseType.Substring(num, i - num)));
						i++;
						int num3 = 0;
						while (i < baseType.Length && baseType[i] >= '0' && baseType[i] <= '9')
						{
							num3 = num3 * 10 + (int)(baseType[i] - '0');
							i++;
						}
						this.GetTypeArgumentsOutput(typeRef.TypeArguments, num2, num3, stringBuilder);
						num2 += num3;
						if (i < baseType.Length && (baseType[i] == '+' || baseType[i] == '.'))
						{
							stringBuilder.Append('.');
							i++;
						}
						num = i;
					}
				}
				else
				{
					stringBuilder.Append(this.CreateEscapedIdentifier(baseType.Substring(num, i - num)));
					stringBuilder.Append('.');
					i++;
					num = i;
				}
			}
			if (num < baseType.Length)
			{
				stringBuilder.Append(this.CreateEscapedIdentifier(baseType.Substring(num)));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006014 File Offset: 0x00004214
		private string GetTypeOutputWithoutArrayPostFix(CodeTypeReference typeRef)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (typeRef.ArrayElementType != null)
			{
				typeRef = typeRef.ArrayElementType;
			}
			stringBuilder.Append(this.GetBaseTypeOutput(typeRef));
			return stringBuilder.ToString();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00006050 File Offset: 0x00004250
		private string GetTypeArgumentsOutput(CodeTypeReferenceCollection typeArguments)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			this.GetTypeArgumentsOutput(typeArguments, 0, typeArguments.Count, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00006080 File Offset: 0x00004280
		private void GetTypeArgumentsOutput(CodeTypeReferenceCollection typeArguments, int start, int length, StringBuilder sb)
		{
			sb.Append("(Of ");
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
			sb.Append(')');
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000060E8 File Offset: 0x000042E8
		protected override string GetTypeOutput(CodeTypeReference typeRef)
		{
			string text = string.Empty;
			text += this.GetTypeOutputWithoutArrayPostFix(typeRef);
			if (typeRef.ArrayRank > 0)
			{
				text += this.GetArrayPostfix(typeRef);
			}
			return text;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00006121 File Offset: 0x00004321
		protected override void ContinueOnNewLine(string st)
		{
			base.Output.Write(st);
			base.Output.WriteLine(" _");
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000613F File Offset: 0x0000433F
		private bool IsGeneratingStatements()
		{
			return this.statementDepth > 0;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000614C File Offset: 0x0000434C
		private void GenerateVBStatements(CodeStatementCollection stms)
		{
			this.statementDepth++;
			try
			{
				base.GenerateStatements(stms);
			}
			finally
			{
				this.statementDepth--;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00006190 File Offset: 0x00004390
		protected override CompilerResults FromFileBatch(CompilerParameters options, string[] fileNames)
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
			string text2 = (options.GenerateExecutable ? "exe" : "dll");
			string text3 = "." + text2;
			if (options.OutputAssembly == null || options.OutputAssembly.Length == 0)
			{
				options.OutputAssembly = compilerResults.TempFiles.AddExtension(text2, !options.GenerateInMemory);
				new FileStream(options.OutputAssembly, FileMode.Create, FileAccess.ReadWrite).Close();
				flag = true;
			}
			string text4 = options.OutputAssembly;
			if (!Path.GetExtension(text4).Equals(text3, StringComparison.OrdinalIgnoreCase))
			{
				text4 += text3;
			}
			string text5 = "pdb";
			if (options.CompilerOptions != null && options.CompilerOptions.IndexOf("/debug:pdbonly", StringComparison.OrdinalIgnoreCase) != -1)
			{
				compilerResults.TempFiles.AddExtension(text5, true);
			}
			else
			{
				compilerResults.TempFiles.AddExtension(text5);
			}
			string text6 = this.CmdArgsFromParameters(options) + " " + CodeCompiler.JoinStringArray(fileNames, " ");
			string responseFileCmdArgs = this.GetResponseFileCmdArgs(options, text6);
			string text7 = null;
			if (responseFileCmdArgs != null)
			{
				text7 = text6;
				text6 = responseFileCmdArgs;
			}
			base.Compile(options, RedistVersionInfo.GetCompilerPath(this.provOptions, this.CompilerName), this.CompilerName, text6, ref text, ref num, text7);
			compilerResults.NativeCompilerReturnValue = num;
			if (num != 0 || options.WarningLevel > 0)
			{
				byte[] array = VBCodeGenerator.ReadAllBytes(text, FileShare.ReadWrite);
				string @string = Encoding.UTF8.GetString(array);
				string[] array2 = Regex.Split(@string, "\\r\\n");
				foreach (string text8 in array2)
				{
					compilerResults.Output.Add(text8);
					this.ProcessCompilerOutputLine(compilerResults, text8);
				}
				if (num != 0 && flag)
				{
					File.Delete(text4);
				}
			}
			if (!compilerResults.Errors.HasErrors && options.GenerateInMemory)
			{
				FileStream fileStream = new FileStream(text4, FileMode.Open, FileAccess.Read, FileShare.Read);
				try
				{
					int num2 = (int)fileStream.Length;
					byte[] array4 = new byte[num2];
					fileStream.Read(array4, 0, num2);
					SecurityPermission securityPermission2 = new SecurityPermission(SecurityPermissionFlag.ControlEvidence);
					securityPermission2.Assert();
					try
					{
						if (!FileIntegrity.IsEnabled)
						{
							compilerResults.CompiledAssembly = Assembly.Load(array4, null, options.Evidence);
							return compilerResults;
						}
						if (!FileIntegrity.IsTrusted(fileStream.SafeFileHandle))
						{
							throw new IOException(SR.GetString("FileIntegrityCheckFailed", new object[] { text4 }));
						}
						compilerResults.CompiledAssembly = CodeCompiler.LoadImageSkipIntegrityCheck(array4, null, options.Evidence);
						return compilerResults;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				finally
				{
					fileStream.Close();
				}
			}
			compilerResults.PathToAssembly = text4;
			return compilerResults;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00006498 File Offset: 0x00004698
		private static byte[] ReadAllBytes(string file, FileShare share)
		{
			byte[] array;
			using (FileStream fileStream = File.Open(file, FileMode.Open, FileAccess.Read, share))
			{
				int num = 0;
				long length = fileStream.Length;
				if (length > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("file");
				}
				int i = (int)length;
				array = new byte[i];
				while (i > 0)
				{
					int num2 = fileStream.Read(array, num, i);
					if (num2 == 0)
					{
						throw new EndOfStreamException();
					}
					num += num2;
					i -= num2;
				}
			}
			return array;
		}

		// Token: 0x0400005A RID: 90
		private const int MaxLineLength = 80;

		// Token: 0x0400005B RID: 91
		private const GeneratorSupport LanguageSupport = GeneratorSupport.ArraysOfArrays | GeneratorSupport.EntryPointMethod | GeneratorSupport.GotoStatements | GeneratorSupport.MultidimensionalArrays | GeneratorSupport.StaticConstructors | GeneratorSupport.TryCatchStatements | GeneratorSupport.ReturnTypeAttributes | GeneratorSupport.DeclareValueTypes | GeneratorSupport.DeclareEnums | GeneratorSupport.DeclareDelegates | GeneratorSupport.DeclareInterfaces | GeneratorSupport.DeclareEvents | GeneratorSupport.AssemblyAttributes | GeneratorSupport.ParameterAttributes | GeneratorSupport.ReferenceParameters | GeneratorSupport.ChainedConstructorArguments | GeneratorSupport.NestedTypes | GeneratorSupport.MultipleInterfaceMembers | GeneratorSupport.PublicStaticMembers | GeneratorSupport.ComplexExpressions | GeneratorSupport.Win32Resources | GeneratorSupport.Resources | GeneratorSupport.PartialTypes | GeneratorSupport.GenericTypeReference | GeneratorSupport.GenericTypeDeclaration | GeneratorSupport.DeclareIndexerProperties;

		// Token: 0x0400005C RID: 92
		private static volatile Regex outputReg;

		// Token: 0x0400005D RID: 93
		private int statementDepth;

		// Token: 0x0400005E RID: 94
		private IDictionary<string, string> provOptions;

		// Token: 0x0400005F RID: 95
		private static readonly string[][] keywords = new string[][]
		{
			null,
			new string[] { "as", "do", "if", "in", "is", "me", "of", "on", "or", "to" },
			new string[]
			{
				"and", "dim", "end", "for", "get", "let", "lib", "mod", "new", "not",
				"rem", "set", "sub", "try", "xor"
			},
			new string[]
			{
				"ansi", "auto", "byte", "call", "case", "cdbl", "cdec", "char", "cint", "clng",
				"cobj", "csng", "cstr", "date", "each", "else", "enum", "exit", "goto", "like",
				"long", "loop", "next", "step", "stop", "then", "true", "wend", "when", "with"
			},
			new string[]
			{
				"alias", "byref", "byval", "catch", "cbool", "cbyte", "cchar", "cdate", "class", "const",
				"ctype", "cuint", "culng", "endif", "erase", "error", "event", "false", "gosub", "isnot",
				"redim", "sbyte", "short", "throw", "ulong", "until", "using", "while"
			},
			new string[]
			{
				"csbyte", "cshort", "double", "elseif", "friend", "global", "module", "mybase", "object", "option",
				"orelse", "public", "resume", "return", "select", "shared", "single", "static", "string", "typeof",
				"ushort"
			},
			new string[]
			{
				"andalso", "boolean", "cushort", "decimal", "declare", "default", "finally", "gettype", "handles", "imports",
				"integer", "myclass", "nothing", "partial", "private", "shadows", "trycast", "unicode", "variant"
			},
			new string[]
			{
				"assembly", "continue", "delegate", "function", "inherits", "operator", "optional", "preserve", "property", "readonly",
				"synclock", "uinteger", "widening"
			},
			new string[] { "addressof", "interface", "namespace", "narrowing", "overloads", "overrides", "protected", "structure", "writeonly" },
			new string[] { "addhandler", "directcast", "implements", "paramarray", "raiseevent", "withevents" },
			new string[] { "mustinherit", "overridable" },
			new string[] { "mustoverride" },
			new string[] { "removehandler" },
			new string[] { "class_finalize", "notinheritable", "notoverridable" },
			null,
			new string[] { "class_initialize" }
		};
	}
}
