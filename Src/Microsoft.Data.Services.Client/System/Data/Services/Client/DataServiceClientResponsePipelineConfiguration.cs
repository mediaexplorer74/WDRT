using System;
using System.Collections.Generic;
using System.Data.Services.Client.Materialization;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000056 RID: 86
	public class DataServiceClientResponsePipelineConfiguration
	{
		// Token: 0x060002BF RID: 703 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		internal DataServiceClientResponsePipelineConfiguration(object sender)
		{
			this.sender = sender;
			this.readingEndEntryActions = new List<Action<ReadingEntryArgs>>();
			this.readingEndFeedActions = new List<Action<ReadingFeedArgs>>();
			this.readingEndNavigationLinkActions = new List<Action<ReadingNavigationLinkArgs>>();
			this.readingStartEntryActions = new List<Action<ReadingEntryArgs>>();
			this.readingStartFeedActions = new List<Action<ReadingFeedArgs>>();
			this.readingStartNavigationLinkActions = new List<Action<ReadingNavigationLinkArgs>>();
			this.materializedEntityActions = new List<Action<MaterializedEntityArgs>>();
			this.messageReaderSettingsConfigurationActions = new List<Action<MessageReaderSettingsArgs>>();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002C0 RID: 704 RVA: 0x0000D02C File Offset: 0x0000B22C
		// (remove) Token: 0x060002C1 RID: 705 RVA: 0x0000D064 File Offset: 0x0000B264
		internal event EventHandler<ReadingWritingEntityEventArgs> ReadingAtomEntity;

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000D09C File Offset: 0x0000B29C
		internal bool HasConfigurations
		{
			get
			{
				return this.readingStartEntryActions.Count > 0 || this.readingEndEntryActions.Count > 0 || this.readingStartFeedActions.Count > 0 || this.readingEndFeedActions.Count > 0 || this.readingStartNavigationLinkActions.Count > 0 || this.readingEndNavigationLinkActions.Count > 0;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000D0FF File Offset: 0x0000B2FF
		internal bool HasAtomReadingEntityHandlers
		{
			get
			{
				return this.ReadingAtomEntity != null;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000D10D File Offset: 0x0000B30D
		internal bool HasReadingEntityHandlers
		{
			get
			{
				return this.ReadingAtomEntity != null || this.materializedEntityActions.Count > 0;
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D128 File Offset: 0x0000B328
		public DataServiceClientResponsePipelineConfiguration OnMessageReaderSettingsCreated(Action<MessageReaderSettingsArgs> messageReaderSettingsAction)
		{
			WebUtil.CheckArgumentNull<Action<MessageReaderSettingsArgs>>(messageReaderSettingsAction, "messageReaderSettingsAction");
			this.messageReaderSettingsConfigurationActions.Add(messageReaderSettingsAction);
			return this;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D143 File Offset: 0x0000B343
		public DataServiceClientResponsePipelineConfiguration OnEntryStarted(Action<ReadingEntryArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<ReadingEntryArgs>>(action, "action");
			this.readingStartEntryActions.Add(action);
			return this;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D15E File Offset: 0x0000B35E
		public DataServiceClientResponsePipelineConfiguration OnEntryEnded(Action<ReadingEntryArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<ReadingEntryArgs>>(action, "action");
			this.readingEndEntryActions.Add(action);
			return this;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D179 File Offset: 0x0000B379
		public DataServiceClientResponsePipelineConfiguration OnFeedStarted(Action<ReadingFeedArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<ReadingFeedArgs>>(action, "action");
			this.readingStartFeedActions.Add(action);
			return this;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D194 File Offset: 0x0000B394
		public DataServiceClientResponsePipelineConfiguration OnFeedEnded(Action<ReadingFeedArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<ReadingFeedArgs>>(action, "action");
			this.readingEndFeedActions.Add(action);
			return this;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000D1AF File Offset: 0x0000B3AF
		public DataServiceClientResponsePipelineConfiguration OnNavigationLinkStarted(Action<ReadingNavigationLinkArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<ReadingNavigationLinkArgs>>(action, "action");
			this.readingStartNavigationLinkActions.Add(action);
			return this;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000D1CA File Offset: 0x0000B3CA
		public DataServiceClientResponsePipelineConfiguration OnNavigationLinkEnded(Action<ReadingNavigationLinkArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<ReadingNavigationLinkArgs>>(action, "action");
			this.readingEndNavigationLinkActions.Add(action);
			return this;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000D1E5 File Offset: 0x0000B3E5
		public DataServiceClientResponsePipelineConfiguration OnEntityMaterialized(Action<MaterializedEntityArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<MaterializedEntityArgs>>(action, "action");
			this.materializedEntityActions.Add(action);
			return this;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000D200 File Offset: 0x0000B400
		internal void ExecuteReaderSettingsConfiguration(ODataMessageReaderSettingsBase readerSettings)
		{
			if (this.messageReaderSettingsConfigurationActions.Count > 0)
			{
				MessageReaderSettingsArgs messageReaderSettingsArgs = new MessageReaderSettingsArgs(new DataServiceClientMessageReaderSettingsShim(readerSettings));
				foreach (Action<MessageReaderSettingsArgs> action in this.messageReaderSettingsConfigurationActions)
				{
					action(messageReaderSettingsArgs);
				}
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000D270 File Offset: 0x0000B470
		internal void ExecuteOnEntryEndActions(ODataEntry entry)
		{
			if (this.readingEndEntryActions.Count > 0)
			{
				ReadingEntryArgs readingEntryArgs = new ReadingEntryArgs(entry);
				foreach (Action<ReadingEntryArgs> action in this.readingEndEntryActions)
				{
					action(readingEntryArgs);
				}
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		internal void ExecuteOnEntryStartActions(ODataEntry entry)
		{
			if (this.readingStartEntryActions.Count > 0)
			{
				ReadingEntryArgs readingEntryArgs = new ReadingEntryArgs(entry);
				foreach (Action<ReadingEntryArgs> action in this.readingStartEntryActions)
				{
					action(readingEntryArgs);
				}
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000D340 File Offset: 0x0000B540
		internal void ExecuteOnFeedEndActions(ODataFeed feed)
		{
			if (this.readingEndFeedActions.Count > 0)
			{
				ReadingFeedArgs readingFeedArgs = new ReadingFeedArgs(feed);
				foreach (Action<ReadingFeedArgs> action in this.readingEndFeedActions)
				{
					action(readingFeedArgs);
				}
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		internal void ExecuteOnFeedStartActions(ODataFeed feed)
		{
			if (this.readingStartFeedActions.Count > 0)
			{
				ReadingFeedArgs readingFeedArgs = new ReadingFeedArgs(feed);
				foreach (Action<ReadingFeedArgs> action in this.readingStartFeedActions)
				{
					action(readingFeedArgs);
				}
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000D410 File Offset: 0x0000B610
		internal void ExecuteOnNavigationEndActions(ODataNavigationLink link)
		{
			if (this.readingEndNavigationLinkActions.Count > 0)
			{
				ReadingNavigationLinkArgs readingNavigationLinkArgs = new ReadingNavigationLinkArgs(link);
				foreach (Action<ReadingNavigationLinkArgs> action in this.readingEndNavigationLinkActions)
				{
					action(readingNavigationLinkArgs);
				}
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000D478 File Offset: 0x0000B678
		internal void ExecuteOnNavigationStartActions(ODataNavigationLink link)
		{
			if (this.readingStartNavigationLinkActions.Count > 0)
			{
				ReadingNavigationLinkArgs readingNavigationLinkArgs = new ReadingNavigationLinkArgs(link);
				foreach (Action<ReadingNavigationLinkArgs> action in this.readingStartNavigationLinkActions)
				{
					action(readingNavigationLinkArgs);
				}
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		internal void ExecuteEntityMaterializedActions(ODataEntry entry, object entity)
		{
			if (this.materializedEntityActions.Count > 0)
			{
				MaterializedEntityArgs materializedEntityArgs = new MaterializedEntityArgs(entry, entity);
				foreach (Action<MaterializedEntityArgs> action in this.materializedEntityActions)
				{
					action(materializedEntityArgs);
				}
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000D54C File Offset: 0x0000B74C
		internal void FireReadingAtomEntityEvent(MaterializerEntry materializerEntry)
		{
			if (this.ReadingAtomEntity != null && materializerEntry.Format == ODataFormat.Atom)
			{
				ReadingEntityInfo annotation = materializerEntry.Entry.GetAnnotation<ReadingEntityInfo>();
				ReadingWritingEntityEventArgs readingWritingEntityEventArgs = new ReadingWritingEntityEventArgs(materializerEntry.ResolvedObject, annotation.EntryPayload, annotation.BaseUri);
				this.ReadingAtomEntity(this.sender, readingWritingEntityEventArgs);
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000D5A4 File Offset: 0x0000B7A4
		internal void FireEndEntryEvents(MaterializerEntry entry)
		{
			if (this.HasReadingEntityHandlers)
			{
				this.ExecuteEntityMaterializedActions(entry.Entry, entry.ResolvedObject);
				this.FireReadingAtomEntityEvent(entry);
			}
		}

		// Token: 0x04000256 RID: 598
		private readonly List<Action<ReadingEntryArgs>> readingStartEntryActions;

		// Token: 0x04000257 RID: 599
		private readonly List<Action<ReadingEntryArgs>> readingEndEntryActions;

		// Token: 0x04000258 RID: 600
		private readonly List<Action<ReadingFeedArgs>> readingStartFeedActions;

		// Token: 0x04000259 RID: 601
		private readonly List<Action<ReadingFeedArgs>> readingEndFeedActions;

		// Token: 0x0400025A RID: 602
		private readonly List<Action<ReadingNavigationLinkArgs>> readingStartNavigationLinkActions;

		// Token: 0x0400025B RID: 603
		private readonly List<Action<ReadingNavigationLinkArgs>> readingEndNavigationLinkActions;

		// Token: 0x0400025C RID: 604
		private readonly List<Action<MaterializedEntityArgs>> materializedEntityActions;

		// Token: 0x0400025D RID: 605
		private readonly List<Action<MessageReaderSettingsArgs>> messageReaderSettingsConfigurationActions;

		// Token: 0x0400025E RID: 606
		private readonly object sender;
	}
}
