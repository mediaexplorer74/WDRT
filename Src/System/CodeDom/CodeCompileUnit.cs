using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	/// <summary>Provides a container for a CodeDOM program graph.</summary>
	// Token: 0x02000628 RID: 1576
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeCompileUnit : CodeObject
	{
		/// <summary>Gets the collection of namespaces.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceCollection" /> that indicates the namespaces that the compile unit uses.</returns>
		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06003982 RID: 14722 RVA: 0x000F257B File Offset: 0x000F077B
		public CodeNamespaceCollection Namespaces
		{
			get
			{
				return this.namespaces;
			}
		}

		/// <summary>Gets the referenced assemblies.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the file names of the referenced assemblies.</returns>
		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06003983 RID: 14723 RVA: 0x000F2583 File Offset: 0x000F0783
		public StringCollection ReferencedAssemblies
		{
			get
			{
				if (this.assemblies == null)
				{
					this.assemblies = new StringCollection();
				}
				return this.assemblies;
			}
		}

		/// <summary>Gets a collection of custom attributes for the generated assembly.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes for the generated assembly.</returns>
		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06003984 RID: 14724 RVA: 0x000F259E File Offset: 0x000F079E
		public CodeAttributeDeclarationCollection AssemblyCustomAttributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new CodeAttributeDeclarationCollection();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x000F25B9 File Offset: 0x000F07B9
		public CodeDirectiveCollection StartDirectives
		{
			get
			{
				if (this.startDirectives == null)
				{
					this.startDirectives = new CodeDirectiveCollection();
				}
				return this.startDirectives;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x000F25D4 File Offset: 0x000F07D4
		public CodeDirectiveCollection EndDirectives
		{
			get
			{
				if (this.endDirectives == null)
				{
					this.endDirectives = new CodeDirectiveCollection();
				}
				return this.endDirectives;
			}
		}

		// Token: 0x04002B9B RID: 11163
		private CodeNamespaceCollection namespaces = new CodeNamespaceCollection();

		// Token: 0x04002B9C RID: 11164
		private StringCollection assemblies;

		// Token: 0x04002B9D RID: 11165
		private CodeAttributeDeclarationCollection attributes;

		// Token: 0x04002B9E RID: 11166
		[OptionalField]
		private CodeDirectiveCollection startDirectives;

		// Token: 0x04002B9F RID: 11167
		[OptionalField]
		private CodeDirectiveCollection endDirectives;
	}
}
