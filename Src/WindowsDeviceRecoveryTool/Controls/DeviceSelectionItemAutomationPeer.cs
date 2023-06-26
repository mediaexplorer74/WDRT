using System;
using System.Windows.Automation.Peers;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000C4 RID: 196
	public class DeviceSelectionItemAutomationPeer : ItemAutomationPeer
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x0001C8DB File Offset: 0x0001AADB
		public DeviceSelectionItemAutomationPeer(object item, ItemsControlAutomationPeer itemsControlAutomationPeer)
			: base(item, itemsControlAutomationPeer)
		{
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001C8E8 File Offset: 0x0001AAE8
		protected override AutomationControlType GetAutomationControlTypeCore()
		{
			return AutomationControlType.Button;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001C8FC File Offset: 0x0001AAFC
		protected override string GetClassNameCore()
		{
			return "Button";
		}
	}
}
