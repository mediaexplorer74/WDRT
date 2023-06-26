using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Threading;

namespace System.Security
{
	/// <summary>Provides the main access point for classes interacting with the security system. This class cannot be inherited.</summary>
	// Token: 0x020001F3 RID: 499
	[ComVisible(true)]
	public static class SecurityManager
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x00068EBF File Offset: 0x000670BF
		internal static PolicyManager PolicyManager
		{
			get
			{
				return SecurityManager.polmgr;
			}
		}

		/// <summary>Determines whether a permission is granted to the caller.</summary>
		/// <param name="perm">The permission to test against the grant of the caller.</param>
		/// <returns>
		///   <see langword="true" /> if the permissions granted to the caller include the permission <paramref name="perm" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E11 RID: 7697 RVA: 0x00068EC8 File Offset: 0x000670C8
		[SecuritySafeCritical]
		[Obsolete("IsGranted is obsolete and will be removed in a future release of the .NET Framework.  Please use the PermissionSet property of either AppDomain or Assembly instead.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool IsGranted(IPermission perm)
		{
			if (perm == null)
			{
				return true;
			}
			PermissionSet permissionSet = null;
			PermissionSet permissionSet2 = null;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityManager.GetGrantedPermissions(JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref permissionSet), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref permissionSet2), JitHelpers.GetStackCrawlMarkHandle(ref stackCrawlMark));
			return permissionSet.Contains(perm) && (permissionSet2 == null || !permissionSet2.Contains(perm));
		}

		/// <summary>Gets a permission set that is safe to grant to an application that has the provided evidence.</summary>
		/// <param name="evidence">The host evidence to match to a permission set.</param>
		/// <returns>A permission set that can be used as a grant set for the application that has the provided evidence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="evidence" /> is <see langword="null" />.</exception>
		// Token: 0x06001E12 RID: 7698 RVA: 0x00068F14 File Offset: 0x00067114
		public static PermissionSet GetStandardSandbox(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			Zone hostEvidence = evidence.GetHostEvidence<Zone>();
			if (hostEvidence == null)
			{
				return new PermissionSet(PermissionState.None);
			}
			if (hostEvidence.SecurityZone == SecurityZone.MyComputer)
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			if (hostEvidence.SecurityZone == SecurityZone.Intranet)
			{
				PermissionSet localIntranet = BuiltInPermissionSets.LocalIntranet;
				PolicyStatement policyStatement = new NetCodeGroup(new AllMembershipCondition()).Resolve(evidence);
				PolicyStatement policyStatement2 = new FileCodeGroup(new AllMembershipCondition(), FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery).Resolve(evidence);
				if (policyStatement != null)
				{
					localIntranet.InplaceUnion(policyStatement.PermissionSet);
				}
				if (policyStatement2 != null)
				{
					localIntranet.InplaceUnion(policyStatement2.PermissionSet);
				}
				return localIntranet;
			}
			if (hostEvidence.SecurityZone == SecurityZone.Internet || hostEvidence.SecurityZone == SecurityZone.Trusted)
			{
				PermissionSet internet = BuiltInPermissionSets.Internet;
				PolicyStatement policyStatement3 = new NetCodeGroup(new AllMembershipCondition()).Resolve(evidence);
				if (policyStatement3 != null)
				{
					internet.InplaceUnion(policyStatement3.PermissionSet);
				}
				return internet;
			}
			return new PermissionSet(PermissionState.None);
		}

