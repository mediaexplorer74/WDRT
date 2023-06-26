using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020003A0 RID: 928
	internal class TextBoxAutoCompleteSourceConverter : EnumConverter
	{
		// Token: 0x06003CDD RID: 15581 RVA: 0x000181B9 File Offset: 0x000163B9
		public TextBoxAutoCompleteSourceConverter(Type type)
			: base(type)
		{
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x00108250 File Offset: 0x00106450
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			TypeConverter.StandardValuesCollection standardValues = base.GetStandardValues(context);
			ArrayList arrayList = new ArrayList();
			int count = standardValues.Count;
			for (int i = 0; i < count; i++)
			{
				string text = standardValues[i].ToString();
				if (!text.Equals("ListItems"))
				{
					arrayList.Add(standardValues[i]);
				}
			}
			return new TypeConverter.StandardValuesCollection(arrayList);
		}
	}
}
