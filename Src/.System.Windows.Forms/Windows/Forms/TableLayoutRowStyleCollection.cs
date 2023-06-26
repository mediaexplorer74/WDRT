using System;
using System.Collections;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>A collection that stores <see cref="T:System.Windows.Forms.RowStyle" /> objects.</summary>
	// Token: 0x0200039B RID: 923
	public class TableLayoutRowStyleCollection : TableLayoutStyleCollection
	{
		// Token: 0x06003C50 RID: 15440 RVA: 0x00106EDC File Offset: 0x001050DC
		internal TableLayoutRowStyleCollection(IArrangedElement Owner)
			: base(Owner)
		{
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x00106EE5 File Offset: 0x001050E5
		internal TableLayoutRowStyleCollection()
			: base(null)
		{
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06003C52 RID: 15442 RVA: 0x00106F28 File Offset: 0x00105128
		internal override string PropertyName
		{
			get
			{
				return PropertyNames.RowStyles;
			}
		}

		/// <summary>Adds a new <see cref="T:System.Windows.Forms.RowStyle" /> to the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />.</summary>
		/// <param name="rowStyle">The <see cref="T:System.Windows.Forms.RowStyle" /> to add to the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		// Token: 0x06003C53 RID: 15443 RVA: 0x00106C42 File Offset: 0x00104E42
		public int Add(RowStyle rowStyle)
		{
			return ((IList)this).Add(rowStyle);
		}

		/// <summary>Inserts a <see cref="T:System.Windows.Forms.RowStyle" /> into the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" /> at the specified position.</summary>
		/// <param name="index">The zero-based index at which the <see cref="T:System.Windows.Forms.RowStyle" /> should be inserted.</param>
		/// <param name="rowStyle">The <see cref="T:System.Windows.Forms.RowStyle" /> to insert into the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />. The value can be <see langword="null" />.</param>
		// Token: 0x06003C54 RID: 15444 RVA: 0x00106EF5 File Offset: 0x001050F5
		public void Insert(int index, RowStyle rowStyle)
		{
			((IList)this).Insert(index, rowStyle);
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.RowStyle" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.RowStyle" /> to get or set.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.RowStyle" /> at the specified index.</returns>
		// Token: 0x17000EB3 RID: 3763
		public RowStyle this[int index]
		{
			get
			{
				return (RowStyle)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />.</summary>
		/// <param name="rowStyle">The <see cref="T:System.Windows.Forms.RowStyle" /> to remove from the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />. The value can be <see langword="null" />.</param>
		// Token: 0x06003C57 RID: 15447 RVA: 0x00106F0D File Offset: 0x0010510D
		public void Remove(RowStyle rowStyle)
		{
			((IList)this).Remove(rowStyle);
		}

		/// <summary>Determines whether the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" /> contains a specific style.</summary>
		/// <param name="rowStyle">The <see cref="T:System.Windows.Forms.RowStyle" /> to locate in the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.RowStyle" /> is found in the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003C58 RID: 15448 RVA: 0x00106F16 File Offset: 0x00105116
		public bool Contains(RowStyle rowStyle)
		{
			return ((IList)this).Contains(rowStyle);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />.</summary>
		/// <param name="rowStyle">The <see cref="T:System.Windows.Forms.RowStyle" /> to locate in the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />.</param>
		/// <returns>The index of <paramref name="rowStyle" /> if found in the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />; otherwise, -1.</returns>
		// Token: 0x06003C59 RID: 15449 RVA: 0x00106F1F File Offset: 0x0010511F
		public int IndexOf(RowStyle rowStyle)
		{
			return ((IList)this).IndexOf(rowStyle);
		}
	}
}
