using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Marks each type of member that is defined as a derived class of <see cref="T:System.Reflection.MemberInfo" />.</summary>
	// Token: 0x02000601 RID: 1537
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum MemberTypes
	{
		/// <summary>Specifies that the member is a constructor</summary>
		// Token: 0x04001D66 RID: 7526
		Constructor = 1,
		/// <summary>Specifies that the member is an event.</summary>
		// Token: 0x04001D67 RID: 7527
		Event = 2,
		/// <summary>Specifies that the member is a field.</summary>
		// Token: 0x04001D68 RID: 7528
		Field = 4,
		/// <summary>Specifies that the member is a method.</summary>
		// Token: 0x04001D69 RID: 7529
		Method = 8,
		/// <summary>Specifies that the member is a property.</summary>
		// Token: 0x04001D6A RID: 7530
		Property = 16,
		/// <summary>Specifies that the member is a type.</summary>
		// Token: 0x04001D6B RID: 7531
		TypeInfo = 32,
		/// <summary>Specifies that the member is a custom member type.</summary>
		// Token: 0x04001D6C RID: 7532
		Custom = 64,
		/// <summary>Specifies that the member is a nested type.</summary>
		// Token: 0x04001D6D RID: 7533
		NestedType = 128,
		/// <summary>Specifies all member types.</summary>
		// Token: 0x04001D6E RID: 7534
		All = 191
	}
}
