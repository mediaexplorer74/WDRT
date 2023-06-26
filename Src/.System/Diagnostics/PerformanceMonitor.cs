using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004E3 RID: 1251
	internal class PerformanceMonitor
	{
		// Token: 0x06002F5B RID: 12123 RVA: 0x000D5AAD File Offset: 0x000D3CAD
		internal PerformanceMonitor(string machineName)
		{
			this.machineName = machineName;
			this.Init();
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000D5AC4 File Offset: 0x000D3CC4
		private void Init()
		{
			try
			{
				if (this.machineName != "." && string.Compare(this.machineName, PerformanceCounterLib.ComputerName, StringComparison.OrdinalIgnoreCase) != 0)
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
					this.perfDataKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.PerformanceData, this.machineName);
				}
				else
				{
					this.perfDataKey = Registry.PerformanceData;
				}
			}
			catch (UnauthorizedAccessException)
			{
				throw new Win32Exception(5);
			}
			catch (IOException ex)
			{
				throw new Win32Exception(Marshal.GetHRForException(ex));
			}
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000D5B58 File Offset: 0x000D3D58
		internal void Close()
		{
			if (this.perfDataKey != null)
			{
				this.perfDataKey.Close();
			}
			this.perfDataKey = null;
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000D5B74 File Offset: 0x000D3D74
		internal byte[] GetData(string item)
		{
			int i = 17;
			int num = 0;
			int num2 = 0;
			new RegistryPermission(PermissionState.Unrestricted).Assert();
			while (i > 0)
			{
				try
				{
					return (byte[])this.perfDataKey.GetValue(item);
				}
				catch (IOException ex)
				{
					num2 = Marshal.GetHRForException(ex);
					if (num2 <= 167)
					{
						if (num2 != 6)
						{
							if (num2 != 21 && num2 != 167)
							{
								goto IL_A1;
							}
							goto IL_89;
						}
					}
					else if (num2 <= 258)
					{
						if (num2 != 170 && num2 != 258)
						{
							goto IL_A1;
						}
						goto IL_89;
					}
					else if (num2 != 1722 && num2 != 1726)
					{
						goto IL_A1;
					}
					this.Init();
					IL_89:
					i--;
					if (num == 0)
					{
						num = 10;
					}
					else
					{
						Thread.Sleep(num);
						num *= 2;
					}
					continue;
					IL_A1:
					throw SharedUtils.CreateSafeWin32Exception(num2);
				}
				catch (InvalidCastException ex2)
				{
					throw new InvalidOperationException(SR.GetString("CounterDataCorrupt", new object[] { this.perfDataKey.ToString() }), ex2);
				}
			}
			throw SharedUtils.CreateSafeWin32Exception(num2);
		}

		// Token: 0x040027D5 RID: 10197
		private RegistryKey perfDataKey;

		// Token: 0x040027D6 RID: 10198
		private string machineName;
	}
}
