using System;
using System.Configuration.Install;

namespace System.Management.Instrumentation
{
	/// <summary>Installs an instrumented assembly. To use this default project installer, derive a class from <see cref="T:System.Management.Instrumentation.DefaultManagementProjectInstaller" /> inside the assembly. No methods need to be overridden.  
	///  Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x020000BF RID: 191
	public class DefaultManagementProjectInstaller : Installer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.Instrumentation.DefaultManagementProjectInstaller" /> class. This is the default constructor.</summary>
		// Token: 0x06000515 RID: 1301 RVA: 0x000243C8 File Offset: 0x000225C8
		public DefaultManagementProjectInstaller()
		{
			ManagementInstaller managementInstaller = new ManagementInstaller();
			base.Installers.Add(managementInstaller);
		}
	}
}
