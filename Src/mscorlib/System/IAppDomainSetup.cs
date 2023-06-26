using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents assembly binding information that can be added to an instance of <see cref="T:System.AppDomain" />.</summary>
	// Token: 0x0200009F RID: 159
	[Guid("27FFF232-A7A8-40dd-8D4A-734AD59FCD41")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface IAppDomainSetup
	{
		/// <summary>Gets or sets the name of the directory containing the application.</summary>
		/// <returns>A <see cref="T:System.String" /> containg the name of the application base directory.</returns>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600094D RID: 2381
		// (set) Token: 0x0600094E RID: 2382
		string ApplicationBase { get; set; }

		/// <summary>Gets or sets the name of the application.</summary>
		/// <returns>A <see cref="T:System.String" /> that is the name of the application.</returns>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600094F RID: 2383
		// (set) Token: 0x06000950 RID: 2384
		string ApplicationName { get; set; }

		/// <summary>Gets or sets the name of an area specific to the application where files are shadow copied.</summary>
		/// <returns>A <see cref="T:System.String" /> that is the fully-qualified name of the directory path and file name where files are shadow copied.</returns>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000951 RID: 2385
		// (set) Token: 0x06000952 RID: 2386
		string CachePath { get; set; }

		/// <summary>Gets or sets the name of the configuration file for an application domain.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of the configuration file.</returns>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000953 RID: 2387
		// (set) Token: 0x06000954 RID: 2388
		string ConfigurationFile { get; set; }

		/// <summary>Gets or sets the directory where dynamically generated files are stored and accessed.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the directory containing dynamic assemblies.</returns>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000955 RID: 2389
		// (set) Token: 0x06000956 RID: 2390
		string DynamicBase { get; set; }

		/// <summary>Gets or sets the location of the license file associated with this domain.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of the license file.</returns>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000957 RID: 2391
		// (set) Token: 0x06000958 RID: 2392
		string LicenseFile { get; set; }

		/// <summary>Gets or sets the list of directories that is combined with the <see cref="P:System.AppDomainSetup.ApplicationBase" /> directory to probe for private assemblies.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where each name is separated by a semicolon.</returns>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000959 RID: 2393
		// (set) Token: 0x0600095A RID: 2394
		string PrivateBinPath { get; set; }

		/// <summary>Gets or sets the private binary directory path used to locate an application.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where each name is separated by a semicolon.</returns>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600095B RID: 2395
		// (set) Token: 0x0600095C RID: 2396
		string PrivateBinPathProbe { get; set; }

		/// <summary>Gets or sets the names of the directories containing assemblies to be shadow copied.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where each name is separated by a semicolon.</returns>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600095D RID: 2397
		// (set) Token: 0x0600095E RID: 2398
		string ShadowCopyDirectories { get; set; }

		/// <summary>Gets or sets a string that indicates whether shadow copying is turned on or off.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value "true" to indicate that shadow copying is turned on; or "false" to indicate that shadow copying is turned off.</returns>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600095F RID: 2399
		// (set) Token: 0x06000960 RID: 2400
		string ShadowCopyFiles { get; set; }
	}
}
