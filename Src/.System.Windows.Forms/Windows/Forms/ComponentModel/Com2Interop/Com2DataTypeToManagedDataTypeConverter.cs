using System;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000493 RID: 1171
	internal abstract class Com2DataTypeToManagedDataTypeConverter
	{
		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06004E5F RID: 20063 RVA: 0x0001180C File Offset: 0x0000FA0C
		public virtual bool AllowExpand
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06004E60 RID: 20064
		public abstract Type ManagedType { get; }

		// Token: 0x06004E61 RID: 20065
		public abstract object ConvertNativeToManaged(object nativeValue, Com2PropertyDescriptor pd);

		// Token: 0x06004E62 RID: 20066
		public abstract object ConvertManagedToNative(object managedValue, Com2PropertyDescriptor pd, ref bool cancelSet);
	}
}
