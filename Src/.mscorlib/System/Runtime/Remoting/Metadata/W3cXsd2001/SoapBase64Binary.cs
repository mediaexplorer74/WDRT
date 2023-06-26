using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="base64Binary" /> type.</summary>
	// Token: 0x020007E6 RID: 2022
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapBase64Binary : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06005792 RID: 22418 RVA: 0x00137708 File Offset: 0x00135908
		public static string XsdType
		{
			get
			{
				return "base64Binary";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005793 RID: 22419 RVA: 0x0013770F File Offset: 0x0013590F
		public string GetXsdType()
		{
			return SoapBase64Binary.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> class.</summary>
		// Token: 0x06005794 RID: 22420 RVA: 0x00137716 File Offset: 0x00135916
		public SoapBase64Binary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> class with the binary representation of a 64-bit number.</summary>
		/// <param name="value">A <see cref="T:System.Byte" /> array that contains a 64-bit number.</param>
		// Token: 0x06005795 RID: 22421 RVA: 0x0013771E File Offset: 0x0013591E
		public SoapBase64Binary(byte[] value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the binary representation of a 64-bit number.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the binary representation of a 64-bit number.</returns>
		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06005796 RID: 22422 RVA: 0x0013772D File Offset: 0x0013592D
		// (set) Token: 0x06005797 RID: 22423 RVA: 0x00137735 File Offset: 0x00135935
		public byte[] Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary.Value" />.</returns>
		// Token: 0x06005798 RID: 22424 RVA: 0x0013773E File Offset: 0x0013593E
		public override string ToString()
		{
			if (this._value == null)
			{
				return null;
			}
			return SoapType.LineFeedsBin64(Convert.ToBase64String(this._value));
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> object that is obtained from <paramref name="value" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following:  
		///
		/// <paramref name="value" /> is <see langword="null" />.  
		///
		/// The length of <paramref name="value" /> is less than 4.  
		///
		/// The length of <paramref name="value" /> is not a multiple of 4.</exception>
		// Token: 0x06005799 RID: 22425 RVA: 0x0013775C File Offset: 0x0013595C
		public static SoapBase64Binary Parse(string value)
		{
			if (value == null || value.Length == 0)
			{
				return new SoapBase64Binary(new byte[0]);
			}
			byte[] array;
			try
			{
				array = Convert.FromBase64String(SoapType.FilterBin64(value));
			}
			catch (Exception)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "base64Binary", value));
			}
			return new SoapBase64Binary(array);
		}

		// Token: 0x04002821 RID: 10273
		private byte[] _value;
	}
}
