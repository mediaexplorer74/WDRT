using System;
using System.Collections;
using System.Globalization;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000501 RID: 1281
	internal class AttributeTypeSorter : IComparer
	{
		// Token: 0x06005472 RID: 21618 RVA: 0x001617B0 File Offset: 0x0015F9B0
		private static string GetTypeIdString(Attribute a)
		{
			object typeId = a.TypeId;
			if (typeId == null)
			{
				return "";
			}
			string text;
			if (AttributeTypeSorter.typeIds == null)
			{
				AttributeTypeSorter.typeIds = new Hashtable();
				text = null;
			}
			else
			{
				text = AttributeTypeSorter.typeIds[typeId] as string;
			}
			if (text == null)
			{
				text = typeId.ToString();
				AttributeTypeSorter.typeIds[typeId] = text;
			}
			return text;
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x0016180C File Offset: 0x0015FA0C
		public int Compare(object obj1, object obj2)
		{
			Attribute attribute = obj1 as Attribute;
			Attribute attribute2 = obj2 as Attribute;
			if (attribute == null && attribute2 == null)
			{
				return 0;
			}
			if (attribute == null)
			{
				return -1;
			}
			if (attribute2 == null)
			{
				return 1;
			}
			return string.Compare(AttributeTypeSorter.GetTypeIdString(attribute), AttributeTypeSorter.GetTypeIdString(attribute2), false, CultureInfo.InvariantCulture);
		}

		// Token: 0x040036FB RID: 14075
		private static IDictionary typeIds;
	}
}
