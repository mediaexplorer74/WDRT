using System;
using System.Collections;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>A collection that stores <see cref="T:System.Windows.Forms.ColumnStyle" /> objects.</summary>
	// Token: 0x0200039A RID: 922
	public class TableLayoutColumnStyleCollection : TableLayoutStyleCollection
	{
		// Token: 0x06003C46 RID: 15430 RVA: 0x00106EDC File Offset: 0x001050DC
		internal TableLayoutColumnStyleCollection(IArrangedElement Owner)
			: base(Owner)
		{
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x00106EE5 File Offset: 0x001050E5
		internal TableLayoutColumnStyleCollection()
			: base(null)
		{
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06003C48 RID: 15432 RVA: 0x00106EEE File Offset: 0x001050EE
		internal override string PropertyName
		{
			get
			{
				return PropertyNames.ColumnStyles;
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</summary>
		/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to add to the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		// Token: 0x06003C49 RID: 15433 RVA: 0x00106C42 File Offset: 0x00104E42
		public int Add(ColumnStyle columnStyle)
		{
			return ((IList)this).Add(columnStyle);
		}

		/// <summary>Inserts a <see cref="T:System.Windows.Forms.ColumnStyle" /> into the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" /> at the specified position.</summary>
		/// <param name="index">The zero-based index at which <see cref="T:System.Windows.Forms.ColumnStyle" /> should be inserted.</param>
		/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to insert into the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</param>
		// Token: 0x06003C4A RID: 15434 RVA: 0x00106EF5 File Offset: 0x001050F5
		public void Insert(int index, ColumnStyle columnStyle)
		{
			((IList)this).Insert(index, columnStyle);
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ColumnStyle" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.ColumnStyle" /> to get or set.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.ColumnStyle" /> at the specified index.</returns>
		// Token: 0x17000EB1 RID: 3761
		public ColumnStyle this[int index]
		{
			get
			{
				return (ColumnStyle)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		/// <summary>Removes the first occurrence of a specific <see cref="T:System.Windows.Forms.ColumnStyle" /> from the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</summary>
		/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to remove from the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />. The value can be <see langword="null" />.</param>
		// Token: 0x06003C4D RID: 15437 RVA: 0x00106F0D File Offset: 0x0010510D
		public void Remove(ColumnStyle columnStyle)
		{
			((IList)this).Remove(columnStyle);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Windows.Forms.ColumnStyle" /> is in the collection.</summary>
		/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to locate in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ColumnStyle" /> is found in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003C4E RID: 15438 RVA: 0x00106F16 File Offset: 0x00105116
		public bool Contains(ColumnStyle columnStyle)
		{
			return ((IList)this).Contains(columnStyle);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</summary>
		/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to locate in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</param>
		/// <returns>The index of <paramref name="columnStyle" /> if found in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />; otherwise, -1.</returns>
		// Token: 0x06003C4F RID: 15439 RVA: 0x00106F1F File Offset: 0x0010511F
		public int IndexOf(ColumnStyle columnStyle)
		{
			return ((IList)this).IndexOf(columnStyle);
		}
	}
}
