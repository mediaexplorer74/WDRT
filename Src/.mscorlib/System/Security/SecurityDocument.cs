﻿using System;
using System.Collections;
using System.Security.Util;
using System.Text;

namespace System.Security
{
	// Token: 0x020001BF RID: 447
	[Serializable]
	internal sealed class SecurityDocument
	{
		// Token: 0x06001C01 RID: 7169 RVA: 0x0006075D File Offset: 0x0005E95D
		public SecurityDocument(int numData)
		{
			this.m_data = new byte[numData];
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00060771 File Offset: 0x0005E971
		public SecurityDocument(byte[] data)
		{
			this.m_data = data;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00060780 File Offset: 0x0005E980
		public SecurityDocument(SecurityElement elRoot)
		{
			this.m_data = new byte[32];
			int num = 0;
			this.ConvertElement(elRoot, ref num);
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000607AC File Offset: 0x0005E9AC
		public void GuaranteeSize(int size)
		{
			if (this.m_data.Length < size)
			{
				byte[] array = new byte[(size / 32 + 1) * 32];
				Array.Copy(this.m_data, 0, array, 0, this.m_data.Length);
				this.m_data = array;
			}
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x000607F0 File Offset: 0x0005E9F0
		public void AddString(string str, ref int position)
		{
			this.GuaranteeSize(position + str.Length * 2 + 2);
			for (int i = 0; i < str.Length; i++)
			{
				this.m_data[position + 2 * i] = (byte)(str[i] >> 8);
				this.m_data[position + 2 * i + 1] = (byte)(str[i] & 'ÿ');
			}
			this.m_data[position + str.Length * 2] = 0;
			this.m_data[position + str.Length * 2 + 1] = 0;
			position += str.Length * 2 + 2;
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x0006088C File Offset: 0x0005EA8C
		public void AppendString(string str, ref int position)
		{
			if (position <= 1 || this.m_data[position - 1] != 0 || this.m_data[position - 2] != 0)
			{
				throw new XmlSyntaxException();
			}
			position -= 2;
			this.AddString(str, ref position);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000608C1 File Offset: 0x0005EAC1
		public static int EncodedStringSize(string str)
		{
			return str.Length * 2 + 2;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x000608CD File Offset: 0x0005EACD
		public string GetString(ref int position)
		{
			return this.GetString(ref position, true);
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x000608D8 File Offset: 0x0005EAD8
		public string GetString(ref int position, bool bCreate)
		{
			int i;
			for (i = position; i < this.m_data.Length - 1; i += 2)
			{
				if (this.m_data[i] == 0 && this.m_data[i + 1] == 0)
				{
					break;
				}
			}
			Tokenizer.StringMaker sharedStringMaker = SharedStatics.GetSharedStringMaker();
			string text;
			try
			{
				if (bCreate)
				{
					sharedStringMaker._outStringBuilder = null;
					sharedStringMaker._outIndex = 0;
					for (int j = position; j < i; j += 2)
					{
						char c = (char)(((int)this.m_data[j] << 8) | (int)this.m_data[j + 1]);
						if (sharedStringMaker._outIndex < 512)
						{
							char[] outChars = sharedStringMaker._outChars;
							Tokenizer.StringMaker stringMaker = sharedStringMaker;
							int outIndex = stringMaker._outIndex;
							stringMaker._outIndex = outIndex + 1;
							outChars[outIndex] = c;
						}
						else
						{
							if (sharedStringMaker._outStringBuilder == null)
							{
								sharedStringMaker._outStringBuilder = new StringBuilder();
							}
							sharedStringMaker._outStringBuilder.Append(sharedStringMaker._outChars, 0, 512);
							sharedStringMaker._outChars[0] = c;
							sharedStringMaker._outIndex = 1;
						}
					}
				}
				position = i + 2;
				if (bCreate)
				{
					text = sharedStringMaker.MakeString();
				}
				else
				{
					text = null;
				}
			}
			finally
			{
				SharedStatics.ReleaseSharedStringMaker(ref sharedStringMaker);
			}
			return text;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000609F8 File Offset: 0x0005EBF8
		public void AddToken(byte b, ref int position)
		{
			this.GuaranteeSize(position + 1);
			byte[] data = this.m_data;
			int num = position;
			position = num + 1;
			data[num] = b;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00060A20 File Offset: 0x0005EC20
		public void ConvertElement(SecurityElement elCurrent, ref int position)
		{
			this.AddToken(1, ref position);
			this.AddString(elCurrent.m_strTag, ref position);
			if (elCurrent.m_lAttributes != null)
			{
				for (int i = 0; i < elCurrent.m_lAttributes.Count; i += 2)
				{
					this.AddToken(2, ref position);
					this.AddString((string)elCurrent.m_lAttributes[i], ref position);
					this.AddString((string)elCurrent.m_lAttributes[i + 1], ref position);
				}
			}
			if (elCurrent.m_strText != null)
			{
				this.AddToken(3, ref position);
				this.AddString(elCurrent.m_strText, ref position);
			}
			if (elCurrent.InternalChildren != null)
			{
				for (int j = 0; j < elCurrent.InternalChildren.Count; j++)
				{
					this.ConvertElement((SecurityElement)elCurrent.Children[j], ref position);
				}
			}
			this.AddToken(4, ref position);
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00060AF5 File Offset: 0x0005ECF5
		public SecurityElement GetRootElement()
		{
			return this.GetElement(0, true);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00060B00 File Offset: 0x0005ED00
		public SecurityElement GetElement(int position, bool bCreate)
		{
			return this.InternalGetElement(ref position, bCreate);
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00060B18 File Offset: 0x0005ED18
		internal SecurityElement InternalGetElement(ref int position, bool bCreate)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			byte[] data = this.m_data;
			int num = position;
			position = num + 1;
			if (data[num] != 1)
			{
				throw new XmlSyntaxException();
			}
			SecurityElement securityElement = null;
			string @string = this.GetString(ref position, bCreate);
			if (bCreate)
			{
				securityElement = new SecurityElement(@string);
			}
			while (this.m_data[position] == 2)
			{
				position++;
				string string2 = this.GetString(ref position, bCreate);
				string string3 = this.GetString(ref position, bCreate);
				if (bCreate)
				{
					securityElement.AddAttribute(string2, string3);
				}
			}
			if (this.m_data[position] == 3)
			{
				position++;
				string string4 = this.GetString(ref position, bCreate);
				if (bCreate)
				{
					securityElement.m_strText = string4;
				}
			}
			while (this.m_data[position] != 4)
			{
				SecurityElement securityElement2 = this.InternalGetElement(ref position, bCreate);
				if (bCreate)
				{
					securityElement.AddChild(securityElement2);
				}
			}
			position++;
			return securityElement;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00060BEC File Offset: 0x0005EDEC
		public string GetTagForElement(int position)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			if (this.m_data[position++] != 1)
			{
				throw new XmlSyntaxException();
			}
			return this.GetString(ref position);
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00060C2C File Offset: 0x0005EE2C
		public ArrayList GetChildrenPositionForElement(int position)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			if (this.m_data[position++] != 1)
			{
				throw new XmlSyntaxException();
			}
			ArrayList arrayList = new ArrayList();
			this.GetString(ref position);
			while (this.m_data[position] == 2)
			{
				position++;
				this.GetString(ref position, false);
				this.GetString(ref position, false);
			}
			if (this.m_data[position] == 3)
			{
				position++;
				this.GetString(ref position, false);
			}
			while (this.m_data[position] != 4)
			{
				arrayList.Add(position);
				this.InternalGetElement(ref position, false);
			}
			position++;
			return arrayList;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00060CDC File Offset: 0x0005EEDC
		public string GetAttributeForElement(int position, string attributeName)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			if (this.m_data[position++] != 1)
			{
				throw new XmlSyntaxException();
			}
			string text = null;
			this.GetString(ref position, false);
			while (this.m_data[position] == 2)
			{
				position++;
				string @string = this.GetString(ref position);
				string string2 = this.GetString(ref position);
				if (string.Equals(@string, attributeName))
				{
					text = string2;
					break;
				}
			}
			return text;
		}

		// Token: 0x040009B8 RID: 2488
		internal byte[] m_data;

		// Token: 0x040009B9 RID: 2489
		internal const byte c_element = 1;

		// Token: 0x040009BA RID: 2490
		internal const byte c_attribute = 2;

		// Token: 0x040009BB RID: 2491
		internal const byte c_text = 3;

		// Token: 0x040009BC RID: 2492
		internal const byte c_children = 4;

		// Token: 0x040009BD RID: 2493
		internal const int c_growthSize = 32;
	}
}
