using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020002D0 RID: 720
	internal class ListViewSubItemConverter : ExpandableObjectConverter
	{
		// Token: 0x06002CB2 RID: 11442 RVA: 0x0002792C File Offset: 0x00025B2C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000C8D0C File Offset: 0x000C6F0C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is ListViewItem.ListViewSubItem)
			{
				ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)value;
				ConstructorInfo constructorInfo;
				if (listViewSubItem.CustomStyle)
				{
					constructorInfo = typeof(ListViewItem.ListViewSubItem).GetConstructor(new Type[]
					{
						typeof(ListViewItem),
						typeof(string),
						typeof(Color),
						typeof(Color),
						typeof(Font)
					});
					if (constructorInfo != null)
					{
						return new InstanceDescriptor(constructorInfo, new object[] { null, listViewSubItem.Text, listViewSubItem.ForeColor, listViewSubItem.BackColor, listViewSubItem.Font }, true);
					}
				}
				constructorInfo = typeof(ListViewItem.ListViewSubItem).GetConstructor(new Type[]
				{
					typeof(ListViewItem),
					typeof(string)
				});
				if (constructorInfo != null)
				{
					return new InstanceDescriptor(constructorInfo, new object[] { null, listViewSubItem.Text }, true);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
