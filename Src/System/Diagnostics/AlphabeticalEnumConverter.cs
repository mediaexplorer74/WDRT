using System;
using System.Collections;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x020004BC RID: 1212
	internal class AlphabeticalEnumConverter : EnumConverter
	{
		// Token: 0x06002D46 RID: 11590 RVA: 0x000CBE7F File Offset: 0x000CA07F
		public AlphabeticalEnumConverter(Type type)
			: base(type)
		{
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000CBE88 File Offset: 0x000CA088
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (base.Values == null)
			{
				Array values = Enum.GetValues(base.EnumType);
				object[] array = new object[values.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.ConvertTo(context, null, values.GetValue(i), typeof(string));
				}
				Array.Sort(array, values, 0, values.Length, System.Collections.Comparer.Default);
				base.Values = new TypeConverter.StandardValuesCollection(values);
			}
			return base.Values;
		}
	}
}
