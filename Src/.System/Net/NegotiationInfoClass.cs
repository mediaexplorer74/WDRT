using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000215 RID: 533
	internal class NegotiationInfoClass
	{
		// Token: 0x060013B6 RID: 5046 RVA: 0x00068480 File Offset: 0x00066680
		internal NegotiationInfoClass(SafeHandle safeHandle, int negotiationState)
		{
			if (safeHandle.IsInvalid)
			{
				return;
			}
			IntPtr intPtr = safeHandle.DangerousGetHandle();
			if (negotiationState == 0 || negotiationState == 1)
			{
				IntPtr intPtr2 = Marshal.ReadIntPtr(intPtr, SecurityPackageInfo.NameOffest);
				string text = null;
				if (intPtr2 != IntPtr.Zero)
				{
					text = Marshal.PtrToStringUni(intPtr2);
				}
				if (string.Compare(text, "Kerberos", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AuthenticationPackage = "Kerberos";
					return;
				}
				if (string.Compare(text, "NTLM", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AuthenticationPackage = "NTLM";
					return;
				}
				if (string.Compare(text, "WDigest", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AuthenticationPackage = "WDigest";
					return;
				}
				this.AuthenticationPackage = text;
			}
		}

		// Token: 0x040015A6 RID: 5542
		internal const string NTLM = "NTLM";

		// Token: 0x040015A7 RID: 5543
		internal const string Kerberos = "Kerberos";

		// Token: 0x040015A8 RID: 5544
		internal const string WDigest = "WDigest";

		// Token: 0x040015A9 RID: 5545
		internal const string Negotiate = "Negotiate";

		// Token: 0x040015AA RID: 5546
		internal string AuthenticationPackage;
	}
}
