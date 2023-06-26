using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009FD RID: 2557
	internal abstract class RuntimeClass : __ComObject
	{
		// Token: 0x0600650B RID: 25867
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetRedirectedGetHashCodeMD();

		// Token: 0x0600650C RID: 25868
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int RedirectGetHashCode(IntPtr pMD);

		// Token: 0x0600650D RID: 25869 RVA: 0x0015948C File Offset: 0x0015768C
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			IntPtr redirectedGetHashCodeMD = this.GetRedirectedGetHashCodeMD();
			if (redirectedGetHashCodeMD == IntPtr.Zero)
			{
				return base.GetHashCode();
			}
			return this.RedirectGetHashCode(redirectedGetHashCodeMD);
		}

		// Token: 0x0600650E RID: 25870
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetRedirectedToStringMD();

		// Token: 0x0600650F RID: 25871
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string RedirectToString(IntPtr pMD);

		// Token: 0x06006510 RID: 25872 RVA: 0x001594BC File Offset: 0x001576BC
		[SecuritySafeCritical]
		public override string ToString()
		{
			IStringable stringable = this as IStringable;
			if (stringable != null)
			{
				return stringable.ToString();
			}
			IntPtr redirectedToStringMD = this.GetRedirectedToStringMD();
			if (redirectedToStringMD == IntPtr.Zero)
			{
				return base.ToString();
			}
			return this.RedirectToString(redirectedToStringMD);
		}

		// Token: 0x06006511 RID: 25873
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetRedirectedEqualsMD();

		// Token: 0x06006512 RID: 25874
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool RedirectEquals(object obj, IntPtr pMD);

		// Token: 0x06006513 RID: 25875 RVA: 0x001594FC File Offset: 0x001576FC
		[SecuritySafeCritical]
		public override bool Equals(object obj)
		{
			IntPtr redirectedEqualsMD = this.GetRedirectedEqualsMD();
			if (redirectedEqualsMD == IntPtr.Zero)
			{
				return base.Equals(obj);
			}
			return this.RedirectEquals(obj, redirectedEqualsMD);
		}
	}
}
