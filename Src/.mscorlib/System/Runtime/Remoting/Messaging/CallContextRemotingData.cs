using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200088E RID: 2190
	[Serializable]
	internal class CallContextRemotingData : ICloneable
	{
		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06005CF8 RID: 23800 RVA: 0x001470D8 File Offset: 0x001452D8
		// (set) Token: 0x06005CF9 RID: 23801 RVA: 0x001470E0 File Offset: 0x001452E0
		internal string LogicalCallID
		{
			get
			{
				return this._logicalCallID;
			}
			set
			{
				this._logicalCallID = value;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06005CFA RID: 23802 RVA: 0x001470E9 File Offset: 0x001452E9
		internal bool HasInfo
		{
			get
			{
				return this._logicalCallID != null;
			}
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x001470F4 File Offset: 0x001452F4
		public object Clone()
		{
			return new CallContextRemotingData
			{
				LogicalCallID = this.LogicalCallID
			};
		}

		// Token: 0x040029EB RID: 10731
		private string _logicalCallID;
	}
}
