using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000063 RID: 99
	internal static class IsolationInterop
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007F64 File Offset: 0x00006164
		public static Store UserStore
		{
			get
			{
				if (IsolationInterop._userStore == null)
				{
					object synchObject = IsolationInterop._synchObject;
					lock (synchObject)
					{
						if (IsolationInterop._userStore == null)
						{
							IsolationInterop._userStore = new Store(IsolationInterop.GetUserStore(0U, IntPtr.Zero, ref IsolationInterop.IID_IStore) as IStore);
						}
					}
				}
				return IsolationInterop._userStore;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007FD0 File Offset: 0x000061D0
		[SecuritySafeCritical]
		public static Store GetUserStore()
		{
			return new Store(IsolationInterop.GetUserStore(0U, IntPtr.Zero, ref IsolationInterop.IID_IStore) as IStore);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00007FEC File Offset: 0x000061EC
		public static Store SystemStore
		{
			get
			{
				if (IsolationInterop._systemStore == null)
				{
					object synchObject = IsolationInterop._synchObject;
					lock (synchObject)
					{
						if (IsolationInterop._systemStore == null)
						{
							IsolationInterop._systemStore = new Store(IsolationInterop.GetSystemStore(0U, ref IsolationInterop.IID_IStore) as IStore);
						}
					}
				}
				return IsolationInterop._systemStore;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00008054 File Offset: 0x00006254
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000080AC File Offset: 0x000062AC
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

		// Token: 0x060001DE RID: 478 RVA: 0x00008104 File Offset: 0x00006304
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

		// Token: 0x060001DF RID: 479 RVA: 0x00008250 File Offset: 0x00006450
		internal static IActContext CreateActContext(IReferenceAppId AppId)
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
			createActContextParametersSource.SourceType = 2U;
			createActContextParametersSource.Data = IntPtr.Zero;
			IsolationInterop.CreateActContextParametersSourceReferenceAppid createActContextParametersSourceReferenceAppid;
			createActContextParametersSourceReferenceAppid.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParametersSourceReferenceAppid));
			createActContextParametersSourceReferenceAppid.Flags = 0U;
			createActContextParametersSourceReferenceAppid.AppId = AppId;
			IActContext actContext;
			try
			{
				createActContextParametersSource.Data = createActContextParametersSourceReferenceAppid.ToIntPtr();
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

		// Token: 0x060001E0 RID: 480
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object CreateActContext(ref IsolationInterop.CreateActContextParameters Params);

		// Token: 0x060001E1 RID: 481
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object CreateCMSFromXml([In] byte[] buffer, [In] uint bufferSize, [In] IManifestParseErrorCallback Callback, [In] ref Guid riid);

		// Token: 0x060001E2 RID: 482
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object ParseManifest([MarshalAs(UnmanagedType.LPWStr)] [In] string pszManifestPath, [In] IManifestParseErrorCallback pIManifestParseErrorCallback, [In] ref Guid riid);

		// Token: 0x060001E3 RID: 483
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		private static extern object GetUserStore([In] uint Flags, [In] IntPtr hToken, [In] ref Guid riid);

		// Token: 0x060001E4 RID: 484
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		private static extern object GetSystemStore([In] uint Flags, [In] ref Guid riid);

		// Token: 0x060001E5 RID: 485
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern IIdentityAuthority GetIdentityAuthority();

		// Token: 0x060001E6 RID: 486
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern IAppIdAuthority GetAppIdAuthority();

		// Token: 0x060001E7 RID: 487
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object GetUserStateManager([In] uint Flags, [In] IntPtr hToken, [In] ref Guid riid);

		// Token: 0x060001E8 RID: 488 RVA: 0x0000839C File Offset: 0x0000659C
		internal static Guid GetGuidOfType(Type type)
		{
			GuidAttribute guidAttribute = (GuidAttribute)Attribute.GetCustomAttribute(type, typeof(GuidAttribute), false);
			return new Guid(guidAttribute.Value);
		}

		// Token: 0x0400019B RID: 411
		private static object _synchObject = new object();

		// Token: 0x0400019C RID: 412
		private static Store _userStore = null;

		// Token: 0x0400019D RID: 413
		private static Store _systemStore = null;

		// Token: 0x0400019E RID: 414
		private static IIdentityAuthority _idAuth = null;

		// Token: 0x0400019F RID: 415
		private static IAppIdAuthority _appIdAuth = null;

		// Token: 0x040001A0 RID: 416
		public const string IsolationDllName = "clr.dll";

		// Token: 0x040001A1 RID: 417
		public static Guid IID_ICMS = IsolationInterop.GetGuidOfType(typeof(ICMS));

		// Token: 0x040001A2 RID: 418
		public static Guid IID_IDefinitionIdentity = IsolationInterop.GetGuidOfType(typeof(IDefinitionIdentity));

		// Token: 0x040001A3 RID: 419
		public static Guid IID_IManifestInformation = IsolationInterop.GetGuidOfType(typeof(IManifestInformation));

		// Token: 0x040001A4 RID: 420
		public static Guid IID_IEnumSTORE_ASSEMBLY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY));

		// Token: 0x040001A5 RID: 421
		public static Guid IID_IEnumSTORE_ASSEMBLY_FILE = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));

		// Token: 0x040001A6 RID: 422
		public static Guid IID_IEnumSTORE_CATEGORY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY));

		// Token: 0x040001A7 RID: 423
		public static Guid IID_IEnumSTORE_CATEGORY_INSTANCE = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_INSTANCE));

		// Token: 0x040001A8 RID: 424
		public static Guid IID_IEnumSTORE_DEPLOYMENT_METADATA = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_DEPLOYMENT_METADATA));

		// Token: 0x040001A9 RID: 425
		public static Guid IID_IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY));

		// Token: 0x040001AA RID: 426
		public static Guid IID_IStore = IsolationInterop.GetGuidOfType(typeof(IStore));

		// Token: 0x040001AB RID: 427
		public static Guid GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING = new Guid("2ec93463-b0c3-45e1-8364-327e96aea856");

		// Token: 0x040001AC RID: 428
		public static Guid SXS_INSTALL_REFERENCE_SCHEME_SXS_STRONGNAME_SIGNED_PRIVATE_ASSEMBLY = new Guid("3ab20ac0-67e8-4512-8385-a487e35df3da");

		// Token: 0x0200053B RID: 1339
		internal struct CreateActContextParameters
		{
			// Token: 0x040037E2 RID: 14306
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040037E3 RID: 14307
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040037E4 RID: 14308
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr CustomStoreList;

			// Token: 0x040037E5 RID: 14309
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr CultureFallbackList;

			// Token: 0x040037E6 RID: 14310
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr ProcessorArchitectureList;

			// Token: 0x040037E7 RID: 14311
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr Source;

			// Token: 0x040037E8 RID: 14312
			[MarshalAs(UnmanagedType.U2)]
			public ushort ProcArch;

			// Token: 0x020008A1 RID: 2209
			[Flags]
			public enum CreateFlags
			{
				// Token: 0x040044C8 RID: 17608
				Nothing = 0,
				// Token: 0x040044C9 RID: 17609
				StoreListValid = 1,
				// Token: 0x040044CA RID: 17610
				CultureListValid = 2,
				// Token: 0x040044CB RID: 17611
				ProcessorFallbackListValid = 4,
				// Token: 0x040044CC RID: 17612
				ProcessorValid = 8,
				// Token: 0x040044CD RID: 17613
				SourceValid = 16,
				// Token: 0x040044CE RID: 17614
				IgnoreVisibility = 32
			}
		}

		// Token: 0x0200053C RID: 1340
		internal struct CreateActContextParametersSource
		{
			// Token: 0x0600554B RID: 21835 RVA: 0x00165B6C File Offset: 0x00163D6C
			[SecurityCritical]
			public IntPtr ToIntPtr()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(this));
				Marshal.StructureToPtr(this, intPtr, false);
				return intPtr;
			}

			// Token: 0x0600554C RID: 21836 RVA: 0x00165BA2 File Offset: 0x00163DA2
			[SecurityCritical]
			public static void Destroy(IntPtr p)
			{
				Marshal.DestroyStructure(p, typeof(IsolationInterop.CreateActContextParametersSource));
				Marshal.FreeCoTaskMem(p);
			}

			// Token: 0x040037E9 RID: 14313
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040037EA RID: 14314
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040037EB RID: 14315
			[MarshalAs(UnmanagedType.U4)]
			public uint SourceType;

			// Token: 0x040037EC RID: 14316
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr Data;

			// Token: 0x020008A2 RID: 2210
			[Flags]
			public enum SourceFlags
			{
				// Token: 0x040044D0 RID: 17616
				Definition = 1,
				// Token: 0x040044D1 RID: 17617
				Reference = 2
			}
		}

		// Token: 0x0200053D RID: 1341
		internal struct CreateActContextParametersSourceReferenceAppid
		{
			// Token: 0x0600554D RID: 21837 RVA: 0x00165BBC File Offset: 0x00163DBC
			public IntPtr ToIntPtr()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(this));
				Marshal.StructureToPtr(this, intPtr, false);
				return intPtr;
			}

			// Token: 0x0600554E RID: 21838 RVA: 0x00165BF2 File Offset: 0x00163DF2
			public static void Destroy(IntPtr p)
			{
				Marshal.DestroyStructure(p, typeof(IsolationInterop.CreateActContextParametersSourceReferenceAppid));
				Marshal.FreeCoTaskMem(p);
			}

			// Token: 0x040037ED RID: 14317
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040037EE RID: 14318
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040037EF RID: 14319
			public IReferenceAppId AppId;
		}

		// Token: 0x0200053E RID: 1342
		internal struct CreateActContextParametersSourceDefinitionAppid
		{
			// Token: 0x0600554F RID: 21839 RVA: 0x00165C0C File Offset: 0x00163E0C
			[SecurityCritical]
			public IntPtr ToIntPtr()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(this));
				Marshal.StructureToPtr(this, intPtr, false);
				return intPtr;
			}

			// Token: 0x06005550 RID: 21840 RVA: 0x00165C42 File Offset: 0x00163E42
			[SecurityCritical]
			public static void Destroy(IntPtr p)
			{
				Marshal.DestroyStructure(p, typeof(IsolationInterop.CreateActContextParametersSourceDefinitionAppid));
				Marshal.FreeCoTaskMem(p);
			}

			// Token: 0x040037F0 RID: 14320
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040037F1 RID: 14321
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040037F2 RID: 14322
			public IDefinitionAppId AppId;
		}
	}
}
