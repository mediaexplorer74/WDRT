using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Supports a simple iteration over an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />. This class cannot be inherited.</summary>
	// Token: 0x0200046F RID: 1135
	public sealed class X509ChainElementEnumerator : IEnumerator
	{
		// Token: 0x06002A36 RID: 10806 RVA: 0x000C0D1A File Offset: 0x000BEF1A
		private X509ChainElementEnumerator()
		{
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000C0D22 File Offset: 0x000BEF22
		internal X509ChainElementEnumerator(X509ChainElementCollection chainElements)
		{
			this.m_chainElements = chainElements;
			this.m_current = -1;
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002A38 RID: 10808 RVA: 0x000C0D38 File Offset: 0x000BEF38
		public X509ChainElement Current
		{
			get
			{
				return this.m_chainElements[this.m_current];
			}
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002A39 RID: 10809 RVA: 0x000C0D4B File Offset: 0x000BEF4B
		object IEnumerator.Current
		{
			get
			{
				return this.m_chainElements[this.m_current];
			}
		}

		/// <summary>Advances the enumerator to the next element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06002A3A RID: 10810 RVA: 0x000C0D5E File Offset: 0x000BEF5E
		public bool MoveNext()
		{
			if (this.m_current == this.m_chainElements.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06002A3B RID: 10811 RVA: 0x000C0D86 File Offset: 0x000BEF86
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x040025F1 RID: 9713
		private X509ChainElementCollection m_chainElements;

		// Token: 0x040025F2 RID: 9714
		private int m_current;
	}
}
