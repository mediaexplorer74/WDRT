using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F7 RID: 503
	internal sealed class SafeCertSelectCritera : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x000643E0 File Offset: 0x000625E0
		internal int Count
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000643E4 File Offset: 0x000625E4
		private IntPtr AllocBuffer(int size)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(size);
			this.unmanagedMemoryList.Add(intPtr);
			return intPtr;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00064408 File Offset: 0x00062608
		private IntPtr AllocString(string str)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAnsi(str);
			this.unmanagedMemoryList.Add(intPtr);
			return intPtr;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0006442C File Offset: 0x0006262C
		internal SafeCertSelectCritera()
			: base(true)
		{
			UnsafeNclNativeMethods.NativePKI.CERT_SELECT_CRITERIA cert_SELECT_CRITERIA = default(UnsafeNclNativeMethods.NativePKI.CERT_SELECT_CRITERIA);
			this.unmanagedMemoryList = new List<IntPtr>();
			IntPtr intPtr = this.AllocBuffer(2 * Marshal.SizeOf(cert_SELECT_CRITERIA));
			base.SetHandle(intPtr);
			cert_SELECT_CRITERIA.dwType = 1U;
			cert_SELECT_CRITERIA.cPara = 1U;
			IntPtr intPtr2 = this.AllocString("1.3.6.1.5.5.7.3.2");
			IntPtr intPtr3 = this.AllocBuffer(Marshal.SizeOf(intPtr2));
			Marshal.WriteIntPtr(intPtr3, intPtr2);
			cert_SELECT_CRITERIA.ppPara = intPtr3;
			Marshal.StructureToPtr(cert_SELECT_CRITERIA, intPtr, false);
			cert_SELECT_CRITERIA = default(UnsafeNclNativeMethods.NativePKI.CERT_SELECT_CRITERIA);
			cert_SELECT_CRITERIA.dwType = 2U;
			cert_SELECT_CRITERIA.cPara = 1U;
			UnsafeNclNativeMethods.NativePKI.CERT_EXTENSION cert_EXTENSION = default(UnsafeNclNativeMethods.NativePKI.CERT_EXTENSION);
			cert_EXTENSION.pszObjId = IntPtr.Zero;
			cert_EXTENSION.fCritical = 0U;
			cert_EXTENSION.Value.cbData = 1U;
			IntPtr intPtr4 = this.AllocBuffer(Marshal.SizeOf(128));
			Marshal.WriteByte(intPtr4, 128);
			cert_EXTENSION.Value.pbData = intPtr4;
			IntPtr intPtr5 = this.AllocBuffer(Marshal.SizeOf(cert_EXTENSION));
			Marshal.StructureToPtr(cert_EXTENSION, intPtr5, false);
			intPtr3 = this.AllocBuffer(Marshal.SizeOf(intPtr5));
			Marshal.WriteIntPtr(intPtr3, intPtr5);
			cert_SELECT_CRITERIA.ppPara = intPtr3;
			Marshal.StructureToPtr(cert_SELECT_CRITERIA, intPtr + Marshal.SizeOf(cert_SELECT_CRITERIA), false);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00064590 File Offset: 0x00062790
		public override string ToString()
		{
			return "0x" + base.DangerousGetHandle().ToString("x");
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000645BC File Offset: 0x000627BC
		protected override bool ReleaseHandle()
		{
			try
			{
				foreach (IntPtr intPtr in this.unmanagedMemoryList)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x04001531 RID: 5425
		private const string szOID_PKIX_KP_CLIENT_AUTH = "1.3.6.1.5.5.7.3.2";

		// Token: 0x04001532 RID: 5426
		private const int CERT_SELECT_BY_ENHKEY_USAGE = 1;

		// Token: 0x04001533 RID: 5427
		private const int CERT_SELECT_BY_KEY_USAGE = 2;

		// Token: 0x04001534 RID: 5428
		private const byte CERT_DIGITAL_SIGNATURE_KEY_USAGE = 128;

		// Token: 0x04001535 RID: 5429
		private const int criteriaCount = 2;

		// Token: 0x04001536 RID: 5430
		private List<IntPtr> unmanagedMemoryList;
	}
}
