using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000834 RID: 2100
	[Serializable]
	internal class CrossAppDomainChannel : IChannel, IChannelSender, IChannelReceiver
	{
		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x060059E9 RID: 23017 RVA: 0x0013E349 File Offset: 0x0013C549
		// (set) Token: 0x060059EA RID: 23018 RVA: 0x0013E35F File Offset: 0x0013C55F
		private static CrossAppDomainChannel gAppDomainChannel
		{
			get
			{
				return Thread.GetDomain().RemotingData.ChannelServicesData.xadmessageSink;
			}
			set
			{
				Thread.GetDomain().RemotingData.ChannelServicesData.xadmessageSink = value;
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x060059EB RID: 23019 RVA: 0x0013E378 File Offset: 0x0013C578
		internal static CrossAppDomainChannel AppDomainChannel
		{
			get
			{
				if (CrossAppDomainChannel.gAppDomainChannel == null)
				{
					CrossAppDomainChannel crossAppDomainChannel = new CrossAppDomainChannel();
					object obj = CrossAppDomainChannel.staticSyncObject;
					lock (obj)
					{
						if (CrossAppDomainChannel.gAppDomainChannel == null)
						{
							CrossAppDomainChannel.gAppDomainChannel = crossAppDomainChannel;
						}
					}
				}
				return CrossAppDomainChannel.gAppDomainChannel;
			}
		}

		// Token: 0x060059EC RID: 23020 RVA: 0x0013E3D0 File Offset: 0x0013C5D0
		[SecurityCritical]
		internal static void RegisterChannel()
		{
			CrossAppDomainChannel appDomainChannel = CrossAppDomainChannel.AppDomainChannel;
			ChannelServices.RegisterChannelInternal(appDomainChannel, false);
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x060059ED RID: 23021 RVA: 0x0013E3EA File Offset: 0x0013C5EA
		public virtual string ChannelName
		{
			[SecurityCritical]
			get
			{
				return "XAPPDMN";
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x060059EE RID: 23022 RVA: 0x0013E3F1 File Offset: 0x0013C5F1
		public virtual string ChannelURI
		{
			get
			{
				return "XAPPDMN_URI";
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x060059EF RID: 23023 RVA: 0x0013E3F8 File Offset: 0x0013C5F8
		public virtual int ChannelPriority
		{
			[SecurityCritical]
			get
			{
				return 100;
			}
		}

		// Token: 0x060059F0 RID: 23024 RVA: 0x0013E3FC File Offset: 0x0013C5FC
		[SecurityCritical]
		public string Parse(string url, out string objectURI)
		{
			objectURI = url;
			return null;
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x060059F1 RID: 23025 RVA: 0x0013E402 File Offset: 0x0013C602
		public virtual object ChannelData
		{
			[SecurityCritical]
			get
			{
				return new CrossAppDomainData(Context.DefaultContext.InternalContextID, Thread.GetDomain().GetId(), Identity.ProcessGuid);
			}
		}

		// Token: 0x060059F2 RID: 23026 RVA: 0x0013E424 File Offset: 0x0013C624
		[SecurityCritical]
		public virtual IMessageSink CreateMessageSink(string url, object data, out string objectURI)
		{
			objectURI = null;
			IMessageSink messageSink = null;
			if (url != null && data == null)
			{
				if (url.StartsWith("XAPPDMN", StringComparison.Ordinal))
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_AppDomains_NYI"));
				}
			}
			else
			{
				CrossAppDomainData crossAppDomainData = data as CrossAppDomainData;
				if (crossAppDomainData != null && crossAppDomainData.ProcessGuid.Equals(Identity.ProcessGuid))
				{
					messageSink = CrossAppDomainSink.FindOrCreateSink(crossAppDomainData);
				}
			}
			return messageSink;
		}

		// Token: 0x060059F3 RID: 23027 RVA: 0x0013E47E File Offset: 0x0013C67E
		[SecurityCritical]
		public virtual string[] GetUrlsForUri(string objectURI)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x060059F4 RID: 23028 RVA: 0x0013E48F File Offset: 0x0013C68F
		[SecurityCritical]
		public virtual void StartListening(object data)
		{
		}

		// Token: 0x060059F5 RID: 23029 RVA: 0x0013E491 File Offset: 0x0013C691
		[SecurityCritical]
		public virtual void StopListening(object data)
		{
		}

		// Token: 0x040028ED RID: 10477
		private const string _channelName = "XAPPDMN";

		// Token: 0x040028EE RID: 10478
		private const string _channelURI = "XAPPDMN_URI";

		// Token: 0x040028EF RID: 10479
		private static object staticSyncObject = new object();

		// Token: 0x040028F0 RID: 10480
		private static PermissionSet s_fullTrust = new PermissionSet(PermissionState.Unrestricted);
	}
}
