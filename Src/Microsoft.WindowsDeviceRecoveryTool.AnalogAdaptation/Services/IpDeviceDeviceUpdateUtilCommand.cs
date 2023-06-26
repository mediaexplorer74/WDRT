using System;
using Microsoft.Tools.Connectivity;

namespace Microsoft.WindowsDeviceRecoveryTool.AnalogAdaptation.Services
{
	// Token: 0x02000009 RID: 9
	public class IpDeviceDeviceUpdateUtilCommand : IpDeviceCommand
	{
		// Token: 0x06000052 RID: 82 RVA: 0x000042C1 File Offset: 0x000024C1
		public IpDeviceDeviceUpdateUtilCommand(string args, string secondaryArgs = null)
			: base("C:\\Windows\\System32\\DeviceUpdateUtil.exe", "DeviceUpdateUtil.exe", args)
		{
			this.secondaryArgs = secondaryArgs;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000042E0 File Offset: 0x000024E0
		private bool HasSecondaryArgs()
		{
			return this.secondaryArgs != null;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000042FC File Offset: 0x000024FC
		private string SecondaryArgs(string additionalArgs = null)
		{
			bool flag = string.IsNullOrEmpty(additionalArgs);
			string text;
			if (flag)
			{
				text = this.secondaryArgs;
			}
			else
			{
				text = string.Format("{0} {1}", this.secondaryArgs, additionalArgs);
			}
			return text;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004334 File Offset: 0x00002534
		public override string Execute(RemoteDevice device, string additionalArgs)
		{
			string text;
			try
			{
				text = this.Execute(device, additionalArgs, false);
			}
			catch (DeviceException ex)
			{
				bool flag = ex.InnerException is InvalidOperationException;
				if (!flag)
				{
					throw;
				}
				text = this.Execute(device, additionalArgs, true);
			}
			return text;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004384 File Offset: 0x00002584
		private string Execute(RemoteDevice device, string additionalArgs, bool useSecondaryArgs)
		{
			string fullCommandString = base.GetFullCommandString(additionalArgs);
			string text;
			try
			{
				try
				{
					text = device.RunCommand(base.Command, useSecondaryArgs ? this.SecondaryArgs(additionalArgs) : base.Args(additionalArgs));
				}
				catch (OperationFailedException)
				{
					text = device.RunCommand(base.AlternateCommand, useSecondaryArgs ? this.SecondaryArgs(additionalArgs) : base.Args(additionalArgs));
				}
			}
			catch (Exception ex)
			{
				throw new DeviceException(string.Format("Unexpected failure for command \"{0}\"", fullCommandString), ex);
			}
			bool flag = !text.Contains(";");
			if (flag)
			{
				throw new DeviceException(string.Format("Unexpected device response for command \"{0}\": {1}", fullCommandString, text));
			}
			int num;
			try
			{
				num = int.Parse(text.Substring(text.LastIndexOf(';') + 1));
			}
			catch (Exception ex2)
			{
				throw new DeviceException(string.Format("Unexpected status for command \"{0}\"\n{1}", fullCommandString), ex2);
			}
			bool flag2 = num == 4317;
			if (flag2)
			{
				Exception ex3 = new InvalidOperationException(string.Format("Command \"{0}\" failed with status {1}", fullCommandString, num));
				throw new DeviceException(ex3.Message, ex3);
			}
			bool flag3 = num == 87;
			if (flag3)
			{
				Exception ex4 = new ArgumentException(string.Format("Command \"{0}\" failed with status {1}", fullCommandString, num));
				throw new DeviceException(ex4.Message, ex4);
			}
			bool flag4 = num != 0;
			if (flag4)
			{
				throw new DeviceException(string.Format("Command \"{0}\" failed with status {1}", fullCommandString, num));
			}
			return text.Substring(0, text.LastIndexOf(';'));
		}

		// Token: 0x0400002A RID: 42
		private const string DeviceUpdateUtilPath = "C:\\Windows\\System32\\DeviceUpdateUtil.exe";

		// Token: 0x0400002B RID: 43
		private const string DeviceUpdateUtilAlternatePath = "DeviceUpdateUtil.exe";

		// Token: 0x0400002C RID: 44
		private const int DeviceUpdateStatusSuccess = 0;

		// Token: 0x0400002D RID: 45
		private const int DeviceUpdateStatusInvalidParameter = 87;

		// Token: 0x0400002E RID: 46
		private const int DeviceUpdateStatusInvalidOperation = 4317;

		// Token: 0x0400002F RID: 47
		private readonly string secondaryArgs;
	}
}
