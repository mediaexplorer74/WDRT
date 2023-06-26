using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.Binding" /> objects for a control.</summary>
	// Token: 0x02000139 RID: 313
	[DefaultEvent("CollectionChanged")]
	public class BindingsCollection : BaseCollection
	{
		// Token: 0x06000B91 RID: 2961 RVA: 0x000210C7 File Offset: 0x0001F2C7
		internal BindingsCollection()
		{
		}

		/// <summary>Gets the total number of bindings in the collection.</summary>
		/// <returns>The total number of bindings in the collection.</returns>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x000210CF File Offset: 0x0001F2CF
		public override int Count
		{
			get
			{
				if (this.list == null)
				{
					return 0;
				}
				return base.Count;
			}
		}

		/// <summary>Gets the bindings in the collection as an object.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing all of the collection members.</returns>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x000210E1 File Offset: 0x0001F2E1
		protected override ArrayList List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Binding" /> at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.Binding" /> to find.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.Binding" /> at the specified index.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The collection doesn't contain an item at the specified index.</exception>
		// Token: 0x170002DF RID: 735
		public Binding this[int index]
		{
			get
			{
				return (Binding)this.List[index];
			}
		}

		/// <summary>Adds the specified binding to the collection.</summary>
		/// <param name="binding">The <see cref="T:System.Windows.Forms.Binding" /> to add to the collection.</param>
		// Token: 0x06000B95 RID: 2965 RVA: 0x00021110 File Offset: 0x0001F310
		protected internal void Add(Binding binding)
		{
			CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Add, binding);
			this.OnCollectionChanging(collectionChangeEventArgs);
			this.AddCore(binding);
			this.OnCollectionChanged(collectionChangeEventArgs);
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.Binding" /> to the collection.</summary>
		/// <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding" /> to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataBinding" /> argument was <see langword="null" />.</exception>
		// Token: 0x06000B96 RID: 2966 RVA: 0x0002113A File Offset: 0x0001F33A
		protected virtual void AddCore(Binding dataBinding)
		{
			if (dataBinding == null)
			{
				throw new ArgumentNullException("dataBinding");
			}
			this.List.Add(dataBinding);
		}

		/// <summary>Occurs when the collection is about to change.</summary>
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06000B97 RID: 2967 RVA: 0x00021157 File Offset: 0x0001F357
		// (remove) Token: 0x06000B98 RID: 2968 RVA: 0x00021170 File Offset: 0x0001F370
		[SRDescription("collectionChangingEventDescr")]
		public event CollectionChangeEventHandler CollectionChanging
		{
			add
			{
				this.onCollectionChanging = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChanging, value);
			}
			remove
			{
				this.onCollectionChanging = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChanging, value);
			}
		}

		/// <summary>Occurs when the collection has changed.</summary>
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06000B99 RID: 2969 RVA: 0x00021189 File Offset: 0x0001F389
		// (remove) Token: 0x06000B9A RID: 2970 RVA: 0x000211A2 File Offset: 0x0001F3A2
		[SRDescription("collectionChangedEventDescr")]
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChanged, value);
			}
			remove
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChanged, value);
			}
		}

		/// <summary>Clears the collection of binding objects.</summary>
		// Token: 0x06000B9B RID: 2971 RVA: 0x000211BC File Offset: 0x0001F3BC
		protected internal void Clear()
		{
			CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null);
			this.OnCollectionChanging(collectionChangeEventArgs);
			this.ClearCore();
			this.OnCollectionChanged(collectionChangeEventArgs);
		}

		/// <summary>Clears the collection of any members.</summary>
		// Token: 0x06000B9C RID: 2972 RVA: 0x000211E5 File Offset: 0x0001F3E5
		protected virtual void ClearCore()
		{
			this.List.Clear();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingsCollection.CollectionChanging" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains event data.</param>
		// Token: 0x06000B9D RID: 2973 RVA: 0x000211F2 File Offset: 0x0001F3F2
		protected virtual void OnCollectionChanging(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanging != null)
			{
				this.onCollectionChanging(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingsCollection.CollectionChanged" /> event.</summary>
		/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x06000B9E RID: 2974 RVA: 0x00021209 File Offset: 0x0001F409
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, ccevent);
			}
		}

		/// <summary>Deletes the specified binding from the collection.</summary>
		/// <param name="binding">The Binding to remove from the collection.</param>
		// Token: 0x06000B9F RID: 2975 RVA: 0x00021220 File Offset: 0x0001F420
		protected internal void Remove(Binding binding)
		{
			CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Remove, binding);
			this.OnCollectionChanging(collectionChangeEventArgs);
			this.RemoveCore(binding);
			this.OnCollectionChanged(collectionChangeEventArgs);
		}

		/// <summary>Deletes the binding from the collection at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.Binding" /> to remove.</param>
		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002124A File Offset: 0x0001F44A
		protected internal void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.Binding" /> from the collection.</summary>
		/// <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding" /> to remove.</param>
		// Token: 0x06000BA1 RID: 2977 RVA: 0x00021259 File Offset: 0x0001F459
		protected virtual void RemoveCore(Binding dataBinding)
		{
			this.List.Remove(dataBinding);
		}

		/// <summary>Gets a value that indicates whether the collection should be serialized.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection count is greater than zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000BA2 RID: 2978 RVA: 0x00021267 File Offset: 0x0001F467
		protected internal bool ShouldSerializeMyAll()
		{
			return this.Count > 0;
		}

		// Token: 0x040006CC RID: 1740
		private ArrayList list;

		// Token: 0x040006CD RID: 1741
		private CollectionChangeEventHandler onCollectionChanging;

		// Token: 0x040006CE RID: 1742
		private CollectionChangeEventHandler onCollectionChanged;
	}
}
