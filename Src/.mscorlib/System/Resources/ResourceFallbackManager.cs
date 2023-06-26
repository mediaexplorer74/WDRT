using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Resources
{
	// Token: 0x02000392 RID: 914
	internal class ResourceFallbackManager : IEnumerable<CultureInfo>, IEnumerable
	{
		// Token: 0x06002D29 RID: 11561 RVA: 0x000ABA99 File Offset: 0x000A9C99
		internal ResourceFallbackManager(CultureInfo startingCulture, CultureInfo neutralResourcesCulture, bool useParents)
		{
			if (startingCulture != null)
			{
				this.m_startingCulture = startingCulture;
			}
			else
			{
				this.m_startingCulture = CultureInfo.CurrentUICulture;
			}
			this.m_neutralResourcesCulture = neutralResourcesCulture;
			this.m_useParents = useParents;
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000ABAC6 File Offset: 0x000A9CC6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x000ABACE File Offset: 0x000A9CCE
		public IEnumerator<CultureInfo> GetEnumerator()
		{
			bool reachedNeutralResourcesCulture = false;
			CultureInfo currentCulture = this.m_startingCulture;
			while (this.m_neutralResourcesCulture == null || !(currentCulture.Name == this.m_neutralResourcesCulture.Name))
			{
				yield return currentCulture;
				currentCulture = currentCulture.Parent;
				if (!this.m_useParents || currentCulture.HasInvariantCultureName)
				{
					IL_CE:
					if (!this.m_useParents || this.m_startingCulture.HasInvariantCultureName)
					{
						yield break;
					}
					if (reachedNeutralResourcesCulture)
					{
						yield break;
					}
					yield return CultureInfo.InvariantCulture;
					yield break;
				}
			}
			yield return CultureInfo.InvariantCulture;
			reachedNeutralResourcesCulture = true;
			goto IL_CE;
		}

		// Token: 0x04001238 RID: 4664
		private CultureInfo m_startingCulture;

		// Token: 0x04001239 RID: 4665
		private CultureInfo m_neutralResourcesCulture;

		// Token: 0x0400123A RID: 4666
		private bool m_useParents;
	}
}
