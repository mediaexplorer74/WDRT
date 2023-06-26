using System;

namespace System.Configuration
{
	/// <summary>Specifies the special setting category of a application settings property.</summary>
	// Token: 0x020000A6 RID: 166
	public enum SpecialSetting
	{
		/// <summary>The configuration property represents a connection string, typically for a data store or network resource.</summary>
		// Token: 0x04000C3C RID: 3132
		ConnectionString,
		/// <summary>The configuration property represents a Uniform Resource Locator (URL) to a Web service.</summary>
		// Token: 0x04000C3D RID: 3133
		WebServiceUrl
	}
}
