using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Security.Permissions;

namespace Microsoft.VisualBasic
{
	/// <summary>Provides access to instances of the Visual Basic code generator and code compiler.</summary>
	// Token: 0x02000007 RID: 7
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class VBCodeProvider : CodeDomProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.VisualBasic.VBCodeProvider" /> class.</summary>
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public VBCodeProvider()
		{
			this.generator = new VBCodeGenerator();
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.VisualBasic.VBCodeProvider" /> class by using the specified provider options.</summary>
		/// <param name="providerOptions">A <see cref="T:System.Collections.Generic.IDictionary`2" /> object that contains the provider options from the configuration file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerOptions" /> is <see langword="null" />.</exception>
		// Token: 0x06000002 RID: 2 RVA: 0x00002063 File Offset: 0x00000263
		public VBCodeProvider(IDictionary<string, string> providerOptions)
		{
			if (providerOptions == null)
			{
				throw new ArgumentNullException("providerOptions");
			}
			this.generator = new VBCodeGenerator(providerOptions);
		}

		/// <summary>Gets the file name extension to use when creating source code files.</summary>
		/// <returns>The file name extension to use for generated source code files.</returns>
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002085 File Offset: 0x00000285
		public override string FileExtension
		{
			get
			{
				return "vb";
			}
		}

		/// <summary>Gets a language features identifier.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.LanguageOptions" /> that indicates special features of the language.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000208C File Offset: 0x0000028C
		public override LanguageOptions LanguageOptions
		{
			get
			{
				return LanguageOptions.CaseInsensitive;
			}
		}

		/// <summary>Gets an instance of the Visual Basic code generator.</summary>
		/// <returns>An instance of the Visual Basic <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> implementation.</returns>
		// Token: 0x06000005 RID: 5 RVA: 0x0000208F File Offset: 0x0000028F
		[Obsolete("Callers should not use the ICodeGenerator interface and should instead use the methods directly on the CodeDomProvider class.")]
		public override ICodeGenerator CreateGenerator()
		{
			return this.generator;
		}

		/// <summary>Gets an instance of the Visual Basic code compiler.</summary>
		/// <returns>An instance of the Visual Basic <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> implementation.</returns>
		// Token: 0x06000006 RID: 6 RVA: 0x00002097 File Offset: 0x00000297
		[Obsolete("Callers should not use the ICodeCompiler interface and should instead use the methods directly on the CodeDomProvider class.")]
		public override ICodeCompiler CreateCompiler()
		{
			return this.generator;
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type of object.</summary>
		/// <param name="type">The type of object to retrieve a type converter for.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type.</returns>
		// Token: 0x06000007 RID: 7 RVA: 0x0000209F File Offset: 0x0000029F
		public override TypeConverter GetConverter(Type type)
		{
			if (type == typeof(MemberAttributes))
			{
				return VBMemberAttributeConverter.Default;
			}
			if (type == typeof(TypeAttributes))
			{
				return VBTypeAttributeConverter.Default;
			}
			return base.GetConverter(type);
		}

		/// <summary>Generates code for the specified class member using the specified text writer and code generator options.</summary>
		/// <param name="member">A <see cref="T:System.CodeDom.CodeTypeMember" /> to generate code for.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to write to.</param>
		/// <param name="options">The <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> to use when generating the code.</param>
		// Token: 0x06000008 RID: 8 RVA: 0x000020D8 File Offset: 0x000002D8
		public override void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			this.generator.GenerateCodeFromMember(member, writer, options);
		}

		// Token: 0x04000059 RID: 89
		private VBCodeGenerator generator;
	}
}
