using System;
using System.Collections;

namespace System.Security.Cryptography
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.AsnEncodedData" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200044E RID: 1102
	public sealed class AsnEncodedDataCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> class.</summary>
		// Token: 0x060028BD RID: 10429 RVA: 0x000BA92F File Offset: 0x000B8B2F
		public AsnEncodedDataCollection()
		{
			this.m_list = new ArrayList();
			this.m_oid = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> class and adds an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to the collection.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to add to the collection.</param>
		// Token: 0x060028BE RID: 10430 RVA: 0x000BA949 File Offset: 0x000B8B49
		public AsnEncodedDataCollection(AsnEncodedData asnEncodedData)
			: this()
		{
			this.m_list.Add(asnEncodedData);
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to add to the collection.</param>
		/// <returns>The index of the added <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">Neither of the OIDs are <see langword="null" /> and the OIDs do not match.
		/// -or-
		/// One of the OIDs is <see langword="null" /> and the OIDs do not match.</exception>
		// Token: 0x060028BF RID: 10431 RVA: 0x000BA960 File Offset: 0x000B8B60
		public int Add(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			if (this.m_oid != null)
			{
				string value = this.m_oid.Value;
				string value2 = asnEncodedData.Oid.Value;
				if (value != null && value2 != null)
				{
					if (string.Compare(value, value2, StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new CryptographicException(SR.GetString("Cryptography_Asn_MismatchedOidInCollection"));
					}
				}
				else if (value != null || value2 != null)
				{
					throw new CryptographicException(SR.GetString("Cryptography_Asn_MismatchedOidInCollection"));
				}
			}
			return this.m_list.Add(asnEncodedData);
		}

		/// <summary>Removes an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		// Token: 0x060028C0 RID: 10432 RVA: 0x000BA9DD File Offset: 0x000B8BDD
		public void Remove(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			this.m_list.Remove(asnEncodedData);
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <param name="index">The location in the collection.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</returns>
		// Token: 0x17000A00 RID: 2560
		public AsnEncodedData this[int index]
		{
			get
			{
				return (AsnEncodedData)this.m_list[index];
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.AsnEncodedData" /> objects in a collection.</summary>
		/// <returns>The number of <see cref="T:System.Security.Cryptography.AsnEncodedData" /> objects.</returns>
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x000BAA0C File Offset: 0x000B8C0C
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.AsnEncodedDataEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsnEncodedDataEnumerator" /> object.</returns>
		// Token: 0x060028C3 RID: 10435 RVA: 0x000BAA19 File Offset: 0x000B8C19
		public AsnEncodedDataEnumerator GetEnumerator()
		{
			return new AsnEncodedDataEnumerator(this);
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.AsnEncodedDataEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsnEncodedDataEnumerator" /> object that can be used to navigate the collection.</returns>
		// Token: 0x060028C4 RID: 10436 RVA: 0x000BAA21 File Offset: 0x000B8C21
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AsnEncodedDataEnumerator(this);
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object into an array.</summary>
		/// <param name="array">The array that the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object is to be copied into.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is a multidimensional array, which is not supported by this method.
		/// -or-
		/// The length for <paramref name="index" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length for <paramref name="index" /> is out of range.</exception>
		// Token: 0x060028C5 RID: 10437 RVA: 0x000BAA2C File Offset: 0x000B8C2C
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_Index"));
			}
			if (index + this.Count > array.Length)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object into an array.</summary>
		/// <param name="array">The array that the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object is to be copied into.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		// Token: 0x060028C6 RID: 10438 RVA: 0x000BAAC6 File Offset: 0x000B8CC6
		public void CopyTo(AsnEncodedData[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object is thread safe.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060028C7 RID: 10439 RVA: 0x000BAAD0 File Offset: 0x000B8CD0
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>An object used to synchronize access to the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</returns>
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060028C8 RID: 10440 RVA: 0x000BAAD3 File Offset: 0x000B8CD3
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04002269 RID: 8809
		private ArrayList m_list;

		// Token: 0x0400226A RID: 8810
		private Oid m_oid;
	}
}
