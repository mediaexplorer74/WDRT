using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Allows managed code to call into unmanaged code without a stack walk. This class cannot be inherited.</summary>
	// Token: 0x020001C2 RID: 450
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class SuppressUnmanagedCodeSecurityAttribute : Attribute
	{
	}
}
