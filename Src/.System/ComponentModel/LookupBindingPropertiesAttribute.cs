using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the properties that support lookup-based binding. This class cannot be inherited.</summary>
	// Token: 0x0200058B RID: 1419
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class LookupBindingPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class using no parameters.</summary>
		// Token: 0x06003448 RID: 13384 RVA: 0x000E4615 File Offset: 0x000E2815
		public LookupBindingPropertiesAttribute()
		{
			this.dataSource = null;
			this.displayMember = null;
			this.valueMember = null;
			this.lookupMember = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class.</summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		/// <param name="displayMember">The name of the property to be used for the display name.</param>
		/// <param name="valueMember">The name of the property to be used as the source for values.</param>
		/// <param name="lookupMember">The name of the property to be used for lookups.</param>
		// Token: 0x06003449 RID: 13385 RVA: 0x000E4639 File Offset: 0x000E2839
		public LookupBindingPropertiesAttribute(string dataSource, string displayMember, string valueMember, string lookupMember)
		{
			this.dataSource = dataSource;
			this.displayMember = displayMember;
			this.valueMember = valueMember;
			this.lookupMember = lookupMember;
		}

		/// <summary>Gets the name of the data source property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The data source property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x000E465E File Offset: 0x000E285E
		public string DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		/// <summary>Gets the name of the display member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the display member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x0600344B RID: 13387 RVA: 0x000E4666 File Offset: 0x000E2866
		public string DisplayMember
		{
			get
			{
				return this.displayMember;
			}
		}

		/// <summary>Gets the name of the value member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the value member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x000E466E File Offset: 0x000E286E
		public string ValueMember
		{
			get
			{
				return this.valueMember;
			}
		}

		/// <summary>Gets the name of the lookup member for the component to which this attribute is bound.</summary>
		/// <returns>The name of the lookup member for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x0600344D RID: 13389 RVA: 0x000E4676 File Offset: 0x000E2876
		public string LookupMember
		{
			get
			{
				return this.lookupMember;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> instance.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> instance</param>
		/// <returns>
		///   <see langword="true" /> if the object is equal to the current instance; otherwise, <see langword="false" />, indicating they are not equal.</returns>
		// Token: 0x0600344E RID: 13390 RVA: 0x000E4680 File Offset: 0x000E2880
		public override bool Equals(object obj)
		{
			LookupBindingPropertiesAttribute lookupBindingPropertiesAttribute = obj as LookupBindingPropertiesAttribute;
			return lookupBindingPropertiesAttribute != null && lookupBindingPropertiesAttribute.DataSource == this.dataSource && lookupBindingPropertiesAttribute.displayMember == this.displayMember && lookupBindingPropertiesAttribute.valueMember == this.valueMember && lookupBindingPropertiesAttribute.lookupMember == this.lookupMember;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" />.</returns>
		// Token: 0x0600344F RID: 13391 RVA: 0x000E46E3 File Offset: 0x000E28E3
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040029D8 RID: 10712
		private readonly string dataSource;

		// Token: 0x040029D9 RID: 10713
		private readonly string displayMember;

		// Token: 0x040029DA RID: 10714
		private readonly string valueMember;

		// Token: 0x040029DB RID: 10715
		private readonly string lookupMember;

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class.</summary>
		// Token: 0x040029DC RID: 10716
		public static readonly LookupBindingPropertiesAttribute Default = new LookupBindingPropertiesAttribute();
	}
}
