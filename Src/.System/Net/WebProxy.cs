using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace System.Net
{
	/// <summary>Contains HTTP proxy settings for the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x02000189 RID: 393
	[Serializable]
	public class WebProxy : IAutoWebProxy, IWebProxy, ISerializable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.WebProxy" /> class.</summary>
		// Token: 0x06000E93 RID: 3731 RVA: 0x0004D189 File Offset: 0x0004B389
		public WebProxy()
			: this(null, false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class from the specified <see cref="T:System.Uri" /> instance.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server.</param>
		// Token: 0x06000E94 RID: 3732 RVA: 0x0004D195 File Offset: 0x0004B395
		public WebProxy(Uri Address)
			: this(Address, false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the <see cref="T:System.Uri" /> instance and bypass setting.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server.</param>
		/// <param name="BypassOnLocal">
		///   <see langword="true" /> to bypass the proxy for local addresses; otherwise, <see langword="false" />.</param>
		// Token: 0x06000E95 RID: 3733 RVA: 0x0004D1A1 File Offset: 0x0004B3A1
		public WebProxy(Uri Address, bool BypassOnLocal)
			: this(Address, BypassOnLocal, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified <see cref="T:System.Uri" /> instance, bypass setting, and list of URIs to bypass.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server.</param>
		/// <param name="BypassOnLocal">
		///   <see langword="true" /> to bypass the proxy for local addresses; otherwise, <see langword="false" />.</param>
		/// <param name="BypassList">An array of regular expression strings that contains the URIs of the servers to bypass.</param>
		// Token: 0x06000E96 RID: 3734 RVA: 0x0004D1AD File Offset: 0x0004B3AD
		public WebProxy(Uri Address, bool BypassOnLocal, string[] BypassList)
			: this(Address, BypassOnLocal, BypassList, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified <see cref="T:System.Uri" /> instance, bypass setting, list of URIs to bypass, and credentials.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server.</param>
		/// <param name="BypassOnLocal">
		///   <see langword="true" /> to bypass the proxy for local addresses; otherwise, <see langword="false" />.</param>
		/// <param name="BypassList">An array of regular expression strings that contains the URIs of the servers to bypass.</param>
		/// <param name="Credentials">An <see cref="T:System.Net.ICredentials" /> instance to submit to the proxy server for authentication.</param>
		// Token: 0x06000E97 RID: 3735 RVA: 0x0004D1B9 File Offset: 0x0004B3B9
		public WebProxy(Uri Address, bool BypassOnLocal, string[] BypassList, ICredentials Credentials)
		{
			this._ProxyAddress = Address;
			this._BypassOnLocal = BypassOnLocal;
			if (BypassList != null)
			{
				this._BypassList = new ArrayList(BypassList);
				this.UpdateRegExList(true);
			}
			this._Credentials = Credentials;
			this.m_EnableAutoproxy = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified host and port number.</summary>
		/// <param name="Host">The name of the proxy host.</param>
		/// <param name="Port">The port number on <paramref name="Host" /> to use.</param>
		/// <exception cref="T:System.UriFormatException">The URI formed by combining <paramref name="Host" /> and <paramref name="Port" /> is not a valid URI.</exception>
		// Token: 0x06000E98 RID: 3736 RVA: 0x0004D1F4 File Offset: 0x0004B3F4
		public WebProxy(string Host, int Port)
			: this(new Uri("http://" + Host + ":" + Port.ToString(CultureInfo.InvariantCulture)), false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI.</summary>
		/// <param name="Address">The URI of the proxy server.</param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI.</exception>
		// Token: 0x06000E99 RID: 3737 RVA: 0x0004D220 File Offset: 0x0004B420
		public WebProxy(string Address)
			: this(WebProxy.CreateProxyUri(Address), false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI and bypass setting.</summary>
		/// <param name="Address">The URI of the proxy server.</param>
		/// <param name="BypassOnLocal">
		///   <see langword="true" /> to bypass the proxy for local addresses; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI.</exception>
		// Token: 0x06000E9A RID: 3738 RVA: 0x0004D231 File Offset: 0x0004B431
		public WebProxy(string Address, bool BypassOnLocal)
			: this(WebProxy.CreateProxyUri(Address), BypassOnLocal, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI, bypass setting, and list of URIs to bypass.</summary>
		/// <param name="Address">The URI of the proxy server.</param>
		/// <param name="BypassOnLocal">
		///   <see langword="true" /> to bypass the proxy for local addresses; otherwise, <see langword="false" />.</param>
		/// <param name="BypassList">An array of regular expression strings that contain the URIs of the servers to bypass.</param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI.</exception>
		// Token: 0x06000E9B RID: 3739 RVA: 0x0004D242 File Offset: 0x0004B442
		public WebProxy(string Address, bool BypassOnLocal, string[] BypassList)
			: this(WebProxy.CreateProxyUri(Address), BypassOnLocal, BypassList, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI, bypass setting, list of URIs to bypass, and credentials.</summary>
		/// <param name="Address">The URI of the proxy server.</param>
		/// <param name="BypassOnLocal">
		///   <see langword="true" /> to bypass the proxy for local addresses; otherwise, <see langword="false" />.</param>
		/// <param name="BypassList">An array of regular expression strings that contains the URIs of the servers to bypass.</param>
		/// <param name="Credentials">An <see cref="T:System.Net.ICredentials" /> instance to submit to the proxy server for authentication.</param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI.</exception>
		// Token: 0x06000E9C RID: 3740 RVA: 0x0004D253 File Offset: 0x0004B453
		public WebProxy(string Address, bool BypassOnLocal, string[] BypassList, ICredentials Credentials)
			: this(WebProxy.CreateProxyUri(Address), BypassOnLocal, BypassList, Credentials)
		{
		}

		/// <summary>Gets or sets the address of the proxy server.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that contains the address of the proxy server.</returns>
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x0004D265 File Offset: 0x0004B465
		// (set) Token: 0x06000E9E RID: 3742 RVA: 0x0004D273 File Offset: 0x0004B473
		public Uri Address
		{
			get
			{
				this.CheckForChanges();
				return this._ProxyAddress;
			}
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._ProxyHostAddresses = null;
				this._ProxyAddress = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x0004D290 File Offset: 0x0004B490
		internal bool AutoDetect
		{
			set
			{
				if (this.ScriptEngine == null)
				{
					this.ScriptEngine = new AutoWebProxyScriptEngine(this, false);
				}
				this.ScriptEngine.AutomaticallyDetectSettings = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (set) Token: 0x06000EA0 RID: 3744 RVA: 0x0004D2B3 File Offset: 0x0004B4B3
		internal Uri ScriptLocation
		{
			set
			{
				if (this.ScriptEngine == null)
				{
					this.ScriptEngine = new AutoWebProxyScriptEngine(this, false);
				}
				this.ScriptEngine.AutomaticConfigurationScript = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to bypass the proxy server for local addresses.</summary>
		/// <returns>
		///   <see langword="true" /> to bypass the proxy server for local addresses; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0004D2D6 File Offset: 0x0004B4D6
		// (set) Token: 0x06000EA2 RID: 3746 RVA: 0x0004D2E4 File Offset: 0x0004B4E4
		public bool BypassProxyOnLocal
		{
			get
			{
				this.CheckForChanges();
				return this._BypassOnLocal;
			}
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._BypassOnLocal = value;
			}
		}

		/// <summary>Gets or sets an array of addresses that do not use the proxy server.</summary>
		/// <returns>An array that contains a list of regular expressions that describe URIs that do not use the proxy server when accessed.</returns>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0004D2FA File Offset: 0x0004B4FA
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x0004D32F File Offset: 0x0004B52F
		public string[] BypassList
		{
			get
			{
				this.CheckForChanges();
				if (this._BypassList == null)
				{
					this._BypassList = new ArrayList();
				}
				return (string[])this._BypassList.ToArray(typeof(string));
			}
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._BypassList = new ArrayList(value);
				this.UpdateRegExList(true);
			}
		}

		/// <summary>Gets or sets the credentials to submit to the proxy server for authentication.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance that contains the credentials to submit to the proxy server for authentication.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property when the <see cref="P:System.Net.WebProxy.UseDefaultCredentials" /> property was set to <see langword="true" />.</exception>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x0004D351 File Offset: 0x0004B551
		// (set) Token: 0x06000EA6 RID: 3750 RVA: 0x0004D359 File Offset: 0x0004B559
		public ICredentials Credentials
		{
			get
			{
				return this._Credentials;
			}
			set
			{
				this._Credentials = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>
		///   <see langword="true" /> if the default credentials are used; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property when the <see cref="P:System.Net.WebProxy.Credentials" /> property contains credentials other than the default credentials.</exception>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0004D362 File Offset: 0x0004B562
		// (set) Token: 0x06000EA8 RID: 3752 RVA: 0x0004D374 File Offset: 0x0004B574
		public bool UseDefaultCredentials
		{
			get
			{
				return this.Credentials is SystemNetworkCredential;
			}
			set
			{
				this._Credentials = (value ? CredentialCache.DefaultCredentials : null);
			}
		}

		/// <summary>Gets a list of addresses that do not use the proxy server.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains a list of <see cref="P:System.Net.WebProxy.BypassList" /> arrays that represents URIs that do not use the proxy server when accessed.</returns>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0004D387 File Offset: 0x0004B587
		public ArrayList BypassArrayList
		{
			get
			{
				this.CheckForChanges();
				if (this._BypassList == null)
				{
					this._BypassList = new ArrayList();
				}
				return this._BypassList;
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0004D3A8 File Offset: 0x0004B5A8
		internal void CheckForChanges()
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.CheckForChanges();
			}
		}

		/// <summary>Returns the proxied URI for a request.</summary>
		/// <param name="destination">The <see cref="T:System.Uri" /> instance of the requested Internet resource.</param>
		/// <returns>The <see cref="T:System.Uri" /> instance of the Internet resource, if the resource is on the bypass list; otherwise, the <see cref="T:System.Uri" /> instance of the proxy.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destination" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000EAB RID: 3755 RVA: 0x0004D3C0 File Offset: 0x0004B5C0
		public Uri GetProxy(Uri destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Uri uri;
			if (this.GetProxyAuto(destination, out uri))
			{
				return uri;
			}
			if (this.IsBypassedManual(destination))
			{
				return destination;
			}
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			Uri uri2 = ((proxyHostAddresses != null) ? (proxyHostAddresses[destination.Scheme] as Uri) : this._ProxyAddress);
			if (!(uri2 != null))
			{
				return destination;
			}
			return uri2;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0004D429 File Offset: 0x0004B629
		private static Uri CreateProxyUri(string address)
		{
			if (address == null)
			{
				return null;
			}
			if (address.IndexOf("://") == -1)
			{
				address = "http://" + address;
			}
			return new Uri(address);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0004D454 File Offset: 0x0004B654
		private void UpdateRegExList(bool canThrow)
		{
			Regex[] array = null;
			ArrayList bypassList = this._BypassList;
			try
			{
				if (bypassList != null && bypassList.Count > 0)
				{
					array = new Regex[bypassList.Count];
					for (int i = 0; i < bypassList.Count; i++)
					{
						array[i] = new Regex((string)bypassList[i], RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
					}
				}
			}
			catch
			{
				if (!canThrow)
				{
					this._RegExBypassList = null;
					return;
				}
				throw;
			}
			this._RegExBypassList = array;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0004D4D4 File Offset: 0x0004B6D4
		private bool IsMatchInBypassList(Uri input)
		{
			this.UpdateRegExList(false);
			if (this._RegExBypassList == null)
			{
				return false;
			}
			string text = input.Scheme + "://" + input.Host + ((!input.IsDefaultPort) ? (":" + input.Port.ToString()) : "");
			for (int i = 0; i < this._BypassList.Count; i++)
			{
				if (this._RegExBypassList[i].IsMatch(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0004D55C File Offset: 0x0004B75C
		private bool IsLocal(Uri host)
		{
			string host2 = host.Host;
			IPAddress ipaddress;
			if (IPAddress.TryParse(host2, out ipaddress))
			{
				return IPAddress.IsLoopback(ipaddress) || NclUtilities.IsAddressLocal(ipaddress);
			}
			int num = host2.IndexOf('.');
			if (num == -1)
			{
				return true;
			}
			string text = "." + IPGlobalProperties.InternalGetIPGlobalProperties().DomainName;
			return text != null && text.Length == host2.Length - num && string.Compare(text, 0, host2, num, text.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0004D5D8 File Offset: 0x0004B7D8
		private bool IsLocalInProxyHash(Uri host)
		{
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			if (proxyHostAddresses != null)
			{
				Uri uri = (Uri)proxyHostAddresses[host.Scheme];
				if (uri == null)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Indicates whether to use the proxy server for the specified host.</summary>
		/// <param name="host">The <see cref="T:System.Uri" /> instance of the host to check for proxy use.</param>
		/// <returns>
		///   <see langword="true" /> if the proxy server should not be used for <paramref name="host" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000EB1 RID: 3761 RVA: 0x0004D610 File Offset: 0x0004B810
		public bool IsBypassed(Uri host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			bool flag;
			if (this.IsBypassedAuto(host, out flag))
			{
				return flag;
			}
			return this.IsBypassedManual(host);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0004D648 File Offset: 0x0004B848
		private bool IsBypassedManual(Uri host)
		{
			return host.IsLoopback || (this._ProxyAddress == null && this._ProxyHostAddresses == null) || (this._BypassOnLocal && this.IsLocal(host)) || this.IsMatchInBypassList(host) || this.IsLocalInProxyHash(host);
		}

		/// <summary>Reads the Internet Explorer nondynamic proxy settings.</summary>
		/// <returns>A <see cref="T:System.Net.WebProxy" /> instance that contains the nondynamic proxy settings from Internet Explorer 5.5 and later.</returns>
		// Token: 0x06000EB3 RID: 3763 RVA: 0x0004D698 File Offset: 0x0004B898
		[Obsolete("This method has been deprecated. Please use the proxy selected for you by default. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static WebProxy GetDefaultProxy()
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			return new WebProxy(true);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Net.WebProxy" /> class using previously serialized content.</summary>
		/// <param name="serializationInfo">The serialization data.</param>
		/// <param name="streamingContext">The context for the serialized data.</param>
		// Token: 0x06000EB4 RID: 3764 RVA: 0x0004D6AC File Offset: 0x0004B8AC
		protected WebProxy(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			bool flag = false;
			try
			{
				flag = serializationInfo.GetBoolean("_UseRegistry");
			}
			catch
			{
			}
			if (flag)
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				this.UnsafeUpdateFromRegistry();
				return;
			}
			this._ProxyAddress = (Uri)serializationInfo.GetValue("_ProxyAddress", typeof(Uri));
			this._BypassOnLocal = serializationInfo.GetBoolean("_BypassOnLocal");
			this._BypassList = (ArrayList)serializationInfo.GetValue("_BypassList", typeof(ArrayList));
			try
			{
				this.UseDefaultCredentials = serializationInfo.GetBoolean("_UseDefaultCredentials");
			}
			catch
			{
			}
		}

		/// <summary>Creates the serialization data and context that are used by the system to serialize a <see cref="T:System.Net.WebProxy" /> object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that indicates the destination for this serialization.</param>
		// Token: 0x06000EB5 RID: 3765 RVA: 0x0004D76C File Offset: 0x0004B96C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data that is needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06000EB6 RID: 3766 RVA: 0x0004D778 File Offset: 0x0004B978
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("_BypassOnLocal", this._BypassOnLocal);
			serializationInfo.AddValue("_ProxyAddress", this._ProxyAddress);
			serializationInfo.AddValue("_BypassList", this._BypassList);
			serializationInfo.AddValue("_UseDefaultCredentials", this.UseDefaultCredentials);
			if (this._UseRegistry)
			{
				serializationInfo.AddValue("_UseRegistry", true);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0004D7DD File Offset: 0x0004B9DD
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x0004D7E5 File Offset: 0x0004B9E5
		internal AutoWebProxyScriptEngine ScriptEngine
		{
			get
			{
				return this.m_ScriptEngine;
			}
			set
			{
				this.m_ScriptEngine = value;
			}
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0004D7EE File Offset: 0x0004B9EE
		internal WebProxy(bool enableAutoproxy)
		{
			this.m_EnableAutoproxy = enableAutoproxy;
			this.UnsafeUpdateFromRegistry();
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0004D803 File Offset: 0x0004BA03
		internal void DeleteScriptEngine()
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.Close();
				this.ScriptEngine = null;
			}
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0004D820 File Offset: 0x0004BA20
		internal void UnsafeUpdateFromRegistry()
		{
			this._UseRegistry = true;
			this.ScriptEngine = new AutoWebProxyScriptEngine(this, true);
			WebProxyData webProxyData = this.ScriptEngine.GetWebProxyData();
			this.Update(webProxyData);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0004D854 File Offset: 0x0004BA54
		internal void Update(WebProxyData webProxyData)
		{
			lock (this)
			{
				this._BypassOnLocal = webProxyData.bypassOnLocal;
				this._ProxyAddress = webProxyData.proxyAddress;
				this._ProxyHostAddresses = webProxyData.proxyHostAddresses;
				this._BypassList = webProxyData.bypassList;
				this.ScriptEngine.AutomaticallyDetectSettings = this.m_EnableAutoproxy && webProxyData.automaticallyDetectSettings;
				this.ScriptEngine.AutomaticConfigurationScript = (this.m_EnableAutoproxy ? webProxyData.scriptLocation : null);
			}
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0004D8F4 File Offset: 0x0004BAF4
		ProxyChain IAutoWebProxy.GetProxies(Uri destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			return new ProxyScriptChain(this, destination);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0004D914 File Offset: 0x0004BB14
		private bool GetProxyAuto(Uri destination, out Uri proxyUri)
		{
			proxyUri = null;
			if (this.ScriptEngine == null)
			{
				return false;
			}
			IList<string> list = null;
			if (!this.ScriptEngine.GetProxies(destination, out list))
			{
				return false;
			}
			if (list.Count > 0)
			{
				if (WebProxy.AreAllBypassed(list, true))
				{
					proxyUri = destination;
				}
				else
				{
					proxyUri = WebProxy.ProxyUri(list[0]);
				}
			}
			return true;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0004D968 File Offset: 0x0004BB68
		private bool IsBypassedAuto(Uri destination, out bool isBypassed)
		{
			isBypassed = true;
			if (this.ScriptEngine == null)
			{
				return false;
			}
			IList<string> list;
			if (!this.ScriptEngine.GetProxies(destination, out list))
			{
				return false;
			}
			if (list.Count == 0)
			{
				isBypassed = false;
			}
			else
			{
				isBypassed = WebProxy.AreAllBypassed(list, true);
			}
			return true;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0004D9AC File Offset: 0x0004BBAC
		internal Uri[] GetProxiesAuto(Uri destination, ref int syncStatus)
		{
			if (this.ScriptEngine == null)
			{
				return null;
			}
			IList<string> list = null;
			if (!this.ScriptEngine.GetProxies(destination, out list, ref syncStatus))
			{
				return null;
			}
			Uri[] array;
			if (list.Count == 0)
			{
				array = new Uri[0];
			}
			else if (WebProxy.AreAllBypassed(list, false))
			{
				array = new Uri[1];
			}
			else
			{
				array = new Uri[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					array[i] = WebProxy.ProxyUri(list[i]);
				}
			}
			return array;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0004DA2A File Offset: 0x0004BC2A
		internal void AbortGetProxiesAuto(ref int syncStatus)
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.Abort(ref syncStatus);
			}
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0004DA40 File Offset: 0x0004BC40
		internal Uri GetProxyAutoFailover(Uri destination)
		{
			if (this.IsBypassedManual(destination))
			{
				return null;
			}
			Uri uri = this._ProxyAddress;
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			if (proxyHostAddresses != null)
			{
				uri = proxyHostAddresses[destination.Scheme] as Uri;
			}
			return uri;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0004DA7C File Offset: 0x0004BC7C
		private static bool AreAllBypassed(IEnumerable<string> proxies, bool checkFirstOnly)
		{
			bool flag = true;
			foreach (string text in proxies)
			{
				flag = string.IsNullOrEmpty(text);
				if (checkFirstOnly)
				{
					break;
				}
				if (!flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0004DAD0 File Offset: 0x0004BCD0
		private static Uri ProxyUri(string proxyName)
		{
			if (proxyName != null && proxyName.Length != 0)
			{
				return new Uri("http://" + proxyName);
			}
			return null;
		}

		// Token: 0x04001272 RID: 4722
		private bool _UseRegistry;

		// Token: 0x04001273 RID: 4723
		private bool _BypassOnLocal;

		// Token: 0x04001274 RID: 4724
		private bool m_EnableAutoproxy;

		// Token: 0x04001275 RID: 4725
		private Uri _ProxyAddress;

		// Token: 0x04001276 RID: 4726
		private ArrayList _BypassList;

		// Token: 0x04001277 RID: 4727
		private ICredentials _Credentials;

		// Token: 0x04001278 RID: 4728
		private Regex[] _RegExBypassList;

		// Token: 0x04001279 RID: 4729
		private Hashtable _ProxyHostAddresses;

		// Token: 0x0400127A RID: 4730
		private AutoWebProxyScriptEngine m_ScriptEngine;
	}
}
