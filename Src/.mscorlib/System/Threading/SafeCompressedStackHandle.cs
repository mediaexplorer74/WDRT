using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004EE RID: 1262
	[SecurityCritical]
	internal class SafeCompressedStackHandle : SafeHandle
	{
		// Token: 0x06003BC2 RID: 15298 RVA: 0x000E4095 File Offset: 0x000E2295
		public SafeCompressedStackHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x000E40A3 File Offset: 0x000E22A3
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000E40B5 File Offset: 0x000E22B5
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			CompressedStack.DestroyDelayedCompressedStack(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}
	}
}
