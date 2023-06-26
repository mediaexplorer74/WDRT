using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.IsolatedStorage
{
	/// <summary>Represents an isolated storage area containing files and directories.</summary>
	// Token: 0x020001B5 RID: 437
	[ComVisible(true)]
	public sealed class IsolatedStorageFile : IsolatedStorage, IDisposable
	{
		// Token: 0x06001B57 RID: 6999 RVA: 0x0005C9C4 File Offset: 0x0005ABC4
		internal IsolatedStorageFile()
		{
		}

		/// <summary>Obtains user-scoped isolated storage corresponding to the application domain identity and assembly identity.</summary>
		/// <returns>An object corresponding to the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />, based on a combination of the application domain identity and the assembly identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The store failed to open.  
		///  -or-  
		///  The assembly specified has insufficient permissions to create isolated stores.  
		///  -or-  
		///  An isolated storage location cannot be initialized.  
		///  -or-  
		///  The permissions for the application domain cannot be determined.</exception>
		// Token: 0x06001B58 RID: 7000 RVA: 0x0005C9D7 File Offset: 0x0005ABD7
		public static IsolatedStorageFile GetUserStoreForDomain()
		{
			return IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);
		}

		/// <summary>Obtains user-scoped isolated storage corresponding to the calling code's assembly identity.</summary>
		/// <returns>An object corresponding to the isolated storage scope based on the calling code's assembly identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.  
		///  -or-  
		///  The permissions for the calling assembly cannot be determined.</exception>
		// Token: 0x06001B59 RID: 7001 RVA: 0x0005C9E1 File Offset: 0x0005ABE1
		public static IsolatedStorageFile GetUserStoreForAssembly()
		{
			return IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
		}

		/// <summary>Obtains user-scoped isolated storage corresponding to the calling code's application identity.</summary>
		/// <returns>An object corresponding to the isolated storage scope based on the calling code's assembly identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.  
		///  -or-  
		///  The application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> property returned <see langword="null" />.  
		///  -or-  
		///  The permissions for the application domain cannot be determined.</exception>
		// Token: 0x06001B5A RID: 7002 RVA: 0x0005C9EB File Offset: 0x0005ABEB
		public static IsolatedStorageFile GetUserStoreForApplication()
		{
			return IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Application, null);
		}

		/// <summary>Obtains a user-scoped isolated store for use by applications in a virtual host domain.</summary>
		/// <returns>The isolated storage file that corresponds to the isolated storage scope based on the calling code's application identity.</returns>
		// Token: 0x06001B5B RID: 7003 RVA: 0x0005C9F5 File Offset: 0x0005ABF5
		[ComVisible(false)]
		public static IsolatedStorageFile GetUserStoreForSite()
		{
			throw new NotSupportedException(Environment.GetResourceString("IsolatedStorage_NotValidOnDesktop"));
		}

		/// <summary>Obtains machine-scoped isolated storage corresponding to the application domain identity and the assembly identity.</summary>
		/// <returns>An object corresponding to the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />, based on a combination of the application domain identity and the assembly identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The store failed to open.  
		///  -or-  
		///  The assembly specified has insufficient permissions to create isolated stores.  
		///  -or-  
		///  The permissions for the application domain cannot be determined.  
		///  -or-  
		///  An isolated storage location cannot be initialized.</exception>
		// Token: 0x06001B5C RID: 7004 RVA: 0x0005CA06 File Offset: 0x0005AC06
		public static IsolatedStorageFile GetMachineStoreForDomain()
		{
			return IsolatedStorageFile.GetStore(IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine, null, null);
		}

		/// <summary>Obtains machine-scoped isolated storage corresponding to the calling code's assembly identity.</summary>
		/// <returns>An object corresponding to the isolated storage scope based on the calling code's assembly identity.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.</exception>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		// Token: 0x06001B5D RID: 7005 RVA: 0x0005CA11 File Offset: 0x0005AC11
		public static IsolatedStorageFile GetMachineStoreForAssembly()
		{
			return IsolatedStorageFile.GetStore(IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine, null, null);
		}

		/// <summary>Obtains machine-scoped isolated storage corresponding to the calling code's application identity.</summary>
		/// <returns>An object corresponding to the isolated storage scope based on the calling code's application identity.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The application identity of the caller could not be determined.  
		///  -or-  
		///  The granted permission set for the application domain could not be determined.  
		///  -or-  
		///  An isolated storage location cannot be initialized.</exception>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		// Token: 0x06001B5E RID: 7006 RVA: 0x0005CA1C File Offset: 0x0005AC1C
		public static IsolatedStorageFile GetMachineStoreForApplication()
		{
			return IsolatedStorageFile.GetStore(IsolatedStorageScope.Machine | IsolatedStorageScope.Application, null);
		}

		/// <summary>Obtains isolated storage corresponding to the isolated storage scope given the application domain and assembly evidence types.</summary>
		/// <param name="scope">A bitwise combination of the enumeration values.</param>
		/// <param name="domainEvidenceType">The type of the <see cref="T:System.Security.Policy.Evidence" /> that you can chose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the domain of the calling application. <see langword="null" /> lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence.</param>
		/// <param name="assemblyEvidenceType">The type of the <see cref="T:System.Security.Policy.Evidence" /> that you can chose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the domain of the calling application. <see langword="null" /> lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence.</param>
		/// <returns>An object that represents the parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The evidence type provided is missing in the assembly evidence list.  
		///  -or-  
		///  An isolated storage location cannot be initialized.  
		///  -or-  
		///  <paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned <see langword="null" />.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.  
		///  -or-  
		///  <paramref name="scope" /> contains <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		// Token: 0x06001B5F RID: 7007 RVA: 0x0005CA28 File Offset: 0x0005AC28
		[SecuritySafeCritical]
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
		{
			if (domainEvidenceType != null)
			{
				IsolatedStorageFile.DemandAdminPermission();
			}
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
			isolatedStorageFile.InitStore(scope, domainEvidenceType, assemblyEvidenceType);
			isolatedStorageFile.Init(scope);
			return isolatedStorageFile;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0005CA5A File Offset: 0x0005AC5A
		internal void EnsureStoreIsValid()
		{
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
		}

		/// <summary>Obtains the isolated storage corresponding to the given application domain and assembly evidence objects.</summary>
		/// <param name="scope">A bitwise combination of the enumeration values.</param>
		/// <param name="domainIdentity">An object that contains evidence for the application domain identity.</param>
		/// <param name="assemblyIdentity">An object that contains evidence for the code assembly identity.</param>
		/// <returns>An object that represents the parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.ArgumentNullException">Neither <paramref name="domainIdentity" /> nor <paramref name="assemblyIdentity" /> has been passed in. This verifies that the correct constructor is being used.  
		///  -or-  
		///  Either <paramref name="domainIdentity" /> or <paramref name="assemblyIdentity" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.  
		///  -or-  
		///  <paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned <see langword="null" />.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		// Token: 0x06001B61 RID: 7009 RVA: 0x0005CA90 File Offset: 0x0005AC90
		[SecuritySafeCritical]
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, object domainIdentity, object assemblyIdentity)
		{
			if (assemblyIdentity == null)
			{
				throw new ArgumentNullException("assemblyIdentity");
			}
			if (IsolatedStorage.IsDomain(scope) && domainIdentity == null)
			{
				throw new ArgumentNullException("domainIdentity");
			}
			IsolatedStorageFile.DemandAdminPermission();
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
			isolatedStorageFile.InitStore(scope, domainIdentity, assemblyIdentity, null);
			isolatedStorageFile.Init(scope);
			return isolatedStorageFile;
		}

		/// <summary>Obtains isolated storage corresponding to the given application domain and the assembly evidence objects and types.</summary>
		/// <param name="scope">A bitwise combination of the enumeration values.</param>
		/// <param name="domainEvidence">An object that contains the application domain identity.</param>
		/// <param name="domainEvidenceType">The identity type to choose from the application domain evidence.</param>
		/// <param name="assemblyEvidence">An object that contains the code assembly identity.</param>
		/// <param name="assemblyEvidenceType">The identity type to choose from the application code assembly evidence.</param>
		/// <returns>An object that represents the parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="domainEvidence" /> or <paramref name="assemblyEvidence" /> identity has not been passed in.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.  
		///  -or-  
		///  <paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned <see langword="null" />.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		// Token: 0x06001B62 RID: 7010 RVA: 0x0005CAE0 File Offset: 0x0005ACE0
		[SecuritySafeCritical]
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Evidence domainEvidence, Type domainEvidenceType, Evidence assemblyEvidence, Type assemblyEvidenceType)
		{
			if (assemblyEvidence == null)
			{
				throw new ArgumentNullException("assemblyEvidence");
			}
			if (IsolatedStorage.IsDomain(scope) && domainEvidence == null)
			{
				throw new ArgumentNullException("domainEvidence");
			}
			IsolatedStorageFile.DemandAdminPermission();
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
			isolatedStorageFile.InitStore(scope, domainEvidence, domainEvidenceType, assemblyEvidence, assemblyEvidenceType, null, null);
			isolatedStorageFile.Init(scope);
			return isolatedStorageFile;
		}

		/// <summary>Obtains isolated storage corresponding to the isolation scope and the application identity object.</summary>
		/// <param name="scope">A bitwise combination of the enumeration values.</param>
		/// <param name="applicationEvidenceType">An object that contains the application identity.</param>
		/// <returns>An object that represents the parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.ArgumentNullException">The   <paramref name="applicationEvidence" /> identity has not been passed in.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.  
		///  -or-  
		///  <paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned <see langword="null" />.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		// Token: 0x06001B63 RID: 7011 RVA: 0x0005CB34 File Offset: 0x0005AD34
		[SecuritySafeCritical]
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type applicationEvidenceType)
		{
			if (applicationEvidenceType != null)
			{
				IsolatedStorageFile.DemandAdminPermission();
			}
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
			isolatedStorageFile.InitStore(scope, applicationEvidenceType);
			isolatedStorageFile.Init(scope);
			return isolatedStorageFile;
		}

		/// <summary>Obtains isolated storage corresponding to the given application identity.</summary>
		/// <param name="scope">A bitwise combination of the enumeration values.</param>
		/// <param name="applicationIdentity">An object that contains evidence for the application identity.</param>
		/// <returns>An object that represents the parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.ArgumentNullException">The  <paramref name="applicationIdentity" /> identity has not been passed in.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.  
		///  -or-  
		///  <paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined,because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned <see langword="null" />.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		// Token: 0x06001B64 RID: 7012 RVA: 0x0005CB68 File Offset: 0x0005AD68
		[SecuritySafeCritical]
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, object applicationIdentity)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			IsolatedStorageFile.DemandAdminPermission();
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
			isolatedStorageFile.InitStore(scope, null, null, applicationIdentity);
			isolatedStorageFile.Init(scope);
			return isolatedStorageFile;
		}

		/// <summary>Gets a value that represents the amount of the space used for isolated storage.</summary>
		/// <returns>The used isolated storage space, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x0005CBA0 File Offset: 0x0005ADA0
		public override long UsedSize
		{
			[SecuritySafeCritical]
			get
			{
				if (base.IsRoaming())
				{
					throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_CurrentSizeUndefined"));
				}
				object internalLock = this.m_internalLock;
				long usage;
				lock (internalLock)
				{
					if (this.m_bDisposed)
					{
						throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
					}
					if (this.m_closed)
					{
						throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
					}
					if (this.InvalidFileHandle)
					{
						this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
					}
					usage = (long)IsolatedStorageFile.GetUsage(this.m_handle);
				}
				return usage;
			}
		}

		/// <summary>Gets the current size of the isolated storage.</summary>
		/// <returns>The total number of bytes of storage currently in use within the isolated storage scope.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is unavailable. The current store has a roaming scope or is not open.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object size is undefined.</exception>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0005CC50 File Offset: 0x0005AE50
		[CLSCompliant(false)]
		[Obsolete("IsolatedStorageFile.CurrentSize has been deprecated because it is not CLS Compliant.  To get the current size use IsolatedStorageFile.UsedSize")]
		public override ulong CurrentSize
		{
			[SecuritySafeCritical]
			get
			{
				if (base.IsRoaming())
				{
					throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_CurrentSizeUndefined"));
				}
				object internalLock = this.m_internalLock;
				ulong usage;
				lock (internalLock)
				{
					if (this.m_bDisposed)
					{
						throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
					}
					if (this.m_closed)
					{
						throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
					}
					if (this.InvalidFileHandle)
					{
						this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
					}
					usage = IsolatedStorageFile.GetUsage(this.m_handle);
				}
				return usage;
			}
		}

		/// <summary>Gets a value that represents the amount of free space available for isolated storage.</summary>
		/// <returns>The available free space for isolated storage, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x0005CD00 File Offset: 0x0005AF00
		[ComVisible(false)]
		public override long AvailableFreeSpace
		{
			[SecuritySafeCritical]
			get
			{
				if (base.IsRoaming())
				{
					return long.MaxValue;
				}
				object internalLock = this.m_internalLock;
				long usage;
				lock (internalLock)
				{
					if (this.m_bDisposed)
					{
						throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
					}
					if (this.m_closed)
					{
						throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
					}
					if (this.InvalidFileHandle)
					{
						this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
					}
					usage = (long)IsolatedStorageFile.GetUsage(this.m_handle);
				}
				return this.Quota - usage;
			}
		}

		/// <summary>Gets a value that represents the maximum amount of space available for isolated storage.</summary>
		/// <returns>The limit of isolated storage space, in bytes.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0005CDB0 File Offset: 0x0005AFB0
		// (set) Token: 0x06001B69 RID: 7017 RVA: 0x0005CDCC File Offset: 0x0005AFCC
		[ComVisible(false)]
		public override long Quota
		{
			get
			{
				if (base.IsRoaming())
				{
					return long.MaxValue;
				}
				return base.Quota;
			}
			[SecuritySafeCritical]
			internal set
			{
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this.Lock(ref flag);
					object internalLock = this.m_internalLock;
					lock (internalLock)
					{
						if (this.InvalidFileHandle)
						{
							this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
						}
						IsolatedStorageFile.SetQuota(this.m_handle, value);
					}
				}
				finally
				{
					if (flag)
					{
						this.Unlock();
					}
				}
				base.Quota = value;
			}
		}

		/// <summary>Gets a value that indicates whether isolated storage is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x0005CE60 File Offset: 0x0005B060
		[ComVisible(false)]
		public static bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value representing the maximum amount of space available for isolated storage within the limits established by the quota.</summary>
		/// <returns>The limit of isolated storage space in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is unavailable. <see cref="P:System.IO.IsolatedStorage.IsolatedStorageFile.MaximumSize" /> cannot be determined without evidence from the assembly's creation. The evidence could not be determined when the object was created.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage error occurred.</exception>
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0005CE63 File Offset: 0x0005B063
		[CLSCompliant(false)]
		[Obsolete("IsolatedStorageFile.MaximumSize has been deprecated because it is not CLS Compliant.  To get the maximum size use IsolatedStorageFile.Quota")]
		public override ulong MaximumSize
		{
			get
			{
				if (base.IsRoaming())
				{
					return 9223372036854775807UL;
				}
				return base.MaximumSize;
			}
		}

		/// <summary>Enables an application to explicitly request a larger quota size, in bytes.</summary>
		/// <param name="newQuotaSize">The requested size, in bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the new quota is accepted; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="newQuotaSize" /> is less than current quota size.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="newQuotaSize" /> is less than zero, or less than or equal to the current quota size.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current scope is not for an application user.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		// Token: 0x06001B6C RID: 7020 RVA: 0x0005CE80 File Offset: 0x0005B080
		[SecuritySafeCritical]
		[ComVisible(false)]
		public override bool IncreaseQuotaTo(long newQuotaSize)
		{
			if (newQuotaSize <= this.Quota)
			{
				throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_OldQuotaLarger"));
			}
			if (this.m_StoreScope != (IsolatedStorageScope.User | IsolatedStorageScope.Application))
			{
				throw new NotSupportedException(Environment.GetResourceString("IsolatedStorage_OnlyIncreaseUserApplicationStore"));
			}
			IsolatedStorageSecurityState isolatedStorageSecurityState = IsolatedStorageSecurityState.CreateStateToIncreaseQuotaForApplication(newQuotaSize, this.Quota - this.AvailableFreeSpace);
			try
			{
				isolatedStorageSecurityState.EnsureState();
			}
			catch (IsolatedStorageException)
			{
				return false;
			}
			this.Quota = newQuotaSize;
			return true;
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x0005CEFC File Offset: 0x0005B0FC
		[SecuritySafeCritical]
		internal void Reserve(ulong lReserve)
		{
			if (base.IsRoaming())
			{
				return;
			}
			ulong quota = (ulong)this.Quota;
			object internalLock = this.m_internalLock;
			lock (internalLock)
			{
				if (this.m_bDisposed)
				{
					throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
				}
				if (this.m_closed)
				{
					throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
				}
				if (this.InvalidFileHandle)
				{
					this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
				}
				IsolatedStorageFile.Reserve(this.m_handle, quota, lReserve, false);
			}
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0005CFA8 File Offset: 0x0005B1A8
		internal void Unreserve(ulong lFree)
		{
			if (base.IsRoaming())
			{
				return;
			}
			ulong quota = (ulong)this.Quota;
			this.Unreserve(lFree, quota);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0005CFD0 File Offset: 0x0005B1D0
		[SecuritySafeCritical]
		internal void Unreserve(ulong lFree, ulong quota)
		{
			if (base.IsRoaming())
			{
				return;
			}
			object internalLock = this.m_internalLock;
			lock (internalLock)
			{
				if (this.m_bDisposed)
				{
					throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
				}
				if (this.m_closed)
				{
					throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
				}
				if (this.InvalidFileHandle)
				{
					this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
				}
				IsolatedStorageFile.Reserve(this.m_handle, quota, lFree, true);
			}
		}

		/// <summary>Deletes a file in the isolated storage scope.</summary>
		/// <param name="file">The relative path of the file to delete within the isolated storage scope.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The target file is open or the path is incorrect.</exception>
		/// <exception cref="T:System.ArgumentNullException">The file path is <see langword="null" />.</exception>
		// Token: 0x06001B70 RID: 7024 RVA: 0x0005D074 File Offset: 0x0005B274
		[SecuritySafeCritical]
		public void DeleteFile(string file)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			this.m_fiop.Assert();
			this.m_fiop.PermitOnly();
			long num = 0L;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.Lock(ref flag);
				try
				{
					string fullPath = this.GetFullPath(file);
					num = LongPathFile.GetLength(fullPath);
					LongPathFile.Delete(fullPath);
				}
				catch
				{
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteFile"));
				}
				this.Unreserve(IsolatedStorageFile.RoundToBlockSize((ulong)num));
			}
			finally
			{
				if (flag)
				{
					this.Unlock();
				}
			}
			CodeAccessPermission.RevertAll();
		}

		/// <summary>Determines whether the specified path refers to an existing file in the isolated store.</summary>
		/// <param name="path">The path and file name to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="path" /> refers to an existing file in the isolated store and is not <see langword="null" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.</exception>
		// Token: 0x06001B71 RID: 7025 RVA: 0x0005D118 File Offset: 0x0005B318
		[SecuritySafeCritical]
		[ComVisible(false)]
		public bool FileExists(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			this.m_fiop.Assert();
			this.m_fiop.PermitOnly();
			string fullPath = this.GetFullPath(path);
			string text = LongPath.NormalizePath(fullPath);
			if (path.EndsWith(Path.DirectorySeparatorChar.ToString() + ".", StringComparison.Ordinal))
			{
				if (text.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
				{
					text += ".";
				}
				else
				{
					text = text + Path.DirectorySeparatorChar.ToString() + ".";
				}
			}
			try
			{
				IsolatedStorageFile.Demand(new FileIOPermission(FileIOPermissionAccess.Read, new string[] { text }, false, false));
			}
			catch
			{
				return false;
			}
			bool flag = LongPathFile.Exists(text);
			CodeAccessPermission.RevertAll();
			return flag;
		}

		/// <summary>Determines whether the specified path refers to an existing directory in the isolated store.</summary>
		/// <param name="path">The path to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="path" /> refers to an existing directory in the isolated store and is not <see langword="null" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		// Token: 0x06001B72 RID: 7026 RVA: 0x0005D228 File Offset: 0x0005B428
		[SecuritySafeCritical]
		[ComVisible(false)]
		public bool DirectoryExists(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			this.m_fiop.Assert();
			this.m_fiop.PermitOnly();
			string fullPath = this.GetFullPath(path);
			string text = LongPath.NormalizePath(fullPath);
			if (fullPath.EndsWith(Path.DirectorySeparatorChar.ToString() + ".", StringComparison.Ordinal))
			{
				if (text.EndsWith(Path.DirectorySeparatorChar))
				{
					text += ".";
				}
				else
				{
					text = text + Path.DirectorySeparatorChar.ToString() + ".";
				}
			}
			try
			{
				IsolatedStorageFile.Demand(new FileIOPermission(FileIOPermissionAccess.Read, new string[] { text }, false, false));
			}
			catch
			{
				return false;
			}
			bool flag = LongPathDirectory.Exists(text);
			CodeAccessPermission.RevertAll();
			return flag;
		}

		/// <summary>Creates a directory in the isolated storage scope.</summary>
		/// <param name="dir">The relative path of the directory to create within the isolated storage scope.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The current code has insufficient permissions to create isolated storage directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">The directory path is <see langword="null" />.</exception>
		// Token: 0x06001B73 RID: 7027 RVA: 0x0005D330 File Offset: 0x0005B530
		[SecuritySafeCritical]
		public void CreateDirectory(string dir)
		{
			if (dir == null)
			{
				throw new ArgumentNullException("dir");
			}
			this.m_fiop.Assert();
			this.m_fiop.PermitOnly();
			string fullPath = this.GetFullPath(dir);
			string text = LongPath.NormalizePath(fullPath);
			try
			{
				IsolatedStorageFile.Demand(new FileIOPermission(FileIOPermissionAccess.Read, new string[] { text }, false, false));
			}
			catch
			{
				throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_CreateDirectory"));
			}
			string[] array = this.DirectoriesToCreate(text);
			if (array != null && array.Length != 0)
			{
				this.Reserve((ulong)(1024L * (long)array.Length));
				try
				{
					LongPathDirectory.CreateDirectory(array[array.Length - 1]);
				}
				catch
				{
					this.Unreserve((ulong)(1024L * (long)array.Length));
					try
					{
						LongPathDirectory.Delete(array[0], true);
					}
					catch
					{
					}
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_CreateDirectory"));
				}
				CodeAccessPermission.RevertAll();
				return;
			}
			if (LongPathDirectory.Exists(fullPath))
			{
				return;
			}
			throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_CreateDirectory"));
		}

		/// <summary>Returns the creation date and time of a specified file or directory.</summary>
		/// <param name="path">The path to the file or directory for which to obtain creation date and time information.</param>
		/// <returns>The creation date and time for the specified file or directory. This value is expressed in local time.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		// Token: 0x06001B74 RID: 7028 RVA: 0x0005D440 File Offset: 0x0005B640
		[SecuritySafeCritical]
		[ComVisible(false)]
		public DateTimeOffset GetCreationTime(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			this.m_fiop.Assert();
			this.m_fiop.PermitOnly();
			string fullPath = this.GetFullPath(path);
			string text = LongPath.NormalizePath(fullPath);
			try
			{
				IsolatedStorageFile.Demand(new FileIOPermission(FileIOPermissionAccess.Read, new string[] { text }, false, false));
			}
			catch
			{
				return new DateTimeOffset(1601, 1, 1, 0, 0, 0, TimeSpan.Zero).ToLocalTime();
			}
			DateTimeOffset creationTime = LongPathFile.GetCreationTime(text);
			CodeAccessPermission.RevertAll();
			return creationTime;
		}

		/// <summary>Returns the date and time a specified file or directory was last accessed.</summary>
		/// <param name="path">The path to the file or directory for which to obtain last access date and time information.</param>
		/// <returns>The date and time that the specified file or directory was last accessed. This value is expressed in local time.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		// Token: 0x06001B75 RID: 7029 RVA: 0x0005D52C File Offset: 0x0005B72C
		[SecuritySafeCritical]
		[ComVisible(false)]
		public DateTimeOffset GetLastAccessTime(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			this.m_fiop.Assert();
			this.m_fiop.PermitOnly();
			string fullPath = this.GetFullPath(path);
			string text = LongPath.NormalizePath(fullPath);
			try
			{
				IsolatedStorageFile.Demand(new FileIOPermission(FileIOPermissionAccess.Read, new string[] { text }, false, false));
			}
			catch
			{
				return new DateTimeOffset(1601, 1, 1, 0, 0, 0, TimeSpan.Zero).ToLocalTime();
			}
			DateTimeOffset lastAccessTime = LongPathFile.GetLastAccessTime(text);
			CodeAccessPermission.RevertAll();
			return lastAccessTime;
		}

		/// <summary>Returns the date and time a specified file or directory was last written to.</summary>
		/// <param name="path">The path to the file or directory for which to obtain last write date and time information.</param>
		/// <returns>The date and time that the specified file or directory was last written to. This value is expressed in local time.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		// Token: 0x06001B76 RID: 7030 RVA: 0x0005D618 File Offset: 0x0005B818
		[SecuritySafeCritical]
		[ComVisible(false)]
		public DateTimeOffset GetLastWriteTime(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			this.m_fiop.Assert();
			this.m_fiop.PermitOnly();
			string fullPath = this.GetFullPath(path);
			string text = LongPath.NormalizePath(fullPath);
			try
			{
				IsolatedStorageFile.Demand(new FileIOPermission(FileIOPermissionAccess.Read, new string[] { text }, false, false));
			}
			catch
			{
				return new DateTimeOffset(1601, 1, 1, 0, 0, 0, TimeSpan.Zero).ToLocalTime();
			}
			DateTimeOffset lastWriteTime = LongPathFile.GetLastWriteTime(text);
			CodeAccessPermission.RevertAll();
			return lastWriteTime;
		}

		/// <summary>Copies an existing file to a new file.</summary>
		/// <param name="sourceFileName">The name of the file to copy.</param>
		/// <param name="destinationFileName">The name of the destination file. This cannot be a directory or an existing file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.  
		///  -or-  
		///  <paramref name="destinationFileName" /> exists.  
		///  -or-  
		///  An I/O error has occurred.</exception>
		// Token: 0x06001B77 RID: 7031 RVA: 0x0005D704 File Offset: 0x0005B904
		[ComVisible(false)]
		public void CopyFile(string sourceFileName, string destinationFileName)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			if (sourceFileName.Trim().Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "sourceFileName");
			}
			if (destinationFileName.Trim().Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "destinationFileName");
			}
			this.CopyFile(sourceFileName, destinationFileName, false);
		}

		/// <summary>Copies an existing file to a new file, and optionally overwrites an existing file.</summary>
		/// <param name="sourceFileName">The name of the file to copy.</param>
		/// <param name="destinationFileName">The name of the destination file. This cannot be a directory.</param>
		/// <param name="overwrite">
		///   <see langword="true" /> if the destination file can be overwritten; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.  
		///  -or-  
		///  An I/O error has occurred.</exception>
		// Token: 0x06001B78 RID: 7032 RVA: 0x0005D77C File Offset: 0x0005B97C
		[SecuritySafeCritical]
		[ComVisible(false)]
		public void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			if (sourceFileName.Trim().Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "sourceFileName");
			}
			if (destinationFileName.Trim().Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "destinationFileName");
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			this.m_fiop.Assert();
			this.m_fiop.PermitOnly();
			string text = LongPath.NormalizePath(this.GetFullPath(sourceFileName));
			string text2 = LongPath.NormalizePath(this.GetFullPath(destinationFileName));
			try
			{
				IsolatedStorageFile.Demand(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[] { text }, false, false));
				IsolatedStorageFile.Demand(new FileIOPermission(FileIOPermissionAccess.Write, new string[] { text2 }, false, false));
			}
			catch
			{
				throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.Lock(ref flag);
				long num = 0L;
				try
				{
					num = LongPathFile.GetLength(text);
				}
				catch (FileNotFoundException)
				{
					throw new FileNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", new object[] { sourceFileName }));
				}
				catch (DirectoryNotFoundException)
				{
					throw new DirectoryNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", new object[] { sourceFileName }));
				}
				catch
				{
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
				}
				long num2 = 0L;
				if (LongPathFile.Exists(text2))
				{
					try
					{
						num2 = LongPathFile.GetLength(text2);
					}
					catch
					{
						throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
					}
				}
				if (num2 < num)
				{
					this.Reserve(IsolatedStorageFile.RoundToBlockSize((ulong)(num - num2)));
				}
				try
				{
					LongPathFile.Copy(text, text2, overwrite);
				}
				catch (FileNotFoundException)
				{
					if (num2 < num)
					{
						this.Unreserve(IsolatedStorageFile.RoundToBlockSize((ulong)(num - num2)));
					}
					throw new FileNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", new object[] { sourceFileName }));
				}
				catch
				{
					if (num2 < num)
					{
						this.Unreserve(IsolatedStorageFile.RoundToBlockSize((ulong)(num - num2)));
					}
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
				}
				if (num2 > num && overwrite)
				{
					this.Unreserve(IsolatedStorageFile.RoundToBlockSizeFloor((ulong)(num2 - num)));
				}
			}
			finally
			{
				if (flag)
				{
					this.Unlock();
				}
			}
		}

		/// <summary>Moves a specified file to a new location, and optionally lets you specify a new file name.</summary>
		/// <param name="sourceFileName">The name of the file to move.</param>
		/// <param name="destinationFileName">The path to the new location for the file. If a file name is included, the moved file will have that name.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		// Token: 0x06001B79 RID: 7033 RVA: 0x0005DA70 File Offset: 0x0005BC70
		[SecuritySafeCritical]
		[ComVisible(false)]
		public void MoveFile(string sourceFileName, string destinationFileName)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			if (sourceFileName.Trim().Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "sourceFileName");
			}
			if (destinationFileName.Trim().Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "destinationFileName");
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			this.m_fiop.Assert();
			this.m_fiop.PermitOnly();
			string text = LongPath.NormalizePath(this.GetFullPath(sourceFileName));
			string text2 = LongPath.NormalizePath(this.GetFullPath(destinationFileName));
			try
			{
				IsolatedStorageFile.Demand(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[] { text }, false, false));
				IsolatedStorageFile.Demand(new FileIOPermission(FileIOPermissionAccess.Write, new string[] { text2 }, false, false));
			}
			catch
			{
				throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
			}
			try
			{
				LongPathFile.Move(text, text2);
			}
			catch (FileNotFoundException)
			{
				throw new FileNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", new object[] { sourceFileName }));
			}
			catch
			{
				throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
			}
			CodeAccessPermission.RevertAll();
		}

		/// <summary>Moves a specified directory and its contents to a new location.</summary>
		/// <param name="sourceDirectoryName">The name of the directory to move.</param>
		/// <param name="destinationDirectoryName">The path to the new location for <paramref name="sourceDirectoryName" />. This cannot be the path to an existing directory.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceDirectoryName" /> does not exist.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.  
		///  -or-  
		///  <paramref name="destinationDirectoryName" /> already exists.  
		///  -or-  
		///  <paramref name="sourceDirectoryName" /> and <paramref name="destinationDirectoryName" /> refer to the same directory.</exception>
		// Token: 0x06001B7A RID: 7034 RVA: 0x0005DBE4 File Offset: 0x0005BDE4
		[SecuritySafeCritical]
		[ComVisible(false)]
		public void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName)
		{
			if (sourceDirectoryName == null)
			{
				throw new ArgumentNullException("sourceDirectoryName");
			}
			if (destinationDirectoryName == null)
			{
				throw new ArgumentNullException("destinationDirectoryName");
			}
			if (sourceDirectoryName.Trim().Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "sourceDirectoryName");
			}
			if (destinationDirectoryName.Trim().Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "destinationDirectoryName");
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			this.m_fiop.Assert();
			this.m_fiop.PermitOnly();
			string text = LongPath.NormalizePath(this.GetFullPath(sourceDirectoryName));
			string text2 = LongPath.NormalizePath(this.GetFullPath(destinationDirectoryName));
			try
			{
				IsolatedStorageFile.Demand(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[] { text }, false, false));
				IsolatedStorageFile.Demand(new FileIOPermission(FileIOPermissionAccess.Write, new string[] { text2 }, false, false));
			}
			catch
			{
				throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
			}
			try
			{
				LongPathDirectory.Move(text, text2);
			}
			catch (DirectoryNotFoundException)
			{
				throw new DirectoryNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", new object[] { sourceDirectoryName }));
			}
			catch
			{
				throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
			}
			CodeAccessPermission.RevertAll();
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0005DD58 File Offset: 0x0005BF58
		[SecurityCritical]
		private string[] DirectoriesToCreate(string fullPath)
		{
			List<string> list = new List<string>();
			int num = fullPath.Length;
			if (num >= 2 && fullPath[num - 1] == this.SeparatorExternal)
			{
				num--;
			}
			int i = LongPath.GetRootLength(fullPath);
			while (i < num)
			{
				i++;
				while (i < num && fullPath[i] != this.SeparatorExternal)
				{
					i++;
				}
				string text = fullPath.Substring(0, i);
				if (!LongPathDirectory.InternalExists(text))
				{
					list.Add(text);
				}
			}
			if (list.Count != 0)
			{
				return list.ToArray();
			}
			return null;
		}

		/// <summary>Deletes a directory in the isolated storage scope.</summary>
		/// <param name="dir">The relative path of the directory to delete within the isolated storage scope.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The directory could not be deleted.</exception>
		/// <exception cref="T:System.ArgumentNullException">The directory path was <see langword="null" />.</exception>
		// Token: 0x06001B7C RID: 7036 RVA: 0x0005DDE0 File Offset: 0x0005BFE0
		[SecuritySafeCritical]
		public void DeleteDirectory(string dir)
		{
			if (dir == null)
			{
				throw new ArgumentNullException("dir");
			}
			this.m_fiop.Assert();
			this.m_fiop.PermitOnly();
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.Lock(ref flag);
				try
				{
					string text = LongPath.NormalizePath(this.GetFullPath(dir));
					if (text.Equals(LongPath.NormalizePath(this.GetFullPath(".")), StringComparison.Ordinal))
					{
						throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectory"));
					}
					LongPathDirectory.Delete(text, false);
				}
				catch
				{
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectory"));
				}
				this.Unreserve(1024UL);
			}
			finally
			{
				if (flag)
				{
					this.Unlock();
				}
			}
			CodeAccessPermission.RevertAll();
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0005DEAC File Offset: 0x0005C0AC
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void Demand(CodeAccessPermission permission)
		{
			permission.Demand();
		}

		/// <summary>Enumerates the file names at the root of an isolated store.</summary>
		/// <returns>An array of relative paths of files at the root of the isolated store.  A zero-length array specifies that there are no files at the root.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">File paths from the isolated store root cannot be determined.</exception>
		// Token: 0x06001B7E RID: 7038 RVA: 0x0005DEB4 File Offset: 0x0005C0B4
		[ComVisible(false)]
		public string[] GetFileNames()
		{
			return this.GetFileNames("*");
		}

		/// <summary>Gets the file names that match a search pattern.</summary>
		/// <param name="searchPattern">A search pattern. Both single-character ("?") and multi-character ("*") wildcards are supported.</param>
		/// <returns>An array of relative paths of files in the isolated storage scope that match <paramref name="searchPattern" />. A zero-length array specifies that there are no files that match.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The file path specified by <paramref name="searchPattern" /> cannot be found.</exception>
		// Token: 0x06001B7F RID: 7039 RVA: 0x0005DEC4 File Offset: 0x0005C0C4
		[SecuritySafeCritical]
		public string[] GetFileNames(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			this.m_fiop.Assert();
			this.m_fiop.PermitOnly();
			string[] fileDirectoryNames = IsolatedStorageFile.GetFileDirectoryNames(this.GetFullPath(searchPattern), searchPattern, true);
			CodeAccessPermission.RevertAll();
			return fileDirectoryNames;
		}

		/// <summary>Enumerates the directories at the root of an isolated store.</summary>
		/// <returns>An array of relative paths of directories at the root of the isolated store. A zero-length array specifies that there are no directories at the root.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Caller does not have permission to enumerate directories.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">One or more directories are not found.</exception>
		// Token: 0x06001B80 RID: 7040 RVA: 0x0005DF3B File Offset: 0x0005C13B
		[ComVisible(false)]
		public string[] GetDirectoryNames()
		{
			return this.GetDirectoryNames("*");
		}

		/// <summary>Enumerates the directories in an isolated storage scope that match a given search pattern.</summary>
		/// <param name="searchPattern">A search pattern. Both single-character ("?") and multi-character ("*") wildcards are supported.</param>
		/// <returns>An array of the relative paths of directories in the isolated storage scope that match <paramref name="searchPattern" />. A zero-length array specifies that there are no directories that match.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Caller does not have permission to enumerate directories resolved from <paramref name="searchPattern" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory or directories specified by <paramref name="searchPattern" /> are not found.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.</exception>
		// Token: 0x06001B81 RID: 7041 RVA: 0x0005DF48 File Offset: 0x0005C148
		[SecuritySafeCritical]
		public string[] GetDirectoryNames(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			if (this.m_closed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
			}
			this.m_fiop.Assert();
			this.m_fiop.PermitOnly();
			string[] fileDirectoryNames = IsolatedStorageFile.GetFileDirectoryNames(this.GetFullPath(searchPattern), searchPattern, false);
			CodeAccessPermission.RevertAll();
			return fileDirectoryNames;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0005DFC0 File Offset: 0x0005C1C0
		private static string NormalizeSearchPattern(string searchPattern)
		{
			string text = searchPattern.TrimEnd(Path.TrimEndChars);
			Path.CheckSearchPattern(text);
			return text;
		}

		/// <summary>Opens a file in the specified mode.</summary>
		/// <param name="path">The relative path of the file within the isolated store.</param>
		/// <param name="mode">One of the enumeration values that specifies how to open the file.</param>
		/// <returns>A file that is opened in the specified mode, with read/write access, and is unshared.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory in <paramref name="path" /> does not exist.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x06001B83 RID: 7043 RVA: 0x0005DFE0 File Offset: 0x0005C1E0
		[ComVisible(false)]
		public IsolatedStorageFileStream OpenFile(string path, FileMode mode)
		{
			return new IsolatedStorageFileStream(path, mode, this);
		}

		/// <summary>Opens a file in the specified mode with the specified read/write access.</summary>
		/// <param name="path">The relative path of the file within the isolated store.</param>
		/// <param name="mode">One of the enumeration values that specifies how to open the file.</param>
		/// <param name="access">One of the enumeration values that specifies whether the file will be opened with read, write, or read/write access.</param>
		/// <returns>A file that is opened in the specified mode and access, and is unshared.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory in <paramref name="path" /> does not exist.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x06001B84 RID: 7044 RVA: 0x0005DFEA File Offset: 0x0005C1EA
		[ComVisible(false)]
		public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access)
		{
			return new IsolatedStorageFileStream(path, mode, access, this);
		}

		/// <summary>Opens a file in the specified mode, with the specified read/write access and sharing permission.</summary>
		/// <param name="path">The relative path of the file within the isolated store.</param>
		/// <param name="mode">One of the enumeration values that specifies how to open or create the file.</param>
		/// <param name="access">One of the enumeration values that specifies whether the file will be opened with read, write, or read/write access</param>
		/// <param name="share">A bitwise combination of enumeration values that specify the type of access other <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> objects have to this file.</param>
		/// <returns>A file that is opened in the specified mode and access, and with the specified sharing options.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory in <paramref name="path" /> does not exist.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="M:System.IO.FileInfo.Open(System.IO.FileMode)" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x06001B85 RID: 7045 RVA: 0x0005DFF5 File Offset: 0x0005C1F5
		[ComVisible(false)]
		public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access, FileShare share)
		{
			return new IsolatedStorageFileStream(path, mode, access, share, this);
		}

		/// <summary>Creates a file in the isolated store.</summary>
		/// <param name="path">The relative path of the file to create.</param>
		/// <returns>A new isolated storage file.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory in <paramref name="path" /> does not exist.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x06001B86 RID: 7046 RVA: 0x0005E002 File Offset: 0x0005C202
		[ComVisible(false)]
		public IsolatedStorageFileStream CreateFile(string path)
		{
			return new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, this);
		}

		/// <summary>Removes the isolated storage scope and all its contents.</summary>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store cannot be deleted.</exception>
		// Token: 0x06001B87 RID: 7047 RVA: 0x0005E010 File Offset: 0x0005C210
		[SecuritySafeCritical]
		public override void Remove()
		{
			string text = null;
			this.RemoveLogicalDir();
			this.Close();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(IsolatedStorageFile.GetRootDir(base.Scope));
			if (base.IsApp())
			{
				stringBuilder.Append(base.AppName);
				stringBuilder.Append(this.SeparatorExternal);
			}
			else
			{
				if (base.IsDomain())
				{
					stringBuilder.Append(base.DomainName);
					stringBuilder.Append(this.SeparatorExternal);
					text = stringBuilder.ToString();
				}
				stringBuilder.Append(base.AssemName);
				stringBuilder.Append(this.SeparatorExternal);
			}
			string text2 = stringBuilder.ToString();
			new FileIOPermission(FileIOPermissionAccess.AllAccess, text2).Assert();
			if (this.ContainsUnknownFiles(text2))
			{
				return;
			}
			try
			{
				LongPathDirectory.Delete(text2, true);
			}
			catch
			{
				return;
			}
			if (base.IsDomain())
			{
				CodeAccessPermission.RevertAssert();
				new FileIOPermission(FileIOPermissionAccess.AllAccess, text).Assert();
				if (!this.ContainsUnknownFiles(text))
				{
					try
					{
						LongPathDirectory.Delete(text, true);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x0005E120 File Offset: 0x0005C320
		[SecuritySafeCritical]
		private void RemoveLogicalDir()
		{
			this.m_fiop.Assert();
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.Lock(ref flag);
				if (Directory.Exists(this.RootDirectory))
				{
					ulong num = (ulong)(base.IsRoaming() ? 0L : (this.Quota - this.AvailableFreeSpace));
					ulong quota = (ulong)this.Quota;
					try
					{
						LongPathDirectory.Delete(this.RootDirectory, true);
					}
					catch
					{
						throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectories"));
					}
					this.Unreserve(num, quota);
				}
			}
			finally
			{
				if (flag)
				{
					this.Unlock();
				}
			}
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0005E1C8 File Offset: 0x0005C3C8
		private bool ContainsUnknownFiles(string rootDir)
		{
			string[] fileDirectoryNames;
			string[] fileDirectoryNames2;
			try
			{
				fileDirectoryNames = IsolatedStorageFile.GetFileDirectoryNames(rootDir + "*", "*", true);
				fileDirectoryNames2 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + "*", "*", false);
			}
			catch
			{
				throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectories"));
			}
			if (fileDirectoryNames2 != null && fileDirectoryNames2.Length != 0)
			{
				if (fileDirectoryNames2.Length > 1)
				{
					return true;
				}
				if (base.IsApp())
				{
					if (IsolatedStorageFile.NotAppFilesDir(fileDirectoryNames2[0]))
					{
						return true;
					}
				}
				else if (base.IsDomain())
				{
					if (IsolatedStorageFile.NotFilesDir(fileDirectoryNames2[0]))
					{
						return true;
					}
				}
				else if (IsolatedStorageFile.NotAssemFilesDir(fileDirectoryNames2[0]))
				{
					return true;
				}
			}
			if (fileDirectoryNames == null || fileDirectoryNames.Length == 0)
			{
				return false;
			}
			if (base.IsRoaming())
			{
				return fileDirectoryNames.Length > 1 || IsolatedStorageFile.NotIDFile(fileDirectoryNames[0]);
			}
			return fileDirectoryNames.Length > 2 || (IsolatedStorageFile.NotIDFile(fileDirectoryNames[0]) && IsolatedStorageFile.NotInfoFile(fileDirectoryNames[0])) || (fileDirectoryNames.Length == 2 && IsolatedStorageFile.NotIDFile(fileDirectoryNames[1]) && IsolatedStorageFile.NotInfoFile(fileDirectoryNames[1]));
		}

		/// <summary>Closes a store previously opened with <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetStore(System.IO.IsolatedStorage.IsolatedStorageScope,System.Type,System.Type)" />, <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForAssembly" />, or <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForDomain" />.</summary>
		// Token: 0x06001B8A RID: 7050 RVA: 0x0005E2C8 File Offset: 0x0005C4C8
		[SecuritySafeCritical]
		public void Close()
		{
			if (base.IsRoaming())
			{
				return;
			}
			object internalLock = this.m_internalLock;
			lock (internalLock)
			{
				if (!this.m_closed)
				{
					this.m_closed = true;
					if (this.m_handle != null)
					{
						this.m_handle.Dispose();
					}
					GC.SuppressFinalize(this);
				}
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />.</summary>
		// Token: 0x06001B8B RID: 7051 RVA: 0x0005E334 File Offset: 0x0005C534
		public void Dispose()
		{
			this.Close();
			this.m_bDisposed = true;
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06001B8C RID: 7052 RVA: 0x0005E344 File Offset: 0x0005C544
		~IsolatedStorageFile()
		{
			this.Dispose();
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0005E370 File Offset: 0x0005C570
		private static bool NotIDFile(string file)
		{
			return string.Compare(file, "identity.dat", StringComparison.Ordinal) != 0;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0005E381 File Offset: 0x0005C581
		private static bool NotInfoFile(string file)
		{
			return string.Compare(file, "info.dat", StringComparison.Ordinal) != 0 && string.Compare(file, "appinfo.dat", StringComparison.Ordinal) != 0;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0005E3A2 File Offset: 0x0005C5A2
		private static bool NotFilesDir(string dir)
		{
			return string.Compare(dir, "Files", StringComparison.Ordinal) != 0;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0005E3B3 File Offset: 0x0005C5B3
		internal static bool NotAssemFilesDir(string dir)
		{
			return string.Compare(dir, "AssemFiles", StringComparison.Ordinal) != 0;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0005E3C4 File Offset: 0x0005C5C4
		internal static bool NotAppFilesDir(string dir)
		{
			return string.Compare(dir, "AppFiles", StringComparison.Ordinal) != 0;
		}

		/// <summary>Removes the specified isolated storage scope for all identities.</summary>
		/// <param name="scope">A bitwise combination of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> values.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store cannot be removed.</exception>
		// Token: 0x06001B92 RID: 7058 RVA: 0x0005E3D8 File Offset: 0x0005C5D8
		[SecuritySafeCritical]
		public static void Remove(IsolatedStorageScope scope)
		{
			IsolatedStorageFile.VerifyGlobalScope(scope);
			IsolatedStorageFile.DemandAdminPermission();
			string rootDir = IsolatedStorageFile.GetRootDir(scope);
			new FileIOPermission(FileIOPermissionAccess.Write, rootDir).Assert();
			try
			{
				LongPathDirectory.Delete(rootDir, true);
				LongPathDirectory.CreateDirectory(rootDir);
			}
			catch
			{
				throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectories"));
			}
		}

		/// <summary>Gets the enumerator for the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> stores within an isolated storage scope.</summary>
		/// <param name="scope">Represents the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> for which to return isolated stores. <see langword="User" /> and <see langword="User|Roaming" /> are the only <see langword="IsolatedStorageScope" /> combinations supported.</param>
		/// <returns>Enumerator for the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> stores within the specified isolated storage scope.</returns>
		// Token: 0x06001B93 RID: 7059 RVA: 0x0005E434 File Offset: 0x0005C634
		[SecuritySafeCritical]
		public static IEnumerator GetEnumerator(IsolatedStorageScope scope)
		{
			IsolatedStorageFile.VerifyGlobalScope(scope);
			IsolatedStorageFile.DemandAdminPermission();
			return new IsolatedStorageFileEnumerator(scope);
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0005E447 File Offset: 0x0005C647
		internal string RootDirectory
		{
			get
			{
				return this.m_RootDir;
			}
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0005E450 File Offset: 0x0005C650
		internal string GetFullPath(string path)
		{
			if (path == string.Empty)
			{
				return this.RootDirectory;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.RootDirectory);
			if (path[0] == this.SeparatorExternal)
			{
				stringBuilder.Append(path.Substring(1));
			}
			else
			{
				stringBuilder.Append(path);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0005E4B4 File Offset: 0x0005C6B4
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private static string GetDataDirectoryFromActivationContext()
		{
			if (IsolatedStorageFile.s_appDataDir == null)
			{
				ActivationContext activationContext = AppDomain.CurrentDomain.ActivationContext;
				if (activationContext == null)
				{
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_ApplicationMissingIdentity"));
				}
				string text = activationContext.DataDirectory;
				if (text != null && text[text.Length - 1] != '\\')
				{
					text += "\\";
				}
				IsolatedStorageFile.s_appDataDir = text;
			}
			return IsolatedStorageFile.s_appDataDir;
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0005E520 File Offset: 0x0005C720
		[SecuritySafeCritical]
		internal void Init(IsolatedStorageScope scope)
		{
			IsolatedStorageFile.GetGlobalFileIOPerm(scope).Assert();
			this.m_StoreScope = scope;
			StringBuilder stringBuilder = new StringBuilder();
			if (IsolatedStorage.IsApp(scope))
			{
				stringBuilder.Append(IsolatedStorageFile.GetRootDir(scope));
				if (IsolatedStorageFile.s_appDataDir == null)
				{
					stringBuilder.Append(base.AppName);
					stringBuilder.Append(this.SeparatorExternal);
				}
				try
				{
					LongPathDirectory.CreateDirectory(stringBuilder.ToString());
				}
				catch
				{
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
				}
				this.CreateIDFile(stringBuilder.ToString(), IsolatedStorageScope.Application);
				this.m_InfoFile = stringBuilder.ToString() + "appinfo.dat";
				stringBuilder.Append("AppFiles");
			}
			else
			{
				stringBuilder.Append(IsolatedStorageFile.GetRootDir(scope));
				if (IsolatedStorage.IsDomain(scope))
				{
					stringBuilder.Append(base.DomainName);
					stringBuilder.Append(this.SeparatorExternal);
					try
					{
						LongPathDirectory.CreateDirectory(stringBuilder.ToString());
						this.CreateIDFile(stringBuilder.ToString(), IsolatedStorageScope.Domain);
					}
					catch
					{
						throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
					}
					this.m_InfoFile = stringBuilder.ToString() + "info.dat";
				}
				stringBuilder.Append(base.AssemName);
				stringBuilder.Append(this.SeparatorExternal);
				try
				{
					LongPathDirectory.CreateDirectory(stringBuilder.ToString());
					this.CreateIDFile(stringBuilder.ToString(), IsolatedStorageScope.Assembly);
				}
				catch
				{
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
				}
				if (IsolatedStorage.IsDomain(scope))
				{
					stringBuilder.Append("Files");
				}
				else
				{
					this.m_InfoFile = stringBuilder.ToString() + "info.dat";
					stringBuilder.Append("AssemFiles");
				}
			}
			stringBuilder.Append(this.SeparatorExternal);
			string text = stringBuilder.ToString();
			try
			{
				LongPathDirectory.CreateDirectory(text);
			}
			catch
			{
				throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
			}
			this.m_RootDir = text;
			this.m_fiop = new FileIOPermission(FileIOPermissionAccess.AllAccess, text);
			if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Application))
			{
				this.UpdateQuotaFromInfoFile();
			}
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0005E744 File Offset: 0x0005C944
		[SecurityCritical]
		private void UpdateQuotaFromInfoFile()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this.Lock(ref flag);
				object internalLock = this.m_internalLock;
				lock (internalLock)
				{
					if (this.InvalidFileHandle)
					{
						this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
					}
					long num = 0L;
					if (IsolatedStorageFile.GetQuota(this.m_handle, out num))
					{
						base.Quota = num;
					}
				}
			}
			finally
			{
				if (flag)
				{
					this.Unlock();
				}
			}
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0005E7DC File Offset: 0x0005C9DC
		[SecuritySafeCritical]
		internal bool InitExistingStore(IsolatedStorageScope scope)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.m_StoreScope = scope;
			stringBuilder.Append(IsolatedStorageFile.GetRootDir(scope));
			if (IsolatedStorage.IsApp(scope))
			{
				stringBuilder.Append(base.AppName);
				stringBuilder.Append(this.SeparatorExternal);
				this.m_InfoFile = stringBuilder.ToString() + "appinfo.dat";
				stringBuilder.Append("AppFiles");
			}
			else
			{
				if (IsolatedStorage.IsDomain(scope))
				{
					stringBuilder.Append(base.DomainName);
					stringBuilder.Append(this.SeparatorExternal);
					this.m_InfoFile = stringBuilder.ToString() + "info.dat";
				}
				stringBuilder.Append(base.AssemName);
				stringBuilder.Append(this.SeparatorExternal);
				if (IsolatedStorage.IsDomain(scope))
				{
					stringBuilder.Append("Files");
				}
				else
				{
					this.m_InfoFile = stringBuilder.ToString() + "info.dat";
					stringBuilder.Append("AssemFiles");
				}
			}
			stringBuilder.Append(this.SeparatorExternal);
			FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, stringBuilder.ToString());
			fileIOPermission.Assert();
			if (!LongPathDirectory.Exists(stringBuilder.ToString()))
			{
				return false;
			}
			this.m_RootDir = stringBuilder.ToString();
			this.m_fiop = fileIOPermission;
			if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Application))
			{
				this.UpdateQuotaFromInfoFile();
			}
			return true;
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0005E925 File Offset: 0x0005CB25
		protected override IsolatedStoragePermission GetPermission(PermissionSet ps)
		{
			if (ps == null)
			{
				return null;
			}
			if (ps.IsUnrestricted())
			{
				return new IsolatedStorageFilePermission(PermissionState.Unrestricted);
			}
			return (IsolatedStoragePermission)ps.GetPermission(typeof(IsolatedStorageFilePermission));
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0005E950 File Offset: 0x0005CB50
		internal void UndoReserveOperation(ulong oldLen, ulong newLen)
		{
			oldLen = IsolatedStorageFile.RoundToBlockSize(oldLen);
			if (newLen > oldLen)
			{
				this.Unreserve(IsolatedStorageFile.RoundToBlockSize(newLen - oldLen));
			}
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0005E96C File Offset: 0x0005CB6C
		internal void Reserve(ulong oldLen, ulong newLen)
		{
			oldLen = IsolatedStorageFile.RoundToBlockSize(oldLen);
			if (newLen > oldLen)
			{
				this.Reserve(IsolatedStorageFile.RoundToBlockSize(newLen - oldLen));
			}
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0005E988 File Offset: 0x0005CB88
		internal void ReserveOneBlock()
		{
			this.Reserve(1024UL);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0005E996 File Offset: 0x0005CB96
		internal void UnreserveOneBlock()
		{
			this.Unreserve(1024UL);
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0005E9A4 File Offset: 0x0005CBA4
		internal static ulong RoundToBlockSize(ulong num)
		{
			if (num < 1024UL)
			{
				return 1024UL;
			}
			ulong num2 = num % 1024UL;
			if (num2 != 0UL)
			{
				num += 1024UL - num2;
			}
			return num;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0005E9DC File Offset: 0x0005CBDC
		internal static ulong RoundToBlockSizeFloor(ulong num)
		{
			if (num < 1024UL)
			{
				return 0UL;
			}
			ulong num2 = num % 1024UL;
			num -= num2;
			return num;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0005EA04 File Offset: 0x0005CC04
		[SecurityCritical]
		internal static string GetRootDir(IsolatedStorageScope scope)
		{
			if (IsolatedStorage.IsRoaming(scope))
			{
				if (IsolatedStorageFile.s_RootDirRoaming == null)
				{
					string text = null;
					IsolatedStorageFile.GetRootDir(scope, JitHelpers.GetStringHandleOnStack(ref text));
					IsolatedStorageFile.s_RootDirRoaming = text;
				}
				return IsolatedStorageFile.s_RootDirRoaming;
			}
			if (IsolatedStorage.IsMachine(scope))
			{
				if (IsolatedStorageFile.s_RootDirMachine == null)
				{
					IsolatedStorageFile.InitGlobalsMachine(scope);
				}
				return IsolatedStorageFile.s_RootDirMachine;
			}
			if (IsolatedStorageFile.s_RootDirUser == null)
			{
				IsolatedStorageFile.InitGlobalsNonRoamingUser(scope);
			}
			return IsolatedStorageFile.s_RootDirUser;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0005EA78 File Offset: 0x0005CC78
		[SecuritySafeCritical]
		[HandleProcessCorruptedStateExceptions]
		private static void InitGlobalsMachine(IsolatedStorageScope scope)
		{
			string text = null;
			IsolatedStorageFile.GetRootDir(scope, JitHelpers.GetStringHandleOnStack(ref text));
			new FileIOPermission(FileIOPermissionAccess.AllAccess, text).Assert();
			string text2 = IsolatedStorageFile.GetMachineRandomDirectory(text);
			if (text2 == null)
			{
				Mutex mutex = IsolatedStorageFile.CreateMutexNotOwned(text);
				if (!mutex.WaitOne())
				{
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
				}
				try
				{
					text2 = IsolatedStorageFile.GetMachineRandomDirectory(text);
					if (text2 == null)
					{
						string randomFileName = Path.GetRandomFileName();
						string randomFileName2 = Path.GetRandomFileName();
						try
						{
							IsolatedStorageFile.CreateDirectoryWithDacl(text + randomFileName);
							IsolatedStorageFile.CreateDirectoryWithDacl(text + randomFileName + "\\" + randomFileName2);
						}
						catch
						{
							throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
						}
						text2 = randomFileName + "\\" + randomFileName2;
					}
				}
				finally
				{
					mutex.ReleaseMutex();
				}
			}
			IsolatedStorageFile.s_RootDirMachine = text + text2 + "\\";
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0005EB60 File Offset: 0x0005CD60
		[SecuritySafeCritical]
		private static void InitGlobalsNonRoamingUser(IsolatedStorageScope scope)
		{
			string text = null;
			if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Application))
			{
				text = IsolatedStorageFile.GetDataDirectoryFromActivationContext();
				if (text != null)
				{
					IsolatedStorageFile.s_RootDirUser = text;
					return;
				}
			}
			IsolatedStorageFile.GetRootDir(scope, JitHelpers.GetStringHandleOnStack(ref text));
			new FileIOPermission(FileIOPermissionAccess.AllAccess, text).Assert();
			bool flag = false;
			string text2 = null;
			string text3 = IsolatedStorageFile.GetRandomDirectory(text, out flag, out text2);
			if (text3 == null)
			{
				Mutex mutex = IsolatedStorageFile.CreateMutexNotOwned(text);
				if (!mutex.WaitOne())
				{
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
				}
				try
				{
					text3 = IsolatedStorageFile.GetRandomDirectory(text, out flag, out text2);
					if (text3 == null)
					{
						if (flag)
						{
							text3 = IsolatedStorageFile.MigrateOldIsoStoreDirectory(text, text2);
						}
						else
						{
							text3 = IsolatedStorageFile.CreateRandomDirectory(text);
						}
					}
				}
				finally
				{
					mutex.ReleaseMutex();
				}
			}
			IsolatedStorageFile.s_RootDirUser = text + text3 + "\\";
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x0005EC24 File Offset: 0x0005CE24
		internal bool Disposed
		{
			get
			{
				return this.m_bDisposed;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x0005EC2C File Offset: 0x0005CE2C
		private bool InvalidFileHandle
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_handle == null || this.m_handle.IsClosed || this.m_handle.IsInvalid;
			}
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0005EC50 File Offset: 0x0005CE50
		[SecuritySafeCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static string MigrateOldIsoStoreDirectory(string rootDir, string oldRandomDirectory)
		{
			string randomFileName = Path.GetRandomFileName();
			string randomFileName2 = Path.GetRandomFileName();
			string text = rootDir + randomFileName;
			string text2 = text + "\\" + randomFileName2;
			try
			{
				LongPathDirectory.CreateDirectory(text);
				LongPathDirectory.Move(rootDir + oldRandomDirectory, text2);
			}
			catch
			{
				throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
			}
			return randomFileName + "\\" + randomFileName2;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0005ECC0 File Offset: 0x0005CEC0
		[SecuritySafeCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static string CreateRandomDirectory(string rootDir)
		{
			string text;
			string text2;
			do
			{
				text = Path.GetRandomFileName() + "\\" + Path.GetRandomFileName();
				text2 = rootDir + text;
			}
			while (LongPathDirectory.Exists(text2));
			try
			{
				LongPathDirectory.CreateDirectory(text2);
			}
			catch
			{
				throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
			}
			return text;
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0005ED20 File Offset: 0x0005CF20
		internal static string GetRandomDirectory(string rootDir, out bool bMigrateNeeded, out string sOldStoreLocation)
		{
			bMigrateNeeded = false;
			sOldStoreLocation = null;
			string[] fileDirectoryNames = IsolatedStorageFile.GetFileDirectoryNames(rootDir + "*", "*", false);
			for (int i = 0; i < fileDirectoryNames.Length; i++)
			{
				if (fileDirectoryNames[i].Length == 12)
				{
					string[] fileDirectoryNames2 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + fileDirectoryNames[i] + "\\*", "*", false);
					for (int j = 0; j < fileDirectoryNames2.Length; j++)
					{
						if (fileDirectoryNames2[j].Length == 12)
						{
							return fileDirectoryNames[i] + "\\" + fileDirectoryNames2[j];
						}
					}
				}
			}
			for (int k = 0; k < fileDirectoryNames.Length; k++)
			{
				if (fileDirectoryNames[k].Length == 24)
				{
					bMigrateNeeded = true;
					sOldStoreLocation = fileDirectoryNames[k];
					return null;
				}
			}
			return null;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0005EDD4 File Offset: 0x0005CFD4
		internal static string GetMachineRandomDirectory(string rootDir)
		{
			string[] fileDirectoryNames = IsolatedStorageFile.GetFileDirectoryNames(rootDir + "*", "*", false);
			for (int i = 0; i < fileDirectoryNames.Length; i++)
			{
				if (fileDirectoryNames[i].Length == 12)
				{
					string[] fileDirectoryNames2 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + fileDirectoryNames[i] + "\\*", "*", false);
					for (int j = 0; j < fileDirectoryNames2.Length; j++)
					{
						if (fileDirectoryNames2[j].Length == 12)
						{
							return fileDirectoryNames[i] + "\\" + fileDirectoryNames2[j];
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0005EE58 File Offset: 0x0005D058
		[SecurityCritical]
		internal static Mutex CreateMutexNotOwned(string pathName)
		{
			return new Mutex(false, "Global\\" + IsolatedStorageFile.GetStrongHashSuitableForObjectName(pathName));
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0005EE70 File Offset: 0x0005D070
		internal static string GetStrongHashSuitableForObjectName(string name)
		{
			MemoryStream memoryStream = new MemoryStream();
			new BinaryWriter(memoryStream).Write(name.ToUpper(CultureInfo.InvariantCulture));
			memoryStream.Position = 0L;
			return Path.ToBase32StringSuitableForDirName(new SHA1CryptoServiceProvider().ComputeHash(memoryStream));
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0005EEB1 File Offset: 0x0005D0B1
		private string GetSyncObjectName()
		{
			if (this.m_SyncObjectName == null)
			{
				this.m_SyncObjectName = IsolatedStorageFile.GetStrongHashSuitableForObjectName(this.m_InfoFile);
			}
			return this.m_SyncObjectName;
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0005EED4 File Offset: 0x0005D0D4
		[SecuritySafeCritical]
		internal void Lock(ref bool locked)
		{
			locked = false;
			if (base.IsRoaming())
			{
				return;
			}
			object internalLock = this.m_internalLock;
			lock (internalLock)
			{
				if (this.m_bDisposed)
				{
					throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
				}
				if (this.m_closed)
				{
					throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
				}
				if (this.InvalidFileHandle)
				{
					this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
				}
				locked = IsolatedStorageFile.Lock(this.m_handle, true);
			}
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0005EF78 File Offset: 0x0005D178
		[SecuritySafeCritical]
		internal void Unlock()
		{
			if (base.IsRoaming())
			{
				return;
			}
			object internalLock = this.m_internalLock;
			lock (internalLock)
			{
				if (this.m_bDisposed)
				{
					throw new ObjectDisposedException(null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
				}
				if (this.m_closed)
				{
					throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
				}
				if (this.InvalidFileHandle)
				{
					this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
				}
				IsolatedStorageFile.Lock(this.m_handle, false);
			}
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0005F018 File Offset: 0x0005D218
		[SecurityCritical]
		internal static FileIOPermission GetGlobalFileIOPerm(IsolatedStorageScope scope)
		{
			if (IsolatedStorage.IsRoaming(scope))
			{
				if (IsolatedStorageFile.s_PermRoaming == null)
				{
					IsolatedStorageFile.s_PermRoaming = new FileIOPermission(FileIOPermissionAccess.AllAccess, IsolatedStorageFile.GetRootDir(scope));
				}
				return IsolatedStorageFile.s_PermRoaming;
			}
			if (IsolatedStorage.IsMachine(scope))
			{
				if (IsolatedStorageFile.s_PermMachine == null)
				{
					IsolatedStorageFile.s_PermMachine = new FileIOPermission(FileIOPermissionAccess.AllAccess, IsolatedStorageFile.GetRootDir(scope));
				}
				return IsolatedStorageFile.s_PermMachine;
			}
			if (IsolatedStorageFile.s_PermUser == null)
			{
				IsolatedStorageFile.s_PermUser = new FileIOPermission(FileIOPermissionAccess.AllAccess, IsolatedStorageFile.GetRootDir(scope));
			}
			return IsolatedStorageFile.s_PermUser;
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0005F0A3 File Offset: 0x0005D2A3
		[SecurityCritical]
		private static void DemandAdminPermission()
		{
			if (IsolatedStorageFile.s_PermAdminUser == null)
			{
				IsolatedStorageFile.s_PermAdminUser = new IsolatedStorageFilePermission(IsolatedStorageContainment.AdministerIsolatedStorageByUser, 0L, false);
			}
			IsolatedStorageFile.s_PermAdminUser.Demand();
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0005F0CB File Offset: 0x0005D2CB
		internal static void VerifyGlobalScope(IsolatedStorageScope scope)
		{
			if (scope != IsolatedStorageScope.User && scope != (IsolatedStorageScope.User | IsolatedStorageScope.Roaming) && scope != IsolatedStorageScope.Machine)
			{
				throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_Scope_U_R_M"));
			}
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0005F0EC File Offset: 0x0005D2EC
		[SecuritySafeCritical]
		internal void CreateIDFile(string path, IsolatedStorageScope scope)
		{
			try
			{
				using (FileStream fileStream = new FileStream(path + "identity.dat", FileMode.OpenOrCreate))
				{
					MemoryStream identityStream = base.GetIdentityStream(scope);
					byte[] buffer = identityStream.GetBuffer();
					fileStream.Write(buffer, 0, (int)identityStream.Length);
					identityStream.Close();
				}
			}
			catch
			{
			}
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0005F15C File Offset: 0x0005D35C
		[SecuritySafeCritical]
		internal static string[] GetFileDirectoryNames(string path, string userSearchPattern, bool file)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path", Environment.GetResourceString("ArgumentNull_Path"));
			}
			userSearchPattern = IsolatedStorageFile.NormalizeSearchPattern(userSearchPattern);
			if (userSearchPattern.Length == 0)
			{
				return new string[0];
			}
			bool flag = false;
			char c = path[path.Length - 1];
			if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar || c == '.')
			{
				flag = true;
			}
			string text = LongPath.NormalizePath(path);
			if (flag && text[text.Length - 1] != c)
			{
				text += "\\*";
			}
			string text2 = LongPath.GetDirectoryName(text);
			if (text2 != null)
			{
				text2 += "\\";
			}
			try
			{
				new FileIOPermission(FileIOPermissionAccess.Read, new string[] { (text2 == null) ? text : text2 }, false, false).Demand();
			}
			catch
			{
				throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
			}
			string[] array = new string[10];
			int num = 0;
			Win32Native.WIN32_FIND_DATA win32_FIND_DATA = default(Win32Native.WIN32_FIND_DATA);
			SafeFindHandle safeFindHandle = Win32Native.FindFirstFile(Path.AddLongPathPrefix(text), ref win32_FIND_DATA);
			int num2;
			if (safeFindHandle.IsInvalid)
			{
				num2 = Marshal.GetLastWin32Error();
				if (num2 == 2)
				{
					return new string[0];
				}
				__Error.WinIOError(num2, userSearchPattern);
			}
			int num3 = 0;
			do
			{
				bool flag2;
				if (file)
				{
					flag2 = win32_FIND_DATA.IsFile;
				}
				else
				{
					flag2 = win32_FIND_DATA.IsNormalDirectory;
				}
				if (flag2)
				{
					num3++;
					if (num == array.Length)
					{
						Array.Resize<string>(ref array, 2 * array.Length);
					}
					array[num++] = win32_FIND_DATA.cFileName;
				}
			}
			while (Win32Native.FindNextFile(safeFindHandle, ref win32_FIND_DATA));
			num2 = Marshal.GetLastWin32Error();
			safeFindHandle.Close();
			if (num2 != 0 && num2 != 18)
			{
				__Error.WinIOError(num2, userSearchPattern);
			}
			if (!file && num3 == 1 && (win32_FIND_DATA.dwFileAttributes & 16) != 0)
			{
				return new string[] { win32_FIND_DATA.cFileName };
			}
			if (num == array.Length)
			{
				return array;
			}
			Array.Resize<string>(ref array, num);
			return array;
		}

		// Token: 0x06001BB4 RID: 7092
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern ulong GetUsage(SafeIsolatedStorageFileHandle handle);

		// Token: 0x06001BB5 RID: 7093
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern SafeIsolatedStorageFileHandle Open(string infoFile, string syncName);

		// Token: 0x06001BB6 RID: 7094
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void Reserve(SafeIsolatedStorageFileHandle handle, ulong plQuota, ulong plReserve, [MarshalAs(UnmanagedType.Bool)] bool fFree);

		// Token: 0x06001BB7 RID: 7095
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void GetRootDir(IsolatedStorageScope scope, StringHandleOnStack retRootDir);

		// Token: 0x06001BB8 RID: 7096
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool Lock(SafeIsolatedStorageFileHandle handle, [MarshalAs(UnmanagedType.Bool)] bool fLock);

		// Token: 0x06001BB9 RID: 7097
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void CreateDirectoryWithDacl(string path);

		// Token: 0x06001BBA RID: 7098
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetQuota(SafeIsolatedStorageFileHandle scope, out long quota);

		// Token: 0x06001BBB RID: 7099
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern void SetQuota(SafeIsolatedStorageFileHandle scope, long quota);

		// Token: 0x04000979 RID: 2425
		private const int s_BlockSize = 1024;

		// Token: 0x0400097A RID: 2426
		private const int s_DirSize = 1024;

		// Token: 0x0400097B RID: 2427
		private const string s_name = "file.store";

		// Token: 0x0400097C RID: 2428
		internal const string s_Files = "Files";

		// Token: 0x0400097D RID: 2429
		internal const string s_AssemFiles = "AssemFiles";

		// Token: 0x0400097E RID: 2430
		internal const string s_AppFiles = "AppFiles";

		// Token: 0x0400097F RID: 2431
		internal const string s_IDFile = "identity.dat";

		// Token: 0x04000980 RID: 2432
		internal const string s_InfoFile = "info.dat";

		// Token: 0x04000981 RID: 2433
		internal const string s_AppInfoFile = "appinfo.dat";

		// Token: 0x04000982 RID: 2434
		private static volatile string s_RootDirUser;

		// Token: 0x04000983 RID: 2435
		private static volatile string s_RootDirMachine;

		// Token: 0x04000984 RID: 2436
		private static volatile string s_RootDirRoaming;

		// Token: 0x04000985 RID: 2437
		private static volatile string s_appDataDir;

		// Token: 0x04000986 RID: 2438
		private static volatile FileIOPermission s_PermUser;

		// Token: 0x04000987 RID: 2439
		private static volatile FileIOPermission s_PermMachine;

		// Token: 0x04000988 RID: 2440
		private static volatile FileIOPermission s_PermRoaming;

		// Token: 0x04000989 RID: 2441
		private static volatile IsolatedStorageFilePermission s_PermAdminUser;

		// Token: 0x0400098A RID: 2442
		private FileIOPermission m_fiop;

		// Token: 0x0400098B RID: 2443
		private string m_RootDir;

		// Token: 0x0400098C RID: 2444
		private string m_InfoFile;

		// Token: 0x0400098D RID: 2445
		private string m_SyncObjectName;

		// Token: 0x0400098E RID: 2446
		[SecurityCritical]
		private SafeIsolatedStorageFileHandle m_handle;

		// Token: 0x0400098F RID: 2447
		private bool m_closed;

		// Token: 0x04000990 RID: 2448
		private bool m_bDisposed;

		// Token: 0x04000991 RID: 2449
		private object m_internalLock = new object();

		// Token: 0x04000992 RID: 2450
		private IsolatedStorageScope m_StoreScope;
	}
}
