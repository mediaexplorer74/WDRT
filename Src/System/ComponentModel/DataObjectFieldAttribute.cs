using System;

namespace System.ComponentModel
{
	/// <summary>Provides metadata for a property representing a data field. This class cannot be inherited.</summary>
	// Token: 0x02000534 RID: 1332
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DataObjectFieldAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row.</summary>
		/// <param name="primaryKey">
		///   <see langword="true" /> to indicate that the field is in the primary key of the data row; otherwise, <see langword="false" />.</param>
		// Token: 0x06003245 RID: 12869 RVA: 0x000E11AD File Offset: 0x000DF3AD
		public DataObjectFieldAttribute(bool primaryKey)
			: this(primaryKey, false, false, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, and whether the field is a database identity field.</summary>
		/// <param name="primaryKey">
		///   <see langword="true" /> to indicate that the field is in the primary key of the data row; otherwise, <see langword="false" />.</param>
		/// <param name="isIdentity">
		///   <see langword="true" /> to indicate that the field is an identity field that uniquely identifies the data row; otherwise, <see langword="false" />.</param>
		// Token: 0x06003246 RID: 12870 RVA: 0x000E11B9 File Offset: 0x000DF3B9
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity)
			: this(primaryKey, isIdentity, false, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, whether the field is a database identity field, and whether the field can be null.</summary>
		/// <param name="primaryKey">
		///   <see langword="true" /> to indicate that the field is in the primary key of the data row; otherwise, <see langword="false" />.</param>
		/// <param name="isIdentity">
		///   <see langword="true" /> to indicate that the field is an identity field that uniquely identifies the data row; otherwise, <see langword="false" />.</param>
		/// <param name="isNullable">
		///   <see langword="true" /> to indicate that the field can be null in the data store; otherwise, <see langword="false" />.</param>
		// Token: 0x06003247 RID: 12871 RVA: 0x000E11C5 File Offset: 0x000DF3C5
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable)
			: this(primaryKey, isIdentity, isNullable, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, whether it is a database identity field, and whether it can be null and sets the length of the field.</summary>
		/// <param name="primaryKey">
		///   <see langword="true" /> to indicate that the field is in the primary key of the data row; otherwise, <see langword="false" />.</param>
		/// <param name="isIdentity">
		///   <see langword="true" /> to indicate that the field is an identity field that uniquely identifies the data row; otherwise, <see langword="false" />.</param>
		/// <param name="isNullable">
		///   <see langword="true" /> to indicate that the field can be null in the data store; otherwise, <see langword="false" />.</param>
		/// <param name="length">The length of the field in bytes.</param>
		// Token: 0x06003248 RID: 12872 RVA: 0x000E11D1 File Offset: 0x000DF3D1
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable, int length)
		{
			this._primaryKey = primaryKey;
			this._isIdentity = isIdentity;
			this._isNullable = isNullable;
			this._length = length;
		}

		/// <summary>Gets a value indicating whether a property represents an identity field in the underlying data.</summary>
		/// <returns>
		///   <see langword="true" /> if the property represents an identity field in the underlying data; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06003249 RID: 12873 RVA: 0x000E11F6 File Offset: 0x000DF3F6
		public bool IsIdentity
		{
			get
			{
				return this._isIdentity;
			}
		}

		/// <summary>Gets a value indicating whether a property represents a field that can be null in the underlying data store.</summary>
		/// <returns>
		///   <see langword="true" /> if the property represents a field that can be null in the underlying data store; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600324A RID: 12874 RVA: 0x000E11FE File Offset: 0x000DF3FE
		public bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
		}

		/// <summary>Gets the length of the property in bytes.</summary>
		/// <returns>The length of the property in bytes, or -1 if not set.</returns>
		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x000E1206 File Offset: 0x000DF406
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		/// <summary>Gets a value indicating whether a property is in the primary key in the underlying data.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is in the primary key of the data store; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x000E120E File Offset: 0x000DF40E
		public bool PrimaryKey
		{
			get
			{
				return this._primaryKey;
			}
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectFieldAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600324D RID: 12877 RVA: 0x000E1218 File Offset: 0x000DF418
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectFieldAttribute dataObjectFieldAttribute = obj as DataObjectFieldAttribute;
			return dataObjectFieldAttribute != null && dataObjectFieldAttribute.IsIdentity == this.IsIdentity && dataObjectFieldAttribute.IsNullable == this.IsNullable && dataObjectFieldAttribute.Length == this.Length && dataObjectFieldAttribute.PrimaryKey == this.PrimaryKey;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600324E RID: 12878 RVA: 0x000E126F File Offset: 0x000DF46F
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04002958 RID: 10584
		private bool _primaryKey;

		// Token: 0x04002959 RID: 10585
		private bool _isIdentity;

		// Token: 0x0400295A RID: 10586
		private bool _isNullable;

		// Token: 0x0400295B RID: 10587
		private int _length;
	}
}
