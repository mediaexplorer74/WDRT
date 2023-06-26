using System;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000115 RID: 277
	internal class MaterializerEntry
	{
		// Token: 0x06000914 RID: 2324 RVA: 0x0002524A File Offset: 0x0002344A
		private MaterializerEntry()
		{
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00025260 File Offset: 0x00023460
		private MaterializerEntry(ODataEntry entry, ODataFormat format, bool isTracking, ClientEdmModel model)
		{
			this.entry = entry;
			this.Format = format;
			this.entityDescriptor = new EntityDescriptor(model);
			this.isAtomOrTracking = isTracking || this.Format == ODataFormat.Atom;
			string text = this.Entry.TypeName;
			SerializationTypeNameAnnotation annotation = entry.GetAnnotation<SerializationTypeNameAnnotation>();
			if (annotation != null && (annotation.TypeName != null || this.Format != ODataFormat.Json))
			{
				text = annotation.TypeName;
			}
			this.entityDescriptor.ServerTypeName = text;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000252F0 File Offset: 0x000234F0
		private MaterializerEntry(EntityDescriptor entityDescriptor, ODataFormat format, bool isTracking)
		{
			this.entityDescriptor = entityDescriptor;
			this.Format = format;
			this.isAtomOrTracking = isTracking || this.Format == ODataFormat.Atom;
			this.SetFlagValue(MaterializerEntry.EntryFlags.ShouldUpdateFromPayload | MaterializerEntry.EntryFlags.EntityHasBeenResolved | MaterializerEntry.EntryFlags.ForLoadProperty, true);
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0002533E File Offset: 0x0002353E
		public ODataEntry Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00025346 File Offset: 0x00023546
		public bool IsAtomOrTracking
		{
			get
			{
				return this.isAtomOrTracking;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0002534E File Offset: 0x0002354E
		public string Id
		{
			get
			{
				return this.entry.Id;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0002535B File Offset: 0x0002355B
		public IEnumerable<ODataProperty> Properties
		{
			get
			{
				if (this.entry == null)
				{
					return null;
				}
				return this.entry.Properties;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00025372 File Offset: 0x00023572
		public EntityDescriptor EntityDescriptor
		{
			get
			{
				return this.entityDescriptor;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0002537A File Offset: 0x0002357A
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x00025391 File Offset: 0x00023591
		public object ResolvedObject
		{
			get
			{
				if (this.entityDescriptor == null)
				{
					return null;
				}
				return this.entityDescriptor.Entity;
			}
			set
			{
				this.entityDescriptor.Entity = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0002539F File Offset: 0x0002359F
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x000253A7 File Offset: 0x000235A7
		public ClientTypeAnnotation ActualType { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x000253B0 File Offset: 0x000235B0
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x000253B9 File Offset: 0x000235B9
		public bool ShouldUpdateFromPayload
		{
			get
			{
				return this.GetFlagValue(MaterializerEntry.EntryFlags.ShouldUpdateFromPayload);
			}
			set
			{
				this.SetFlagValue(MaterializerEntry.EntryFlags.ShouldUpdateFromPayload, value);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x000253C3 File Offset: 0x000235C3
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x000253CC File Offset: 0x000235CC
		public bool EntityHasBeenResolved
		{
			get
			{
				return this.GetFlagValue(MaterializerEntry.EntryFlags.EntityHasBeenResolved);
			}
			set
			{
				this.SetFlagValue(MaterializerEntry.EntryFlags.EntityHasBeenResolved, value);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x000253D6 File Offset: 0x000235D6
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x000253DF File Offset: 0x000235DF
		public bool CreatedByMaterializer
		{
			get
			{
				return this.GetFlagValue(MaterializerEntry.EntryFlags.CreatedByMaterializer);
			}
			set
			{
				this.SetFlagValue(MaterializerEntry.EntryFlags.CreatedByMaterializer, value);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x000253E9 File Offset: 0x000235E9
		public bool ForLoadProperty
		{
			get
			{
				return this.GetFlagValue(MaterializerEntry.EntryFlags.ForLoadProperty);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x000253F3 File Offset: 0x000235F3
		public ICollection<ODataNavigationLink> NavigationLinks
		{
			get
			{
				return this.navigationLinks;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x000253FB File Offset: 0x000235FB
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x00025403 File Offset: 0x00023603
		internal ODataFormat Format { get; private set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0002540C File Offset: 0x0002360C
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x00025415 File Offset: 0x00023615
		private bool EntityDescriptorUpdated
		{
			get
			{
				return this.GetFlagValue(MaterializerEntry.EntryFlags.EntityDescriptorUpdated);
			}
			set
			{
				this.SetFlagValue(MaterializerEntry.EntryFlags.EntityDescriptorUpdated, value);
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0002541F File Offset: 0x0002361F
		public static MaterializerEntry CreateEmpty()
		{
			return new MaterializerEntry();
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00025428 File Offset: 0x00023628
		public static MaterializerEntry CreateEntry(ODataEntry entry, ODataFormat format, bool isTracking, ClientEdmModel model)
		{
			MaterializerEntry materializerEntry = new MaterializerEntry(entry, format, isTracking, model);
			entry.SetAnnotation<MaterializerEntry>(materializerEntry);
			return materializerEntry;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00025447 File Offset: 0x00023647
		public static MaterializerEntry CreateEntryForLoadProperty(EntityDescriptor descriptor, ODataFormat format, bool isTracking)
		{
			return new MaterializerEntry(descriptor, format, isTracking);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00025451 File Offset: 0x00023651
		public static MaterializerEntry GetEntry(ODataEntry entry)
		{
			if (entry != null)
			{
				return entry.GetAnnotation<MaterializerEntry>();
			}
			return null;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00025460 File Offset: 0x00023660
		public void AddNavigationLink(ODataNavigationLink link)
		{
			if (this.IsAtomOrTracking)
			{
				this.EntityDescriptor.AddNavigationLink(link.Name, link.Url);
				Uri associationLinkUrl = link.AssociationLinkUrl;
				if (associationLinkUrl != null)
				{
					this.EntityDescriptor.AddAssociationLink(link.Name, associationLinkUrl);
				}
			}
			if (this.navigationLinks == ODataMaterializer.EmptyLinks)
			{
				this.navigationLinks = new List<ODataNavigationLink>();
			}
			this.navigationLinks.Add(link);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000254D4 File Offset: 0x000236D4
		public void UpdateEntityDescriptor()
		{
			if (!this.EntityDescriptorUpdated)
			{
				foreach (ODataProperty odataProperty in this.Properties)
				{
					ODataStreamReferenceValue odataStreamReferenceValue = odataProperty.Value as ODataStreamReferenceValue;
					if (odataStreamReferenceValue != null)
					{
						StreamDescriptor streamDescriptor = this.EntityDescriptor.AddStreamInfoIfNotPresent(odataProperty.Name);
						if (odataStreamReferenceValue.ReadLink != null)
						{
							streamDescriptor.SelfLink = odataStreamReferenceValue.ReadLink;
						}
						if (odataStreamReferenceValue.EditLink != null)
						{
							streamDescriptor.EditLink = odataStreamReferenceValue.EditLink;
						}
						streamDescriptor.ETag = odataStreamReferenceValue.ETag;
						streamDescriptor.ContentType = odataStreamReferenceValue.ContentType;
					}
				}
				if (this.IsAtomOrTracking)
				{
					if (this.Id == null)
					{
						throw Error.InvalidOperation(Strings.Deserialize_MissingIdElement);
					}
					this.EntityDescriptor.Identity = this.entry.Id;
					this.EntityDescriptor.EditLink = this.entry.EditLink;
					this.EntityDescriptor.SelfLink = this.entry.ReadLink;
					this.EntityDescriptor.ETag = this.entry.ETag;
					if (this.entry.MediaResource != null)
					{
						if (this.entry.MediaResource.ReadLink != null)
						{
							this.EntityDescriptor.ReadStreamUri = this.entry.MediaResource.ReadLink;
						}
						if (this.entry.MediaResource.EditLink != null)
						{
							this.EntityDescriptor.EditStreamUri = this.entry.MediaResource.EditLink;
						}
						if (this.entry.MediaResource.ETag != null)
						{
							this.EntityDescriptor.StreamETag = this.entry.MediaResource.ETag;
						}
					}
					if (this.entry.AssociationLinks != null)
					{
						foreach (ODataAssociationLink odataAssociationLink in this.entry.AssociationLinks)
						{
							this.EntityDescriptor.AddAssociationLink(odataAssociationLink.Name, odataAssociationLink.Url);
						}
					}
					if (this.entry.Functions != null)
					{
						foreach (ODataFunction odataFunction in this.entry.Functions)
						{
							this.EntityDescriptor.AddOperationDescriptor(new FunctionDescriptor
							{
								Title = odataFunction.Title,
								Metadata = odataFunction.Metadata,
								Target = odataFunction.Target
							});
						}
					}
					if (this.entry.Actions != null)
					{
						foreach (ODataAction odataAction in this.entry.Actions)
						{
							this.EntityDescriptor.AddOperationDescriptor(new ActionDescriptor
							{
								Title = odataAction.Title,
								Metadata = odataAction.Metadata,
								Target = odataAction.Target
							});
						}
					}
				}
				this.EntityDescriptorUpdated = true;
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00025830 File Offset: 0x00023A30
		private bool GetFlagValue(MaterializerEntry.EntryFlags mask)
		{
			return (this.flags & mask) != (MaterializerEntry.EntryFlags)0;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00025840 File Offset: 0x00023A40
		private void SetFlagValue(MaterializerEntry.EntryFlags mask, bool value)
		{
			if (value)
			{
				this.flags |= mask;
				return;
			}
			this.flags &= ~mask;
		}

		// Token: 0x04000552 RID: 1362
		private readonly ODataEntry entry;

		// Token: 0x04000553 RID: 1363
		private readonly EntityDescriptor entityDescriptor;

		// Token: 0x04000554 RID: 1364
		private readonly bool isAtomOrTracking;

		// Token: 0x04000555 RID: 1365
		private MaterializerEntry.EntryFlags flags;

		// Token: 0x04000556 RID: 1366
		private ICollection<ODataNavigationLink> navigationLinks = ODataMaterializer.EmptyLinks;

		// Token: 0x02000116 RID: 278
		[Flags]
		private enum EntryFlags
		{
			// Token: 0x0400055A RID: 1370
			ShouldUpdateFromPayload = 1,
			// Token: 0x0400055B RID: 1371
			CreatedByMaterializer = 2,
			// Token: 0x0400055C RID: 1372
			EntityHasBeenResolved = 4,
			// Token: 0x0400055D RID: 1373
			EntityDescriptorUpdated = 8,
			// Token: 0x0400055E RID: 1374
			ForLoadProperty = 16
		}
	}
}
