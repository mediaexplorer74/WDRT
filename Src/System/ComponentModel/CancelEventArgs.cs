using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for a cancelable event.</summary>
	// Token: 0x02000520 RID: 1312
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class CancelEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to <see langword="false" />.</summary>
		// Token: 0x060031C2 RID: 12738 RVA: 0x000DFC09 File Offset: 0x000DDE09
		[global::__DynamicallyInvokable]
		public CancelEventArgs()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to the given value.</summary>
		/// <param name="cancel">
		///   <see langword="true" /> to cancel the event; otherwise, <see langword="false" />.</param>
		// Token: 0x060031C3 RID: 12739 RVA: 0x000DFC12 File Offset: 0x000DDE12
		[global::__DynamicallyInvokable]
		public CancelEventArgs(bool cancel)
		{
			this.cancel = cancel;
		}

		/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
		/// <returns>
		///   <see langword="true" /> if the event should be canceled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000DFC21 File Offset: 0x000DDE21
		// (set) Token: 0x060031C5 RID: 12741 RVA: 0x000DFC29 File Offset: 0x000DDE29
		[global::__DynamicallyInvokable]
		public bool Cancel
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.cancel;
			}
			[global::__DynamicallyInvokable]
			set
			{
				this.cancel = value;
			}
		}

		// Token: 0x0400292A RID: 10538
		private bool cancel;
	}
}
