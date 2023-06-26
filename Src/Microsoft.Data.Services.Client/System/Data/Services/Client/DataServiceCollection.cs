using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Services.Client.Materialization;
using System.Linq;
using System.Threading;

namespace System.Data.Services.Client
{
	// Token: 0x020000F5 RID: 245
	public class DataServiceCollection<T> : ObservableCollection<T>
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x0002277C File Offset: 0x0002097C
		public DataServiceCollection()
			: this(null, null, TrackingMode.AutoChangeTracking, null, null, null)
		{
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0002278A File Offset: 0x0002098A
		public DataServiceCollection(IEnumerable<T> items)
			: this(null, items, TrackingMode.AutoChangeTracking, null, null, null)
		{
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00022798 File Offset: 0x00020998
		public DataServiceCollection(IEnumerable<T> items, TrackingMode trackingMode)
			: this(null, items, trackingMode, null, null, null)
		{
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000227A6 File Offset: 0x000209A6
		public DataServiceCollection(DataServiceContext context)
			: this(context, null, TrackingMode.AutoChangeTracking, null, null, null)
		{
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000227B4 File Offset: 0x000209B4
		public DataServiceCollection(DataServiceContext context, string entitySetName, Func<EntityChangedParams, bool> entityChangedCallback, Func<EntityCollectionChangedParams, bool> collectionChangedCallback)
			: this(context, null, TrackingMode.AutoChangeTracking, entitySetName, entityChangedCallback, collectionChangedCallback)
		{
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000227C3 File Offset: 0x000209C3
		public DataServiceCollection(IEnumerable<T> items, TrackingMode trackingMode, string entitySetName, Func<EntityChangedParams, bool> entityChangedCallback, Func<EntityCollectionChangedParams, bool> collectionChangedCallback)
			: this(null, items, trackingMode, entitySetName, entityChangedCallback, collectionChangedCallback)
		{
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000227D4 File Offset: 0x000209D4
		public DataServiceCollection(DataServiceContext context, IEnumerable<T> items, TrackingMode trackingMode, string entitySetName, Func<EntityChangedParams, bool> entityChangedCallback, Func<EntityCollectionChangedParams, bool> collectionChangedCallback)
		{
			if (trackingMode == TrackingMode.AutoChangeTracking)
			{
				if (context == null)
				{
					if (items == null)
					{
						this.trackingOnLoad = true;
						this.entitySetName = entitySetName;
						this.entityChangedCallback = entityChangedCallback;
						this.collectionChangedCallback = collectionChangedCallback;
					}
					else
					{
						context = DataServiceCollection<T>.GetContextFromItems(items);
					}
				}
				if (!this.trackingOnLoad)
				{
					if (items != null)
					{
						DataServiceCollection<T>.ValidateIteratorParameter(items);
					}
					this.StartTracking(context, items, entitySetName, entityChangedCallback, collectionChangedCallback);
					return;
				}
			}
			else if (items != null)
			{
				this.Load(items);
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00022854 File Offset: 0x00020A54
		internal DataServiceCollection(object entityMaterializer, DataServiceContext context, IEnumerable<T> items, TrackingMode trackingMode, string entitySetName, Func<EntityChangedParams, bool> entityChangedCallback, Func<EntityCollectionChangedParams, bool> collectionChangedCallback)
			: this((context != null) ? context : ((ODataEntityMaterializer)entityMaterializer).EntityTrackingAdapter.Context, items, trackingMode, entitySetName, entityChangedCallback, collectionChangedCallback)
		{
			if (items != null)
			{
				((ODataEntityMaterializer)entityMaterializer).PropagateContinuation<T>(items, this);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000824 RID: 2084 RVA: 0x0002288C File Offset: 0x00020A8C
		// (remove) Token: 0x06000825 RID: 2085 RVA: 0x000228C4 File Offset: 0x00020AC4
		public event EventHandler<LoadCompletedEventArgs> LoadCompleted;

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x000228F9 File Offset: 0x00020AF9
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00022901 File Offset: 0x00020B01
		public DataServiceQueryContinuation<T> Continuation
		{
			get
			{
				return this.continuation;
			}
			set
			{
				this.continuation = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0002290A File Offset: 0x00020B0A
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x00022912 File Offset: 0x00020B12
		internal BindingObserver Observer
		{
			get
			{
				return this.observer;
			}
			set
			{
				this.observer = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0002291B File Offset: 0x00020B1B
		internal bool IsTracking
		{
			get
			{
				return this.observer != null;
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0002292C File Offset: 0x00020B2C
		public void Load(IEnumerable<T> items)
		{
			DataServiceCollection<T>.ValidateIteratorParameter(items);
			if (this.trackingOnLoad)
			{
				DataServiceContext contextFromItems = DataServiceCollection<T>.GetContextFromItems(items);
				this.trackingOnLoad = false;
				this.StartTracking(contextFromItems, items, this.entitySetName, this.entityChangedCallback, this.collectionChangedCallback);
				return;
			}
			this.StartLoading();
			try
			{
				this.InternalLoadCollection(items);
			}
			finally
			{
				this.FinishLoading();
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000229DC File Offset: 0x00020BDC
		public void LoadAsync(IQueryable<T> query)
		{
			Util.CheckArgumentNull<IQueryable<T>>(query, "query");
			DataServiceQuery<T> dsq = query as DataServiceQuery<T>;
			if (dsq == null)
			{
				throw new ArgumentException(Strings.DataServiceCollection_LoadAsyncRequiresDataServiceQuery, "query");
			}
			if (this.ongoingAsyncOperation != null)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime);
			}
			if (this.trackingOnLoad)
			{
				this.StartTracking(((DataServiceQueryProvider)dsq.Provider).Context, null, this.entitySetName, this.entityChangedCallback, this.collectionChangedCallback);
				this.trackingOnLoad = false;
			}
			this.BeginLoadAsyncOperation((AsyncCallback asyncCallback) => dsq.BeginExecute(asyncCallback, null), delegate(IAsyncResult asyncResult)
			{
				QueryOperationResponse<T> queryOperationResponse = (QueryOperationResponse<T>)dsq.EndExecute(asyncResult);
				this.Load(queryOperationResponse);
				return queryOperationResponse;
			});
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00022AE0 File Offset: 0x00020CE0
		public void LoadAsync(Uri requestUri)
		{
			Util.CheckArgumentNull<Uri>(requestUri, "requestUri");
			if (!this.IsTracking)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_OperationForTrackedOnly);
			}
			if (this.ongoingAsyncOperation != null)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime);
			}
			DataServiceContext context = this.observer.Context;
			requestUri = UriUtil.CreateUri(context.BaseUri, requestUri);
			this.BeginLoadAsyncOperation((AsyncCallback asyncCallback) => context.BeginExecute<T>(requestUri, asyncCallback, null), delegate(IAsyncResult asyncResult)
			{
				QueryOperationResponse<T> queryOperationResponse = (QueryOperationResponse<T>)context.EndExecute<T>(asyncResult);
				this.Load(queryOperationResponse);
				return queryOperationResponse;
			});
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00022BC4 File Offset: 0x00020DC4
		public void LoadAsync()
		{
			if (!this.IsTracking)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_OperationForTrackedOnly);
			}
			object parent;
			string property;
			if (!this.observer.LookupParent<T>(this, out parent, out property))
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_LoadAsyncNoParamsWithoutParentEntity);
			}
			if (this.ongoingAsyncOperation != null)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime);
			}
			this.BeginLoadAsyncOperation((AsyncCallback asyncCallback) => this.observer.Context.BeginLoadProperty(parent, property, asyncCallback, null), (IAsyncResult asyncResult) => this.observer.Context.EndLoadProperty(asyncResult));
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00022C90 File Offset: 0x00020E90
		public bool LoadNextPartialSetAsync()
		{
			if (!this.IsTracking)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_OperationForTrackedOnly);
			}
			if (this.ongoingAsyncOperation != null)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_MultipleLoadAsyncOperationsAtTheSameTime);
			}
			if (this.Continuation == null)
			{
				if (this.LoadCompleted != null)
				{
					this.LoadCompleted(this, new LoadCompletedEventArgs(null, null));
				}
				return false;
			}
			this.BeginLoadAsyncOperation((AsyncCallback asyncCallback) => this.observer.Context.BeginExecute<T>(this.Continuation, asyncCallback, null), delegate(IAsyncResult asyncResult)
			{
				QueryOperationResponse<T> queryOperationResponse = (QueryOperationResponse<T>)this.observer.Context.EndExecute<T>(asyncResult);
				this.Load(queryOperationResponse);
				return queryOperationResponse;
			});
			return true;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00022D07 File Offset: 0x00020F07
		public void CancelAsyncLoad()
		{
			if (this.ongoingAsyncOperation != null)
			{
				this.observer.Context.CancelRequest(this.ongoingAsyncOperation);
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00022D28 File Offset: 0x00020F28
		public void Load(T item)
		{
			if (item == null)
			{
				throw Error.ArgumentNull("item");
			}
			this.StartLoading();
			try
			{
				if (!this.hashedItems.Contains(item))
				{
					base.Add(item);
				}
			}
			finally
			{
				this.FinishLoading();
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00022D7C File Offset: 0x00020F7C
		public void Clear(bool stopTracking)
		{
			if (!this.IsTracking)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_OperationForTrackedOnly);
			}
			if (!stopTracking)
			{
				base.Clear();
				return;
			}
			try
			{
				this.observer.DetachBehavior = true;
				base.Clear();
			}
			finally
			{
				this.observer.DetachBehavior = false;
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00022DD8 File Offset: 0x00020FD8
		public void Detach()
		{
			if (!this.IsTracking)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_OperationForTrackedOnly);
			}
			if (!this.rootCollection)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_CannotStopTrackingChildCollection);
			}
			this.observer.StopTracking();
			this.observer = null;
			this.rootCollection = false;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00022E24 File Offset: 0x00021024
		protected override void InsertItem(int index, T item)
		{
			if (this.trackingOnLoad)
			{
				throw new InvalidOperationException(Strings.DataServiceCollection_InsertIntoTrackedButNotLoadedCollection);
			}
			if (this.IsTracking && item != null && !(item is INotifyPropertyChanged))
			{
				throw new InvalidOperationException(Strings.DataBinding_NotifyPropertyChangedNotImpl(item.GetType()));
			}
			base.InsertItem(index, item);
			this.hashedItems.Add(item);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00022E90 File Offset: 0x00021090
		protected override void ClearItems()
		{
			this.hashedItems.Clear();
			base.ClearItems();
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00022EA3 File Offset: 0x000210A3
		protected override void RemoveItem(int index)
		{
			if (index < base.Count)
			{
				this.hashedItems.Remove(base[index]);
			}
			base.RemoveItem(index);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00022EC8 File Offset: 0x000210C8
		protected override void SetItem(int index, T item)
		{
			if (index < base.Count)
			{
				this.hashedItems.Remove(base[index]);
			}
			base.SetItem(index, item);
			this.hashedItems.Add(item);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00022EFB File Offset: 0x000210FB
		private static void ValidateIteratorParameter(IEnumerable<T> items)
		{
			Util.CheckArgumentNull<IEnumerable<T>>(items, "items");
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00022F0C File Offset: 0x0002110C
		private static DataServiceContext GetContextFromItems(IEnumerable<T> items)
		{
			DataServiceQuery<T> dataServiceQuery = items as DataServiceQuery<T>;
			if (dataServiceQuery != null)
			{
				DataServiceQueryProvider dataServiceQueryProvider = dataServiceQuery.Provider as DataServiceQueryProvider;
				return dataServiceQueryProvider.Context;
			}
			QueryOperationResponse queryOperationResponse = items as QueryOperationResponse;
			if (queryOperationResponse != null)
			{
				return queryOperationResponse.Results.Context;
			}
			throw new ArgumentException(Strings.DataServiceCollection_CannotDetermineContextFromItems);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00022F5C File Offset: 0x0002115C
		private void InternalLoadCollection(IEnumerable<T> items)
		{
			DataServiceQuery<T> dataServiceQuery = items as DataServiceQuery<T>;
			if (dataServiceQuery != null)
			{
				items = dataServiceQuery.Execute() as QueryOperationResponse<T>;
			}
			if (this.IsTracking)
			{
				this.observer.PauseTracking(this);
			}
			int count = base.Count;
			foreach (T t in items)
			{
				if (!this.hashedItems.Contains(t))
				{
					base.Add(t);
				}
			}
			if (this.IsTracking)
			{
				if (base.Count > count && this.observer.AttachBehavior)
				{
					this.observer.OnDataServiceCollectionBulkAdded(this, base.Items.Skip(count));
				}
				this.observer.ResumeTracking(this);
			}
			QueryOperationResponse<T> queryOperationResponse = items as QueryOperationResponse<T>;
			if (queryOperationResponse != null)
			{
				this.continuation = queryOperationResponse.GetContinuation();
				return;
			}
			this.continuation = null;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0002304C File Offset: 0x0002124C
		private void StartLoading()
		{
			if (this.IsTracking)
			{
				if (this.observer.Context == null)
				{
					throw new InvalidOperationException(Strings.DataServiceCollection_LoadRequiresTargetCollectionObserved);
				}
				this.observer.AttachBehavior = true;
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002307A File Offset: 0x0002127A
		private void FinishLoading()
		{
			if (this.IsTracking)
			{
				this.observer.AttachBehavior = false;
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00023090 File Offset: 0x00021290
		private void StartTracking(DataServiceContext context, IEnumerable<T> items, string entitySet, Func<EntityChangedParams, bool> entityChanged, Func<EntityCollectionChangedParams, bool> collectionChanged)
		{
			if (!BindingEntityInfo.IsEntityType(typeof(T), context.Model))
			{
				throw new ArgumentException(Strings.DataBinding_DataServiceCollectionArgumentMustHaveEntityType(typeof(T)));
			}
			this.observer = new BindingObserver(context, entityChanged, collectionChanged);
			if (items != null)
			{
				try
				{
					this.InternalLoadCollection(items);
				}
				catch
				{
					this.observer = null;
					throw;
				}
			}
			this.observer.StartTracking<T>(this, entitySet);
			this.rootCollection = true;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000231AC File Offset: 0x000213AC
		private void BeginLoadAsyncOperation(Func<AsyncCallback, IAsyncResult> beginCall, Func<IAsyncResult, QueryOperationResponse> endCall)
		{
			DataServiceCollection<T>.<>c__DisplayClass12 CS$<>8__locals1 = new DataServiceCollection<T>.<>c__DisplayClass12();
			CS$<>8__locals1.endCall = endCall;
			CS$<>8__locals1.<>4__this = this;
			this.ongoingAsyncOperation = null;
			try
			{
				SynchronizationContext syncContext = SynchronizationContext.Current;
				AsyncCallback asyncCallback;
				if (syncContext == null)
				{
					asyncCallback = delegate(IAsyncResult ar)
					{
						CS$<>8__locals1.<>4__this.EndLoadAsyncOperation(CS$<>8__locals1.endCall, ar);
					};
				}
				else
				{
					asyncCallback = delegate(IAsyncResult ar)
					{
						DataServiceCollection<T>.<>c__DisplayClass12 CS$<>8__locals13 = CS$<>8__locals1;
						IAsyncResult ar = ar;
						syncContext.Post(delegate(object unused)
						{
							CS$<>8__locals13.<>4__this.EndLoadAsyncOperation(CS$<>8__locals13.endCall, ar);
						}, null);
					};
				}
				this.ongoingAsyncOperation = beginCall(asyncCallback);
			}
			catch (Exception)
			{
				this.ongoingAsyncOperation = null;
				throw;
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0002324C File Offset: 0x0002144C
		private void EndLoadAsyncOperation(Func<IAsyncResult, QueryOperationResponse> endCall, IAsyncResult asyncResult)
		{
			try
			{
				QueryOperationResponse queryOperationResponse = endCall(asyncResult);
				this.ongoingAsyncOperation = null;
				if (this.LoadCompleted != null)
				{
					this.LoadCompleted(this, new LoadCompletedEventArgs(queryOperationResponse, null));
				}
			}
			catch (Exception ex)
			{
				if (!CommonUtil.IsCatchableExceptionType(ex))
				{
					throw;
				}
				this.ongoingAsyncOperation = null;
				if (this.LoadCompleted != null)
				{
					this.LoadCompleted(this, new LoadCompletedEventArgs(null, ex));
				}
			}
		}

		// Token: 0x040004D9 RID: 1241
		private BindingObserver observer;

		// Token: 0x040004DA RID: 1242
		private bool rootCollection;

		// Token: 0x040004DB RID: 1243
		private DataServiceQueryContinuation<T> continuation;

		// Token: 0x040004DC RID: 1244
		private bool trackingOnLoad;

		// Token: 0x040004DD RID: 1245
		private Func<EntityChangedParams, bool> entityChangedCallback;

		// Token: 0x040004DE RID: 1246
		private Func<EntityCollectionChangedParams, bool> collectionChangedCallback;

		// Token: 0x040004DF RID: 1247
		private string entitySetName;

		// Token: 0x040004E0 RID: 1248
		private IAsyncResult ongoingAsyncOperation;

		// Token: 0x040004E1 RID: 1249
		private HashSet<T> hashedItems = new HashSet<T>(EqualityComparer<T>.Default);
	}
}
