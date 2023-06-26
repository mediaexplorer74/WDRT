using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the value of a property.</summary>
	// Token: 0x0200064C RID: 1612
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodePropertyReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePropertyReferenceExpression" /> class.</summary>
		// Token: 0x06003A92 RID: 14994 RVA: 0x000F3BFC File Offset: 0x000F1DFC
		public CodePropertyReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePropertyReferenceExpression" /> class using the specified target object and property name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the property to reference.</param>
		/// <param name="propertyName">The name of the property to reference.</param>
		// Token: 0x06003A93 RID: 14995 RVA: 0x000F3C0F File Offset: 0x000F1E0F
		public CodePropertyReferenceExpression(CodeExpression targetObject, string propertyName)
		{
			this.TargetObject = targetObject;
			this.PropertyName = propertyName;
		}

		/// <summary>Gets or sets the object that contains the property to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the property to reference.</returns>
		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06003A94 RID: 14996 RVA: 0x000F3C30 File Offset: 0x000F1E30
		// (set) Token: 0x06003A95 RID: 14997 RVA: 0x000F3C38 File Offset: 0x000F1E38
		public CodeExpression TargetObject
		{
			get
			{
				return this.targetObject;
			}
			set
			{
				this.targetObject = value;
			}
		}

		/// <summary>Gets or sets the name of the property to reference.</summary>
		/// <returns>The name of the property to reference.</returns>
		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06003A96 RID: 14998 RVA: 0x000F3C41 File Offset: 0x000F1E41
		// (set) Token: 0x06003A97 RID: 14999 RVA: 0x000F3C57 File Offset: 0x000F1E57
		public string PropertyName
		{
			get
			{
				if (this.propertyName != null)
				{
					return this.propertyName;
				}
				return string.Empty;
			}
			set
			{
				this.propertyName = value;
			}
		}

		// Token: 0x04002BF6 RID: 11254
		private CodeExpression targetObject;

		// Token: 0x04002BF7 RID: 11255
		private string propertyName;

		// Token: 0x04002BF8 RID: 11256
		private CodeExpressionCollection parameters = new CodeExpressionCollection();
	}
}
