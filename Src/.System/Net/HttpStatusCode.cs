using System;

namespace System.Net
{
	/// <summary>Contains the values of status codes defined for HTTP.</summary>
	// Token: 0x02000104 RID: 260
	[global::__DynamicallyInvokable]
	public enum HttpStatusCode
	{
		/// <summary>Equivalent to HTTP status 100. <see cref="F:System.Net.HttpStatusCode.Continue" /> indicates that the client can continue with its request.</summary>
		// Token: 0x04000E91 RID: 3729
		[global::__DynamicallyInvokable]
		Continue = 100,
		/// <summary>Equivalent to HTTP status 101. <see cref="F:System.Net.HttpStatusCode.SwitchingProtocols" /> indicates that the protocol version or protocol is being changed.</summary>
		// Token: 0x04000E92 RID: 3730
		[global::__DynamicallyInvokable]
		SwitchingProtocols,
		/// <summary>Equivalent to HTTP status 200. <see cref="F:System.Net.HttpStatusCode.OK" /> indicates that the request succeeded and that the requested information is in the response. This is the most common status code to receive.</summary>
		// Token: 0x04000E93 RID: 3731
		[global::__DynamicallyInvokable]
		OK = 200,
		/// <summary>Equivalent to HTTP status 201. <see cref="F:System.Net.HttpStatusCode.Created" /> indicates that the request resulted in a new resource created before the response was sent.</summary>
		// Token: 0x04000E94 RID: 3732
		[global::__DynamicallyInvokable]
		Created,
		/// <summary>Equivalent to HTTP status 202. <see cref="F:System.Net.HttpStatusCode.Accepted" /> indicates that the request has been accepted for further processing.</summary>
		// Token: 0x04000E95 RID: 3733
		[global::__DynamicallyInvokable]
		Accepted,
		/// <summary>Equivalent to HTTP status 203. <see cref="F:System.Net.HttpStatusCode.NonAuthoritativeInformation" /> indicates that the returned metainformation is from a cached copy instead of the origin server and therefore may be incorrect.</summary>
		// Token: 0x04000E96 RID: 3734
		[global::__DynamicallyInvokable]
		NonAuthoritativeInformation,
		/// <summary>Equivalent to HTTP status 204. <see cref="F:System.Net.HttpStatusCode.NoContent" /> indicates that the request has been successfully processed and that the response is intentionally blank.</summary>
		// Token: 0x04000E97 RID: 3735
		[global::__DynamicallyInvokable]
		NoContent,
		/// <summary>Equivalent to HTTP status 205. <see cref="F:System.Net.HttpStatusCode.ResetContent" /> indicates that the client should reset (not reload) the current resource.</summary>
		// Token: 0x04000E98 RID: 3736
		[global::__DynamicallyInvokable]
		ResetContent,
		/// <summary>Equivalent to HTTP status 206. <see cref="F:System.Net.HttpStatusCode.PartialContent" /> indicates that the response is a partial response as requested by a GET request that includes a byte range.</summary>
		// Token: 0x04000E99 RID: 3737
		[global::__DynamicallyInvokable]
		PartialContent,
		/// <summary>Equivalent to HTTP status 300. <see cref="F:System.Net.HttpStatusCode.MultipleChoices" /> indicates that the requested information has multiple representations. The default action is to treat this status as a redirect and follow the contents of the Location header associated with this response.</summary>
		// Token: 0x04000E9A RID: 3738
		[global::__DynamicallyInvokable]
		MultipleChoices = 300,
		/// <summary>Equivalent to HTTP status 300. <see cref="F:System.Net.HttpStatusCode.Ambiguous" /> indicates that the requested information has multiple representations. The default action is to treat this status as a redirect and follow the contents of the Location header associated with this response.</summary>
		// Token: 0x04000E9B RID: 3739
		[global::__DynamicallyInvokable]
		Ambiguous = 300,
		/// <summary>Equivalent to HTTP status 301. <see cref="F:System.Net.HttpStatusCode.MovedPermanently" /> indicates that the requested information has been moved to the URI specified in the Location header. The default action when this status is received is to follow the Location header associated with the response.</summary>
		// Token: 0x04000E9C RID: 3740
		[global::__DynamicallyInvokable]
		MovedPermanently,
		/// <summary>Equivalent to HTTP status 301. <see cref="F:System.Net.HttpStatusCode.Moved" /> indicates that the requested information has been moved to the URI specified in the Location header. The default action when this status is received is to follow the Location header associated with the response. When the original request method was POST, the redirected request will use the GET method.</summary>
		// Token: 0x04000E9D RID: 3741
		[global::__DynamicallyInvokable]
		Moved = 301,
		/// <summary>Equivalent to HTTP status 302. <see cref="F:System.Net.HttpStatusCode.Found" /> indicates that the requested information is located at the URI specified in the Location header. The default action when this status is received is to follow the Location header associated with the response. When the original request method was POST, the redirected request will use the GET method.</summary>
		// Token: 0x04000E9E RID: 3742
		[global::__DynamicallyInvokable]
		Found,
		/// <summary>Equivalent to HTTP status 302. <see cref="F:System.Net.HttpStatusCode.Redirect" /> indicates that the requested information is located at the URI specified in the Location header. The default action when this status is received is to follow the Location header associated with the response. When the original request method was POST, the redirected request will use the GET method.</summary>
		// Token: 0x04000E9F RID: 3743
		[global::__DynamicallyInvokable]
		Redirect = 302,
		/// <summary>Equivalent to HTTP status 303. <see cref="F:System.Net.HttpStatusCode.SeeOther" /> automatically redirects the client to the URI specified in the Location header as the result of a POST. The request to the resource specified by the Location header will be made with a GET.</summary>
		// Token: 0x04000EA0 RID: 3744
		[global::__DynamicallyInvokable]
		SeeOther,
		/// <summary>Equivalent to HTTP status 303. <see cref="F:System.Net.HttpStatusCode.RedirectMethod" /> automatically redirects the client to the URI specified in the Location header as the result of a POST. The request to the resource specified by the Location header will be made with a GET.</summary>
		// Token: 0x04000EA1 RID: 3745
		[global::__DynamicallyInvokable]
		RedirectMethod = 303,
		/// <summary>Equivalent to HTTP status 304. <see cref="F:System.Net.HttpStatusCode.NotModified" /> indicates that the client's cached copy is up to date. The contents of the resource are not transferred.</summary>
		// Token: 0x04000EA2 RID: 3746
		[global::__DynamicallyInvokable]
		NotModified,
		/// <summary>Equivalent to HTTP status 305. <see cref="F:System.Net.HttpStatusCode.UseProxy" /> indicates that the request should use the proxy server at the URI specified in the Location header.</summary>
		// Token: 0x04000EA3 RID: 3747
		[global::__DynamicallyInvokable]
		UseProxy,
		/// <summary>Equivalent to HTTP status 306. <see cref="F:System.Net.HttpStatusCode.Unused" /> is a proposed extension to the HTTP/1.1 specification that is not fully specified.</summary>
		// Token: 0x04000EA4 RID: 3748
		[global::__DynamicallyInvokable]
		Unused,
		/// <summary>Equivalent to HTTP status 307. <see cref="F:System.Net.HttpStatusCode.TemporaryRedirect" /> indicates that the request information is located at the URI specified in the Location header. The default action when this status is received is to follow the Location header associated with the response. When the original request method was POST, the redirected request will also use the POST method.</summary>
		// Token: 0x04000EA5 RID: 3749
		[global::__DynamicallyInvokable]
		TemporaryRedirect,
		/// <summary>Equivalent to HTTP status 307. <see cref="F:System.Net.HttpStatusCode.RedirectKeepVerb" /> indicates that the request information is located at the URI specified in the Location header. The default action when this status is received is to follow the Location header associated with the response. When the original request method was POST, the redirected request will also use the POST method.</summary>
		// Token: 0x04000EA6 RID: 3750
		[global::__DynamicallyInvokable]
		RedirectKeepVerb = 307,
		/// <summary>Equivalent to HTTP status 400. <see cref="F:System.Net.HttpStatusCode.BadRequest" /> indicates that the request could not be understood by the server. <see cref="F:System.Net.HttpStatusCode.BadRequest" /> is sent when no other error is applicable, or if the exact error is unknown or does not have its own error code.</summary>
		// Token: 0x04000EA7 RID: 3751
		[global::__DynamicallyInvokable]
		BadRequest = 400,
		/// <summary>Equivalent to HTTP status 401. <see cref="F:System.Net.HttpStatusCode.Unauthorized" /> indicates that the requested resource requires authentication. The WWW-Authenticate header contains the details of how to perform the authentication.</summary>
		// Token: 0x04000EA8 RID: 3752
		[global::__DynamicallyInvokable]
		Unauthorized,
		/// <summary>Equivalent to HTTP status 402. <see cref="F:System.Net.HttpStatusCode.PaymentRequired" /> is reserved for future use.</summary>
		// Token: 0x04000EA9 RID: 3753
		[global::__DynamicallyInvokable]
		PaymentRequired,
		/// <summary>Equivalent to HTTP status 403. <see cref="F:System.Net.HttpStatusCode.Forbidden" /> indicates that the server refuses to fulfill the request.</summary>
		// Token: 0x04000EAA RID: 3754
		[global::__DynamicallyInvokable]
		Forbidden,
		/// <summary>Equivalent to HTTP status 404. <see cref="F:System.Net.HttpStatusCode.NotFound" /> indicates that the requested resource does not exist on the server.</summary>
		// Token: 0x04000EAB RID: 3755
		[global::__DynamicallyInvokable]
		NotFound,
		/// <summary>Equivalent to HTTP status 405. <see cref="F:System.Net.HttpStatusCode.MethodNotAllowed" /> indicates that the request method (POST or GET) is not allowed on the requested resource.</summary>
		// Token: 0x04000EAC RID: 3756
		[global::__DynamicallyInvokable]
		MethodNotAllowed,
		/// <summary>Equivalent to HTTP status 406. <see cref="F:System.Net.HttpStatusCode.NotAcceptable" /> indicates that the client has indicated with Accept headers that it will not accept any of the available representations of the resource.</summary>
		// Token: 0x04000EAD RID: 3757
		[global::__DynamicallyInvokable]
		NotAcceptable,
		/// <summary>Equivalent to HTTP status 407. <see cref="F:System.Net.HttpStatusCode.ProxyAuthenticationRequired" /> indicates that the requested proxy requires authentication. The Proxy-authenticate header contains the details of how to perform the authentication.</summary>
		// Token: 0x04000EAE RID: 3758
		[global::__DynamicallyInvokable]
		ProxyAuthenticationRequired,
		/// <summary>Equivalent to HTTP status 408. <see cref="F:System.Net.HttpStatusCode.RequestTimeout" /> indicates that the client did not send a request within the time the server was expecting the request.</summary>
		// Token: 0x04000EAF RID: 3759
		[global::__DynamicallyInvokable]
		RequestTimeout,
		/// <summary>Equivalent to HTTP status 409. <see cref="F:System.Net.HttpStatusCode.Conflict" /> indicates that the request could not be carried out because of a conflict on the server.</summary>
		// Token: 0x04000EB0 RID: 3760
		[global::__DynamicallyInvokable]
		Conflict,
		/// <summary>Equivalent to HTTP status 410. <see cref="F:System.Net.HttpStatusCode.Gone" /> indicates that the requested resource is no longer available.</summary>
		// Token: 0x04000EB1 RID: 3761
		[global::__DynamicallyInvokable]
		Gone,
		/// <summary>Equivalent to HTTP status 411. <see cref="F:System.Net.HttpStatusCode.LengthRequired" /> indicates that the required Content-length header is missing.</summary>
		// Token: 0x04000EB2 RID: 3762
		[global::__DynamicallyInvokable]
		LengthRequired,
		/// <summary>Equivalent to HTTP status 412. <see cref="F:System.Net.HttpStatusCode.PreconditionFailed" /> indicates that a condition set for this request failed, and the request cannot be carried out. Conditions are set with conditional request headers like If-Match, If-None-Match, or If-Unmodified-Since.</summary>
		// Token: 0x04000EB3 RID: 3763
		[global::__DynamicallyInvokable]
		PreconditionFailed,
		/// <summary>Equivalent to HTTP status 413. <see cref="F:System.Net.HttpStatusCode.RequestEntityTooLarge" /> indicates that the request is too large for the server to process.</summary>
		// Token: 0x04000EB4 RID: 3764
		[global::__DynamicallyInvokable]
		RequestEntityTooLarge,
		/// <summary>Equivalent to HTTP status 414. <see cref="F:System.Net.HttpStatusCode.RequestUriTooLong" /> indicates that the URI is too long.</summary>
		// Token: 0x04000EB5 RID: 3765
		[global::__DynamicallyInvokable]
		RequestUriTooLong,
		/// <summary>Equivalent to HTTP status 415. <see cref="F:System.Net.HttpStatusCode.UnsupportedMediaType" /> indicates that the request is an unsupported type.</summary>
		// Token: 0x04000EB6 RID: 3766
		[global::__DynamicallyInvokable]
		UnsupportedMediaType,
		/// <summary>Equivalent to HTTP status 416. <see cref="F:System.Net.HttpStatusCode.RequestedRangeNotSatisfiable" /> indicates that the range of data requested from the resource cannot be returned, either because the beginning of the range is before the beginning of the resource, or the end of the range is after the end of the resource.</summary>
		// Token: 0x04000EB7 RID: 3767
		[global::__DynamicallyInvokable]
		RequestedRangeNotSatisfiable,
		/// <summary>Equivalent to HTTP status 417. <see cref="F:System.Net.HttpStatusCode.ExpectationFailed" /> indicates that an expectation given in an Expect header could not be met by the server.</summary>
		// Token: 0x04000EB8 RID: 3768
		[global::__DynamicallyInvokable]
		ExpectationFailed,
		/// <summary>Equivalent to HTTP status 426. <see cref="F:System.Net.HttpStatusCode.UpgradeRequired" /> indicates that the client should switch to a different protocol such as TLS/1.0.</summary>
		// Token: 0x04000EB9 RID: 3769
		[global::__DynamicallyInvokable]
		UpgradeRequired = 426,
		/// <summary>Equivalent to HTTP status 500. <see cref="F:System.Net.HttpStatusCode.InternalServerError" /> indicates that a generic error has occurred on the server.</summary>
		// Token: 0x04000EBA RID: 3770
		[global::__DynamicallyInvokable]
		InternalServerError = 500,
		/// <summary>Equivalent to HTTP status 501. <see cref="F:System.Net.HttpStatusCode.NotImplemented" /> indicates that the server does not support the requested function.</summary>
		// Token: 0x04000EBB RID: 3771
		[global::__DynamicallyInvokable]
		NotImplemented,
		/// <summary>Equivalent to HTTP status 502. <see cref="F:System.Net.HttpStatusCode.BadGateway" /> indicates that an intermediate proxy server received a bad response from another proxy or the origin server.</summary>
		// Token: 0x04000EBC RID: 3772
		[global::__DynamicallyInvokable]
		BadGateway,
		/// <summary>Equivalent to HTTP status 503. <see cref="F:System.Net.HttpStatusCode.ServiceUnavailable" /> indicates that the server is temporarily unavailable, usually due to high load or maintenance.</summary>
		// Token: 0x04000EBD RID: 3773
		[global::__DynamicallyInvokable]
		ServiceUnavailable,
		/// <summary>Equivalent to HTTP status 504. <see cref="F:System.Net.HttpStatusCode.GatewayTimeout" /> indicates that an intermediate proxy server timed out while waiting for a response from another proxy or the origin server.</summary>
		// Token: 0x04000EBE RID: 3774
		[global::__DynamicallyInvokable]
		GatewayTimeout,
		/// <summary>Equivalent to HTTP status 505. <see cref="F:System.Net.HttpStatusCode.HttpVersionNotSupported" /> indicates that the requested HTTP version is not supported by the server.</summary>
		// Token: 0x04000EBF RID: 3775
		[global::__DynamicallyInvokable]
		HttpVersionNotSupported
	}
}
