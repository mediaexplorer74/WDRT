using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines the attributes that can be associated with a property. These attribute values are defined in corhdr.h.</summary>
	// Token: 0x02000618 RID: 1560
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum PropertyAttributes
	{
		/// <summary>Specifies that no attributes are associated with a property.</summary>
		// Token: 0x04001E0B RID: 7691
		[__DynamicallyInvokable]
		None = 0,
		/// <summary>Specifies that the property is special, with the name describing how the property is special.</summary>
		// Token: 0x04001E0C RID: 7692
		[__DynamicallyInvokable]
		SpecialName = 512,
		/// <summary>Specifies a flag reserved for runtime use only.</summary>
		// Token: 0x04001E0D RID: 7693
		ReservedMask = 62464,
		/// <summary>Specifies that the metadata internal APIs check the name encoding.</summary>
		// Token: 0x04001E0E RID: 7694
		[__DynamicallyInvokable]
		RTSpecialName = 1024,
		/// <summary>Specifies that the property has a default value.</summary>
		// Token: 0x04001E0F RID: 7695
		[__DynamicallyInvokable]
		HasDefault = 4096,
		/// <summary>Reserved.</summary>
		// Token: 0x04001E10 RID: 7696
		Reserved2 = 8192,
		/// <summary>Reserved.</summary>
		// Token: 0x04001E11 RID: 7697
		Reserved3 = 16384,
		/// <summary>Reserved.</summary>
		// Token: 0x04001E12 RID: 7698
		Reserved4 = 32768
	}
}
