using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a field of a type.</summary>
	// Token: 0x0200063D RID: 1597
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMemberField : CodeTypeMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class.</summary>
		// Token: 0x060039FB RID: 14843 RVA: 0x000F2D5A File Offset: 0x000F0F5A
		public CodeMemberField()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">An object that indicates the type of the field.</param>
		/// <param name="name">The name of the field.</param>
		// Token: 0x060039FC RID: 14844 RVA: 0x000F2D62 File Offset: 0x000F0F62
		public CodeMemberField(CodeTypeReference type, string name)
		{
			this.Type = type;
			base.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">The type of the field.</param>
		/// <param name="name">The name of the field.</param>
		// Token: 0x060039FD RID: 14845 RVA: 0x000F2D78 File Offset: 0x000F0F78
		public CodeMemberField(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">The type of the field.</param>
		/// <param name="name">The name of the field.</param>
		// Token: 0x060039FE RID: 14846 RVA: 0x000F2D93 File Offset: 0x000F0F93
		public CodeMemberField(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		/// <summary>Gets or sets the type of the field.</summary>
		/// <returns>The type of the field.</returns>
		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x060039FF RID: 14847 RVA: 0x000F2DAE File Offset: 0x000F0FAE
		// (set) Token: 0x06003A00 RID: 14848 RVA: 0x000F2DCE File Offset: 0x000F0FCE
		public CodeTypeReference Type
		{
			get
			{
				if (this.type == null)
				{
					this.type = new CodeTypeReference("");
				}
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets the initialization expression for the field.</summary>
		/// <returns>The initialization expression for the field.</returns>
		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06003A01 RID: 14849 RVA: 0x000F2DD7 File Offset: 0x000F0FD7
		// (set) Token: 0x06003A02 RID: 14850 RVA: 0x000F2DDF File Offset: 0x000F0FDF
		public CodeExpression InitExpression
		{
			get
			{
				return this.initExpression;
			}
			set
			{
				this.initExpression = value;
			}
		}

		// Token: 0x04002BC0 RID: 11200
		private CodeTypeReference type;

		// Token: 0x04002BC1 RID: 11201
		private CodeExpression initExpression;
	}
}
