using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines a collection that stores <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects.</summary>
	// Token: 0x02000482 RID: 1154
	[Serializable]
	public class X509CertificateCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> class.</summary>
		// Token: 0x06002ABD RID: 10941 RVA: 0x000C2DC3 File Offset: 0x000C0FC3
		public X509CertificateCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> class from another <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> with which to initialize the new object.</param>
		// Token: 0x06002ABE RID: 10942 RVA: 0x000C2DCB File Offset: 0x000C0FCB
		public X509CertificateCollection(X509CertificateCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> class from an array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects.</summary>
		/// <param name="value">The array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects with which to initialize the new object.</param>
		// Token: 0x06002ABF RID: 10943 RVA: 0x000C2DDA File Offset: 0x000C0FDA
		public X509CertificateCollection(X509Certificate[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the entry at the specified index of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="index">The zero-based index of the entry to locate in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> at the specified index of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000A59 RID: 2649
		public X509Certificate this[int index]
		{
			get
			{
				return (X509Certificate)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> with the specified value to the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to add to the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</param>
		/// <returns>The index into the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> at which the new <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> was inserted.</returns>
		// Token: 0x06002AC2 RID: 10946 RVA: 0x000C2E0B File Offset: 0x000C100B
		public int Add(X509Certificate value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of an array of type <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to the end of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The array of type <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> containing the objects to add to the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002AC3 RID: 10947 RVA: 0x000C2E1C File Offset: 0x000C101C
		public void AddRange(X509Certificate[] value)
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

		/// <summary>Copies the elements of the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> to the end of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002AC4 RID: 10948 RVA: 0x000C2E50 File Offset: 0x000C1050
		public void AddRange(X509CertificateCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Count; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> contains the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to locate.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> is contained in this collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002AC5 RID: 10949 RVA: 0x000C2E8C File Offset: 0x000C108C
		public bool Contains(X509Certificate value)
		{
			foreach (object obj in base.List)
			{
				X509Certificate x509Certificate = (X509Certificate)obj;
				if (x509Certificate.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> values in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</param>
		/// <param name="index">The index into <paramref name="array" /> to begin copying.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="array" /> parameter is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> is greater than the available space between <paramref name="arrayIndex" /> and the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="arrayIndex" /> parameter is less than the <paramref name="array" /> parameter's lower bound.</exception>
		// Token: 0x06002AC6 RID: 10950 RVA: 0x000C2EF0 File Offset: 0x000C10F0
		public void CopyTo(X509Certificate[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to locate.</param>
		/// <returns>The index of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> specified by the <paramref name="value" /> parameter in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />, if found; otherwise, -1.</returns>
		// Token: 0x06002AC7 RID: 10951 RVA: 0x000C2EFF File Offset: 0x000C10FF
		public int IndexOf(X509Certificate value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> into the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index where <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to insert.</param>
		// Token: 0x06002AC8 RID: 10952 RVA: 0x000C2F0D File Offset: 0x000C110D
		public void Insert(int index, X509Certificate value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <returns>An enumerator of the subelements of <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> you can use to iterate through the collection.</returns>
		// Token: 0x06002AC9 RID: 10953 RVA: 0x000C2F1C File Offset: 0x000C111C
		public new X509CertificateCollection.X509CertificateEnumerator GetEnumerator()
		{
			return new X509CertificateCollection.X509CertificateEnumerator(this);
		}

		/// <summary>Removes a specific <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> from the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to remove from the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> specified by the <paramref name="value" /> parameter is not found in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</exception>
		// Token: 0x06002ACA RID: 10954 RVA: 0x000C2F24 File Offset: 0x000C1124
		public void Remove(X509Certificate value)
		{
			base.List.Remove(value);
		}

		/// <summary>Builds a hash value based on all values contained in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <returns>A hash value based on all values contained in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</returns>
		// Token: 0x06002ACB RID: 10955 RVA: 0x000C2F34 File Offset: 0x000C1134
		public override int GetHashCode()
		{
			int num = 0;
			foreach (X509Certificate x509Certificate in this)
			{
				num += x509Certificate.GetHashCode();
			}
			return num;
		}

		/// <summary>Enumerates the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects in an <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		// Token: 0x02000878 RID: 2168
		public class X509CertificateEnumerator : IEnumerator
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection.X509CertificateEnumerator" /> class for the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
			/// <param name="mappings">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> to enumerate.</param>
			// Token: 0x06004548 RID: 17736 RVA: 0x00120D0D File Offset: 0x0011EF0D
			public X509CertificateEnumerator(X509CertificateCollection mappings)
			{
				this.temp = mappings;
				this.baseEnumerator = this.temp.GetEnumerator();
			}

			/// <summary>Gets the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
			/// <returns>The current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x17000FAD RID: 4013
			// (get) Token: 0x06004549 RID: 17737 RVA: 0x00120D2D File Offset: 0x0011EF2D
			public X509Certificate Current
			{
				get
				{
					return (X509Certificate)this.baseEnumerator.Current;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IEnumerator.Current" />.</summary>
			/// <returns>The current X.509 certificate object in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> object.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x17000FAE RID: 4014
			// (get) Token: 0x0600454A RID: 17738 RVA: 0x00120D3F File Offset: 0x0011EF3F
			object IEnumerator.Current
			{
				get
				{
					return this.baseEnumerator.Current;
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was instantiated.</exception>
			// Token: 0x0600454B RID: 17739 RVA: 0x00120D4C File Offset: 0x0011EF4C
			public bool MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.MoveNext" />.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was instantiated.</exception>
			// Token: 0x0600454C RID: 17740 RVA: 0x00120D59 File Offset: 0x0011EF59
			bool IEnumerator.MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection is modified after the enumerator is instantiated.</exception>
			// Token: 0x0600454D RID: 17741 RVA: 0x00120D66 File Offset: 0x0011EF66
			public void Reset()
			{
				this.baseEnumerator.Reset();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.Reset" />.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was instantiated.</exception>
			// Token: 0x0600454E RID: 17742 RVA: 0x00120D73 File Offset: 0x0011EF73
			void IEnumerator.Reset()
			{
				this.baseEnumerator.Reset();
			}

			// Token: 0x0400370A RID: 14090
			private IEnumerator baseEnumerator;

			// Token: 0x0400370B RID: 14091
			private IEnumerable temp;
		}
	}
}
