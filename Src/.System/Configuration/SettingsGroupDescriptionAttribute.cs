using System;

namespace System.Configuration
{
	/// <summary>Provides a string that describes an application settings property group. This class cannot be inherited.</summary>
	// Token: 0x0200009E RID: 158
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SettingsGroupDescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsGroupDescriptionAttribute" /> class.</summary>
		/// <param name="description">A <see cref="T:System.String" /> containing the descriptive text for the application settings group.</param>
		// Token: 0x06000599 RID: 1433 RVA: 0x000229BC File Offset: 0x00020BBC
		public SettingsGroupDescriptionAttribute(string description)
		{
			this._desc = description;
		}

		/// <summary>The descriptive text for the application settings properties group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the descriptive text for the application settings group.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x000229CB File Offset: 0x00020BCB
		public string Description
		{
			get
			{
				return this._desc;
			}
		}

		// Token: 0x04000C33 RID: 3123
		private readonly string _desc;
	}
}
