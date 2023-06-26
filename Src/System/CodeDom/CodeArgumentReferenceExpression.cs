using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the value of an argument passed to a method.</summary>
	// Token: 0x02000615 RID: 1557
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeArgumentReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> class.</summary>
		// Token: 0x060038E7 RID: 14567 RVA: 0x000F1A4B File Offset: 0x000EFC4B
		public CodeArgumentReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> class using the specified parameter name.</summary>
		/// <param name="parameterName">The name of the parameter to reference.</param>
		// Token: 0x060038E8 RID: 14568 RVA: 0x000F1A53 File Offset: 0x000EFC53
		public CodeArgumentReferenceExpression(string parameterName)
		{
			this.parameterName = parameterName;
		}

		/// <summary>Gets or sets the name of the parameter this expression references.</summary>
		/// <returns>The name of the parameter to reference.</returns>
		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x060038E9 RID: 14569 RVA: 0x000F1A62 File Offset: 0x000EFC62
		// (set) Token: 0x060038EA RID: 14570 RVA: 0x000F1A78 File Offset: 0x000EFC78
		public string ParameterName
		{
			get
			{
				if (this.parameterName != null)
				{
					return this.parameterName;
				}
				return string.Empty;
			}
			set
			{
				this.parameterName = value;
			}
		}

		// Token: 0x04002B6B RID: 11115
		private string parameterName;
	}
}
