using System;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007B8 RID: 1976
	[Serializable]
	internal sealed class ChannelInfo : IChannelInfo
	{
		// Token: 0x060055B3 RID: 21939 RVA: 0x001317F6 File Offset: 0x0012F9F6
		[SecurityCritical]
		internal ChannelInfo()
		{
			this.ChannelData = ChannelServices.CurrentChannelData;
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x060055B4 RID: 21940 RVA: 0x00131809 File Offset: 0x0012FA09
		// (set) Token: 0x060055B5 RID: 21941 RVA: 0x00131811 File Offset: 0x0012FA11
		public object[] ChannelData
		{
			[SecurityCritical]
			get
			{
				return this.channelData;
			}
			[SecurityCritical]
			set
			{
				this.channelData = value;
			}
		}

		// Token: 0x04002770 RID: 10096
		private object[] channelData;
	}
}
