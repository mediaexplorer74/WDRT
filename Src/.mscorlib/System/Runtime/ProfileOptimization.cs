using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime
{
	/// <summary>Improves the startup performance of application domains in applications that require the just-in-time (JIT) compiler by performing background compilation of methods that are likely to be executed, based on profiles created during previous compilations.</summary>
	// Token: 0x02000718 RID: 1816
	public static class ProfileOptimization
	{
		// Token: 0x0600514C RID: 20812
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void InternalSetProfileRoot(string directoryPath);

		// Token: 0x0600514D RID: 20813
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void InternalStartProfile(string profile, IntPtr ptrNativeAssemblyLoadContext);

		/// <summary>Enables optimization profiling for the current application domain, and sets the folder where the optimization profile files are stored. On a single-core computer, the method is ignored.</summary>
		/// <param name="directoryPath">The full path to the folder where profile files are stored for the current application domain.</param>
		// Token: 0x0600514E RID: 20814 RVA: 0x0011FE0D File Offset: 0x0011E00D
		[SecurityCritical]
		public static void SetProfileRoot(string directoryPath)
		{
			ProfileOptimization.InternalSetProfileRoot(directoryPath);
		}

		/// <summary>Starts just-in-time (JIT) compilation of the methods that were previously recorded in the specified profile file, on a background thread. Starts the process of recording current method use, which later overwrites the specified profile file.</summary>
		/// <param name="profile">The file name of the profile to use.</param>
		// Token: 0x0600514F RID: 20815 RVA: 0x0011FE15 File Offset: 0x0011E015
		[SecurityCritical]
		public static void StartProfile(string profile)
		{
			ProfileOptimization.InternalStartProfile(profile, IntPtr.Zero);
		}
	}
}
