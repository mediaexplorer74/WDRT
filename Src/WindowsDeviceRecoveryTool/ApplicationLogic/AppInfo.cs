using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic
{
	// Token: 0x020000E4 RID: 228
	public static class AppInfo
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x000211F8 File Offset: 0x0001F3F8
		public static int ApplicationId
		{
			get
			{
				return ApplicationBuildSettings.ApplicationId;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00021210 File Offset: 0x0001F410
		public static string ApplicationName
		{
			get
			{
				bool flag = string.IsNullOrEmpty(AppInfo.applicationName);
				if (flag)
				{
					AppInfo.applicationName = AppInfo.GetAppName();
				}
				return AppInfo.applicationName;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00021244 File Offset: 0x0001F444
		public static string Version
		{
			get
			{
				bool flag = string.IsNullOrEmpty(AppInfo.version);
				if (flag)
				{
					AppInfo.version = AppInfo.GetVersion();
				}
				return AppInfo.version;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00021278 File Offset: 0x0001F478
		public static string AppName()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			bool flag = entryAssembly != null;
			string text;
			if (flag)
			{
				text = entryAssembly.GetName().Name;
			}
			else
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000212B0 File Offset: 0x0001F4B0
		public static string AppTitle()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			object[] customAttributes = entryAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
			bool flag = customAttributes.Length == 1;
			string text;
			if (flag)
			{
				text = ((AssemblyTitleAttribute)customAttributes[0]).Title;
			}
			else
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000212FC File Offset: 0x0001F4FC
		public static string AppVersion()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			bool flag = entryAssembly != null;
			string text4;
			if (flag)
			{
				string text = entryAssembly.GetName().Version.Major.ToString(CultureInfo.InvariantCulture);
				string text2 = entryAssembly.GetName().Version.Minor.ToString(CultureInfo.InvariantCulture);
				string text3 = entryAssembly.GetName().Version.Build.ToString(CultureInfo.InvariantCulture);
				text4 = string.Concat(new string[] { text, ".", text2, ".", text3 });
			}
			else
			{
				text4 = string.Empty;
			}
			return text4;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000213B4 File Offset: 0x0001F5B4
		public static string Copyrights()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			bool flag = entryAssembly != null;
			if (flag)
			{
				object[] customAttributes = entryAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				bool flag2 = customAttributes.Length != 0;
				if (flag2)
				{
					return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00021410 File Offset: 0x0001F610
		public static string AppAssemblyFilePath()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			bool flag = entryAssembly != null;
			string text;
			if (flag)
			{
				text = new Uri(entryAssembly.CodeBase, UriKind.Absolute).LocalPath;
			}
			else
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00021450 File Offset: 0x0001F650
		public static bool IsAnotherInstanceRunning()
		{
			string value = Assembly.GetExecutingAssembly().GetCustomAttributes(false).OfType<GuidAttribute>()
				.First<GuidAttribute>()
				.Value;
			bool flag;
			AppInfo.mutex = new Mutex(false, value, out flag);
			return !flag;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0002149C File Offset: 0x0001F69C
		private static string GetVersion()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			bool flag = entryAssembly != null;
			string text4;
			if (flag)
			{
				Version version = entryAssembly.GetName().Version;
				string text = version.Major.ToString(CultureInfo.InvariantCulture);
				string text2 = version.Minor.ToString(CultureInfo.InvariantCulture);
				string text3 = version.Build.ToString(CultureInfo.InvariantCulture);
				text4 = string.Concat(new string[] { text, ".", text2, ".", text3 });
			}
			else
			{
				text4 = string.Empty;
			}
			return text4;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00021544 File Offset: 0x0001F744
		private static string GetAppName()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			bool flag = entryAssembly != null;
			string text;
			if (flag)
			{
				text = entryAssembly.GetName().Name;
			}
			else
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0400033D RID: 829
		private static string version;

		// Token: 0x0400033E RID: 830
		private static string applicationName;

		// Token: 0x0400033F RID: 831
		private static Mutex mutex;
	}
}
