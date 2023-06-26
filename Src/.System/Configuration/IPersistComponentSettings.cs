using System;

namespace System.Configuration
{
	/// <summary>Defines standard functionality for controls or libraries that store and retrieve application settings.</summary>
	// Token: 0x02000092 RID: 146
	public interface IPersistComponentSettings
	{
		/// <summary>Gets or sets a value indicating whether the control should automatically persist its application settings properties.</summary>
		/// <returns>
		///   <see langword="true" /> if the control should automatically persist its state; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600056A RID: 1386
		// (set) Token: 0x0600056B RID: 1387
		bool SaveSettings { get; set; }

		/// <summary>Gets or sets the value of the application settings key for the current instance of the control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the settings key for the current instance of the control.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600056C RID: 1388
		// (set) Token: 0x0600056D RID: 1389
		string SettingsKey { get; set; }

		/// <summary>Reads the control's application settings into their corresponding properties and updates the control's state.</summary>
		// Token: 0x0600056E RID: 1390
		void LoadComponentSettings();

		/// <summary>Persists the control's application settings properties.</summary>
		// Token: 0x0600056F RID: 1391
		void SaveComponentSettings();

		/// <summary>Resets the control's application settings properties to their default values.</summary>
		// Token: 0x06000570 RID: 1392
		void ResetComponentSettings();
	}
}
