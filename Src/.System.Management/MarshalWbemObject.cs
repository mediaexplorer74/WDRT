using System;
using System.Runtime.InteropServices;

namespace System.Management
{
	// Token: 0x02000057 RID: 87
	internal class MarshalWbemObject : ICustomMarshaler
	{
		// Token: 0x06000386 RID: 902 RVA: 0x000218C1 File Offset: 0x0001FAC1
		public static ICustomMarshaler GetInstance(string cookie)
		{
			return new MarshalWbemObject(cookie);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000218C9 File Offset: 0x0001FAC9
		private MarshalWbemObject(string cookie)
		{
			this.cookie = cookie;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00002208 File Offset: 0x00000408
		public void CleanUpManagedData(object obj)
		{
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00002208 File Offset: 0x00000408
		public void CleanUpNativeData(IntPtr pObj)
		{
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000218D8 File Offset: 0x0001FAD8
		public int GetNativeDataSize()
		{
			return -1;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000218DB File Offset: 0x0001FADB
		public IntPtr MarshalManagedToNative(object obj)
		{
			return (IntPtr)obj;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000218E3 File Offset: 0x0001FAE3
		public object MarshalNativeToManaged(IntPtr pObj)
		{
			return new IWbemClassObjectFreeThreaded(pObj);
		}

		// Token: 0x0400024C RID: 588
		private string cookie;
	}
}
