using System;

namespace System.Net
{
	/// <summary>The HTTP headers that may be specified in a client request.</summary>
	// Token: 0x02000102 RID: 258
	[global::__DynamicallyInvokable]
	public enum HttpRequestHeader
	{
		/// <summary>The Cache-Control header, which specifies directives that must be obeyed by all cache control mechanisms along the request/response chain.</summary>
		// Token: 0x04000E48 RID: 3656
		[global::__DynamicallyInvokable]
		CacheControl,
		/// <summary>The Connection header, which specifies options that are desired for a particular connection.</summary>
		// Token: 0x04000E49 RID: 3657
		[global::__DynamicallyInvokable]
		Connection,
		/// <summary>The Date header, which specifies the date and time at which the request originated.</summary>
		// Token: 0x04000E4A RID: 3658
		[global::__DynamicallyInvokable]
		Date,
		/// <summary>The Keep-Alive header, which specifies a parameter used into order to maintain a persistent connection.</summary>
		// Token: 0x04000E4B RID: 3659
		[global::__DynamicallyInvokable]
		KeepAlive,
		/// <summary>The Pragma header, which specifies implementation-specific directives that might apply to any agent along the request/response chain.</summary>
		// Token: 0x04000E4C RID: 3660
		[global::__DynamicallyInvokable]
		Pragma,
		/// <summary>The Trailer header, which specifies the header fields present in the trailer of a message encoded with chunked transfer-coding.</summary>
		// Token: 0x04000E4D RID: 3661
		[global::__DynamicallyInvokable]
		Trailer,
		/// <summary>The Transfer-Encoding header, which specifies what (if any) type of transformation that has been applied to the message body.</summary>
		// Token: 0x04000E4E RID: 3662
		[global::__DynamicallyInvokable]
		TransferEncoding,
		/// <summary>The Upgrade header, which specifies additional communications protocols that the client supports.</summary>
		// Token: 0x04000E4F RID: 3663
		[global::__DynamicallyInvokable]
		Upgrade,
		/// <summary>The Via header, which specifies intermediate protocols to be used by gateway and proxy agents.</summary>
		// Token: 0x04000E50 RID: 3664
		[global::__DynamicallyInvokable]
		Via,
		/// <summary>The Warning header, which specifies additional information about that status or transformation of a message that might not be reflected in the message.</summary>
		// Token: 0x04000E51 RID: 3665
		[global::__DynamicallyInvokable]
		Warning,
		/// <summary>The Allow header, which specifies the set of HTTP methods supported.</summary>
		// Token: 0x04000E52 RID: 3666
		[global::__DynamicallyInvokable]
		Allow,
		/// <summary>The Content-Length header, which specifies the length, in bytes, of the accompanying body data.</summary>
		// Token: 0x04000E53 RID: 3667
		[global::__DynamicallyInvokable]
		ContentLength,
		/// <summary>The Content-Type header, which specifies the MIME type of the accompanying body data.</summary>
		// Token: 0x04000E54 RID: 3668
		[global::__DynamicallyInvokable]
		ContentType,
		/// <summary>The Content-Encoding header, which specifies the encodings that have been applied to the accompanying body data.</summary>
		// Token: 0x04000E55 RID: 3669
		[global::__DynamicallyInvokable]
		ContentEncoding,
		/// <summary>The Content-Langauge header, which specifies the natural language(s) of the accompanying body data.</summary>
		// Token: 0x04000E56 RID: 3670
		[global::__DynamicallyInvokable]
		ContentLanguage,
		/// <summary>The Content-Location header, which specifies a URI from which the accompanying body may be obtained.</summary>
		// Token: 0x04000E57 RID: 3671
		[global::__DynamicallyInvokable]
		ContentLocation,
		/// <summary>The Content-MD5 header, which specifies the MD5 digest of the accompanying body data, for the purpose of providing an end-to-end message integrity check.</summary>
		// Token: 0x04000E58 RID: 3672
		[global::__DynamicallyInvokable]
		ContentMd5,
		/// <summary>The Content-Range header, which specifies where in the full body the accompanying partial body data should be applied.</summary>
		// Token: 0x04000E59 RID: 3673
		[global::__DynamicallyInvokable]
		ContentRange,
		/// <summary>The Expires header, which specifies the date and time after which the accompanying body data should be considered stale.</summary>
		// Token: 0x04000E5A RID: 3674
		[global::__DynamicallyInvokable]
		Expires,
		/// <summary>The Last-Modified header, which specifies the date and time at which the accompanying body data was last modified.</summary>
		// Token: 0x04000E5B RID: 3675
		[global::__DynamicallyInvokable]
		LastModified,
		/// <summary>The Accept header, which specifies the MIME types that are acceptable for the response.</summary>
		// Token: 0x04000E5C RID: 3676
		[global::__DynamicallyInvokable]
		Accept,
		/// <summary>The Accept-Charset header, which specifies the character sets that are acceptable for the response.</summary>
		// Token: 0x04000E5D RID: 3677
		[global::__DynamicallyInvokable]
		AcceptCharset,
		/// <summary>The Accept-Encoding header, which specifies the content encodings that are acceptable for the response.</summary>
		// Token: 0x04000E5E RID: 3678
		[global::__DynamicallyInvokable]
		AcceptEncoding,
		/// <summary>The Accept-Langauge header, which specifies that natural languages that are preferred for the response.</summary>
		// Token: 0x04000E5F RID: 3679
		[global::__DynamicallyInvokable]
		AcceptLanguage,
		/// <summary>The Authorization header, which specifies the credentials that the client presents in order to authenticate itself to the server.</summary>
		// Token: 0x04000E60 RID: 3680
		[global::__DynamicallyInvokable]
		Authorization,
		/// <summary>The Cookie header, which specifies cookie data presented to the server.</summary>
		// Token: 0x04000E61 RID: 3681
		[global::__DynamicallyInvokable]
		Cookie,
		/// <summary>The Expect header, which specifies particular server behaviors that are required by the client.</summary>
		// Token: 0x04000E62 RID: 3682
		[global::__DynamicallyInvokable]
		Expect,
		/// <summary>The From header, which specifies an Internet Email address for the human user who controls the requesting user agent.</summary>
		// Token: 0x04000E63 RID: 3683
		[global::__DynamicallyInvokable]
		From,
		/// <summary>The Host header, which specifies the host name and port number of the resource being requested.</summary>
		// Token: 0x04000E64 RID: 3684
		[global::__DynamicallyInvokable]
		Host,
		/// <summary>The If-Match header, which specifies that the requested operation should be performed only if the client's cached copy of the indicated resource is current.</summary>
		// Token: 0x04000E65 RID: 3685
		[global::__DynamicallyInvokable]
		IfMatch,
		/// <summary>The If-Modified-Since header, which specifies that the requested operation should be performed only if the requested resource has been modified since the indicated data and time.</summary>
		// Token: 0x04000E66 RID: 3686
		[global::__DynamicallyInvokable]
		IfModifiedSince,
		/// <summary>The If-None-Match header, which specifies that the requested operation should be performed only if none of client's cached copies of the indicated resources are current.</summary>
		// Token: 0x04000E67 RID: 3687
		[global::__DynamicallyInvokable]
		IfNoneMatch,
		/// <summary>The If-Range header, which specifies that only the specified range of the requested resource should be sent, if the client's cached copy is current.</summary>
		// Token: 0x04000E68 RID: 3688
		[global::__DynamicallyInvokable]
		IfRange,
		/// <summary>The If-Unmodified-Since header, which specifies that the requested operation should be performed only if the requested resource has not been modified since the indicated date and time.</summary>
		// Token: 0x04000E69 RID: 3689
		[global::__DynamicallyInvokable]
		IfUnmodifiedSince,
		/// <summary>The Max-Forwards header, which specifies an integer indicating the remaining number of times that this request may be forwarded.</summary>
		// Token: 0x04000E6A RID: 3690
		[global::__DynamicallyInvokable]
		MaxForwards,
		/// <summary>The Proxy-Authorization header, which specifies the credentials that the client presents in order to authenticate itself to a proxy.</summary>
		// Token: 0x04000E6B RID: 3691
		[global::__DynamicallyInvokable]
		ProxyAuthorization,
		/// <summary>The Referer header, which specifies the URI of the resource from which the request URI was obtained.</summary>
		// Token: 0x04000E6C RID: 3692
		[global::__DynamicallyInvokable]
		Referer,
		/// <summary>The Range header, which specifies the sub-range(s) of the response that the client requests be returned in lieu of the entire response.</summary>
		// Token: 0x04000E6D RID: 3693
		[global::__DynamicallyInvokable]
		Range,
		/// <summary>The TE header, which specifies the transfer encodings that are acceptable for the response.</summary>
		// Token: 0x04000E6E RID: 3694
		[global::__DynamicallyInvokable]
		Te,
		/// <summary>The Translate header, a Microsoft extension to the HTTP specification used in conjunction with WebDAV functionality.</summary>
		// Token: 0x04000E6F RID: 3695
		[global::__DynamicallyInvokable]
		Translate,
		/// <summary>The User-Agent header, which specifies information about the client agent.</summary>
		// Token: 0x04000E70 RID: 3696
		[global::__DynamicallyInvokable]
		UserAgent
	}
}
