using System;
using System.Data.Services.Client.Metadata;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000045 RID: 69
	internal class EntityTrackingAdapter
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000B418 File Offset: 0x00009618
		internal EntityTrackingAdapter(EntityTrackerBase entityTracker, MergeOption mergeOption, ClientEdmModel model, DataServiceContext context)
		{
			this.MaterializationLog = new AtomMaterializerLog(mergeOption, model, entityTracker);
			this.MergeOption = mergeOption;
			this.EntityTracker = entityTracker;
			this.Model = model;
			this.Context = context;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000B44B File Offset: 0x0000964B
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000B453 File Offset: 0x00009653
		internal MergeOption MergeOption { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000B45C File Offset: 0x0000965C
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000B464 File Offset: 0x00009664
		internal DataServiceContext Context { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000B46D File Offset: 0x0000966D
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000B475 File Offset: 0x00009675
		internal AtomMaterializerLog MaterializationLog { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000B47E File Offset: 0x0000967E
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000B486 File Offset: 0x00009686
		internal EntityTrackerBase EntityTracker { get; private set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000B48F File Offset: 0x0000968F
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000B497 File Offset: 0x00009697
		internal ClientEdmModel Model { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000B4A0 File Offset: 0x000096A0
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000B4A8 File Offset: 0x000096A8
		internal object TargetInstance
		{
			get
			{
				return this.targetInstance;
			}
			set
			{
				this.targetInstance = value;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000B4B1 File Offset: 0x000096B1
		internal virtual bool TryResolveExistingEntity(MaterializerEntry entry, Type expectedEntryType)
		{
			return this.TryResolveAsTarget(entry) || this.TryResolveAsExistingEntry(entry, expectedEntryType);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000B4CB File Offset: 0x000096CB
		internal bool TryResolveAsExistingEntry(MaterializerEntry entry, Type expectedEntryType)
		{
			if (!entry.IsAtomOrTracking)
			{
				return false;
			}
			if (entry.Id == null)
			{
				throw Error.InvalidOperation(Strings.Deserialize_MissingIdElement);
			}
			return this.TryResolveAsCreated(entry) || this.TryResolveFromContext(entry, expectedEntryType);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000B500 File Offset: 0x00009700
		private bool TryResolveAsTarget(MaterializerEntry entry)
		{
			if (entry.ResolvedObject == null)
			{
				return false;
			}
			ClientEdmModel model = this.Model;
			entry.ActualType = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entry.ResolvedObject.GetType()));
			this.MaterializationLog.FoundTargetInstance(entry);
			entry.ShouldUpdateFromPayload = this.MergeOption != MergeOption.PreserveChanges;
			entry.EntityHasBeenResolved = true;
			return true;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000B564 File Offset: 0x00009764
		private bool TryResolveFromContext(MaterializerEntry entry, Type expectedEntryType)
		{
			if (this.MergeOption != MergeOption.NoTracking)
			{
				EntityStates entityStates;
				entry.ResolvedObject = this.EntityTracker.TryGetEntity(entry.Id, out entityStates);
				if (entry.ResolvedObject != null)
				{
					if (!expectedEntryType.IsInstanceOfType(entry.ResolvedObject))
					{
						throw Error.InvalidOperation(Strings.Deserialize_Current(expectedEntryType, entry.ResolvedObject.GetType()));
					}
					ClientEdmModel model = this.Model;
					entry.ActualType = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entry.ResolvedObject.GetType()));
					entry.EntityHasBeenResolved = true;
					entry.ShouldUpdateFromPayload = this.MergeOption == MergeOption.OverwriteChanges || (this.MergeOption == MergeOption.PreserveChanges && entityStates == EntityStates.Unchanged) || (this.MergeOption == MergeOption.PreserveChanges && entityStates == EntityStates.Deleted);
					this.MaterializationLog.FoundExistingInstance(entry);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000B630 File Offset: 0x00009830
		private bool TryResolveAsCreated(MaterializerEntry entry)
		{
			MaterializerEntry materializerEntry;
			if (!this.MaterializationLog.TryResolve(entry, out materializerEntry))
			{
				return false;
			}
			entry.ActualType = materializerEntry.ActualType;
			entry.ResolvedObject = materializerEntry.ResolvedObject;
			entry.CreatedByMaterializer = materializerEntry.CreatedByMaterializer;
			entry.ShouldUpdateFromPayload = materializerEntry.ShouldUpdateFromPayload;
			entry.EntityHasBeenResolved = true;
			return true;
		}

		// Token: 0x04000227 RID: 551
		private object targetInstance;
	}
}
