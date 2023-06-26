using System;

namespace System.Security.Util
{
	// Token: 0x02000386 RID: 902
	internal sealed class TokenizerStream
	{
		// Token: 0x06002CE7 RID: 11495 RVA: 0x000AA699 File Offset: 0x000A8899
		internal TokenizerStream()
		{
			this.m_countTokens = 0;
			this.m_headTokens = new TokenizerShortBlock();
			this.m_headStrings = new TokenizerStringBlock();
			this.Reset();
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000AA6C4 File Offset: 0x000A88C4
		internal void AddToken(short token)
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_currentTokens.m_next = new TokenizerShortBlock();
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			this.m_countTokens++;
			short[] block = this.m_currentTokens.m_block;
			int indexTokens = this.m_indexTokens;
			this.m_indexTokens = indexTokens + 1;
			block[indexTokens] = token;
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000AA73C File Offset: 0x000A893C
		internal void AddString(string str)
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings.m_next = new TokenizerStringBlock();
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			string[] block = this.m_currentStrings.m_block;
			int indexStrings = this.m_indexStrings;
			this.m_indexStrings = indexStrings + 1;
			block[indexStrings] = str;
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000AA7A4 File Offset: 0x000A89A4
		internal void Reset()
		{
			this.m_lastTokens = null;
			this.m_currentTokens = this.m_headTokens;
			this.m_currentStrings = this.m_headStrings;
			this.m_indexTokens = 0;
			this.m_indexStrings = 0;
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000AA7D4 File Offset: 0x000A89D4
		internal short GetNextFullToken()
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_lastTokens = this.m_currentTokens;
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			short[] block = this.m_currentTokens.m_block;
			int indexTokens = this.m_indexTokens;
			this.m_indexTokens = indexTokens + 1;
			return block[indexTokens];
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000AA838 File Offset: 0x000A8A38
		internal short GetNextToken()
		{
			return this.GetNextFullToken() & 255;
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000AA854 File Offset: 0x000A8A54
		internal string GetNextString()
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			string[] block = this.m_currentStrings.m_block;
			int indexStrings = this.m_indexStrings;
			this.m_indexStrings = indexStrings + 1;
			return block[indexStrings];
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000AA8AB File Offset: 0x000A8AAB
		internal void ThrowAwayNextString()
		{
			this.GetNextString();
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000AA8B4 File Offset: 0x000A8AB4
		internal void TagLastToken(short tag)
		{
			if (this.m_indexTokens == 0)
			{
				this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] = (short)((ushort)this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] | (ushort)tag);
				return;
			}
			this.m_currentTokens.m_block[this.m_indexTokens - 1] = (short)((ushort)this.m_currentTokens.m_block[this.m_indexTokens - 1] | (ushort)tag);
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000AA932 File Offset: 0x000A8B32
		internal int GetTokenCount()
		{
			return this.m_countTokens;
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000AA93C File Offset: 0x000A8B3C
		internal void GoToPosition(int position)
		{
			this.Reset();
			for (int i = 0; i < position; i++)
			{
				if (this.GetNextToken() == 3)
				{
					this.ThrowAwayNextString();
				}
			}
		}

		// Token: 0x04001229 RID: 4649
		private int m_countTokens;

		// Token: 0x0400122A RID: 4650
		private TokenizerShortBlock m_headTokens;

		// Token: 0x0400122B RID: 4651
		private TokenizerShortBlock m_lastTokens;

		// Token: 0x0400122C RID: 4652
		private TokenizerShortBlock m_currentTokens;

		// Token: 0x0400122D RID: 4653
		private int m_indexTokens;

		// Token: 0x0400122E RID: 4654
		private TokenizerStringBlock m_headStrings;

		// Token: 0x0400122F RID: 4655
		private TokenizerStringBlock m_currentStrings;

		// Token: 0x04001230 RID: 4656
		private int m_indexStrings;
	}
}
