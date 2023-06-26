using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000835 RID: 2101
	[Serializable]
	internal class CrossAppDomainData
	{
		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x060059F8 RID: 23032 RVA: 0x0013E4B2 File Offset: 0x0013C6B2
		internal virtual IntPtr ContextID
		{
			get
			{
				return new IntPtr((long)this._ContextID);
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x060059F9 RID: 23033 RVA: 0x0013E4C4 File Offset: 0x0013C6C4
		internal virtual int DomainID
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._DomainID;
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x060059FA RID: 23034 RVA: 0x0013E4CC File Offset: 0x0013C6CC
		internal virtual string ProcessGuid
		{
			get
			{
				return this._processGuid;
			}
		}

		// Token: 0x060059FB RID: 23035 RVA: 0x0013E4D4 File Offset: 0x0013C6D4
		internal CrossAppDomainData(IntPtr ctxId, int domainID, string processGuid)
		{
			this._DomainID = domainID;
			this._processGuid = processGuid;
			this._ContextID = ctxId.ToInt64();
		}

		// Token: 0x060059FC RID: 23036 RVA: 0x0013E508 File Offset: 0x0013C708
		internal bool IsFromThisProcess()
		{
			return Identity.ProcessGuid.Equals(this._processGuid);
		}

		// Token: 0x060059FD RID: 23037 RVA: 0x0013E51A File Offset: 0x0013C71A
		[SecurityCritical]
		internal bool IsFromThisAppDomain()
		{
			return this.IsFromThisProcess() && Thread.GetDomain().GetId() == this._DomainID;
		}

		// Token: 0x040028F1 RID: 10481
		private object _ContextID = 0;

		// Token: 0x040028F2 RID: 10482
		private int _DomainID;

		// Token: 0x040028F3 RID: 10483
		private string _processGuid;
	}
}
