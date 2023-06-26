using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert data for an image index to and from a string.</summary>
	// Token: 0x02000290 RID: 656
	public class ImageIndexConverter : Int32Converter
	{
		/// <summary>Gets or sets a value indicating whether a <see langword="none" /> or <see langword="null" /> value is valid in the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> collection.</summary>
		/// <returns>
		///   <see langword="true" /> if a <see langword="none" /> or <see langword="null" /> value is valid in the standard values collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060029B3 RID: 10675 RVA: 0x00012E4E File Offset: 0x0001104E
		protected virtual bool IncludeNoneAsStandardValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060029B4 RID: 10676 RVA: 0x000BDB5B File Offset: 0x000BBD5B
		// (set) Token: 0x060029B5 RID: 10677 RVA: 0x000BDB63 File Offset: 0x000BBD63
		internal string ParentImageListProperty
		{
			get
			{
				return this.parentImageListProperty;
			}
			set
			{
				this.parentImageListProperty = value;
			}
		}

		/// <summary>Converts the specified value object to a 32-bit signed integer object.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> to provide locale information.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <exception cref="T:System.Exception">The conversion could not be performed.</exception>
		// Token: 0x060029B6 RID: 10678 RVA: 0x000BDB6C File Offset: 0x000BBD6C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null && string.Compare(text, SR.GetString("toStringNone"), true, culture) == 0)
			{
				return -1;
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the specified object to the specified destination type.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that provides locale information.</param>
		/// <param name="value">The object to convert, typically an index represented as an <see cref="T:System.Int32" />.</param>
		/// <param name="destinationType">The type to convert the object to, often a <see cref="T:System.String" />.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The specified <paramref name="value" /> could not be converted to the specified <paramref name="destinationType" />.</exception>
		// Token: 0x060029B7 RID: 10679 RVA: 0x000BDBA8 File Offset: 0x000BBDA8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is int && (int)value == -1)
			{
				return SR.GetString("toStringNone");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns a collection of standard index values for the image list associated with the specified format context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid index values. If no image list is found, this collection will contain a single object with a value of -1.</returns>
		// Token: 0x060029B8 RID: 10680 RVA: 0x000BDC04 File Offset: 0x000BBE04
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context != null && context.Instance != null)
			{
				object obj = context.Instance;
				PropertyDescriptor propertyDescriptor = ImageListUtils.GetImageListProperty(context.PropertyDescriptor, ref obj);
				while (obj != null && propertyDescriptor == null)
				{
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
					foreach (object obj2 in properties)
					{
						PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)obj2;
						if (typeof(ImageList).IsAssignableFrom(propertyDescriptor2.PropertyType))
						{
							propertyDescriptor = propertyDescriptor2;
							break;
						}
					}
					if (propertyDescriptor == null)
					{
						PropertyDescriptor propertyDescriptor3 = properties[this.ParentImageListProperty];
						if (propertyDescriptor3 != null)
						{
							obj = propertyDescriptor3.GetValue(obj);
						}
						else
						{
							obj = null;
						}
					}
				}
				if (propertyDescriptor != null)
				{
					ImageList imageList = (ImageList)propertyDescriptor.GetValue(obj);
					if (imageList != null)
					{
						int count = imageList.Images.Count;
						object[] array;
						if (this.IncludeNoneAsStandardValue)
						{
							array = new object[count + 1];
							array[count] = -1;
						}
						else
						{
							array = new object[count];
						}
						for (int i = 0; i < count; i++)
						{
							array[i] = i;
						}
						return new TypeConverter.StandardValuesCollection(array);
					}
				}
			}
			if (this.IncludeNoneAsStandardValue)
			{
				return new TypeConverter.StandardValuesCollection(new object[] { -1 });
			}
			return new TypeConverter.StandardValuesCollection(new object[0]);
		}

		/// <summary>Determines if the list of standard values returned from the <see cref="Overload:System.Windows.Forms.ImageIndexConverter.GetStandardValues" /> method is an exclusive list.</summary>
		/// <param name="context">A formatter context.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="Overload:System.Windows.Forms.ImageIndexConverter.GetStandardValues" /> method returns an exclusive list of valid values; otherwise, <see langword="false" />. This implementation always returns <see langword="false" />.</returns>
		// Token: 0x060029B9 RID: 10681 RVA: 0x0001180C File Offset: 0x0000FA0C
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Determines if the type converter supports a standard set of values that can be picked from a list.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="Overload:System.Windows.Forms.ImageIndexConverter.GetStandardValues" /> method returns a standard set of values; otherwise, <see langword="false" />. Always returns <see langword="true" />.</returns>
		// Token: 0x060029BA RID: 10682 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x040010E1 RID: 4321
		private string parentImageListProperty = "Parent";
	}
}
