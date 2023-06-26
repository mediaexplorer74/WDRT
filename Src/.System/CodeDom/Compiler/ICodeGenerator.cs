using System;
using System.IO;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines an interface for generating code.</summary>
	// Token: 0x0200067E RID: 1662
	public interface ICodeGenerator
	{
		/// <summary>Gets a value that indicates whether the specified value is a valid identifier for the current language.</summary>
		/// <param name="value">The value to test for being a valid identifier.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is a valid identifier; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003D28 RID: 15656
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		bool IsValidIdentifier(string value);

		/// <summary>Throws an exception if the specified value is not a valid identifier.</summary>
		/// <param name="value">The identifier to validate.</param>
		/// <exception cref="T:System.ArgumentException">The identifier is not valid.</exception>
		// Token: 0x06003D29 RID: 15657
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		void ValidateIdentifier(string value);

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <param name="value">The string to create an escaped identifier for.</param>
		/// <returns>The escaped identifier for the value.</returns>
		// Token: 0x06003D2A RID: 15658
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		string CreateEscapedIdentifier(string value);

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <param name="value">The string to generate a valid identifier for.</param>
		/// <returns>A valid identifier for the specified value.</returns>
		// Token: 0x06003D2B RID: 15659
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		string CreateValidIdentifier(string value);

		/// <summary>Gets the type indicated by the specified <see cref="T:System.CodeDom.CodeTypeReference" />.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type to return.</param>
		/// <returns>A text representation of the specified type for the language this code generator is designed to generate code in. For example, in Visual Basic, passing in type System.Int32 will return "Integer".</returns>
		// Token: 0x06003D2C RID: 15660
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		string GetTypeOutput(CodeTypeReference type);

		/// <summary>Gets a value indicating whether the generator provides support for the language features represented by the specified <see cref="T:System.CodeDom.Compiler.GeneratorSupport" /> object.</summary>
		/// <param name="supports">The capabilities to test the generator for.</param>
		/// <returns>
		///   <see langword="true" /> if the specified capabilities are supported; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003D2D RID: 15661
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		bool Supports(GeneratorSupport supports);

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) expression and outputs it to the specified text writer.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to generate code for.</param>
		/// <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to.</param>
		/// <param name="o">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		// Token: 0x06003D2E RID: 15662
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		void GenerateCodeFromExpression(CodeExpression e, TextWriter w, CodeGeneratorOptions o);

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) statement and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeStatement" /> containing the CodeDOM elements to translate.</param>
		/// <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to.</param>
		/// <param name="o">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		// Token: 0x06003D2F RID: 15663
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		void GenerateCodeFromStatement(CodeStatement e, TextWriter w, CodeGeneratorOptions o);

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) namespace and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeNamespace" /> that indicates the namespace to generate code for.</param>
		/// <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to.</param>
		/// <param name="o">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		// Token: 0x06003D30 RID: 15664
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		void GenerateCodeFromNamespace(CodeNamespace e, TextWriter w, CodeGeneratorOptions o);

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) compilation unit and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> to generate code for.</param>
		/// <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to.</param>
		/// <param name="o">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		// Token: 0x06003D31 RID: 15665
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		void GenerateCodeFromCompileUnit(CodeCompileUnit e, TextWriter w, CodeGeneratorOptions o);

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) type declaration and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that indicates the type to generate code for.</param>
		/// <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to.</param>
		/// <param name="o">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code.</param>
		// Token: 0x06003D32 RID: 15666
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		void GenerateCodeFromType(CodeTypeDeclaration e, TextWriter w, CodeGeneratorOptions o);
	}
}
