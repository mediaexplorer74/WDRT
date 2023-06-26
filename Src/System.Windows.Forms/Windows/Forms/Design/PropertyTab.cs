using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a base class for property tabs.</summary>
	// Token: 0x0200048B RID: 1163
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class PropertyTab : IExtenderProvider
	{
		/// <summary>Allows a <see cref="T:System.Windows.Forms.Design.PropertyTab" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Windows.Forms.Design.PropertyTab" /> is reclaimed by garbage collection.</summary>
		// Token: 0x06004E29 RID: 20009 RVA: 0x00142584 File Offset: 0x00140784
		~PropertyTab()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the bitmap that is displayed for the <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> to display for the <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</returns>
		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06004E2A RID: 20010 RVA: 0x001425B4 File Offset: 0x001407B4
		public virtual Bitmap Bitmap
		{
			get
			{
				if (!this.checkedBmp && this.bitmap == null)
				{
					string text = base.GetType().Name + ".bmp";
					try
					{
						this.bitmap = new Bitmap(base.GetType(), text);
					}
					catch (Exception ex)
					{
					}
					this.checkedBmp = true;
				}
				return this.bitmap;
			}
		}

		/// <summary>Gets or sets the array of components the property tab is associated with.</summary>
		/// <returns>The array of components the property tab is associated with.</returns>
		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06004E2B RID: 20011 RVA: 0x0014261C File Offset: 0x0014081C
		// (set) Token: 0x06004E2C RID: 20012 RVA: 0x00142624 File Offset: 0x00140824
		public virtual object[] Components
		{
			get
			{
				return this.components;
			}
			set
			{
				this.components = value;
			}
		}

		/// <summary>Gets the name for the property tab.</summary>
		/// <returns>The name for the property tab.</returns>
		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06004E2D RID: 20013
		public abstract string TabName { get; }

		/// <summary>Gets the Help keyword that is to be associated with this tab.</summary>
		/// <returns>The Help keyword to be associated with this tab.</returns>
		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06004E2E RID: 20014 RVA: 0x0014262D File Offset: 0x0014082D
		public virtual string HelpKeyword
		{
			get
			{
				return this.TabName;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Windows.Forms.Design.PropertyTab" /> can display properties for the specified component.</summary>
		/// <param name="extendee">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if the object can be extended; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E2F RID: 20015 RVA: 0x00012E4E File Offset: 0x0001104E
		public virtual bool CanExtend(object extendee)
		{
			return true;
		}

		/// <summary>Releases all the resources used by the <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</summary>
		// Token: 0x06004E30 RID: 20016 RVA: 0x00142635 File Offset: 0x00140835
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.PropertyTab" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06004E31 RID: 20017 RVA: 0x00142644 File Offset: 0x00140844
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.bitmap != null)
			{
				this.bitmap.Dispose();
				this.bitmap = null;
			}
		}

		/// <summary>Gets the default property of the specified component.</summary>
		/// <param name="component">The component to retrieve the default property of.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property.</returns>
		// Token: 0x06004E32 RID: 20018 RVA: 0x00142663 File Offset: 0x00140863
		public virtual PropertyDescriptor GetDefaultProperty(object component)
		{
			return TypeDescriptor.GetDefaultProperty(component);
		}

		/// <summary>Gets the properties of the specified component.</summary>
		/// <param name="component">The component to retrieve the properties of.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties of the component.</returns>
		// Token: 0x06004E33 RID: 20019 RVA: 0x0014266B File Offset: 0x0014086B
		public virtual PropertyDescriptorCollection GetProperties(object component)
		{
			return this.GetProperties(component, null);
		}

		/// <summary>Gets the properties of the specified component that match the specified attributes.</summary>
		/// <param name="component">The component to retrieve properties from.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the properties to retrieve.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties.</returns>
		// Token: 0x06004E34 RID: 20020
		public abstract PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes);

		/// <summary>Gets the properties of the specified component that match the specified attributes and context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context to retrieve properties from.</param>
		/// <param name="component">The component to retrieve properties from.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the properties to retrieve.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties matching the specified context and attributes.</returns>
		// Token: 0x06004E35 RID: 20021 RVA: 0x00142675 File Offset: 0x00140875
		public virtual PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
		{
			return this.GetProperties(component, attributes);
		}

		// Token: 0x040033EB RID: 13291
		private object[] components;

		// Token: 0x040033EC RID: 13292
		private Bitmap bitmap;

		// Token: 0x040033ED RID: 13293
		private bool checkedBmp;
	}
}
