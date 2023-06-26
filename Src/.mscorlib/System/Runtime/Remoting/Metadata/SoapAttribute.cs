using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Provides default functionality for all SOAP attributes.</summary>
	// Token: 0x020007D9 RID: 2009
	[ComVisible(true)]
	public class SoapAttribute : Attribute
	{
		// Token: 0x06005729 RID: 22313 RVA: 0x00136443 File Offset: 0x00134643
		internal void SetReflectInfo(object info)
		{
			this.ReflectInfo = info;
		}

		/// <summary>Gets or sets the XML namespace name.</summary>
		/// <returns>The XML namespace name under which the target of the current attribute is serialized.</returns>
		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x0600572A RID: 22314 RVA: 0x0013644C File Offset: 0x0013464C
		// (set) Token: 0x0600572B RID: 22315 RVA: 0x00136454 File Offset: 0x00134654
		public virtual string XmlNamespace
		{
			get
			{
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the target of the current attribute will be serialized as an XML attribute instead of an XML field.</summary>
		/// <returns>
		///   <see langword="true" /> if the target object of the current attribute must be serialized as an XML attribute; <see langword="false" /> if the target object must be serialized as a subelement.</returns>
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x0600572C RID: 22316 RVA: 0x0013645D File Offset: 0x0013465D
		// (set) Token: 0x0600572D RID: 22317 RVA: 0x00136465 File Offset: 0x00134665
		public virtual bool UseAttribute
		{
			get
			{
				return this._bUseAttribute;
			}
			set
			{
				this._bUseAttribute = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type must be nested during SOAP serialization.</summary>
		/// <returns>
		///   <see langword="true" /> if the target object must be nested during SOAP serialization; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x0600572E RID: 22318 RVA: 0x0013646E File Offset: 0x0013466E
		// (set) Token: 0x0600572F RID: 22319 RVA: 0x00136476 File Offset: 0x00134676
		public virtual bool Embedded
		{
			get
			{
				return this._bEmbedded;
			}
			set
			{
				this._bEmbedded = value;
			}
		}

		/// <summary>The XML namespace to which the target of the current SOAP attribute is serialized.</summary>
		// Token: 0x040027EB RID: 10219
		protected string ProtXmlNamespace;

		// Token: 0x040027EC RID: 10220
		private bool _bUseAttribute;

		// Token: 0x040027ED RID: 10221
		private bool _bEmbedded;

		/// <summary>A reflection object used by attribute classes derived from the <see cref="T:System.Runtime.Remoting.Metadata.SoapAttribute" /> class to set XML serialization information.</summary>
		// Token: 0x040027EE RID: 10222
		protected object ReflectInfo;
	}
}
