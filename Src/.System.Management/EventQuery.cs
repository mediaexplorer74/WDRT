﻿using System;

namespace System.Management
{
	/// <summary>Represents a WMI event query.</summary>
	// Token: 0x02000037 RID: 55
	public class EventQuery : ManagementQuery
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.EventQuery" /> class. This is the default constructor.</summary>
		// Token: 0x060001D5 RID: 469 RVA: 0x00009B90 File Offset: 0x00007D90
		public EventQuery()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.EventQuery" /> class for the specified query.</summary>
		/// <param name="query">A textual representation of the event query.</param>
		// Token: 0x060001D6 RID: 470 RVA: 0x00009B98 File Offset: 0x00007D98
		public EventQuery(string query)
			: base(query)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.EventQuery" /> class for the specified language and query.</summary>
		/// <param name="language">The language in which the query string is specified.</param>
		/// <param name="query">The string representation of the query.</param>
		// Token: 0x060001D7 RID: 471 RVA: 0x00009BA1 File Offset: 0x00007DA1
		public EventQuery(string language, string query)
			: base(language, query)
		{
		}

		/// <summary>Returns a copy of the object.</summary>
		/// <returns>The cloned object.</returns>
		// Token: 0x060001D8 RID: 472 RVA: 0x00009BBE File Offset: 0x00007DBE
		public override object Clone()
		{
			return new EventQuery(this.QueryLanguage, this.QueryString);
		}
	}
}
