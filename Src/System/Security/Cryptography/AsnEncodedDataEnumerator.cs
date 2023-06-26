using System;
using System.Collections;

namespace System.Security.Cryptography
{
	/// <summary>Provides the ability to navigate through an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object. This class cannot be inherited.</summary>
	// Token: 0x0200044F RID: 1103
	public sealed class AsnEncodedDataEnumerator : IEnumerator
	{
		// Token: 0x060028C9 RID: 10441 RVA: 0x000BAAD6 File Offset: 0x000B8CD6
		private AsnEncodedDataEnumerator()
		{
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000BAADE File Offset: 0x000B8CDE
		internal AsnEncodedDataEnumerator(AsnEncodedDataCollection asnEncodedDatas)
		{
			this.m_asnEncodedDatas = asnEncodedDatas;
			this.m_current = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in the collection.</returns>
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x000BAAF4 File Offset: 0x000B8CF4
		public AsnEncodedData Current
		{
			get
			{
				return this.m_asnEncodedDatas[this.m_current];
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</returns>
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060028CC RID: 10444 RVA: 0x000BAB07 File Offset: 0x000B8D07
		object IEnumerator.Current
		{
			get
			{
				return this.m_asnEncodedDatas[this.m_current];
			}
		}

		/// <summary>Advances to the next <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>
		///   <see langword="true" />, if the enumerator was successfully advanced to the next element; <see langword="false" />, if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060028CD RID: 10445 RVA: 0x000BAB1A File Offset: 0x000B8D1A
		public bool MoveNext()
		{
			if (this.m_current == this.m_asnEncodedDatas.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		/// <summary>Sets an enumerator to its initial position.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060028CE RID: 10446 RVA: 0x000BAB42 File Offset: 0x000B8D42
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x0400226B RID: 8811
		private AsnEncodedDataCollection m_asnEncodedDatas;

		// Token: 0x0400226C RID: 8812
		private int m_current;
	}
}
