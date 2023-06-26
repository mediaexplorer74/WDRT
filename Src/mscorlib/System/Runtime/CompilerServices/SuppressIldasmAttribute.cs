using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Prevents the Ildasm.exe (IL Disassembler) from disassembling an assembly. This class cannot be inherited.</summary>
	// Token: 0x020008DB RID: 2267
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module)]
	public sealed class SuppressIldasmAttribute : Attribute
	{
	}
}
