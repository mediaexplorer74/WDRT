using System;

namespace System.Management
{
	/// <summary>Represents a WMI data query in WQL format.</summary>
	// Token: 0x02000038 RID: 56
	public class WqlObjectQuery : ObjectQuery
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.WqlObjectQuery" /> class. This is the default constructor.</summary>
		// Token: 0x060001D9 RID: 473 RVA: 0x00009BD1 File Offset: 0x00007DD1
		public WqlObjectQuery()
			: base(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.WqlObjectQuery" /> class initialized to the specified query.</summary>
		/// <param name="query">The representation of the data query.</param>
		// Token: 0x060001DA RID: 474 RVA: 0x00009BDA File Offset: 0x00007DDA
		public WqlObjectQuery(string query)
			: base(query)
		{
		}

		/// <summary>Gets the language of the query.</summary>
		/// <returns>The language of the query.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00009BE3 File Offset: 0x00007DE3
		public override string QueryLanguage
		{
			get
			{
				return base.QueryLanguage;
			}
		}

		/// <summary>Creates a copy of the object.</summary>
		/// <returns>The copied object.</returns>
		// Token: 0x060001DC RID: 476 RVA: 0x00009BEB File Offset: 0x00007DEB
		public override object Clone()
		{
			return new WqlObjectQuery(this.QueryString);
		}
	}
}