		/// <summary>Gets the granted zone identity and URL identity permission sets for the current assembly.</summary>
		/// <param name="zone">An output parameter that contains an <see cref="T:System.Collections.ArrayList" /> of granted <see cref="P:System.Security.Permissions.ZoneIdentityPermissionAttribute.Zone" /> objects.</param>
		/// <param name="origin">An output parameter that contains an <see cref="T:System.Collections.ArrayList" /> of granted <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> objects.</param>
		/// <exception cref="T:System.Security.SecurityException">The request for <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> failed.</exception>
		// Token: 0x06001E13 RID: 7699 RVA: 0x00068FE8 File Offset: 0x000671E8
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void GetZoneAndOrigin(out ArrayList zone, out ArrayList origin)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			CodeAccessSecurityEngine.GetZoneAndOrigin(ref stackCrawlMark, out zone, out origin);
		}

		/// <summary>Loads a <see cref="T:System.Security.Policy.PolicyLevel" /> from the specified file.</summary>
		/// <param name="path">The physical file path to a file containing the security policy information.</param>
		/// <param name="type">One of the enumeration values that specifies the type of the policy level to be loaded.</param>
		/// <returns>The loaded policy level.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The file indicated by the <paramref name="path" /> parameter does not exist.</exception>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.  
		///  -or-  
		///  The code that calls this method does not have <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />.  
		///  -or-  
		///  The code that calls this method does not have <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />.  
		///  -or-  
		///  The code that calls this method does not have <see cref="F:System.Security.Permissions.FileIOPermissionAccess.PathDiscovery" />.</exception>
		/// <exception cref="T:System.NotSupportedException">This method uses code access security (CAS) policy, which is obsolete in the .NET Framework 4. To enable CAS policy for compatibility with earlier versions of the .NET Framework, use the &lt;legacyCasPolicy&gt; element.</exception>
		// Token: 0x06001E14 RID: 7700 RVA: 0x00069000 File Offset: 0x00067200
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
		public static PolicyLevel LoadPolicyLevelFromFile(string path, PolicyLevelType type)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
			}
			if (!File.InternalExists(path))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_PolicyFileDoesNotExist"));
			}
			string fullPath = Path.GetFullPath(path);
			FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.None);
			fileIOPermission.AddPathList(FileIOPermissionAccess.Read, fullPath);
			fileIOPermission.AddPathList(FileIOPermissionAccess.Write, fullPath);
			fileIOPermission.Demand();
			PolicyLevel policyLevel;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					policyLevel = SecurityManager.LoadPolicyLevelFromStringHelper(streamReader.ReadToEnd(), path, type);
				}
			}
			return policyLevel;
		}

		/// <summary>Loads a <see cref="T:System.Security.Policy.PolicyLevel" /> from the specified string.</summary>
		/// <param name="str">The XML representation of a security policy level in the same form in which it appears in a configuration file.</param>
		/// <param name="type">One of the enumeration values that specifies the type of the policy level to be loaded.</param>
		/// <returns>The loaded policy level.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="str" /> parameter is not valid.</exception>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.</exception>
		// Token: 0x06001E15 RID: 7701 RVA: 0x000690C8 File Offset: 0x000672C8
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
		public static PolicyLevel LoadPolicyLevelFromString(string str, PolicyLevelType type)
		{
			return SecurityManager.LoadPolicyLevelFromStringHelper(str, null, type);
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000690D4 File Offset: 0x000672D4
		private static PolicyLevel LoadPolicyLevelFromStringHelper(string str, string path, PolicyLevelType type)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			PolicyLevel policyLevel = new PolicyLevel(type, path);
			Parser parser = new Parser(str);
			SecurityElement topElement = parser.GetTopElement();
			if (topElement == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), "configuration"));
			}
			SecurityElement securityElement = topElement.SearchForChildByTag("mscorlib");
			if (securityElement == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), "mscorlib"));
			}
			SecurityElement securityElement2 = securityElement.SearchForChildByTag("security");
			if (securityElement2 == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), "security"));
			}
			SecurityElement securityElement3 = securityElement2.SearchForChildByTag("policy");
			if (securityElement3 == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), "policy"));
			}
			SecurityElement securityElement4 = securityElement3.SearchForChildByTag("PolicyLevel");
			if (securityElement4 != null)
			{
				policyLevel.FromXml(securityElement4);
				return policyLevel;
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), "PolicyLevel"));
		}

		/// <summary>Saves a modified security policy level loaded with <see cref="M:System.Security.SecurityManager.LoadPolicyLevelFromFile(System.String,System.Security.PolicyLevelType)" />.</summary>
		/// <param name="level">The policy level object to be saved.</param>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.</exception>
		/// <exception cref="T:System.NotSupportedException">This method uses code access security (CAS) policy, which is obsolete in the .NET Framework 4. To enable CAS policy for compatibility with earlier versions of the .NET Framework, use the &lt;legacyCasPolicy&gt; element.</exception>
		// Token: 0x06001E17 RID: 7703 RVA: 0x000691F2 File Offset: 0x000673F2
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
		public static void SavePolicyLevel(PolicyLevel level)
		{
			if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
			}
			PolicyManager.EncodeLevel(level);
		}

		/// <summary>Determines what permissions to grant to code based on the specified evidence and requests.</summary>
		/// <param name="evidence">The evidence set used to evaluate policy.</param>
		/// <param name="reqdPset">The required permissions the code needs to run.</param>
		/// <param name="optPset">The optional permissions that will be used if granted, but aren't required for the code to run.</param>
		/// <param name="denyPset">The denied permissions that must never be granted to the code even if policy otherwise permits it.</param>
		/// <param name="denied">An output parameter that contains the set of permissions not granted.</param>
		/// <returns>The set of permissions that would be granted by the security system.</returns>
		/// <exception cref="T:System.NotSupportedException">This method uses code access security (CAS) policy, which is obsolete in the .NET Framework 4. To enable CAS policy for compatibility with earlier versions of the .NET Framework, use the &lt;legacyCasPolicy&gt; element.</exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">Policy fails to grant the minimum required permissions specified by the <paramref name="reqdPset" /> parameter.</exception>
		// Token: 0x06001E18 RID: 7704 RVA: 0x00069216 File Offset: 0x00067416
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static PermissionSet ResolvePolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied)
		{
			if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
			}
			return SecurityManager.ResolvePolicy(evidence, reqdPset, optPset, denyPset, out denied, true);
		}

		/// <summary>Determines what permissions to grant to code based on the specified evidence.</summary>
		/// <param name="evidence">The evidence set used to evaluate policy.</param>
		/// <returns>The set of permissions that can be granted by the security system.</returns>
		/// <exception cref="T:System.NotSupportedException">This method uses code access security (CAS) policy, which is obsolete in the .NET Framework 4. To enable CAS policy for compatibility with earlier versions of the .NET Framework, use the &lt;legacyCasPolicy&gt; element.</exception>
		// Token: 0x06001E19 RID: 7705 RVA: 0x00069240 File Offset: 0x00067440
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static PermissionSet ResolvePolicy(Evidence evidence)
		{
			if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
			}
			if (evidence == null)
			{
				evidence = new Evidence();
			}
			return SecurityManager.polmgr.Resolve(evidence);
		}

		/// <summary>Determines what permissions to grant to code based on the specified evidence.</summary>
		/// <param name="evidences">An array of evidence objects used to evaluate policy.</param>
		/// <returns>The set of permissions that is appropriate for all of the provided evidence.</returns>
		/// <exception cref="T:System.NotSupportedException">This method uses code access security (CAS) policy, which is obsolete in the .NET Framework 4. To enable CAS policy for compatibility with earlier versions of the .NET Framework, use the &lt;legacyCasPolicy&gt; element.</exception>
		// Token: 0x06001E1A RID: 7706 RVA: 0x00069274 File Offset: 0x00067474
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static PermissionSet ResolvePolicy(Evidence[] evidences)
		{
			if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
			}
			if (evidences == null || evidences.Length == 0)
			{
				evidences = new Evidence[1];
			}
			PermissionSet permissionSet = SecurityManager.ResolvePolicy(evidences[0]);
			if (permissionSet == null)
			{
				return null;
			}
			for (int i = 1; i < evidences.Length; i++)
			{
				permissionSet = permissionSet.Intersect(SecurityManager.ResolvePolicy(evidences[i]));
				if (permissionSet == null || permissionSet.IsEmpty())
				{
					return permissionSet;
				}
			}
			return permissionSet;
		}

		/// <summary>Determines whether the current thread requires a security context capture if its security state has to be re-created at a later point in time.</summary>
		/// <returns>
		///   <see langword="false" /> if the stack contains no partially trusted application domains, no partially trusted assemblies, and no currently active <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> or <see cref="M:System.Security.CodeAccessPermission.Deny" /> modifiers; <see langword="true" /> if the common language runtime cannot guarantee that the stack contains none of these.</returns>
		// Token: 0x06001E1B RID: 7707 RVA: 0x000692E5 File Offset: 0x000674E5
		[SecurityCritical]
		public static bool CurrentThreadRequiresSecurityContextCapture()
		{
			return !CodeAccessSecurityEngine.QuickCheckForAllDemands();
		}

		/// <summary>Determines which permissions to grant to code based on the specified evidence, excluding the policy for the <see cref="T:System.AppDomain" /> level.</summary>
		/// <param name="evidence">The evidence set used to evaluate policy.</param>
		/// <returns>The set of permissions that can be granted by the security system.</returns>
		/// <exception cref="T:System.NotSupportedException">This method uses code access security (CAS) policy, which is obsolete in the .NET Framework 4. To enable CAS policy for compatibility with earlier versions of the .NET Framework, use the &lt;legacyCasPolicy&gt; element.</exception>
		// Token: 0x06001E1C RID: 7708 RVA: 0x000692EF File Offset: 0x000674EF
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static PermissionSet ResolveSystemPolicy(Evidence evidence)
		{
			if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
			}
			if (PolicyManager.IsGacAssembly(evidence))
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			return SecurityManager.polmgr.CodeGroupResolve(evidence, true);
		}

		/// <summary>Gets a collection of code groups matching the specified evidence.</summary>
		/// <param name="evidence">The evidence set against which the policy is evaluated.</param>
		/// <returns>An enumeration of the set of code groups matching the evidence.</returns>
		/// <exception cref="T:System.NotSupportedException">This method uses code access security (CAS) policy, which is obsolete in the .NET Framework 4. To enable CAS policy for compatibility with earlier versions of the .NET Framework, use the &lt;legacyCasPolicy&gt; element.</exception>
		// Token: 0x06001E1D RID: 7709 RVA: 0x00069328 File Offset: 0x00067528
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static IEnumerator ResolvePolicyGroups(Evidence evidence)
		{
			if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
			}
			return SecurityManager.polmgr.ResolveCodeGroups(evidence);
		}

		/// <summary>Provides an enumerator to access the security policy hierarchy by levels, such as computer policy and user policy.</summary>
		/// <returns>An enumerator for <see cref="T:System.Security.Policy.PolicyLevel" /> objects that compose the security policy hierarchy.</returns>
		/// <exception cref="T:System.NotSupportedException">This method uses code access security (CAS) policy, which is obsolete in the .NET Framework 4. To enable CAS policy for compatibility with earlier versions of the .NET Framework, use the &lt;legacyCasPolicy&gt; element.</exception>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.</exception>
		// Token: 0x06001E1E RID: 7710 RVA: 0x00069351 File Offset: 0x00067551
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static IEnumerator PolicyHierarchy()
		{
			if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
			}
			return SecurityManager.polmgr.PolicyHierarchy();
		}

		/// <summary>Saves the modified security policy state.</summary>
		/// <exception cref="T:System.NotSupportedException">This method uses code access security (CAS) policy, which is obsolete in the .NET Framework 4. To enable CAS policy for compatibility with earlier versions of the .NET Framework, use the &lt;legacyCasPolicy&gt; element.</exception>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.</exception>
		// Token: 0x06001E1F RID: 7711 RVA: 0x00069379 File Offset: 0x00067579
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
		public static void SavePolicy()
		{
			if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
			}
			SecurityManager.polmgr.Save();
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x000693A4 File Offset: 0x000675A4
		[SecurityCritical]
		private static PermissionSet ResolveCasPolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied, out int securitySpecialFlags, bool checkExecutionPermission)
		{
			CodeAccessPermission.Assert(true);
			PermissionSet permissionSet = SecurityManager.ResolvePolicy(evidence, reqdPset, optPset, denyPset, out denied, checkExecutionPermission);
			securitySpecialFlags = SecurityManager.GetSpecialFlags(permissionSet, denied);
			return permissionSet;
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000693D4 File Offset: 0x000675D4
		[SecurityCritical]
		private static PermissionSet ResolvePolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied, bool checkExecutionPermission)
		{
			if (SecurityManager.executionSecurityPermission == null)
			{
				SecurityManager.executionSecurityPermission = new SecurityPermission(SecurityPermissionFlag.Execution);
			}
			Exception ex = null;
			PermissionSet permissionSet;
			if (reqdPset == null)
			{
				permissionSet = optPset;
			}
			else
			{
				permissionSet = ((optPset == null) ? null : reqdPset.Union(optPset));
			}
			if (permissionSet != null && !permissionSet.IsUnrestricted())
			{
				permissionSet.AddPermission(SecurityManager.executionSecurityPermission);
			}
			if (evidence == null)
			{
				evidence = new Evidence();
			}
			PermissionSet permissionSet2 = SecurityManager.polmgr.Resolve(evidence);
			if (permissionSet != null)
			{
				permissionSet2.InplaceIntersect(permissionSet);
			}
			if (checkExecutionPermission && (!permissionSet2.Contains(SecurityManager.executionSecurityPermission) || (denyPset != null && denyPset.Contains(SecurityManager.executionSecurityPermission))))
			{
				throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"), -2146233320, ex);
			}
			if (reqdPset != null && !reqdPset.IsSubsetOf(permissionSet2))
			{
				throw new PolicyException(Environment.GetResourceString("Policy_NoRequiredPermission"), -2146233321, ex);
			}
			if (denyPset != null)
			{
				denied = denyPset.Copy();
				permissionSet2.MergeDeniedSet(denied);
				if (denied.IsEmpty())
				{
					denied = null;
				}
			}
			else
			{
				denied = null;
			}
			permissionSet2.IgnoreTypeLoadFailures = true;
			return permissionSet2;
		}

		/// <summary>Gets or sets a value indicating whether code must have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> in order to execute.</summary>
		/// <returns>
		///   <see langword="true" /> if code must have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> in order to execute; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.</exception>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x000694D9 File Offset: 0x000676D9
		// (set) Token: 0x06001E23 RID: 7715 RVA: 0x000694DC File Offset: 0x000676DC
		[Obsolete("Because execution permission checks can no longer be turned off, the CheckExecutionRights property no longer has any effect.")]
		public static bool CheckExecutionRights
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value indicating whether security is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if security is enabled; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.</exception>
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x000694DE File Offset: 0x000676DE
		// (set) Token: 0x06001E25 RID: 7717 RVA: 0x000694E1 File Offset: 0x000676E1
		[Obsolete("Because security can no longer be turned off, the SecurityEnabled property no longer has any effect.")]
		public static bool SecurityEnabled
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x000694E4 File Offset: 0x000676E4
		internal static int GetSpecialFlags(PermissionSet grantSet, PermissionSet deniedSet)
		{
			if (grantSet != null && grantSet.IsUnrestricted() && (deniedSet == null || deniedSet.IsEmpty()))
			{
				return -1;
			}
			SecurityPermissionFlag securityPermissionFlag = SecurityPermissionFlag.NoFlags;
			ReflectionPermissionFlag reflectionPermissionFlag = ReflectionPermissionFlag.NoFlags;
			CodeAccessPermission[] array = new CodeAccessPermission[6];
			if (grantSet != null)
			{
				if (grantSet.IsUnrestricted())
				{
					securityPermissionFlag = SecurityPermissionFlag.AllFlags;
					reflectionPermissionFlag = ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess;
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = SecurityManager.s_UnrestrictedSpecialPermissionMap[i];
					}
				}
				else
				{
					SecurityPermission securityPermission = grantSet.GetPermission(6) as SecurityPermission;
					if (securityPermission != null)
					{
						securityPermissionFlag = securityPermission.Flags;
					}
					ReflectionPermission reflectionPermission = grantSet.GetPermission(4) as ReflectionPermission;
					if (reflectionPermission != null)
					{
						reflectionPermissionFlag = reflectionPermission.Flags;
					}
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = grantSet.GetPermission(SecurityManager.s_BuiltInPermissionIndexMap[j][0]) as CodeAccessPermission;
					}
				}
			}
			if (deniedSet != null)
			{
				if (deniedSet.IsUnrestricted())
				{
					securityPermissionFlag = SecurityPermissionFlag.NoFlags;
					reflectionPermissionFlag = ReflectionPermissionFlag.NoFlags;
					for (int k = 0; k < SecurityManager.s_BuiltInPermissionIndexMap.Length; k++)
					{
						array[k] = null;
					}
				}
				else
				{
					SecurityPermission securityPermission = deniedSet.GetPermission(6) as SecurityPermission;
					if (securityPermission != null)
					{
						securityPermissionFlag &= ~securityPermission.Flags;
					}
					ReflectionPermission reflectionPermission = deniedSet.GetPermission(4) as ReflectionPermission;
					if (reflectionPermission != null)
					{
						reflectionPermissionFlag &= ~reflectionPermission.Flags;
					}
					for (int l = 0; l < SecurityManager.s_BuiltInPermissionIndexMap.Length; l++)
					{
						CodeAccessPermission codeAccessPermission = deniedSet.GetPermission(SecurityManager.s_BuiltInPermissionIndexMap[l][0]) as CodeAccessPermission;
						if (codeAccessPermission != null && !codeAccessPermission.IsSubsetOf(null))
						{
							array[l] = null;
						}
					}
				}
			}
			int num = SecurityManager.MapToSpecialFlags(securityPermissionFlag, reflectionPermissionFlag);
			if (num != -1)
			{
				for (int m = 0; m < array.Length; m++)
				{
					if (array[m] != null && ((IUnrestrictedPermission)array[m]).IsUnrestricted())
					{
						num |= 1 << SecurityManager.s_BuiltInPermissionIndexMap[m][1];
					}
				}
			}
			return num;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000696A4 File Offset: 0x000678A4
		private static int MapToSpecialFlags(SecurityPermissionFlag securityPermissionFlags, ReflectionPermissionFlag reflectionPermissionFlags)
		{
			int num = 0;
			if ((securityPermissionFlags & SecurityPermissionFlag.UnmanagedCode) == SecurityPermissionFlag.UnmanagedCode)
			{
				num |= 1;
			}
			if ((securityPermissionFlags & SecurityPermissionFlag.SkipVerification) == SecurityPermissionFlag.SkipVerification)
			{
				num |= 2;
			}
			if ((securityPermissionFlags & SecurityPermissionFlag.Assertion) == SecurityPermissionFlag.Assertion)
			{
				num |= 8;
			}
			if ((securityPermissionFlags & SecurityPermissionFlag.SerializationFormatter) == SecurityPermissionFlag.SerializationFormatter)
			{
				num |= 32;
			}
			if ((securityPermissionFlags & SecurityPermissionFlag.BindingRedirects) == SecurityPermissionFlag.BindingRedirects)
			{
				num |= 256;
			}
			if ((securityPermissionFlags & SecurityPermissionFlag.ControlEvidence) == SecurityPermissionFlag.ControlEvidence)
			{
				num |= 65536;
			}
			if ((securityPermissionFlags & SecurityPermissionFlag.ControlPrincipal) == SecurityPermissionFlag.ControlPrincipal)
			{
				num |= 131072;
			}
			if ((reflectionPermissionFlags & ReflectionPermissionFlag.RestrictedMemberAccess) == ReflectionPermissionFlag.RestrictedMemberAccess)
			{
				num |= 64;
			}
			if ((reflectionPermissionFlags & ReflectionPermissionFlag.MemberAccess) == ReflectionPermissionFlag.MemberAccess)
			{
				num |= 16;
			}
			return num;
		}

		// Token: 0x06001E28 RID: 7720
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool IsSameType(string strLeft, string strRight);

		// Token: 0x06001E29 RID: 7721
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _SetThreadSecurity(bool bThreadSecurity);

		// Token: 0x06001E2A RID: 7722
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void GetGrantedPermissions(ObjectHandleOnStack retGranted, ObjectHandleOnStack retDenied, StackCrawlMarkHandle stackMark);

		// Token: 0x04000A8F RID: 2703
		private static volatile SecurityPermission executionSecurityPermission = null;

		// Token: 0x04000A90 RID: 2704
		private static PolicyManager polmgr = new PolicyManager();

		// Token: 0x04000A91 RID: 2705
		private static int[][] s_BuiltInPermissionIndexMap = new int[][]
		{
			new int[] { 0, 10 },
			new int[] { 1, 11 },
			new int[] { 2, 12 },
			new int[] { 4, 13 },
			new int[] { 6, 14 },
			new int[] { 7, 9 }
		};

		// Token: 0x04000A92 RID: 2706
		private static CodeAccessPermission[] s_UnrestrictedSpecialPermissionMap = new CodeAccessPermission[]
		{
			new EnvironmentPermission(PermissionState.Unrestricted),
			new FileDialogPermission(PermissionState.Unrestricted),
			new FileIOPermission(PermissionState.Unrestricted),
			new ReflectionPermission(PermissionState.Unrestricted),
			new SecurityPermission(PermissionState.Unrestricted),
			new UIPermission(PermissionState.Unrestricted)
		};
	}
}
