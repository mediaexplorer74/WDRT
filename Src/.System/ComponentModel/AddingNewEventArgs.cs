using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event.</summary>
	// Token: 0x0200050B RID: 1291
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class AddingNewEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AddingNewEventArgs" /> class using no parameters.</summary>
		// Token: 0x060030F5 RID: 12533 RVA: 0x000DE36E File Offset: 0x000DC56E
		public AddingNewEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AddingNewEventArgs" /> class using the specified object as the new item.</summary>
		/// <param name="newObject">An <see cref="T:System.Object" /> to use as the new item value.</param>
		// Token: 0x060030F6 RID: 12534 RVA: 0x000DE376 File Offset: 0x000DC576
		public AddingNewEventArgs(object newObject)
		{
			this.newObject = newObject;
		}

		/// <summary>Gets or sets the object to be added to the binding list.</summary>
		/// <returns>The <see cref="T:System.Object" /> to be added as a new item to the associated collection.</returns>
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x060030F7 RID: 12535 RVA: 0x000DE385 File Offset: 0x000DC585
		// (set) Token: 0x060030F8 RID: 12536 RVA: 0x000DE38D File Offset: 0x000DC58D
		public object NewObject
		{
			get
			{
				return this.newObject;
			}
			set
			{
				this.newObject = value;
			}
		}

		// Token: 0x040028ED RID: 10477
		private object newObject;
	}
}
