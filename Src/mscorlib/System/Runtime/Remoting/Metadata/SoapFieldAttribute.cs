using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for a field. This class cannot be inherited.</summary>
	// Token: 0x020007D7 RID: 2007
	[AttributeUsage(AttributeTargets.Field)]
	[ComVisible(true)]
	public sealed class SoapFieldAttribute : SoapAttribute
	{
		/// <summary>Returns a value indicating whether the current attribute contains interop XML element values.</summary>
		/// <returns>
		///   <see langword="true" /> if the current attribute contains interop XML element values; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005722 RID: 22306 RVA: 0x001363D0 File Offset: 0x001345D0
		public bool IsInteropXmlElement()
		{
			return (this._explicitlySet & SoapFieldAttribute.ExplicitlySet.XmlElementName) > SoapFieldAttribute.ExplicitlySet.None;
		}

		/// <summary>Gets or sets the XML element name of the field contained in the <see cref="T:System.Runtime.Remoting.Metadata.SoapFieldAttribute" /> attribute.</summary>
		/// <returns>The XML element name of the field contained in this attribute.</returns>
		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06005723 RID: 22307 RVA: 0x001363DD File Offset: 0x001345DD
		// (set) Token: 0x06005724 RID: 22308 RVA: 0x0013640B File Offset: 0x0013460B
		public string XmlElementName
		{
			get
			{
				if (this._xmlElementName == null && this.ReflectInfo != null)
				{
					this._xmlElementName = ((FieldInfo)this.ReflectInfo).Name;
				}
				return this._xmlElementName;
			}
			set
			{
				this._xmlElementName = value;
				this._explicitlySet |= SoapFieldAttribute.ExplicitlySet.XmlElementName;
			}
		}

		/// <summary>Gets or sets the order of the current field attribute.</summary>
		/// <returns>The order of the current field attribute.</returns>
		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06005725 RID: 22309 RVA: 0x00136422 File Offset: 0x00134622
		// (set) Token: 0x06005726 RID: 22310 RVA: 0x0013642A File Offset: 0x0013462A
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				this._order = value;
			}
		}

		// Token: 0x040027E8 RID: 10216
		private SoapFieldAttribute.ExplicitlySet _explicitlySet;

		// Token: 0x040027E9 RID: 10217
		private string _xmlElementName;

		// Token: 0x040027EA RID: 10218
		private int _order;

		// Token: 0x02000C6B RID: 3179
		[Flags]
		[Serializable]
		private enum ExplicitlySet
		{
			// Token: 0x040037F1 RID: 14321
			None = 0,
			// Token: 0x040037F2 RID: 14322
			XmlElementName = 1
		}
	}
}
