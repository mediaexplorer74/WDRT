using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000C1 RID: 193
	public class AnnounceablePasswordBoxAutomationPeer : PasswordBoxAutomationPeer, IValueProvider
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x0001C84A File Offset: 0x0001AA4A
		public AnnounceablePasswordBoxAutomationPeer(PasswordBox owner)
			: base(owner)
		{
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001C858 File Offset: 0x0001AA58
		string IValueProvider.Value
		{
			get
			{
				return string.Empty;
			}
		}
	}
}
