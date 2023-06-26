using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Services.Client.Metadata;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Values;

namespace System.Data.Services.Client
{
	// Token: 0x0200011C RID: 284
	[DebuggerDisplay("State = {state}, Uri = {editLink}, Element = {entity.GetType().ToString()}")]
	public sealed class EntityDescriptor : Descriptor
	{
		// Token: 0x06000946 RID: 2374 RVA: 0x0002598E File Offset: 0x00023B8E
		internal EntityDescriptor(ClientEdmModel model)
			: base(EntityStates.Unchanged)
		{
			this.Model = model;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0002599E File Offset: 0x00023B9E
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x000259A6 File Offset: 0x00023BA6
		public string Identity
		{
			get
			{
				return this.identity;
			}
			internal set
			{
				Util.CheckArgumentNullAndEmpty(value, "Identity");
				this.identity = value;
				this.ParentForInsert = null;
				this.ParentPropertyForInsert = null;
				this.addToUri = null;
				this.identity = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x000259D6 File Offset: 0x00023BD6
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x000259DE File Offset: 0x00023BDE
		public Uri SelfLink
		{
			get
			{
				return this.selfLink;
			}
			internal set
			{
				this.selfLink = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x000259E7 File Offset: 0x00023BE7
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x000259EF File Offset: 0x00023BEF
		public Uri EditLink
		{
			get
			{
				return this.editLink;
			}
			internal set
			{
				this.editLink = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x000259F8 File Offset: 0x00023BF8
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x00025A0F File Offset: 0x00023C0F
		public Uri ReadStreamUri
		{
			get
			{
				if (this.defaultStreamDescriptor == null)
				{
					return null;
				}
				return this.defaultStreamDescriptor.SelfLink;
			}
			internal set
			{
				if (value != null)
				{
					this.CreateDefaultStreamDescriptor().SelfLink = value;
				}
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00025A26 File Offset: 0x00023C26
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x00025A3D File Offset: 0x00023C3D
		public Uri EditStreamUri
		{
			get
			{
				if (this.defaultStreamDescriptor == null)
				{
					return null;
				}
				return this.defaultStreamDescriptor.EditLink;
			}
			internal set
			{
				if (value != null)
				{
					this.CreateDefaultStreamDescriptor().EditLink = value;
				}
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00025A54 File Offset: 0x00023C54
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x00025A5C File Offset: 0x00023C5C
		public object Entity
		{
			get
			{
				return this.entity;
			}
			internal set
			{
				this.entity = value;
				if (value != null)
				{
					IEdmType orCreateEdmType = this.Model.GetOrCreateEdmType(value.GetType());
					ClientTypeAnnotation clientTypeAnnotation = this.Model.GetClientTypeAnnotation(orCreateEdmType);
					this.EdmValue = new ClientEdmStructuredValue(value, this.Model, clientTypeAnnotation);
					if (clientTypeAnnotation.IsMediaLinkEntry)
					{
						this.CreateDefaultStreamDescriptor();
					}
				}
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00025AB4 File Offset: 0x00023CB4
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x00025ABC File Offset: 0x00023CBC
		public string ETag { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x00025AC5 File Offset: 0x00023CC5
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x00025ADC File Offset: 0x00023CDC
		public string StreamETag
		{
			get
			{
				if (this.defaultStreamDescriptor == null)
				{
					return null;
				}
				return this.defaultStreamDescriptor.ETag;
			}
			internal set
			{
				this.CreateDefaultStreamDescriptor().ETag = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00025AEA File Offset: 0x00023CEA
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x00025AF2 File Offset: 0x00023CF2
		public EntityDescriptor ParentForInsert { get; internal set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00025AFB File Offset: 0x00023CFB
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x00025B03 File Offset: 0x00023D03
		public string ParentPropertyForInsert { get; internal set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x00025B0C File Offset: 0x00023D0C
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x00025B14 File Offset: 0x00023D14
		public string ServerTypeName { get; internal set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00025B1D File Offset: 0x00023D1D
		public ReadOnlyCollection<LinkInfo> LinkInfos
		{
			get
			{
				if (this.relatedEntityLinks != null)
				{
					return new ReadOnlyCollection<LinkInfo>(this.relatedEntityLinks.Values.ToList<LinkInfo>());
				}
				return new ReadOnlyCollection<LinkInfo>(new List<LinkInfo>(0));
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00025B48 File Offset: 0x00023D48
		public ReadOnlyCollection<StreamDescriptor> StreamDescriptors
		{
			get
			{
				if (this.streamDescriptors != null)
				{
					return new ReadOnlyCollection<StreamDescriptor>(this.streamDescriptors.Values.ToList<StreamDescriptor>());
				}
				return new ReadOnlyCollection<StreamDescriptor>(new List<StreamDescriptor>(0));
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00025B73 File Offset: 0x00023D73
		public ReadOnlyCollection<OperationDescriptor> OperationDescriptors
		{
			get
			{
				if (this.operationDescriptors != null)
				{
					return new ReadOnlyCollection<OperationDescriptor>(this.operationDescriptors);
				}
				return new ReadOnlyCollection<OperationDescriptor>(new List<OperationDescriptor>());
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00025B93 File Offset: 0x00023D93
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x00025B9B File Offset: 0x00023D9B
		internal ClientEdmModel Model { get; private set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00025BA4 File Offset: 0x00023DA4
		internal object ParentEntity
		{
			get
			{
				if (this.ParentForInsert == null)
				{
					return null;
				}
				return this.ParentForInsert.entity;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00025BBB File Offset: 0x00023DBB
		internal override DescriptorKind DescriptorKind
		{
			get
			{
				return DescriptorKind.Entity;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00025BBE File Offset: 0x00023DBE
		internal bool IsDeepInsert
		{
			get
			{
				return this.ParentForInsert != null;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00025BCC File Offset: 0x00023DCC
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x00025BE3 File Offset: 0x00023DE3
		internal DataServiceSaveStream SaveStream
		{
			get
			{
				if (this.defaultStreamDescriptor == null)
				{
					return null;
				}
				return this.defaultStreamDescriptor.SaveStream;
			}
			set
			{
				this.CreateDefaultStreamDescriptor().SaveStream = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00025BF1 File Offset: 0x00023DF1
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x00025C08 File Offset: 0x00023E08
		internal EntityStates StreamState
		{
			get
			{
				if (this.defaultStreamDescriptor == null)
				{
					return EntityStates.Unchanged;
				}
				return this.defaultStreamDescriptor.State;
			}
			set
			{
				this.defaultStreamDescriptor.State = value;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00025C16 File Offset: 0x00023E16
		internal bool IsMediaLinkEntry
		{
			get
			{
				return this.defaultStreamDescriptor != null;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00025C24 File Offset: 0x00023E24
		internal override bool IsModified
		{
			get
			{
				return base.IsModified || (this.defaultStreamDescriptor != null && this.defaultStreamDescriptor.SaveStream != null);
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00025C4B File Offset: 0x00023E4B
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00025C54 File Offset: 0x00023E54
		internal EntityDescriptor TransientEntityDescriptor
		{
			get
			{
				return this.transientEntityDescriptor;
			}
			set
			{
				if (this.transientEntityDescriptor == null)
				{
					this.transientEntityDescriptor = value;
				}
				else
				{
					AtomMaterializerLog.MergeEntityDescriptorInfo(this.transientEntityDescriptor, value, true, MergeOption.OverwriteChanges);
				}
				if (value.streamDescriptors != null && this.streamDescriptors != null)
				{
					foreach (StreamDescriptor streamDescriptor in value.streamDescriptors.Values)
					{
						StreamDescriptor streamDescriptor2;
						if (this.streamDescriptors.TryGetValue(streamDescriptor.Name, out streamDescriptor2))
						{
							streamDescriptor2.TransientNamedStreamInfo = streamDescriptor;
						}
					}
				}
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00025CF0 File Offset: 0x00023EF0
		internal StreamDescriptor DefaultStreamDescriptor
		{
			get
			{
				return this.defaultStreamDescriptor;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00025CF8 File Offset: 0x00023EF8
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x00025D00 File Offset: 0x00023F00
		internal IEdmStructuredValue EdmValue { get; private set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00025D09 File Offset: 0x00023F09
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x00025D11 File Offset: 0x00023F11
		internal string EntitySetName { get; set; }

		// Token: 0x06000972 RID: 2418 RVA: 0x00025D1A File Offset: 0x00023F1A
		internal string GetLatestIdentity()
		{
			if (this.TransientEntityDescriptor != null && this.TransientEntityDescriptor.Identity != null)
			{
				return this.TransientEntityDescriptor.Identity;
			}
			return this.Identity;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00025D43 File Offset: 0x00023F43
		internal Uri GetLatestEditLink()
		{
			if (this.TransientEntityDescriptor != null && this.TransientEntityDescriptor.EditLink != null)
			{
				return this.TransientEntityDescriptor.EditLink;
			}
			return this.EditLink;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00025D72 File Offset: 0x00023F72
		internal Uri GetLatestEditStreamUri()
		{
			if (this.TransientEntityDescriptor != null && this.TransientEntityDescriptor.EditStreamUri != null)
			{
				return this.TransientEntityDescriptor.EditStreamUri;
			}
			return this.EditStreamUri;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00025DA1 File Offset: 0x00023FA1
		internal string GetLatestETag()
		{
			if (this.TransientEntityDescriptor != null && !string.IsNullOrEmpty(this.TransientEntityDescriptor.ETag))
			{
				return this.TransientEntityDescriptor.ETag;
			}
			return this.ETag;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00025DCF File Offset: 0x00023FCF
		internal string GetLatestStreamETag()
		{
			if (this.TransientEntityDescriptor != null && !string.IsNullOrEmpty(this.TransientEntityDescriptor.StreamETag))
			{
				return this.TransientEntityDescriptor.StreamETag;
			}
			return this.StreamETag;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00025DFD File Offset: 0x00023FFD
		internal string GetLatestServerTypeName()
		{
			if (this.TransientEntityDescriptor != null && !string.IsNullOrEmpty(this.TransientEntityDescriptor.ServerTypeName))
			{
				return this.TransientEntityDescriptor.ServerTypeName;
			}
			return this.ServerTypeName;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00025E2C File Offset: 0x0002402C
		internal Uri GetResourceUri(UriResolver baseUriResolver, bool queryLink)
		{
			if (this.ParentForInsert == null)
			{
				return this.GetLink(queryLink);
			}
			if (this.ParentForInsert.Identity == null)
			{
				Uri uri = UriUtil.CreateUri("$" + this.ParentForInsert.ChangeOrder.ToString(CultureInfo.InvariantCulture), UriKind.Relative);
				Uri orCreateAbsoluteUri = baseUriResolver.GetOrCreateAbsoluteUri(uri);
				Uri uri2 = UriUtil.CreateUri(this.ParentPropertyForInsert, UriKind.Relative);
				return UriUtil.CreateUri(orCreateAbsoluteUri, uri2);
			}
			LinkInfo linkInfo;
			if (this.ParentForInsert.TryGetLinkInfo(this.ParentPropertyForInsert, out linkInfo) && linkInfo.NavigationLink != null)
			{
				return linkInfo.NavigationLink;
			}
			return UriUtil.CreateUri(this.ParentForInsert.GetLink(queryLink), this.GetLink(queryLink));
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00025EE2 File Offset: 0x000240E2
		internal bool IsRelatedEntity(LinkDescriptor related)
		{
			return this.entity == related.Source || this.entity == related.Target;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00025F02 File Offset: 0x00024102
		internal LinkDescriptor GetRelatedEnd()
		{
			return new LinkDescriptor(this.ParentForInsert.entity, this.ParentPropertyForInsert, this.entity, this.Model);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00025F26 File Offset: 0x00024126
		internal override void ClearChanges()
		{
			this.transientEntityDescriptor = null;
			this.CloseSaveStream();
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00025F35 File Offset: 0x00024135
		internal void CloseSaveStream()
		{
			if (this.defaultStreamDescriptor != null)
			{
				this.defaultStreamDescriptor.CloseSaveStream();
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00025F4C File Offset: 0x0002414C
		internal void AddNavigationLink(string propertyName, Uri navigationUri)
		{
			LinkInfo linkInfo = this.GetLinkInfo(propertyName);
			linkInfo.NavigationLink = navigationUri;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00025F68 File Offset: 0x00024168
		internal void AddAssociationLink(string propertyName, Uri associationUri)
		{
			LinkInfo linkInfo = this.GetLinkInfo(propertyName);
			linkInfo.AssociationLink = associationUri;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00025F84 File Offset: 0x00024184
		internal void MergeLinkInfo(LinkInfo linkInfo)
		{
			if (this.relatedEntityLinks == null)
			{
				this.relatedEntityLinks = new Dictionary<string, LinkInfo>(StringComparer.Ordinal);
			}
			LinkInfo linkInfo2 = null;
			if (!this.relatedEntityLinks.TryGetValue(linkInfo.Name, out linkInfo2))
			{
				this.relatedEntityLinks[linkInfo.Name] = linkInfo;
				return;
			}
			if (linkInfo.AssociationLink != null)
			{
				linkInfo2.AssociationLink = linkInfo.AssociationLink;
			}
			if (linkInfo.NavigationLink != null)
			{
				linkInfo2.NavigationLink = linkInfo.NavigationLink;
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00026008 File Offset: 0x00024208
		internal Uri GetNavigationLink(UriResolver baseUriResolver, ClientPropertyAnnotation property)
		{
			LinkInfo linkInfo = null;
			Uri uri = null;
			if (this.TryGetLinkInfo(property.PropertyName, out linkInfo))
			{
				uri = linkInfo.NavigationLink;
			}
			if (uri == null)
			{
				Uri uri2 = UriUtil.CreateUri(property.PropertyName + (property.IsEntityCollection ? "()" : string.Empty), UriKind.Relative);
				uri = UriUtil.CreateUri(this.GetResourceUri(baseUriResolver, true), uri2);
			}
			return uri;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0002606F File Offset: 0x0002426F
		internal bool TryGetLinkInfo(string propertyName, out LinkInfo linkInfo)
		{
			Util.CheckArgumentNullAndEmpty(propertyName, "propertyName");
			linkInfo = null;
			return (this.TransientEntityDescriptor != null && this.TransientEntityDescriptor.TryGetLinkInfo(propertyName, out linkInfo)) || (this.relatedEntityLinks != null && this.relatedEntityLinks.TryGetValue(propertyName, out linkInfo));
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000260B0 File Offset: 0x000242B0
		internal StreamDescriptor AddStreamInfoIfNotPresent(string name)
		{
			if (this.streamDescriptors == null)
			{
				this.streamDescriptors = new Dictionary<string, StreamDescriptor>(StringComparer.Ordinal);
			}
			StreamDescriptor streamDescriptor;
			if (!this.streamDescriptors.TryGetValue(name, out streamDescriptor))
			{
				streamDescriptor = new StreamDescriptor(name, this);
				this.streamDescriptors.Add(name, streamDescriptor);
			}
			return streamDescriptor;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000260FB File Offset: 0x000242FB
		internal void AddOperationDescriptor(OperationDescriptor operationDescriptor)
		{
			if (this.operationDescriptors == null)
			{
				this.operationDescriptors = new List<OperationDescriptor>();
			}
			this.operationDescriptors.Add(operationDescriptor);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0002611C File Offset: 0x0002431C
		internal void ClearOperationDescriptors()
		{
			if (this.operationDescriptors != null)
			{
				this.operationDescriptors.Clear();
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00026131 File Offset: 0x00024331
		internal void AppendOperationalDescriptors(IEnumerable<OperationDescriptor> descriptors)
		{
			if (this.operationDescriptors == null)
			{
				this.operationDescriptors = new List<OperationDescriptor>();
			}
			this.operationDescriptors.AddRange(descriptors);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00026152 File Offset: 0x00024352
		internal bool TryGetNamedStreamInfo(string name, out StreamDescriptor namedStreamInfo)
		{
			namedStreamInfo = null;
			return this.streamDescriptors != null && this.streamDescriptors.TryGetValue(name, out namedStreamInfo);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00026170 File Offset: 0x00024370
		internal void MergeStreamDescriptor(StreamDescriptor materializedStreamDescriptor)
		{
			if (this.streamDescriptors == null)
			{
				this.streamDescriptors = new Dictionary<string, StreamDescriptor>(StringComparer.Ordinal);
			}
			StreamDescriptor streamDescriptor = null;
			if (!this.streamDescriptors.TryGetValue(materializedStreamDescriptor.Name, out streamDescriptor))
			{
				this.streamDescriptors[materializedStreamDescriptor.Name] = materializedStreamDescriptor;
				materializedStreamDescriptor.EntityDescriptor = this;
				return;
			}
			StreamDescriptor.MergeStreamDescriptor(streamDescriptor, materializedStreamDescriptor);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000261CD File Offset: 0x000243CD
		internal void SetParentForInsert(EntityDescriptor parentDescriptor, string propertyForInsert)
		{
			this.ParentForInsert = parentDescriptor;
			this.ParentPropertyForInsert = propertyForInsert;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000261DD File Offset: 0x000243DD
		internal void SetEntitySetUriForInsert(Uri entitySetInsertUri)
		{
			this.addToUri = entitySetInsertUri;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000261E8 File Offset: 0x000243E8
		private LinkInfo GetLinkInfo(string propertyName)
		{
			if (this.relatedEntityLinks == null)
			{
				this.relatedEntityLinks = new Dictionary<string, LinkInfo>(StringComparer.Ordinal);
			}
			LinkInfo linkInfo = null;
			if (!this.relatedEntityLinks.TryGetValue(propertyName, out linkInfo))
			{
				linkInfo = new LinkInfo(propertyName);
				this.relatedEntityLinks[propertyName] = linkInfo;
			}
			return linkInfo;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00026234 File Offset: 0x00024434
		private Uri GetLink(bool queryLink)
		{
			if (queryLink && this.SelfLink != null)
			{
				return this.SelfLink;
			}
			Uri latestEditLink;
			if ((latestEditLink = this.GetLatestEditLink()) != null)
			{
				return latestEditLink;
			}
			if (base.State != EntityStates.Added)
			{
				throw new ArgumentNullException(Strings.EntityDescriptor_MissingSelfEditLink(this.identity));
			}
			if (this.addToUri != null)
			{
				return this.addToUri;
			}
			return UriUtil.CreateUri(this.ParentPropertyForInsert, UriKind.Relative);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000262A6 File Offset: 0x000244A6
		private StreamDescriptor CreateDefaultStreamDescriptor()
		{
			if (this.defaultStreamDescriptor == null)
			{
				this.defaultStreamDescriptor = new StreamDescriptor(this);
			}
			return this.defaultStreamDescriptor;
		}

		// Token: 0x0400056B RID: 1387
		private string identity;

		// Token: 0x0400056C RID: 1388
		private object entity;

		// Token: 0x0400056D RID: 1389
		private StreamDescriptor defaultStreamDescriptor;

		// Token: 0x0400056E RID: 1390
		private Uri addToUri;

		// Token: 0x0400056F RID: 1391
		private Uri selfLink;

		// Token: 0x04000570 RID: 1392
		private Uri editLink;

		// Token: 0x04000571 RID: 1393
		private Dictionary<string, LinkInfo> relatedEntityLinks;

		// Token: 0x04000572 RID: 1394
		private EntityDescriptor transientEntityDescriptor;

		// Token: 0x04000573 RID: 1395
		private Dictionary<string, StreamDescriptor> streamDescriptors;

		// Token: 0x04000574 RID: 1396
		private List<OperationDescriptor> operationDescriptors;
	}
}
