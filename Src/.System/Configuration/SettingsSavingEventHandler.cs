using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsSaving" /> event.</summary>
	/// <param name="sender">The source of the event, typically a data container or data-bound collection.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
	// Token: 0x0200007B RID: 123
	// (Invoke) Token: 0x060004F4 RID: 1268
	public delegate void SettingsSavingEventHandler(object sender, CancelEventArgs e);
}
