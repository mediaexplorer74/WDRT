using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement that attaches an event-handler delegate to an event.</summary>
	// Token: 0x02000619 RID: 1561
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeAttachEventStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class.</summary>
		// Token: 0x06003907 RID: 14599 RVA: 0x000F1CD7 File Offset: 0x000EFED7
		public CodeAttachEventStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class using the specified event and delegate.</summary>
		/// <param name="eventRef">A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to attach an event handler to.</param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler.</param>
		// Token: 0x06003908 RID: 14600 RVA: 0x000F1CDF File Offset: 0x000EFEDF
		public CodeAttachEventStatement(CodeEventReferenceExpression eventRef, CodeExpression listener)
		{
			this.eventRef = eventRef;
			this.listener = listener;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class using the specified object containing the event, event name, and event-handler delegate.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event.</param>
		/// <param name="eventName">The name of the event to attach an event handler to.</param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler.</param>
		// Token: 0x06003909 RID: 14601 RVA: 0x000F1CF5 File Offset: 0x000EFEF5
		public CodeAttachEventStatement(CodeExpression targetObject, string eventName, CodeExpression listener)
		{
			this.eventRef = new CodeEventReferenceExpression(targetObject, eventName);
			this.listener = listener;
		}

		/// <summary>Gets or sets the event to attach an event-handler delegate to.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to attach an event handler to.</returns>
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x0600390A RID: 14602 RVA: 0x000F1D11 File Offset: 0x000EFF11
		// (set) Token: 0x0600390B RID: 14603 RVA: 0x000F1D27 File Offset: 0x000EFF27
		public CodeEventReferenceExpression Event
		{
			get
			{
				if (this.eventRef == null)
				{
					return new CodeEventReferenceExpression();
				}
				return this.eventRef;
			}
			set
			{
				this.eventRef = value;
			}
		}

		/// <summary>Gets or sets the new event-handler delegate to attach to the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler to attach.</returns>
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x0600390C RID: 14604 RVA: 0x000F1D30 File Offset: 0x000EFF30
		// (set) Token: 0x0600390D RID: 14605 RVA: 0x000F1D38 File Offset: 0x000EFF38
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

		// Token: 0x04002B74 RID: 11124
		private CodeEventReferenceExpression eventRef;

		// Token: 0x04002B75 RID: 11125
		private CodeExpression listener;
	}
}
