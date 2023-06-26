using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020002EE RID: 750
	internal class MdiWindowListItemConverter : ComponentConverter
	{
		// Token: 0x06002F91 RID: 12177 RVA: 0x000D6BB3 File Offset: 0x000D4DB3
		public MdiWindowListItemConverter(Type type)
			: base(type)
		{
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000D6BBC File Offset: 0x000D4DBC
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			MenuStrip menuStrip = context.Instance as MenuStrip;
			if (menuStrip != null)
			{
				TypeConverter.StandardValuesCollection standardValues = base.GetStandardValues(context);
				ArrayList arrayList = new ArrayList();
				int count = standardValues.Count;
				for (int i = 0; i < count; i++)
				{
					ToolStripItem toolStripItem = standardValues[i] as ToolStripItem;
					if (toolStripItem != null && toolStripItem.Owner == menuStrip)
					{
						arrayList.Add(toolStripItem);
					}
				}
				return new TypeConverter.StandardValuesCollection(arrayList);
			}
			return base.GetStandardValues(context);
		}
	}
}
