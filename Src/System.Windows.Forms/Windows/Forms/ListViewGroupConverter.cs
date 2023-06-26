using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020002D6 RID: 726
	internal class ListViewGroupConverter : TypeConverter
	{
		// Token: 0x06002E0B RID: 11787 RVA: 0x000D118F File Offset: 0x000CF38F
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (sourceType == typeof(string) && context != null && context.Instance is ListViewItem) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000D11C0 File Offset: 0x000CF3C0
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || (destinationType == typeof(string) && context != null && context.Instance is ListViewItem) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000D1210 File Offset: 0x000CF410
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (context != null && context.Instance != null)
				{
					ListViewItem listViewItem = context.Instance as ListViewItem;
					if (listViewItem != null && listViewItem.ListView != null)
					{
						foreach (object obj in listViewItem.ListView.Groups)
						{
							ListViewGroup listViewGroup = (ListViewGroup)obj;
							if (listViewGroup.Header == text)
							{
								return listViewGroup;
							}
						}
					}
				}
			}
			if (value == null || value.Equals(SR.GetString("toStringNone")))
			{
				return null;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000D12D8 File Offset: 0x000CF4D8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is ListViewGroup)
			{
				ListViewGroup listViewGroup = (ListViewGroup)value;
				ConstructorInfo constructor = typeof(ListViewGroup).GetConstructor(new Type[]
				{
					typeof(string),
					typeof(HorizontalAlignment)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[] { listViewGroup.Header, listViewGroup.HeaderAlignment }, false);
				}
			}
			if (destinationType == typeof(string) && value == null)
			{
				return SR.GetString("toStringNone");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000D13A8 File Offset: 0x000CF5A8
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context != null && context.Instance != null)
			{
				ListViewItem listViewItem = context.Instance as ListViewItem;
				if (listViewItem != null && listViewItem.ListView != null)
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj in listViewItem.ListView.Groups)
					{
						ListViewGroup listViewGroup = (ListViewGroup)obj;
						arrayList.Add(listViewGroup);
					}
					arrayList.Add(null);
					return new TypeConverter.StandardValuesCollection(arrayList);
				}
			}
			return null;
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
