using System;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000C8 RID: 200
	public class TileAutomationPeer : FrameworkElementAutomationPeer
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x0001CAF5 File Offset: 0x0001ACF5
		public TileAutomationPeer(ListViewItem owner)
			: base(owner)
		{
			this._listViewItem = (ListViewItem)base.Owner;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001CB14 File Offset: 0x0001AD14
		protected override AutomationControlType GetAutomationControlTypeCore()
		{
			return AutomationControlType.Button;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001CB28 File Offset: 0x0001AD28
		protected override string GetHelpTextCore()
		{
			return this._listViewItem.Content.ToString();
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001CB4C File Offset: 0x0001AD4C
		protected override bool IsEnabledCore()
		{
			return this._listViewItem.IsFocused;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001CB6C File Offset: 0x0001AD6C
		protected override string GetNameCore()
		{
			return this._listViewItem.Content.ToString();
		}

		// Token: 0x040002BC RID: 700
		private readonly ListViewItem _listViewItem;
	}
}
