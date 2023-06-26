using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Provides storage for multiple credentials.</summary>
	// Token: 0x020000DB RID: 219
	[global::__DynamicallyInvokable]
	public class CredentialCache : ICredentials, ICredentialsByHost, IEnumerable
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0002977B File Offset: 0x0002797B
		internal bool IsDefaultInCache
		{
			get
			{
				return this.m_NumbDefaultCredInCache != 0;
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.CredentialCache" /> class.</summary>
		// Token: 0x06000774 RID: 1908 RVA: 0x00029786 File Offset: 0x00027986
		[global::__DynamicallyInvokable]
		public CredentialCache()
		{
		}

		/// <summary>Adds a <see cref="T:System.Net.NetworkCredential" /> instance to the credential cache for use with protocols other than SMTP and associates it with a Uniform Resource Identifier (URI) prefix and authentication protocol.</summary>
		/// <param name="uriPrefix">A <see cref="T:System.Uri" /> that specifies the URI prefix of the resources that the credential grants access to.</param>
		/// <param name="authType">The authentication scheme used by the resource named in <paramref name="uriPrefix" />.</param>
		/// <param name="cred">The <see cref="T:System.Net.NetworkCredential" /> to add to the credential cache.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="authType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The same credentials are added more than once.</exception>
		// Token: 0x06000775 RID: 1909 RVA: 0x000297A4 File Offset: 0x000279A4
		[global::__DynamicallyInvokable]
		public void Add(Uri uriPrefix, string authType, NetworkCredential cred)
		{
			if (uriPrefix == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			if (authType == null)
			{
				throw new ArgumentNullException("authType");
			}
			if (cred is SystemNetworkCredential && string.Compare(authType, "NTLM", StringComparison.OrdinalIgnoreCase) != 0 && (!DigestClient.WDigestAvailable || string.Compare(authType, "Digest", StringComparison.OrdinalIgnoreCase) != 0) && string.Compare(authType, "Kerberos", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(authType, "Negotiate", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(SR.GetString("net_nodefaultcreds", new object[] { authType }), "authType");
			}
			this.m_version++;
			CredentialKey credentialKey = new CredentialKey(uriPrefix, authType);
			this.cache.Add(credentialKey, cred);
			if (cred is SystemNetworkCredential)
			{
				this.m_NumbDefaultCredInCache++;
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.NetworkCredential" /> instance for use with SMTP to the credential cache and associates it with a host computer, port, and authentication protocol. Credentials added using this method are valid for SMTP only. This method does not work for HTTP or FTP requests.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that identifies the host computer.</param>
		/// <param name="port">A <see cref="T:System.Int32" /> that specifies the port to connect to on <paramref name="host" />.</param>
		/// <param name="authenticationType">A <see cref="T:System.String" /> that identifies the authentication scheme used when connecting to <paramref name="host" /> using <paramref name="cred" />.</param>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> to add to the credential cache.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="authType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="authType" /> not an accepted value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than zero.</exception>
		// Token: 0x06000776 RID: 1910 RVA: 0x00029874 File Offset: 0x00027A74
		[global::__DynamicallyInvokable]
		public void Add(string host, int port, string authenticationType, NetworkCredential credential)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			if (host.Length == 0)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[] { "host" }));
			}
			if (port < 0)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (credential is SystemNetworkCredential && string.Compare(authenticationType, "NTLM", StringComparison.OrdinalIgnoreCase) != 0 && (!DigestClient.WDigestAvailable || string.Compare(authenticationType, "Digest", StringComparison.OrdinalIgnoreCase) != 0) && string.Compare(authenticationType, "Kerberos", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(authenticationType, "Negotiate", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(SR.GetString("net_nodefaultcreds", new object[] { authenticationType }), "authenticationType");
			}
			this.m_version++;
			CredentialHostKey credentialHostKey = new CredentialHostKey(host, port, authenticationType);
			this.cacheForHosts.Add(credentialHostKey, credential);
			if (credential is SystemNetworkCredential)
			{
				this.m_NumbDefaultCredInCache++;
			}
		}

		/// <summary>Deletes a <see cref="T:System.Net.NetworkCredential" /> instance from the cache if it is associated with the specified Uniform Resource Identifier (URI) prefix and authentication protocol.</summary>
		/// <param name="uriPrefix">A <see cref="T:System.Uri" /> that specifies the URI prefix of the resources that the credential is used for.</param>
		/// <param name="authType">The authentication scheme used by the host named in <paramref name="uriPrefix" />.</param>
		// Token: 0x06000777 RID: 1911 RVA: 0x00029978 File Offset: 0x00027B78
		[global::__DynamicallyInvokable]
		public void Remove(Uri uriPrefix, string authType)
		{
			if (uriPrefix == null || authType == null)
			{
				return;
			}
			this.m_version++;
			CredentialKey credentialKey = new CredentialKey(uriPrefix, authType);
			if (this.cache[credentialKey] is SystemNetworkCredential)
			{
				this.m_NumbDefaultCredInCache--;
			}
			this.cache.Remove(credentialKey);
		}

		/// <summary>Deletes a <see cref="T:System.Net.NetworkCredential" /> instance from the cache if it is associated with the specified host, port, and authentication protocol.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that identifies the host computer.</param>
		/// <param name="port">A <see cref="T:System.Int32" /> that specifies the port to connect to on <paramref name="host" />.</param>
		/// <param name="authenticationType">A <see cref="T:System.String" /> that identifies the authentication scheme used when connecting to <paramref name="host" />.</param>
		// Token: 0x06000778 RID: 1912 RVA: 0x000299D8 File Offset: 0x00027BD8
		[global::__DynamicallyInvokable]
		public void Remove(string host, int port, string authenticationType)
		{
			if (host == null || authenticationType == null)
			{
				return;
			}
			if (port < 0)
			{
				return;
			}
			this.m_version++;
			CredentialHostKey credentialHostKey = new CredentialHostKey(host, port, authenticationType);
			if (this.cacheForHosts[credentialHostKey] is SystemNetworkCredential)
			{
				this.m_NumbDefaultCredInCache--;
			}
			this.cacheForHosts.Remove(credentialHostKey);
		}

		/// <summary>Returns the <see cref="T:System.Net.NetworkCredential" /> instance associated with the specified Uniform Resource Identifier (URI) and authentication type.</summary>
		/// <param name="uriPrefix">A <see cref="T:System.Uri" /> that specifies the URI prefix of the resources that the credential grants access to.</param>
		/// <param name="authType">The authentication scheme used by the resource named in <paramref name="uriPrefix" />.</param>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> or, if there is no matching credential in the cache, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> or <paramref name="authType" /> is <see langword="null" />.</exception>
		// Token: 0x06000779 RID: 1913 RVA: 0x00029A38 File Offset: 0x00027C38
		[global::__DynamicallyInvokable]
		public NetworkCredential GetCredential(Uri uriPrefix, string authType)
		{
			if (uriPrefix == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			if (authType == null)
			{
				throw new ArgumentNullException("authType");
			}
			int num = -1;
			NetworkCredential networkCredential = null;
			IDictionaryEnumerator enumerator = this.cache.GetEnumerator();
			while (enumerator.MoveNext())
			{
				CredentialKey credentialKey = (CredentialKey)enumerator.Key;
				if (credentialKey.Match(uriPrefix, authType))
				{
					int uriPrefixLength = credentialKey.UriPrefixLength;
					if (uriPrefixLength > num)
					{
						num = uriPrefixLength;
						networkCredential = (NetworkCredential)enumerator.Value;
					}
				}
			}
			return networkCredential;
		}

		/// <summary>Returns the <see cref="T:System.Net.NetworkCredential" /> instance associated with the specified host, port, and authentication protocol.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that identifies the host computer.</param>
		/// <param name="port">A <see cref="T:System.Int32" /> that specifies the port to connect to on <paramref name="host" />.</param>
		/// <param name="authenticationType">A <see cref="T:System.String" /> that identifies the authentication scheme used when connecting to <paramref name="host" />.</param>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> or, if there is no matching credential in the cache, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="authType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="authType" /> not an accepted value.  
		/// -or-  
		/// <paramref name="host" /> is equal to the empty string ("").</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than zero.</exception>
		// Token: 0x0600077A RID: 1914 RVA: 0x00029AB4 File Offset: 0x00027CB4
		[global::__DynamicallyInvokable]
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			if (host.Length == 0)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[] { "host" }));
			}
			if (port < 0)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			NetworkCredential networkCredential = null;
			IDictionaryEnumerator enumerator = this.cacheForHosts.GetEnumerator();
			while (enumerator.MoveNext())
			{
				CredentialHostKey credentialHostKey = (CredentialHostKey)enumerator.Key;
				if (credentialHostKey.Match(host, port, authenticationType))
				{
					networkCredential = (NetworkCredential)enumerator.Value;
				}
			}
			return networkCredential;
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Net.CredentialCache" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Net.CredentialCache" />.</returns>
		// Token: 0x0600077B RID: 1915 RVA: 0x00029B4E File Offset: 0x00027D4E
		[global::__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			return new CredentialCache.CredentialEnumerator(this, this.cache, this.cacheForHosts, this.m_version);
		}

		/// <summary>Gets the system credentials of the application.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> that represents the system credentials of the application.</returns>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00029B68 File Offset: 0x00027D68
		[global::__DynamicallyInvokable]
		public static ICredentials DefaultCredentials
		{
			[global::__DynamicallyInvokable]
			get
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME").Demand();
				return SystemNetworkCredential.defaultCredential;
			}
		}

		/// <summary>Gets the network credentials of the current security context.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkCredential" /> that represents the network credentials of the current user or application.</returns>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00029B7F File Offset: 0x00027D7F
		[global::__DynamicallyInvokable]
		public static NetworkCredential DefaultNetworkCredentials
		{
			[global::__DynamicallyInvokable]
			get
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME").Demand();
				return SystemNetworkCredential.defaultCredential;
			}
		}

		// Token: 0x04000D14 RID: 3348
		private Hashtable cache = new Hashtable();

		// Token: 0x04000D15 RID: 3349
		private Hashtable cacheForHosts = new Hashtable();

		// Token: 0x04000D16 RID: 3350
		internal int m_version;

		// Token: 0x04000D17 RID: 3351
		private int m_NumbDefaultCredInCache;

		// Token: 0x020006F5 RID: 1781
		private class CredentialEnumerator : IEnumerator
		{
			// Token: 0x0600405A RID: 16474 RVA: 0x0010DC58 File Offset: 0x0010BE58
			internal CredentialEnumerator(CredentialCache cache, Hashtable table, Hashtable hostTable, int version)
			{
				this.m_cache = cache;
				this.m_array = new ICredentials[table.Count + hostTable.Count];
				table.Values.CopyTo(this.m_array, 0);
				hostTable.Values.CopyTo(this.m_array, table.Count);
				this.m_version = version;
			}

			// Token: 0x17000EE1 RID: 3809
			// (get) Token: 0x0600405B RID: 16475 RVA: 0x0010DCC4 File Offset: 0x0010BEC4
			object IEnumerator.Current
			{
				get
				{
					if (this.m_index < 0 || this.m_index >= this.m_array.Length)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
					}
					if (this.m_version != this.m_cache.m_version)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
					}
					return this.m_array[this.m_index];
				}
			}

			// Token: 0x0600405C RID: 16476 RVA: 0x0010DD2C File Offset: 0x0010BF2C
			bool IEnumerator.MoveNext()
			{
				if (this.m_version != this.m_cache.m_version)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
				}
				int num = this.m_index + 1;
				this.m_index = num;
				if (num < this.m_array.Length)
				{
					return true;
				}
				this.m_index = this.m_array.Length;
				return false;
			}

			// Token: 0x0600405D RID: 16477 RVA: 0x0010DD88 File Offset: 0x0010BF88
			void IEnumerator.Reset()
			{
				this.m_index = -1;
			}

			// Token: 0x04003082 RID: 12418
			private CredentialCache m_cache;

			// Token: 0x04003083 RID: 12419
			private ICredentials[] m_array;

			// Token: 0x04003084 RID: 12420
			private int m_index = -1;

			// Token: 0x04003085 RID: 12421
			private int m_version;
		}
	}
}
