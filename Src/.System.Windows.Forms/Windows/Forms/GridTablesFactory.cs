using System;

namespace System.Windows.Forms
{
	/// <summary>Provides the <see cref="M:System.Windows.Forms.GridTablesFactory.CreateGridTables(System.Windows.Forms.DataGridTableStyle,System.Object,System.String,System.Windows.Forms.BindingContext)" /> method.</summary>
	// Token: 0x0200018A RID: 394
	public sealed class GridTablesFactory
	{
		// Token: 0x0600183C RID: 6204 RVA: 0x00002843 File Offset: 0x00000A43
		private GridTablesFactory()
		{
		}

		/// <summary>Returns the specified <see cref="P:System.Windows.Forms.DataGridColumnStyle.DataGridTableStyle" /> in a one-element array.</summary>
		/// <param name="gridTable">A <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" />.</param>
		/// <param name="dataMember">A <see cref="T:System.String" />.</param>
		/// <param name="bindingManager">A <see cref="T:System.Windows.Forms.BindingContext" />.</param>
		/// <returns>An array of <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects.</returns>
		// Token: 0x0600183D RID: 6205 RVA: 0x00056E12 File Offset: 0x00055012
		public static DataGridTableStyle[] CreateGridTables(DataGridTableStyle gridTable, object dataSource, string dataMember, BindingContext bindingManager)
		{
			return new DataGridTableStyle[] { gridTable };
		}
	}
}
