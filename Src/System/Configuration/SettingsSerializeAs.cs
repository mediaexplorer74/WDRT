using System;

namespace System.Configuration
{
	/// <summary>Determines the serialization scheme used to store application settings.</summary>
	// Token: 0x020000B2 RID: 178
	public enum SettingsSerializeAs
	{
		/// <summary>The settings property is serialized as plain text.</summary>
		// Token: 0x04000C59 RID: 3161
		String,
		/// <summary>The settings property is serialized as XML using XML serialization.</summary>
		// Token: 0x04000C5A RID: 3162
		Xml,
		/// <summary>The settings property is serialized using binary object serialization.</summary>
		// Token: 0x04000C5B RID: 3163
		Binary,
		/// <summary>The settings provider has implicit knowledge of the property or its type and picks an appropriate serialization mechanism. Often used for custom serialization.</summary>
		// Token: 0x04000C5C RID: 3164
		ProviderSpecific
	}
}
