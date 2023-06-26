using System;
using System.Collections;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Net
{
	/// <summary>Controls rights to make or accept connections on a transport address.</summary>
	// Token: 0x02000163 RID: 355
	[Serializable]
	public sealed class SocketPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Gets a list of <see cref="T:System.Net.EndpointPermission" /> instances that identifies the endpoints that can be connected to under this permission instance.</summary>
		/// <returns>An instance that implements the <see cref="T:System.Collections.IEnumerator" /> interface that contains <see cref="T:System.Net.EndpointPermission" /> instances.</returns>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x00044374 File Offset: 0x00042574
		public IEnumerator ConnectList
		{
			get
			{
				return this.m_connectList.GetEnumerator();
			}
		}

		/// <summary>Gets a list of <see cref="T:System.Net.EndpointPermission" /> instances that identifies the endpoints that can be accepted under this permission instance.</summary>
		/// <returns>An instance that implements the <see cref="T:System.Collections.IEnumerator" /> interface that contains <see cref="T:System.Net.EndpointPermission" /> instances.</returns>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00044381 File Offset: 0x00042581
		public IEnumerator AcceptList
		{
			get
			{
				return this.m_acceptList.GetEnumerator();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.SocketPermission" /> class that allows unrestricted access to the <see cref="T:System.Net.Sockets.Socket" /> or disallows access to the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		// Token: 0x06000CCD RID: 3277 RVA: 0x0004438E File Offset: 0x0004258E
		public SocketPermission(PermissionState state)
		{
			this.initialize();
			this.m_noRestriction = state == PermissionState.Unrestricted;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000443A6 File Offset: 0x000425A6
		internal SocketPermission(bool free)
		{
			this.initialize();
			this.m_noRestriction = free;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.SocketPermission" /> class for the given transport address with the specified permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkAccess" /> values.</param>
		/// <param name="transport">One of the <see cref="T:System.Net.TransportType" /> values.</param>
		/// <param name="hostName">The host name for the transport address.</param>
		/// <param name="portNumber">The port number for the transport address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is <see langword="null" />.</exception>
		// Token: 0x06000CCF RID: 3279 RVA: 0x000443BB File Offset: 0x000425BB
		public SocketPermission(NetworkAccess access, TransportType transport, string hostName, int portNumber)
		{
			this.initialize();
			this.m_noRestriction = false;
			this.AddPermission(access, transport, hostName, portNumber);
		}

		/// <summary>Adds a permission to the set of permissions for a transport address.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkAccess" /> values.</param>
		/// <param name="transport">One of the <see cref="T:System.Net.TransportType" /> values.</param>
		/// <param name="hostName">The host name for the transport address.</param>
		/// <param name="portNumber">The port number for the transport address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is <see langword="null" />.</exception>
		// Token: 0x06000CD0 RID: 3280 RVA: 0x000443DC File Offset: 0x000425DC
		public void AddPermission(NetworkAccess access, TransportType transport, string hostName, int portNumber)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			EndpointPermission endpointPermission = new EndpointPermission(hostName, portNumber, transport);
			this.AddPermission(access, endpointPermission);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00044409 File Offset: 0x00042609
		internal void AddPermission(NetworkAccess access, EndpointPermission endPoint)
		{
			if (this.m_noRestriction)
			{
				return;
			}
			if ((access & NetworkAccess.Connect) != (NetworkAccess)0)
			{
				this.m_connectList.Add(endPoint);
			}
			if ((access & NetworkAccess.Accept) != (NetworkAccess)0)
			{
				this.m_acceptList.Add(endPoint);
			}
		}

		/// <summary>Checks the overall permission state of the object.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.SocketPermission" /> instance is created with the <see langword="Unrestricted" /> value from <see cref="T:System.Security.Permissions.PermissionState" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000CD2 RID: 3282 RVA: 0x0004443D File Offset: 0x0004263D
		public bool IsUnrestricted()
		{
			return this.m_noRestriction;
		}

		/// <summary>Creates a copy of a <see cref="T:System.Net.SocketPermission" /> instance.</summary>
		/// <returns>A new instance of the <see cref="T:System.Net.SocketPermission" /> class that is a copy of the current instance.</returns>
		// Token: 0x06000CD3 RID: 3283 RVA: 0x00044448 File Offset: 0x00042648
		public override IPermission Copy()
		{
			return new SocketPermission(this.m_noRestriction)
			{
				m_connectList = (ArrayList)this.m_connectList.Clone(),
				m_acceptList = (ArrayList)this.m_acceptList.Clone()
			};
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00044490 File Offset: 0x00042690
		private bool FindSubset(ArrayList source, ArrayList target)
		{
			foreach (object obj in source)
			{
				EndpointPermission endpointPermission = (EndpointPermission)obj;
				bool flag = false;
				foreach (object obj2 in target)
				{
					EndpointPermission endpointPermission2 = (EndpointPermission)obj2;
					if (endpointPermission.SubsetMatch(endpointPermission2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns the logical union between two <see cref="T:System.Net.SocketPermission" /> instances.</summary>
		/// <param name="target">The <see cref="T:System.Net.SocketPermission" /> instance to combine with the current instance.</param>
		/// <returns>The <see cref="T:System.Net.SocketPermission" /> instance that represents the union of two <see cref="T:System.Net.SocketPermission" /> instances. If <paramref name="target" /> parameter is <see langword="null" />, it returns a copy of the current instance.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a <see cref="T:System.Net.SocketPermission" />.</exception>
		// Token: 0x06000CD5 RID: 3285 RVA: 0x0004453C File Offset: 0x0004273C
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (this.m_noRestriction || socketPermission.m_noRestriction)
			{
				return new SocketPermission(true);
			}
			SocketPermission socketPermission2 = (SocketPermission)socketPermission.Copy();
			for (int i = 0; i < this.m_connectList.Count; i++)
			{
				socketPermission2.AddPermission(NetworkAccess.Connect, (EndpointPermission)this.m_connectList[i]);
			}
			for (int j = 0; j < this.m_acceptList.Count; j++)
			{
				socketPermission2.AddPermission(NetworkAccess.Accept, (EndpointPermission)this.m_acceptList[j]);
			}
			return socketPermission2;
		}

		/// <summary>Returns the logical intersection between two <see cref="T:System.Net.SocketPermission" /> instances.</summary>
		/// <param name="target">The <see cref="T:System.Net.SocketPermission" /> instance to intersect with the current instance.</param>
		/// <returns>The <see cref="T:System.Net.SocketPermission" /> instance that represents the intersection of two <see cref="T:System.Net.SocketPermission" /> instances. If the intersection is empty, the method returns <see langword="null" />. If the <paramref name="target" /> parameter is a null reference, the method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not a <see cref="T:System.Net.SocketPermission" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Net.DnsPermission" /> is not granted to the method caller.</exception>
		// Token: 0x06000CD6 RID: 3286 RVA: 0x000445F8 File Offset: 0x000427F8
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			SocketPermission socketPermission2;
			if (this.m_noRestriction)
			{
				socketPermission2 = (SocketPermission)socketPermission.Copy();
			}
			else if (socketPermission.m_noRestriction)
			{
				socketPermission2 = (SocketPermission)this.Copy();
			}
			else
			{
				socketPermission2 = new SocketPermission(false);
				SocketPermission.intersectLists(this.m_connectList, socketPermission.m_connectList, socketPermission2.m_connectList);
				SocketPermission.intersectLists(this.m_acceptList, socketPermission.m_acceptList, socketPermission2.m_acceptList);
			}
			if (!socketPermission2.m_noRestriction && socketPermission2.m_connectList.Count == 0 && socketPermission2.m_acceptList.Count == 0)
			{
				return null;
			}
			return socketPermission2;
		}

		/// <summary>Determines if the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A <see cref="T:System.Net.SocketPermission" /> that is to be tested for the subset relationship.</param>
		/// <returns>If <paramref name="target" /> is <see langword="null" />, this method returns <see langword="true" /> if the current instance defines no permissions; otherwise, <see langword="false" />. If <paramref name="target" /> is not <see langword="null" />, this method returns <see langword="true" /> if the current instance defines a subset of <paramref name="target" /> permissions; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a <see cref="T:System.Net.Sockets.SocketException" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Net.DnsPermission" /> is not granted to the method caller.</exception>
		// Token: 0x06000CD7 RID: 3287 RVA: 0x000446B0 File Offset: 0x000428B0
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_noRestriction && this.m_connectList.Count == 0 && this.m_acceptList.Count == 0;
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (socketPermission.IsUnrestricted())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return false;
			}
			if (this.m_acceptList.Count + this.m_connectList.Count == 0)
			{
				return true;
			}
			if (socketPermission.m_acceptList.Count + socketPermission.m_connectList.Count == 0)
			{
				return false;
			}
			bool flag = false;
			try
			{
				if (this.FindSubset(this.m_connectList, socketPermission.m_connectList) && this.FindSubset(this.m_acceptList, socketPermission.m_acceptList))
				{
					flag = true;
				}
			}
			finally
			{
				this.CleanupDNS();
			}
			return flag;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00044798 File Offset: 0x00042998
		private void CleanupDNS()
		{
			foreach (object obj in this.m_connectList)
			{
				EndpointPermission endpointPermission = (EndpointPermission)obj;
				if (!endpointPermission.cached)
				{
					endpointPermission.address = null;
				}
			}
			foreach (object obj2 in this.m_acceptList)
			{
				EndpointPermission endpointPermission2 = (EndpointPermission)obj2;
				if (!endpointPermission2.cached)
				{
					endpointPermission2.address = null;
				}
			}
		}

		/// <summary>Reconstructs a <see cref="T:System.Net.SocketPermission" /> instance for an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding used to reconstruct the <see cref="T:System.Net.SocketPermission" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="securityElement" /> is not a permission element for this type.</exception>
		// Token: 0x06000CD9 RID: 3289 RVA: 0x0004484C File Offset: 0x00042A4C
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (!securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("net_not_ipermission"), "securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				throw new ArgumentException(SR.GetString("net_no_classname"), "securityElement");
			}
			if (text.IndexOf(base.GetType().FullName) < 0)
			{
				throw new ArgumentException(SR.GetString("net_no_typename"), "securityElement");
			}
			this.initialize();
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null)
			{
				this.m_noRestriction = string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0;
				if (this.m_noRestriction)
				{
					return;
				}
			}
			this.m_noRestriction = false;
			this.m_connectList = new ArrayList();
			this.m_acceptList = new ArrayList();
			SecurityElement securityElement2 = securityElement.SearchForChildByTag("ConnectAccess");
			if (securityElement2 != null)
			{
				SocketPermission.ParseAddXmlElement(securityElement2, this.m_connectList, "ConnectAccess, ");
			}
			securityElement2 = securityElement.SearchForChildByTag("AcceptAccess");
			if (securityElement2 != null)
			{
				SocketPermission.ParseAddXmlElement(securityElement2, this.m_acceptList, "AcceptAccess, ");
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0004496C File Offset: 0x00042B6C
		private static void ParseAddXmlElement(SecurityElement et, ArrayList listToAdd, string accessStr)
		{
			foreach (object obj in et.Children)
			{
				SecurityElement securityElement = (SecurityElement)obj;
				if (securityElement.Tag.Equals("ENDPOINT"))
				{
					Hashtable attributes = securityElement.Attributes;
					string text;
					try
					{
						text = attributes["host"] as string;
					}
					catch
					{
						text = null;
					}
					if (text == null)
					{
						throw new ArgumentNullException(accessStr + "host");
					}
					string text2 = text;
					try
					{
						text = attributes["transport"] as string;
					}
					catch
					{
						text = null;
					}
					if (text == null)
					{
						throw new ArgumentNullException(accessStr + "transport");
					}
					TransportType transportType;
					try
					{
						transportType = (TransportType)Enum.Parse(typeof(TransportType), text, true);
					}
					catch (Exception ex)
					{
						if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
						{
							throw;
						}
						throw new ArgumentException(accessStr + "transport", ex);
					}
					try
					{
						text = attributes["port"] as string;
					}
					catch
					{
						text = null;
					}
					if (text == null)
					{
						throw new ArgumentNullException(accessStr + "port");
					}
					if (string.Compare(text, "All", StringComparison.OrdinalIgnoreCase) == 0)
					{
						text = "-1";
					}
					int num;
					try
					{
						num = int.Parse(text, NumberFormatInfo.InvariantInfo);
					}
					catch (Exception ex2)
					{
						if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
						{
							throw;
						}
						throw new ArgumentException(SR.GetString("net_perm_invalid_val", new object[]
						{
							accessStr + "port",
							text
						}), ex2);
					}
					if (!ValidationHelper.ValidateTcpPort(num) && num != -1)
					{
						throw new ArgumentOutOfRangeException("port", num, SR.GetString("net_perm_invalid_val", new object[]
						{
							accessStr + "port",
							text
						}));
					}
					listToAdd.Add(new EndpointPermission(text2, num, transportType));
				}
			}
		}

		/// <summary>Creates an XML encoding of a <see cref="T:System.Net.SocketPermission" /> instance and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> instance that contains an XML-encoded representation of the <see cref="T:System.Net.SocketPermission" /> instance, including state information.</returns>
		// Token: 0x06000CDB RID: 3291 RVA: 0x00044C00 File Offset: 0x00042E00
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (!this.IsUnrestricted())
			{
				if (this.m_connectList.Count > 0)
				{
					SecurityElement securityElement2 = new SecurityElement("ConnectAccess");
					foreach (object obj in this.m_connectList)
					{
						EndpointPermission endpointPermission = (EndpointPermission)obj;
						SecurityElement securityElement3 = new SecurityElement("ENDPOINT");
						securityElement3.AddAttribute("host", endpointPermission.Hostname);
						securityElement3.AddAttribute("transport", endpointPermission.Transport.ToString());
						securityElement3.AddAttribute("port", (endpointPermission.Port != -1) ? endpointPermission.Port.ToString(NumberFormatInfo.InvariantInfo) : "All");
						securityElement2.AddChild(securityElement3);
					}
					securityElement.AddChild(securityElement2);
				}
				if (this.m_acceptList.Count > 0)
				{
					SecurityElement securityElement4 = new SecurityElement("AcceptAccess");
					foreach (object obj2 in this.m_acceptList)
					{
						EndpointPermission endpointPermission2 = (EndpointPermission)obj2;
						SecurityElement securityElement5 = new SecurityElement("ENDPOINT");
						securityElement5.AddAttribute("host", endpointPermission2.Hostname);
						securityElement5.AddAttribute("transport", endpointPermission2.Transport.ToString());
						securityElement5.AddAttribute("port", (endpointPermission2.Port != -1) ? endpointPermission2.Port.ToString(NumberFormatInfo.InvariantInfo) : "All");
						securityElement4.AddChild(securityElement5);
					}
					securityElement.AddChild(securityElement4);
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00044E5C File Offset: 0x0004305C
		private void initialize()
		{
			this.m_noRestriction = false;
			this.m_connectList = new ArrayList();
			this.m_acceptList = new ArrayList();
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00044E7C File Offset: 0x0004307C
		private static void intersectLists(ArrayList A, ArrayList B, ArrayList result)
		{
			bool[] array = new bool[A.Count];
			bool[] array2 = new bool[B.Count];
			int num = 0;
			int num2 = 0;
			foreach (object obj in A)
			{
				EndpointPermission endpointPermission = (EndpointPermission)obj;
				num2 = 0;
				foreach (object obj2 in B)
				{
					EndpointPermission endpointPermission2 = (EndpointPermission)obj2;
					if (!array2[num2] && endpointPermission.Equals(endpointPermission2))
					{
						result.Add(endpointPermission);
						array[num] = (array2[num2] = true);
						break;
					}
					num2++;
				}
				num++;
			}
			num = 0;
			foreach (object obj3 in A)
			{
				EndpointPermission endpointPermission3 = (EndpointPermission)obj3;
				if (!array[num])
				{
					num2 = 0;
					foreach (object obj4 in B)
					{
						EndpointPermission endpointPermission4 = (EndpointPermission)obj4;
						if (!array2[num2])
						{
							EndpointPermission endpointPermission5 = endpointPermission3.Intersect(endpointPermission4);
							if (endpointPermission5 != null)
							{
								bool flag = false;
								foreach (object obj5 in result)
								{
									EndpointPermission endpointPermission6 = (EndpointPermission)obj5;
									if (endpointPermission6.Equals(endpointPermission5))
									{
										flag = true;
										break;
									}
								}
								if (!flag)
								{
									result.Add(endpointPermission5);
								}
							}
						}
						num2++;
					}
				}
				num++;
			}
		}

		// Token: 0x040011B6 RID: 4534
		private ArrayList m_connectList;

		// Token: 0x040011B7 RID: 4535
		private ArrayList m_acceptList;

		// Token: 0x040011B8 RID: 4536
		private bool m_noRestriction;

		/// <summary>Defines a constant that represents all ports.</summary>
		// Token: 0x040011B9 RID: 4537
		public const int AllPorts = -1;

		// Token: 0x040011BA RID: 4538
		internal const int AnyPort = 0;
	}
}
