using System;

namespace System.ComponentModel
{
	/// <summary>Identifies a data operation method exposed by a type, what type of operation the method performs, and whether the method is the default data method. This class cannot be inherited.</summary>
	// Token: 0x02000535 RID: 1333
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class DataObjectMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> class and identifies the type of data operation the method performs.</summary>
		/// <param name="methodType">One of the <see cref="T:System.ComponentModel.DataObjectMethodType" /> values that describes the data operation the method performs.</param>
		// Token: 0x0600324F RID: 12879 RVA: 0x000E1277 File Offset: 0x000DF477
		public DataObjectMethodAttribute(DataObjectMethodType methodType)
			: this(methodType, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> class, identifies the type of data operation the method performs, and identifies whether the method is the default data method that the data object exposes.</summary>
		/// <param name="methodType">One of the <see cref="T:System.ComponentModel.DataObjectMethodType" /> values that describes the data operation the method performs.</param>
		/// <param name="isDefault">
		///   <see langword="true" /> to indicate the method that the attribute is applied to is the default method of the data object for the specified <paramref name="methodType" />; otherwise, <see langword="false" />.</param>
		// Token: 0x06003250 RID: 12880 RVA: 0x000E1281 File Offset: 0x000DF481
		public DataObjectMethodAttribute(DataObjectMethodType methodType, bool isDefault)
		{
			this._methodType = methodType;
			this._isDefault = isDefault;
		}

		/// <summary>Gets a value indicating whether the method that the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> is applied to is the default data method exposed by the data object for a specific method type.</summary>
		/// <returns>
		///   <see langword="true" /> if the method is the default method exposed by the object for a method type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06003251 RID: 12881 RVA: 0x000E1297 File Offset: 0x000DF497
		public bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.DataObjectMethodType" /> value indicating the type of data operation the method performs.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DataObjectMethodType" /> values that identifies the type of data operation performed by the method to which the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> is applied.</returns>
		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x000E129F File Offset: 0x000DF49F
		public DataObjectMethodType MethodType
		{
			get
			{
				return this._methodType;
			}
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectMethodAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003253 RID: 12883 RVA: 0x000E12A8 File Offset: 0x000DF4A8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectMethodAttribute dataObjectMethodAttribute = obj as DataObjectMethodAttribute;
			return dataObjectMethodAttribute != null && dataObjectMethodAttribute.MethodType == this.MethodType && dataObjectMethodAttribute.IsDefault == this.IsDefault;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003254 RID: 12884 RVA: 0x000E12E4 File Offset: 0x000DF4E4
		public override int GetHashCode()
		{
			int methodType = (int)this._methodType;
			return methodType.GetHashCode() ^ this._isDefault.GetHashCode();
		}

		/// <summary>Gets a value indicating whether this instance shares a common pattern with a specified attribute.</summary>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectMethodAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003255 RID: 12885 RVA: 0x000E130C File Offset: 0x000DF50C
		public override bool Match(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectMethodAttribute dataObjectMethodAttribute = obj as DataObjectMethodAttribute;
			return dataObjectMethodAttribute != null && dataObjectMethodAttribute.MethodType == this.MethodType;
		}

		// Token: 0x0400295C RID: 10588
		private bool _isDefault;

		// Token: 0x0400295D RID: 10589
		private DataObjectMethodType _methodType;
	}
}
