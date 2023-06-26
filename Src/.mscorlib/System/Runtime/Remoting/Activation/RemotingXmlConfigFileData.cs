using System;
using System.Collections;
using System.Reflection;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200089B RID: 2203
	internal class RemotingXmlConfigFileData
	{
		// Token: 0x06005D4A RID: 23882 RVA: 0x001482A8 File Offset: 0x001464A8
		internal void AddInteropXmlElementEntry(string xmlElementName, string xmlElementNamespace, string urtTypeName, string urtAssemblyName)
		{
			this.TryToLoadTypeIfApplicable(urtTypeName, urtAssemblyName);
			RemotingXmlConfigFileData.InteropXmlElementEntry interopXmlElementEntry = new RemotingXmlConfigFileData.InteropXmlElementEntry(xmlElementName, xmlElementNamespace, urtTypeName, urtAssemblyName);
			this.InteropXmlElementEntries.Add(interopXmlElementEntry);
		}

		// Token: 0x06005D4B RID: 23883 RVA: 0x001482D8 File Offset: 0x001464D8
		internal void AddInteropXmlTypeEntry(string xmlTypeName, string xmlTypeNamespace, string urtTypeName, string urtAssemblyName)
		{
			this.TryToLoadTypeIfApplicable(urtTypeName, urtAssemblyName);
			RemotingXmlConfigFileData.InteropXmlTypeEntry interopXmlTypeEntry = new RemotingXmlConfigFileData.InteropXmlTypeEntry(xmlTypeName, xmlTypeNamespace, urtTypeName, urtAssemblyName);
			this.InteropXmlTypeEntries.Add(interopXmlTypeEntry);
		}

		// Token: 0x06005D4C RID: 23884 RVA: 0x00148308 File Offset: 0x00146508
		internal void AddPreLoadEntry(string typeName, string assemblyName)
		{
			this.TryToLoadTypeIfApplicable(typeName, assemblyName);
			RemotingXmlConfigFileData.PreLoadEntry preLoadEntry = new RemotingXmlConfigFileData.PreLoadEntry(typeName, assemblyName);
			this.PreLoadEntries.Add(preLoadEntry);
		}

		// Token: 0x06005D4D RID: 23885 RVA: 0x00148334 File Offset: 0x00146534
		internal RemotingXmlConfigFileData.RemoteAppEntry AddRemoteAppEntry(string appUri)
		{
			RemotingXmlConfigFileData.RemoteAppEntry remoteAppEntry = new RemotingXmlConfigFileData.RemoteAppEntry(appUri);
			this.RemoteAppEntries.Add(remoteAppEntry);
			return remoteAppEntry;
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x00148358 File Offset: 0x00146558
		internal void AddServerActivatedEntry(string typeName, string assemName, ArrayList contextAttributes)
		{
			this.TryToLoadTypeIfApplicable(typeName, assemName);
			RemotingXmlConfigFileData.TypeEntry typeEntry = new RemotingXmlConfigFileData.TypeEntry(typeName, assemName, contextAttributes);
			this.ServerActivatedEntries.Add(typeEntry);
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x00148384 File Offset: 0x00146584
		internal RemotingXmlConfigFileData.ServerWellKnownEntry AddServerWellKnownEntry(string typeName, string assemName, ArrayList contextAttributes, string objURI, WellKnownObjectMode objMode)
		{
			this.TryToLoadTypeIfApplicable(typeName, assemName);
			RemotingXmlConfigFileData.ServerWellKnownEntry serverWellKnownEntry = new RemotingXmlConfigFileData.ServerWellKnownEntry(typeName, assemName, contextAttributes, objURI, objMode);
			this.ServerWellKnownEntries.Add(serverWellKnownEntry);
			return serverWellKnownEntry;
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x001483B4 File Offset: 0x001465B4
		private void TryToLoadTypeIfApplicable(string typeName, string assemblyName)
		{
			if (!RemotingXmlConfigFileData.LoadTypes)
			{
				return;
			}
			Assembly assembly = Assembly.Load(assemblyName);
			if (assembly == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_AssemblyLoadFailed", new object[] { assemblyName }));
			}
			Type type = assembly.GetType(typeName, false, false);
			if (type == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_BadType", new object[] { typeName }));
			}
		}

		// Token: 0x04002A03 RID: 10755
		internal static volatile bool LoadTypes;

		// Token: 0x04002A04 RID: 10756
		internal string ApplicationName;

		// Token: 0x04002A05 RID: 10757
		internal RemotingXmlConfigFileData.LifetimeEntry Lifetime;

		// Token: 0x04002A06 RID: 10758
		internal bool UrlObjRefMode = RemotingConfigHandler.UrlObjRefMode;

		// Token: 0x04002A07 RID: 10759
		internal RemotingXmlConfigFileData.CustomErrorsEntry CustomErrors;

		// Token: 0x04002A08 RID: 10760
		internal ArrayList ChannelEntries = new ArrayList();

		// Token: 0x04002A09 RID: 10761
		internal ArrayList InteropXmlElementEntries = new ArrayList();

		// Token: 0x04002A0A RID: 10762
		internal ArrayList InteropXmlTypeEntries = new ArrayList();

		// Token: 0x04002A0B RID: 10763
		internal ArrayList PreLoadEntries = new ArrayList();

		// Token: 0x04002A0C RID: 10764
		internal ArrayList RemoteAppEntries = new ArrayList();

		// Token: 0x04002A0D RID: 10765
		internal ArrayList ServerActivatedEntries = new ArrayList();

		// Token: 0x04002A0E RID: 10766
		internal ArrayList ServerWellKnownEntries = new ArrayList();

		// Token: 0x02000C78 RID: 3192
		internal class ChannelEntry
		{
			// Token: 0x060070E2 RID: 28898 RVA: 0x00186466 File Offset: 0x00184666
			internal ChannelEntry(string typeName, string assemblyName, Hashtable properties)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemblyName;
				this.Properties = properties;
			}

			// Token: 0x0400380B RID: 14347
			internal string TypeName;

			// Token: 0x0400380C RID: 14348
			internal string AssemblyName;

			// Token: 0x0400380D RID: 14349
			internal Hashtable Properties;

			// Token: 0x0400380E RID: 14350
			internal bool DelayLoad;

			// Token: 0x0400380F RID: 14351
			internal ArrayList ClientSinkProviders = new ArrayList();

			// Token: 0x04003810 RID: 14352
			internal ArrayList ServerSinkProviders = new ArrayList();
		}

		// Token: 0x02000C79 RID: 3193
		internal class ClientWellKnownEntry
		{
			// Token: 0x060070E3 RID: 28899 RVA: 0x00186499 File Offset: 0x00184699
			internal ClientWellKnownEntry(string typeName, string assemName, string url)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemName;
				this.Url = url;
			}

			// Token: 0x04003811 RID: 14353
			internal string TypeName;

			// Token: 0x04003812 RID: 14354
			internal string AssemblyName;

			// Token: 0x04003813 RID: 14355
			internal string Url;
		}

		// Token: 0x02000C7A RID: 3194
		internal class ContextAttributeEntry
		{
			// Token: 0x060070E4 RID: 28900 RVA: 0x001864B6 File Offset: 0x001846B6
			internal ContextAttributeEntry(string typeName, string assemName, Hashtable properties)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemName;
				this.Properties = properties;
			}

			// Token: 0x04003814 RID: 14356
			internal string TypeName;

			// Token: 0x04003815 RID: 14357
			internal string AssemblyName;

			// Token: 0x04003816 RID: 14358
			internal Hashtable Properties;
		}

		// Token: 0x02000C7B RID: 3195
		internal class InteropXmlElementEntry
		{
			// Token: 0x060070E5 RID: 28901 RVA: 0x001864D3 File Offset: 0x001846D3
			internal InteropXmlElementEntry(string xmlElementName, string xmlElementNamespace, string urtTypeName, string urtAssemblyName)
			{
				this.XmlElementName = xmlElementName;
				this.XmlElementNamespace = xmlElementNamespace;
				this.UrtTypeName = urtTypeName;
				this.UrtAssemblyName = urtAssemblyName;
			}

			// Token: 0x04003817 RID: 14359
			internal string XmlElementName;

			// Token: 0x04003818 RID: 14360
			internal string XmlElementNamespace;

			// Token: 0x04003819 RID: 14361
			internal string UrtTypeName;

			// Token: 0x0400381A RID: 14362
			internal string UrtAssemblyName;
		}

		// Token: 0x02000C7C RID: 3196
		internal class CustomErrorsEntry
		{
			// Token: 0x060070E6 RID: 28902 RVA: 0x001864F8 File Offset: 0x001846F8
			internal CustomErrorsEntry(CustomErrorsModes mode)
			{
				this.Mode = mode;
			}

			// Token: 0x0400381B RID: 14363
			internal CustomErrorsModes Mode;
		}

		// Token: 0x02000C7D RID: 3197
		internal class InteropXmlTypeEntry
		{
			// Token: 0x060070E7 RID: 28903 RVA: 0x00186507 File Offset: 0x00184707
			internal InteropXmlTypeEntry(string xmlTypeName, string xmlTypeNamespace, string urtTypeName, string urtAssemblyName)
			{
				this.XmlTypeName = xmlTypeName;
				this.XmlTypeNamespace = xmlTypeNamespace;
				this.UrtTypeName = urtTypeName;
				this.UrtAssemblyName = urtAssemblyName;
			}

			// Token: 0x0400381C RID: 14364
			internal string XmlTypeName;

			// Token: 0x0400381D RID: 14365
			internal string XmlTypeNamespace;

			// Token: 0x0400381E RID: 14366
			internal string UrtTypeName;

			// Token: 0x0400381F RID: 14367
			internal string UrtAssemblyName;
		}

		// Token: 0x02000C7E RID: 3198
		internal class LifetimeEntry
		{
			// Token: 0x1700135C RID: 4956
			// (get) Token: 0x060070E8 RID: 28904 RVA: 0x0018652C File Offset: 0x0018472C
			// (set) Token: 0x060070E9 RID: 28905 RVA: 0x00186534 File Offset: 0x00184734
			internal TimeSpan LeaseTime
			{
				get
				{
					return this._leaseTime;
				}
				set
				{
					this._leaseTime = value;
					this.IsLeaseTimeSet = true;
				}
			}

			// Token: 0x1700135D RID: 4957
			// (get) Token: 0x060070EA RID: 28906 RVA: 0x00186544 File Offset: 0x00184744
			// (set) Token: 0x060070EB RID: 28907 RVA: 0x0018654C File Offset: 0x0018474C
			internal TimeSpan RenewOnCallTime
			{
				get
				{
					return this._renewOnCallTime;
				}
				set
				{
					this._renewOnCallTime = value;
					this.IsRenewOnCallTimeSet = true;
				}
			}

			// Token: 0x1700135E RID: 4958
			// (get) Token: 0x060070EC RID: 28908 RVA: 0x0018655C File Offset: 0x0018475C
			// (set) Token: 0x060070ED RID: 28909 RVA: 0x00186564 File Offset: 0x00184764
			internal TimeSpan SponsorshipTimeout
			{
				get
				{
					return this._sponsorshipTimeout;
				}
				set
				{
					this._sponsorshipTimeout = value;
					this.IsSponsorshipTimeoutSet = true;
				}
			}

			// Token: 0x1700135F RID: 4959
			// (get) Token: 0x060070EE RID: 28910 RVA: 0x00186574 File Offset: 0x00184774
			// (set) Token: 0x060070EF RID: 28911 RVA: 0x0018657C File Offset: 0x0018477C
			internal TimeSpan LeaseManagerPollTime
			{
				get
				{
					return this._leaseManagerPollTime;
				}
				set
				{
					this._leaseManagerPollTime = value;
					this.IsLeaseManagerPollTimeSet = true;
				}
			}

			// Token: 0x04003820 RID: 14368
			internal bool IsLeaseTimeSet;

			// Token: 0x04003821 RID: 14369
			internal bool IsRenewOnCallTimeSet;

			// Token: 0x04003822 RID: 14370
			internal bool IsSponsorshipTimeoutSet;

			// Token: 0x04003823 RID: 14371
			internal bool IsLeaseManagerPollTimeSet;

			// Token: 0x04003824 RID: 14372
			private TimeSpan _leaseTime;

			// Token: 0x04003825 RID: 14373
			private TimeSpan _renewOnCallTime;

			// Token: 0x04003826 RID: 14374
			private TimeSpan _sponsorshipTimeout;

			// Token: 0x04003827 RID: 14375
			private TimeSpan _leaseManagerPollTime;
		}

		// Token: 0x02000C7F RID: 3199
		internal class PreLoadEntry
		{
			// Token: 0x060070F1 RID: 28913 RVA: 0x00186594 File Offset: 0x00184794
			public PreLoadEntry(string typeName, string assemblyName)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemblyName;
			}

			// Token: 0x04003828 RID: 14376
			internal string TypeName;

			// Token: 0x04003829 RID: 14377
			internal string AssemblyName;
		}

		// Token: 0x02000C80 RID: 3200
		internal class RemoteAppEntry
		{
			// Token: 0x060070F2 RID: 28914 RVA: 0x001865AA File Offset: 0x001847AA
			internal RemoteAppEntry(string appUri)
			{
				this.AppUri = appUri;
			}

			// Token: 0x060070F3 RID: 28915 RVA: 0x001865D0 File Offset: 0x001847D0
			internal void AddWellKnownEntry(string typeName, string assemName, string url)
			{
				RemotingXmlConfigFileData.ClientWellKnownEntry clientWellKnownEntry = new RemotingXmlConfigFileData.ClientWellKnownEntry(typeName, assemName, url);
				this.WellKnownObjects.Add(clientWellKnownEntry);
			}

			// Token: 0x060070F4 RID: 28916 RVA: 0x001865F4 File Offset: 0x001847F4
			internal void AddActivatedEntry(string typeName, string assemName, ArrayList contextAttributes)
			{
				RemotingXmlConfigFileData.TypeEntry typeEntry = new RemotingXmlConfigFileData.TypeEntry(typeName, assemName, contextAttributes);
				this.ActivatedObjects.Add(typeEntry);
			}

			// Token: 0x0400382A RID: 14378
			internal string AppUri;

			// Token: 0x0400382B RID: 14379
			internal ArrayList WellKnownObjects = new ArrayList();

			// Token: 0x0400382C RID: 14380
			internal ArrayList ActivatedObjects = new ArrayList();
		}

		// Token: 0x02000C81 RID: 3201
		internal class ServerWellKnownEntry : RemotingXmlConfigFileData.TypeEntry
		{
			// Token: 0x060070F5 RID: 28917 RVA: 0x00186617 File Offset: 0x00184817
			internal ServerWellKnownEntry(string typeName, string assemName, ArrayList contextAttributes, string objURI, WellKnownObjectMode objMode)
				: base(typeName, assemName, contextAttributes)
			{
				this.ObjectURI = objURI;
				this.ObjectMode = objMode;
			}

			// Token: 0x0400382D RID: 14381
			internal string ObjectURI;

			// Token: 0x0400382E RID: 14382
			internal WellKnownObjectMode ObjectMode;
		}

		// Token: 0x02000C82 RID: 3202
		internal class SinkProviderEntry
		{
			// Token: 0x060070F6 RID: 28918 RVA: 0x00186632 File Offset: 0x00184832
			internal SinkProviderEntry(string typeName, string assemName, Hashtable properties, bool isFormatter)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemName;
				this.Properties = properties;
				this.IsFormatter = isFormatter;
			}

			// Token: 0x0400382F RID: 14383
			internal string TypeName;

			// Token: 0x04003830 RID: 14384
			internal string AssemblyName;

			// Token: 0x04003831 RID: 14385
			internal Hashtable Properties;

			// Token: 0x04003832 RID: 14386
			internal ArrayList ProviderData = new ArrayList();

			// Token: 0x04003833 RID: 14387
			internal bool IsFormatter;
		}

		// Token: 0x02000C83 RID: 3203
		internal class TypeEntry
		{
			// Token: 0x060070F7 RID: 28919 RVA: 0x00186662 File Offset: 0x00184862
			internal TypeEntry(string typeName, string assemName, ArrayList contextAttributes)
			{
				this.TypeName = typeName;
				this.AssemblyName = assemName;
				this.ContextAttributes = contextAttributes;
			}

			// Token: 0x04003834 RID: 14388
			internal string TypeName;

			// Token: 0x04003835 RID: 14389
			internal string AssemblyName;

			// Token: 0x04003836 RID: 14390
			internal ArrayList ContextAttributes;
		}
	}
}
