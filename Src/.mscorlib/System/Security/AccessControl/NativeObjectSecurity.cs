using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to control access to native objects without direct manipulation of Access Control Lists (ACLs). Native object types are defined by the <see cref="T:System.Security.AccessControl.ResourceType" /> enumeration.</summary>
	// Token: 0x02000222 RID: 546
	public abstract class NativeObjectSecurity : CommonObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		// Token: 0x06001F7B RID: 8059 RVA: 0x0006DDCF File Offset: 0x0006BFCF
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType)
			: base(isContainer)
		{
			this._resourceType = resourceType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class by using the specified values.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="exceptionFromErrorCode">A delegate implemented by integrators that provides custom exceptions.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x06001F7C RID: 8060 RVA: 0x0006DE0B File Offset: 0x0006C00B
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: this(isContainer, resourceType)
		{
			this._exceptionContext = exceptionContext;
			this._exceptionFromErrorCode = exceptionFromErrorCode;
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0006DE24 File Offset: 0x0006C024
		[SecurityCritical]
		internal NativeObjectSecurity(ResourceType resourceType, CommonSecurityDescriptor securityDescriptor)
			: this(resourceType, securityDescriptor, null)
		{
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x0006DE30 File Offset: 0x0006C030
		[SecurityCritical]
		internal NativeObjectSecurity(ResourceType resourceType, CommonSecurityDescriptor securityDescriptor, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode)
			: base(securityDescriptor)
		{
			this._resourceType = resourceType;
			this._exceptionFromErrorCode = exceptionFromErrorCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="name">The name of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		/// <param name="exceptionFromErrorCode">A delegate implemented by integrators that provides custom exceptions.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x06001F7F RID: 8063 RVA: 0x0006DE80 File Offset: 0x0006C080
		[SecuritySafeCritical]
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: this(resourceType, NativeObjectSecurity.CreateInternal(resourceType, isContainer, name, null, includeSections, true, exceptionFromErrorCode, exceptionContext), exceptionFromErrorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="name">The name of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		// Token: 0x06001F80 RID: 8064 RVA: 0x0006DEA6 File Offset: 0x0006C0A6
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections)
			: this(isContainer, resourceType, name, includeSections, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="handle">The handle of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		/// <param name="exceptionFromErrorCode">A delegate implemented by integrators that provides custom exceptions.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		// Token: 0x06001F81 RID: 8065 RVA: 0x0006DEB8 File Offset: 0x0006C0B8
		[SecuritySafeCritical]
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: this(resourceType, NativeObjectSecurity.CreateInternal(resourceType, isContainer, null, handle, includeSections, false, exceptionFromErrorCode, exceptionContext), exceptionFromErrorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class with the specified values. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is a container object.</param>
		/// <param name="resourceType">The type of securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="handle">The handle of the securable object with which the new <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to include in this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object.</param>
		// Token: 0x06001F82 RID: 8066 RVA: 0x0006DEDE File Offset: 0x0006C0DE
		[SecuritySafeCritical]
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections)
			: this(isContainer, resourceType, handle, includeSections, null, null)
		{
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x0006DEF0 File Offset: 0x0006C0F0
		[SecurityCritical]
		private static CommonSecurityDescriptor CreateInternal(ResourceType resourceType, bool isContainer, string name, SafeHandle handle, AccessControlSections includeSections, bool createByName, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
		{
			if (createByName && name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (!createByName && handle == null)
			{
				throw new ArgumentNullException("handle");
			}
			RawSecurityDescriptor rawSecurityDescriptor;
			int securityInfo = Win32.GetSecurityInfo(resourceType, name, handle, includeSections, out rawSecurityDescriptor);
			if (securityInfo != 0)
			{
				Exception ex = null;
				if (exceptionFromErrorCode != null)
				{
					ex = exceptionFromErrorCode(securityInfo, name, handle, exceptionContext);
				}
				if (ex == null)
				{
					if (securityInfo == 5)
					{
						ex = new UnauthorizedAccessException();
					}
					else if (securityInfo == 1307)
					{
						ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidOwner"));
					}
					else if (securityInfo == 1308)
					{
						ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidGroup"));
					}
					else if (securityInfo == 87)
					{
						ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_UnexpectedError", new object[] { securityInfo }));
					}
					else if (securityInfo == 123)
					{
						ex = new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), "name");
					}
					else if (securityInfo == 2)
					{
						ex = ((name == null) ? new FileNotFoundException() : new FileNotFoundException(name));
					}
					else if (securityInfo == 1350)
					{
						ex = new NotSupportedException(Environment.GetResourceString("AccessControl_NoAssociatedSecurity"));
					}
					else
					{
						ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_UnexpectedError", new object[] { securityInfo }));
					}
				}
				throw ex;
			}
			return new CommonSecurityDescriptor(isContainer, false, rawSecurityDescriptor, true);
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x0006E034 File Offset: 0x0006C234
		[SecurityCritical]
		private void Persist(string name, SafeHandle handle, AccessControlSections includeSections, object exceptionContext)
		{
			base.WriteLock();
			try
			{
				SecurityInfos securityInfos = (SecurityInfos)0;
				SecurityIdentifier securityIdentifier = null;
				SecurityIdentifier securityIdentifier2 = null;
				SystemAcl systemAcl = null;
				DiscretionaryAcl discretionaryAcl = null;
				if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None && this._securityDescriptor.Owner != null)
				{
					securityInfos |= SecurityInfos.Owner;
					securityIdentifier = this._securityDescriptor.Owner;
				}
				if ((includeSections & AccessControlSections.Group) != AccessControlSections.None && this._securityDescriptor.Group != null)
				{
					securityInfos |= SecurityInfos.Group;
					securityIdentifier2 = this._securityDescriptor.Group;
				}
				if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
				{
					securityInfos |= SecurityInfos.SystemAcl;
					if (this._securityDescriptor.IsSystemAclPresent && this._securityDescriptor.SystemAcl != null && this._securityDescriptor.SystemAcl.Count > 0)
					{
						systemAcl = this._securityDescriptor.SystemAcl;
					}
					else
					{
						systemAcl = null;
					}
					if ((this._securityDescriptor.ControlFlags & ControlFlags.SystemAclProtected) != ControlFlags.None)
					{
						securityInfos |= (SecurityInfos)this.ProtectedSystemAcl;
					}
					else
					{
						securityInfos |= (SecurityInfos)this.UnprotectedSystemAcl;
					}
				}
				if ((includeSections & AccessControlSections.Access) != AccessControlSections.None && this._securityDescriptor.IsDiscretionaryAclPresent)
				{
					securityInfos |= SecurityInfos.DiscretionaryAcl;
					if (this._securityDescriptor.DiscretionaryAcl.EveryOneFullAccessForNullDacl)
					{
						discretionaryAcl = null;
					}
					else
					{
						discretionaryAcl = this._securityDescriptor.DiscretionaryAcl;
					}
					if ((this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclProtected) != ControlFlags.None)
					{
						securityInfos |= (SecurityInfos)this.ProtectedDiscretionaryAcl;
					}
					else
					{
						securityInfos |= (SecurityInfos)this.UnprotectedDiscretionaryAcl;
					}
				}
				if (securityInfos != (SecurityInfos)0)
				{
					int num = Win32.SetSecurityInfo(this._resourceType, name, handle, securityInfos, securityIdentifier, securityIdentifier2, systemAcl, discretionaryAcl);
					if (num != 0)
					{
						Exception ex = null;
						if (this._exceptionFromErrorCode != null)
						{
							ex = this._exceptionFromErrorCode(num, name, handle, exceptionContext);
						}
						if (ex == null)
						{
							if (num == 5)
							{
								ex = new UnauthorizedAccessException();
							}
							else if (num == 1307)
							{
								ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidOwner"));
							}
							else if (num == 1308)
							{
								ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidGroup"));
							}
							else if (num == 123)
							{
								ex = new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), "name");
							}
							else if (num == 6)
							{
								ex = new NotSupportedException(Environment.GetResourceString("AccessControl_InvalidHandle"));
							}
							else if (num == 2)
							{
								ex = new FileNotFoundException();
							}
							else if (num == 1350)
							{
								ex = new NotSupportedException(Environment.GetResourceString("AccessControl_NoAssociatedSecurity"));
							}
							else
							{
								ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_UnexpectedError", new object[] { num }));
							}
						}
						throw ex;
					}
					base.OwnerModified = false;
					base.GroupModified = false;
					base.AccessRulesModified = false;
					base.AuditRulesModified = false;
				}
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="name">The name of the securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated is either a directory or a file, and that directory or file could not be found.</exception>
		// Token: 0x06001F85 RID: 8069 RVA: 0x0006E2C4 File Offset: 0x0006C4C4
		protected sealed override void Persist(string name, AccessControlSections includeSections)
		{
			this.Persist(name, includeSections, this._exceptionContext);
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="name">The name of the securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated is either a directory or a file, and that directory or file could not be found.</exception>
		// Token: 0x06001F86 RID: 8070 RVA: 0x0006E2D4 File Offset: 0x0006C4D4
		[SecuritySafeCritical]
		protected void Persist(string name, AccessControlSections includeSections, object exceptionContext)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.Persist(name, null, includeSections, exceptionContext);
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object to permanent storage. We recommend.persist that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="handle">The handle of the securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated is either a directory or a file, and that directory or file could not be found.</exception>
		// Token: 0x06001F87 RID: 8071 RVA: 0x0006E2EE File Offset: 0x0006C4EE
		[SecuritySafeCritical]
		protected sealed override void Persist(SafeHandle handle, AccessControlSections includeSections)
		{
			this.Persist(handle, includeSections, this._exceptionContext);
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="handle">The handle of the securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		/// <param name="exceptionContext">An object that contains contextual information about the source or destination of the exception.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The securable object with which this <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated is either a directory or a file, and that directory or file could not be found.</exception>
		// Token: 0x06001F88 RID: 8072 RVA: 0x0006E2FE File Offset: 0x0006C4FE
		[SecuritySafeCritical]
		protected void Persist(SafeHandle handle, AccessControlSections includeSections, object exceptionContext)
		{
			if (handle == null)
			{
				throw new ArgumentNullException("handle");
			}
			this.Persist(null, handle, includeSections, exceptionContext);
		}

		// Token: 0x04000B4F RID: 2895
		private readonly ResourceType _resourceType;

		// Token: 0x04000B50 RID: 2896
		private NativeObjectSecurity.ExceptionFromErrorCode _exceptionFromErrorCode;

		// Token: 0x04000B51 RID: 2897
		private object _exceptionContext;

		// Token: 0x04000B52 RID: 2898
		private readonly uint ProtectedDiscretionaryAcl = 2147483648U;

		// Token: 0x04000B53 RID: 2899
		private readonly uint ProtectedSystemAcl = 1073741824U;

		// Token: 0x04000B54 RID: 2900
		private readonly uint UnprotectedDiscretionaryAcl = 536870912U;

		// Token: 0x04000B55 RID: 2901
		private readonly uint UnprotectedSystemAcl = 268435456U;

		/// <summary>Provides a way for integrators to map numeric error codes to specific exceptions that they create.</summary>
		/// <param name="errorCode">The numeric error code.</param>
		/// <param name="name">The name of the securable object with which the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="handle">The handle of the securable object with which the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> object is associated.</param>
		/// <param name="context">An object that contains contextual information about the source or destination of the exception.</param>
		/// <returns>The <see cref="T:System.Exception" /> this delegate creates.</returns>
		// Token: 0x02000B30 RID: 2864
		// (Invoke) Token: 0x06006B82 RID: 27522
		[SecuritySafeCritical]
		protected internal delegate Exception ExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context);
	}
}
