using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.DateTime" /> objects to and from various other representations.</summary>
	// Token: 0x02000537 RID: 1335
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DateTimeConverter : TypeConverter
	{
		/// <summary>Gets a value indicating whether this converter can convert an object in the given source type to a <see cref="T:System.DateTime" /> using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if this object can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003256 RID: 12886 RVA: 0x000E1339 File Offset: 0x000DF539
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003257 RID: 12887 RVA: 0x000E1357 File Offset: 0x000DF557
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given value object to a <see cref="T:System.DateTime" />.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">An optional <see cref="T:System.Globalization.CultureInfo" />. If not supplied, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a valid value for the target type.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003258 RID: 12888 RVA: 0x000E1378 File Offset: 0x000DF578
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (text.Length == 0)
				{
					return DateTime.MinValue;
				}
				try
				{
					DateTimeFormatInfo dateTimeFormatInfo = null;
					if (culture != null)
					{
						dateTimeFormatInfo = (DateTimeFormatInfo)culture.GetFormat(typeof(DateTimeFormatInfo));
					}
					if (dateTimeFormatInfo != null)
					{
						return DateTime.Parse(text, dateTimeFormatInfo);
					}
					return DateTime.Parse(text, culture);
				}
				catch (FormatException ex)
				{
					throw new FormatException(SR.GetString("ConvertInvalidPrimitive", new object[]
					{
						(string)value,
						"DateTime"
					}), ex);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the given value object to a <see cref="T:System.DateTime" /> using the arguments.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">An optional <see cref="T:System.Globalization.CultureInfo" />. If not supplied, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003259 RID: 12889 RVA: 0x000E1430 File Offset: 0x000DF630
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)) || !(value is DateTime))
			{
				if (destinationType == typeof(InstanceDescriptor) && value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					if (dateTime.Ticks == 0L)
					{
						ConstructorInfo constructor = typeof(DateTime).GetConstructor(new Type[] { typeof(long) });
						if (constructor != null)
						{
							return new InstanceDescriptor(constructor, new object[] { dateTime.Ticks });
						}
					}
					ConstructorInfo constructor2 = typeof(DateTime).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int)
					});
					if (constructor2 != null)
					{
						return new InstanceDescriptor(constructor2, new object[] { dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond });
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			DateTime dateTime2 = (DateTime)value;
			if (dateTime2 == DateTime.MinValue)
			{
				return string.Empty;
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)culture.GetFormat(typeof(DateTimeFormatInfo));
			if (culture != CultureInfo.InvariantCulture)
			{
				string text;
				if (dateTime2.TimeOfDay.TotalSeconds == 0.0)
				{
					text = dateTimeFormatInfo.ShortDatePattern;
				}
				else
				{
					text = dateTimeFormatInfo.ShortDatePattern + " " + dateTimeFormatInfo.ShortTimePattern;
				}
				return dateTime2.ToString(text, CultureInfo.CurrentCulture);
			}
			if (dateTime2.TimeOfDay.TotalSeconds == 0.0)
			{
				return dateTime2.ToString("yyyy-MM-dd", culture);
			}
			return dateTime2.ToString(culture);
		}
	}
}
