using System;

namespace System.Windows.Forms.Automation
{
	/// <summary>Describes the notification characteristics of a particular live region.</summary>
	// Token: 0x020004F4 RID: 1268
	public enum AutomationLiveSetting
	{
		/// <summary>The element doesn't send notifications if the content of the live region has changed.</summary>
		// Token: 0x04003645 RID: 13893
		Off,
		/// <summary>The element sends non-interruptive notifications if the content of the live region has changed. With this setting, UI Automation clients and assistive technologies are expected to not interrupt the user to inform of changes to the live region.</summary>
		// Token: 0x04003646 RID: 13894
		Polite,
		/// <summary>The element sends interruptive notifications if the content of the live region has changed. With this setting, UI Automation clients and assistive technologies are expected to interrupt the user to inform of changes to the live region.</summary>
		// Token: 0x04003647 RID: 13895
		Assertive
	}
}
