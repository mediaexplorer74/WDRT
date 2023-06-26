using System;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000014 RID: 20
	public class PlatformId
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004AED File Offset: 0x00002CED
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00004AF5 File Offset: 0x00002CF5
		public string Manufacturer { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00004AFE File Offset: 0x00002CFE
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00004B06 File Offset: 0x00002D06
		public string Family { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00004B0F File Offset: 0x00002D0F
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00004B17 File Offset: 0x00002D17
		public string ProductName { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004B20 File Offset: 0x00002D20
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00004B28 File Offset: 0x00002D28
		public string Version { get; private set; }

		// Token: 0x06000143 RID: 323 RVA: 0x00004B34 File Offset: 0x00002D34
		public void SetPlatformId(string platformId)
		{
			bool flag = string.Equals("*", platformId, StringComparison.InvariantCultureIgnoreCase);
			if (flag)
			{
				this.fullPlatformId = platformId;
				this.Manufacturer = (this.Family = (this.ProductName = (this.Version = string.Empty)));
			}
			else
			{
				string[] array = platformId.Split(new char[] { '.' }, 4);
				bool flag2 = array.Length < 3;
				if (flag2)
				{
					Tracer<PlatformId>.WriteWarning("Platform ID is incorrect: {0}", new object[] { platformId });
				}
				this.Manufacturer = ((array.Length >= 1) ? array[0] : string.Empty);
				this.Family = ((array.Length >= 2) ? array[1] : string.Empty);
				this.ProductName = ((array.Length >= 3) ? array[2] : string.Empty);
				this.Version = ((array.Length >= 4) ? array[3] : string.Empty);
				this.fullPlatformId = platformId;
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004C24 File Offset: 0x00002E24
		public bool IsCompatibleWithDevicePlatformId(PlatformId devicePlatformId)
		{
			bool flag = string.Equals(string.Empty, this.fullPlatformId, StringComparison.InvariantCultureIgnoreCase) || string.Equals("*", this.fullPlatformId, StringComparison.InvariantCultureIgnoreCase);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = string.Compare(this.Manufacturer, devicePlatformId.Manufacturer, StringComparison.CurrentCultureIgnoreCase) != 0;
				if (flag3)
				{
					Tracer<PlatformId>.WriteVerbose("Platform ID manufacturers do not match: {0} - {1}", new object[] { this.Manufacturer, devicePlatformId.Manufacturer });
					flag2 = false;
				}
				else
				{
					bool flag4 = string.Compare(this.Family, devicePlatformId.Family, StringComparison.CurrentCultureIgnoreCase) != 0;
					if (flag4)
					{
						Tracer<PlatformId>.WriteVerbose("Platform ID families do not match: {0} - {1}", new object[] { this.Family, devicePlatformId.Family });
						flag2 = false;
					}
					else
					{
						bool flag5 = string.Compare(this.ProductName, devicePlatformId.ProductName, StringComparison.CurrentCultureIgnoreCase) != 0;
						if (flag5)
						{
							Tracer<PlatformId>.WriteVerbose("Platform ID product names do not match: {0} - {1}", new object[] { this.ProductName, devicePlatformId.ProductName });
							flag2 = false;
						}
						else
						{
							flag2 = true;
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004D34 File Offset: 0x00002F34
		public override string ToString()
		{
			return this.fullPlatformId;
		}

		// Token: 0x0400006A RID: 106
		public const string WildcardPlatformId = "*";

		// Token: 0x0400006B RID: 107
		private string fullPlatformId;
	}
}
