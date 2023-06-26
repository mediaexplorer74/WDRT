using System;

namespace System.Management
{
	/// <summary>Represents a management query that returns instances or classes.</summary>
	// Token: 0x02000036 RID: 54
	public class ObjectQuery : ManagementQuery
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ObjectQuery" /> class with no initialized values. This is the default constructor.</summary>
		// Token: 0x060001D1 RID: 465 RVA: 0x00009B90 File Offset: 0x00007D90
		public ObjectQuery()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ObjectQuery" /> class for a specific query string.</summary>
		/// <param name="query">The string representation of the query.</param>
		// Token: 0x060001D2 RID: 466 RVA: 0x00009B98 File Offset: 0x00007D98
		public ObjectQuery(string query)
			: base(query)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.ObjectQuery" /> class for a specific query string and language.</summary>
		/// <param name="language">The query language in which this query is specified.</param>
		/// <param name="query">The string representation of the query.</param>
		// Token: 0x060001D3 RID: 467 RVA: 0x00009BA1 File Offset: 0x00007DA1
		public ObjectQuery(string language, string query)
			: base(language, query)
		{
		}

		/// <summary>Returns a copy of the object.</summary>
		/// <returns>The cloned object.</returns>
		// Token: 0x060001D4 RID: 468 RVA: 0x00009BAB File Offset: 0x00007DAB
		public override object Clone()
		{
			return new ObjectQuery(this.QueryLanguage, this.QueryString);
		}
	}
}
