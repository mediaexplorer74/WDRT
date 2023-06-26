using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x02000629 RID: 1577
	internal class AssemblyBuilderData
	{
		// Token: 0x06004994 RID: 18836 RVA: 0x0010B6AC File Offset: 0x001098AC
		[SecurityCritical]
		internal AssemblyBuilderData(InternalAssemblyBuilder assembly, string strAssemblyName, AssemblyBuilderAccess access, string dir)
		{
			this.m_assembly = assembly;
			this.m_strAssemblyName = strAssemblyName;
			this.m_access = access;
			this.m_moduleBuilderList = new List<ModuleBuilder>();
			this.m_resWriterList = new List<ResWriterData>();
			if (dir == null && access != AssemblyBuilderAccess.Run)
			{
				this.m_strDir = Environment.CurrentDirectory;
			}
			else
			{
				this.m_strDir = dir;
			}
			this.m_peFileKind = PEFileKinds.Dll;
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x0010B70E File Offset: 0x0010990E
		internal void AddModule(ModuleBuilder dynModule)
		{
			this.m_moduleBuilderList.Add(dynModule);
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x0010B71C File Offset: 0x0010991C
		internal void AddResWriter(ResWriterData resData)
		{
			this.m_resWriterList.Add(resData);
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x0010B72C File Offset: 0x0010992C
		internal void AddCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (this.m_CABuilders == null)
			{
				this.m_CABuilders = new CustomAttributeBuilder[16];
			}
			if (this.m_iCABuilder == this.m_CABuilders.Length)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.m_iCABuilder * 2];
				Array.Copy(this.m_CABuilders, array, this.m_iCABuilder);
				this.m_CABuilders = array;
			}
			this.m_CABuilders[this.m_iCABuilder] = customBuilder;
			this.m_iCABuilder++;
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x0010B7A4 File Offset: 0x001099A4
		internal void AddCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (this.m_CABytes == null)
			{
				this.m_CABytes = new byte[16][];
				this.m_CACons = new ConstructorInfo[16];
			}
			if (this.m_iCAs == this.m_CABytes.Length)
			{
				byte[][] array = new byte[this.m_iCAs * 2][];
				ConstructorInfo[] array2 = new ConstructorInfo[this.m_iCAs * 2];
				for (int i = 0; i < this.m_iCAs; i++)
				{
					array[i] = this.m_CABytes[i];
					array2[i] = this.m_CACons[i];
				}
				this.m_CABytes = array;
				this.m_CACons = array2;
			}
			byte[] array3 = new byte[binaryAttribute.Length];
			Array.Copy(binaryAttribute, array3, binaryAttribute.Length);
			this.m_CABytes[this.m_iCAs] = array3;
			this.m_CACons[this.m_iCAs] = con;
			this.m_iCAs++;
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x0010B874 File Offset: 0x00109A74
		[SecurityCritical]
		internal void FillUnmanagedVersionInfo()
		{
			CultureInfo locale = this.m_assembly.GetLocale();
			if (locale != null)
			{
				this.m_nativeVersion.m_lcid = locale.LCID;
			}
			for (int i = 0; i < this.m_iCABuilder; i++)
			{
				Type declaringType = this.m_CABuilders[i].m_con.DeclaringType;
				if (this.m_CABuilders[i].m_constructorArgs.Length != 0 && this.m_CABuilders[i].m_constructorArgs[0] != null)
				{
					if (declaringType.Equals(typeof(AssemblyCopyrightAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[] { this.m_CABuilders[i].m_con.ReflectedType.Name }));
						}
						if (!this.m_OverrideUnmanagedVersionInfo)
						{
							this.m_nativeVersion.m_strCopyright = this.m_CABuilders[i].m_constructorArgs[0].ToString();
						}
					}
					else if (declaringType.Equals(typeof(AssemblyTrademarkAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[] { this.m_CABuilders[i].m_con.ReflectedType.Name }));
						}
						if (!this.m_OverrideUnmanagedVersionInfo)
						{
							this.m_nativeVersion.m_strTrademark = this.m_CABuilders[i].m_constructorArgs[0].ToString();
						}
					}
					else if (declaringType.Equals(typeof(AssemblyProductAttribute)))
					{
						if (!this.m_OverrideUnmanagedVersionInfo)
						{
							this.m_nativeVersion.m_strProduct = this.m_CABuilders[i].m_constructorArgs[0].ToString();
						}
					}
					else if (declaringType.Equals(typeof(AssemblyCompanyAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[] { this.m_CABuilders[i].m_con.ReflectedType.Name }));
						}
						if (!this.m_OverrideUnmanagedVersionInfo)
						{
							this.m_nativeVersion.m_strCompany = this.m_CABuilders[i].m_constructorArgs[0].ToString();
						}
					}
					else if (declaringType.Equals(typeof(AssemblyDescriptionAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[] { this.m_CABuilders[i].m_con.ReflectedType.Name }));
						}
						this.m_nativeVersion.m_strDescription = this.m_CABuilders[i].m_constructorArgs[0].ToString();
					}
					else if (declaringType.Equals(typeof(AssemblyTitleAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[] { this.m_CABuilders[i].m_con.ReflectedType.Name }));
						}
						this.m_nativeVersion.m_strTitle = this.m_CABuilders[i].m_constructorArgs[0].ToString();
					}
					else if (declaringType.Equals(typeof(AssemblyInformationalVersionAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[] { this.m_CABuilders[i].m_con.ReflectedType.Name }));
						}
						if (!this.m_OverrideUnmanagedVersionInfo)
						{
							this.m_nativeVersion.m_strProductVersion = this.m_CABuilders[i].m_constructorArgs[0].ToString();
						}
					}
					else if (declaringType.Equals(typeof(AssemblyCultureAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[] { this.m_CABuilders[i].m_con.ReflectedType.Name }));
						}
						CultureInfo cultureInfo = new CultureInfo(this.m_CABuilders[i].m_constructorArgs[0].ToString());
						this.m_nativeVersion.m_lcid = cultureInfo.LCID;
					}
					else if (declaringType.Equals(typeof(AssemblyFileVersionAttribute)))
					{
						if (this.m_CABuilders[i].m_constructorArgs.Length != 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadCAForUnmngRSC", new object[] { this.m_CABuilders[i].m_con.ReflectedType.Name }));
						}
						if (!this.m_OverrideUnmanagedVersionInfo)
						{
							this.m_nativeVersion.m_strFileVersion = this.m_CABuilders[i].m_constructorArgs[0].ToString();
						}
					}
				}
			}
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x0010BD2C File Offset: 0x00109F2C
		internal void CheckResNameConflict(string strNewResName)
		{
			int count = this.m_resWriterList.Count;
			for (int i = 0; i < count; i++)
			{
				ResWriterData resWriterData = this.m_resWriterList[i];
				if (resWriterData.m_strName.Equals(strNewResName))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DuplicateResourceName"));
				}
			}
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x0010BD7C File Offset: 0x00109F7C
		internal void CheckNameConflict(string strNewModuleName)
		{
			int count = this.m_moduleBuilderList.Count;
			for (int i = 0; i < count; i++)
			{
				ModuleBuilder moduleBuilder = this.m_moduleBuilderList[i];
				if (moduleBuilder.m_moduleData.m_strModuleName.Equals(strNewModuleName))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DuplicateModuleName"));
				}
			}
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x0010BDD4 File Offset: 0x00109FD4
		internal void CheckTypeNameConflict(string strTypeName, TypeBuilder enclosingType)
		{
			for (int i = 0; i < this.m_moduleBuilderList.Count; i++)
			{
				ModuleBuilder moduleBuilder = this.m_moduleBuilderList[i];
				moduleBuilder.CheckTypeNameConflict(strTypeName, enclosingType);
			}
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x0010BE0C File Offset: 0x0010A00C
		internal void CheckFileNameConflict(string strFileName)
		{
			int num = this.m_moduleBuilderList.Count;
			for (int i = 0; i < num; i++)
			{
				ModuleBuilder moduleBuilder = this.m_moduleBuilderList[i];
				if (moduleBuilder.m_moduleData.m_strFileName != null && string.Compare(moduleBuilder.m_moduleData.m_strFileName, strFileName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DuplicatedFileName"));
				}
			}
			num = this.m_resWriterList.Count;
			for (int i = 0; i < num; i++)
			{
				ResWriterData resWriterData = this.m_resWriterList[i];
				if (resWriterData.m_strFileName != null && string.Compare(resWriterData.m_strFileName, strFileName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DuplicatedFileName"));
				}
			}
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x0010BEBC File Offset: 0x0010A0BC
		internal ModuleBuilder FindModuleWithFileName(string strFileName)
		{
			int count = this.m_moduleBuilderList.Count;
			for (int i = 0; i < count; i++)
			{
				ModuleBuilder moduleBuilder = this.m_moduleBuilderList[i];
				if (moduleBuilder.m_moduleData.m_strFileName != null && string.Compare(moduleBuilder.m_moduleData.m_strFileName, strFileName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return moduleBuilder;
				}
			}
			return null;
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x0010BF14 File Offset: 0x0010A114
		internal ModuleBuilder FindModuleWithName(string strName)
		{
			int count = this.m_moduleBuilderList.Count;
			for (int i = 0; i < count; i++)
			{
				ModuleBuilder moduleBuilder = this.m_moduleBuilderList[i];
				if (moduleBuilder.m_moduleData.m_strModuleName != null && string.Compare(moduleBuilder.m_moduleData.m_strModuleName, strName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return moduleBuilder;
				}
			}
			return null;
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x0010BF6A File Offset: 0x0010A16A
		internal void AddPublicComType(Type type)
		{
			if (this.m_isSaved)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotAlterAssembly"));
			}
			this.EnsurePublicComTypeCapacity();
			this.m_publicComTypeList[this.m_iPublicComTypeCount] = type;
			this.m_iPublicComTypeCount++;
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x0010BFA6 File Offset: 0x0010A1A6
		internal void AddPermissionRequests(PermissionSet required, PermissionSet optional, PermissionSet refused)
		{
			if (this.m_isSaved)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotAlterAssembly"));
			}
			this.m_RequiredPset = required;
			this.m_OptionalPset = optional;
			this.m_RefusedPset = refused;
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x0010BFD8 File Offset: 0x0010A1D8
		private void EnsurePublicComTypeCapacity()
		{
			if (this.m_publicComTypeList == null)
			{
				this.m_publicComTypeList = new Type[16];
			}
			if (this.m_iPublicComTypeCount == this.m_publicComTypeList.Length)
			{
				Type[] array = new Type[this.m_iPublicComTypeCount * 2];
				Array.Copy(this.m_publicComTypeList, array, this.m_iPublicComTypeCount);
				this.m_publicComTypeList = array;
			}
		}

		// Token: 0x04001E58 RID: 7768
		internal List<ModuleBuilder> m_moduleBuilderList;

		// Token: 0x04001E59 RID: 7769
		internal List<ResWriterData> m_resWriterList;

		// Token: 0x04001E5A RID: 7770
		internal string m_strAssemblyName;

		// Token: 0x04001E5B RID: 7771
		internal AssemblyBuilderAccess m_access;

		// Token: 0x04001E5C RID: 7772
		private InternalAssemblyBuilder m_assembly;

		// Token: 0x04001E5D RID: 7773
		internal Type[] m_publicComTypeList;

		// Token: 0x04001E5E RID: 7774
		internal int m_iPublicComTypeCount;

		// Token: 0x04001E5F RID: 7775
		internal bool m_isSaved;

		// Token: 0x04001E60 RID: 7776
		internal const int m_iInitialSize = 16;

		// Token: 0x04001E61 RID: 7777
		internal string m_strDir;

		// Token: 0x04001E62 RID: 7778
		internal const int m_tkAssembly = 536870913;

		// Token: 0x04001E63 RID: 7779
		internal PermissionSet m_RequiredPset;

		// Token: 0x04001E64 RID: 7780
		internal PermissionSet m_OptionalPset;

		// Token: 0x04001E65 RID: 7781
		internal PermissionSet m_RefusedPset;

		// Token: 0x04001E66 RID: 7782
		internal CustomAttributeBuilder[] m_CABuilders;

		// Token: 0x04001E67 RID: 7783
		internal int m_iCABuilder;

		// Token: 0x04001E68 RID: 7784
		internal byte[][] m_CABytes;

		// Token: 0x04001E69 RID: 7785
		internal ConstructorInfo[] m_CACons;

		// Token: 0x04001E6A RID: 7786
		internal int m_iCAs;

		// Token: 0x04001E6B RID: 7787
		internal PEFileKinds m_peFileKind;

		// Token: 0x04001E6C RID: 7788
		internal MethodInfo m_entryPointMethod;

		// Token: 0x04001E6D RID: 7789
		internal Assembly m_ISymWrapperAssembly;

		// Token: 0x04001E6E RID: 7790
		internal ModuleBuilder m_entryPointModule;

		// Token: 0x04001E6F RID: 7791
		internal string m_strResourceFileName;

		// Token: 0x04001E70 RID: 7792
		internal byte[] m_resourceBytes;

		// Token: 0x04001E71 RID: 7793
		internal NativeVersionInfo m_nativeVersion;

		// Token: 0x04001E72 RID: 7794
		internal bool m_hasUnmanagedVersionInfo;

		// Token: 0x04001E73 RID: 7795
		internal bool m_OverrideUnmanagedVersionInfo;
	}
}
