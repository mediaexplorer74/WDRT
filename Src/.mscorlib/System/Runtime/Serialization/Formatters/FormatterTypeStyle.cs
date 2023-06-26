using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Indicates the format in which type descriptions are laid out in the serialized stream.</summary>
	// Token: 0x0200075B RID: 1883
	[ComVisible(true)]
	[Serializable]
	public enum FormatterTypeStyle
	{
		/// <summary>Indicates that types can be stated only for arrays of objects, object members of type <see cref="T:System.Object" />, and <see cref="T:System.Runtime.Serialization.ISerializable" /> non-primitive value types.</summary>
		// Token: 0x040024D5 RID: 9429
		TypesWhenNeeded,
		/// <summary>Indicates that types can be given to all object members and <see cref="T:System.Runtime.Serialization.ISerializable" /> object members.</summary>
		// Token: 0x040024D6 RID: 9430
		TypesAlways,
		/// <summary>Indicates that strings can be given in the XSD format rather than SOAP. No string IDs are transmitted.</summary>
		// Token: 0x040024D7 RID: 9431
		XsdString
	}
}
