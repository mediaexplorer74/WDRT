using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a primitive data type value.</summary>
	// Token: 0x0200064B RID: 1611
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodePrimitiveExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class.</summary>
		// Token: 0x06003A8E RID: 14990 RVA: 0x000F3BD4 File Offset: 0x000F1DD4
		public CodePrimitiveExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class using the specified object.</summary>
		/// <param name="value">The object to represent.</param>
		// Token: 0x06003A8F RID: 14991 RVA: 0x000F3BDC File Offset: 0x000F1DDC
		public CodePrimitiveExpression(object value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the primitive data type to represent.</summary>
		/// <returns>The primitive data type instance to represent the value of.</returns>
		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06003A90 RID: 14992 RVA: 0x000F3BEB File Offset: 0x000F1DEB
		// (set) Token: 0x06003A91 RID: 14993 RVA: 0x000F3BF3 File Offset: 0x000F1DF3
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04002BF5 RID: 11253
		private object value;
	}
}
