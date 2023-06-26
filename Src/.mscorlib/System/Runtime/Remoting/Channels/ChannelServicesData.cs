using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000829 RID: 2089
	internal class ChannelServicesData
	{
		// Token: 0x040028D6 RID: 10454
		internal long remoteCalls;

		// Token: 0x040028D7 RID: 10455
		internal CrossContextChannel xctxmessageSink;

		// Token: 0x040028D8 RID: 10456
		internal CrossAppDomainChannel xadmessageSink;

		// Token: 0x040028D9 RID: 10457
		internal bool fRegisterWellKnownChannels;
	}
}
