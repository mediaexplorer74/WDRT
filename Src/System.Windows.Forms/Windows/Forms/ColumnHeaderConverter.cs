using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.ColumnHeader" /> objects from one type to another.</summary>
	// Token: 0x02000156 RID: 342
	public class ColumnHeaderConverter : ExpandableObjectConverter
	{
		/// <summary>Returns a value indicating whether the <see cref="T:System.Windows.Forms.ColumnHeaderConverter" /> can convert a <see cref="T:System.Windows.Forms.ColumnHeader" /> to the specified type, using the specified context.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A type representing the type to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DC1 RID: 3521 RVA: 0x0002792C File Offset: 0x00025B2C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to the specified type, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that represents information about a culture, such as language and calendar system. Can be <see langword="null" />.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert to.</param>
		/// <returns>The <see cref="T:System.Object" /> that is the result of the conversion.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed <paramref name="." /></exception>
		// Token: 0x06000DC2 RID: 3522 RVA: 0x0002794C File Offset: 0x00025B4C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(InstanceDescriptor)) || !(value is ColumnHeader))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			ColumnHeader columnHeader = (ColumnHeader)value;
			Type reflectionType = TypeDescriptor.GetReflectionType(value);
			InstanceDescriptor instanceDescriptor = null;
			ConstructorInfo constructorInfo;
			if (columnHeader.ImageIndex != -1)
			{
				constructorInfo = reflectionType.GetConstructor(new Type[] { typeof(int) });
				if (constructorInfo != null)
				{
					instanceDescriptor = new InstanceDescriptor(constructorInfo, new object[] { columnHeader.ImageIndex }, false);
				}
			}
			if (instanceDescriptor == null && !string.IsNullOrEmpty(columnHeader.ImageKey))
			{
				constructorInfo = reflectionType.GetConstructor(new Type[] { typeof(string) });
				if (constructorInfo != null)
				{
					instanceDescriptor = new InstanceDescriptor(constructorInfo, new object[] { columnHeader.ImageKey }, false);
				}
			}
			if (instanceDescriptor != null)
			{
				return instanceDescriptor;
			}
			constructorInfo = reflectionType.GetConstructor(new Type[0]);
			if (constructorInfo != null)
			{
				return new InstanceDescriptor(constructorInfo, new object[0], false);
			}
			throw new ArgumentException(SR.GetString("NoDefaultConstructor", new object[] { reflectionType.FullName }));
		}
	}
}
