using System;
using System.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001FB RID: 507
	internal sealed class SafeCredentialReference : CriticalHandleMinusOneIsInvalid
	{
		// Token: 0x06001323 RID: 4899 RVA: 0x0006495C File Offset: 0x00062B5C
		internal static SafeCredentialReference CreateReference(SafeFreeCredentials target)
		{
			SafeCredentialReference safeCredentialReference = new SafeCredentialReference(target);
			if (safeCredentialReference.IsInvalid)
			{
				return null;
			}
			return safeCredentialReference;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0006497C File Offset: 0x00062B7C
		private SafeCredentialReference(SafeFreeCredentials target)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target.DangerousAddRef(ref flag);
			}
			catch
			{
				if (flag)
				{
					target.DangerousRelease();
					flag = false;
				}
			}
			finally
			{
				if (flag)
				{
					this._Target = target;
					base.SetHandle(new IntPtr(0));
				}
			}
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x000649E4 File Offset: 0x00062BE4
		protected override bool ReleaseHandle()
		{
			SafeFreeCredentials target = this._Target;
			if (target != null)
			{
				target.DangerousRelease();
			}
			this._Target = null;
			return true;
		}

		// Token: 0x0400153D RID: 5437
		internal SafeFreeCredentials _Target;
	}
}
