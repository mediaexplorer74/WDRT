using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the name of a key container within the CSP containing the key pair used to generate a strong name.</summary>
	// Token: 0x020005C4 RID: 1476
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyKeyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyKeyNameAttribute" /> class with the name of the container holding the key pair used to generate a strong name for the assembly being attributed.</summary>
		/// <param name="keyName">The name of the container containing the key pair.</param>
		// Token: 0x06004493 RID: 17555 RVA: 0x000FDD1B File Offset: 0x000FBF1B
		[__DynamicallyInvokable]
		public AssemblyKeyNameAttribute(string keyName)
		{
			this.m_keyName = keyName;
		}

		/// <summary>Gets the name of the container having the key pair that is used to generate a strong name for the attributed assembly.</summary>
		/// <returns>A string containing the name of the container that has the relevant key pair.</returns>
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06004494 RID: 17556 RVA: 0x000FDD2A File Offset: 0x000FBF2A
		[__DynamicallyInvokable]
		public string KeyName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_keyName;
			}
		}

		// Token: 0x04001C19 RID: 7193
		private string m_keyName;
	}
}
