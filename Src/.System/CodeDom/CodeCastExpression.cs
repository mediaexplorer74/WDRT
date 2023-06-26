using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression cast to a data type or interface.</summary>
	// Token: 0x02000621 RID: 1569
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeCastExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class.</summary>
		// Token: 0x06003941 RID: 14657 RVA: 0x000F20F0 File Offset: 0x000F02F0
		public CodeCastExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the destination type of the cast.</param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast.</param>
		// Token: 0x06003942 RID: 14658 RVA: 0x000F20F8 File Offset: 0x000F02F8
		public CodeCastExpression(CodeTypeReference targetType, CodeExpression expression)
		{
			this.TargetType = targetType;
			this.Expression = expression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">The name of the destination type of the cast.</param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast.</param>
		// Token: 0x06003943 RID: 14659 RVA: 0x000F210E File Offset: 0x000F030E
		public CodeCastExpression(string targetType, CodeExpression expression)
		{
			this.TargetType = new CodeTypeReference(targetType);
			this.Expression = expression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">The destination data type of the cast.</param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast.</param>
		// Token: 0x06003944 RID: 14660 RVA: 0x000F2129 File Offset: 0x000F0329
		public CodeCastExpression(Type targetType, CodeExpression expression)
		{
			this.TargetType = new CodeTypeReference(targetType);
			this.Expression = expression;
		}

		/// <summary>Gets or sets the destination type of the cast.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the destination type to cast to.</returns>
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06003945 RID: 14661 RVA: 0x000F2144 File Offset: 0x000F0344
		// (set) Token: 0x06003946 RID: 14662 RVA: 0x000F2164 File Offset: 0x000F0364
		public CodeTypeReference TargetType
		{
			get
			{
				if (this.targetType == null)
				{
					this.targetType = new CodeTypeReference("");
				}
				return this.targetType;
			}
			set
			{
				this.targetType = value;
			}
		}

		/// <summary>Gets or sets the expression to cast.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the code to cast.</returns>
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06003947 RID: 14663 RVA: 0x000F216D File Offset: 0x000F036D
		// (set) Token: 0x06003948 RID: 14664 RVA: 0x000F2175 File Offset: 0x000F0375
		public CodeExpression Expression
		{
			get
			{
				return this.expression;
			}
			set
			{
				this.expression = value;
			}
		}

		// Token: 0x04002B90 RID: 11152
		private CodeTypeReference targetType;

		// Token: 0x04002B91 RID: 11153
		private CodeExpression expression;
	}
}
