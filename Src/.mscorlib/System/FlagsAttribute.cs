using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that an enumeration can be treated as a bit field; that is, a set of flags.</summary>
	// Token: 0x020000E3 RID: 227
	[AttributeUsage(AttributeTargets.Enum, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class FlagsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.FlagsAttribute" /> class.</summary>
		// Token: 0x06000E82 RID: 3714 RVA: 0x0002CCB7 File Offset: 0x0002AEB7
		[__DynamicallyInvokable]
		public FlagsAttribute()
		{
		}
	}
}
