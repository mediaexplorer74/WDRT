using System;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>Represents types of token binding.</summary>
	// Token: 0x02000447 RID: 1095
	public enum TokenBindingType
	{
		/// <summary>Used to establish a token binding when connecting to a server.</summary>
		// Token: 0x04002258 RID: 8792
		Provided,
		/// <summary>Used when requesting tokens to be presented to a different server.</summary>
		// Token: 0x04002259 RID: 8793
		Referred
	}
}
