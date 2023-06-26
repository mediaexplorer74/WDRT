using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.Binding" /> objects to and from various other representations.</summary>
	// Token: 0x020002C9 RID: 713
	public class ListBindingConverter : TypeConverter
	{
		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x000C4B38 File Offset: 0x000C2D38
		private static Type[] ConstructorParamaterTypes
		{
			get
			{
				if (ListBindingConverter.ctorTypes == null)
				{
					ListBindingConverter.ctorTypes = new Type[]
					{
						typeof(string),
						typeof(object),
						typeof(string),
						typeof(bool),
						typeof(DataSourceUpdateMode),
						typeof(object),
						typeof(string),
						typeof(IFormatProvider)
					};
				}
				return ListBindingConverter.ctorTypes;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x000C4BC4 File Offset: 0x000C2DC4
		private static string[] ConstructorParameterProperties
		{
			get
			{
				if (ListBindingConverter.ctorParamProps == null)
				{
					ListBindingConverter.ctorParamProps = new string[] { null, null, null, "FormattingEnabled", "DataSourceUpdateMode", "NullValue", "FormatString", "FormatInfo" };
				}
				return ListBindingConverter.ctorParamProps;
			}
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002BBA RID: 11194 RVA: 0x0002792C File Offset: 0x00025B2C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If <see langword="null" /> is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		// Token: 0x06002BBB RID: 11195 RVA: 0x000C4C10 File Offset: 0x000C2E10
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Binding)
			{
				Binding binding = (Binding)value;
				return this.GetInstanceDescriptorFromValues(binding);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Creates an instance of the type that this <see cref="T:System.ComponentModel.TypeConverter" /> is associated with, using the specified context, given a set of property values for the object.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or <see langword="null" /> if the object cannot be created. This method always returns <see langword="null" />.</returns>
		// Token: 0x06002BBC RID: 11196 RVA: 0x000C4C68 File Offset: 0x000C2E68
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			object obj;
			try
			{
				obj = new Binding((string)propertyValues["PropertyName"], propertyValues["DataSource"], (string)propertyValues["DataMember"]);
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

		/// <summary>Returns whether changing a value on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002BBD RID: 11197 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000C4CEC File Offset: 0x000C2EEC
		private InstanceDescriptor GetInstanceDescriptorFromValues(Binding b)
		{
			b.FormattingEnabled = true;
			bool flag = true;
			int num = ListBindingConverter.ConstructorParameterProperties.Length - 1;
			while (num >= 0 && ListBindingConverter.ConstructorParameterProperties[num] != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(b)[ListBindingConverter.ConstructorParameterProperties[num]];
				if (propertyDescriptor != null && propertyDescriptor.ShouldSerializeValue(b))
				{
					break;
				}
				num--;
			}
			Type[] array = new Type[num + 1];
			Array.Copy(ListBindingConverter.ConstructorParamaterTypes, 0, array, 0, array.Length);
			ConstructorInfo constructorInfo = typeof(Binding).GetConstructor(array);
			if (constructorInfo == null)
			{
				flag = false;
				constructorInfo = typeof(Binding).GetConstructor(new Type[]
				{
					typeof(string),
					typeof(object),
					typeof(string)
				});
			}
			object[] array2 = new object[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				object obj;
				switch (i)
				{
				case 0:
					obj = b.PropertyName;
					break;
				case 1:
					obj = b.BindToObject.DataSource;
					break;
				case 2:
					obj = b.BindToObject.BindingMemberInfo.BindingMember;
					break;
				default:
					obj = TypeDescriptor.GetProperties(b)[ListBindingConverter.ConstructorParameterProperties[i]].GetValue(b);
					break;
				}
				array2[i] = obj;
			}
			return new InstanceDescriptor(constructorInfo, array2, flag);
		}

		// Token: 0x0400124B RID: 4683
		private static Type[] ctorTypes;

		// Token: 0x0400124C RID: 4684
		private static string[] ctorParamProps;
	}
}
