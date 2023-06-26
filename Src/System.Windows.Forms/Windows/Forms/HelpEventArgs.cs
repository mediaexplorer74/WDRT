using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.HelpRequested" /> event.</summary>
	// Token: 0x02000270 RID: 624
	[ComVisible(true)]
	public class HelpEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HelpEventArgs" /> class.</summary>
		/// <param name="mousePos">The coordinates of the mouse pointer.</param>
		// Token: 0x06002808 RID: 10248 RVA: 0x000BA718 File Offset: 0x000B8918
		public HelpEventArgs(Point mousePos)
		{
			this.mousePos = mousePos;
		}

		/// <summary>Gets the screen coordinates of the mouse pointer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> representing the screen coordinates of the mouse pointer.</returns>
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x000BA727 File Offset: 0x000B8927
		public Point MousePos
		{
			get
			{
				return this.mousePos;
			}
		}

		/// <summary>Gets or sets a value indicating whether the help event was handled.</summary>
		/// <returns>
		///   <see langword="true" /> if the event is handled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x000BA72F File Offset: 0x000B892F
		// (set) Token: 0x0600280B RID: 10251 RVA: 0x000BA737 File Offset: 0x000B8937
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

		// Token: 0x0400107F RID: 4223
		private readonly Point mousePos;

		// Token: 0x04001080 RID: 4224
		private bool handled;
	}
}
