using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Net
{
	// Token: 0x0200010E RID: 270
	internal sealed class HybridWebProxyFinder : IWebProxyFinder, IDisposable
	{
		// Token: 0x06000AEE RID: 2798 RVA: 0x0003CA04 File Offset: 0x0003AC04
		static HybridWebProxyFinder()
		{
			HybridWebProxyFinder.InitializeFallbackSettings();
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0003CA0B File Offset: 0x0003AC0B
		public HybridWebProxyFinder(AutoWebProxyScriptEngine engine)
		{
			this.engine = engine;
			this.winHttpFinder = new WinHttpWebProxyFinder(engine);
			this.currentFinder = this.winHttpFinder;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0003CA32 File Offset: 0x0003AC32
		public bool IsValid
		{
			get
			{
				return this.currentFinder.IsValid;
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0003CA40 File Offset: 0x0003AC40
		public bool GetProxies(Uri destination, out IList<string> proxyList)
		{
			if (this.currentFinder.GetProxies(destination, out proxyList))
			{
				return true;
			}
			if (HybridWebProxyFinder.allowFallback && this.currentFinder.IsUnrecognizedScheme && this.currentFinder == this.winHttpFinder)
			{
				if (this.netFinder == null)
				{
					this.netFinder = new NetWebProxyFinder(this.engine);
				}
				this.currentFinder = this.netFinder;
				return this.currentFinder.GetProxies(destination, out proxyList);
			}
			return false;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0003CAB4 File Offset: 0x0003ACB4
		public void Abort()
		{
			this.currentFinder.Abort();
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0003CAC1 File Offset: 0x0003ACC1
		public void Reset()
		{
			this.winHttpFinder.Reset();
			if (this.netFinder != null)
			{
				this.netFinder.Reset();
			}
			this.currentFinder = this.winHttpFinder;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0003CAED File Offset: 0x0003ACED
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0003CAF6 File Offset: 0x0003ACF6
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.winHttpFinder.Dispose();
				if (this.netFinder != null)
				{
					this.netFinder.Dispose();
				}
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0003CB1C File Offset: 0x0003AD1C
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\.NETFramework")]
		private static void InitializeFallbackSettings()
		{
			HybridWebProxyFinder.allowFallback = false;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework"))
				{
					try
					{
						object value = registryKey.GetValue("LegacyWPADSupport", null);
						if (value != null && registryKey.GetValueKind("LegacyWPADSupport") == RegistryValueKind.DWord)
						{
							HybridWebProxyFinder.allowFallback = (int)value == 1;
						}
					}
					catch (UnauthorizedAccessException)
					{
					}
					catch (IOException)
					{
					}
				}
			}
			catch (SecurityException)
			{
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x04000F49 RID: 3913
		private const string allowFallbackKey = "SOFTWARE\\Microsoft\\.NETFramework";

		// Token: 0x04000F4A RID: 3914
		private const string allowFallbackKeyPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\.NETFramework";

		// Token: 0x04000F4B RID: 3915
		private const string allowFallbackValueName = "LegacyWPADSupport";

		// Token: 0x04000F4C RID: 3916
		private static bool allowFallback;

		// Token: 0x04000F4D RID: 3917
		private NetWebProxyFinder netFinder;

		// Token: 0x04000F4E RID: 3918
		private WinHttpWebProxyFinder winHttpFinder;

		// Token: 0x04000F4F RID: 3919
		private BaseWebProxyFinder currentFinder;

		// Token: 0x04000F50 RID: 3920
		private AutoWebProxyScriptEngine engine;
	}
}
