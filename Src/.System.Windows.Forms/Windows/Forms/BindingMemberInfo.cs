using System;

namespace System.Windows.Forms
{
	/// <summary>Contains information that enables a <see cref="T:System.Windows.Forms.Binding" /> to resolve a data binding to either the property of an object or the property of the current object in a list of objects.</summary>
	// Token: 0x02000137 RID: 311
	public struct BindingMemberInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingMemberInfo" /> class.</summary>
		/// <param name="dataMember">A navigation path that resolves to either the property of an object or the property of the current object in a list of objects.</param>
		// Token: 0x06000B53 RID: 2899 RVA: 0x000201D0 File Offset: 0x0001E3D0
		public BindingMemberInfo(string dataMember)
		{
			if (dataMember == null)
			{
				dataMember = "";
			}
			int num = dataMember.LastIndexOf(".");
			if (num != -1)
			{
				this.dataList = dataMember.Substring(0, num);
				this.dataField = dataMember.Substring(num + 1);
				return;
			}
			this.dataList = "";
			this.dataField = dataMember;
		}

		/// <summary>Gets the property name, or the period-delimited hierarchy of property names, that comes before the property name of the data-bound object.</summary>
		/// <returns>The property name, or the period-delimited hierarchy of property names, that comes before the data-bound object property name.</returns>
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00020227 File Offset: 0x0001E427
		public string BindingPath
		{
			get
			{
				if (this.dataList == null)
				{
					return "";
				}
				return this.dataList;
			}
		}

		/// <summary>Gets the property name of the data-bound object.</summary>
		/// <returns>The property name of the data-bound object. This can be an empty string ("").</returns>
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0002023D File Offset: 0x0001E43D
		public string BindingField
		{
			get
			{
				if (this.dataField == null)
				{
					return "";
				}
				return this.dataField;
			}
		}

		/// <summary>Gets the information that is used to specify the property name of the data-bound object.</summary>
		/// <returns>An empty string (""), a single property name, or a hierarchy of period-delimited property names that resolves to the property name of the final data-bound object.</returns>
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00020253 File Offset: 0x0001E453
		public string BindingMember
		{
			get
			{
				if (this.BindingPath.Length <= 0)
				{
					return this.BindingField;
				}
				return this.BindingPath + "." + this.BindingField;
			}
		}

		/// <summary>Determines whether the specified object is equal to this <see cref="T:System.Windows.Forms.BindingMemberInfo" />.</summary>
		/// <param name="otherObject">The object to compare for equality.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="otherObject" /> is a <see cref="T:System.Windows.Forms.BindingMemberInfo" /> and both <see cref="P:System.Windows.Forms.BindingMemberInfo.BindingMember" /> strings are equal; otherwise <see langword="false" />.</returns>
		// Token: 0x06000B57 RID: 2903 RVA: 0x00020280 File Offset: 0x0001E480
		public override bool Equals(object otherObject)
		{
			if (otherObject is BindingMemberInfo)
			{
				BindingMemberInfo bindingMemberInfo = (BindingMemberInfo)otherObject;
				return string.Equals(this.BindingMember, bindingMemberInfo.BindingMember, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		/// <summary>Determines whether two <see cref="T:System.Windows.Forms.BindingMemberInfo" /> objects are equal.</summary>
		/// <param name="a">The first <see cref="T:System.Windows.Forms.BindingMemberInfo" /> to compare for equality.</param>
		/// <param name="b">The second <see cref="T:System.Windows.Forms.BindingMemberInfo" /> to compare for equality.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.BindingMemberInfo.BindingMember" /> strings for <paramref name="a" /> and <paramref name="b" /> are equal; otherwise <see langword="false" />.</returns>
		// Token: 0x06000B58 RID: 2904 RVA: 0x000202B1 File Offset: 0x0001E4B1
		public static bool operator ==(BindingMemberInfo a, BindingMemberInfo b)
		{
			return a.Equals(b);
		}

		/// <summary>Determines whether two <see cref="T:System.Windows.Forms.BindingMemberInfo" /> objects are not equal.</summary>
		/// <param name="a">The first <see cref="T:System.Windows.Forms.BindingMemberInfo" /> to compare for inequality.</param>
		/// <param name="b">The second <see cref="T:System.Windows.Forms.BindingMemberInfo" /> to compare for inequality.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.BindingMemberInfo.BindingMember" /> strings for <paramref name="a" /> and <paramref name="b" /> are not equal; otherwise <see langword="false" />.</returns>
		// Token: 0x06000B59 RID: 2905 RVA: 0x000202C6 File Offset: 0x0001E4C6
		public static bool operator !=(BindingMemberInfo a, BindingMemberInfo b)
		{
			return !a.Equals(b);
		}

		/// <summary>Returns the hash code for this <see cref="T:System.Windows.Forms.BindingMemberInfo" />.</summary>
		/// <returns>The hash code for this <see cref="T:System.Windows.Forms.BindingMemberInfo" />.</returns>
		// Token: 0x06000B5A RID: 2906 RVA: 0x000202DE File Offset: 0x0001E4DE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040006BC RID: 1724
		private string dataList;

		// Token: 0x040006BD RID: 1725
		private string dataField;
	}
}
