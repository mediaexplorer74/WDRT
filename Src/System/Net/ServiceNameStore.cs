using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000209 RID: 521
	internal class ServiceNameStore
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x00066235 File Offset: 0x00064435
		public ServiceNameCollection ServiceNames
		{
			get
			{
				if (this.serviceNameCollection == null)
				{
					this.serviceNameCollection = new ServiceNameCollection(this.serviceNames);
				}
				return this.serviceNameCollection;
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00066256 File Offset: 0x00064456
		public ServiceNameStore()
		{
			this.serviceNames = new List<string>();
			this.serviceNameCollection = null;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00066270 File Offset: 0x00064470
		private bool AddSingleServiceName(string spn)
		{
			spn = ServiceNameCollection.NormalizeServiceName(spn);
			if (this.Contains(spn))
			{
				return false;
			}
			this.serviceNames.Add(spn);
			return true;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00066294 File Offset: 0x00064494
		public bool Add(string uriPrefix)
		{
			string[] array = this.BuildServiceNames(uriPrefix);
			bool flag = false;
			foreach (string text in array)
			{
				if (this.AddSingleServiceName(text))
				{
					flag = true;
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.HttpListener, "ServiceNameStore#" + ValidationHelper.HashString(this) + "::Add() " + SR.GetString("net_log_listener_spn_add", new object[] { text, uriPrefix }));
					}
				}
			}
			if (flag)
			{
				this.serviceNameCollection = null;
			}
			else if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, "ServiceNameStore#" + ValidationHelper.HashString(this) + "::Add() " + SR.GetString("net_log_listener_spn_not_add", new object[] { uriPrefix }));
			}
			return flag;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00066354 File Offset: 0x00064554
		public bool Remove(string uriPrefix)
		{
			string text = this.BuildSimpleServiceName(uriPrefix);
			text = ServiceNameCollection.NormalizeServiceName(text);
			bool flag = this.Contains(text);
			if (flag)
			{
				this.serviceNames.Remove(text);
				this.serviceNameCollection = null;
			}
			if (Logging.On)
			{
				if (flag)
				{
					Logging.PrintInfo(Logging.HttpListener, "ServiceNameStore#" + ValidationHelper.HashString(this) + "::Remove() " + SR.GetString("net_log_listener_spn_remove", new object[] { text, uriPrefix }));
				}
				else
				{
					Logging.PrintInfo(Logging.HttpListener, "ServiceNameStore#" + ValidationHelper.HashString(this) + "::Remove() " + SR.GetString("net_log_listener_spn_not_remove", new object[] { uriPrefix }));
				}
			}
			return flag;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00066406 File Offset: 0x00064606
		private bool Contains(string newServiceName)
		{
			return newServiceName != null && ServiceNameCollection.Contains(newServiceName, this.serviceNames);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00066419 File Offset: 0x00064619
		public void Clear()
		{
			this.serviceNames.Clear();
			this.serviceNameCollection = null;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00066430 File Offset: 0x00064630
		private string ExtractHostname(string uriPrefix, bool allowInvalidUriStrings)
		{
			if (Uri.IsWellFormedUriString(uriPrefix, UriKind.Absolute))
			{
				Uri uri = new Uri(uriPrefix);
				return uri.Host;
			}
			if (allowInvalidUriStrings)
			{
				int num = uriPrefix.IndexOf("://") + 3;
				int num2 = num;
				bool flag = false;
				while (num2 < uriPrefix.Length && uriPrefix[num2] != '/' && (uriPrefix[num2] != ':' || flag))
				{
					if (uriPrefix[num2] == '[')
					{
						if (flag)
						{
							num2 = num;
							break;
						}
						flag = true;
					}
					if (flag && uriPrefix[num2] == ']')
					{
						flag = false;
					}
					num2++;
				}
				return uriPrefix.Substring(num, num2 - num);
			}
			return null;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x000664C8 File Offset: 0x000646C8
		public string BuildSimpleServiceName(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, false);
			if (text != null)
			{
				return "HTTP/" + text;
			}
			return null;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000664F0 File Offset: 0x000646F0
		public string[] BuildServiceNames(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, true);
			IPAddress ipaddress = null;
			if (string.Compare(text, "*", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(text, "+", StringComparison.InvariantCultureIgnoreCase) == 0 || IPAddress.TryParse(text, out ipaddress))
			{
				try
				{
					string hostName = Dns.GetHostEntry(string.Empty).HostName;
					return new string[] { "HTTP/" + hostName };
				}
				catch (SocketException)
				{
					return new string[0];
				}
				catch (SecurityException)
				{
					return new string[0];
				}
			}
			if (!text.Contains("."))
			{
				try
				{
					string hostName2 = Dns.GetHostEntry(text).HostName;
					return new string[]
					{
						"HTTP/" + text,
						"HTTP/" + hostName2
					};
				}
				catch (SocketException)
				{
					return new string[] { "HTTP/" + text };
				}
				catch (SecurityException)
				{
					return new string[] { "HTTP/" + text };
				}
			}
			return new string[] { "HTTP/" + text };
		}

		// Token: 0x04001550 RID: 5456
		private List<string> serviceNames;

		// Token: 0x04001551 RID: 5457
		private ServiceNameCollection serviceNameCollection;
	}
}
