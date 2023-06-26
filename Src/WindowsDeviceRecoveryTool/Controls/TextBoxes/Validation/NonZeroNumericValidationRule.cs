using System;
using System.Globalization;
using System.Windows.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls.TextBoxes.Validation
{
	// Token: 0x020000E0 RID: 224
	public class NonZeroNumericValidationRule : ValidationRule
	{
		// Token: 0x0600071D RID: 1821 RVA: 0x00020324 File Offset: 0x0001E524
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string text = value as string;
			bool flag = !string.IsNullOrEmpty(text) && !text.StartsWith("0", StringComparison.Ordinal);
			if (flag)
			{
				uint num;
				bool flag2 = uint.TryParse(text, out num);
				if (flag2)
				{
					bool flag3 = num > 0U;
					if (flag3)
					{
						return new ValidationResult(true, null);
					}
				}
			}
			return new ValidationResult(false, null);
		}
	}
}
