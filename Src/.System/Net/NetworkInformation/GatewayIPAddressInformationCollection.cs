using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> types.</summary>
	// Token: 0x020002B1 RID: 689
	[global::__DynamicallyInvokable]
	public class GatewayIPAddressInformationCollection : ICollection<GatewayIPAddressInformation>, IEnumerable<GatewayIPAddressInformation>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformationCollection" /> class.</summary>
		// Token: 0x06001993 RID: 6547 RVA: 0x0007DE10 File Offset: 0x0007C010
		[global::__DynamicallyInvokable]
		protected internal GatewayIPAddressInformationCollection()
		{
		}

		/// <summary>Copies the elements in this collection to a one-dimensional array of type <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" />.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in this <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> is greater than the available space from <paramref name="count" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06001994 RID: 6548 RVA: 0x0007DE23 File Offset: 0x0007C023
		[global::__DynamicallyInvokable]
		public virtual void CopyTo(GatewayIPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x0007DE32 File Offset: 0x0007C032
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
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x0007DE3F File Offset: 0x0007C03F
		[global::__DynamicallyInvokable]
		public virtual bool IsReadOnly
		{
			[global::__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> at the specific index of the collection.</summary>
		/// <param name="index">The index of interest.</param>
		/// <returns>The <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> at the specific index in the collection.</returns>
		// Token: 0x170005AF RID: 1455
		[global::__DynamicallyInvokable]
		public virtual GatewayIPAddressInformation this[int index]
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.addresses[index];
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x06001998 RID: 6552 RVA: 0x0007DE50 File Offset: 0x0007C050
		[global::__DynamicallyInvokable]
		public virtual void Add(GatewayIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0007DE61 File Offset: 0x0007C061
		internal void InternalAdd(GatewayIPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> object.</summary>
		/// <param name="address">The <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> object to be searched in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> object exists in the collection; otherwise <see langword="false" />.</returns>
		// Token: 0x0600199A RID: 6554 RVA: 0x0007DE6F File Offset: 0x0007C06F
		[global::__DynamicallyInvokable]
		public virtual bool Contains(GatewayIPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x0600199B RID: 6555 RVA: 0x0007DE7D File Offset: 0x0007C07D
		[global::__DynamicallyInvokable]
		public virtual IEnumerator<GatewayIPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x0600199C RID: 6556 RVA: 0x0007DE8A File Offset: 0x0007C08A
		[global::__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <param name="address">The object to be removed.</param>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		// Token: 0x0600199D RID: 6557 RVA: 0x0007DE92 File Offset: 0x0007C092
		[global::__DynamicallyInvokable]
		public virtual bool Remove(GatewayIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		// Token: 0x0600199E RID: 6558 RVA: 0x0007DEA3 File Offset: 0x0007C0A3
		[global::__DynamicallyInvokable]
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x040018FA RID: 6394
		private Collection<GatewayIPAddressInformation> addresses = new Collection<GatewayIPAddressInformation>();
	}
}
