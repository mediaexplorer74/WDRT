using System;

namespace System.Configuration
{
	/// <summary>Provides a string that describes an individual configuration property. This class cannot be inherited.</summary>
	// Token: 0x0200009D RID: 157
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsDescriptionAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsDescriptionAttribute" /> class.</summary>
		/// <param name="description">The <see cref="T:System.String" /> used as descriptive text.</param>
		// Token: 0x06000597 RID: 1431 RVA: 0x000229A5 File Offset: 0x00020BA5
		public SettingsDescriptionAttribute(string description)
		{
			this._desc = description;
		}

		/// <summary>Gets the descriptive text for the associated configuration property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the descriptive text for the associated configuration property.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x000229B4 File Offset: 0x00020BB4
		public string Description
		{
			get
			{
				return this._desc;
			}
		}

		// Token: 0x04000C32 RID: 3122
		private readonly string _desc;
	}
}
