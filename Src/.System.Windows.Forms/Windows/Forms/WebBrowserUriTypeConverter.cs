using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x0200043F RID: 1087
	internal class WebBrowserUriTypeConverter : UriTypeConverter
	{
		// Token: 0x06004BAA RID: 19370 RVA: 0x0013ABA0 File Offset: 0x00138DA0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			Uri uri = base.ConvertFrom(context, culture, value) as Uri;
			if (uri != null && !string.IsNullOrEmpty(uri.OriginalString) && !uri.IsAbsoluteUri)
			{
				try
				{
					uri = new Uri("http://" + uri.OriginalString.Trim());
				}
				catch (UriFormatException)
				{
				}
			}
			return uri;
		}
	}
}
