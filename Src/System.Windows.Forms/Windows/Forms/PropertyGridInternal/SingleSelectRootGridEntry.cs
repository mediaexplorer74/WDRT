using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000513 RID: 1299
	internal class SingleSelectRootGridEntry : GridEntry, IRootGridEntry
	{
		// Token: 0x0600551A RID: 21786 RVA: 0x001653DC File Offset: 0x001635DC
		internal SingleSelectRootGridEntry(PropertyGridView gridEntryHost, object value, GridEntry parent, IServiceProvider baseProvider, IDesignerHost host, PropertyTab tab, PropertySort sortType)
			: base(gridEntryHost.OwnerGrid, parent)
		{
			this.host = host;
			this.gridEntryHost = gridEntryHost;
			this.baseProvider = baseProvider;
			this.tab = tab;
			this.objValue = value;
			this.objValueClassName = TypeDescriptor.GetClassName(this.objValue);
			this.IsExpandable = true;
			this.PropertySort = sortType;
			this.InternalExpanded = true;
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x00165443 File Offset: 0x00163643
		internal SingleSelectRootGridEntry(PropertyGridView view, object value, IServiceProvider baseProvider, IDesignerHost host, PropertyTab tab, PropertySort sortType)
			: this(view, value, null, baseProvider, host, tab, sortType)
		{
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x0600551C RID: 21788 RVA: 0x00165455 File Offset: 0x00163655
		// (set) Token: 0x0600551D RID: 21789 RVA: 0x00165480 File Offset: 0x00163680
		public override AttributeCollection BrowsableAttributes
		{
			get
			{
				if (this.browsableAttributes == null)
				{
					this.browsableAttributes = new AttributeCollection(new Attribute[] { BrowsableAttribute.Yes });
				}
				return this.browsableAttributes;
			}
			set
			{
				if (value == null)
				{
					this.ResetBrowsableAttributes();
					return;
				}
				bool flag = true;
				if (this.browsableAttributes != null && value != null && this.browsableAttributes.Count == value.Count)
				{
					Attribute[] array = new Attribute[this.browsableAttributes.Count];
					Attribute[] array2 = new Attribute[value.Count];
					this.browsableAttributes.CopyTo(array, 0);
					value.CopyTo(array2, 0);
					Array.Sort(array, GridEntry.AttributeTypeSorter);
					Array.Sort(array2, GridEntry.AttributeTypeSorter);
					for (int i = 0; i < array.Length; i++)
					{
						if (!array[i].Equals(array2[i]))
						{
							flag = false;
							break;
						}
					}
				}
				else
				{
					flag = false;
				}
				this.browsableAttributes = value;
				if (!flag && this.Children != null && this.Children.Count > 0)
				{
					this.DisposeChildren();
				}
			}
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x0600551E RID: 21790 RVA: 0x00165548 File Offset: 0x00163748
		protected override IComponentChangeService ComponentChangeService
		{
			get
			{
				if (this.changeService == null)
				{
					this.changeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				}
				return this.changeService;
			}
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x0600551F RID: 21791 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool AlwaysAllowExpand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06005520 RID: 21792 RVA: 0x00165573 File Offset: 0x00163773
		// (set) Token: 0x06005521 RID: 21793 RVA: 0x0016557B File Offset: 0x0016377B
		public override PropertyTab CurrentTab
		{
			get
			{
				return this.tab;
			}
			set
			{
				this.tab = value;
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x06005522 RID: 21794 RVA: 0x00165584 File Offset: 0x00163784
		// (set) Token: 0x06005523 RID: 21795 RVA: 0x0016558C File Offset: 0x0016378C
		internal override GridEntry DefaultChild
		{
			get
			{
				return this.propDefault;
			}
			set
			{
				this.propDefault = value;
			}
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06005524 RID: 21796 RVA: 0x00165595 File Offset: 0x00163795
		// (set) Token: 0x06005525 RID: 21797 RVA: 0x0016559D File Offset: 0x0016379D
		internal override IDesignerHost DesignerHost
		{
			get
			{
				return this.host;
			}
			set
			{
				this.host = value;
			}
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06005526 RID: 21798 RVA: 0x001655A8 File Offset: 0x001637A8
		internal override bool ForceReadOnly
		{
			get
			{
				if (!this.forceReadOnlyChecked)
				{
					ReadOnlyAttribute readOnlyAttribute = (ReadOnlyAttribute)TypeDescriptor.GetAttributes(this.objValue)[typeof(ReadOnlyAttribute)];
					if ((readOnlyAttribute != null && !readOnlyAttribute.IsDefaultAttribute()) || TypeDescriptor.GetAttributes(this.objValue).Contains(InheritanceAttribute.InheritedReadOnly))
					{
						this.flags |= 1024;
					}
					this.forceReadOnlyChecked = true;
				}
				return base.ForceReadOnly || (this.GridEntryHost != null && !this.GridEntryHost.Enabled);
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06005527 RID: 21799 RVA: 0x0016563A File Offset: 0x0016383A
		// (set) Token: 0x06005528 RID: 21800 RVA: 0x00165642 File Offset: 0x00163842
		internal override PropertyGridView GridEntryHost
		{
			get
			{
				return this.gridEntryHost;
			}
			set
			{
				this.gridEntryHost = value;
			}
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06005529 RID: 21801 RVA: 0x00023BD7 File Offset: 0x00021DD7
		public override GridItemType GridItemType
		{
			get
			{
				return GridItemType.Root;
			}
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x0600552A RID: 21802 RVA: 0x0016564C File Offset: 0x0016384C
		public override string HelpKeyword
		{
			get
			{
				HelpKeywordAttribute helpKeywordAttribute = (HelpKeywordAttribute)TypeDescriptor.GetAttributes(this.objValue)[typeof(HelpKeywordAttribute)];
				if (helpKeywordAttribute != null && !helpKeywordAttribute.IsDefaultAttribute())
				{
					return helpKeywordAttribute.HelpKeyword;
				}
				return this.objValueClassName;
			}
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x0600552B RID: 21803 RVA: 0x00165694 File Offset: 0x00163894
		public override string PropertyLabel
		{
			get
			{
				if (this.objValue is IComponent)
				{
					ISite site = ((IComponent)this.objValue).Site;
					if (site == null)
					{
						return this.objValue.GetType().Name;
					}
					return site.Name;
				}
				else
				{
					if (this.objValue != null)
					{
						return this.objValue.ToString();
					}
					return null;
				}
			}
		}

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x0600552C RID: 21804 RVA: 0x001656EF File Offset: 0x001638EF
		// (set) Token: 0x0600552D RID: 21805 RVA: 0x001656F8 File Offset: 0x001638F8
		public override object PropertyValue
		{
			get
			{
				return this.objValue;
			}
			set
			{
				object obj = this.objValue;
				this.objValue = value;
				this.objValueClassName = TypeDescriptor.GetClassName(this.objValue);
				this.ownerGrid.ReplaceSelectedObject(obj, value);
			}
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x00165734 File Offset: 0x00163934
		protected override bool CreateChildren()
		{
			bool flag = base.CreateChildren();
			this.CategorizePropEntries();
			return flag;
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x00165750 File Offset: 0x00163950
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.host = null;
				this.baseProvider = null;
				this.tab = null;
				this.gridEntryHost = null;
				this.changeService = null;
			}
			this.objValue = null;
			this.objValueClassName = null;
			this.propDefault = null;
			base.Dispose(disposing);
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x001657A0 File Offset: 0x001639A0
		public override object GetService(Type serviceType)
		{
			object obj = null;
			if (this.host != null)
			{
				obj = this.host.GetService(serviceType);
			}
			if (obj == null && this.baseProvider != null)
			{
				obj = this.baseProvider.GetService(serviceType);
			}
			return obj;
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x001657DD File Offset: 0x001639DD
		public void ResetBrowsableAttributes()
		{
			this.browsableAttributes = new AttributeCollection(new Attribute[] { BrowsableAttribute.Yes });
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x001657F8 File Offset: 0x001639F8
		public virtual void ShowCategories(bool fCategories)
		{
			if ((this.PropertySort &= PropertySort.Categorized) > PropertySort.NoSort != fCategories)
			{
				if (fCategories)
				{
					this.PropertySort |= PropertySort.Categorized;
				}
				else
				{
					this.PropertySort &= (PropertySort)(-3);
				}
				if (this.Expandable && base.ChildCollection != null)
				{
					this.CreateChildren();
				}
			}
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x00165858 File Offset: 0x00163A58
		internal void CategorizePropEntries()
		{
			if (this.Children.Count > 0)
			{
				GridEntry[] array = new GridEntry[this.Children.Count];
				this.Children.CopyTo(array, 0);
				if ((this.PropertySort & PropertySort.Categorized) != PropertySort.NoSort)
				{
					Hashtable hashtable = new Hashtable();
					foreach (GridEntry gridEntry in array)
					{
						if (gridEntry != null)
						{
							string propertyCategory = gridEntry.PropertyCategory;
							ArrayList arrayList = (ArrayList)hashtable[propertyCategory];
							if (arrayList == null)
							{
								arrayList = new ArrayList();
								hashtable[propertyCategory] = arrayList;
							}
							arrayList.Add(gridEntry);
						}
					}
					ArrayList arrayList2 = new ArrayList();
					IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
					while (enumerator.MoveNext())
					{
						ArrayList arrayList3 = (ArrayList)enumerator.Value;
						if (arrayList3 != null)
						{
							string text = (string)enumerator.Key;
							if (arrayList3.Count > 0)
							{
								GridEntry[] array2 = new GridEntry[arrayList3.Count];
								arrayList3.CopyTo(array2, 0);
								try
								{
									arrayList2.Add(new CategoryGridEntry(this.ownerGrid, this, text, array2));
								}
								catch
								{
								}
							}
						}
					}
					array = new GridEntry[arrayList2.Count];
					arrayList2.CopyTo(array, 0);
					object[] array3 = array;
					StringSorter.Sort(array3);
					base.ChildCollection.Clear();
					base.ChildCollection.AddRange(array);
				}
			}
		}

		// Token: 0x04003742 RID: 14146
		protected object objValue;

		// Token: 0x04003743 RID: 14147
		protected string objValueClassName;

		// Token: 0x04003744 RID: 14148
		protected GridEntry propDefault;

		// Token: 0x04003745 RID: 14149
		protected IDesignerHost host;

		// Token: 0x04003746 RID: 14150
		protected IServiceProvider baseProvider;

		// Token: 0x04003747 RID: 14151
		protected PropertyTab tab;

		// Token: 0x04003748 RID: 14152
		protected PropertyGridView gridEntryHost;

		// Token: 0x04003749 RID: 14153
		protected AttributeCollection browsableAttributes;

		// Token: 0x0400374A RID: 14154
		private IComponentChangeService changeService;

		// Token: 0x0400374B RID: 14155
		protected bool forceReadOnlyChecked;
	}
}
