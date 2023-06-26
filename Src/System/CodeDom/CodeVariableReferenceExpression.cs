using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a local variable.</summary>
	// Token: 0x02000668 RID: 1640
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeVariableReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableReferenceExpression" /> class.</summary>
		// Token: 0x06003B6D RID: 15213 RVA: 0x000F5262 File Offset: 0x000F3462
		public CodeVariableReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableReferenceExpression" /> class using the specified local variable name.</summary>
		/// <param name="variableName">The name of the local variable to reference.</param>
		// Token: 0x06003B6E RID: 15214 RVA: 0x000F526A File Offset: 0x000F346A
		public CodeVariableReferenceExpression(string variableName)
		{
			this.variableName = variableName;
		}

		/// <summary>Gets or sets the name of the local variable to reference.</summary>
		/// <returns>The name of the local variable to reference.</returns>
		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x000F5279 File Offset: 0x000F3479
		// (set) Token: 0x06003B70 RID: 15216 RVA: 0x000F528F File Offset: 0x000F348F
		public string VariableName
		{
			get
			{
				if (this.variableName != null)
				{
					return this.variableName;
				}
				return string.Empty;
			}
			set
			{
				this.variableName = value;
			}
		}

		// Token: 0x04002C35 RID: 11317
		private string variableName;
	}
}
