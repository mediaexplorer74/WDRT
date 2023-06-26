using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.CounterCreationData" /> objects.</summary>
	// Token: 0x020004C0 RID: 1216
	[Serializable]
	public class CounterCreationDataCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> class, with no associated <see cref="T:System.Diagnostics.CounterCreationData" /> instances.</summary>
		// Token: 0x06002D62 RID: 11618 RVA: 0x000CC4B7 File Offset: 0x000CA6B7
		public CounterCreationDataCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> class by using the specified collection of <see cref="T:System.Diagnostics.CounterCreationData" /> instances.</summary>
		/// <param name="value">A <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> that holds <see cref="T:System.Diagnostics.CounterCreationData" /> instances with which to initialize this <see cref="T:System.Diagnostics.CounterCreationDataCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002D63 RID: 11619 RVA: 0x000CC4BF File Offset: 0x000CA6BF
		public CounterCreationDataCollection(CounterCreationDataCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> class by using the specified array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances.</summary>
		/// <param name="value">An array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances with which to initialize this <see cref="T:System.Diagnostics.CounterCreationDataCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002D64 RID: 11620 RVA: 0x000CC4CE File Offset: 0x000CA6CE
		public CounterCreationDataCollection(CounterCreationData[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Indexes the <see cref="T:System.Diagnostics.CounterCreationData" /> collection.</summary>
		/// <param name="index">An index into the <see cref="T:System.Diagnostics.CounterCreationDataCollection" />.</param>
		/// <returns>The collection index, which is used to access individual elements of the collection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the number of items in the collection.</exception>
		// Token: 0x17000AF4 RID: 2804
		public CounterCreationData this[int index]
		{
			get
			{
				return (CounterCreationData)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds an instance of the <see cref="T:System.Diagnostics.CounterCreationData" /> class to the collection.</summary>
		/// <param name="value">A <see cref="T:System.Diagnostics.CounterCreationData" /> object to append to the existing collection.</param>
		/// <returns>The index of the new <see cref="T:System.Diagnostics.CounterCreationData" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.CounterCreationData" /> object.</exception>
		// Token: 0x06002D67 RID: 11623 RVA: 0x000CC4FF File Offset: 0x000CA6FF
		public int Add(CounterCreationData value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds the specified array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to the collection.</summary>
		/// <param name="value">An array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to append to the existing collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002D68 RID: 11624 RVA: 0x000CC510 File Offset: 0x000CA710
		public void AddRange(CounterCreationData[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Adds the specified collection of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to the collection.</summary>
		/// <param name="value">A collection of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to append to the existing collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002D69 RID: 11625 RVA: 0x000CC544 File Offset: 0x000CA744
		public void AddRange(CounterCreationDataCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Determines whether a <see cref="T:System.Diagnostics.CounterCreationData" /> instance exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.CounterCreationData" /> object to find in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Diagnostics.CounterCreationData" /> object exists in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002D6A RID: 11626 RVA: 0x000CC580 File Offset: 0x000CA780
		public bool Contains(CounterCreationData value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Diagnostics.CounterCreationData" /> to an array, starting at the specified index of the array.</summary>
		/// <param name="array">An array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to add to the collection.</param>
		/// <param name="index">The location at which to add the new instances.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		// Token: 0x06002D6B RID: 11627 RVA: 0x000CC58E File Offset: 0x000CA78E
		public void CopyTo(CounterCreationData[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Returns the index of a <see cref="T:System.Diagnostics.CounterCreationData" /> object in the collection.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.CounterCreationData" /> object to locate in the collection.</param>
		/// <returns>The zero-based index of the specified <see cref="T:System.Diagnostics.CounterCreationData" />, if it is found, in the collection; otherwise, -1.</returns>
		// Token: 0x06002D6C RID: 11628 RVA: 0x000CC59D File Offset: 0x000CA79D
		public int IndexOf(CounterCreationData value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Diagnostics.CounterCreationData" /> object into the collection, at the specified index.</summary>
		/// <param name="index">The zero-based index of the location at which the <see cref="T:System.Diagnostics.CounterCreationData" /> is to be inserted.</param>
		/// <param name="value">The <see cref="T:System.Diagnostics.CounterCreationData" /> to insert into the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.CounterCreationData" /> object.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is greater than the number of items in the collection.</exception>
		// Token: 0x06002D6D RID: 11629 RVA: 0x000CC5AB File Offset: 0x000CA7AB
		public void Insert(int index, CounterCreationData value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes a <see cref="T:System.Diagnostics.CounterCreationData" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.CounterCreationData" /> to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.CounterCreationData" /> object.  
		/// -or-  
		/// <paramref name="value" /> does not exist in the collection.</exception>
		// Token: 0x06002D6E RID: 11630 RVA: 0x000CC5BA File Offset: 0x000CA7BA
		public virtual void Remove(CounterCreationData value)
		{
			base.List.Remove(value);
		}

		/// <summary>Checks the specified object to determine whether it is a valid <see cref="T:System.Diagnostics.CounterCreationData" /> type.</summary>
		/// <param name="value">The object that will be validated.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.CounterCreationData" /> object.</exception>
		// Token: 0x06002D6F RID: 11631 RVA: 0x000CC5C8 File Offset: 0x000CA7C8
		protected override void OnValidate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is CounterCreationData))
			{
				throw new ArgumentException(SR.GetString("MustAddCounterCreationData"));
			}
		}
	}
}
