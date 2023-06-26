using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace System.Management
{
	// Token: 0x02000046 RID: 70
	internal sealed class RC
	{
		// Token: 0x0600028B RID: 651 RVA: 0x000035AF File Offset: 0x000017AF
		private RC()
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000DBE2 File Offset: 0x0000BDE2
		public static string GetString(string strToGet)
		{
			return RC.resMgr.GetString(strToGet, CultureInfo.CurrentCulture);
		}

		// Token: 0x040001C4 RID: 452
		private static readonly ResourceManager resMgr = new ResourceManager(Assembly.GetExecutingAssembly().GetName().Name, Assembly.GetExecutingAssembly(), null);
	}
}
