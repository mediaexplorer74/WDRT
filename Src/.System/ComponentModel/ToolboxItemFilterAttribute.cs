using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the filter string and filter type to use for a toolbox item.</summary>
	// Token: 0x020005AF RID: 1455
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	[Serializable]
	public sealed class ToolboxItemFilterAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string.</summary>
		/// <param name="filterString">The filter string for the toolbox item.</param>
		// Token: 0x06003621 RID: 13857 RVA: 0x000EC00B File Offset: 0x000EA20B
		public ToolboxItemFilterAttribute(string filterString)
			: this(filterString, ToolboxItemFilterType.Allow)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string and type.</summary>
		/// <param name="filterString">The filter string for the toolbox item.</param>
		/// <param name="filterType">A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> indicating the type of the filter.</param>
		// Token: 0x06003622 RID: 13858 RVA: 0x000EC015 File Offset: 0x000EA215
		public ToolboxItemFilterAttribute(string filterString, ToolboxItemFilterType filterType)
		{
			if (filterString == null)
			{
				filterString = string.Empty;
			}
			this.filterString = filterString;
			this.filterType = filterType;
		}

		/// <summary>Gets the filter string for the toolbox item.</summary>
		/// <returns>The filter string for the toolbox item.</returns>
		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06003623 RID: 13859 RVA: 0x000EC035 File Offset: 0x000EA235
		public string FilterString
		{
			get
			{
				return this.filterString;
			}
		}

		/// <summary>Gets the type of the filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> that indicates the type of the filter.</returns>
		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x000EC03D File Offset: 0x000EA23D
		public ToolboxItemFilterType FilterType
		{
			get
			{
				return this.filterType;
			}
		}

		/// <summary>Gets the type ID for the attribute.</summary>
		/// <returns>The type ID for this attribute. All <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> objects with the same filter string return the same type ID.</returns>
		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x000EC045 File Offset: 0x000EA245
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					this.typeId = base.GetType().FullName + this.filterString;
				}
				return this.typeId;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003626 RID: 13862 RVA: 0x000EC074 File Offset: 0x000EA274
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterType.Equals(this.FilterType) && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06003627 RID: 13863 RVA: 0x000EC0C5 File Offset: 0x000EA2C5
		public override int GetHashCode()
		{
			return this.filterString.GetHashCode();
		}

		/// <summary>Indicates whether the specified object has a matching filter string.</summary>
		/// <param name="obj">The object to test for a matching filter string.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object has a matching filter string; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003628 RID: 13864 RVA: 0x000EC0D4 File Offset: 0x000EA2D4
		public override bool Match(object obj)
		{
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x06003629 RID: 13865 RVA: 0x000EC103 File Offset: 0x000EA303
		public override string ToString()
		{
			return this.filterString + "," + Enum.GetName(typeof(ToolboxItemFilterType), this.filterType);
		}

		// Token: 0x04002A82 RID: 10882
		private ToolboxItemFilterType filterType;

		// Token: 0x04002A83 RID: 10883
		private string filterString;

		// Token: 0x04002A84 RID: 10884
		private string typeId;
	}
}
