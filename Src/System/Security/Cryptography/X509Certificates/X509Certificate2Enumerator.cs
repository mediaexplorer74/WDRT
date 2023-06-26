using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000469 RID: 1129
	public sealed class X509Certificate2Enumerator : IEnumerator
	{
		// Token: 0x06002A09 RID: 10761 RVA: 0x000C00CA File Offset: 0x000BE2CA
		private X509Certificate2Enumerator()
		{
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x000C00D2 File Offset: 0x000BE2D2
		internal X509Certificate2Enumerator(X509Certificate2Collection mappings)
		{
			this.baseEnumerator = ((IEnumerable)mappings).GetEnumerator();
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06002A0B RID: 10763 RVA: 0x000C00E6 File Offset: 0x000BE2E6
		public X509Certificate2 Current
		{
			get
			{
				return (X509Certificate2)this.baseEnumerator.Current;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IEnumerator.Current" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002A0C RID: 10764 RVA: 0x000C00F8 File Offset: 0x000BE2F8
		object IEnumerator.Current
		{
			get
			{
				return this.baseEnumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06002A0D RID: 10765 RVA: 0x000C0105 File Offset: 0x000BE305
		public bool MoveNext()
		{
			return this.baseEnumerator.MoveNext();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.MoveNext" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06002A0E RID: 10766 RVA: 0x000C0112 File Offset: 0x000BE312
		bool IEnumerator.MoveNext()
		{
			return this.baseEnumerator.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06002A0F RID: 10767 RVA: 0x000C011F File Offset: 0x000BE31F
		public void Reset()
		{
			this.baseEnumerator.Reset();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.Reset" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06002A10 RID: 10768 RVA: 0x000C012C File Offset: 0x000BE32C
		void IEnumerator.Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x040025C7 RID: 9671
		private IEnumerator baseEnumerator;
	}
}
