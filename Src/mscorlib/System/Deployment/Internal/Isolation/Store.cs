using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006AC RID: 1708
	internal class Store
	{
		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06004FF9 RID: 20473 RVA: 0x0011E4D8 File Offset: 0x0011C6D8
		public IStore InternalStore
		{
			get
			{
				return this._pStore;
			}
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x0011E4E0 File Offset: 0x0011C6E0
		public Store(IStore pStore)
		{
			if (pStore == null)
			{
				throw new ArgumentNullException("pStore");
			}
			this._pStore = pStore;
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x0011E500 File Offset: 0x0011C700
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

		// Token: 0x06004FFC RID: 20476 RVA: 0x0011E548 File Offset: 0x0011C748
		[SecuritySafeCritical]
		public IDefinitionIdentity BindReferenceToAssemblyIdentity(uint Flags, IReferenceIdentity ReferenceIdentity, uint cDeploymentsToIgnore, IDefinitionIdentity[] DefinitionIdentity_DeploymentsToIgnore)
		{
			Guid iid_IDefinitionIdentity = IsolationInterop.IID_IDefinitionIdentity;
			object obj = this._pStore.BindReferenceToAssembly(Flags, ReferenceIdentity, cDeploymentsToIgnore, DefinitionIdentity_DeploymentsToIgnore, ref iid_IDefinitionIdentity);
			return (IDefinitionIdentity)obj;
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x0011E574 File Offset: 0x0011C774
		[SecuritySafeCritical]
		public void CalculateDelimiterOfDeploymentsBasedOnQuota(uint dwFlags, uint cDeployments, IDefinitionAppId[] rgpIDefinitionAppId_Deployments, ref StoreApplicationReference InstallerReference, ulong ulonglongQuota, ref uint Delimiter, ref ulong SizeSharedWithExternalDeployment, ref ulong SizeConsumedByInputDeploymentArray)
		{
			IntPtr zero = IntPtr.Zero;
			this._pStore.CalculateDelimiterOfDeploymentsBasedOnQuota(dwFlags, new IntPtr((long)((ulong)cDeployments)), rgpIDefinitionAppId_Deployments, ref InstallerReference, ulonglongQuota, ref zero, ref SizeSharedWithExternalDeployment, ref SizeConsumedByInputDeploymentArray);
			Delimiter = (uint)zero.ToInt64();
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x0011E5B0 File Offset: 0x0011C7B0
		[SecuritySafeCritical]
		public ICMS BindReferenceToAssemblyManifest(uint Flags, IReferenceIdentity ReferenceIdentity, uint cDeploymentsToIgnore, IDefinitionIdentity[] DefinitionIdentity_DeploymentsToIgnore)
		{
			Guid iid_ICMS = IsolationInterop.IID_ICMS;
			object obj = this._pStore.BindReferenceToAssembly(Flags, ReferenceIdentity, cDeploymentsToIgnore, DefinitionIdentity_DeploymentsToIgnore, ref iid_ICMS);
			return (ICMS)obj;
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x0011E5DC File Offset: 0x0011C7DC
		[SecuritySafeCritical]
		public ICMS GetAssemblyManifest(uint Flags, IDefinitionIdentity DefinitionIdentity)
		{
			Guid iid_ICMS = IsolationInterop.IID_ICMS;
			object assemblyInformation = this._pStore.GetAssemblyInformation(Flags, DefinitionIdentity, ref iid_ICMS);
			return (ICMS)assemblyInformation;
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x0011E608 File Offset: 0x0011C808
		[SecuritySafeCritical]
		public IDefinitionIdentity GetAssemblyIdentity(uint Flags, IDefinitionIdentity DefinitionIdentity)
		{
			Guid iid_IDefinitionIdentity = IsolationInterop.IID_IDefinitionIdentity;
			object assemblyInformation = this._pStore.GetAssemblyInformation(Flags, DefinitionIdentity, ref iid_IDefinitionIdentity);
			return (IDefinitionIdentity)assemblyInformation;
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x0011E631 File Offset: 0x0011C831
		public StoreAssemblyEnumeration EnumAssemblies(Store.EnumAssembliesFlags Flags)
		{
			return this.EnumAssemblies(Flags, null);
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x0011E63C File Offset: 0x0011C83C
		[SecuritySafeCritical]
		public StoreAssemblyEnumeration EnumAssemblies(Store.EnumAssembliesFlags Flags, IReferenceIdentity refToMatch)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY));
			object obj = this._pStore.EnumAssemblies((uint)Flags, refToMatch, ref guidOfType);
			return new StoreAssemblyEnumeration((IEnumSTORE_ASSEMBLY)obj);
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x0011E674 File Offset: 0x0011C874
		[SecuritySafeCritical]
		public StoreAssemblyFileEnumeration EnumFiles(Store.EnumAssemblyFilesFlags Flags, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));
			object obj = this._pStore.EnumFiles((uint)Flags, Assembly, ref guidOfType);
			return new StoreAssemblyFileEnumeration((IEnumSTORE_ASSEMBLY_FILE)obj);
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0011E6AC File Offset: 0x0011C8AC
		[SecuritySafeCritical]
		public StoreAssemblyFileEnumeration EnumPrivateFiles(Store.EnumApplicationPrivateFiles Flags, IDefinitionAppId Application, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));
			object obj = this._pStore.EnumPrivateFiles((uint)Flags, Application, Assembly, ref guidOfType);
			return new StoreAssemblyFileEnumeration((IEnumSTORE_ASSEMBLY_FILE)obj);
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x0011E6E8 File Offset: 0x0011C8E8
		[SecuritySafeCritical]
		public IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE EnumInstallationReferences(Store.EnumAssemblyInstallReferenceFlags Flags, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE));
			object obj = this._pStore.EnumInstallationReferences((uint)Flags, Assembly, ref guidOfType);
			return (IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE)obj;
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x0011E71C File Offset: 0x0011C91C
		[SecuritySafeCritical]
		public Store.IPathLock LockAssemblyPath(IDefinitionIdentity asm)
		{
			IntPtr intPtr;
			string text = this._pStore.LockAssemblyPath(0U, asm, out intPtr);
			return new Store.AssemblyPathLock(this._pStore, intPtr, text);
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0011E748 File Offset: 0x0011C948
		[SecuritySafeCritical]
		public Store.IPathLock LockApplicationPath(IDefinitionAppId app)
		{
			IntPtr intPtr;
			string text = this._pStore.LockApplicationPath(0U, app, out intPtr);
			return new Store.ApplicationPathLock(this._pStore, intPtr, text);
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x0011E774 File Offset: 0x0011C974
		[SecuritySafeCritical]
		public ulong QueryChangeID(IDefinitionIdentity asm)
		{
			return this._pStore.QueryChangeID(asm);
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x0011E790 File Offset: 0x0011C990
		[SecuritySafeCritical]
		public StoreCategoryEnumeration EnumCategories(Store.EnumCategoriesFlags Flags, IReferenceIdentity CategoryMatch)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY));
			object obj = this._pStore.EnumCategories((uint)Flags, CategoryMatch, ref guidOfType);
			return new StoreCategoryEnumeration((IEnumSTORE_CATEGORY)obj);
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x0011E7C8 File Offset: 0x0011C9C8
		public StoreSubcategoryEnumeration EnumSubcategories(Store.EnumSubcategoriesFlags Flags, IDefinitionIdentity CategoryMatch)
		{
			return this.EnumSubcategories(Flags, CategoryMatch, null);
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x0011E7D4 File Offset: 0x0011C9D4
		[SecuritySafeCritical]
		public StoreSubcategoryEnumeration EnumSubcategories(Store.EnumSubcategoriesFlags Flags, IDefinitionIdentity Category, string SearchPattern)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_SUBCATEGORY));
			object obj = this._pStore.EnumSubcategories((uint)Flags, Category, SearchPattern, ref guidOfType);
			return new StoreSubcategoryEnumeration((IEnumSTORE_CATEGORY_SUBCATEGORY)obj);
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x0011E810 File Offset: 0x0011CA10
		[SecuritySafeCritical]
		public StoreCategoryInstanceEnumeration EnumCategoryInstances(Store.EnumCategoryInstancesFlags Flags, IDefinitionIdentity Category, string SubCat)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_INSTANCE));
			object obj = this._pStore.EnumCategoryInstances((uint)Flags, Category, SubCat, ref guidOfType);
			return new StoreCategoryInstanceEnumeration((IEnumSTORE_CATEGORY_INSTANCE)obj);
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x0011E84C File Offset: 0x0011CA4C
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

		// Token: 0x0600500E RID: 20494 RVA: 0x0011E8B4 File Offset: 0x0011CAB4
		[SecuritySafeCritical]
		public StoreDeploymentMetadataEnumeration EnumInstallerDeployments(Guid InstallerId, string InstallerName, string InstallerMetadata, IReferenceAppId DeploymentFilter)
		{
			StoreApplicationReference storeApplicationReference = new StoreApplicationReference(InstallerId, InstallerName, InstallerMetadata);
			object obj = this._pStore.EnumInstallerDeploymentMetadata(0U, ref storeApplicationReference, DeploymentFilter, ref IsolationInterop.IID_IEnumSTORE_DEPLOYMENT_METADATA);
			return new StoreDeploymentMetadataEnumeration((IEnumSTORE_DEPLOYMENT_METADATA)obj);
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x0011E8F0 File Offset: 0x0011CAF0
		[SecuritySafeCritical]
		public StoreDeploymentMetadataPropertyEnumeration EnumInstallerDeploymentProperties(Guid InstallerId, string InstallerName, string InstallerMetadata, IDefinitionAppId Deployment)
		{
			StoreApplicationReference storeApplicationReference = new StoreApplicationReference(InstallerId, InstallerName, InstallerMetadata);
			object obj = this._pStore.EnumInstallerDeploymentMetadataProperties(0U, ref storeApplicationReference, Deployment, ref IsolationInterop.IID_IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY);
			return new StoreDeploymentMetadataPropertyEnumeration((IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY)obj);
		}

		// Token: 0x0400226F RID: 8815
		private IStore _pStore;

		// Token: 0x02000C4F RID: 3151
		[Flags]
		public enum EnumAssembliesFlags
		{
			// Token: 0x04003790 RID: 14224
			Nothing = 0,
			// Token: 0x04003791 RID: 14225
			VisibleOnly = 1,
			// Token: 0x04003792 RID: 14226
			MatchServicing = 2,
			// Token: 0x04003793 RID: 14227
			ForceLibrarySemantics = 4
		}

		// Token: 0x02000C50 RID: 3152
		[Flags]
		public enum EnumAssemblyFilesFlags
		{
			// Token: 0x04003795 RID: 14229
			Nothing = 0,
			// Token: 0x04003796 RID: 14230
			IncludeInstalled = 1,
			// Token: 0x04003797 RID: 14231
			IncludeMissing = 2
		}

		// Token: 0x02000C51 RID: 3153
		[Flags]
		public enum EnumApplicationPrivateFiles
		{
			// Token: 0x04003799 RID: 14233
			Nothing = 0,
			// Token: 0x0400379A RID: 14234
			IncludeInstalled = 1,
			// Token: 0x0400379B RID: 14235
			IncludeMissing = 2
		}

		// Token: 0x02000C52 RID: 3154
		[Flags]
		public enum EnumAssemblyInstallReferenceFlags
		{
			// Token: 0x0400379D RID: 14237
			Nothing = 0
		}

		// Token: 0x02000C53 RID: 3155
		public interface IPathLock : IDisposable
		{
			// Token: 0x1700134F RID: 4943
			// (get) Token: 0x06007082 RID: 28802
			string Path { get; }
		}

		// Token: 0x02000C54 RID: 3156
		private class AssemblyPathLock : Store.IPathLock, IDisposable
		{
			// Token: 0x06007083 RID: 28803 RVA: 0x00184AD2 File Offset: 0x00182CD2
			public AssemblyPathLock(IStore s, IntPtr c, string path)
			{
				this._pSourceStore = s;
				this._pLockCookie = c;
				this._path = path;
			}

			// Token: 0x06007084 RID: 28804 RVA: 0x00184AFA File Offset: 0x00182CFA
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

			// Token: 0x06007085 RID: 28805 RVA: 0x00184B34 File Offset: 0x00182D34
			~AssemblyPathLock()
			{
				this.Dispose(false);
			}

			// Token: 0x06007086 RID: 28806 RVA: 0x00184B64 File Offset: 0x00182D64
			void IDisposable.Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x17001350 RID: 4944
			// (get) Token: 0x06007087 RID: 28807 RVA: 0x00184B6D File Offset: 0x00182D6D
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x0400379E RID: 14238
			private IStore _pSourceStore;

			// Token: 0x0400379F RID: 14239
			private IntPtr _pLockCookie = IntPtr.Zero;

			// Token: 0x040037A0 RID: 14240
			private string _path;
		}

		// Token: 0x02000C55 RID: 3157
		private class ApplicationPathLock : Store.IPathLock, IDisposable
		{
			// Token: 0x06007088 RID: 28808 RVA: 0x00184B75 File Offset: 0x00182D75
			public ApplicationPathLock(IStore s, IntPtr c, string path)
			{
				this._pSourceStore = s;
				this._pLockCookie = c;
				this._path = path;
			}

			// Token: 0x06007089 RID: 28809 RVA: 0x00184B9D File Offset: 0x00182D9D
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

			// Token: 0x0600708A RID: 28810 RVA: 0x00184BD8 File Offset: 0x00182DD8
			~ApplicationPathLock()
			{
				this.Dispose(false);
			}

			// Token: 0x0600708B RID: 28811 RVA: 0x00184C08 File Offset: 0x00182E08
			void IDisposable.Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x17001351 RID: 4945
			// (get) Token: 0x0600708C RID: 28812 RVA: 0x00184C11 File Offset: 0x00182E11
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x040037A1 RID: 14241
			private IStore _pSourceStore;

			// Token: 0x040037A2 RID: 14242
			private IntPtr _pLockCookie = IntPtr.Zero;

			// Token: 0x040037A3 RID: 14243
			private string _path;
		}

		// Token: 0x02000C56 RID: 3158
		[Flags]
		public enum EnumCategoriesFlags
		{
			// Token: 0x040037A5 RID: 14245
			Nothing = 0
		}

		// Token: 0x02000C57 RID: 3159
		[Flags]
		public enum EnumSubcategoriesFlags
		{
			// Token: 0x040037A7 RID: 14247
			Nothing = 0
		}

		// Token: 0x02000C58 RID: 3160
		[Flags]
		public enum EnumCategoryInstancesFlags
		{
			// Token: 0x040037A9 RID: 14249
			Nothing = 0
		}

		// Token: 0x02000C59 RID: 3161
		[Flags]
		public enum GetPackagePropertyFlags
		{
			// Token: 0x040037AB RID: 14251
			Nothing = 0
		}
	}
}
