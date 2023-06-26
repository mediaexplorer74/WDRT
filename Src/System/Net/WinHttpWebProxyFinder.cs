using System;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Net
{
	// Token: 0x0200018D RID: 397
	internal sealed class WinHttpWebProxyFinder : BaseWebProxyFinder
	{
		// Token: 0x06000F3E RID: 3902 RVA: 0x0004F13C File Offset: 0x0004D33C
		public WinHttpWebProxyFinder(AutoWebProxyScriptEngine engine)
			: base(engine)
		{
			this.session = UnsafeNclNativeMethods.WinHttp.WinHttpOpen(null, UnsafeNclNativeMethods.WinHttp.AccessType.NoProxy, null, null, 0);
			if (this.session == null || this.session.IsInvalid)
			{
				int lastWin32Error = WinHttpWebProxyFinder.GetLastWin32Error();
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, SR.GetString("net_log_proxy_winhttp_cant_open_session", new object[] { lastWin32Error }));
					return;
				}
			}
			else
			{
				int downloadTimeout = SettingsSectionInternal.Section.DownloadTimeout;
				if (!UnsafeNclNativeMethods.WinHttp.WinHttpSetTimeouts(this.session, downloadTimeout, downloadTimeout, downloadTimeout, downloadTimeout))
				{
					int lastWin32Error2 = WinHttpWebProxyFinder.GetLastWin32Error();
					if (Logging.On)
					{
						Logging.PrintError(Logging.Web, SR.GetString("net_log_proxy_winhttp_timeout_error", new object[] { lastWin32Error2 }));
					}
				}
			}
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0004F1F4 File Offset: 0x0004D3F4
		public override bool GetProxies(Uri destination, out IList<string> proxyList)
		{
			proxyList = null;
			if (this.session == null || this.session.IsInvalid)
			{
				return false;
			}
			if (base.State == BaseWebProxyFinder.AutoWebProxyState.UnrecognizedScheme)
			{
				return false;
			}
			string text = null;
			int num = 12180;
			if (base.Engine.AutomaticallyDetectSettings && !this.autoDetectFailed)
			{
				num = this.GetProxies(destination, null, out text);
				this.autoDetectFailed = WinHttpWebProxyFinder.IsErrorFatalForAutoDetect(num);
				if (num == 12006)
				{
					base.State = BaseWebProxyFinder.AutoWebProxyState.UnrecognizedScheme;
					return false;
				}
			}
			if (base.Engine.AutomaticConfigurationScript != null && WinHttpWebProxyFinder.IsRecoverableAutoProxyError(num))
			{
				num = this.GetProxies(destination, base.Engine.AutomaticConfigurationScript, out text);
			}
			base.State = WinHttpWebProxyFinder.GetStateFromErrorCode(num);
			if (base.State == BaseWebProxyFinder.AutoWebProxyState.Completed)
			{
				if (string.IsNullOrEmpty(text))
				{
					proxyList = new string[1];
				}
				else
				{
					text = WinHttpWebProxyFinder.RemoveWhitespaces(text);
					proxyList = text.Split(new char[] { ';' });
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0004F2DF File Offset: 0x0004D4DF
		public override void Abort()
		{
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0004F2E1 File Offset: 0x0004D4E1
		public override void Reset()
		{
			base.Reset();
			this.autoDetectFailed = false;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0004F2F0 File Offset: 0x0004D4F0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.session != null && !this.session.IsInvalid)
			{
				this.session.Close();
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0004F318 File Offset: 0x0004D518
		private int GetProxies(Uri destination, Uri scriptLocation, out string proxyListString)
		{
			int num = 0;
			proxyListString = null;
			UnsafeNclNativeMethods.WinHttp.WINHTTP_AUTOPROXY_OPTIONS winhttp_AUTOPROXY_OPTIONS = default(UnsafeNclNativeMethods.WinHttp.WINHTTP_AUTOPROXY_OPTIONS);
			winhttp_AUTOPROXY_OPTIONS.AutoLogonIfChallenged = false;
			if (scriptLocation == null)
			{
				winhttp_AUTOPROXY_OPTIONS.Flags = UnsafeNclNativeMethods.WinHttp.AutoProxyFlags.AutoDetect;
				winhttp_AUTOPROXY_OPTIONS.AutoConfigUrl = null;
				winhttp_AUTOPROXY_OPTIONS.AutoDetectFlags = UnsafeNclNativeMethods.WinHttp.AutoDetectType.Dhcp | UnsafeNclNativeMethods.WinHttp.AutoDetectType.DnsA;
			}
			else
			{
				winhttp_AUTOPROXY_OPTIONS.Flags = UnsafeNclNativeMethods.WinHttp.AutoProxyFlags.AutoProxyConfigUrl;
				winhttp_AUTOPROXY_OPTIONS.AutoConfigUrl = scriptLocation.ToString();
				winhttp_AUTOPROXY_OPTIONS.AutoDetectFlags = UnsafeNclNativeMethods.WinHttp.AutoDetectType.None;
			}
			if (!this.WinHttpGetProxyForUrl(destination.ToString(), ref winhttp_AUTOPROXY_OPTIONS, out proxyListString))
			{
				num = WinHttpWebProxyFinder.GetLastWin32Error();
				if (num == 12015 && base.Engine.Credentials != null)
				{
					winhttp_AUTOPROXY_OPTIONS.AutoLogonIfChallenged = true;
					if (!this.WinHttpGetProxyForUrl(destination.ToString(), ref winhttp_AUTOPROXY_OPTIONS, out proxyListString))
					{
						num = WinHttpWebProxyFinder.GetLastWin32Error();
					}
				}
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, SR.GetString("net_log_proxy_winhttp_getproxy_failed", new object[] { destination, num }));
				}
			}
			return num;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0004F3F4 File Offset: 0x0004D5F4
		private bool WinHttpGetProxyForUrl(string destination, ref UnsafeNclNativeMethods.WinHttp.WINHTTP_AUTOPROXY_OPTIONS autoProxyOptions, out string proxyListString)
		{
			proxyListString = null;
			bool flag = false;
			UnsafeNclNativeMethods.WinHttp.WINHTTP_PROXY_INFO winhttp_PROXY_INFO = default(UnsafeNclNativeMethods.WinHttp.WINHTTP_PROXY_INFO);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				flag = UnsafeNclNativeMethods.WinHttp.WinHttpGetProxyForUrl(this.session, destination, ref autoProxyOptions, out winhttp_PROXY_INFO);
				if (flag)
				{
					proxyListString = Marshal.PtrToStringUni(winhttp_PROXY_INFO.Proxy);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(winhttp_PROXY_INFO.Proxy);
				Marshal.FreeHGlobal(winhttp_PROXY_INFO.ProxyBypass);
			}
			return flag;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0004F460 File Offset: 0x0004D660
		private static int GetLastWin32Error()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 8)
			{
				throw new OutOfMemoryException();
			}
			return lastWin32Error;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0004F480 File Offset: 0x0004D680
		private static bool IsRecoverableAutoProxyError(int errorCode)
		{
			if (errorCode <= 12015)
			{
				if (errorCode != 12002 && errorCode != 12006 && errorCode != 12015)
				{
					return false;
				}
			}
			else if (errorCode <= 12167)
			{
				if (errorCode != 12017 && errorCode - 12166 > 1)
				{
					return false;
				}
			}
			else if (errorCode != 12178 && errorCode != 12180)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0004F4E0 File Offset: 0x0004D6E0
		private static BaseWebProxyFinder.AutoWebProxyState GetStateFromErrorCode(int errorCode)
		{
			if ((long)errorCode == 0L)
			{
				return BaseWebProxyFinder.AutoWebProxyState.Completed;
			}
			if (errorCode <= 12166)
			{
				if (errorCode != 12005)
				{
					if (errorCode == 12006)
					{
						return BaseWebProxyFinder.AutoWebProxyState.UnrecognizedScheme;
					}
					if (errorCode != 12166)
					{
						return BaseWebProxyFinder.AutoWebProxyState.CompilationFailure;
					}
				}
			}
			else
			{
				if (errorCode == 12167)
				{
					return BaseWebProxyFinder.AutoWebProxyState.DownloadFailure;
				}
				if (errorCode != 12178)
				{
					if (errorCode == 12180)
					{
						return BaseWebProxyFinder.AutoWebProxyState.DiscoveryFailure;
					}
					return BaseWebProxyFinder.AutoWebProxyState.CompilationFailure;
				}
			}
			return BaseWebProxyFinder.AutoWebProxyState.Completed;
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0004F538 File Offset: 0x0004D738
		private static string RemoveWhitespaces(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (!char.IsWhiteSpace(c))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0004F57C File Offset: 0x0004D77C
		private static bool IsErrorFatalForAutoDetect(int errorCode)
		{
			if (errorCode <= 12005)
			{
				if (errorCode != 0 && errorCode != 12005)
				{
					return true;
				}
			}
			else if (errorCode != 12166 && errorCode != 12178)
			{
				return true;
			}
			return false;
		}

		// Token: 0x04001298 RID: 4760
		private SafeInternetHandle session;

		// Token: 0x04001299 RID: 4761
		private bool autoDetectFailed;
	}
}
