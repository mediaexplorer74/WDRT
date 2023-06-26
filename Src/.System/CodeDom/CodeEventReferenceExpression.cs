using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an event.</summary>
	// Token: 0x02000632 RID: 1586
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeEventReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> class.</summary>
		// Token: 0x060039B8 RID: 14776 RVA: 0x000F2960 File Offset: 0x000F0B60
		public CodeEventReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> class using the specified target object and event name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event.</param>
		/// <param name="eventName">The name of the event to reference.</param>
		// Token: 0x060039B9 RID: 14777 RVA: 0x000F2968 File Offset: 0x000F0B68
		public CodeEventReferenceExpression(CodeExpression targetObject, string eventName)
		{
			this.targetObject = targetObject;
			this.eventName = eventName;
		}

		/// <summary>Gets or sets the object that contains the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event.</returns>
		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060039BA RID: 14778 RVA: 0x000F297E File Offset: 0x000F0B7E
		// (set) Token: 0x060039BB RID: 14779 RVA: 0x000F2986 File Offset: 0x000F0B86
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

		/// <summary>Gets or sets the name of the event.</summary>
		/// <returns>The name of the event.</returns>
		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060039BC RID: 14780 RVA: 0x000F298F File Offset: 0x000F0B8F
		// (set) Token: 0x060039BD RID: 14781 RVA: 0x000F29A5 File Offset: 0x000F0BA5
		public string EventName
		{
			get
			{
				if (this.eventName != null)
				{
					return this.eventName;
				}
				return string.Empty;
			}
			set
			{
				this.eventName = value;
			}
		}

		// Token: 0x04002BAD RID: 11181
		private CodeExpression targetObject;

		// Token: 0x04002BAE RID: 11182
		private string eventName;
	}
}
