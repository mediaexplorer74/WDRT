using System;
using System.Runtime.InteropServices;

namespace System.Management
{
	// Token: 0x02000044 RID: 68
	internal class SecurityHandler
	{
		// Token: 0x06000283 RID: 643 RVA: 0x0000D9A6 File Offset: 0x0000BBA6
		internal SecurityHandler(ManagementScope theScope)
		{
			this.scope = theScope;
			if (this.scope != null && this.scope.Options.EnablePrivileges)
			{
				WmiNetUtilsHelper.SetSecurity_f(ref this.needToReset, ref this.handle);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000D9E6 File Offset: 0x0000BBE6
		internal void Reset()
		{
			if (this.needToReset)
			{
				this.needToReset = false;
				if (this.scope != null)
				{
					WmiNetUtilsHelper.ResetSecurity_f(this.handle);
				}
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000DA10 File Offset: 0x0000BC10
		internal void Secure(IWbemServices services)
		{
			if (this.scope != null)
			{
				IntPtr password = this.scope.Options.GetPassword();
				int num = WmiNetUtilsHelper.BlessIWbemServices_f(services, this.scope.Options.Username, password, this.scope.Options.Authority, (int)this.scope.Options.Impersonation, (int)this.scope.Options.Authentication);
				Marshal.ZeroFreeBSTR(password);
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
				}
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000DAA0 File Offset: 0x0000BCA0
		internal void SecureIUnknown(object unknown)
		{
			if (this.scope != null)
			{
				IntPtr password = this.scope.Options.GetPassword();
				int num = WmiNetUtilsHelper.BlessIWbemServicesObject_f(unknown, this.scope.Options.Username, password, this.scope.Options.Authority, (int)this.scope.Options.Impersonation, (int)this.scope.Options.Authentication);
				Marshal.ZeroFreeBSTR(password);
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
				}
			}
		}

		// Token: 0x040001C1 RID: 449
		private bool needToReset;

		// Token: 0x040001C2 RID: 450
		private IntPtr handle;

		// Token: 0x040001C3 RID: 451
		private ManagementScope scope;
	}
}
