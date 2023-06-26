using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a collection of <see langword="static" /> methods that return information about the common language runtime environment.</summary>
	// Token: 0x02000952 RID: 2386
	[ComVisible(true)]
	public class RuntimeEnvironment
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.RuntimeEnvironment" /> class.</summary>
		// Token: 0x060061C8 RID: 25032 RVA: 0x0014FCF0 File Offset: 0x0014DEF0
		[Obsolete("Do not create instances of the RuntimeEnvironment class.  Call the static methods directly on this type instead", true)]
		public RuntimeEnvironment()
		{
		}

		// Token: 0x060061C9 RID: 25033
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetModuleFileName();

		// Token: 0x060061CA RID: 25034
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetDeveloperPath();

		// Token: 0x060061CB RID: 25035
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetHostBindingFile();

		// Token: 0x060061CC RID: 25036
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void _GetSystemVersion(StringHandleOnStack retVer);

		/// <summary>Tests whether the specified assembly is loaded in the global assembly cache.</summary>
		/// <param name="a">The assembly to test.</param>
		/// <returns>
		///   <see langword="true" /> if the assembly is loaded in the global assembly cache; otherwise, <see langword="false" />.</returns>
		// Token: 0x060061CD RID: 25037 RVA: 0x0014FCF8 File Offset: 0x0014DEF8
		public static bool FromGlobalAccessCache(Assembly a)
		{
			return a.GlobalAssemblyCache;
		}

		/// <summary>Gets the version number of the common language runtime that is running the current process.</summary>
		/// <returns>A string containing the version number of the common language runtime.</returns>
		// Token: 0x060061CE RID: 25038 RVA: 0x0014FD00 File Offset: 0x0014DF00
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetSystemVersion()
		{
			string text = null;
			RuntimeEnvironment._GetSystemVersion(JitHelpers.GetStringHandleOnStack(ref text));
			return text;
		}

		/// <summary>Returns the directory where the common language runtime is installed.</summary>
		/// <returns>A string that contains the path to the directory where the common language runtime is installed.</returns>
		// Token: 0x060061CF RID: 25039 RVA: 0x0014FD1C File Offset: 0x0014DF1C
		[SecuritySafeCritical]
		public static string GetRuntimeDirectory()
		{
			string runtimeDirectoryImpl = RuntimeEnvironment.GetRuntimeDirectoryImpl();
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, runtimeDirectoryImpl).Demand();
			return runtimeDirectoryImpl;
		}

		// Token: 0x060061D0 RID: 25040
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetRuntimeDirectoryImpl();

		/// <summary>Gets the path to the system configuration file.</summary>
		/// <returns>The path to the system configuration file.</returns>
		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x060061D1 RID: 25041 RVA: 0x0014FD3C File Offset: 0x0014DF3C
		public static string SystemConfigurationFile
		{
			[SecuritySafeCritical]
			get
			{
				StringBuilder stringBuilder = new StringBuilder(260);
				stringBuilder.Append(RuntimeEnvironment.GetRuntimeDirectory());
				stringBuilder.Append(AppDomainSetup.RuntimeConfigurationFile);
				string text = stringBuilder.ToString();
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				return text;
			}
		}

		// Token: 0x060061D2 RID: 25042
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr GetRuntimeInterfaceImpl([MarshalAs(UnmanagedType.LPStruct)] [In] Guid clsid, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid);

		/// <summary>Returns the specified interface on the specified class.</summary>
		/// <param name="clsid">The identifier for the desired class.</param>
		/// <param name="riid">The identifier for the desired interface.</param>
		/// <returns>An unmanaged pointer to the requested interface.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">IUnknown::QueryInterface failure.</exception>
		// Token: 0x060061D3 RID: 25043 RVA: 0x0014FD80 File Offset: 0x0014DF80
		[SecurityCritical]
		[ComVisible(false)]
		public static IntPtr GetRuntimeInterfaceAsIntPtr(Guid clsid, Guid riid)
		{
			return RuntimeEnvironment.GetRuntimeInterfaceImpl(clsid, riid);
		}

		/// <summary>Returns an instance of a type that represents a COM object by a pointer to its <see langword="IUnknown" /> interface.</summary>
		/// <param name="clsid">The identifier for the desired class.</param>
		/// <param name="riid">The identifier for the desired interface.</param>
		/// <returns>An object that represents the specified unmanaged COM object.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">IUnknown::QueryInterface failure.</exception>
		// Token: 0x060061D4 RID: 25044 RVA: 0x0014FD8C File Offset: 0x0014DF8C
		[SecurityCritical]
		[ComVisible(false)]
		public static object GetRuntimeInterfaceAsObject(Guid clsid, Guid riid)
		{
			IntPtr intPtr = IntPtr.Zero;
			object objectForIUnknown;
			try
			{
				intPtr = RuntimeEnvironment.GetRuntimeInterfaceImpl(clsid, riid);
				objectForIUnknown = Marshal.GetObjectForIUnknown(intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.Release(intPtr);
				}
			}
			return objectForIUnknown;
		}
	}
}
