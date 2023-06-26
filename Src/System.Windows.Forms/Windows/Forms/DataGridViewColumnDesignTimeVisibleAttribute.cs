using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies whether a column type is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer. This class cannot be inherited.</summary>
	// Token: 0x020001C0 RID: 448
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataGridViewColumnDesignTimeVisibleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> class using the specified value to initialize the <see cref="P:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Visible" /> property.</summary>
		/// <param name="visible">The value of the <see cref="P:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Visible" /> property.</param>
		// Token: 0x06001FA2 RID: 8098 RVA: 0x000958E8 File Offset: 0x00093AE8
		public DataGridViewColumnDesignTimeVisibleAttribute(bool visible)
		{
			this.visible = visible;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> class using the default <see cref="P:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Visible" /> property value of <see langword="true" />.</summary>
		// Token: 0x06001FA3 RID: 8099 RVA: 0x000958F7 File Offset: 0x00093AF7
		public DataGridViewColumnDesignTimeVisibleAttribute()
		{
		}

		/// <summary>Gets a value indicating whether the column type is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate that the column type is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x000958FF File Offset: 0x00093AFF
		public bool Visible
		{
			get
			{
				return this.visible;
			}
		}

		/// <summary>Gets a value indicating whether this object is equivalent to the specified object.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		/// <returns>
		///   <see langword="true" /> to indicate that the specified object is a <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> instance with the same <see cref="P:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Visible" /> property value as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001FA5 RID: 8101 RVA: 0x00095908 File Offset: 0x00093B08
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataGridViewColumnDesignTimeVisibleAttribute dataGridViewColumnDesignTimeVisibleAttribute = obj as DataGridViewColumnDesignTimeVisibleAttribute;
			return dataGridViewColumnDesignTimeVisibleAttribute != null && dataGridViewColumnDesignTimeVisibleAttribute.Visible == this.visible;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001FA6 RID: 8102 RVA: 0x00095935 File Offset: 0x00093B35
		public override int GetHashCode()
		{
			return typeof(DataGridViewColumnDesignTimeVisibleAttribute).GetHashCode() ^ (this.visible ? (-1) : 0);
		}

		/// <summary>Gets a value indicating whether this attribute instance is equal to the <see cref="F:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Default" /> attribute value.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate that this instance is equal to the <see cref="F:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Default" /> instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001FA7 RID: 8103 RVA: 0x00095953 File Offset: 0x00093B53
		public override bool IsDefaultAttribute()
		{
			return this.Visible == DataGridViewColumnDesignTimeVisibleAttribute.Default.Visible;
		}

		// Token: 0x04000D3F RID: 3391
		private bool visible;

		/// <summary>A <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> value indicating that the column is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer.</summary>
		// Token: 0x04000D40 RID: 3392
		public static readonly DataGridViewColumnDesignTimeVisibleAttribute Yes = new DataGridViewColumnDesignTimeVisibleAttribute(true);

		/// <summary>A <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> value indicating that the column is not visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer.</summary>
		// Token: 0x04000D41 RID: 3393
		public static readonly DataGridViewColumnDesignTimeVisibleAttribute No = new DataGridViewColumnDesignTimeVisibleAttribute(false);

		/// <summary>The default <see cref="T:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute" /> value, which is <see cref="F:System.Windows.Forms.DataGridViewColumnDesignTimeVisibleAttribute.Yes" />, indicating that the column is visible in the <see cref="T:System.Windows.Forms.DataGridView" /> designer.</summary>
		// Token: 0x04000D42 RID: 3394
		public static readonly DataGridViewColumnDesignTimeVisibleAttribute Default = DataGridViewColumnDesignTimeVisibleAttribute.Yes;
	}
}
