using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x0200014C RID: 332
	internal abstract class ODataCollectionReaderCore : ODataCollectionReader
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x0001C868 File Offset: 0x0001AA68
		protected ODataCollectionReaderCore(ODataInputContext inputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
		{
			this.inputContext = inputContext;
			this.expectedItemTypeReference = expectedItemTypeReference;
			if (this.expectedItemTypeReference == null)
			{
				this.collectionValidator = new CollectionWithoutExpectedTypeValidator(null);
			}
			this.listener = listener;
			this.EnterScope(ODataCollectionReaderState.Start, null);
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0001C8B7 File Offset: 0x0001AAB7
		public sealed override ODataCollectionReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0001C8D4 File Offset: 0x0001AAD4
		public sealed override object Item
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Item;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0001C8F1 File Offset: 0x0001AAF1
		protected bool IsCollectionElementEmpty
		{
			get
			{
				return this.scopes.Peek().IsCollectionElementEmpty;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0001C903 File Offset: 0x0001AB03
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x0001C90C File Offset: 0x0001AB0C
		protected IEdmTypeReference ExpectedItemTypeReference
		{
			get
			{
				return this.expectedItemTypeReference;
			}
			set
			{
				ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(value, "value");
				if (this.State != ODataCollectionReaderState.Start)
				{
					throw new ODataException(Strings.ODataCollectionReaderCore_ExpectedItemTypeSetInInvalidState(this.State.ToString(), ODataCollectionReaderState.Start.ToString()));
				}
				if (this.expectedItemTypeReference != value)
				{
					this.expectedItemTypeReference = value;
					this.collectionValidator = null;
				}
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0001C969 File Offset: 0x0001AB69
		protected CollectionWithoutExpectedTypeValidator CollectionValidator
		{
			get
			{
				return this.collectionValidator;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0001C971 File Offset: 0x0001AB71
		protected bool IsReadingNestedPayload
		{
			get
			{
				return this.listener != null;
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001C97F File Offset: 0x0001AB7F
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001C9A4 File Offset: 0x0001ABA4
		public sealed override Task<bool> ReadAsync()
		{
			this.VerifyCanRead(false);
			return this.ReadAsynchronously().FollowOnFaultWith(delegate(Task<bool> t)
			{
				this.EnterScope(ODataCollectionReaderState.Exception, null);
			});
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001C9C4 File Offset: 0x0001ABC4
		protected bool ReadImplementation()
		{
			bool flag;
			switch (this.State)
			{
			case ODataCollectionReaderState.Start:
				flag = this.ReadAtStartImplementation();
				break;
			case ODataCollectionReaderState.CollectionStart:
				flag = this.ReadAtCollectionStartImplementation();
				break;
			case ODataCollectionReaderState.Value:
				flag = this.ReadAtValueImplementation();
				break;
			case ODataCollectionReaderState.CollectionEnd:
				flag = this.ReadAtCollectionEndImplementation();
				break;
			case ODataCollectionReaderState.Exception:
			case ODataCollectionReaderState.Completed:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataCollectionReaderCore_ReadImplementation));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataCollectionReaderCore_ReadImplementation));
			}
			return flag;
		}

		// Token: 0x060008F7 RID: 2295
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x060008F8 RID: 2296
		protected abstract bool ReadAtCollectionStartImplementation();

		// Token: 0x060008F9 RID: 2297
		protected abstract bool ReadAtValueImplementation();

		// Token: 0x060008FA RID: 2298
		protected abstract bool ReadAtCollectionEndImplementation();

		// Token: 0x060008FB RID: 2299 RVA: 0x0001CA41 File Offset: 0x0001AC41
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001CA49 File Offset: 0x0001AC49
		protected virtual Task<bool> ReadAsynchronously()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadImplementation));
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001CA5C File Offset: 0x0001AC5C
		protected void EnterScope(ODataCollectionReaderState state, object item)
		{
			this.EnterScope(state, item, false);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001CA68 File Offset: 0x0001AC68
		protected void EnterScope(ODataCollectionReaderState state, object item, bool isCollectionElementEmpty)
		{
			if (state == ODataCollectionReaderState.Value)
			{
				ValidationUtils.ValidateCollectionItem(item, true);
			}
			this.scopes.Push(new ODataCollectionReaderCore.Scope(state, item, isCollectionElementEmpty));
			if (this.listener != null)
			{
				if (state == ODataCollectionReaderState.Exception)
				{
					this.listener.OnException();
					return;
				}
				if (state == ODataCollectionReaderState.Completed)
				{
					this.listener.OnCompleted();
				}
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001CABA File Offset: 0x0001ACBA
		protected void ReplaceScope(ODataCollectionReaderState state, object item)
		{
			if (state == ODataCollectionReaderState.Value)
			{
				ValidationUtils.ValidateCollectionItem(item, true);
			}
			this.scopes.Pop();
			this.EnterScope(state, item);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001CADB File Offset: 0x0001ACDB
		protected void PopScope(ODataCollectionReaderState state)
		{
			this.scopes.Pop();
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001CAEC File Offset: 0x0001ACEC
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
					this.EnterScope(ODataCollectionReaderState.Exception, null);
				}
				throw;
			}
			return t;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001CB28 File Offset: 0x0001AD28
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataCollectionReaderState.Exception || this.State == ODataCollectionReaderState.Completed)
			{
				throw new ODataException(Strings.ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001CB64 File Offset: 0x0001AD64
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				this.VerifySynchronousCallAllowed();
				return;
			}
			this.VerifyAsynchronousCallAllowed();
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001CB76 File Offset: 0x0001AD76
		private void VerifySynchronousCallAllowed()
		{
			if (!this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataCollectionReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001CB90 File Offset: 0x0001AD90
		private void VerifyAsynchronousCallAllowed()
		{
			if (this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataCollectionReaderCore_AsyncCallOnSyncReader);
			}
		}

		// Token: 0x04000359 RID: 857
		private readonly ODataInputContext inputContext;

		// Token: 0x0400035A RID: 858
		private readonly Stack<ODataCollectionReaderCore.Scope> scopes = new Stack<ODataCollectionReaderCore.Scope>();

		// Token: 0x0400035B RID: 859
		private readonly IODataReaderWriterListener listener;

		// Token: 0x0400035C RID: 860
		private CollectionWithoutExpectedTypeValidator collectionValidator;

		// Token: 0x0400035D RID: 861
		private IEdmTypeReference expectedItemTypeReference;

		// Token: 0x0200014D RID: 333
		protected sealed class Scope
		{
			// Token: 0x06000907 RID: 2311 RVA: 0x0001CBAA File Offset: 0x0001ADAA
			public Scope(ODataCollectionReaderState state, object item)
				: this(state, item, false)
			{
			}

			// Token: 0x06000908 RID: 2312 RVA: 0x0001CBB5 File Offset: 0x0001ADB5
			public Scope(ODataCollectionReaderState state, object item, bool isCollectionElementEmpty)
			{
				this.state = state;
				this.item = item;
				this.isCollectionElementEmpty = isCollectionElementEmpty;
				bool flag = this.isCollectionElementEmpty;
			}

			// Token: 0x17000226 RID: 550
			// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001CBD9 File Offset: 0x0001ADD9
			public ODataCollectionReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000227 RID: 551
			// (get) Token: 0x0600090A RID: 2314 RVA: 0x0001CBE1 File Offset: 0x0001ADE1
			public object Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x17000228 RID: 552
			// (get) Token: 0x0600090B RID: 2315 RVA: 0x0001CBE9 File Offset: 0x0001ADE9
			public bool IsCollectionElementEmpty
			{
				get
				{
					return this.isCollectionElementEmpty;
				}
			}

			// Token: 0x0400035E RID: 862
			private readonly ODataCollectionReaderState state;

			// Token: 0x0400035F RID: 863
			private readonly object item;

			// Token: 0x04000360 RID: 864
			private readonly bool isCollectionElementEmpty;
		}
	}
}
