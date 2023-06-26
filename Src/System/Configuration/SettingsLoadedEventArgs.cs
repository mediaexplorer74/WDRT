using System;

namespace System.Configuration
{
	/// <summary>Provides data for the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsLoaded" /> event.</summary>
	// Token: 0x0200007E RID: 126
	public class SettingsLoadedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsLoadedEventArgs" /> class.</summary>
		/// <param name="provider">A <see cref="T:System.Configuration.SettingsProvider" /> object from which settings are loaded.</param>
		// Token: 0x06000500 RID: 1280 RVA: 0x00020C97 File Offset: 0x0001EE97
		public SettingsLoadedEventArgs(SettingsProvider provider)
		{
			this._provider = provider;
		}

		/// <summary>Gets the settings provider used to store configuration settings.</summary>
		/// <returns>A settings provider.</returns>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00020CA6 File Offset: 0x0001EEA6
		public SettingsProvider Provider
		{
			get
			{
				return this._provider;
			}
		}

		// Token: 0x04000C0D RID: 3085
		private SettingsProvider _provider;
	}
}
