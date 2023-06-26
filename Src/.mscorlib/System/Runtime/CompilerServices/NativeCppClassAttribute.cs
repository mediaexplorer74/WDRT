using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Applies metadata to an assembly that indicates that a type is an unmanaged type.  This class cannot be inherited.</summary>
	// Token: 0x020008DC RID: 2268
	[AttributeUsage(AttributeTargets.Struct, Inherited = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class NativeCppClassAttribute : Attribute
	{
	}
}
