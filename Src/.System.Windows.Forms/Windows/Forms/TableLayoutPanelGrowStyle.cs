using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a <see cref="T:System.Windows.Forms.TableLayoutPanel" /> will gain additional rows or columns after its existing cells are full.</summary>
	// Token: 0x02000391 RID: 913
	public enum TableLayoutPanelGrowStyle
	{
		/// <summary>The <see cref="T:System.Windows.Forms.TableLayoutPanel" /> does not allow additional rows or columns after it is full.</summary>
		// Token: 0x04002383 RID: 9091
		FixedSize,
		/// <summary>The <see cref="T:System.Windows.Forms.TableLayoutPanel" /> gains additional rows after it is full.</summary>
		// Token: 0x04002384 RID: 9092
		AddRows,
		/// <summary>The <see cref="T:System.Windows.Forms.TableLayoutPanel" /> gains additional columns after it is full.</summary>
		// Token: 0x04002385 RID: 9093
		AddColumns
	}
}
