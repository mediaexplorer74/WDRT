using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an argument used in a metadata attribute declaration.</summary>
	// Token: 0x0200061A RID: 1562
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeAttributeArgument
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class.</summary>
		// Token: 0x0600390E RID: 14606 RVA: 0x000F1D41 File Offset: 0x000EFF41
		public CodeAttributeArgument()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class using the specified value.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpression" /> that represents the value of the argument.</param>
		// Token: 0x0600390F RID: 14607 RVA: 0x000F1D49 File Offset: 0x000EFF49
		public CodeAttributeArgument(CodeExpression value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class using the specified name and value.</summary>
		/// <param name="name">The name of the attribute property the argument applies to.</param>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpression" /> that represents the value of the argument.</param>
		// Token: 0x06003910 RID: 14608 RVA: 0x000F1D58 File Offset: 0x000EFF58
		public CodeAttributeArgument(string name, CodeExpression value)
		{
			this.Name = name;
			this.Value = value;
		}

		/// <summary>Gets or sets the name of the attribute.</summary>
		/// <returns>The name of the attribute property the argument is for.</returns>
		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x000F1D6E File Offset: 0x000EFF6E
		// (set) Token: 0x06003912 RID: 14610 RVA: 0x000F1D84 File Offset: 0x000EFF84
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

		/// <summary>Gets or sets the value for the attribute argument.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the value for the attribute argument.</returns>
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06003913 RID: 14611 RVA: 0x000F1D8D File Offset: 0x000EFF8D
		// (set) Token: 0x06003914 RID: 14612 RVA: 0x000F1D95 File Offset: 0x000EFF95
		public CodeExpression Value
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

		// Token: 0x04002B76 RID: 11126
		private string name;

		// Token: 0x04002B77 RID: 11127
		private CodeExpression value;
	}
}
