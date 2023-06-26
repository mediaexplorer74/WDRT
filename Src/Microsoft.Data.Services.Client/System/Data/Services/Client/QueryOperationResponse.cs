using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Data.Services.Client
{
	// Token: 0x0200010E RID: 270
	public class QueryOperationResponse : OperationResponse, IEnumerable
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x000249E6 File Offset: 0x00022BE6
		internal QueryOperationResponse(HeaderCollection headers, DataServiceRequest query, MaterializeAtom results)
			: base(headers)
		{
			this.query = query;
			this.results = results;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x000249FD File Offset: 0x00022BFD
		public DataServiceRequest Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00024A05 File Offset: 0x00022C05
		public virtual long TotalCount
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00024A0C File Offset: 0x00022C0C
		internal MaterializeAtom Results
		{
			get
			{
				if (base.Error != null)
				{
					throw System.Data.Services.Client.Error.InvalidOperation(Strings.Context_BatchExecuteError, base.Error);
				}
				return this.results;
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00024A3A File Offset: 0x00022C3A
		public IEnumerator GetEnumerator()
		{
			return this.GetEnumeratorHelper<IEnumerator>(() => this.Results.GetEnumerator());
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00024A4E File Offset: 0x00022C4E
		public DataServiceQueryContinuation GetContinuation()
		{
			return this.results.GetContinuation(null);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00024A5C File Offset: 0x00022C5C
		public DataServiceQueryContinuation GetContinuation(IEnumerable collection)
		{
			return this.results.GetContinuation(collection);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00024A6A File Offset: 0x00022C6A
		public DataServiceQueryContinuation<T> GetContinuation<T>(IEnumerable<T> collection)
		{
			return (DataServiceQueryContinuation<T>)this.results.GetContinuation(collection);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00024A80 File Offset: 0x00022C80
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal static QueryOperationResponse GetInstance(Type elementType, HeaderCollection headers, DataServiceRequest query, MaterializeAtom results)
		{
			Type type = typeof(QueryOperationResponse<>).MakeGenericType(new Type[] { elementType });
			return (QueryOperationResponse)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[] { headers, query, results }, CultureInfo.InvariantCulture);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00024AD4 File Offset: 0x00022CD4
		protected T GetEnumeratorHelper<T>(Func<T> getEnumerator) where T : IEnumerator
		{
			if (getEnumerator == null)
			{
				throw new ArgumentNullException("getEnumerator");
			}
			if (this.Results.Context != null)
			{
				bool? singleResult = this.Query.QueryComponents(this.Results.Context.Model).SingleResult;
				if (singleResult != null && !singleResult.Value)
				{
					IEnumerator enumerator = this.Results.GetEnumerator();
					if (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ICollection collection = obj as ICollection;
						if (collection == null)
						{
							throw new DataServiceClientException(Strings.AtomMaterializer_CollectionExpectedCollection(obj.GetType().ToString()));
						}
						return (T)((object)collection.GetEnumerator());
					}
				}
			}
			return getEnumerator();
		}

		// Token: 0x04000517 RID: 1303
		private readonly DataServiceRequest query;

		// Token: 0x04000518 RID: 1304
		private readonly MaterializeAtom results;
	}
}
