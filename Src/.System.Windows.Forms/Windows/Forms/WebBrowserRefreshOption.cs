using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants that define how the <see cref="T:System.Windows.Forms.WebBrowser" /> control can refresh its contents.</summary>
	// Token: 0x02000432 RID: 1074
	public enum WebBrowserRefreshOption
	{
		/// <summary>A refresh that requests a copy of the current Web page that has been cached on the server.</summary>
		// Token: 0x040027F5 RID: 10229
		Normal,
		/// <summary>A refresh that requests an update only if the current Web page has expired.</summary>
		// Token: 0x040027F6 RID: 10230
		IfExpired,
		/// <summary>For internal use only; do not use.</summary>
		// Token: 0x040027F7 RID: 10231
		Continue,
		/// <summary>A refresh that requests the latest version of the current Web page.</summary>
		// Token: 0x040027F8 RID: 10232
		Completely
	}
}
