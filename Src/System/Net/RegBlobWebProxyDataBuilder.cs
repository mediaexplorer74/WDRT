using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Net
{
	// Token: 0x020001E4 RID: 484
	internal class RegBlobWebProxyDataBuilder : WebProxyDataBuilder
	{
		// Token: 0x060012CE RID: 4814 RVA: 0x00063A70 File Offset: 0x00061C70
		public RegBlobWebProxyDataBuilder(string connectoid, SafeRegistryHandle registry)
		{
			this.m_Registry = registry;
			this.m_Connectoid = connectoid;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00063A88 File Offset: 0x00061C88
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\CurrentVersion\\Internet Settings")]
		private bool ReadRegSettings()
		{
			SafeRegistryHandle safeRegistryHandle = null;
			RegistryKey registryKey = null;
			try
			{
				bool flag = true;
				registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
				if (registryKey != null)
				{
					object value = registryKey.GetValue("ProxySettingsPerUser");
					if (value != null && value.GetType() == typeof(int) && (int)value == 0)
					{
						flag = false;
					}
				}
				uint num;
				if (flag)
				{
					if (this.m_Registry != null)
					{
						num = this.m_Registry.RegOpenKeyEx("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Connections", 0U, 131097U, out safeRegistryHandle);
					}
					else
					{
						num = 1168U;
					}
				}
				else
				{
					num = SafeRegistryHandle.RegOpenKeyEx(UnsafeNclNativeMethods.RegistryHelper.HKEY_LOCAL_MACHINE, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Connections", 0U, 131097U, out safeRegistryHandle);
				}
				if (num != 0U)
				{
					safeRegistryHandle = null;
				}
				object obj;
				if (safeRegistryHandle != null && safeRegistryHandle.QueryValue((this.m_Connectoid != null) ? this.m_Connectoid : "DefaultConnectionSettings", out obj) == 0U)
				{
					this.m_RegistryBytes = (byte[])obj;
				}
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				if (safeRegistryHandle != null)
				{
					safeRegistryHandle.RegCloseKey();
				}
			}
			return this.m_RegistryBytes != null;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00063BA8 File Offset: 0x00061DA8
		public string ReadString()
		{
			string text = null;
			int num = this.ReadInt32();
			if (num > 0)
			{
				int num2 = this.m_RegistryBytes.Length - this.m_ByteOffset;
				if (num >= num2)
				{
					num = num2;
				}
				text = Encoding.UTF8.GetString(this.m_RegistryBytes, this.m_ByteOffset, num);
				this.m_ByteOffset += num;
			}
			return text;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00063C00 File Offset: 0x00061E00
		internal unsafe int ReadInt32()
		{
			int num = 0;
			int num2 = this.m_RegistryBytes.Length - this.m_ByteOffset;
			if (num2 >= 4)
			{
				byte[] array;
				byte* ptr;
				if ((array = this.m_RegistryBytes) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (sizeof(IntPtr) == 4)
				{
					num = *(int*)(ptr + this.m_ByteOffset);
				}
				else
				{
					num = Marshal.ReadInt32((IntPtr)((void*)ptr), this.m_ByteOffset);
				}
				array = null;
				this.m_ByteOffset += 4;
			}
			return num;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00063C78 File Offset: 0x00061E78
		protected override void BuildInternal()
		{
			bool flag = this.ReadRegSettings();
			if (flag)
			{
				flag = this.ReadInt32() >= 60;
			}
			if (!flag)
			{
				base.SetAutoDetectSettings(true);
				return;
			}
			this.ReadInt32();
			RegBlobWebProxyDataBuilder.ProxyTypeFlags proxyTypeFlags = (RegBlobWebProxyDataBuilder.ProxyTypeFlags)this.ReadInt32();
			string text = this.ReadString();
			string text2 = this.ReadString();
			if ((proxyTypeFlags & RegBlobWebProxyDataBuilder.ProxyTypeFlags.PROXY_TYPE_PROXY) != (RegBlobWebProxyDataBuilder.ProxyTypeFlags)0)
			{
				base.SetProxyAndBypassList(text, text2);
			}
			base.SetAutoDetectSettings((proxyTypeFlags & RegBlobWebProxyDataBuilder.ProxyTypeFlags.PROXY_TYPE_AUTO_DETECT) > (RegBlobWebProxyDataBuilder.ProxyTypeFlags)0);
			string text3 = this.ReadString();
			if ((proxyTypeFlags & RegBlobWebProxyDataBuilder.ProxyTypeFlags.PROXY_TYPE_AUTO_PROXY_URL) != (RegBlobWebProxyDataBuilder.ProxyTypeFlags)0)
			{
				base.SetAutoProxyUrl(text3);
			}
		}

		// Token: 0x04001513 RID: 5395
		internal const string PolicyKey = "SOFTWARE\\Policies\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";

		// Token: 0x04001514 RID: 5396
		internal const string ProxyKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Connections";

		// Token: 0x04001515 RID: 5397
		private const string DefaultConnectionSettings = "DefaultConnectionSettings";

		// Token: 0x04001516 RID: 5398
		private const string ProxySettingsPerUser = "ProxySettingsPerUser";

		// Token: 0x04001517 RID: 5399
		private const int IE50StrucSize = 60;

		// Token: 0x04001518 RID: 5400
		private byte[] m_RegistryBytes;

		// Token: 0x04001519 RID: 5401
		private int m_ByteOffset;

		// Token: 0x0400151A RID: 5402
		private string m_Connectoid;

		// Token: 0x0400151B RID: 5403
		private SafeRegistryHandle m_Registry;

		// Token: 0x02000755 RID: 1877
		[Flags]
		private enum ProxyTypeFlags
		{
			// Token: 0x040031FA RID: 12794
			PROXY_TYPE_DIRECT = 1,
			// Token: 0x040031FB RID: 12795
			PROXY_TYPE_PROXY = 2,
			// Token: 0x040031FC RID: 12796
			PROXY_TYPE_AUTO_PROXY_URL = 4,
			// Token: 0x040031FD RID: 12797
			PROXY_TYPE_AUTO_DETECT = 8
		}
	}
}
