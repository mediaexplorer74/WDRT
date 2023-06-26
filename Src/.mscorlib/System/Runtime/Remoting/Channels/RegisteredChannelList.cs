using System;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000828 RID: 2088
	internal class RegisteredChannelList
	{
		// Token: 0x060059A8 RID: 22952 RVA: 0x0013D699 File Offset: 0x0013B899
		internal RegisteredChannelList()
		{
			this._channels = new RegisteredChannel[0];
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x0013D6AD File Offset: 0x0013B8AD
		internal RegisteredChannelList(RegisteredChannel[] channels)
		{
			this._channels = channels;
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x060059AA RID: 22954 RVA: 0x0013D6BC File Offset: 0x0013B8BC
		internal RegisteredChannel[] RegisteredChannels
		{
			get
			{
				return this._channels;
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x060059AB RID: 22955 RVA: 0x0013D6C4 File Offset: 0x0013B8C4
		internal int Count
		{
			get
			{
				if (this._channels == null)
				{
					return 0;
				}
				return this._channels.Length;
			}
		}

		// Token: 0x060059AC RID: 22956 RVA: 0x0013D6D8 File Offset: 0x0013B8D8
		internal IChannel GetChannel(int index)
		{
			return this._channels[index].Channel;
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x0013D6E7 File Offset: 0x0013B8E7
		internal bool IsSender(int index)
		{
			return this._channels[index].IsSender();
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x0013D6F6 File Offset: 0x0013B8F6
		internal bool IsReceiver(int index)
		{
			return this._channels[index].IsReceiver();
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x060059AF RID: 22959 RVA: 0x0013D708 File Offset: 0x0013B908
		internal int ReceiverCount
		{
			get
			{
				if (this._channels == null)
				{
					return 0;
				}
				int num = 0;
				for (int i = 0; i < this._channels.Length; i++)
				{
					if (this.IsReceiver(i))
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x0013D744 File Offset: 0x0013B944
		internal int FindChannelIndex(IChannel channel)
		{
			for (int i = 0; i < this._channels.Length; i++)
			{
				if (channel == this.GetChannel(i))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x0013D774 File Offset: 0x0013B974
		[SecurityCritical]
		internal int FindChannelIndex(string name)
		{
			for (int i = 0; i < this._channels.Length; i++)
			{
				if (string.Compare(name, this.GetChannel(i).ChannelName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x040028D5 RID: 10453
		private RegisteredChannel[] _channels;
	}
}
