using System;

namespace System.ComponentModel
{
	/// <summary>Identifies a type as an object suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000533 RID: 1331
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataObjectAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class.</summary>
		// Token: 0x0600323E RID: 12862 RVA: 0x000E1123 File Offset: 0x000DF323
		public DataObjectAttribute()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class and indicates whether an object is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object.</summary>
		/// <param name="isDataObject">
		///   <see langword="true" /> if the object is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object; otherwise, <see langword="false" />.</param>
		// Token: 0x0600323F RID: 12863 RVA: 0x000E112C File Offset: 0x000DF32C
		public DataObjectAttribute(bool isDataObject)
		{
			this._isDataObject = isDataObject;
		}

		/// <summary>Gets a value indicating whether an object should be considered suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time.</summary>
		/// <returns>
		///   <see langword="true" /> if the object should be considered suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x000E113B File Offset: 0x000DF33B
		public bool IsDataObject
		{
			get
			{
				return this._isDataObject;
			}
		}

		/// <summary>Determines whether this instance of <see cref="T:System.ComponentModel.DataObjectAttribute" /> fits the pattern of another object.</summary>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003241 RID: 12865 RVA: 0x000E1144 File Offset: 0x000DF344
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectAttribute dataObjectAttribute = obj as DataObjectAttribute;
			return dataObjectAttribute != null && dataObjectAttribute.IsDataObject == this.IsDataObject;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003242 RID: 12866 RVA: 0x000E1171 File Offset: 0x000DF371
		public override int GetHashCode()
		{
			return this._isDataObject.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the current value of the attribute is the default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003243 RID: 12867 RVA: 0x000E117E File Offset: 0x000DF37E
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DataObjectAttribute.Default);
		}

		/// <summary>Indicates that the class is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x04002954 RID: 10580
		public static readonly DataObjectAttribute DataObject = new DataObjectAttribute(true);

		/// <summary>Indicates that the class is not suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x04002955 RID: 10581
		public static readonly DataObjectAttribute NonDataObject = new DataObjectAttribute(false);

		/// <summary>Represents the default value of the <see cref="T:System.ComponentModel.DataObjectAttribute" /> class, which indicates that the class is suitable for binding to an <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object at design time. This field is read-only.</summary>
		// Token: 0x04002956 RID: 10582
		public static readonly DataObjectAttribute Default = DataObjectAttribute.NonDataObject;

		// Token: 0x04002957 RID: 10583
		private bool _isDataObject;
	}
}
