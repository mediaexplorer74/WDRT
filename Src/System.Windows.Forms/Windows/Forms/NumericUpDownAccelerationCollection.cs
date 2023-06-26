using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a sorted collection of <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> objects in the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</summary>
	// Token: 0x0200030E RID: 782
	[ListBindable(false)]
	public class NumericUpDownAccelerationCollection : MarshalByRefObject, ICollection<NumericUpDownAcceleration>, IEnumerable<NumericUpDownAcceleration>, IEnumerable
	{
		/// <summary>Adds a new <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</summary>
		/// <param name="acceleration">The <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to add to the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="acceleration" /> is <see langword="null" />.</exception>
		// Token: 0x060031F5 RID: 12789 RVA: 0x000E1074 File Offset: 0x000DF274
		public void Add(NumericUpDownAcceleration acceleration)
		{
			if (acceleration == null)
			{
				throw new ArgumentNullException("acceleration");
			}
			int num = 0;
			while (num < this.items.Count && acceleration.Seconds >= this.items[num].Seconds)
			{
				num++;
			}
			this.items.Insert(num, acceleration);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</summary>
		// Token: 0x060031F6 RID: 12790 RVA: 0x000E10CB File Offset: 0x000DF2CB
		public void Clear()
		{
			this.items.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> contains a specific <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" />.</summary>
		/// <param name="acceleration">The <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to locate in the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> is found in the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060031F7 RID: 12791 RVA: 0x000E10D8 File Offset: 0x000DF2D8
		public bool Contains(NumericUpDownAcceleration acceleration)
		{
			return this.items.Contains(acceleration);
		}

		/// <summary>Copies the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> values to a one-dimensional <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> that is the destination of the values copied from <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</param>
		/// <param name="index">The index in <paramref name="array" /> where copying begins.</param>
		// Token: 0x060031F8 RID: 12792 RVA: 0x000E10E6 File Offset: 0x000DF2E6
		public void CopyTo(NumericUpDownAcceleration[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Gets the number of objects in the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</summary>
		/// <returns>The number of objects in the collection.</returns>
		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x000E10F5 File Offset: 0x000DF2F5
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x0001180C File Offset: 0x0000FA0C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> from the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</summary>
		/// <param name="acceleration">The <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to remove from the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> is removed from <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060031FB RID: 12795 RVA: 0x000E1102 File Offset: 0x000DF302
		public bool Remove(NumericUpDownAcceleration acceleration)
		{
			return this.items.Remove(acceleration);
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000E1110 File Offset: 0x000DF310
		IEnumerator<NumericUpDownAcceleration> IEnumerable<NumericUpDownAcceleration>.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Gets the enumerator for the collection.</summary>
		/// <returns>An iteration over the collection.</returns>
		// Token: 0x060031FD RID: 12797 RVA: 0x000E1122 File Offset: 0x000DF322
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.items).GetEnumerator();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> class.</summary>
		// Token: 0x060031FE RID: 12798 RVA: 0x000E112F File Offset: 0x000DF32F
		public NumericUpDownAccelerationCollection()
		{
			this.items = new List<NumericUpDownAcceleration>();
		}

		/// <summary>Adds the elements of the specified array to the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />, keeping the collection sorted.</summary>
		/// <param name="accelerations">An array of type <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="accelerations" /> is <see langword="null" />, or one of the entries in the <paramref name="accelerations" /> array is <see langword="null" />.</exception>
		// Token: 0x060031FF RID: 12799 RVA: 0x000E1144 File Offset: 0x000DF344
		public void AddRange(params NumericUpDownAcceleration[] accelerations)
		{
			if (accelerations == null)
			{
				throw new ArgumentNullException("accelerations");
			}
			for (int i = 0; i < accelerations.Length; i++)
			{
				if (accelerations[i] == null)
				{
					throw new ArgumentNullException(SR.GetString("NumericUpDownAccelerationCollectionAtLeastOneEntryIsNull"));
				}
			}
			foreach (NumericUpDownAcceleration numericUpDownAcceleration in accelerations)
			{
				this.Add(numericUpDownAcceleration);
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> at the specified index number.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to get from the collection.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> with the specified index.</returns>
		// Token: 0x17000BB6 RID: 2998
		public NumericUpDownAcceleration this[int index]
		{
			get
			{
				return this.items[index];
			}
		}

		// Token: 0x04001E54 RID: 7764
		private List<NumericUpDownAcceleration> items;
	}
}
