using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert string objects to and from other representations.</summary>
	// Token: 0x020005AC RID: 1452
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class StringConverter : TypeConverter
	{
		/// <summary>Gets a value indicating whether this converter can convert an object in the given source type to a string using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003616 RID: 13846 RVA: 0x000EBE05 File Offset: 0x000EA005
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the specified value object to a <see cref="T:System.String" /> object.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion could not be performed.</exception>
		// Token: 0x06003617 RID: 13847 RVA: 0x000EBE23 File Offset: 0x000EA023
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return (string)value;
			}
			if (value == null)
			{
				return "";
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
