using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Services.Client
{
	// Token: 0x0200012F RID: 303
	public class DataServiceQuery<TElement> : DataServiceQuery, IQueryable<TElement>, IEnumerable<TElement>, IQueryable, IEnumerable
	{
		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002B085 File Offset: 0x00029285
		private DataServiceQuery(Expression expression, DataServiceQueryProvider provider)
		{
			this.queryExpression = expression;
			this.queryProvider = provider;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002B09B File Offset: 0x0002929B
		public override Type ElementType
		{
			get
			{
				return typeof(TElement);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0002B0A7 File Offset: 0x000292A7
		public override Expression Expression
		{
			get
			{
				return this.queryExpression;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0002B0AF File Offset: 0x000292AF
		public override IQueryProvider Provider
		{
			get
			{
				return this.queryProvider;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0002B0B7 File Offset: 0x000292B7
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x0002B0C4 File Offset: 0x000292C4
		public override Uri RequestUri
		{
			get
			{
				return this.Translate().Uri;
			}
			internal set
			{
				this.Translate().Uri = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0002B0D2 File Offset: 0x000292D2
		internal override ProjectionPlan Plan
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0002B0D5 File Offset: 0x000292D5
		private DataServiceContext Context
		{
			get
			{
				return this.queryProvider.Context;
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002B0E2 File Offset: 0x000292E2
		public new IAsyncResult BeginExecute(AsyncCallback callback, object state)
		{
			return base.BeginExecute(this, this.Context, callback, state, "Execute");
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002B0F8 File Offset: 0x000292F8
		public new IEnumerable<TElement> EndExecute(IAsyncResult asyncResult)
		{
			return DataServiceRequest.EndExecute<TElement>(this, this.Context, "Execute", asyncResult);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0002B10C File Offset: 0x0002930C
		public new IEnumerable<TElement> Execute()
		{
			return base.Execute<TElement>(this.Context, this.Translate());
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0002B120 File Offset: 0x00029320
		public DataServiceQuery<TElement> Expand(string path)
		{
			Util.CheckArgumentNullAndEmpty(path, "path");
			return (DataServiceQuery<TElement>)this.Provider.CreateQuery<TElement>(Expression.Call(Expression.Convert(this.Expression, typeof(DataServiceQuery<TElement>.DataServiceOrderedQuery)), DataServiceQuery<TElement>.expandMethodInfo, new Expression[] { Expression.Constant(path) }));
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002B178 File Offset: 0x00029378
		public DataServiceQuery<TElement> Expand<TTarget>(Expression<Func<TElement, TTarget>> navigationPropertyAccessor)
		{
			Util.CheckArgumentNull<Expression<Func<TElement, TTarget>>>(navigationPropertyAccessor, "navigationPropertyAccessor");
			MethodInfo methodInfo = DataServiceQuery<TElement>.expandGenericMethodInfo.MakeGenericMethod(new Type[] { typeof(TTarget) });
			return (DataServiceQuery<TElement>)this.Provider.CreateQuery<TElement>(Expression.Call(Expression.Convert(this.Expression, typeof(DataServiceQuery<TElement>.DataServiceOrderedQuery)), methodInfo, new Expression[] { navigationPropertyAccessor }));
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002B1E8 File Offset: 0x000293E8
		public DataServiceQuery<TElement> IncludeTotalCount()
		{
			MethodInfo method = typeof(DataServiceQuery<TElement>).GetMethod("IncludeTotalCount");
			return (DataServiceQuery<TElement>)this.Provider.CreateQuery<TElement>(Expression.Call(Expression.Convert(this.Expression, typeof(DataServiceQuery<TElement>.DataServiceOrderedQuery)), method));
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002B238 File Offset: 0x00029438
		public DataServiceQuery<TElement> AddQueryOption(string name, object value)
		{
			Util.CheckArgumentNull<string>(name, "name");
			Util.CheckArgumentNull<object>(value, "value");
			MethodInfo method = typeof(DataServiceQuery<TElement>).GetMethod("AddQueryOption");
			return (DataServiceQuery<TElement>)this.Provider.CreateQuery<TElement>(Expression.Call(Expression.Convert(this.Expression, typeof(DataServiceQuery<TElement>.DataServiceOrderedQuery)), method, new Expression[]
			{
				Expression.Constant(name),
				Expression.Constant(value, typeof(object))
			}));
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002B2C1 File Offset: 0x000294C1
		public IEnumerator<TElement> GetEnumerator()
		{
			return this.Execute().GetEnumerator();
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002B2D0 File Offset: 0x000294D0
		public override string ToString()
		{
			string text;
			try
			{
				text = this.QueryComponents(this.Context.Model).Uri.ToString();
			}
			catch (NotSupportedException ex)
			{
				text = Strings.ALinq_TranslationError(ex.Message);
			}
			return text;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002B31C File Offset: 0x0002951C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002B324 File Offset: 0x00029524
		internal override QueryComponents QueryComponents(ClientEdmModel model)
		{
			return this.Translate();
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002B32C File Offset: 0x0002952C
		internal override IEnumerable ExecuteInternal()
		{
			return this.Execute();
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002B334 File Offset: 0x00029534
		internal override IAsyncResult BeginExecuteInternal(AsyncCallback callback, object state)
		{
			return this.BeginExecute(callback, state);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002B33E File Offset: 0x0002953E
		internal override IEnumerable EndExecuteInternal(IAsyncResult asyncResult)
		{
			return this.EndExecute(asyncResult);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002B347 File Offset: 0x00029547
		private QueryComponents Translate()
		{
			if (this.queryComponents == null)
			{
				this.queryComponents = this.queryProvider.Translate(this.queryExpression);
			}
			return this.queryComponents;
		}

		// Token: 0x040005E9 RID: 1513
		private static readonly MethodInfo expandMethodInfo = typeof(DataServiceQuery<TElement>).GetMethod("Expand", new Type[] { typeof(string) });

		// Token: 0x040005EA RID: 1514
		private static readonly MethodInfo expandGenericMethodInfo = (MethodInfo)typeof(DataServiceQuery<TElement>).GetMember("Expand*").Single((MemberInfo m) => ((MethodInfo)m).GetGenericArguments().Count<Type>() == 1);

		// Token: 0x040005EB RID: 1515
		private readonly Expression queryExpression;

		// Token: 0x040005EC RID: 1516
		private readonly DataServiceQueryProvider queryProvider;

		// Token: 0x040005ED RID: 1517
		private QueryComponents queryComponents;

		// Token: 0x02000130 RID: 304
		internal class DataServiceOrderedQuery : DataServiceQuery<TElement>, IOrderedQueryable<TElement>, IQueryable<TElement>, IEnumerable<TElement>, IOrderedQueryable, IQueryable, IEnumerable
		{
			// Token: 0x06000AEB RID: 2795 RVA: 0x0002B3FF File Offset: 0x000295FF
			internal DataServiceOrderedQuery(Expression expression, DataServiceQueryProvider provider)
				: base(expression, provider)
			{
			}
		}
	}
}
