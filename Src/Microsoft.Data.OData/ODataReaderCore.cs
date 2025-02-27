﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x0200015A RID: 346
	internal abstract class ODataReaderCore : ODataReader
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		protected ODataReaderCore(ODataInputContext inputContext, bool readingFeed, IODataReaderWriterListener listener)
		{
			this.inputContext = inputContext;
			this.readingFeed = readingFeed;
			this.listener = listener;
			this.currentEntryDepth = 0;
			if (this.readingFeed && this.inputContext.Model.IsUserModel())
			{
				this.feedValidator = new FeedWithoutExpectedTypeValidator();
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0001D407 File Offset: 0x0001B607
		public sealed override ODataReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x0001D424 File Offset: 0x0001B624
		public sealed override ODataItem Item
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Item;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0001D441 File Offset: 0x0001B641
		protected ODataEntry CurrentEntry
		{
			get
			{
				return (ODataEntry)this.Item;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0001D44E File Offset: 0x0001B64E
		protected ODataFeed CurrentFeed
		{
			get
			{
				return (ODataFeed)this.Item;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0001D45B File Offset: 0x0001B65B
		protected ODataNavigationLink CurrentNavigationLink
		{
			get
			{
				return (ODataNavigationLink)this.Item;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0001D468 File Offset: 0x0001B668
		protected ODataEntityReferenceLink CurrentEntityReferenceLink
		{
			get
			{
				return (ODataEntityReferenceLink)this.Item;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0001D478 File Offset: 0x0001B678
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x0001D497 File Offset: 0x0001B697
		protected IEdmEntityType CurrentEntityType
		{
			get
			{
				return this.scopes.Peek().EntityType;
			}
			set
			{
				this.scopes.Peek().EntityType = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0001D4AC File Offset: 0x0001B6AC
		protected IEdmEntitySet CurrentEntitySet
		{
			get
			{
				return this.scopes.Peek().EntitySet;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0001D4CB File Offset: 0x0001B6CB
		protected ODataReaderCore.Scope CurrentScope
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
		protected ODataReaderCore.Scope LinkParentEntityScope
		{
			get
			{
				return this.scopes.Skip(1).First<ODataReaderCore.Scope>();
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0001D4EB File Offset: 0x0001B6EB
		protected bool IsTopLevel
		{
			get
			{
				return this.scopes.Count <= 2;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0001D500 File Offset: 0x0001B700
		protected ODataReaderCore.Scope ExpandedLinkContentParentScope
		{
			get
			{
				ODataReaderCore.Scope scope = this.scopes.Skip(1).First<ODataReaderCore.Scope>();
				if (scope.State == ODataReaderState.NavigationLinkStart)
				{
					return scope;
				}
				return null;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0001D52B File Offset: 0x0001B72B
		protected bool IsExpandedLinkContent
		{
			get
			{
				return this.ExpandedLinkContentParentScope != null;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0001D539 File Offset: 0x0001B739
		protected bool ReadingFeed
		{
			get
			{
				return this.readingFeed;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001D541 File Offset: 0x0001B741
		protected bool IsReadingNestedPayload
		{
			get
			{
				return this.listener != null;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0001D54F File Offset: 0x0001B74F
		protected FeedWithoutExpectedTypeValidator CurrentFeedValidator
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

		// Token: 0x06000964 RID: 2404 RVA: 0x0001D567 File Offset: 0x0001B767
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0001D593 File Offset: 0x0001B793
		public sealed override Task<bool> ReadAsync()
		{
			this.VerifyCanRead(false);
			return this.ReadAsynchronously().FollowOnFaultWith(delegate(Task<bool> t)
			{
				this.EnterScope(new ODataReaderCore.Scope(ODataReaderState.Exception, null, null, null));
			});
		}

		// Token: 0x06000966 RID: 2406
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x06000967 RID: 2407
		protected abstract bool ReadAtFeedStartImplementation();

		// Token: 0x06000968 RID: 2408
		protected abstract bool ReadAtFeedEndImplementation();

		// Token: 0x06000969 RID: 2409
		protected abstract bool ReadAtEntryStartImplementation();

		// Token: 0x0600096A RID: 2410
		protected abstract bool ReadAtEntryEndImplementation();

		// Token: 0x0600096B RID: 2411
		protected abstract bool ReadAtNavigationLinkStartImplementation();

		// Token: 0x0600096C RID: 2412
		protected abstract bool ReadAtNavigationLinkEndImplementation();

		// Token: 0x0600096D RID: 2413
		protected abstract bool ReadAtEntityReferenceLink();

		// Token: 0x0600096E RID: 2414 RVA: 0x0001D5B3 File Offset: 0x0001B7B3
		protected void EnterScope(ODataReaderCore.Scope scope)
		{
			this.scopes.Push(scope);
			if (this.listener != null)
			{
				if (scope.State == ODataReaderState.Exception)
				{
					this.listener.OnException();
					return;
				}
				if (scope.State == ODataReaderState.Completed)
				{
					this.listener.OnCompleted();
				}
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001D5F3 File Offset: 0x0001B7F3
		protected void ReplaceScope(ODataReaderCore.Scope scope)
		{
			this.scopes.Pop();
			this.EnterScope(scope);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001D608 File Offset: 0x0001B808
		protected void PopScope(ODataReaderState state)
		{
			this.scopes.Pop();
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001D616 File Offset: 0x0001B816
		protected void EndEntry(ODataReaderCore.Scope scope)
		{
			this.scopes.Pop();
			this.EnterScope(scope);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001D630 File Offset: 0x0001B830
		protected void ApplyEntityTypeNameFromPayload(string entityTypeNameFromPayload)
		{
			EdmTypeKind edmTypeKind;
			SerializationTypeNameAnnotation serializationTypeNameAnnotation;
			IEdmEntityTypeReference edmEntityTypeReference = (IEdmEntityTypeReference)ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.Entity, null, this.CurrentEntityType.ToTypeReference(), entityTypeNameFromPayload, this.inputContext.Model, this.inputContext.MessageReaderSettings, this.inputContext.Version, () => EdmTypeKind.Entity, out edmTypeKind, out serializationTypeNameAnnotation);
			IEdmEntityType edmEntityType = null;
			ODataEntry currentEntry = this.CurrentEntry;
			if (edmEntityTypeReference != null)
			{
				edmEntityType = edmEntityTypeReference.EntityDefinition();
				currentEntry.TypeName = edmEntityType.ODataFullName();
				if (serializationTypeNameAnnotation != null)
				{
					currentEntry.SetAnnotation<SerializationTypeNameAnnotation>(serializationTypeNameAnnotation);
				}
			}
			else if (entityTypeNameFromPayload != null)
			{
				currentEntry.TypeName = entityTypeNameFromPayload;
			}
			this.CurrentEntityType = edmEntityType;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001D6D8 File Offset: 0x0001B8D8
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001D6E0 File Offset: 0x0001B8E0
		protected virtual Task<bool> ReadAsynchronously()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadImplementation));
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001D6F4 File Offset: 0x0001B8F4
		protected void IncreaseEntryDepth()
		{
			this.currentEntryDepth++;
			if (this.currentEntryDepth > this.inputContext.MessageReaderSettings.MessageQuotas.MaxNestingDepth)
			{
				throw new ODataException(Strings.ValidationUtils_MaxDepthOfNestedEntriesExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxNestingDepth));
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001D751 File Offset: 0x0001B951
		protected void DecreaseEntryDepth()
		{
			this.currentEntryDepth--;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001D764 File Offset: 0x0001B964
		private bool ReadImplementation()
		{
			bool flag;
			switch (this.State)
			{
			case ODataReaderState.Start:
				flag = this.ReadAtStartImplementation();
				break;
			case ODataReaderState.FeedStart:
				flag = this.ReadAtFeedStartImplementation();
				break;
			case ODataReaderState.FeedEnd:
				flag = this.ReadAtFeedEndImplementation();
				break;
			case ODataReaderState.EntryStart:
				this.IncreaseEntryDepth();
				flag = this.ReadAtEntryStartImplementation();
				break;
			case ODataReaderState.EntryEnd:
				this.DecreaseEntryDepth();
				flag = this.ReadAtEntryEndImplementation();
				break;
			case ODataReaderState.NavigationLinkStart:
				flag = this.ReadAtNavigationLinkStartImplementation();
				break;
			case ODataReaderState.NavigationLinkEnd:
				flag = this.ReadAtNavigationLinkEndImplementation();
				break;
			case ODataReaderState.EntityReferenceLink:
				flag = this.ReadAtEntityReferenceLink();
				break;
			case ODataReaderState.Exception:
			case ODataReaderState.Completed:
				throw new ODataException(Strings.ODataReaderCore_NoReadCallsAllowed(this.State));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataReaderCore_ReadImplementation));
			}
			if ((this.State == ODataReaderState.EntryStart || this.State == ODataReaderState.EntryEnd) && this.Item != null)
			{
				ReaderValidationUtils.ValidateEntry(this.CurrentEntry);
			}
			return flag;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001D84C File Offset: 0x0001BA4C
		private T InterceptException<T>(Func<T> action)
		{
			T t;
			try
			{
				t = action();
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					this.EnterScope(new ODataReaderCore.Scope(ODataReaderState.Exception, null, null, null));
				}
				throw;
			}
			return t;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001D890 File Offset: 0x0001BA90
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataReaderState.Exception || this.State == ODataReaderState.Completed)
			{
				throw new ODataException(Strings.ODataReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001D8CD File Offset: 0x0001BACD
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.inputContext.Synchronous)
				{
					throw new ODataException(Strings.ODataReaderCore_SyncCallOnAsyncReader);
				}
			}
			else if (this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataReaderCore_AsyncCallOnSyncReader);
			}
		}

		// Token: 0x04000374 RID: 884
		private readonly ODataInputContext inputContext;

		// Token: 0x04000375 RID: 885
		private readonly bool readingFeed;

		// Token: 0x04000376 RID: 886
		private readonly Stack<ODataReaderCore.Scope> scopes = new Stack<ODataReaderCore.Scope>();

		// Token: 0x04000377 RID: 887
		private readonly IODataReaderWriterListener listener;

		// Token: 0x04000378 RID: 888
		private readonly FeedWithoutExpectedTypeValidator feedValidator;

		// Token: 0x04000379 RID: 889
		private int currentEntryDepth;

		// Token: 0x0200015B RID: 347
		protected internal class Scope
		{
			// Token: 0x0600097D RID: 2429 RVA: 0x0001D902 File Offset: 0x0001BB02
			internal Scope(ODataReaderState state, ODataItem item, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
			{
				this.state = state;
				this.item = item;
				this.EntityType = expectedEntityType;
				this.EntitySet = entitySet;
			}

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x0600097E RID: 2430 RVA: 0x0001D927 File Offset: 0x0001BB27
			internal ODataReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x0600097F RID: 2431 RVA: 0x0001D92F File Offset: 0x0001BB2F
			internal ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x1700024C RID: 588
			// (get) Token: 0x06000980 RID: 2432 RVA: 0x0001D937 File Offset: 0x0001BB37
			// (set) Token: 0x06000981 RID: 2433 RVA: 0x0001D93F File Offset: 0x0001BB3F
			internal IEdmEntitySet EntitySet { get; set; }

			// Token: 0x1700024D RID: 589
			// (get) Token: 0x06000982 RID: 2434 RVA: 0x0001D948 File Offset: 0x0001BB48
			// (set) Token: 0x06000983 RID: 2435 RVA: 0x0001D950 File Offset: 0x0001BB50
			internal IEdmEntityType EntityType { get; set; }

			// Token: 0x0400037B RID: 891
			private readonly ODataReaderState state;

			// Token: 0x0400037C RID: 892
			private readonly ODataItem item;
		}
	}
}
