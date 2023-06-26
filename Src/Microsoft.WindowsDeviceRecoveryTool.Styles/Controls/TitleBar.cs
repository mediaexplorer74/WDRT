using System;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Controls
{
	// Token: 0x02000010 RID: 16
	public sealed class TitleBar : Border
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000028B8 File Offset: 0x00000AB8
		protected override AutomationPeer OnCreateAutomationPeer()
		{
			return new TitleBar.TitleBarAutomationPeer(this);
		}

		// Token: 0x0200001D RID: 29
		private sealed class TitleBarAutomationPeer : FrameworkElementAutomationPeer
		{
			// Token: 0x0600009F RID: 159 RVA: 0x00003D8F File Offset: 0x00001F8F
			public TitleBarAutomationPeer(TitleBar owner)
				: base(owner)
			{
			}

			// Token: 0x060000A0 RID: 160 RVA: 0x00003D9C File Offset: 0x00001F9C
			protected override AutomationControlType GetAutomationControlTypeCore()
			{
				return AutomationControlType.TitleBar;
			}

			// Token: 0x060000A1 RID: 161 RVA: 0x00003DB0 File Offset: 0x00001FB0
			protected override string GetNameCore()
			{
				return string.Empty;
			}

			// Token: 0x060000A2 RID: 162 RVA: 0x00003DC8 File Offset: 0x00001FC8
			protected override bool IsContentElementCore()
			{
				return false;
			}

			// Token: 0x060000A3 RID: 163 RVA: 0x00003DDC File Offset: 0x00001FDC
			protected override string GetAutomationIdCore()
			{
				return "TitleBar";
			}
		}
	}
}
