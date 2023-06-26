using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides data for the <see cref="E:System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged" /> event.</summary>
	// Token: 0x020002DB RID: 731
	public class NetworkAvailabilityEventArgs : EventArgs
	{
		// Token: 0x060019CC RID: 6604 RVA: 0x0007E02B File Offset: 0x0007C22B
		internal NetworkAvailabilityEventArgs(bool isAvailable)
		{
			this.isAvailable = isAvailable;
		}

		/// <summary>Gets the current status of the network connection.</summary>
		/// <returns>
		///   <see langword="true" /> if the network is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x0007E03A File Offset: 0x0007C23A
		public bool IsAvailable
		{
			get
			{
				return this.isAvailable;
			}
		}

		// Token: 0x04001A38 RID: 6712
		private bool isAvailable;
	}
}
