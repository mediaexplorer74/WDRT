using System;
using System.Globalization;
using System.IO;
using System.Windows.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls.TextBoxes.Validation
{
	// Token: 0x020000DF RID: 223
	public class ExistingFolderValidationRule : ValidationRule
	{
		// Token: 0x0600071A RID: 1818 RVA: 0x00020250 File Offset: 0x0001E450
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string text = value as string;
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				bool flag2 = Directory.Exists(text) && ExistingFolderValidationRule.IsValidPath(text);
				if (flag2)
				{
					return new ValidationResult(true, null);
				}
			}
			return new ValidationResult(false, null);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000202A0 File Offset: 0x0001E4A0
		private static bool IsValidPath(string path)
		{
			return !path.Contains(" \\") && !path.Contains("\\\\") && !path.Contains("/") && !path.StartsWith(" ", StringComparison.Ordinal) && !path.StartsWith(".", StringComparison.Ordinal) && !path.StartsWith("\\", StringComparison.Ordinal) && !path.EndsWith(" ", StringComparison.Ordinal) && !path.EndsWith(".", StringComparison.Ordinal);
		}
	}
}
