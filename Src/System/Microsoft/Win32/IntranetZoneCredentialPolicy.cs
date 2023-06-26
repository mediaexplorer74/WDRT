using System;
using System.Net;

namespace Microsoft.Win32
{
	/// <summary>Defines a credential policy to be used for resource requests that are made using <see cref="T:System.Net.WebRequest" /> and its derived classes.</summary>
	// Token: 0x02000029 RID: 41
	public class IntranetZoneCredentialPolicy : ICredentialPolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.IntranetZoneCredentialPolicy" /> class.</summary>
		// Token: 0x0600027D RID: 637 RVA: 0x0000F3D2 File Offset: 0x0000D5D2
		public IntranetZoneCredentialPolicy()
		{
			ExceptionHelper.ControlPolicyPermission.Demand();
			this._ManagerRef = (IInternetSecurityManager)new InternetSecurityManager();
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether the client's credentials are sent with a request for a resource that was made using <see cref="T:System.Net.WebRequest" />.</summary>
		/// <param name="challengeUri">The <see cref="T:System.Uri" /> that will receive the request.</param>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> that represents the resource being requested.</param>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that will be sent with the request if this method returns <see langword="true" />.</param>
		/// <param name="authModule">The <see cref="T:System.Net.IAuthenticationModule" /> that will conduct the authentication, if authentication is required.</param>
		/// <returns>
		///   <see langword="true" /> if the requested resource is in the same domain as the client making the request; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600027E RID: 638 RVA: 0x0000F3F4 File Offset: 0x0000D5F4
		public virtual bool ShouldSendCredential(Uri challengeUri, WebRequest request, NetworkCredential credential, IAuthenticationModule authModule)
		{
			int num;
			this._ManagerRef.MapUrlToZone(challengeUri.AbsoluteUri, out num, 0);
			return num == 1;
		}

		// Token: 0x0400038E RID: 910
		private const int URLZONE_INTRANET = 1;

		// Token: 0x0400038F RID: 911
		private IInternetSecurityManager _ManagerRef;
	}
}
