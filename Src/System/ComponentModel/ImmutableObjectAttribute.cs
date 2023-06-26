using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that an object has no subproperties capable of being edited. This class cannot be inherited.</summary>
	// Token: 0x02000564 RID: 1380
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ImmutableObjectAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ImmutableObjectAttribute" /> class.</summary>
		/// <param name="immutable">
		///   <see langword="true" /> if the object is immutable; otherwise, <see langword="false" />.</param>
		// Token: 0x06003396 RID: 13206 RVA: 0x000E379B File Offset: 0x000E199B
		public ImmutableObjectAttribute(bool immutable)
		{
			this.immutable = immutable;
		}

		/// <summary>Gets whether the object is immutable.</summary>
		/// <returns>
		///   <see langword="true" /> if the object is immutable; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06003397 RID: 13207 RVA: 0x000E37B1 File Offset: 0x000E19B1
		public bool Immutable
		{
			get
			{
				return this.immutable;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003398 RID: 13208 RVA: 0x000E37BC File Offset: 0x000E19BC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ImmutableObjectAttribute immutableObjectAttribute = obj as ImmutableObjectAttribute;
			return immutableObjectAttribute != null && immutableObjectAttribute.Immutable == this.immutable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</returns>
		// Token: 0x06003399 RID: 13209 RVA: 0x000E37E9 File Offset: 0x000E19E9
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the value of this instance is the default value.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is the default attribute for the class; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600339A RID: 13210 RVA: 0x000E37F1 File Offset: 0x000E19F1
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ImmutableObjectAttribute.Default);
		}

		/// <summary>Specifies that an object has no subproperties that can be edited. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x040029A6 RID: 10662
		public static readonly ImmutableObjectAttribute Yes = new ImmutableObjectAttribute(true);

		/// <summary>Specifies that an object has at least one editable subproperty. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x040029A7 RID: 10663
		public static readonly ImmutableObjectAttribute No = new ImmutableObjectAttribute(false);

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</summary>
		// Token: 0x040029A8 RID: 10664
		public static readonly ImmutableObjectAttribute Default = ImmutableObjectAttribute.No;

		// Token: 0x040029A9 RID: 10665
		private bool immutable = true;
	}
}
