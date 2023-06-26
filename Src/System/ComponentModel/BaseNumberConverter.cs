using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a base type converter for nonfloating-point numerical types.</summary>
	// Token: 0x02000518 RID: 1304
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class BaseNumberConverter : TypeConverter
	{
		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000DF09A File Offset: 0x000DD29A
		internal virtual bool AllowHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06003160 RID: 12640
		internal abstract Type TargetType { get; }

		// Token: 0x06003161 RID: 12641
		internal abstract object FromString(string value, int radix);

		// Token: 0x06003162 RID: 12642
		internal abstract object FromString(string value, NumberFormatInfo formatInfo);

		// Token: 0x06003163 RID: 12643
		internal abstract object FromString(string value, CultureInfo culture);

		// Token: 0x06003164 RID: 12644 RVA: 0x000DF09D File Offset: 0x000DD29D
		internal virtual Exception FromStringError(string failedText, Exception innerException)
		{
			return new Exception(SR.GetString("ConvertInvalidPrimitive", new object[]
			{
				failedText,
				this.TargetType.Name
			}), innerException);
		}

		// Token: 0x06003165 RID: 12645
		internal abstract string ToString(object value, NumberFormatInfo formatInfo);

		/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type from which you want to convert.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the operation; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003166 RID: 12646 RVA: 0x000DF0C7 File Offset: 0x000DD2C7
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the given object to the converter's native type.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the number.</param>
		/// <param name="value">The object to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.Exception">
		///   <paramref name="value" /> is not a valid value for the target type.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003167 RID: 12647 RVA: 0x000DF0E8 File Offset: 0x000DD2E8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				try
				{
					if (this.AllowHex && text[0] == '#')
					{
						return this.FromString(text.Substring(1), 16);
					}
					if ((this.AllowHex && text.StartsWith("0x")) || text.StartsWith("0X") || text.StartsWith("&h") || text.StartsWith("&H"))
					{
						return this.FromString(text.Substring(2), 16);
					}
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					NumberFormatInfo numberFormatInfo = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
					return this.FromString(text, numberFormatInfo);
				}
				catch (Exception ex)
				{
					throw this.FromStringError(text, ex);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the specified object to another type.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the number.</param>
		/// <param name="value">The object to convert.</param>
		/// <param name="destinationType">The type to convert the object to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003168 RID: 12648 RVA: 0x000DF1D4 File Offset: 0x000DD3D4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null && this.TargetType.IsInstanceOfType(value))
			{
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				NumberFormatInfo numberFormatInfo = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
				return this.ToString(value, numberFormatInfo);
			}
			if (destinationType.IsPrimitive)
			{
				return Convert.ChangeType(value, destinationType, culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="t">A <see cref="T:System.Type" /> that represents the type to which you want to convert.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the operation; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003169 RID: 12649 RVA: 0x000DF261 File Offset: 0x000DD461
		public override bool CanConvertTo(ITypeDescriptorContext context, Type t)
		{
			return base.CanConvertTo(context, t) || t.IsPrimitive;
		}
	}
}
