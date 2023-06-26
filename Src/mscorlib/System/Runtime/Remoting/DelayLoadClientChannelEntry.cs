using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007AE RID: 1966
	internal class DelayLoadClientChannelEntry
	{
		// Token: 0x0600555A RID: 21850 RVA: 0x0013076A File Offset: 0x0012E96A
		internal DelayLoadClientChannelEntry(RemotingXmlConfigFileData.ChannelEntry entry, bool ensureSecurity)
		{
			this._entry = entry;
			this._channel = null;
			this._bRegistered = false;
			this._ensureSecurity = ensureSecurity;
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x0600555B RID: 21851 RVA: 0x0013078E File Offset: 0x0012E98E
		internal IChannelSender Channel
		{
			[SecurityCritical]
			get
			{
				if (this._channel == null && !this._bRegistered)
				{
					this._channel = (IChannelSender)RemotingConfigHandler.CreateChannelFromConfigEntry(this._entry);
					this._entry = null;
				}
				return this._channel;
			}
		}

		// Token: 0x0600555C RID: 21852 RVA: 0x001307C3 File Offset: 0x0012E9C3
		internal void RegisterChannel()
		{
			ChannelServices.RegisterChannel(this._channel, this._ensureSecurity);
			this._bRegistered = true;
			this._channel = null;
		}

		// Token: 0x04002746 RID: 10054
		private RemotingXmlConfigFileData.ChannelEntry _entry;

		// Token: 0x04002747 RID: 10055
		private IChannelSender _channel;

		// Token: 0x04002748 RID: 10056
		private bool _bRegistered;

		// Token: 0x04002749 RID: 10057
		private bool _ensureSecurity;
	}
}
