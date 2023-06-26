using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement that throws an exception.</summary>
	// Token: 0x02000658 RID: 1624
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeThrowExceptionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeThrowExceptionStatement" /> class.</summary>
		// Token: 0x06003ACC RID: 15052 RVA: 0x000F3F84 File Offset: 0x000F2184
		public CodeThrowExceptionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeThrowExceptionStatement" /> class with the specified exception type instance.</summary>
		/// <param name="toThrow">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the exception to throw.</param>
		// Token: 0x06003ACD RID: 15053 RVA: 0x000F3F8C File Offset: 0x000F218C
		public CodeThrowExceptionStatement(CodeExpression toThrow)
		{
			this.ToThrow = toThrow;
		}

		/// <summary>Gets or sets the exception to throw.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> representing an instance of the exception to throw.</returns>
		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06003ACE RID: 15054 RVA: 0x000F3F9B File Offset: 0x000F219B
		// (set) Token: 0x06003ACF RID: 15055 RVA: 0x000F3FA3 File Offset: 0x000F21A3
		public CodeExpression ToThrow
		{
			get
			{
				return this.toThrow;
			}
			set
			{
				this.toThrow = value;
			}
		}

		// Token: 0x04002C09 RID: 11273
		private CodeExpression toThrow;
	}
}
