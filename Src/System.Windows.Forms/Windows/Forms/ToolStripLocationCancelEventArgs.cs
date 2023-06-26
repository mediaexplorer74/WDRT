using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000408 RID: 1032
	internal class ToolStripLocationCancelEventArgs : CancelEventArgs
	{
		// Token: 0x06004764 RID: 18276 RVA: 0x0012BF6D File Offset: 0x0012A16D
		public ToolStripLocationCancelEventArgs(Point newLocation, bool value)
			: base(value)
		{
			this.newLocation = newLocation;
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06004765 RID: 18277 RVA: 0x0012BF7D File Offset: 0x0012A17D
		public Point NewLocation
		{
			get
			{
				return this.newLocation;
			}
		}

		// Token: 0x040026DE RID: 9950
		private Point newLocation;
	}
}
