using System;

namespace System.Configuration
{
	/// <summary>Specifies the serialization mechanism that the settings provider should use. This class cannot be inherited.</summary>
	// Token: 0x020000A2 RID: 162
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsSerializeAsAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsSerializeAsAttribute" /> class.</summary>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> enumerated value that specifies the serialization scheme.</param>
		// Token: 0x060005A2 RID: 1442 RVA: 0x00022A35 File Offset: 0x00020C35
		public SettingsSerializeAsAttribute(SettingsSerializeAs serializeAs)
		{
			this._serializeAs = serializeAs;
		}

		/// <summary>Gets the <see cref="T:System.Configuration.SettingsSerializeAs" /> enumeration value that specifies the serialization scheme.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> enumerated value that specifies the serialization scheme.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00022A44 File Offset: 0x00020C44
		public SettingsSerializeAs SerializeAs
		{
			get
			{
				return this._serializeAs;
			}
		}

		// Token: 0x04000C37 RID: 3127
		private readonly SettingsSerializeAs _serializeAs;
	}
}
