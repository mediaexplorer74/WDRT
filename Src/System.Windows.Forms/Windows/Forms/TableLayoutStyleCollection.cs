using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality for a collection of table layout styles.</summary>
	// Token: 0x02000397 RID: 919
	[Editor("System.Windows.Forms.Design.StyleCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public abstract class TableLayoutStyleCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06003C25 RID: 15397 RVA: 0x00106BE0 File Offset: 0x00104DE0
		internal TableLayoutStyleCollection(IArrangedElement owner)
		{
			this._owner = owner;
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06003C26 RID: 15398 RVA: 0x00106BFA File Offset: 0x00104DFA
		internal IArrangedElement Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06003C27 RID: 15399 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual string PropertyName
		{
			get
			{
				return null;
			}
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.Add(System.Object)" /> method.</summary>
		/// <param name="style">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The position into which <paramref name="style" /> was inserted.</returns>
		// Token: 0x06003C28 RID: 15400 RVA: 0x00106C04 File Offset: 0x00104E04
		int IList.Add(object style)
		{
			this.EnsureNotOwned((TableLayoutStyle)style);
			((TableLayoutStyle)style).Owner = this.Owner;
			int num = this._innerList.Add(style);
			this.PerformLayoutIfOwned();
			return num;
		}

		/// <summary>Adds a new <see cref="T:System.Windows.Forms.TableLayoutStyle" /> to the end of the current collection.</summary>
		/// <param name="style">The <see cref="T:System.Windows.Forms.TableLayoutStyle" /> to add to the <see cref="T:System.Windows.Forms.TableLayoutStyleCollection" />.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is already assigned to another owner. You must first remove it from its current location or clone it.</exception>
		// Token: 0x06003C29 RID: 15401 RVA: 0x00106C42 File Offset: 0x00104E42
		public int Add(TableLayoutStyle style)
		{
			return ((IList)this).Add(style);
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index at which <paramref name="style" /> should be inserted.</param>
		/// <param name="style">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is already assigned to another owner. You must first remove it from its current location or clone it.</exception>
		// Token: 0x06003C2A RID: 15402 RVA: 0x00106C4B File Offset: 0x00104E4B
		void IList.Insert(int index, object style)
		{
			this.EnsureNotOwned((TableLayoutStyle)style);
			((TableLayoutStyle)style).Owner = this.Owner;
			this._innerList.Insert(index, style);
			this.PerformLayoutIfOwned();
		}

		/// <summary>For a description of this method, see the <see cref="P:System.Collections.IList.Item(System.Int32)" /> property.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		// Token: 0x17000EA6 RID: 3750
		object IList.this[int index]
		{
			get
			{
				return this._innerList[index];
			}
			set
			{
				TableLayoutStyle tableLayoutStyle = (TableLayoutStyle)value;
				this.EnsureNotOwned(tableLayoutStyle);
				tableLayoutStyle.Owner = this.Owner;
				this._innerList[index] = tableLayoutStyle;
				this.PerformLayoutIfOwned();
			}
		}

		/// <summary>Gets or sets <see cref="T:System.Windows.Forms.TableLayoutStyle" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.TableLayoutStyle" /> to get or set.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutStyle" /> at the specified index.</returns>
		/// <exception cref="T:System.ArgumentException">The property value is already assigned to another owner. You must first remove it from its current location or clone it.</exception>
		// Token: 0x17000EA7 RID: 3751
		public TableLayoutStyle this[int index]
		{
			get
			{
				return (TableLayoutStyle)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method.</summary>
		/// <param name="style">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003C2F RID: 15407 RVA: 0x00106CDE File Offset: 0x00104EDE
		void IList.Remove(object style)
		{
			((TableLayoutStyle)style).Owner = null;
			this._innerList.Remove(style);
			this.PerformLayoutIfOwned();
		}

		/// <summary>Disassociates the collection from its associated <see cref="T:System.Windows.Forms.TableLayoutPanel" /> and empties the collection.</summary>
		// Token: 0x06003C30 RID: 15408 RVA: 0x00106D00 File Offset: 0x00104F00
		public void Clear()
		{
			foreach (object obj in this._innerList)
			{
				TableLayoutStyle tableLayoutStyle = (TableLayoutStyle)obj;
				tableLayoutStyle.Owner = null;
			}
			this._innerList.Clear();
			this.PerformLayoutIfOwned();
		}

		/// <summary>Removes the style at the specified index of the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.TableLayoutStyle" /> to be removed.</param>
		// Token: 0x06003C31 RID: 15409 RVA: 0x00106D6C File Offset: 0x00104F6C
		public void RemoveAt(int index)
		{
			TableLayoutStyle tableLayoutStyle = (TableLayoutStyle)this._innerList[index];
			tableLayoutStyle.Owner = null;
			this._innerList.RemoveAt(index);
			this.PerformLayoutIfOwned();
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.Contains(System.Object)" /> method.</summary>
		/// <param name="style">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="style" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003C32 RID: 15410 RVA: 0x00106DA4 File Offset: 0x00104FA4
		bool IList.Contains(object style)
		{
			return this._innerList.Contains(style);
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method.</summary>
		/// <param name="style">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The index of <paramref name="style" /> if found in the list; otherwise, -1.</returns>
		// Token: 0x06003C33 RID: 15411 RVA: 0x00106DB2 File Offset: 0x00104FB2
		int IList.IndexOf(object style)
		{
			return this._innerList.IndexOf(style);
		}

		/// <summary>For a description of this method, see the <see cref="P:System.Collections.IList.IsFixedSize" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06003C34 RID: 15412 RVA: 0x00106DC0 File Offset: 0x00104FC0
		bool IList.IsFixedSize
		{
			get
			{
				return this._innerList.IsFixedSize;
			}
		}

		/// <summary>For a description of this method, see the <see cref="P:System.Collections.IList.IsReadOnly" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x00106DCD File Offset: 0x00104FCD
		bool IList.IsReadOnly
		{
			get
			{
				return this._innerList.IsReadOnly;
			}
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" /> method.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="startIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06003C36 RID: 15414 RVA: 0x00106DDA File Offset: 0x00104FDA
		void ICollection.CopyTo(Array array, int startIndex)
		{
			this._innerList.CopyTo(array, startIndex);
		}

		/// <summary>Gets the number of styles actually contained in the <see cref="T:System.Windows.Forms.TableLayoutStyleCollection" />.</summary>
		/// <returns>The number of styles actually contained in the <see cref="T:System.Windows.Forms.TableLayoutStyleCollection" />.</returns>
		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06003C37 RID: 15415 RVA: 0x00106DE9 File Offset: 0x00104FE9
		public int Count
		{
			get
			{
				return this._innerList.Count;
			}
		}

		/// <summary>For a description of this method, see the <see cref="P:System.Collections.ICollection.IsSynchronized" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06003C38 RID: 15416 RVA: 0x00106DF6 File Offset: 0x00104FF6
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._innerList.IsSynchronized;
			}
		}

		/// <summary>For a description of this method, see the <see cref="P:System.Collections.ICollection.SyncRoot" /> property.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06003C39 RID: 15417 RVA: 0x00106E03 File Offset: 0x00105003
		object ICollection.SyncRoot
		{
			get
			{
				return this._innerList.SyncRoot;
			}
		}

		/// <summary>For a description of this method, see the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003C3A RID: 15418 RVA: 0x00106E10 File Offset: 0x00105010
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._innerList.GetEnumerator();
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x00106E1D File Offset: 0x0010501D
		private void EnsureNotOwned(TableLayoutStyle style)
		{
			if (style.Owner != null)
			{
				throw new ArgumentException(SR.GetString("OnlyOneControl", new object[] { style.GetType().Name }), "style");
			}
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x00106E50 File Offset: 0x00105050
		internal void EnsureOwnership(IArrangedElement owner)
		{
			this._owner = owner;
			for (int i = 0; i < this.Count; i++)
			{
				this[i].Owner = owner;
			}
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x00106E82 File Offset: 0x00105082
		private void PerformLayoutIfOwned()
		{
			if (this.Owner != null)
			{
				LayoutTransaction.DoLayout(this.Owner, this.Owner, this.PropertyName);
			}
		}

		// Token: 0x04002390 RID: 9104
		private IArrangedElement _owner;

		// Token: 0x04002391 RID: 9105
		private ArrayList _innerList = new ArrayList();
	}
}
