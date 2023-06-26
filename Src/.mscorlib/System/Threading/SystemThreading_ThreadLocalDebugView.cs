using System;
using System.Collections.Generic;

namespace System.Threading
{
	// Token: 0x0200053F RID: 1343
	internal sealed class SystemThreading_ThreadLocalDebugView<T>
	{
		// Token: 0x06003F17 RID: 16151 RVA: 0x000EBEC3 File Offset: 0x000EA0C3
		public SystemThreading_ThreadLocalDebugView(ThreadLocal<T> tlocal)
		{
			this.m_tlocal = tlocal;
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x000EBED2 File Offset: 0x000EA0D2
		public bool IsValueCreated
		{
			get
			{
				return this.m_tlocal.IsValueCreated;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06003F19 RID: 16153 RVA: 0x000EBEDF File Offset: 0x000EA0DF
		public T Value
		{
			get
			{
				return this.m_tlocal.ValueForDebugDisplay;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003F1A RID: 16154 RVA: 0x000EBEEC File Offset: 0x000EA0EC
		public List<T> Values
		{
			get
			{
				return this.m_tlocal.ValuesForDebugDisplay;
			}
		}

		// Token: 0x04001A87 RID: 6791
		private readonly ThreadLocal<T> m_tlocal;
	}
}
