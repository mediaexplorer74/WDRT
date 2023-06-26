using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="hexBinary" /> type.</summary>
	// Token: 0x020007E5 RID: 2021
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapHexBinary : ISoapXsd
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the XSD of the current SOAP type.</returns>
		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06005788 RID: 22408 RVA: 0x0013754C File Offset: 0x0013574C
		public static string XsdType
		{
			get
			{
				return "hexBinary";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06005789 RID: 22409 RVA: 0x00137553 File Offset: 0x00135753
		public string GetXsdType()
		{
			return SoapHexBinary.XsdType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> class.</summary>
		// Token: 0x0600578A RID: 22410 RVA: 0x0013755A File Offset: 0x0013575A
		public SoapHexBinary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> class.</summary>
		/// <param name="value">A <see cref="T:System.Byte" /> array that contains a hexadecimal number.</param>
		// Token: 0x0600578B RID: 22411 RVA: 0x0013756F File Offset: 0x0013576F
		public SoapHexBinary(byte[] value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the hexadecimal representation of a number.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the hexadecimal representation of a number.</returns>
		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x0600578C RID: 22412 RVA: 0x0013758B File Offset: 0x0013578B
		// (set) Token: 0x0600578D RID: 22413 RVA: 0x00137593 File Offset: 0x00135793
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

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary.Value" />.</returns>
		// Token: 0x0600578E RID: 22414 RVA: 0x0013759C File Offset: 0x0013579C
		public override string ToString()
		{
			this.sb.Length = 0;
			for (int i = 0; i < this._value.Length; i++)
			{
				string text = this._value[i].ToString("X", CultureInfo.InvariantCulture);
				if (text.Length == 1)
				{
					this.sb.Append('0');
				}
				this.sb.Append(text);
			}
			return this.sb.ToString();
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x0600578F RID: 22415 RVA: 0x00137613 File Offset: 0x00135813
		public static SoapHexBinary Parse(string value)
		{
			return new SoapHexBinary(SoapHexBinary.ToByteArray(SoapType.FilterBin64(value)));
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x00137628 File Offset: 0x00135828
		private static byte[] ToByteArray(string value)
		{
			char[] array = value.ToCharArray();
			if (array.Length % 2 != 0)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:hexBinary", value));
			}
			byte[] array2 = new byte[array.Length / 2];
			for (int i = 0; i < array.Length / 2; i++)
			{
				array2[i] = SoapHexBinary.ToByte(array[i * 2], value) * 16 + SoapHexBinary.ToByte(array[i * 2 + 1], value);
			}
			return array2;
		}

		// Token: 0x06005791 RID: 22417 RVA: 0x001376A0 File Offset: 0x001358A0
		private static byte ToByte(char c, string value)
		{
			byte b = 0;
			string text = c.ToString();
			try
			{
				text = c.ToString();
				b = byte.Parse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			catch (Exception)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid", new object[] { "xsd:hexBinary", value }));
			}
			return b;
		}

		// Token: 0x0400281F RID: 10271
		private byte[] _value;

		// Token: 0x04002820 RID: 10272
		private StringBuilder sb = new StringBuilder(100);
	}
}
