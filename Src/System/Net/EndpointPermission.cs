using System;
using System.Globalization;
using System.Security;

namespace System.Net
{
	/// <summary>Defines an endpoint that is authorized by a <see cref="T:System.Net.SocketPermission" /> instance.</summary>
	// Token: 0x02000164 RID: 356
	[Serializable]
	public class EndpointPermission
	{
		/// <summary>Gets the DNS host name or IP address of the server that is associated with this endpoint.</summary>
		/// <returns>A string that contains the DNS host name or IP address of the server.</returns>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00045080 File Offset: 0x00043280
		public string Hostname
		{
			get
			{
				return this.hostname;
			}
		}

		/// <summary>Gets the transport type that is associated with this endpoint.</summary>
		/// <returns>One of the <see cref="T:System.Net.TransportType" /> values.</returns>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x00045088 File Offset: 0x00043288
		public TransportType Transport
		{
			get
			{
				return this.transport;
			}
		}

		/// <summary>Gets the network port number that is associated with this endpoint.</summary>
		/// <returns>The network port number that is associated with this request, or <see cref="F:System.Net.SocketPermission.AllPorts" />.</returns>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00045090 File Offset: 0x00043290
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00045098 File Offset: 0x00043298
		internal EndpointPermission(string epname, int port, TransportType trtype)
		{
			if (EndpointPermission.CheckEndPointName(epname) == EndpointPermission.EndPointType.Invalid)
			{
				throw new ArgumentException(SR.GetString("net_perm_epname", new object[] { epname }), "epname");
			}
			if (!ValidationHelper.ValidateTcpPort(port) && port != -1)
			{
				throw new ArgumentOutOfRangeException("port", SR.GetString("net_perm_invalid_val", new object[]
				{
					"Port",
					port.ToString(NumberFormatInfo.InvariantInfo)
				}));
			}
			this.hostname = epname;
			this.port = port;
			this.transport = trtype;
			this.wildcard = false;
		}

		/// <summary>Determines whether the specified <see langword="Object" /> is equal to the current <see langword="Object" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see langword="Object" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see langword="Object" /> is equal to the current <see langword="Object" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000CE2 RID: 3298 RVA: 0x0004512C File Offset: 0x0004332C
		public override bool Equals(object obj)
		{
			EndpointPermission endpointPermission = (EndpointPermission)obj;
			return string.Compare(this.hostname, endpointPermission.hostname, StringComparison.OrdinalIgnoreCase) == 0 && this.port == endpointPermission.port && this.transport == endpointPermission.transport;
		}

