using System;

namespace System.Windows.Forms
{
	/// <summary>Indicates which <see cref="T:System.Windows.Forms.ImageList" /> a property is related to.</summary>
	// Token: 0x0200033F RID: 831
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class RelatedImageListAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RelatedImageListAttribute" /> class.</summary>
		/// <param name="relatedImageList">The name of the <see cref="T:System.Windows.Forms.ImageList" /> the property relates to.</param>
		// Token: 0x060035CD RID: 13773 RVA: 0x000F3484 File Offset: 0x000F1684
		public RelatedImageListAttribute(string relatedImageList)
		{
			this.relatedImageList = relatedImageList;
		}

		/// <summary>Gets the name of the related <see cref="T:System.Windows.Forms.ImageList" /></summary>
		/// <returns>The name of the related <see cref="T:System.Windows.Forms.ImageList" /></returns>
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060035CE RID: 13774 RVA: 0x000F3493 File Offset: 0x000F1693
		public string RelatedImageList
		{
			get
			{
				return this.relatedImageList;
			}
		}

		// Token: 0x04001F62 RID: 8034
		private string relatedImageList;
	}
}
