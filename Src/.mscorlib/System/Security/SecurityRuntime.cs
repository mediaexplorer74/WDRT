using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Security
{
	// Token: 0x020001F4 RID: 500
	internal class SecurityRuntime
	{
		// Token: 0x06001E2C RID: 7724 RVA: 0x0006980B File Offset: 0x00067A0B
		private SecurityRuntime()
		{
		}

		// Token: 0x06001E2D RID: 7725
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern FrameSecurityDescriptor GetSecurityObjectForFrame(ref StackCrawlMark stackMark, bool create);

		// Token: 0x06001E2E RID: 7726 RVA: 0x00069813 File Offset: 0x00067A13
		[SecurityCritical]
		internal static MethodInfo GetMethodInfo(RuntimeMethodHandleInternal rmh)
		{
			if (rmh.IsNullHandle())
			{
				return null;
			}
			PermissionSet.s_fullTrust.Assert();
			return RuntimeType.GetMethodBase(RuntimeMethodHandle.GetDeclaringType(rmh), rmh) as MethodInfo;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x0006983B File Offset: 0x00067A3B
		[SecurityCritical]
		private static bool FrameDescSetHelper(FrameSecurityDescriptor secDesc, PermissionSet demandSet, out PermissionSet alteredDemandSet, RuntimeMethodHandleInternal rmh)
		{
			return secDesc.CheckSetDemand(demandSet, out alteredDemandSet, rmh);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00069846 File Offset: 0x00067A46
		[SecurityCritical]
		private static bool FrameDescHelper(FrameSecurityDescriptor secDesc, IPermission demandIn, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
		{
			return secDesc.CheckDemand((CodeAccessPermission)demandIn, permToken, rmh);
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x00069858 File Offset: 0x00067A58
		[SecurityCritical]
		private static bool CheckDynamicMethodSetHelper(DynamicResolver dynamicResolver, PermissionSet demandSet, out PermissionSet alteredDemandSet, RuntimeMethodHandleInternal rmh)
		{
			CompressedStack securityContext = dynamicResolver.GetSecurityContext();
			bool flag;
			try
			{
				flag = securityContext.CheckSetDemandWithModificationNoHalt(demandSet, out alteredDemandSet, rmh);
			}
			catch (SecurityException ex)
			{
				throw new SecurityException(Environment.GetResourceString("Security_AnonymouslyHostedDynamicMethodCheckFailed"), ex);
			}
			return flag;
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0006989C File Offset: 0x00067A9C
		[SecurityCritical]
		private static bool CheckDynamicMethodHelper(DynamicResolver dynamicResolver, IPermission demandIn, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
		{
			CompressedStack securityContext = dynamicResolver.GetSecurityContext();
			bool flag;
			try
			{
				flag = securityContext.CheckDemandNoHalt((CodeAccessPermission)demandIn, permToken, rmh);
			}
			catch (SecurityException ex)
			{
				throw new SecurityException(Environment.GetResourceString("Security_AnonymouslyHostedDynamicMethodCheckFailed"), ex);
			}
			return flag;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x000698E4 File Offset: 0x00067AE4
		[SecurityCritical]
		internal static void Assert(PermissionSet permSet, ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor frameSecurityDescriptor = CodeAccessSecurityEngine.CheckNReturnSO(CodeAccessSecurityEngine.AssertPermissionToken, CodeAccessSecurityEngine.AssertPermission, ref stackMark, 1);
			if (frameSecurityDescriptor == null)
			{
				Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
				return;
			}
			if (frameSecurityDescriptor.HasImperativeAsserts())
			{
				throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
			}
			frameSecurityDescriptor.SetAssert(permSet);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00069938 File Offset: 0x00067B38
		[SecurityCritical]
		internal static void AssertAllPossible(ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
			if (securityObjectForFrame == null)
			{
				Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
				return;
			}
			if (securityObjectForFrame.GetAssertAllPossible())
			{
				throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
			}
			securityObjectForFrame.SetAssertAllPossible();
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x00069980 File Offset: 0x00067B80
		[SecurityCritical]
		internal static void Deny(PermissionSet permSet, ref StackCrawlMark stackMark)
		{
			if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_CasDeny"));
			}
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
			if (securityObjectForFrame == null)
			{
				Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
				return;
			}
			if (securityObjectForFrame.HasImperativeDenials())
			{
				throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
			}
			securityObjectForFrame.SetDeny(permSet);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x000699E4 File Offset: 0x00067BE4
		[SecurityCritical]
		internal static void PermitOnly(PermissionSet permSet, ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
			if (securityObjectForFrame == null)
			{
				Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
				return;
			}
			if (securityObjectForFrame.HasImperativeRestrictions())
			{
				throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
			}
			securityObjectForFrame.SetPermitOnly(permSet);
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00069A2C File Offset: 0x00067C2C
		[SecurityCritical]
		internal static void RevertAssert(ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, false);
			if (securityObjectForFrame != null)
			{
				securityObjectForFrame.RevertAssert();
				return;
			}
			throw new InvalidOperationException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00069A5C File Offset: 0x00067C5C
		[SecurityCritical]
		internal static void RevertDeny(ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, false);
			if (securityObjectForFrame != null)
			{
				securityObjectForFrame.RevertDeny();
				return;
			}
			throw new InvalidOperationException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00069A8C File Offset: 0x00067C8C
		[SecurityCritical]
		internal static void RevertPermitOnly(ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, false);
			if (securityObjectForFrame != null)
			{
				securityObjectForFrame.RevertPermitOnly();
				return;
			}
			throw new InvalidOperationException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x00069ABC File Offset: 0x00067CBC
		[SecurityCritical]
		internal static void RevertAll(ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, false);
			if (securityObjectForFrame != null)
			{
				securityObjectForFrame.RevertAll();
				return;
			}
			throw new InvalidOperationException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
		}

		// Token: 0x04000A93 RID: 2707
		internal const bool StackContinue = true;

		// Token: 0x04000A94 RID: 2708
		internal const bool StackHalt = false;
	}
}
