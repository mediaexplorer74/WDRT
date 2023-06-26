using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types.</summary>
	// Token: 0x020002AB RID: 683
	[global::__DynamicallyInvokable]
	public class UnicastIPAddressInformationCollection : ICollection<UnicastIPAddressInformation>, IEnumerable<UnicastIPAddressInformation>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> class.</summary>
		// Token: 0x06001963 RID: 6499 RVA: 0x0007DBA9 File Offset: 0x0007BDA9
		[global::__DynamicallyInvokable]
		protected internal UnicastIPAddressInformationCollection()
		{
		}

		/// <summary>Copies the elements in this collection to a one-dimensional array of type <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" />.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in this <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> is greater than the available space from <paramref name="offset" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06001964 RID: 6500 RVA: 0x0007DBBC File Offset: 0x0007BDBC
		[global::__DynamicallyInvokable]
		public virtual void CopyTo(UnicastIPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x0007DBCB File Offset: 0x0007BDCB
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
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x0007DBD8 File Offset: 0x0007BDD8
		[global::__DynamicallyInvokable]
		public virtual bool IsReadOnly
		{
			[global::__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x06001967 RID: 6503 RVA: 0x0007DBDB File Offset: 0x0007BDDB
		[global::__DynamicallyInvokable]
		public virtual void Add(UnicastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0007DBEC File Offset: 0x0007BDEC
		internal void InternalAdd(UnicastIPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> object.</summary>
		/// <param name="address">The <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> object to be searched in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> object exists in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001969 RID: 6505 RVA: 0x0007DBFA File Offset: 0x0007BDFA
		[global::__DynamicallyInvokable]
		public virtual bool Contains(UnicastIPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x0600196A RID: 6506 RVA: 0x0007DC08 File Offset: 0x0007BE08
		[global::__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x0600196B RID: 6507 RVA: 0x0007DC10 File Offset: 0x0007BE10
		[global::__DynamicallyInvokable]
		public virtual IEnumerator<UnicastIPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		/// <summary>Gets the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> instance at the specified index in the collection.</summary>
		/// <param name="index">The zero-based index of the element.</param>
		/// <returns>The <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> at the specified location.</returns>
		// Token: 0x1700059E RID: 1438
		[global::__DynamicallyInvokable]
		public virtual UnicastIPAddressInformation this[int index]
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
		// Token: 0x0600196D RID: 6509 RVA: 0x0007DC2B File Offset: 0x0007BE2B
		[global::__DynamicallyInvokable]
		public virtual bool Remove(UnicastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		// Token: 0x0600196E RID: 6510 RVA: 0x0007DC3C File Offset: 0x0007BE3C
		[global::__DynamicallyInvokable]
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x040018F6 RID: 6390
		private Collection<UnicastIPAddressInformation> addresses = new Collection<UnicastIPAddressInformation>();
	}
}
