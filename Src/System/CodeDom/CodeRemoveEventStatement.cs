using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement that removes an event handler.</summary>
	// Token: 0x02000650 RID: 1616
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeRemoveEventStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class.</summary>
		// Token: 0x06003A9F RID: 15007 RVA: 0x000F3CB6 File Offset: 0x000F1EB6
		public CodeRemoveEventStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class with the specified event and event handler.</summary>
		/// <param name="eventRef">A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to detach the event handler from.</param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove.</param>
		// Token: 0x06003AA0 RID: 15008 RVA: 0x000F3CBE File Offset: 0x000F1EBE
		public CodeRemoveEventStatement(CodeEventReferenceExpression eventRef, CodeExpression listener)
		{
			this.eventRef = eventRef;
			this.listener = listener;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class using the specified target object, event name, and event handler.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event.</param>
		/// <param name="eventName">The name of the event.</param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove.</param>
		// Token: 0x06003AA1 RID: 15009 RVA: 0x000F3CD4 File Offset: 0x000F1ED4
		public CodeRemoveEventStatement(CodeExpression targetObject, string eventName, CodeExpression listener)
		{
			this.eventRef = new CodeEventReferenceExpression(targetObject, eventName);
			this.listener = listener;
		}

		/// <summary>Gets or sets the event to remove a listener from.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to remove a listener from.</returns>
		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06003AA2 RID: 15010 RVA: 0x000F3CF0 File Offset: 0x000F1EF0
		// (set) Token: 0x06003AA3 RID: 15011 RVA: 0x000F3D0B File Offset: 0x000F1F0B
		public CodeEventReferenceExpression Event
		{
			get
			{
				if (this.eventRef == null)
				{
					this.eventRef = new CodeEventReferenceExpression();
				}
				return this.eventRef;
			}
			set
			{
				this.eventRef = value;
			}
		}

		/// <summary>Gets or sets the event handler to remove.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove.</returns>
		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x000F3D14 File Offset: 0x000F1F14
		// (set) Token: 0x06003AA5 RID: 15013 RVA: 0x000F3D1C File Offset: 0x000F1F1C
		public CodeExpression Listener
		{
			get
			{
				return this.listener;
			}
			set
			{
				this.listener = value;
			}
		}

		// Token: 0x04002BFF RID: 11263
		private CodeEventReferenceExpression eventRef;

		// Token: 0x04002C00 RID: 11264
		private CodeExpression listener;
	}
}
