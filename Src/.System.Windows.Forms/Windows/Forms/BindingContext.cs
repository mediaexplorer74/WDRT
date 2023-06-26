using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Manages the collection of <see cref="T:System.Windows.Forms.BindingManagerBase" /> objects for any object that inherits from the <see cref="T:System.Windows.Forms.Control" /> class.</summary>
	// Token: 0x02000133 RID: 307
	[DefaultEvent("CollectionChanged")]
	public class BindingContext : ICollection, IEnumerable
	{
		/// <summary>Gets the total number of <see cref="T:System.Windows.Forms.CurrencyManager" /> objects managed by the <see cref="T:System.Windows.Forms.BindingContext" />.</summary>
		/// <returns>The number of data sources managed by the <see cref="T:System.Windows.Forms.BindingContext" />.</returns>
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0001F6B8 File Offset: 0x0001D8B8
		int ICollection.Count
		{
			get
			{
				this.ScrubWeakRefs();
				return this.listManagers.Count;
			}
		}

		/// <summary>Copies the elements of the collection into a specified array, starting at the collection index.</summary>
		/// <param name="ar">An <see cref="T:System.Array" /> to copy into.</param>
		/// <param name="index">The collection index to begin copying from.</param>
		// Token: 0x06000B07 RID: 2823 RVA: 0x0001F6CB File Offset: 0x0001D8CB
		void ICollection.CopyTo(Array ar, int index)
		{
			IntSecurity.UnmanagedCode.Demand();
			this.ScrubWeakRefs();
			this.listManagers.CopyTo(ar, index);
		}

		/// <summary>Gets an enumerator for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		// Token: 0x06000B08 RID: 2824 RVA: 0x0001F6EA File Offset: 0x0001D8EA
		IEnumerator IEnumerable.GetEnumerator()
		{
			IntSecurity.UnmanagedCode.Demand();
			this.ScrubWeakRefs();
			return this.listManagers.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0001180C File Offset: 0x0000FA0C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the collection is synchronized.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is thread safe; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object to use for synchronization (thread safety).</summary>
		/// <returns>This property is derived from <see cref="T:System.Collections.ICollection" />, and is overridden to always return <see langword="null" />.</returns>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00015C90 File Offset: 0x00013E90
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingContext" /> class.</summary>
		// Token: 0x06000B0C RID: 2828 RVA: 0x0001F707 File Offset: 0x0001D907
		public BindingContext()
		{
			this.listManagers = new Hashtable();
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.BindingManagerBase" /> that is associated with the specified data source.</summary>
		/// <param name="dataSource">The data source associated with a particular <see cref="T:System.Windows.Forms.BindingManagerBase" />.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingManagerBase" /> for the specified data source.</returns>
		// Token: 0x170002C5 RID: 709
		public BindingManagerBase this[object dataSource]
		{
			get
			{
				return this[dataSource, ""];
			}
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.BindingManagerBase" /> that is associated with the specified data source and data member.</summary>
		/// <param name="dataSource">The data source associated with a particular <see cref="T:System.Windows.Forms.BindingManagerBase" />.</param>
		/// <param name="dataMember">A navigation path containing the information that resolves to a specific <see cref="T:System.Windows.Forms.BindingManagerBase" />.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.BindingManagerBase" /> for the specified data source and data member.</returns>
		/// <exception cref="T:System.Exception">The specified <paramref name="dataMember" /> does not exist within the data source.</exception>
		// Token: 0x170002C6 RID: 710
		public BindingManagerBase this[object dataSource, string dataMember]
		{
			get
			{
				return this.EnsureListManager(dataSource, dataMember);
			}
		}

		/// <summary>Adds the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with a specific data source to the collection.</summary>
		/// <param name="dataSource">The <see cref="T:System.Object" /> associated with the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</param>
		/// <param name="listManager">The <see cref="T:System.Windows.Forms.BindingManagerBase" /> to add.</param>
		// Token: 0x06000B0F RID: 2831 RVA: 0x0001F732 File Offset: 0x0001D932
		protected internal void Add(object dataSource, BindingManagerBase listManager)
		{
			this.AddCore(dataSource, listManager);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataSource));
		}

		/// <summary>Adds the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with a specific data source to the collection.</summary>
		/// <param name="dataSource">The object associated with the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</param>
		/// <param name="listManager">The <see cref="T:System.Windows.Forms.BindingManagerBase" /> to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataSource" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="listManager" /> is <see langword="null" />.</exception>
		// Token: 0x06000B10 RID: 2832 RVA: 0x0001F749 File Offset: 0x0001D949
		protected virtual void AddCore(object dataSource, BindingManagerBase listManager)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (listManager == null)
			{
				throw new ArgumentNullException("listManager");
			}
			this.listManagers[this.GetKey(dataSource, "")] = new WeakReference(listManager, false);
		}

		/// <summary>Always raises a <see cref="T:System.NotImplementedException" /> when handled.</summary>
		/// <exception cref="T:System.NotImplementedException">Occurs in all cases.</exception>
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06000B11 RID: 2833 RVA: 0x0001F785 File Offset: 0x0001D985
		// (remove) Token: 0x06000B12 RID: 2834 RVA: 0x000070A6 File Offset: 0x000052A6
		[SRDescription("collectionChangedEventDescr")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				throw new NotImplementedException();
			}
			remove
			{
			}
		}

		/// <summary>Clears the collection of any <see cref="T:System.Windows.Forms.BindingManagerBase" /> objects.</summary>
		// Token: 0x06000B13 RID: 2835 RVA: 0x0001F78C File Offset: 0x0001D98C
		protected internal void Clear()
		{
			this.ClearCore();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		/// <summary>Clears the collection.</summary>
		// Token: 0x06000B14 RID: 2836 RVA: 0x0001F7A1 File Offset: 0x0001D9A1
		protected virtual void ClearCore()
		{
			this.listManagers.Clear();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.BindingContext" /> contains the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with the specified data source.</summary>
		/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.BindingContext" /> contains the specified <see cref="T:System.Windows.Forms.BindingManagerBase" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000B15 RID: 2837 RVA: 0x0001F7AE File Offset: 0x0001D9AE
		public bool Contains(object dataSource)
		{
			return this.Contains(dataSource, "");
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.BindingContext" /> contains the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with the specified data source and data member.</summary>
		/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source.</param>
		/// <param name="dataMember">The information needed to resolve to a specific <see cref="T:System.Windows.Forms.BindingManagerBase" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.BindingContext" /> contains the specified <see cref="T:System.Windows.Forms.BindingManagerBase" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000B16 RID: 2838 RVA: 0x0001F7BC File Offset: 0x0001D9BC
		public bool Contains(object dataSource, string dataMember)
		{
			return this.listManagers.ContainsKey(this.GetKey(dataSource, dataMember));
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001F7D1 File Offset: 0x0001D9D1
		internal BindingContext.HashKey GetKey(object dataSource, string dataMember)
		{
			return new BindingContext.HashKey(dataSource, dataMember);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingContext.CollectionChanged" /> event.</summary>
		/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x06000B18 RID: 2840 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
		}

		/// <summary>Deletes the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with the specified data source.</summary>
		/// <param name="dataSource">The data source associated with the <see cref="T:System.Windows.Forms.BindingManagerBase" /> to remove.</param>
		// Token: 0x06000B19 RID: 2841 RVA: 0x0001F7DA File Offset: 0x0001D9DA
		protected internal void Remove(object dataSource)
		{
			this.RemoveCore(dataSource);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataSource));
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.BindingManagerBase" /> associated with the specified data source.</summary>
		/// <param name="dataSource">The data source associated with the <see cref="T:System.Windows.Forms.BindingManagerBase" /> to remove.</param>
		// Token: 0x06000B1A RID: 2842 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
		protected virtual void RemoveCore(object dataSource)
		{
			this.listManagers.Remove(this.GetKey(dataSource, ""));
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0001F80C File Offset: 0x0001DA0C
		internal BindingManagerBase EnsureListManager(object dataSource, string dataMember)
		{
			BindingManagerBase bindingManagerBase = null;
			if (dataMember == null)
			{
				dataMember = "";
			}
			if (dataSource is ICurrencyManagerProvider)
			{
				bindingManagerBase = (dataSource as ICurrencyManagerProvider).GetRelatedCurrencyManager(dataMember);
				if (bindingManagerBase != null)
				{
					return bindingManagerBase;
				}
			}
			BindingContext.HashKey key = this.GetKey(dataSource, dataMember);
			WeakReference weakReference = this.listManagers[key] as WeakReference;
			if (weakReference != null)
			{
				bindingManagerBase = (BindingManagerBase)weakReference.Target;
			}
			if (bindingManagerBase != null)
			{
				return bindingManagerBase;
			}
			if (dataMember.Length == 0)
			{
				if (dataSource is IList || dataSource is IListSource)
				{
					bindingManagerBase = new CurrencyManager(dataSource);
				}
				else
				{
					bindingManagerBase = new PropertyManager(dataSource);
				}
			}
			else
			{
				int num = dataMember.LastIndexOf(".");
				string text = ((num == -1) ? "" : dataMember.Substring(0, num));
				string text2 = dataMember.Substring(num + 1);
				BindingManagerBase bindingManagerBase2 = this.EnsureListManager(dataSource, text);
				PropertyDescriptor propertyDescriptor = bindingManagerBase2.GetItemProperties().Find(text2, true);
				if (propertyDescriptor == null)
				{
					throw new ArgumentException(SR.GetString("RelatedListManagerChild", new object[] { text2 }));
				}
				if (typeof(IList).IsAssignableFrom(propertyDescriptor.PropertyType))
				{
					bindingManagerBase = new RelatedCurrencyManager(bindingManagerBase2, text2);
				}
				else
				{
					bindingManagerBase = new RelatedPropertyManager(bindingManagerBase2, text2);
				}
			}
			if (weakReference == null)
			{
				this.listManagers.Add(key, new WeakReference(bindingManagerBase, false));
			}
			else
			{
				weakReference.Target = bindingManagerBase;
			}
			IntSecurity.UnmanagedCode.Demand();
			this.ScrubWeakRefs();
			return bindingManagerBase;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0001F964 File Offset: 0x0001DB64
		private static void CheckPropertyBindingCycles(BindingContext newBindingContext, Binding propBinding)
		{
			if (newBindingContext == null || propBinding == null)
			{
				return;
			}
			if (newBindingContext.Contains(propBinding.BindableComponent, ""))
			{
				BindingManagerBase bindingManagerBase = newBindingContext.EnsureListManager(propBinding.BindableComponent, "");
				for (int i = 0; i < bindingManagerBase.Bindings.Count; i++)
				{
					Binding binding = bindingManagerBase.Bindings[i];
					if (binding.DataSource == propBinding.BindableComponent)
					{
						if (propBinding.BindToObject.BindingMemberInfo.BindingMember.Equals(binding.PropertyName))
						{
							throw new ArgumentException(SR.GetString("DataBindingCycle", new object[] { binding.PropertyName }), "propBinding");
						}
					}
					else if (propBinding.BindToObject.BindingManagerBase is PropertyManager)
					{
						BindingContext.CheckPropertyBindingCycles(newBindingContext, binding);
					}
				}
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0001FA34 File Offset: 0x0001DC34
		private void ScrubWeakRefs()
		{
			ArrayList arrayList = null;
			foreach (object obj in this.listManagers)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				WeakReference weakReference = (WeakReference)dictionaryEntry.Value;
				if (weakReference.Target == null)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(dictionaryEntry.Key);
				}
			}
			if (arrayList != null)
			{
				foreach (object obj2 in arrayList)
				{
					this.listManagers.Remove(obj2);
				}
			}
		}

		/// <summary>Associates a <see cref="T:System.Windows.Forms.Binding" /> with a new <see cref="T:System.Windows.Forms.BindingContext" />.</summary>
		/// <param name="newBindingContext">The new <see cref="T:System.Windows.Forms.BindingContext" /> to associate with the <see cref="T:System.Windows.Forms.Binding" />.</param>
		/// <param name="binding">The <see cref="T:System.Windows.Forms.Binding" /> to associate with the new <see cref="T:System.Windows.Forms.BindingContext" />.</param>
		// Token: 0x06000B1E RID: 2846 RVA: 0x0001FB08 File Offset: 0x0001DD08
		public static void UpdateBinding(BindingContext newBindingContext, Binding binding)
		{
			BindingManagerBase bindingManagerBase = binding.BindingManagerBase;
			if (bindingManagerBase != null)
			{
				bindingManagerBase.Bindings.Remove(binding);
			}
			if (newBindingContext != null)
			{
				if (binding.BindToObject.BindingManagerBase is PropertyManager)
				{
					BindingContext.CheckPropertyBindingCycles(newBindingContext, binding);
				}
				BindToObject bindToObject = binding.BindToObject;
				BindingManagerBase bindingManagerBase2 = newBindingContext.EnsureListManager(bindToObject.DataSource, bindToObject.BindingMemberInfo.BindingPath);
				bindingManagerBase2.Bindings.Add(binding);
			}
		}

		// Token: 0x040006B3 RID: 1715
		private Hashtable listManagers;

		// Token: 0x02000619 RID: 1561
		internal class HashKey
		{
			// Token: 0x060062EF RID: 25327 RVA: 0x0016DFE8 File Offset: 0x0016C1E8
			internal HashKey(object dataSource, string dataMember)
			{
				if (dataSource == null)
				{
					throw new ArgumentNullException("dataSource");
				}
				if (dataMember == null)
				{
					dataMember = "";
				}
				this.wRef = new WeakReference(dataSource, false);
				this.dataSourceHashCode = dataSource.GetHashCode();
				this.dataMember = dataMember.ToLower(CultureInfo.InvariantCulture);
			}

			// Token: 0x060062F0 RID: 25328 RVA: 0x0016E03D File Offset: 0x0016C23D
			public override int GetHashCode()
			{
				return this.dataSourceHashCode * this.dataMember.GetHashCode();
			}

			// Token: 0x060062F1 RID: 25329 RVA: 0x0016E054 File Offset: 0x0016C254
			public override bool Equals(object target)
			{
				if (target is BindingContext.HashKey)
				{
					BindingContext.HashKey hashKey = (BindingContext.HashKey)target;
					return this.wRef.Target == hashKey.wRef.Target && this.dataMember == hashKey.dataMember;
				}
				return false;
			}

			// Token: 0x04003912 RID: 14610
			private WeakReference wRef;

			// Token: 0x04003913 RID: 14611
			private int dataSourceHashCode;

			// Token: 0x04003914 RID: 14612
			private string dataMember;
		}
	}
}
