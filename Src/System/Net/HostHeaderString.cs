using System;
using System.Text;

namespace System.Net
{
	// Token: 0x02000184 RID: 388
	internal class HostHeaderString
	{
		// Token: 0x06000E62 RID: 3682 RVA: 0x0004B642 File Offset: 0x00049842
		internal HostHeaderString()
		{
			this.Init(null);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0004B651 File Offset: 0x00049851
		internal HostHeaderString(string s)
		{
			this.Init(s);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0004B660 File Offset: 0x00049860
		private void Init(string s)
		{
			this.m_String = s;
			this.m_Converted = false;
			this.m_Bytes = null;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0004B678 File Offset: 0x00049878
		private void Convert()
		{
			if (this.m_String != null && !this.m_Converted)
			{
				this.m_Bytes = Encoding.Default.GetBytes(this.m_String);
				string @string = Encoding.Default.GetString(this.m_Bytes);
				if (string.Compare(this.m_String, @string, StringComparison.Ordinal) != 0)
				{
					this.m_Bytes = Encoding.UTF8.GetBytes(this.m_String);
				}
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0004B6E1 File Offset: 0x000498E1
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x0004B6E9 File Offset: 0x000498E9
		internal string String
		{
			get
			{
				return this.m_String;
			}
			set
			{
				this.Init(value);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0004B6F2 File Offset: 0x000498F2
		internal int ByteCount
		{
			get
			{
				this.Convert();
				return this.m_Bytes.Length;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0004B702 File Offset: 0x00049902
		internal byte[] Bytes
		{
			get
			{
				this.Convert();
				return this.m_Bytes;
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0004B710 File Offset: 0x00049910
		internal void Copy(byte[] destBytes, int destByteIndex)
		{
			this.Convert();
			Array.Copy(this.m_Bytes, 0, destBytes, destByteIndex, this.m_Bytes.Length);
		}

		// Token: 0x0400125E RID: 4702
		private bool m_Converted;

		// Token: 0x0400125F RID: 4703
		private string m_String;

		// Token: 0x04001260 RID: 4704
		private byte[] m_Bytes;
	}
}
