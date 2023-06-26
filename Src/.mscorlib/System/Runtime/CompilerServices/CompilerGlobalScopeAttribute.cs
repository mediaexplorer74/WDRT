using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that a class should be treated as if it has global scope.</summary>
	// Token: 0x020008B3 RID: 2227
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	[Serializable]
	public class CompilerGlobalScopeAttribute : Attribute
	{
	}
}
