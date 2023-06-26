using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Represents a collection of key/value pairs used to describe a configuration object as well as a <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
	// Token: 0x02000098 RID: 152
	[Serializable]
	public class SettingsAttributeDictionary : Hashtable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsAttributeDictionary" /> class.</summary>
		// Token: 0x06000590 RID: 1424 RVA: 0x00022965 File Offset: 0x00020B65
		public SettingsAttributeDictionary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsAttributeDictionary" /> class.</summary>
		/// <param name="attributes">A collection of key/value pairs that are related to configuration settings.</param>
		// Token: 0x06000591 RID: 1425 RVA: 0x0002296D File Offset: 0x00020B6D
		public SettingsAttributeDictionary(SettingsAttributeDictionary attributes)
			: base(attributes)
		{
		}
	}
}
