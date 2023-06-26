using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines the attributes that can be associated with a parameter. These are defined in CorHdr.h.</summary>
	// Token: 0x02000613 RID: 1555
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum ParameterAttributes
	{
		/// <summary>Specifies that there is no parameter attribute.</summary>
		// Token: 0x04001DE9 RID: 7657
		[__DynamicallyInvokable]
		None = 0,
		/// <summary>Specifies that the parameter is an input parameter.</summary>
		// Token: 0x04001DEA RID: 7658
		[__DynamicallyInvokable]
		In = 1,
		/// <summary>Specifies that the parameter is an output parameter.</summary>
		// Token: 0x04001DEB RID: 7659
		[__DynamicallyInvokable]
		Out = 2,
		/// <summary>Specifies that the parameter is a locale identifier (lcid).</summary>
		// Token: 0x04001DEC RID: 7660
		[__DynamicallyInvokable]
		Lcid = 4,
		/// <summary>Specifies that the parameter is a return value.</summary>
		// Token: 0x04001DED RID: 7661
		[__DynamicallyInvokable]
		Retval = 8,
		/// <summary>Specifies that the parameter is optional.</summary>
		// Token: 0x04001DEE RID: 7662
		[__DynamicallyInvokable]
		Optional = 16,
		/// <summary>Specifies that the parameter is reserved.</summary>
		// Token: 0x04001DEF RID: 7663
		ReservedMask = 61440,
		/// <summary>Specifies that the parameter has a default value.</summary>
		// Token: 0x04001DF0 RID: 7664
		[__DynamicallyInvokable]
		HasDefault = 4096,
		/// <summary>Specifies that the parameter has field marshaling information.</summary>
		// Token: 0x04001DF1 RID: 7665
		[__DynamicallyInvokable]
		HasFieldMarshal = 8192,
		/// <summary>Reserved.</summary>
		// Token: 0x04001DF2 RID: 7666
		Reserved3 = 16384,
		/// <summary>Reserved.</summary>
		// Token: 0x04001DF3 RID: 7667
		Reserved4 = 32768
	}
}
