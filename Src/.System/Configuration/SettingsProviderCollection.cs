using System;
using System.Configuration.Provider;

namespace System.Configuration
{
	/// <summary>Represents a collection of application settings providers.</summary>
	// Token: 0x020000B1 RID: 177
	public class SettingsProviderCollection : ProviderCollection
	{
		/// <summary>Adds a new settings provider to the collection.</summary>
		/// <param name="provider">A <see cref="T:System.Configuration.Provider.ProviderBase" /> to add to the collection.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="provider" /> parameter is not of type <see cref="T:System.Configuration.SettingsProvider" />.  
		///  -or-  
		///  The <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> property of the provider parameter is null or an empty string.  
		///  -or-  
		///  A settings provider with the same <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> already exists in the collection.</exception>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="provider" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600060D RID: 1549 RVA: 0x00023C78 File Offset: 0x00021E78
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is SettingsProvider))
			{
				throw new ArgumentException(SR.GetString("Config_provider_must_implement_type", new object[] { typeof(SettingsProvider).ToString() }), "provider");
			}
			base.Add(provider);
		}

		/// <summary>Gets the settings provider in the collection that matches the specified name.</summary>
		/// <param name="name">A <see cref="T:System.String" /> containing the friendly name of the settings provider.</param>
		/// <returns>If found, the <see cref="T:System.Configuration.SettingsProvider" /> whose name matches that specified by the name parameter; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only when setting this value.</exception>
		// Token: 0x170000FB RID: 251
		public SettingsProvider this[string name]
		{
			get
			{
				return (SettingsProvider)base[name];
			}
		}
	}
}
