using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x020005A9 RID: 1449
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	[SecurityCritical]
	internal sealed class CleanupWorkList
	{
		// Token: 0x06004346 RID: 17222 RVA: 0x000FBACC File Offset: 0x000F9CCC
		public void Add(CleanupWorkListElement elem)
		{
			this.m_list.Add(elem);
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x000FBADC File Offset: 0x000F9CDC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Destroy()
		{
			for (int i = this.m_list.Count - 1; i >= 0; i--)
			{
				if (this.m_list[i].m_owned)
				{
					StubHelpers.SafeHandleRelease(this.m_list[i].m_handle);
				}
			}
		}

		// Token: 0x04001BF5 RID: 7157
		private List<CleanupWorkListElement> m_list = new List<CleanupWorkListElement>();
	}
}
