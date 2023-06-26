using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x0200020A RID: 522
	internal class DataGridViewRowConverter : ExpandableObjectConverter
	{
		// Token: 0x06002271 RID: 8817 RVA: 0x0002792C File Offset: 0x00025B2C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000A4E58 File Offset: 0x000A3058
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			DataGridViewRow dataGridViewRow = value as DataGridViewRow;
			if (destinationType == typeof(InstanceDescriptor) && dataGridViewRow != null)
			{
				ConstructorInfo constructor = dataGridViewRow.GetType().GetConstructor(new Type[0]);
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[0], false);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
