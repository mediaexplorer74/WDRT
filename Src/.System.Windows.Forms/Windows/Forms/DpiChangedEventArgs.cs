using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the DPIChanged events of a form or control.</summary>
	// Token: 0x02000232 RID: 562
	public sealed class DpiChangedEventArgs : CancelEventArgs
	{
		// Token: 0x06002491 RID: 9361 RVA: 0x000AC820 File Offset: 0x000AAA20
		internal DpiChangedEventArgs(int old, Message m)
		{
			this.DeviceDpiOld = old;
			this.DeviceDpiNew = NativeMethods.Util.SignedLOWORD(m.WParam);
			NativeMethods.RECT rect = (NativeMethods.RECT)UnsafeNativeMethods.PtrToStructure(m.LParam, typeof(NativeMethods.RECT));
			this.SuggestedRectangle = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		/// <summary>Gets the DPI value for the display device where the control or form was previously displayed.</summary>
		/// <returns>A DPI value.</returns>
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x000AC88B File Offset: 0x000AAA8B
		// (set) Token: 0x06002493 RID: 9363 RVA: 0x000AC893 File Offset: 0x000AAA93
		public int DeviceDpiOld { get; private set; }

		/// <summary>Gets the DPI value for the new display device where the control or form is currently being displayed.</summary>
		/// <returns>The DPI value.</returns>
		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x000AC89C File Offset: 0x000AAA9C
		// (set) Token: 0x06002495 RID: 9365 RVA: 0x000AC8A4 File Offset: 0x000AAAA4
		public int DeviceDpiNew { get; private set; }

		/// <summary>Gets a <see cref="T:System.Drawing.Rectangle" /> that represents the new bounding rectangle for the control or form based on the DPI of the display device where it's displayed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" />.</returns>
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x000AC8AD File Offset: 0x000AAAAD
		// (set) Token: 0x06002497 RID: 9367 RVA: 0x000AC8B5 File Offset: 0x000AAAB5
		public Rectangle SuggestedRectangle { get; private set; }

		/// <summary>Creates and returns a string representation of the current <see cref="T:System.Windows.Forms.DpiChangedEventArgs" />.</summary>
		/// <returns>A string.</returns>
		// Token: 0x06002498 RID: 9368 RVA: 0x000AC8BE File Offset: 0x000AAABE
		public override string ToString()
		{
			return string.Format("was: {0}, now: {1}", this.DeviceDpiOld, this.DeviceDpiNew);
		}
	}
}
