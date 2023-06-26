using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x020001A5 RID: 421
	internal abstract class ODataWriterCore : ODataWriter, IODataOutputInStreamErrorListener
	{
		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002C8CC File Offset: 0x0002AACC
		protected ODataWriterCore(ODataOutputContext outputContext, IEdmEntitySet entitySet, IEdmEntityType entityType, bool writingFeed)
		{
			this.outputContext = outputContext;
			this.writingFeed = writingFeed;
			if (this.writingFeed && this.outputContext.Model.IsUserModel())
			{
				this.feedValidator = new FeedWithoutExpectedTypeValidator();
			}
			if (entitySet != null && entityType == null)
			{
				entityType = this.outputContext.EdmTypeResolver.GetElementType(entitySet);
			}
			this.scopes.Push(new ODataWriterCore.Scope(ODataWriterCore.WriterState.Start, null, entitySet, entityType, false, outputContext.MessageWriterSettings.MetadataDocumentUri.SelectedProperties()));
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002C95C File Offset: 0x0002AB5C
		protected ODataWriterCore.Scope CurrentScope
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0002C969 File Offset: 0x0002AB69
		protected ODataWriterCore.WriterState State
		{
			get
			{
				return this.CurrentScope.State;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0002C976 File Offset: 0x0002AB76
		protected bool SkipWriting
		{
			get
			{
				return this.CurrentScope.SkipWriting;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0002C983 File Offset: 0x0002AB83
		protected bool IsTopLevel
		{
			get
			{
				return this.scopes.Count == 2;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0002C994 File Offset: 0x0002AB94
		protected ODataNavigationLink ParentNavigationLink
		{
			get
			{
				ODataWriterCore.Scope parentOrNull = this.scopes.ParentOrNull;
				if (parentOrNull != null)
				{
					return parentOrNull.Item as ODataNavigationLink;
				}
				return null;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0002C9C0 File Offset: 0x0002ABC0
		protected IEdmEntityType ParentEntryEntityType
		{
			get
			{
				ODataWriterCore.Scope parent = this.scopes.Parent;
				return parent.EntityType;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0002C9E0 File Offset: 0x0002ABE0
		protected IEdmEntitySet ParentEntryEntitySet
		{
			get
			{
				ODataWriterCore.Scope parent = this.scopes.Parent;
				return parent.EntitySet;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0002C9FF File Offset: 0x0002ABFF
		protected int FeedScopeEntryCount
		{
			get
			{
				return ((ODataWriterCore.FeedScope)this.CurrentScope).EntryCount;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0002CA14 File Offset: 0x0002AC14
		protected DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker
		{
			get
			{
				ODataWriterCore.EntryScope entryScope;
				switch (this.State)
				{
				case ODataWriterCore.WriterState.Entry:
					entryScope = (ODataWriterCore.EntryScope)this.CurrentScope;
					goto IL_53;
				case ODataWriterCore.WriterState.NavigationLink:
				case ODataWriterCore.WriterState.NavigationLinkWithContent:
					entryScope = (ODataWriterCore.EntryScope)this.scopes.Parent;
					goto IL_53;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_DuplicatePropertyNamesChecker));
				IL_53:
				return entryScope.DuplicatePropertyNamesChecker;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0002CA7A File Offset: 0x0002AC7A
		protected IEdmEntityType EntryEntityType
		{
			get
			{
				return this.CurrentScope.EntityType;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0002CA88 File Offset: 0x0002AC88
		protected ODataWriterCore.NavigationLinkScope ParentNavigationLinkScope
		{
			get
			{
				ODataWriterCore.Scope scope = this.scopes.Parent;
				if (scope.State == ODataWriterCore.WriterState.Start)
				{
					return null;
				}
				if (scope.State == ODataWriterCore.WriterState.Feed)
				{
					scope = this.scopes.ParentOfParent;
					if (scope.State == ODataWriterCore.WriterState.Start)
					{
						return null;
					}
				}
				if (scope.State == ODataWriterCore.WriterState.NavigationLinkWithContent)
				{
					return (ODataWriterCore.NavigationLinkScope)scope;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_ParentNavigationLinkScope));
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0002CAEA File Offset: 0x0002ACEA
		private FeedWithoutExpectedTypeValidator CurrentFeedValidator
		{
			get
			{
				if (this.scopes.Count != 3)
				{
					return null;
				}
				return this.feedValidator;
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002CB04 File Offset: 0x0002AD04
		public sealed override void Flush()
		{
			this.VerifyCanFlush(true);
			try
			{
				this.FlushSynchronously();
			}
			catch
			{
				this.EnterScope(ODataWriterCore.WriterState.Error, null);
				throw;
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002CB46 File Offset: 0x0002AD46
		public sealed override Task FlushAsync()
		{
			this.VerifyCanFlush(false);
			return this.FlushAsynchronously().FollowOnFaultWith(delegate(Task t)
			{
				this.EnterScope(ODataWriterCore.WriterState.Error, null);
			});
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002CB66 File Offset: 0x0002AD66
		public sealed override void WriteStart(ODataFeed feed)
		{
			this.VerifyCanWriteStartFeed(true, feed);
			this.WriteStartFeedImplementation(feed);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0002CB94 File Offset: 0x0002AD94
		public sealed override Task WriteStartAsync(ODataFeed feed)
		{
			this.VerifyCanWriteStartFeed(false, feed);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStartFeedImplementation(feed);
			});
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002CBD3 File Offset: 0x0002ADD3
		public sealed override void WriteStart(ODataEntry entry)
		{
			this.VerifyCanWriteStartEntry(true, entry);
			this.WriteStartEntryImplementation(entry);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002CC00 File Offset: 0x0002AE00
		public sealed override Task WriteStartAsync(ODataEntry entry)
		{
			this.VerifyCanWriteStartEntry(false, entry);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStartEntryImplementation(entry);
			});
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002CC3F File Offset: 0x0002AE3F
		public sealed override void WriteStart(ODataNavigationLink navigationLink)
		{
			this.VerifyCanWriteStartNavigationLink(true, navigationLink);
			this.WriteStartNavigationLinkImplementation(navigationLink);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002CC6C File Offset: 0x0002AE6C
		public sealed override Task WriteStartAsync(ODataNavigationLink navigationLink)
		{
			this.VerifyCanWriteStartNavigationLink(false, navigationLink);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStartNavigationLinkImplementation(navigationLink);
			});
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002CCAB File Offset: 0x0002AEAB
		public sealed override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.WriteEndImplementation();
			if (this.CurrentScope.State == ODataWriterCore.WriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002CCEA File Offset: 0x0002AEEA
		public sealed override Task WriteEndAsync()
		{
			this.VerifyCanWriteEnd(false);
			return TaskUtils.GetTaskForSynchronousOperation(new Action(this.WriteEndImplementation)).FollowOnSuccessWithTask(delegate(Task task)
			{
				if (this.CurrentScope.State == ODataWriterCore.WriterState.Completed)
				{
					return this.FlushAsync();
				}
				return TaskUtils.CompletedTask;
			});
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002CD15 File Offset: 0x0002AF15
		public sealed override void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
			this.VerifyCanWriteEntityReferenceLink(entityReferenceLink, true);
			this.WriteEntityReferenceLinkImplementation(entityReferenceLink);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002CD44 File Offset: 0x0002AF44
		public sealed override Task WriteEntityReferenceLinkAsync(ODataEntityReferenceLink entityReferenceLink)
		{
			this.VerifyCanWriteEntityReferenceLink(entityReferenceLink, false);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteEntityReferenceLinkImplementation(entityReferenceLink);
			});
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002CD84 File Offset: 0x0002AF84
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.VerifyNotDisposed();
			if (this.State == ODataWriterCore.WriterState.Completed)
			{
				throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromCompleted(this.State.ToString(), ODataWriterCore.WriterState.Error.ToString()));
			}
			this.StartPayloadInStartState();
			this.EnterScope(ODataWriterCore.WriterState.Error, this.CurrentScope.Item);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002CDDE File Offset: 0x0002AFDE
		protected static bool IsErrorState(ODataWriterCore.WriterState state)
		{
			return state == ODataWriterCore.WriterState.Error;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002CDE4 File Offset: 0x0002AFE4
		protected static ProjectedPropertiesAnnotation GetProjectedPropertiesAnnotation(ODataWriterCore.Scope currentScope)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataWriterCore.Scope>(currentScope, "currentScope");
			ODataItem item = currentScope.Item;
			if (item != null)
			{
				return item.GetAnnotation<ProjectedPropertiesAnnotation>();
			}
			return null;
		}

		// Token: 0x06000CEF RID: 3311
		protected abstract void VerifyNotDisposed();

		// Token: 0x06000CF0 RID: 3312
		protected abstract void FlushSynchronously();

		// Token: 0x06000CF1 RID: 3313
		protected abstract Task FlushAsynchronously();

		// Token: 0x06000CF2 RID: 3314
		protected abstract void StartPayload();

		// Token: 0x06000CF3 RID: 3315
		protected abstract void StartEntry(ODataEntry entry);

		// Token: 0x06000CF4 RID: 3316
		protected abstract void EndEntry(ODataEntry entry);

		// Token: 0x06000CF5 RID: 3317
		protected abstract void StartFeed(ODataFeed feed);

		// Token: 0x06000CF6 RID: 3318
		protected abstract void EndPayload();

		// Token: 0x06000CF7 RID: 3319
		protected abstract void EndFeed(ODataFeed feed);

		// Token: 0x06000CF8 RID: 3320
		protected abstract void WriteDeferredNavigationLink(ODataNavigationLink navigationLink);

		// Token: 0x06000CF9 RID: 3321
		protected abstract void StartNavigationLinkWithContent(ODataNavigationLink navigationLink);

		// Token: 0x06000CFA RID: 3322
		protected abstract void EndNavigationLinkWithContent(ODataNavigationLink navigationLink);

		// Token: 0x06000CFB RID: 3323
		protected abstract void WriteEntityReferenceInNavigationLinkContent(ODataNavigationLink parentNavigationLink, ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x06000CFC RID: 3324
		protected abstract ODataWriterCore.FeedScope CreateFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties);

		// Token: 0x06000CFD RID: 3325
		protected abstract ODataWriterCore.EntryScope CreateEntryScope(ODataEntry entry, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties);

		// Token: 0x06000CFE RID: 3326 RVA: 0x0002CE10 File Offset: 0x0002B010
		protected ODataFeedAndEntrySerializationInfo GetEntrySerializationInfo(ODataEntry entry)
		{
			ODataFeedAndEntrySerializationInfo odataFeedAndEntrySerializationInfo = ((entry == null) ? null : entry.SerializationInfo);
			if (odataFeedAndEntrySerializationInfo != null)
			{
				return odataFeedAndEntrySerializationInfo;
			}
			ODataWriterCore.FeedScope feedScope = this.CurrentScope as ODataWriterCore.FeedScope;
			if (feedScope != null)
			{
				ODataFeed odataFeed = (ODataFeed)feedScope.Item;
				return odataFeed.SerializationInfo;
			}
			return null;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002CE52 File Offset: 0x0002B052
		protected virtual ODataWriterCore.NavigationLinkScope CreateNavigationLinkScope(ODataWriterCore.WriterState writerState, ODataNavigationLink navLink, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			return new ODataWriterCore.NavigationLinkScope(writerState, navLink, entitySet, entityType, skipWriting, selectedProperties);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0002CE62 File Offset: 0x0002B062
		protected virtual void PrepareEntryForWriteStart(ODataEntry entry, ODataFeedAndEntryTypeContext typeContext, SelectedPropertiesNode selectedProperties)
		{
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0002CE64 File Offset: 0x0002B064
		protected virtual void ValidateEntryMediaResource(ODataEntry entry, IEdmEntityType entityType)
		{
			bool flag = this.outputContext.UseDefaultFormatBehavior || this.outputContext.UseServerFormatBehavior;
			ValidationUtils.ValidateEntryMetadataResource(entry, entityType, this.outputContext.Model, flag);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0002CEA0 File Offset: 0x0002B0A0
		protected IEdmEntityType ValidateEntryType(ODataEntry entry)
		{
			if (entry.TypeName == null && this.CurrentScope.EntityType != null)
			{
				return this.CurrentScope.EntityType;
			}
			return (IEdmEntityType)TypeNameOracle.ResolveAndValidateTypeName(this.outputContext.Model, entry.TypeName, EdmTypeKind.Entity);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0002CEDF File Offset: 0x0002B0DF
		protected void ValidateNoDeltaLinkForExpandedFeed(ODataFeed feed)
		{
			if (feed.DeltaLink != null)
			{
				throw new ODataException(Strings.ODataWriterCore_DeltaLinkNotSupportedOnExpandedFeed);
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0002CEFA File Offset: 0x0002B0FA
		private void VerifyCanWriteStartFeed(bool synchronousCall, ODataFeed feed)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFeed>(feed, "feed");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			this.StartPayloadInStartState();
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0002CFAC File Offset: 0x0002B1AC
		private void WriteStartFeedImplementation(ODataFeed feed)
		{
			this.CheckForNavigationLinkWithContent(ODataPayloadKind.Feed);
			this.EnterScope(ODataWriterCore.WriterState.Feed, feed);
			if (!this.SkipWriting)
			{
				this.InterceptException(delegate
				{
					if (feed.Count != null)
					{
						if (!this.IsTopLevel)
						{
							throw new ODataException(Strings.ODataWriterCore_OnlyTopLevelFeedsSupportInlineCount);
						}
						if (!this.outputContext.WritingResponse)
						{
							this.ThrowODataException(Strings.ODataWriterCore_InlineCountInRequest, feed);
						}
						ODataVersionChecker.CheckCount(this.outputContext.Version);
					}
					this.StartFeed(feed);
				});
			}
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0002D002 File Offset: 0x0002B202
		private void VerifyCanWriteStartEntry(bool synchronousCall, ODataEntry entry)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State != ODataWriterCore.WriterState.NavigationLink)
			{
				ExceptionUtils.CheckArgumentNotNull<ODataEntry>(entry, "entry");
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0002D130 File Offset: 0x0002B330
		private void WriteStartEntryImplementation(ODataEntry entry)
		{
			this.StartPayloadInStartState();
			this.CheckForNavigationLinkWithContent(ODataPayloadKind.Entry);
			this.EnterScope(ODataWriterCore.WriterState.Entry, entry);
			if (!this.SkipWriting)
			{
				this.IncreaseEntryDepth();
				this.InterceptException(delegate
				{
					if (entry != null)
					{
						ODataWriterCore.EntryScope entryScope = (ODataWriterCore.EntryScope)this.CurrentScope;
						IEdmEntityType edmEntityType = this.ValidateEntryType(entry);
						entryScope.EntityTypeFromMetadata = entryScope.EntityType;
						ODataWriterCore.NavigationLinkScope parentNavigationLinkScope = this.ParentNavigationLinkScope;
						if (parentNavigationLinkScope != null)
						{
							WriterValidationUtils.ValidateEntryInExpandedLink(edmEntityType, parentNavigationLinkScope.EntityType);
							entryScope.EntityTypeFromMetadata = parentNavigationLinkScope.EntityType;
						}
						else if (this.CurrentFeedValidator != null)
						{
							this.CurrentFeedValidator.ValidateEntry(edmEntityType);
						}
						entryScope.EntityType = edmEntityType;
						this.PrepareEntryForWriteStart(entry, entryScope.GetOrCreateTypeContext(this.outputContext.Model, this.outputContext.WritingResponse), entryScope.SelectedProperties);
						this.ValidateEntryMediaResource(entry, edmEntityType);
						WriterValidationUtils.ValidateEntryAtStart(entry);
					}
					this.StartEntry(entry);
				});
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002D192 File Offset: 0x0002B392
		private void VerifyCanWriteStartNavigationLink(bool synchronousCall, ODataNavigationLink navigationLink)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataNavigationLink>(navigationLink, "navigationLink");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0002D1AC File Offset: 0x0002B3AC
		private void WriteStartNavigationLinkImplementation(ODataNavigationLink navigationLink)
		{
			this.EnterScope(ODataWriterCore.WriterState.NavigationLink, navigationLink);
			ODataEntry odataEntry = (ODataEntry)this.scopes.Parent.Item;
			if (odataEntry.MetadataBuilder != null)
			{
				navigationLink.SetMetadataBuilder(odataEntry.MetadataBuilder);
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0002D1EB File Offset: 0x0002B3EB
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0002D360 File Offset: 0x0002B560
		private void WriteEndImplementation()
		{
			this.InterceptException(delegate
			{
				ODataWriterCore.Scope currentScope = this.CurrentScope;
				switch (currentScope.State)
				{
				case ODataWriterCore.WriterState.Start:
				case ODataWriterCore.WriterState.Completed:
				case ODataWriterCore.WriterState.Error:
					throw new ODataException(Strings.ODataWriterCore_WriteEndCalledInInvalidState(currentScope.State.ToString()));
				case ODataWriterCore.WriterState.Entry:
					if (!this.SkipWriting)
					{
						ODataEntry odataEntry = (ODataEntry)currentScope.Item;
						if (odataEntry != null)
						{
							WriterValidationUtils.ValidateEntryAtEnd(odataEntry);
						}
						this.EndEntry(odataEntry);
						this.DecreaseEntryDepth();
					}
					break;
				case ODataWriterCore.WriterState.Feed:
					if (!this.SkipWriting)
					{
						ODataFeed odataFeed = (ODataFeed)currentScope.Item;
						WriterValidationUtils.ValidateFeedAtEnd(odataFeed, !this.outputContext.WritingResponse, this.outputContext.Version);
						this.EndFeed(odataFeed);
					}
					break;
				case ODataWriterCore.WriterState.NavigationLink:
					if (!this.outputContext.WritingResponse)
					{
						throw new ODataException(Strings.ODataWriterCore_DeferredLinkInRequest);
					}
					if (!this.SkipWriting)
					{
						ODataNavigationLink odataNavigationLink = (ODataNavigationLink)currentScope.Item;
						this.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataNavigationLink, false, odataNavigationLink.IsCollection);
						this.WriteDeferredNavigationLink(odataNavigationLink);
						this.MarkNavigationLinkAsProcessed(odataNavigationLink);
					}
					break;
				case ODataWriterCore.WriterState.NavigationLinkWithContent:
					if (!this.SkipWriting)
					{
						ODataNavigationLink odataNavigationLink2 = (ODataNavigationLink)currentScope.Item;
						this.EndNavigationLinkWithContent(odataNavigationLink2);
						this.MarkNavigationLinkAsProcessed(odataNavigationLink2);
					}
					break;
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_WriteEnd_UnreachableCodePath));
				}
				this.LeaveScope();
			});
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002D374 File Offset: 0x0002B574
		private void MarkNavigationLinkAsProcessed(ODataNavigationLink link)
		{
			ODataEntry odataEntry = (ODataEntry)this.scopes.Parent.Item;
			odataEntry.MetadataBuilder.MarkNavigationLinkProcessed(link.Name);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002D3A8 File Offset: 0x0002B5A8
		private void VerifyCanWriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink, bool synchronousCall)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(entityReferenceLink, "entityReferenceLink");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002D400 File Offset: 0x0002B600
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink entityReferenceLink)
		{
			if (this.outputContext.WritingResponse)
			{
				this.ThrowODataException(Strings.ODataWriterCore_EntityReferenceLinkInResponse, null);
			}
			this.CheckForNavigationLinkWithContent(ODataPayloadKind.EntityReferenceLink);
			if (!this.SkipWriting)
			{
				this.InterceptException(delegate
				{
					WriterValidationUtils.ValidateEntityReferenceLink(entityReferenceLink);
					this.WriteEntityReferenceInNavigationLinkContent((ODataNavigationLink)this.CurrentScope.Item, entityReferenceLink);
				});
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002D462 File Offset: 0x0002B662
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0002D471 File Offset: 0x0002B671
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.outputContext.Synchronous)
				{
					throw new ODataException(Strings.ODataWriterCore_SyncCallOnAsyncWriter);
				}
			}
			else if (this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataWriterCore_AsyncCallOnSyncWriter);
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002D4A6 File Offset: 0x0002B6A6
		private void ThrowODataException(string errorMessage, ODataItem item)
		{
			this.EnterScope(ODataWriterCore.WriterState.Error, item);
			throw new ODataException(errorMessage);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002D4B6 File Offset: 0x0002B6B6
		private void StartPayloadInStartState()
		{
			if (this.State == ODataWriterCore.WriterState.Start)
			{
				this.InterceptException(new Action(this.StartPayload));
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002D5EC File Offset: 0x0002B7EC
		private void CheckForNavigationLinkWithContent(ODataPayloadKind contentPayloadKind)
		{
			ODataWriterCore.Scope currentScope = this.CurrentScope;
			if (currentScope.State == ODataWriterCore.WriterState.NavigationLink || currentScope.State == ODataWriterCore.WriterState.NavigationLinkWithContent)
			{
				ODataNavigationLink currentNavigationLink = (ODataNavigationLink)currentScope.Item;
				this.InterceptException(delegate
				{
					IEdmNavigationProperty edmNavigationProperty = WriterValidationUtils.ValidateNavigationLink(currentNavigationLink, this.ParentEntryEntityType, new ODataPayloadKind?(contentPayloadKind), this.outputContext.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
					if (edmNavigationProperty != null)
					{
						this.CurrentScope.EntityType = edmNavigationProperty.ToEntityType();
						IEdmEntitySet parentEntryEntitySet = this.ParentEntryEntitySet;
						this.CurrentScope.EntitySet = ((parentEntryEntitySet == null) ? null : parentEntryEntitySet.FindNavigationTarget(edmNavigationProperty));
					}
				});
				if (currentScope.State == ODataWriterCore.WriterState.NavigationLinkWithContent)
				{
					if (this.outputContext.WritingResponse || currentNavigationLink.IsCollection != true)
					{
						this.ThrowODataException(Strings.ODataWriterCore_MultipleItemsInNavigationLinkContent, currentNavigationLink);
						return;
					}
				}
				else
				{
					this.PromoteNavigationLinkScope();
					if (!this.SkipWriting)
					{
						this.InterceptException(delegate
						{
							this.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(currentNavigationLink, contentPayloadKind != ODataPayloadKind.EntityReferenceLink, new bool?(contentPayloadKind == ODataPayloadKind.Feed));
							this.StartNavigationLinkWithContent(currentNavigationLink);
						});
						return;
					}
				}
			}
			else if (contentPayloadKind == ODataPayloadKind.EntityReferenceLink)
			{
				this.ThrowODataException(Strings.ODataWriterCore_EntityReferenceLinkWithoutNavigationLink, null);
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0002D6E4 File Offset: 0x0002B8E4
		private void InterceptException(Action action)
		{
			try
			{
				action();
			}
			catch
			{
				if (!ODataWriterCore.IsErrorState(this.State))
				{
					this.EnterScope(ODataWriterCore.WriterState.Error, this.CurrentScope.Item);
				}
				throw;
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0002D72C File Offset: 0x0002B92C
		private void IncreaseEntryDepth()
		{
			this.currentEntryDepth++;
			if (this.currentEntryDepth > this.outputContext.MessageWriterSettings.MessageQuotas.MaxNestingDepth)
			{
				this.ThrowODataException(Strings.ValidationUtils_MaxDepthOfNestedEntriesExceeded(this.outputContext.MessageWriterSettings.MessageQuotas.MaxNestingDepth), null);
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0002D78A File Offset: 0x0002B98A
		private void DecreaseEntryDepth()
		{
			this.currentEntryDepth--;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0002D7B8 File Offset: 0x0002B9B8
		private void EnterScope(ODataWriterCore.WriterState newState, ODataItem item)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			bool flag = this.SkipWriting;
			ODataWriterCore.Scope currentScope = this.CurrentScope;
			IEdmEntitySet edmEntitySet = null;
			IEdmEntityType edmEntityType = null;
			SelectedPropertiesNode selectedPropertiesNode = currentScope.SelectedProperties;
			if (newState == ODataWriterCore.WriterState.Entry || newState == ODataWriterCore.WriterState.Feed)
			{
				edmEntitySet = currentScope.EntitySet;
				edmEntityType = currentScope.EntityType;
			}
			ODataWriterCore.WriterState state = currentScope.State;
			if (state == ODataWriterCore.WriterState.Entry && newState == ODataWriterCore.WriterState.NavigationLink)
			{
				ODataNavigationLink odataNavigationLink = (ODataNavigationLink)item;
				if (!flag)
				{
					ProjectedPropertiesAnnotation projectedPropertiesAnnotation = ODataWriterCore.GetProjectedPropertiesAnnotation(currentScope);
					flag = projectedPropertiesAnnotation.ShouldSkipProperty(odataNavigationLink.Name);
					selectedPropertiesNode = currentScope.SelectedProperties.GetSelectedPropertiesForNavigationProperty(currentScope.EntityType, odataNavigationLink.Name);
					if (this.outputContext.WritingResponse)
					{
						IEdmEntityType entityType = currentScope.EntityType;
						IEdmNavigationProperty edmNavigationProperty = WriterValidationUtils.ValidateNavigationLink(odataNavigationLink, entityType, null, this.outputContext.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
						if (edmNavigationProperty != null)
						{
							edmEntityType = edmNavigationProperty.ToEntityType();
							IEdmEntitySet entitySet = currentScope.EntitySet;
							edmEntitySet = ((entitySet == null) ? null : entitySet.FindNavigationTarget(edmNavigationProperty));
						}
					}
				}
			}
			else if (newState == ODataWriterCore.WriterState.Entry && state == ODataWriterCore.WriterState.Feed)
			{
				((ODataWriterCore.FeedScope)currentScope).EntryCount++;
			}
			this.PushScope(newState, item, edmEntitySet, edmEntityType, flag, selectedPropertiesNode);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0002D920 File Offset: 0x0002BB20
		private void LeaveScope()
		{
			this.scopes.Pop();
			if (this.scopes.Count == 1)
			{
				ODataWriterCore.Scope scope = this.scopes.Pop();
				this.PushScope(ODataWriterCore.WriterState.Completed, null, scope.EntitySet, scope.EntityType, false, scope.SelectedProperties);
				this.InterceptException(new Action(this.EndPayload));
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002D984 File Offset: 0x0002BB84
		private void PromoteNavigationLinkScope()
		{
			this.ValidateTransition(ODataWriterCore.WriterState.NavigationLinkWithContent);
			ODataWriterCore.NavigationLinkScope navigationLinkScope = (ODataWriterCore.NavigationLinkScope)this.scopes.Pop();
			ODataWriterCore.NavigationLinkScope navigationLinkScope2 = navigationLinkScope.Clone(ODataWriterCore.WriterState.NavigationLinkWithContent);
			this.scopes.Push(navigationLinkScope2);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002D9C0 File Offset: 0x0002BBC0
		private void ValidateTransition(ODataWriterCore.WriterState newState)
		{
			if (!ODataWriterCore.IsErrorState(this.State) && ODataWriterCore.IsErrorState(newState))
			{
				return;
			}
			switch (this.State)
			{
			case ODataWriterCore.WriterState.Start:
				if (newState != ODataWriterCore.WriterState.Feed && newState != ODataWriterCore.WriterState.Entry)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromStart(this.State.ToString(), newState.ToString()));
				}
				if (newState == ODataWriterCore.WriterState.Feed && !this.writingFeed)
				{
					throw new ODataException(Strings.ODataWriterCore_CannotWriteTopLevelFeedWithEntryWriter);
				}
				if (newState == ODataWriterCore.WriterState.Entry && this.writingFeed)
				{
					throw new ODataException(Strings.ODataWriterCore_CannotWriteTopLevelEntryWithFeedWriter);
				}
				break;
			case ODataWriterCore.WriterState.Entry:
				if (this.CurrentScope.Item == null)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromNullEntry(this.State.ToString(), newState.ToString()));
				}
				if (newState != ODataWriterCore.WriterState.NavigationLink)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromEntry(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataWriterCore.WriterState.Feed:
				if (newState != ODataWriterCore.WriterState.Entry)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromFeed(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataWriterCore.WriterState.NavigationLink:
				if (newState != ODataWriterCore.WriterState.NavigationLinkWithContent)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidStateTransition(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataWriterCore.WriterState.NavigationLinkWithContent:
				if (newState != ODataWriterCore.WriterState.Feed && newState != ODataWriterCore.WriterState.Entry)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromExpandedLink(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataWriterCore.WriterState.Completed:
				throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromCompleted(this.State.ToString(), newState.ToString()));
			case ODataWriterCore.WriterState.Error:
				if (newState != ODataWriterCore.WriterState.Error)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromError(this.State.ToString(), newState.ToString()));
				}
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_ValidateTransition_UnreachableCodePath));
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0002DBC4 File Offset: 0x0002BDC4
		private void PushScope(ODataWriterCore.WriterState state, ODataItem item, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			ODataWriterCore.Scope scope;
			switch (state)
			{
			case ODataWriterCore.WriterState.Start:
			case ODataWriterCore.WriterState.Completed:
			case ODataWriterCore.WriterState.Error:
				scope = new ODataWriterCore.Scope(state, item, entitySet, entityType, skipWriting, selectedProperties);
				break;
			case ODataWriterCore.WriterState.Entry:
				scope = this.CreateEntryScope((ODataEntry)item, entitySet, entityType, skipWriting, selectedProperties);
				break;
			case ODataWriterCore.WriterState.Feed:
				scope = this.CreateFeedScope((ODataFeed)item, entitySet, entityType, skipWriting, selectedProperties);
				break;
			case ODataWriterCore.WriterState.NavigationLink:
			case ODataWriterCore.WriterState.NavigationLinkWithContent:
				scope = this.CreateNavigationLinkScope(state, (ODataNavigationLink)item, entitySet, entityType, skipWriting, selectedProperties);
				break;
			default:
			{
				string text = Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_Scope_Create_UnreachableCodePath);
				throw new ODataException(text);
			}
			}
			this.scopes.Push(scope);
		}

		// Token: 0x04000452 RID: 1106
		private readonly ODataOutputContext outputContext;

		// Token: 0x04000453 RID: 1107
		private readonly bool writingFeed;

		// Token: 0x04000454 RID: 1108
		private readonly ODataWriterCore.ScopeStack scopes = new ODataWriterCore.ScopeStack();

		// Token: 0x04000455 RID: 1109
		private readonly FeedWithoutExpectedTypeValidator feedValidator;

		// Token: 0x04000456 RID: 1110
		private int currentEntryDepth;

		// Token: 0x020001A6 RID: 422
		internal enum WriterState
		{
			// Token: 0x04000458 RID: 1112
			Start,
			// Token: 0x04000459 RID: 1113
			Entry,
			// Token: 0x0400045A RID: 1114
			Feed,
			// Token: 0x0400045B RID: 1115
			NavigationLink,
			// Token: 0x0400045C RID: 1116
			NavigationLinkWithContent,
			// Token: 0x0400045D RID: 1117
			Completed,
			// Token: 0x0400045E RID: 1118
			Error
		}

		// Token: 0x020001A7 RID: 423
		internal sealed class ScopeStack
		{
			// Token: 0x06000D1F RID: 3359 RVA: 0x0002DC6A File Offset: 0x0002BE6A
			internal ScopeStack()
			{
			}

			// Token: 0x170002CD RID: 717
			// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0002DC7D File Offset: 0x0002BE7D
			internal int Count
			{
				get
				{
					return this.scopes.Count;
				}
			}

			// Token: 0x170002CE RID: 718
			// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0002DC8C File Offset: 0x0002BE8C
			internal ODataWriterCore.Scope Parent
			{
				get
				{
					ODataWriterCore.Scope scope = this.scopes.Pop();
					ODataWriterCore.Scope scope2 = this.scopes.Peek();
					this.scopes.Push(scope);
					return scope2;
				}
			}

			// Token: 0x170002CF RID: 719
			// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0002DCC0 File Offset: 0x0002BEC0
			internal ODataWriterCore.Scope ParentOfParent
			{
				get
				{
					ODataWriterCore.Scope scope = this.scopes.Pop();
					ODataWriterCore.Scope scope2 = this.scopes.Pop();
					ODataWriterCore.Scope scope3 = this.scopes.Peek();
					this.scopes.Push(scope2);
					this.scopes.Push(scope);
					return scope3;
				}
			}

			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0002DD0A File Offset: 0x0002BF0A
			internal ODataWriterCore.Scope ParentOrNull
			{
				get
				{
					if (this.Count != 0)
					{
						return this.Parent;
					}
					return null;
				}
			}

			// Token: 0x06000D24 RID: 3364 RVA: 0x0002DD1C File Offset: 0x0002BF1C
			internal void Push(ODataWriterCore.Scope scope)
			{
				this.scopes.Push(scope);
			}

			// Token: 0x06000D25 RID: 3365 RVA: 0x0002DD2A File Offset: 0x0002BF2A
			internal ODataWriterCore.Scope Pop()
			{
				return this.scopes.Pop();
			}

			// Token: 0x06000D26 RID: 3366 RVA: 0x0002DD37 File Offset: 0x0002BF37
			internal ODataWriterCore.Scope Peek()
			{
				return this.scopes.Peek();
			}

			// Token: 0x0400045F RID: 1119
			private readonly Stack<ODataWriterCore.Scope> scopes = new Stack<ODataWriterCore.Scope>();
		}

		// Token: 0x020001A8 RID: 424
		internal class Scope
		{
			// Token: 0x06000D27 RID: 3367 RVA: 0x0002DD44 File Offset: 0x0002BF44
			internal Scope(ODataWriterCore.WriterState state, ODataItem item, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
			{
				this.state = state;
				this.item = item;
				this.entityType = entityType;
				this.entitySet = entitySet;
				this.skipWriting = skipWriting;
				this.selectedProperties = selectedProperties;
			}

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0002DD79 File Offset: 0x0002BF79
			// (set) Token: 0x06000D29 RID: 3369 RVA: 0x0002DD81 File Offset: 0x0002BF81
			public IEdmEntityType EntityType
			{
				get
				{
					return this.entityType;
				}
				set
				{
					this.entityType = value;
				}
			}

			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0002DD8A File Offset: 0x0002BF8A
			internal ODataWriterCore.WriterState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0002DD92 File Offset: 0x0002BF92
			internal ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0002DD9A File Offset: 0x0002BF9A
			// (set) Token: 0x06000D2D RID: 3373 RVA: 0x0002DDA2 File Offset: 0x0002BFA2
			internal IEdmEntitySet EntitySet
			{
				get
				{
					return this.entitySet;
				}
				set
				{
					this.entitySet = value;
				}
			}

			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0002DDAB File Offset: 0x0002BFAB
			internal SelectedPropertiesNode SelectedProperties
			{
				get
				{
					return this.selectedProperties;
				}
			}

			// Token: 0x170002D6 RID: 726
			// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0002DDB3 File Offset: 0x0002BFB3
			internal bool SkipWriting
			{
				get
				{
					return this.skipWriting;
				}
			}

			// Token: 0x04000460 RID: 1120
			private readonly ODataWriterCore.WriterState state;

			// Token: 0x04000461 RID: 1121
			private readonly ODataItem item;

			// Token: 0x04000462 RID: 1122
			private readonly bool skipWriting;

			// Token: 0x04000463 RID: 1123
			private readonly SelectedPropertiesNode selectedProperties;

			// Token: 0x04000464 RID: 1124
			private IEdmEntitySet entitySet;

			// Token: 0x04000465 RID: 1125
			private IEdmEntityType entityType;
		}

		// Token: 0x020001A9 RID: 425
		internal abstract class FeedScope : ODataWriterCore.Scope
		{
			// Token: 0x06000D30 RID: 3376 RVA: 0x0002DDBB File Offset: 0x0002BFBB
			internal FeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
				: base(ODataWriterCore.WriterState.Feed, feed, entitySet, entityType, skipWriting, selectedProperties)
			{
				this.serializationInfo = feed.SerializationInfo;
			}

			// Token: 0x170002D7 RID: 727
			// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0002DDD7 File Offset: 0x0002BFD7
			// (set) Token: 0x06000D32 RID: 3378 RVA: 0x0002DDDF File Offset: 0x0002BFDF
			internal int EntryCount
			{
				get
				{
					return this.entryCount;
				}
				set
				{
					this.entryCount = value;
				}
			}

			// Token: 0x170002D8 RID: 728
			// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0002DDE8 File Offset: 0x0002BFE8
			internal InstanceAnnotationWriteTracker InstanceAnnotationWriteTracker
			{
				get
				{
					if (this.instanceAnnotationWriteTracker == null)
					{
						this.instanceAnnotationWriteTracker = new InstanceAnnotationWriteTracker();
					}
					return this.instanceAnnotationWriteTracker;
				}
			}

			// Token: 0x06000D34 RID: 3380 RVA: 0x0002DE03 File Offset: 0x0002C003
			internal ODataFeedAndEntryTypeContext GetOrCreateTypeContext(IEdmModel model, bool writingResponse)
			{
				if (this.typeContext == null)
				{
					this.typeContext = ODataFeedAndEntryTypeContext.Create(this.serializationInfo, base.EntitySet, EdmTypeWriterResolver.Instance.GetElementType(base.EntitySet), base.EntityType, model, writingResponse);
				}
				return this.typeContext;
			}

			// Token: 0x04000466 RID: 1126
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;

			// Token: 0x04000467 RID: 1127
			private int entryCount;

			// Token: 0x04000468 RID: 1128
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;

			// Token: 0x04000469 RID: 1129
			private ODataFeedAndEntryTypeContext typeContext;
		}

		// Token: 0x020001AA RID: 426
		internal class EntryScope : ODataWriterCore.Scope
		{
			// Token: 0x06000D35 RID: 3381 RVA: 0x0002DE42 File Offset: 0x0002C042
			internal EntryScope(ODataEntry entry, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, bool writingResponse, ODataWriterBehavior writerBehavior, SelectedPropertiesNode selectedProperties)
				: base(ODataWriterCore.WriterState.Entry, entry, entitySet, entityType, skipWriting, selectedProperties)
			{
				if (entry != null)
				{
					this.duplicatePropertyNamesChecker = new DuplicatePropertyNamesChecker(writerBehavior.AllowDuplicatePropertyNames, writingResponse);
					this.odataEntryTypeName = entry.TypeName;
				}
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x170002D9 RID: 729
			// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0002DE7D File Offset: 0x0002C07D
			// (set) Token: 0x06000D37 RID: 3383 RVA: 0x0002DE85 File Offset: 0x0002C085
			public IEdmEntityType EntityTypeFromMetadata
			{
				get
				{
					return this.entityTypeFromMetadata;
				}
				internal set
				{
					this.entityTypeFromMetadata = value;
				}
			}

			// Token: 0x170002DA RID: 730
			// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0002DE8E File Offset: 0x0002C08E
			public ODataFeedAndEntrySerializationInfo SerializationInfo
			{
				get
				{
					return this.serializationInfo;
				}
			}

			// Token: 0x170002DB RID: 731
			// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0002DE96 File Offset: 0x0002C096
			internal DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker
			{
				get
				{
					return this.duplicatePropertyNamesChecker;
				}
			}

			// Token: 0x170002DC RID: 732
			// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0002DE9E File Offset: 0x0002C09E
			internal InstanceAnnotationWriteTracker InstanceAnnotationWriteTracker
			{
				get
				{
					if (this.instanceAnnotationWriteTracker == null)
					{
						this.instanceAnnotationWriteTracker = new InstanceAnnotationWriteTracker();
					}
					return this.instanceAnnotationWriteTracker;
				}
			}

			// Token: 0x06000D3B RID: 3387 RVA: 0x0002DEB9 File Offset: 0x0002C0B9
			public ODataFeedAndEntryTypeContext GetOrCreateTypeContext(IEdmModel model, bool writingResponse)
			{
				if (this.typeContext == null)
				{
					this.typeContext = ODataFeedAndEntryTypeContext.Create(this.serializationInfo, base.EntitySet, EdmTypeWriterResolver.Instance.GetElementType(base.EntitySet), this.EntityTypeFromMetadata, model, writingResponse);
				}
				return this.typeContext;
			}

			// Token: 0x0400046A RID: 1130
			private readonly DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;

			// Token: 0x0400046B RID: 1131
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;

			// Token: 0x0400046C RID: 1132
			private readonly string odataEntryTypeName;

			// Token: 0x0400046D RID: 1133
			private IEdmEntityType entityTypeFromMetadata;

			// Token: 0x0400046E RID: 1134
			private ODataFeedAndEntryTypeContext typeContext;

			// Token: 0x0400046F RID: 1135
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;
		}

		// Token: 0x020001AB RID: 427
		internal class NavigationLinkScope : ODataWriterCore.Scope
		{
			// Token: 0x06000D3C RID: 3388 RVA: 0x0002DEF8 File Offset: 0x0002C0F8
			internal NavigationLinkScope(ODataWriterCore.WriterState writerState, ODataNavigationLink navLink, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
				: base(writerState, navLink, entitySet, entityType, skipWriting, selectedProperties)
			{
			}

			// Token: 0x06000D3D RID: 3389 RVA: 0x0002DF09 File Offset: 0x0002C109
			internal virtual ODataWriterCore.NavigationLinkScope Clone(ODataWriterCore.WriterState newWriterState)
			{
				return new ODataWriterCore.NavigationLinkScope(newWriterState, (ODataNavigationLink)base.Item, base.EntitySet, base.EntityType, base.SkipWriting, base.SelectedProperties);
			}
		}
	}
}
