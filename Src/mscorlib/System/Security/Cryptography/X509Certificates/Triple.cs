using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002CE RID: 718
	internal struct Triple<T1, T2, T3>
	{
		// Token: 0x0600257D RID: 9597 RVA: 0x00089FE4 File Offset: 0x000881E4
		internal Triple(T1 first, T2 second, T3 third)
		{
			this._first = first;
			this._second = second;
			this._third = third;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x00089FFB File Offset: 0x000881FB
		public T1 Item1
		{
			get
			{
				return this._first;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x0008A003 File Offset: 0x00088203
		public T2 Item2
		{
			get
			{
				return this._second;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x0008A00B File Offset: 0x0008820B
		public T3 Item3
		{
			get
			{
				return this._third;
			}
		}

		// Token: 0x04000E23 RID: 3619
		private readonly T1 _first;

		// Token: 0x04000E24 RID: 3620
		private readonly T2 _second;

		// Token: 0x04000E25 RID: 3621
		private readonly T3 _third;
	}
}
