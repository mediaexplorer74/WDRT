using System;
using System.ComponentModel;
using System.Reflection;

namespace Microsoft.WindowsDeviceRecoveryTool.Common
{
	// Token: 0x02000004 RID: 4
	public static class EnumExtensions
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021C8 File Offset: 0x000003C8
		public static string GetDescription(this Enum enumType)
		{
			FieldInfo field = enumType.GetType().GetField(enumType.ToString());
			DescriptionAttribute descriptionAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
			return (descriptionAttribute == null) ? null : descriptionAttribute.Description;
		}
	}
}
