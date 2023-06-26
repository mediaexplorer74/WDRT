using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Provides an event for resolving reflection-only type requests for types that are provided by Windows Metadata files, and methods for performing the resolution.</summary>
	// Token: 0x020009F5 RID: 2549
	public static class WindowsRuntimeMetadata
	{
		/// <summary>Locates the Windows Metadata files for the specified namespace, given the specified locations to search.</summary>
		/// <param name="namespaceName">The namespace to resolve.</param>
		/// <param name="packageGraphFilePaths">The application paths to search for Windows Metadata files, or <see langword="null" /> to search only for Windows Metadata files from the operating system installation.</param>
		/// <returns>An enumerable list of strings that represent the Windows Metadata files that define <paramref name="namespaceName" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system version does not support the Windows Runtime.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceName" /> is <see langword="null" />.</exception>
		// Token: 0x060064F3 RID: 25843 RVA: 0x001590A2 File Offset: 0x001572A2
		[SecurityCritical]
		public static IEnumerable<string> ResolveNamespace(string namespaceName, IEnumerable<string> packageGraphFilePaths)
		{
			return WindowsRuntimeMetadata.ResolveNamespace(namespaceName, null, packageGraphFilePaths);
		}

		/// <summary>Locates the Windows Metadata files for the specified namespace, given the specified locations to search.</summary>
		/// <param name="namespaceName">The namespace to resolve.</param>
		/// <param name="windowsSdkFilePath">The path to search for Windows Metadata files provided by the SDK, or <see langword="null" /> to search for Windows Metadata files from the operating system installation.</param>
		/// <param name="packageGraphFilePaths">The application paths to search for Windows Metadata files.</param>
		/// <returns>An enumerable list of strings that represent the Windows Metadata files that define <paramref name="namespaceName" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system version does not support the Windows Runtime.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceName" /> is <see langword="null" />.</exception>
		// Token: 0x060064F4 RID: 25844 RVA: 0x001590AC File Offset: 0x001572AC
		[SecurityCritical]
		public static IEnumerable<string> ResolveNamespace(string namespaceName, string windowsSdkFilePath, IEnumerable<string> packageGraphFilePaths)
		{
			if (namespaceName == null)
			{
				throw new ArgumentNullException("namespaceName");
			}
			string[] array = null;
			if (packageGraphFilePaths != null)
			{
				List<string> list = new List<string>(packageGraphFilePaths);
				array = new string[list.Count];
				int num = 0;
				foreach (string text in list)
				{
					array[num] = text;
					num++;
				}
			}
			string[] array2 = null;
			WindowsRuntimeMetadata.nResolveNamespace(namespaceName, windowsSdkFilePath, array, (array == null) ? 0 : array.Length, JitHelpers.GetObjectHandleOnStack<string[]>(ref array2));
			return array2;
		}

		// Token: 0x060064F5 RID: 25845
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void nResolveNamespace(string namespaceName, string windowsSdkFilePath, string[] packageGraphFilePaths, int cPackageGraphFilePaths, ObjectHandleOnStack retFileNames);

		/// <summary>Occurs when the resolution of a Windows Metadata file fails in the reflection-only context.</summary>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060064F6 RID: 25846 RVA: 0x00159144 File Offset: 0x00157344
		// (remove) Token: 0x060064F7 RID: 25847 RVA: 0x00159178 File Offset: 0x00157378
		[method: SecurityCritical]
		public static event EventHandler<NamespaceResolveEventArgs> ReflectionOnlyNamespaceResolve;

		// Token: 0x060064F8 RID: 25848 RVA: 0x001591AC File Offset: 0x001573AC
		internal static RuntimeAssembly[] OnReflectionOnlyNamespaceResolveEvent(AppDomain appDomain, RuntimeAssembly assembly, string namespaceName)
		{
			EventHandler<NamespaceResolveEventArgs> reflectionOnlyNamespaceResolve = WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve;
			if (reflectionOnlyNamespaceResolve != null)
			{
				Delegate[] invocationList = reflectionOnlyNamespaceResolve.GetInvocationList();
				int num = invocationList.Length;
				for (int i = 0; i < num; i++)
				{
					NamespaceResolveEventArgs namespaceResolveEventArgs = new NamespaceResolveEventArgs(namespaceName, assembly);
					((EventHandler<NamespaceResolveEventArgs>)invocationList[i])(appDomain, namespaceResolveEventArgs);
					Collection<Assembly> resolvedAssemblies = namespaceResolveEventArgs.ResolvedAssemblies;
					if (resolvedAssemblies.Count > 0)
					{
						RuntimeAssembly[] array = new RuntimeAssembly[resolvedAssemblies.Count];
						int num2 = 0;
						foreach (Assembly assembly2 in resolvedAssemblies)
						{
							array[num2] = AppDomain.GetRuntimeAssembly(assembly2);
							num2++;
						}
						return array;
					}
				}
			}
			return null;
		}

		/// <summary>Occurs when the resolution of a Windows Metadata file fails in the design environment.</summary>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060064F9 RID: 25849 RVA: 0x00159270 File Offset: 0x00157470
		// (remove) Token: 0x060064FA RID: 25850 RVA: 0x001592A4 File Offset: 0x001574A4
		[method: SecurityCritical]
		public static event EventHandler<DesignerNamespaceResolveEventArgs> DesignerNamespaceResolve;

		// Token: 0x060064FB RID: 25851 RVA: 0x001592D8 File Offset: 0x001574D8
		internal static string[] OnDesignerNamespaceResolveEvent(AppDomain appDomain, string namespaceName)
		{
			EventHandler<DesignerNamespaceResolveEventArgs> designerNamespaceResolve = WindowsRuntimeMetadata.DesignerNamespaceResolve;
			if (designerNamespaceResolve != null)
			{
				Delegate[] invocationList = designerNamespaceResolve.GetInvocationList();
				int num = invocationList.Length;
				for (int i = 0; i < num; i++)
				{
					DesignerNamespaceResolveEventArgs designerNamespaceResolveEventArgs = new DesignerNamespaceResolveEventArgs(namespaceName);
					((EventHandler<DesignerNamespaceResolveEventArgs>)invocationList[i])(appDomain, designerNamespaceResolveEventArgs);
					Collection<string> resolvedAssemblyFiles = designerNamespaceResolveEventArgs.ResolvedAssemblyFiles;
					if (resolvedAssemblyFiles.Count > 0)
					{
						string[] array = new string[resolvedAssemblyFiles.Count];
						int num2 = 0;
						foreach (string text in resolvedAssemblyFiles)
						{
							if (string.IsNullOrEmpty(text))
							{
								throw new ArgumentException(Environment.GetResourceString("Arg_EmptyOrNullString"), "DesignerNamespaceResolveEventArgs.ResolvedAssemblyFiles");
							}
							array[num2] = text;
							num2++;
						}
						return array;
					}
				}
			}
			return null;
		}
	}
}
