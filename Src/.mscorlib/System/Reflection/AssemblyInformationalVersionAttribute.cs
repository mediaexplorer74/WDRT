using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines additional version information for an assembly manifest.</summary>
	// Token: 0x020005BA RID: 1466
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyInformationalVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyInformationalVersionAttribute" /> class.</summary>
		/// <param name="informationalVersion">The assembly version information.</param>
		// Token: 0x06004479 RID: 17529 RVA: 0x000FDBD4 File Offset: 0x000FBDD4
		[__DynamicallyInvokable]
		public AssemblyInformationalVersionAttribute(string informationalVersion)
		{
			this.m_informationalVersion = informationalVersion;
		}

		/// <summary>Gets version information.</summary>
		/// <returns>A string containing the version information.</returns>
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x0600447A RID: 17530 RVA: 0x000FDBE3 File Offset: 0x000FBDE3
		[__DynamicallyInvokable]
		public string InformationalVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_informationalVersion;
			}
		}

		// Token: 0x04001C0D RID: 7181
		private string m_informationalVersion;
	}
}
