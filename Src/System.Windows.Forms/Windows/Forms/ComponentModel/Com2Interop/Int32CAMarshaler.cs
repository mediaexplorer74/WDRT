using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004B5 RID: 1205
	internal class Int32CAMarshaler : BaseCAMarshaler
	{
		// Token: 0x06004F77 RID: 20343 RVA: 0x00147DCD File Offset: 0x00145FCD
		public Int32CAMarshaler(NativeMethods.CA_STRUCT caStruct)
			: base(caStruct)
		{
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06004F78 RID: 20344 RVA: 0x00147DD6 File Offset: 0x00145FD6
		public override Type ItemType
		{
			get
			{
				return typeof(int);
			}
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x00147DE2 File Offset: 0x00145FE2
		protected override Array CreateArray()
		{
			return new int[base.Count];
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x00147DEF File Offset: 0x00145FEF
		protected override object UnmarshalAndFreeOneItem(IntPtr arrayAddr, int itemIndex)
		{
			return Marshal.ReadInt32(arrayAddr, itemIndex * 4);
		}
	}
}
