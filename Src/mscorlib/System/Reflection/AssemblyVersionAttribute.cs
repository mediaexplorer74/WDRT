using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the version of the assembly being attributed.</summary>
	// Token: 0x020005BD RID: 1469
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="AssemblyVersionAttribute" /> class with the version number of the assembly being attributed.</summary>
		/// <param name="version">The version number of the attributed assembly.</param>
		// Token: 0x0600447F RID: 17535 RVA: 0x000FDC27 File Offset: 0x000FBE27
		[__DynamicallyInvokable]
		public AssemblyVersionAttribute(string version)
		{
			this.m_version = version;
		}

		/// <summary>Gets the version number of the attributed assembly.</summary>
		/// <returns>A string containing the assembly version number.</returns>
		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x000FDC36 File Offset: 0x000FBE36
		[__DynamicallyInvokable]
		public string Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x04001C10 RID: 7184
		private string m_version;
	}
}
