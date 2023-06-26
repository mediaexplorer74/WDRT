using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the name of a file containing the key pair used to generate a strong name.</summary>
	// Token: 0x020005BE RID: 1470
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyKeyFileAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="AssemblyKeyFileAttribute" /> class with the name of the file containing the key pair to generate a strong name for the assembly being attributed.</summary>
		/// <param name="keyFile">The name of the file containing the key pair.</param>
		// Token: 0x06004481 RID: 17537 RVA: 0x000FDC3E File Offset: 0x000FBE3E
		[__DynamicallyInvokable]
		public AssemblyKeyFileAttribute(string keyFile)
		{
			this.m_keyFile = keyFile;
		}

		/// <summary>Gets the name of the file containing the key pair used to generate a strong name for the attributed assembly.</summary>
		/// <returns>A string containing the name of the file that contains the key pair.</returns>
		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06004482 RID: 17538 RVA: 0x000FDC4D File Offset: 0x000FBE4D
		[__DynamicallyInvokable]
		public string KeyFile
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_keyFile;
			}
		}

		// Token: 0x04001C11 RID: 7185
		private string m_keyFile;
	}
}
