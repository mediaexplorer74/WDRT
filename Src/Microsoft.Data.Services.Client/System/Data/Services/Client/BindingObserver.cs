using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Services.Client.Metadata;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Data.Services.Client
{
	// Token: 0x020000EE RID: 238
	internal sealed class BindingObserver
	{
		// Token: 0x060007DD RID: 2013 RVA: 0x0002190A File Offset: 0x0001FB0A
		internal BindingObserver(DataServiceContext context, Func<EntityChangedParams, bool> entityChanged, Func<EntityCollectionChangedParams, bool> collectionChanged)
		{
			this.Context = context;
			this.Context.ChangesSaved += this.OnChangesSaved;
			this.EntityChanged = entityChanged;
			this.CollectionChanged = collectionChanged;
			this.bindingGraph = new BindingGraph(this);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0002194A File Offset: 0x0001FB4A
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x00021952 File Offset: 0x0001FB52
		internal DataServiceContext Context { get; private set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0002195B File Offset: 0x0001FB5B
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x00021963 File Offset: 0x0001FB63
		internal bool AttachBehavior { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0002196C File Offset: 0x0001FB6C
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x00021974 File Offset: 0x0001FB74
		internal bool DetachBehavior { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0002197D File Offset: 0x0001FB7D
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x00021985 File Offset: 0x0001FB85
		internal Func<EntityChangedParams, bool> EntityChanged { get; private set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0002198E File Offset: 0x0001FB8E
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x00021996 File Offset: 0x0001FB96
		internal Func<EntityCollectionChangedParams, bool> CollectionChanged { get; private set; }

		// Token: 0x060007E8 RID: 2024 RVA: 0x0002199F File Offset: 0x0001FB9F
		internal void PauseTracking(object collection)
		{
			this.bindingGraph.Pause(collection);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000219AD File Offset: 0x0001FBAD
		internal void ResumeTracking(object collection)
		{
			this.bindingGraph.Resume(collection);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000219BC File Offset: 0x0001FBBC
		internal void StartTracking<T>(DataServiceCollection<T> collection, string collectionEntitySet)
		{
			try
			{
				this.AttachBehavior = true;
				this.bindingGraph.AddDataServiceCollection(null, null, collection, collectionEntitySet);
			}
			finally
			{
				this.AttachBehavior = false;
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000219FC File Offset: 0x0001FBFC
		internal void StopTracking()
		{
			this.bindingGraph.Reset();
			this.Context.ChangesSaved -= this.OnChangesSaved;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00021A20 File Offset: 0x0001FC20
		internal bool LookupParent<T>(DataServiceCollection<T> collection, out object parentEntity, out string parentProperty)
		{
			string text;
			string text2;
			this.bindingGraph.GetDataServiceCollectionInfo(collection, out parentEntity, out parentProperty, out text, out text2);
			return parentEntity != null;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00021A48 File Offset: 0x0001FC48
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal void OnPropertyChanged(object source, PropertyChangedEventArgs eventArgs)
		{
			Util.CheckArgumentNull<object>(source, "source");
			Util.CheckArgumentNull<PropertyChangedEventArgs>(eventArgs, "eventArgs");
			string propertyName = eventArgs.PropertyName;
			if (string.IsNullOrEmpty(propertyName))
			{
				this.HandleUpdateEntity(source, null, null);
				return;
			}
			BindingEntityInfo.BindingPropertyInfo bindingPropertyInfo;
			ClientPropertyAnnotation clientPropertyAnnotation;
			object obj;
			if (BindingEntityInfo.TryGetPropertyValue(source, propertyName, this.Context.Model, out bindingPropertyInfo, out clientPropertyAnnotation, out obj))
			{
				if (bindingPropertyInfo != null)
				{
					this.bindingGraph.RemoveRelation(source, propertyName);
					switch (bindingPropertyInfo.PropertyKind)
					{
					case BindingPropertyKind.BindingPropertyKindEntity:
						this.bindingGraph.AddEntity(source, propertyName, obj, null, source);
						return;
					case BindingPropertyKind.BindingPropertyKindDataServiceCollection:
						if (obj == null)
						{
							return;
						}
						try
						{
							typeof(BindingUtils).GetMethod("VerifyObserverNotPresent", false, true).MakeGenericMethod(new Type[] { bindingPropertyInfo.PropertyInfo.EntityCollectionItemType }).Invoke(null, new object[]
							{
								obj,
								propertyName,
								source.GetType()
							});
						}
						catch (TargetInvocationException ex)
						{
							throw ex.InnerException;
						}
						try
						{
							this.AttachBehavior = true;
							this.bindingGraph.AddDataServiceCollection(source, propertyName, obj, null);
							return;
						}
						finally
						{
							this.AttachBehavior = false;
						}
						break;
					case BindingPropertyKind.BindingPropertyKindPrimitiveOrComplexCollection:
						break;
					default:
						if (obj != null)
						{
							this.bindingGraph.AddComplexObject(source, propertyName, obj);
						}
						this.HandleUpdateEntity(source, propertyName, obj);
						return;
					}
					if (obj != null)
					{
						this.bindingGraph.AddPrimitiveOrComplexCollection(source, propertyName, obj, bindingPropertyInfo.PropertyInfo.PrimitiveOrComplexCollectionItemType);
					}
					this.HandleUpdateEntity(source, propertyName, obj);
					return;
				}
				if (!clientPropertyAnnotation.IsStreamLinkProperty)
				{
					this.HandleUpdateEntity(source, propertyName, obj);
				}
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00021BE4 File Offset: 0x0001FDE4
		internal void OnDataServiceCollectionChanged(object collection, NotifyCollectionChangedEventArgs eventArgs)
		{
			Util.CheckArgumentNull<object>(collection, "collection");
			Util.CheckArgumentNull<NotifyCollectionChangedEventArgs>(eventArgs, "eventArgs");
			object obj;
			string text;
			string text2;
			string text3;
			this.bindingGraph.GetDataServiceCollectionInfo(collection, out obj, out text, out text2, out text3);
			switch (eventArgs.Action)
			{
			case NotifyCollectionChangedAction.Add:
				this.OnAddToDataServiceCollection(eventArgs, obj, text, text3, collection);
				return;
			case NotifyCollectionChangedAction.Remove:
				this.OnRemoveFromDataServiceCollection(eventArgs, obj, text, collection);
				return;
			case NotifyCollectionChangedAction.Replace:
				this.OnRemoveFromDataServiceCollection(eventArgs, obj, text, collection);
				this.OnAddToDataServiceCollection(eventArgs, obj, text, text3, collection);
				return;
			case NotifyCollectionChangedAction.Move:
				return;
			case NotifyCollectionChangedAction.Reset:
				if (this.DetachBehavior)
				{
					this.RemoveWithDetachDataServiceCollection(collection);
					return;
				}
				this.bindingGraph.RemoveCollection(collection);
				return;
			default:
				throw new InvalidOperationException(Strings.DataBinding_DataServiceCollectionChangedUnknownActionCollection(eventArgs.Action));
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00021CA4 File Offset: 0x0001FEA4
		internal void OnDataServiceCollectionBulkAdded(object collection, IEnumerable newItems)
		{
			Util.CheckArgumentNull<object>(collection, "collection");
			Util.CheckArgumentNull<IEnumerable>(newItems, "newItems");
			object obj;
			string text;
			string text2;
			string text3;
			this.bindingGraph.GetDataServiceCollectionInfo(collection, out obj, out text, out text2, out text3);
			foreach (object obj2 in newItems)
			{
				if (obj2 == null)
				{
					throw new InvalidOperationException(Strings.DataBinding_BindingOperation_ArrayItemNull("Add"));
				}
				this.bindingGraph.AddEntity(obj, text, obj2, text3, collection);
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00021D48 File Offset: 0x0001FF48
		internal void OnPrimitiveOrComplexCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Util.CheckArgumentNull<object>(sender, "sender");
			Util.CheckArgumentNull<NotifyCollectionChangedEventArgs>(e, "e");
			object obj;
			string text;
			Type type;
			this.bindingGraph.GetPrimitiveOrComplexCollectionInfo(sender, out obj, out text, out type);
			if (!PrimitiveType.IsKnownNullableType(type))
			{
				switch (e.Action)
				{
				case NotifyCollectionChangedAction.Add:
					this.OnAddToComplexTypeCollection(sender, e.NewItems);
					break;
				case NotifyCollectionChangedAction.Remove:
					this.OnRemoveFromComplexTypeCollection(sender, e.OldItems);
					break;
				case NotifyCollectionChangedAction.Replace:
					this.OnRemoveFromComplexTypeCollection(sender, e.OldItems);
					this.OnAddToComplexTypeCollection(sender, e.NewItems);
					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Reset:
					this.bindingGraph.RemoveCollection(sender);
					break;
				default:
					throw new InvalidOperationException(Strings.DataBinding_CollectionChangedUnknownActionCollection(e.Action, sender.GetType()));
				}
			}
			this.HandleUpdateEntity(obj, text, sender);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00021E1C File Offset: 0x0002001C
		internal void HandleAddEntity(object source, string sourceProperty, string sourceEntitySet, ICollection collection, object target, string targetEntitySet)
		{
			if (this.Context.ApplyingChanges)
			{
				return;
			}
			if (source != null && this.IsDetachedOrDeletedFromContext(source))
			{
				return;
			}
			EntityDescriptor entityDescriptor = this.Context.GetEntityDescriptor(target);
			bool flag = !this.AttachBehavior && (entityDescriptor == null || (source != null && !this.IsContextTrackingLink(source, sourceProperty, target) && entityDescriptor.State != EntityStates.Deleted));
			if (flag && this.CollectionChanged != null)
			{
				EntityCollectionChangedParams entityCollectionChangedParams = new EntityCollectionChangedParams(this.Context, source, sourceProperty, sourceEntitySet, collection, target, targetEntitySet, NotifyCollectionChangedAction.Add);
				if (this.CollectionChanged(entityCollectionChangedParams))
				{
					return;
				}
			}
			if (source != null && this.IsDetachedOrDeletedFromContext(source))
			{
				throw new InvalidOperationException(Strings.DataBinding_BindingOperation_DetachedSource);
			}
			entityDescriptor = this.Context.GetEntityDescriptor(target);
			if (source != null)
			{
				if (this.AttachBehavior)
				{
					if (entityDescriptor == null)
					{
						BindingUtils.ValidateEntitySetName(targetEntitySet, target);
						this.Context.AttachTo(targetEntitySet, target);
						this.Context.AttachLink(source, sourceProperty, target);
						return;
					}
					if (entityDescriptor.State != EntityStates.Deleted && !this.IsContextTrackingLink(source, sourceProperty, target))
					{
						this.Context.AttachLink(source, sourceProperty, target);
						return;
					}
				}
				else
				{
					if (entityDescriptor == null)
					{
						this.Context.AddRelatedObject(source, sourceProperty, target);
						return;
					}
					if (entityDescriptor.State != EntityStates.Deleted && !this.IsContextTrackingLink(source, sourceProperty, target))
					{
						this.Context.AddLink(source, sourceProperty, target);
						return;
					}
				}
			}
			else if (entityDescriptor == null)
			{
				BindingUtils.ValidateEntitySetName(targetEntitySet, target);
				if (this.AttachBehavior)
				{
					this.Context.AttachTo(targetEntitySet, target);
					return;
				}
				this.Context.AddObject(targetEntitySet, target);
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00021FA4 File Offset: 0x000201A4
		internal void HandleDeleteEntity(object source, string sourceProperty, string sourceEntitySet, ICollection collection, object target, string targetEntitySet)
		{
			if (this.Context.ApplyingChanges)
			{
				return;
			}
			if (source != null && this.IsDetachedOrDeletedFromContext(source))
			{
				return;
			}
			bool flag = this.IsContextTrackingEntity(target) && !this.DetachBehavior;
			if (flag && this.CollectionChanged != null)
			{
				EntityCollectionChangedParams entityCollectionChangedParams = new EntityCollectionChangedParams(this.Context, source, sourceProperty, sourceEntitySet, collection, target, targetEntitySet, NotifyCollectionChangedAction.Remove);
				if (this.CollectionChanged(entityCollectionChangedParams))
				{
					return;
				}
			}
			if (source != null && !this.IsContextTrackingEntity(source))
			{
				throw new InvalidOperationException(Strings.DataBinding_BindingOperation_DetachedSource);
			}
			if (this.IsContextTrackingEntity(target))
			{
				if (this.DetachBehavior)
				{
					this.Context.Detach(target);
					return;
				}
				this.Context.DeleteObject(target);
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00022058 File Offset: 0x00020258
		internal void HandleUpdateEntityReference(object source, string sourceProperty, string sourceEntitySet, object target, string targetEntitySet)
		{
			if (this.Context.ApplyingChanges)
			{
				return;
			}
			if (this.IsDetachedOrDeletedFromContext(source))
			{
				return;
			}
			EntityDescriptor entityDescriptor = ((target != null) ? this.Context.GetEntityDescriptor(target) : null);
			bool flag = !this.AttachBehavior && (entityDescriptor == null || !this.IsContextTrackingLink(source, sourceProperty, target));
			if (flag && this.EntityChanged != null)
			{
				EntityChangedParams entityChangedParams = new EntityChangedParams(this.Context, source, sourceProperty, target, sourceEntitySet, targetEntitySet);
				if (this.EntityChanged(entityChangedParams))
				{
					return;
				}
			}
			if (this.IsDetachedOrDeletedFromContext(source))
			{
				throw new InvalidOperationException(Strings.DataBinding_BindingOperation_DetachedSource);
			}
			entityDescriptor = ((target != null) ? this.Context.GetEntityDescriptor(target) : null);
			if (target != null)
			{
				if (entityDescriptor == null)
				{
					BindingUtils.ValidateEntitySetName(targetEntitySet, target);
					if (this.AttachBehavior)
					{
						this.Context.AttachTo(targetEntitySet, target);
					}
					else
					{
						this.Context.AddObject(targetEntitySet, target);
					}
					entityDescriptor = this.Context.GetEntityDescriptor(target);
				}
				if (!this.IsContextTrackingLink(source, sourceProperty, target))
				{
					if (!this.AttachBehavior)
					{
						this.Context.SetLink(source, sourceProperty, target);
						return;
					}
					if (entityDescriptor.State != EntityStates.Deleted)
					{
						this.Context.AttachLink(source, sourceProperty, target);
						return;
					}
				}
			}
			else
			{
				this.Context.SetLink(source, sourceProperty, null);
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00022198 File Offset: 0x00020398
		internal bool IsContextTrackingEntity(object entity)
		{
			return this.Context.GetEntityDescriptor(entity) != null;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000221AC File Offset: 0x000203AC
		private void HandleUpdateEntity(object entity, string propertyName, object propertyValue)
		{
			if (this.Context.ApplyingChanges)
			{
				return;
			}
			if (!BindingEntityInfo.IsEntityType(entity.GetType(), this.Context.Model))
			{
				this.bindingGraph.GetAncestorEntityForComplexProperty(ref entity, ref propertyName, ref propertyValue);
			}
			if (this.IsDetachedOrDeletedFromContext(entity))
			{
				return;
			}
			if (this.EntityChanged != null)
			{
				EntityChangedParams entityChangedParams = new EntityChangedParams(this.Context, entity, propertyName, propertyValue, null, null);
				if (this.EntityChanged(entityChangedParams))
				{
					return;
				}
			}
			if (this.IsContextTrackingEntity(entity))
			{
				this.Context.UpdateObject(entity);
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00022238 File Offset: 0x00020438
		private void OnAddToDataServiceCollection(NotifyCollectionChangedEventArgs eventArgs, object source, string sourceProperty, string targetEntitySet, object collection)
		{
			if (eventArgs.NewItems != null)
			{
				foreach (object obj in eventArgs.NewItems)
				{
					if (obj == null)
					{
						throw new InvalidOperationException(Strings.DataBinding_BindingOperation_ArrayItemNull("Add"));
					}
					this.bindingGraph.AddEntity(source, sourceProperty, obj, targetEntitySet, collection);
				}
			}
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000222B4 File Offset: 0x000204B4
		private void OnRemoveFromDataServiceCollection(NotifyCollectionChangedEventArgs eventArgs, object source, string sourceProperty, object collection)
		{
			if (eventArgs.OldItems != null)
			{
				this.DeepRemoveDataServiceCollection(eventArgs.OldItems, source ?? collection, sourceProperty, new Action<object>(this.ValidateDataServiceCollectionItem));
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000222E0 File Offset: 0x000204E0
		private void RemoveWithDetachDataServiceCollection(object collection)
		{
			object obj = null;
			string text = null;
			string text2 = null;
			string text3 = null;
			this.bindingGraph.GetDataServiceCollectionInfo(collection, out obj, out text, out text2, out text3);
			this.DeepRemoveDataServiceCollection(this.bindingGraph.GetDataServiceCollectionItems(collection), obj ?? collection, text, null);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00022324 File Offset: 0x00020524
		private void DeepRemoveDataServiceCollection(IEnumerable collection, object source, string sourceProperty, Action<object> itemValidator)
		{
			foreach (object obj in collection)
			{
				if (itemValidator != null)
				{
					itemValidator(obj);
				}
				List<BindingObserver.UnTrackingInfo> list = new List<BindingObserver.UnTrackingInfo>();
				this.CollectUnTrackingInfo(obj, source, sourceProperty, list);
				foreach (BindingObserver.UnTrackingInfo unTrackingInfo in list)
				{
					this.bindingGraph.RemoveDataServiceCollectionItem(unTrackingInfo.Entity, unTrackingInfo.Parent, unTrackingInfo.ParentProperty);
				}
			}
			this.bindingGraph.RemoveUnreachableVertices();
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000223EC File Offset: 0x000205EC
		private void OnAddToComplexTypeCollection(object collection, IList newItems)
		{
			if (newItems != null)
			{
				this.bindingGraph.AddComplexObjectsFromCollection(collection, newItems);
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00022400 File Offset: 0x00020600
		private void OnRemoveFromComplexTypeCollection(object collection, IList items)
		{
			if (items != null)
			{
				foreach (object obj in items)
				{
					this.bindingGraph.RemoveComplexTypeCollectionItem(obj, collection);
				}
				this.bindingGraph.RemoveUnreachableVertices();
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00022464 File Offset: 0x00020664
		private void OnChangesSaved(object sender, SaveChangesEventArgs eventArgs)
		{
			this.bindingGraph.RemoveNonTrackedEntities();
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00022494 File Offset: 0x00020694
		private void CollectUnTrackingInfo(object currentEntity, object parentEntity, string parentProperty, IList<BindingObserver.UnTrackingInfo> entitiesToUnTrack)
		{
			foreach (EntityDescriptor entityDescriptor in this.Context.Entities.Where((EntityDescriptor x) => x.ParentEntity == currentEntity && x.State == EntityStates.Added))
			{
				this.CollectUnTrackingInfo(entityDescriptor.Entity, entityDescriptor.ParentEntity, entityDescriptor.ParentPropertyForInsert, entitiesToUnTrack);
			}
			entitiesToUnTrack.Add(new BindingObserver.UnTrackingInfo
			{
				Entity = currentEntity,
				Parent = parentEntity,
				ParentProperty = parentProperty
			});
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00022540 File Offset: 0x00020740
		private bool IsContextTrackingLink(object source, string sourceProperty, object target)
		{
			return this.Context.GetLinkDescriptor(source, sourceProperty, target) != null;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00022558 File Offset: 0x00020758
		private bool IsDetachedOrDeletedFromContext(object entity)
		{
			EntityDescriptor entityDescriptor = this.Context.GetEntityDescriptor(entity);
			return entityDescriptor == null || entityDescriptor.State == EntityStates.Deleted;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00022580 File Offset: 0x00020780
		private void ValidateDataServiceCollectionItem(object target)
		{
			if (target == null)
			{
				throw new InvalidOperationException(Strings.DataBinding_BindingOperation_ArrayItemNull("Remove"));
			}
			if (!BindingEntityInfo.IsEntityType(target.GetType(), this.Context.Model))
			{
				throw new InvalidOperationException(Strings.DataBinding_BindingOperation_ArrayItemNotEntity("Remove"));
			}
		}

		// Token: 0x040004BE RID: 1214
		private BindingGraph bindingGraph;

		// Token: 0x020000EF RID: 239
		private class UnTrackingInfo
		{
			// Token: 0x170001CB RID: 459
			// (get) Token: 0x06000801 RID: 2049 RVA: 0x000225BD File Offset: 0x000207BD
			// (set) Token: 0x06000802 RID: 2050 RVA: 0x000225C5 File Offset: 0x000207C5
			public object Entity { get; set; }

			// Token: 0x170001CC RID: 460
			// (get) Token: 0x06000803 RID: 2051 RVA: 0x000225CE File Offset: 0x000207CE
			// (set) Token: 0x06000804 RID: 2052 RVA: 0x000225D6 File Offset: 0x000207D6
			public object Parent { get; set; }

			// Token: 0x170001CD RID: 461
			// (get) Token: 0x06000805 RID: 2053 RVA: 0x000225DF File Offset: 0x000207DF
			// (set) Token: 0x06000806 RID: 2054 RVA: 0x000225E7 File Offset: 0x000207E7
			public string ParentProperty { get; set; }
		}
	}
}
