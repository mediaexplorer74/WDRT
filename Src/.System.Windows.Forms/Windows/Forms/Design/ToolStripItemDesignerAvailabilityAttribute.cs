using System;

namespace System.Windows.Forms.Design
{
	/// <summary>Specifies which types a <see cref="T:System.Windows.Forms.ToolStripItem" /> can appear in. This class cannot be inherited.</summary>
	// Token: 0x0200048D RID: 1165
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ToolStripItemDesignerAvailabilityAttribute : Attribute
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailabilityAttribute" /> class.</summary>
		// Token: 0x06004E37 RID: 20023 RVA: 0x0014267F File Offset: 0x0014087F
		public ToolStripItemDesignerAvailabilityAttribute()
		{
			this.visibility = ToolStripItemDesignerAvailability.None;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailabilityAttribute" /> class with the specified visibility.</summary>
		/// <param name="visibility">A <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailability" /> value specifying the visibility.</param>
		// Token: 0x06004E38 RID: 20024 RVA: 0x0014268E File Offset: 0x0014088E
		public ToolStripItemDesignerAvailabilityAttribute(ToolStripItemDesignerAvailability visibility)
		{
			this.visibility = visibility;
		}

		/// <summary>Gets the visibility of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailability" /> representing the visibility.</returns>
		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06004E39 RID: 20025 RVA: 0x0014269D File Offset: 0x0014089D
		public ToolStripItemDesignerAvailability ItemAdditionVisibility
		{
			get
			{
				return this.visibility;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E3A RID: 20026 RVA: 0x001426A8 File Offset: 0x001408A8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolStripItemDesignerAvailabilityAttribute toolStripItemDesignerAvailabilityAttribute = obj as ToolStripItemDesignerAvailabilityAttribute;
			return toolStripItemDesignerAvailabilityAttribute != null && toolStripItemDesignerAvailabilityAttribute.ItemAdditionVisibility == this.visibility;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06004E3B RID: 20027 RVA: 0x001426D5 File Offset: 0x001408D5
		public override int GetHashCode()
		{
			return this.visibility.GetHashCode();
		}

		/// <summary>When overriden in a derived class, indicates whether the value of this instance is the default value for the derived class.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is the default attribute for the class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E3C RID: 20028 RVA: 0x001426E8 File Offset: 0x001408E8
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ToolStripItemDesignerAvailabilityAttribute.Default);
		}

		// Token: 0x040033F5 RID: 13301
		private ToolStripItemDesignerAvailability visibility;

		/// <summary>Specifies the default value of the <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailabilityAttribute" />. This field is read-only.</summary>
		// Token: 0x040033F6 RID: 13302
		public static readonly ToolStripItemDesignerAvailabilityAttribute Default = new ToolStripItemDesignerAvailabilityAttribute();
	}
}
