using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006B2 RID: 1714
	internal static class IsolationInterop
	{
		// Token: 0x06005034 RID: 20532 RVA: 0x0011EDDE File Offset: 0x0011CFDE
		[SecuritySafeCritical]
		public static Store GetUserStore()
		{
			return new Store(IsolationInterop.GetUserStore(0U, IntPtr.Zero, ref IsolationInterop.IID_IStore) as IStore);
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06005035 RID: 20533 RVA: 0x0011EDFC File Offset: 0x0011CFFC
		public static IIdentityAuthority IdentityAuthority
		{
			[SecuritySafeCritical]
			get
			{
				if (IsolationInterop._idAuth == null)
				{
					object synchObject = IsolationInterop._synchObject;
					lock (synchObject)
					{
						if (IsolationInterop._idAuth == null)
						{
							IsolationInterop._idAuth = IsolationInterop.GetIdentityAuthority();
						}
					}
				}
				return IsolationInterop._idAuth;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06005036 RID: 20534 RVA: 0x0011EE5C File Offset: 0x0011D05C
		public static IAppIdAuthority AppIdAuthority
		{
			[SecuritySafeCritical]
			get
			{
				if (IsolationInterop._appIdAuth == null)
				{
					object synchObject = IsolationInterop._synchObject;
					lock (synchObject)
					{
						if (IsolationInterop._appIdAuth == null)
						{
							IsolationInterop._appIdAuth = IsolationInterop.GetAppIdAuthority();
						}
					}
				}
				return IsolationInterop._appIdAuth;
			}
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x0011EEBC File Offset: 0x0011D0BC
		[SecuritySafeCritical]
		internal static IActContext CreateActContext(IDefinitionAppId AppId)
		{
			IsolationInterop.CreateActContextParameters createActContextParameters;
			createActContextParameters.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParameters));
			createActContextParameters.Flags = 16U;
			createActContextParameters.CustomStoreList = IntPtr.Zero;
			createActContextParameters.CultureFallbackList = IntPtr.Zero;
			createActContextParameters.ProcessorArchitectureList = IntPtr.Zero;
			createActContextParameters.Source = IntPtr.Zero;
			createActContextParameters.ProcArch = 0;
			IsolationInterop.CreateActContextParametersSource createActContextParametersSource;
			createActContextParametersSource.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParametersSource));
			createActContextParametersSource.Flags = 0U;
			createActContextParametersSource.SourceType = 1U;
			createActContextParametersSource.Data = IntPtr.Zero;
			IsolationInterop.CreateActContextParametersSourceDefinitionAppid createActContextParametersSourceDefinitionAppid;
			createActContextParametersSourceDefinitionAppid.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParametersSourceDefinitionAppid));
			createActContextParametersSourceDefinitionAppid.Flags = 0U;
			createActContextParametersSourceDefinitionAppid.AppId = AppId;
			IActContext actContext;
			try
			{
				createActContextParametersSource.Data = createActContextParametersSourceDefinitionAppid.ToIntPtr();
				createActContextParameters.Source = createActContextParametersSource.ToIntPtr();
				actContext = IsolationInterop.CreateActContext(ref createActContextParameters) as IActContext;
			}
			finally
			{
				if (createActContextParametersSource.Data != IntPtr.Zero)
				{
					IsolationInterop.CreateActContextParametersSourceDefinitionAppid.Destroy(createActContextParametersSource.Data);
					createActContextParametersSource.Data = IntPtr.Zero;
				}
				if (createActContextParameters.Source != IntPtr.Zero)
				{
					IsolationInterop.CreateActContextParametersSource.Destroy(createActContextParameters.Source);
					createActContextParameters.Source = IntPtr.Zero;
				}
			}
			return actContext;
		}

		// Token: 0x06005038 RID: 20536
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object CreateActContext(ref IsolationInterop.CreateActContextParameters Params);

		// Token: 0x06005039 RID: 20537
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object CreateCMSFromXml([In] byte[] buffer, [In] uint bufferSize, [In] IManifestParseErrorCallback Callback, [In] ref Guid riid);

		// Token: 0x0600503A RID: 20538
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object ParseManifest([MarshalAs(UnmanagedType.LPWStr)] [In] string pszManifestPath, [In] IManifestParseErrorCallback pIManifestParseErrorCallback, [In] ref Guid riid);

		// Token: 0x0600503B RID: 20539
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		private static extern object GetUserStore([In] uint Flags, [In] IntPtr hToken, [In] ref Guid riid);

		// Token: 0x0600503C RID: 20540
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern IIdentityAuthority GetIdentityAuthority();

		// Token: 0x0600503D RID: 20541
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern IAppIdAuthority GetAppIdAuthority();

		// Token: 0x0600503E RID: 20542 RVA: 0x0011F008 File Offset: 0x0011D208
		internal static Guid GetGuidOfType(Type type)
		{
			GuidAttribute guidAttribute = (GuidAttribute)Attribute.GetCustomAttribute(type, typeof(GuidAttribute), false);
			return new Guid(guidAttribute.Value);
		}

		// Token: 0x0400227B RID: 8827
		private static object _synchObject = new object();

		// Token: 0x0400227C RID: 8828
		private static volatile IIdentityAuthority _idAuth = null;

		// Token: 0x0400227D RID: 8829
		private static volatile IAppIdAuthority _appIdAuth = null;

		// Token: 0x0400227E RID: 8830
		public const string IsolationDllName = "clr.dll";

		// Token: 0x0400227F RID: 8831
		public static Guid IID_ICMS = IsolationInterop.GetGuidOfType(typeof(ICMS));

		// Token: 0x04002280 RID: 8832
		public static Guid IID_IDefinitionIdentity = IsolationInterop.GetGuidOfType(typeof(IDefinitionIdentity));

		// Token: 0x04002281 RID: 8833
		public static Guid IID_IManifestInformation = IsolationInterop.GetGuidOfType(typeof(IManifestInformation));

		// Token: 0x04002282 RID: 8834
		public static Guid IID_IEnumSTORE_ASSEMBLY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY));

		// Token: 0x04002283 RID: 8835
		public static Guid IID_IEnumSTORE_ASSEMBLY_FILE = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));

		// Token: 0x04002284 RID: 8836
		public static Guid IID_IEnumSTORE_CATEGORY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY));

		// Token: 0x04002285 RID: 8837
		public static Guid IID_IEnumSTORE_CATEGORY_INSTANCE = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_INSTANCE));

		// Token: 0x04002286 RID: 8838
		public static Guid IID_IEnumSTORE_DEPLOYMENT_METADATA = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_DEPLOYMENT_METADATA));

		// Token: 0x04002287 RID: 8839
		public static Guid IID_IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY));

		// Token: 0x04002288 RID: 8840
		public static Guid IID_IStore = IsolationInterop.GetGuidOfType(typeof(IStore));

		// Token: 0x04002289 RID: 8841
		public static Guid GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING = new Guid("2ec93463-b0c3-45e1-8364-327e96aea856");

		// Token: 0x0400228A RID: 8842
		public static Guid SXS_INSTALL_REFERENCE_SCHEME_SXS_STRONGNAME_SIGNED_PRIVATE_ASSEMBLY = new Guid("3ab20ac0-67e8-4512-8385-a487e35df3da");

		// Token: 0x02000C5A RID: 3162
		internal struct CreateActContextParameters
		{
			// Token: 0x040037AC RID: 14252
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040037AD RID: 14253
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040037AE RID: 14254
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr CustomStoreList;

			// Token: 0x040037AF RID: 14255
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr CultureFallbackList;

			// Token: 0x040037B0 RID: 14256
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr ProcessorArchitectureList;

			// Token: 0x040037B1 RID: 14257
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr Source;

			// Token: 0x040037B2 RID: 14258
			[MarshalAs(UnmanagedType.U2)]
			public ushort ProcArch;

			// Token: 0x02000D0A RID: 3338
			[Flags]
			public enum CreateFlags
			{
				// Token: 0x0400395C RID: 14684
				Nothing = 0,
				// Token: 0x0400395D RID: 14685
				StoreListValid = 1,
				// Token: 0x0400395E RID: 14686
				CultureListValid = 2,
				// Token: 0x0400395F RID: 14687
				ProcessorFallbackListValid = 4,
				// Token: 0x04003960 RID: 14688
				ProcessorValid = 8,
				// Token: 0x04003961 RID: 14689
				SourceValid = 16,
				// Token: 0x04003962 RID: 14690
				IgnoreVisibility = 32
			}
		}

		// Token: 0x02000C5B RID: 3163
		internal struct CreateActContextParametersSource
		{
			// Token: 0x0600708D RID: 28813 RVA: 0x00184C1C File Offset: 0x00182E1C
			[SecurityCritical]
			public IntPtr ToIntPtr()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf<IsolationInterop.CreateActContextParametersSource>(this));
				Marshal.StructureToPtr<IsolationInterop.CreateActContextParametersSource>(this, intPtr, false);
				return intPtr;
			}

			// Token: 0x0600708E RID: 28814 RVA: 0x00184C48 File Offset: 0x00182E48
			[SecurityCritical]
			public static void Destroy(IntPtr p)
			{
				Marshal.DestroyStructure(p, typeof(IsolationInterop.CreateActContextParametersSource));
				Marshal.FreeCoTaskMem(p);
			}

			// Token: 0x040037B3 RID: 14259
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040037B4 RID: 14260
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040037B5 RID: 14261
			[MarshalAs(UnmanagedType.U4)]
			public uint SourceType;

			// Token: 0x040037B6 RID: 14262
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr Data;

			// Token: 0x02000D0B RID: 3339
			[Flags]
			public enum SourceFlags
			{
				// Token: 0x04003964 RID: 14692
				Definition = 1,
				// Token: 0x04003965 RID: 14693
				Reference = 2
			}
		}

		// Token: 0x02000C5C RID: 3164
		internal struct CreateActContextParametersSourceDefinitionAppid
		{
			// Token: 0x0600708F RID: 28815 RVA: 0x00184C60 File Offset: 0x00182E60
			[SecurityCritical]
			public IntPtr ToIntPtr()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf<IsolationInterop.CreateActContextParametersSourceDefinitionAppid>(this));
				Marshal.StructureToPtr<IsolationInterop.CreateActContextParametersSourceDefinitionAppid>(this, intPtr, false);
				return intPtr;
			}

			// Token: 0x06007090 RID: 28816 RVA: 0x00184C8C File Offset: 0x00182E8C
			[SecurityCritical]
			public static void Destroy(IntPtr p)
			{
				Marshal.DestroyStructure(p, typeof(IsolationInterop.CreateActContextParametersSourceDefinitionAppid));
				Marshal.FreeCoTaskMem(p);
			}

			// Token: 0x040037B7 RID: 14263
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040037B8 RID: 14264
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040037B9 RID: 14265
			public IDefinitionAppId AppId;
		}
	}
}
