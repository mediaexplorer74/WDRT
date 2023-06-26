using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System
{
	/// <summary>Converts a <see cref="T:System.String" /> type to a <see cref="T:System.Uri" /> type, and vice versa.</summary>
	// Token: 0x02000047 RID: 71
	public class UriTypeConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UriTypeConverter" /> class.</summary>
		// Token: 0x060003E2 RID: 994 RVA: 0x0001BE0B File Offset: 0x0001A00B
		public UriTypeConverter()
			: this(UriKind.RelativeOrAbsolute)
		{
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001BE14 File Offset: 0x0001A014
		internal UriTypeConverter(UriKind uriKind)
		{
			this.m_UriKind = uriKind;
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type that you want to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="sourceType" /> is a <see cref="T:System.String" /> type or a <see cref="T:System.Uri" /> type can be assigned from <paramref name="sourceType" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceType" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060003E4 RID: 996 RVA: 0x0001BE24 File Offset: 0x0001A024
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == null)
			{
				throw new ArgumentNullException("sourceType");
			}
			return sourceType == typeof(string) || typeof(Uri).IsAssignableFrom(sourceType) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type that you want to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="destinationType" /> is of type <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" />, <see cref="T:System.String" />, or <see cref="T:System.Uri" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003E5 RID: 997 RVA: 0x0001BE78 File Offset: 0x0001A078
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string) || destinationType == typeof(Uri) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x060003E6 RID: 998 RVA: 0x0001BECC File Offset: 0x0001A0CC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				return new Uri(text, this.m_UriKind);
			}
			Uri uri = value as Uri;
			if (uri != null)
			{
				return new Uri(uri.OriginalString, (this.m_UriKind == UriKind.RelativeOrAbsolute) ? (uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative) : this.m_UriKind);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts a given value object to the specified type, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If <see langword="null" /> is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x060003E7 RID: 999 RVA: 0x0001BF34 File Offset: 0x0001A134
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			Uri uri = value as Uri;
			if (uri != null && destinationType == typeof(InstanceDescriptor))
			{
				ConstructorInfo constructor = typeof(Uri).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[]
				{
					typeof(string),
					typeof(UriKind)
				}, null);
				return new InstanceDescriptor(constructor, new object[]
				{
					uri.OriginalString,
					(this.m_UriKind == UriKind.RelativeOrAbsolute) ? (uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative) : this.m_UriKind
				});
			}
			if (uri != null && destinationType == typeof(string))
			{
				return uri.OriginalString;
			}
			if (uri != null && destinationType == typeof(Uri))
			{
				return new Uri(uri.OriginalString, (this.m_UriKind == UriKind.RelativeOrAbsolute) ? (uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative) : this.m_UriKind);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns whether the given value object is a <see cref="T:System.Uri" /> or a <see cref="T:System.Uri" /> can be created from it.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is a <see cref="T:System.Uri" /> or a <see cref="T:System.String" /> from which a <see cref="T:System.Uri" /> can be created; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003E8 RID: 1000 RVA: 0x0001C048 File Offset: 0x0001A248
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			string text = value as string;
			if (text != null)
			{
				Uri uri;
				return Uri.TryCreate(text, this.m_UriKind, out uri);
			}
			return value is Uri;
		}

		// Token: 0x04000476 RID: 1142
		private UriKind m_UriKind;
	}
}
