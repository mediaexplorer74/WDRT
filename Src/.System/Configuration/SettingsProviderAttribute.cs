using System;

namespace System.Configuration
{
	/// <summary>Specifies the settings provider used to provide storage for the current application settings class or property. This class cannot be inherited.</summary>
	// Token: 0x020000A1 RID: 161
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsProviderAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsProviderAttribute" /> class.</summary>
		/// <param name="providerTypeName">A <see cref="T:System.String" /> containing the name of the settings provider.</param>
		// Token: 0x0600059F RID: 1439 RVA: 0x00022A01 File Offset: 0x00020C01
		public SettingsProviderAttribute(string providerTypeName)
		{
			this._providerTypeName = providerTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProviderAttribute" /> class.</summary>
		/// <param name="providerType">A <see cref="T:System.Type" /> containing the settings provider type.</param>
		// Token: 0x060005A0 RID: 1440 RVA: 0x00022A10 File Offset: 0x00020C10
		public SettingsProviderAttribute(Type providerType)
		{
			if (providerType != null)
			{
				this._providerTypeName = providerType.AssemblyQualifiedName;
			}
		}

		/// <summary>Gets the type name of the settings provider.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the settings provider.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00022A2D File Offset: 0x00020C2D
		public string ProviderTypeName
		{
			get
			{
				return this._providerTypeName;
			}
		}

		// Token: 0x04000C36 RID: 3126
		private readonly string _providerTypeName;
	}
}
