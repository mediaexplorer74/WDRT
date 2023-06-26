using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004B6 RID: 1206
	internal class OleStrCAMarshaler : BaseCAMarshaler
	{
		// Token: 0x06004F7B RID: 20347 RVA: 0x00147DCD File Offset: 0x00145FCD
		public OleStrCAMarshaler(NativeMethods.CA_STRUCT caAddr)
			: base(caAddr)
		{
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06004F7C RID: 20348 RVA: 0x00142943 File Offset: 0x00140B43
		public override Type ItemType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x00147DFF File Offset: 0x00145FFF
		protected override Array CreateArray()
		{
			return new string[base.Count];
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x00147E0C File Offset: 0x0014600C
		protected override object UnmarshalAndFreeOneItem(IntPtr arrayAddr, int itemIndex)
		{
			IntPtr intPtr = Marshal.ReadIntPtr(arrayAddr, itemIndex * IntPtr.Size);
			string text = Marshal.PtrToStringUni(intPtr);
			Marshal.FreeCoTaskMem(intPtr);
			return text;
		}
	}
}
