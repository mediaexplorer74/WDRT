using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert data for an image key to and from another data type.</summary>
	// Token: 0x02000291 RID: 657
	public class ImageKeyConverter : StringConverter
	{
		/// <summary>Gets or sets a value indicating whether <see langword="null" /> is valid in the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> collection.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases, indicating <see langword="null" /> is valid in the standard values collection.</returns>
		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x00012E4E File Offset: 0x0001104E
		protected virtual bool IncludeNoneAsStandardValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060029BD RID: 10685 RVA: 0x000BDD77 File Offset: 0x000BBF77
		// (set) Token: 0x060029BE RID: 10686 RVA: 0x000BDD7F File Offset: 0x000BBF7F
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

		/// <summary>Returns whether this converter can convert an object of the given type to a string using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that specifies the type you want to convert from.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the specified conversion can be performed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060029BF RID: 10687 RVA: 0x000BDD88 File Offset: 0x000BBF88
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts from the specified object to a string.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> to provide locale information.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x060029C0 RID: 10688 RVA: 0x000BDDA6 File Offset: 0x000BBFA6
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return (string)value;
			}
			if (value == null)
			{
				return "";
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the given object to the specified type.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that provides locale information.</param>
		/// <param name="value">The object to convert, typically an image key.</param>
		/// <param name="destinationType">The type to convert the object to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The specified <paramref name="value" /> could not be converted to the specified <paramref name="destinationType" />.</exception>
		// Token: 0x060029C1 RID: 10689 RVA: 0x000BDDCC File Offset: 0x000BBFCC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null && value is string && ((string)value).Length == 0)
			{
				return SR.GetString("toStringNone");
			}
			if (destinationType == typeof(string) && value == null)
			{
				return SR.GetString("toStringNone");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns a collection of standard image keys for the image list associated with the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that contains the standard set of image key values.</returns>
		// Token: 0x060029C2 RID: 10690 RVA: 0x000BDE50 File Offset: 0x000BC050
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
							array[count] = "";
						}
						else
						{
							array = new object[count];
						}
						StringCollection keys = imageList.Images.Keys;
						for (int i = 0; i < keys.Count; i++)
						{
							if (keys[i] != null && keys[i].Length != 0)
							{
								array[i] = keys[i];
							}
						}
						return new TypeConverter.StandardValuesCollection(array);
					}
				}
			}
			if (this.IncludeNoneAsStandardValue)
			{
				return new TypeConverter.StandardValuesCollection(new object[] { "" });
			}
			return new TypeConverter.StandardValuesCollection(new object[0]);
		}

		/// <summary>Determines whether the list of standard values for the <see cref="T:System.Windows.Forms.ImageKeyConverter" /> is exclusive (that is, whether it allows values other than those returned by <see cref="Overload:System.Windows.Forms.ImageKeyConverter.GetStandardValues" />).</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the list does not allow additional values; otherwise, <see langword="false" />. Always returns <see langword="true" />.</returns>
		// Token: 0x060029C3 RID: 10691 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Determines whether this type converter supports a standard set of values that can be picked from a list.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> to indicate a list of standard values is supported; otherwise, <see langword="false" />. Always returns <see langword="true" />.</returns>
		// Token: 0x060029C4 RID: 10692 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x040010E2 RID: 4322
		private string parentImageListProperty = "Parent";
	}
}
