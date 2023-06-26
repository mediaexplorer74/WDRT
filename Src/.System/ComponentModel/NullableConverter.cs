using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides automatic conversion between a nullable type and its underlying primitive type.</summary>
	// Token: 0x02000593 RID: 1427
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class NullableConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NullableConverter" /> class.</summary>
		/// <param name="type">The specified nullable type.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a nullable type.</exception>
		// Token: 0x060034F9 RID: 13561 RVA: 0x000E6FC4 File Offset: 0x000E51C4
		public NullableConverter(Type type)
		{
			this.nullableType = type;
			this.simpleType = Nullable.GetUnderlyingType(type);
			if (this.simpleType == null)
			{
				throw new ArgumentException(SR.GetString("NullableConverterBadCtorArg"), "type");
			}
			this.simpleTypeConverter = TypeDescriptor.GetConverter(this.simpleType);
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034FA RID: 13562 RVA: 0x000E701E File Offset: 0x000E521E
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == this.simpleType)
			{
				return true;
			}
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.CanConvertFrom(context, sourceType);
			}
			return base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x060034FB RID: 13563 RVA: 0x000E7050 File Offset: 0x000E5250
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || value.GetType() == this.simpleType)
			{
				return value;
			}
			if (value is string && string.IsNullOrEmpty(value as string))
			{
				return null;
			}
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.ConvertFrom(context, culture, value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034FC RID: 13564 RVA: 0x000E70B0 File Offset: 0x000E52B0
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == this.simpleType)
			{
				return true;
			}
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.CanConvertTo(context, destinationType);
			}
			return base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x060034FD RID: 13565 RVA: 0x000E7100 File Offset: 0x000E5300
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == this.simpleType && this.nullableType.IsInstanceOfType(value))
			{
				return value;
			}
			if (destinationType == typeof(InstanceDescriptor))
			{
				ConstructorInfo constructor = this.nullableType.GetConstructor(new Type[] { this.simpleType });
				return new InstanceDescriptor(constructor, new object[] { value }, true);
			}
			if (value == null)
			{
				if (destinationType == typeof(string))
				{
					return string.Empty;
				}
			}
			else if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.ConvertTo(context, culture, value, destinationType);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Creates an instance of the type that this <see cref="T:System.ComponentModel.TypeConverter" /> is associated with, using the specified context, given a set of property values for the object.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or <see langword="null" /> if the object cannot be created. This method always returns <see langword="null" />.</returns>
		// Token: 0x060034FE RID: 13566 RVA: 0x000E71C4 File Offset: 0x000E53C4
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.CreateInstance(context, propertyValues);
			}
			return base.CreateInstance(context, propertyValues);
		}

		/// <summary>Returns whether changing a value on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034FF RID: 13567 RVA: 0x000E71F1 File Offset: 0x000E53F1
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetCreateInstanceSupported(context);
			}
			return base.GetCreateInstanceSupported(context);
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or <see langword="null" /> if there are no properties.</returns>
		// Token: 0x06003500 RID: 13568 RVA: 0x000E7210 File Offset: 0x000E5410
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetProperties(context, value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		/// <summary>Returns whether this object supports properties, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003501 RID: 13569 RVA: 0x000E723F File Offset: 0x000E543F
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetPropertiesSupported(context);
			}
			return base.GetPropertiesSupported(context);
		}

		/// <summary>Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or <see langword="null" /> if the data type does not support a standard set of values.</returns>
		// Token: 0x06003502 RID: 13570 RVA: 0x000E7260 File Offset: 0x000E5460
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				TypeConverter.StandardValuesCollection standardValues = this.simpleTypeConverter.GetStandardValues(context);
				if (this.GetStandardValuesSupported(context) && standardValues != null)
				{
					object[] array = new object[standardValues.Count + 1];
					int num = 0;
					array[num++] = null;
					foreach (object obj in standardValues)
					{
						array[num++] = obj;
					}
					return new TypeConverter.StandardValuesCollection(array);
				}
			}
			return base.GetStandardValues(context);
		}

		/// <summary>Returns whether the collection of standard values returned from <see cref="Overload:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list of possible values, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; <see langword="false" /> if other values are possible.</returns>
		// Token: 0x06003503 RID: 13571 RVA: 0x000E72FC File Offset: 0x000E54FC
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetStandardValuesExclusive(context);
			}
			return base.GetStandardValuesExclusive(context);
		}

		/// <summary>Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003504 RID: 13572 RVA: 0x000E731A File Offset: 0x000E551A
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			if (this.simpleTypeConverter != null)
			{
				return this.simpleTypeConverter.GetStandardValuesSupported(context);
			}
			return base.GetStandardValuesSupported(context);
		}

		/// <summary>Returns whether the given value object is valid for this type and for the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the specified value is valid for this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003505 RID: 13573 RVA: 0x000E7338 File Offset: 0x000E5538
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (this.simpleTypeConverter != null)
			{
				return value == null || this.simpleTypeConverter.IsValid(context, value);
			}
			return base.IsValid(context, value);
		}

		/// <summary>Gets the nullable type.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the nullable type.</returns>
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06003506 RID: 13574 RVA: 0x000E736A File Offset: 0x000E556A
		public Type NullableType
		{
			get
			{
				return this.nullableType;
			}
		}

		/// <summary>Gets the underlying type.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the underlying type.</returns>
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x000E7372 File Offset: 0x000E5572
		public Type UnderlyingType
		{
			get
			{
				return this.simpleType;
			}
		}

		/// <summary>Gets the underlying type converter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that represents the underlying type converter.</returns>
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06003508 RID: 13576 RVA: 0x000E737A File Offset: 0x000E557A
		public TypeConverter UnderlyingTypeConverter
		{
			get
			{
				return this.simpleTypeConverter;
			}
		}

		// Token: 0x04002A1E RID: 10782
		private Type nullableType;

		// Token: 0x04002A1F RID: 10783
		private Type simpleType;

		// Token: 0x04002A20 RID: 10784
		private TypeConverter simpleTypeConverter;
	}
}
