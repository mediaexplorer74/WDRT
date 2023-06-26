using System;
using System.IO;
using System.Text;

namespace System.Security.Util
{
	// Token: 0x02000382 RID: 898
	internal sealed class Parser
	{
		// Token: 0x06002CD0 RID: 11472 RVA: 0x000A95DB File Offset: 0x000A77DB
		internal SecurityElement GetTopElement()
		{
			return this._doc.GetRootElement();
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000A95E8 File Offset: 0x000A77E8
		private void GetRequiredSizes(TokenizerStream stream, ref int index)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			int num = 1;
			SecurityElementType securityElementType = SecurityElementType.Regular;
			string text = null;
			bool flag5 = false;
			bool flag6 = false;
			int num2 = 0;
			for (;;)
			{
				short num3 = stream.GetNextToken();
				while (num3 != -1)
				{
					switch (num3 & 255)
					{
					case 0:
						flag4 = true;
						flag6 = false;
						num3 = stream.GetNextToken();
						if (num3 == 2)
						{
							stream.TagLastToken(17408);
							for (;;)
							{
								num3 = stream.GetNextToken();
								if (num3 != 3)
								{
									break;
								}
								stream.ThrowAwayNextString();
								stream.TagLastToken(20480);
							}
							if (num3 == -1)
							{
								goto Block_9;
							}
							if (num3 != 1)
							{
								goto Block_10;
							}
							flag4 = false;
							index++;
							flag6 = false;
							num--;
							flag = true;
							goto IL_3B9;
						}
						else if (num3 == 3)
						{
							flag3 = true;
							stream.TagLastToken(16640);
							index += SecurityDocument.EncodedStringSize(stream.GetNextString()) + 1;
							if (securityElementType != SecurityElementType.Regular)
							{
								goto Block_12;
							}
							flag = true;
							num++;
							goto IL_3B9;
						}
						else
						{
							if (num3 == 6)
							{
								num2 = 1;
								do
								{
									num3 = stream.GetNextToken();
									switch (num3)
									{
									case 0:
										num2++;
										break;
									case 1:
										num2--;
										break;
									case 3:
										stream.ThrowAwayNextString();
										stream.TagLastToken(20480);
										break;
									}
								}
								while (num2 > 0);
								flag4 = false;
								flag6 = false;
								flag = true;
								goto IL_3B9;
							}
							if (num3 != 5)
							{
								goto IL_2B3;
							}
							num3 = stream.GetNextToken();
							if (num3 != 3)
							{
								goto Block_17;
							}
							flag3 = true;
							securityElementType = SecurityElementType.Format;
							stream.TagLastToken(16640);
							index += SecurityDocument.EncodedStringSize(stream.GetNextString()) + 1;
							num2 = 1;
							num++;
							flag = true;
							goto IL_3B9;
						}
						break;
					case 1:
						if (flag4)
						{
							flag4 = false;
							goto IL_3C4;
						}
						goto IL_2E0;
					case 2:
						num3 = stream.GetNextToken();
						if (num3 == 1)
						{
							stream.TagLastToken(17408);
							index++;
							num--;
							flag6 = false;
							flag = true;
							goto IL_3B9;
						}
						goto IL_329;
					case 3:
						if (flag4)
						{
							if (securityElementType == SecurityElementType.Comment)
							{
								stream.ThrowAwayNextString();
								stream.TagLastToken(20480);
								goto IL_3B9;
							}
							if (text == null)
							{
								text = stream.GetNextString();
								goto IL_3B9;
							}
							if (!flag5)
							{
								goto Block_5;
							}
							stream.TagLastToken(16896);
							index += SecurityDocument.EncodedStringSize(text) + SecurityDocument.EncodedStringSize(stream.GetNextString()) + 1;
							text = null;
							flag5 = false;
							goto IL_3B9;
						}
						else
						{
							if (flag6)
							{
								stream.TagLastToken(25344);
								index += SecurityDocument.EncodedStringSize(stream.GetNextString()) + SecurityDocument.EncodedStringSize(" ");
								goto IL_3B9;
							}
							stream.TagLastToken(17152);
							index += SecurityDocument.EncodedStringSize(stream.GetNextString()) + 1;
							flag6 = true;
							goto IL_3B9;
						}
						break;
					case 4:
						flag5 = true;
						goto IL_3B9;
					case 5:
						if (!flag4 || securityElementType != SecurityElementType.Format || num2 != 1)
						{
							goto IL_397;
						}
						num3 = stream.GetNextToken();
						if (num3 == 1)
						{
							stream.TagLastToken(17408);
							index++;
							num--;
							flag6 = false;
							flag = true;
							goto IL_3B9;
						}
						goto IL_37C;
					}
					goto Block_1;
					IL_3C4:
					num3 = stream.GetNextToken();
					continue;
					IL_3B9:
					if (flag)
					{
						flag = false;
						flag2 = false;
						break;
					}
					flag2 = true;
					goto IL_3C4;
				}
				if (flag2)
				{
					index++;
					num--;
					flag6 = false;
				}
				else if (num3 == -1 && (num != 1 || !flag3))
				{
					goto IL_3F5;
				}
				if (num <= 1)
				{
					return;
				}
			}
			Block_1:
			goto IL_3A8;
			Block_5:
			throw new XmlSyntaxException(this._t.LineNo);
			Block_9:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_UnexpectedEndOfFile"));
			Block_10:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_ExpectedCloseBracket"));
			Block_12:
			throw new XmlSyntaxException(this._t.LineNo);
			Block_17:
			throw new XmlSyntaxException(this._t.LineNo);
			IL_2B3:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_ExpectedSlashOrString"));
			IL_2E0:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_UnexpectedCloseBracket"));
			IL_329:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_ExpectedCloseBracket"));
			IL_37C:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_ExpectedCloseBracket"));
			IL_397:
			throw new XmlSyntaxException(this._t.LineNo);
			IL_3A8:
			throw new XmlSyntaxException(this._t.LineNo);
			IL_3F5:
			throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_UnexpectedEndOfFile"));
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000A9A10 File Offset: 0x000A7C10
		private int DetermineFormat(TokenizerStream stream)
		{
			if (stream.GetNextToken() == 0 && stream.GetNextToken() == 5)
			{
				this._t.GetTokens(stream, -1, true);
				stream.GoToPosition(2);
				bool flag = false;
				bool flag2 = false;
				short num = stream.GetNextToken();
				while (num != -1 && num != 1)
				{
					if (num != 3)
					{
						if (num != 4)
						{
							throw new XmlSyntaxException(this._t.LineNo, Environment.GetResourceString("XMLSyntax_UnexpectedEndOfFile"));
						}
						flag = true;
					}
					else
					{
						if (flag && flag2)
						{
							this._t.ChangeFormat(Encoding.GetEncoding(stream.GetNextString()));
							return 0;
						}
						if (!flag)
						{
							if (string.Compare(stream.GetNextString(), "encoding", StringComparison.Ordinal) == 0)
							{
								flag2 = true;
							}
						}
						else
						{
							flag = false;
							flag2 = false;
							stream.ThrowAwayNextString();
						}
					}
					num = stream.GetNextToken();
				}
				return 0;
			}
			return 2;
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000A9AD4 File Offset: 0x000A7CD4
		private void ParseContents()
		{
			TokenizerStream tokenizerStream = new TokenizerStream();
			this._t.GetTokens(tokenizerStream, 2, false);
			tokenizerStream.Reset();
			int num = this.DetermineFormat(tokenizerStream);
			tokenizerStream.GoToPosition(num);
			this._t.GetTokens(tokenizerStream, -1, false);
			tokenizerStream.Reset();
			int num2 = 0;
			this.GetRequiredSizes(tokenizerStream, ref num2);
			this._doc = new SecurityDocument(num2);
			int num3 = 0;
			tokenizerStream.Reset();
			for (short num4 = tokenizerStream.GetNextFullToken(); num4 != -1; num4 = tokenizerStream.GetNextFullToken())
			{
				if ((num4 & 16384) == 16384)
				{
					short num5 = (short)((int)num4 & 65280);
					if (num5 <= 17152)
					{
						if (num5 == 16640)
						{
							this._doc.AddToken(1, ref num3);
							this._doc.AddString(tokenizerStream.GetNextString(), ref num3);
							goto IL_19D;
						}
						if (num5 == 16896)
						{
							this._doc.AddToken(2, ref num3);
							this._doc.AddString(tokenizerStream.GetNextString(), ref num3);
							this._doc.AddString(tokenizerStream.GetNextString(), ref num3);
							goto IL_19D;
						}
						if (num5 == 17152)
						{
							this._doc.AddToken(3, ref num3);
							this._doc.AddString(tokenizerStream.GetNextString(), ref num3);
							goto IL_19D;
						}
					}
					else
					{
						if (num5 == 17408)
						{
							this._doc.AddToken(4, ref num3);
							goto IL_19D;
						}
						if (num5 == 20480)
						{
							tokenizerStream.ThrowAwayNextString();
							goto IL_19D;
						}
						if (num5 == 25344)
						{
							this._doc.AppendString(" ", ref num3);
							this._doc.AppendString(tokenizerStream.GetNextString(), ref num3);
							goto IL_19D;
						}
					}
					throw new XmlSyntaxException();
				}
				IL_19D:;
			}
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000A9C8C File Offset: 0x000A7E8C
		private Parser(Tokenizer t)
		{
			this._t = t;
			this._doc = null;
			try
			{
				this.ParseContents();
			}
			finally
			{
				this._t.Recycle();
			}
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000A9CD4 File Offset: 0x000A7ED4
		internal Parser(string input)
			: this(new Tokenizer(input))
		{
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000A9CE2 File Offset: 0x000A7EE2
		internal Parser(string input, string[] searchStrings, string[] replaceStrings)
			: this(new Tokenizer(input, searchStrings, replaceStrings))
		{
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000A9CF2 File Offset: 0x000A7EF2
		internal Parser(byte[] array, Tokenizer.ByteTokenEncoding encoding)
			: this(new Tokenizer(array, encoding, 0))
		{
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000A9D02 File Offset: 0x000A7F02
		internal Parser(byte[] array, Tokenizer.ByteTokenEncoding encoding, int startIndex)
			: this(new Tokenizer(array, encoding, startIndex))
		{
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000A9D12 File Offset: 0x000A7F12
		internal Parser(StreamReader input)
			: this(new Tokenizer(input))
		{
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000A9D20 File Offset: 0x000A7F20
		internal Parser(char[] array)
			: this(new Tokenizer(array))
		{
		}

		// Token: 0x040011F8 RID: 4600
		private SecurityDocument _doc;

		// Token: 0x040011F9 RID: 4601
		private Tokenizer _t;

		// Token: 0x040011FA RID: 4602
		private const short c_flag = 16384;

		// Token: 0x040011FB RID: 4603
		private const short c_elementtag = 16640;

		// Token: 0x040011FC RID: 4604
		private const short c_attributetag = 16896;

		// Token: 0x040011FD RID: 4605
		private const short c_texttag = 17152;

		// Token: 0x040011FE RID: 4606
		private const short c_additionaltexttag = 25344;

		// Token: 0x040011FF RID: 4607
		private const short c_childrentag = 17408;

		// Token: 0x04001200 RID: 4608
		private const short c_wastedstringtag = 20480;
	}
}
