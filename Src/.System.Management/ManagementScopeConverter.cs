using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Management
{
	// Token: 0x02000045 RID: 69
	internal class ManagementScopeConverter : ExpandableObjectConverter
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000DB2D File Offset: 0x0000BD2D
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(ManagementScope) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000098AD File Offset: 0x00007AAD
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value is ManagementScope && destinationType == typeof(InstanceDescriptor))
			{
				ManagementScope managementScope = (ManagementScope)value;
				ConstructorInfo constructor = typeof(ManagementScope).GetConstructor(new Type[] { typeof(string) });
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[] { managementScope.Path.Path });
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
