using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Implements one row in a <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
	// Token: 0x02000268 RID: 616
	public abstract class GridItem
	{
		/// <summary>Gets or sets user-defined data about the <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.GridItem" />.</returns>
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002794 RID: 10132 RVA: 0x000B8D17 File Offset: 0x000B6F17
		// (set) Token: 0x06002795 RID: 10133 RVA: 0x000B8D1F File Offset: 0x000B6F1F
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		/// <summary>When overridden in a derived class, gets the collection of <see cref="T:System.Windows.Forms.GridItem" /> objects, if any, associated as a child of this <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>The collection of <see cref="T:System.Windows.Forms.GridItem" /> objects.</returns>
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002796 RID: 10134
		public abstract GridItemCollection GridItems { get; }

		/// <summary>When overridden in a derived class, gets the type of this <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.GridItemType" /> values.</returns>
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002797 RID: 10135
		public abstract GridItemType GridItemType { get; }

		/// <summary>When overridden in a derived class, gets the text of this <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the text associated with this <see cref="T:System.Windows.Forms.GridItem" />.</returns>
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002798 RID: 10136
		public abstract string Label { get; }

		/// <summary>When overridden in a derived class, gets the parent <see cref="T:System.Windows.Forms.GridItem" /> of this <see cref="T:System.Windows.Forms.GridItem" />, if any.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.GridItem" /> representing the parent of the <see cref="T:System.Windows.Forms.GridItem" />.</returns>
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002799 RID: 10137
		public abstract GridItem Parent { get; }

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is associated with this <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with this <see cref="T:System.Windows.Forms.GridItem" />.</returns>
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600279A RID: 10138
		public abstract PropertyDescriptor PropertyDescriptor { get; }

		/// <summary>When overridden in a derived class, gets the current value of this <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>The current value of this <see cref="T:System.Windows.Forms.GridItem" />. This can be <see langword="null" />.</returns>
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x0600279B RID: 10139
		public abstract object Value { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the specified property is expandable to show nested properties.</summary>
		/// <returns>
		///   <see langword="true" /> if the specified property can be expanded; otherwise, <see langword="false" />. The default is false.</returns>
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x0001180C File Offset: 0x0000FA0C
		public virtual bool Expandable
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.GridItem" /> is in an expanded state.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Windows.Forms.GridItem.Expanded" /> property was set to <see langword="true" />, but a <see cref="T:System.Windows.Forms.GridItem" /> is not expandable.</exception>
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x0600279E RID: 10142 RVA: 0x000B8D28 File Offset: 0x000B6F28
		public virtual bool Expanded
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("GridItemNotExpandable"));
			}
		}

		/// <summary>When overridden in a derived class, selects this <see cref="T:System.Windows.Forms.GridItem" /> in the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the selection is successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600279F RID: 10143
		public abstract bool Select();

		// Token: 0x0400104A RID: 4170
		private object userData;
	}
}