		/// <summary>Serves as a hash function for a particular type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06000CE3 RID: 3299 RVA: 0x00045177 File Offset: 0x00043377
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00045184 File Offset: 0x00043384
		internal bool IsDns
		{
			get
			{
				return !this.IsValidWildcard && EndpointPermission.CheckEndPointName(this.hostname) == EndpointPermission.EndPointType.DnsOrWildcard;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x000451A0 File Offset: 0x000433A0
		private bool IsValidWildcard
		{
			get
			{
				int length = this.hostname.Length;
				if (length < 3)
				{
					return false;
				}
				if (this.hostname[0] == '.' || this.hostname[length - 1] == '.')
				{
					return false;
				}
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < this.hostname.Length; i++)
				{
					if (this.hostname[i] == '.')
					{
						num++;
					}
					else if (this.hostname[i] == '*')
					{
						num2++;
					}
					else if (!char.IsDigit(this.hostname[i]))
					{
						return false;
					}
				}
				return num == 3 && num2 > 0;
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00045248 File Offset: 0x00043448
		internal bool MatchAddress(EndpointPermission e)
		{
			if (this.Hostname.Length == 0 || e.Hostname.Length == 0)
			{
				return false;
			}
			if (this.Hostname.Equals("0.0.0.0"))
			{
				return e.Hostname.Equals("*.*.*.*") || e.Hostname.Equals("0.0.0.0");
			}
			if (this.IsDns && e.IsDns)
			{
				return string.Compare(this.hostname, e.hostname, StringComparison.OrdinalIgnoreCase) == 0;
			}
			this.Resolve();
			e.Resolve();
			if ((this.address == null && !this.wildcard) || (e.address == null && !e.wildcard))
			{
				return false;
			}
			if (this.wildcard && !e.wildcard)
			{
				return false;
			}
			if (e.wildcard)
			{
				if (this.wildcard)
				{
					if (this.MatchWildcard(e.hostname))
					{
						return true;
					}
				}
				else
				{
					for (int i = 0; i < this.address.Length; i++)
					{
						if (e.MatchWildcard(this.address[i].ToString()))
						{
							return true;
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < this.address.Length; j++)
				{
					for (int k = 0; k < e.address.Length; k++)
					{
						if (this.address[j].Equals(e.address[k]))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x000453A0 File Offset: 0x000435A0
		internal bool MatchWildcard(string str)
		{
			string[] array = this.hostname.Split(EndpointPermission.DotSeparator);
			string[] array2 = str.Split(EndpointPermission.DotSeparator);
			if (array2.Length != 4 || array.Length != 4)
			{
				return false;
			}
			for (int i = 0; i < 4; i++)
			{
				if (array2[i] != array[i] && array[i] != "*")
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00045404 File Offset: 0x00043604
		internal void Resolve()
		{
			if (this.cached)
			{
				return;
			}
			if (this.wildcard)
			{
				return;
			}
			if (this.IsValidWildcard)
			{
				this.wildcard = true;
				this.cached = true;
				return;
			}
			IPAddress ipaddress;
			if (IPAddress.TryParse(this.hostname, out ipaddress))
			{
				this.address = new IPAddress[1];
				this.address[0] = ipaddress;
				this.cached = true;
				return;
			}
			try
			{
				IPHostEntry iphostEntry;
				if (Dns.TryInternalResolve(this.hostname, out iphostEntry))
				{
					this.address = iphostEntry.AddressList;
				}
			}
			catch (SecurityException)
			{
				throw;
			}
			catch
			{
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000454A8 File Offset: 0x000436A8
		internal bool SubsetMatch(EndpointPermission e)
		{
			return (this.transport == e.transport || e.transport == TransportType.All) && (this.port == e.port || e.port == -1 || this.port == 0) && this.MatchAddress(e);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.EndpointPermission" /> instance.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Net.EndpointPermission" /> instance.</returns>
		// Token: 0x06000CEA RID: 3306 RVA: 0x000454F4 File Offset: 0x000436F4
		public override string ToString()
		{
			string[] array = new string[5];
			array[0] = this.hostname;
			array[1] = "#";
			array[2] = this.port.ToString();
			array[3] = "#";
			int num = 4;
			int num2 = (int)this.transport;
			array[num] = num2.ToString(NumberFormatInfo.InvariantInfo);
			return string.Concat(array);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0004554C File Offset: 0x0004374C
		internal EndpointPermission Intersect(EndpointPermission E)
		{
			string text = null;
			TransportType transportType;
			if (this.transport == E.transport)
			{
				transportType = this.transport;
			}
			else if (this.transport == TransportType.All)
			{
				transportType = E.transport;
			}
			else
			{
				if (E.transport != TransportType.All)
				{
					return null;
				}
				transportType = this.transport;
			}
			int num;
			if (this.port == E.port)
			{
				num = this.port;
			}
			else if (this.port == -1)
			{
				num = E.port;
			}
			else
			{
				if (E.port != -1)
				{
					return null;
				}
				num = this.port;
			}
			if (this.Hostname.Equals("0.0.0.0"))
			{
				if (!E.Hostname.Equals("*.*.*.*") && !E.Hostname.Equals("0.0.0.0"))
				{
					return null;
				}
				text = this.Hostname;
			}
			else if (E.Hostname.Equals("0.0.0.0"))
			{
				if (!this.Hostname.Equals("*.*.*.*") && !this.Hostname.Equals("0.0.0.0"))
				{
					return null;
				}
				text = E.Hostname;
			}
			else if (this.IsDns && E.IsDns)
			{
				if (string.Compare(this.hostname, E.hostname, StringComparison.OrdinalIgnoreCase) != 0)
				{
					return null;
				}
				text = this.hostname;
			}
			else
			{
				this.Resolve();
				E.Resolve();
				if ((this.address == null && !this.wildcard) || (E.address == null && !E.wildcard))
				{
					return null;
				}
				if (this.wildcard && E.wildcard)
				{
					string[] array = this.hostname.Split(EndpointPermission.DotSeparator);
					string[] array2 = E.hostname.Split(EndpointPermission.DotSeparator);
					string text2 = "";
					if (array2.Length != 4 || array.Length != 4)
					{
						return null;
					}
					for (int i = 0; i < 4; i++)
					{
						if (i != 0)
						{
							text2 += ".";
						}
						if (array2[i] == array[i])
						{
							text2 += array2[i];
						}
						else if (array2[i] == "*")
						{
							text2 += array[i];
						}
						else
						{
							if (!(array[i] == "*"))
							{
								return null;
							}
							text2 += array2[i];
						}
					}
					text = text2;
				}
				else if (this.wildcard)
				{
					for (int j = 0; j < E.address.Length; j++)
					{
						if (this.MatchWildcard(E.address[j].ToString()))
						{
							text = E.hostname;
							break;
						}
					}
				}
				else if (E.wildcard)
				{
					for (int k = 0; k < this.address.Length; k++)
					{
						if (E.MatchWildcard(this.address[k].ToString()))
						{
							text = this.hostname;
							break;
						}
					}
				}
				else
				{
					if (this.address == E.address)
					{
						text = this.hostname;
					}
					int num2 = 0;
					while (text == null && num2 < this.address.Length)
					{
						for (int l = 0; l < E.address.Length; l++)
						{
							if (this.address[num2].Equals(E.address[l]))
							{
								text = this.hostname;
								break;
							}
						}
						num2++;
					}
				}
				if (text == null)
				{
					return null;
				}
			}
			return new EndpointPermission(text, num, transportType);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00045894 File Offset: 0x00043A94
		private static EndpointPermission.EndPointType CheckEndPointName(string name)
		{
			if (name == null)
			{
				return EndpointPermission.EndPointType.Invalid;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int i = 0;
			while (i < name.Length)
			{
				char c = name[i];
				if (c <= '.')
				{
					if (c == '%')
					{
						goto IL_57;
					}
					switch (c)
					{
					case '*':
					case '-':
						goto IL_53;
					case '+':
					case ',':
						goto IL_5B;
					case '.':
						break;
					default:
						goto IL_5B;
					}
				}
				else
				{
					if (c == ':')
					{
						goto IL_57;
					}
					if (c == '_')
					{
						goto IL_53;
					}
					goto IL_5B;
				}
				IL_A1:
				i++;
				continue;
				IL_53:
				flag2 = true;
				goto IL_A1;
				IL_57:
				flag = true;
				goto IL_A1;
				IL_5B:
				if ((c > 'f' && c <= 'z') || (c > 'F' && c <= 'Z'))
				{
					flag2 = true;
					goto IL_A1;
				}
				if ((c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
				{
					flag3 = true;
					goto IL_A1;
				}
				if (c < '0' || c > '9')
				{
					return EndpointPermission.EndPointType.Invalid;
				}
				goto IL_A1;
			}
			if (!flag)
			{
				if (flag2)
				{
					return EndpointPermission.EndPointType.DnsOrWildcard;
				}
				if (!flag3)
				{
					return EndpointPermission.EndPointType.IPv4;
				}
				return EndpointPermission.EndPointType.DnsOrWildcard;
			}
			else
			{
				if (!flag2)
				{
					return EndpointPermission.EndPointType.IPv6;
				}
				return EndpointPermission.EndPointType.Invalid;
			}
		}

		// Token: 0x040011BB RID: 4539
		internal string hostname;

		// Token: 0x040011BC RID: 4540
		internal int port;

		// Token: 0x040011BD RID: 4541
		internal TransportType transport;

		// Token: 0x040011BE RID: 4542
		internal bool wildcard;

		// Token: 0x040011BF RID: 4543
		internal IPAddress[] address;

		// Token: 0x040011C0 RID: 4544
		internal bool cached;

		// Token: 0x040011C1 RID: 4545
		private static char[] DotSeparator = new char[] { '.' };

		// Token: 0x040011C2 RID: 4546
		private const string encSeperator = "#";

		// Token: 0x02000710 RID: 1808
		private enum EndPointType
		{
			// Token: 0x04003101 RID: 12545
			Invalid,
			// Token: 0x04003102 RID: 12546
			IPv6,
			// Token: 0x04003103 RID: 12547
			DnsOrWildcard,
			// Token: 0x04003104 RID: 12548
			IPv4
		}
	}
}
