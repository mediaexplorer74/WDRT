using System;
using System.Collections;

namespace System.Security.AccessControl
{
	/// <summary>Represents an access control list (ACL) and is the base class for the <see cref="T:System.Security.AccessControl.CommonAcl" />, <see cref="T:System.Security.AccessControl.DiscretionaryAcl" />, <see cref="T:System.Security.AccessControl.RawAcl" />, and <see cref="T:System.Security.AccessControl.SystemAcl" /> classes.</summary>
	// Token: 0x0200020B RID: 523
	public abstract class GenericAcl : ICollection, IEnumerable
	{
		/// <summary>Gets the revision level of the <see cref="T:System.Security.AccessControl.GenericAcl" />.</summary>
		/// <returns>A byte value that specifies the revision level of the <see cref="T:System.Security.AccessControl.GenericAcl" />.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001E8A RID: 7818
		public abstract byte Revision { get; }

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericAcl" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.GenericAcl.GetBinaryForm(System.Byte[],System.Int32)" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericAcl" /> object.</returns>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001E8B RID: 7819
		public abstract int BinaryLength { get; }

		/// <summary>Gets or sets the <see cref="T:System.Security.AccessControl.GenericAce" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.AccessControl.GenericAce" /> to get or set.</param>
		/// <returns>The <see cref="T:System.Security.AccessControl.GenericAce" /> at the specified index.</returns>
		// Token: 0x1700037E RID: 894
		public abstract GenericAce this[int index] { get; set; }

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.GenericAcl" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.GenericAcl" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.GenericAcl" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x06001E8E RID: 7822
		public abstract void GetBinaryForm(byte[] binaryForm, int offset);

		/// <summary>Copies each <see cref="T:System.Security.AccessControl.GenericAce" /> of the current <see cref="T:System.Security.AccessControl.GenericAcl" /> into the specified array.</summary>
		/// <param name="array">The array into which copies of the <see cref="T:System.Security.AccessControl.GenericAce" /> objects contained by the current <see cref="T:System.Security.AccessControl.GenericAcl" /> are placed.</param>
		/// <param name="index">The zero-based index of <paramref name="array" /> where the copying begins.</param>
		// Token: 0x06001E8F RID: 7823 RVA: 0x0006AD0C File Offset: 0x00068F0C
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentOutOfRangeException("array", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		/// <summary>Copies each <see cref="T:System.Security.AccessControl.GenericAce" /> of the current <see cref="T:System.Security.AccessControl.GenericAcl" /> into the specified array.</summary>
		/// <param name="array">The array into which copies of the <see cref="T:System.Security.AccessControl.GenericAce" /> objects contained by the current <see cref="T:System.Security.AccessControl.GenericAcl" /> are placed.</param>
		/// <param name="index">The zero-based index of <paramref name="array" /> where the copying begins.</param>
		// Token: 0x06001E90 RID: 7824 RVA: 0x0006AD9F File Offset: 0x00068F9F
		public void CopyTo(GenericAce[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Gets the number of access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.GenericAcl" /> object.</summary>
		/// <returns>The number of ACEs in the current <see cref="T:System.Security.AccessControl.GenericAcl" /> object.</returns>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001E91 RID: 7825
		public abstract int Count { get; }

		/// <summary>This property is always set to <see langword="false" />. It is implemented only because it is required for the implementation of the <see cref="T:System.Collections.ICollection" /> interface.</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x0006ADA9 File Offset: 0x00068FA9
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>This property always returns <see langword="null" />. It is implemented only because it is required for the implementation of the <see cref="T:System.Collections.ICollection" /> interface.</summary>
		/// <returns>Always returns <see langword="null" />.</returns>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x0006ADAC File Offset: 0x00068FAC
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Returns a new instance of the <see cref="T:System.Security.AccessControl.AceEnumerator" /> class cast as an instance of the <see cref="T:System.Collections.IEnumerator" /> interface.</summary>
		/// <returns>A new <see cref="T:System.Security.AccessControl.AceEnumerator" /> object, cast as an instance of the <see cref="T:System.Collections.IEnumerator" /> interface.</returns>
		// Token: 0x06001E94 RID: 7828 RVA: 0x0006ADAF File Offset: 0x00068FAF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AceEnumerator(this);
		}

		/// <summary>Retrieves an object that you can use to iterate through the access control entries (ACEs) in an access control list (ACL).</summary>
		/// <returns>An enumerator object.</returns>
		// Token: 0x06001E95 RID: 7829 RVA: 0x0006ADB7 File Offset: 0x00068FB7
		public AceEnumerator GetEnumerator()
		{
			return ((IEnumerable)this).GetEnumerator() as AceEnumerator;
		}

		/// <summary>The revision level of the current <see cref="T:System.Security.AccessControl.GenericAcl" />. This value is returned by the <see cref="P:System.Security.AccessControl.GenericAcl.Revision" /> property for Access Control Lists (ACLs) that are not associated with Directory Services objects.</summary>
		// Token: 0x04000B05 RID: 2821
		public static readonly byte AclRevision = 2;

		/// <summary>The revision level of the current <see cref="T:System.Security.AccessControl.GenericAcl" />. This value is returned by the <see cref="P:System.Security.AccessControl.GenericAcl.Revision" /> property for Access Control Lists (ACLs) that are associated with Directory Services objects.</summary>
		// Token: 0x04000B06 RID: 2822
		public static readonly byte AclRevisionDS = 4;

		/// <summary>The maximum allowed binary length of a <see cref="T:System.Security.AccessControl.GenericAcl" /> object.</summary>
		// Token: 0x04000B07 RID: 2823
		public static readonly int MaxBinaryLength = 65535;

		// Token: 0x04000B08 RID: 2824
		internal const int HeaderLength = 8;
	}
}
