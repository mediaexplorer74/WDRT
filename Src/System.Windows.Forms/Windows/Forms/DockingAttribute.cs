using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the default docking behavior for a control.</summary>
	// Token: 0x0200022E RID: 558
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DockingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DockingAttribute" /> class.</summary>
		// Token: 0x0600246C RID: 9324 RVA: 0x000AC15C File Offset: 0x000AA35C
		public DockingAttribute()
		{
			this.dockingBehavior = DockingBehavior.Never;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DockingAttribute" /> class with the given docking behavior.</summary>
		/// <param name="dockingBehavior">A <see cref="T:System.Windows.Forms.DockingBehavior" /> value specifying the default behavior.</param>
		// Token: 0x0600246D RID: 9325 RVA: 0x000AC16B File Offset: 0x000AA36B
		public DockingAttribute(DockingBehavior dockingBehavior)
		{
			this.dockingBehavior = dockingBehavior;
		}

		/// <summary>Gets the docking behavior supplied to this attribute.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DockingBehavior" /> value.</returns>
		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x000AC17A File Offset: 0x000AA37A
		public DockingBehavior DockingBehavior
		{
			get
			{
				return this.dockingBehavior;
			}
		}

		/// <summary>Compares an arbitrary object with the <see cref="T:System.Windows.Forms.DockingAttribute" /> object for equality.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> against which to compare this <see cref="T:System.Windows.Forms.DockingAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> is <paramref name="obj" /> is equal to this <see cref="T:System.Windows.Forms.DockingAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600246F RID: 9327 RVA: 0x000AC184 File Offset: 0x000AA384
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DockingAttribute dockingAttribute = obj as DockingAttribute;
			return dockingAttribute != null && dockingAttribute.DockingBehavior == this.dockingBehavior;
		}

		/// <summary>The hash code for this object.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing an in-memory hash of this object.</returns>
		// Token: 0x06002470 RID: 9328 RVA: 0x000AC1B1 File Offset: 0x000AA3B1
		public override int GetHashCode()
		{
			return this.dockingBehavior.GetHashCode();
		}

		/// <summary>Specifies whether this <see cref="T:System.Windows.Forms.DockingAttribute" /> is the default docking attribute.</summary>
		/// <returns>
		///   <see langword="true" /> is the current <see cref="T:System.Windows.Forms.DockingAttribute" /> is the default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002471 RID: 9329 RVA: 0x000AC1C4 File Offset: 0x000AA3C4
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DockingAttribute.Default);
		}

		// Token: 0x04000EF0 RID: 3824
		private DockingBehavior dockingBehavior;

		/// <summary>The default <see cref="T:System.Windows.Forms.DockingAttribute" /> for this control.</summary>
		// Token: 0x04000EF1 RID: 3825
		public static readonly DockingAttribute Default = new DockingAttribute();
	}
}
