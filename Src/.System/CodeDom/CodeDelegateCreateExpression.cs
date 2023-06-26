using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates a delegate.</summary>
	// Token: 0x0200062C RID: 1580
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeDelegateCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateCreateExpression" /> class.</summary>
		// Token: 0x06003995 RID: 14741 RVA: 0x000F2715 File Offset: 0x000F0915
		public CodeDelegateCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateCreateExpression" /> class.</summary>
		/// <param name="delegateType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the delegate.</param>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object containing the event-handler method.</param>
		/// <param name="methodName">The name of the event-handler method.</param>
		// Token: 0x06003996 RID: 14742 RVA: 0x000F271D File Offset: 0x000F091D
		public CodeDelegateCreateExpression(CodeTypeReference delegateType, CodeExpression targetObject, string methodName)
		{
			this.delegateType = delegateType;
			this.targetObject = targetObject;
			this.methodName = methodName;
		}

		/// <summary>Gets or sets the data type of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the delegate.</returns>
		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06003997 RID: 14743 RVA: 0x000F273A File Offset: 0x000F093A
		// (set) Token: 0x06003998 RID: 14744 RVA: 0x000F275A File Offset: 0x000F095A
		public CodeTypeReference DelegateType
		{
			get
			{
				if (this.delegateType == null)
				{
					this.delegateType = new CodeTypeReference("");
				}
				return this.delegateType;
			}
			set
			{
				this.delegateType = value;
			}
		}

		/// <summary>Gets or sets the object that contains the event-handler method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object containing the event-handler method.</returns>
		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x000F2763 File Offset: 0x000F0963
		// (set) Token: 0x0600399A RID: 14746 RVA: 0x000F276B File Offset: 0x000F096B
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

		/// <summary>Gets or sets the name of the event handler method.</summary>
		/// <returns>The name of the event handler method.</returns>
		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x0600399B RID: 14747 RVA: 0x000F2774 File Offset: 0x000F0974
		// (set) Token: 0x0600399C RID: 14748 RVA: 0x000F278A File Offset: 0x000F098A
		public string MethodName
		{
			get
			{
				if (this.methodName != null)
				{
					return this.methodName;
				}
				return string.Empty;
			}
			set
			{
				this.methodName = value;
			}
		}

		// Token: 0x04002BA6 RID: 11174
		private CodeTypeReference delegateType;

		// Token: 0x04002BA7 RID: 11175
		private CodeExpression targetObject;

		// Token: 0x04002BA8 RID: 11176
		private string methodName;
	}
}
