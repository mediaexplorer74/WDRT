using System;

namespace System.ComponentModel
{
	/// <summary>
	///   <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> marks a component's visibility. If <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Yes" /> is present, a visual designer can show this component on a designer.</summary>
	// Token: 0x02000545 RID: 1349
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public sealed class DesignTimeVisibleAttribute : Attribute
	{
		/// <summary>Creates a new <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> with the <see cref="P:System.ComponentModel.DesignTimeVisibleAttribute.Visible" /> property set to the given value in <paramref name="visible" />.</summary>
		/// <param name="visible">The value that the <see cref="P:System.ComponentModel.DesignTimeVisibleAttribute.Visible" /> property will be set against.</param>
		// Token: 0x060032BA RID: 12986 RVA: 0x000E2284 File Offset: 0x000E0484
		public DesignTimeVisibleAttribute(bool visible)
		{
			this.visible = visible;
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> set to the default value of <see langword="false" />.</summary>
		// Token: 0x060032BB RID: 12987 RVA: 0x000E2293 File Offset: 0x000E0493
		public DesignTimeVisibleAttribute()
		{
		}

		/// <summary>Gets or sets whether the component should be shown at design time.</summary>
		/// <returns>
		///   <see langword="true" /> if this component should be shown at design time, or <see langword="false" /> if it shouldn't.</returns>
		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x060032BC RID: 12988 RVA: 0x000E229B File Offset: 0x000E049B
		public bool Visible
		{
			get
			{
				return this.visible;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An Object to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032BD RID: 12989 RVA: 0x000E22A4 File Offset: 0x000E04A4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignTimeVisibleAttribute designTimeVisibleAttribute = obj as DesignTimeVisibleAttribute;
			return designTimeVisibleAttribute != null && designTimeVisibleAttribute.Visible == this.visible;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060032BE RID: 12990 RVA: 0x000E22D1 File Offset: 0x000E04D1
		public override int GetHashCode()
		{
			return typeof(DesignTimeVisibleAttribute).GetHashCode() ^ (this.visible ? (-1) : 0);
		}

		/// <summary>Gets a value indicating if this instance is equal to the <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Default" /> value.</summary>
		/// <returns>
		///   <see langword="true" />, if this instance is equal to the <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Default" /> value; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032BF RID: 12991 RVA: 0x000E22EF File Offset: 0x000E04EF
		public override bool IsDefaultAttribute()
		{
			return this.Visible == DesignTimeVisibleAttribute.Default.Visible;
		}

		// Token: 0x04002984 RID: 10628
		private bool visible;

		/// <summary>Marks a component as visible in a visual designer.</summary>
		// Token: 0x04002985 RID: 10629
		public static readonly DesignTimeVisibleAttribute Yes = new DesignTimeVisibleAttribute(true);

		/// <summary>Marks a component as not visible in a visual designer.</summary>
		// Token: 0x04002986 RID: 10630
		public static readonly DesignTimeVisibleAttribute No = new DesignTimeVisibleAttribute(false);

		/// <summary>The default visibility which is <see langword="Yes" />.</summary>
		// Token: 0x04002987 RID: 10631
		public static readonly DesignTimeVisibleAttribute Default = DesignTimeVisibleAttribute.Yes;
	}
}
