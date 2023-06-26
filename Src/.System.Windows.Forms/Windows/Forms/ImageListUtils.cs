using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200010D RID: 269
	internal static class ImageListUtils
	{
		// Token: 0x06000736 RID: 1846 RVA: 0x00014A18 File Offset: 0x00012C18
		public static PropertyDescriptor GetImageListProperty(PropertyDescriptor currentComponent, ref object instance)
		{
			if (instance is object[])
			{
				return null;
			}
			PropertyDescriptor propertyDescriptor = null;
			object obj = instance;
			RelatedImageListAttribute relatedImageListAttribute = currentComponent.Attributes[typeof(RelatedImageListAttribute)] as RelatedImageListAttribute;
			if (relatedImageListAttribute != null)
			{
				string[] array = relatedImageListAttribute.RelatedImageList.Split(new char[] { '.' });
				int num = 0;
				while (num < array.Length && obj != null)
				{
					PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(obj)[array[num]];
					if (propertyDescriptor2 == null)
					{
						break;
					}
					if (num == array.Length - 1)
					{
						if (typeof(ImageList).IsAssignableFrom(propertyDescriptor2.PropertyType))
						{
							instance = obj;
							propertyDescriptor = propertyDescriptor2;
							break;
						}
					}
					else
					{
						obj = propertyDescriptor2.GetValue(obj);
					}
					num++;
				}
			}
			return propertyDescriptor;
		}
	}
}
