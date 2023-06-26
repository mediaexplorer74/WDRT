using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Provides static methods for the serialization and deserialization of <see cref="T:System.DateTime" /> to a string that is formatted as XSD <see langword="dateTime" />.</summary>
	// Token: 0x020007DC RID: 2012
	[ComVisible(true)]
	public sealed class SoapDateTime
	{
		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06005736 RID: 22326 RVA: 0x00136877 File Offset: 0x00134A77
		public static string XsdType
		{
			get
			{
				return "dateTime";
			}
		}

		/// <summary>Returns the specified <see cref="T:System.DateTime" /> object as a <see cref="T:System.String" />.</summary>
		/// <param name="value">The <see cref="T:System.DateTime" /> object to convert.</param>
		/// <returns>A <see cref="T:System.String" /> representation of <paramref name="value" /> in the format "yyyy-MM-dd'T'HH:mm:ss.fffffffzzz".</returns>
		// Token: 0x06005737 RID: 22327 RVA: 0x0013687E File Offset: 0x00134A7E
		public static string ToString(DateTime value)
		{
			return value.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.DateTime" /> object obtained from <paramref name="value" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following:  
		///
		/// <paramref name="value" /> is an empty string.  
		///
		/// <paramref name="value" /> is <see langword="null" /> reference.  
		///
		/// <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns.</exception>
		// Token: 0x06005738 RID: 22328 RVA: 0x00136894 File Offset: 0x00134A94
		public static DateTime Parse(string value)
		{
			DateTime dateTime;
			try
			{
				if (value == null)
				{
					dateTime = DateTime.MinValue;
				}
				else
				{
					string text = value;
					if (value.EndsWith("Z", StringComparison.Ordinal))
					{
						text = value.Substring(0, value.Length - 1) + "-00:00";
					}
					dateTime = DateTime.ParseExact(text, SoapDateTime.formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
				}
			}
			catch (Exception)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), "xsd:dateTime", value));
			}
			return dateTime;
		}

		// Token: 0x0400280D RID: 10253
		private static string[] formats = new string[]
		{
			"yyyy-MM-dd'T'HH:mm:ss.fffffffzzz", "yyyy-MM-dd'T'HH:mm:ss.ffff", "yyyy-MM-dd'T'HH:mm:ss.ffffzzz", "yyyy-MM-dd'T'HH:mm:ss.fff", "yyyy-MM-dd'T'HH:mm:ss.fffzzz", "yyyy-MM-dd'T'HH:mm:ss.ff", "yyyy-MM-dd'T'HH:mm:ss.ffzzz", "yyyy-MM-dd'T'HH:mm:ss.f", "yyyy-MM-dd'T'HH:mm:ss.fzzz", "yyyy-MM-dd'T'HH:mm:ss",
			"yyyy-MM-dd'T'HH:mm:sszzz", "yyyy-MM-dd'T'HH:mm:ss.fffff", "yyyy-MM-dd'T'HH:mm:ss.fffffzzz", "yyyy-MM-dd'T'HH:mm:ss.ffffff", "yyyy-MM-dd'T'HH:mm:ss.ffffffzzz", "yyyy-MM-dd'T'HH:mm:ss.fffffff", "yyyy-MM-dd'T'HH:mm:ss.ffffffff", "yyyy-MM-dd'T'HH:mm:ss.ffffffffzzz", "yyyy-MM-dd'T'HH:mm:ss.fffffffff", "yyyy-MM-dd'T'HH:mm:ss.fffffffffzzz",
			"yyyy-MM-dd'T'HH:mm:ss.ffffffffff", "yyyy-MM-dd'T'HH:mm:ss.ffffffffffzzz"
		};
	}
}
