using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Data.Services.Client
{
	// Token: 0x020000E0 RID: 224
	internal sealed class DataServiceQueryProvider : IQueryProvider
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x0001F4CD File Offset: 0x0001D6CD
		internal DataServiceQueryProvider(DataServiceContext context)
		{
			this.Context = context;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001F4DC File Offset: 0x0001D6DC
		public IQueryable CreateQuery(Expression expression)
		{
			Util.CheckArgumentNull<Expression>(expression, "expression");
			Type elementType = TypeSystem.GetElementType(expression.Type);
			Type type = typeof(DataServiceQuery<>.DataServiceOrderedQuery).MakeGenericType(new Type[] { elementType });
			object[] array = new object[] { expression, this };
			ConstructorInfo instanceConstructor = type.GetInstanceConstructor(false, new Type[]
			{
				typeof(Expression),
				typeof(DataServiceQueryProvider)
			});
			return (IQueryable)Util.ConstructorInvoke(instanceConstructor, array);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001F56F File Offset: 0x0001D76F
		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			Util.CheckArgumentNull<Expression>(expression, "expression");
			return new DataServiceQuery<TElement>.DataServiceOrderedQuery(expression, this);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001F584 File Offset: 0x0001D784
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public object Execute(Expression expression)
		{
			Util.CheckArgumentNull<Expression>(expression, "expression");
			MethodInfo method = typeof(DataServiceQueryProvider).GetMethod("ReturnSingleton", false, false);
			return method.MakeGenericMethod(new Type[] { expression.Type }).Invoke(this, new object[] { expression });
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001F5DD File Offset: 0x0001D7DD
		public TResult Execute<TResult>(Expression expression)
		{
			Util.CheckArgumentNull<Expression>(expression, "expression");
			return this.ReturnSingleton<TResult>(expression);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001F5F4 File Offset: 0x0001D7F4
		internal TElement ReturnSingleton<TElement>(Expression expression)
		{
			IQueryable<TElement> queryable = new DataServiceQuery<TElement>.DataServiceOrderedQuery(expression, this);
			MethodCallExpression methodCallExpression = expression as MethodCallExpression;
			SequenceMethod sequenceMethod;
			if (ReflectionUtil.TryIdentifySequenceMethod(methodCallExpression.Method, out sequenceMethod))
			{
				SequenceMethod sequenceMethod2 = sequenceMethod;
				switch (sequenceMethod2)
				{
				case SequenceMethod.First:
					return queryable.AsEnumerable<TElement>().First<TElement>();
				case SequenceMethod.FirstPredicate:
					break;
				case SequenceMethod.FirstOrDefault:
					return queryable.AsEnumerable<TElement>().FirstOrDefault<TElement>();
				default:
					switch (sequenceMethod2)
					{
					case SequenceMethod.Single:
						return queryable.AsEnumerable<TElement>().Single<TElement>();
					case SequenceMethod.SinglePredicate:
						break;
					case SequenceMethod.SingleOrDefault:
						return queryable.AsEnumerable<TElement>().SingleOrDefault<TElement>();
					default:
						switch (sequenceMethod2)
						{
						case SequenceMethod.Count:
						case SequenceMethod.LongCount:
							return (TElement)((object)Convert.ChangeType(((DataServiceQuery<TElement>)queryable).GetQuerySetCount(this.Context), typeof(TElement), CultureInfo.InvariantCulture.NumberFormat));
						}
						break;
					}
					break;
				}
				throw Error.MethodNotSupported(methodCallExpression);
			}
			throw Error.MethodNotSupported(methodCallExpression);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001F6D8 File Offset: 0x0001D8D8
		internal QueryComponents Translate(Expression e)
		{
			bool flag = false;
			Dictionary<Expression, Expression> dictionary = null;
			if (!(e is ResourceSetExpression))
			{
				dictionary = new Dictionary<Expression, Expression>(ReferenceEqualityComparer<Expression>.Instance);
				e = Evaluator.PartialEval(e);
				e = ExpressionNormalizer.Normalize(e, dictionary);
				e = ResourceBinder.Bind(e, this.Context);
				flag = true;
			}
			Uri uri;
			Version version;
			UriWriter.Translate(this.Context, flag, e, out uri, out version);
			ResourceExpression resourceExpression = e as ResourceExpression;
			Type type = ((resourceExpression.Projection == null) ? resourceExpression.ResourceType : resourceExpression.Projection.Selector.Parameters[0].Type);
			LambdaExpression lambdaExpression = ((resourceExpression.Projection == null) ? null : resourceExpression.Projection.Selector);
			return new QueryComponents(uri, version, type, lambdaExpression, dictionary);
		}

		// Token: 0x04000489 RID: 1161
		internal readonly DataServiceContext Context;
	}
}
