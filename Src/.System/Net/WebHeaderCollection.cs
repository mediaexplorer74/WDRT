using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Net
{
	/// <summary>Contains protocol headers associated with a request or response.</summary>
	// Token: 0x02000182 RID: 386
	[ComVisible(true)]
	[global::__DynamicallyInvokable]
	[Serializable]
	public class WebHeaderCollection : NameValueCollection, ISerializable
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x00049A4F File Offset: 0x00047C4F
		internal string ContentLength
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[1]);
				}
				return this.m_CommonHeaders[1];
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00049A6F File Offset: 0x00047C6F
		internal string CacheControl
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[2]);
				}
				return this.m_CommonHeaders[2];
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x00049A8F File Offset: 0x00047C8F
		internal string ContentType
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[3]);
				}
				return this.m_CommonHeaders[3];
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00049AAF File Offset: 0x00047CAF
		internal string Date
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[4]);
				}
				return this.m_CommonHeaders[4];
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x00049ACF File Offset: 0x00047CCF
		internal string Expires
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[5]);
				}
				return this.m_CommonHeaders[5];
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00049AEF File Offset: 0x00047CEF
		internal string ETag
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[6]);
				}
				return this.m_CommonHeaders[6];
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00049B0F File Offset: 0x00047D0F
		internal string LastModified
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[7]);
				}
				return this.m_CommonHeaders[7];
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x00049B30 File Offset: 0x00047D30
		internal string Location
		{
			get
			{
				string text = ((this.m_CommonHeaders != null) ? this.m_CommonHeaders[8] : this.Get(WebHeaderCollection.s_CommonHeaderNames[8]));
				return WebHeaderCollection.HeaderEncoding.DecodeUtf8FromString(text);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00049B63 File Offset: 0x00047D63
		internal string ProxyAuthenticate
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[9]);
				}
				return this.m_CommonHeaders[9];
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00049B85 File Offset: 0x00047D85
		internal string SetCookie2
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[11]);
				}
				return this.m_CommonHeaders[11];
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00049BA7 File Offset: 0x00047DA7
		internal string SetCookie
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[12]);
				}
				return this.m_CommonHeaders[12];
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00049BC9 File Offset: 0x00047DC9
		internal string Server
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[13]);
				}
				return this.m_CommonHeaders[13];
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00049BEB File Offset: 0x00047DEB
		internal string Via
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[14]);
				}
				return this.m_CommonHeaders[14];
			}
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00049C10 File Offset: 0x00047E10
		private void NormalizeCommonHeaders()
		{
			if (this.m_CommonHeaders == null)
			{
				return;
			}
			for (int i = 0; i < this.m_CommonHeaders.Length; i++)
			{
				if (this.m_CommonHeaders[i] != null)
				{
					this.InnerCollection.Add(WebHeaderCollection.s_CommonHeaderNames[i], this.m_CommonHeaders[i]);
				}
			}
			this.m_CommonHeaders = null;
			this.m_NumCommonHeaders = 0;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x00049C6B File Offset: 0x00047E6B
		private NameValueCollection InnerCollection
		{
			get
			{
				if (this.m_InnerCollection == null)
				{
					this.m_InnerCollection = new NameValueCollection(16, CaseInsensitiveAscii.StaticInstance);
				}
				return this.m_InnerCollection;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x00049C8D File Offset: 0x00047E8D
		private bool AllowHttpRequestHeader
		{
			get
			{
				if (this.m_Type == WebHeaderCollectionType.Unknown)
				{
					this.m_Type = WebHeaderCollectionType.WebRequest;
				}
				return this.m_Type == WebHeaderCollectionType.WebRequest || this.m_Type == WebHeaderCollectionType.HttpWebRequest || this.m_Type == WebHeaderCollectionType.HttpListenerRequest;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x00049CBB File Offset: 0x00047EBB
		internal bool AllowHttpResponseHeader
		{
			get
			{
				if (this.m_Type == WebHeaderCollectionType.Unknown)
				{
					this.m_Type = WebHeaderCollectionType.WebResponse;
				}
				return this.m_Type == WebHeaderCollectionType.WebResponse || this.m_Type == WebHeaderCollectionType.HttpWebResponse || this.m_Type == WebHeaderCollectionType.HttpListenerResponse;
			}
		}

		/// <summary>Gets or sets the specified request header.</summary>
		/// <param name="header">The request header value.</param>
		/// <returns>A <see cref="T:System.String" /> instance containing the specified header value.</returns>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />.</exception>
		// Token: 0x17000327 RID: 807
		[global::__DynamicallyInvokable]
		public string this[HttpRequestHeader header]
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (!this.AllowHttpRequestHeader)
				{
					throw new InvalidOperationException(SR.GetString("net_headers_req"));
				}
				return base[UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header)];
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (!this.AllowHttpRequestHeader)
				{
					throw new InvalidOperationException(SR.GetString("net_headers_req"));
				}
				base[UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header)] = value;
			}
		}

		/// <summary>Gets or sets the specified response header.</summary>
		/// <param name="header">The response header value.</param>
		/// <returns>A <see cref="T:System.String" /> instance containing the specified header.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />.</exception>
		// Token: 0x17000328 RID: 808
		[global::__DynamicallyInvokable]
		public string this[HttpResponseHeader header]
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (!this.AllowHttpResponseHeader)
				{
					throw new InvalidOperationException(SR.GetString("net_headers_rsp"));
				}
				if (this.m_CommonHeaders != null)
				{
					if (header == HttpResponseHeader.ProxyAuthenticate)
					{
						return this.m_CommonHeaders[9];
					}
					if (header == HttpResponseHeader.WwwAuthenticate)
					{
						return this.m_CommonHeaders[15];
					}
				}
				return base[UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header)];
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (!this.AllowHttpResponseHeader)
				{
					throw new InvalidOperationException(SR.GetString("net_headers_rsp"));
				}
				if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.GetString("net_headers_toolong", new object[] { ushort.MaxValue }));
				}
				base[UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header)] = value;
			}
		}

		/// <summary>Inserts the specified header with the specified value into the collection.</summary>
		/// <param name="header">The header to add to the collection.</param>
		/// <param name="value">The content of the header.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />.</exception>
		// Token: 0x06000E2A RID: 3626 RVA: 0x00049E08 File Offset: 0x00048008
		public void Add(HttpRequestHeader header, string value)
		{
			if (!this.AllowHttpRequestHeader)
			{
				throw new InvalidOperationException(SR.GetString("net_headers_req"));
			}
			this.Add(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Inserts the specified header with the specified value into the collection.</summary>
		/// <param name="header">The header to add to the collection.</param>
		/// <param name="value">The content of the header.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />.</exception>
		// Token: 0x06000E2B RID: 3627 RVA: 0x00049E30 File Offset: 0x00048030
		public void Add(HttpResponseHeader header, string value)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(SR.GetString("net_headers_rsp"));
			}
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("net_headers_toolong", new object[] { ushort.MaxValue }));
			}
			this.Add(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpRequestHeader" /> value to set.</param>
		/// <param name="value">The content of the header to set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />.</exception>
		// Token: 0x06000E2C RID: 3628 RVA: 0x00049EA4 File Offset: 0x000480A4
		public void Set(HttpRequestHeader header, string value)
		{
			if (!this.AllowHttpRequestHeader)
			{
				throw new InvalidOperationException(SR.GetString("net_headers_req"));
			}
			this.Set(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpResponseHeader" /> value to set.</param>
		/// <param name="value">The content of the header to set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />.</exception>
		// Token: 0x06000E2D RID: 3629 RVA: 0x00049ECC File Offset: 0x000480CC
		public void Set(HttpResponseHeader header, string value)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(SR.GetString("net_headers_rsp"));
			}
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("net_headers_toolong", new object[] { ushort.MaxValue }));
			}
			this.Set(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header), value);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00049F40 File Offset: 0x00048140
		internal void SetInternal(HttpResponseHeader header, string value)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(SR.GetString("net_headers_rsp"));
			}
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("net_headers_toolong", new object[] { ushort.MaxValue }));
			}
			this.SetInternal(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpRequestHeader" /> instance to remove from the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />.</exception>
		// Token: 0x06000E2F RID: 3631 RVA: 0x00049FB4 File Offset: 0x000481B4
		public void Remove(HttpRequestHeader header)
		{
			if (!this.AllowHttpRequestHeader)
			{
				throw new InvalidOperationException(SR.GetString("net_headers_req"));
			}
			this.Remove(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header));
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpResponseHeader" /> instance to remove from the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />.</exception>
		// Token: 0x06000E30 RID: 3632 RVA: 0x00049FDA File Offset: 0x000481DA
		public void Remove(HttpResponseHeader header)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(SR.GetString("net_headers_rsp"));
			}
			this.Remove(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header));
		}

		/// <summary>Inserts a header into the collection without checking whether the header is on the restricted header list.</summary>
		/// <param name="headerName">The header to add to the collection.</param>
		/// <param name="headerValue">The content of the header.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> is <see langword="null" />, <see cref="F:System.String.Empty" />, or contains invalid characters.  
		/// -or-  
		/// <paramref name="headerValue" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="headerName" /> is not <see langword="null" /> and the length of <paramref name="headerValue" /> is too long (greater than 65,535 characters).</exception>
		// Token: 0x06000E31 RID: 3633 RVA: 0x0004A000 File Offset: 0x00048200
		protected void AddWithoutValidate(string headerName, string headerValue)
		{
			headerName = WebHeaderCollection.CheckBadChars(headerName, false);
			headerValue = WebHeaderCollection.CheckBadChars(headerValue, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && headerValue != null && headerValue.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("headerValue", headerValue, SR.GetString("net_headers_toolong", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(headerName, headerValue);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0004A07C File Offset: 0x0004827C
		internal void SetAddVerified(string name, string value)
		{
			if (WebHeaderCollection.HInfo[name].AllowMultiValues)
			{
				this.NormalizeCommonHeaders();
				base.InvalidateCachedArrays();
				this.InnerCollection.Add(name, value);
				return;
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0004A0CE File Offset: 0x000482CE
		internal void AddInternal(string name, string value)
		{
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0004A0E9 File Offset: 0x000482E9
		internal void ChangeInternal(string name, string value)
		{
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0004A104 File Offset: 0x00048304
		internal void RemoveInternal(string name)
		{
			this.NormalizeCommonHeaders();
			if (this.m_InnerCollection != null)
			{
				base.InvalidateCachedArrays();
				this.m_InnerCollection.Remove(name);
			}
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0004A126 File Offset: 0x00048326
		internal void CheckUpdate(string name, string value)
		{
			value = WebHeaderCollection.CheckBadChars(value, true);
			this.ChangeInternal(name, value);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0004A139 File Offset: 0x00048339
		private void AddInternalNotCommon(string name, string value)
		{
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0004A150 File Offset: 0x00048350
		internal static string CheckBadChars(string name, bool isHeaderValue)
		{
			if (name != null && name.Length != 0)
			{
				if (isHeaderValue)
				{
					name = name.Trim(WebHeaderCollection.HttpTrimCharacters);
					int num = 0;
					for (int i = 0; i < name.Length; i++)
					{
						char c = 'ÿ' & name[i];
						switch (num)
						{
						case 0:
							if (c == '\r')
							{
								num = 1;
							}
							else if (c == '\n')
							{
								num = 2;
							}
							else if (c == '\u007f' || (c < ' ' && c != '\t'))
							{
								throw new ArgumentException(SR.GetString("net_WebHeaderInvalidControlChars"), "value");
							}
							break;
						case 1:
							if (c != '\n')
							{
								throw new ArgumentException(SR.GetString("net_WebHeaderInvalidCRLFChars"), "value");
							}
							num = 2;
							break;
						case 2:
							if (c != ' ' && c != '\t')
							{
								throw new ArgumentException(SR.GetString("net_WebHeaderInvalidCRLFChars"), "value");
							}
							num = 0;
							break;
						}
					}
					if (num != 0)
					{
						throw new ArgumentException(SR.GetString("net_WebHeaderInvalidCRLFChars"), "value");
					}
				}
				else
				{
					if (name.IndexOfAny(ValidationHelper.InvalidParamChars) != -1)
					{
						throw new ArgumentException(SR.GetString("net_WebHeaderInvalidHeaderChars"), "name");
					}
					if (WebHeaderCollection.ContainsNonAsciiChars(name))
					{
						throw new ArgumentException(SR.GetString("net_WebHeaderInvalidNonAsciiChars"), "name");
					}
				}
				return name;
			}
			if (!isHeaderValue)
			{
				throw (name == null) ? new ArgumentNullException("name") : new ArgumentException(SR.GetString("net_emptystringcall", new object[] { "name" }), "name");
			}
			return string.Empty;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0004A2C2 File Offset: 0x000484C2
		internal static bool IsValidToken(string token)
		{
			return token.Length > 0 && token.IndexOfAny(ValidationHelper.InvalidParamChars) == -1 && !WebHeaderCollection.ContainsNonAsciiChars(token);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0004A2E8 File Offset: 0x000484E8
		internal static bool ContainsNonAsciiChars(string token)
		{
			for (int i = 0; i < token.Length; i++)
			{
				if (token[i] < ' ' || token[i] > '~')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0004A320 File Offset: 0x00048520
		internal void ThrowOnRestrictedHeader(string headerName)
		{
			if (this.m_Type == WebHeaderCollectionType.HttpWebRequest)
			{
				if (WebHeaderCollection.HInfo[headerName].IsRequestRestricted)
				{
					throw new ArgumentException(SR.GetString("net_headerrestrict", new object[] { headerName }), "name");
				}
			}
			else if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && WebHeaderCollection.HInfo[headerName].IsResponseRestricted)
			{
				throw new ArgumentException(SR.GetString("net_headerrestrict", new object[] { headerName }), "name");
			}
		}

		/// <summary>Inserts a header with the specified name and value into the collection.</summary>
		/// <param name="name">The header to add to the collection.</param>
		/// <param name="value">The content of the header.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is <see langword="null" />, <see cref="F:System.String.Empty" />, or contains invalid characters.  
		/// -or-  
		/// <paramref name="name" /> is a restricted header that must be set with a property setting.  
		/// -or-  
		/// <paramref name="value" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		// Token: 0x06000E3C RID: 3644 RVA: 0x0004A3A4 File Offset: 0x000485A4
		public override void Add(string name, string value)
		{
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.ThrowOnRestrictedHeader(name);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("net_headers_toolong", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		/// <summary>Inserts the specified header into the collection.</summary>
		/// <param name="header">The header to add, with the name and value separated by a colon.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="header" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="header" /> does not contain a colon (:) character.  
		/// The length of <paramref name="value" /> is greater than 65535.  
		/// -or-  
		/// The name part of <paramref name="header" /> is <see cref="F:System.String.Empty" /> or contains invalid characters.  
		/// -or-  
		/// <paramref name="header" /> is a restricted header that should be set with a property.  
		/// -or-  
		/// The value part of <paramref name="header" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length the string after the colon (:) is greater than 65535.</exception>
		// Token: 0x06000E3D RID: 3645 RVA: 0x0004A428 File Offset: 0x00048628
		public void Add(string header)
		{
			if (ValidationHelper.IsBlankString(header))
			{
				throw new ArgumentNullException("header");
			}
			int num = header.IndexOf(':');
			if (num < 0)
			{
				throw new ArgumentException(SR.GetString("net_WebHeaderMissingColon"), "header");
			}
			string text = header.Substring(0, num);
			string text2 = header.Substring(num + 1);
			text = WebHeaderCollection.CheckBadChars(text, false);
			this.ThrowOnRestrictedHeader(text);
			text2 = WebHeaderCollection.CheckBadChars(text2, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && text2 != null && text2.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", text2, SR.GetString("net_headers_toolong", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(text, text2);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="name">The header to set.</param>
		/// <param name="value">The content of the header to set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a restricted header.  
		/// -or-  
		/// <paramref name="name" /> or <paramref name="value" /> contain invalid characters.</exception>
		// Token: 0x06000E3E RID: 3646 RVA: 0x0004A4F0 File Offset: 0x000486F0
		public override void Set(string name, string value)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.ThrowOnRestrictedHeader(name);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("net_headers_toolong", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0004A584 File Offset: 0x00048784
		internal void SetInternal(string name, string value)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			name = WebHeaderCollection.CheckBadChars(name, false);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("net_headers_toolong", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="name">The name of the header to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" /><see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a restricted header.  
		/// -or-  
		/// <paramref name="name" /> contains invalid characters.</exception>
		// Token: 0x06000E40 RID: 3648 RVA: 0x0004A614 File Offset: 0x00048814
		[global::__DynamicallyInvokable]
		public override void Remove(string name)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			this.ThrowOnRestrictedHeader(name);
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.NormalizeCommonHeaders();
			if (this.m_InnerCollection != null)
			{
				base.InvalidateCachedArrays();
				this.m_InnerCollection.Remove(name);
			}
		}

		/// <summary>Gets an array of header values stored in a header.</summary>
		/// <param name="header">The header to return.</param>
		/// <returns>An array of header strings.</returns>
		// Token: 0x06000E41 RID: 3649 RVA: 0x0004A664 File Offset: 0x00048864
		public override string[] GetValues(string header)
		{
			this.NormalizeCommonHeaders();
			HeaderInfo headerInfo = WebHeaderCollection.HInfo[header];
			string[] values = this.InnerCollection.GetValues(header);
			if (headerInfo == null || values == null || !headerInfo.AllowMultiValues)
			{
				return values;
			}
			ArrayList arrayList = null;
			for (int i = 0; i < values.Length; i++)
			{
				string[] array = headerInfo.Parser(values[i]);
				if (arrayList == null)
				{
					if (array.Length > 1)
					{
						arrayList = new ArrayList(values);
						arrayList.RemoveRange(i, values.Length - i);
						arrayList.AddRange(array);
					}
				}
				else
				{
					arrayList.AddRange(array);
				}
			}
			if (arrayList != null)
			{
				string[] array2 = new string[arrayList.Count];
				arrayList.CopyTo(array2);
				return array2;
			}
			return values;
		}

		/// <summary>This method is obsolete.</summary>
		/// <returns>The <see cref="T:System.String" /> representation of the collection.</returns>
		// Token: 0x06000E42 RID: 3650 RVA: 0x0004A710 File Offset: 0x00048910
		[global::__DynamicallyInvokable]
		public override string ToString()
		{
			return WebHeaderCollection.GetAsString(this, false, false);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0004A727 File Offset: 0x00048927
		internal string ToString(bool forTrace)
		{
			return WebHeaderCollection.GetAsString(this, false, true);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0004A734 File Offset: 0x00048934
		internal static string GetAsString(NameValueCollection cc, bool winInetCompat, bool forTrace)
		{
			if (cc == null || cc.Count == 0)
			{
				return "\r\n";
			}
			StringBuilder stringBuilder = new StringBuilder(30 * cc.Count);
			string text = cc[string.Empty];
			if (text != null)
			{
				stringBuilder.Append(text).Append("\r\n");
			}
			for (int i = 0; i < cc.Count; i++)
			{
				string key = cc.GetKey(i);
				string text2 = cc.Get(i);
				if (!ValidationHelper.IsBlankString(key))
				{
					stringBuilder.Append(key);
					if (winInetCompat)
					{
						stringBuilder.Append(':');
					}
					else
					{
						stringBuilder.Append(": ");
					}
					stringBuilder.Append(text2).Append("\r\n");
				}
			}
			if (!forTrace)
			{
				stringBuilder.Append("\r\n");
			}
			return stringBuilder.ToString();
		}

		/// <summary>Converts the <see cref="T:System.Net.WebHeaderCollection" /> to a byte array.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array holding the header collection.</returns>
		// Token: 0x06000E45 RID: 3653 RVA: 0x0004A7F8 File Offset: 0x000489F8
		public byte[] ToByteArray()
		{
			string text = this.ToString();
			return WebHeaderCollection.HeaderEncoding.GetBytes(text);
		}

		/// <summary>Tests whether the specified HTTP header can be set for the request.</summary>
		/// <param name="headerName">The header to test.</param>
		/// <returns>
		///   <see langword="true" /> if the header is restricted; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters.</exception>
		// Token: 0x06000E46 RID: 3654 RVA: 0x0004A814 File Offset: 0x00048A14
		public static bool IsRestricted(string headerName)
		{
			return WebHeaderCollection.IsRestricted(headerName, false);
		}

		/// <summary>Tests whether the specified HTTP header can be set for the request or the response.</summary>
		/// <param name="headerName">The header to test.</param>
		/// <param name="response">Does the Framework test the response or the request?</param>
		/// <returns>
		///   <see langword="true" /> if the header is restricted; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters.</exception>
		// Token: 0x06000E47 RID: 3655 RVA: 0x0004A81D File Offset: 0x00048A1D
		public static bool IsRestricted(string headerName, bool response)
		{
			if (!response)
			{
				return WebHeaderCollection.HInfo[WebHeaderCollection.CheckBadChars(headerName, false)].IsRequestRestricted;
			}
			return WebHeaderCollection.HInfo[WebHeaderCollection.CheckBadChars(headerName, false)].IsResponseRestricted;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebHeaderCollection" /> class.</summary>
		// Token: 0x06000E48 RID: 3656 RVA: 0x0004A84F File Offset: 0x00048A4F
		[global::__DynamicallyInvokable]
		public WebHeaderCollection()
			: base(DBNull.Value)
		{
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0004A85C File Offset: 0x00048A5C
		internal WebHeaderCollection(WebHeaderCollectionType type)
			: base(DBNull.Value)
		{
			this.m_Type = type;
			if (type == WebHeaderCollectionType.HttpWebResponse)
			{
				this.m_CommonHeaders = new string[WebHeaderCollection.s_CommonHeaderNames.Length - 1];
			}
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0004A888 File Offset: 0x00048A88
		internal WebHeaderCollection(NameValueCollection cc)
			: base(DBNull.Value)
		{
			this.m_InnerCollection = new NameValueCollection(cc.Count + 2, CaseInsensitiveAscii.StaticInstance);
			int count = cc.Count;
			for (int i = 0; i < count; i++)
			{
				string key = cc.GetKey(i);
				string[] values = cc.GetValues(i);
				if (values != null)
				{
					for (int j = 0; j < values.Length; j++)
					{
						this.InnerCollection.Add(key, values[j]);
					}
				}
				else
				{
					this.InnerCollection.Add(key, null);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebHeaderCollection" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> containing the information required to serialize the <see cref="T:System.Net.WebHeaderCollection" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> containing the source of the serialized stream associated with the new <see cref="T:System.Net.WebHeaderCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is a null reference or <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x06000E4B RID: 3659 RVA: 0x0004A910 File Offset: 0x00048B10
		protected WebHeaderCollection(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(DBNull.Value)
		{
			int @int = serializationInfo.GetInt32("Count");
			this.m_InnerCollection = new NameValueCollection(@int + 2, CaseInsensitiveAscii.StaticInstance);
			for (int i = 0; i < @int; i++)
			{
				string @string = serializationInfo.GetString(i.ToString(NumberFormatInfo.InvariantInfo));
				string string2 = serializationInfo.GetString((i + @int).ToString(NumberFormatInfo.InvariantInfo));
				this.InnerCollection.Add(@string, string2);
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x06000E4C RID: 3660 RVA: 0x0004A98B File Offset: 0x00048B8B
		public override void OnDeserialization(object sender)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06000E4D RID: 3661 RVA: 0x0004A990 File Offset: 0x00048B90
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.NormalizeCommonHeaders();
			serializationInfo.AddValue("Count", this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				serializationInfo.AddValue(i.ToString(NumberFormatInfo.InvariantInfo), this.GetKey(i));
				serializationInfo.AddValue((i + this.Count).ToString(NumberFormatInfo.InvariantInfo), this.Get(i));
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0004AA00 File Offset: 0x00048C00
		internal unsafe DataParseStatus ParseHeaders(byte[] buffer, int size, ref int unparsed, ref int totalResponseHeadersLength, int maximumResponseHeadersLength, ref WebParseError parseError)
		{
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			if (buffer.Length < size)
			{
				return DataParseStatus.NeedMoreData;
			}
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int i = unparsed;
			int num4 = totalResponseHeadersLength;
			WebParseErrorCode webParseErrorCode = WebParseErrorCode.Generic;
			for (;;)
			{
				string text = string.Empty;
				string text2 = string.Empty;
				bool flag = false;
				string text3 = null;
				if (this.Count == 0)
				{
					while (i < size)
					{
						char c = (char)ptr[i];
						if (c != ' ' && c != '\t')
						{
							break;
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_6;
						}
					}
					if (i == size)
					{
						goto Block_7;
					}
				}
				int num5 = i;
				while (i < size)
				{
					char c = (char)ptr[i];
					if (c != ':' && c != '\n')
					{
						if (c > ' ')
						{
							num = i;
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_12;
						}
					}
					else
					{
						if (c != ':')
						{
							break;
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_15;
						}
						break;
					}
				}
				if (i == size)
				{
					goto Block_16;
				}
				int num6;
				for (;;)
				{
					num6 = ((this.Count == 0 && num < 0) ? 1 : 0);
					char c;
					while (i < size && num6 < 2)
					{
						c = (char)ptr[i];
						if (c > ' ')
						{
							break;
						}
						if (c == '\n')
						{
							num6++;
							if (num6 == 1)
							{
								if (i + 1 == size)
								{
									goto Block_21;
								}
								flag = ptr[i + 1] == 32 || ptr[i + 1] == 9;
							}
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_24;
						}
					}
					if (num6 != 2 && (num6 != 1 || flag))
					{
						if (i == size)
						{
							goto Block_28;
						}
						num2 = i;
						while (i < size)
						{
							c = (char)ptr[i];
							if (c == '\n')
							{
								break;
							}
							if (c > ' ')
							{
								num3 = i;
							}
							i++;
							if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
							{
								goto Block_32;
							}
						}
						if (i == size)
						{
							goto Block_33;
						}
						num6 = 0;
						while (i < size && num6 < 2)
						{
							c = (char)ptr[i];
							if (c != '\r' && c != '\n')
							{
								break;
							}
							if (c == '\n')
							{
								num6++;
							}
							i++;
							if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
							{
								goto Block_37;
							}
						}
						if (i == size && num6 < 2)
						{
							goto Block_40;
						}
					}
					if (num2 >= 0 && num2 > num && num3 >= num2)
					{
						text2 = WebHeaderCollection.HeaderEncoding.GetString(ptr + num2, num3 - num2 + 1);
					}
					text3 = ((text3 == null) ? text2 : (text3 + " " + text2));
					if (i >= size || num6 != 1)
					{
						break;
					}
					c = (char)ptr[i];
					if (c != ' ' && c != '\t')
					{
						break;
					}
					i++;
					if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
					{
						goto Block_49;
					}
				}
				if (num5 >= 0 && num >= num5)
				{
					text = WebHeaderCollection.HeaderEncoding.GetString(ptr + num5, num - num5 + 1);
				}
				if (text.Length > 0)
				{
					this.AddInternal(text, text3);
				}
				totalResponseHeadersLength = num4;
				unparsed = i;
				if (num6 == 2)
				{
					goto Block_53;
				}
			}
			Block_6:
			DataParseStatus dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_7:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_30A;
			Block_12:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_15:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_16:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_30A;
			Block_21:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_30A;
			Block_24:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_28:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_30A;
			Block_32:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_33:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_30A;
			Block_37:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_40:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_30A;
			Block_49:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_30A;
			Block_53:
			dataParseStatus = DataParseStatus.Done;
			IL_30A:
			if (dataParseStatus == DataParseStatus.Invalid)
			{
				parseError.Section = WebParseErrorSection.ResponseHeader;
				parseError.Code = webParseErrorCode;
			}
			return dataParseStatus;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0004AD30 File Offset: 0x00048F30
		internal unsafe DataParseStatus ParseHeadersStrict(byte[] buffer, int size, ref int unparsed, ref int totalResponseHeadersLength, int maximumResponseHeadersLength, ref WebParseError parseError)
		{
			WebParseErrorCode webParseErrorCode = WebParseErrorCode.Generic;
			DataParseStatus dataParseStatus = DataParseStatus.Invalid;
			int num = unparsed;
			int num2 = ((maximumResponseHeadersLength <= 0) ? int.MaxValue : (maximumResponseHeadersLength - totalResponseHeadersLength + num));
			DataParseStatus dataParseStatus2 = DataParseStatus.DataTooBig;
			if (size < num2)
			{
				num2 = size;
				dataParseStatus2 = DataParseStatus.NeedMoreData;
			}
			if (num >= num2)
			{
				dataParseStatus = dataParseStatus2;
			}
			else
			{
				try
				{
					fixed (byte[] array = buffer)
					{
						byte* ptr;
						if (buffer == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						while (ptr[num] != 13)
						{
							int num3 = num;
							while (num < num2 && ((ptr[num] > 127) ? WebHeaderCollection.RfcChar.High : WebHeaderCollection.RfcCharMap[(int)ptr[num]]) == WebHeaderCollection.RfcChar.Reg)
							{
								num++;
							}
							if (num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_416;
							}
							if (num == num3)
							{
								dataParseStatus = DataParseStatus.Invalid;
								webParseErrorCode = WebParseErrorCode.InvalidHeaderName;
								goto IL_416;
							}
							int num4 = num - 1;
							int num5 = 0;
							WebHeaderCollection.RfcChar rfcChar;
							while (num < num2 && (rfcChar = ((ptr[num] > 127) ? WebHeaderCollection.RfcChar.High : WebHeaderCollection.RfcCharMap[(int)ptr[num]])) != WebHeaderCollection.RfcChar.Colon)
							{
								switch (rfcChar)
								{
								case WebHeaderCollection.RfcChar.CR:
									if (num5 != 0)
									{
										goto IL_11D;
									}
									num5 = 1;
									break;
								case WebHeaderCollection.RfcChar.LF:
									if (num5 != 1)
									{
										goto IL_11D;
									}
									num5 = 2;
									break;
								case WebHeaderCollection.RfcChar.WS:
									if (num5 == 1)
									{
										goto IL_11D;
									}
									num5 = 0;
									break;
								default:
									goto IL_11D;
								}
								num++;
								continue;
								IL_11D:
								dataParseStatus = DataParseStatus.Invalid;
								webParseErrorCode = WebParseErrorCode.CrLfError;
								goto IL_416;
							}
							if (num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_416;
							}
							if (num5 != 0)
							{
								dataParseStatus = DataParseStatus.Invalid;
								webParseErrorCode = WebParseErrorCode.IncompleteHeaderLine;
								goto IL_416;
							}
							if (++num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_416;
							}
							int num6 = -1;
							int num7 = -1;
							StringBuilder stringBuilder = null;
							while (num < num2 && ((rfcChar = ((ptr[num] > 127) ? WebHeaderCollection.RfcChar.High : WebHeaderCollection.RfcCharMap[(int)ptr[num]])) == WebHeaderCollection.RfcChar.WS || num5 != 2))
							{
								switch (rfcChar)
								{
								case WebHeaderCollection.RfcChar.High:
								case WebHeaderCollection.RfcChar.Reg:
								case WebHeaderCollection.RfcChar.Colon:
								case WebHeaderCollection.RfcChar.Delim:
									if (num5 == 1)
									{
										goto IL_23E;
									}
									if (num5 == 3)
									{
										num5 = 0;
										if (num6 != -1)
										{
											string @string = WebHeaderCollection.HeaderEncoding.GetString(ptr + num6, num7 - num6 + 1);
											if (stringBuilder == null)
											{
												stringBuilder = new StringBuilder(@string, @string.Length * 5);
											}
											else
											{
												stringBuilder.Append(" ");
												stringBuilder.Append(@string);
											}
										}
										num6 = -1;
									}
									if (num6 == -1)
									{
										num6 = num;
									}
									num7 = num;
									break;
								case WebHeaderCollection.RfcChar.Ctl:
									goto IL_23E;
								case WebHeaderCollection.RfcChar.CR:
									if (num5 != 0)
									{
										goto IL_23E;
									}
									num5 = 1;
									break;
								case WebHeaderCollection.RfcChar.LF:
									if (num5 != 1)
									{
										goto IL_23E;
									}
									num5 = 2;
									break;
								case WebHeaderCollection.RfcChar.WS:
									if (num5 == 1)
									{
										goto IL_23E;
									}
									if (num5 == 2)
									{
										num5 = 3;
									}
									break;
								default:
									goto IL_23E;
								}
								num++;
								continue;
								IL_23E:
								dataParseStatus = DataParseStatus.Invalid;
								webParseErrorCode = WebParseErrorCode.CrLfError;
								goto IL_416;
							}
							if (num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_416;
							}
							string text = ((num6 == -1) ? "" : WebHeaderCollection.HeaderEncoding.GetString(ptr + num6, num7 - num6 + 1));
							if (stringBuilder != null)
							{
								if (text.Length != 0)
								{
									stringBuilder.Append(" ");
									stringBuilder.Append(text);
								}
								text = stringBuilder.ToString();
							}
							string text2 = null;
							int num8 = num4 - num3 + 1;
							if (this.m_CommonHeaders != null)
							{
								int num9 = (int)WebHeaderCollection.s_CommonHeaderHints[(int)(ptr[num3] & 31)];
								if (num9 >= 0)
								{
									string text3;
									for (;;)
									{
										text3 = WebHeaderCollection.s_CommonHeaderNames[num9++];
										if (text3.Length < num8 || CaseInsensitiveAscii.AsciiToLower[(int)ptr[num3]] != CaseInsensitiveAscii.AsciiToLower[(int)text3[0]])
										{
											goto IL_3E3;
										}
										if (text3.Length <= num8)
										{
											byte* ptr2 = ptr + num3 + 1;
											int num10 = 1;
											while (num10 < text3.Length && ((char)(*(ptr2++)) == text3[num10] || CaseInsensitiveAscii.AsciiToLower[(int)(*(ptr2 - 1))] == CaseInsensitiveAscii.AsciiToLower[(int)text3[num10]]))
											{
												num10++;
											}
											if (num10 == text3.Length)
											{
												break;
											}
										}
									}
									this.m_NumCommonHeaders++;
									num9--;
									if (this.m_CommonHeaders[num9] == null)
									{
										this.m_CommonHeaders[num9] = text;
									}
									else
									{
										this.NormalizeCommonHeaders();
										this.AddInternalNotCommon(text3, text);
									}
									text2 = text3;
								}
							}
							IL_3E3:
							if (text2 == null)
							{
								text2 = WebHeaderCollection.HeaderEncoding.GetString(ptr + num3, num8);
								this.AddInternalNotCommon(text2, text);
							}
							totalResponseHeadersLength += num - unparsed;
							unparsed = num;
						}
						if (++num == num2)
						{
							dataParseStatus = dataParseStatus2;
						}
						else if (ptr[num++] == 10)
						{
							totalResponseHeadersLength += num - unparsed;
							unparsed = num;
							dataParseStatus = DataParseStatus.Done;
						}
						else
						{
							dataParseStatus = DataParseStatus.Invalid;
							webParseErrorCode = WebParseErrorCode.CrLfError;
						}
					}
				}
				finally
				{
					byte[] array = null;
				}
			}
			IL_416:
			if (dataParseStatus == DataParseStatus.Invalid)
			{
				parseError.Section = WebParseErrorSection.ResponseHeader;
				parseError.Code = webParseErrorCode;
			}
			return dataParseStatus;
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.WebHeaderCollection" /> will be serialized.</param>
		/// <param name="streamingContext">The destination of the serialization.</param>
		// Token: 0x06000E50 RID: 3664 RVA: 0x0004B184 File Offset: 0x00049384
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Gets the value of a particular header in the collection, specified by the name of the header.</summary>
		/// <param name="name">The name of the Web header.</param>
		/// <returns>A <see cref="T:System.String" /> holding the value of the specified header.</returns>
		// Token: 0x06000E51 RID: 3665 RVA: 0x0004B190 File Offset: 0x00049390
		public override string Get(string name)
		{
			if (this.m_CommonHeaders != null && name != null && name.Length > 0 && name[0] < 'Ā')
			{
				int num = (int)WebHeaderCollection.s_CommonHeaderHints[(int)(name[0] & '\u001f')];
				if (num >= 0)
				{
					for (;;)
					{
						string text = WebHeaderCollection.s_CommonHeaderNames[num++];
						if (text.Length < name.Length || CaseInsensitiveAscii.AsciiToLower[(int)name[0]] != CaseInsensitiveAscii.AsciiToLower[(int)text[0]])
						{
							goto IL_EF;
						}
						if (text.Length <= name.Length)
						{
							int num2 = 1;
							while (num2 < text.Length && (name[num2] == text[num2] || (name[num2] <= 'ÿ' && CaseInsensitiveAscii.AsciiToLower[(int)name[num2]] == CaseInsensitiveAscii.AsciiToLower[(int)text[num2]])))
							{
								num2++;
							}
							if (num2 == text.Length)
							{
								break;
							}
						}
					}
					return this.m_CommonHeaders[num - 1];
				}
			}
			IL_EF:
			if (this.m_InnerCollection == null)
			{
				return null;
			}
			return this.m_InnerCollection.Get(name);
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Net.WebHeaderCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Net.WebHeaderCollection" />.</returns>
		// Token: 0x06000E52 RID: 3666 RVA: 0x0004B2A2 File Offset: 0x000494A2
		public override IEnumerator GetEnumerator()
		{
			this.NormalizeCommonHeaders();
			return new NameObjectCollectionBase.NameObjectKeysEnumerator(this.InnerCollection);
		}

		/// <summary>Gets the number of headers in the collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> indicating the number of headers in a request.</returns>
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0004B2B5 File Offset: 0x000494B5
		[global::__DynamicallyInvokable]
		public override int Count
		{
			[global::__DynamicallyInvokable]
			get
			{
				return ((this.m_InnerCollection == null) ? 0 : this.m_InnerCollection.Count) + this.m_NumCommonHeaders;
			}
		}

		/// <summary>Gets the collection of header names (keys) in the collection.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> containing all header names in a Web request.</returns>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0004B2D4 File Offset: 0x000494D4
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				this.NormalizeCommonHeaders();
				return this.InnerCollection.Keys;
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0004B2E7 File Offset: 0x000494E7
		internal override bool InternalHasKeys()
		{
			this.NormalizeCommonHeaders();
			return this.m_InnerCollection != null && this.m_InnerCollection.HasKeys();
		}

		/// <summary>Gets the value of a particular header in the collection, specified by an index into the collection.</summary>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <returns>A <see cref="T:System.String" /> containing the value of the specified header.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative.  
		/// -or-  
		/// <paramref name="index" /> exceeds the size of the collection.</exception>
		// Token: 0x06000E56 RID: 3670 RVA: 0x0004B304 File Offset: 0x00049504
		public override string Get(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.Get(index);
		}

		/// <summary>Gets an array of header values stored in the <paramref name="index" /> position of the header collection.</summary>
		/// <param name="index">The header index to return.</param>
		/// <returns>An array of header strings.</returns>
		// Token: 0x06000E57 RID: 3671 RVA: 0x0004B318 File Offset: 0x00049518
		public override string[] GetValues(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.GetValues(index);
		}

		/// <summary>Gets the header name at the specified position in the collection.</summary>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <returns>A <see cref="T:System.String" /> holding the header name.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative.  
		/// -or-  
		/// <paramref name="index" /> exceeds the size of the collection.</exception>
		// Token: 0x06000E58 RID: 3672 RVA: 0x0004B32C File Offset: 0x0004952C
		public override string GetKey(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.GetKey(index);
		}

		/// <summary>Gets all header names (keys) in the collection.</summary>
		/// <returns>An array of type <see cref="T:System.String" /> containing all header names in a Web request.</returns>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0004B340 File Offset: 0x00049540
		[global::__DynamicallyInvokable]
		public override string[] AllKeys
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.NormalizeCommonHeaders();
				return this.InnerCollection.AllKeys;
			}
		}

		/// <summary>Removes all headers from the collection.</summary>
		// Token: 0x06000E5A RID: 3674 RVA: 0x0004B353 File Offset: 0x00049553
		public override void Clear()
		{
			this.m_CommonHeaders = null;
			this.m_NumCommonHeaders = 0;
			base.InvalidateCachedArrays();
			if (this.m_InnerCollection != null)
			{
				this.m_InnerCollection.Clear();
			}
		}

		// Token: 0x0400123F RID: 4671
		private const int ApproxAveHeaderLineSize = 30;

		// Token: 0x04001240 RID: 4672
		private const int ApproxHighAvgNumHeaders = 16;

		// Token: 0x04001241 RID: 4673
		private static readonly HeaderInfoTable HInfo = new HeaderInfoTable();

		// Token: 0x04001242 RID: 4674
		private string[] m_CommonHeaders;

		// Token: 0x04001243 RID: 4675
		private int m_NumCommonHeaders;

		// Token: 0x04001244 RID: 4676
		private static readonly string[] s_CommonHeaderNames = new string[]
		{
			"Accept-Ranges", "Content-Length", "Cache-Control", "Content-Type", "Date", "Expires", "ETag", "Last-Modified", "Location", "Proxy-Authenticate",
			"P3P", "Set-Cookie2", "Set-Cookie", "Server", "Via", "WWW-Authenticate", "X-AspNet-Version", "X-Powered-By", "["
		};

		// Token: 0x04001245 RID: 4677
		private static readonly sbyte[] s_CommonHeaderHints = new sbyte[]
		{
			-1, 0, -1, 1, 4, 5, -1, -1, -1, -1,
			-1, -1, 7, -1, -1, -1, 9, -1, -1, 11,
			-1, -1, 14, 15, 16, -1, -1, -1, -1, -1,
			-1, -1
		};

		// Token: 0x04001246 RID: 4678
		private const int c_AcceptRanges = 0;

		// Token: 0x04001247 RID: 4679
		private const int c_ContentLength = 1;

		// Token: 0x04001248 RID: 4680
		private const int c_CacheControl = 2;

		// Token: 0x04001249 RID: 4681
		private const int c_ContentType = 3;

		// Token: 0x0400124A RID: 4682
		private const int c_Date = 4;

		// Token: 0x0400124B RID: 4683
		private const int c_Expires = 5;

		// Token: 0x0400124C RID: 4684
		private const int c_ETag = 6;

		// Token: 0x0400124D RID: 4685
		private const int c_LastModified = 7;

		// Token: 0x0400124E RID: 4686
		private const int c_Location = 8;

		// Token: 0x0400124F RID: 4687
		private const int c_ProxyAuthenticate = 9;

		// Token: 0x04001250 RID: 4688
		private const int c_P3P = 10;

		// Token: 0x04001251 RID: 4689
		private const int c_SetCookie2 = 11;

		// Token: 0x04001252 RID: 4690
		private const int c_SetCookie = 12;

		// Token: 0x04001253 RID: 4691
		private const int c_Server = 13;

		// Token: 0x04001254 RID: 4692
		private const int c_Via = 14;

		// Token: 0x04001255 RID: 4693
		private const int c_WwwAuthenticate = 15;

		// Token: 0x04001256 RID: 4694
		private const int c_XAspNetVersion = 16;

		// Token: 0x04001257 RID: 4695
		private const int c_XPoweredBy = 17;

		// Token: 0x04001258 RID: 4696
		private NameValueCollection m_InnerCollection;

		// Token: 0x04001259 RID: 4697
		private WebHeaderCollectionType m_Type;

		// Token: 0x0400125A RID: 4698
		private static readonly char[] HttpTrimCharacters = new char[] { '\t', '\n', '\v', '\f', '\r', ' ' };

		// Token: 0x0400125B RID: 4699
		private static WebHeaderCollection.RfcChar[] RfcCharMap = new WebHeaderCollection.RfcChar[]
		{
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.WS,
			WebHeaderCollection.RfcChar.LF,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.CR,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.WS,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Colon,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Ctl
		};

		// Token: 0x02000732 RID: 1842
		internal static class HeaderEncoding
		{
			// Token: 0x06004181 RID: 16769 RVA: 0x00110064 File Offset: 0x0010E264
			internal unsafe static string GetString(byte[] bytes, int byteIndex, int byteCount)
			{
				byte* ptr;
				if (bytes == null || bytes.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &bytes[0];
				}
				return WebHeaderCollection.HeaderEncoding.GetString(ptr + byteIndex, byteCount);
			}

			// Token: 0x06004182 RID: 16770 RVA: 0x00110094 File Offset: 0x0010E294
			internal unsafe static string GetString(byte* pBytes, int byteCount)
			{
				if (byteCount < 1)
				{
					return "";
				}
				string text = new string('\0', byteCount);
				fixed (string text2 = text)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr2 = ptr;
					while (byteCount >= 8)
					{
						*ptr2 = (char)(*pBytes);
						ptr2[1] = (char)pBytes[1];
						ptr2[2] = (char)pBytes[2];
						ptr2[3] = (char)pBytes[3];
						ptr2[4] = (char)pBytes[4];
						ptr2[5] = (char)pBytes[5];
						ptr2[6] = (char)pBytes[6];
						ptr2[7] = (char)pBytes[7];
						ptr2 += 8;
						pBytes += 8;
						byteCount -= 8;
					}
					for (int i = 0; i < byteCount; i++)
					{
						ptr2[i] = (char)pBytes[i];
					}
				}
				return text;
			}

			// Token: 0x06004183 RID: 16771 RVA: 0x0011014A File Offset: 0x0010E34A
			internal static int GetByteCount(string myString)
			{
				return myString.Length;
			}

			// Token: 0x06004184 RID: 16772 RVA: 0x00110154 File Offset: 0x0010E354
			internal unsafe static void GetBytes(string myString, int charIndex, int charCount, byte[] bytes, int byteIndex)
			{
				if (myString.Length == 0)
				{
					return;
				}
				fixed (byte[] array = bytes)
				{
					byte* ptr;
					if (bytes == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr + byteIndex;
					int num = charIndex + charCount;
					while (charIndex < num)
					{
						*(ptr2++) = (byte)myString[charIndex++];
					}
				}
			}

			// Token: 0x06004185 RID: 16773 RVA: 0x001101A8 File Offset: 0x0010E3A8
			internal static byte[] GetBytes(string myString)
			{
				byte[] array = new byte[myString.Length];
				if (myString.Length != 0)
				{
					WebHeaderCollection.HeaderEncoding.GetBytes(myString, 0, myString.Length, array, 0);
				}
				return array;
			}

			// Token: 0x06004186 RID: 16774 RVA: 0x001101DC File Offset: 0x0010E3DC
			[FriendAccessAllowed]
			internal static string DecodeUtf8FromString(string input)
			{
				if (string.IsNullOrWhiteSpace(input))
				{
					return input;
				}
				bool flag = false;
				for (int i = 0; i < input.Length; i++)
				{
					if (input[i] > 'ÿ')
					{
						return input;
					}
					if (input[i] > '\u007f')
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					byte[] array = new byte[input.Length];
					for (int j = 0; j < input.Length; j++)
					{
						if (input[j] > 'ÿ')
						{
							return input;
						}
						array[j] = (byte)input[j];
					}
					try
					{
						Encoding encoding = Encoding.GetEncoding("utf-8", EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
						return encoding.GetString(array);
					}
					catch (ArgumentException)
					{
					}
					return input;
				}
				return input;
			}
		}

		// Token: 0x02000733 RID: 1843
		private enum RfcChar : byte
		{
			// Token: 0x0400318D RID: 12685
			High,
			// Token: 0x0400318E RID: 12686
			Reg,
			// Token: 0x0400318F RID: 12687
			Ctl,
			// Token: 0x04003190 RID: 12688
			CR,
			// Token: 0x04003191 RID: 12689
			LF,
			// Token: 0x04003192 RID: 12690
			WS,
			// Token: 0x04003193 RID: 12691
			Colon,
			// Token: 0x04003194 RID: 12692
			Delim
		}
	}
}
