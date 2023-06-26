using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Management
{
	// Token: 0x02000034 RID: 52
	internal class ManagementPathConverter : ExpandableObjectConverter
	{
		// Token: 0x060001BD RID: 445 RVA: 0x0000988F File Offset: 0x00007A8F
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(ManagementPath) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000098AD File Offset: 0x00007AAD
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000098CC File Offset: 0x00007ACC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value is ManagementPath && destinationType == typeof(InstanceDescriptor))
			{
				ManagementPath managementPath = (ManagementPath)value;
				ConstructorInfo constructor = typeof(ManagementPath).GetConstructor(new Type[] { typeof(string) });
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[] { managementPath.Path });
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
