using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Decimal" /> objects to and from various other representations.</summary>
	// Token: 0x02000539 RID: 1337
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DecimalConverter : BaseNumberConverter
	{
		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x000E1A30 File Offset: 0x000DFC30
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x000E1A33 File Offset: 0x000DFC33
		internal override Type TargetType
		{
			get
			{
				return typeof(decimal);
			}
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003262 RID: 12898 RVA: 0x000E1A3F File Offset: 0x000DFC3F
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given value object to a <see cref="T:System.Decimal" /> using the arguments.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">An optional <see cref="T:System.Globalization.CultureInfo" />. If not supplied, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003263 RID: 12899 RVA: 0x000E1A60 File Offset: 0x000DFC60
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(InstanceDescriptor)) || !(value is decimal))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			object[] array = new object[] { decimal.GetBits((decimal)value) };
			MemberInfo constructor = typeof(decimal).GetConstructor(new Type[] { typeof(int[]) });
			if (constructor != null)
			{
				return new InstanceDescriptor(constructor, array);
			}
			return null;
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000E1AF3 File Offset: 0x000DFCF3
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDecimal(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000E1B05 File Offset: 0x000DFD05
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return decimal.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x000E1B18 File Offset: 0x000DFD18
		internal override object FromString(string value, CultureInfo culture)
		{
			return decimal.Parse(value, culture);
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000E1B28 File Offset: 0x000DFD28
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((decimal)value).ToString("G", formatInfo);
		}
	}
}
