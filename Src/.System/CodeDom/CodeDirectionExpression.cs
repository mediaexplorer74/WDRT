using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression used as a method invoke parameter along with a reference direction indicator.</summary>
	// Token: 0x0200062E RID: 1582
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeDirectionExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectionExpression" /> class.</summary>
		// Token: 0x060039A3 RID: 14755 RVA: 0x000F27FF File Offset: 0x000F09FF
		public CodeDirectionExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectionExpression" /> class using the specified field direction and expression.</summary>
		/// <param name="direction">A <see cref="T:System.CodeDom.FieldDirection" /> that indicates the field direction of the expression.</param>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the code expression to represent.</param>
		// Token: 0x060039A4 RID: 14756 RVA: 0x000F2807 File Offset: 0x000F0A07
		public CodeDirectionExpression(FieldDirection direction, CodeExpression expression)
		{
			this.expression = expression;
			this.direction = direction;
		}

		/// <summary>Gets or sets the code expression to represent.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to represent.</returns>
		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060039A5 RID: 14757 RVA: 0x000F281D File Offset: 0x000F0A1D
		// (set) Token: 0x060039A6 RID: 14758 RVA: 0x000F2825 File Offset: 0x000F0A25
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

		/// <summary>Gets or sets the field direction for this direction expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.FieldDirection" /> that indicates the field direction for this direction expression.</returns>
		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060039A7 RID: 14759 RVA: 0x000F282E File Offset: 0x000F0A2E
		// (set) Token: 0x060039A8 RID: 14760 RVA: 0x000F2836 File Offset: 0x000F0A36
		public FieldDirection Direction
		{
			get
			{
				return this.direction;
			}
			set
			{
				this.direction = value;
			}
		}

		// Token: 0x04002BAB RID: 11179
		private CodeExpression expression;

		// Token: 0x04002BAC RID: 11180
		private FieldDirection direction;
	}
}
