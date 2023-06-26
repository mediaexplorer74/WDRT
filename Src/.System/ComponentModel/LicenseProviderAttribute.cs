using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the <see cref="T:System.ComponentModel.LicenseProvider" /> to use with a class. This class cannot be inherited.</summary>
	// Token: 0x02000580 RID: 1408
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class LicenseProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class without a license provider.</summary>
		// Token: 0x0600340A RID: 13322 RVA: 0x000E40DA File Offset: 0x000E22DA
		public LicenseProviderAttribute()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class with the specified type.</summary>
		/// <param name="typeName">The fully qualified name of the license provider class.</param>
		// Token: 0x0600340B RID: 13323 RVA: 0x000E40E3 File Offset: 0x000E22E3
		public LicenseProviderAttribute(string typeName)
		{
			this.licenseProviderName = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class with the specified type of license provider.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the license provider class.</param>
		// Token: 0x0600340C RID: 13324 RVA: 0x000E40F2 File Offset: 0x000E22F2
		public LicenseProviderAttribute(Type type)
		{
			this.licenseProviderType = type;
		}

		/// <summary>Gets the license provider that must be used with the associated class.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the license provider. The default value is <see langword="null" />.</returns>
		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x000E4101 File Offset: 0x000E2301
		public Type LicenseProvider
		{
			get
			{
				if (this.licenseProviderType == null && this.licenseProviderName != null)
				{
					this.licenseProviderType = Type.GetType(this.licenseProviderName);
				}
				return this.licenseProviderType;
			}
		}

		/// <summary>Indicates a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x0600340E RID: 13326 RVA: 0x000E4130 File Offset: 0x000E2330
		public override object TypeId
		{
			get
			{
				string fullName = this.licenseProviderName;
				if (fullName == null && this.licenseProviderType != null)
				{
					fullName = this.licenseProviderType.FullName;
				}
				return base.GetType().FullName + fullName;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="value">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600340F RID: 13327 RVA: 0x000E4174 File Offset: 0x000E2374
		public override bool Equals(object value)
		{
			if (value is LicenseProviderAttribute && value != null)
			{
				Type licenseProvider = ((LicenseProviderAttribute)value).LicenseProvider;
				if (licenseProvider == this.LicenseProvider)
				{
					return true;
				}
				if (licenseProvider != null && licenseProvider.Equals(this.LicenseProvider))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LicenseProviderAttribute" />.</returns>
		// Token: 0x06003410 RID: 13328 RVA: 0x000E41C2 File Offset: 0x000E23C2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Specifies the default value, which is no provider. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x040029B6 RID: 10678
		public static readonly LicenseProviderAttribute Default = new LicenseProviderAttribute();

		// Token: 0x040029B7 RID: 10679
		private Type licenseProviderType;

		// Token: 0x040029B8 RID: 10680
		private string licenseProviderName;
	}
}
