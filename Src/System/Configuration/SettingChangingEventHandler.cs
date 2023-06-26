using System;

namespace System.Configuration
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event.</summary>
	/// <param name="sender">The source of the event, typically an application settings wrapper class derived from the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class.</param>
	/// <param name="e">A <see cref="T:System.Configuration.SettingChangingEventArgs" /> containing the data for the event.</param>
	// Token: 0x0200007C RID: 124
	// (Invoke) Token: 0x060004F8 RID: 1272
	public delegate void SettingChangingEventHandler(object sender, SettingChangingEventArgs e);
}
