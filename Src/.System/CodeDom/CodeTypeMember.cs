using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	/// <summary>Provides a base class for a member of a type. Type members include fields, methods, properties, constructors and nested types.</summary>
	// Token: 0x0200065E RID: 1630
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeMember : CodeObject
	{
		/// <summary>Gets or sets the name of the member.</summary>
		/// <returns>The name of the member.</returns>
		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x000F4540 File Offset: 0x000F2740
		// (set) Token: 0x06003AFF RID: 15103 RVA: 0x000F4556 File Offset: 0x000F2756
		public string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the attributes of the member.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.CodeDom.MemberAttributes" /> values used to indicate the attributes of the member. The default value is <see cref="F:System.CodeDom.MemberAttributes.Private" /> | <see cref="F:System.CodeDom.MemberAttributes.Final" />.</returns>
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x000F455F File Offset: 0x000F275F
		// (set) Token: 0x06003B01 RID: 15105 RVA: 0x000F4567 File Offset: 0x000F2767
		public MemberAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
			set
			{
				this.attributes = value;
			}
		}

		/// <summary>Gets or sets the custom attributes of the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes of the member.</returns>
		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x000F4570 File Offset: 0x000F2770
		// (set) Token: 0x06003B03 RID: 15107 RVA: 0x000F458B File Offset: 0x000F278B
		public CodeAttributeDeclarationCollection CustomAttributes
		{
			get
			{
				if (this.customAttributes == null)
				{
					this.customAttributes = new CodeAttributeDeclarationCollection();
				}
				return this.customAttributes;
			}
			set
			{
				this.customAttributes = value;
			}
		}

		/// <summary>Gets or sets the line on which the type member statement occurs.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> object that indicates the location of the type member declaration.</returns>
		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06003B04 RID: 15108 RVA: 0x000F4594 File Offset: 0x000F2794
		// (set) Token: 0x06003B05 RID: 15109 RVA: 0x000F459C File Offset: 0x000F279C
		public CodeLinePragma LinePragma
		{
			get
			{
				return this.linePragma;
			}
			set
			{
				this.linePragma = value;
			}
		}

		/// <summary>Gets the collection of comments for the type member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that indicates the comments for the member.</returns>
		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06003B06 RID: 15110 RVA: 0x000F45A5 File Offset: 0x000F27A5
		public CodeCommentStatementCollection Comments
		{
			get
			{
				return this.comments;
			}
		}

		/// <summary>Gets the start directives for the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06003B07 RID: 15111 RVA: 0x000F45AD File Offset: 0x000F27AD
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

		/// <summary>Gets the end directives for the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06003B08 RID: 15112 RVA: 0x000F45C8 File Offset: 0x000F27C8
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

		// Token: 0x04002C1B RID: 11291
		private MemberAttributes attributes = (MemberAttributes)20482;

		// Token: 0x04002C1C RID: 11292
		private string name;

		// Token: 0x04002C1D RID: 11293
		private CodeCommentStatementCollection comments = new CodeCommentStatementCollection();

		// Token: 0x04002C1E RID: 11294
		private CodeAttributeDeclarationCollection customAttributes;

		// Token: 0x04002C1F RID: 11295
		private CodeLinePragma linePragma;

		// Token: 0x04002C20 RID: 11296
		[OptionalField]
		private CodeDirectiveCollection startDirectives;

		// Token: 0x04002C21 RID: 11297
		[OptionalField]
		private CodeDirectiveCollection endDirectives;
	}
}
