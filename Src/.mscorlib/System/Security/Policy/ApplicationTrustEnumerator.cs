using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Represents the enumerator for <see cref="T:System.Security.Policy.ApplicationTrust" /> objects in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
	// Token: 0x02000346 RID: 838
	[ComVisible(true)]
	public sealed class ApplicationTrustEnumerator : IEnumerator
	{
		// Token: 0x060029D9 RID: 10713 RVA: 0x0009BD61 File Offset: 0x00099F61
		private ApplicationTrustEnumerator()
		{
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x0009BD69 File Offset: 0x00099F69
		[SecurityCritical]
		internal ApplicationTrustEnumerator(ApplicationTrustCollection trusts)
		{
			this.m_trusts = trusts;
			this.m_current = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Policy.ApplicationTrust" /> object in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		/// <returns>The current <see cref="T:System.Security.Policy.ApplicationTrust" /> in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" />.</returns>
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x0009BD7F File Offset: 0x00099F7F
		public ApplicationTrust Current
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_trusts[this.m_current];
			}
		}

		/// <summary>Gets the current <see cref="T:System.Object" /> in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		/// <returns>The current <see cref="T:System.Object" /> in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" />.</returns>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x0009BD92 File Offset: 0x00099F92
		object IEnumerator.Current
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_trusts[this.m_current];
			}
		}

		/// <summary>Moves to the next element in the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		// Token: 0x060029DD RID: 10717 RVA: 0x0009BDA5 File Offset: 0x00099FA5
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			if (this.m_current == this.m_trusts.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		/// <summary>Resets the enumerator to the beginning of the <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> collection.</summary>
		// Token: 0x060029DE RID: 10718 RVA: 0x0009BDCD File Offset: 0x00099FCD
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x0400112B RID: 4395
		[SecurityCritical]
		private ApplicationTrustCollection m_trusts;

		// Token: 0x0400112C RID: 4396
		private int m_current;
	}
}
