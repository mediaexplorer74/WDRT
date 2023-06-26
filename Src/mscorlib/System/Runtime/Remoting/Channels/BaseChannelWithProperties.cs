using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides a base implementation for channels that want to expose a dictionary interface to their properties.</summary>
	// Token: 0x0200084E RID: 2126
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public abstract class BaseChannelWithProperties : BaseChannelObjectWithProperties
	{
		/// <summary>Gets a <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the current channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the current channel object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06005A52 RID: 23122 RVA: 0x0013EF4C File Offset: 0x0013D14C
		public override IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this);
				if (this.SinksWithProperties != null)
				{
					IServerChannelSink serverChannelSink = this.SinksWithProperties as IServerChannelSink;
					if (serverChannelSink != null)
					{
						while (serverChannelSink != null)
						{
							IDictionary properties = serverChannelSink.Properties;
							if (properties != null)
							{
								arrayList.Add(properties);
							}
							serverChannelSink = serverChannelSink.NextChannelSink;
						}
					}
					else
					{
						for (IClientChannelSink clientChannelSink = (IClientChannelSink)this.SinksWithProperties; clientChannelSink != null; clientChannelSink = clientChannelSink.NextChannelSink)
						{
							IDictionary properties2 = clientChannelSink.Properties;
							if (properties2 != null)
							{
								arrayList.Add(properties2);
							}
						}
					}
				}
				return new AggregateDictionary(arrayList);
			}
		}

		/// <summary>Indicates the top channel sink in the channel sink stack.</summary>
		// Token: 0x04002909 RID: 10505
		protected IChannelSinkBase SinksWithProperties;
	}
}
