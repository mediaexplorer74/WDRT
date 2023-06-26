using System;
using System.Configuration.Provider;

namespace System.Configuration
{
	/// <summary>Acts as a base class for deriving custom settings providers in the application settings architecture.</summary>
	// Token: 0x020000B0 RID: 176
	public abstract class SettingsProvider : ProviderBase
	{
		/// <summary>Returns the collection of settings property values for the specified application instance and settings property group.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application use.</param>
		/// <param name="collection">A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing the settings property group whose values are to be retrieved.</param>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> containing the values for the specified settings property group.</returns>
		// Token: 0x06000608 RID: 1544
		public abstract SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection);

		/// <summary>Sets the values of the specified group of property settings.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		/// <param name="collection">A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> representing the group of property settings to set.</param>
		// Token: 0x06000609 RID: 1545
		public abstract void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection);

		/// <summary>Gets or sets the name of the currently running application.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the application's shortened name, which does not contain a full path or extension, for example, <c>SimpleAppSettings</c>.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600060A RID: 1546
		// (set) Token: 0x0600060B RID: 1547
		public abstract string ApplicationName { get; set; }
	}
}
