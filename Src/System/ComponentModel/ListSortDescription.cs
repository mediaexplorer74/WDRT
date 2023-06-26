using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a description of the sort operation applied to a data source.</summary>
	// Token: 0x02000587 RID: 1415
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ListSortDescription
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescription" /> class with the specified property description and direction.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the property by which the data source is sorted.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDescription" /> values.</param>
		// Token: 0x06003429 RID: 13353 RVA: 0x000E4428 File Offset: 0x000E2628
		public ListSortDescription(PropertyDescriptor property, ListSortDirection direction)
		{
			this.property = property;
			this.sortDirection = direction;
		}

		/// <summary>Gets or sets the abstract description of a class property associated with this <see cref="T:System.ComponentModel.ListSortDescription" /></summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with this <see cref="T:System.ComponentModel.ListSortDescription" />.</returns>
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x000E443E File Offset: 0x000E263E
		// (set) Token: 0x0600342B RID: 13355 RVA: 0x000E4446 File Offset: 0x000E2646
		public PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.property;
			}
			set
			{
				this.property = value;
			}
		}

		/// <summary>Gets or sets the direction of the sort operation associated with this <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</returns>
		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x0600342C RID: 13356 RVA: 0x000E444F File Offset: 0x000E264F
		// (set) Token: 0x0600342D RID: 13357 RVA: 0x000E4457 File Offset: 0x000E2657
		public ListSortDirection SortDirection
		{
			get
			{
				return this.sortDirection;
			}
			set
			{
				this.sortDirection = value;
			}
		}

		// Token: 0x040029CE RID: 10702
		private PropertyDescriptor property;

		// Token: 0x040029CF RID: 10703
		private ListSortDirection sortDirection;
	}
}
