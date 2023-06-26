using System;
using System.Collections;

namespace System.Security.Cryptography
{
	/// <summary>Provides the ability to navigate through an <see cref="T:System.Security.Cryptography.OidCollection" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000460 RID: 1120
	public sealed class OidEnumerator : IEnumerator
	{
		// Token: 0x0600298D RID: 10637 RVA: 0x000BC7BE File Offset: 0x000BA9BE
		private OidEnumerator()
		{
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000BC7C6 File Offset: 0x000BA9C6
		internal OidEnumerator(OidCollection oids)
		{
			this.m_oids = oids;
			this.m_current = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.Oid" /> object in the collection.</returns>
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x000BC7DC File Offset: 0x000BA9DC
		public Oid Current
		{
			get
			{
				return this.m_oids[this.m_current];
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002990 RID: 10640 RVA: 0x000BC7EF File Offset: 0x000BA9EF
		object IEnumerator.Current
		{
			get
			{
				return this.m_oids[this.m_current];
			}
		}

		/// <summary>Advances to the next <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>
		///   <see langword="true" />, if the enumerator was successfully advanced to the next element; <see langword="false" />, if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06002991 RID: 10641 RVA: 0x000BC802 File Offset: 0x000BAA02
		public bool MoveNext()
		{
			if (this.m_current == this.m_oids.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		/// <summary>Sets an enumerator to its initial position.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06002992 RID: 10642 RVA: 0x000BC82A File Offset: 0x000BAA2A
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x04002587 RID: 9607
		private OidCollection m_oids;

		// Token: 0x04002588 RID: 9608
		private int m_current;
	}
}
