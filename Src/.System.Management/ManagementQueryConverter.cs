using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Management
{
	// Token: 0x0200003D RID: 61
	internal class ManagementQueryConverter : ExpandableObjectConverter
	{
		// Token: 0x0600023A RID: 570 RVA: 0x0000BE3B File Offset: 0x0000A03B
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(ManagementQuery) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000098AD File Offset: 0x00007AAD
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000BE5C File Offset: 0x0000A05C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value is EventQuery && destinationType == typeof(InstanceDescriptor))
			{
				EventQuery eventQuery = (EventQuery)value;
				ConstructorInfo constructor = typeof(EventQuery).GetConstructor(new Type[] { typeof(string) });
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[] { eventQuery.QueryString });
				}
			}
			if (value is ObjectQuery && destinationType == typeof(InstanceDescriptor))
			{
				ObjectQuery objectQuery = (ObjectQuery)value;
				ConstructorInfo constructor2 = typeof(ObjectQuery).GetConstructor(new Type[] { typeof(string) });
				if (constructor2 != null)
				{
					return new InstanceDescriptor(constructor2, new object[] { objectQuery.QueryString });
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
