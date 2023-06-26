using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for events that can be handled completely in an event handler.</summary>
	// Token: 0x02000555 RID: 1365
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class HandledEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.HandledEventArgs" /> class with a default <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property value of <see langword="false" />.</summary>
		// Token: 0x06003348 RID: 13128 RVA: 0x000E3772 File Offset: 0x000E1972
		public HandledEventArgs()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.HandledEventArgs" /> class with the specified default value for the <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property.</summary>
		/// <param name="defaultHandledValue">The default value for the <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property.</param>
		// Token: 0x06003349 RID: 13129 RVA: 0x000E377B File Offset: 0x000E197B
		public HandledEventArgs(bool defaultHandledValue)
		{
			this.handled = defaultHandledValue;
		}

		/// <summary>Gets or sets a value that indicates whether the event handler has completely handled the event or whether the system should continue its own processing.</summary>
		/// <returns>
		///   <see langword="true" /> if the event has been completely handled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x000E378A File Offset: 0x000E198A
		// (set) Token: 0x0600334B RID: 13131 RVA: 0x000E3792 File Offset: 0x000E1992
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		// Token: 0x040029A5 RID: 10661
		private bool handled;
	}
}
