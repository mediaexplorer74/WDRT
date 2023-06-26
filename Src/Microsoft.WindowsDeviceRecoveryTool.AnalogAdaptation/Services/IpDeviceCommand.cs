using System;
using Microsoft.Tools.Connectivity;

namespace Microsoft.WindowsDeviceRecoveryTool.AnalogAdaptation.Services
{
	// Token: 0x02000007 RID: 7
	public abstract class IpDeviceCommand
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003F09 File Offset: 0x00002109
		protected IpDeviceCommand(string command, string alternateCommand, string args)
		{
			this.Command = command;
			this.AlternateCommand = alternateCommand;
			this.args = args;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003F2A File Offset: 0x0000212A
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00003F32 File Offset: 0x00002132
		private protected string Command { protected get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003F3B File Offset: 0x0000213B
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00003F43 File Offset: 0x00002143
		private protected string AlternateCommand { protected get; private set; }

		// Token: 0x06000046 RID: 70 RVA: 0x00003F4C File Offset: 0x0000214C
		protected string Args(string additionalArgs = null)
		{
			bool flag = string.IsNullOrEmpty(additionalArgs);
			string text;
			if (flag)
			{
				text = this.args;
			}
			else
			{
				text = string.Format("{0} {1}", this.args, additionalArgs);
			}
			return text;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003F84 File Offset: 0x00002184
		protected string GetFullCommandString(string additionalArgs = null)
		{
			return string.Format("{0} {1}", this.Command, this.Args(additionalArgs));
		}

		// Token: 0x06000048 RID: 72
		public abstract string Execute(RemoteDevice device, string additionalArgs);

		// Token: 0x04000016 RID: 22
		private readonly string args;
	}
}
