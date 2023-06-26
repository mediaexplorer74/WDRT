using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.DateTimeOffset" /> structures to and from various other representations.</summary>
	// Token: 0x02000538 RID: 1336
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DateTimeOffsetConverter : TypeConverter
	{
		/// <summary>Returns a value that indicates whether an object of the specified source type can be converted to a <see cref="T:System.DateTimeOffset" />.</summary>
		/// <param name="context">The date format context.</param>
		/// <param name="sourceType">The source type to check.</param>
		/// <returns>
		///   <see langword="true" /> if the specified type can be converted to a <see cref="T:System.DateTimeOffset" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600325B RID: 12891 RVA: 0x000E169E File Offset: 0x000DF89E
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns a value that indicates whether a <see cref="T:System.DateTimeOffset" /> can be converted to an object of the specified type.</summary>
		/// <param name="context">The date format context.</param>
		/// <param name="destinationType">The destination type to check.</param>
		/// <returns>
		///   <see langword="true" /> if a <see cref="T:System.DateTimeOffset" /> can be converted to the specified type; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600325C RID: 12892 RVA: 0x000E16BC File Offset: 0x000DF8BC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to a <see cref="T:System.DateTimeOffset" />.</summary>
		/// <param name="context">The date format context.</param>
		/// <param name="culture">The date culture.</param>
		/// <param name="value">The object to be converted.</param>
		/// <returns>A <see cref="T:System.DateTimeOffset" /> that represents the specified object.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x0600325D RID: 12893 RVA: 0x000E16DC File Offset: 0x000DF8DC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (text.Length == 0)
				{
					return DateTimeOffset.MinValue;
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
						return DateTimeOffset.Parse(text, dateTimeFormatInfo);
					}
					return DateTimeOffset.Parse(text, culture);
				}
				catch (FormatException ex)
				{
					throw new FormatException(SR.GetString("ConvertInvalidPrimitive", new object[]
					{
						(string)value,
						"DateTimeOffset"
					}), ex);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts a <see cref="T:System.DateTimeOffset" /> to an object of the specified type.</summary>
		/// <param name="context">The date format context.</param>
		/// <param name="culture">The date culture.</param>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> to be converted.</param>
		/// <param name="destinationType">The type to convert to.</param>
		/// <returns>An object of the specified type that represents the <see cref="T:System.DateTimeOffset" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x0600325E RID: 12894 RVA: 0x000E1794 File Offset: 0x000DF994
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)) || !(value is DateTimeOffset))
			{
				if (destinationType == typeof(InstanceDescriptor) && value is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
					if (dateTimeOffset.Ticks == 0L)
					{
						ConstructorInfo constructor = typeof(DateTimeOffset).GetConstructor(new Type[] { typeof(long) });
						if (constructor != null)
						{
							return new InstanceDescriptor(constructor, new object[] { dateTimeOffset.Ticks });
						}
					}
					ConstructorInfo constructor2 = typeof(DateTimeOffset).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(TimeSpan)
					});
					if (constructor2 != null)
					{
						return new InstanceDescriptor(constructor2, new object[] { dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second, dateTimeOffset.Millisecond, dateTimeOffset.Offset });
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			DateTimeOffset dateTimeOffset2 = (DateTimeOffset)value;
			if (dateTimeOffset2 == DateTimeOffset.MinValue)
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
				if (dateTimeOffset2.TimeOfDay.TotalSeconds == 0.0)
				{
					text = dateTimeFormatInfo.ShortDatePattern + " zzz";
				}
				else
				{
					text = dateTimeFormatInfo.ShortDatePattern + " " + dateTimeFormatInfo.ShortTimePattern + " zzz";
				}
				return dateTimeOffset2.ToString(text, CultureInfo.CurrentCulture);
			}
			if (dateTimeOffset2.TimeOfDay.TotalSeconds == 0.0)
			{
				return dateTimeOffset2.ToString("yyyy-MM-dd zzz", culture);
			}
			return dateTimeOffset2.ToString(culture);
		}
	}
}
