using System;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000C2 RID: 194
	public class DeviceSelectionListBox : ListBox
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x0001C870 File Offset: 0x0001AA70
		protected override AutomationPeer OnCreateAutomationPeer()
		{
			return new DeviceSelectionListBoxAutomationPeer(this);
		}
	}
}
