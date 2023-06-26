using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace System
{
	/// <summary>Provides a custom constructor for uniform resource identifiers (URIs) and modifies URIs for the <see cref="T:System.Uri" /> class.</summary>
	// Token: 0x0200003E RID: 62
	[global::__DynamicallyInvokable]
	public class UriBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class.</summary>
		// Token: 0x0600039C RID: 924 RVA: 0x00019F5C File Offset: 0x0001815C
		[global::__DynamicallyInvokable]
		public UriBuilder()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified URI.</summary>
		/// <param name="uri">A URI string.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UriFormatException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.  
		///
		///
		///     <paramref name="uri" /> is a zero length string or contains only spaces.  
		///  -or-  
		///  The parsing routine detected a scheme in an invalid form.  
		///  -or-  
		///  The parser detected more than two consecutive slashes in a URI that does not use the "file" scheme.  
		///  -or-  
		///  <paramref name="uri" /> is not a valid URI.</exception>
		// Token: 0x0600039D RID: 925 RVA: 0x00019FD8 File Offset: 0x000181D8
		[global::__DynamicallyInvokable]
		public UriBuilder(string uri)
		{
			Uri uri2 = new Uri(uri, UriKind.RelativeOrAbsolute);
			if (uri2.IsAbsoluteUri)
			{
				this.Init(uri2);
				return;
			}
			uri = Uri.UriSchemeHttp + Uri.SchemeDelimiter + uri;
			this.Init(new Uri(uri));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified <see cref="T:System.Uri" /> instance.</summary>
		/// <param name="uri">An instance of the <see cref="T:System.Uri" /> class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is <see langword="null" />.</exception>
		// Token: 0x0600039E RID: 926 RVA: 0x0001A088 File Offset: 0x00018288
		[global::__DynamicallyInvokable]
		public UriBuilder(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.Init(uri);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001A118 File Offset: 0x00018318
		private void Init(Uri uri)
		{
			this.m_fragment = uri.Fragment;
			this.m_query = uri.Query;
			this.m_host = uri.Host;
			this.m_path = uri.AbsolutePath;
			this.m_port = uri.Port;
			this.m_scheme = uri.Scheme;
			this.m_schemeDelimiter = (uri.HasAuthority ? Uri.SchemeDelimiter : ":");
			string userInfo = uri.UserInfo;
			if (!string.IsNullOrEmpty(userInfo))
			{
				int num = userInfo.IndexOf(':');
				if (num != -1)
				{
					this.m_password = userInfo.Substring(num + 1);
					this.m_username = userInfo.Substring(0, num);
				}
				else
				{
					this.m_username = userInfo;
				}
			}
			this.SetFieldsFromUri(uri);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme and host.</summary>
		/// <param name="schemeName">An Internet access protocol.</param>
		/// <param name="hostName">A DNS-style domain name or IP address.</param>
		// Token: 0x060003A0 RID: 928 RVA: 0x0001A1D0 File Offset: 0x000183D0
		[global::__DynamicallyInvokable]
		public UriBuilder(string schemeName, string hostName)
		{
			this.Scheme = schemeName;
			this.Host = hostName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, and port.</summary>
		/// <param name="scheme">An Internet access protocol.</param>
		/// <param name="host">A DNS-style domain name or IP address.</param>
		/// <param name="portNumber">An IP port number for the service.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="portNumber" /> is less than -1 or greater than 65,535.</exception>
		// Token: 0x060003A1 RID: 929 RVA: 0x0001A257 File Offset: 0x00018457
		[global::__DynamicallyInvokable]
		public UriBuilder(string scheme, string host, int portNumber)
			: this(scheme, host)
		{
			this.Port = portNumber;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, port number, and path.</summary>
		/// <param name="scheme">An Internet access protocol.</param>
		/// <param name="host">A DNS-style domain name or IP address.</param>
		/// <param name="port">An IP port number for the service.</param>
		/// <param name="pathValue">The path to the Internet resource.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than -1 or greater than 65,535.</exception>
		// Token: 0x060003A2 RID: 930 RVA: 0x0001A268 File Offset: 0x00018468
		[global::__DynamicallyInvokable]
		public UriBuilder(string scheme, string host, int port, string pathValue)
			: this(scheme, host, port)
		{
			this.Path = pathValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, port number, path and query string or fragment identifier.</summary>
		/// <param name="scheme">An Internet access protocol.</param>
		/// <param name="host">A DNS-style domain name or IP address.</param>
		/// <param name="port">An IP port number for the service.</param>
		/// <param name="path">The path to the Internet resource.</param>
		/// <param name="extraValue">A query string or fragment identifier.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="extraValue" /> is neither <see langword="null" /> nor <see cref="F:System.String.Empty" />, nor does a valid fragment identifier begin with a number sign (#), nor a valid query string begin with a question mark (?).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than -1 or greater than 65,535.</exception>
		// Token: 0x060003A3 RID: 931 RVA: 0x0001A27C File Offset: 0x0001847C
		[global::__DynamicallyInvokable]
		public UriBuilder(string scheme, string host, int port, string path, string extraValue)
			: this(scheme, host, port, path)
		{
			try
			{
				this.Extra = extraValue;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new ArgumentException("extraValue");
			}
		}

		// Token: 0x17000077 RID: 119
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0001A2D4 File Offset: 0x000184D4
		private string Extra
		{
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length <= 0)
				{
					this.Fragment = string.Empty;
					this.Query = string.Empty;
					return;
				}
				if (value[0] == '#')
				{
					this.Fragment = value.Substring(1);
					return;
				}
				if (value[0] == '?')
				{
					int num = value.IndexOf('#');
					if (num == -1)
					{
						num = value.Length;
					}
					else
					{
						this.Fragment = value.Substring(num + 1);
					}
					this.Query = value.Substring(1, num - 1);
					return;
				}
				throw new ArgumentException("value");
			}
		}

		/// <summary>Gets or sets the fragment portion of the URI.</summary>
		/// <returns>The fragment portion of the URI. The fragment identifier ("#") is added to the beginning of the fragment.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001A36F File Offset: 0x0001856F
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0001A377 File Offset: 0x00018577
		[global::__DynamicallyInvokable]
		public string Fragment
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_fragment;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length > 0)
				{
					value = "#" + value;
				}
				this.m_fragment = value;
				this.m_changed = true;
			}
		}

		/// <summary>Gets or sets the Domain Name System (DNS) host name or IP address of a server.</summary>
		/// <returns>The DNS host name or IP address of the server.</returns>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0001A3A7 File Offset: 0x000185A7
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0001A3B0 File Offset: 0x000185B0
		[global::__DynamicallyInvokable]
		public string Host
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_host;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.m_host = value;
				if (this.m_host.IndexOf(':') >= 0 && this.m_host[0] != '[')
				{
					this.m_host = "[" + this.m_host + "]";
				}
				this.m_changed = true;
			}
		}

		/// <summary>Gets or sets the password associated with the user that accesses the URI.</summary>
		/// <returns>The password of the user that accesses the URI.</returns>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0001A410 File Offset: 0x00018610
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0001A418 File Offset: 0x00018618
		[global::__DynamicallyInvokable]
		public string Password
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_password;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.m_password = value;
				this.m_changed = true;
			}
		}

		/// <summary>Gets or sets the path to the resource referenced by the URI.</summary>
		/// <returns>The path to the resource referenced by the URI.</returns>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0001A432 File Offset: 0x00018632
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0001A43A File Offset: 0x0001863A
		[global::__DynamicallyInvokable]
		public string Path
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_path;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (value == null || value.Length == 0)
				{
					value = "/";
				}
				this.m_path = Uri.InternalEscapeString(this.ConvertSlashes(value));
				this.m_changed = true;
			}
		}

		/// <summary>Gets or sets the port number of the URI.</summary>
		/// <returns>The port number of the URI.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port cannot be set to a value less than -1 or greater than 65,535.</exception>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0001A467 File Offset: 0x00018667
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0001A46F File Offset: 0x0001866F
		[global::__DynamicallyInvokable]
		public int Port
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_port;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (value < -1 || value > 65535)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_port = value;
				this.m_changed = true;
			}
		}

		/// <summary>Gets or sets any query information included in the URI.</summary>
		/// <returns>The query information included in the URI.</returns>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0001A496 File Offset: 0x00018696
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0001A49E File Offset: 0x0001869E
		[global::__DynamicallyInvokable]
		public string Query
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_query;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length > 0)
				{
					value = "?" + value;
				}
				this.m_query = value;
				this.m_changed = true;
			}
		}

		/// <summary>Gets or sets the scheme name of the URI.</summary>
		/// <returns>The scheme of the URI.</returns>
		/// <exception cref="T:System.ArgumentException">The scheme cannot be set to an invalid scheme name.</exception>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0001A4CE File Offset: 0x000186CE
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0001A4D8 File Offset: 0x000186D8
		[global::__DynamicallyInvokable]
		public string Scheme
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_scheme;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				int num = value.IndexOf(':');
				if (num != -1)
				{
					value = value.Substring(0, num);
				}
				if (value.Length != 0)
				{
					if (!Uri.CheckSchemeName(value))
					{
						throw new ArgumentException("value");
					}
					value = value.ToLower(CultureInfo.InvariantCulture);
				}
				this.m_scheme = value;
				this.m_changed = true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Uri" /> instance constructed by the specified <see cref="T:System.UriBuilder" /> instance.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI constructed by the <see cref="T:System.UriBuilder" />.</returns>
		/// <exception cref="T:System.UriFormatException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.  
		///
		///
		///
		///
		///  The URI constructed by the <see cref="T:System.UriBuilder" /> properties is invalid.</exception>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0001A53C File Offset: 0x0001873C
		[global::__DynamicallyInvokable]
		public Uri Uri
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this.m_changed)
				{
					this.m_uri = new Uri(this.ToString());
					this.SetFieldsFromUri(this.m_uri);
					this.m_changed = false;
				}
				return this.m_uri;
			}
		}

		/// <summary>The user name associated with the user that accesses the URI.</summary>
		/// <returns>The user name of the user that accesses the URI.</returns>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0001A570 File Offset: 0x00018770
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0001A578 File Offset: 0x00018778
		[global::__DynamicallyInvokable]
		public string UserName
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_username;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.m_username = value;
				this.m_changed = true;
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001A594 File Offset: 0x00018794
		private string ConvertSlashes(string path)
		{
			StringBuilder stringBuilder = new StringBuilder(path.Length);
			foreach (char c in path)
			{
				if (c == '\\')
				{
					c = '/';
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Compares an existing <see cref="T:System.Uri" /> instance with the contents of the <see cref="T:System.UriBuilder" /> for equality.</summary>
		/// <param name="rparam">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="rparam" /> represents the same <see cref="T:System.Uri" /> as the <see cref="T:System.Uri" /> constructed by this <see cref="T:System.UriBuilder" /> instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003B7 RID: 951 RVA: 0x0001A5DC File Offset: 0x000187DC
		[global::__DynamicallyInvokable]
		public override bool Equals(object rparam)
		{
			return rparam != null && this.Uri.Equals(rparam.ToString());
		}

		/// <summary>Returns the hash code for the URI.</summary>
		/// <returns>The hash code generated for the URI.</returns>
		// Token: 0x060003B8 RID: 952 RVA: 0x0001A5F4 File Offset: 0x000187F4
		[global::__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.Uri.GetHashCode();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001A604 File Offset: 0x00018804
		private void SetFieldsFromUri(Uri uri)
		{
			this.m_fragment = uri.Fragment;
			this.m_query = uri.Query;
			this.m_host = uri.Host;
			this.m_path = uri.AbsolutePath;
			this.m_port = uri.Port;
			this.m_scheme = uri.Scheme;
			this.m_schemeDelimiter = (uri.HasAuthority ? Uri.SchemeDelimiter : ":");
			string userInfo = uri.UserInfo;
			if (userInfo.Length > 0)
			{
				int num = userInfo.IndexOf(':');
				if (num != -1)
				{
					this.m_password = userInfo.Substring(num + 1);
					this.m_username = userInfo.Substring(0, num);
					return;
				}
				this.m_username = userInfo;
			}
		}

		/// <summary>Returns the display string for the specified <see cref="T:System.UriBuilder" /> instance.</summary>
		/// <returns>The string that contains the unescaped display string of the <see cref="T:System.UriBuilder" />.</returns>
		/// <exception cref="T:System.UriFormatException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.  
		///
		///
		///
		///
		///  The <see cref="T:System.UriBuilder" /> instance has a bad password.</exception>
		// Token: 0x060003BA RID: 954 RVA: 0x0001A6B8 File Offset: 0x000188B8
		[global::__DynamicallyInvokable]
		public override string ToString()
		{
			if (this.m_username.Length == 0 && this.m_password.Length > 0)
			{
				throw new UriFormatException(SR.GetString("net_uri_BadUserPassword"));
			}
			if (this.m_scheme.Length != 0)
			{
				UriParser syntax = UriParser.GetSyntax(this.m_scheme);
				if (syntax != null)
				{
					this.m_schemeDelimiter = ((syntax.InFact(UriSyntaxFlags.MustHaveAuthority) || (this.m_host.Length != 0 && syntax.NotAny(UriSyntaxFlags.MailToLikeUri) && syntax.InFact(UriSyntaxFlags.OptionalAuthority))) ? Uri.SchemeDelimiter : ":");
				}
				else
				{
					this.m_schemeDelimiter = ((this.m_host.Length != 0) ? Uri.SchemeDelimiter : ":");
				}
			}
			string text = ((this.m_scheme.Length != 0) ? (this.m_scheme + this.m_schemeDelimiter) : string.Empty);
			return string.Concat(new string[]
			{
				text,
				this.m_username,
				(this.m_password.Length > 0) ? (":" + this.m_password) : string.Empty,
				(this.m_username.Length > 0) ? "@" : string.Empty,
				this.m_host,
				(this.m_port != -1 && this.m_host.Length > 0) ? (":" + this.m_port.ToString()) : string.Empty,
				(this.m_host.Length > 0 && this.m_path.Length != 0 && this.m_path[0] != '/') ? "/" : string.Empty,
				this.m_path,
				this.m_query,
				this.m_fragment
			});
		}

		// Token: 0x04000432 RID: 1074
		private bool m_changed = true;

		// Token: 0x04000433 RID: 1075
		private string m_fragment = string.Empty;

		// Token: 0x04000434 RID: 1076
		private string m_host = "localhost";

		// Token: 0x04000435 RID: 1077
		private string m_password = string.Empty;

		// Token: 0x04000436 RID: 1078
		private string m_path = "/";

		// Token: 0x04000437 RID: 1079
		private int m_port = -1;

		// Token: 0x04000438 RID: 1080
		private string m_query = string.Empty;

		// Token: 0x04000439 RID: 1081
		private string m_scheme = "http";

		// Token: 0x0400043A RID: 1082
		private string m_schemeDelimiter = Uri.SchemeDelimiter;

		// Token: 0x0400043B RID: 1083
		private Uri m_uri;

		// Token: 0x0400043C RID: 1084
		private string m_username = string.Empty;
	}
}
