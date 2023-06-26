using System;
using System.Windows.Automation.Peers;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000C3 RID: 195
	public class DeviceSelectionListBoxAutomationPeer : ListBoxAutomationPeer
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x0001C891 File Offset: 0x0001AA91
		public DeviceSelectionListBoxAutomationPeer(DeviceSelectionListBox owner)
			: base(owner)
		{
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001C89C File Offset: 0x0001AA9C
		protected override AutomationControlType GetAutomationControlTypeCore()
		{
			return AutomationControlType.List;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001C8B0 File Offset: 0x0001AAB0
		protected override ItemAutomationPeer CreateItemAutomationPeer(object item)
		{
			ItemAutomationPeer itemAutomationPeer = base.CreateItemAutomationPeer(item);
			return new DeviceSelectionItemAutomationPeer(itemAutomationPeer.Item, itemAutomationPeer.ItemsControlAutomationPeer);
		}
	}
}
