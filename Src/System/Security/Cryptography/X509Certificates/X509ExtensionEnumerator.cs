using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />. This class cannot be inherited.</summary>
	// Token: 0x0200047C RID: 1148
	public sealed class X509ExtensionEnumerator : IEnumerator
	{
		// Token: 0x06002A84 RID: 10884 RVA: 0x000C1D8E File Offset: 0x000BFF8E
		private X509ExtensionEnumerator()
		{
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000C1D96 File Offset: 0x000BFF96
		internal X509ExtensionEnumerator(X509ExtensionCollection extensions)
		{
			this.m_extensions = extensions;
			this.m_current = -1;
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x000C1DAC File Offset: 0x000BFFAC
		public X509Extension Current
		{
			get
			{
				return this.m_extensions[this.m_current];
			}
		}

		/// <summary>Gets an object from a collection.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002A87 RID: 10887 RVA: 0x000C1DBF File Offset: 0x000BFFBF
		object IEnumerator.Current
		{
			get
			{
				return this.m_extensions[this.m_current];
			}
		}

		/// <summary>Advances the enumerator to the next element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06002A88 RID: 10888 RVA: 0x000C1DD2 File Offset: 0x000BFFD2
		public bool MoveNext()
		{
			if (this.m_current == this.m_extensions.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06002A89 RID: 10889 RVA: 0x000C1DFA File Offset: 0x000BFFFA
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x0400262D RID: 9773
		private X509ExtensionCollection m_extensions;

		// Token: 0x0400262E RID: 9774
		private int m_current;
	}
}
