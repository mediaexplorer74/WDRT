using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGrid.Navigate" /> event.</summary>
	// Token: 0x02000306 RID: 774
	[ComVisible(true)]
	public class NavigateEventArgs : EventArgs
	{
		/// <summary>Gets a value indicating whether to navigate in a forward direction.</summary>
		/// <returns>
		///   <see langword="true" /> if the navigation is in a forward direction; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x000DF5F4 File Offset: 0x000DD7F4
		public bool Forward
		{
			get
			{
				return this.isForward;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NavigateEventArgs" /> class.</summary>
		/// <param name="isForward">
		///   <see langword="true" /> to navigate in a forward direction; otherwise, <see langword="false" />.</param>
		// Token: 0x06003164 RID: 12644 RVA: 0x000DF5FC File Offset: 0x000DD7FC
		public NavigateEventArgs(bool isForward)
		{
			this.isForward = isForward;
		}

		// Token: 0x04001E1E RID: 7710
		private bool isForward = true;
	}
}
