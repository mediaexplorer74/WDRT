using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200046E RID: 1134
	public sealed class X509ChainElementCollection : ICollection, IEnumerable
	{
		// Token: 0x06002A2C RID: 10796 RVA: 0x000C0B4E File Offset: 0x000BED4E
		internal X509ChainElementCollection()
		{
			this.m_elements = new X509ChainElement[0];
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x000C0B64 File Offset: 0x000BED64
		internal unsafe X509ChainElementCollection(IntPtr pSimpleChain)
		{
			CAPIBase.CERT_SIMPLE_CHAIN cert_SIMPLE_CHAIN = new CAPIBase.CERT_SIMPLE_CHAIN(Marshal.SizeOf(typeof(CAPIBase.CERT_SIMPLE_CHAIN)));
			uint num = (uint)Marshal.ReadInt32(pSimpleChain);
			if ((ulong)num > (ulong)((long)Marshal.SizeOf(cert_SIMPLE_CHAIN)))
			{
				num = (uint)Marshal.SizeOf(cert_SIMPLE_CHAIN);
			}
			X509Utils.memcpy(pSimpleChain, new IntPtr((void*)(&cert_SIMPLE_CHAIN)), num);
			this.m_elements = new X509ChainElement[cert_SIMPLE_CHAIN.cElement];
			for (int i = 0; i < this.m_elements.Length; i++)
			{
				this.m_elements[i] = new X509ChainElement(Marshal.ReadIntPtr(new IntPtr((long)cert_SIMPLE_CHAIN.rgpElement + (long)(i * Marshal.SizeOf(typeof(IntPtr))))));
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> object at the specified index.</summary>
		/// <param name="index">An integer value.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is greater than or equal to the length of the collection.</exception>
		// Token: 0x17000A39 RID: 2617
		public X509ChainElement this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumNotStarted"));
				}
				if (index >= this.m_elements.Length)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_Index"));
				}
				return this.m_elements[index];
			}
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>An integer representing the number of elements in the collection.</returns>
		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002A2F RID: 10799 RVA: 0x000C0C56 File Offset: 0x000BEE56
		public int Count
		{
			get
			{
				return this.m_elements.Length;
			}
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementEnumerator" /> object that can be used to navigate through a collection of chain elements.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementEnumerator" /> object.</returns>
		// Token: 0x06002A30 RID: 10800 RVA: 0x000C0C60 File Offset: 0x000BEE60
		public X509ChainElementEnumerator GetEnumerator()
		{
			return new X509ChainElementEnumerator(this);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> object that can be used to navigate a collection of chain elements.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
		// Token: 0x06002A31 RID: 10801 RVA: 0x000C0C68 File Offset: 0x000BEE68
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new X509ChainElementEnumerator(this);
		}

		/// <summary>Copies an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object into an array, starting at the specified index.</summary>
		/// <param name="array">An array to copy the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object to.</param>
		/// <param name="index">The index of <paramref name="array" /> at which to start copying.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is less than zero, or greater than or equal to the length of the array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus the current count is greater than the length of the array.</exception>
		// Token: 0x06002A32 RID: 10802 RVA: 0x000C0C70 File Offset: 0x000BEE70
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

		/// <summary>Copies an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object into an array, starting at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> objects.</param>
		/// <param name="index">An integer representing the index value.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is less than zero, or greater than or equal to the length of the array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus the current count is greater than the length of the array.</exception>
		// Token: 0x06002A33 RID: 10803 RVA: 0x000C0D0A File Offset: 0x000BEF0A
		public void CopyTo(X509ChainElement[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Gets a value indicating whether the collection of chain elements is synchronized.</summary>
		/// <returns>Always returns <see langword="false" />.</returns>
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002A34 RID: 10804 RVA: 0x000C0D14 File Offset: 0x000BEF14
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object.</summary>
		/// <returns>A pointer reference to the current object.</returns>
		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002A35 RID: 10805 RVA: 0x000C0D17 File Offset: 0x000BEF17
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x040025F0 RID: 9712
		private X509ChainElement[] m_elements;
	}
}
