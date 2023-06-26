using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the paths that are used to search for DLLs that provide functions for platform invokes.</summary>
	// Token: 0x02000931 RID: 2353
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method, AllowMultiple = false)]
	[ComVisible(false)]
	[__DynamicallyInvokable]
	public sealed class DefaultDllImportSearchPathsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DefaultDllImportSearchPathsAttribute" /> class, specifying the paths to use when searching for the targets of platform invokes.</summary>
		/// <param name="paths">A bitwise combination of enumeration values that specify the paths that the LoadLibraryEx function searches during platform invokes.</param>
		// Token: 0x0600604F RID: 24655 RVA: 0x0014D2A8 File Offset: 0x0014B4A8
		[__DynamicallyInvokable]
		public DefaultDllImportSearchPathsAttribute(DllImportSearchPath paths)
		{
			this._paths = paths;
		}

		/// <summary>Gets a bitwise combination of enumeration values that specify the paths that the LoadLibraryEx function searches during platform invokes.</summary>
		/// <returns>A bitwise combination of enumeration values that specify search paths for platform invokes.</returns>
		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06006050 RID: 24656 RVA: 0x0014D2B7 File Offset: 0x0014B4B7
		[__DynamicallyInvokable]
		public DllImportSearchPath Paths
		{
			[__DynamicallyInvokable]
			get
			{
				return this._paths;
			}
		}

		// Token: 0x04002B19 RID: 11033
		internal DllImportSearchPath _paths;
	}
}
