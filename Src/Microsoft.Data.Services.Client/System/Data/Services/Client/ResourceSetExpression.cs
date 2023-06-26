using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Services.Client
{
	// Token: 0x020000D7 RID: 215
	[DebuggerDisplay("ResourceSetExpression {Source}.{MemberExpression}")]
	internal class ResourceSetExpression : ResourceExpression
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x0001C94C File Offset: 0x0001AB4C
		internal ResourceSetExpression(Type type, Expression source, Expression memberExpression, Type resourceType, List<string> expandPaths, CountOption countOption, Dictionary<ConstantExpression, ConstantExpression> customQueryOptions, ProjectionQueryOptionExpression projection, Type resourceTypeAs, Version uriVersion)
			: base(source, type, expandPaths, countOption, customQueryOptions, projection, resourceTypeAs, uriVersion)
		{
			this.member = memberExpression;
			this.resourceType = resourceType;
			this.sequenceQueryOptions = new List<QueryOptionExpression>();
			this.keyPredicateConjuncts = new List<Expression>();
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0001C992 File Offset: 0x0001AB92
		public override ExpressionType NodeType
		{
			get
			{
				if (this.source == null)
				{
					return (ExpressionType)10000;
				}
				return (ExpressionType)10001;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001C9A7 File Offset: 0x0001ABA7
		internal Expression MemberExpression
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001C9AF File Offset: 0x0001ABAF
		internal override Type ResourceType
		{
			get
			{
				return this.resourceType;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001C9B7 File Offset: 0x0001ABB7
		internal bool HasTransparentScope
		{
			get
			{
				return this.transparentScope != null;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001C9C5 File Offset: 0x0001ABC5
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x0001C9CD File Offset: 0x0001ABCD
		internal ResourceSetExpression.TransparentAccessors TransparentScope
		{
			get
			{
				return this.transparentScope;
			}
			set
			{
				this.transparentScope = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001C9D6 File Offset: 0x0001ABD6
		internal bool HasKeyPredicate
		{
			get
			{
				return this.keyPredicateConjuncts.Count > 0;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001C9E6 File Offset: 0x0001ABE6
		internal ReadOnlyCollection<Expression> KeyPredicateConjuncts
		{
			get
			{
				return new ReadOnlyCollection<Expression>(this.keyPredicateConjuncts);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001C9F3 File Offset: 0x0001ABF3
		internal override bool IsSingleton
		{
			get
			{
				return this.keyPredicateConjuncts.Count > 0;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001CA04 File Offset: 0x0001AC04
		internal override bool HasQueryOptions
		{
			get
			{
				return this.sequenceQueryOptions.Count > 0 || this.ExpandPaths.Count > 0 || this.CountOption == CountOption.InlineAll || this.CustomQueryOptions.Count > 0 || base.Projection != null;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001CA52 File Offset: 0x0001AC52
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0001CA5A File Offset: 0x0001AC5A
		internal bool ContainsNonKeyPredicate { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001CA63 File Offset: 0x0001AC63
		internal FilterQueryOptionExpression Filter
		{
			get
			{
				return this.sequenceQueryOptions.OfType<FilterQueryOptionExpression>().SingleOrDefault<FilterQueryOptionExpression>();
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001CA75 File Offset: 0x0001AC75
		internal OrderByQueryOptionExpression OrderBy
		{
			get
			{
				return this.sequenceQueryOptions.OfType<OrderByQueryOptionExpression>().SingleOrDefault<OrderByQueryOptionExpression>();
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001CA87 File Offset: 0x0001AC87
		internal SkipQueryOptionExpression Skip
		{
			get
			{
				return this.sequenceQueryOptions.OfType<SkipQueryOptionExpression>().SingleOrDefault<SkipQueryOptionExpression>();
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001CA99 File Offset: 0x0001AC99
		internal TakeQueryOptionExpression Take
		{
			get
			{
				return this.sequenceQueryOptions.OfType<TakeQueryOptionExpression>().SingleOrDefault<TakeQueryOptionExpression>();
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001CAAB File Offset: 0x0001ACAB
		internal IEnumerable<QueryOptionExpression> SequenceQueryOptions
		{
			get
			{
				return this.sequenceQueryOptions.ToList<QueryOptionExpression>();
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0001CAB8 File Offset: 0x0001ACB8
		internal bool HasSequenceQueryOptions
		{
			get
			{
				return this.sequenceQueryOptions.Count > 0;
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001CAC8 File Offset: 0x0001ACC8
		internal override ResourceExpression CreateCloneWithNewType(Type type)
		{
			return this.CreateCloneWithNewTypes(type, TypeSystem.GetElementType(type));
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001CAD8 File Offset: 0x0001ACD8
		internal ResourceSetExpression CreateCloneForTransparentScope(Type type)
		{
			Type elementType = TypeSystem.GetElementType(type);
			Type type2 = typeof(IOrderedQueryable<>).MakeGenericType(new Type[] { elementType });
			return this.CreateCloneWithNewTypes(type2, this.ResourceType);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001CB15 File Offset: 0x0001AD15
		internal void ConvertKeyToFilterExpression()
		{
			if (this.keyPredicateConjuncts.Count > 0)
			{
				this.AddFilter(this.keyPredicateConjuncts);
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001CB34 File Offset: 0x0001AD34
		internal void AddFilter(IEnumerable<Expression> predicateConjuncts)
		{
			if (this.Skip != null)
			{
				throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("filter", "skip"));
			}
			if (this.Take != null)
			{
				throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("filter", "top"));
			}
			if (base.Projection != null)
			{
				throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("filter", "select"));
			}
			if (this.Filter == null)
			{
				this.AddSequenceQueryOption(new FilterQueryOptionExpression(this.Type));
			}
			this.Filter.AddPredicateConjuncts(predicateConjuncts);
			this.keyPredicateConjuncts.Clear();
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001CBE8 File Offset: 0x0001ADE8
		internal void AddSequenceQueryOption(QueryOptionExpression qoe)
		{
			QueryOptionExpression queryOptionExpression = this.sequenceQueryOptions.Where((QueryOptionExpression o) => o.GetType() == qoe.GetType()).FirstOrDefault<QueryOptionExpression>();
			if (queryOptionExpression != null)
			{
				qoe = qoe.ComposeMultipleSpecification(queryOptionExpression);
				this.sequenceQueryOptions.Remove(queryOptionExpression);
			}
			this.sequenceQueryOptions.Add(qoe);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001CC52 File Offset: 0x0001AE52
		internal void RemoveFilterExpression()
		{
			if (this.Filter != null)
			{
				this.sequenceQueryOptions.Remove(this.Filter);
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001CC70 File Offset: 0x0001AE70
		internal void OverrideInputReference(ResourceSetExpression newInput)
		{
			InputReferenceExpression inputRef = newInput.inputRef;
			if (inputRef != null)
			{
				this.inputRef = inputRef;
				inputRef.OverrideTarget(this);
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001CC98 File Offset: 0x0001AE98
		internal void SetKeyPredicate(IEnumerable<Expression> keyValues)
		{
			this.keyPredicateConjuncts.Clear();
			foreach (Expression expression in keyValues)
			{
				this.keyPredicateConjuncts.Add(expression);
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001CCF0 File Offset: 0x0001AEF0
		internal Dictionary<PropertyInfo, ConstantExpression> GetKeyProperties()
		{
			Dictionary<PropertyInfo, ConstantExpression> dictionary = new Dictionary<PropertyInfo, ConstantExpression>(EqualityComparer<PropertyInfo>.Default);
			if (this.keyPredicateConjuncts.Count > 0)
			{
				foreach (Expression expression in this.keyPredicateConjuncts)
				{
					PropertyInfo propertyInfo;
					ConstantExpression constantExpression;
					if (ResourceBinder.PatternRules.MatchKeyComparison(expression, out propertyInfo, out constantExpression))
					{
						dictionary.Add(propertyInfo, constantExpression);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001CD80 File Offset: 0x0001AF80
		private ResourceSetExpression CreateCloneWithNewTypes(Type newType, Type newResourceType)
		{
			ResourceSetExpression resourceSetExpression = new ResourceSetExpression(newType, this.source, this.MemberExpression, newResourceType, this.ExpandPaths.ToList<string>(), this.CountOption, this.CustomQueryOptions.ToDictionary((KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Key, (KeyValuePair<ConstantExpression, ConstantExpression> kvp) => kvp.Value), base.Projection, base.ResourceTypeAs, base.UriVersion);
			if (this.keyPredicateConjuncts != null && this.keyPredicateConjuncts.Count > 0)
			{
				resourceSetExpression.SetKeyPredicate(this.keyPredicateConjuncts);
			}
			resourceSetExpression.keyFilter = this.keyFilter;
			resourceSetExpression.sequenceQueryOptions = this.sequenceQueryOptions;
			resourceSetExpression.transparentScope = this.transparentScope;
			return resourceSetExpression;
		}

		// Token: 0x04000438 RID: 1080
		private readonly Type resourceType;

		// Token: 0x04000439 RID: 1081
		private readonly Expression member;

		// Token: 0x0400043A RID: 1082
		private Dictionary<PropertyInfo, ConstantExpression> keyFilter;

		// Token: 0x0400043B RID: 1083
		private List<QueryOptionExpression> sequenceQueryOptions;

		// Token: 0x0400043C RID: 1084
		private ResourceSetExpression.TransparentAccessors transparentScope;

		// Token: 0x0400043D RID: 1085
		private readonly List<Expression> keyPredicateConjuncts;

		// Token: 0x020000D8 RID: 216
		[DebuggerDisplay("{ToString()}")]
		internal class TransparentAccessors
		{
			// Token: 0x06000707 RID: 1799 RVA: 0x0001CE50 File Offset: 0x0001B050
			internal TransparentAccessors(string acc, Dictionary<string, Expression> sourceAccesors)
			{
				this.Accessor = acc;
				this.SourceAccessors = sourceAccesors;
			}

			// Token: 0x06000708 RID: 1800 RVA: 0x0001CE68 File Offset: 0x0001B068
			public override string ToString()
			{
				string text = "SourceAccessors=[" + string.Join(",", this.SourceAccessors.Keys.ToArray<string>());
				return text + "] ->* Accessor=" + this.Accessor;
			}

			// Token: 0x04000441 RID: 1089
			internal readonly string Accessor;

			// Token: 0x04000442 RID: 1090
			internal readonly Dictionary<string, Expression> SourceAccessors;
		}
	}
}
