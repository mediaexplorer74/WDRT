using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000056 RID: 86
	internal struct StoreOperationMetadataProperty
	{
		// Token: 0x06000191 RID: 401 RVA: 0x000072FE File Offset: 0x000054FE
		public StoreOperationMetadataProperty(Guid PropertySet, string Name)
		{
			this = new StoreOperationMetadataProperty(PropertySet, Name, null);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007309 File Offset: 0x00005509
		public StoreOperationMetadataProperty(Guid PropertySet, string Name, string Value)
		{
			this.GuidPropertySet = PropertySet;
			this.Name = Name;
			this.Value = Value;
			this.ValueSize = ((Value != null) ? new IntPtr((Value.Length + 1) * 2) : IntPtr.Zero);
		}

		// Token: 0x0400016C RID: 364
		public Guid GuidPropertySet;

		// Token: 0x0400016D RID: 365
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x0400016E RID: 366
		[MarshalAs(UnmanagedType.SysUInt)]
		public IntPtr ValueSize;

		// Token: 0x0400016F RID: 367
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Value;
	}
}
