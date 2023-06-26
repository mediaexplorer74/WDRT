using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Services.Client.Materialization;
using System.Data.Services.Client.Metadata;
using System.IO;
using System.Xml;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000113 RID: 275
	internal class MaterializeAtom : IDisposable, IEnumerable, IEnumerator
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x00024CE8 File Offset: 0x00022EE8
		internal MaterializeAtom(ResponseInfo responseInfo, QueryComponents queryComponents, ProjectionPlan plan, IODataResponseMessage responseMessage, ODataPayloadKind payloadKind)
		{
			this.responseInfo = responseInfo;
			this.elementType = queryComponents.LastSegmentType;
			this.expectingPrimitiveValue = PrimitiveType.IsKnownNullableType(this.elementType);
			this.responseMessage = responseMessage;
			Type type;
			Type typeForMaterializer = MaterializeAtom.GetTypeForMaterializer(this.expectingPrimitiveValue, this.elementType, responseInfo.Model, out type);
			this.materializer = ODataMaterializer.CreateMaterializerForMessage(responseMessage, responseInfo, typeForMaterializer, queryComponents, plan, payloadKind);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00024D54 File Offset: 0x00022F54
		internal MaterializeAtom(ResponseInfo responseInfo, IEnumerable<ODataEntry> entries, Type elementType, ODataFormat format)
		{
			this.responseInfo = responseInfo;
			this.elementType = elementType;
			this.expectingPrimitiveValue = PrimitiveType.IsKnownNullableType(elementType);
			Type type;
			Type typeForMaterializer = MaterializeAtom.GetTypeForMaterializer(this.expectingPrimitiveValue, this.elementType, responseInfo.Model, out type);
			QueryComponents queryComponents = new QueryComponents(null, Util.DataServiceVersionEmpty, elementType, null, null);
			ODataMaterializerContext odataMaterializerContext = new ODataMaterializerContext(responseInfo);
			EntityTrackingAdapter entityTrackingAdapter = new EntityTrackingAdapter(responseInfo.EntityTracker, responseInfo.MergeOption, responseInfo.Model, responseInfo.Context);
			this.materializer = new ODataEntriesEntityMaterializer(entries, odataMaterializerContext, entityTrackingAdapter, queryComponents, typeForMaterializer, null, format);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00024DE4 File Offset: 0x00022FE4
		private MaterializeAtom()
		{
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00024DEC File Offset: 0x00022FEC
		public object Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00024E01 File Offset: 0x00023001
		internal static MaterializeAtom EmptyResults
		{
			get
			{
				return new MaterializeAtom.ResultsWrapper(null, null, null);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00024E0B File Offset: 0x0002300B
		internal bool IsCountable
		{
			get
			{
				return this.materializer != null && this.materializer.IsCountable;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00024E22 File Offset: 0x00023022
		internal virtual DataServiceContext Context
		{
			get
			{
				return this.responseInfo.Context;
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00024E30 File Offset: 0x00023030
		public void Dispose()
		{
			this.current = null;
			if (this.materializer != null)
			{
				this.materializer.Dispose();
			}
			if (this.writer != null)
			{
				this.writer.Dispose();
			}
			if (this.responseMessage != null)
			{
				WebUtil.DisposeMessage(this.responseMessage);
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00024E83 File Offset: 0x00023083
		public virtual IEnumerator GetEnumerator()
		{
			this.CheckGetEnumerator();
			return this;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00024E8C File Offset: 0x0002308C
		private static Type GetTypeForMaterializer(bool expectingPrimitiveValue, Type elementType, ClientEdmModel model, out Type implementationType)
		{
			if (!expectingPrimitiveValue && typeof(IEnumerable).IsAssignableFrom(elementType))
			{
				implementationType = ClientTypeUtil.GetImplementationType(elementType, typeof(ICollection<>));
				if (implementationType != null)
				{
					Type type = implementationType.GetGenericArguments()[0];
					if (ClientTypeUtil.TypeIsEntity(type, model))
					{
						return type;
					}
				}
			}
			implementationType = null;
			return elementType;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00024EE4 File Offset: 0x000230E4
		public bool MoveNext()
		{
			bool applyingChanges = this.responseInfo.ApplyingChanges;
			bool flag;
			try
			{
				this.responseInfo.ApplyingChanges = true;
				flag = this.MoveNextInternal();
			}
			finally
			{
				this.responseInfo.ApplyingChanges = applyingChanges;
			}
			return flag;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00024F30 File Offset: 0x00023130
		private bool MoveNextInternal()
		{
			if (this.materializer == null)
			{
				return false;
			}
			this.current = null;
			this.materializer.ClearLog();
			bool flag = false;
			Type type;
			MaterializeAtom.GetTypeForMaterializer(this.expectingPrimitiveValue, this.elementType, this.responseInfo.Model, out type);
			if (type != null)
			{
				if (this.moved)
				{
					return false;
				}
				Type type2 = type.GetGenericArguments()[0];
				type = this.elementType;
				if (type.IsInterface())
				{
					type = typeof(Collection<>).MakeGenericType(new Type[] { type2 });
				}
				IList list = (IList)Activator.CreateInstance(type);
				while (this.materializer.Read())
				{
					list.Add(this.materializer.CurrentValue);
				}
				this.moved = true;
				this.current = list;
				flag = true;
			}
			if (this.current == null)
			{
				if (this.expectingPrimitiveValue && this.moved)
				{
					flag = false;
				}
				else
				{
					flag = this.materializer.Read();
					if (flag)
					{
						this.current = this.materializer.CurrentValue;
					}
					this.moved = true;
				}
			}
			this.materializer.ApplyLogToContext();
			return flag;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00025050 File Offset: 0x00023250
		void IEnumerator.Reset()
		{
			throw Error.NotSupported();
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00025057 File Offset: 0x00023257
		internal static MaterializeAtom CreateWrapper(DataServiceContext context, IEnumerable results)
		{
			return new MaterializeAtom.ResultsWrapper(context, results, null);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00025061 File Offset: 0x00023261
		internal static MaterializeAtom CreateWrapper(DataServiceContext context, IEnumerable results, DataServiceQueryContinuation continuation)
		{
			return new MaterializeAtom.ResultsWrapper(context, results, continuation);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0002506B File Offset: 0x0002326B
		internal void SetInsertingObject(object addedObject)
		{
			((ODataEntityMaterializer)this.materializer).TargetInstance = addedObject;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0002507E File Offset: 0x0002327E
		internal long CountValue()
		{
			return this.materializer.CountValue;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0002508C File Offset: 0x0002328C
		internal virtual DataServiceQueryContinuation GetContinuation(IEnumerable key)
		{
			DataServiceQueryContinuation dataServiceQueryContinuation;
			if (key == null)
			{
				if ((this.expectingPrimitiveValue && !this.moved) || (!this.expectingPrimitiveValue && !this.materializer.IsEndOfStream))
				{
					throw new InvalidOperationException(Strings.MaterializeFromAtom_TopLevelLinkNotAvailable);
				}
				if (this.expectingPrimitiveValue || this.materializer.CurrentFeed == null)
				{
					dataServiceQueryContinuation = null;
				}
				else
				{
					dataServiceQueryContinuation = DataServiceQueryContinuation.Create(this.materializer.CurrentFeed.NextPageLink, this.materializer.MaterializeEntryPlan);
				}
			}
			else if (!this.materializer.NextLinkTable.TryGetValue(key, out dataServiceQueryContinuation))
			{
				throw new ArgumentException(Strings.MaterializeFromAtom_CollectionKeyNotPresentInLinkTable);
			}
			return dataServiceQueryContinuation;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00025129 File Offset: 0x00023329
		private void CheckGetEnumerator()
		{
			if (this.calledGetEnumerator)
			{
				throw Error.NotSupported(Strings.Deserialize_GetEnumerator);
			}
			this.calledGetEnumerator = true;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00025148 File Offset: 0x00023348
		internal static string ReadElementString(XmlReader reader, bool checkNullAttribute)
		{
			string text = null;
			bool flag = checkNullAttribute && !Util.DoesNullAttributeSayTrue(reader);
			if (!reader.IsEmptyElement)
			{
				while (reader.Read())
				{
					XmlNodeType nodeType = reader.NodeType;
					switch (nodeType)
					{
					case XmlNodeType.Element:
					case XmlNodeType.Attribute:
						goto IL_86;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						break;
					default:
						if (nodeType == XmlNodeType.Comment)
						{
							continue;
						}
						switch (nodeType)
						{
						case XmlNodeType.Whitespace:
							continue;
						case XmlNodeType.SignificantWhitespace:
							break;
						case XmlNodeType.EndElement:
						{
							string text2;
							if ((text2 = text) == null)
							{
								if (!flag)
								{
									return null;
								}
								text2 = string.Empty;
							}
							return text2;
						}
						default:
							goto IL_86;
						}
						break;
					}
					if (text != null)
					{
						throw Error.InvalidOperation(Strings.Deserialize_MixedTextWithComment);
					}
					text = reader.Value;
					continue;
					IL_86:
					throw Error.InvalidOperation(Strings.Deserialize_ExpectingSimpleValue);
				}
				throw Error.InvalidOperation(Strings.Deserialize_ExpectingSimpleValue);
			}
			if (!flag)
			{
				return null;
			}
			return string.Empty;
		}

		// Token: 0x04000546 RID: 1350
		private readonly ResponseInfo responseInfo;

		// Token: 0x04000547 RID: 1351
		private readonly Type elementType;

		// Token: 0x04000548 RID: 1352
		private readonly bool expectingPrimitiveValue;

		// Token: 0x04000549 RID: 1353
		private readonly ODataMaterializer materializer;

		// Token: 0x0400054A RID: 1354
		private object current;

		// Token: 0x0400054B RID: 1355
		private bool calledGetEnumerator;

		// Token: 0x0400054C RID: 1356
		private bool moved;

		// Token: 0x0400054D RID: 1357
		private IODataResponseMessage responseMessage;

		// Token: 0x0400054E RID: 1358
		private TextWriter writer;

		// Token: 0x02000114 RID: 276
		private class ResultsWrapper : MaterializeAtom
		{
			// Token: 0x06000910 RID: 2320 RVA: 0x000251F8 File Offset: 0x000233F8
			internal ResultsWrapper(DataServiceContext context, IEnumerable results, DataServiceQueryContinuation continuation)
			{
				this.context = context;
				this.results = results ?? new object[0];
				this.continuation = continuation;
			}

			// Token: 0x17000217 RID: 535
			// (get) Token: 0x06000911 RID: 2321 RVA: 0x0002521F File Offset: 0x0002341F
			internal override DataServiceContext Context
			{
				get
				{
					return this.context;
				}
			}

			// Token: 0x06000912 RID: 2322 RVA: 0x00025227 File Offset: 0x00023427
			internal override DataServiceQueryContinuation GetContinuation(IEnumerable key)
			{
				if (key == null)
				{
					return this.continuation;
				}
				throw new InvalidOperationException(Strings.MaterializeFromAtom_GetNestLinkForFlatCollection);
			}

			// Token: 0x06000913 RID: 2323 RVA: 0x0002523D File Offset: 0x0002343D
			public override IEnumerator GetEnumerator()
			{
				return this.results.GetEnumerator();
			}

			// Token: 0x0400054F RID: 1359
			private readonly IEnumerable results;

			// Token: 0x04000550 RID: 1360
			private readonly DataServiceQueryContinuation continuation;

			// Token: 0x04000551 RID: 1361
			private readonly DataServiceContext context;
		}
	}
}
