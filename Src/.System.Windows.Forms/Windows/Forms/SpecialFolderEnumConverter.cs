using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200036B RID: 875
	internal class SpecialFolderEnumConverter : AlphaSortedEnumConverter
	{
		// Token: 0x060038B1 RID: 14513 RVA: 0x000FC1AC File Offset: 0x000FA3AC
		public SpecialFolderEnumConverter(Type type)
			: base(type)
		{
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000FC1B8 File Offset: 0x000FA3B8
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			TypeConverter.StandardValuesCollection standardValues = base.GetStandardValues(context);
			ArrayList arrayList = new ArrayList();
			int count = standardValues.Count;
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				if (standardValues[i] is Environment.SpecialFolder && standardValues[i].Equals(Environment.SpecialFolder.Personal))
				{
					if (!flag)
					{
						flag = true;
						arrayList.Add(standardValues[i]);
					}
				}
				else
				{
					arrayList.Add(standardValues[i]);
				}
			}
			return new TypeConverter.StandardValuesCollection(arrayList);
		}
	}
}
