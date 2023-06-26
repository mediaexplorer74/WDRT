using System;
using System.Reflection;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers
{
	// Token: 0x02000037 RID: 55
	public static class UriDataArgument
	{
		// Token: 0x06000350 RID: 848 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		public static string Description(UriData value)
		{
			string text = string.Empty;
			Type type = value.GetType();
			FieldInfo field = type.GetField(value.ToString());
			bool flag = field != null;
			if (flag)
			{
				UriDescriptionAttribute[] array = field.GetCustomAttributes(typeof(UriDescriptionAttribute), false) as UriDescriptionAttribute[];
				bool flag2 = array != null && array.Length != 0;
				if (flag2)
				{
					text = array[0].Value;
				}
			}
			bool flag3 = !string.IsNullOrEmpty(text);
			if (flag3)
			{
				string text2 = text;
				string text3 = " (";
				int num = (int)value;
				text = text2 + text3 + num.ToString() + ")";
			}
			return text;
		}
	}
}
