using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	/// <summary>Represents a collection of toolbox items.</summary>
	// Token: 0x02000080 RID: 128
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ToolboxItemCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> class using the specified collection.</summary>
		/// <param name="value">A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> to fill the new collection with.</param>
		// Token: 0x060008AC RID: 2220 RVA: 0x00020D06 File Offset: 0x0001EF06
		public ToolboxItemCollection(ToolboxItemCollection value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> class using the specified array of toolbox items.</summary>
		/// <param name="value">An array of type <see cref="T:System.Drawing.Design.ToolboxItem" /> containing the toolbox items to fill the collection with.</param>
		// Token: 0x060008AD RID: 2221 RVA: 0x00020D06 File Offset: 0x0001EF06
		public ToolboxItemCollection(ToolboxItem[] value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Design.ToolboxItem" /> at the specified index.</summary>
		/// <param name="index">The index of the object to get or set.</param>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItem" /> at each valid index in the collection.</returns>
		// Token: 0x17000334 RID: 820
		public ToolboxItem this[int index]
		{
			get
			{
				return (ToolboxItem)base.InnerList[index];
			}
		}

		/// <summary>Indicates whether the collection contains the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <param name="value">A <see cref="T:System.Drawing.Design.ToolboxItem" /> to search the collection for.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060008AF RID: 2223 RVA: 0x00020D2D File Offset: 0x0001EF2D
		public bool Contains(ToolboxItem value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the collection to the specified array beginning with the specified destination index.</summary>
		/// <param name="array">The array to copy to.</param>
		/// <param name="index">The index to begin copying to.</param>
		// Token: 0x060008B0 RID: 2224 RVA: 0x00020D3B File Offset: 0x0001EF3B
		public void CopyTo(ToolboxItem[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Drawing.Design.ToolboxItem" />, if it exists in the collection.</summary>
		/// <param name="value">A <see cref="T:System.Drawing.Design.ToolboxItem" /> to get the index of in the collection.</param>
		/// <returns>The index of the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x060008B1 RID: 2225 RVA: 0x00020D4A File Offset: 0x0001EF4A
		public int IndexOf(ToolboxItem value)
		{
			return base.InnerList.IndexOf(value);
		}
	}
}
