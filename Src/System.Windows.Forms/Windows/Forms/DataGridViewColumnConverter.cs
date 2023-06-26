using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020001BF RID: 447
	internal class DataGridViewColumnConverter : ExpandableObjectConverter
	{
		// Token: 0x06001F9F RID: 8095 RVA: 0x0002792C File Offset: 0x00025B2C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x00095828 File Offset: 0x00093A28
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			DataGridViewColumn dataGridViewColumn = value as DataGridViewColumn;
			if (destinationType == typeof(InstanceDescriptor) && dataGridViewColumn != null)
			{
				ConstructorInfo constructorInfo;
				if (dataGridViewColumn.CellType != null)
				{
					constructorInfo = dataGridViewColumn.GetType().GetConstructor(new Type[] { typeof(Type) });
					if (constructorInfo != null)
					{
						return new InstanceDescriptor(constructorInfo, new object[] { dataGridViewColumn.CellType }, false);
					}
				}
				constructorInfo = dataGridViewColumn.GetType().GetConstructor(new Type[0]);
				if (constructorInfo != null)
				{
					return new InstanceDescriptor(constructorInfo, new object[0], false);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
