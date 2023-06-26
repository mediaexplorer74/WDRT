using System;
using System.Drawing;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000491 RID: 1169
	internal class Com2ColorConverter : Com2DataTypeToManagedDataTypeConverter
	{
		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06004E58 RID: 20056 RVA: 0x00142983 File Offset: 0x00140B83
		public override Type ManagedType
		{
			get
			{
				return typeof(Color);
			}
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x00142990 File Offset: 0x00140B90
		public override object ConvertNativeToManaged(object nativeValue, Com2PropertyDescriptor pd)
		{
			int num = 0;
			if (nativeValue is uint)
			{
				num = (int)((uint)nativeValue);
			}
			else if (nativeValue is int)
			{
				num = (int)nativeValue;
			}
			return ColorTranslator.FromOle(num);
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x001429CC File Offset: 0x00140BCC
		public override object ConvertManagedToNative(object managedValue, Com2PropertyDescriptor pd, ref bool cancelSet)
		{
			cancelSet = false;
			if (managedValue == null)
			{
				managedValue = Color.Black;
			}
			if (managedValue is Color)
			{
				return ColorTranslator.ToOle((Color)managedValue);
			}
			return 0;
		}
	}
}
