using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200005D RID: 93
	internal class Store
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00007632 File Offset: 0x00005832
		public IStore InternalStore
		{
			get
			{
				return this._pStore;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000763A File Offset: 0x0000583A
		public Store(IStore pStore)
		{
			if (pStore == null)
			{
				throw new ArgumentNullException("pStore");
			}
			this._pStore = pStore;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007658 File Offset: 0x00005858
		[SecuritySafeCritical]
		public uint[] Transact(StoreTransactionOperation[] operations)
		{
			if (operations == null || operations.Length == 0)
			{
				throw new ArgumentException("operations");
			}
			uint[] array = new uint[operations.Length];
			int[] array2 = new int[operations.Length];
			this._pStore.Transact(new IntPtr(operations.Length), operations, array, array2);
			return array;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000076A0 File Offset: 0x000058A0
		public void Transact(StoreTransactionOperation[] operations, uint[] rgDispositions, int[] rgResults)
		{
			if (operations == null || operations.Length == 0)
			{
				throw new ArgumentException("operations");
			}
			this._pStore.Transact(new IntPtr(operations.Length), operations, rgDispositions, rgResults);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000076CC File Offset: 0x000058CC
		[SecuritySafeCritical]
		public IDefinitionIdentity BindReferenceToAssemblyIdentity(uint Flags, IReferenceIdentity ReferenceIdentity, uint cDeploymentsToIgnore, IDefinitionIdentity[] DefinitionIdentity_DeploymentsToIgnore)
		{
			Guid iid_IDefinitionIdentity = IsolationInterop.IID_IDefinitionIdentity;
			object obj = this._pStore.BindReferenceToAssembly(Flags, ReferenceIdentity, cDeploymentsToIgnore, DefinitionIdentity_DeploymentsToIgnore, ref iid_IDefinitionIdentity);
			return (IDefinitionIdentity)obj;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000076F8 File Offset: 0x000058F8
		[SecuritySafeCritical]
		public void CalculateDelimiterOfDeploymentsBasedOnQuota(uint dwFlags, uint cDeployments, IDefinitionAppId[] rgpIDefinitionAppId_Deployments, ref StoreApplicationReference InstallerReference, ulong ulonglongQuota, ref uint Delimiter, ref ulong SizeSharedWithExternalDeployment, ref ulong SizeConsumedByInputDeploymentArray)
		{
			IntPtr zero = IntPtr.Zero;
			this._pStore.CalculateDelimiterOfDeploymentsBasedOnQuota(dwFlags, new IntPtr((long)((ulong)cDeployments)), rgpIDefinitionAppId_Deployments, ref InstallerReference, ulonglongQuota, ref zero, ref SizeSharedWithExternalDeployment, ref SizeConsumedByInputDeploymentArray);
			Delimiter = (uint)zero.ToInt64();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007734 File Offset: 0x00005934
		[SecuritySafeCritical]
		public ICMS BindReferenceToAssemblyManifest(uint Flags, IReferenceIdentity ReferenceIdentity, uint cDeploymentsToIgnore, IDefinitionIdentity[] DefinitionIdentity_DeploymentsToIgnore)
		{
			Guid iid_ICMS = IsolationInterop.IID_ICMS;
			object obj = this._pStore.BindReferenceToAssembly(Flags, ReferenceIdentity, cDeploymentsToIgnore, DefinitionIdentity_DeploymentsToIgnore, ref iid_ICMS);
			return (ICMS)obj;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007760 File Offset: 0x00005960
		[SecuritySafeCritical]
		public ICMS GetAssemblyManifest(uint Flags, IDefinitionIdentity DefinitionIdentity)
		{
			Guid iid_ICMS = IsolationInterop.IID_ICMS;
			object assemblyInformation = this._pStore.GetAssemblyInformation(Flags, DefinitionIdentity, ref iid_ICMS);
			return (ICMS)assemblyInformation;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000778C File Offset: 0x0000598C
		[SecuritySafeCritical]
		public IDefinitionIdentity GetAssemblyIdentity(uint Flags, IDefinitionIdentity DefinitionIdentity)
		{
			Guid iid_IDefinitionIdentity = IsolationInterop.IID_IDefinitionIdentity;
			object assemblyInformation = this._pStore.GetAssemblyInformation(Flags, DefinitionIdentity, ref iid_IDefinitionIdentity);
			return (IDefinitionIdentity)assemblyInformation;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000077B5 File Offset: 0x000059B5
		public StoreAssemblyEnumeration EnumAssemblies(Store.EnumAssembliesFlags Flags)
		{
			return this.EnumAssemblies(Flags, null);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000077C0 File Offset: 0x000059C0
		[SecuritySafeCritical]
		public StoreAssemblyEnumeration EnumAssemblies(Store.EnumAssembliesFlags Flags, IReferenceIdentity refToMatch)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY));
			object obj = this._pStore.EnumAssemblies((uint)Flags, refToMatch, ref guidOfType);
			return new StoreAssemblyEnumeration((IEnumSTORE_ASSEMBLY)obj);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000077F8 File Offset: 0x000059F8
		[SecuritySafeCritical]
		public StoreAssemblyFileEnumeration EnumFiles(Store.EnumAssemblyFilesFlags Flags, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));
			object obj = this._pStore.EnumFiles((uint)Flags, Assembly, ref guidOfType);
			return new StoreAssemblyFileEnumeration((IEnumSTORE_ASSEMBLY_FILE)obj);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007830 File Offset: 0x00005A30
		[SecuritySafeCritical]
		public StoreAssemblyFileEnumeration EnumPrivateFiles(Store.EnumApplicationPrivateFiles Flags, IDefinitionAppId Application, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));
			object obj = this._pStore.EnumPrivateFiles((uint)Flags, Application, Assembly, ref guidOfType);
			return new StoreAssemblyFileEnumeration((IEnumSTORE_ASSEMBLY_FILE)obj);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000786C File Offset: 0x00005A6C
		[SecuritySafeCritical]
		public IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE EnumInstallationReferences(Store.EnumAssemblyInstallReferenceFlags Flags, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE));
			object obj = this._pStore.EnumInstallationReferences((uint)Flags, Assembly, ref guidOfType);
			return (IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE)obj;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000078A0 File Offset: 0x00005AA0
		[SecuritySafeCritical]
		public Store.IPathLock LockAssemblyPath(IDefinitionIdentity asm)
		{
			IntPtr intPtr;
			string text = this._pStore.LockAssemblyPath(0U, asm, out intPtr);
			return new Store.AssemblyPathLock(this._pStore, intPtr, text);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000078CC File Offset: 0x00005ACC
		[SecuritySafeCritical]
		public Store.IPathLock LockApplicationPath(IDefinitionAppId app)
		{
			IntPtr intPtr;
			string text = this._pStore.LockApplicationPath(0U, app, out intPtr);
			return new Store.ApplicationPathLock(this._pStore, intPtr, text);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000078F8 File Offset: 0x00005AF8
		[SecuritySafeCritical]
		public ulong QueryChangeID(IDefinitionIdentity asm)
		{
			return this._pStore.QueryChangeID(asm);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007914 File Offset: 0x00005B14
		[SecuritySafeCritical]
		public StoreCategoryEnumeration EnumCategories(Store.EnumCategoriesFlags Flags, IReferenceIdentity CategoryMatch)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY));
			object obj = this._pStore.EnumCategories((uint)Flags, CategoryMatch, ref guidOfType);
			return new StoreCategoryEnumeration((IEnumSTORE_CATEGORY)obj);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000794C File Offset: 0x00005B4C
		public StoreSubcategoryEnumeration EnumSubcategories(Store.EnumSubcategoriesFlags Flags, IDefinitionIdentity CategoryMatch)
		{
			return this.EnumSubcategories(Flags, CategoryMatch, null);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007958 File Offset: 0x00005B58
		[SecuritySafeCritical]
		public StoreSubcategoryEnumeration EnumSubcategories(Store.EnumSubcategoriesFlags Flags, IDefinitionIdentity Category, string SearchPattern)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_SUBCATEGORY));
			object obj = this._pStore.EnumSubcategories((uint)Flags, Category, SearchPattern, ref guidOfType);
			return new StoreSubcategoryEnumeration((IEnumSTORE_CATEGORY_SUBCATEGORY)obj);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007994 File Offset: 0x00005B94
		[SecuritySafeCritical]
		public StoreCategoryInstanceEnumeration EnumCategoryInstances(Store.EnumCategoryInstancesFlags Flags, IDefinitionIdentity Category, string SubCat)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_INSTANCE));
			object obj = this._pStore.EnumCategoryInstances((uint)Flags, Category, SubCat, ref guidOfType);
			return new StoreCategoryInstanceEnumeration((IEnumSTORE_CATEGORY_INSTANCE)obj);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000079D0 File Offset: 0x00005BD0
		[SecurityCritical]
		public byte[] GetDeploymentProperty(Store.GetPackagePropertyFlags Flags, IDefinitionAppId Deployment, StoreApplicationReference Reference, Guid PropertySet, string PropertyName)
		{
			BLOB blob = default(BLOB);
			byte[] array = null;
			try
			{
				this._pStore.GetDeploymentProperty((uint)Flags, Deployment, ref Reference, ref PropertySet, PropertyName, out blob);
				array = new byte[blob.Size];
				Marshal.Copy(blob.BlobData, array, 0, (int)blob.Size);
			}
			finally
			{
				blob.Dispose();
			}
			return array;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007A38 File Offset: 0x00005C38
		[SecuritySafeCritical]
		public StoreDeploymentMetadataEnumeration EnumInstallerDeployments(Guid InstallerId, string InstallerName, string InstallerMetadata, IReferenceAppId DeploymentFilter)
		{
			StoreApplicationReference storeApplicationReference = new StoreApplicationReference(InstallerId, InstallerName, InstallerMetadata);
			object obj = this._pStore.EnumInstallerDeploymentMetadata(0U, ref storeApplicationReference, DeploymentFilter, ref IsolationInterop.IID_IEnumSTORE_DEPLOYMENT_METADATA);
			return new StoreDeploymentMetadataEnumeration((IEnumSTORE_DEPLOYMENT_METADATA)obj);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007A74 File Offset: 0x00005C74
		[SecuritySafeCritical]
		public StoreDeploymentMetadataPropertyEnumeration EnumInstallerDeploymentProperties(Guid InstallerId, string InstallerName, string InstallerMetadata, IDefinitionAppId Deployment)
		{
			StoreApplicationReference storeApplicationReference = new StoreApplicationReference(InstallerId, InstallerName, InstallerMetadata);
			object obj = this._pStore.EnumInstallerDeploymentMetadataProperties(0U, ref storeApplicationReference, Deployment, ref IsolationInterop.IID_IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY);
			return new StoreDeploymentMetadataPropertyEnumeration((IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY)obj);
		}

		// Token: 0x0400018F RID: 399
		private IStore _pStore;

		// Token: 0x02000530 RID: 1328
		[Flags]
		public enum EnumAssembliesFlags
		{
			// Token: 0x040037C6 RID: 14278
			Nothing = 0,
			// Token: 0x040037C7 RID: 14279
			VisibleOnly = 1,
			// Token: 0x040037C8 RID: 14280
			MatchServicing = 2,
			// Token: 0x040037C9 RID: 14281
			ForceLibrarySemantics = 4
		}

		// Token: 0x02000531 RID: 1329
		[Flags]
		public enum EnumAssemblyFilesFlags
		{
			// Token: 0x040037CB RID: 14283
			Nothing = 0,
			// Token: 0x040037CC RID: 14284
			IncludeInstalled = 1,
			// Token: 0x040037CD RID: 14285
			IncludeMissing = 2
		}

		// Token: 0x02000532 RID: 1330
		[Flags]
		public enum EnumApplicationPrivateFiles
		{
			// Token: 0x040037CF RID: 14287
			Nothing = 0,
			// Token: 0x040037D0 RID: 14288
			IncludeInstalled = 1,
			// Token: 0x040037D1 RID: 14289
			IncludeMissing = 2
		}

		// Token: 0x02000533 RID: 1331
		[Flags]
		public enum EnumAssemblyInstallReferenceFlags
		{
			// Token: 0x040037D3 RID: 14291
			Nothing = 0
		}

		// Token: 0x02000534 RID: 1332
		public interface IPathLock : IDisposable
		{
			// Token: 0x17001482 RID: 5250
			// (get) Token: 0x06005540 RID: 21824
			string Path { get; }
		}

		// Token: 0x02000535 RID: 1333
		private class AssemblyPathLock : Store.IPathLock, IDisposable
		{
			// Token: 0x06005541 RID: 21825 RVA: 0x00165A20 File Offset: 0x00163C20
			public AssemblyPathLock(IStore s, IntPtr c, string path)
			{
				this._pSourceStore = s;
				this._pLockCookie = c;
				this._path = path;
			}

			// Token: 0x06005542 RID: 21826 RVA: 0x00165A48 File Offset: 0x00163C48
			[SecuritySafeCritical]
			private void Dispose(bool fDisposing)
			{
				if (fDisposing)
				{
					GC.SuppressFinalize(this);
				}
				if (this._pLockCookie != IntPtr.Zero)
				{
					this._pSourceStore.ReleaseAssemblyPath(this._pLockCookie);
					this._pLockCookie = IntPtr.Zero;
				}
			}

			// Token: 0x06005543 RID: 21827 RVA: 0x00165A84 File Offset: 0x00163C84
			~AssemblyPathLock()
			{
				this.Dispose(false);
			}

			// Token: 0x06005544 RID: 21828 RVA: 0x00165AB4 File Offset: 0x00163CB4
			void IDisposable.Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x17001483 RID: 5251
			// (get) Token: 0x06005545 RID: 21829 RVA: 0x00165ABD File Offset: 0x00163CBD
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x040037D4 RID: 14292
			private IStore _pSourceStore;

			// Token: 0x040037D5 RID: 14293
			private IntPtr _pLockCookie = IntPtr.Zero;

			// Token: 0x040037D6 RID: 14294
			private string _path;
		}

		// Token: 0x02000536 RID: 1334
		private class ApplicationPathLock : Store.IPathLock, IDisposable
		{
			// Token: 0x06005546 RID: 21830 RVA: 0x00165AC5 File Offset: 0x00163CC5
			public ApplicationPathLock(IStore s, IntPtr c, string path)
			{
				this._pSourceStore = s;
				this._pLockCookie = c;
				this._path = path;
			}

			// Token: 0x06005547 RID: 21831 RVA: 0x00165AED File Offset: 0x00163CED
			[SecuritySafeCritical]
			private void Dispose(bool fDisposing)
			{
				if (fDisposing)
				{
					GC.SuppressFinalize(this);
				}
				if (this._pLockCookie != IntPtr.Zero)
				{
					this._pSourceStore.ReleaseApplicationPath(this._pLockCookie);
					this._pLockCookie = IntPtr.Zero;
				}
			}

			// Token: 0x06005548 RID: 21832 RVA: 0x00165B28 File Offset: 0x00163D28
			~ApplicationPathLock()
			{
				this.Dispose(false);
			}

			// Token: 0x06005549 RID: 21833 RVA: 0x00165B58 File Offset: 0x00163D58
			void IDisposable.Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x17001484 RID: 5252
			// (get) Token: 0x0600554A RID: 21834 RVA: 0x00165B61 File Offset: 0x00163D61
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x040037D7 RID: 14295
			private IStore _pSourceStore;

			// Token: 0x040037D8 RID: 14296
			private IntPtr _pLockCookie = IntPtr.Zero;

			// Token: 0x040037D9 RID: 14297
			private string _path;
		}

		// Token: 0x02000537 RID: 1335
		[Flags]
		public enum EnumCategoriesFlags
		{
			// Token: 0x040037DB RID: 14299
			Nothing = 0
		}

		// Token: 0x02000538 RID: 1336
		[Flags]
		public enum EnumSubcategoriesFlags
		{
			// Token: 0x040037DD RID: 14301
			Nothing = 0
		}

		// Token: 0x02000539 RID: 1337
		[Flags]
		public enum EnumCategoryInstancesFlags
		{
			// Token: 0x040037DF RID: 14303
			Nothing = 0
		}

		// Token: 0x0200053A RID: 1338
		[Flags]
		public enum GetPackagePropertyFlags
		{
			// Token: 0x040037E1 RID: 14305
			Nothing = 0
		}
	}
}
