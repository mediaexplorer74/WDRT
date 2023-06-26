using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007B9 RID: 1977
	[Serializable]
	internal sealed class EnvoyInfo : IEnvoyInfo
	{
		// Token: 0x060055B6 RID: 21942 RVA: 0x0013181C File Offset: 0x0012FA1C
		[SecurityCritical]
		internal static IEnvoyInfo CreateEnvoyInfo(ServerIdentity serverID)
		{
			IEnvoyInfo envoyInfo = null;
			if (serverID != null)
			{
				if (serverID.EnvoyChain == null)
				{
					serverID.RaceSetEnvoyChain(serverID.ServerContext.CreateEnvoyChain(serverID.TPOrObject));
				}
				if (!(serverID.EnvoyChain is EnvoyTerminatorSink))
				{
					envoyInfo = new EnvoyInfo(serverID.EnvoyChain);
				}
			}
			return envoyInfo;
		}

		// Token: 0x060055B7 RID: 21943 RVA: 0x0013186A File Offset: 0x0012FA6A
		[SecurityCritical]
		private EnvoyInfo(IMessageSink sinks)
		{
			this.EnvoySinks = sinks;
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x060055B8 RID: 21944 RVA: 0x00131879 File Offset: 0x0012FA79
		// (set) Token: 0x060055B9 RID: 21945 RVA: 0x00131881 File Offset: 0x0012FA81
		public IMessageSink EnvoySinks
		{
			[SecurityCritical]
			get
			{
				return this.envoySinks;
			}
			[SecurityCritical]
			set
			{
				this.envoySinks = value;
			}
		}

		// Token: 0x04002771 RID: 10097
		private IMessageSink envoySinks;
	}
}
