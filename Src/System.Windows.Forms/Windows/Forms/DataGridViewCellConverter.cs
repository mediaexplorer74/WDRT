using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020001A6 RID: 422
	internal class DataGridViewCellConverter : ExpandableObjectConverter
	{
		// Token: 0x06001E2E RID: 7726 RVA: 0x0002792C File Offset: 0x00025B2C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x0008EE2C File Offset: 0x0008D02C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			DataGridViewCell dataGridViewCell = value as DataGridViewCell;
			if (destinationType == typeof(InstanceDescriptor) && dataGridViewCell != null)
			{
				ConstructorInfo constructor = dataGridViewCell.GetType().GetConstructor(new Type[0]);
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[0], false);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
