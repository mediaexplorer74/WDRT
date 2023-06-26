using System;
using System.Globalization;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000A3 RID: 163
	public class LanguageChangedMessage
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x0001B471 File Offset: 0x00019671
		public LanguageChangedMessage(CultureInfo language)
		{
			this.Language = language;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0001B483 File Offset: 0x00019683
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0001B48B File Offset: 0x0001968B
		public CultureInfo Language { get; private set; }
	}
}
