using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a simple assignment statement.</summary>
	// Token: 0x02000618 RID: 1560
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeAssignStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAssignStatement" /> class.</summary>
		// Token: 0x06003901 RID: 14593 RVA: 0x000F1C97 File Offset: 0x000EFE97
		public CodeAssignStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAssignStatement" /> class using the specified expressions.</summary>
		/// <param name="left">The variable to assign to.</param>
		/// <param name="right">The value to assign.</param>
		// Token: 0x06003902 RID: 14594 RVA: 0x000F1C9F File Offset: 0x000EFE9F
		public CodeAssignStatement(CodeExpression left, CodeExpression right)
		{
			this.Left = left;
			this.Right = right;
		}

		/// <summary>Gets or sets the expression representing the object or reference to assign to.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object or reference to assign to.</returns>
		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x000F1CB5 File Offset: 0x000EFEB5
		// (set) Token: 0x06003904 RID: 14596 RVA: 0x000F1CBD File Offset: 0x000EFEBD
		public CodeExpression Left
		{
			get
			{
				return this.left;
			}
			set
			{
				this.left = value;
			}
		}

		/// <summary>Gets or sets the expression representing the object or reference to assign.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object or reference to assign.</returns>
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06003905 RID: 14597 RVA: 0x000F1CC6 File Offset: 0x000EFEC6
		// (set) Token: 0x06003906 RID: 14598 RVA: 0x000F1CCE File Offset: 0x000EFECE
		public CodeExpression Right
		{
			get
			{
				return this.right;
			}
			set
			{
				this.right = value;
			}
		}

		// Token: 0x04002B72 RID: 11122
		private CodeExpression left;

		// Token: 0x04002B73 RID: 11123
		private CodeExpression right;
	}
}
