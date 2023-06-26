using System;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000839 RID: 2105
	internal class DispatchChannelSinkProvider : IServerChannelSinkProvider
	{
		// Token: 0x06005A13 RID: 23059 RVA: 0x0013EC4E File Offset: 0x0013CE4E
		internal DispatchChannelSinkProvider()
		{
		}

		// Token: 0x06005A14 RID: 23060 RVA: 0x0013EC56 File Offset: 0x0013CE56
		[SecurityCritical]
		public void GetChannelData(IChannelDataStore channelData)
		{
		}

		// Token: 0x06005A15 RID: 23061 RVA: 0x0013EC58 File Offset: 0x0013CE58
		[SecurityCritical]
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			return new DispatchChannelSink();
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06005A16 RID: 23062 RVA: 0x0013EC5F File Offset: 0x0013CE5F
		// (set) Token: 0x06005A17 RID: 23063 RVA: 0x0013EC62 File Offset: 0x0013CE62
		public IServerChannelSinkProvider Next
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
