using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.TCEAdapterGen;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a set of services that convert a managed assembly to a COM type library and vice versa.</summary>
	// Token: 0x02000977 RID: 2423
	[Guid("F1C3BF79-C3E4-11d3-88E7-00902754C43A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public sealed class TypeLibConverter : ITypeLibConverter
	{
		/// <summary>Converts a COM type library to an assembly.</summary>
		/// <param name="typeLib">The object that implements the <see langword="ITypeLib" /> interface.</param>
		/// <param name="asmFileName">The file name of the resulting assembly.</param>
		/// <param name="flags">A <see cref="T:System.Runtime.InteropServices.TypeLibImporterFlags" /> value indicating any special settings.</param>
		/// <param name="notifySink">
		///   <see cref="T:System.Runtime.InteropServices.ITypeLibImporterNotifySink" /> interface implemented by the caller.</param>
		/// <param name="publicKey">A <see langword="byte" /> array containing the public key.</param>
		/// <param name="keyPair">A <see cref="T:System.Reflection.StrongNameKeyPair" /> object containing the public and private cryptographic key pair.</param>
		/// <param name="unsafeInterfaces">If <see langword="true" />, the interfaces require link time checks for <see cref="F:System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode" /> permission. If <see langword="false" />, the interfaces require run time checks that require a stack walk and are more expensive, but help provide greater protection.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> object containing the converted type library.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeLib" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="asmFileName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="notifySink" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asmFileName" /> is an empty string.  
		/// -or-  
		/// <paramref name="asmFileName" /> is longer than the system-defined maximum length. For more information, see <see cref="T:System.IO.PathTooLongException" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="flags" /> is not <see cref="F:System.Runtime.InteropServices.TypeLibImporterFlags.PrimaryInteropAssembly" />.  
		/// -or-  
		/// <paramref name="publicKey" /> and <paramref name="keyPair" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">The metadata produced has errors preventing any types from loading.</exception>
		// Token: 0x0600627E RID: 25214 RVA: 0x001527AC File Offset: 0x001509AC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, int flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, bool unsafeInterfaces)
		{
			return this.ConvertTypeLibToAssembly(typeLib, asmFileName, unsafeInterfaces ? TypeLibImporterFlags.UnsafeInterfaces : TypeLibImporterFlags.None, notifySink, publicKey, keyPair, null, null);
		}

		/// <summary>Converts a COM type library to an assembly.</summary>
		/// <param name="typeLib">The object that implements the <see langword="ITypeLib" /> interface.</param>
		/// <param name="asmFileName">The file name of the resulting assembly.</param>
		/// <param name="flags">A <see cref="T:System.Runtime.InteropServices.TypeLibImporterFlags" /> value indicating any special settings.</param>
		/// <param name="notifySink">
		///   <see cref="T:System.Runtime.InteropServices.ITypeLibImporterNotifySink" /> interface implemented by the caller.</param>
		/// <param name="publicKey">A <see langword="byte" /> array containing the public key.</param>
		/// <param name="keyPair">A <see cref="T:System.Reflection.StrongNameKeyPair" /> object containing the public and private cryptographic key pair.</param>
		/// <param name="asmNamespace">The namespace for the resulting assembly.</param>
		/// <param name="asmVersion">The version of the resulting assembly. If <see langword="null" />, the version of the type library is used.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> object containing the converted type library.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeLib" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="asmFileName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="notifySink" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asmFileName" /> is an empty string.  
		/// -or-  
		/// <paramref name="asmFileName" /> is longer than the system-defined maximum length. For more information, see <see cref="T:System.IO.PathTooLongException" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="flags" /> is not <see cref="F:System.Runtime.InteropServices.TypeLibImporterFlags.PrimaryInteropAssembly" />.  
		/// -or-  
		/// <paramref name="publicKey" /> and <paramref name="keyPair" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">The metadata produced has errors preventing any types from loading.</exception>
		// Token: 0x0600627F RID: 25215 RVA: 0x001527D4 File Offset: 0x001509D4
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, string asmNamespace, Version asmVersion)
		{
			if (typeLib == null)
			{
				throw new ArgumentNullException("typeLib");
			}
			if (asmFileName == null)
			{
				throw new ArgumentNullException("asmFileName");
			}
			if (notifySink == null)
			{
				throw new ArgumentNullException("notifySink");
			}
			if (string.Empty.Equals(asmFileName))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileName"), "asmFileName");
			}
			if (asmFileName.Length > 260)
			{
				throw new ArgumentException(Environment.GetResourceString("IO.PathTooLong"), asmFileName);
			}
			if ((flags & TypeLibImporterFlags.PrimaryInteropAssembly) != TypeLibImporterFlags.None && publicKey == null && keyPair == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_PIAMustBeStrongNamed"));
			}
			ArrayList arrayList = null;
			AssemblyNameFlags assemblyNameFlags = AssemblyNameFlags.None;
			AssemblyName assemblyNameFromTypelib = TypeLibConverter.GetAssemblyNameFromTypelib(typeLib, asmFileName, publicKey, keyPair, asmVersion, assemblyNameFlags);
			AssemblyBuilder assemblyBuilder = TypeLibConverter.CreateAssemblyForTypeLib(typeLib, asmFileName, assemblyNameFromTypelib, (flags & TypeLibImporterFlags.PrimaryInteropAssembly) > TypeLibImporterFlags.None, (flags & TypeLibImporterFlags.ReflectionOnlyLoading) > TypeLibImporterFlags.None, (flags & TypeLibImporterFlags.NoDefineVersionResource) > TypeLibImporterFlags.None);
			string fileName = Path.GetFileName(asmFileName);
			ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(fileName, fileName);
			if (asmNamespace == null)
			{
				asmNamespace = assemblyNameFromTypelib.Name;
			}
			TypeLibConverter.TypeResolveHandler typeResolveHandler = new TypeLibConverter.TypeResolveHandler(moduleBuilder, notifySink);
			AppDomain domain = Thread.GetDomain();
			ResolveEventHandler resolveEventHandler = new ResolveEventHandler(typeResolveHandler.ResolveEvent);
			ResolveEventHandler resolveEventHandler2 = new ResolveEventHandler(typeResolveHandler.ResolveAsmEvent);
			ResolveEventHandler resolveEventHandler3 = new ResolveEventHandler(typeResolveHandler.ResolveROAsmEvent);
			domain.TypeResolve += resolveEventHandler;
			domain.AssemblyResolve += resolveEventHandler2;
			domain.ReflectionOnlyAssemblyResolve += resolveEventHandler3;
			TypeLibConverter.nConvertTypeLibToMetadata(typeLib, assemblyBuilder.InternalAssembly, moduleBuilder.InternalModule, asmNamespace, flags, typeResolveHandler, out arrayList);
			TypeLibConverter.UpdateComTypesInAssembly(assemblyBuilder, moduleBuilder);
			if (arrayList.Count > 0)
			{
				new TCEAdapterGenerator().Process(moduleBuilder, arrayList);
			}
			domain.TypeResolve -= resolveEventHandler;
			domain.AssemblyResolve -= resolveEventHandler2;
			domain.ReflectionOnlyAssemblyResolve -= resolveEventHandler3;
			return assemblyBuilder;
		}

		/// <summary>Converts an assembly to a COM type library.</summary>
		/// <param name="assembly">The assembly to convert.</param>
		/// <param name="strTypeLibName">The file name of the resulting type library.</param>
		/// <param name="flags">A <see cref="T:System.Runtime.InteropServices.TypeLibExporterFlags" /> value indicating any special settings.</param>
		/// <param name="notifySink">The <see cref="T:System.Runtime.InteropServices.ITypeLibExporterNotifySink" /> interface implemented by the caller.</param>
		/// <returns>An object that implements the <see langword="ITypeLib" /> interface.</returns>
		// Token: 0x06006280 RID: 25216 RVA: 0x00152970 File Offset: 0x00150B70
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public object ConvertAssemblyToTypeLib(Assembly assembly, string strTypeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink)
		{
			AssemblyBuilder assemblyBuilder = assembly as AssemblyBuilder;
			RuntimeAssembly runtimeAssembly;
			if (assemblyBuilder != null)
			{
				runtimeAssembly = assemblyBuilder.InternalAssembly;
			}
			else
			{
				runtimeAssembly = assembly as RuntimeAssembly;
			}
			return TypeLibConverter.nConvertAssemblyToTypeLib(runtimeAssembly, strTypeLibName, flags, notifySink);
		}

		/// <summary>Gets the name and code base of a primary interop assembly for a specified type library.</summary>
		/// <param name="g">The GUID of the type library.</param>
		/// <param name="major">The major version number of the type library.</param>
		/// <param name="minor">The minor version number of the type library.</param>
		/// <param name="lcid">The LCID of the type library.</param>
		/// <param name="asmName">On successful return, the name of the primary interop assembly associated with <paramref name="g" />.</param>
		/// <param name="asmCodeBase">On successful return, the code base of the primary interop assembly associated with <paramref name="g" />.</param>
		/// <returns>
		///   <see langword="true" /> if the primary interop assembly was found in the registry; otherwise <see langword="false" />.</returns>
		// Token: 0x06006281 RID: 25217 RVA: 0x001529A8 File Offset: 0x00150BA8
		public bool GetPrimaryInteropAssembly(Guid g, int major, int minor, int lcid, out string asmName, out string asmCodeBase)
		{
			string text = "{" + g.ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
			string text2 = major.ToString("x", CultureInfo.InvariantCulture) + "." + minor.ToString("x", CultureInfo.InvariantCulture);
			asmName = null;
			asmCodeBase = null;
			using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("TypeLib", false))
			{
				if (registryKey != null)
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(text))
					{
						if (registryKey2 != null)
						{
							using (RegistryKey registryKey3 = registryKey2.OpenSubKey(text2, false))
							{
								if (registryKey3 != null)
								{
									asmName = (string)registryKey3.GetValue("PrimaryInteropAssemblyName");
									asmCodeBase = (string)registryKey3.GetValue("PrimaryInteropAssemblyCodeBase");
								}
							}
						}
					}
				}
			}
			return asmName != null;
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x00152ABC File Offset: 0x00150CBC
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static AssemblyBuilder CreateAssemblyForTypeLib(object typeLib, string asmFileName, AssemblyName asmName, bool bPrimaryInteropAssembly, bool bReflectionOnly, bool bNoDefineVersionResource)
		{
			AppDomain domain = Thread.GetDomain();
			string text = null;
			if (asmFileName != null)
			{
				text = Path.GetDirectoryName(asmFileName);
				if (string.IsNullOrEmpty(text))
				{
					text = null;
				}
			}
			AssemblyBuilderAccess assemblyBuilderAccess;
			if (bReflectionOnly)
			{
				assemblyBuilderAccess = AssemblyBuilderAccess.ReflectionOnly;
			}
			else
			{
				assemblyBuilderAccess = AssemblyBuilderAccess.RunAndSave;
			}
			List<CustomAttributeBuilder> list = new List<CustomAttributeBuilder>();
			ConstructorInfo constructor = typeof(SecurityRulesAttribute).GetConstructor(new Type[] { typeof(SecurityRuleSet) });
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(constructor, new object[] { SecurityRuleSet.Level2 });
			list.Add(customAttributeBuilder);
			AssemblyBuilder assemblyBuilder = domain.DefineDynamicAssembly(asmName, assemblyBuilderAccess, text, false, list);
			TypeLibConverter.SetGuidAttributeOnAssembly(assemblyBuilder, typeLib);
			TypeLibConverter.SetImportedFromTypeLibAttrOnAssembly(assemblyBuilder, typeLib);
			if (bNoDefineVersionResource)
			{
				TypeLibConverter.SetTypeLibVersionAttribute(assemblyBuilder, typeLib);
			}
			else
			{
				TypeLibConverter.SetVersionInformation(assemblyBuilder, typeLib, asmName);
			}
			if (bPrimaryInteropAssembly)
			{
				TypeLibConverter.SetPIAAttributeOnAssembly(assemblyBuilder, typeLib);
			}
			return assemblyBuilder;
		}

		// Token: 0x06006283 RID: 25219 RVA: 0x00152B78 File Offset: 0x00150D78
		[SecurityCritical]
		internal static AssemblyName GetAssemblyNameFromTypelib(object typeLib, string asmFileName, byte[] publicKey, StrongNameKeyPair keyPair, Version asmVersion, AssemblyNameFlags asmNameFlags)
		{
			string text = null;
			string text2 = null;
			int num = 0;
			string text3 = null;
			ITypeLib typeLib2 = (ITypeLib)typeLib;
			typeLib2.GetDocumentation(-1, out text, out text2, out num, out text3);
			if (asmFileName == null)
			{
				asmFileName = text;
			}
			else
			{
				string fileName = Path.GetFileName(asmFileName);
				string extension = Path.GetExtension(asmFileName);
				if (!".dll".Equals(extension, StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileExtension"));
				}
				asmFileName = fileName.Substring(0, fileName.Length - ".dll".Length);
			}
			if (asmVersion == null)
			{
				int num2;
				int num3;
				Marshal.GetTypeLibVersion(typeLib2, out num2, out num3);
				asmVersion = new Version(num2, num3, 0, 0);
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Init(asmFileName, publicKey, null, asmVersion, null, AssemblyHashAlgorithm.None, AssemblyVersionCompatibility.SameMachine, null, asmNameFlags, keyPair);
			return assemblyName;
		}

		// Token: 0x06006284 RID: 25220 RVA: 0x00152C3C File Offset: 0x00150E3C
		private static void UpdateComTypesInAssembly(AssemblyBuilder asmBldr, ModuleBuilder modBldr)
		{
			AssemblyBuilderData assemblyData = asmBldr.m_assemblyData;
			Type[] types = modBldr.GetTypes();
			int num = types.Length;
			for (int i = 0; i < num; i++)
			{
				assemblyData.AddPublicComType(types[i]);
			}
		}

		// Token: 0x06006285 RID: 25221 RVA: 0x00152C70 File Offset: 0x00150E70
		[SecurityCritical]
		private static void SetGuidAttributeOnAssembly(AssemblyBuilder asmBldr, object typeLib)
		{
			Type[] array = new Type[] { typeof(string) };
			ConstructorInfo constructor = typeof(GuidAttribute).GetConstructor(array);
			object[] array2 = new object[] { Marshal.GetTypeLibGuid((ITypeLib)typeLib).ToString() };
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(constructor, array2);
			asmBldr.SetCustomAttribute(customAttributeBuilder);
		}

		// Token: 0x06006286 RID: 25222 RVA: 0x00152CD8 File Offset: 0x00150ED8
		[SecurityCritical]
		private static void SetImportedFromTypeLibAttrOnAssembly(AssemblyBuilder asmBldr, object typeLib)
		{
			Type[] array = new Type[] { typeof(string) };
			ConstructorInfo constructor = typeof(ImportedFromTypeLibAttribute).GetConstructor(array);
			string typeLibName = Marshal.GetTypeLibName((ITypeLib)typeLib);
			object[] array2 = new object[] { typeLibName };
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(constructor, array2);
			asmBldr.SetCustomAttribute(customAttributeBuilder);
		}

		// Token: 0x06006287 RID: 25223 RVA: 0x00152D34 File Offset: 0x00150F34
		[SecurityCritical]
		private static void SetTypeLibVersionAttribute(AssemblyBuilder asmBldr, object typeLib)
		{
			Type[] array = new Type[]
			{
				typeof(int),
				typeof(int)
			};
			ConstructorInfo constructor = typeof(TypeLibVersionAttribute).GetConstructor(array);
			int num;
			int num2;
			Marshal.GetTypeLibVersion((ITypeLib)typeLib, out num, out num2);
			object[] array2 = new object[] { num, num2 };
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(constructor, array2);
			asmBldr.SetCustomAttribute(customAttributeBuilder);
		}

		// Token: 0x06006288 RID: 25224 RVA: 0x00152DB0 File Offset: 0x00150FB0
		[SecurityCritical]
		private static void SetVersionInformation(AssemblyBuilder asmBldr, object typeLib, AssemblyName asmName)
		{
			string text = null;
			string text2 = null;
			int num = 0;
			string text3 = null;
			ITypeLib typeLib2 = (ITypeLib)typeLib;
			typeLib2.GetDocumentation(-1, out text, out text2, out num, out text3);
			string text4 = string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("TypeLibConverter_ImportedTypeLibProductName"), text);
			asmBldr.DefineVersionInfoResource(text4, asmName.Version.ToString(), null, null, null);
			TypeLibConverter.SetTypeLibVersionAttribute(asmBldr, typeLib);
		}

		// Token: 0x06006289 RID: 25225 RVA: 0x00152E14 File Offset: 0x00151014
		[SecurityCritical]
		private static void SetPIAAttributeOnAssembly(AssemblyBuilder asmBldr, object typeLib)
		{
			IntPtr zero = IntPtr.Zero;
			ITypeLib typeLib2 = (ITypeLib)typeLib;
			int num = 0;
			int num2 = 0;
			Type[] array = new Type[]
			{
				typeof(int),
				typeof(int)
			};
			ConstructorInfo constructor = typeof(PrimaryInteropAssemblyAttribute).GetConstructor(array);
			try
			{
				typeLib2.GetLibAttr(out zero);
				TYPELIBATTR typelibattr = (TYPELIBATTR)Marshal.PtrToStructure(zero, typeof(TYPELIBATTR));
				num = (int)typelibattr.wMajorVerNum;
				num2 = (int)typelibattr.wMinorVerNum;
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					typeLib2.ReleaseTLibAttr(zero);
				}
			}
			object[] array2 = new object[] { num, num2 };
			CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(constructor, array2);
			asmBldr.SetCustomAttribute(customAttributeBuilder);
		}

		// Token: 0x0600628A RID: 25226
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nConvertTypeLibToMetadata(object typeLib, RuntimeAssembly asmBldr, RuntimeModule modBldr, string nameSpace, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, out ArrayList eventItfInfoList);

		// Token: 0x0600628B RID: 25227
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object nConvertAssemblyToTypeLib(RuntimeAssembly assembly, string strTypeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink);

		// Token: 0x0600628C RID: 25228
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void LoadInMemoryTypeByName(RuntimeModule module, string className);

		// Token: 0x04002BE6 RID: 11238
		private const string s_strTypeLibAssemblyTitlePrefix = "TypeLib ";

		// Token: 0x04002BE7 RID: 11239
		private const string s_strTypeLibAssemblyDescPrefix = "Assembly generated from typelib ";

		// Token: 0x04002BE8 RID: 11240
		private const int MAX_NAMESPACE_LENGTH = 1024;

		// Token: 0x02000C93 RID: 3219
		private class TypeResolveHandler : ITypeLibImporterNotifySink
		{
			// Token: 0x0600712F RID: 28975 RVA: 0x00186B2E File Offset: 0x00184D2E
			public TypeResolveHandler(ModuleBuilder mod, ITypeLibImporterNotifySink userSink)
			{
				this.m_Module = mod;
				this.m_UserSink = userSink;
			}

			// Token: 0x06007130 RID: 28976 RVA: 0x00186B4F File Offset: 0x00184D4F
			public void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg)
			{
				this.m_UserSink.ReportEvent(eventKind, eventCode, eventMsg);
			}

			// Token: 0x06007131 RID: 28977 RVA: 0x00186B60 File Offset: 0x00184D60
			public Assembly ResolveRef(object typeLib)
			{
				Assembly assembly = this.m_UserSink.ResolveRef(typeLib);
				if (assembly == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
				if (runtimeAssembly == null)
				{
					AssemblyBuilder assemblyBuilder = assembly as AssemblyBuilder;
					if (assemblyBuilder != null)
					{
						runtimeAssembly = assemblyBuilder.InternalAssembly;
					}
				}
				if (runtimeAssembly == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
				}
				this.m_AsmList.Add(runtimeAssembly);
				return runtimeAssembly;
			}

			// Token: 0x06007132 RID: 28978 RVA: 0x00186BD8 File Offset: 0x00184DD8
			[SecurityCritical]
			public Assembly ResolveEvent(object sender, ResolveEventArgs args)
			{
				try
				{
					TypeLibConverter.LoadInMemoryTypeByName(this.m_Module.GetNativeHandle(), args.Name);
					return this.m_Module.Assembly;
				}
				catch (TypeLoadException ex)
				{
					if (ex.ResourceId != -2146233054)
					{
						throw;
					}
				}
				foreach (RuntimeAssembly runtimeAssembly in this.m_AsmList)
				{
					try
					{
						runtimeAssembly.GetType(args.Name, true, false);
						return runtimeAssembly;
					}
					catch (TypeLoadException ex2)
					{
						if (ex2._HResult != -2146233054)
						{
							throw;
						}
					}
				}
				return null;
			}

			// Token: 0x06007133 RID: 28979 RVA: 0x00186C9C File Offset: 0x00184E9C
			public Assembly ResolveAsmEvent(object sender, ResolveEventArgs args)
			{
				foreach (RuntimeAssembly runtimeAssembly in this.m_AsmList)
				{
					if (string.Compare(runtimeAssembly.FullName, args.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return runtimeAssembly;
					}
				}
				return null;
			}

			// Token: 0x06007134 RID: 28980 RVA: 0x00186D04 File Offset: 0x00184F04
			public Assembly ResolveROAsmEvent(object sender, ResolveEventArgs args)
			{
				foreach (RuntimeAssembly runtimeAssembly in this.m_AsmList)
				{
					if (string.Compare(runtimeAssembly.FullName, args.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return runtimeAssembly;
					}
				}
				string text = AppDomain.CurrentDomain.ApplyPolicy(args.Name);
				return Assembly.ReflectionOnlyLoad(text);
			}

			// Token: 0x04003852 RID: 14418
			private ModuleBuilder m_Module;

			// Token: 0x04003853 RID: 14419
			private ITypeLibImporterNotifySink m_UserSink;

			// Token: 0x04003854 RID: 14420
			private List<RuntimeAssembly> m_AsmList = new List<RuntimeAssembly>();
		}
	}
}
