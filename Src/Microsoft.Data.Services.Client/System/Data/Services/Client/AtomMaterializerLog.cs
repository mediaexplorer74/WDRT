using System;
using System.Collections.Generic;
using System.Data.Services.Client.Materialization;
using System.Data.Services.Client.Metadata;
using System.Linq;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x020000E3 RID: 227
	internal class AtomMaterializerLog
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x0001FAC0 File Offset: 0x0001DCC0
		internal AtomMaterializerLog(MergeOption mergeOption, ClientEdmModel model, EntityTrackerBase entityTracker)
		{
			this.appendOnlyEntries = new Dictionary<string, ODataEntry>(StringComparer.Ordinal);
			this.mergeOption = mergeOption;
			this.clientEdmModel = model;
			this.entityTracker = entityTracker;
			this.identityStack = new Dictionary<string, ODataEntry>(StringComparer.Ordinal);
			this.links = new List<LinkDescriptor>();
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001FB13 File Offset: 0x0001DD13
		internal bool Tracking
		{
			get
			{
				return this.mergeOption != MergeOption.NoTracking;
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001FB24 File Offset: 0x0001DD24
		internal static void MergeEntityDescriptorInfo(EntityDescriptor trackedEntityDescriptor, EntityDescriptor entityDescriptorFromMaterializer, bool mergeInfo, MergeOption mergeOption)
		{
			if (!object.ReferenceEquals(trackedEntityDescriptor, entityDescriptorFromMaterializer))
			{
				if (entityDescriptorFromMaterializer.ETag != null && mergeOption != MergeOption.AppendOnly)
				{
					trackedEntityDescriptor.ETag = entityDescriptorFromMaterializer.ETag;
				}
				if (mergeInfo)
				{
					if (entityDescriptorFromMaterializer.SelfLink != null)
					{
						trackedEntityDescriptor.SelfLink = entityDescriptorFromMaterializer.SelfLink;
					}
					if (entityDescriptorFromMaterializer.EditLink != null)
					{
						trackedEntityDescriptor.EditLink = entityDescriptorFromMaterializer.EditLink;
					}
					foreach (LinkInfo linkInfo in entityDescriptorFromMaterializer.LinkInfos)
					{
						trackedEntityDescriptor.MergeLinkInfo(linkInfo);
					}
					foreach (StreamDescriptor streamDescriptor in entityDescriptorFromMaterializer.StreamDescriptors)
					{
						trackedEntityDescriptor.MergeStreamDescriptor(streamDescriptor);
					}
					trackedEntityDescriptor.ServerTypeName = entityDescriptorFromMaterializer.ServerTypeName;
				}
				if (entityDescriptorFromMaterializer.OperationDescriptors != null)
				{
					trackedEntityDescriptor.ClearOperationDescriptors();
					trackedEntityDescriptor.AppendOperationalDescriptors(entityDescriptorFromMaterializer.OperationDescriptors);
				}
				if (entityDescriptorFromMaterializer.ReadStreamUri != null)
				{
					trackedEntityDescriptor.ReadStreamUri = entityDescriptorFromMaterializer.ReadStreamUri;
				}
				if (entityDescriptorFromMaterializer.EditStreamUri != null)
				{
					trackedEntityDescriptor.EditStreamUri = entityDescriptorFromMaterializer.EditStreamUri;
				}
				if (entityDescriptorFromMaterializer.ReadStreamUri != null || entityDescriptorFromMaterializer.EditStreamUri != null)
				{
					trackedEntityDescriptor.StreamETag = entityDescriptorFromMaterializer.StreamETag;
				}
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001FC8C File Offset: 0x0001DE8C
		internal void ApplyToContext()
		{
			if (!this.Tracking)
			{
				return;
			}
			foreach (KeyValuePair<string, ODataEntry> keyValuePair in this.identityStack)
			{
				MaterializerEntry entry = MaterializerEntry.GetEntry(keyValuePair.Value);
				bool flag = entry.CreatedByMaterializer || entry.ResolvedObject == this.insertRefreshObject || entry.ShouldUpdateFromPayload;
				EntityDescriptor entityDescriptor = this.entityTracker.InternalAttachEntityDescriptor(entry.EntityDescriptor, false);
				AtomMaterializerLog.MergeEntityDescriptorInfo(entityDescriptor, entry.EntityDescriptor, flag, this.mergeOption);
				if (flag && (this.mergeOption != MergeOption.PreserveChanges || entityDescriptor.State != EntityStates.Deleted))
				{
					entityDescriptor.State = EntityStates.Unchanged;
				}
			}
			foreach (LinkDescriptor linkDescriptor in this.links)
			{
				if (EntityStates.Added == linkDescriptor.State)
				{
					this.entityTracker.AttachLink(linkDescriptor.Source, linkDescriptor.SourceProperty, linkDescriptor.Target, this.mergeOption);
				}
				else if (EntityStates.Modified == linkDescriptor.State)
				{
					object obj = linkDescriptor.Target;
					if (MergeOption.PreserveChanges == this.mergeOption)
					{
						LinkDescriptor linkDescriptor2 = this.entityTracker.GetLinks(linkDescriptor.Source, linkDescriptor.SourceProperty).SingleOrDefault<LinkDescriptor>();
						if (linkDescriptor2 != null && linkDescriptor2.Target == null)
						{
							continue;
						}
						if ((obj != null && EntityStates.Deleted == this.entityTracker.GetEntityDescriptor(obj).State) || EntityStates.Deleted == this.entityTracker.GetEntityDescriptor(linkDescriptor.Source).State)
						{
							obj = null;
						}
					}
					this.entityTracker.AttachLink(linkDescriptor.Source, linkDescriptor.SourceProperty, obj, this.mergeOption);
				}
				else
				{
					this.entityTracker.DetachExistingLink(linkDescriptor, false);
				}
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001FE98 File Offset: 0x0001E098
		internal void Clear()
		{
			this.identityStack.Clear();
			this.links.Clear();
			this.insertRefreshObject = null;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001FEB7 File Offset: 0x0001E0B7
		internal void FoundExistingInstance(MaterializerEntry entry)
		{
			this.identityStack[entry.Id] = entry.Entry;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001FED0 File Offset: 0x0001E0D0
		internal void FoundTargetInstance(MaterializerEntry entry)
		{
			if (AtomMaterializerLog.IsEntity(entry))
			{
				this.entityTracker.AttachIdentity(entry.EntityDescriptor, this.mergeOption);
				this.identityStack.Add(entry.Id, entry.Entry);
				this.insertRefreshObject = entry.ResolvedObject;
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001FF20 File Offset: 0x0001E120
		internal bool TryResolve(MaterializerEntry entry, out MaterializerEntry existingEntry)
		{
			ODataEntry odataEntry;
			if (this.identityStack.TryGetValue(entry.Id, out odataEntry))
			{
				existingEntry = MaterializerEntry.GetEntry(odataEntry);
				return true;
			}
			if (this.appendOnlyEntries.TryGetValue(entry.Id, out odataEntry))
			{
				EntityStates entityStates;
				this.entityTracker.TryGetEntity(entry.Id, out entityStates);
				if (entityStates == EntityStates.Unchanged)
				{
					existingEntry = MaterializerEntry.GetEntry(odataEntry);
					return true;
				}
				this.appendOnlyEntries.Remove(entry.Id);
			}
			existingEntry = null;
			return false;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001FF9C File Offset: 0x0001E19C
		internal void AddedLink(MaterializerEntry source, string propertyName, object target)
		{
			if (!this.Tracking)
			{
				return;
			}
			if (AtomMaterializerLog.IsEntity(source) && AtomMaterializerLog.IsEntity(target, this.clientEdmModel))
			{
				LinkDescriptor linkDescriptor = new LinkDescriptor(source.ResolvedObject, propertyName, target, EntityStates.Added);
				this.links.Add(linkDescriptor);
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001FFE4 File Offset: 0x0001E1E4
		internal void CreatedInstance(MaterializerEntry entry)
		{
			if (AtomMaterializerLog.IsEntity(entry) && entry.IsAtomOrTracking)
			{
				this.identityStack.Add(entry.Id, entry.Entry);
				if (this.mergeOption == MergeOption.AppendOnly)
				{
					this.appendOnlyEntries.Add(entry.Id, entry.Entry);
				}
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00020038 File Offset: 0x0001E238
		internal void RemovedLink(MaterializerEntry source, string propertyName, object target)
		{
			if (AtomMaterializerLog.IsEntity(source) && AtomMaterializerLog.IsEntity(target, this.clientEdmModel))
			{
				LinkDescriptor linkDescriptor = new LinkDescriptor(source.ResolvedObject, propertyName, target, EntityStates.Detached);
				this.links.Add(linkDescriptor);
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00020078 File Offset: 0x0001E278
		internal void SetLink(MaterializerEntry source, string propertyName, object target)
		{
			if (!this.Tracking)
			{
				return;
			}
			if (AtomMaterializerLog.IsEntity(source) && AtomMaterializerLog.IsEntity(target, this.clientEdmModel))
			{
				LinkDescriptor linkDescriptor = new LinkDescriptor(source.ResolvedObject, propertyName, target, EntityStates.Modified);
				this.links.Add(linkDescriptor);
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000200C0 File Offset: 0x0001E2C0
		private static bool IsEntity(MaterializerEntry entry)
		{
			return entry.ActualType.IsEntityType;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000200CD File Offset: 0x0001E2CD
		private static bool IsEntity(object entity, ClientEdmModel model)
		{
			return entity == null || ClientTypeUtil.TypeIsEntity(entity.GetType(), model);
		}

		// Token: 0x0400048F RID: 1167
		private readonly MergeOption mergeOption;

		// Token: 0x04000490 RID: 1168
		private readonly ClientEdmModel clientEdmModel;

		// Token: 0x04000491 RID: 1169
		private readonly EntityTrackerBase entityTracker;

		// Token: 0x04000492 RID: 1170
		private readonly Dictionary<string, ODataEntry> appendOnlyEntries;

		// Token: 0x04000493 RID: 1171
		private readonly Dictionary<string, ODataEntry> identityStack;

		// Token: 0x04000494 RID: 1172
		private readonly List<LinkDescriptor> links;

		// Token: 0x04000495 RID: 1173
		private object insertRefreshObject;
	}
}
