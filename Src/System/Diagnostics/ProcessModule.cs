using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Represents a.dll or .exe file that is loaded into a particular process.</summary>
	// Token: 0x020004FB RID: 1275
	[Designer("System.Diagnostics.Design.ProcessModuleDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ProcessModule : Component
	{
		// Token: 0x0600304A RID: 12362 RVA: 0x000DB0F8 File Offset: 0x000D92F8
		internal ProcessModule(ModuleInfo moduleInfo)
		{
			this.moduleInfo = moduleInfo;
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000DB10D File Offset: 0x000D930D
		internal void EnsureNtProcessInfo()
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
			}
		}

		/// <summary>Gets the name of the process module.</summary>
		/// <returns>The name of the module.</returns>
		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600304C RID: 12364 RVA: 0x000DB12C File Offset: 0x000D932C
		[MonitoringDescription("ProcModModuleName")]
		public string ModuleName
		{
			get
			{
				return this.moduleInfo.baseName;
			}
		}

		/// <summary>Gets the full path to the module.</summary>
		/// <returns>The fully qualified path that defines the location of the module.</returns>
		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x0600304D RID: 12365 RVA: 0x000DB139 File Offset: 0x000D9339
		[MonitoringDescription("ProcModFileName")]
		public string FileName
		{
			get
			{
				return this.moduleInfo.fileName;
			}
		}

		/// <summary>Gets the memory address where the module was loaded.</summary>
		/// <returns>The load address of the module.</returns>
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x000DB146 File Offset: 0x000D9346
		[MonitoringDescription("ProcModBaseAddress")]
		public IntPtr BaseAddress
		{
			get
			{
				return this.moduleInfo.baseOfDll;
			}
		}

		/// <summary>Gets the amount of memory that is required to load the module.</summary>
		/// <returns>The size, in bytes, of the memory that the module occupies.</returns>
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x000DB153 File Offset: 0x000D9353
		[MonitoringDescription("ProcModModuleMemorySize")]
		public int ModuleMemorySize
		{
			get
			{
				return this.moduleInfo.sizeOfImage;
			}
		}

		/// <summary>Gets the memory address for the function that runs when the system loads and runs the module.</summary>
		/// <returns>The entry point of the module.</returns>
		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x000DB160 File Offset: 0x000D9360
		[MonitoringDescription("ProcModEntryPointAddress")]
		public IntPtr EntryPointAddress
		{
			get
			{
				this.EnsureNtProcessInfo();
				return this.moduleInfo.entryPoint;
			}
		}

		/// <summary>Gets version information about the module.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.FileVersionInfo" /> that contains the module's version information.</returns>
		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06003051 RID: 12369 RVA: 0x000DB173 File Offset: 0x000D9373
		[Browsable(false)]
		public FileVersionInfo FileVersionInfo
		{
			get
			{
				if (this.fileVersionInfo == null)
				{
					this.fileVersionInfo = FileVersionInfo.GetVersionInfo(this.FileName);
				}
				return this.fileVersionInfo;
			}
		}

		/// <summary>Converts the name of the module to a string.</summary>
		/// <returns>The value of the <see cref="P:System.Diagnostics.ProcessModule.ModuleName" /> property.</returns>
		// Token: 0x06003052 RID: 12370 RVA: 0x000DB194 File Offset: 0x000D9394
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} ({1})", new object[]
			{
				base.ToString(),
				this.ModuleName
			});
		}

		// Token: 0x0400287B RID: 10363
		internal ModuleInfo moduleInfo;

		// Token: 0x0400287C RID: 10364
		private FileVersionInfo fileVersionInfo;
	}
}
