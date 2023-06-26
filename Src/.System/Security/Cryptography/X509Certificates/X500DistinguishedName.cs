using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents the distinguished name of an X509 certificate. This class cannot be inherited.</summary>
	// Token: 0x02000462 RID: 1122
	public sealed class X500DistinguishedName : AsnEncodedData
	{
		// Token: 0x06002993 RID: 10643 RVA: 0x000BC833 File Offset: 0x000BAA33
		internal X500DistinguishedName(CAPIBase.CRYPTOAPI_BLOB encodedDistinguishedNameBlob)
			: base(new Oid(), encodedDistinguishedNameBlob)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using information from the specified byte array.</summary>
		/// <param name="encodedDistinguishedName">A byte array that contains distinguished name information.</param>
		// Token: 0x06002994 RID: 10644 RVA: 0x000BC841 File Offset: 0x000BAA41
		public X500DistinguishedName(byte[] encodedDistinguishedName)
			: base(new Oid(), encodedDistinguishedName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="encodedDistinguishedName">An <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object that represents the distinguished name.</param>
		// Token: 0x06002995 RID: 10645 RVA: 0x000BC84F File Offset: 0x000BAA4F
		public X500DistinguishedName(AsnEncodedData encodedDistinguishedName)
			: base(encodedDistinguishedName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object.</summary>
		/// <param name="distinguishedName">An <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object.</param>
		// Token: 0x06002996 RID: 10646 RVA: 0x000BC858 File Offset: 0x000BAA58
		public X500DistinguishedName(X500DistinguishedName distinguishedName)
			: base(distinguishedName)
		{
			this.m_distinguishedName = distinguishedName.Name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using information from the specified string.</summary>
		/// <param name="distinguishedName">A string that represents the distinguished name.</param>
		// Token: 0x06002997 RID: 10647 RVA: 0x000BC86D File Offset: 0x000BAA6D
		public X500DistinguishedName(string distinguishedName)
			: this(distinguishedName, X500DistinguishedNameFlags.Reversed)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using the specified string and <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedNameFlags" /> flag.</summary>
		/// <param name="distinguishedName">A string that represents the distinguished name.</param>
		/// <param name="flag">A bitwise combination of the enumeration values that specify the characteristics of the distinguished name.</param>
		// Token: 0x06002998 RID: 10648 RVA: 0x000BC877 File Offset: 0x000BAA77
		public X500DistinguishedName(string distinguishedName, X500DistinguishedNameFlags flag)
			: base(new Oid(), X500DistinguishedName.Encode(distinguishedName, flag))
		{
			this.m_distinguishedName = distinguishedName;
		}

		/// <summary>Gets the comma-delimited distinguished name from an X500 certificate.</summary>
		/// <returns>The comma-delimited distinguished name of the X509 certificate.</returns>
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002999 RID: 10649 RVA: 0x000BC892 File Offset: 0x000BAA92
		public string Name
		{
			get
			{
				if (this.m_distinguishedName == null)
				{
					this.m_distinguishedName = this.Decode(X500DistinguishedNameFlags.Reversed);
				}
				return this.m_distinguishedName;
			}
		}

		/// <summary>Decodes a distinguished name using the characteristics specified by the <paramref name="flag" /> parameter.</summary>
		/// <param name="flag">A bitwise combination of the enumeration values that specify the characteristics of the distinguished name.</param>
		/// <returns>The decoded distinguished name.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate has an invalid name.</exception>
		// Token: 0x0600299A RID: 10650 RVA: 0x000BC8B0 File Offset: 0x000BAAB0
		public unsafe string Decode(X500DistinguishedNameFlags flag)
		{
			uint num = 3U | X500DistinguishedName.MapNameToStrFlag(flag);
			byte[] rawData = this.m_rawData;
			byte[] array;
			byte* ptr;
			if ((array = rawData) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB;
			IntPtr intPtr = new IntPtr((void*)(&cryptoapi_BLOB));
			cryptoapi_BLOB.cbData = (uint)rawData.Length;
			cryptoapi_BLOB.pbData = new IntPtr((void*)ptr);
			uint num2 = CAPISafe.CertNameToStrW(65537U, intPtr, num, SafeLocalAllocHandle.InvalidHandle, 0U);
			if (num2 == 0U)
			{
				throw new CryptographicException(-2146762476);
			}
			string text;
			using (SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)((ulong)(2U * num2)))))
			{
				if (CAPISafe.CertNameToStrW(65537U, intPtr, num, safeLocalAllocHandle, num2) == 0U)
				{
					throw new CryptographicException(-2146762476);
				}
				text = Marshal.PtrToStringUni(safeLocalAllocHandle.DangerousGetHandle());
			}
			return text;
		}

		/// <summary>Returns a formatted version of an X500 distinguished name for printing or for output to a text window or to a console.</summary>
		/// <param name="multiLine">
		///   <see langword="true" /> if the return string should contain carriage returns; otherwise, <see langword="false" />.</param>
		/// <returns>A formatted string that represents the X500 distinguished name.</returns>
		// Token: 0x0600299B RID: 10651 RVA: 0x000BC98C File Offset: 0x000BAB8C
		public override string Format(bool multiLine)
		{
			if (this.m_rawData == null || this.m_rawData.Length == 0)
			{
				return string.Empty;
			}
			return CAPI.CryptFormatObject(1U, multiLine ? 1U : 0U, new IntPtr(7L), this.m_rawData);
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000BC9C0 File Offset: 0x000BABC0
		private unsafe static byte[] Encode(string distinguishedName, X500DistinguishedNameFlags flag)
		{
			if (distinguishedName == null)
			{
				throw new ArgumentNullException("distinguishedName");
			}
			uint num = 0U;
			uint num2 = 3U | X500DistinguishedName.MapNameToStrFlag(flag);
			if (!CAPISafe.CertStrToNameW(65537U, distinguishedName, num2, IntPtr.Zero, IntPtr.Zero, ref num, IntPtr.Zero))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			byte[] array = new byte[num];
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			if (!CAPISafe.CertStrToNameW(65537U, distinguishedName, num2, IntPtr.Zero, new IntPtr((void*)ptr), ref num, IntPtr.Zero))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			array2 = null;
			return array;
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000BCA64 File Offset: 0x000BAC64
		private static uint MapNameToStrFlag(X500DistinguishedNameFlags flag)
		{
			uint num = 29169U;
			if ((flag & (X500DistinguishedNameFlags)(~(X500DistinguishedNameFlags)num)) != X500DistinguishedNameFlags.None)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[] { "flag" }));
			}
			uint num2 = 0U;
			if (flag != X500DistinguishedNameFlags.None)
			{
				if ((flag & X500DistinguishedNameFlags.Reversed) == X500DistinguishedNameFlags.Reversed)
				{
					num2 |= 33554432U;
				}
				if ((flag & X500DistinguishedNameFlags.UseSemicolons) == X500DistinguishedNameFlags.UseSemicolons)
				{
					num2 |= 1073741824U;
				}
				else if ((flag & X500DistinguishedNameFlags.UseCommas) == X500DistinguishedNameFlags.UseCommas)
				{
					num2 |= 67108864U;
				}
				else if ((flag & X500DistinguishedNameFlags.UseNewLines) == X500DistinguishedNameFlags.UseNewLines)
				{
					num2 |= 134217728U;
				}
				if ((flag & X500DistinguishedNameFlags.DoNotUsePlusSign) == X500DistinguishedNameFlags.DoNotUsePlusSign)
				{
					num2 |= 536870912U;
				}
				if ((flag & X500DistinguishedNameFlags.DoNotUseQuotes) == X500DistinguishedNameFlags.DoNotUseQuotes)
				{
					num2 |= 268435456U;
				}
				if ((flag & X500DistinguishedNameFlags.ForceUTF8Encoding) == X500DistinguishedNameFlags.ForceUTF8Encoding)
				{
					num2 |= 524288U;
				}
				if ((flag & X500DistinguishedNameFlags.UseUTF8Encoding) == X500DistinguishedNameFlags.UseUTF8Encoding)
				{
					num2 |= 262144U;
				}
				else if ((flag & X500DistinguishedNameFlags.UseT61Encoding) == X500DistinguishedNameFlags.UseT61Encoding)
				{
					num2 |= 131072U;
				}
			}
			return num2;
		}

		// Token: 0x04002594 RID: 9620
		private string m_distinguishedName;
	}
}
