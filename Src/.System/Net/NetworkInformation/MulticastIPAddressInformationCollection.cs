using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> types.</summary>
	// Token: 0x020002AD RID: 685
	[global::__DynamicallyInvokable]
	public class MulticastIPAddressInformationCollection : ICollection<MulticastIPAddressInformation>, IEnumerable<MulticastIPAddressInformation>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformationCollection" /> class.</summary>
		// Token: 0x06001976 RID: 6518 RVA: 0x0007DC55 File Offset: 0x0007BE55
		[global::__DynamicallyInvokable]
		protected internal MulticastIPAddressInformationCollection()
		{
		}

		/// <summary>Copies the elements in this collection to a one-dimensional array of type <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" />.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in this <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> is greater than the available space from <paramref name="count" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06001977 RID: 6519 RVA: 0x0007DC68 File Offset: 0x0007BE68
		[global::__DynamicallyInvokable]
		public virtual void CopyTo(MulticastIPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x0007DC77 File Offset: 0x0007BE77
		[global::__DynamicallyInvokable]
		public virtual int Count
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.addresses.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to this collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x0007DC84 File Offset: 0x0007BE84
		[global::__DynamicallyInvokable]
		public virtual bool IsReadOnly
		{
			[global::__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be added to the collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x0600197A RID: 6522 RVA: 0x0007DC87 File Offset: 0x0007BE87
		[global::__DynamicallyInvokable]
		public virtual void Add(MulticastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0007DC98 File Offset: 0x0007BE98
		internal void InternalAdd(MulticastIPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> object.</summary>
		/// <param name="address">The <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> object to be searched in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> object exists in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600197C RID: 6524 RVA: 0x0007DCA6 File Offset: 0x0007BEA6
		[global::__DynamicallyInvokable]
		public virtual bool Contains(MulticastIPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x0600197D RID: 6525 RVA: 0x0007DCB4 File Offset: 0x0007BEB4
		[global::__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x0600197E RID: 6526 RVA: 0x0007DCBC File Offset: 0x0007BEBC
		[global::__DynamicallyInvokable]
		public virtual IEnumerator<MulticastIPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		/// <summary>Gets the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> at the specific index of the collection.</summary>
		/// <param name="index">The index of interest.</param>
		/// <returns>The <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> at the specific index in the collection.</returns>
		// Token: 0x170005A7 RID: 1447
		[global::__DynamicallyInvokable]
		public virtual MulticastIPAddressInformation this[int index]
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.addresses[index];
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be removed.</summary>
		/// <param name="address">The object to be removed.</param>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		// Token: 0x06001980 RID: 6528 RVA: 0x0007DCD7 File Offset: 0x0007BED7
		[global::__DynamicallyInvokable]
		public virtual bool Remove(MulticastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be removed.</summary>
		// Token: 0x06001981 RID: 6529 RVA: 0x0007DCE8 File Offset: 0x0007BEE8
		[global::__DynamicallyInvokable]
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x040018F7 RID: 6391
		private Collection<MulticastIPAddressInformation> addresses = new Collection<MulticastIPAddressInformation>();
	}
}
