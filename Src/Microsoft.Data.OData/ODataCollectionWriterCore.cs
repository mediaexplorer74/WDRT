using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x02000187 RID: 391
	internal abstract class ODataCollectionWriterCore : ODataCollectionWriter, IODataOutputInStreamErrorListener
	{
		// Token: 0x06000AFB RID: 2811 RVA: 0x000249FF File Offset: 0x00022BFF
		protected ODataCollectionWriterCore(ODataOutputContext outputContext, IEdmTypeReference itemTypeReference)
			: this(outputContext, itemTypeReference, null)
		{
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00024A0A File Offset: 0x00022C0A
		protected ODataCollectionWriterCore(ODataOutputContext outputContext, IEdmTypeReference expectedItemType, IODataReaderWriterListener listener)
		{
			this.outputContext = outputContext;
			this.expectedItemType = expectedItemType;
			this.listener = listener;
			this.scopes.Push(new ODataCollectionWriterCore.Scope(ODataCollectionWriterCore.CollectionWriterState.Start, null));
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00024A44 File Offset: 0x00022C44
		protected ODataCollectionWriterCore.CollectionWriterState State
		{
			get
			{
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00024A56 File Offset: 0x00022C56
		protected DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker
		{
			get
			{
				if (this.duplicatePropertyNamesChecker == null)
				{
					this.duplicatePropertyNamesChecker = new DuplicatePropertyNamesChecker(this.outputContext.MessageWriterSettings.WriterBehavior.AllowDuplicatePropertyNames, this.outputContext.WritingResponse);
				}
				return this.duplicatePropertyNamesChecker;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00024A91 File Offset: 0x00022C91
		protected CollectionWithoutExpectedTypeValidator CollectionValidator
		{
			get
			{
				return this.collectionValidator;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00024A99 File Offset: 0x00022C99
		protected IEdmTypeReference ItemTypeReference
		{
			get
			{
				return this.expectedItemType;
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00024AA4 File Offset: 0x00022CA4
		public sealed override void Flush()
		{
			this.VerifyCanFlush(true);
			try
			{
				this.FlushSynchronously();
			}
			catch
			{
				this.ReplaceScope(ODataCollectionWriterCore.CollectionWriterState.Error, null);
				throw;
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00024AE6 File Offset: 0x00022CE6
		public sealed override Task FlushAsync()
		{
			this.VerifyCanFlush(false);
			return this.FlushAsynchronously().FollowOnFaultWith(delegate(Task t)
			{
				this.ReplaceScope(ODataCollectionWriterCore.CollectionWriterState.Error, null);
			});
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00024B06 File Offset: 0x00022D06
		public sealed override void WriteStart(ODataCollectionStart collectionStart)
		{
			this.VerifyCanWriteStart(true, collectionStart);
			this.WriteStartImplementation(collectionStart);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00024B34 File Offset: 0x00022D34
		public sealed override Task WriteStartAsync(ODataCollectionStart collection)
		{
			this.VerifyCanWriteStart(false, collection);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStartImplementation(collection);
			});
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00024B73 File Offset: 0x00022D73
		public sealed override void WriteItem(object item)
		{
			this.VerifyCanWriteItem(true);
			this.WriteItemImplementation(item);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00024BA0 File Offset: 0x00022DA0
		public sealed override Task WriteItemAsync(object item)
		{
			this.VerifyCanWriteItem(false);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteItemImplementation(item);
			});
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00024BD9 File Offset: 0x00022DD9
		public sealed override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.WriteEndImplementation();
			if (this.scopes.Peek().State == ODataCollectionWriterCore.CollectionWriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00024C22 File Offset: 0x00022E22
		public sealed override Task WriteEndAsync()
		{
			this.VerifyCanWriteEnd(false);
			return TaskUtils.GetTaskForSynchronousOperation(new Action(this.WriteEndImplementation)).FollowOnSuccessWithTask(delegate(Task task)
			{
				if (this.scopes.Peek().State == ODataCollectionWriterCore.CollectionWriterState.Completed)
				{
					return this.FlushAsync();
				}
				return TaskUtils.CompletedTask;
			});
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00024C50 File Offset: 0x00022E50
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.VerifyNotDisposed();
			if (this.State == ODataCollectionWriterCore.CollectionWriterState.Completed)
			{
				throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromCompleted(this.State.ToString(), ODataCollectionWriterCore.CollectionWriterState.Error.ToString()));
			}
			this.StartPayloadInStartState();
			this.EnterScope(ODataCollectionWriterCore.CollectionWriterState.Error, this.scopes.Peek().Item);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00024CAF File Offset: 0x00022EAF
		protected static bool IsErrorState(ODataCollectionWriterCore.CollectionWriterState state)
		{
			return state == ODataCollectionWriterCore.CollectionWriterState.Error;
		}

		// Token: 0x06000B0B RID: 2827
		protected abstract void VerifyNotDisposed();

		// Token: 0x06000B0C RID: 2828
		protected abstract void FlushSynchronously();

		// Token: 0x06000B0D RID: 2829
		protected abstract Task FlushAsynchronously();

		// Token: 0x06000B0E RID: 2830
		protected abstract void StartPayload();

		// Token: 0x06000B0F RID: 2831
		protected abstract void EndPayload();

		// Token: 0x06000B10 RID: 2832
		protected abstract void StartCollection(ODataCollectionStart collectionStart);

		// Token: 0x06000B11 RID: 2833
		protected abstract void EndCollection();

		// Token: 0x06000B12 RID: 2834
		protected abstract void WriteCollectionItem(object item, IEdmTypeReference expectedItemTypeReference);

		// Token: 0x06000B13 RID: 2835 RVA: 0x00024CB8 File Offset: 0x00022EB8
		private void VerifyCanWriteStart(bool synchronousCall, ODataCollectionStart collectionStart)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataCollectionStart>(collectionStart, "collection");
			string name = collectionStart.Name;
			if (name != null && name.Length == 0)
			{
				throw new ODataException(Strings.ODataCollectionWriterCore_CollectionsMustNotHaveEmptyName);
			}
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00024D34 File Offset: 0x00022F34
		private void WriteStartImplementation(ODataCollectionStart collectionStart)
		{
			this.StartPayloadInStartState();
			this.EnterScope(ODataCollectionWriterCore.CollectionWriterState.Collection, collectionStart);
			this.InterceptException(delegate
			{
				if (this.expectedItemType == null)
				{
					this.collectionValidator = new CollectionWithoutExpectedTypeValidator(null);
				}
				this.StartCollection(collectionStart);
			});
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00024D7A File Offset: 0x00022F7A
		private void VerifyCanWriteItem(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00024DBC File Offset: 0x00022FBC
		private void WriteItemImplementation(object item)
		{
			if (this.scopes.Peek().State != ODataCollectionWriterCore.CollectionWriterState.Item)
			{
				this.EnterScope(ODataCollectionWriterCore.CollectionWriterState.Item, item);
			}
			this.InterceptException(delegate
			{
				ValidationUtils.ValidateCollectionItem(item, true);
				this.WriteCollectionItem(item, this.expectedItemType);
			});
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00024E0F File Offset: 0x0002300F
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00024EA5 File Offset: 0x000230A5
		private void WriteEndImplementation()
		{
			this.InterceptException(delegate
			{
				ODataCollectionWriterCore.Scope scope = this.scopes.Peek();
				switch (scope.State)
				{
				case ODataCollectionWriterCore.CollectionWriterState.Start:
				case ODataCollectionWriterCore.CollectionWriterState.Completed:
				case ODataCollectionWriterCore.CollectionWriterState.Error:
					throw new ODataException(Strings.ODataCollectionWriterCore_WriteEndCalledInInvalidState(scope.State.ToString()));
				case ODataCollectionWriterCore.CollectionWriterState.Collection:
					this.EndCollection();
					break;
				case ODataCollectionWriterCore.CollectionWriterState.Item:
					this.LeaveScope();
					this.EndCollection();
					break;
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataCollectionWriterCore_WriteEnd_UnreachableCodePath));
				}
				this.LeaveScope();
			});
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00024EB9 File Offset: 0x000230B9
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00024EC8 File Offset: 0x000230C8
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.outputContext.Synchronous)
				{
					throw new ODataException(Strings.ODataCollectionWriterCore_SyncCallOnAsyncWriter);
				}
			}
			else if (this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataCollectionWriterCore_AsyncCallOnSyncWriter);
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00024F00 File Offset: 0x00023100
		private void StartPayloadInStartState()
		{
			ODataCollectionWriterCore.Scope scope = this.scopes.Peek();
			if (scope.State == ODataCollectionWriterCore.CollectionWriterState.Start)
			{
				this.InterceptException(new Action(this.StartPayload));
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00024F34 File Offset: 0x00023134
		private void InterceptException(Action action)
		{
			try
			{
				action();
			}
			catch
			{
				if (!ODataCollectionWriterCore.IsErrorState(this.State))
				{
					this.EnterScope(ODataCollectionWriterCore.CollectionWriterState.Error, this.scopes.Peek().Item);
				}
				throw;
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00024F80 File Offset: 0x00023180
		private void NotifyListener(ODataCollectionWriterCore.CollectionWriterState newState)
		{
			if (this.listener != null)
			{
				if (ODataCollectionWriterCore.IsErrorState(newState))
				{
					this.listener.OnException();
					return;
				}
				if (newState == ODataCollectionWriterCore.CollectionWriterState.Completed)
				{
					this.listener.OnCompleted();
				}
			}
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00024FC8 File Offset: 0x000231C8
		private void EnterScope(ODataCollectionWriterCore.CollectionWriterState newState, object item)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			this.scopes.Push(new ODataCollectionWriterCore.Scope(newState, item));
			this.NotifyListener(newState);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00025020 File Offset: 0x00023220
		private void LeaveScope()
		{
			this.scopes.Pop();
			if (this.scopes.Count == 1)
			{
				this.scopes.Pop();
				this.scopes.Push(new ODataCollectionWriterCore.Scope(ODataCollectionWriterCore.CollectionWriterState.Completed, null));
				this.InterceptException(new Action(this.EndPayload));
				this.NotifyListener(ODataCollectionWriterCore.CollectionWriterState.Completed);
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002507F File Offset: 0x0002327F
		private void ReplaceScope(ODataCollectionWriterCore.CollectionWriterState newState, ODataItem item)
		{
			this.ValidateTransition(newState);
			this.scopes.Pop();
			this.scopes.Push(new ODataCollectionWriterCore.Scope(newState, item));
			this.NotifyListener(newState);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000250B0 File Offset: 0x000232B0
		private void ValidateTransition(ODataCollectionWriterCore.CollectionWriterState newState)
		{
			if (!ODataCollectionWriterCore.IsErrorState(this.State) && ODataCollectionWriterCore.IsErrorState(newState))
			{
				return;
			}
			switch (this.State)
			{
			case ODataCollectionWriterCore.CollectionWriterState.Start:
				if (newState != ODataCollectionWriterCore.CollectionWriterState.Collection && newState != ODataCollectionWriterCore.CollectionWriterState.Completed)
				{
					throw new ODataException(Strings.ODataCollectionWriterCore_InvalidTransitionFromStart(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataCollectionWriterCore.CollectionWriterState.Collection:
				if (newState != ODataCollectionWriterCore.CollectionWriterState.Item && newState != ODataCollectionWriterCore.CollectionWriterState.Completed)
				{
					throw new ODataException(Strings.ODataCollectionWriterCore_InvalidTransitionFromCollection(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataCollectionWriterCore.CollectionWriterState.Item:
				if (newState != ODataCollectionWriterCore.CollectionWriterState.Completed)
				{
					throw new ODataException(Strings.ODataCollectionWriterCore_InvalidTransitionFromItem(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataCollectionWriterCore.CollectionWriterState.Completed:
				throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromCompleted(this.State.ToString(), newState.ToString()));
			case ODataCollectionWriterCore.CollectionWriterState.Error:
				if (newState != ODataCollectionWriterCore.CollectionWriterState.Error)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromError(this.State.ToString(), newState.ToString()));
				}
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataCollectionWriterCore_ValidateTransition_UnreachableCodePath));
			}
		}

		// Token: 0x04000407 RID: 1031
		private readonly ODataOutputContext outputContext;

		// Token: 0x04000408 RID: 1032
		private readonly IODataReaderWriterListener listener;

		// Token: 0x04000409 RID: 1033
		private readonly Stack<ODataCollectionWriterCore.Scope> scopes = new Stack<ODataCollectionWriterCore.Scope>();

		// Token: 0x0400040A RID: 1034
		private readonly IEdmTypeReference expectedItemType;

		// Token: 0x0400040B RID: 1035
		private DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;

		// Token: 0x0400040C RID: 1036
		private CollectionWithoutExpectedTypeValidator collectionValidator;

		// Token: 0x02000188 RID: 392
		internal enum CollectionWriterState
		{
			// Token: 0x0400040E RID: 1038
			Start,
			// Token: 0x0400040F RID: 1039
			Collection,
			// Token: 0x04000410 RID: 1040
			Item,
			// Token: 0x04000411 RID: 1041
			Completed,
			// Token: 0x04000412 RID: 1042
			Error
		}

		// Token: 0x02000189 RID: 393
		private sealed class Scope
		{
			// Token: 0x06000B25 RID: 2853 RVA: 0x000251F0 File Offset: 0x000233F0
			public Scope(ODataCollectionWriterCore.CollectionWriterState state, object item)
			{
				this.state = state;
				this.item = item;
			}

			// Token: 0x1700029A RID: 666
			// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00025206 File Offset: 0x00023406
			public ODataCollectionWriterCore.CollectionWriterState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x1700029B RID: 667
			// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0002520E File Offset: 0x0002340E
			public object Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x04000413 RID: 1043
			private readonly ODataCollectionWriterCore.CollectionWriterState state;

			// Token: 0x04000414 RID: 1044
			private readonly object item;
		}
	}
}
