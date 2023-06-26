using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000C5 RID: 197
	public sealed class GenericRoot : ContentControl
	{
		// Token: 0x060005F4 RID: 1524 RVA: 0x0001C914 File Offset: 0x0001AB14
		protected override AutomationPeer OnCreateAutomationPeer()
		{
			return new GenericRoot.CustomRootAutomationPeer(this);
		}

		// Token: 0x0200015B RID: 347
		private sealed class CustomRootAutomationPeer : GenericRootAutomationPeer
		{
			// Token: 0x06000873 RID: 2163 RVA: 0x000256E0 File Offset: 0x000238E0
			public CustomRootAutomationPeer(UIElement owner)
				: base(owner)
			{
			}

			// Token: 0x06000874 RID: 2164 RVA: 0x000256EC File Offset: 0x000238EC
			protected override List<AutomationPeer> GetChildrenCore()
			{
				List<AutomationPeer> childrenCore = base.GetChildrenCore();
				bool flag = childrenCore == null;
				List<AutomationPeer> list;
				if (flag)
				{
					list = null;
				}
				else
				{
					for (int i = 0; i < childrenCore.Count; i++)
					{
						AutomationPeer automationPeer = childrenCore[i];
						bool flag2 = automationPeer.GetType() == typeof(PasswordBoxAutomationPeer);
						if (flag2)
						{
							PasswordBoxAutomationPeer passwordBoxAutomationPeer = (PasswordBoxAutomationPeer)automationPeer;
							PasswordBox passwordBox = (PasswordBox)passwordBoxAutomationPeer.Owner;
							AnnounceablePasswordBoxAutomationPeer announceablePasswordBoxAutomationPeer = new AnnounceablePasswordBoxAutomationPeer((PasswordBox)passwordBoxAutomationPeer.Owner);
							object obj = typeof(UIElement).InvokeMember("AutomationPeerField", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField, null, null, null);
							obj.GetType().InvokeMember("SetValue", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, obj, new object[] { passwordBox, announceablePasswordBoxAutomationPeer });
							childrenCore[i] = announceablePasswordBoxAutomationPeer;
						}
					}
					list = childrenCore;
				}
				return list;
			}
		}
	}
}
