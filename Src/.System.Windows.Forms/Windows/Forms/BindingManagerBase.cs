using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Manages all <see cref="T:System.Windows.Forms.Binding" /> objects that are bound to the same data source and data member. This class is abstract.</summary>
	// Token: 0x02000134 RID: 308
	public abstract class BindingManagerBase
	{
		/// <summary>Gets the collection of bindings being managed.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingsCollection" /> that contains the <see cref="T:System.Windows.Forms.Binding" /> objects managed by this <see cref="T:System.Windows.Forms.BindingManagerBase" />.</returns>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0001FB78 File Offset: 0x0001DD78
		public BindingsCollection Bindings
		{
			get
			{
				if (this.bindings == null)
				{
					this.bindings = new ListManagerBindingsCollection(this);
					this.bindings.CollectionChanging += this.OnBindingsCollectionChanging;
					this.bindings.CollectionChanged += this.OnBindingsCollectionChanged;
				}
				return this.bindings;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.BindingComplete" /> event.</summary>
		/// <param name="args">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" /> that contains the event data.</param>
		// Token: 0x06000B20 RID: 2848 RVA: 0x0001FBCD File Offset: 0x0001DDCD
		protected internal void OnBindingComplete(BindingCompleteEventArgs args)
		{
			if (this.onBindingCompleteHandler != null)
			{
				this.onBindingCompleteHandler(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000B21 RID: 2849
		protected internal abstract void OnCurrentChanged(EventArgs e);

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentItemChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000B22 RID: 2850
		protected internal abstract void OnCurrentItemChanged(EventArgs e);

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Exception" /> that caused the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event to occur.</param>
		// Token: 0x06000B23 RID: 2851 RVA: 0x0001FBE4 File Offset: 0x0001DDE4
		protected internal void OnDataError(Exception e)
		{
			if (this.onDataErrorHandler != null)
			{
				this.onDataErrorHandler(this, new BindingManagerDataErrorEventArgs(e));
			}
		}

		/// <summary>When overridden in a derived class, gets the current object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the current object.</returns>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B24 RID: 2852
		public abstract object Current { get; }

		// Token: 0x06000B25 RID: 2853
		internal abstract void SetDataSource(object dataSource);

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingManagerBase" /> class.</summary>
		// Token: 0x06000B26 RID: 2854 RVA: 0x00002843 File Offset: 0x00000A43
		public BindingManagerBase()
		{
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0001FC00 File Offset: 0x0001DE00
		internal BindingManagerBase(object dataSource)
		{
			this.SetDataSource(dataSource);
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B28 RID: 2856
		internal abstract Type BindType { get; }

		// Token: 0x06000B29 RID: 2857
		internal abstract PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);

		/// <summary>When overridden in a derived class, gets the collection of property descriptors for the binding.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the property descriptors for the binding.</returns>
		// Token: 0x06000B2A RID: 2858 RVA: 0x0001FC0F File Offset: 0x0001DE0F
		public virtual PropertyDescriptorCollection GetItemProperties()
		{
			return this.GetItemProperties(null);
		}

		/// <summary>Gets the collection of property descriptors for the binding using the specified <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="dataSources">An <see cref="T:System.Collections.ArrayList" /> containing the data sources.</param>
		/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> containing the table's bound properties.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the property descriptors for the binding.</returns>
		// Token: 0x06000B2B RID: 2859 RVA: 0x0001FC18 File Offset: 0x0001DE18
		protected internal virtual PropertyDescriptorCollection GetItemProperties(ArrayList dataSources, ArrayList listAccessors)
		{
			IList list = null;
			if (this is CurrencyManager)
			{
				list = ((CurrencyManager)this).List;
			}
			if (list is ITypedList)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[listAccessors.Count];
				listAccessors.CopyTo(array, 0);
				return ((ITypedList)list).GetItemProperties(array);
			}
			return this.GetItemProperties(this.BindType, 0, dataSources, listAccessors);
		}

		/// <summary>Gets the list of properties of the items managed by this <see cref="T:System.Windows.Forms.BindingManagerBase" />.</summary>
		/// <param name="listType">The <see cref="T:System.Type" /> of the bound list.</param>
		/// <param name="offset">A counter used to recursively call the method.</param>
		/// <param name="dataSources">An <see cref="T:System.Collections.ArrayList" /> containing the data sources.</param>
		/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> containing the table's bound properties.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the property descriptors for the binding.</returns>
		// Token: 0x06000B2C RID: 2860 RVA: 0x0001FC74 File Offset: 0x0001DE74
		protected virtual PropertyDescriptorCollection GetItemProperties(Type listType, int offset, ArrayList dataSources, ArrayList listAccessors)
		{
			if (listAccessors.Count < offset)
			{
				return null;
			}
			if (listAccessors.Count != offset)
			{
				PropertyInfo[] properties = listType.GetProperties();
				if (typeof(IList).IsAssignableFrom(listType))
				{
					PropertyDescriptorCollection propertyDescriptorCollection = null;
					for (int i = 0; i < properties.Length; i++)
					{
						if ("Item".Equals(properties[i].Name) && properties[i].PropertyType != typeof(object))
						{
							propertyDescriptorCollection = TypeDescriptor.GetProperties(properties[i].PropertyType, new Attribute[]
							{
								new BrowsableAttribute(true)
							});
						}
					}
					if (propertyDescriptorCollection == null)
					{
						IList list;
						if (offset == 0)
						{
							list = this.DataSource as IList;
						}
						else
						{
							list = dataSources[offset - 1] as IList;
						}
						if (list != null && list.Count > 0)
						{
							propertyDescriptorCollection = TypeDescriptor.GetProperties(list[0]);
						}
					}
					if (propertyDescriptorCollection != null)
					{
						for (int j = 0; j < propertyDescriptorCollection.Count; j++)
						{
							if (propertyDescriptorCollection[j].Equals(listAccessors[offset]))
							{
								return this.GetItemProperties(propertyDescriptorCollection[j].PropertyType, offset + 1, dataSources, listAccessors);
							}
						}
					}
				}
				else
				{
					for (int k = 0; k < properties.Length; k++)
					{
						if (properties[k].Name.Equals(((PropertyDescriptor)listAccessors[offset]).Name))
						{
							return this.GetItemProperties(properties[k].PropertyType, offset + 1, dataSources, listAccessors);
						}
					}
				}
				return null;
			}
			if (!typeof(IList).IsAssignableFrom(listType))
			{
				return TypeDescriptor.GetProperties(listType);
			}
			PropertyInfo[] properties2 = listType.GetProperties();
			for (int l = 0; l < properties2.Length; l++)
			{
				if ("Item".Equals(properties2[l].Name) && properties2[l].PropertyType != typeof(object))
				{
					return TypeDescriptor.GetProperties(properties2[l].PropertyType, new Attribute[]
					{
						new BrowsableAttribute(true)
					});
				}
			}
			IList list2 = dataSources[offset - 1] as IList;
			if (list2 != null && list2.Count > 0)
			{
				return TypeDescriptor.GetProperties(list2[0]);
			}
			return null;
		}

		/// <summary>Occurs at the completion of a data-binding operation.</summary>
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06000B2D RID: 2861 RVA: 0x0001FE9E File Offset: 0x0001E09E
		// (remove) Token: 0x06000B2E RID: 2862 RVA: 0x0001FEB7 File Offset: 0x0001E0B7
		public event BindingCompleteEventHandler BindingComplete
		{
			add
			{
				this.onBindingCompleteHandler = (BindingCompleteEventHandler)Delegate.Combine(this.onBindingCompleteHandler, value);
			}
			remove
			{
				this.onBindingCompleteHandler = (BindingCompleteEventHandler)Delegate.Remove(this.onBindingCompleteHandler, value);
			}
		}

		/// <summary>Occurs when the currently bound item changes.</summary>
		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06000B2F RID: 2863 RVA: 0x0001FED0 File Offset: 0x0001E0D0
		// (remove) Token: 0x06000B30 RID: 2864 RVA: 0x0001FEE9 File Offset: 0x0001E0E9
		public event EventHandler CurrentChanged
		{
			add
			{
				this.onCurrentChangedHandler = (EventHandler)Delegate.Combine(this.onCurrentChangedHandler, value);
			}
			remove
			{
				this.onCurrentChangedHandler = (EventHandler)Delegate.Remove(this.onCurrentChangedHandler, value);
			}
		}

		/// <summary>Occurs when the state of the currently bound item changes.</summary>
		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06000B31 RID: 2865 RVA: 0x0001FF02 File Offset: 0x0001E102
		// (remove) Token: 0x06000B32 RID: 2866 RVA: 0x0001FF1B File Offset: 0x0001E11B
		public event EventHandler CurrentItemChanged
		{
			add
			{
				this.onCurrentItemChangedHandler = (EventHandler)Delegate.Combine(this.onCurrentItemChangedHandler, value);
			}
			remove
			{
				this.onCurrentItemChangedHandler = (EventHandler)Delegate.Remove(this.onCurrentItemChangedHandler, value);
			}
		}

		/// <summary>Occurs when an <see cref="T:System.Exception" /> is silently handled by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</summary>
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06000B33 RID: 2867 RVA: 0x0001FF34 File Offset: 0x0001E134
		// (remove) Token: 0x06000B34 RID: 2868 RVA: 0x0001FF4D File Offset: 0x0001E14D
		public event BindingManagerDataErrorEventHandler DataError
		{
			add
			{
				this.onDataErrorHandler = (BindingManagerDataErrorEventHandler)Delegate.Combine(this.onDataErrorHandler, value);
			}
			remove
			{
				this.onDataErrorHandler = (BindingManagerDataErrorEventHandler)Delegate.Remove(this.onDataErrorHandler, value);
			}
		}

		// Token: 0x06000B35 RID: 2869
		internal abstract string GetListName();

		/// <summary>When overridden in a derived class, cancels the current edit.</summary>
		// Token: 0x06000B36 RID: 2870
		public abstract void CancelCurrentEdit();

		/// <summary>When overridden in a derived class, ends the current edit.</summary>
		// Token: 0x06000B37 RID: 2871
		public abstract void EndCurrentEdit();

		/// <summary>When overridden in a derived class, adds a new item to the underlying list.</summary>
		// Token: 0x06000B38 RID: 2872
		public abstract void AddNew();

		/// <summary>When overridden in a derived class, deletes the row at the specified index from the underlying list.</summary>
		/// <param name="index">The index of the row to delete.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">There is no row at the specified <paramref name="index" />.</exception>
		// Token: 0x06000B39 RID: 2873
		public abstract void RemoveAt(int index);

		/// <summary>When overridden in a derived class, gets or sets the position in the underlying list that controls bound to this data source point to.</summary>
		/// <returns>A zero-based index that specifies a position in the underlying list.</returns>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B3A RID: 2874
		// (set) Token: 0x06000B3B RID: 2875
		public abstract int Position { get; set; }

		/// <summary>Occurs after the value of the <see cref="P:System.Windows.Forms.BindingManagerBase.Position" /> property has changed.</summary>
		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06000B3C RID: 2876 RVA: 0x0001FF66 File Offset: 0x0001E166
		// (remove) Token: 0x06000B3D RID: 2877 RVA: 0x0001FF7F File Offset: 0x0001E17F
		public event EventHandler PositionChanged
		{
			add
			{
				this.onPositionChangedHandler = (EventHandler)Delegate.Combine(this.onPositionChangedHandler, value);
			}
			remove
			{
				this.onPositionChangedHandler = (EventHandler)Delegate.Remove(this.onPositionChangedHandler, value);
			}
		}

		/// <summary>When overridden in a derived class, updates the binding.</summary>
		// Token: 0x06000B3E RID: 2878
		protected abstract void UpdateIsBinding();

		/// <summary>When overridden in a derived class, gets the name of the list supplying the data for the binding.</summary>
		/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> containing the table's bound properties.</param>
		/// <returns>The name of the list supplying the data for the binding.</returns>
		// Token: 0x06000B3F RID: 2879
		protected internal abstract string GetListName(ArrayList listAccessors);

		/// <summary>When overridden in a derived class, suspends data binding.</summary>
		// Token: 0x06000B40 RID: 2880
		public abstract void SuspendBinding();

		/// <summary>When overridden in a derived class, resumes data binding.</summary>
		// Token: 0x06000B41 RID: 2881
		public abstract void ResumeBinding();

		/// <summary>Pulls data from the data-bound control into the data source, returning no information.</summary>
		// Token: 0x06000B42 RID: 2882 RVA: 0x0001FF98 File Offset: 0x0001E198
		protected void PullData()
		{
			bool flag;
			this.PullData(out flag);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
		internal void PullData(out bool success)
		{
			success = true;
			this.pullingData = true;
			try
			{
				this.UpdateIsBinding();
				int count = this.Bindings.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.Bindings[i].PullData())
					{
						success = false;
					}
				}
			}
			finally
			{
				this.pullingData = false;
			}
		}

		/// <summary>Pushes data from the data source into the data-bound control, returning no information.</summary>
		// Token: 0x06000B44 RID: 2884 RVA: 0x00020018 File Offset: 0x0001E218
		protected void PushData()
		{
			bool flag;
			this.PushData(out flag);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00020030 File Offset: 0x0001E230
		internal void PushData(out bool success)
		{
			success = true;
			if (this.pullingData)
			{
				return;
			}
			this.UpdateIsBinding();
			int count = this.Bindings.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.Bindings[i].PushData())
				{
					success = false;
				}
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B46 RID: 2886
		internal abstract object DataSource { get; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B47 RID: 2887
		internal abstract bool IsBinding { get; }

		/// <summary>Gets a value indicating whether binding is suspended.</summary>
		/// <returns>
		///   <see langword="true" /> if binding is suspended; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0002007D File Offset: 0x0001E27D
		public bool IsBindingSuspended
		{
			get
			{
				return !this.IsBinding;
			}
		}

		/// <summary>When overridden in a derived class, gets the number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</summary>
		/// <returns>The number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</returns>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B49 RID: 2889
		public abstract int Count { get; }

		// Token: 0x06000B4A RID: 2890 RVA: 0x00020088 File Offset: 0x0001E288
		private void OnBindingsCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			Binding binding = e.Element as Binding;
			switch (e.Action)
			{
			case CollectionChangeAction.Add:
				binding.BindingComplete += this.Binding_BindingComplete;
				return;
			case CollectionChangeAction.Remove:
				binding.BindingComplete -= this.Binding_BindingComplete;
				return;
			case CollectionChangeAction.Refresh:
				foreach (object obj in this.bindings)
				{
					Binding binding2 = (Binding)obj;
					binding2.BindingComplete += this.Binding_BindingComplete;
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00020140 File Offset: 0x0001E340
		private void OnBindingsCollectionChanging(object sender, CollectionChangeEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Refresh)
			{
				foreach (object obj in this.bindings)
				{
					Binding binding = (Binding)obj;
					binding.BindingComplete -= this.Binding_BindingComplete;
				}
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000201B0 File Offset: 0x0001E3B0
		internal void Binding_BindingComplete(object sender, BindingCompleteEventArgs args)
		{
			this.OnBindingComplete(args);
		}

		// Token: 0x040006B4 RID: 1716
		private BindingsCollection bindings;

		// Token: 0x040006B5 RID: 1717
		private bool pullingData;

		/// <summary>Specifies the event handler for the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentChanged" /> event.</summary>
		// Token: 0x040006B6 RID: 1718
		protected EventHandler onCurrentChangedHandler;

		/// <summary>Specifies the event handler for the <see cref="E:System.Windows.Forms.BindingManagerBase.PositionChanged" /> event.</summary>
		// Token: 0x040006B7 RID: 1719
		protected EventHandler onPositionChangedHandler;

		// Token: 0x040006B8 RID: 1720
		private BindingCompleteEventHandler onBindingCompleteHandler;

		// Token: 0x040006B9 RID: 1721
		internal EventHandler onCurrentItemChangedHandler;

		// Token: 0x040006BA RID: 1722
		internal BindingManagerDataErrorEventHandler onDataErrorHandler;
	}
}
