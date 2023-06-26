using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Configuration;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a unified way of converting types of values to other types, as well as for accessing standard values and subproperties.</summary>
	// Token: 0x020005B1 RID: 1457
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class TypeConverter
	{
		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x000EC130 File Offset: 0x000EA330
		private static bool UseCompatibleTypeConversion
		{
			get
			{
				if (TypeConverter.firstLoadAppSetting)
				{
					object obj = TypeConverter.loadAppSettingLock;
					lock (obj)
					{
						if (TypeConverter.firstLoadAppSetting)
						{
							string text = ConfigurationManager.AppSettings["UseCompatibleTypeConverterBehavior"];
							try
							{
								if (!string.IsNullOrEmpty(text))
								{
									TypeConverter.useCompatibleTypeConversion = bool.Parse(text.Trim());
								}
							}
							catch
							{
								TypeConverter.useCompatibleTypeConversion = false;
							}
							TypeConverter.firstLoadAppSetting = false;
						}
					}
				}
				return TypeConverter.useCompatibleTypeConversion;
			}
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter.</summary>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600362B RID: 13867 RVA: 0x000EC1D0 File Offset: 0x000EA3D0
		public bool CanConvertFrom(Type sourceType)
		{
			return this.CanConvertFrom(null, sourceType);
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600362C RID: 13868 RVA: 0x000EC1DA File Offset: 0x000EA3DA
		public virtual bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(InstanceDescriptor);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type.</summary>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600362D RID: 13869 RVA: 0x000EC1F1 File Offset: 0x000EA3F1
		public bool CanConvertTo(Type destinationType)
		{
			return this.CanConvertTo(null, destinationType);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600362E RID: 13870 RVA: 0x000EC1FB File Offset: 0x000EA3FB
		public virtual bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		/// <summary>Converts the given value to the type of this converter.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x0600362F RID: 13871 RVA: 0x000EC20D File Offset: 0x000EA40D
		public object ConvertFrom(object value)
		{
			return this.ConvertFrom(null, CultureInfo.CurrentCulture, value);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003630 RID: 13872 RVA: 0x000EC21C File Offset: 0x000EA41C
		public virtual object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			InstanceDescriptor instanceDescriptor = value as InstanceDescriptor;
			if (instanceDescriptor != null)
			{
				return instanceDescriptor.Invoke();
			}
			throw this.GetConvertFromException(value);
		}

		/// <summary>Converts the given string to the type of this converter, using the invariant culture.</summary>
		/// <param name="text">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003631 RID: 13873 RVA: 0x000EC241 File Offset: 0x000EA441
		public object ConvertFromInvariantString(string text)
		{
			return this.ConvertFromString(null, CultureInfo.InvariantCulture, text);
		}

		/// <summary>Converts the given string to the type of this converter, using the invariant culture and the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="text">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003632 RID: 13874 RVA: 0x000EC250 File Offset: 0x000EA450
		public object ConvertFromInvariantString(ITypeDescriptorContext context, string text)
		{
			return this.ConvertFromString(context, CultureInfo.InvariantCulture, text);
		}

		/// <summary>Converts the specified text to an object.</summary>
		/// <param name="text">The text representation of the object to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <exception cref="T:System.NotSupportedException">The string cannot be converted into the appropriate object.</exception>
		// Token: 0x06003633 RID: 13875 RVA: 0x000EC25F File Offset: 0x000EA45F
		public object ConvertFromString(string text)
		{
			return this.ConvertFrom(null, null, text);
		}

		/// <summary>Converts the given text to an object, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="text">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003634 RID: 13876 RVA: 0x000EC26A File Offset: 0x000EA46A
		public object ConvertFromString(ITypeDescriptorContext context, string text)
		{
			return this.ConvertFrom(context, CultureInfo.CurrentCulture, text);
		}

		/// <summary>Converts the given text to an object, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If <see langword="null" /> is passed, the current culture is assumed.</param>
		/// <param name="text">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003635 RID: 13877 RVA: 0x000EC279 File Offset: 0x000EA479
		public object ConvertFromString(ITypeDescriptorContext context, CultureInfo culture, string text)
		{
			return this.ConvertFrom(context, culture, text);
		}

		/// <summary>Converts the given value object to the specified type, using the arguments.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003636 RID: 13878 RVA: 0x000EC284 File Offset: 0x000EA484
		public object ConvertTo(object value, Type destinationType)
		{
			return this.ConvertTo(null, null, value, destinationType);
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If <see langword="null" /> is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003637 RID: 13879 RVA: 0x000EC290 File Offset: 0x000EA490
		public virtual object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				throw this.GetConvertToException(value, destinationType);
			}
			if (value == null)
			{
				return string.Empty;
			}
			if (culture != null && culture != CultureInfo.CurrentCulture)
			{
				IFormattable formattable = value as IFormattable;
				if (formattable != null)
				{
					return formattable.ToString(null, culture);
				}
			}
			return value.ToString();
		}

		/// <summary>Converts the specified value to a culture-invariant string representation.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>A <see cref="T:System.String" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003638 RID: 13880 RVA: 0x000EC2FC File Offset: 0x000EA4FC
		public string ConvertToInvariantString(object value)
		{
			return this.ConvertToString(null, CultureInfo.InvariantCulture, value);
		}

		/// <summary>Converts the specified value to a culture-invariant string representation, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>A <see cref="T:System.String" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003639 RID: 13881 RVA: 0x000EC30B File Offset: 0x000EA50B
		public string ConvertToInvariantString(ITypeDescriptorContext context, object value)
		{
			return this.ConvertToString(context, CultureInfo.InvariantCulture, value);
		}

		/// <summary>Converts the specified value to a string representation.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x0600363A RID: 13882 RVA: 0x000EC31A File Offset: 0x000EA51A
		public string ConvertToString(object value)
		{
			return (string)this.ConvertTo(null, CultureInfo.CurrentCulture, value, typeof(string));
		}

		/// <summary>Converts the given value to a string representation, using the given context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x0600363B RID: 13883 RVA: 0x000EC338 File Offset: 0x000EA538
		public string ConvertToString(ITypeDescriptorContext context, object value)
		{
			return (string)this.ConvertTo(context, CultureInfo.CurrentCulture, value, typeof(string));
		}

		/// <summary>Converts the given value to a string representation, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If <see langword="null" /> is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x0600363C RID: 13884 RVA: 0x000EC356 File Offset: 0x000EA556
		public string ConvertToString(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return (string)this.ConvertTo(context, culture, value, typeof(string));
		}

		/// <summary>Re-creates an <see cref="T:System.Object" /> given a set of property values for the object.</summary>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> that represents a dictionary of new property values.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or <see langword="null" /> if the object cannot be created. This method always returns <see langword="null" />.</returns>
		// Token: 0x0600363D RID: 13885 RVA: 0x000EC370 File Offset: 0x000EA570
		public object CreateInstance(IDictionary propertyValues)
		{
			return this.CreateInstance(null, propertyValues);
		}

		/// <summary>Creates an instance of the type that this <see cref="T:System.ComponentModel.TypeConverter" /> is associated with, using the specified context, given a set of property values for the object.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or <see langword="null" /> if the object cannot be created. This method always returns <see langword="null" />.</returns>
		// Token: 0x0600363E RID: 13886 RVA: 0x000EC37A File Offset: 0x000EA57A
		public virtual object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return null;
		}

		/// <summary>Returns an exception to throw when a conversion cannot be performed.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to convert, or <see langword="null" /> if the object is not available.</param>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception to throw when a conversion cannot be performed.</returns>
		/// <exception cref="T:System.NotSupportedException">Automatically thrown by this method.</exception>
		// Token: 0x0600363F RID: 13887 RVA: 0x000EC380 File Offset: 0x000EA580
		protected Exception GetConvertFromException(object value)
		{
			string text;
			if (value == null)
			{
				text = SR.GetString("ToStringNull");
			}
			else
			{
				text = value.GetType().FullName;
			}
			throw new NotSupportedException(SR.GetString("ConvertFromException", new object[]
			{
				base.GetType().Name,
				text
			}));
		}

		/// <summary>Returns an exception to throw when a conversion cannot be performed.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to convert, or <see langword="null" /> if the object is not available.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type the conversion was trying to convert to.</param>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception to throw when a conversion cannot be performed.</returns>
		/// <exception cref="T:System.NotSupportedException">Automatically thrown by this method.</exception>
		// Token: 0x06003640 RID: 13888 RVA: 0x000EC3D0 File Offset: 0x000EA5D0
		protected Exception GetConvertToException(object value, Type destinationType)
		{
			string text;
			if (value == null)
			{
				text = SR.GetString("ToStringNull");
			}
			else
			{
				text = value.GetType().FullName;
			}
			throw new NotSupportedException(SR.GetString("ConvertToException", new object[]
			{
				base.GetType().Name,
				text,
				destinationType.FullName
			}));
		}

		/// <summary>Returns whether changing a value on this object requires a call to the <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> method to create a new value.</summary>
		/// <returns>
		///   <see langword="true" /> if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003641 RID: 13889 RVA: 0x000EC429 File Offset: 0x000EA629
		public bool GetCreateInstanceSupported()
		{
			return this.GetCreateInstanceSupported(null);
		}

		/// <summary>Returns whether changing a value on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003642 RID: 13890 RVA: 0x000EC432 File Offset: 0x000EA632
		public virtual bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or <see langword="null" /> if there are no properties.</returns>
		// Token: 0x06003643 RID: 13891 RVA: 0x000EC435 File Offset: 0x000EA635
		public PropertyDescriptorCollection GetProperties(object value)
		{
			return this.GetProperties(null, value);
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or <see langword="null" /> if there are no properties.</returns>
		// Token: 0x06003644 RID: 13892 RVA: 0x000EC43F File Offset: 0x000EA63F
		public PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value)
		{
			return this.GetProperties(context, value, new Attribute[] { BrowsableAttribute.Yes });
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or <see langword="null" /> if there are no properties.</returns>
		// Token: 0x06003645 RID: 13893 RVA: 0x000EC457 File Offset: 0x000EA657
		public virtual PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return null;
		}

		/// <summary>Returns whether this object supports properties.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003646 RID: 13894 RVA: 0x000EC45A File Offset: 0x000EA65A
		public bool GetPropertiesSupported()
		{
			return this.GetPropertiesSupported(null);
		}

		/// <summary>Returns whether this object supports properties, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003647 RID: 13895 RVA: 0x000EC463 File Offset: 0x000EA663
		public virtual bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns a collection of standard values from the default context for the data type this type converter is designed for.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> containing a standard set of valid values, or <see langword="null" /> if the data type does not support a standard set of values.</returns>
		// Token: 0x06003648 RID: 13896 RVA: 0x000EC466 File Offset: 0x000EA666
		public ICollection GetStandardValues()
		{
			return this.GetStandardValues(null);
		}

		/// <summary>Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or <see langword="null" /> if the data type does not support a standard set of values.</returns>
		// Token: 0x06003649 RID: 13897 RVA: 0x000EC46F File Offset: 0x000EA66F
		public virtual TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return null;
		}

		/// <summary>Returns whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; <see langword="false" /> if other values are possible.</returns>
		// Token: 0x0600364A RID: 13898 RVA: 0x000EC472 File Offset: 0x000EA672
		public bool GetStandardValuesExclusive()
		{
			return this.GetStandardValuesExclusive(null);
		}

		/// <summary>Returns whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list of possible values, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; <see langword="false" /> if other values are possible.</returns>
		// Token: 0x0600364B RID: 13899 RVA: 0x000EC47B File Offset: 0x000EA67B
		public virtual bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns whether this object supports a standard set of values that can be picked from a list.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600364C RID: 13900 RVA: 0x000EC47E File Offset: 0x000EA67E
		public bool GetStandardValuesSupported()
		{
			return this.GetStandardValuesSupported(null);
		}

		/// <summary>Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600364D RID: 13901 RVA: 0x000EC487 File Offset: 0x000EA687
		public virtual bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns whether the given value object is valid for this type.</summary>
		/// <param name="value">The object to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the specified value is valid for this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600364E RID: 13902 RVA: 0x000EC48A File Offset: 0x000EA68A
		public bool IsValid(object value)
		{
			return this.IsValid(null, value);
		}

		/// <summary>Returns whether the given value object is valid for this type and for the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the specified value is valid for this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600364F RID: 13903 RVA: 0x000EC494 File Offset: 0x000EA694
		public virtual bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (TypeConverter.UseCompatibleTypeConversion)
			{
				return true;
			}
			bool flag = true;
			try
			{
				if (value == null || this.CanConvertFrom(context, value.GetType()))
				{
					this.ConvertFrom(context, CultureInfo.InvariantCulture, value);
				}
				else
				{
					flag = false;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		/// <summary>Sorts a collection of properties.</summary>
		/// <param name="props">A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that has the properties to sort.</param>
		/// <param name="names">An array of names in the order you want the properties to appear in the collection.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted properties.</returns>
		// Token: 0x06003650 RID: 13904 RVA: 0x000EC4E8 File Offset: 0x000EA6E8
		protected PropertyDescriptorCollection SortProperties(PropertyDescriptorCollection props, string[] names)
		{
			props.Sort(names);
			return props;
		}

		// Token: 0x04002A8A RID: 10890
		private const string s_UseCompatibleTypeConverterBehavior = "UseCompatibleTypeConverterBehavior";

		// Token: 0x04002A8B RID: 10891
		private static volatile bool useCompatibleTypeConversion = false;

		// Token: 0x04002A8C RID: 10892
		private static volatile bool firstLoadAppSetting = true;

		// Token: 0x04002A8D RID: 10893
		private static object loadAppSettingLock = new object();

		/// <summary>Represents an <see langword="abstract" /> class that provides properties for objects that do not have properties.</summary>
		// Token: 0x0200089B RID: 2203
		protected abstract class SimplePropertyDescriptor : PropertyDescriptor
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverter.SimplePropertyDescriptor" /> class.</summary>
			/// <param name="componentType">A <see cref="T:System.Type" /> that represents the type of component to which this property descriptor binds.</param>
			/// <param name="name">The name of the property.</param>
			/// <param name="propertyType">A <see cref="T:System.Type" /> that represents the data type for this property.</param>
			// Token: 0x0600459A RID: 17818 RVA: 0x001230E6 File Offset: 0x001212E6
			protected SimplePropertyDescriptor(Type componentType, string name, Type propertyType)
				: this(componentType, name, propertyType, new Attribute[0])
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverter.SimplePropertyDescriptor" /> class.</summary>
			/// <param name="componentType">A <see cref="T:System.Type" /> that represents the type of component to which this property descriptor binds.</param>
			/// <param name="name">The name of the property.</param>
			/// <param name="propertyType">A <see cref="T:System.Type" /> that represents the data type for this property.</param>
			/// <param name="attributes">An <see cref="T:System.Attribute" /> array with the attributes to associate with the property.</param>
			// Token: 0x0600459B RID: 17819 RVA: 0x001230F7 File Offset: 0x001212F7
			protected SimplePropertyDescriptor(Type componentType, string name, Type propertyType, Attribute[] attributes)
				: base(name, attributes)
			{
				this.componentType = componentType;
				this.propertyType = propertyType;
			}

			/// <summary>Gets the type of component to which this property description binds.</summary>
			/// <returns>A <see cref="T:System.Type" /> that represents the type of component to which this property binds.</returns>
			// Token: 0x17000FC0 RID: 4032
			// (get) Token: 0x0600459C RID: 17820 RVA: 0x00123110 File Offset: 0x00121310
			public override Type ComponentType
			{
				get
				{
					return this.componentType;
				}
			}

			/// <summary>Gets a value indicating whether this property is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the property is read-only; <see langword="false" /> if the property is read/write.</returns>
			// Token: 0x17000FC1 RID: 4033
			// (get) Token: 0x0600459D RID: 17821 RVA: 0x00123118 File Offset: 0x00121318
			public override bool IsReadOnly
			{
				get
				{
					return this.Attributes.Contains(ReadOnlyAttribute.Yes);
				}
			}

			/// <summary>Gets the type of the property.</summary>
			/// <returns>A <see cref="T:System.Type" /> that represents the type of the property.</returns>
			// Token: 0x17000FC2 RID: 4034
			// (get) Token: 0x0600459E RID: 17822 RVA: 0x0012312A File Offset: 0x0012132A
			public override Type PropertyType
			{
				get
				{
					return this.propertyType;
				}
			}

			/// <summary>Returns whether resetting the component changes the value of the component.</summary>
			/// <param name="component">The component to test for reset capability.</param>
			/// <returns>
			///   <see langword="true" /> if resetting the component changes the value of the component; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600459F RID: 17823 RVA: 0x00123134 File Offset: 0x00121334
			public override bool CanResetValue(object component)
			{
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
				return defaultValueAttribute != null && defaultValueAttribute.Value.Equals(this.GetValue(component));
			}

			/// <summary>Resets the value for this property of the component.</summary>
			/// <param name="component">The component with the property value to be reset.</param>
			// Token: 0x060045A0 RID: 17824 RVA: 0x00123174 File Offset: 0x00121374
			public override void ResetValue(object component)
			{
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
				if (defaultValueAttribute != null)
				{
					this.SetValue(component, defaultValueAttribute.Value);
				}
			}

			/// <summary>Returns whether the value of this property can persist.</summary>
			/// <param name="component">The component with the property that is to be examined for persistence.</param>
			/// <returns>
			///   <see langword="true" /> if the value of the property can persist; otherwise, <see langword="false" />.</returns>
			// Token: 0x060045A1 RID: 17825 RVA: 0x001231AC File Offset: 0x001213AC
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x040037D1 RID: 14289
			private Type componentType;

			// Token: 0x040037D2 RID: 14290
			private Type propertyType;
		}

		/// <summary>Represents a collection of values.</summary>
		// Token: 0x0200089C RID: 2204
		public class StandardValuesCollection : ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> class.</summary>
			/// <param name="values">An <see cref="T:System.Collections.ICollection" /> that represents the objects to put into the collection.</param>
			// Token: 0x060045A2 RID: 17826 RVA: 0x001231B0 File Offset: 0x001213B0
			public StandardValuesCollection(ICollection values)
			{
				if (values == null)
				{
					values = new object[0];
				}
				Array array = values as Array;
				if (array != null)
				{
					this.valueArray = array;
				}
				this.values = values;
			}

			/// <summary>Gets the number of objects in the collection.</summary>
			/// <returns>The number of objects in the collection.</returns>
			// Token: 0x17000FC3 RID: 4035
			// (get) Token: 0x060045A3 RID: 17827 RVA: 0x001231E6 File Offset: 0x001213E6
			public int Count
			{
				get
				{
					if (this.valueArray != null)
					{
						return this.valueArray.Length;
					}
					return this.values.Count;
				}
			}

			/// <summary>Gets the object at the specified index number.</summary>
			/// <param name="index">The zero-based index of the <see cref="T:System.Object" /> to get from the collection.</param>
			/// <returns>The <see cref="T:System.Object" /> with the specified index.</returns>
			// Token: 0x17000FC4 RID: 4036
			public object this[int index]
			{
				get
				{
					if (this.valueArray != null)
					{
						return this.valueArray.GetValue(index);
					}
					IList list = this.values as IList;
					if (list != null)
					{
						return list[index];
					}
					this.valueArray = new object[this.values.Count];
					this.values.CopyTo(this.valueArray, 0);
					return this.valueArray.GetValue(index);
				}
			}

			/// <summary>Copies the contents of this collection to an array.</summary>
			/// <param name="array">An <see cref="T:System.Array" /> that represents the array to copy to.</param>
			/// <param name="index">The index to start from.</param>
			// Token: 0x060045A5 RID: 17829 RVA: 0x00123275 File Offset: 0x00121475
			public void CopyTo(Array array, int index)
			{
				this.values.CopyTo(array, index);
			}

			/// <summary>Returns an enumerator for this collection.</summary>
			/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
			// Token: 0x060045A6 RID: 17830 RVA: 0x00123284 File Offset: 0x00121484
			public IEnumerator GetEnumerator()
			{
				return this.values.GetEnumerator();
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
			// Token: 0x17000FC5 RID: 4037
			// (get) Token: 0x060045A7 RID: 17831 RVA: 0x00123291 File Offset: 0x00121491
			int ICollection.Count
			{
				get
				{
					return this.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17000FC6 RID: 4038
			// (get) Token: 0x060045A8 RID: 17832 RVA: 0x00123299 File Offset: 0x00121499
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>
			///   <see langword="null" /> in all cases.</returns>
			// Token: 0x17000FC7 RID: 4039
			// (get) Token: 0x060045A9 RID: 17833 RVA: 0x0012329C File Offset: 0x0012149C
			object ICollection.SyncRoot
			{
				get
				{
					return null;
				}
			}

			/// <summary>Copies the contents of this collection to an array.</summary>
			/// <param name="array">The array to copy to.</param>
			/// <param name="index">The index in the array where copying should begin.</param>
			// Token: 0x060045AA RID: 17834 RVA: 0x0012329F File Offset: 0x0012149F
			void ICollection.CopyTo(Array array, int index)
			{
				this.CopyTo(array, index);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x060045AB RID: 17835 RVA: 0x001232A9 File Offset: 0x001214A9
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040037D3 RID: 14291
			private ICollection values;

			// Token: 0x040037D4 RID: 14292
			private Array valueArray;
		}
	}
}
