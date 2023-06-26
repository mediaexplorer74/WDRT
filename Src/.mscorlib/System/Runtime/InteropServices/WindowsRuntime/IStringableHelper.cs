using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009FC RID: 2556
	internal class IStringableHelper
	{
		// Token: 0x06006509 RID: 25865 RVA: 0x00159460 File Offset: 0x00157660
		internal static string ToString(object obj)
		{
			IStringable stringable = obj as IStringable;
			if (stringable != null)
			{
				return stringable.ToString();
			}
			return obj.ToString();
		}
	}
}
