using System;

namespace System.Net
{
	/// <summary>Provides the base interface for implementation of proxy access for the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x0200014E RID: 334
	[global::__DynamicallyInvokable]
	public interface IWebProxy
	{
		/// <summary>Returns the URI of a proxy.</summary>
		/// <param name="destination">A <see cref="T:System.Uri" /> that specifies the requested Internet resource.</param>
		/// <returns>A <see cref="T:System.Uri" /> instance that contains the URI of the proxy used to contact <paramref name="destination" />.</returns>
		// Token: 0x06000BA2 RID: 2978
		[global::__DynamicallyInvokable]
		Uri GetProxy(Uri destination);

		/// <summary>Indicates that the proxy should not be used for the specified host.</summary>
		/// <param name="host">The <see cref="T:System.Uri" /> of the host to check for proxy use.</param>
		/// <returns>
		///   <see langword="true" /> if the proxy server should not be used for <paramref name="host" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000BA3 RID: 2979
		[global::__DynamicallyInvokable]
		bool IsBypassed(Uri host);

		/// <summary>The credentials to submit to the proxy server for authentication.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance that contains the credentials that are needed to authenticate a request to the proxy server.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000BA4 RID: 2980
		// (set) Token: 0x06000BA5 RID: 2981
		[global::__DynamicallyInvokable]
		ICredentials Credentials
		{
			[global::__DynamicallyInvokable]
			get;
			[global::__DynamicallyInvokable]
			set;
		}
	}
}
