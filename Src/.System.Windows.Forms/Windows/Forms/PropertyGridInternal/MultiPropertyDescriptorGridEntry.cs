using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x0200050E RID: 1294
	internal class MultiPropertyDescriptorGridEntry : PropertyDescriptorGridEntry
	{
		// Token: 0x060054D6 RID: 21718 RVA: 0x001637E5 File Offset: 0x001619E5
		public MultiPropertyDescriptorGridEntry(PropertyGrid ownerGrid, GridEntry peParent, object[] objectArray, PropertyDescriptor[] propInfo, bool hide)
			: base(ownerGrid, peParent, hide)
		{
			this.mergedPd = new MergePropertyDescriptor(propInfo);
			this.objs = objectArray;
			base.Initialize(this.mergedPd);
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x060054D7 RID: 21719 RVA: 0x00163814 File Offset: 0x00161A14
		public override IContainer Container
		{
			get
			{
				IContainer container = null;
				object[] array = this.objs;
				int i = 0;
				while (i < array.Length)
				{
					object obj = array[i];
					IComponent component = obj as IComponent;
					if (component == null)
					{
						container = null;
						break;
					}
					if (component.Site != null)
					{
						if (container == null)
						{
							container = component.Site.Container;
						}
						else if (container != component.Site.Container)
						{
							goto IL_4B;
						}
						i++;
						continue;
					}
					IL_4B:
					container = null;
					break;
				}
				return container;
			}
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x060054D8 RID: 21720 RVA: 0x0016387C File Offset: 0x00161A7C
		public override bool Expandable
		{
			get
			{
				bool flag = this.GetFlagSet(131072);
				if (flag && base.ChildCollection.Count > 0)
				{
					return true;
				}
				if (this.GetFlagSet(524288))
				{
					return false;
				}
				try
				{
					object[] values = this.mergedPd.GetValues(this.objs);
					for (int i = 0; i < values.Length; i++)
					{
						if (values[i] == null)
						{
							flag = false;
							break;
						}
					}
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x1700145D RID: 5213
		// (set) Token: 0x060054D9 RID: 21721 RVA: 0x001638FC File Offset: 0x00161AFC
		public override object PropertyValue
		{
			set
			{
				base.PropertyValue = value;
				base.RecreateChildren();
				if (this.Expanded)
				{
					this.GridEntryHost.Refresh(false);
				}
			}
		}

		// Token: 0x060054DA RID: 21722 RVA: 0x0015FB48 File Offset: 0x0015DD48
		protected override bool CreateChildren()
		{
			return this.CreateChildren(false);
		}

		// Token: 0x060054DB RID: 21723 RVA: 0x00163920 File Offset: 0x00161B20
		protected override bool CreateChildren(bool diffOldChildren)
		{
			bool flag;
			try
			{
				if (this.mergedPd.PropertyType.IsValueType || (this.Flags & 512) != 0)
				{
					flag = base.CreateChildren(diffOldChildren);
				}
				else
				{
					base.ChildCollection.Clear();
					MultiPropertyDescriptorGridEntry[] mergedProperties = MultiSelectRootGridEntry.PropertyMerger.GetMergedProperties(this.mergedPd.GetValues(this.objs), this, this.PropertySort, this.CurrentTab);
					if (mergedProperties != null)
					{
						GridEntryCollection childCollection = base.ChildCollection;
						GridEntry[] array = mergedProperties;
						childCollection.AddRange(array);
					}
					bool flag2 = this.Children.Count > 0;
					if (!flag2)
					{
						this.SetFlag(524288, true);
					}
					flag = flag2;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060054DC RID: 21724 RVA: 0x001639D0 File Offset: 0x00161BD0
		public override object GetChildValueOwner(GridEntry childEntry)
		{
			if (this.mergedPd.PropertyType.IsValueType || (this.Flags & 512) != 0)
			{
				return base.GetChildValueOwner(childEntry);
			}
			return this.mergedPd.GetValues(this.objs);
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x00163A0C File Offset: 0x00161C0C
		public override IComponent[] GetComponents()
		{
			IComponent[] array = new IComponent[this.objs.Length];
			Array.Copy(this.objs, 0, array, 0, this.objs.Length);
			return array;
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x00163A40 File Offset: 0x00161C40
		public override string GetPropertyTextValue(object value)
		{
			bool flag = true;
			try
			{
				if (value == null && this.mergedPd.GetValue(this.objs, out flag) == null && !flag)
				{
					return "";
				}
			}
			catch
			{
				return "";
			}
			return base.GetPropertyTextValue(value);
		}

		// Token: 0x060054DF RID: 21727 RVA: 0x00163A98 File Offset: 0x00161C98
		internal override bool NotifyChildValue(GridEntry pe, int type)
		{
			bool flag = false;
			IDesignerHost designerHost = this.DesignerHost;
			DesignerTransaction designerTransaction = null;
			if (designerHost != null)
			{
				designerTransaction = designerHost.CreateTransaction();
			}
			try
			{
				flag = base.NotifyChildValue(pe, type);
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
				}
			}
			return flag;
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x00163AE4 File Offset: 0x00161CE4
		protected override void NotifyParentChange(GridEntry ge)
		{
			while (ge != null && ge is PropertyDescriptorGridEntry && ((PropertyDescriptorGridEntry)ge).propertyInfo.Attributes.Contains(NotifyParentPropertyAttribute.Yes))
			{
				object obj = ge.GetValueOwner();
				while (!(ge is PropertyDescriptorGridEntry) || this.OwnersEqual(obj, ge.GetValueOwner()))
				{
					ge = ge.ParentGridEntry;
					if (ge == null)
					{
						break;
					}
				}
				if (ge != null)
				{
					obj = ge.GetValueOwner();
					IComponentChangeService componentChangeService = this.ComponentChangeService;
					if (componentChangeService != null)
					{
						Array array = obj as Array;
						if (array != null)
						{
							for (int i = 0; i < array.Length; i++)
							{
								PropertyDescriptor propertyDescriptor = ((PropertyDescriptorGridEntry)ge).propertyInfo;
								if (propertyDescriptor is MergePropertyDescriptor)
								{
									propertyDescriptor = ((MergePropertyDescriptor)propertyDescriptor)[i];
								}
								if (propertyDescriptor != null)
								{
									componentChangeService.OnComponentChanging(array.GetValue(i), propertyDescriptor);
									componentChangeService.OnComponentChanged(array.GetValue(i), propertyDescriptor, null, null);
								}
							}
						}
						else
						{
							componentChangeService.OnComponentChanging(obj, ((PropertyDescriptorGridEntry)ge).propertyInfo);
							componentChangeService.OnComponentChanged(obj, ((PropertyDescriptorGridEntry)ge).propertyInfo, null, null);
						}
					}
				}
			}
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x00163BF4 File Offset: 0x00161DF4
		internal override bool NotifyValueGivenParent(object obj, int type)
		{
			if (obj is ICustomTypeDescriptor)
			{
				obj = ((ICustomTypeDescriptor)obj).GetPropertyOwner(this.propertyInfo);
			}
			switch (type)
			{
			case 1:
			{
				object[] array = (object[])obj;
				if (array != null && array.Length != 0)
				{
					IDesignerHost designerHost = this.DesignerHost;
					DesignerTransaction designerTransaction = null;
					if (designerHost != null)
					{
						designerTransaction = designerHost.CreateTransaction(SR.GetString("PropertyGridResetValue", new object[] { this.PropertyName }));
					}
					try
					{
						bool flag = !(array[0] is IComponent) || ((IComponent)array[0]).Site == null;
						if (flag && !this.OnComponentChanging())
						{
							if (designerTransaction != null)
							{
								designerTransaction.Cancel();
								designerTransaction = null;
							}
							return false;
						}
						this.mergedPd.ResetValue(obj);
						if (flag)
						{
							this.OnComponentChanged();
						}
						this.NotifyParentChange(this);
					}
					finally
					{
						if (designerTransaction != null)
						{
							designerTransaction.Commit();
						}
					}
					return false;
				}
				return false;
			}
			case 3:
			case 5:
			{
				MergePropertyDescriptor mergePropertyDescriptor = this.propertyInfo as MergePropertyDescriptor;
				if (mergePropertyDescriptor != null)
				{
					object[] array2 = (object[])obj;
					if (this.eventBindings == null)
					{
						this.eventBindings = (IEventBindingService)this.GetService(typeof(IEventBindingService));
					}
					if (this.eventBindings != null)
					{
						EventDescriptor @event = this.eventBindings.GetEvent(mergePropertyDescriptor[0]);
						if (@event != null)
						{
							return base.ViewEvent(obj, null, @event, true);
						}
					}
					return false;
				}
				return base.NotifyValueGivenParent(obj, type);
			}
			}
			return base.NotifyValueGivenParent(obj, type);
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x00163D74 File Offset: 0x00161F74
		private bool OwnersEqual(object owner1, object owner2)
		{
			if (!(owner1 is Array))
			{
				return owner1 == owner2;
			}
			Array array = owner1 as Array;
			Array array2 = owner2 as Array;
			if (array != null && array2 != null && array.Length == array2.Length)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array.GetValue(i) != array2.GetValue(i))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x00163DD8 File Offset: 0x00161FD8
		public override bool OnComponentChanging()
		{
			if (this.ComponentChangeService != null)
			{
				int num = this.objs.Length;
				for (int i = 0; i < num; i++)
				{
					try
					{
						this.ComponentChangeService.OnComponentChanging(this.objs[i], this.mergedPd[i]);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return false;
						}
						throw ex;
					}
				}
			}
			return true;
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x00163E48 File Offset: 0x00162048
		public override void OnComponentChanged()
		{
			if (this.ComponentChangeService != null)
			{
				int num = this.objs.Length;
				for (int i = 0; i < num; i++)
				{
					this.ComponentChangeService.OnComponentChanged(this.objs[i], this.mergedPd[i], null, null);
				}
			}
		}

		// Token: 0x04003720 RID: 14112
		private MergePropertyDescriptor mergedPd;

		// Token: 0x04003721 RID: 14113
		private object[] objs;
	}
}
