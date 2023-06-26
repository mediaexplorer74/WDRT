using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A0 RID: 1696
	internal struct StoreApplicationReference
	{
		// Token: 0x06004FE0 RID: 20448 RVA: 0x0011DFDD File Offset: 0x0011C1DD
		public StoreApplicationReference(Guid RefScheme, string Id, string NcData)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreApplicationReference));
			this.Flags = StoreApplicationReference.RefFlags.Nothing;
			this.GuidScheme = RefScheme;
			this.Identifier = Id;
			this.NonCanonicalData = NcData;
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x0011E010 File Offset: 0x0011C210
		[SecurityCritical]
		public IntPtr ToIntPtr()
		{
			IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf<StoreApplicationReference>(this));
			Marshal.StructureToPtr<StoreApplicationReference>(this, intPtr, false);
			return intPtr;
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x0011E03C File Offset: 0x0011C23C
		[SecurityCritical]
		public static void Destroy(IntPtr ip)
		{
			if (ip != IntPtr.Zero)
			{
				Marshal.DestroyStructure(ip, typeof(StoreApplicationReference));
				Marshal.FreeCoTaskMem(ip);
			}
		}

		// Token: 0x04002236 RID: 8758
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002237 RID: 8759
		[MarshalAs(UnmanagedType.U4)]
		public StoreApplicationReference.RefFlags Flags;

		// Token: 0x04002238 RID: 8760
		public Guid GuidScheme;

		// Token: 0x04002239 RID: 8761
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Identifier;

		// Token: 0x0400223A RID: 8762
		[MarshalAs(UnmanagedType.LPWStr)]
		public string NonCanonicalData;

		// Token: 0x02000C42 RID: 3138
		[Flags]
		public enum RefFlags
		{
			// Token: 0x04003769 RID: 14185
			Nothing = 0
		}
	}
}
