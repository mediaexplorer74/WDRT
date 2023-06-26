using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.SelectionRange" /> objects to and from various other types.</summary>
	// Token: 0x02000366 RID: 870
	public class SelectionRangeConverter : TypeConverter
	{
		/// <summary>Determines if this converter can convert an object of the specified source type to the native type of the converter by querying the supplied type descriptor context.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">The source <see cref="T:System.Type" /> to be converted.</param>
		/// <returns>
		///   <see langword="true" /> if the converter can perform the specified conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600388B RID: 14475 RVA: 0x000FA9AA File Offset: 0x000F8BAA
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(DateTime) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the specified destination type by using the specified context.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">The destination <see cref="T:System.Type" /> to convert into.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the specified conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600388C RID: 14476 RVA: 0x000FA9DA File Offset: 0x000F8BDA
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(DateTime) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified value to the converter's native type by using the specified locale.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The locale information for the conversion.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of type <see cref="T:System.String" /> but could not be parsed into two strings representing dates.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is of type <see cref="T:System.String" /> that was parsed into two strings, but the conversion of one or both into the <see cref="T:System.DateTime" /> type did not succeed.</exception>
		// Token: 0x0600388D RID: 14477 RVA: 0x000FAA0C File Offset: 0x000F8C0C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (text.Length == 0)
				{
					return new SelectionRange(DateTime.Now.Date, DateTime.Now.Date);
				}
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				char c = culture.TextInfo.ListSeparator[0];
				string[] array = text.Split(new char[] { c });
				if (array.Length == 2)
				{
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(DateTime));
					DateTime dateTime = (DateTime)converter.ConvertFromString(context, culture, array[0]);
					DateTime dateTime2 = (DateTime)converter.ConvertFromString(context, culture, array[1]);
					return new SelectionRange(dateTime, dateTime2);
				}
				throw new ArgumentException(SR.GetString("TextParseFailedFormat", new object[]
				{
					text,
					"Start" + c.ToString() + " End"
				}));
			}
			else
			{
				if (value is DateTime)
				{
					DateTime dateTime3 = (DateTime)value;
					return new SelectionRange(dateTime3, dateTime3);
				}
				return base.ConvertFrom(context, culture, value);
			}
		}

		/// <summary>Converts the specified <see cref="T:System.Windows.Forms.SelectionRangeConverter" /> object to another type by using the specified culture.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The locale information for the conversion.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The destination <see cref="T:System.Type" /> to convert into.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> cannot be converted to the <paramref name="destinationType" />.</exception>
		// Token: 0x0600388E RID: 14478 RVA: 0x000FAB24 File Offset: 0x000F8D24
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			SelectionRange selectionRange = value as SelectionRange;
			if (selectionRange != null)
			{
				if (destinationType == typeof(string))
				{
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					string text = culture.TextInfo.ListSeparator + " ";
					PropertyDescriptorCollection properties = base.GetProperties(value);
					string[] array = new string[properties.Count];
					for (int i = 0; i < properties.Count; i++)
					{
						object value2 = properties[i].GetValue(value);
						array[i] = TypeDescriptor.GetConverter(value2).ConvertToString(context, culture, value2);
					}
					return string.Join(text, array);
				}
				if (destinationType == typeof(DateTime))
				{
					return selectionRange.Start;
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo constructor = typeof(SelectionRange).GetConstructor(new Type[]
					{
						typeof(DateTime),
						typeof(DateTime)
					});
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[] { selectionRange.Start, selectionRange.End });
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Creates a <see cref="T:System.Windows.Forms.SelectionRange" /> object by using the specified type descriptor and set of property values for that object.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> that contains the new property values.</param>
		/// <returns>If successful, the newly created <see cref="T:System.Windows.Forms.SelectionRange" />; otherwise, this method throws an exception.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyValues" /> is <see langword="null" /> or its <c>Start</c> and <c>End</c> elements could not be converted into a <see cref="T:System.Windows.Forms.SelectionRange" />.</exception>
		// Token: 0x0600388F RID: 14479 RVA: 0x000FAC80 File Offset: 0x000F8E80
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			object obj;
			try
			{
				obj = new SelectionRange((DateTime)propertyValues["Start"], (DateTime)propertyValues["End"]);
			}
			catch (InvalidCastException ex)
			{
				throw new ArgumentException(SR.GetString("PropertyValueInvalidEntry"), ex);
			}
			catch (NullReferenceException ex2)
			{
				throw new ArgumentException(SR.GetString("PropertyValueInvalidEntry"), ex2);
			}
			return obj;
		}

		/// <summary>Determines if changing a value on an instance should require a call to <see cref="Overload:System.Windows.Forms.SelectionRangeConverter.CreateInstance" /> to create a new value.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if <see cref="Overload:System.Windows.Forms.SelectionRangeConverter.CreateInstance" /> must be called to make a change to one or more properties; otherwise <see langword="false" />.</returns>
		// Token: 0x06003890 RID: 14480 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Returns the set of filtered properties for the <see cref="T:System.Windows.Forms.SelectionRange" /> type</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <returns>If successful, the set of properties that should be exposed for the <see cref="T:System.Windows.Forms.SelectionRange" /> type; otherwise, <see langword="null" />.</returns>
		// Token: 0x06003891 RID: 14481 RVA: 0x000FACF8 File Offset: 0x000F8EF8
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(SelectionRange), attributes);
			return properties.Sort(new string[] { "Start", "End" });
		}

		/// <summary>Determines whether the current object supports properties that use the specified type descriptor context.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if <see cref="Overload:System.Windows.Forms.SelectionRangeConverter.GetProperties" /> can be called to find the properties of a <see cref="T:System.Windows.Forms.SelectionRange" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003892 RID: 14482 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
