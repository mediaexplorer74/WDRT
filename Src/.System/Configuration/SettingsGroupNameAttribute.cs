using System;

namespace System.Configuration
{
	/// <summary>Specifies a name for application settings property group. This class cannot be inherited.</summary>
	// Token: 0x0200009F RID: 159
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SettingsGroupNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsGroupNameAttribute" /> class.</summary>
		/// <param name="groupName">A <see cref="T:System.String" /> containing the name of the application settings property group.</param>
		// Token: 0x0600059B RID: 1435 RVA: 0x000229D3 File Offset: 0x00020BD3
		public SettingsGroupNameAttribute(string groupName)
		{
			this._groupName = groupName;
		}

		/// <summary>Gets the name of the application settings property group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the application settings property group.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x000229E2 File Offset: 0x00020BE2
		public string GroupName
		{
			get
			{
				return this._groupName;
			}
		}

		// Token: 0x04000C34 RID: 3124
		private readonly string _groupName;
	}
}
