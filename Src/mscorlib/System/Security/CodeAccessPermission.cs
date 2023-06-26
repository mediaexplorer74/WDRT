using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Threading;

namespace System.Security
{
	/// <summary>Defines the underlying structure of all code access permissions.</summary>
	// Token: 0x020001CE RID: 462
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class CodeAccessPermission : IPermission, ISecurityEncodable, IStackWalk
	{
		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame to be removed and no longer in effect.</summary>
		/// <exception cref="T:System.InvalidOperationException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame.</exception>
		// Token: 0x06001C33 RID: 7219 RVA: 0x0006109C File Offset: 0x0005F29C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void RevertAssert()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.RevertAssert(ref stackCrawlMark);
		}

		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.Deny" /> for the current frame to be removed and no longer in effect.</summary>
		/// <exception cref="T:System.InvalidOperationException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Deny" /> for the current frame.</exception>
		// Token: 0x06001C34 RID: 7220 RVA: 0x000610B4 File Offset: 0x0005F2B4
		[SecuritySafeCritical]
		[Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void RevertDeny()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.RevertDeny(ref stackCrawlMark);
		}

		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> for the current frame to be removed and no longer in effect.</summary>
		/// <exception cref="T:System.InvalidOperationException">There is no previous <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> for the current frame.</exception>
		// Token: 0x06001C35 RID: 7221 RVA: 0x000610CC File Offset: 0x0005F2CC
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void RevertPermitOnly()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.RevertPermitOnly(ref stackCrawlMark);
		}

		/// <summary>Causes all previous overrides for the current frame to be removed and no longer in effect.</summary>
		/// <exception cref="T:System.InvalidOperationException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Assert" />, <see cref="M:System.Security.CodeAccessPermission.Deny" />, or <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> for the current frame.</exception>
		// Token: 0x06001C36 RID: 7222 RVA: 0x000610E4 File Offset: 0x0005F2E4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void RevertAll()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.RevertAll(ref stackCrawlMark);
		}

		/// <summary>Forces a <see cref="T:System.Security.SecurityException" /> at run time if all callers higher in the call stack have not been granted the permission specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have the permission specified by the current instance.  
		///  -or-  
		///  A caller higher in the call stack has called <see cref="M:System.Security.CodeAccessPermission.Deny" /> on the current permission object.</exception>
		// Token: 0x06001C37 RID: 7223 RVA: 0x000610FC File Offset: 0x0005F2FC
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Demand()
		{
			if (!this.CheckDemand(null))
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCallersCaller;
				CodeAccessSecurityEngine.Check(this, ref stackCrawlMark);
			}
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0006111C File Offset: 0x0005F31C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void Demand(PermissionType permissionType)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCallersCaller;
			CodeAccessSecurityEngine.SpecialDemand(permissionType, ref stackCrawlMark);
		}

		/// <summary>Declares that the calling code can access the resource protected by a permission demand through the code that calls this method, even if callers higher in the stack have not been granted permission to access the resource. Using <see cref="M:System.Security.CodeAccessPermission.Assert" /> can create security issues.</summary>
		/// <exception cref="T:System.Security.SecurityException">The calling code does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Assertion" />.  
		///  -or-  
		///  There is already an active <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame.</exception>
		// Token: 0x06001C39 RID: 7225 RVA: 0x00061134 File Offset: 0x0005F334
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Assert()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			CodeAccessSecurityEngine.Assert(this, ref stackCrawlMark);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0006114C File Offset: 0x0005F34C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void Assert(bool allPossible)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityRuntime.AssertAllPossible(ref stackCrawlMark);
		}

		/// <summary>Prevents callers higher in the call stack from using the code that calls this method to access the resource specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">There is already an active <see cref="M:System.Security.CodeAccessPermission.Deny" /> for the current frame.</exception>
		// Token: 0x06001C3B RID: 7227 RVA: 0x00061164 File Offset: 0x0005F364
		[SecuritySafeCritical]
		[Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Deny()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			CodeAccessSecurityEngine.Deny(this, ref stackCrawlMark);
		}

		/// <summary>Prevents callers higher in the call stack from using the code that calls this method to access all resources except for the resource specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">There is already an active <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> for the current frame.</exception>
		// Token: 0x06001C3C RID: 7228 RVA: 0x0006117C File Offset: 0x0005F37C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void PermitOnly()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			CodeAccessSecurityEngine.PermitOnly(this, ref stackCrawlMark);
		}

		/// <summary>When overridden in a derived class, creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="other" /> parameter is not <see langword="null" />. This method is only supported at this level when passed <see langword="null" />.</exception>
		// Token: 0x06001C3D RID: 7229 RVA: 0x00061193 File Offset: 0x0005F393
		public virtual IPermission Union(IPermission other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SecurityPermissionUnion"));
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x000611B0 File Offset: 0x0005F3B0
		internal static SecurityElement CreatePermissionElement(IPermission perm, string permname)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			XMLUtil.AddClassAttribute(securityElement, perm.GetType(), permname);
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x000611E8 File Offset: 0x0005F3E8
		internal static void ValidateElement(SecurityElement elem, IPermission perm)
		{
			if (elem == null)
			{
				throw new ArgumentNullException("elem");
			}
			if (!XMLUtil.IsPermissionElement(perm, elem))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotAPermissionElement"));
			}
			string text = elem.Attribute("version");
			if (text != null && !text.Equals("1"))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLBadVersion"));
			}
		}

		/// <summary>When overridden in a derived class, creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06001C40 RID: 7232
		public abstract SecurityElement ToXml();

		/// <summary>When overridden in a derived class, reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="elem">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elem" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="elem" /> parameter does not contain the XML encoding for an instance of the same type as the current instance.  
		///  -or-  
		///  The version number of the <paramref name="elem" /> parameter is not supported.</exception>
		// Token: 0x06001C41 RID: 7233
		public abstract void FromXml(SecurityElement elem);

		/// <summary>Creates and returns a string representation of the current permission object.</summary>
		/// <returns>A string representation of the current permission object.</returns>
		// Token: 0x06001C42 RID: 7234 RVA: 0x00061248 File Offset: 0x0005F448
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00061255 File Offset: 0x0005F455
		internal bool VerifyType(IPermission perm)
		{
			return perm != null && !(perm.GetType() != base.GetType());
		}

		/// <summary>When implemented by a derived class, creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x06001C44 RID: 7236
		public abstract IPermission Copy();

		/// <summary>When implemented by a derived class, creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not an instance of the same class as the current permission.</exception>
		// Token: 0x06001C45 RID: 7237
		public abstract IPermission Intersect(IPermission target);

		/// <summary>When implemented by a derived class, determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06001C46 RID: 7238
		public abstract bool IsSubsetOf(IPermission target);

		/// <summary>Determines whether the specified <see cref="T:System.Security.CodeAccessPermission" /> object is equal to the current <see cref="T:System.Security.CodeAccessPermission" />.</summary>
		/// <param name="obj">The <see cref="T:System.Security.CodeAccessPermission" /> object to compare with the current <see cref="T:System.Security.CodeAccessPermission" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Security.CodeAccessPermission" /> object is equal to the current <see cref="T:System.Security.CodeAccessPermission" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001C47 RID: 7239 RVA: 0x00061270 File Offset: 0x0005F470
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			IPermission permission = obj as IPermission;
			if (obj != null && permission == null)
			{
				return false;
			}
			try
			{
				if (!this.IsSubsetOf(permission))
				{
					return false;
				}
				if (permission != null && !permission.IsSubsetOf(this))
				{
					return false;
				}
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.CodeAccessPermission" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.CodeAccessPermission" /> object.</returns>
		// Token: 0x06001C48 RID: 7240 RVA: 0x000612C4 File Offset: 0x0005F4C4
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000612CC File Offset: 0x0005F4CC
		internal bool CheckDemand(CodeAccessPermission grant)
		{
			return this.IsSubsetOf(grant);
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x000612D5 File Offset: 0x0005F4D5
		internal bool CheckPermitOnly(CodeAccessPermission permitted)
		{
			return this.IsSubsetOf(permitted);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000612E0 File Offset: 0x0005F4E0
		internal bool CheckDeny(CodeAccessPermission denied)
		{
			IPermission permission = this.Intersect(denied);
			return permission == null || permission.IsSubsetOf(null);
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00061301 File Offset: 0x0005F501
		internal bool CheckAssert(CodeAccessPermission asserted)
		{
			return this.IsSubsetOf(asserted);
		}
	}
}
