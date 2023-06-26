using System;
using System.Threading;

namespace System.Management
{
	// Token: 0x02000026 RID: 38
	internal class WmiEventState
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00007EE4 File Offset: 0x000060E4
		internal WmiEventState(Delegate d, ManagementEventArgs args, AutoResetEvent h)
		{
			this.d = d;
			this.args = args;
			this.h = h;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00007F01 File Offset: 0x00006101
		public Delegate Delegate
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00007F09 File Offset: 0x00006109
		public ManagementEventArgs Args
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00007F11 File Offset: 0x00006111
		public AutoResetEvent AutoResetEvent
		{
			get
			{
				return this.h;
			}
		}

		// Token: 0x0400011D RID: 285
		private Delegate d;

		// Token: 0x0400011E RID: 286
		private ManagementEventArgs args;

		// Token: 0x0400011F RID: 287
		private AutoResetEvent h;
	}
}
