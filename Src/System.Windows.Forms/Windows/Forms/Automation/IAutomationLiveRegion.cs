using System;

namespace System.Windows.Forms.Automation
{
	/// <summary>Provides support for UI automation live regions.</summary>
	// Token: 0x020004F7 RID: 1271
	public interface IAutomationLiveRegion
	{
		/// <summary>Gets or sets the notification characteristics of the live region.</summary>
		/// <returns>The notification characteristics of the live region.</returns>
		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06005294 RID: 21140
		// (set) Token: 0x06005295 RID: 21141
		AutomationLiveSetting LiveSetting { get; set; }
	}
}
