using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Contains values of properties that a component might need only occasionally.</summary>
	// Token: 0x02000314 RID: 788
	[Serializable]
	public class OwnerDrawPropertyBag : MarshalByRefObject, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> class.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> value.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> value.</param>
		// Token: 0x06003222 RID: 12834 RVA: 0x000E1890 File Offset: 0x000DFA90
		protected OwnerDrawPropertyBag(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "Font")
				{
					this.font = (Font)serializationEntry.Value;
				}
				else if (serializationEntry.Name == "ForeColor")
				{
					this.foreColor = (Color)serializationEntry.Value;
				}
				else if (serializationEntry.Name == "BackColor")
				{
					this.backColor = (Color)serializationEntry.Value;
				}
			}
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000E1947 File Offset: 0x000DFB47
		internal OwnerDrawPropertyBag()
		{
		}

		/// <summary>Gets or sets the font of the text displayed by the component.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the component. The default is <see langword="null" />.</returns>
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x000E1965 File Offset: 0x000DFB65
		// (set) Token: 0x06003225 RID: 12837 RVA: 0x000E196D File Offset: 0x000DFB6D
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the component.</summary>
		/// <returns>The foreground color of the component. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x000E1976 File Offset: 0x000DFB76
		// (set) Token: 0x06003227 RID: 12839 RVA: 0x000E197E File Offset: 0x000DFB7E
		public Color ForeColor
		{
			get
			{
				return this.foreColor;
			}
			set
			{
				this.foreColor = value;
			}
		}

		/// <summary>Gets or sets the background color for the component.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the component. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x000E1987 File Offset: 0x000DFB87
		// (set) Token: 0x06003229 RID: 12841 RVA: 0x000E198F File Offset: 0x000DFB8F
		public Color BackColor
		{
			get
			{
				return this.backColor;
			}
			set
			{
				this.backColor = value;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x000E1998 File Offset: 0x000DFB98
		internal IntPtr FontHandle
		{
			get
			{
				if (this.fontWrapper == null)
				{
					this.fontWrapper = new Control.FontHandleWrapper(this.Font);
				}
				return this.fontWrapper.Handle;
			}
		}

		/// <summary>Returns whether the <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> contains all default values.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> contains all default values; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600322B RID: 12843 RVA: 0x000E19BE File Offset: 0x000DFBBE
		public virtual bool IsEmpty()
		{
			return this.Font == null && this.foreColor.IsEmpty && this.backColor.IsEmpty;
		}

		/// <summary>Copies an <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> to be copied.</param>
		/// <returns>A new copy of the <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> control.</returns>
		// Token: 0x0600322C RID: 12844 RVA: 0x000E19E4 File Offset: 0x000DFBE4
		public static OwnerDrawPropertyBag Copy(OwnerDrawPropertyBag value)
		{
			object obj = OwnerDrawPropertyBag.internalSyncObject;
			OwnerDrawPropertyBag ownerDrawPropertyBag2;
			lock (obj)
			{
				OwnerDrawPropertyBag ownerDrawPropertyBag = new OwnerDrawPropertyBag();
				if (value == null)
				{
					ownerDrawPropertyBag2 = ownerDrawPropertyBag;
				}
				else
				{
					ownerDrawPropertyBag.backColor = value.backColor;
					ownerDrawPropertyBag.foreColor = value.foreColor;
					ownerDrawPropertyBag.Font = value.font;
					ownerDrawPropertyBag2 = ownerDrawPropertyBag;
				}
			}
			return ownerDrawPropertyBag2;
		}

		/// <summary>Populates the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x0600322D RID: 12845 RVA: 0x000E1A54 File Offset: 0x000DFC54
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("BackColor", this.BackColor);
			si.AddValue("ForeColor", this.ForeColor);
			si.AddValue("Font", this.Font);
		}

		// Token: 0x04001E5E RID: 7774
		private Font font;

		// Token: 0x04001E5F RID: 7775
		private Color foreColor = Color.Empty;

		// Token: 0x04001E60 RID: 7776
		private Color backColor = Color.Empty;

		// Token: 0x04001E61 RID: 7777
		private Control.FontHandleWrapper fontWrapper;

		// Token: 0x04001E62 RID: 7778
		private static object internalSyncObject = new object();
	}
}
