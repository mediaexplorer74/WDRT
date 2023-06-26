using System;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x0200011B RID: 283
	internal static class ExceptionHelper
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0003D894 File Offset: 0x0003BA94
		internal static NotImplementedException MethodNotImplementedException
		{
			get
			{
				return new NotImplementedException(SR.GetString("net_MethodNotImplementedException"));
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0003D8A5 File Offset: 0x0003BAA5
		internal static NotImplementedException PropertyNotImplementedException
		{
			get
			{
				return new NotImplementedException(SR.GetString("net_PropertyNotImplementedException"));
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0003D8B6 File Offset: 0x0003BAB6
		internal static NotSupportedException MethodNotSupportedException
		{
			get
			{
				return new NotSupportedException(SR.GetString("net_MethodNotSupportedException"));
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0003D8C7 File Offset: 0x0003BAC7
		internal static NotSupportedException PropertyNotSupportedException
		{
			get
			{
				return new NotSupportedException(SR.GetString("net_PropertyNotSupportedException"));
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0003D8D8 File Offset: 0x0003BAD8
		internal static WebException IsolatedException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.KeepAliveFailure), WebExceptionStatus.KeepAliveFailure, WebExceptionInternalStatus.Isolated, null);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0003D8EF File Offset: 0x0003BAEF
		internal static WebException RequestAbortedException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0003D902 File Offset: 0x0003BB02
		internal static WebException CacheEntryNotFoundException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.CacheEntryNotFound), WebExceptionStatus.CacheEntryNotFound);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0003D917 File Offset: 0x0003BB17
		internal static WebException RequestProhibitedByCachePolicyException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestProhibitedByCachePolicy), WebExceptionStatus.RequestProhibitedByCachePolicy);
			}
		}

		// Token: 0x04000F73 RID: 3955
		internal static readonly KeyContainerPermission KeyContainerPermissionOpen = new KeyContainerPermission(KeyContainerPermissionFlags.Open);

		// Token: 0x04000F74 RID: 3956
		internal static readonly WebPermission WebPermissionUnrestricted = new WebPermission(NetworkAccess.Connect);

		// Token: 0x04000F75 RID: 3957
		internal static readonly SecurityPermission UnmanagedPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);

		// Token: 0x04000F76 RID: 3958
		internal static readonly SocketPermission UnrestrictedSocketPermission = new SocketPermission(PermissionState.Unrestricted);

		// Token: 0x04000F77 RID: 3959
		internal static readonly SecurityPermission InfrastructurePermission = new SecurityPermission(SecurityPermissionFlag.Infrastructure);

		// Token: 0x04000F78 RID: 3960
		internal static readonly SecurityPermission ControlPolicyPermission = new SecurityPermission(SecurityPermissionFlag.ControlPolicy);

		// Token: 0x04000F79 RID: 3961
		internal static readonly SecurityPermission ControlPrincipalPermission = new SecurityPermission(SecurityPermissionFlag.ControlPrincipal);
	}
}
