using System;
using System.Collections.Generic;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200004F RID: 79
	public class DataServiceClientRequestPipelineConfiguration
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		internal DataServiceClientRequestPipelineConfiguration()
		{
			this.writeEntityReferenceLinkActions = new List<Action<WritingEntityReferenceLinkArgs>>();
			this.writingEndEntryActions = new List<Action<WritingEntryArgs>>();
			this.writingEndNavigationLinkActions = new List<Action<WritingNavigationLinkArgs>>();
			this.writingStartEntryActions = new List<Action<WritingEntryArgs>>();
			this.writingStartNavigationLinkActions = new List<Action<WritingNavigationLinkArgs>>();
			this.messageWriterSettingsConfigurationActions = new List<Action<MessageWriterSettingsArgs>>();
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000CB19 File Offset: 0x0000AD19
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000CB21 File Offset: 0x0000AD21
		public Func<DataServiceClientRequestMessageArgs, DataServiceClientRequestMessage> OnMessageCreating
		{
			get
			{
				return this.onmessageCreating;
			}
			set
			{
				if (this.ContextUsingSendingRequest)
				{
					throw new DataServiceClientException(Strings.Context_SendingRequest_InvalidWhenUsingOnMessageCreating);
				}
				this.onmessageCreating = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000CB3D File Offset: 0x0000AD3D
		internal bool HasOnMessageCreating
		{
			get
			{
				return this.OnMessageCreating != null;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000CB4B File Offset: 0x0000AD4B
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000CB53 File Offset: 0x0000AD53
		internal bool ContextUsingSendingRequest { get; set; }

		// Token: 0x06000293 RID: 659 RVA: 0x0000CB5C File Offset: 0x0000AD5C
		public DataServiceClientRequestPipelineConfiguration OnMessageWriterSettingsCreated(Action<MessageWriterSettingsArgs> args)
		{
			WebUtil.CheckArgumentNull<Action<MessageWriterSettingsArgs>>(args, "args");
			this.messageWriterSettingsConfigurationActions.Add(args);
			return this;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000CB77 File Offset: 0x0000AD77
		public DataServiceClientRequestPipelineConfiguration OnEntryStarting(Action<WritingEntryArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<WritingEntryArgs>>(action, "action");
			this.writingStartEntryActions.Add(action);
			return this;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000CB92 File Offset: 0x0000AD92
		public DataServiceClientRequestPipelineConfiguration OnEntryEnding(Action<WritingEntryArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<WritingEntryArgs>>(action, "action");
			this.writingEndEntryActions.Add(action);
			return this;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000CBAD File Offset: 0x0000ADAD
		public DataServiceClientRequestPipelineConfiguration OnEntityReferenceLink(Action<WritingEntityReferenceLinkArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<WritingEntityReferenceLinkArgs>>(action, "action");
			this.writeEntityReferenceLinkActions.Add(action);
			return this;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000CBC8 File Offset: 0x0000ADC8
		public DataServiceClientRequestPipelineConfiguration OnNavigationLinkStarting(Action<WritingNavigationLinkArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<WritingNavigationLinkArgs>>(action, "action");
			this.writingStartNavigationLinkActions.Add(action);
			return this;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000CBE3 File Offset: 0x0000ADE3
		public DataServiceClientRequestPipelineConfiguration OnNavigationLinkEnding(Action<WritingNavigationLinkArgs> action)
		{
			WebUtil.CheckArgumentNull<Action<WritingNavigationLinkArgs>>(action, "action");
			this.writingEndNavigationLinkActions.Add(action);
			return this;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000CC00 File Offset: 0x0000AE00
		internal void ExecuteWriterSettingsConfiguration(ODataMessageWriterSettingsBase writerSettings)
		{
			if (this.messageWriterSettingsConfigurationActions.Count > 0)
			{
				MessageWriterSettingsArgs messageWriterSettingsArgs = new MessageWriterSettingsArgs(new DataServiceClientMessageWriterSettingsShim(writerSettings));
				foreach (Action<MessageWriterSettingsArgs> action in this.messageWriterSettingsConfigurationActions)
				{
					action(messageWriterSettingsArgs);
				}
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000CC70 File Offset: 0x0000AE70
		internal void ExecuteOnEntryEndActions(ODataEntry entry, object entity)
		{
			if (this.writingEndEntryActions.Count > 0)
			{
				WritingEntryArgs writingEntryArgs = new WritingEntryArgs(entry, entity);
				foreach (Action<WritingEntryArgs> action in this.writingEndEntryActions)
				{
					action(writingEntryArgs);
				}
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		internal void ExecuteOnEntryStartActions(ODataEntry entry, object entity)
		{
			if (this.writingStartEntryActions.Count > 0)
			{
				WritingEntryArgs writingEntryArgs = new WritingEntryArgs(entry, entity);
				foreach (Action<WritingEntryArgs> action in this.writingStartEntryActions)
				{
					action(writingEntryArgs);
				}
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000CD48 File Offset: 0x0000AF48
		internal void ExecuteOnNavigationLinkEndActions(ODataNavigationLink link, object source, object target)
		{
			if (this.writingEndNavigationLinkActions.Count > 0)
			{
				WritingNavigationLinkArgs writingNavigationLinkArgs = new WritingNavigationLinkArgs(link, source, target);
				foreach (Action<WritingNavigationLinkArgs> action in this.writingEndNavigationLinkActions)
				{
					action(writingNavigationLinkArgs);
				}
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
		internal void ExecuteOnNavigationLinkStartActions(ODataNavigationLink link, object source, object target)
		{
			if (this.writingStartNavigationLinkActions.Count > 0)
			{
				WritingNavigationLinkArgs writingNavigationLinkArgs = new WritingNavigationLinkArgs(link, source, target);
				foreach (Action<WritingNavigationLinkArgs> action in this.writingStartNavigationLinkActions)
				{
					action(writingNavigationLinkArgs);
				}
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000CE20 File Offset: 0x0000B020
		internal void ExecuteEntityReferenceLinkActions(ODataEntityReferenceLink entityReferenceLink, object source, object target)
		{
			if (this.writeEntityReferenceLinkActions.Count > 0)
			{
				WritingEntityReferenceLinkArgs writingEntityReferenceLinkArgs = new WritingEntityReferenceLinkArgs(entityReferenceLink, source, target);
				foreach (Action<WritingEntityReferenceLinkArgs> action in this.writeEntityReferenceLinkActions)
				{
					action(writingEntityReferenceLinkArgs);
				}
			}
		}

		// Token: 0x0400024A RID: 586
		private readonly List<Action<WritingEntryArgs>> writingStartEntryActions;

		// Token: 0x0400024B RID: 587
		private readonly List<Action<WritingEntryArgs>> writingEndEntryActions;

		// Token: 0x0400024C RID: 588
		private readonly List<Action<WritingEntityReferenceLinkArgs>> writeEntityReferenceLinkActions;

		// Token: 0x0400024D RID: 589
		private readonly List<Action<WritingNavigationLinkArgs>> writingStartNavigationLinkActions;

		// Token: 0x0400024E RID: 590
		private readonly List<Action<WritingNavigationLinkArgs>> writingEndNavigationLinkActions;

		// Token: 0x0400024F RID: 591
		private readonly List<Action<MessageWriterSettingsArgs>> messageWriterSettingsConfigurationActions;

		// Token: 0x04000250 RID: 592
		private Func<DataServiceClientRequestMessageArgs, DataServiceClientRequestMessage> onmessageCreating;
	}
}
