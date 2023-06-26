using System;
using System.Runtime.InteropServices;

namespace System.Management
{
	// Token: 0x02000042 RID: 66
	internal class SecuredConnectHandler
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0000D23A File Offset: 0x0000B43A
		internal SecuredConnectHandler(ManagementScope theScope)
		{
			this.scope = theScope;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000D24C File Offset: 0x0000B44C
		internal int ConnectNSecureIWbemServices(string path, ref IWbemServices pServices)
		{
			int num = -2147217407;
			if (this.scope != null)
			{
				bool flag = false;
				IntPtr zero = IntPtr.Zero;
				try
				{
					if (this.scope.Options.EnablePrivileges && !CompatSwitches.AllowIManagementObjectQI)
					{
						WmiNetUtilsHelper.SetSecurity_f(ref flag, ref zero);
					}
					IntPtr password = this.scope.Options.GetPassword();
					num = WmiNetUtilsHelper.ConnectServerWmi_f(path, this.scope.Options.Username, password, this.scope.Options.Locale, this.scope.Options.Flags, this.scope.Options.Authority, this.scope.Options.GetContext(), out pServices, (int)this.scope.Options.Impersonation, (int)this.scope.Options.Authentication);
					Marshal.ZeroFreeBSTR(password);
				}
				finally
				{
					if (flag)
					{
						flag = false;
						WmiNetUtilsHelper.ResetSecurity_f(zero);
					}
				}
			}
			return num;
		}

		// Token: 0x040001BE RID: 446
		private ManagementScope scope;
	}
}
