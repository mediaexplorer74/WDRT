using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200047B RID: 1147
	public sealed class X509ExtensionCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> class.</summary>
		// Token: 0x06002A78 RID: 10872 RVA: 0x000C1AD4 File Offset: 0x000BFCD4
		public X509ExtensionCollection()
		{
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000C1AE8 File Offset: 0x000BFCE8
		internal unsafe X509ExtensionCollection(SafeCertContextHandle safeCertContextHandle)
		{
			using (SafeCertContextHandle safeCertContextHandle2 = CAPI.CertDuplicateCertificateContext(safeCertContextHandle))
			{
				CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)safeCertContextHandle2.DangerousGetHandle();
				CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
				uint cExtension = cert_INFO.cExtension;
				IntPtr rgExtension = cert_INFO.rgExtension;
				for (uint num = 0U; num < cExtension; num += 1U)
				{
					X509Extension x509Extension = new X509Extension(new IntPtr((long)rgExtension + (long)((ulong)num * (ulong)((long)Marshal.SizeOf(typeof(CAPIBase.CERT_EXTENSION))))));
					X509Extension x509Extension2 = CryptoConfig.CreateFromName(x509Extension.Oid.Value) as X509Extension;
					if (x509Extension2 != null)
					{
						x509Extension2.CopyFrom(x509Extension);
						x509Extension = x509Extension2;
					}
					this.Add(x509Extension);
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object at the specified index.</summary>
		/// <param name="index">The location of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object to retrieve.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is equal to or greater than the length of the array.</exception>
		// Token: 0x17000A4E RID: 2638
		public X509Extension this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumNotStarted"));
				}
				if (index >= this.m_list.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_Index"));
				}
				return (X509Extension)this.m_list[index];
			}
		}

		/// <summary>Gets the first <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object whose value or friendly name is specified by an object identifier (OID).</summary>
		/// <param name="oid">The object identifier (OID) of the extension to retrieve.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object.</returns>
		// Token: 0x17000A4F RID: 2639
		public X509Extension this[string oid]
		{
			get
			{
				string text = X509Utils.FindOidInfoWithFallback(2U, oid, OidGroup.ExtensionOrAttribute);
				if (text == null)
				{
					text = oid;
				}
				foreach (object obj in this.m_list)
				{
					X509Extension x509Extension = (X509Extension)obj;
					if (string.Compare(x509Extension.Oid.Value, text, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return x509Extension;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects in a <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An integer representing the number of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002A7C RID: 10876 RVA: 0x000C1CA8 File Offset: 0x000BFEA8
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object to an <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <param name="extension">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object to add to the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</param>
		/// <returns>The index at which the <paramref name="extension" /> parameter was added.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="extension" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002A7D RID: 10877 RVA: 0x000C1CB5 File Offset: 0x000BFEB5
		public int Add(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			return this.m_list.Add(extension);
		}

		/// <summary>Returns an enumerator that can iterate through an <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionEnumerator" /> object to use to iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x06002A7E RID: 10878 RVA: 0x000C1CD1 File Offset: 0x000BFED1
		public X509ExtensionEnumerator GetEnumerator()
		{
			return new X509ExtensionEnumerator(this);
		}

		/// <summary>Returns an enumerator that can iterate through an <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object to use to iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x06002A7F RID: 10879 RVA: 0x000C1CD9 File Offset: 0x000BFED9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new X509ExtensionEnumerator(this);
		}

		/// <summary>Copies the collection into an array starting at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects.</param>
		/// <param name="index">The location in the array at which copying starts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is a zero-length string or contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="index" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> specifies a value that is not in the range of the array.</exception>
		// Token: 0x06002A80 RID: 10880 RVA: 0x000C1CE4 File Offset: 0x000BFEE4
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

		/// <summary>Copies a collection into an array starting at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects.</param>
		/// <param name="index">The location in the array at which copying starts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is a zero-length string or contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="index" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> specifies a value that is not in the range of the array.</exception>
		// Token: 0x06002A81 RID: 10881 RVA: 0x000C1D7E File Offset: 0x000BFF7E
		public void CopyTo(X509Extension[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Gets a value indicating whether the collection is guaranteed to be thread safe.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is thread safe; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x000C1D88 File Offset: 0x000BFF88
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that you can use to synchronize access to the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An object that you can use to synchronize access to the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x000C1D8B File Offset: 0x000BFF8B
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0400262C RID: 9772
		private ArrayList m_list = new ArrayList();
	}
}
