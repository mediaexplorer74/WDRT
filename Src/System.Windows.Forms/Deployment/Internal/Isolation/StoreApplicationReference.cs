using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000051 RID: 81
	internal struct StoreApplicationReference
	{
		// Token: 0x06000184 RID: 388 RVA: 0x0000712D File Offset: 0x0000532D
		[SecuritySafeCritical]
		public StoreApplicationReference(Guid RefScheme, string Id, string NcData)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreApplicationReference));
			this.Flags = StoreApplicationReference.RefFlags.Nothing;
			this.GuidScheme = RefScheme;
			this.Identifier = Id;
			this.NonCanonicalData = NcData;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007160 File Offset: 0x00005360
		[SecurityCritical]
		public IntPtr ToIntPtr()
		{
			IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(this));
			Marshal.StructureToPtr(this, intPtr, false);
			return intPtr;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007196 File Offset: 0x00005396
		[SecurityCritical]
		public static void Destroy(IntPtr ip)
		{
			if (ip != IntPtr.Zero)
			{
				Marshal.DestroyStructure(ip, typeof(StoreApplicationReference));
				Marshal.FreeCoTaskMem(ip);
			}
		}

		// Token: 0x04000156 RID: 342
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04000157 RID: 343
		[MarshalAs(UnmanagedType.U4)]
		public StoreApplicationReference.RefFlags Flags;

		// Token: 0x04000158 RID: 344
		public Guid GuidScheme;

		// Token: 0x04000159 RID: 345
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Identifier;

		// Token: 0x0400015A RID: 346
		[MarshalAs(UnmanagedType.LPWStr)]
		public string NonCanonicalData;

		// Token: 0x02000523 RID: 1315
		[Flags]
		public enum RefFlags
		{
			// Token: 0x0400379F RID: 14239
			Nothing = 0
		}
	}
}
