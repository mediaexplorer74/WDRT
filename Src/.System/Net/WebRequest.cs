using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.IO;
using System.Net.Cache;
using System.Net.Configuration;
using System.Net.Security;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	/// <summary>Makes a request to a Uniform Resource Identifier (URI). This is an <see langword="abstract" /> class.</summary>
	// Token: 0x0200018A RID: 394
	[global::__DynamicallyInvokable]
	[Serializable]
	public abstract class WebRequest : MarshalByRefObject, ISerializable
	{
		/// <summary>When overridden in a descendant class, gets the factory object derived from the <see cref="T:System.Net.IWebRequestCreate" /> class used to create the <see cref="T:System.Net.WebRequest" /> instantiated for making the request to the specified URI.</summary>
		/// <returns>The derived <see cref="T:System.Net.WebRequest" /> type returned by the <see cref="M:System.Net.IWebRequestCreate.Create(System.Uri)" /> method.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0004DAEF File Offset: 0x0004BCEF
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual IWebRequestCreate CreatorInstance
		{
			get
			{
				return WebRequest.webRequestCreate;
			}
		}

		/// <summary>Register an <see cref="T:System.Net.IWebRequestCreate" /> object.</summary>
		/// <param name="creator">The <see cref="T:System.Net.IWebRequestCreate" /> object to register.</param>
		// Token: 0x06000EC6 RID: 3782 RVA: 0x0004DAF6 File Offset: 0x0004BCF6
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void RegisterPortableWebRequestCreator(IWebRequestCreate creator)
		{
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x0004DAF8 File Offset: 0x0004BCF8
		private static object InternalSyncObject
		{
			get
			{
				if (WebRequest.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref WebRequest.s_InternalSyncObject, obj, null);
				}
				return WebRequest.s_InternalSyncObject;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x0004DB24 File Offset: 0x0004BD24
		internal static TimerThread.Queue DefaultTimerQueue
		{
			get
			{
				return WebRequest.s_DefaultTimerQueue;
			}
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0004DB2C File Offset: 0x0004BD2C
		private static WebRequest Create(Uri requestUri, bool useUriBase)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, "WebRequest", "Create", requestUri.ToString());
			}
			WebRequestPrefixElement webRequestPrefixElement = null;
			bool flag = false;
			string text;
			if (!useUriBase)
			{
				text = requestUri.AbsoluteUri;
			}
			else
			{
				text = requestUri.Scheme + ":";
			}
			int length = text.Length;
			ArrayList prefixList = WebRequest.PrefixList;
			for (int i = 0; i < prefixList.Count; i++)
			{
				webRequestPrefixElement = (WebRequestPrefixElement)prefixList[i];
				if (length >= webRequestPrefixElement.Prefix.Length && string.Compare(webRequestPrefixElement.Prefix, 0, text, 0, webRequestPrefixElement.Prefix.Length, StringComparison.OrdinalIgnoreCase) == 0)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				WebRequest webRequest = webRequestPrefixElement.Creator.Create(requestUri);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, "WebRequest", "Create", webRequest);
				}
				return webRequest;
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, "WebRequest", "Create", null);
			}
			throw new NotSupportedException(SR.GetString("net_unknown_prefix"));
		}

		/// <summary>Initializes a new <see cref="T:System.Net.WebRequest" /> instance for the specified URI scheme.</summary>
		/// <param name="requestUriString">The URI that identifies the Internet resource.</param>
		/// <returns>A <see cref="T:System.Net.WebRequest" /> descendant for the specific URI scheme.</returns>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="requestUriString" /> has not been registered.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="requestUriString" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have <see cref="T:System.Net.WebPermissionAttribute" /> permission to connect to the requested URI or a URI that the request is redirected to.</exception>
		/// <exception cref="T:System.UriFormatException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.  
		///
		///
		///
		///
		///  The URI specified in <paramref name="requestUriString" /> is not a valid URI.</exception>
		// Token: 0x06000ECA RID: 3786 RVA: 0x0004DC3B File Offset: 0x0004BE3B
		[global::__DynamicallyInvokable]
		public static WebRequest Create(string requestUriString)
		{
			if (requestUriString == null)
			{
				throw new ArgumentNullException("requestUriString");
			}
			return WebRequest.Create(new Uri(requestUriString), false);
		}

		/// <summary>Initializes a new <see cref="T:System.Net.WebRequest" /> instance for the specified URI scheme.</summary>
		/// <param name="requestUri">A <see cref="T:System.Uri" /> containing the URI of the requested resource.</param>
		/// <returns>A <see cref="T:System.Net.WebRequest" /> descendant for the specified URI scheme.</returns>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="requestUri" /> is not registered.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have <see cref="T:System.Net.WebPermissionAttribute" /> permission to connect to the requested URI or a URI that the request is redirected to.</exception>
		// Token: 0x06000ECB RID: 3787 RVA: 0x0004DC57 File Offset: 0x0004BE57
		[global::__DynamicallyInvokable]
		public static WebRequest Create(Uri requestUri)
		{
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			return WebRequest.Create(requestUri, false);
		}

		/// <summary>Initializes a new <see cref="T:System.Net.WebRequest" /> instance for the specified URI scheme.</summary>
		/// <param name="requestUri">A <see cref="T:System.Uri" /> containing the URI of the requested resource.</param>
		/// <returns>A <see cref="T:System.Net.WebRequest" /> descendant for the specified URI scheme.</returns>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="requestUri" /> is not registered.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have <see cref="T:System.Net.WebPermissionAttribute" /> permission to connect to the requested URI or a URI that the request is redirected to.</exception>
		// Token: 0x06000ECC RID: 3788 RVA: 0x0004DC74 File Offset: 0x0004BE74
		public static WebRequest CreateDefault(Uri requestUri)
		{
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			return WebRequest.Create(requestUri, true);
		}

		/// <summary>Initializes a new <see cref="T:System.Net.HttpWebRequest" /> instance for the specified URI string.</summary>
		/// <param name="requestUriString">A URI string that identifies the Internet resource.</param>
		/// <returns>An <see cref="T:System.Net.HttpWebRequest" /> instance for the specific URI string.</returns>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="requestUriString" /> is the http or https scheme.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="requestUriString" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have <see cref="T:System.Net.WebPermissionAttribute" /> permission to connect to the requested URI or a URI that the request is redirected to.</exception>
		/// <exception cref="T:System.UriFormatException">The URI specified in <paramref name="requestUriString" /> is not a valid URI.</exception>
		// Token: 0x06000ECD RID: 3789 RVA: 0x0004DC91 File Offset: 0x0004BE91
		[global::__DynamicallyInvokable]
		public static HttpWebRequest CreateHttp(string requestUriString)
		{
			if (requestUriString == null)
			{
				throw new ArgumentNullException("requestUriString");
			}
			return WebRequest.CreateHttp(new Uri(requestUriString));
		}

		/// <summary>Initializes a new <see cref="T:System.Net.HttpWebRequest" /> instance for the specified URI.</summary>
		/// <param name="requestUri">A URI that identifies the Internet resource.</param>
		/// <returns>An <see cref="T:System.Net.HttpWebRequest" /> instance for the specific URI string.</returns>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="requestUri" /> is the http or https scheme.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have <see cref="T:System.Net.WebPermissionAttribute" /> permission to connect to the requested URI or a URI that the request is redirected to.</exception>
		/// <exception cref="T:System.UriFormatException">The URI specified in <paramref name="requestUri" /> is not a valid URI.</exception>
		// Token: 0x06000ECE RID: 3790 RVA: 0x0004DCAC File Offset: 0x0004BEAC
		[global::__DynamicallyInvokable]
		public static HttpWebRequest CreateHttp(Uri requestUri)
		{
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			if (requestUri.Scheme != Uri.UriSchemeHttp && requestUri.Scheme != Uri.UriSchemeHttps)
			{
				throw new NotSupportedException(SR.GetString("net_unknown_prefix"));
			}
			return (HttpWebRequest)WebRequest.CreateDefault(requestUri);
		}

		/// <summary>Registers a <see cref="T:System.Net.WebRequest" /> descendant for the specified URI.</summary>
		/// <param name="prefix">The complete URI or URI prefix that the <see cref="T:System.Net.WebRequest" /> descendant services.</param>
		/// <param name="creator">The create method that the <see cref="T:System.Net.WebRequest" /> calls to create the <see cref="T:System.Net.WebRequest" /> descendant.</param>
		/// <returns>
		///   <see langword="true" /> if registration is successful; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="prefix" /> is <see langword="null" />  
		/// -or-  
		/// <paramref name="creator" /> is <see langword="null" />.</exception>
		// Token: 0x06000ECF RID: 3791 RVA: 0x0004DD0C File Offset: 0x0004BF0C
		[global::__DynamicallyInvokable]
		public static bool RegisterPrefix(string prefix, IWebRequestCreate creator)
		{
			bool flag = false;
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (creator == null)
			{
				throw new ArgumentNullException("creator");
			}
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			object internalSyncObject = WebRequest.InternalSyncObject;
			lock (internalSyncObject)
			{
				ArrayList arrayList = (ArrayList)WebRequest.PrefixList.Clone();
				Uri uri;
				if (Uri.TryCreate(prefix, UriKind.Absolute, out uri))
				{
					string text = uri.AbsoluteUri;
					if (!prefix.EndsWith("/", StringComparison.Ordinal) && uri.GetComponents(UriComponents.Path | UriComponents.Query | UriComponents.Fragment, UriFormat.UriEscaped).Equals("/"))
					{
						text = text.Substring(0, text.Length - 1);
					}
					prefix = text;
				}
				int i;
				for (i = 0; i < arrayList.Count; i++)
				{
					WebRequestPrefixElement webRequestPrefixElement = (WebRequestPrefixElement)arrayList[i];
					if (prefix.Length > webRequestPrefixElement.Prefix.Length)
					{
						break;
					}
					if (prefix.Length == webRequestPrefixElement.Prefix.Length && string.Compare(webRequestPrefixElement.Prefix, prefix, StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					arrayList.Insert(i, new WebRequestPrefixElement(prefix, creator));
					WebRequest.PrefixList = arrayList;
				}
			}
			return !flag;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0004DE48 File Offset: 0x0004C048
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x0004DEAC File Offset: 0x0004C0AC
		internal static ArrayList PrefixList
		{
			get
			{
				if (WebRequest.s_PrefixList == null)
				{
					object internalSyncObject = WebRequest.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (WebRequest.s_PrefixList == null)
						{
							WebRequest.s_PrefixList = WebRequestModulesSectionInternal.GetSection().WebRequestModules;
						}
					}
				}
				return WebRequest.s_PrefixList;
			}
			set
			{
				WebRequest.s_PrefixList = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebRequest" /> class.</summary>
		// Token: 0x06000ED2 RID: 3794 RVA: 0x0004DEB6 File Offset: 0x0004C0B6
		[global::__DynamicallyInvokable]
		protected WebRequest()
		{
			this.m_ImpersonationLevel = TokenImpersonationLevel.Delegation;
			this.m_AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebRequest" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.WebRequest" /> instance.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the source of the serialized stream associated with the new <see cref="T:System.Net.WebRequest" /> instance.</param>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the constructor, when the constructor is not overridden in a descendant class.</exception>
		// Token: 0x06000ED3 RID: 3795 RVA: 0x0004DECC File Offset: 0x0004C0CC
		protected WebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		/// <summary>When overridden in a descendant class, populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.WebRequest" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" />, which holds the serialized data for the <see cref="T:System.Net.WebRequest" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream associated with the new <see cref="T:System.Net.WebRequest" />.</param>
		/// <exception cref="T:System.NotImplementedException">An attempt is made to serialize the object, when the interface is not overridden in a descendant class.</exception>
		// Token: 0x06000ED4 RID: 3796 RVA: 0x0004DED4 File Offset: 0x0004C0D4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06000ED5 RID: 3797 RVA: 0x0004DEDE File Offset: 0x0004C0DE
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		/// <summary>Gets or sets the default cache policy for this request.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> that specifies the cache policy in effect for this request when no other policy is applicable.</returns>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x0004DEE0 File Offset: 0x0004C0E0
		// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x0004DEF4 File Offset: 0x0004C0F4
		public static RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				return RequestCacheManager.GetBinding(string.Empty).Policy;
			}
			set
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				RequestCacheBinding binding = RequestCacheManager.GetBinding(string.Empty);
				RequestCacheManager.SetBinding(string.Empty, new RequestCacheBinding(binding.Cache, binding.Validator, value));
			}
		}

		/// <summary>Gets or sets the cache policy for this request.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCachePolicy" /> object that defines a cache policy.</returns>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x0004DF32 File Offset: 0x0004C132
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x0004DF3A File Offset: 0x0004C13A
		public virtual RequestCachePolicy CachePolicy
		{
			get
			{
				return this.m_CachePolicy;
			}
			set
			{
				this.InternalSetCachePolicy(value);
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0004DF44 File Offset: 0x0004C144
		private void InternalSetCachePolicy(RequestCachePolicy policy)
		{
			if (this.m_CacheBinding != null && this.m_CacheBinding.Cache != null && this.m_CacheBinding.Validator != null && this.CacheProtocol == null && policy != null && policy.Level != RequestCacheLevel.BypassCache)
			{
				this.CacheProtocol = new RequestCacheProtocol(this.m_CacheBinding.Cache, this.m_CacheBinding.Validator.CreateValidator());
			}
			this.m_CachePolicy = policy;
		}

		/// <summary>When overridden in a descendant class, gets or sets the protocol method to use in this request.</summary>
		/// <returns>The protocol method to use in this request.</returns>
		/// <exception cref="T:System.NotImplementedException">If the property is not overridden in a descendant class, any attempt is made to get or set the property.</exception>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0004DFB4 File Offset: 0x0004C1B4
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x0004DFBB File Offset: 0x0004C1BB
		[global::__DynamicallyInvokable]
		public virtual string Method
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			[global::__DynamicallyInvokable]
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets the URI of the Internet resource associated with the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the resource associated with the request</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0004DFC2 File Offset: 0x0004C1C2
		[global::__DynamicallyInvokable]
		public virtual Uri RequestUri
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the name of the connection group for the request.</summary>
		/// <returns>The name of the connection group for the request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x0004DFC9 File Offset: 0x0004C1C9
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x0004DFD0 File Offset: 0x0004C1D0
		public virtual string ConnectionGroupName
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the collection of header name/value pairs associated with the request.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> containing the header name/value pairs associated with this request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0004DFD7 File Offset: 0x0004C1D7
		// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x0004DFDE File Offset: 0x0004C1DE
		[global::__DynamicallyInvokable]
		public virtual WebHeaderCollection Headers
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			[global::__DynamicallyInvokable]
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the content length of the request data being sent.</summary>
		/// <returns>The number of bytes of request data being sent.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x0004DFE5 File Offset: 0x0004C1E5
		// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x0004DFEC File Offset: 0x0004C1EC
		public virtual long ContentLength
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the content type of the request data being sent.</summary>
		/// <returns>The content type of the request data.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0004DFF3 File Offset: 0x0004C1F3
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x0004DFFA File Offset: 0x0004C1FA
		[global::__DynamicallyInvokable]
		public virtual string ContentType
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			[global::__DynamicallyInvokable]
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the network credentials used for authenticating the request with the Internet resource.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> containing the authentication credentials associated with the request. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0004E001 File Offset: 0x0004C201
		// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x0004E008 File Offset: 0x0004C208
		[global::__DynamicallyInvokable]
		public virtual ICredentials Credentials
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			[global::__DynamicallyInvokable]
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets a <see cref="T:System.Boolean" /> value that controls whether <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>
		///   <see langword="true" /> if the default credentials are used; otherwise <see langword="false" />. The default value is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property after the request was sent.</exception>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0004E00F File Offset: 0x0004C20F
		// (set) Token: 0x06000EE9 RID: 3817 RVA: 0x0004E016 File Offset: 0x0004C216
		[global::__DynamicallyInvokable]
		public virtual bool UseDefaultCredentials
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			[global::__DynamicallyInvokable]
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the network proxy to use to access this Internet resource.</summary>
		/// <returns>The <see cref="T:System.Net.IWebProxy" /> to use to access the Internet resource.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0004E01D File Offset: 0x0004C21D
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x0004E024 File Offset: 0x0004C224
		[global::__DynamicallyInvokable]
		public virtual IWebProxy Proxy
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			[global::__DynamicallyInvokable]
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, indicates whether to pre-authenticate the request.</summary>
		/// <returns>
		///   <see langword="true" /> to pre-authenticate; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0004E02B File Offset: 0x0004C22B
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x0004E032 File Offset: 0x0004C232
		public virtual bool PreAuthenticate
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>Gets or sets the length of time, in milliseconds, before the request times out.</summary>
		/// <returns>The length of time, in milliseconds, until the request times out, or the value <see cref="F:System.Threading.Timeout.Infinite" /> to indicate that the request does not time out. The default value is defined by the descendant class.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0004E039 File Offset: 0x0004C239
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x0004E040 File Offset: 0x0004C240
		public virtual int Timeout
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, returns a <see cref="T:System.IO.Stream" /> for writing data to the Internet resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> for writing data to the Internet resource.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06000EF0 RID: 3824 RVA: 0x0004E047 File Offset: 0x0004C247
		public virtual Stream GetRequestStream()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a descendant class, returns a response to an Internet request.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> containing the response to the Internet request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06000EF1 RID: 3825 RVA: 0x0004E04E File Offset: 0x0004C24E
		public virtual WebResponse GetResponse()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a descendant class, begins an asynchronous request for an Internet resource.</summary>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object containing state information for this asynchronous request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06000EF2 RID: 3826 RVA: 0x0004E055 File Offset: 0x0004C255
		[global::__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a descendant class, returns a <see cref="T:System.Net.WebResponse" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references a pending request for a response.</param>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains a response to the Internet request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06000EF3 RID: 3827 RVA: 0x0004E05C File Offset: 0x0004C25C
		[global::__DynamicallyInvokable]
		public virtual WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a descendant class, provides an asynchronous version of the <see cref="M:System.Net.WebRequest.GetRequestStream" /> method.</summary>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object containing state information for this asynchronous request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06000EF4 RID: 3828 RVA: 0x0004E063 File Offset: 0x0004C263
		[global::__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a descendant class, returns a <see cref="T:System.IO.Stream" /> for writing data to the Internet resource.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references a pending request for a stream.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> to write data to.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06000EF5 RID: 3829 RVA: 0x0004E06A File Offset: 0x0004C26A
		[global::__DynamicallyInvokable]
		public virtual Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a descendant class, returns a <see cref="T:System.IO.Stream" /> for writing data to the Internet resource as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06000EF6 RID: 3830 RVA: 0x0004E074 File Offset: 0x0004C274
		[global::__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<Stream> GetRequestStreamAsync()
		{
			IWebProxy webProxy = null;
			try
			{
				webProxy = this.Proxy;
			}
			catch (NotImplementedException)
			{
			}
			if (ExecutionContext.IsFlowSuppressed() && (this.UseDefaultCredentials || this.Credentials != null || (webProxy != null && webProxy.Credentials != null)))
			{
				WindowsIdentity currentUser = this.SafeCaptureIdenity();
				return Task.Run<Stream>(delegate
				{
					Task<Stream> task;
					using (currentUser)
					{
						using (currentUser.Impersonate())
						{
							task = Task<Stream>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetRequestStream), new Func<IAsyncResult, Stream>(this.EndGetRequestStream), null);
						}
					}
					return task;
				});
			}
			return Task.Run<Stream>(() => Task<Stream>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetRequestStream), new Func<IAsyncResult, Stream>(this.EndGetRequestStream), null));
		}

		/// <summary>When overridden in a descendant class, returns a response to an Internet request as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06000EF7 RID: 3831 RVA: 0x0004E100 File Offset: 0x0004C300
		[global::__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<WebResponse> GetResponseAsync()
		{
			IWebProxy webProxy = null;
			try
			{
				webProxy = this.Proxy;
			}
			catch (NotImplementedException)
			{
			}
			if (ExecutionContext.IsFlowSuppressed() && (this.UseDefaultCredentials || this.Credentials != null || (webProxy != null && webProxy.Credentials != null)))
			{
				WindowsIdentity currentUser = this.SafeCaptureIdenity();
				return Task.Run<WebResponse>(delegate
				{
					Task<WebResponse> task;
					using (currentUser)
					{
						using (currentUser.Impersonate())
						{
							task = Task<WebResponse>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetResponse), new Func<IAsyncResult, WebResponse>(this.EndGetResponse), null);
						}
					}
					return task;
				});
			}
			return Task.Run<WebResponse>(() => Task<WebResponse>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetResponse), new Func<IAsyncResult, WebResponse>(this.EndGetResponse), null));
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0004E18C File Offset: 0x0004C38C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		private WindowsIdentity SafeCaptureIdenity()
		{
			return WindowsIdentity.GetCurrent();
		}

		/// <summary>Aborts the request.</summary>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06000EF9 RID: 3833 RVA: 0x0004E193 File Offset: 0x0004C393
		[global::__DynamicallyInvokable]
		public virtual void Abort()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x0004E19A File Offset: 0x0004C39A
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x0004E1A2 File Offset: 0x0004C3A2
		internal RequestCacheProtocol CacheProtocol
		{
			get
			{
				return this.m_CacheProtocol;
			}
			set
			{
				this.m_CacheProtocol = value;
			}
		}

		/// <summary>Gets or sets values indicating the level of authentication and impersonation used for this request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Net.Security.AuthenticationLevel" /> values. The default value is <see cref="F:System.Net.Security.AuthenticationLevel.MutualAuthRequested" />.  
		///  In mutual authentication, both the client and server present credentials to establish their identity. The <see cref="F:System.Net.Security.AuthenticationLevel.MutualAuthRequired" /> and <see cref="F:System.Net.Security.AuthenticationLevel.MutualAuthRequested" /> values are relevant for Kerberos authentication. Kerberos authentication can be supported directly, or can be used if the Negotiate security protocol is used to select the actual security protocol. For more information about authentication protocols, see Internet Authentication.  
		///  To determine whether mutual authentication occurred, check the <see cref="P:System.Net.WebResponse.IsMutuallyAuthenticated" /> property.  
		///  If you specify the <see cref="F:System.Net.Security.AuthenticationLevel.MutualAuthRequired" /> authentication flag value and mutual authentication does not occur, your application will receive an <see cref="T:System.IO.IOException" /> with a <see cref="T:System.Net.ProtocolViolationException" /> inner exception indicating that mutual authentication failed.</returns>
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0004E1AB File Offset: 0x0004C3AB
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x0004E1B3 File Offset: 0x0004C3B3
		public AuthenticationLevel AuthenticationLevel
		{
			get
			{
				return this.m_AuthenticationLevel;
			}
			set
			{
				this.m_AuthenticationLevel = value;
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0004E1BC File Offset: 0x0004C3BC
		internal virtual ContextAwareResult GetConnectingContext()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0004E1C3 File Offset: 0x0004C3C3
		internal virtual ContextAwareResult GetWritingContext()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0004E1CA File Offset: 0x0004C3CA
		internal virtual ContextAwareResult GetReadingContext()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Gets or sets the impersonation level for the current request.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> value.</returns>
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0004E1D1 File Offset: 0x0004C3D1
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x0004E1D9 File Offset: 0x0004C3D9
		public TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this.m_ImpersonationLevel;
			}
			set
			{
				this.m_ImpersonationLevel = value;
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0004E1E2 File Offset: 0x0004C3E2
		internal virtual void RequestCallback(object obj)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0004E1EC File Offset: 0x0004C3EC
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x0004E25C File Offset: 0x0004C45C
		internal static IWebProxy InternalDefaultWebProxy
		{
			get
			{
				if (!WebRequest.s_DefaultWebProxyInitialized)
				{
					object internalSyncObject = WebRequest.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (!WebRequest.s_DefaultWebProxyInitialized)
						{
							DefaultProxySectionInternal section = DefaultProxySectionInternal.GetSection();
							if (section != null)
							{
								WebRequest.s_DefaultWebProxy = section.WebProxy;
							}
							WebRequest.s_DefaultWebProxyInitialized = true;
						}
					}
				}
				return WebRequest.s_DefaultWebProxy;
			}
			set
			{
				if (!WebRequest.s_DefaultWebProxyInitialized)
				{
					object internalSyncObject = WebRequest.InternalSyncObject;
					lock (internalSyncObject)
					{
						WebRequest.s_DefaultWebProxy = value;
						WebRequest.s_DefaultWebProxyInitialized = true;
						return;
					}
				}
				WebRequest.s_DefaultWebProxy = value;
			}
		}

		/// <summary>Gets or sets the global HTTP proxy.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> used by every call to instances of <see cref="T:System.Net.WebRequest" />.</returns>
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0004E2B8 File Offset: 0x0004C4B8
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x0004E2C9 File Offset: 0x0004C4C9
		[global::__DynamicallyInvokable]
		public static IWebProxy DefaultWebProxy
		{
			[global::__DynamicallyInvokable]
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				return WebRequest.InternalDefaultWebProxy;
			}
			[global::__DynamicallyInvokable]
			set
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				WebRequest.InternalDefaultWebProxy = value;
			}
		}

		/// <summary>Returns a proxy configured with the Internet Explorer settings of the currently impersonated user.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> used by every call to instances of <see cref="T:System.Net.WebRequest" />.</returns>
		// Token: 0x06000F08 RID: 3848 RVA: 0x0004E2DB File Offset: 0x0004C4DB
		public static IWebProxy GetSystemWebProxy()
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			return WebRequest.InternalGetSystemWebProxy();
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0004E2EC File Offset: 0x0004C4EC
		internal static IWebProxy InternalGetSystemWebProxy()
		{
			return new WebRequest.WebProxyWrapperOpaque(new WebProxy(true));
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0004E2F9 File Offset: 0x0004C4F9
		internal void SetupCacheProtocol(Uri uri)
		{
			this.m_CacheBinding = RequestCacheManager.GetBinding(uri.Scheme);
			this.InternalSetCachePolicy(this.m_CacheBinding.Policy);
			if (this.m_CachePolicy == null)
			{
				this.InternalSetCachePolicy(WebRequest.DefaultCachePolicy);
			}
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0004E330 File Offset: 0x0004C530
		private static void InitEtwMethods()
		{
			Type typeFromHandle = typeof(FrameworkEventSource);
			Type[] array = new Type[]
			{
				typeof(object),
				typeof(string),
				typeof(bool),
				typeof(bool)
			};
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			MethodInfo method = typeFromHandle.GetMethod("BeginGetResponse", bindingFlags, null, array, null);
			MethodInfo method2 = typeFromHandle.GetMethod("EndGetResponse", bindingFlags, null, new Type[]
			{
				typeof(object),
				typeof(bool),
				typeof(bool),
				typeof(int)
			}, null);
			MethodInfo method3 = typeFromHandle.GetMethod("BeginGetRequestStream", bindingFlags, null, array, null);
			MethodInfo method4 = typeFromHandle.GetMethod("EndGetRequestStream", bindingFlags, null, new Type[]
			{
				typeof(object),
				typeof(bool),
				typeof(bool)
			}, null);
			if (method != null && method2 != null && method3 != null && method4 != null)
			{
				WebRequest.s_EtwFireBeginGetResponse = (WebRequest.DelEtwFireBeginWRGet)method.CreateDelegate(typeof(WebRequest.DelEtwFireBeginWRGet), FrameworkEventSource.Log);
				WebRequest.s_EtwFireEndGetResponse = (WebRequest.DelEtwFireEndWRespGet)method2.CreateDelegate(typeof(WebRequest.DelEtwFireEndWRespGet), FrameworkEventSource.Log);
				WebRequest.s_EtwFireBeginGetRequestStream = (WebRequest.DelEtwFireBeginWRGet)method3.CreateDelegate(typeof(WebRequest.DelEtwFireBeginWRGet), FrameworkEventSource.Log);
				WebRequest.s_EtwFireEndGetRequestStream = (WebRequest.DelEtwFireEndWRGet)method4.CreateDelegate(typeof(WebRequest.DelEtwFireEndWRGet), FrameworkEventSource.Log);
			}
			WebRequest.s_TriedGetEtwDelegates = true;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0004E4E8 File Offset: 0x0004C6E8
		internal void LogBeginGetResponse(bool success, bool synchronous)
		{
			string originalString = this.RequestUri.OriginalString;
			if (!WebRequest.s_TriedGetEtwDelegates)
			{
				WebRequest.InitEtwMethods();
			}
			if (WebRequest.s_EtwFireBeginGetResponse != null)
			{
				WebRequest.s_EtwFireBeginGetResponse(this, originalString, success, synchronous);
			}
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0004E524 File Offset: 0x0004C724
		internal void LogEndGetResponse(bool success, bool synchronous, int statusCode)
		{
			if (!WebRequest.s_TriedGetEtwDelegates)
			{
				WebRequest.InitEtwMethods();
			}
			if (WebRequest.s_EtwFireEndGetResponse != null)
			{
				WebRequest.s_EtwFireEndGetResponse(this, success, synchronous, statusCode);
			}
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0004E54C File Offset: 0x0004C74C
		internal void LogBeginGetRequestStream(bool success, bool synchronous)
		{
			string originalString = this.RequestUri.OriginalString;
			if (!WebRequest.s_TriedGetEtwDelegates)
			{
				WebRequest.InitEtwMethods();
			}
			if (WebRequest.s_EtwFireBeginGetRequestStream != null)
			{
				WebRequest.s_EtwFireBeginGetRequestStream(this, originalString, success, synchronous);
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0004E588 File Offset: 0x0004C788
		internal void LogEndGetRequestStream(bool success, bool synchronous)
		{
			if (!WebRequest.s_TriedGetEtwDelegates)
			{
				WebRequest.InitEtwMethods();
			}
			if (WebRequest.s_EtwFireEndGetRequestStream != null)
			{
				WebRequest.s_EtwFireEndGetRequestStream(this, success, synchronous);
			}
		}

		// Token: 0x0400127B RID: 4731
		internal const int DefaultTimeout = 100000;

		// Token: 0x0400127C RID: 4732
		private static volatile ArrayList s_PrefixList;

		// Token: 0x0400127D RID: 4733
		private static object s_InternalSyncObject;

		// Token: 0x0400127E RID: 4734
		private static TimerThread.Queue s_DefaultTimerQueue = TimerThread.CreateQueue(100000);

		// Token: 0x0400127F RID: 4735
		private AuthenticationLevel m_AuthenticationLevel;

		// Token: 0x04001280 RID: 4736
		private TokenImpersonationLevel m_ImpersonationLevel;

		// Token: 0x04001281 RID: 4737
		private RequestCachePolicy m_CachePolicy;

		// Token: 0x04001282 RID: 4738
		private RequestCacheProtocol m_CacheProtocol;

		// Token: 0x04001283 RID: 4739
		private RequestCacheBinding m_CacheBinding;

		// Token: 0x04001284 RID: 4740
		private static WebRequest.DesignerWebRequestCreate webRequestCreate = new WebRequest.DesignerWebRequestCreate();

		// Token: 0x04001285 RID: 4741
		private static volatile IWebProxy s_DefaultWebProxy;

		// Token: 0x04001286 RID: 4742
		private static volatile bool s_DefaultWebProxyInitialized;

		// Token: 0x04001287 RID: 4743
		private static WebRequest.DelEtwFireBeginWRGet s_EtwFireBeginGetResponse;

		// Token: 0x04001288 RID: 4744
		private static WebRequest.DelEtwFireEndWRespGet s_EtwFireEndGetResponse;

		// Token: 0x04001289 RID: 4745
		private static WebRequest.DelEtwFireBeginWRGet s_EtwFireBeginGetRequestStream;

		// Token: 0x0400128A RID: 4746
		private static WebRequest.DelEtwFireEndWRGet s_EtwFireEndGetRequestStream;

		// Token: 0x0400128B RID: 4747
		private static volatile bool s_TriedGetEtwDelegates;

		// Token: 0x02000734 RID: 1844
		internal class DesignerWebRequestCreate : IWebRequestCreate
		{
			// Token: 0x06004187 RID: 16775 RVA: 0x0011029C File Offset: 0x0010E49C
			public WebRequest Create(Uri uri)
			{
				return WebRequest.Create(uri);
			}
		}

		// Token: 0x02000735 RID: 1845
		internal class WebProxyWrapperOpaque : IAutoWebProxy, IWebProxy
		{
			// Token: 0x06004189 RID: 16777 RVA: 0x001102AC File Offset: 0x0010E4AC
			internal WebProxyWrapperOpaque(WebProxy webProxy)
			{
				this.webProxy = webProxy;
			}

			// Token: 0x0600418A RID: 16778 RVA: 0x001102BB File Offset: 0x0010E4BB
			public Uri GetProxy(Uri destination)
			{
				return this.webProxy.GetProxy(destination);
			}

			// Token: 0x0600418B RID: 16779 RVA: 0x001102C9 File Offset: 0x0010E4C9
			public bool IsBypassed(Uri host)
			{
				return this.webProxy.IsBypassed(host);
			}

			// Token: 0x17000EFD RID: 3837
			// (get) Token: 0x0600418C RID: 16780 RVA: 0x001102D7 File Offset: 0x0010E4D7
			// (set) Token: 0x0600418D RID: 16781 RVA: 0x001102E4 File Offset: 0x0010E4E4
			public ICredentials Credentials
			{
				get
				{
					return this.webProxy.Credentials;
				}
				set
				{
					this.webProxy.Credentials = value;
				}
			}

			// Token: 0x0600418E RID: 16782 RVA: 0x001102F2 File Offset: 0x0010E4F2
			public ProxyChain GetProxies(Uri destination)
			{
				return ((IAutoWebProxy)this.webProxy).GetProxies(destination);
			}

			// Token: 0x04003195 RID: 12693
			protected readonly WebProxy webProxy;
		}

		// Token: 0x02000736 RID: 1846
		internal class WebProxyWrapper : WebRequest.WebProxyWrapperOpaque
		{
			// Token: 0x0600418F RID: 16783 RVA: 0x00110300 File Offset: 0x0010E500
			internal WebProxyWrapper(WebProxy webProxy)
				: base(webProxy)
			{
			}

			// Token: 0x17000EFE RID: 3838
			// (get) Token: 0x06004190 RID: 16784 RVA: 0x00110309 File Offset: 0x0010E509
			internal WebProxy WebProxy
			{
				get
				{
					return this.webProxy;
				}
			}
		}

		// Token: 0x02000737 RID: 1847
		// (Invoke) Token: 0x06004192 RID: 16786
		private delegate void DelEtwFireBeginWRGet(object id, string uri, bool success, bool synchronous);

		// Token: 0x02000738 RID: 1848
		// (Invoke) Token: 0x06004196 RID: 16790
		private delegate void DelEtwFireEndWRGet(object id, bool success, bool synchronous);

		// Token: 0x02000739 RID: 1849
		// (Invoke) Token: 0x0600419A RID: 16794
		private delegate void DelEtwFireEndWRespGet(object id, bool success, bool synchronous, int statusCode);
	}
}
