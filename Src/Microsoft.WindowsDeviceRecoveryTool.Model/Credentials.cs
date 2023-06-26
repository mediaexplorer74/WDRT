using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.WindowsDeviceRecoveryTool.Common;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x0200000D RID: 13
	public class Credentials : NotificationObject
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00002C88 File Offset: 0x00000E88
		public Credentials()
		{
			this.InitializeEntropy();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002C9C File Offset: 0x00000E9C
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				base.SetValue<string>(() => this.UserName, ref this.userName, value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002CF4 File Offset: 0x00000EF4
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002D0C File Offset: 0x00000F0C
		public bool AccountBlocked
		{
			get
			{
				return this.accountBlocked;
			}
			set
			{
				base.SetValue<bool>(() => this.AccountBlocked, ref this.accountBlocked, value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002D4C File Offset: 0x00000F4C
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002D64 File Offset: 0x00000F64
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				base.SetValue<string>(() => this.Password, ref this.password, value);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public string EncryptString(string input)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(input);
			byte[] array = ProtectedData.Protect(bytes, this.entropy, DataProtectionScope.CurrentUser);
			return Convert.ToBase64String(array);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public string DecryptString(string input)
		{
			string text;
			try
			{
				bool flag = !string.IsNullOrEmpty(input);
				if (flag)
				{
					byte[] array = ProtectedData.Unprotect(Convert.FromBase64String(input), this.entropy, DataProtectionScope.CurrentUser);
					text = Encoding.Unicode.GetString(array);
				}
				else
				{
					text = string.Empty;
				}
			}
			catch
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002E3C File Offset: 0x0000103C
		public string ToInsecureString(SecureString input)
		{
			IntPtr intPtr = Marshal.SecureStringToBSTR(input);
			string text;
			try
			{
				text = Marshal.PtrToStringBSTR(intPtr);
			}
			finally
			{
				Marshal.ZeroFreeBSTR(intPtr);
			}
			return text;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002E7C File Offset: 0x0000107C
		private void InitializeEntropy()
		{
			this.entropy = Encoding.Unicode.GetBytes("Windows Phone Recovery Tool, salt and papper entropy");
		}

		// Token: 0x04000037 RID: 55
		private string userName;

		// Token: 0x04000038 RID: 56
		private string password;

		// Token: 0x04000039 RID: 57
		private bool accountBlocked;

		// Token: 0x0400003A RID: 58
		private byte[] entropy;
	}
}
