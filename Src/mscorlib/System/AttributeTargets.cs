using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the application elements on which it is valid to apply an attribute.</summary>
	// Token: 0x020000AE RID: 174
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum AttributeTargets
	{
		/// <summary>Attribute can be applied to an assembly.</summary>
		// Token: 0x040003D6 RID: 982
		[__DynamicallyInvokable]
		Assembly = 1,
		/// <summary>Attribute can be applied to a module.</summary>
		// Token: 0x040003D7 RID: 983
		[__DynamicallyInvokable]
		Module = 2,
		/// <summary>Attribute can be applied to a class.</summary>
		// Token: 0x040003D8 RID: 984
		[__DynamicallyInvokable]
		Class = 4,
		/// <summary>Attribute can be applied to a structure; that is, a value type.</summary>
		// Token: 0x040003D9 RID: 985
		[__DynamicallyInvokable]
		Struct = 8,
		/// <summary>Attribute can be applied to an enumeration.</summary>
		// Token: 0x040003DA RID: 986
		[__DynamicallyInvokable]
		Enum = 16,
		/// <summary>Attribute can be applied to a constructor.</summary>
		// Token: 0x040003DB RID: 987
		[__DynamicallyInvokable]
		Constructor = 32,
		/// <summary>Attribute can be applied to a method.</summary>
		// Token: 0x040003DC RID: 988
		[__DynamicallyInvokable]
		Method = 64,
		/// <summary>Attribute can be applied to a property.</summary>
		// Token: 0x040003DD RID: 989
		[__DynamicallyInvokable]
		Property = 128,
		/// <summary>Attribute can be applied to a field.</summary>
		// Token: 0x040003DE RID: 990
		[__DynamicallyInvokable]
		Field = 256,
		/// <summary>Attribute can be applied to an event.</summary>
		// Token: 0x040003DF RID: 991
		[__DynamicallyInvokable]
		Event = 512,
		/// <summary>Attribute can be applied to an interface.</summary>
		// Token: 0x040003E0 RID: 992
		[__DynamicallyInvokable]
		Interface = 1024,
		/// <summary>Attribute can be applied to a parameter.</summary>
		// Token: 0x040003E1 RID: 993
		[__DynamicallyInvokable]
		Parameter = 2048,
		/// <summary>Attribute can be applied to a delegate.</summary>
		// Token: 0x040003E2 RID: 994
		[__DynamicallyInvokable]
		Delegate = 4096,
		/// <summary>Attribute can be applied to a return value.</summary>
		// Token: 0x040003E3 RID: 995
		[__DynamicallyInvokable]
		ReturnValue = 8192,
		/// <summary>Attribute can be applied to a generic parameter.</summary>
		// Token: 0x040003E4 RID: 996
		[__DynamicallyInvokable]
		GenericParameter = 16384,
		/// <summary>Attribute can be applied to any application element.</summary>
		// Token: 0x040003E5 RID: 997
		[__DynamicallyInvokable]
		All = 32767
	}
}
