using System;

namespace System.Configuration
{
	/// <summary>Provides an interface for defining an alternate application settings provider.</summary>
	// Token: 0x02000093 RID: 147
	public interface ISettingsProviderService
	{
		/// <summary>Returns the settings provider compatible with the specified settings property.</summary>
		/// <param name="property">The <see cref="T:System.Configuration.SettingsProperty" /> that requires serialization.</param>
		/// <returns>If found, the <see cref="T:System.Configuration.SettingsProvider" /> that can persist the specified settings property; otherwise, <see langword="null" />.</returns>
		// Token: 0x06000571 RID: 1393
		SettingsProvider GetSettingsProvider(SettingsProperty property);
	}
}
