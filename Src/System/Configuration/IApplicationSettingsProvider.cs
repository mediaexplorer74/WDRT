using System;
using System.Security.Permissions;

namespace System.Configuration
{
	/// <summary>Defines extended capabilities for client-based application settings providers.</summary>
	// Token: 0x0200008E RID: 142
	public interface IApplicationSettingsProvider
	{
		/// <summary>Returns the value of the specified settings property for the previous version of the same application.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		/// <param name="property">The <see cref="T:System.Configuration.SettingsProperty" /> whose value is to be returned.</param>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValue" /> containing the value of the specified property setting as it was last set in the previous version of the application; or <see langword="null" /> if the setting cannot be found.</returns>
		// Token: 0x06000562 RID: 1378
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property);

		/// <summary>Resets the application settings associated with the specified application to their default values.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		// Token: 0x06000563 RID: 1379
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		void Reset(SettingsContext context);

		/// <summary>Indicates to the provider that the application has been upgraded. This offers the provider an opportunity to upgrade its stored settings as appropriate.</summary>
		/// <param name="context">A <see cref="T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
		/// <param name="properties">A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing the settings property group whose values are to be retrieved.</param>
		// Token: 0x06000564 RID: 1380
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		void Upgrade(SettingsContext context, SettingsPropertyCollection properties);
	}
}
