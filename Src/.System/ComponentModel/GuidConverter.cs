using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Guid" /> objects to and from various other representations.</summary>
	// Token: 0x02000554 RID: 1364
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class GuidConverter : TypeConverter
	{
		/// <summary>Gets a value indicating whether this converter can convert an object in the given source type to a GUID object using the context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003343 RID: 13123 RVA: 0x000E366C File Offset: 0x000E186C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003344 RID: 13124 RVA: 0x000E368A File Offset: 0x000E188A
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given object to a GUID object.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">An optional <see cref="T:System.Globalization.CultureInfo" />. If not supplied, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003345 RID: 13125 RVA: 0x000E36A8 File Offset: 0x000E18A8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				return new Guid(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the given object to another type.</summary>
		/// <param name="context">A formatter context.</param>
		/// <param name="culture">The culture into which <paramref name="value" /> will be converted.</param>
		/// <param name="value">The object to convert.</param>
		/// <param name="destinationType">The type to convert the object to.</param>
		/// <returns>The converted object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003346 RID: 13126 RVA: 0x000E36E0 File Offset: 0x000E18E0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Guid)
			{
				ConstructorInfo constructor = typeof(Guid).GetConstructor(new Type[] { typeof(string) });
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[] { value.ToString() });
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
