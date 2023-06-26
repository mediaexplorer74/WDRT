using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Data.Services.Client
{
	// Token: 0x020000E9 RID: 233
	internal sealed class BindingGraph
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x00020637 File Offset: 0x0001E837
		public BindingGraph(BindingObserver observer)
		{
			this.observer = observer;
			this.graph = new BindingGraph.Graph();
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00020654 File Offset: 0x0001E854
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public bool AddDataServiceCollection(object source, string sourceProperty, object collection, string collectionEntitySet)
		{
			if (this.graph.ExistsVertex(collection))
			{
				return false;
			}
			BindingGraph.Vertex vertex = this.graph.AddVertex(collection);
			vertex.IsDataServiceCollection = true;
			vertex.EntitySet = collectionEntitySet;
			ICollection collection2 = collection as ICollection;
			if (source != null)
			{
				vertex.Parent = this.graph.LookupVertex(source);
				vertex.ParentProperty = sourceProperty;
				this.graph.AddEdge(source, collection, sourceProperty);
				Type collectionEntityType = BindingUtils.GetCollectionEntityType(collection.GetType());
				if (!typeof(INotifyPropertyChanged).IsAssignableFrom(collectionEntityType))
				{
					throw new InvalidOperationException(Strings.DataBinding_NotifyPropertyChangedNotImpl(collectionEntityType));
				}
				typeof(BindingGraph).GetMethod("SetObserver", false, false).MakeGenericMethod(new Type[] { collectionEntityType }).Invoke(this, new object[] { collection2 });
			}
			else
			{
				this.graph.Root = vertex;
			}
			this.AttachDataServiceCollectionNotification(collection);
			foreach (object obj in collection2)
			{
				this.AddEntity(source, sourceProperty, obj, collectionEntitySet, collection);
			}
			return true;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00020794 File Offset: 0x0001E994
		public void AddPrimitiveOrComplexCollection(object source, string sourceProperty, object collection, Type collectionItemType)
		{
			BindingGraph.Vertex vertex = this.graph.LookupVertex(source);
			if (this.graph.LookupVertex(collection) != null)
			{
				throw new InvalidOperationException(Strings.DataBinding_CollectionAssociatedWithMultipleEntities(collection.GetType()));
			}
			BindingGraph.Vertex vertex2 = this.graph.AddVertex(collection);
			vertex2.Parent = vertex;
			vertex2.ParentProperty = sourceProperty;
			vertex2.IsPrimitiveOrComplexCollection = true;
			vertex2.PrimitiveOrComplexCollectionItemType = collectionItemType;
			this.graph.AddEdge(source, collection, sourceProperty);
			if (!this.AttachPrimitiveOrComplexCollectionNotification(collection))
			{
				throw new InvalidOperationException(Strings.DataBinding_NotifyCollectionChangedNotImpl(collection.GetType()));
			}
			if (PrimitiveType.IsKnownNullableType(collectionItemType))
			{
				return;
			}
			if (!typeof(INotifyPropertyChanged).IsAssignableFrom(collectionItemType))
			{
				throw new InvalidOperationException(Strings.DataBinding_NotifyPropertyChangedNotImpl(collectionItemType));
			}
			this.AddComplexObjectsFromCollection(collection, (IEnumerable)collection);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0002085C File Offset: 0x0001EA5C
		public bool AddEntity(object source, string sourceProperty, object target, string targetEntitySet, object edgeSource)
		{
			BindingGraph.Vertex vertex = this.graph.LookupVertex(edgeSource);
			BindingGraph.Vertex vertex2 = null;
			bool flag = false;
			if (target != null)
			{
				vertex2 = this.graph.LookupVertex(target);
				if (vertex2 == null)
				{
					vertex2 = this.graph.AddVertex(target);
					vertex2.EntitySet = BindingEntityInfo.GetEntitySet(target, targetEntitySet, this.observer.Context.Model);
					if (!this.AttachEntityOrComplexObjectNotification(target))
					{
						throw new InvalidOperationException(Strings.DataBinding_NotifyPropertyChangedNotImpl(target.GetType()));
					}
					flag = true;
				}
				if (this.graph.ExistsEdge(edgeSource, target, vertex.IsDataServiceCollection ? null : sourceProperty))
				{
					throw new InvalidOperationException(Strings.DataBinding_EntityAlreadyInCollection(target.GetType()));
				}
				this.graph.AddEdge(edgeSource, target, vertex.IsDataServiceCollection ? null : sourceProperty);
			}
			if (!vertex.IsDataServiceCollection)
			{
				this.observer.HandleUpdateEntityReference(source, sourceProperty, vertex.EntitySet, target, (vertex2 == null) ? null : vertex2.EntitySet);
			}
			else
			{
				this.observer.HandleAddEntity(source, sourceProperty, (vertex.Parent != null) ? vertex.Parent.EntitySet : null, edgeSource as ICollection, target, vertex2.EntitySet);
			}
			if (flag)
			{
				this.AddFromProperties(target);
			}
			return flag;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000209A8 File Offset: 0x0001EBA8
		public void RemoveDataServiceCollectionItem(object item, object parent, string parentProperty)
		{
			if (this.graph.LookupVertex(item) == null)
			{
				return;
			}
			if (parentProperty != null)
			{
				BindingEntityInfo.BindingPropertyInfo bindingPropertyInfo = BindingEntityInfo.GetObservableProperties(parent.GetType(), this.observer.Context.Model).Single((BindingEntityInfo.BindingPropertyInfo p) => p.PropertyInfo.PropertyName == parentProperty);
				parent = bindingPropertyInfo.PropertyInfo.GetValue(parent);
			}
			object obj = null;
			string text = null;
			string text2 = null;
			string text3 = null;
			this.GetDataServiceCollectionInfo(parent, out obj, out text, out text2, out text3);
			text3 = BindingEntityInfo.GetEntitySet(item, text3, this.observer.Context.Model);
			this.observer.HandleDeleteEntity(obj, text, text2, parent as ICollection, item, text3);
			this.graph.RemoveEdge(parent, item, null);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00020A80 File Offset: 0x0001EC80
		public void RemoveComplexTypeCollectionItem(object item, object collection)
		{
			if (item == null)
			{
				return;
			}
			if (this.graph.LookupVertex(item) == null)
			{
				return;
			}
			this.graph.RemoveEdge(collection, item, null);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00020AB0 File Offset: 0x0001ECB0
		public void RemoveCollection(object source)
		{
			BindingGraph.Vertex vertex = this.graph.LookupVertex(source);
			foreach (BindingGraph.Edge edge in vertex.OutgoingEdges.ToList<BindingGraph.Edge>())
			{
				this.graph.RemoveEdge(source, edge.Target.Item, null);
			}
			this.RemoveUnreachableVertices();
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00020B5C File Offset: 0x0001ED5C
		public void RemoveRelation(object source, string relation)
		{
			BindingGraph.Edge edge = this.graph.LookupVertex(source).OutgoingEdges.SingleOrDefault((BindingGraph.Edge e) => e.Source.Item == source && e.Label == relation);
			if (edge != null)
			{
				this.graph.RemoveEdge(edge.Source.Item, edge.Target.Item, edge.Label);
			}
			this.RemoveUnreachableVertices();
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00020C08 File Offset: 0x0001EE08
		public void RemoveNonTrackedEntities()
		{
			foreach (object obj in this.graph.Select((object o) => BindingEntityInfo.IsEntityType(o.GetType(), this.observer.Context.Model) && !this.observer.IsContextTrackingEntity(o)))
			{
				this.graph.ClearEdgesForVertex(this.graph.LookupVertex(obj));
			}
			this.RemoveUnreachableVertices();
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00020E48 File Offset: 0x0001F048
		public IEnumerable<object> GetDataServiceCollectionItems(object collection)
		{
			BindingGraph.Vertex collectionVertex = this.graph.LookupVertex(collection);
			foreach (BindingGraph.Edge collectionEdge in collectionVertex.OutgoingEdges.ToList<BindingGraph.Edge>())
			{
				yield return collectionEdge.Target.Item;
			}
			yield break;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00020E6C File Offset: 0x0001F06C
		public void Reset()
		{
			this.graph.Reset(new Action<object>(this.DetachNotifications));
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00020E85 File Offset: 0x0001F085
		public void Pause(object collection)
		{
			this.DetachCollectionNotifications(collection);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00020E8E File Offset: 0x0001F08E
		public void Resume(object collection)
		{
			this.AttachDataServiceCollectionNotification(collection);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00020E97 File Offset: 0x0001F097
		public void RemoveUnreachableVertices()
		{
			this.graph.RemoveUnreachableVertices(new Action<object>(this.DetachNotifications));
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00020EB0 File Offset: 0x0001F0B0
		public void GetDataServiceCollectionInfo(object collection, out object source, out string sourceProperty, out string sourceEntitySet, out string targetEntitySet)
		{
			this.graph.LookupVertex(collection).GetDataServiceCollectionInfo(out source, out sourceProperty, out sourceEntitySet, out targetEntitySet);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00020EC9 File Offset: 0x0001F0C9
		public void GetPrimitiveOrComplexCollectionInfo(object collection, out object source, out string sourceProperty, out Type collectionItemType)
		{
			this.graph.LookupVertex(collection).GetPrimitiveOrComplexCollectionInfo(out source, out sourceProperty, out collectionItemType);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00020EE0 File Offset: 0x0001F0E0
		public void GetAncestorEntityForComplexProperty(ref object entity, ref string propertyName, ref object propertyValue)
		{
			BindingGraph.Vertex vertex = this.graph.LookupVertex(entity);
			while (vertex.IsComplex || vertex.IsPrimitiveOrComplexCollection)
			{
				propertyName = vertex.IncomingEdges[0].Label;
				propertyValue = vertex.Item;
				entity = vertex.Parent.Item;
				vertex = vertex.Parent;
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00020F3C File Offset: 0x0001F13C
		public void AddComplexObject(object source, string sourceProperty, object target)
		{
			if (this.graph.LookupVertex(target) != null)
			{
				throw new InvalidOperationException(Strings.DataBinding_ComplexObjectAssociatedWithMultipleEntities(target.GetType()));
			}
			BindingGraph.Vertex vertex = this.graph.LookupVertex(source);
			BindingGraph.Vertex vertex2 = this.graph.AddVertex(target);
			vertex2.Parent = vertex;
			vertex2.IsComplex = true;
			if (!this.AttachEntityOrComplexObjectNotification(target))
			{
				throw new InvalidOperationException(Strings.DataBinding_NotifyPropertyChangedNotImpl(target.GetType()));
			}
			this.graph.AddEdge(source, target, sourceProperty);
			this.AddFromProperties(target);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00020FC4 File Offset: 0x0001F1C4
		public void AddComplexObjectsFromCollection(object collection, IEnumerable collectionItems)
		{
			foreach (object obj in collectionItems)
			{
				if (obj != null)
				{
					this.AddComplexObject(collection, null, obj);
				}
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00021018 File Offset: 0x0001F218
		private void AddFromProperties(object entity)
		{
			foreach (BindingEntityInfo.BindingPropertyInfo bindingPropertyInfo in BindingEntityInfo.GetObservableProperties(entity.GetType(), this.observer.Context.Model))
			{
				object fieldOrPropertyValue = bindingPropertyInfo.PropertyInfo.GetFieldOrPropertyValue(entity);
				if (fieldOrPropertyValue != null)
				{
					switch (bindingPropertyInfo.PropertyKind)
					{
					case BindingPropertyKind.BindingPropertyKindEntity:
						this.AddEntity(entity, bindingPropertyInfo.PropertyInfo.PropertyName, fieldOrPropertyValue, null, entity);
						break;
					case BindingPropertyKind.BindingPropertyKindDataServiceCollection:
						this.AddDataServiceCollection(entity, bindingPropertyInfo.PropertyInfo.PropertyName, fieldOrPropertyValue, null);
						break;
					case BindingPropertyKind.BindingPropertyKindPrimitiveOrComplexCollection:
						this.AddPrimitiveOrComplexCollection(entity, bindingPropertyInfo.PropertyInfo.PropertyName, fieldOrPropertyValue, bindingPropertyInfo.PropertyInfo.PrimitiveOrComplexCollectionItemType);
						break;
					default:
						this.AddComplexObject(entity, bindingPropertyInfo.PropertyInfo.PropertyName, fieldOrPropertyValue);
						break;
					}
				}
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00021108 File Offset: 0x0001F308
		private void AttachDataServiceCollectionNotification(object target)
		{
			INotifyCollectionChanged notifyCollectionChanged = target as INotifyCollectionChanged;
			notifyCollectionChanged.CollectionChanged -= this.observer.OnDataServiceCollectionChanged;
			notifyCollectionChanged.CollectionChanged += this.observer.OnDataServiceCollectionChanged;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0002114C File Offset: 0x0001F34C
		private bool AttachPrimitiveOrComplexCollectionNotification(object collection)
		{
			INotifyCollectionChanged notifyCollectionChanged = collection as INotifyCollectionChanged;
			if (notifyCollectionChanged != null)
			{
				notifyCollectionChanged.CollectionChanged -= this.observer.OnPrimitiveOrComplexCollectionChanged;
				notifyCollectionChanged.CollectionChanged += this.observer.OnPrimitiveOrComplexCollectionChanged;
				return true;
			}
			return false;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00021194 File Offset: 0x0001F394
		private bool AttachEntityOrComplexObjectNotification(object target)
		{
			INotifyPropertyChanged notifyPropertyChanged = target as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				notifyPropertyChanged.PropertyChanged -= this.observer.OnPropertyChanged;
				notifyPropertyChanged.PropertyChanged += this.observer.OnPropertyChanged;
				return true;
			}
			return false;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000211DC File Offset: 0x0001F3DC
		private void DetachNotifications(object target)
		{
			this.DetachCollectionNotifications(target);
			INotifyPropertyChanged notifyPropertyChanged = target as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				notifyPropertyChanged.PropertyChanged -= this.observer.OnPropertyChanged;
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00021214 File Offset: 0x0001F414
		private void DetachCollectionNotifications(object target)
		{
			INotifyCollectionChanged notifyCollectionChanged = target as INotifyCollectionChanged;
			if (notifyCollectionChanged != null)
			{
				notifyCollectionChanged.CollectionChanged -= this.observer.OnDataServiceCollectionChanged;
				notifyCollectionChanged.CollectionChanged -= this.observer.OnPrimitiveOrComplexCollectionChanged;
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0002125C File Offset: 0x0001F45C
		private void SetObserver<T>(ICollection collection)
		{
			DataServiceCollection<T> dataServiceCollection = collection as DataServiceCollection<T>;
			dataServiceCollection.Observer = this.observer;
		}

		// Token: 0x040004AA RID: 1194
		private BindingObserver observer;

		// Token: 0x040004AB RID: 1195
		private BindingGraph.Graph graph;

		// Token: 0x020000EA RID: 234
		internal sealed class Graph
		{
			// Token: 0x060007AA RID: 1962 RVA: 0x0002127C File Offset: 0x0001F47C
			public Graph()
			{
				this.vertices = new Dictionary<object, BindingGraph.Vertex>(ReferenceEqualityComparer<object>.Instance);
			}

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x060007AB RID: 1963 RVA: 0x00021294 File Offset: 0x0001F494
			// (set) Token: 0x060007AC RID: 1964 RVA: 0x0002129C File Offset: 0x0001F49C
			public BindingGraph.Vertex Root
			{
				get
				{
					return this.root;
				}
				set
				{
					this.root = value;
				}
			}

			// Token: 0x060007AD RID: 1965 RVA: 0x000212A8 File Offset: 0x0001F4A8
			public BindingGraph.Vertex AddVertex(object item)
			{
				BindingGraph.Vertex vertex = new BindingGraph.Vertex(item);
				this.vertices.Add(item, vertex);
				return vertex;
			}

			// Token: 0x060007AE RID: 1966 RVA: 0x000212CC File Offset: 0x0001F4CC
			public void ClearEdgesForVertex(BindingGraph.Vertex v)
			{
				foreach (BindingGraph.Edge edge in v.OutgoingEdges.Concat(v.IncomingEdges).ToList<BindingGraph.Edge>())
				{
					this.RemoveEdge(edge.Source.Item, edge.Target.Item, edge.Label);
				}
			}

			// Token: 0x060007AF RID: 1967 RVA: 0x0002134C File Offset: 0x0001F54C
			public bool ExistsVertex(object item)
			{
				BindingGraph.Vertex vertex;
				return this.vertices.TryGetValue(item, out vertex);
			}

			// Token: 0x060007B0 RID: 1968 RVA: 0x00021368 File Offset: 0x0001F568
			public BindingGraph.Vertex LookupVertex(object item)
			{
				BindingGraph.Vertex vertex;
				this.vertices.TryGetValue(item, out vertex);
				return vertex;
			}

			// Token: 0x060007B1 RID: 1969 RVA: 0x00021388 File Offset: 0x0001F588
			public BindingGraph.Edge AddEdge(object source, object target, string label)
			{
				BindingGraph.Vertex vertex = this.vertices[source];
				BindingGraph.Vertex vertex2 = this.vertices[target];
				BindingGraph.Edge edge = new BindingGraph.Edge(vertex, vertex2, label);
				vertex.OutgoingEdges.Add(edge);
				vertex2.IncomingEdges.Add(edge);
				return edge;
			}

			// Token: 0x060007B2 RID: 1970 RVA: 0x000213D4 File Offset: 0x0001F5D4
			public void RemoveEdge(object source, object target, string label)
			{
				BindingGraph.Vertex vertex = this.vertices[source];
				BindingGraph.Vertex vertex2 = this.vertices[target];
				BindingGraph.Edge edge = new BindingGraph.Edge(vertex, vertex2, label);
				vertex.OutgoingEdges.Remove(edge);
				vertex2.IncomingEdges.Remove(edge);
			}

			// Token: 0x060007B3 RID: 1971 RVA: 0x00021420 File Offset: 0x0001F620
			public bool ExistsEdge(object source, object target, string label)
			{
				BindingGraph.Edge edge = new BindingGraph.Edge(this.vertices[source], this.vertices[target], label);
				return this.vertices[source].OutgoingEdges.Contains(edge);
			}

			// Token: 0x060007B4 RID: 1972 RVA: 0x00021463 File Offset: 0x0001F663
			public IList<object> Select(Func<object, bool> filter)
			{
				return this.vertices.Keys.Where(filter).ToList<object>();
			}

			// Token: 0x060007B5 RID: 1973 RVA: 0x0002147C File Offset: 0x0001F67C
			public void Reset(Action<object> action)
			{
				foreach (object obj in this.vertices.Keys)
				{
					action(obj);
				}
				this.vertices.Clear();
			}

			// Token: 0x060007B6 RID: 1974 RVA: 0x000214E0 File Offset: 0x0001F6E0
			public void RemoveUnreachableVertices(Action<object> detachAction)
			{
				try
				{
					foreach (BindingGraph.Vertex vertex in this.UnreachableVertices())
					{
						this.ClearEdgesForVertex(vertex);
						detachAction(vertex.Item);
						this.vertices.Remove(vertex.Item);
					}
				}
				finally
				{
					foreach (BindingGraph.Vertex vertex2 in this.vertices.Values)
					{
						vertex2.Color = VertexColor.White;
					}
				}
			}

			// Token: 0x060007B7 RID: 1975 RVA: 0x000215B0 File Offset: 0x0001F7B0
			private IEnumerable<BindingGraph.Vertex> UnreachableVertices()
			{
				Queue<BindingGraph.Vertex> queue = new Queue<BindingGraph.Vertex>();
				this.Root.Color = VertexColor.Gray;
				queue.Enqueue(this.Root);
				while (queue.Count != 0)
				{
					BindingGraph.Vertex vertex = queue.Dequeue();
					foreach (BindingGraph.Edge edge in vertex.OutgoingEdges)
					{
						if (edge.Target.Color == VertexColor.White)
						{
							edge.Target.Color = VertexColor.Gray;
							queue.Enqueue(edge.Target);
						}
					}
					vertex.Color = VertexColor.Black;
				}
				return this.vertices.Values.Where((BindingGraph.Vertex v) => v.Color == VertexColor.White).ToList<BindingGraph.Vertex>();
			}

			// Token: 0x040004AC RID: 1196
			private Dictionary<object, BindingGraph.Vertex> vertices;

			// Token: 0x040004AD RID: 1197
			private BindingGraph.Vertex root;
		}

		// Token: 0x020000EB RID: 235
		internal sealed class Vertex
		{
			// Token: 0x060007B9 RID: 1977 RVA: 0x0002168C File Offset: 0x0001F88C
			public Vertex(object item)
			{
				this.Item = item;
				this.Color = VertexColor.White;
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x060007BA RID: 1978 RVA: 0x000216A2 File Offset: 0x0001F8A2
			// (set) Token: 0x060007BB RID: 1979 RVA: 0x000216AA File Offset: 0x0001F8AA
			public object Item { get; private set; }

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x060007BC RID: 1980 RVA: 0x000216B3 File Offset: 0x0001F8B3
			// (set) Token: 0x060007BD RID: 1981 RVA: 0x000216BB File Offset: 0x0001F8BB
			public string EntitySet { get; set; }

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x060007BE RID: 1982 RVA: 0x000216C4 File Offset: 0x0001F8C4
			// (set) Token: 0x060007BF RID: 1983 RVA: 0x000216CC File Offset: 0x0001F8CC
			public bool IsDataServiceCollection { get; set; }

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x060007C0 RID: 1984 RVA: 0x000216D5 File Offset: 0x0001F8D5
			// (set) Token: 0x060007C1 RID: 1985 RVA: 0x000216DD File Offset: 0x0001F8DD
			public bool IsComplex { get; set; }

			// Token: 0x170001BB RID: 443
			// (get) Token: 0x060007C2 RID: 1986 RVA: 0x000216E6 File Offset: 0x0001F8E6
			// (set) Token: 0x060007C3 RID: 1987 RVA: 0x000216EE File Offset: 0x0001F8EE
			public bool IsPrimitiveOrComplexCollection { get; set; }

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x060007C4 RID: 1988 RVA: 0x000216F7 File Offset: 0x0001F8F7
			// (set) Token: 0x060007C5 RID: 1989 RVA: 0x000216FF File Offset: 0x0001F8FF
			public Type PrimitiveOrComplexCollectionItemType { get; set; }

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00021708 File Offset: 0x0001F908
			// (set) Token: 0x060007C7 RID: 1991 RVA: 0x00021710 File Offset: 0x0001F910
			public BindingGraph.Vertex Parent { get; set; }

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00021719 File Offset: 0x0001F919
			// (set) Token: 0x060007C9 RID: 1993 RVA: 0x00021721 File Offset: 0x0001F921
			public string ParentProperty { get; set; }

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x060007CA RID: 1994 RVA: 0x0002172A File Offset: 0x0001F92A
			public bool IsRootDataServiceCollection
			{
				get
				{
					return this.IsDataServiceCollection && this.Parent == null;
				}
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x060007CB RID: 1995 RVA: 0x0002173F File Offset: 0x0001F93F
			// (set) Token: 0x060007CC RID: 1996 RVA: 0x00021747 File Offset: 0x0001F947
			public VertexColor Color { get; set; }

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x060007CD RID: 1997 RVA: 0x00021750 File Offset: 0x0001F950
			public IList<BindingGraph.Edge> IncomingEdges
			{
				get
				{
					if (this.incomingEdges == null)
					{
						this.incomingEdges = new List<BindingGraph.Edge>();
					}
					return this.incomingEdges;
				}
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x060007CE RID: 1998 RVA: 0x0002176B File Offset: 0x0001F96B
			public HashSet<BindingGraph.Edge> OutgoingEdges
			{
				get
				{
					if (this.outgoingEdges == null)
					{
						this.outgoingEdges = new HashSet<BindingGraph.Edge>(BindingGraph.Vertex.edgeComparer);
					}
					return this.outgoingEdges;
				}
			}

			// Token: 0x060007CF RID: 1999 RVA: 0x0002178B File Offset: 0x0001F98B
			public void GetDataServiceCollectionInfo(out object source, out string sourceProperty, out string sourceEntitySet, out string targetEntitySet)
			{
				if (!this.IsRootDataServiceCollection)
				{
					source = this.Parent.Item;
					sourceProperty = this.ParentProperty;
					sourceEntitySet = this.Parent.EntitySet;
				}
				else
				{
					source = null;
					sourceProperty = null;
					sourceEntitySet = null;
				}
				targetEntitySet = this.EntitySet;
			}

			// Token: 0x060007D0 RID: 2000 RVA: 0x000217CB File Offset: 0x0001F9CB
			public void GetPrimitiveOrComplexCollectionInfo(out object source, out string sourceProperty, out Type collectionItemType)
			{
				source = this.Parent.Item;
				sourceProperty = this.ParentProperty;
				collectionItemType = this.PrimitiveOrComplexCollectionItemType;
			}

			// Token: 0x040004AF RID: 1199
			private List<BindingGraph.Edge> incomingEdges;

			// Token: 0x040004B0 RID: 1200
			private HashSet<BindingGraph.Edge> outgoingEdges;

			// Token: 0x040004B1 RID: 1201
			private static BindingGraph.EdgeComparer edgeComparer = new BindingGraph.EdgeComparer();
		}

		// Token: 0x020000EC RID: 236
		internal sealed class Edge : IEquatable<BindingGraph.Edge>
		{
			// Token: 0x060007D2 RID: 2002 RVA: 0x000217F6 File Offset: 0x0001F9F6
			public Edge(BindingGraph.Vertex source, BindingGraph.Vertex target, string label)
			{
				this.Source = source;
				this.Target = target;
				this.Label = label;
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00021813 File Offset: 0x0001FA13
			// (set) Token: 0x060007D4 RID: 2004 RVA: 0x0002181B File Offset: 0x0001FA1B
			public BindingGraph.Vertex Source { get; private set; }

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00021824 File Offset: 0x0001FA24
			// (set) Token: 0x060007D6 RID: 2006 RVA: 0x0002182C File Offset: 0x0001FA2C
			public BindingGraph.Vertex Target { get; private set; }

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00021835 File Offset: 0x0001FA35
			// (set) Token: 0x060007D8 RID: 2008 RVA: 0x0002183D File Offset: 0x0001FA3D
			public string Label { get; private set; }

			// Token: 0x060007D9 RID: 2009 RVA: 0x00021846 File Offset: 0x0001FA46
			public bool Equals(BindingGraph.Edge other)
			{
				return other != null && object.ReferenceEquals(this.Source, other.Source) && object.ReferenceEquals(this.Target, other.Target) && this.Label == other.Label;
			}
		}

		// Token: 0x020000ED RID: 237
		private class EdgeComparer : IEqualityComparer<BindingGraph.Edge>
		{
			// Token: 0x060007DA RID: 2010 RVA: 0x00021884 File Offset: 0x0001FA84
			public bool Equals(BindingGraph.Edge x, BindingGraph.Edge y)
			{
				if (x == null)
				{
					return y == null;
				}
				return x.Equals(y);
			}

			// Token: 0x060007DB RID: 2011 RVA: 0x00021898 File Offset: 0x0001FA98
			public int GetHashCode(BindingGraph.Edge obj)
			{
				if (obj == null)
				{
					return 0;
				}
				int num = 17;
				num = num * 23 + ((obj.Source == null) ? 0 : obj.Source.GetHashCode());
				num = num * 23 + ((obj.Target == null) ? 0 : obj.Target.GetHashCode());
				return num * 23 + ((obj.Label == null) ? 0 : obj.Label.GetHashCode());
			}
		}
	}
}
