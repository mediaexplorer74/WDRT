using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the display name for a property, event, or public void method which takes no arguments.</summary>
	// Token: 0x02000546 RID: 1350
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
	public class DisplayNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DisplayNameAttribute" /> class.</summary>
		// Token: 0x060032C1 RID: 12993 RVA: 0x000E2325 File Offset: 0x000E0525
		public DisplayNameAttribute()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DisplayNameAttribute" /> class using the display name.</summary>
		/// <param name="displayName">The display name.</param>
		// Token: 0x060032C2 RID: 12994 RVA: 0x000E2332 File Offset: 0x000E0532
		public DisplayNameAttribute(string displayName)
		{
			this._displayName = displayName;
		}

		/// <summary>Gets the display name for a property, event, or public void method that takes no arguments stored in this attribute.</summary>
		/// <returns>The display name.</returns>
		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x060032C3 RID: 12995 RVA: 0x000E2341 File Offset: 0x000E0541
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		/// <summary>Gets or sets the display name.</summary>
		/// <returns>The display name.</returns>
		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000E2349 File Offset: 0x000E0549
		// (set) Token: 0x060032C5 RID: 12997 RVA: 0x000E2351 File Offset: 0x000E0551
		protected string DisplayNameValue
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.DisplayNameAttribute" /> instances are equal.</summary>
		/// <param name="obj">The <see cref="T:System.ComponentModel.DisplayNameAttribute" /> to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032C6 RID: 12998 RVA: 0x000E235C File Offset: 0x000E055C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DisplayNameAttribute displayNameAttribute = obj as DisplayNameAttribute;
			return displayNameAttribute != null && displayNameAttribute.DisplayName == this.DisplayName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.DisplayNameAttribute" />.</returns>
		// Token: 0x060032C7 RID: 12999 RVA: 0x000E238C File Offset: 0x000E058C
		public override int GetHashCode()
		{
			return this.DisplayName.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032C8 RID: 13000 RVA: 0x000E2399 File Offset: 0x000E0599
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DisplayNameAttribute.Default);
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DisplayNameAttribute" />. This field is read-only.</summary>
		// Token: 0x04002988 RID: 10632
		public static readonly DisplayNameAttribute Default = new DisplayNameAttribute();

		// Token: 0x04002989 RID: 10633
		private string _displayName;
	}
}
