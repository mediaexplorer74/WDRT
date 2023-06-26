using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
	// Token: 0x02000595 RID: 1429
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ProgressChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProgressChangedEventArgs" /> class.</summary>
		/// <param name="progressPercentage">The percentage of an asynchronous task that has been completed.</param>
		/// <param name="userState">A unique user state.</param>
		// Token: 0x06003510 RID: 13584 RVA: 0x000E73F8 File Offset: 0x000E55F8
		[global::__DynamicallyInvokable]
		public ProgressChangedEventArgs(int progressPercentage, object userState)
		{
			this.progressPercentage = progressPercentage;
			this.userState = userState;
		}

		/// <summary>Gets the asynchronous task progress percentage.</summary>
		/// <returns>A percentage value indicating the asynchronous task progress.</returns>
		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x000E740E File Offset: 0x000E560E
		[SRDescription("Async_ProgressChangedEventArgs_ProgressPercentage")]
		[global::__DynamicallyInvokable]
		public int ProgressPercentage
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.progressPercentage;
			}
		}

		/// <summary>Gets a unique user state.</summary>
		/// <returns>A unique <see cref="T:System.Object" /> indicating the user state.</returns>
		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x000E7416 File Offset: 0x000E5616
		[SRDescription("Async_ProgressChangedEventArgs_UserState")]
		[global::__DynamicallyInvokable]
		public object UserState
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.userState;
			}
		}

		// Token: 0x04002A25 RID: 10789
		private readonly int progressPercentage;

		// Token: 0x04002A26 RID: 10790
		private readonly object userState;
	}
}
