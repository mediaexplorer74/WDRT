using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Specifies the SOAP configuration options for use with the <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> class.</summary>
	// Token: 0x020007D3 RID: 2003
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum SoapOption
	{
		/// <summary>The default option indicating that no extra options are selected.</summary>
		// Token: 0x040027D3 RID: 10195
		None = 0,
		/// <summary>Indicates that type will always be included on SOAP elements. This option is useful when performing SOAP interop with SOAP implementations that require types on all elements.</summary>
		// Token: 0x040027D4 RID: 10196
		AlwaysIncludeTypes = 1,
		/// <summary>Indicates that the output SOAP string type in a SOAP Envelope is using the <see langword="XSD" /> prefix, and that the resulting XML does not have an ID attribute for the string.</summary>
		// Token: 0x040027D5 RID: 10197
		XsdString = 2,
		/// <summary>Indicates that SOAP will be generated without references. This option is currently not implemented.</summary>
		// Token: 0x040027D6 RID: 10198
		EmbedAll = 4,
		/// <summary>Public reserved option for temporary interop conditions; the use will change.</summary>
		// Token: 0x040027D7 RID: 10199
		Option1 = 8,
		/// <summary>Public reserved option for temporary interop conditions; the use will change.</summary>
		// Token: 0x040027D8 RID: 10200
		Option2 = 16
	}
}
