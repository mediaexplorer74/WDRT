using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides an example implementation of the <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> interface. This class is abstract.</summary>
	// Token: 0x02000670 RID: 1648
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class CodeGenerator : ICodeGenerator
	{
		/// <summary>Gets the code type declaration for the current class.</summary>
		/// <returns>The code type declaration for the current class.</returns>
		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x000F68D0 File Offset: 0x000F4AD0
		protected CodeTypeDeclaration CurrentClass
		{
			get
			{
				return this.currentClass;
			}
		}

		/// <summary>Gets the current class name.</summary>
		/// <returns>The current class name.</returns>
		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06003BC4 RID: 15300 RVA: 0x000F68D8 File Offset: 0x000F4AD8
		protected string CurrentTypeName
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

		/// <summary>Gets the current member of the class.</summary>
		/// <returns>The current member of the class.</returns>
		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x000F68F3 File Offset: 0x000F4AF3
		protected CodeTypeMember CurrentMember
		{
			get
			{
				return this.currentMember;
			}
		}

		/// <summary>Gets the current member name.</summary>
		/// <returns>The name of the current member.</returns>
		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06003BC6 RID: 15302 RVA: 0x000F68FB File Offset: 0x000F4AFB
		protected string CurrentMemberName
		{
			get
			{
				if (this.currentMember != null)
				{
					return this.currentMember.Name;
				}
				return "<% unknown %>";
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is an interface.</summary>
		/// <returns>
		///   <see langword="true" /> if the current object is an interface; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x000F6916 File Offset: 0x000F4B16
		protected bool IsCurrentInterface
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsInterface;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is a class.</summary>
		/// <returns>
		///   <see langword="true" /> if the current object is a class; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x000F693A File Offset: 0x000F4B3A
		protected bool IsCurrentClass
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsClass;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is a value type or struct.</summary>
		/// <returns>
		///   <see langword="true" /> if the current object is a value type or struct; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06003BC9 RID: 15305 RVA: 0x000F695E File Offset: 0x000F4B5E
		protected bool IsCurrentStruct
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsStruct;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is an enumeration.</summary>
		/// <returns>
		///   <see langword="true" /> if the current object is an enumeration; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06003BCA RID: 15306 RVA: 0x000F6982 File Offset: 0x000F4B82
		protected bool IsCurrentEnum
		{
			get
			{
				return this.currentClass != null && !(this.currentClass is CodeTypeDelegate) && this.currentClass.IsEnum;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is a delegate.</summary>
		/// <returns>
		///   <see langword="true" /> if the current object is a delegate; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06003BCB RID: 15307 RVA: 0x000F69A6 File Offset: 0x000F4BA6
		protected bool IsCurrentDelegate
		{
			get
			{
				return this.currentClass != null && this.currentClass is CodeTypeDelegate;
			}
		}

		/// <summary>Gets or sets the amount of spaces to indent each indentation level.</summary>
		/// <returns>The number of spaces to indent for each indentation level.</returns>
		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x000F69C0 File Offset: 0x000F4BC0
		// (set) Token: 0x06003BCD RID: 15309 RVA: 0x000F69CD File Offset: 0x000F4BCD
		protected int Indent
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

		/// <summary>Gets the token that represents <see langword="null" />.</summary>
		/// <returns>The token that represents <see langword="null" />.</returns>
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06003BCE RID: 15310
		protected abstract string NullToken { get; }

		/// <summary>Gets the text writer to use for output.</summary>
		/// <returns>The text writer to use for output.</returns>
		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06003BCF RID: 15311 RVA: 0x000F69DB File Offset: 0x000F4BDB
		protected TextWriter Output
		{
			get
			{
				return this.output;
			}
		}

		/// <summary>Gets the options to be used by the code generator.</summary>
		/// <returns>An object that indicates the options for the code generator to use.</returns>
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06003BD0 RID: 15312 RVA: 0x000F69E3 File Offset: 0x000F4BE3
		protected CodeGeneratorOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x000F69EC File Offset: 0x000F4BEC
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

		/// <summary>Generates code for the specified code directives.</summary>
		/// <param name="directives">The code directives to generate code for.</param>
		// Token: 0x06003BD2 RID: 15314 RVA: 0x000F6B10 File Offset: 0x000F4D10
		protected virtual void GenerateDirectives(CodeDirectiveCollection directives)
		{
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x000F6B14 File Offset: 0x000F4D14
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

		// Token: 0x06003BD4 RID: 15316 RVA: 0x000F6CAC File Offset: 0x000F4EAC
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

		/// <summary>Generates code for the namespaces in the specified compile unit.</summary>
		/// <param name="e">The compile unit to generate namespaces for.</param>
		// Token: 0x06003BD5 RID: 15317 RVA: 0x000F6DA4 File Offset: 0x000F4FA4
		protected void GenerateNamespaces(CodeCompileUnit e)
		{
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				((ICodeGenerator)this).GenerateCodeFromNamespace(codeNamespace, this.output.InnerWriter, this.options);
			}
		}

		/// <summary>Generates code for the specified namespace and the classes it contains.</summary>
		/// <param name="e">The namespace to generate classes for.</param>
		// Token: 0x06003BD6 RID: 15318 RVA: 0x000F6E10 File Offset: 0x000F5010
		protected void GenerateTypes(CodeNamespace e)
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

		/// <summary>Gets a value indicating whether the generator provides support for the language features represented by the specified <see cref="T:System.CodeDom.Compiler.GeneratorSupport" /> object.</summary>
		/// <param name="support">The capabilities to test the generator for.</param>
		/// <returns>
		///   <see langword="true" /> if the specified capabilities are supported; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003BD7 RID: 15319 RVA: 0x000F6E94 File Offset: 0x000F5094
		bool ICodeGenerator.Supports(GeneratorSupport support)
		{
			return this.Supports(support);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) type declaration and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">The type to generate code for.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06003BD8 RID: 15320 RVA: 0x000F6EA0 File Offset: 0x000F50A0
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

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) expression and outputs it to the specified text writer.</summary>
		/// <param name="e">The expression to generate code for.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06003BD9 RID: 15321 RVA: 0x000F6F34 File Offset: 0x000F5134
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

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) compilation unit and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">The CodeDOM compilation unit to generate code for.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06003BDA RID: 15322 RVA: 0x000F6FC8 File Offset: 0x000F51C8
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

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) namespace and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">The namespace to generate code for.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06003BDB RID: 15323 RVA: 0x000F7074 File Offset: 0x000F5274
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

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) statement and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">The statement that contains the CodeDOM elements to translate.</param>
		/// <param name="w">The text writer to output code to.</param>
		/// <param name="o">The options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06003BDC RID: 15324 RVA: 0x000F7108 File Offset: 0x000F5308
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

		/// <summary>Generates code for the specified class member using the specified text writer and code generator options.</summary>
		/// <param name="member">The class member to generate code for.</param>
		/// <param name="writer">The text writer to output code to.</param>
		/// <param name="options">The options to use when generating the code.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.CodeDom.Compiler.CodeGenerator.Output" /> property is not <see langword="null" />.</exception>
		// Token: 0x06003BDD RID: 15325 RVA: 0x000F719C File Offset: 0x000F539C
		public virtual void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
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

		/// <summary>Gets a value that indicates whether the specified value is a valid identifier for the current language.</summary>
		/// <param name="value">The value to test.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is a valid identifier; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003BDE RID: 15326 RVA: 0x000F7228 File Offset: 0x000F5428
		bool ICodeGenerator.IsValidIdentifier(string value)
		{
			return this.IsValidIdentifier(value);
		}

		/// <summary>Throws an exception if the specified value is not a valid identifier.</summary>
		/// <param name="value">The identifier to validate.</param>
		// Token: 0x06003BDF RID: 15327 RVA: 0x000F7231 File Offset: 0x000F5431
		void ICodeGenerator.ValidateIdentifier(string value)
		{
			this.ValidateIdentifier(value);
		}

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <param name="value">The string to create an escaped identifier for.</param>
		/// <returns>The escaped identifier for the value.</returns>
		// Token: 0x06003BE0 RID: 15328 RVA: 0x000F723A File Offset: 0x000F543A
		string ICodeGenerator.CreateEscapedIdentifier(string value)
		{
			return this.CreateEscapedIdentifier(value);
		}

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <param name="value">The string to generate a valid identifier for.</param>
		/// <returns>A valid identifier for the specified value.</returns>
		// Token: 0x06003BE1 RID: 15329 RVA: 0x000F7243 File Offset: 0x000F5443
		string ICodeGenerator.CreateValidIdentifier(string value)
		{
			return this.CreateValidIdentifier(value);
		}

		/// <summary>Gets the type indicated by the specified <see cref="T:System.CodeDom.CodeTypeReference" />.</summary>
		/// <param name="type">The type to return.</param>
		/// <returns>The name of the data type reference.</returns>
		// Token: 0x06003BE2 RID: 15330 RVA: 0x000F724C File Offset: 0x000F544C
		string ICodeGenerator.GetTypeOutput(CodeTypeReference type)
		{
			return this.GetTypeOutput(type);
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x000F7258 File Offset: 0x000F5458
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

		// Token: 0x06003BE4 RID: 15332 RVA: 0x000F7350 File Offset: 0x000F5550
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

		/// <summary>Generates code for the specified code expression.</summary>
		/// <param name="e">The code expression to generate code for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid <see cref="T:System.CodeDom.CodeStatement" />.</exception>
		// Token: 0x06003BE5 RID: 15333 RVA: 0x000F7448 File Offset: 0x000F5648
		protected void GenerateExpression(CodeExpression e)
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

		// Token: 0x06003BE6 RID: 15334 RVA: 0x000F7698 File Offset: 0x000F5898
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

		// Token: 0x06003BE7 RID: 15335 RVA: 0x000F7790 File Offset: 0x000F5990
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

		/// <summary>Outputs the code of the specified literal code fragment compile unit.</summary>
		/// <param name="e">The literal code fragment compile unit to generate code for.</param>
		// Token: 0x06003BE8 RID: 15336 RVA: 0x000F78B0 File Offset: 0x000F5AB0
		protected virtual void GenerateSnippetCompileUnit(CodeSnippetCompileUnit e)
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

		// Token: 0x06003BE9 RID: 15337 RVA: 0x000F791C File Offset: 0x000F5B1C
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

		// Token: 0x06003BEA RID: 15338 RVA: 0x000F7A54 File Offset: 0x000F5C54
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

		/// <summary>Generates code for the specified compile unit.</summary>
		/// <param name="e">The compile unit to generate code for.</param>
		// Token: 0x06003BEB RID: 15339 RVA: 0x000F7AC0 File Offset: 0x000F5CC0
		protected virtual void GenerateCompileUnit(CodeCompileUnit e)
		{
			this.GenerateCompileUnitStart(e);
			this.GenerateNamespaces(e);
			this.GenerateCompileUnitEnd(e);
		}

		/// <summary>Generates code for the specified namespace.</summary>
		/// <param name="e">The namespace to generate code for.</param>
		// Token: 0x06003BEC RID: 15340 RVA: 0x000F7AD7 File Offset: 0x000F5CD7
		protected virtual void GenerateNamespace(CodeNamespace e)
		{
			this.GenerateCommentStatements(e.Comments);
			this.GenerateNamespaceStart(e);
			this.GenerateNamespaceImports(e);
			this.Output.WriteLine("");
			this.GenerateTypes(e);
			this.GenerateNamespaceEnd(e);
		}

		/// <summary>Generates code for the specified namespace import.</summary>
		/// <param name="e">The namespace import to generate code for.</param>
		// Token: 0x06003BED RID: 15341 RVA: 0x000F7B14 File Offset: 0x000F5D14
		protected void GenerateNamespaceImports(CodeNamespace e)
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

		// Token: 0x06003BEE RID: 15342 RVA: 0x000F7B74 File Offset: 0x000F5D74
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

		/// <summary>Generates code for the specified statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid <see cref="T:System.CodeDom.CodeStatement" />.</exception>
		// Token: 0x06003BEF RID: 15343 RVA: 0x000F7C6C File Offset: 0x000F5E6C
		protected void GenerateStatement(CodeStatement e)
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

		/// <summary>Generates code for the specified statement collection.</summary>
		/// <param name="stms">The statements to generate code for.</param>
		// Token: 0x06003BF0 RID: 15344 RVA: 0x000F7E68 File Offset: 0x000F6068
		protected void GenerateStatements(CodeStatementCollection stms)
		{
			foreach (object obj in stms)
			{
				((ICodeGenerator)this).GenerateCodeFromStatement((CodeStatement)obj, this.output.InnerWriter, this.options);
			}
		}

		/// <summary>Generates code for the specified attribute declaration collection.</summary>
		/// <param name="attributes">The attributes to generate code for.</param>
		// Token: 0x06003BF1 RID: 15345 RVA: 0x000F7EA8 File Offset: 0x000F60A8
		protected virtual void OutputAttributeDeclarations(CodeAttributeDeclarationCollection attributes)
		{
			if (attributes.Count == 0)
			{
				return;
			}
			this.GenerateAttributeDeclarationsStart(attributes);
			bool flag = true;
			IEnumerator enumerator = attributes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.ContinueOnNewLine(", ");
				}
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)enumerator.Current;
				this.Output.Write(codeAttributeDeclaration.Name);
				this.Output.Write("(");
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
						this.Output.Write(", ");
					}
					this.OutputAttributeArgument(codeAttributeArgument);
				}
				this.Output.Write(")");
			}
			this.GenerateAttributeDeclarationsEnd(attributes);
		}

		/// <summary>Outputs an argument in an attribute block.</summary>
		/// <param name="arg">The attribute argument to generate code for.</param>
		// Token: 0x06003BF2 RID: 15346 RVA: 0x000F7FA4 File Offset: 0x000F61A4
		protected virtual void OutputAttributeArgument(CodeAttributeArgument arg)
		{
			if (arg.Name != null && arg.Name.Length > 0)
			{
				this.OutputIdentifier(arg.Name);
				this.Output.Write("=");
			}
			((ICodeGenerator)this).GenerateCodeFromExpression(arg.Value, this.output.InnerWriter, this.options);
		}

		/// <summary>Generates code for the specified <see cref="T:System.CodeDom.FieldDirection" />.</summary>
		/// <param name="dir">One of the enumeration values that indicates the attribute of the field.</param>
		// Token: 0x06003BF3 RID: 15347 RVA: 0x000F8000 File Offset: 0x000F6200
		protected virtual void OutputDirection(FieldDirection dir)
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

		/// <summary>Outputs a field scope modifier that corresponds to the specified attributes.</summary>
		/// <param name="attributes">One of the enumeration values that specifies the attributes.</param>
		// Token: 0x06003BF4 RID: 15348 RVA: 0x000F8038 File Offset: 0x000F6238
		protected virtual void OutputFieldScopeModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.VTableMask;
			if (memberAttributes == MemberAttributes.New)
			{
				this.Output.Write("new ");
			}
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

		/// <summary>Generates code for the specified member access modifier.</summary>
		/// <param name="attributes">One of the enumeration values that indicates the member access modifier to generate code for.</param>
		// Token: 0x06003BF5 RID: 15349 RVA: 0x000F80A4 File Offset: 0x000F62A4
		protected virtual void OutputMemberAccessModifier(MemberAttributes attributes)
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

		/// <summary>Generates code for the specified member scope modifier.</summary>
		/// <param name="attributes">One of the enumeration values that indicates the member scope modifier to generate code for.</param>
		// Token: 0x06003BF6 RID: 15350 RVA: 0x000F8158 File Offset: 0x000F6358
		protected virtual void OutputMemberScopeModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.VTableMask;
			if (memberAttributes == MemberAttributes.New)
			{
				this.Output.Write("new ");
			}
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
				MemberAttributes memberAttributes2 = attributes & MemberAttributes.AccessMask;
				if (memberAttributes2 == MemberAttributes.Family || memberAttributes2 == MemberAttributes.Public)
				{
					this.Output.Write("virtual ");
				}
				return;
			}
			}
		}

		/// <summary>Generates code for the specified type.</summary>
		/// <param name="typeRef">The type to generate code for.</param>
		// Token: 0x06003BF7 RID: 15351
		protected abstract void OutputType(CodeTypeReference typeRef);

		/// <summary>Generates code for the specified type attributes.</summary>
		/// <param name="attributes">One of the enumeration values that indicates the type attributes to generate code for.</param>
		/// <param name="isStruct">
		///   <see langword="true" /> if the type is a struct; otherwise, <see langword="false" />.</param>
		/// <param name="isEnum">
		///   <see langword="true" /> if the type is an enum; otherwise, <see langword="false" />.</param>
		// Token: 0x06003BF8 RID: 15352 RVA: 0x000F8210 File Offset: 0x000F6410
		protected virtual void OutputTypeAttributes(TypeAttributes attributes, bool isStruct, bool isEnum)
		{
			TypeAttributes typeAttributes = attributes & TypeAttributes.VisibilityMask;
			if (typeAttributes - TypeAttributes.Public > 1)
			{
				if (typeAttributes == TypeAttributes.NestedPrivate)
				{
					this.Output.Write("private ");
				}
			}
			else
			{
				this.Output.Write("public ");
			}
			if (isStruct)
			{
				this.Output.Write("struct ");
				return;
			}
			if (isEnum)
			{
				this.Output.Write("enum ");
				return;
			}
			TypeAttributes typeAttributes2 = attributes & TypeAttributes.ClassSemanticsMask;
			if (typeAttributes2 == TypeAttributes.NotPublic)
			{
				if ((attributes & TypeAttributes.Sealed) == TypeAttributes.Sealed)
				{
					this.Output.Write("sealed ");
				}
				if ((attributes & TypeAttributes.Abstract) == TypeAttributes.Abstract)
				{
					this.Output.Write("abstract ");
				}
				this.Output.Write("class ");
				return;
			}
			if (typeAttributes2 != TypeAttributes.ClassSemanticsMask)
			{
				return;
			}
			this.Output.Write("interface ");
		}

		/// <summary>Generates code for the specified object type and name pair.</summary>
		/// <param name="typeRef">The type.</param>
		/// <param name="name">The name for the object.</param>
		// Token: 0x06003BF9 RID: 15353 RVA: 0x000F82E2 File Offset: 0x000F64E2
		protected virtual void OutputTypeNamePair(CodeTypeReference typeRef, string name)
		{
			this.OutputType(typeRef);
			this.Output.Write(" ");
			this.OutputIdentifier(name);
		}

		/// <summary>Outputs the specified identifier.</summary>
		/// <param name="ident">The identifier to output.</param>
		// Token: 0x06003BFA RID: 15354 RVA: 0x000F8302 File Offset: 0x000F6502
		protected virtual void OutputIdentifier(string ident)
		{
			this.Output.Write(ident);
		}

		/// <summary>Generates code for the specified expression list.</summary>
		/// <param name="expressions">The expressions to generate code for.</param>
		// Token: 0x06003BFB RID: 15355 RVA: 0x000F8310 File Offset: 0x000F6510
		protected virtual void OutputExpressionList(CodeExpressionCollection expressions)
		{
			this.OutputExpressionList(expressions, false);
		}

		/// <summary>Generates code for the specified expression list.</summary>
		/// <param name="expressions">The expressions to generate code for.</param>
		/// <param name="newlineBetweenItems">
		///   <see langword="true" /> to insert a new line after each item; otherwise, <see langword="false" />.</param>
		// Token: 0x06003BFC RID: 15356 RVA: 0x000F831C File Offset: 0x000F651C
		protected virtual void OutputExpressionList(CodeExpressionCollection expressions, bool newlineBetweenItems)
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

		/// <summary>Generates code for the specified operator.</summary>
		/// <param name="op">The operator to generate code for.</param>
		// Token: 0x06003BFD RID: 15357 RVA: 0x000F83A8 File Offset: 0x000F65A8
		protected virtual void OutputOperator(CodeBinaryOperatorType op)
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

		/// <summary>Generates code for the specified parameters.</summary>
		/// <param name="parameters">The parameter declaration expressions to generate code for.</param>
		// Token: 0x06003BFE RID: 15358 RVA: 0x000F8520 File Offset: 0x000F6720
		protected virtual void OutputParameters(CodeParameterDeclarationExpressionCollection parameters)
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

		/// <summary>Generates code for the specified array creation expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> that indicates the expression to generate code for.</param>
		// Token: 0x06003BFF RID: 15359
		protected abstract void GenerateArrayCreateExpression(CodeArrayCreateExpression e);

		/// <summary>Generates code for the specified base reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeBaseReferenceExpression" /> that indicates the expression to generate code for.</param>
		// Token: 0x06003C00 RID: 15360
		protected abstract void GenerateBaseReferenceExpression(CodeBaseReferenceExpression e);

		/// <summary>Generates code for the specified binary operator expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> that indicates the expression to generate code for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is <see langword="null" />.</exception>
		// Token: 0x06003C01 RID: 15361 RVA: 0x000F85A8 File Offset: 0x000F67A8
		protected virtual void GenerateBinaryOperatorExpression(CodeBinaryOperatorExpression e)
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

		/// <summary>Generates a line-continuation character and outputs the specified string on a new line.</summary>
		/// <param name="st">The string to write on the new line.</param>
		// Token: 0x06003C02 RID: 15362 RVA: 0x000F8677 File Offset: 0x000F6877
		protected virtual void ContinueOnNewLine(string st)
		{
			this.Output.WriteLine(st);
		}

		/// <summary>Generates code for the specified cast expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCastExpression" /> that indicates the expression to generate code for.</param>
		// Token: 0x06003C03 RID: 15363
		protected abstract void GenerateCastExpression(CodeCastExpression e);

		/// <summary>Generates code for the specified delegate creation expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C04 RID: 15364
		protected abstract void GenerateDelegateCreateExpression(CodeDelegateCreateExpression e);

		/// <summary>Generates code for the specified field reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C05 RID: 15365
		protected abstract void GenerateFieldReferenceExpression(CodeFieldReferenceExpression e);

		/// <summary>Generates code for the specified argument reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> that indicates the expression to generate code for.</param>
		// Token: 0x06003C06 RID: 15366
		protected abstract void GenerateArgumentReferenceExpression(CodeArgumentReferenceExpression e);

		/// <summary>Generates code for the specified variable reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C07 RID: 15367
		protected abstract void GenerateVariableReferenceExpression(CodeVariableReferenceExpression e);

		/// <summary>Generates code for the specified indexer expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C08 RID: 15368
		protected abstract void GenerateIndexerExpression(CodeIndexerExpression e);

		/// <summary>Generates code for the specified array indexer expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> that indicates the expression to generate code for.</param>
		// Token: 0x06003C09 RID: 15369
		protected abstract void GenerateArrayIndexerExpression(CodeArrayIndexerExpression e);

		/// <summary>Outputs the code of the specified literal code fragment expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C0A RID: 15370
		protected abstract void GenerateSnippetExpression(CodeSnippetExpression e);

		/// <summary>Generates code for the specified method invoke expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C0B RID: 15371
		protected abstract void GenerateMethodInvokeExpression(CodeMethodInvokeExpression e);

		/// <summary>Generates code for the specified method reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C0C RID: 15372
		protected abstract void GenerateMethodReferenceExpression(CodeMethodReferenceExpression e);

		/// <summary>Generates code for the specified event reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C0D RID: 15373
		protected abstract void GenerateEventReferenceExpression(CodeEventReferenceExpression e);

		/// <summary>Generates code for the specified delegate invoke expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C0E RID: 15374
		protected abstract void GenerateDelegateInvokeExpression(CodeDelegateInvokeExpression e);

		/// <summary>Generates code for the specified object creation expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C0F RID: 15375
		protected abstract void GenerateObjectCreateExpression(CodeObjectCreateExpression e);

		/// <summary>Generates code for the specified parameter declaration expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C10 RID: 15376 RVA: 0x000F8688 File Offset: 0x000F6888
		protected virtual void GenerateParameterDeclarationExpression(CodeParameterDeclarationExpression e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.OutputAttributeDeclarations(e.CustomAttributes);
				this.Output.Write(" ");
			}
			this.OutputDirection(e.Direction);
			this.OutputTypeNamePair(e.Type, e.Name);
		}

		/// <summary>Generates code for the specified direction expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C11 RID: 15377 RVA: 0x000F86DD File Offset: 0x000F68DD
		protected virtual void GenerateDirectionExpression(CodeDirectionExpression e)
		{
			this.OutputDirection(e.Direction);
			this.GenerateExpression(e.Expression);
		}

		/// <summary>Generates code for the specified primitive expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> uses an invalid data type. Only the following data types are valid:  
		///
		/// string  
		///
		/// char  
		///
		/// byte  
		///
		/// Int16  
		///
		/// Int32  
		///
		/// Int64  
		///
		/// Single  
		///
		/// Double  
		///
		/// Decimal</exception>
		// Token: 0x06003C12 RID: 15378 RVA: 0x000F86F8 File Offset: 0x000F68F8
		protected virtual void GeneratePrimitiveExpression(CodePrimitiveExpression e)
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

		/// <summary>Generates code for a single-precision floating point number.</summary>
		/// <param name="s">The value to generate code for.</param>
		// Token: 0x06003C13 RID: 15379 RVA: 0x000F8901 File Offset: 0x000F6B01
		protected virtual void GenerateSingleFloatValue(float s)
		{
			this.Output.Write(s.ToString("R", CultureInfo.InvariantCulture));
		}

		/// <summary>Generates code for a double-precision floating point number.</summary>
		/// <param name="d">The value to generate code for.</param>
		// Token: 0x06003C14 RID: 15380 RVA: 0x000F891F File Offset: 0x000F6B1F
		protected virtual void GenerateDoubleValue(double d)
		{
			this.Output.Write(d.ToString("R", CultureInfo.InvariantCulture));
		}

		/// <summary>Generates code for the specified decimal value.</summary>
		/// <param name="d">The decimal value to generate code for.</param>
		// Token: 0x06003C15 RID: 15381 RVA: 0x000F893D File Offset: 0x000F6B3D
		protected virtual void GenerateDecimalValue(decimal d)
		{
			this.Output.Write(d.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>Generates code for the specified reference to a default value.</summary>
		/// <param name="e">The reference to generate code for.</param>
		// Token: 0x06003C16 RID: 15382 RVA: 0x000F8956 File Offset: 0x000F6B56
		protected virtual void GenerateDefaultValueExpression(CodeDefaultValueExpression e)
		{
		}

		/// <summary>Generates code for the specified property reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C17 RID: 15383
		protected abstract void GeneratePropertyReferenceExpression(CodePropertyReferenceExpression e);

		/// <summary>Generates code for the specified property set value reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C18 RID: 15384
		protected abstract void GeneratePropertySetValueReferenceExpression(CodePropertySetValueReferenceExpression e);

		/// <summary>Generates code for the specified this reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C19 RID: 15385
		protected abstract void GenerateThisReferenceExpression(CodeThisReferenceExpression e);

		/// <summary>Generates code for the specified type reference expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C1A RID: 15386 RVA: 0x000F8958 File Offset: 0x000F6B58
		protected virtual void GenerateTypeReferenceExpression(CodeTypeReferenceExpression e)
		{
			this.OutputType(e.Type);
		}

		/// <summary>Generates code for the specified type of expression.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C1B RID: 15387 RVA: 0x000F8966 File Offset: 0x000F6B66
		protected virtual void GenerateTypeOfExpression(CodeTypeOfExpression e)
		{
			this.Output.Write("typeof(");
			this.OutputType(e.Type);
			this.Output.Write(")");
		}

		/// <summary>Generates code for the specified expression statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06003C1C RID: 15388
		protected abstract void GenerateExpressionStatement(CodeExpressionStatement e);

		/// <summary>Generates code for the specified iteration statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06003C1D RID: 15389
		protected abstract void GenerateIterationStatement(CodeIterationStatement e);

		/// <summary>Generates code for the specified throw exception statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06003C1E RID: 15390
		protected abstract void GenerateThrowExceptionStatement(CodeThrowExceptionStatement e);

		/// <summary>Generates code for the specified comment statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.CodeDom.CodeCommentStatement.Comment" /> property of <paramref name="e" /> is not set.</exception>
		// Token: 0x06003C1F RID: 15391 RVA: 0x000F8994 File Offset: 0x000F6B94
		protected virtual void GenerateCommentStatement(CodeCommentStatement e)
		{
			if (e.Comment == null)
			{
				throw new ArgumentException(SR.GetString("Argument_NullComment", new object[] { "e" }), "e");
			}
			this.GenerateComment(e.Comment);
		}

		/// <summary>Generates code for the specified comment statements.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C20 RID: 15392 RVA: 0x000F89D0 File Offset: 0x000F6BD0
		protected virtual void GenerateCommentStatements(CodeCommentStatementCollection e)
		{
			foreach (object obj in e)
			{
				CodeCommentStatement codeCommentStatement = (CodeCommentStatement)obj;
				this.GenerateCommentStatement(codeCommentStatement);
			}
		}

		/// <summary>Generates code for the specified comment.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeComment" /> to generate code for.</param>
		// Token: 0x06003C21 RID: 15393
		protected abstract void GenerateComment(CodeComment e);

		/// <summary>Generates code for the specified method return statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06003C22 RID: 15394
		protected abstract void GenerateMethodReturnStatement(CodeMethodReturnStatement e);

		/// <summary>Generates code for the specified conditional statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06003C23 RID: 15395
		protected abstract void GenerateConditionStatement(CodeConditionStatement e);

		/// <summary>Generates code for the specified <see langword="try...catch...finally" /> statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06003C24 RID: 15396
		protected abstract void GenerateTryCatchFinallyStatement(CodeTryCatchFinallyStatement e);

		/// <summary>Generates code for the specified assignment statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeAssignStatement" /> that indicates the statement to generate code for.</param>
		// Token: 0x06003C25 RID: 15397
		protected abstract void GenerateAssignStatement(CodeAssignStatement e);

		/// <summary>Generates code for the specified attach event statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeAttachEventStatement" /> that indicates the statement to generate code for.</param>
		// Token: 0x06003C26 RID: 15398
		protected abstract void GenerateAttachEventStatement(CodeAttachEventStatement e);

		/// <summary>Generates code for the specified remove event statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06003C27 RID: 15399
		protected abstract void GenerateRemoveEventStatement(CodeRemoveEventStatement e);

		/// <summary>Generates code for the specified <see langword="goto" /> statement.</summary>
		/// <param name="e">The expression to generate code for.</param>
		// Token: 0x06003C28 RID: 15400
		protected abstract void GenerateGotoStatement(CodeGotoStatement e);

		/// <summary>Generates code for the specified labeled statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06003C29 RID: 15401
		protected abstract void GenerateLabeledStatement(CodeLabeledStatement e);

		/// <summary>Outputs the code of the specified literal code fragment statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06003C2A RID: 15402 RVA: 0x000F8A24 File Offset: 0x000F6C24
		protected virtual void GenerateSnippetStatement(CodeSnippetStatement e)
		{
			this.Output.WriteLine(e.Value);
		}

		/// <summary>Generates code for the specified variable declaration statement.</summary>
		/// <param name="e">The statement to generate code for.</param>
		// Token: 0x06003C2B RID: 15403
		protected abstract void GenerateVariableDeclarationStatement(CodeVariableDeclarationStatement e);

		/// <summary>Generates code for the specified line pragma start.</summary>
		/// <param name="e">The start of the line pragma to generate code for.</param>
		// Token: 0x06003C2C RID: 15404
		protected abstract void GenerateLinePragmaStart(CodeLinePragma e);

		/// <summary>Generates code for the specified line pragma end.</summary>
		/// <param name="e">The end of the line pragma to generate code for.</param>
		// Token: 0x06003C2D RID: 15405
		protected abstract void GenerateLinePragmaEnd(CodeLinePragma e);

		/// <summary>Generates code for the specified event.</summary>
		/// <param name="e">The member event to generate code for.</param>
		/// <param name="c">The type of the object that this event occurs on.</param>
		// Token: 0x06003C2E RID: 15406
		protected abstract void GenerateEvent(CodeMemberEvent e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified member field.</summary>
		/// <param name="e">The field to generate code for.</param>
		// Token: 0x06003C2F RID: 15407
		protected abstract void GenerateField(CodeMemberField e);

		/// <summary>Outputs the code of the specified literal code fragment class member.</summary>
		/// <param name="e">The member to generate code for.</param>
		// Token: 0x06003C30 RID: 15408
		protected abstract void GenerateSnippetMember(CodeSnippetTypeMember e);

		/// <summary>Generates code for the specified entry point method.</summary>
		/// <param name="e">The entry point for the code.</param>
		/// <param name="c">The code that declares the type.</param>
		// Token: 0x06003C31 RID: 15409
		protected abstract void GenerateEntryPointMethod(CodeEntryPointMethod e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified method.</summary>
		/// <param name="e">The member method to generate code for.</param>
		/// <param name="c">The type of the object that this method occurs on.</param>
		// Token: 0x06003C32 RID: 15410
		protected abstract void GenerateMethod(CodeMemberMethod e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified property.</summary>
		/// <param name="e">The property to generate code for.</param>
		/// <param name="c">The type of the object that this property occurs on.</param>
		// Token: 0x06003C33 RID: 15411
		protected abstract void GenerateProperty(CodeMemberProperty e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified constructor.</summary>
		/// <param name="e">The constructor to generate code for.</param>
		/// <param name="c">The type of the object that this constructor constructs.</param>
		// Token: 0x06003C34 RID: 15412
		protected abstract void GenerateConstructor(CodeConstructor e, CodeTypeDeclaration c);

		/// <summary>Generates code for the specified class constructor.</summary>
		/// <param name="e">The class constructor to generate code for.</param>
		// Token: 0x06003C35 RID: 15413
		protected abstract void GenerateTypeConstructor(CodeTypeConstructor e);

		/// <summary>Generates code for the specified start of the class.</summary>
		/// <param name="e">The start of the class to generate code for.</param>
		// Token: 0x06003C36 RID: 15414
		protected abstract void GenerateTypeStart(CodeTypeDeclaration e);

		/// <summary>Generates code for the specified end of the class.</summary>
		/// <param name="e">The end of the class to generate code for.</param>
		// Token: 0x06003C37 RID: 15415
		protected abstract void GenerateTypeEnd(CodeTypeDeclaration e);

		/// <summary>Generates code for the start of a compile unit.</summary>
		/// <param name="e">The compile unit to generate code for.</param>
		// Token: 0x06003C38 RID: 15416 RVA: 0x000F8A37 File Offset: 0x000F6C37
		protected virtual void GenerateCompileUnitStart(CodeCompileUnit e)
		{
			if (e.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(e.StartDirectives);
			}
		}

		/// <summary>Generates code for the end of a compile unit.</summary>
		/// <param name="e">The compile unit to generate code for.</param>
		// Token: 0x06003C39 RID: 15417 RVA: 0x000F8A53 File Offset: 0x000F6C53
		protected virtual void GenerateCompileUnitEnd(CodeCompileUnit e)
		{
			if (e.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(e.EndDirectives);
			}
		}

		/// <summary>Generates code for the start of a namespace.</summary>
		/// <param name="e">The namespace to generate code for.</param>
		// Token: 0x06003C3A RID: 15418
		protected abstract void GenerateNamespaceStart(CodeNamespace e);

		/// <summary>Generates code for the end of a namespace.</summary>
		/// <param name="e">The namespace to generate code for.</param>
		// Token: 0x06003C3B RID: 15419
		protected abstract void GenerateNamespaceEnd(CodeNamespace e);

		/// <summary>Generates code for the specified namespace import.</summary>
		/// <param name="e">The namespace import to generate code for.</param>
		// Token: 0x06003C3C RID: 15420
		protected abstract void GenerateNamespaceImport(CodeNamespaceImport e);

		/// <summary>Generates code for the specified attribute block start.</summary>
		/// <param name="attributes">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the start of the attribute block to generate code for.</param>
		// Token: 0x06003C3D RID: 15421
		protected abstract void GenerateAttributeDeclarationsStart(CodeAttributeDeclarationCollection attributes);

		/// <summary>Generates code for the specified attribute block end.</summary>
		/// <param name="attributes">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the end of the attribute block to generate code for.</param>
		// Token: 0x06003C3E RID: 15422
		protected abstract void GenerateAttributeDeclarationsEnd(CodeAttributeDeclarationCollection attributes);

		/// <summary>Gets a value indicating whether the specified code generation support is provided.</summary>
		/// <param name="support">The type of code generation support to test for.</param>
		/// <returns>
		///   <see langword="true" /> if the specified code generation support is provided; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003C3F RID: 15423
		protected abstract bool Supports(GeneratorSupport support);

		/// <summary>Gets a value indicating whether the specified value is a valid identifier.</summary>
		/// <param name="value">The value to test for conflicts with valid identifiers.</param>
		/// <returns>
		///   <see langword="true" /> if the value is a valid identifier; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003C40 RID: 15424
		protected abstract bool IsValidIdentifier(string value);

		/// <summary>Throws an exception if the specified string is not a valid identifier.</summary>
		/// <param name="value">The identifier to test for validity as an identifier.</param>
		/// <exception cref="T:System.ArgumentException">If the specified identifier is invalid or conflicts with reserved or language keywords.</exception>
		// Token: 0x06003C41 RID: 15425 RVA: 0x000F8A6F File Offset: 0x000F6C6F
		protected virtual void ValidateIdentifier(string value)
		{
			if (!this.IsValidIdentifier(value))
			{
				throw new ArgumentException(SR.GetString("InvalidIdentifier", new object[] { value }));
			}
		}

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <param name="value">The string to create an escaped identifier for.</param>
		/// <returns>The escaped identifier for the value.</returns>
		// Token: 0x06003C42 RID: 15426
		protected abstract string CreateEscapedIdentifier(string value);

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <param name="value">A string to create a valid identifier for.</param>
		/// <returns>A valid identifier for the value.</returns>
		// Token: 0x06003C43 RID: 15427
		protected abstract string CreateValidIdentifier(string value);

		/// <summary>Gets the name of the specified data type.</summary>
		/// <param name="value">The type whose name will be returned.</param>
		/// <returns>The name of the data type reference.</returns>
		// Token: 0x06003C44 RID: 15428
		protected abstract string GetTypeOutput(CodeTypeReference value);

		/// <summary>Converts the specified string by formatting it with escape codes.</summary>
		/// <param name="value">The string to convert.</param>
		/// <returns>The converted string.</returns>
		// Token: 0x06003C45 RID: 15429
		protected abstract string QuoteSnippetString(string value);

		/// <summary>Gets a value indicating whether the specified string is a valid identifier.</summary>
		/// <param name="value">The string to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the specified string is a valid identifier; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003C46 RID: 15430 RVA: 0x000F8A94 File Offset: 0x000F6C94
		public static bool IsValidLanguageIndependentIdentifier(string value)
		{
			return CodeGenerator.IsValidTypeNameOrIdentifier(value, false);
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x000F8A9D File Offset: 0x000F6C9D
		internal static bool IsValidLanguageIndependentTypeName(string value)
		{
			return CodeGenerator.IsValidTypeNameOrIdentifier(value, true);
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x000F8AA8 File Offset: 0x000F6CA8
		private static bool IsValidTypeNameOrIdentifier(string value, bool isTypeName)
		{
			bool flag = true;
			if (value.Length == 0)
			{
				return false;
			}
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				switch (char.GetUnicodeCategory(c))
				{
				case UnicodeCategory.UppercaseLetter:
				case UnicodeCategory.LowercaseLetter:
				case UnicodeCategory.TitlecaseLetter:
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.OtherLetter:
				case UnicodeCategory.LetterNumber:
					flag = false;
					break;
				case UnicodeCategory.NonSpacingMark:
				case UnicodeCategory.SpacingCombiningMark:
				case UnicodeCategory.DecimalDigitNumber:
				case UnicodeCategory.ConnectorPunctuation:
					if (flag && c != '_')
					{
						return false;
					}
					flag = false;
					break;
				case UnicodeCategory.EnclosingMark:
				case UnicodeCategory.OtherNumber:
				case UnicodeCategory.SpaceSeparator:
				case UnicodeCategory.LineSeparator:
				case UnicodeCategory.ParagraphSeparator:
				case UnicodeCategory.Control:
				case UnicodeCategory.Format:
				case UnicodeCategory.Surrogate:
				case UnicodeCategory.PrivateUse:
					goto IL_88;
				default:
					goto IL_88;
				}
				IL_97:
				i++;
				continue;
				IL_88:
				if (!isTypeName || !CodeGenerator.IsSpecialTypeChar(c, ref flag))
				{
					return false;
				}
				goto IL_97;
			}
			return true;
		}

		// Token: 0x06003C49 RID: 15433 RVA: 0x000F8B60 File Offset: 0x000F6D60
		private static bool IsSpecialTypeChar(char ch, ref bool nextMustBeStartChar)
		{
			if (ch <= '>')
			{
				switch (ch)
				{
				case '$':
				case '&':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
					break;
				case '%':
				case '\'':
				case '(':
				case ')':
					return false;
				default:
					switch (ch)
					{
					case ':':
					case '<':
					case '>':
						break;
					case ';':
					case '=':
						return false;
					default:
						return false;
					}
					break;
				}
			}
			else if (ch != '[' && ch != ']')
			{
				if (ch != '`')
				{
					return false;
				}
				return true;
			}
			nextMustBeStartChar = true;
			return true;
		}

		/// <summary>Attempts to validate each identifier field contained in the specified <see cref="T:System.CodeDom.CodeObject" /> or <see cref="N:System.CodeDom" /> tree.</summary>
		/// <param name="e">An object to test for invalid identifiers.</param>
		/// <exception cref="T:System.ArgumentException">The specified <see cref="T:System.CodeDom.CodeObject" /> contains an invalid identifier.</exception>
		// Token: 0x06003C4A RID: 15434 RVA: 0x000F8BE0 File Offset: 0x000F6DE0
		public static void ValidateIdentifiers(CodeObject e)
		{
			CodeValidator codeValidator = new CodeValidator();
			codeValidator.ValidateIdentifiers(e);
		}

		// Token: 0x04002C51 RID: 11345
		private const int ParameterMultilineThreshold = 15;

		// Token: 0x04002C52 RID: 11346
		private IndentedTextWriter output;

		// Token: 0x04002C53 RID: 11347
		private CodeGeneratorOptions options;

		// Token: 0x04002C54 RID: 11348
		private CodeTypeDeclaration currentClass;

		// Token: 0x04002C55 RID: 11349
		private CodeTypeMember currentMember;

		// Token: 0x04002C56 RID: 11350
		private bool inNestedBinary;
	}
}
