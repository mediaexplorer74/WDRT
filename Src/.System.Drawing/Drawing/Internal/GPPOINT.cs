﻿using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Internal
{
	// Token: 0x020000E5 RID: 229
	[StructLayout(LayoutKind.Sequential)]
	internal class GPPOINT
	{
		// Token: 0x06000C2F RID: 3119 RVA: 0x00003800 File Offset: 0x00001A00
		internal GPPOINT()
		{
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002B516 File Offset: 0x00029716
		internal GPPOINT(PointF pt)
		{
			this.X = (int)pt.X;
			this.Y = (int)pt.Y;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0002B53A File Offset: 0x0002973A
		internal GPPOINT(Point pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002B55C File Offset: 0x0002975C
		internal PointF ToPoint()
		{
			return new PointF((float)this.X, (float)this.Y);
		}

		// Token: 0x04000ABC RID: 2748
		internal int X;

		// Token: 0x04000ABD RID: 2749
		internal int Y;
	}
}
