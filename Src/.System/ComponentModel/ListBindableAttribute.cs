using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that a list can be used as a data source. A visual designer should use this attribute to determine whether to display a particular list in a data-binding picker. This class cannot be inherited.</summary>
	// Token: 0x02000583 RID: 1411
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ListBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListBindableAttribute" /> class using a value to indicate whether the list is bindable.</summary>
		/// <param name="listBindable">
		///   <see langword="true" /> if the list is bindable; otherwise, <see langword="false" />.</param>
		// Token: 0x06003416 RID: 13334 RVA: 0x000E430E File Offset: 0x000E250E
		public ListBindableAttribute(bool listBindable)
		{
			this.listBindable = listBindable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListBindableAttribute" /> class using <see cref="T:System.ComponentModel.BindableSupport" /> to indicate whether the list is bindable.</summary>
		/// <param name="flags">A <see cref="T:System.ComponentModel.BindableSupport" /> that indicates whether the list is bindable.</param>
		// Token: 0x06003417 RID: 13335 RVA: 0x000E431D File Offset: 0x000E251D
		public ListBindableAttribute(BindableSupport flags)
		{
			this.listBindable = flags > BindableSupport.No;
			this.isDefault = flags == BindableSupport.Default;
		}

		/// <summary>Gets whether the list is bindable.</summary>
		/// <returns>
		///   <see langword="true" /> if the list is bindable; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06003418 RID: 13336 RVA: 0x000E4339 File Offset: 0x000E2539
		public bool ListBindable
		{
			get
			{
				return this.listBindable;
			}
		}

		/// <summary>Returns whether the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		/// <param name="obj">The object to test equality with.</param>
		/// <returns>
		///   <see langword="true" /> if the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003419 RID: 13337 RVA: 0x000E4344 File Offset: 0x000E2544
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ListBindableAttribute listBindableAttribute = obj as ListBindableAttribute;
			return listBindableAttribute != null && listBindableAttribute.ListBindable == this.listBindable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ListBindableAttribute" />.</returns>
		// Token: 0x0600341A RID: 13338 RVA: 0x000E4371 File Offset: 0x000E2571
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns whether <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600341B RID: 13339 RVA: 0x000E4379 File Offset: 0x000E2579
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ListBindableAttribute.Default) || this.isDefault;
		}

		/// <summary>Specifies that the list is bindable. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x040029BC RID: 10684
		public static readonly ListBindableAttribute Yes = new ListBindableAttribute(true);

		/// <summary>Specifies that the list is not bindable. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x040029BD RID: 10685
		public static readonly ListBindableAttribute No = new ListBindableAttribute(false);

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		// Token: 0x040029BE RID: 10686
		public static readonly ListBindableAttribute Default = ListBindableAttribute.Yes;

		// Token: 0x040029BF RID: 10687
		private bool listBindable;

		// Token: 0x040029C0 RID: 10688
		private bool isDefault;
	}
}
