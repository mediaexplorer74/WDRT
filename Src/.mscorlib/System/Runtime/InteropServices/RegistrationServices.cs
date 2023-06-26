using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a set of services for registering and unregistering managed assemblies for use from COM.</summary>
	// Token: 0x02000974 RID: 2420
	[Guid("475E398F-8AFA-43a7-A3BE-F4EF8D6787C9")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class RegistrationServices : IRegistrationServices
	{
		/// <summary>Registers the classes in a managed assembly to enable creation from COM.</summary>
		/// <param name="assembly">The assembly to be registered.</param>
		/// <param name="flags">An <see cref="T:System.Runtime.InteropServices.AssemblyRegistrationFlags" /> value indicating any special settings used when registering <paramref name="assembly" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="assembly" /> contains types that were successfully registered; otherwise <see langword="false" /> if the assembly contains no eligible types.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The full name of <paramref name="assembly" /> is <see langword="null" />.  
		///  -or-  
		///  A method marked with <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> is not <see langword="static" />.  
		///  -or-  
		///  There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> at a given level of the hierarchy.  
		///  -or-  
		///  The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> is not valid.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A user-defined custom registration function (marked with the <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> attribute) throws an exception.</exception>
		// Token: 0x06006259 RID: 25177 RVA: 0x00150F38 File Offset: 0x0014F138
		[SecurityCritical]
		public virtual bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (assembly.ReflectionOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsmLoadedForReflectionOnly"));
			}
			RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
			if (runtimeAssembly == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
			}
			string fullName = assembly.FullName;
			if (fullName == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoAsmName"));
			}
			string text = null;
			if ((flags & AssemblyRegistrationFlags.SetCodeBase) != AssemblyRegistrationFlags.None)
			{
				text = runtimeAssembly.GetCodeBase(false);
				if (text == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoAsmCodeBase"));
				}
			}
			Type[] registrableTypesInAssembly = this.GetRegistrableTypesInAssembly(assembly);
			int num = registrableTypesInAssembly.Length;
			string text2 = runtimeAssembly.GetVersion().ToString();
			string imageRuntimeVersion = assembly.ImageRuntimeVersion;
			for (int i = 0; i < num; i++)
			{
				if (this.IsRegisteredAsValueType(registrableTypesInAssembly[i]))
				{
					this.RegisterValueType(registrableTypesInAssembly[i], fullName, text2, text, imageRuntimeVersion);
				}
				else if (this.TypeRepresentsComType(registrableTypesInAssembly[i]))
				{
					this.RegisterComImportedType(registrableTypesInAssembly[i], fullName, text2, text, imageRuntimeVersion);
				}
				else
				{
					this.RegisterManagedType(registrableTypesInAssembly[i], fullName, text2, text, imageRuntimeVersion);
				}
				this.CallUserDefinedRegistrationMethod(registrableTypesInAssembly[i], true);
			}
			object[] customAttributes = assembly.GetCustomAttributes(typeof(PrimaryInteropAssemblyAttribute), false);
			int num2 = customAttributes.Length;
			for (int j = 0; j < num2; j++)
			{
				this.RegisterPrimaryInteropAssembly(runtimeAssembly, text, (PrimaryInteropAssemblyAttribute)customAttributes[j]);
			}
			return registrableTypesInAssembly.Length != 0 || num2 > 0;
		}

		/// <summary>Unregisters the classes in a managed assembly.</summary>
		/// <param name="assembly">The assembly to be unregistered.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="assembly" /> contains types that were successfully unregistered; otherwise <see langword="false" /> if the assembly contains no eligible types.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The full name of <paramref name="assembly" /> is <see langword="null" />.  
		///  -or-  
		///  A method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not <see langword="static" />.  
		///  -or-  
		///  There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> at a given level of the hierarchy.  
		///  -or-  
		///  The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not valid.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A user-defined custom unregistration function (marked with the <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> attribute) throws an exception.</exception>
		// Token: 0x0600625A RID: 25178 RVA: 0x001510A0 File Offset: 0x0014F2A0
		[SecurityCritical]
		public virtual bool UnregisterAssembly(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (assembly.ReflectionOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsmLoadedForReflectionOnly"));
			}
			RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
			if (runtimeAssembly == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
			}
			bool flag = true;
			Type[] registrableTypesInAssembly = this.GetRegistrableTypesInAssembly(assembly);
			int num = registrableTypesInAssembly.Length;
			string text = runtimeAssembly.GetVersion().ToString();
			for (int i = 0; i < num; i++)
			{
				this.CallUserDefinedRegistrationMethod(registrableTypesInAssembly[i], false);
				if (this.IsRegisteredAsValueType(registrableTypesInAssembly[i]))
				{
					if (!this.UnregisterValueType(registrableTypesInAssembly[i], text))
					{
						flag = false;
					}
				}
				else if (this.TypeRepresentsComType(registrableTypesInAssembly[i]))
				{
					if (!this.UnregisterComImportedType(registrableTypesInAssembly[i], text))
					{
						flag = false;
					}
				}
				else if (!this.UnregisterManagedType(registrableTypesInAssembly[i], text))
				{
					flag = false;
				}
			}
			object[] customAttributes = assembly.GetCustomAttributes(typeof(PrimaryInteropAssemblyAttribute), false);
			int num2 = customAttributes.Length;
			if (flag)
			{
				for (int j = 0; j < num2; j++)
				{
					this.UnregisterPrimaryInteropAssembly(assembly, (PrimaryInteropAssemblyAttribute)customAttributes[j]);
				}
			}
			return registrableTypesInAssembly.Length != 0 || num2 > 0;
		}

		/// <summary>Retrieves a list of classes in an assembly that would be registered by a call to <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterAssembly(System.Reflection.Assembly,System.Runtime.InteropServices.AssemblyRegistrationFlags)" />.</summary>
		/// <param name="assembly">The assembly to search for classes.</param>
		/// <returns>A <see cref="T:System.Type" /> array containing a list of classes in <paramref name="assembly" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assembly" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600625B RID: 25179 RVA: 0x001511C8 File Offset: 0x0014F3C8
		[SecurityCritical]
		public virtual Type[] GetRegistrableTypesInAssembly(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!(assembly is RuntimeAssembly))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "assembly");
			}
			Type[] exportedTypes = assembly.GetExportedTypes();
			int num = exportedTypes.Length;
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < num; i++)
			{
				Type type = exportedTypes[i];
				if (this.TypeRequiresRegistration(type))
				{
					arrayList.Add(type);
				}
			}
			Type[] array = new Type[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		/// <summary>Retrieves the COM ProgID for the specified type.</summary>
		/// <param name="type">The type corresponding to the ProgID that is being requested.</param>
		/// <returns>The ProgID for the specified type.</returns>
		// Token: 0x0600625C RID: 25180 RVA: 0x00151254 File Offset: 0x0014F454
		[SecurityCritical]
		public virtual string GetProgIdForType(Type type)
		{
			return Marshal.GenerateProgIdForType(type);
		}

		/// <summary>Registers the specified type with COM using the specified GUID.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to be registered for use from COM.</param>
		/// <param name="g">The <see cref="T:System.Guid" /> used to register the specified type.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter cannot be created.</exception>
		// Token: 0x0600625D RID: 25181 RVA: 0x0015125C File Offset: 0x0014F45C
		[SecurityCritical]
		public virtual void RegisterTypeForComClients(Type type, ref Guid g)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type as RuntimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
			}
			if (!this.TypeRequiresRegistration(type))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TypeMustBeComCreatable"), "type");
			}
			RegistrationServices.RegisterTypeForComClientsNative(type, ref g);
		}

		/// <summary>Returns the GUID of the COM category that contains the managed classes.</summary>
		/// <returns>The GUID of the COM category that contains the managed classes.</returns>
		// Token: 0x0600625E RID: 25182 RVA: 0x001512C5 File Offset: 0x0014F4C5
		public virtual Guid GetManagedCategoryGuid()
		{
			return RegistrationServices.s_ManagedCategoryGuid;
		}

		/// <summary>Determines whether the specified type requires registration.</summary>
		/// <param name="type">The type to check for COM registration requirements.</param>
		/// <returns>
		///   <see langword="true" /> if the type must be registered for use from COM; otherwise <see langword="false" />.</returns>
		// Token: 0x0600625F RID: 25183 RVA: 0x001512CC File Offset: 0x0014F4CC
		[SecurityCritical]
		public virtual bool TypeRequiresRegistration(Type type)
		{
			return RegistrationServices.TypeRequiresRegistrationHelper(type);
		}

		/// <summary>Indicates whether a type is marked with the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />, or derives from a type marked with the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> and shares the same GUID as the parent.</summary>
		/// <param name="type">The type to check for being a COM type.</param>
		/// <returns>
		///   <see langword="true" /> if a type is marked with the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />, or derives from a type marked with the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> and shares the same GUID as the parent; otherwise <see langword="false" />.</returns>
		// Token: 0x06006260 RID: 25184 RVA: 0x001512D4 File Offset: 0x0014F4D4
		[SecuritySafeCritical]
		public virtual bool TypeRepresentsComType(Type type)
		{
			if (!type.IsCOMObject)
			{
				return false;
			}
			if (type.IsImport)
			{
				return true;
			}
			Type baseComImportType = this.GetBaseComImportType(type);
			return Marshal.GenerateGuidForType(type) == Marshal.GenerateGuidForType(baseComImportType);
		}

		/// <summary>Registers the specified type with COM using the specified execution context and connection type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> object to register for use from COM.</param>
		/// <param name="classContext">One of the <see cref="T:System.Runtime.InteropServices.RegistrationClassContext" /> values that indicates the context in which the executable code will be run.</param>
		/// <param name="flags">One of the <see cref="T:System.Runtime.InteropServices.RegistrationConnectionType" /> values that specifies how connections are made to the class object.</param>
		/// <returns>An integer that represents a cookie value.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter cannot be created.</exception>
		// Token: 0x06006261 RID: 25185 RVA: 0x00151314 File Offset: 0x0014F514
		[SecurityCritical]
		[ComVisible(false)]
		public virtual int RegisterTypeForComClients(Type type, RegistrationClassContext classContext, RegistrationConnectionType flags)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type as RuntimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
			}
			if (!this.TypeRequiresRegistration(type))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TypeMustBeComCreatable"), "type");
			}
			return RegistrationServices.RegisterTypeForComClientsExNative(type, classContext, flags);
		}

		/// <summary>Removes references to a type registered with the <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterTypeForComClients(System.Type,System.Runtime.InteropServices.RegistrationClassContext,System.Runtime.InteropServices.RegistrationConnectionType)" /> method.</summary>
		/// <param name="cookie">The cookie value returned by a previous call to the <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterTypeForComClients(System.Type,System.Runtime.InteropServices.RegistrationClassContext,System.Runtime.InteropServices.RegistrationConnectionType)" /> method overload.</param>
		// Token: 0x06006262 RID: 25186 RVA: 0x0015137E File Offset: 0x0014F57E
		[SecurityCritical]
		[ComVisible(false)]
		public virtual void UnregisterTypeForComClients(int cookie)
		{
			RegistrationServices.CoRevokeClassObject(cookie);
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x00151388 File Offset: 0x0014F588
		[SecurityCritical]
		internal static bool TypeRequiresRegistrationHelper(Type type)
		{
			return (type.IsClass || type.IsValueType) && !type.IsAbstract && (type.IsValueType || !(type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[0], null) == null)) && Marshal.IsTypeVisibleFromCom(type);
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x001513DC File Offset: 0x0014F5DC
		[SecurityCritical]
		private void RegisterValueType(Type type, string strAsmName, string strAsmVersion, string strAsmCodeBase, string strRuntimeVersion)
		{
			string text = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
			using (RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey("Record"))
			{
				using (RegistryKey registryKey2 = registryKey.CreateSubKey(text))
				{
					using (RegistryKey registryKey3 = registryKey2.CreateSubKey(strAsmVersion))
					{
						registryKey3.SetValue("Class", type.FullName);
						registryKey3.SetValue("Assembly", strAsmName);
						registryKey3.SetValue("RuntimeVersion", strRuntimeVersion);
						if (strAsmCodeBase != null)
						{
							registryKey3.SetValue("CodeBase", strAsmCodeBase);
						}
					}
				}
			}
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x001514C4 File Offset: 0x0014F6C4
		[SecurityCritical]
		private void RegisterManagedType(Type type, string strAsmName, string strAsmVersion, string strAsmCodeBase, string strRuntimeVersion)
		{
			string text = type.FullName ?? "";
			string text2 = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
			string progIdForType = this.GetProgIdForType(type);
			if (progIdForType != string.Empty)
			{
				using (RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey(progIdForType))
				{
					registryKey.SetValue("", text);
					using (RegistryKey registryKey2 = registryKey.CreateSubKey("CLSID"))
					{
						registryKey2.SetValue("", text2);
					}
				}
			}
			using (RegistryKey registryKey3 = Registry.ClassesRoot.CreateSubKey("CLSID"))
			{
				using (RegistryKey registryKey4 = registryKey3.CreateSubKey(text2))
				{
					registryKey4.SetValue("", text);
					using (RegistryKey registryKey5 = registryKey4.CreateSubKey("InprocServer32"))
					{
						registryKey5.SetValue("", "mscoree.dll");
						registryKey5.SetValue("ThreadingModel", "Both");
						registryKey5.SetValue("Class", type.FullName);
						registryKey5.SetValue("Assembly", strAsmName);
						registryKey5.SetValue("RuntimeVersion", strRuntimeVersion);
						if (strAsmCodeBase != null)
						{
							registryKey5.SetValue("CodeBase", strAsmCodeBase);
						}
						using (RegistryKey registryKey6 = registryKey5.CreateSubKey(strAsmVersion))
						{
							registryKey6.SetValue("Class", type.FullName);
							registryKey6.SetValue("Assembly", strAsmName);
							registryKey6.SetValue("RuntimeVersion", strRuntimeVersion);
							if (strAsmCodeBase != null)
							{
								registryKey6.SetValue("CodeBase", strAsmCodeBase);
							}
						}
						if (progIdForType != string.Empty)
						{
							using (RegistryKey registryKey7 = registryKey4.CreateSubKey("ProgId"))
							{
								registryKey7.SetValue("", progIdForType);
							}
						}
					}
					using (RegistryKey registryKey8 = registryKey4.CreateSubKey("Implemented Categories"))
					{
						using (registryKey8.CreateSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}"))
						{
						}
					}
				}
			}
			this.EnsureManagedCategoryExists();
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x001517D8 File Offset: 0x0014F9D8
		[SecurityCritical]
		private void RegisterComImportedType(Type type, string strAsmName, string strAsmVersion, string strAsmCodeBase, string strRuntimeVersion)
		{
			string text = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
			using (RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey("CLSID"))
			{
				using (RegistryKey registryKey2 = registryKey.CreateSubKey(text))
				{
					using (RegistryKey registryKey3 = registryKey2.CreateSubKey("InprocServer32"))
					{
						registryKey3.SetValue("Class", type.FullName);
						registryKey3.SetValue("Assembly", strAsmName);
						registryKey3.SetValue("RuntimeVersion", strRuntimeVersion);
						if (strAsmCodeBase != null)
						{
							registryKey3.SetValue("CodeBase", strAsmCodeBase);
						}
						using (RegistryKey registryKey4 = registryKey3.CreateSubKey(strAsmVersion))
						{
							registryKey4.SetValue("Class", type.FullName);
							registryKey4.SetValue("Assembly", strAsmName);
							registryKey4.SetValue("RuntimeVersion", strRuntimeVersion);
							if (strAsmCodeBase != null)
							{
								registryKey4.SetValue("CodeBase", strAsmCodeBase);
							}
						}
					}
				}
			}
		}

		// Token: 0x06006267 RID: 25191 RVA: 0x00151924 File Offset: 0x0014FB24
		[SecurityCritical]
		private bool UnregisterValueType(Type type, string strAsmVersion)
		{
			bool flag = true;
			string text = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
			using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("Record", true))
			{
				if (registryKey != null)
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(text, true))
					{
						if (registryKey2 != null)
						{
							using (RegistryKey registryKey3 = registryKey2.OpenSubKey(strAsmVersion, true))
							{
								if (registryKey3 != null)
								{
									registryKey3.DeleteValue("Assembly", false);
									registryKey3.DeleteValue("Class", false);
									registryKey3.DeleteValue("CodeBase", false);
									registryKey3.DeleteValue("RuntimeVersion", false);
									if (registryKey3.SubKeyCount == 0 && registryKey3.ValueCount == 0)
									{
										registryKey2.DeleteSubKey(strAsmVersion);
									}
								}
							}
							if (registryKey2.SubKeyCount != 0)
							{
								flag = false;
							}
							if (registryKey2.SubKeyCount == 0 && registryKey2.ValueCount == 0)
							{
								registryKey.DeleteSubKey(text);
							}
						}
					}
					if (registryKey.SubKeyCount == 0 && registryKey.ValueCount == 0)
					{
						Registry.ClassesRoot.DeleteSubKey("Record");
					}
				}
			}
			return flag;
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x00151A7C File Offset: 0x0014FC7C
		[SecurityCritical]
		private bool UnregisterManagedType(Type type, string strAsmVersion)
		{
			bool flag = true;
			string text = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
			string progIdForType = this.GetProgIdForType(type);
			using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("CLSID", true))
			{
				if (registryKey != null)
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(text, true))
					{
						if (registryKey2 != null)
						{
							using (RegistryKey registryKey3 = registryKey2.OpenSubKey("InprocServer32", true))
							{
								if (registryKey3 != null)
								{
									using (RegistryKey registryKey4 = registryKey3.OpenSubKey(strAsmVersion, true))
									{
										if (registryKey4 != null)
										{
											registryKey4.DeleteValue("Assembly", false);
											registryKey4.DeleteValue("Class", false);
											registryKey4.DeleteValue("RuntimeVersion", false);
											registryKey4.DeleteValue("CodeBase", false);
											if (registryKey4.SubKeyCount == 0 && registryKey4.ValueCount == 0)
											{
												registryKey3.DeleteSubKey(strAsmVersion);
											}
										}
									}
									if (registryKey3.SubKeyCount != 0)
									{
										flag = false;
									}
									if (flag)
									{
										registryKey3.DeleteValue("", false);
										registryKey3.DeleteValue("ThreadingModel", false);
									}
									registryKey3.DeleteValue("Assembly", false);
									registryKey3.DeleteValue("Class", false);
									registryKey3.DeleteValue("RuntimeVersion", false);
									registryKey3.DeleteValue("CodeBase", false);
									if (registryKey3.SubKeyCount == 0 && registryKey3.ValueCount == 0)
									{
										registryKey2.DeleteSubKey("InprocServer32");
									}
								}
							}
							if (flag)
							{
								registryKey2.DeleteValue("", false);
								if (progIdForType != string.Empty)
								{
									using (RegistryKey registryKey5 = registryKey2.OpenSubKey("ProgId", true))
									{
										if (registryKey5 != null)
										{
											registryKey5.DeleteValue("", false);
											if (registryKey5.SubKeyCount == 0 && registryKey5.ValueCount == 0)
											{
												registryKey2.DeleteSubKey("ProgId");
											}
										}
									}
								}
								using (RegistryKey registryKey6 = registryKey2.OpenSubKey("Implemented Categories", true))
								{
									if (registryKey6 != null)
									{
										using (RegistryKey registryKey7 = registryKey6.OpenSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}", true))
										{
											if (registryKey7 != null && registryKey7.SubKeyCount == 0 && registryKey7.ValueCount == 0)
											{
												registryKey6.DeleteSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}");
											}
										}
										if (registryKey6.SubKeyCount == 0 && registryKey6.ValueCount == 0)
										{
											registryKey2.DeleteSubKey("Implemented Categories");
										}
									}
								}
							}
							if (registryKey2.SubKeyCount == 0 && registryKey2.ValueCount == 0)
							{
								registryKey.DeleteSubKey(text);
							}
						}
					}
					if (registryKey.SubKeyCount == 0 && registryKey.ValueCount == 0)
					{
						Registry.ClassesRoot.DeleteSubKey("CLSID");
					}
				}
				if (flag && progIdForType != string.Empty)
				{
					using (RegistryKey registryKey8 = Registry.ClassesRoot.OpenSubKey(progIdForType, true))
					{
						if (registryKey8 != null)
						{
							registryKey8.DeleteValue("", false);
							using (RegistryKey registryKey9 = registryKey8.OpenSubKey("CLSID", true))
							{
								if (registryKey9 != null)
								{
									registryKey9.DeleteValue("", false);
									if (registryKey9.SubKeyCount == 0 && registryKey9.ValueCount == 0)
									{
										registryKey8.DeleteSubKey("CLSID");
									}
								}
							}
							if (registryKey8.SubKeyCount == 0 && registryKey8.ValueCount == 0)
							{
								Registry.ClassesRoot.DeleteSubKey(progIdForType);
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x00151EC0 File Offset: 0x001500C0
		[SecurityCritical]
		private bool UnregisterComImportedType(Type type, string strAsmVersion)
		{
			bool flag = true;
			string text = "{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
			using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("CLSID", true))
			{
				if (registryKey != null)
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(text, true))
					{
						if (registryKey2 != null)
						{
							using (RegistryKey registryKey3 = registryKey2.OpenSubKey("InprocServer32", true))
							{
								if (registryKey3 != null)
								{
									registryKey3.DeleteValue("Assembly", false);
									registryKey3.DeleteValue("Class", false);
									registryKey3.DeleteValue("RuntimeVersion", false);
									registryKey3.DeleteValue("CodeBase", false);
									using (RegistryKey registryKey4 = registryKey3.OpenSubKey(strAsmVersion, true))
									{
										if (registryKey4 != null)
										{
											registryKey4.DeleteValue("Assembly", false);
											registryKey4.DeleteValue("Class", false);
											registryKey4.DeleteValue("RuntimeVersion", false);
											registryKey4.DeleteValue("CodeBase", false);
											if (registryKey4.SubKeyCount == 0 && registryKey4.ValueCount == 0)
											{
												registryKey3.DeleteSubKey(strAsmVersion);
											}
										}
									}
									if (registryKey3.SubKeyCount != 0)
									{
										flag = false;
									}
									if (registryKey3.SubKeyCount == 0 && registryKey3.ValueCount == 0)
									{
										registryKey2.DeleteSubKey("InprocServer32");
									}
								}
							}
							if (registryKey2.SubKeyCount == 0 && registryKey2.ValueCount == 0)
							{
								registryKey.DeleteSubKey(text);
							}
						}
					}
					if (registryKey.SubKeyCount == 0 && registryKey.ValueCount == 0)
					{
						Registry.ClassesRoot.DeleteSubKey("CLSID");
					}
				}
			}
			return flag;
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x001520C8 File Offset: 0x001502C8
		[SecurityCritical]
		private void RegisterPrimaryInteropAssembly(RuntimeAssembly assembly, string strAsmCodeBase, PrimaryInteropAssemblyAttribute attr)
		{
			if (assembly.GetPublicKey().Length == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_PIAMustBeStrongNamed"));
			}
			string text = "{" + Marshal.GetTypeLibGuidForAssembly(assembly).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
			string text2 = attr.MajorVersion.ToString("x", CultureInfo.InvariantCulture) + "." + attr.MinorVersion.ToString("x", CultureInfo.InvariantCulture);
			using (RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey("TypeLib"))
			{
				using (RegistryKey registryKey2 = registryKey.CreateSubKey(text))
				{
					using (RegistryKey registryKey3 = registryKey2.CreateSubKey(text2))
					{
						registryKey3.SetValue("PrimaryInteropAssemblyName", assembly.FullName);
						if (strAsmCodeBase != null)
						{
							registryKey3.SetValue("PrimaryInteropAssemblyCodeBase", strAsmCodeBase);
						}
					}
				}
			}
		}

		// Token: 0x0600626B RID: 25195 RVA: 0x001521EC File Offset: 0x001503EC
		[SecurityCritical]
		private void UnregisterPrimaryInteropAssembly(Assembly assembly, PrimaryInteropAssemblyAttribute attr)
		{
			string text = "{" + Marshal.GetTypeLibGuidForAssembly(assembly).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
			string text2 = attr.MajorVersion.ToString("x", CultureInfo.InvariantCulture) + "." + attr.MinorVersion.ToString("x", CultureInfo.InvariantCulture);
			using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("TypeLib", true))
			{
				if (registryKey != null)
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(text, true))
					{
						if (registryKey2 != null)
						{
							using (RegistryKey registryKey3 = registryKey2.OpenSubKey(text2, true))
							{
								if (registryKey3 != null)
								{
									registryKey3.DeleteValue("PrimaryInteropAssemblyName", false);
									registryKey3.DeleteValue("PrimaryInteropAssemblyCodeBase", false);
									if (registryKey3.SubKeyCount == 0 && registryKey3.ValueCount == 0)
									{
										registryKey2.DeleteSubKey(text2);
									}
								}
							}
							if (registryKey2.SubKeyCount == 0 && registryKey2.ValueCount == 0)
							{
								registryKey.DeleteSubKey(text);
							}
						}
					}
					if (registryKey.SubKeyCount == 0 && registryKey.ValueCount == 0)
					{
						Registry.ClassesRoot.DeleteSubKey("TypeLib");
					}
				}
			}
		}

		// Token: 0x0600626C RID: 25196 RVA: 0x0015235C File Offset: 0x0015055C
		private void EnsureManagedCategoryExists()
		{
			if (!RegistrationServices.ManagedCategoryExists())
			{
				using (RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey("Component Categories"))
				{
					using (RegistryKey registryKey2 = registryKey.CreateSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}"))
					{
						registryKey2.SetValue("0", ".NET Category");
					}
				}
			}
		}

		// Token: 0x0600626D RID: 25197 RVA: 0x001523D0 File Offset: 0x001505D0
		private static bool ManagedCategoryExists()
		{
			using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("Component Categories", RegistryKeyPermissionCheck.ReadSubTree))
			{
				if (registryKey == null)
				{
					return false;
				}
				using (RegistryKey registryKey2 = registryKey.OpenSubKey("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}", RegistryKeyPermissionCheck.ReadSubTree))
				{
					if (registryKey2 == null)
					{
						return false;
					}
					object value = registryKey2.GetValue("0");
					if (value == null || value.GetType() != typeof(string))
					{
						return false;
					}
					string text = (string)value;
					if (text != ".NET Category")
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600626E RID: 25198 RVA: 0x00152484 File Offset: 0x00150684
		[SecurityCritical]
		private void CallUserDefinedRegistrationMethod(Type type, bool bRegister)
		{
			bool flag = false;
			Type type2;
			if (bRegister)
			{
				type2 = typeof(ComRegisterFunctionAttribute);
			}
			else
			{
				type2 = typeof(ComUnregisterFunctionAttribute);
			}
			Type type3 = type;
			while (!flag && type3 != null)
			{
				MethodInfo[] methods = type3.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				int num = methods.Length;
				for (int i = 0; i < num; i++)
				{
					MethodInfo methodInfo = methods[i];
					if (methodInfo.GetCustomAttributes(type2, true).Length != 0)
					{
						if (!methodInfo.IsStatic)
						{
							if (bRegister)
							{
								throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NonStaticComRegFunction", new object[] { methodInfo.Name, type3.Name }));
							}
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NonStaticComUnRegFunction", new object[] { methodInfo.Name, type3.Name }));
						}
						else
						{
							ParameterInfo[] parameters = methodInfo.GetParameters();
							if (methodInfo.ReturnType != typeof(void) || parameters == null || parameters.Length != 1 || (parameters[0].ParameterType != typeof(string) && parameters[0].ParameterType != typeof(Type)))
							{
								if (bRegister)
								{
									throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InvalidComRegFunctionSig", new object[] { methodInfo.Name, type3.Name }));
								}
								throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InvalidComUnRegFunctionSig", new object[] { methodInfo.Name, type3.Name }));
							}
							else if (flag)
							{
								if (bRegister)
								{
									throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MultipleComRegFunctions", new object[] { type3.Name }));
								}
								throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MultipleComUnRegFunctions", new object[] { type3.Name }));
							}
							else
							{
								object[] array = new object[1];
								if (parameters[0].ParameterType == typeof(string))
								{
									array[0] = "HKEY_CLASSES_ROOT\\CLSID\\{" + Marshal.GenerateGuidForType(type).ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
								}
								else
								{
									array[0] = type;
								}
								methodInfo.Invoke(null, array);
								flag = true;
							}
						}
					}
				}
				type3 = type3.BaseType;
			}
		}

		// Token: 0x0600626F RID: 25199 RVA: 0x001526C6 File Offset: 0x001508C6
		private Type GetBaseComImportType(Type type)
		{
			while (type != null && !type.IsImport)
			{
				type = type.BaseType;
			}
			return type;
		}

		// Token: 0x06006270 RID: 25200 RVA: 0x001526E4 File Offset: 0x001508E4
		private bool IsRegisteredAsValueType(Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06006271 RID: 25201
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterTypeForComClientsNative(Type type, ref Guid g);

		// Token: 0x06006272 RID: 25202
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RegisterTypeForComClientsExNative(Type t, RegistrationClassContext clsContext, RegistrationConnectionType flags);

		// Token: 0x06006273 RID: 25203
		[DllImport("ole32.dll", CharSet = CharSet.Auto, PreserveSig = false)]
		private static extern void CoRevokeClassObject(int cookie);

		// Token: 0x04002BDB RID: 11227
		private const string strManagedCategoryGuid = "{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}";

		// Token: 0x04002BDC RID: 11228
		private const string strDocStringPrefix = "";

		// Token: 0x04002BDD RID: 11229
		private const string strManagedTypeThreadingModel = "Both";

		// Token: 0x04002BDE RID: 11230
		private const string strComponentCategorySubKey = "Component Categories";

		// Token: 0x04002BDF RID: 11231
		private const string strManagedCategoryDescription = ".NET Category";

		// Token: 0x04002BE0 RID: 11232
		private const string strImplementedCategoriesSubKey = "Implemented Categories";

		// Token: 0x04002BE1 RID: 11233
		private const string strMsCorEEFileName = "mscoree.dll";

		// Token: 0x04002BE2 RID: 11234
		private const string strRecordRootName = "Record";

		// Token: 0x04002BE3 RID: 11235
		private const string strClsIdRootName = "CLSID";

		// Token: 0x04002BE4 RID: 11236
		private const string strTlbRootName = "TypeLib";

		// Token: 0x04002BE5 RID: 11237
		private static Guid s_ManagedCategoryGuid = new Guid("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}");
	}
}
