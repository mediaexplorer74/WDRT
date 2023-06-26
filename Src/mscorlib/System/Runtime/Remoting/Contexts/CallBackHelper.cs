using System;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000808 RID: 2056
	[Serializable]
	internal class CallBackHelper
	{
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x060058CE RID: 22734 RVA: 0x0013A531 File Offset: 0x00138731
		// (set) Token: 0x060058CF RID: 22735 RVA: 0x0013A53E File Offset: 0x0013873E
		internal bool IsEERequested
		{
			get
			{
				return (this._flags & 1) == 1;
			}
			set
			{
				if (value)
				{
					this._flags |= 1;
				}
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (set) Token: 0x060058D0 RID: 22736 RVA: 0x0013A551 File Offset: 0x00138751
		internal bool IsCrossDomain
		{
			set
			{
				if (value)
				{
					this._flags |= 256;
				}
			}
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x0013A568 File Offset: 0x00138768
		internal CallBackHelper(IntPtr privateData, bool bFromEE, int targetDomainID)
		{
			this.IsEERequested = bFromEE;
			this.IsCrossDomain = targetDomainID != 0;
			this._privateData = privateData;
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x0013A588 File Offset: 0x00138788
		[SecurityCritical]
		internal void Func()
		{
			if (this.IsEERequested)
			{
				Context.ExecuteCallBackInEE(this._privateData);
			}
		}

		// Token: 0x04002876 RID: 10358
		internal const int RequestedFromEE = 1;

		// Token: 0x04002877 RID: 10359
		internal const int XDomainTransition = 256;

		// Token: 0x04002878 RID: 10360
		private int _flags;

		// Token: 0x04002879 RID: 10361
		private IntPtr _privateData;
	}
}
