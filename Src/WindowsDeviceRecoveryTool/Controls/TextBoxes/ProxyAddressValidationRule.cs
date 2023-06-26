using System;
using System.Globalization;
using System.Windows.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls.TextBoxes
{
	// Token: 0x020000DE RID: 222
	public class ProxyAddressValidationRule : ValidationRule
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x000201D0 File Offset: 0x0001E3D0
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string text = value as string;
			bool flag = text != null;
			ValidationResult validationResult;
			if (flag)
			{
				bool flag2 = text.Length > 0 && !ProxyAddressValidationRule.IsStringValidUri(text);
				if (flag2)
				{
					validationResult = new ValidationResult(false, null);
				}
				else
				{
					validationResult = new ValidationResult(true, null);
				}
			}
			else
			{
				validationResult = new ValidationResult(false, null);
			}
			return validationResult;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00020228 File Offset: 0x0001E428
		private static bool IsStringValidUri(string str)
		{
			return Uri.IsWellFormedUriString(Uri.EscapeUriString(str), UriKind.RelativeOrAbsolute);
		}
	}
}
