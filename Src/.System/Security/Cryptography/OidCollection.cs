using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.Oid" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200045F RID: 1119
	public sealed class OidCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.OidCollection" /> class.</summary>
		// Token: 0x06002982 RID: 10626 RVA: 0x000BC645 File Offset: 0x000BA845
		public OidCollection()
		{
			this.m_list = new ArrayList();
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.Oid" /> object to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <param name="oid">The <see cref="T:System.Security.Cryptography.Oid" /> object to add to the collection.</param>
		/// <returns>The index of the added <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		// Token: 0x06002983 RID: 10627 RVA: 0x000BC658 File Offset: 0x000BA858
		public int Add(Oid oid)
		{
			return this.m_list.Add(oid);
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.Oid" /> object from the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <param name="index">The location of the <see cref="T:System.Security.Cryptography.Oid" /> object in the collection.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		// Token: 0x17000A0E RID: 2574
		public Oid this[int index]
		{
			get
			{
				return this.m_list[index] as Oid;
			}
		}

		/// <summary>Gets the first <see cref="T:System.Security.Cryptography.Oid" /> object that contains a value of the <see cref="P:System.Security.Cryptography.Oid.Value" /> property or a value of the <see cref="P:System.Security.Cryptography.Oid.FriendlyName" /> property that matches the specified string value from the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <param name="oid">A string that represents a <see cref="P:System.Security.Cryptography.Oid.Value" /> property or a <see cref="P:System.Security.Cryptography.Oid.FriendlyName" /> property.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		// Token: 0x17000A0F RID: 2575
		public Oid this[string oid]
		{
			get
			{
				string text = System.Security.Cryptography.X509Certificates.X509Utils.FindOidInfoWithFallback(2U, oid, OidGroup.All);
				if (text == null)
				{
					text = oid;
				}
				foreach (object obj in this.m_list)
				{
					Oid oid2 = (Oid)obj;
					if (oid2.Value == text)
					{
						return oid2;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.Oid" /> objects in a collection.</summary>
		/// <returns>The number of <see cref="T:System.Security.Cryptography.Oid" /> objects in a collection.</returns>
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x000BC6F4 File Offset: 0x000BA8F4
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidEnumerator" /> object.</returns>
		// Token: 0x06002987 RID: 10631 RVA: 0x000BC701 File Offset: 0x000BA901
		public OidEnumerator GetEnumerator()
		{
			return new OidEnumerator(this);
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the collection.</returns>
		// Token: 0x06002988 RID: 10632 RVA: 0x000BC709 File Offset: 0x000BA909
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new OidEnumerator(this);
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.OidCollection" /> object into an array.</summary>
		/// <param name="array">The array to copy the <see cref="T:System.Security.Cryptography.OidCollection" /> object to.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> cannot be a multidimensional array.  
		/// -or-  
		/// The length of <paramref name="array" /> is an invalid offset length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="index" /> is out range.</exception>
		// Token: 0x06002989 RID: 10633 RVA: 0x000BC714 File Offset: 0x000BA914
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(System.SR.GetString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", System.SR.GetString("ArgumentOutOfRange_Index"));
			}
			if (index + this.Count > array.Length)
			{
				throw new ArgumentException(System.SR.GetString("Argument_InvalidOffLen"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.OidCollection" /> object into an array.</summary>
		/// <param name="array">The array to copy the <see cref="T:System.Security.Cryptography.OidCollection" /> object into.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		// Token: 0x0600298A RID: 10634 RVA: 0x000BC7AE File Offset: 0x000BA9AE
		public void CopyTo(Oid[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object is thread safe.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600298B RID: 10635 RVA: 0x000BC7B8 File Offset: 0x000BA9B8
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</returns>
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x000BC7BB File Offset: 0x000BA9BB
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04002586 RID: 9606
		private ArrayList m_list;
	}
}
