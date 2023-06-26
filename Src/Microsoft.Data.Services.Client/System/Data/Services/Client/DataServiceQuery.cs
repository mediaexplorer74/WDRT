using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Services.Client
{
	// Token: 0x0200010B RID: 267
	public abstract class DataServiceQuery : DataServiceRequest, IQueryable, IEnumerable
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x0002489C File Offset: 0x00022A9C
		internal DataServiceQuery()
		{
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060008C1 RID: 2241
		public abstract Expression Expression { get; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060008C2 RID: 2242
		public abstract IQueryProvider Provider { get; }

		// Token: 0x060008C3 RID: 2243 RVA: 0x000248A4 File Offset: 0x00022AA4
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw Error.NotImplemented();
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x000248AB File Offset: 0x00022AAB
		public IEnumerable Execute()
		{
			return this.ExecuteInternal();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x000248B3 File Offset: 0x00022AB3
		public IAsyncResult BeginExecute(AsyncCallback callback, object state)
		{
			return this.BeginExecuteInternal(callback, state);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x000248BD File Offset: 0x00022ABD
		public IEnumerable EndExecute(IAsyncResult asyncResult)
		{
			return this.EndExecuteInternal(asyncResult);
		}

		// Token: 0x060008C7 RID: 2247
		internal abstract IEnumerable ExecuteInternal();

		// Token: 0x060008C8 RID: 2248
		internal abstract IAsyncResult BeginExecuteInternal(AsyncCallback callback, object state);

		// Token: 0x060008C9 RID: 2249
		internal abstract IEnumerable EndExecuteInternal(IAsyncResult asyncResult);
	}
}
