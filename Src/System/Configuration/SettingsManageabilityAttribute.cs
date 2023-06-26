using System;

namespace System.Configuration
{
	/// <summary>Specifies special services for application settings properties. This class cannot be inherited.</summary>
	// Token: 0x020000A0 RID: 160
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsManageabilityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsManageabilityAttribute" /> class.</summary>
		/// <param name="manageability">A <see cref="T:System.Configuration.SettingsManageability" /> value that enumerates the services being requested.</param>
		// Token: 0x0600059D RID: 1437 RVA: 0x000229EA File Offset: 0x00020BEA
		public SettingsManageabilityAttribute(SettingsManageability manageability)
		{
			this._manageability = manageability;
		}

		/// <summary>Gets the set of special services that have been requested.</summary>
		/// <returns>A value that results from using the logical <see langword="OR" /> operator to combine all the <see cref="T:System.Configuration.SettingsManageability" /> enumeration values corresponding to the requested services.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x000229F9 File Offset: 0x00020BF9
		public SettingsManageability Manageability
		{
			get
			{
				return this._manageability;
			}
		}

		// Token: 0x04000C35 RID: 3125
		private readonly SettingsManageability _manageability;
	}
}
