using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataColumnCollection.CollectionChanged" /> event.</summary>
	// Token: 0x02000525 RID: 1317
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class CollectionChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values that specifies how the collection changed.</param>
		/// <param name="element">An <see cref="T:System.Object" /> that specifies the instance of the collection where the change occurred.</param>
		// Token: 0x060031E3 RID: 12771 RVA: 0x000DFFB4 File Offset: 0x000DE1B4
		public CollectionChangeEventArgs(CollectionChangeAction action, object element)
		{
			this.action = action;
			this.element = element;
		}

		/// <summary>Gets an action that specifies how the collection changed.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values.</returns>
		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x000DFFCA File Offset: 0x000DE1CA
		public virtual CollectionChangeAction Action
		{
			get
			{
				return this.action;
			}
		}

		/// <summary>Gets the instance of the collection with the change.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the instance of the collection with the change, or <see langword="null" /> if you refresh the collection.</returns>
		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x000DFFD2 File Offset: 0x000DE1D2
		public virtual object Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x0400293F RID: 10559
		private CollectionChangeAction action;

		// Token: 0x04002940 RID: 10560
		private object element;
	}
}
