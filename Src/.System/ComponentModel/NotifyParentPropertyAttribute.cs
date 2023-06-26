using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that the parent property is notified when the value of the property that this attribute is applied to is modified. This class cannot be inherited.</summary>
	// Token: 0x020005BE RID: 1470
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NotifyParentPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NotifyParentPropertyAttribute" /> class, using the specified value to determine whether the parent property is notified of changes to the value of the property.</summary>
		/// <param name="notifyParent">
		///   <see langword="true" /> if the parent should be notified of changes; otherwise, <see langword="false" />.</param>
		// Token: 0x0600370B RID: 14091 RVA: 0x000EF2D5 File Offset: 0x000ED4D5
		public NotifyParentPropertyAttribute(bool notifyParent)
		{
			this.notifyParent = notifyParent;
		}

		/// <summary>Gets or sets a value indicating whether the parent property should be notified of changes to the value of the property.</summary>
		/// <returns>
		///   <see langword="true" /> if the parent property should be notified of changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x0600370C RID: 14092 RVA: 0x000EF2E4 File Offset: 0x000ED4E4
		public bool NotifyParent
		{
			get
			{
				return this.notifyParent;
			}
		}

		/// <summary>Gets a value indicating whether the specified object is the same as the current object.</summary>
		/// <param name="obj">The object to test for equality.</param>
		/// <returns>
		///   <see langword="true" /> if the object is the same as this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600370D RID: 14093 RVA: 0x000EF2EC File Offset: 0x000ED4EC
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is NotifyParentPropertyAttribute && ((NotifyParentPropertyAttribute)obj).NotifyParent == this.notifyParent);
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x0600370E RID: 14094 RVA: 0x000EF314 File Offset: 0x000ED514
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the current value of the attribute is the default value of the attribute; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600370F RID: 14095 RVA: 0x000EF31C File Offset: 0x000ED51C
		public override bool IsDefaultAttribute()
		{
			return this.Equals(NotifyParentPropertyAttribute.Default);
		}

		/// <summary>Indicates that the parent property is notified of changes to the value of the property. This field is read-only.</summary>
		// Token: 0x04002AB2 RID: 10930
		public static readonly NotifyParentPropertyAttribute Yes = new NotifyParentPropertyAttribute(true);

		/// <summary>Indicates that the parent property is not be notified of changes to the value of the property. This field is read-only.</summary>
		// Token: 0x04002AB3 RID: 10931
		public static readonly NotifyParentPropertyAttribute No = new NotifyParentPropertyAttribute(false);

		/// <summary>Indicates the default attribute state, that the property should not notify the parent property of changes to its value. This field is read-only.</summary>
		// Token: 0x04002AB4 RID: 10932
		public static readonly NotifyParentPropertyAttribute Default = NotifyParentPropertyAttribute.No;

		// Token: 0x04002AB5 RID: 10933
		private bool notifyParent;
	}
}
