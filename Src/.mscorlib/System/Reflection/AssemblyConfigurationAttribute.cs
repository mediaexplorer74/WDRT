using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the build configuration, such as retail or debug, for an assembly.</summary>
	// Token: 0x020005B8 RID: 1464
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyConfigurationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyConfigurationAttribute" /> class.</summary>
		/// <param name="configuration">The assembly configuration.</param>
		// Token: 0x06004475 RID: 17525 RVA: 0x000FDBA6 File Offset: 0x000FBDA6
		[__DynamicallyInvokable]
		public AssemblyConfigurationAttribute(string configuration)
		{
			this.m_configuration = configuration;
		}

		/// <summary>Gets assembly configuration information.</summary>
		/// <returns>A string containing the assembly configuration information.</returns>
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06004476 RID: 17526 RVA: 0x000FDBB5 File Offset: 0x000FBDB5
		[__DynamicallyInvokable]
		public string Configuration
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_configuration;
			}
		}

		// Token: 0x04001C0B RID: 7179
		private string m_configuration;
	}
}
