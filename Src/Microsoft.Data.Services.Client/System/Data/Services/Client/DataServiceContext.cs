using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Services.Client.Metadata;
using System.Data.Services.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Values;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200012D RID: 301
	public class DataServiceContext
	{
		// Token: 0x06000A19 RID: 2585 RVA: 0x00028D50 File Offset: 0x00026F50
		public DataServiceContext()
			: this(null)
		{
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00028D59 File Offset: 0x00026F59
		public DataServiceContext(Uri serviceRoot)
			: this(serviceRoot, DataServiceProtocolVersion.V2)
		{
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00028D63 File Offset: 0x00026F63
		public DataServiceContext(Uri serviceRoot, DataServiceProtocolVersion maxProtocolVersion)
			: this(serviceRoot, maxProtocolVersion, DataServiceContext.ClientEdmModelCache.GetModel(maxProtocolVersion))
		{
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00028D74 File Offset: 0x00026F74
		internal DataServiceContext(Uri serviceRoot, DataServiceProtocolVersion maxProtocolVersion, ClientEdmModel model)
		{
			this.model = model;
			this.baseUriResolver = UriResolver.CreateFromBaseUri(serviceRoot, "serviceRoot");
			this.maxProtocolVersion = Util.CheckEnumerationValue(maxProtocolVersion, "maxProtocolVersion");
			this.mergeOption = MergeOption.AppendOnly;
			this.dataNamespace = "http://schemas.microsoft.com/ado/2007/08/dataservices";
			this.entityTracker = new EntityTracker(model);
			this.typeScheme = new Uri("http://schemas.microsoft.com/ado/2007/08/dataservices/scheme");
			this.MaxProtocolVersionAsVersion = Util.GetVersionFromMaxProtocolVersion(maxProtocolVersion);
			this.formatTracker = new DataServiceClientFormat(this);
			this.urlConventions = DataServiceUrlConventions.Default;
			this.Configurations = new DataServiceClientConfigurations(this);
			this.httpStack = HttpStack.Auto;
			this.UsePostTunneling = false;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000A1D RID: 2589 RVA: 0x00028E1C File Offset: 0x0002701C
		// (remove) Token: 0x06000A1E RID: 2590 RVA: 0x00028E71 File Offset: 0x00027071
		[Obsolete("SendingRequest2 has been deprecated in favor of SendingRequest2.")]
		public event EventHandler<SendingRequestEventArgs> SendingRequest
		{
			add
			{
				if (this.HasBuildingRequestEventHandlers)
				{
					throw new DataServiceClientException(Strings.Context_BuildingRequestAndSendingRequestCannotBeUsedTogether);
				}
				if (this.Configurations.RequestPipeline.HasOnMessageCreating)
				{
					throw new DataServiceClientException(Strings.Context_SendingRequest_InvalidWhenUsingOnMessageCreating);
				}
				this.Configurations.RequestPipeline.ContextUsingSendingRequest = true;
				this.InnerSendingRequest += value;
			}
			remove
			{
				this.InnerSendingRequest -= value;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000A1F RID: 2591 RVA: 0x00028E7C File Offset: 0x0002707C
		// (remove) Token: 0x06000A20 RID: 2592 RVA: 0x00028EB4 File Offset: 0x000270B4
		public event EventHandler<SendingRequest2EventArgs> SendingRequest2;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000A21 RID: 2593 RVA: 0x00028EE9 File Offset: 0x000270E9
		// (remove) Token: 0x06000A22 RID: 2594 RVA: 0x00028F05 File Offset: 0x00027105
		public event EventHandler<BuildingRequestEventArgs> BuildingRequest
		{
			add
			{
				if (this.InnerSendingRequest != null)
				{
					throw new DataServiceClientException(Strings.Context_BuildingRequestAndSendingRequestCannotBeUsedTogether);
				}
				this.InnerBuildingRequest += value;
			}
			remove
			{
				this.InnerBuildingRequest -= value;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000A23 RID: 2595 RVA: 0x00028F0E File Offset: 0x0002710E
		// (remove) Token: 0x06000A24 RID: 2596 RVA: 0x00028F27 File Offset: 0x00027127
		public event EventHandler<ReadingWritingEntityEventArgs> ReadingEntity
		{
			add
			{
				this.CheckUsingAtom();
				this.Configurations.ResponsePipeline.ReadingAtomEntity += value;
			}
			remove
			{
				this.Configurations.ResponsePipeline.ReadingAtomEntity -= value;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000A25 RID: 2597 RVA: 0x00028F3C File Offset: 0x0002713C
		// (remove) Token: 0x06000A26 RID: 2598 RVA: 0x00028F74 File Offset: 0x00027174
		public event EventHandler<ReceivingResponseEventArgs> ReceivingResponse;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000A27 RID: 2599 RVA: 0x00028FA9 File Offset: 0x000271A9
		// (remove) Token: 0x06000A28 RID: 2600 RVA: 0x00028FB8 File Offset: 0x000271B8
		public event EventHandler<ReadingWritingEntityEventArgs> WritingEntity
		{
			add
			{
				this.CheckUsingAtom();
				this.WritingAtomEntity += value;
			}
			remove
			{
				this.WritingAtomEntity -= value;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000A29 RID: 2601 RVA: 0x00028FC4 File Offset: 0x000271C4
		// (remove) Token: 0x06000A2A RID: 2602 RVA: 0x00028FFC File Offset: 0x000271FC
		internal event EventHandler<SaveChangesEventArgs> ChangesSaved;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000A2B RID: 2603 RVA: 0x00029034 File Offset: 0x00027234
		// (remove) Token: 0x06000A2C RID: 2604 RVA: 0x0002906C File Offset: 0x0002726C
		private event EventHandler<SendingRequestEventArgs> InnerSendingRequest;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000A2D RID: 2605 RVA: 0x000290A4 File Offset: 0x000272A4
		// (remove) Token: 0x06000A2E RID: 2606 RVA: 0x000290DC File Offset: 0x000272DC
		private event EventHandler<BuildingRequestEventArgs> InnerBuildingRequest;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000A2F RID: 2607 RVA: 0x00029114 File Offset: 0x00027314
		// (remove) Token: 0x06000A30 RID: 2608 RVA: 0x0002914C File Offset: 0x0002734C
		private event EventHandler<ReadingWritingEntityEventArgs> WritingAtomEntity;

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00029181 File Offset: 0x00027381
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x0002918E File Offset: 0x0002738E
		public Func<string, Uri> ResolveEntitySet
		{
			get
			{
				return this.baseUriResolver.ResolveEntitySet;
			}
			set
			{
				this.baseUriResolver = this.baseUriResolver.CloneWithOverrideValue(value);
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x000291A2 File Offset: 0x000273A2
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x000291AF File Offset: 0x000273AF
		public Uri BaseUri
		{
			get
			{
				return this.baseUriResolver.RawBaseUriValue;
			}
			set
			{
				if (this.baseUriResolver == null)
				{
					this.baseUriResolver = UriResolver.CreateFromBaseUri(value, "serviceRoot");
					return;
				}
				this.baseUriResolver = this.baseUriResolver.CloneWithOverrideValue(value, null);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x000291DE File Offset: 0x000273DE
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x000291E6 File Offset: 0x000273E6
		public DataServiceResponsePreference AddAndUpdateResponsePreference
		{
			get
			{
				return this.addAndUpdateResponsePreference;
			}
			set
			{
				if (value != DataServiceResponsePreference.None)
				{
					this.EnsureMinimumProtocolVersionV3();
				}
				this.addAndUpdateResponsePreference = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x000291F8 File Offset: 0x000273F8
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x00029200 File Offset: 0x00027400
		public DataServiceProtocolVersion MaxProtocolVersion
		{
			get
			{
				return this.maxProtocolVersion;
			}
			internal set
			{
				this.maxProtocolVersion = value;
				this.MaxProtocolVersionAsVersion = Util.GetVersionFromMaxProtocolVersion(this.maxProtocolVersion);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002921A File Offset: 0x0002741A
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x00029222 File Offset: 0x00027422
		public ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0002922B File Offset: 0x0002742B
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x00029233 File Offset: 0x00027433
		public MergeOption MergeOption
		{
			get
			{
				return this.mergeOption;
			}
			set
			{
				this.mergeOption = Util.CheckEnumerationValue(value, "MergeOption");
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00029246 File Offset: 0x00027446
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x0002924E File Offset: 0x0002744E
		public bool ApplyingChanges
		{
			get
			{
				return this.applyingChanges;
			}
			internal set
			{
				this.applyingChanges = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00029257 File Offset: 0x00027457
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x0002925F File Offset: 0x0002745F
		public bool IgnoreMissingProperties
		{
			get
			{
				return this.ignoreMissingProperties;
			}
			set
			{
				this.ignoreMissingProperties = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00029268 File Offset: 0x00027468
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x00029270 File Offset: 0x00027470
		public bool AutoNullPropagation { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00029279 File Offset: 0x00027479
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x00029281 File Offset: 0x00027481
		public UndeclaredPropertyBehavior UndeclaredPropertyBehavior
		{
			get
			{
				return this.undeclaredPropertyBehavior;
			}
			set
			{
				this.undeclaredPropertyBehavior = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0002928A File Offset: 0x0002748A
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x00029292 File Offset: 0x00027492
		[Obsolete("You cannot change the default data namespace for an OData service that supports version 3 of the OData protocol, or a later version.", false)]
		public string DataNamespace
		{
			get
			{
				return this.dataNamespace;
			}
			set
			{
				Util.CheckArgumentNull<string>(value, "value");
				if (!string.Equals(value, "http://schemas.microsoft.com/ado/2007/08/dataservices", StringComparison.Ordinal))
				{
					this.EnsureMaximumProtocolVersionForProperty("DataNamespace", Util.DataServiceVersion2);
				}
				this.dataNamespace = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x000292C5 File Offset: 0x000274C5
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x000292D2 File Offset: 0x000274D2
		public Func<PropertyInfo, FieldInfo> ResolveBackingField
		{
			get
			{
				return this.model.ResolveBackingField;
			}
			set
			{
				this.model.ResolveBackingField = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x000292E0 File Offset: 0x000274E0
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x000292E8 File Offset: 0x000274E8
		public Func<Type, string> ResolveName
		{
			get
			{
				return this.resolveName;
			}
			set
			{
				this.resolveName = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x000292F1 File Offset: 0x000274F1
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x000292F9 File Offset: 0x000274F9
		public Func<string, Type> ResolveType
		{
			get
			{
				return this.resolveType;
			}
			set
			{
				this.resolveType = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00029302 File Offset: 0x00027502
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0002930A File Offset: 0x0002750A
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < 0)
				{
					throw Error.ArgumentOutOfRange("Timeout");
				}
				this.timeout = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00029322 File Offset: 0x00027522
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x0002932A File Offset: 0x0002752A
		[Obsolete("You cannot change the default type scheme for an OData service that supports version 3 of the OData protocol, or a later version.", false)]
		public Uri TypeScheme
		{
			get
			{
				return this.typeScheme;
			}
			set
			{
				Util.CheckArgumentNull<Uri>(value, "value");
				if (!string.Equals(value.AbsoluteUri, "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme", StringComparison.Ordinal))
				{
					this.EnsureMaximumProtocolVersionForProperty("TypeScheme", Util.DataServiceVersion2);
				}
				this.typeScheme = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00029362 File Offset: 0x00027562
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x0002936A File Offset: 0x0002756A
		public bool UsePostTunneling
		{
			get
			{
				return this.postTunneling;
			}
			set
			{
				this.postTunneling = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0002937B File Offset: 0x0002757B
		public ReadOnlyCollection<LinkDescriptor> Links
		{
			get
			{
				return new ReadOnlyCollection<LinkDescriptor>(this.entityTracker.Links.OrderBy((LinkDescriptor l) => l.ChangeOrder).ToList<LinkDescriptor>());
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x000293BC File Offset: 0x000275BC
		public ReadOnlyCollection<EntityDescriptor> Entities
		{
			get
			{
				return new ReadOnlyCollection<EntityDescriptor>(this.entityTracker.Entities.OrderBy((EntityDescriptor d) => d.ChangeOrder).ToList<EntityDescriptor>());
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x000293F5 File Offset: 0x000275F5
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x000293FD File Offset: 0x000275FD
		public SaveChangesOptions SaveChangesDefaultOptions
		{
			get
			{
				return this.saveChangesDefaultOptions;
			}
			set
			{
				this.ValidateSaveChangesOptions(value);
				this.saveChangesDefaultOptions = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0002940D File Offset: 0x0002760D
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x00029415 File Offset: 0x00027615
		public bool IgnoreResourceNotFoundException
		{
			get
			{
				return this.ignoreResourceNotFoundException;
			}
			set
			{
				this.ignoreResourceNotFoundException = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0002941E File Offset: 0x0002761E
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x00029426 File Offset: 0x00027626
		public DataServiceClientConfigurations Configurations { get; private set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002942F File Offset: 0x0002762F
		public DataServiceClientFormat Format
		{
			get
			{
				return this.formatTracker;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00029437 File Offset: 0x00027637
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x0002943F File Offset: 0x0002763F
		public DataServiceUrlConventions UrlConventions
		{
			get
			{
				return this.urlConventions;
			}
			set
			{
				Util.CheckArgumentNull<DataServiceUrlConventions>(value, "value");
				this.urlConventions = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00029454 File Offset: 0x00027654
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x0002945C File Offset: 0x0002765C
		public bool AllowDirectNetworkStreamReading { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00029465 File Offset: 0x00027665
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x0002946D File Offset: 0x0002766D
		internal bool UseDefaultCredentials { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00029476 File Offset: 0x00027676
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x0002947E File Offset: 0x0002767E
		internal HttpStack HttpStack
		{
			get
			{
				return this.httpStack;
			}
			set
			{
				this.httpStack = Util.CheckEnumerationValue(value, "HttpStack");
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00029491 File Offset: 0x00027691
		internal bool HasWritingEntityHandlers
		{
			[DebuggerStepThrough]
			get
			{
				return this.WritingAtomEntity != null;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0002949F File Offset: 0x0002769F
		internal bool HasAtomEventHandlers
		{
			[DebuggerStepThrough]
			get
			{
				return this.Configurations.ResponsePipeline.HasAtomReadingEntityHandlers || this.HasWritingEntityHandlers;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x000294BB File Offset: 0x000276BB
		internal bool HasSendingRequestEventHandlers
		{
			[DebuggerStepThrough]
			get
			{
				return this.SendingRequest2 == null && this.InnerSendingRequest != null;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x000294D3 File Offset: 0x000276D3
		internal bool HasSendingRequest2EventHandlers
		{
			[DebuggerStepThrough]
			get
			{
				return this.SendingRequest2 != null;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x000294E1 File Offset: 0x000276E1
		internal bool HasBuildingRequestEventHandlers
		{
			[DebuggerStepThrough]
			get
			{
				return this.InnerBuildingRequest != null;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x000294EF File Offset: 0x000276EF
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x000294F7 File Offset: 0x000276F7
		internal EntityTracker EntityTracker
		{
			get
			{
				return this.entityTracker;
			}
			set
			{
				this.entityTracker = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00029500 File Offset: 0x00027700
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x00029508 File Offset: 0x00027708
		internal DataServiceClientFormat FormatTracker
		{
			get
			{
				return this.formatTracker;
			}
			set
			{
				this.formatTracker = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00029511 File Offset: 0x00027711
		internal UriResolver BaseUriResolver
		{
			get
			{
				return this.baseUriResolver;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00029519 File Offset: 0x00027719
		internal ClientEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00029521 File Offset: 0x00027721
		public EntityDescriptor GetEntityDescriptor(object entity)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			return this.entityTracker.TryGetEntityDescriptor(entity);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002953B File Offset: 0x0002773B
		public LinkDescriptor GetLinkDescriptor(object source, string sourceProperty, object target)
		{
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNullAndEmpty(sourceProperty, "sourceProperty");
			Util.CheckArgumentNull<object>(target, "target");
			return this.entityTracker.TryGetLinkDescriptor(source, sourceProperty, target);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00029570 File Offset: 0x00027770
		public void CancelRequest(IAsyncResult asyncResult)
		{
			Util.CheckArgumentNull<IAsyncResult>(asyncResult, "asyncResult");
			BaseAsyncResult baseAsyncResult = asyncResult as BaseAsyncResult;
			if (baseAsyncResult == null || this != baseAsyncResult.Source)
			{
				object obj = null;
				if (baseAsyncResult != null)
				{
					DataServiceQuery dataServiceQuery = baseAsyncResult.Source as DataServiceQuery;
					if (dataServiceQuery != null)
					{
						DataServiceQueryProvider dataServiceQueryProvider = dataServiceQuery.Provider as DataServiceQueryProvider;
						if (dataServiceQueryProvider != null)
						{
							obj = dataServiceQueryProvider.Context;
						}
					}
				}
				if (this != obj)
				{
					throw Error.Argument(Strings.Context_DidNotOriginateAsync, "asyncResult");
				}
			}
			if (!baseAsyncResult.IsCompletedInternally)
			{
				baseAsyncResult.SetAborted();
				ODataRequestMessageWrapper abortable = baseAsyncResult.Abortable;
				if (abortable != null)
				{
					abortable.Abort();
				}
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00029600 File Offset: 0x00027800
		public DataServiceQuery<T> CreateQuery<T>(string entitySetName)
		{
			Util.CheckArgumentNullAndEmpty(entitySetName, "entitySetName");
			DataServiceContext.ValidateEntitySetName(ref entitySetName);
			ResourceSetExpression resourceSetExpression = new ResourceSetExpression(typeof(IOrderedQueryable<T>), null, Expression.Constant(entitySetName), typeof(T), null, CountOption.None, null, null, null, null);
			return new DataServiceQuery<T>.DataServiceOrderedQuery(resourceSetExpression, new DataServiceQueryProvider(this));
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00029654 File Offset: 0x00027854
		public Uri GetMetadataUri()
		{
			return UriUtil.CreateUri(UriUtil.UriToString(this.BaseUriResolver.GetBaseUriWithSlash()) + "$metadata", UriKind.Absolute);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00029683 File Offset: 0x00027883
		public IAsyncResult BeginLoadProperty(object entity, string propertyName, AsyncCallback callback, object state)
		{
			return this.BeginLoadProperty(entity, propertyName, null, callback, state);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00029694 File Offset: 0x00027894
		public IAsyncResult BeginLoadProperty(object entity, string propertyName, Uri nextLinkUri, AsyncCallback callback, object state)
		{
			LoadPropertyResult loadPropertyResult = this.CreateLoadPropertyRequest(entity, propertyName, callback, state, nextLinkUri, null);
			loadPropertyResult.BeginExecuteQuery();
			return loadPropertyResult;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000296B8 File Offset: 0x000278B8
		public IAsyncResult BeginLoadProperty(object entity, string propertyName, DataServiceQueryContinuation continuation, AsyncCallback callback, object state)
		{
			Util.CheckArgumentNull<DataServiceQueryContinuation>(continuation, "continuation");
			LoadPropertyResult loadPropertyResult = this.CreateLoadPropertyRequest(entity, propertyName, callback, state, null, continuation);
			loadPropertyResult.BeginExecuteQuery();
			return loadPropertyResult;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x000296E8 File Offset: 0x000278E8
		public QueryOperationResponse EndLoadProperty(IAsyncResult asyncResult)
		{
			LoadPropertyResult loadPropertyResult = BaseAsyncResult.EndExecute<LoadPropertyResult>(this, "LoadProperty", asyncResult);
			return loadPropertyResult.LoadProperty();
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00029708 File Offset: 0x00027908
		public QueryOperationResponse LoadProperty(object entity, string propertyName)
		{
			return this.LoadProperty(entity, propertyName, null);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00029714 File Offset: 0x00027914
		public QueryOperationResponse LoadProperty(object entity, string propertyName, Uri nextLinkUri)
		{
			LoadPropertyResult loadPropertyResult = this.CreateLoadPropertyRequest(entity, propertyName, null, null, nextLinkUri, null);
			loadPropertyResult.AllowDirectNetworkStreamReading = this.AllowDirectNetworkStreamReading;
			loadPropertyResult.ExecuteQuery();
			return loadPropertyResult.LoadProperty();
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00029748 File Offset: 0x00027948
		public QueryOperationResponse LoadProperty(object entity, string propertyName, DataServiceQueryContinuation continuation)
		{
			LoadPropertyResult loadPropertyResult = this.CreateLoadPropertyRequest(entity, propertyName, null, null, null, continuation);
			loadPropertyResult.AllowDirectNetworkStreamReading = this.AllowDirectNetworkStreamReading;
			loadPropertyResult.ExecuteQuery();
			return loadPropertyResult.LoadProperty();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002977C File Offset: 0x0002797C
		public QueryOperationResponse<T> LoadProperty<T>(object entity, string propertyName, DataServiceQueryContinuation<T> continuation)
		{
			LoadPropertyResult loadPropertyResult = this.CreateLoadPropertyRequest(entity, propertyName, null, null, null, continuation);
			loadPropertyResult.AllowDirectNetworkStreamReading = this.AllowDirectNetworkStreamReading;
			loadPropertyResult.ExecuteQuery();
			return (QueryOperationResponse<T>)loadPropertyResult.LoadProperty();
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x000297B4 File Offset: 0x000279B4
		public Uri GetReadStreamUri(object entity)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			return entityDescriptor.ReadStreamUri;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000297E0 File Offset: 0x000279E0
		public Uri GetReadStreamUri(object entity, string name)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			Util.CheckArgumentNullAndEmpty(name, "name");
			this.EnsureMinimumProtocolVersionV3();
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			StreamDescriptor streamDescriptor;
			if (entityDescriptor.TryGetNamedStreamInfo(name, out streamDescriptor))
			{
				return streamDescriptor.SelfLink ?? streamDescriptor.EditLink;
			}
			return null;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00029834 File Offset: 0x00027A34
		public IAsyncResult BeginGetReadStream(object entity, DataServiceRequestArgs args, AsyncCallback callback, object state)
		{
			GetReadStreamResult getReadStreamResult = this.CreateGetReadStreamResult(entity, args, callback, state, null);
			getReadStreamResult.Begin();
			return getReadStreamResult;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00029858 File Offset: 0x00027A58
		public IAsyncResult BeginGetReadStream(object entity, string name, DataServiceRequestArgs args, AsyncCallback callback, object state)
		{
			Util.CheckArgumentNullAndEmpty(name, "name");
			this.EnsureMinimumProtocolVersionV3();
			GetReadStreamResult getReadStreamResult = this.CreateGetReadStreamResult(entity, args, callback, state, name);
			getReadStreamResult.Begin();
			return getReadStreamResult;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002988C File Offset: 0x00027A8C
		public DataServiceStreamResponse EndGetReadStream(IAsyncResult asyncResult)
		{
			GetReadStreamResult getReadStreamResult = BaseAsyncResult.EndExecute<GetReadStreamResult>(this, "GetReadStream", asyncResult);
			return getReadStreamResult.End();
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x000298AC File Offset: 0x00027AAC
		public DataServiceStreamResponse GetReadStream(object entity)
		{
			DataServiceRequestArgs dataServiceRequestArgs = new DataServiceRequestArgs();
			return this.GetReadStream(entity, dataServiceRequestArgs);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x000298C8 File Offset: 0x00027AC8
		public DataServiceStreamResponse GetReadStream(object entity, string acceptContentType)
		{
			Util.CheckArgumentNullAndEmpty(acceptContentType, "acceptContentType");
			return this.GetReadStream(entity, new DataServiceRequestArgs
			{
				AcceptContentType = acceptContentType
			});
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000298F8 File Offset: 0x00027AF8
		public DataServiceStreamResponse GetReadStream(object entity, DataServiceRequestArgs args)
		{
			GetReadStreamResult getReadStreamResult = this.CreateGetReadStreamResult(entity, args, null, null, null);
			return getReadStreamResult.Execute();
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00029918 File Offset: 0x00027B18
		public DataServiceStreamResponse GetReadStream(object entity, string name, DataServiceRequestArgs args)
		{
			Util.CheckArgumentNullAndEmpty(name, "name");
			this.EnsureMinimumProtocolVersionV3();
			GetReadStreamResult getReadStreamResult = this.CreateGetReadStreamResult(entity, args, null, null, name);
			return getReadStreamResult.Execute();
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00029948 File Offset: 0x00027B48
		public void SetSaveStream(object entity, Stream stream, bool closeStream, string contentType, string slug)
		{
			Util.CheckArgumentNull<string>(contentType, "contentType");
			Util.CheckArgumentNull<string>(slug, "slug");
			this.SetSaveStream(entity, stream, closeStream, new DataServiceRequestArgs
			{
				ContentType = contentType,
				Slug = slug
			});
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00029990 File Offset: 0x00027B90
		public void SetSaveStream(object entity, Stream stream, bool closeStream, DataServiceRequestArgs args)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			Util.CheckArgumentNull<Stream>(stream, "stream");
			Util.CheckArgumentNull<DataServiceRequestArgs>(args, "args");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			ClientTypeAnnotation clientTypeAnnotation = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(entity.GetType()));
			if (clientTypeAnnotation.MediaDataMember != null)
			{
				throw new ArgumentException(Strings.Context_SetSaveStreamOnMediaEntryProperty(clientTypeAnnotation.ElementTypeName), "entity");
			}
			entityDescriptor.SaveStream = new DataServiceSaveStream(stream, closeStream, args);
			EntityStates state = entityDescriptor.State;
			switch (state)
			{
			case EntityStates.Unchanged:
				break;
			case EntityStates.Detached | EntityStates.Unchanged:
				goto IL_AF;
			case EntityStates.Added:
				entityDescriptor.StreamState = EntityStates.Added;
				return;
			default:
				if (state != EntityStates.Modified)
				{
					goto IL_AF;
				}
				break;
			}
			entityDescriptor.StreamState = EntityStates.Modified;
			return;
			IL_AF:
			throw new DataServiceClientException(Strings.Context_SetSaveStreamOnInvalidEntityState(Enum.GetName(typeof(EntityStates), entityDescriptor.State)));
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00029A70 File Offset: 0x00027C70
		public void SetSaveStream(object entity, string name, Stream stream, bool closeStream, string contentType)
		{
			Util.CheckArgumentNullAndEmpty(contentType, "contentType");
			this.SetSaveStream(entity, name, stream, closeStream, new DataServiceRequestArgs
			{
				ContentType = contentType
			});
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00029AA4 File Offset: 0x00027CA4
		public void SetSaveStream(object entity, string name, Stream stream, bool closeStream, DataServiceRequestArgs args)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			Util.CheckArgumentNullAndEmpty(name, "name");
			Util.CheckArgumentNull<Stream>(stream, "stream");
			Util.CheckArgumentNull<DataServiceRequestArgs>(args, "args");
			this.EnsureMinimumProtocolVersionV3();
			if (string.IsNullOrEmpty(args.ContentType))
			{
				throw Error.Argument(Strings.Context_ContentTypeRequiredForNamedStream, "args");
			}
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			if (entityDescriptor.State == EntityStates.Deleted)
			{
				throw new DataServiceClientException(Strings.Context_SetSaveStreamOnInvalidEntityState(Enum.GetName(typeof(EntityStates), entityDescriptor.State)));
			}
			StreamDescriptor streamDescriptor = entityDescriptor.AddStreamInfoIfNotPresent(name);
			streamDescriptor.SaveStream = new DataServiceSaveStream(stream, closeStream, args);
			streamDescriptor.State = EntityStates.Modified;
			this.entityTracker.IncrementChange(streamDescriptor);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00029B6C File Offset: 0x00027D6C
		public IAsyncResult BeginExecuteBatch(AsyncCallback callback, object state, params DataServiceRequest[] queries)
		{
			Util.CheckArgumentNotEmpty<DataServiceRequest>(queries, "queries");
			BatchSaveResult batchSaveResult = new BatchSaveResult(this, "ExecuteBatch", queries, SaveChangesOptions.Batch, callback, state);
			batchSaveResult.BatchBeginRequest();
			return batchSaveResult;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00029B9C File Offset: 0x00027D9C
		public DataServiceResponse EndExecuteBatch(IAsyncResult asyncResult)
		{
			BatchSaveResult batchSaveResult = BaseAsyncResult.EndExecute<BatchSaveResult>(this, "ExecuteBatch", asyncResult);
			return batchSaveResult.EndRequest();
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00029BBC File Offset: 0x00027DBC
		public DataServiceResponse ExecuteBatch(params DataServiceRequest[] queries)
		{
			Util.CheckArgumentNotEmpty<DataServiceRequest>(queries, "queries");
			BatchSaveResult batchSaveResult = new BatchSaveResult(this, "ExecuteBatch", queries, SaveChangesOptions.Batch, null, null);
			batchSaveResult.BatchRequest();
			return batchSaveResult.EndRequest();
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00029BF0 File Offset: 0x00027DF0
		public IAsyncResult BeginExecute<TElement>(Uri requestUri, AsyncCallback callback, object state)
		{
			return this.InnerBeginExecute<TElement>(requestUri, callback, state, "GET", "Execute", null, new OperationParameter[0]);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00029C1F File Offset: 0x00027E1F
		public IAsyncResult BeginExecute(Uri requestUri, AsyncCallback callback, object state, string httpMethod, params OperationParameter[] operationParameters)
		{
			return this.InnerBeginExecute<object>(requestUri, callback, state, httpMethod, "ExecuteVoid", new bool?(false), operationParameters);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00029C39 File Offset: 0x00027E39
		public IAsyncResult BeginExecute<TElement>(Uri requestUri, AsyncCallback callback, object state, string httpMethod, bool singleResult, params OperationParameter[] operationParameters)
		{
			return this.InnerBeginExecute<TElement>(requestUri, callback, state, httpMethod, "Execute", new bool?(singleResult), operationParameters);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00029C54 File Offset: 0x00027E54
		public IAsyncResult BeginExecute<T>(DataServiceQueryContinuation<T> continuation, AsyncCallback callback, object state)
		{
			Util.CheckArgumentNull<DataServiceQueryContinuation<T>>(continuation, "continuation");
			QueryComponents queryComponents = continuation.CreateQueryComponents();
			Uri uri = queryComponents.Uri;
			return new DataServiceRequest<T>(uri, queryComponents, continuation.Plan).BeginExecute(this, this, callback, state, "Execute");
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00029C96 File Offset: 0x00027E96
		public IEnumerable<TElement> EndExecute<TElement>(IAsyncResult asyncResult)
		{
			Util.CheckArgumentNull<IAsyncResult>(asyncResult, "asyncResult");
			return DataServiceRequest.EndExecute<TElement>(this, this, "Execute", asyncResult);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00029CB4 File Offset: 0x00027EB4
		public OperationResponse EndExecute(IAsyncResult asyncResult)
		{
			Util.CheckArgumentNull<IAsyncResult>(asyncResult, "asyncResult");
			QueryOperationResponse<object> queryOperationResponse = (QueryOperationResponse<object>)DataServiceRequest.EndExecute<object>(this, this, "ExecuteVoid", asyncResult);
			if (queryOperationResponse.Any<object>())
			{
				throw new DataServiceClientException(Strings.Context_EndExecuteExpectedVoidResponse);
			}
			return queryOperationResponse;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00029CF4 File Offset: 0x00027EF4
		public IEnumerable<TElement> Execute<TElement>(Uri requestUri)
		{
			return this.InnerSynchExecute<TElement>(requestUri, "GET", null, new OperationParameter[0]);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00029D1C File Offset: 0x00027F1C
		public QueryOperationResponse<T> Execute<T>(DataServiceQueryContinuation<T> continuation)
		{
			Util.CheckArgumentNull<DataServiceQueryContinuation<T>>(continuation, "continuation");
			QueryComponents queryComponents = continuation.CreateQueryComponents();
			Uri uri = queryComponents.Uri;
			DataServiceRequest dataServiceRequest = new DataServiceRequest<T>(uri, queryComponents, continuation.Plan);
			return dataServiceRequest.Execute<T>(this, queryComponents);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00029D5C File Offset: 0x00027F5C
		public OperationResponse Execute(Uri requestUri, string httpMethod, params OperationParameter[] operationParameters)
		{
			QueryOperationResponse<object> queryOperationResponse = (QueryOperationResponse<object>)this.Execute<object>(requestUri, httpMethod, false, operationParameters);
			if (queryOperationResponse.Any<object>())
			{
				throw new DataServiceClientException(Strings.Context_ExecuteExpectedVoidResponse);
			}
			return queryOperationResponse;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00029D8D File Offset: 0x00027F8D
		public IEnumerable<TElement> Execute<TElement>(Uri requestUri, string httpMethod, bool singleResult, params OperationParameter[] operationParameters)
		{
			return this.InnerSynchExecute<TElement>(requestUri, httpMethod, new bool?(singleResult), operationParameters);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00029D9F File Offset: 0x00027F9F
		public IAsyncResult BeginSaveChanges(AsyncCallback callback, object state)
		{
			return this.BeginSaveChanges(this.SaveChangesDefaultOptions, callback, state);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00029DB0 File Offset: 0x00027FB0
		public IAsyncResult BeginSaveChanges(SaveChangesOptions options, AsyncCallback callback, object state)
		{
			this.ValidateSaveChangesOptions(options);
			BaseSaveResult baseSaveResult = BaseSaveResult.CreateSaveResult(this, "SaveChanges", null, options, callback, state);
			if (baseSaveResult.IsBatchRequest)
			{
				((BatchSaveResult)baseSaveResult).BatchBeginRequest();
			}
			else
			{
				((SaveResult)baseSaveResult).BeginCreateNextChange();
			}
			return baseSaveResult;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00029DF8 File Offset: 0x00027FF8
		public DataServiceResponse EndSaveChanges(IAsyncResult asyncResult)
		{
			BaseSaveResult baseSaveResult = BaseAsyncResult.EndExecute<BaseSaveResult>(this, "SaveChanges", asyncResult);
			DataServiceResponse dataServiceResponse = baseSaveResult.EndRequest();
			if (this.ChangesSaved != null)
			{
				this.ChangesSaved(this, new SaveChangesEventArgs(dataServiceResponse));
			}
			return dataServiceResponse;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00029E34 File Offset: 0x00028034
		public DataServiceResponse SaveChanges()
		{
			return this.SaveChanges(this.SaveChangesDefaultOptions);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00029E44 File Offset: 0x00028044
		public DataServiceResponse SaveChanges(SaveChangesOptions options)
		{
			this.ValidateSaveChangesOptions(options);
			BaseSaveResult baseSaveResult = BaseSaveResult.CreateSaveResult(this, "SaveChanges", null, options, null, null);
			if (baseSaveResult.IsBatchRequest)
			{
				((BatchSaveResult)baseSaveResult).BatchRequest();
			}
			else
			{
				((SaveResult)baseSaveResult).CreateNextChange();
			}
			DataServiceResponse dataServiceResponse = baseSaveResult.EndRequest();
			if (this.ChangesSaved != null)
			{
				this.ChangesSaved(this, new SaveChangesEventArgs(dataServiceResponse));
			}
			return dataServiceResponse;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00029EAC File Offset: 0x000280AC
		public void AddLink(object source, string sourceProperty, object target)
		{
			this.EnsureRelatable(source, sourceProperty, target, EntityStates.Added);
			LinkDescriptor linkDescriptor = new LinkDescriptor(source, sourceProperty, target, this.model);
			this.entityTracker.AddLink(linkDescriptor);
			linkDescriptor.State = EntityStates.Added;
			this.entityTracker.IncrementChange(linkDescriptor);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00029EF2 File Offset: 0x000280F2
		public void AttachLink(object source, string sourceProperty, object target)
		{
			this.AttachLink(source, sourceProperty, target, MergeOption.NoTracking);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00029F00 File Offset: 0x00028100
		public bool DetachLink(object source, string sourceProperty, object target)
		{
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNullAndEmpty(sourceProperty, "sourceProperty");
			LinkDescriptor linkDescriptor = this.entityTracker.TryGetLinkDescriptor(source, sourceProperty, target);
			if (linkDescriptor == null)
			{
				return false;
			}
			this.entityTracker.DetachExistingLink(linkDescriptor, false);
			return true;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00029F48 File Offset: 0x00028148
		public void DeleteLink(object source, string sourceProperty, object target)
		{
			bool flag = this.EnsureRelatable(source, sourceProperty, target, EntityStates.Deleted);
			LinkDescriptor linkDescriptor = this.entityTracker.TryGetLinkDescriptor(source, sourceProperty, target);
			if (linkDescriptor != null && EntityStates.Added == linkDescriptor.State)
			{
				this.entityTracker.DetachExistingLink(linkDescriptor, false);
				return;
			}
			if (flag)
			{
				throw Error.InvalidOperation(Strings.Context_NoRelationWithInsertEnd);
			}
			if (linkDescriptor == null)
			{
				LinkDescriptor linkDescriptor2 = new LinkDescriptor(source, sourceProperty, target, this.model);
				this.entityTracker.AddLink(linkDescriptor2);
				linkDescriptor = linkDescriptor2;
			}
			if (EntityStates.Deleted != linkDescriptor.State)
			{
				linkDescriptor.State = EntityStates.Deleted;
				this.entityTracker.IncrementChange(linkDescriptor);
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00029FD4 File Offset: 0x000281D4
		public void SetLink(object source, string sourceProperty, object target)
		{
			this.EnsureRelatable(source, sourceProperty, target, EntityStates.Modified);
			LinkDescriptor linkDescriptor = this.entityTracker.DetachReferenceLink(source, sourceProperty, target, MergeOption.NoTracking);
			if (linkDescriptor == null)
			{
				linkDescriptor = new LinkDescriptor(source, sourceProperty, target, this.model);
				this.entityTracker.AddLink(linkDescriptor);
			}
			if (EntityStates.Modified != linkDescriptor.State)
			{
				linkDescriptor.State = EntityStates.Modified;
				this.entityTracker.IncrementChange(linkDescriptor);
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002A03C File Offset: 0x0002823C
		public void AddObject(string entitySetName, object entity)
		{
			DataServiceContext.ValidateEntitySetName(ref entitySetName);
			DataServiceContext.ValidateEntityType(entity, this.Model);
			EntityDescriptor entityDescriptor = new EntityDescriptor(this.model)
			{
				Entity = entity,
				State = EntityStates.Added,
				EntitySetName = entitySetName
			};
			entityDescriptor.SetEntitySetUriForInsert(this.BaseUriResolver.GetEntitySetUri(entitySetName));
			this.EntityTracker.AddEntityDescriptor(entityDescriptor);
			this.EntityTracker.IncrementChange(entityDescriptor);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002A0AC File Offset: 0x000282AC
		public void AddRelatedObject(object source, string sourceProperty, object target)
		{
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNullAndEmpty(sourceProperty, "sourceProperty");
			Util.CheckArgumentNull<object>(target, "target");
			DataServiceContext.ValidateEntityType(source, this.Model);
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(source);
			if (entityDescriptor.State == EntityStates.Deleted)
			{
				throw Error.InvalidOperation(Strings.Context_AddRelatedObjectSourceDeleted);
			}
			ClientTypeAnnotation clientTypeAnnotation = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(source.GetType()));
			ClientPropertyAnnotation property = clientTypeAnnotation.GetProperty(sourceProperty, false);
			if (property.IsKnownType || !property.IsEntityCollection)
			{
				throw Error.InvalidOperation(Strings.Context_AddRelatedObjectCollectionOnly);
			}
			ClientTypeAnnotation clientTypeAnnotation2 = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(target.GetType()));
			DataServiceContext.ValidateEntityType(target, this.Model);
			ClientTypeAnnotation clientTypeAnnotation3 = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(property.EntityCollectionItemType));
			if (!clientTypeAnnotation3.ElementType.IsAssignableFrom(clientTypeAnnotation2.ElementType))
			{
				throw Error.Argument(Strings.Context_RelationNotRefOrCollection, "target");
			}
			EntityDescriptor entityDescriptor2 = new EntityDescriptor(this.model)
			{
				Entity = target,
				State = EntityStates.Added
			};
			entityDescriptor2.SetParentForInsert(entityDescriptor, sourceProperty);
			this.EntityTracker.AddEntityDescriptor(entityDescriptor2);
			LinkDescriptor relatedEnd = entityDescriptor2.GetRelatedEnd();
			relatedEnd.State = EntityStates.Added;
			this.entityTracker.AddLink(relatedEnd);
			this.entityTracker.IncrementChange(entityDescriptor2);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002A217 File Offset: 0x00028417
		public void AttachTo(string entitySetName, object entity)
		{
			this.AttachTo(entitySetName, entity, null);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002A224 File Offset: 0x00028424
		public void AttachTo(string entitySetName, object entity, string etag)
		{
			DataServiceContext.ValidateEntitySetName(ref entitySetName);
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = new EntityDescriptor(this.model)
			{
				Entity = entity,
				ETag = etag,
				State = EntityStates.Unchanged,
				EntitySetName = entitySetName
			};
			ODataEntityMetadataBuilder entityMetadataBuilderInternal = this.GetEntityMetadataBuilderInternal(entityDescriptor);
			entityDescriptor.EditLink = entityMetadataBuilderInternal.GetEditLink();
			entityDescriptor.Identity = entityMetadataBuilderInternal.GetId();
			this.entityTracker.InternalAttachEntityDescriptor(entityDescriptor, true);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002A29C File Offset: 0x0002849C
		public void DeleteObject(object entity)
		{
			this.DeleteObjectInternal(entity, false);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002A2A8 File Offset: 0x000284A8
		public bool Detach(object entity)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.TryGetEntityDescriptor(entity);
			return entityDescriptor != null && this.entityTracker.DetachResource(entityDescriptor);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002A2DF File Offset: 0x000284DF
		public void UpdateObject(object entity)
		{
			this.UpdateObjectInternal(entity, false);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002A2EC File Offset: 0x000284EC
		public void ChangeState(object entity, EntityStates state)
		{
			switch (state)
			{
			case EntityStates.Detached:
				this.Detach(entity);
				return;
			case EntityStates.Unchanged:
				this.SetStateToUnchanged(entity);
				return;
			case EntityStates.Detached | EntityStates.Unchanged:
				break;
			case EntityStates.Added:
				throw Error.NotSupported(Strings.Context_CannotChangeStateToAdded);
			default:
				if (state == EntityStates.Deleted)
				{
					this.DeleteObjectInternal(entity, true);
					return;
				}
				if (state == EntityStates.Modified)
				{
					this.UpdateObjectInternal(entity, true);
					return;
				}
				break;
			}
			throw Error.InternalError(InternalError.UnvalidatedEntityState);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002A354 File Offset: 0x00028554
		public bool TryGetEntity<TEntity>(Uri identity, out TEntity entity) where TEntity : class
		{
			entity = default(TEntity);
			Util.CheckArgumentNull<Uri>(identity, "relativeUri");
			EntityStates entityStates;
			entity = (TEntity)((object)this.EntityTracker.TryGetEntity(UriUtil.UriToString(identity), out entityStates));
			return null != entity;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002A3A4 File Offset: 0x000285A4
		public bool TryGetUri(object entity, out Uri identity)
		{
			identity = null;
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.TryGetEntityDescriptor(entity);
			if (entityDescriptor != null && entityDescriptor.Identity != null && object.ReferenceEquals(entityDescriptor, this.entityTracker.TryGetEntityDescriptor(entityDescriptor.Identity)))
			{
				string identity2 = entityDescriptor.Identity;
				identity = UriUtil.CreateUri(identity2, UriKind.Absolute);
			}
			return null != identity;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002A40C File Offset: 0x0002860C
		internal QueryOperationResponse<TElement> InnerSynchExecute<TElement>(Uri requestUri, string httpMethod, bool? singleResult, params OperationParameter[] operationParameters)
		{
			List<UriOperationParameter> list = null;
			List<BodyOperationParameter> list2 = null;
			this.ValidateExecuteParameters<TElement>(ref requestUri, httpMethod, ref singleResult, out list2, out list, operationParameters);
			QueryComponents queryComponents = new QueryComponents(requestUri, Util.DataServiceVersionEmpty, typeof(TElement), null, null, httpMethod, singleResult, list2, list);
			requestUri = queryComponents.Uri;
			DataServiceRequest dataServiceRequest = new DataServiceRequest<TElement>(requestUri, queryComponents, null);
			return dataServiceRequest.Execute<TElement>(this, queryComponents);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002A464 File Offset: 0x00028664
		internal IAsyncResult InnerBeginExecute<TElement>(Uri requestUri, AsyncCallback callback, object state, string httpMethod, string method, bool? singleResult, params OperationParameter[] operationParameters)
		{
			List<UriOperationParameter> list = null;
			List<BodyOperationParameter> list2 = null;
			this.ValidateExecuteParameters<TElement>(ref requestUri, httpMethod, ref singleResult, out list2, out list, operationParameters);
			QueryComponents queryComponents = new QueryComponents(requestUri, Util.DataServiceVersionEmpty, typeof(TElement), null, null, httpMethod, singleResult, list2, list);
			requestUri = queryComponents.Uri;
			return new DataServiceRequest<TElement>(requestUri, queryComponents, null).BeginExecute(this, this, callback, state, method);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002A4C0 File Offset: 0x000286C0
		internal void AttachLink(object source, string sourceProperty, object target, MergeOption linkMerge)
		{
			this.EnsureRelatable(source, sourceProperty, target, EntityStates.Unchanged);
			this.entityTracker.AttachLink(source, sourceProperty, target, linkMerge);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002A4E0 File Offset: 0x000286E0
		internal ODataRequestMessageWrapper CreateODataRequestMessage(BuildingRequestEventArgs requestMessageArgs, IEnumerable<string> headersToReset, Descriptor descriptor)
		{
			ODataRequestMessageWrapper odataRequestMessageWrapper = new RequestInfo(this).WriteHelper.CreateRequestMessage(requestMessageArgs);
			if (headersToReset != null)
			{
				odataRequestMessageWrapper.AddHeadersToReset(headersToReset);
			}
			odataRequestMessageWrapper.FireSendingRequest2(descriptor);
			return odataRequestMessageWrapper;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002A514 File Offset: 0x00028714
		internal Type ResolveTypeFromName(string wireName)
		{
			Func<string, Type> func = this.ResolveType;
			if (func != null)
			{
				return func(wireName);
			}
			return null;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002A534 File Offset: 0x00028734
		internal string ResolveNameFromType(Type type)
		{
			Func<Type, string> func = this.ResolveName;
			if (func == null)
			{
				return null;
			}
			return func(type);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002A554 File Offset: 0x00028754
		internal void FireWritingAtomEntityEvent(object entity, XElement data, Uri baseUri)
		{
			ReadingWritingEntityEventArgs readingWritingEntityEventArgs = new ReadingWritingEntityEventArgs(entity, data, baseUri);
			this.WritingAtomEntity(this, readingWritingEntityEventArgs);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002A577 File Offset: 0x00028777
		internal void FireSendingRequest(SendingRequestEventArgs eventArgs)
		{
			this.InnerSendingRequest(this, eventArgs);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002A586 File Offset: 0x00028786
		internal void FireSendingRequest2(SendingRequest2EventArgs eventArgs)
		{
			this.SendingRequest2(this, eventArgs);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002A595 File Offset: 0x00028795
		internal void FireReceivingResponseEvent(ReceivingResponseEventArgs receivingResponseEventArgs)
		{
			if (this.ReceivingResponse != null)
			{
				this.ReceivingResponse(this, receivingResponseEventArgs);
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002A5AC File Offset: 0x000287AC
		internal IODataResponseMessage GetSyncronousResponse(ODataRequestMessageWrapper request, bool handleWebException)
		{
			return this.GetResponseHelper(request, null, handleWebException);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002A5B7 File Offset: 0x000287B7
		internal IODataResponseMessage EndGetResponse(ODataRequestMessageWrapper request, IAsyncResult asyncResult)
		{
			return this.GetResponseHelper(request, asyncResult, true);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002A5C2 File Offset: 0x000287C2
		internal void InternalSendRequest(HttpWebRequest request)
		{
			if (this.sendRequest != null)
			{
				this.sendRequest(request);
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002A5D8 File Offset: 0x000287D8
		internal Stream InternalGetRequestWrappingStream(Stream requestStream)
		{
			if (this.getRequestWrappingStream == null)
			{
				return requestStream;
			}
			return this.getRequestWrappingStream(requestStream);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002A5F0 File Offset: 0x000287F0
		internal void InternalSendResponse(HttpWebResponse response)
		{
			if (this.sendResponse != null)
			{
				this.sendResponse(response);
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002A606 File Offset: 0x00028806
		internal Stream InternalGetResponseWrappingStream(Stream responseStream)
		{
			if (this.getResponseWrappingStream == null)
			{
				return responseStream;
			}
			return this.getResponseWrappingStream(responseStream);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002A61E File Offset: 0x0002881E
		internal virtual ODataEntityMetadataBuilder GetEntityMetadataBuilder(string entitySetName, IEdmStructuredValue entityInstance)
		{
			return new ConventionalODataEntityMetadataBuilder(this.baseUriResolver, entitySetName, entityInstance, this.UrlConventions);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002A634 File Offset: 0x00028834
		internal BuildingRequestEventArgs CreateRequestArgsAndFireBuildingRequest(string method, Uri requestUri, HeaderCollection headers, HttpStack stack, Descriptor descriptor)
		{
			BuildingRequestEventArgs buildingRequestEventArgs = new BuildingRequestEventArgs(method, requestUri, headers, descriptor, stack);
			this.UrlConventions.AddRequiredHeaders(buildingRequestEventArgs.HeaderCollection);
			buildingRequestEventArgs.HeaderCollection.SetDefaultHeaders();
			return this.FireBuildingRequest(buildingRequestEventArgs);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002A674 File Offset: 0x00028874
		protected Type DefaultResolveType(string typeName, string fullNamespace, string languageDependentNamespace)
		{
			if (typeName != null && typeName.StartsWith(fullNamespace, StringComparison.Ordinal))
			{
				int num = ((fullNamespace != null) ? fullNamespace.Length : 0);
				return base.GetType().GetAssembly().GetType(languageDependentNamespace + typeName.Substring(num), false);
			}
			return null;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0002A6BC File Offset: 0x000288BC
		private static void ValidateEntitySetName(ref string entitySetName)
		{
			Util.CheckArgumentNullAndEmpty(entitySetName, "entitySetName");
			entitySetName = entitySetName.Trim(UriUtil.ForwardSlash);
			Util.CheckArgumentNullAndEmpty(entitySetName, "entitySetName");
			Uri uri = UriUtil.CreateUri(entitySetName, UriKind.RelativeOrAbsolute);
			if (uri.IsAbsoluteUri || !string.IsNullOrEmpty(UriUtil.CreateUri(new Uri("http://ConstBaseUri/ConstService.svc/"), uri).GetComponents(UriComponents.Query | UriComponents.Fragment, UriFormat.SafeUnescaped)))
			{
				throw Error.Argument(Strings.Context_EntitySetName, "entitySetName");
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002A72F File Offset: 0x0002892F
		private static void ValidateEntityType(object entity, ClientEdmModel model)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			if (!ClientTypeUtil.TypeIsEntity(entity.GetType(), model))
			{
				throw Error.Argument(Strings.Content_EntityIsNotEntityType, "entity");
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0002A75C File Offset: 0x0002895C
		private static void ValidateOperationParameters(string httpMethod, OperationParameter[] parameters, out List<BodyOperationParameter> bodyOperationParameters, out List<UriOperationParameter> uriOperationParameters)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			HashSet<string> hashSet2 = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			List<UriOperationParameter> list = new List<UriOperationParameter>();
			List<BodyOperationParameter> list2 = new List<BodyOperationParameter>();
			foreach (OperationParameter operationParameter in parameters)
			{
				if (operationParameter == null)
				{
					throw new ArgumentException(Strings.Context_NullElementInOperationParameterArray);
				}
				if (string.IsNullOrEmpty(operationParameter.Name))
				{
					throw new ArgumentException(Strings.Context_MissingOperationParameterName);
				}
				string text = operationParameter.Name.Trim();
				BodyOperationParameter bodyOperationParameter = operationParameter as BodyOperationParameter;
				if (bodyOperationParameter != null)
				{
					if (string.CompareOrdinal("GET", httpMethod) == 0)
					{
						throw new ArgumentException(Strings.Context_BodyOperationParametersNotAllowedWithGet);
					}
					if (!hashSet2.Add(text))
					{
						throw new ArgumentException(Strings.Context_DuplicateBodyOperationParameterName);
					}
					list2.Add(bodyOperationParameter);
				}
				else
				{
					UriOperationParameter uriOperationParameter = operationParameter as UriOperationParameter;
					if (uriOperationParameter != null)
					{
						if (!hashSet.Add(text))
						{
							throw new ArgumentException(Strings.Context_DuplicateUriOperationParameterName);
						}
						list.Add(uriOperationParameter);
					}
				}
			}
			uriOperationParameters = (list.Any<UriOperationParameter>() ? list : null);
			bodyOperationParameters = (list2.Any<BodyOperationParameter>() ? list2 : null);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002A86E File Offset: 0x00028A6E
		private void CheckUsingAtom()
		{
			if (!this.Format.UsingAtom)
			{
				throw new InvalidOperationException(Strings.DataServiceClientFormat_AtomEventsOnlySupportedWithAtomFormat);
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002A888 File Offset: 0x00028A88
		private BuildingRequestEventArgs FireBuildingRequest(BuildingRequestEventArgs buildingRequestEventArgs)
		{
			if (this.HasBuildingRequestEventHandlers)
			{
				this.InnerBuildingRequest(this, buildingRequestEventArgs);
				return buildingRequestEventArgs.Clone();
			}
			return buildingRequestEventArgs;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002A8A8 File Offset: 0x00028AA8
		private void ValidateSaveChangesOptions(SaveChangesOptions options)
		{
			if ((options | (SaveChangesOptions.Batch | SaveChangesOptions.ContinueOnError | SaveChangesOptions.ReplaceOnUpdate | SaveChangesOptions.PatchOnUpdate | SaveChangesOptions.BatchWithIndependentOperations)) != (SaveChangesOptions.Batch | SaveChangesOptions.ContinueOnError | SaveChangesOptions.ReplaceOnUpdate | SaveChangesOptions.PatchOnUpdate | SaveChangesOptions.BatchWithIndependentOperations))
			{
				throw Error.ArgumentOutOfRange("options");
			}
			if (Util.IsFlagSet(options, SaveChangesOptions.Batch | SaveChangesOptions.BatchWithIndependentOperations))
			{
				throw Error.ArgumentOutOfRange("options");
			}
			if (Util.IsFlagSet(options, SaveChangesOptions.Batch | SaveChangesOptions.ContinueOnError))
			{
				throw Error.ArgumentOutOfRange("options");
			}
			if (Util.IsFlagSet(options, SaveChangesOptions.ContinueOnError | SaveChangesOptions.BatchWithIndependentOperations))
			{
				throw Error.ArgumentOutOfRange("options");
			}
			if (Util.IsFlagSet(options, SaveChangesOptions.ReplaceOnUpdate | SaveChangesOptions.PatchOnUpdate))
			{
				throw Error.ArgumentOutOfRange("options");
			}
			if (Util.IsFlagSet(options, SaveChangesOptions.PatchOnUpdate))
			{
				this.EnsureMinimumProtocolVersionV3();
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002A92C File Offset: 0x00028B2C
		private void ValidateExecuteParameters<TElement>(ref Uri requestUri, string httpMethod, ref bool? singleResult, out List<BodyOperationParameter> bodyOperationParameters, out List<UriOperationParameter> uriOperationParameters, params OperationParameter[] operationParameters)
		{
			if (string.CompareOrdinal("GET", httpMethod) != 0 && string.CompareOrdinal("POST", httpMethod) != 0)
			{
				throw new ArgumentException(Strings.Context_ExecuteExpectsGetOrPost, "httpMethod");
			}
			if (ClientTypeUtil.TypeOrElementTypeIsEntity(typeof(TElement)))
			{
				singleResult = null;
			}
			if (operationParameters != null)
			{
				DataServiceContext.ValidateOperationParameters(httpMethod, operationParameters, out bodyOperationParameters, out uriOperationParameters);
			}
			else
			{
				uriOperationParameters = null;
				bodyOperationParameters = null;
			}
			requestUri = this.BaseUriResolver.GetOrCreateAbsoluteUri(requestUri);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002A9A4 File Offset: 0x00028BA4
		private LoadPropertyResult CreateLoadPropertyRequest(object entity, string propertyName, AsyncCallback callback, object state, Uri requestUri, DataServiceQueryContinuation continuation)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			Util.CheckArgumentNullAndEmpty(propertyName, "propertyName");
			ClientTypeAnnotation clientTypeAnnotation = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(entity.GetType()));
			if (EntityStates.Added == entityDescriptor.State)
			{
				throw Error.InvalidOperation(Strings.Context_NoLoadWithInsertEnd);
			}
			ClientPropertyAnnotation property = clientTypeAnnotation.GetProperty(propertyName, false);
			bool flag = requestUri != null || continuation != null;
			ProjectionPlan projectionPlan;
			if (continuation == null)
			{
				projectionPlan = null;
			}
			else
			{
				projectionPlan = continuation.Plan;
				requestUri = continuation.NextLinkUri;
			}
			bool flag2 = clientTypeAnnotation.MediaDataMember != null && propertyName == clientTypeAnnotation.MediaDataMember.PropertyName;
			Version version;
			if (requestUri == null)
			{
				if (flag2)
				{
					Uri uri = UriUtil.CreateUri("$value", UriKind.Relative);
					requestUri = UriUtil.CreateUri(entityDescriptor.GetResourceUri(this.BaseUriResolver, true), uri);
				}
				else
				{
					requestUri = entityDescriptor.GetNavigationLink(this.baseUriResolver, property);
				}
				version = Util.DataServiceVersion1;
			}
			else
			{
				version = Util.DataServiceVersion2;
			}
			HeaderCollection headerCollection = new HeaderCollection();
			headerCollection.SetRequestVersion(version, this.MaxProtocolVersionAsVersion);
			if (flag2)
			{
				this.Format.SetRequestAcceptHeaderForStream(headerCollection);
			}
			else
			{
				this.formatTracker.SetRequestAcceptHeader(headerCollection);
			}
			ODataRequestMessageWrapper odataRequestMessageWrapper = this.CreateODataRequestMessage(this.CreateRequestArgsAndFireBuildingRequest("GET", requestUri, headerCollection, this.HttpStack, null), null, null);
			DataServiceRequest instance = DataServiceRequest.GetInstance(property.PropertyType, requestUri);
			instance.PayloadKind = ODataPayloadKind.Property;
			return new LoadPropertyResult(entity, propertyName, this, odataRequestMessageWrapper, callback, state, instance, projectionPlan, flag);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002AB30 File Offset: 0x00028D30
		private bool EnsureRelatable(object source, string sourceProperty, object target, EntityStates state)
		{
			Util.CheckArgumentNull<object>(source, "source");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(source);
			EntityDescriptor entityDescriptor2 = null;
			if (target != null || (EntityStates.Modified != state && EntityStates.Unchanged != state))
			{
				Util.CheckArgumentNull<object>(target, "target");
				entityDescriptor2 = this.entityTracker.GetEntityDescriptor(target);
			}
			Util.CheckArgumentNullAndEmpty(sourceProperty, "sourceProperty");
			ClientTypeAnnotation clientTypeAnnotation = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(source.GetType()));
			ClientPropertyAnnotation property = clientTypeAnnotation.GetProperty(sourceProperty, false);
			if (property.IsKnownType)
			{
				throw Error.InvalidOperation(Strings.Context_RelationNotRefOrCollection);
			}
			if (EntityStates.Unchanged == state && target == null && property.IsEntityCollection)
			{
				Util.CheckArgumentNull<object>(target, "target");
				entityDescriptor2 = this.entityTracker.GetEntityDescriptor(target);
			}
			if ((EntityStates.Added == state || EntityStates.Deleted == state) && !property.IsEntityCollection)
			{
				throw Error.InvalidOperation(Strings.Context_AddLinkCollectionOnly);
			}
			if (EntityStates.Modified == state && property.IsEntityCollection)
			{
				throw Error.InvalidOperation(Strings.Context_SetLinkReferenceOnly);
			}
			clientTypeAnnotation = this.model.GetClientTypeAnnotation(this.model.GetOrCreateEdmType(property.EntityCollectionItemType ?? property.PropertyType));
			if (target != null && !clientTypeAnnotation.ElementType.IsInstanceOfType(target))
			{
				throw Error.Argument(Strings.Context_RelationNotRefOrCollection, "target");
			}
			if ((EntityStates.Added == state || EntityStates.Unchanged == state) && (entityDescriptor.State == EntityStates.Deleted || (entityDescriptor2 != null && entityDescriptor2.State == EntityStates.Deleted)))
			{
				throw Error.InvalidOperation(Strings.Context_NoRelationWithDeleteEnd);
			}
			if ((EntityStates.Deleted != state && EntityStates.Unchanged != state) || (entityDescriptor.State != EntityStates.Added && (entityDescriptor2 == null || entityDescriptor2.State != EntityStates.Added)))
			{
				return false;
			}
			if (EntityStates.Deleted == state)
			{
				return true;
			}
			throw Error.InvalidOperation(Strings.Context_NoRelationWithInsertEnd);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002ACC8 File Offset: 0x00028EC8
		private GetReadStreamResult CreateGetReadStreamResult(object entity, DataServiceRequestArgs args, AsyncCallback callback, object state, string name)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			Util.CheckArgumentNull<DataServiceRequestArgs>(args, "args");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			Version version;
			Uri uri;
			StreamDescriptor defaultStreamDescriptor;
			if (name == null)
			{
				version = null;
				uri = entityDescriptor.ReadStreamUri;
				if (uri == null)
				{
					throw new ArgumentException(Strings.Context_EntityNotMediaLinkEntry, "entity");
				}
				defaultStreamDescriptor = entityDescriptor.DefaultStreamDescriptor;
			}
			else
			{
				version = Util.DataServiceVersion3;
				if (!entityDescriptor.TryGetNamedStreamInfo(name, out defaultStreamDescriptor))
				{
					throw new ArgumentException(Strings.Context_EntityDoesNotContainNamedStream(name), "name");
				}
				uri = defaultStreamDescriptor.SelfLink ?? defaultStreamDescriptor.EditLink;
				if (uri == null)
				{
					throw new ArgumentException(Strings.Context_MissingSelfAndEditLinkForNamedStream(name), "name");
				}
			}
			HeaderCollection headerCollection = args.HeaderCollection.Copy();
			headerCollection.SetRequestVersion(version, this.MaxProtocolVersionAsVersion);
			IEnumerable<string> enumerable = headerCollection.HeaderNames.ToList<string>();
			this.Format.SetRequestAcceptHeaderForStream(headerCollection);
			BuildingRequestEventArgs buildingRequestEventArgs = this.CreateRequestArgsAndFireBuildingRequest("GET", uri, headerCollection, HttpStack.Auto, defaultStreamDescriptor);
			ODataRequestMessageWrapper odataRequestMessageWrapper = this.CreateODataRequestMessage(buildingRequestEventArgs, enumerable, defaultStreamDescriptor);
			return new GetReadStreamResult(this, "GetReadStream", odataRequestMessageWrapper, callback, state, defaultStreamDescriptor);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002ADDF File Offset: 0x00028FDF
		private void EnsureMinimumProtocolVersionV3()
		{
			if (this.MaxProtocolVersionAsVersion < Util.DataServiceVersion3)
			{
				throw Error.InvalidOperation(Strings.Context_RequestVersionIsBiggerThanProtocolVersion(Util.DataServiceVersion3, this.MaxProtocolVersionAsVersion));
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002AE09 File Offset: 0x00029009
		private void EnsureMaximumProtocolVersionForProperty(string propertyName, Version maxAllowedVersion)
		{
			if (this.MaxProtocolVersionAsVersion > maxAllowedVersion)
			{
				throw Error.NotSupported(Strings.Context_PropertyNotSupportedForMaxDataServiceVersionGreaterThanX(propertyName, maxAllowedVersion));
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002AE28 File Offset: 0x00029028
		private ODataEntityMetadataBuilder GetEntityMetadataBuilderInternal(EntityDescriptor descriptor)
		{
			ODataEntityMetadataBuilder entityMetadataBuilder = this.GetEntityMetadataBuilder(descriptor.EntitySetName, descriptor.EdmValue);
			if (entityMetadataBuilder == null)
			{
				throw new InvalidOperationException(Strings.Context_EntityMetadataBuilderIsRequired);
			}
			return entityMetadataBuilder;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002AE58 File Offset: 0x00029058
		private IODataResponseMessage GetResponseHelper(ODataRequestMessageWrapper request, IAsyncResult asyncResult, bool handleWebException)
		{
			IODataResponseMessage iodataResponseMessage = null;
			try
			{
				if (asyncResult == null)
				{
					iodataResponseMessage = request.GetResponse();
				}
				else
				{
					iodataResponseMessage = request.EndGetResponse(asyncResult);
				}
				this.FireReceivingResponseEvent(new ReceivingResponseEventArgs(iodataResponseMessage, request.Descriptor));
			}
			catch (DataServiceTransportException ex)
			{
				iodataResponseMessage = ex.Response;
				this.FireReceivingResponseEvent(new ReceivingResponseEventArgs(iodataResponseMessage, request.Descriptor));
				if (!handleWebException || iodataResponseMessage == null)
				{
					throw;
				}
			}
			return iodataResponseMessage;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002AEC4 File Offset: 0x000290C4
		private void UpdateObjectInternal(object entity, bool failIfNotUnchanged)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.TryGetEntityDescriptor(entity);
			if (entityDescriptor == null)
			{
				throw Error.Argument(Strings.Context_EntityNotContained, "entity");
			}
			if (entityDescriptor.State == EntityStates.Modified)
			{
				return;
			}
			if (entityDescriptor.State == EntityStates.Unchanged)
			{
				entityDescriptor.State = EntityStates.Modified;
				this.entityTracker.IncrementChange(entityDescriptor);
				return;
			}
			if (failIfNotUnchanged)
			{
				throw Error.InvalidOperation(Strings.Context_CannotChangeStateToModifiedIfNotUnchanged);
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002AF34 File Offset: 0x00029134
		private void DeleteObjectInternal(object entity, bool failIfInAddedState)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			EntityStates state = entityDescriptor.State;
			if (EntityStates.Added != state)
			{
				if (EntityStates.Deleted != state)
				{
					entityDescriptor.State = EntityStates.Deleted;
					this.entityTracker.IncrementChange(entityDescriptor);
				}
				return;
			}
			if (failIfInAddedState)
			{
				throw Error.InvalidOperation(Strings.Context_CannotChangeStateIfAdded(EntityStates.Deleted));
			}
			this.entityTracker.DetachResource(entityDescriptor);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002AFA0 File Offset: 0x000291A0
		private void SetStateToUnchanged(object entity)
		{
			Util.CheckArgumentNull<object>(entity, "entity");
			EntityDescriptor entityDescriptor = this.entityTracker.GetEntityDescriptor(entity);
			if (entityDescriptor.State == EntityStates.Added)
			{
				throw Error.InvalidOperation(Strings.Context_CannotChangeStateIfAdded(EntityStates.Unchanged));
			}
			entityDescriptor.State = EntityStates.Unchanged;
		}

		// Token: 0x040005C1 RID: 1473
		private const string ServiceRootParameterName = "serviceRoot";

		// Token: 0x040005C2 RID: 1474
		internal Version MaxProtocolVersionAsVersion;

		// Token: 0x040005C3 RID: 1475
		private readonly ClientEdmModel model;

		// Token: 0x040005C4 RID: 1476
		private DataServiceClientFormat formatTracker;

		// Token: 0x040005C5 RID: 1477
		private DataServiceProtocolVersion maxProtocolVersion;

		// Token: 0x040005C6 RID: 1478
		private EntityTracker entityTracker;

		// Token: 0x040005C7 RID: 1479
		private DataServiceResponsePreference addAndUpdateResponsePreference;

		// Token: 0x040005C8 RID: 1480
		private UriResolver baseUriResolver;

		// Token: 0x040005C9 RID: 1481
		private ICredentials credentials;

		// Token: 0x040005CA RID: 1482
		private string dataNamespace;

		// Token: 0x040005CB RID: 1483
		private Func<Type, string> resolveName;

		// Token: 0x040005CC RID: 1484
		private Func<string, Type> resolveType;

		// Token: 0x040005CD RID: 1485
		private int timeout;

		// Token: 0x040005CE RID: 1486
		private bool postTunneling;

		// Token: 0x040005CF RID: 1487
		private bool ignoreMissingProperties;

		// Token: 0x040005D0 RID: 1488
		private UndeclaredPropertyBehavior undeclaredPropertyBehavior;

		// Token: 0x040005D1 RID: 1489
		private MergeOption mergeOption;

		// Token: 0x040005D2 RID: 1490
		private SaveChangesOptions saveChangesDefaultOptions;

		// Token: 0x040005D3 RID: 1491
		private Uri typeScheme;

		// Token: 0x040005D4 RID: 1492
		private bool ignoreResourceNotFoundException;

		// Token: 0x040005D5 RID: 1493
		private DataServiceUrlConventions urlConventions;

		// Token: 0x040005D6 RID: 1494
		private HttpStack httpStack;

		// Token: 0x040005D7 RID: 1495
		private Action<object> sendRequest;

		// Token: 0x040005D8 RID: 1496
		private Func<Stream, Stream> getRequestWrappingStream;

		// Token: 0x040005D9 RID: 1497
		private Action<object> sendResponse;

		// Token: 0x040005DA RID: 1498
		private Func<Stream, Stream> getResponseWrappingStream;

		// Token: 0x040005DB RID: 1499
		private bool applyingChanges;

		// Token: 0x0200012E RID: 302
		private static class ClientEdmModelCache
		{
			// Token: 0x06000AD0 RID: 2768 RVA: 0x0002AFE8 File Offset: 0x000291E8
			static ClientEdmModelCache()
			{
				IEnumerable<DataServiceProtocolVersion> enumerable = Enum.GetValues(typeof(DataServiceProtocolVersion)).Cast<DataServiceProtocolVersion>();
				foreach (DataServiceProtocolVersion dataServiceProtocolVersion in enumerable)
				{
					ClientEdmModel clientEdmModel = new ClientEdmModel(dataServiceProtocolVersion);
					clientEdmModel.SetEdmVersion(dataServiceProtocolVersion.ToVersion());
					DataServiceContext.ClientEdmModelCache.modelCache.Add(dataServiceProtocolVersion, clientEdmModel);
				}
			}

			// Token: 0x06000AD1 RID: 2769 RVA: 0x0002B06C File Offset: 0x0002926C
			internal static ClientEdmModel GetModel(DataServiceProtocolVersion maxProtocolVersion)
			{
				Util.CheckEnumerationValue(maxProtocolVersion, "maxProtocolVersion");
				return DataServiceContext.ClientEdmModelCache.modelCache[maxProtocolVersion];
			}

			// Token: 0x040005E8 RID: 1512
			private static readonly Dictionary<DataServiceProtocolVersion, ClientEdmModel> modelCache = new Dictionary<DataServiceProtocolVersion, ClientEdmModel>(EqualityComparer<DataServiceProtocolVersion>.Default);
		}
	}
}
