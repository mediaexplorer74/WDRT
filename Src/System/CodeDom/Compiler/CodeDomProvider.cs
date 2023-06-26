using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides a base class for <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementations. This class is abstract.</summary>
	// Token: 0x0200066F RID: 1647
	[ToolboxItem(false)]
	[ComVisible(true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class CodeDomProvider : Component
	{
		/// <summary>Gets a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the specified language and provider options.</summary>
		/// <param name="language">The language name.</param>
		/// <param name="providerOptions">A collection of provider options from the configuration file.</param>
		/// <returns>A CodeDOM provider that is implemented for the specified language name and options.</returns>
		// Token: 0x06003B9D RID: 15261 RVA: 0x000F64A0 File Offset: 0x000F46A0
		[ComVisible(false)]
		public static CodeDomProvider CreateProvider(string language, IDictionary<string, string> providerOptions)
		{
			CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(language);
			return compilerInfo.CreateProvider(providerOptions);
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the specified language.</summary>
		/// <param name="language">The language name.</param>
		/// <returns>A CodeDOM provider that is implemented for the specified language name.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="language" /> does not have a configured provider on this computer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="language" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06003B9E RID: 15262 RVA: 0x000F64BC File Offset: 0x000F46BC
		[ComVisible(false)]
		public static CodeDomProvider CreateProvider(string language)
		{
			CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(language);
			return compilerInfo.CreateProvider();
		}

		/// <summary>Returns a language name associated with the specified file name extension, as configured in the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> compiler configuration section.</summary>
		/// <param name="extension">A file name extension.</param>
		/// <returns>A language name associated with the file name extension, as configured in the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> compiler configuration settings.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <paramref name="extension" /> does not have a configured language provider on this computer.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="extension" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06003B9F RID: 15263 RVA: 0x000F64D8 File Offset: 0x000F46D8
		[ComVisible(false)]
		public static string GetLanguageFromExtension(string extension)
		{
			CompilerInfo compilerInfoForExtensionNoThrow = CodeDomProvider.GetCompilerInfoForExtensionNoThrow(extension);
			if (compilerInfoForExtensionNoThrow == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("CodeDomProvider_NotDefined"));
			}
			return compilerInfoForExtensionNoThrow._compilerLanguages[0];
		}

		/// <summary>Tests whether a language has a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation configured on the computer.</summary>
		/// <param name="language">The language name.</param>
		/// <returns>
		///   <see langword="true" /> if a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation is configured for the specified language; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="language" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06003BA0 RID: 15264 RVA: 0x000F6507 File Offset: 0x000F4707
		[ComVisible(false)]
		public static bool IsDefinedLanguage(string language)
		{
			return CodeDomProvider.GetCompilerInfoForLanguageNoThrow(language) != null;
		}

		/// <summary>Tests whether a file name extension has an associated <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation configured on the computer.</summary>
		/// <param name="extension">A file name extension.</param>
		/// <returns>
		///   <see langword="true" /> if a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation is configured for the specified file name extension; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="extension" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06003BA1 RID: 15265 RVA: 0x000F6512 File Offset: 0x000F4712
		[ComVisible(false)]
		public static bool IsDefinedExtension(string extension)
		{
			return CodeDomProvider.GetCompilerInfoForExtensionNoThrow(extension) != null;
		}

		/// <summary>Returns the language provider and compiler configuration settings for the specified language.</summary>
		/// <param name="language">A language name.</param>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> object populated with settings of the configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <paramref name="language" /> does not have a configured provider on this computer.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="language" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06003BA2 RID: 15266 RVA: 0x000F6520 File Offset: 0x000F4720
		[ComVisible(false)]
		public static CompilerInfo GetCompilerInfo(string language)
		{
			CompilerInfo compilerInfoForLanguageNoThrow = CodeDomProvider.GetCompilerInfoForLanguageNoThrow(language);
			if (compilerInfoForLanguageNoThrow == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("CodeDomProvider_NotDefined"));
			}
			return compilerInfoForLanguageNoThrow;
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x000F6548 File Offset: 0x000F4748
		private static CompilerInfo GetCompilerInfoForLanguageNoThrow(string language)
		{
			if (language == null)
			{
				throw new ArgumentNullException("language");
			}
			return (CompilerInfo)CodeDomProvider.Config._compilerLanguages[language.Trim()];
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x000F6580 File Offset: 0x000F4780
		private static CompilerInfo GetCompilerInfoForExtensionNoThrow(string extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			return (CompilerInfo)CodeDomProvider.Config._compilerExtensions[extension.Trim()];
		}

		/// <summary>Returns the language provider and compiler configuration settings for this computer.</summary>
		/// <returns>An array of type <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> representing the settings of all configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementations.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06003BA5 RID: 15269 RVA: 0x000F65B8 File Offset: 0x000F47B8
		[ComVisible(false)]
		public static CompilerInfo[] GetAllCompilerInfo()
		{
			ArrayList allCompilerInfo = CodeDomProvider.Config._allCompilerInfo;
			return (CompilerInfo[])allCompilerInfo.ToArray(typeof(CompilerInfo));
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06003BA6 RID: 15270 RVA: 0x000F65E8 File Offset: 0x000F47E8
		private static CodeDomCompilationConfiguration Config
		{
			get
			{
				CodeDomCompilationConfiguration codeDomCompilationConfiguration = (CodeDomCompilationConfiguration)PrivilegedConfigurationManager.GetSection("system.codedom");
				if (codeDomCompilationConfiguration == null)
				{
					return CodeDomCompilationConfiguration.Default;
				}
				return codeDomCompilationConfiguration;
			}
		}

		/// <summary>Gets the default file name extension to use for source code files in the current language.</summary>
		/// <returns>A file name extension corresponding to the extension of the source files of the current language. The base implementation always returns <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x000F660F File Offset: 0x000F480F
		public virtual string FileExtension
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets a language features identifier.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.LanguageOptions" /> that indicates special features of the language.</returns>
		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x000F6616 File Offset: 0x000F4816
		public virtual LanguageOptions LanguageOptions
		{
			get
			{
				return LanguageOptions.None;
			}
		}

		/// <summary>When overridden in a derived class, creates a new code generator.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> that can be used to generate <see cref="N:System.CodeDom" /> based source code representations.</returns>
		// Token: 0x06003BA9 RID: 15273
		[Obsolete("Callers should not use the ICodeGenerator interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public abstract ICodeGenerator CreateGenerator();

		/// <summary>When overridden in a derived class, creates a new code generator using the specified <see cref="T:System.IO.TextWriter" /> for output.</summary>
		/// <param name="output">A <see cref="T:System.IO.TextWriter" /> to use to output.</param>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> that can be used to generate <see cref="N:System.CodeDom" /> based source code representations.</returns>
		// Token: 0x06003BAA RID: 15274 RVA: 0x000F6619 File Offset: 0x000F4819
		public virtual ICodeGenerator CreateGenerator(TextWriter output)
		{
			return this.CreateGenerator();
		}

		/// <summary>When overridden in a derived class, creates a new code generator using the specified file name for output.</summary>
		/// <param name="fileName">The file name to output to.</param>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> that can be used to generate <see cref="N:System.CodeDom" /> based source code representations.</returns>
		// Token: 0x06003BAB RID: 15275 RVA: 0x000F6621 File Offset: 0x000F4821
		public virtual ICodeGenerator CreateGenerator(string fileName)
		{
			return this.CreateGenerator();
		}

		/// <summary>When overridden in a derived class, creates a new code compiler.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> that can be used for compilation of <see cref="N:System.CodeDom" /> based source code representations.</returns>
		// Token: 0x06003BAC RID: 15276
		[Obsolete("Callers should not use the ICodeCompiler interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public abstract ICodeCompiler CreateCompiler();

		/// <summary>When overridden in a derived class, creates a new code parser.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeParser" /> that can be used to parse source code. The base implementation always returns <see langword="null" />.</returns>
		// Token: 0x06003BAD RID: 15277 RVA: 0x000F6629 File Offset: 0x000F4829
		[Obsolete("Callers should not use the ICodeParser interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public virtual ICodeParser CreateParser()
		{
			return null;
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified data type.</summary>
		/// <param name="type">The type of object to retrieve a type converter for.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type, or <see langword="null" /> if a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type cannot be found.</returns>
		// Token: 0x06003BAE RID: 15278 RVA: 0x000F662C File Offset: 0x000F482C
		public virtual TypeConverter GetConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type);
		}

		/// <summary>Compiles an assembly based on the <see cref="N:System.CodeDom" /> trees contained in the specified array of <see cref="T:System.CodeDom.CodeCompileUnit" /> objects, using the specified compiler settings.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the settings for the compilation.</param>
		/// <param name="compilationUnits">An array of type <see cref="T:System.CodeDom.CodeCompileUnit" /> that indicates the code to compile.</param>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of the compilation.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateCompiler" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BAF RID: 15279 RVA: 0x000F6634 File Offset: 0x000F4834
		public virtual CompilerResults CompileAssemblyFromDom(CompilerParameters options, params CodeCompileUnit[] compilationUnits)
		{
			return this.CreateCompilerHelper().CompileAssemblyFromDomBatch(options, compilationUnits);
		}

		/// <summary>Compiles an assembly from the source code contained in the specified files, using the specified compiler settings.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the settings for the compilation.</param>
		/// <param name="fileNames">An array of the names of the files to compile.</param>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of compilation.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateCompiler" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BB0 RID: 15280 RVA: 0x000F6643 File Offset: 0x000F4843
		public virtual CompilerResults CompileAssemblyFromFile(CompilerParameters options, params string[] fileNames)
		{
			return this.CreateCompilerHelper().CompileAssemblyFromFileBatch(options, fileNames);
		}

		/// <summary>Compiles an assembly from the specified array of strings containing source code, using the specified compiler settings.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler settings for this compilation.</param>
		/// <param name="sources">An array of source code strings to compile.</param>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of compilation.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateCompiler" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BB1 RID: 15281 RVA: 0x000F6652 File Offset: 0x000F4852
		public virtual CompilerResults CompileAssemblyFromSource(CompilerParameters options, params string[] sources)
		{
			return this.CreateCompilerHelper().CompileAssemblyFromSourceBatch(options, sources);
		}

		/// <summary>Returns a value that indicates whether the specified value is a valid identifier for the current language.</summary>
		/// <param name="value">The value to verify as a valid identifier.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is a valid identifier; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BB2 RID: 15282 RVA: 0x000F6661 File Offset: 0x000F4861
		public virtual bool IsValidIdentifier(string value)
		{
			return this.CreateGeneratorHelper().IsValidIdentifier(value);
		}

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <param name="value">The string for which to create an escaped identifier.</param>
		/// <returns>The escaped identifier for the value.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BB3 RID: 15283 RVA: 0x000F666F File Offset: 0x000F486F
		public virtual string CreateEscapedIdentifier(string value)
		{
			return this.CreateGeneratorHelper().CreateEscapedIdentifier(value);
		}

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <param name="value">The string for which to generate a valid identifier.</param>
		/// <returns>A valid identifier for the specified value.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BB4 RID: 15284 RVA: 0x000F667D File Offset: 0x000F487D
		public virtual string CreateValidIdentifier(string value)
		{
			return this.CreateGeneratorHelper().CreateValidIdentifier(value);
		}

		/// <summary>Gets the type indicated by the specified <see cref="T:System.CodeDom.CodeTypeReference" />.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type to return.</param>
		/// <returns>A text representation of the specified type, formatted for the language in which code is generated by this code generator. In Visual Basic, for example, passing in a <see cref="T:System.CodeDom.CodeTypeReference" /> for the <see cref="T:System.Int32" /> type will return "Integer".</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BB5 RID: 15285 RVA: 0x000F668B File Offset: 0x000F488B
		public virtual string GetTypeOutput(CodeTypeReference type)
		{
			return this.CreateGeneratorHelper().GetTypeOutput(type);
		}

		/// <summary>Returns a value indicating whether the specified code generation support is provided.</summary>
		/// <param name="generatorSupport">A <see cref="T:System.CodeDom.Compiler.GeneratorSupport" /> object that indicates the type of code generation support to verify.</param>
		/// <returns>
		///   <see langword="true" /> if the specified code generation support is provided; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BB6 RID: 15286 RVA: 0x000F6699 File Offset: 0x000F4899
		public virtual bool Supports(GeneratorSupport generatorSupport)
		{
			return this.CreateGeneratorHelper().Supports(generatorSupport);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) expression and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> object that indicates the expression for which to generate code.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent.</param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BB7 RID: 15287 RVA: 0x000F66A7 File Offset: 0x000F48A7
		public virtual void GenerateCodeFromExpression(CodeExpression expression, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromExpression(expression, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) statement and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="statement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the CodeDOM elements for which to generate code.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent.</param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BB8 RID: 15288 RVA: 0x000F66B7 File Offset: 0x000F48B7
		public virtual void GenerateCodeFromStatement(CodeStatement statement, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromStatement(statement, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) namespace and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> object that indicates the namespace for which to generate code.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent.</param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BB9 RID: 15289 RVA: 0x000F66C7 File Offset: 0x000F48C7
		public virtual void GenerateCodeFromNamespace(CodeNamespace codeNamespace, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromNamespace(codeNamespace, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) compilation unit and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="compileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> for which to generate code.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which the output code is sent.</param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BBA RID: 15290 RVA: 0x000F66D7 File Offset: 0x000F48D7
		public virtual void GenerateCodeFromCompileUnit(CodeCompileUnit compileUnit, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromCompileUnit(compileUnit, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) type declaration and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="codeType">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object that indicates the type for which to generate code.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent.</param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BBB RID: 15291 RVA: 0x000F66E7 File Offset: 0x000F48E7
		public virtual void GenerateCodeFromType(CodeTypeDeclaration codeType, TextWriter writer, CodeGeneratorOptions options)
		{
			this.CreateGeneratorHelper().GenerateCodeFromType(codeType, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) member declaration and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="member">A <see cref="T:System.CodeDom.CodeTypeMember" /> object that indicates the member for which to generate code.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent.</param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.NotImplementedException">This method is not overridden in a derived class.</exception>
		// Token: 0x06003BBC RID: 15292 RVA: 0x000F66F7 File Offset: 0x000F48F7
		public virtual void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			throw new NotImplementedException(SR.GetString("NotSupported_CodeDomAPI"));
		}

		/// <summary>Compiles the code read from the specified text stream into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <param name="codeStream">A <see cref="T:System.IO.TextReader" /> object that is used to read the code to be parsed.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> that contains a representation of the parsed code.</returns>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06003BBD RID: 15293 RVA: 0x000F6708 File Offset: 0x000F4908
		public virtual CodeCompileUnit Parse(TextReader codeStream)
		{
			return this.CreateParserHelper().Parse(codeStream);
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x000F6718 File Offset: 0x000F4918
		private ICodeCompiler CreateCompilerHelper()
		{
			ICodeCompiler codeCompiler = this.CreateCompiler();
			if (codeCompiler == null)
			{
				throw new NotImplementedException(SR.GetString("NotSupported_CodeDomAPI"));
			}
			return codeCompiler;
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x000F6740 File Offset: 0x000F4940
		private ICodeGenerator CreateGeneratorHelper()
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw new NotImplementedException(SR.GetString("NotSupported_CodeDomAPI"));
			}
			return codeGenerator;
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x000F6768 File Offset: 0x000F4968
		private ICodeParser CreateParserHelper()
		{
			ICodeParser codeParser = this.CreateParser();
			if (codeParser == null)
			{
				throw new NotImplementedException(SR.GetString("NotSupported_CodeDomAPI"));
			}
			return codeParser;
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x000F6790 File Offset: 0x000F4990
		internal static bool TryGetProbableCoreAssemblyFilePath(CompilerParameters parameters, out string coreAssemblyFilePath)
		{
			string text = null;
			char[] array = new char[] { Path.DirectorySeparatorChar };
			string text2 = Path.Combine("Reference Assemblies", "Microsoft", "Framework");
			foreach (string text3 in parameters.ReferencedAssemblies)
			{
				if (Path.GetFileName(text3).Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase))
				{
					coreAssemblyFilePath = string.Empty;
					return false;
				}
				if (text3.IndexOf(text2, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					string[] array2 = text3.Split(array, StringSplitOptions.RemoveEmptyEntries);
					for (int i = 0; i < array2.Length - 5; i++)
					{
						if (string.Equals(array2[i], "Reference Assemblies", StringComparison.OrdinalIgnoreCase) && array2[i + 4].StartsWith("v", StringComparison.OrdinalIgnoreCase))
						{
							if (text != null)
							{
								if (!string.Equals(text, Path.GetDirectoryName(text3), StringComparison.OrdinalIgnoreCase))
								{
									coreAssemblyFilePath = string.Empty;
									return false;
								}
							}
							else
							{
								text = Path.GetDirectoryName(text3);
							}
						}
					}
				}
			}
			if (text != null)
			{
				coreAssemblyFilePath = Path.Combine(text, "mscorlib.dll");
				return true;
			}
			coreAssemblyFilePath = string.Empty;
			return false;
		}
	}
}
