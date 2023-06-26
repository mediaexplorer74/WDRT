using System;

namespace System.ComponentModel
{
	/// <summary>Identifies the type of data operation performed by a method, as specified by the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> applied to the method.</summary>
	// Token: 0x02000536 RID: 1334
	public enum DataObjectMethodType
	{
		/// <summary>Indicates that a method is used for a data operation that fills a <see cref="T:System.Data.DataSet" /> object.</summary>
		// Token: 0x0400295F RID: 10591
		Fill,
		/// <summary>Indicates that a method is used for a data operation that retrieves data.</summary>
		// Token: 0x04002960 RID: 10592
		Select,
		/// <summary>Indicates that a method is used for a data operation that updates data.</summary>
		// Token: 0x04002961 RID: 10593
		Update,
		/// <summary>Indicates that a method is used for a data operation that inserts data.</summary>
		// Token: 0x04002962 RID: 10594
		Insert,
		/// <summary>Indicates that a method is used for a data operation that deletes data.</summary>
		// Token: 0x04002963 RID: 10595
		Delete
	}
}
