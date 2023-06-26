using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert collection objects to and from various other representations.</summary>
	// Token: 0x02000527 RID: 1319
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class CollectionConverter : TypeConverter
	{
		/// <summary>Converts the given value object to the specified destination type.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The culture to which <paramref name="value" /> will be converted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. This parameter must inherit from <see cref="T:System.Collections.ICollection" />.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x060031EA RID: 12778 RVA: 0x000DFFDC File Offset: 0x000DE1DC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is ICollection)
			{
				return SR.GetString("CollectionConverterText");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Gets a collection of properties for the type of array specified by the value parameter using the specified context and attributes.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array to get the properties for.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that will be used as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or <see langword="null" /> if there are no properties. This method always returns <see langword="null" />.</returns>
		// Token: 0x060031EB RID: 12779 RVA: 0x000E002F File Offset: 0x000DE22F
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return null;
		}

		/// <summary>Gets a value indicating whether this object supports properties.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="false" /> because <see cref="M:System.ComponentModel.CollectionConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> should not be called to find the properties of this object. This method never returns <see langword="true" />.</returns>
		// Token: 0x060031EC RID: 12780 RVA: 0x000E0032 File Offset: 0x000DE232
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
