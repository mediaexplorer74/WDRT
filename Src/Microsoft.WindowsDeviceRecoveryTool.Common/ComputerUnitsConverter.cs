using System;
using System.Globalization;

namespace Microsoft.WindowsDeviceRecoveryTool.Common
{
	// Token: 0x02000003 RID: 3
	public static class ComputerUnitsConverter
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020BC File Offset: 0x000002BC
		public static string SpeedToString(double bytesPerSecond)
		{
			CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
			return string.Format(currentUICulture, "{0:0.00} {1}", new object[]
			{
				bytesPerSecond / 1024.0,
				"kB/s"
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002100 File Offset: 0x00000300
		public static string SizeToString(long size)
		{
			return ComputerUnitsConverter.SizeToString((float)size, 0);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000211C File Offset: 0x0000031C
		private static string SizeToString(float size, int unit)
		{
			int num = unit + 1;
			bool flag = size >= 1024f && num < ComputerUnitsConverter.Units.Length;
			string text;
			if (flag)
			{
				text = ComputerUnitsConverter.SizeToString(size / 1024f, num);
			}
			else
			{
				string text2 = ComputerUnitsConverter.Units[unit].Replace("{0}", "{0:0.00}");
				CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
				text = string.Format(currentUICulture, text2, new object[] { size });
			}
			return text;
		}

		// Token: 0x04000002 RID: 2
		private const float Kilo = 1024f;

		// Token: 0x04000003 RID: 3
		private static readonly string[] Units = new string[] { "{0} B", "{0} KB", "{0} MB", "{0} GB", "{0} TB" };
	}
}
