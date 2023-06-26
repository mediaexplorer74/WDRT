using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a field.</summary>
	// Token: 0x02000636 RID: 1590
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeFieldReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class.</summary>
		// Token: 0x060039D0 RID: 14800 RVA: 0x000F2AEC File Offset: 0x000F0CEC
		public CodeFieldReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class using the specified target object and field name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field.</param>
		/// <param name="fieldName">The name of the field.</param>
		// Token: 0x060039D1 RID: 14801 RVA: 0x000F2AF4 File Offset: 0x000F0CF4
		public CodeFieldReferenceExpression(CodeExpression targetObject, string fieldName)
		{
			this.TargetObject = targetObject;
			this.FieldName = fieldName;
		}

		/// <summary>Gets or sets the object that contains the field to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field to reference.</returns>
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060039D2 RID: 14802 RVA: 0x000F2B0A File Offset: 0x000F0D0A
		// (set) Token: 0x060039D3 RID: 14803 RVA: 0x000F2B12 File Offset: 0x000F0D12
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

		/// <summary>Gets or sets the name of the field to reference.</summary>
		/// <returns>A string containing the field name.</returns>
		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060039D4 RID: 14804 RVA: 0x000F2B1B File Offset: 0x000F0D1B
		// (set) Token: 0x060039D5 RID: 14805 RVA: 0x000F2B31 File Offset: 0x000F0D31
		public string FieldName
		{
			get
			{
				if (this.fieldName != null)
				{
					return this.fieldName;
				}
				return string.Empty;
			}
			set
			{
				this.fieldName = value;
			}
		}

		// Token: 0x04002BB0 RID: 11184
		private CodeExpression targetObject;

		// Token: 0x04002BB1 RID: 11185
		private string fieldName;
	}
}
