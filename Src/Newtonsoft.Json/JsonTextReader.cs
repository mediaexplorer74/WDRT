﻿using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200002D RID: 45
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonTextReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00006168 File Offset: 0x00004368
		public override Task<bool> ReadAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsync(cancellationToken);
			}
			return this.DoReadAsync(cancellationToken);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006184 File Offset: 0x00004384
		internal Task<bool> DoReadAsync(CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			Task<bool> task;
			for (;;)
			{
				switch (this._currentState)
				{
				case JsonReader.State.Start:
				case JsonReader.State.Property:
				case JsonReader.State.ArrayStart:
				case JsonReader.State.Array:
				case JsonReader.State.ConstructorStart:
				case JsonReader.State.Constructor:
					goto IL_49;
				case JsonReader.State.ObjectStart:
				case JsonReader.State.Object:
					goto IL_51;
				case JsonReader.State.PostValue:
					task = this.ParsePostValueAsync(false, cancellationToken);
					if (!task.IsCompletedSucessfully())
					{
						goto IL_78;
					}
					if (task.Result)
					{
						goto Block_3;
					}
					continue;
				case JsonReader.State.Finished:
					goto IL_81;
				}
				break;
			}
			goto IL_89;
			IL_49:
			return this.ParseValueAsync(cancellationToken);
			IL_51:
			return this.ParseObjectAsync(cancellationToken);
			Block_3:
			return AsyncUtils.True;
			IL_78:
			return this.DoReadAsync(task, cancellationToken);
			IL_81:
			return this.ReadFromFinishedAsync(cancellationToken);
			IL_89:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000623C File Offset: 0x0000443C
		private async Task<bool> DoReadAsync(Task<bool> task, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool flag;
			if (configuredTaskAwaiter.GetResult())
			{
				flag = true;
			}
			else
			{
				flag = await this.DoReadAsync(cancellationToken).ConfigureAwait(false);
			}
			return flag;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006290 File Offset: 0x00004490
		private async Task<bool> ParsePostValueAsync(bool ignoreComments, CancellationToken cancellationToken)
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= ')')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_2D5;
							case '\r':
								await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
								continue;
							default:
								goto IL_2D5;
							}
						}
						else
						{
							if (this._charsUsed != this._charPos)
							{
								this._charPos++;
								continue;
							}
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								await configuredTaskAwaiter;
								ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							}
							if (configuredTaskAwaiter.GetResult() == 0)
							{
								break;
							}
							continue;
						}
					}
					else if (c != ' ')
					{
						if (c != ')')
						{
							goto IL_2D5;
						}
						goto IL_182;
					}
					this._charPos++;
					continue;
				}
				if (c <= '/')
				{
					if (c == ',')
					{
						goto IL_228;
					}
					if (c == '/')
					{
						await this.ParseCommentAsync(!ignoreComments, cancellationToken).ConfigureAwait(false);
						if (!ignoreComments)
						{
							goto Block_14;
						}
						continue;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_165;
					}
					if (c == '}')
					{
						goto IL_148;
					}
				}
				IL_2D5:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_2F0;
				}
				this._charPos++;
			}
			this._currentState = JsonReader.State.Finished;
			return false;
			IL_148:
			this._charPos++;
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_165:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_182:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			Block_14:
			return true;
			IL_228:
			this._charPos++;
			base.SetStateBasedOnCurrent();
			return false;
			IL_2F0:
			if (!base.SupportMultipleContent || this.Depth != 0)
			{
				char c;
				throw JsonReaderException.Create(this, "After parsing a value an unexpected character was encountered: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
			}
			base.SetStateBasedOnCurrent();
			return false;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000062E4 File Offset: 0x000044E4
		private async Task<bool> ReadFromFinishedAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.EnsureCharsAsync(0, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool flag;
			if (configuredTaskAwaiter.GetResult())
			{
				await this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false);
				if (this._isEndOfFile)
				{
					base.SetToken(JsonToken.None);
					flag = false;
				}
				else
				{
					if (this._chars[this._charPos] != '/')
					{
						throw JsonReaderException.Create(this, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
					}
					await this.ParseCommentAsync(true, cancellationToken).ConfigureAwait(false);
					flag = true;
				}
			}
			else
			{
				base.SetToken(JsonToken.None);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000632F File Offset: 0x0000452F
		private Task<int> ReadDataAsync(bool append, CancellationToken cancellationToken)
		{
			return this.ReadDataAsync(append, 0, cancellationToken);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000633C File Offset: 0x0000453C
		private async Task<int> ReadDataAsync(bool append, int charsRequired, CancellationToken cancellationToken)
		{
			int num;
			if (this._isEndOfFile)
			{
				num = 0;
			}
			else
			{
				this.PrepareBufferForReadData(append, charsRequired);
				int num2 = await this._reader.ReadAsync(this._chars, this._charsUsed, this._chars.Length - this._charsUsed - 1, cancellationToken).ConfigureAwait(false);
				this._charsUsed += num2;
				if (num2 == 0)
				{
					this._isEndOfFile = true;
				}
				this._chars[this._charsUsed] = '\0';
				num = num2;
			}
			return num;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006398 File Offset: 0x00004598
		private async Task<bool> ParseValueAsync(CancellationToken cancellationToken)
		{
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= 'N')
				{
					if (c <= ' ')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_94B;
							case '\r':
								await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
								continue;
							default:
								if (c != ' ')
								{
									goto IL_94B;
								}
								break;
							}
							this._charPos++;
							continue;
						}
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter.GetResult() == 0)
						{
							break;
						}
						continue;
					}
					else if (c <= '/')
					{
						if (c == '"')
						{
							goto IL_1E0;
						}
						switch (c)
						{
						case '\'':
							goto IL_1E0;
						case ')':
							goto IL_89B;
						case ',':
							goto IL_88C;
						case '-':
							goto IL_5D7;
						case '/':
							goto IL_74A;
						}
					}
					else
					{
						if (c == 'I')
						{
							goto IL_560;
						}
						if (c == 'N')
						{
							goto IL_4E9;
						}
					}
				}
				else if (c <= 'f')
				{
					if (c == '[')
					{
						goto IL_853;
					}
					if (c == ']')
					{
						goto IL_86F;
					}
					if (c == 'f')
					{
						goto IL_2CC;
					}
				}
				else if (c <= 't')
				{
					if (c == 'n')
					{
						goto IL_341;
					}
					if (c == 't')
					{
						goto IL_257;
					}
				}
				else
				{
					if (c == 'u')
					{
						goto IL_7C1;
					}
					if (c == '{')
					{
						goto IL_837;
					}
				}
				IL_94B:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_966;
				}
				this._charPos++;
			}
			return false;
			IL_1E0:
			await this.ParseStringAsync(c, ReadType.Read, cancellationToken).ConfigureAwait(false);
			return true;
			IL_257:
			await this.ParseTrueAsync(cancellationToken).ConfigureAwait(false);
			return true;
			IL_2CC:
			await this.ParseFalseAsync(cancellationToken).ConfigureAwait(false);
			return true;
			IL_341:
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter3.GetResult())
			{
				char c2 = this._chars[this._charPos + 1];
				if (c2 != 'e')
				{
					if (c2 != 'u')
					{
						throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
					}
					await this.ParseNullAsync(cancellationToken).ConfigureAwait(false);
				}
				else
				{
					await this.ParseConstructorAsync(cancellationToken).ConfigureAwait(false);
				}
				return true;
			}
			this._charPos++;
			throw base.CreateUnexpectedEndException();
			IL_4E9:
			await this.ParseNumberNaNAsync(ReadType.Read, cancellationToken).ConfigureAwait(false);
			return true;
			IL_560:
			await this.ParseNumberPositiveInfinityAsync(ReadType.Read, cancellationToken).ConfigureAwait(false);
			return true;
			IL_5D7:
			configuredTaskAwaiter3 = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter3.GetResult() && this._chars[this._charPos + 1] == 'I')
			{
				await this.ParseNumberNegativeInfinityAsync(ReadType.Read, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				await this.ParseNumberAsync(ReadType.Read, cancellationToken).ConfigureAwait(false);
			}
			return true;
			IL_74A:
			await this.ParseCommentAsync(true, cancellationToken).ConfigureAwait(false);
			return true;
			IL_7C1:
			await this.ParseUndefinedAsync(cancellationToken).ConfigureAwait(false);
			return true;
			IL_837:
			this._charPos++;
			base.SetToken(JsonToken.StartObject);
			return true;
			IL_853:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return true;
			IL_86F:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_88C:
			base.SetToken(JsonToken.Undefined);
			return true;
			IL_89B:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_966:
			if (!char.IsNumber(c) && c != '-' && c != '.')
			{
				throw this.CreateUnexpectedCharacterException(c);
			}
			await this.ParseNumberAsync(ReadType.Read, cancellationToken).ConfigureAwait(false);
			return true;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000063E4 File Offset: 0x000045E4
		private async Task ReadStringIntoBufferAsync(char quote, CancellationToken cancellationToken)
		{
			int charPos = this._charPos;
			int initialPosition = this._charPos;
			int lastWritePosition = this._charPos;
			this._stringBuffer.Position = 0;
			char c2;
			for (;;)
			{
				char[] chars = this._chars;
				int num = charPos;
				charPos = num + 1;
				char c = chars[num];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						if (c != '\n')
						{
							if (c == '\r')
							{
								this._charPos = charPos - 1;
								await this.ProcessCarriageReturnAsync(true, cancellationToken).ConfigureAwait(false);
								charPos = this._charPos;
							}
						}
						else
						{
							this._charPos = charPos - 1;
							this.ProcessLineFeed();
							charPos = this._charPos;
						}
					}
					else if (this._charsUsed == charPos - 1)
					{
						num = charPos;
						charPos = num - 1;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter.GetResult() == 0)
						{
							break;
						}
					}
				}
				else if (c != '"' && c != '\'')
				{
					if (c == '\\')
					{
						this._charPos = charPos;
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.EnsureCharsAsync(0, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							await configuredTaskAwaiter3;
							configuredTaskAwaiter3 = configuredTaskAwaiter4;
							configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						}
						if (!configuredTaskAwaiter3.GetResult())
						{
							goto Block_12;
						}
						int escapeStartPos = charPos - 1;
						c2 = this._chars[charPos];
						num = charPos;
						charPos = num + 1;
						char writeChar;
						if (c2 <= '\\')
						{
							if (c2 <= '\'')
							{
								if (c2 != '"' && c2 != '\'')
								{
									goto Block_16;
								}
							}
							else if (c2 != '/')
							{
								if (c2 != '\\')
								{
									goto Block_18;
								}
								writeChar = '\\';
								goto IL_5A2;
							}
							writeChar = c2;
						}
						else if (c2 <= 'f')
						{
							if (c2 != 'b')
							{
								if (c2 != 'f')
								{
									goto Block_21;
								}
								writeChar = '\f';
							}
							else
							{
								writeChar = '\b';
							}
						}
						else
						{
							if (c2 != 'n')
							{
								switch (c2)
								{
								case 'r':
									writeChar = '\r';
									goto IL_5A2;
								case 't':
									writeChar = '\t';
									goto IL_5A2;
								case 'u':
									this._charPos = charPos;
									writeChar = await this.ParseUnicodeAsync(cancellationToken).ConfigureAwait(false);
									if (StringUtils.IsLowSurrogate(writeChar))
									{
										writeChar = '\ufffd';
									}
									else if (StringUtils.IsHighSurrogate(writeChar))
									{
										bool anotherHighSurrogate;
										do
										{
											anotherHighSurrogate = false;
											configuredTaskAwaiter3 = this.EnsureCharsAsync(2, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
											if (!configuredTaskAwaiter3.IsCompleted)
											{
												await configuredTaskAwaiter3;
												configuredTaskAwaiter3 = configuredTaskAwaiter4;
												configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
											}
											if (configuredTaskAwaiter3.GetResult() && this._chars[this._charPos] == '\\' && this._chars[this._charPos + 1] == 'u')
											{
												char highSurrogate = writeChar;
												this._charPos += 2;
												writeChar = await this.ParseUnicodeAsync(cancellationToken).ConfigureAwait(false);
												if (!StringUtils.IsLowSurrogate(writeChar))
												{
													if (StringUtils.IsHighSurrogate(writeChar))
													{
														highSurrogate = '\ufffd';
														anotherHighSurrogate = true;
													}
													else
													{
														highSurrogate = '\ufffd';
													}
												}
												this.EnsureBufferNotEmpty();
												this.WriteCharToBuffer(highSurrogate, lastWritePosition, escapeStartPos);
												lastWritePosition = this._charPos;
											}
											else
											{
												writeChar = '\ufffd';
											}
										}
										while (anotherHighSurrogate);
									}
									charPos = this._charPos;
									goto IL_5A2;
								}
								goto Block_23;
							}
							writeChar = '\n';
						}
						IL_5A2:
						this.EnsureBufferNotEmpty();
						this.WriteCharToBuffer(writeChar, lastWritePosition, escapeStartPos);
						lastWritePosition = charPos;
					}
				}
				else if (this._chars[charPos - 1] == quote)
				{
					goto Block_31;
				}
			}
			this._charPos = charPos;
			throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
			Block_12:
			throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
			Block_16:
			Block_18:
			Block_21:
			Block_23:
			this._charPos = charPos;
			throw JsonReaderException.Create(this, "Bad JSON escape sequence: {0}.".FormatWith(CultureInfo.InvariantCulture, "\\" + c2.ToString()));
			Block_31:
			this.FinishReadStringIntoBuffer(charPos - 1, initialPosition, lastWritePosition);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006438 File Offset: 0x00004638
		private Task ProcessCarriageReturnAsync(bool append, CancellationToken cancellationToken)
		{
			this._charPos++;
			Task<bool> task = this.EnsureCharsAsync(1, append, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				this.SetNewLine(task.Result);
				return AsyncUtils.CompletedTask;
			}
			return this.ProcessCarriageReturnAsync(task);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006480 File Offset: 0x00004680
		private async Task ProcessCarriageReturnAsync(Task<bool> task)
		{
			bool flag = await task.ConfigureAwait(false);
			this.SetNewLine(flag);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000064CC File Offset: 0x000046CC
		private async Task<char> ParseUnicodeAsync(CancellationToken cancellationToken)
		{
			bool flag = await this.EnsureCharsAsync(4, true, cancellationToken).ConfigureAwait(false);
			return this.ConvertUnicode(flag);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006517 File Offset: 0x00004717
		private Task<bool> EnsureCharsAsync(int relativePosition, bool append, CancellationToken cancellationToken)
		{
			if (this._charPos + relativePosition < this._charsUsed)
			{
				return AsyncUtils.True;
			}
			if (this._isEndOfFile)
			{
				return AsyncUtils.False;
			}
			return this.ReadCharsAsync(relativePosition, append, cancellationToken);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006548 File Offset: 0x00004748
		private async Task<bool> ReadCharsAsync(int relativePosition, bool append, CancellationToken cancellationToken)
		{
			int charsRequired = this._charPos + relativePosition - this._charsUsed + 1;
			for (;;)
			{
				int num = await this.ReadDataAsync(append, charsRequired, cancellationToken).ConfigureAwait(false);
				if (num == 0)
				{
					break;
				}
				charsRequired -= num;
				if (charsRequired <= 0)
				{
					goto Block_2;
				}
			}
			return false;
			Block_2:
			return true;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000065A4 File Offset: 0x000047A4
		private async Task<bool> ParseObjectAsync(CancellationToken cancellationToken)
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_23A;
						case '\r':
							await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
							continue;
						default:
							goto IL_23A;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter.GetResult() == 0)
						{
							break;
						}
						continue;
					}
				}
				else if (c != ' ')
				{
					if (c == '/')
					{
						goto IL_132;
					}
					if (c != '}')
					{
						goto IL_23A;
					}
					goto IL_115;
				}
				this._charPos++;
				continue;
				IL_23A:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_255;
				}
				this._charPos++;
			}
			return false;
			IL_115:
			base.SetToken(JsonToken.EndObject);
			this._charPos++;
			return true;
			IL_132:
			await this.ParseCommentAsync(true, cancellationToken).ConfigureAwait(false);
			return true;
			IL_255:
			return await this.ParsePropertyAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000065F0 File Offset: 0x000047F0
		private async Task ParseCommentAsync(bool setToken, CancellationToken cancellationToken)
		{
			this._charPos++;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.EnsureCharsAsync(1, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing comment.");
			}
			bool singlelineComment;
			if (this._chars[this._charPos] == '*')
			{
				singlelineComment = false;
			}
			else
			{
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, "Error parsing comment. Expected: *, got {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				singlelineComment = true;
			}
			this._charPos++;
			int initialPosition = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= '\n')
				{
					if (c != '\0')
					{
						if (c == '\n')
						{
							if (singlelineComment)
							{
								goto Block_19;
							}
							this.ProcessLineFeed();
							continue;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							await configuredTaskAwaiter3;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
							configuredTaskAwaiter3 = configuredTaskAwaiter4;
							configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter3.GetResult() == 0)
						{
							break;
						}
						continue;
					}
				}
				else if (c != '\r')
				{
					if (c == '*')
					{
						this._charPos++;
						if (singlelineComment)
						{
							continue;
						}
						configuredTaskAwaiter = this.EnsureCharsAsync(0, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter.GetResult() && this._chars[this._charPos] == '/')
						{
							goto Block_17;
						}
						continue;
					}
				}
				else
				{
					if (singlelineComment)
					{
						goto Block_18;
					}
					await this.ProcessCarriageReturnAsync(true, cancellationToken).ConfigureAwait(false);
					continue;
				}
				this._charPos++;
			}
			if (!singlelineComment)
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing comment.");
			}
			this.EndComment(setToken, initialPosition, this._charPos);
			return;
			Block_17:
			this.EndComment(setToken, initialPosition, this._charPos - 1);
			this._charPos++;
			return;
			Block_18:
			this.EndComment(setToken, initialPosition, this._charPos);
			return;
			Block_19:
			this.EndComment(setToken, initialPosition, this._charPos);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006644 File Offset: 0x00004844
		private async Task EatWhitespaceAsync(CancellationToken cancellationToken)
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c != '\0')
				{
					if (c != '\n')
					{
						if (c != '\r')
						{
							if (c != ' ' && !char.IsWhiteSpace(c))
							{
								break;
							}
							this._charPos++;
						}
						else
						{
							await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
						}
					}
					else
					{
						this.ProcessLineFeed();
					}
				}
				else if (this._charsUsed == this._charPos)
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult() == 0)
					{
						break;
					}
				}
				else
				{
					this._charPos++;
				}
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006690 File Offset: 0x00004890
		private async Task ParseStringAsync(char quote, ReadType readType, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this._charPos++;
			this.ShiftBufferIfNeeded();
			await this.ReadStringIntoBufferAsync(quote, cancellationToken).ConfigureAwait(false);
			this.ParseReadString(quote, readType);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000066EC File Offset: 0x000048EC
		private async Task<bool> MatchValueAsync(string value, CancellationToken cancellationToken)
		{
			bool flag = await this.EnsureCharsAsync(value.Length - 1, true, cancellationToken).ConfigureAwait(false);
			return this.MatchValue(flag, value);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006740 File Offset: 0x00004940
		private async Task<bool> MatchValueWithTrailingSeparatorAsync(string value, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.MatchValueAsync(value, cancellationToken).ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool flag;
			if (!configuredTaskAwaiter.GetResult())
			{
				flag = false;
			}
			else
			{
				configuredTaskAwaiter = this.EnsureCharsAsync(0, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					flag = true;
				}
				else
				{
					flag = this.IsSeparator(this._chars[this._charPos]) || this._chars[this._charPos] == '\0';
				}
			}
			return flag;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006794 File Offset: 0x00004994
		private async Task MatchAndSetAsync(string value, JsonToken newToken, [Nullable(2)] object tokenValue, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.MatchValueWithTrailingSeparatorAsync(value, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter.GetResult())
			{
				base.SetToken(newToken, tokenValue);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing " + newToken.ToString().ToLowerInvariant() + " value.");
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000067F8 File Offset: 0x000049F8
		private Task ParseTrueAsync(CancellationToken cancellationToken)
		{
			return this.MatchAndSetAsync(JsonConvert.True, JsonToken.Boolean, true, cancellationToken);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000680E File Offset: 0x00004A0E
		private Task ParseFalseAsync(CancellationToken cancellationToken)
		{
			return this.MatchAndSetAsync(JsonConvert.False, JsonToken.Boolean, false, cancellationToken);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006824 File Offset: 0x00004A24
		private Task ParseNullAsync(CancellationToken cancellationToken)
		{
			return this.MatchAndSetAsync(JsonConvert.Null, JsonToken.Null, null, cancellationToken);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006838 File Offset: 0x00004A38
		private async Task ParseConstructorAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.MatchValueWithTrailingSeparatorAsync("new", cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw JsonReaderException.Create(this, "Unexpected content while parsing JSON.");
			}
			await this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false);
			int initialPosition = this._charPos;
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_1BD;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter3.IsCompleted)
					{
						await configuredTaskAwaiter3;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
						configuredTaskAwaiter3 = configuredTaskAwaiter4;
						configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter3.GetResult() == 0)
					{
						break;
					}
				}
				else
				{
					if (!char.IsLetterOrDigit(c))
					{
						goto IL_1F8;
					}
					this._charPos++;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing constructor.");
			IL_1BD:
			int endPosition = this._charPos;
			this._charPos++;
			goto IL_2EB;
			IL_1F8:
			if (c == '\r')
			{
				endPosition = this._charPos;
				await this.ProcessCarriageReturnAsync(true, cancellationToken).ConfigureAwait(false);
			}
			else if (c == '\n')
			{
				endPosition = this._charPos;
				this.ProcessLineFeed();
			}
			else if (char.IsWhiteSpace(c))
			{
				endPosition = this._charPos;
				this._charPos++;
			}
			else
			{
				if (c != '(')
				{
					throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
				}
				endPosition = this._charPos;
			}
			IL_2EB:
			this._stringReference = new StringReference(this._chars, initialPosition, endPosition - initialPosition);
			string constructorName = this._stringReference.ToString();
			await this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false);
			if (this._chars[this._charPos] != '(')
			{
				throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			this.ClearRecentString();
			base.SetToken(JsonToken.StartConstructor, constructorName);
			constructorName = null;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006884 File Offset: 0x00004A84
		private async Task<object> ParseNumberNaNAsync(ReadType readType, CancellationToken cancellationToken)
		{
			bool flag = await this.MatchValueWithTrailingSeparatorAsync(JsonConvert.NaN, cancellationToken).ConfigureAwait(false);
			return this.ParseNumberNaN(readType, flag);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000068D8 File Offset: 0x00004AD8
		private async Task<object> ParseNumberPositiveInfinityAsync(ReadType readType, CancellationToken cancellationToken)
		{
			bool flag = await this.MatchValueWithTrailingSeparatorAsync(JsonConvert.PositiveInfinity, cancellationToken).ConfigureAwait(false);
			return this.ParseNumberPositiveInfinity(readType, flag);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000692C File Offset: 0x00004B2C
		private async Task<object> ParseNumberNegativeInfinityAsync(ReadType readType, CancellationToken cancellationToken)
		{
			bool flag = await this.MatchValueWithTrailingSeparatorAsync(JsonConvert.NegativeInfinity, cancellationToken).ConfigureAwait(false);
			return this.ParseNumberNegativeInfinity(readType, flag);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006980 File Offset: 0x00004B80
		private async Task ParseNumberAsync(ReadType readType, CancellationToken cancellationToken)
		{
			this.ShiftBufferIfNeeded();
			char firstChar = this._chars[this._charPos];
			int initialPosition = this._charPos;
			await this.ReadNumberIntoBufferAsync(cancellationToken).ConfigureAwait(false);
			this.ParseReadNumber(readType, firstChar, initialPosition);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000069D3 File Offset: 0x00004BD3
		private Task ParseUndefinedAsync(CancellationToken cancellationToken)
		{
			return this.MatchAndSetAsync(JsonConvert.Undefined, JsonToken.Undefined, null, cancellationToken);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000069E4 File Offset: 0x00004BE4
		private async Task<bool> ParsePropertyAsync(CancellationToken cancellationToken)
		{
			char c = this._chars[this._charPos];
			char quoteChar;
			if (c == '"' || c == '\'')
			{
				this._charPos++;
				quoteChar = c;
				this.ShiftBufferIfNeeded();
				await this.ReadStringIntoBufferAsync(quoteChar, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				if (!this.ValidIdentifierChar(c))
				{
					throw JsonReaderException.Create(this, "Invalid property identifier character: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				quoteChar = '\0';
				this.ShiftBufferIfNeeded();
				await this.ParseUnquotedPropertyAsync(cancellationToken).ConfigureAwait(false);
			}
			string propertyName;
			if (this.PropertyNameTable != null)
			{
				propertyName = this.PropertyNameTable.Get(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length) ?? this._stringReference.ToString();
			}
			else
			{
				propertyName = this._stringReference.ToString();
			}
			await this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false);
			if (this._chars[this._charPos] != ':')
			{
				throw JsonReaderException.Create(this, "Invalid character after parsing property name. Expected ':' but got: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			base.SetToken(JsonToken.PropertyName, propertyName);
			this._quoteChar = quoteChar;
			this.ClearRecentString();
			return true;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006A30 File Offset: 0x00004C30
		private async Task ReadNumberIntoBufferAsync(CancellationToken cancellationToken)
		{
			int charPos = this._charPos;
			for (;;)
			{
				char c = this._chars[charPos];
				if (c == '\0')
				{
					this._charPos = charPos;
					if (this._charsUsed != charPos)
					{
						break;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult() == 0)
					{
						break;
					}
				}
				else
				{
					if (this.ReadNumberCharIntoBuffer(c, charPos))
					{
						break;
					}
					charPos++;
				}
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006A7C File Offset: 0x00004C7C
		private async Task ParseUnquotedPropertyAsync(CancellationToken cancellationToken)
		{
			int initialPosition = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_BC;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult() == 0)
					{
						break;
					}
				}
				else if (this.ReadUnquotedPropertyReportIfDone(c, initialPosition))
				{
					return;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing unquoted property name.");
			IL_BC:
			this._stringReference = new StringReference(this._chars, initialPosition, this._charPos - initialPosition);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006AC8 File Offset: 0x00004CC8
		private async Task<bool> ReadNullCharAsync(CancellationToken cancellationToken)
		{
			if (this._charsUsed == this._charPos)
			{
				ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult() == 0)
				{
					this._isEndOfFile = true;
					return true;
				}
			}
			else
			{
				this._charPos++;
			}
			return false;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006B14 File Offset: 0x00004D14
		private async Task HandleNullAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				this._charPos = this._charsUsed;
				throw base.CreateUnexpectedEndException();
			}
			if (this._chars[this._charPos + 1] == 'u')
			{
				await this.ParseNullAsync(cancellationToken).ConfigureAwait(false);
				return;
			}
			this._charPos += 2;
			throw this.CreateUnexpectedCharacterException(this._chars[this._charPos - 1]);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006B60 File Offset: 0x00004D60
		private async Task ReadFinishedAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.EnsureCharsAsync(0, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter.GetResult())
			{
				await this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false);
				if (this._isEndOfFile)
				{
					base.SetToken(JsonToken.None);
					return;
				}
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				await this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false);
			}
			base.SetToken(JsonToken.None);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006BAC File Offset: 0x00004DAC
		[return: Nullable(new byte[] { 1, 2 })]
		private async Task<object> ReadStringValueAsync(ReadType readType, CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_8FB;
			case JsonReader.State.PostValue:
				configuredTaskAwaiter = this.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				await this.ReadFinishedAsync(cancellationToken).ConfigureAwait(false);
				return null;
			default:
				goto IL_8FB;
			}
			char c;
			string expected;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= 'I')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								goto IL_87F;
							case '\v':
							case '\f':
								goto IL_85F;
							case '\r':
								await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
								goto IL_87F;
							default:
								goto IL_85F;
							}
						}
						else
						{
							configuredTaskAwaiter = this.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								await configuredTaskAwaiter;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							}
							if (configuredTaskAwaiter.GetResult())
							{
								break;
							}
							goto IL_87F;
						}
					}
					else
					{
						switch (c)
						{
						case ' ':
							break;
						case '!':
						case '#':
						case '$':
						case '%':
						case '&':
						case '(':
						case ')':
						case '*':
						case '+':
							goto IL_85F;
						case '"':
						case '\'':
							goto IL_294;
						case ',':
							this.ProcessValueComma();
							goto IL_87F;
						case '-':
							goto IL_31C;
						case '.':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							goto IL_433;
						case '/':
							await this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false);
							goto IL_87F;
						default:
							if (c != 'I')
							{
								goto IL_85F;
							}
							goto IL_5AA;
						}
					}
					this._charPos++;
				}
				else if (c <= ']')
				{
					if (c == 'N')
					{
						goto IL_624;
					}
					if (c != ']')
					{
						goto IL_85F;
					}
					goto IL_794;
				}
				else
				{
					if (c == 'f')
					{
						goto IL_4CE;
					}
					if (c == 'n')
					{
						goto IL_69E;
					}
					if (c != 't')
					{
						goto IL_85F;
					}
					goto IL_4CE;
				}
				IL_87F:
				expected = null;
				continue;
				IL_85F:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_28;
				}
				goto IL_87F;
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_294:
			await this.ParseStringAsync(c, readType, cancellationToken).ConfigureAwait(false);
			return this.FinishReadQuotedStringValue(readType);
			IL_31C:
			configuredTaskAwaiter = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter.GetResult() && this._chars[this._charPos + 1] == 'I')
			{
				return this.ParseNumberNegativeInfinity(readType);
			}
			await this.ParseNumberAsync(readType, cancellationToken).ConfigureAwait(false);
			return this.Value;
			IL_433:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			await this.ParseNumberAsync(ReadType.ReadAsString, cancellationToken).ConfigureAwait(false);
			return this.Value;
			IL_4CE:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			expected = ((c == 't') ? JsonConvert.True : JsonConvert.False);
			configuredTaskAwaiter = this.MatchValueWithTrailingSeparatorAsync(expected, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.String, expected);
			return expected;
			IL_5AA:
			return await this.ParseNumberPositiveInfinityAsync(readType, cancellationToken).ConfigureAwait(false);
			IL_624:
			return await this.ParseNumberNaNAsync(readType, cancellationToken).ConfigureAwait(false);
			IL_69E:
			await this.HandleNullAsync(cancellationToken).ConfigureAwait(false);
			return null;
			IL_794:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_28:
			throw this.CreateUnexpectedCharacterException(c);
			IL_8FB:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006C00 File Offset: 0x00004E00
		[return: Nullable(new byte[] { 1, 2 })]
		private async Task<object> ReadNumberValueAsync(ReadType readType, CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_852;
			case JsonReader.State.PostValue:
				configuredTaskAwaiter = this.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				await this.ReadFinishedAsync(cancellationToken).ConfigureAwait(false);
				return null;
			default:
				goto IL_852;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_7BF;
						case '\r':
							await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
							continue;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_7BF;
							case '"':
							case '\'':
								goto IL_277;
							case ',':
								this.ProcessValueComma();
								continue;
							case '-':
								goto IL_468;
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_5EA;
							case '/':
								await this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false);
								continue;
							default:
								goto IL_7BF;
							}
							break;
						}
						this._charPos++;
						continue;
					}
					configuredTaskAwaiter = this.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult())
					{
						break;
					}
					continue;
				}
				else if (c <= 'N')
				{
					if (c == 'I')
					{
						goto IL_3EE;
					}
					if (c == 'N')
					{
						goto IL_374;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_6EB;
					}
					if (c == 'n')
					{
						goto IL_2FF;
					}
				}
				IL_7BF:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_20;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_277:
			await this.ParseStringAsync(c, readType, cancellationToken).ConfigureAwait(false);
			return this.FinishReadQuotedNumber(readType);
			IL_2FF:
			await this.HandleNullAsync(cancellationToken).ConfigureAwait(false);
			return null;
			IL_374:
			return await this.ParseNumberNaNAsync(readType, cancellationToken).ConfigureAwait(false);
			IL_3EE:
			return await this.ParseNumberPositiveInfinityAsync(readType, cancellationToken).ConfigureAwait(false);
			IL_468:
			configuredTaskAwaiter = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter.GetResult() && this._chars[this._charPos + 1] == 'I')
			{
				return await this.ParseNumberNegativeInfinityAsync(readType, cancellationToken).ConfigureAwait(false);
			}
			await this.ParseNumberAsync(readType, cancellationToken).ConfigureAwait(false);
			return this.Value;
			IL_5EA:
			await this.ParseNumberAsync(readType, cancellationToken).ConfigureAwait(false);
			return this.Value;
			IL_6EB:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_20:
			throw this.CreateUnexpectedCharacterException(c);
			IL_852:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006C53 File Offset: 0x00004E53
		public override Task<bool?> ReadAsBooleanAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsBooleanAsync(cancellationToken);
			}
			return this.DoReadAsBooleanAsync(cancellationToken);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006C6C File Offset: 0x00004E6C
		internal async Task<bool?> DoReadAsBooleanAsync(CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_706;
			case JsonReader.State.PostValue:
				configuredTaskAwaiter = this.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				await this.ReadFinishedAsync(cancellationToken).ConfigureAwait(false);
				return null;
			default:
				goto IL_706;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							goto IL_680;
						case '\v':
						case '\f':
							goto IL_660;
						case '\r':
							await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
							goto IL_680;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_660;
							case '"':
							case '\'':
								goto IL_273;
							case ',':
								this.ProcessValueComma();
								goto IL_680;
							case '-':
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_37C;
							case '/':
								await this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false);
								goto IL_680;
							default:
								goto IL_660;
							}
							break;
						}
						this._charPos++;
					}
					else
					{
						configuredTaskAwaiter = this.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter.GetResult())
						{
							break;
						}
					}
				}
				else if (c <= 'f')
				{
					if (c == ']')
					{
						goto IL_590;
					}
					if (c != 'f')
					{
						goto IL_660;
					}
					goto IL_449;
				}
				else
				{
					if (c == 'n')
					{
						goto IL_301;
					}
					if (c != 't')
					{
						goto IL_660;
					}
					goto IL_449;
				}
				IL_680:
				BigInteger i = default(BigInteger);
				continue;
				IL_660:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_21;
				}
				goto IL_680;
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_273:
			await this.ParseStringAsync(c, ReadType.Read, cancellationToken).ConfigureAwait(false);
			return base.ReadBooleanString(this._stringReference.ToString());
			IL_301:
			await this.HandleNullAsync(cancellationToken).ConfigureAwait(false);
			return null;
			IL_37C:
			await this.ParseNumberAsync(ReadType.Read, cancellationToken).ConfigureAwait(false);
			object value = this.Value;
			bool flag;
			if (value is BigInteger)
			{
				BigInteger i = (BigInteger)value;
				flag = i != 0L;
			}
			else
			{
				flag = Convert.ToBoolean(this.Value, CultureInfo.InvariantCulture);
			}
			base.SetToken(JsonToken.Boolean, flag, false);
			return new bool?(flag);
			IL_449:
			bool isTrue = c == 't';
			configuredTaskAwaiter = this.MatchValueWithTrailingSeparatorAsync(isTrue ? JsonConvert.True : JsonConvert.False, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.Boolean, isTrue);
			return new bool?(isTrue);
			IL_590:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_21:
			throw this.CreateUnexpectedCharacterException(c);
			IL_706:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006CB7 File Offset: 0x00004EB7
		[return: Nullable(new byte[] { 1, 2 })]
		public override Task<byte[]> ReadAsBytesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsBytesAsync(cancellationToken);
			}
			return this.DoReadAsBytesAsync(cancellationToken);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006CD0 File Offset: 0x00004ED0
		[return: Nullable(new byte[] { 1, 2 })]
		internal async Task<byte[]> DoReadAsBytesAsync(CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			bool isWrapped = false;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_6E8;
			case JsonReader.State.PostValue:
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					return null;
				}
				break;
			}
			case JsonReader.State.Finished:
				await this.ReadFinishedAsync(cancellationToken).ConfigureAwait(false);
				return null;
			default:
				goto IL_6E8;
			}
			char c;
			byte[] data;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '\'')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								goto IL_66C;
							case '\v':
							case '\f':
								goto IL_64C;
							case '\r':
								await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
								goto IL_66C;
							default:
								goto IL_64C;
							}
						}
						else
						{
							ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								await configuredTaskAwaiter;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							}
							if (configuredTaskAwaiter.GetResult())
							{
								break;
							}
							goto IL_66C;
						}
					}
					else if (c != ' ')
					{
						if (c != '"' && c != '\'')
						{
							goto IL_64C;
						}
						goto IL_235;
					}
					this._charPos++;
				}
				else if (c <= '[')
				{
					if (c != ',')
					{
						if (c != '/')
						{
							if (c != '[')
							{
								goto IL_64C;
							}
							goto IL_405;
						}
						else
						{
							await this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false);
						}
					}
					else
					{
						this.ProcessValueComma();
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_582;
					}
					if (c == 'n')
					{
						goto IL_48E;
					}
					if (c != '{')
					{
						goto IL_64C;
					}
					this._charPos++;
					base.SetToken(JsonToken.StartObject);
					await this.ReadIntoWrappedTypeObjectAsync(cancellationToken).ConfigureAwait(false);
					isWrapped = true;
				}
				IL_66C:
				data = null;
				continue;
				IL_64C:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_24;
				}
				goto IL_66C;
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_235:
			await this.ParseStringAsync(c, ReadType.ReadAsBytes, cancellationToken).ConfigureAwait(false);
			data = (byte[])this.Value;
			if (isWrapped)
			{
				await base.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false);
				if (this.TokenType != JsonToken.EndObject)
				{
					throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
				}
				base.SetToken(JsonToken.Bytes, data, false);
			}
			return data;
			IL_405:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return await base.ReadArrayIntoByteArrayAsync(cancellationToken).ConfigureAwait(false);
			IL_48E:
			await this.HandleNullAsync(cancellationToken).ConfigureAwait(false);
			return null;
			IL_582:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_24:
			throw this.CreateUnexpectedCharacterException(c);
			IL_6E8:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00006D1C File Offset: 0x00004F1C
		private async Task ReadIntoWrappedTypeObjectAsync(CancellationToken cancellationToken)
		{
			await base.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false);
			if (this.Value != null && this.Value.ToString() == "$type")
			{
				await base.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false);
				if (this.Value != null && this.Value.ToString().StartsWith("System.Byte[]", StringComparison.Ordinal))
				{
					await base.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false);
					if (this.Value.ToString() == "$value")
					{
						return;
					}
				}
			}
			throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, JsonToken.StartObject));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00006D67 File Offset: 0x00004F67
		public override Task<DateTime?> ReadAsDateTimeAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsDateTimeAsync(cancellationToken);
			}
			return this.DoReadAsDateTimeAsync(cancellationToken);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006D80 File Offset: 0x00004F80
		internal async Task<DateTime?> DoReadAsDateTimeAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadStringValueAsync(ReadType.ReadAsDateTime, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (DateTime?)configuredTaskAwaiter.GetResult();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006DCB File Offset: 0x00004FCB
		public override Task<DateTimeOffset?> ReadAsDateTimeOffsetAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsDateTimeOffsetAsync(cancellationToken);
			}
			return this.DoReadAsDateTimeOffsetAsync(cancellationToken);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006DE4 File Offset: 0x00004FE4
		internal async Task<DateTimeOffset?> DoReadAsDateTimeOffsetAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadStringValueAsync(ReadType.ReadAsDateTimeOffset, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (DateTimeOffset?)configuredTaskAwaiter.GetResult();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006E2F File Offset: 0x0000502F
		public override Task<decimal?> ReadAsDecimalAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsDecimalAsync(cancellationToken);
			}
			return this.DoReadAsDecimalAsync(cancellationToken);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006E48 File Offset: 0x00005048
		internal async Task<decimal?> DoReadAsDecimalAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadNumberValueAsync(ReadType.ReadAsDecimal, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (decimal?)configuredTaskAwaiter.GetResult();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006E93 File Offset: 0x00005093
		public override Task<double?> ReadAsDoubleAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsDoubleAsync(cancellationToken);
			}
			return this.DoReadAsDoubleAsync(cancellationToken);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006EAC File Offset: 0x000050AC
		internal async Task<double?> DoReadAsDoubleAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadNumberValueAsync(ReadType.ReadAsDouble, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (double?)configuredTaskAwaiter.GetResult();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006EF7 File Offset: 0x000050F7
		public override Task<int?> ReadAsInt32Async(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsInt32Async(cancellationToken);
			}
			return this.DoReadAsInt32Async(cancellationToken);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006F10 File Offset: 0x00005110
		internal async Task<int?> DoReadAsInt32Async(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadNumberValueAsync(ReadType.ReadAsInt32, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (int?)configuredTaskAwaiter.GetResult();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006F5B File Offset: 0x0000515B
		[return: Nullable(new byte[] { 1, 2 })]
		public override Task<string> ReadAsStringAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsStringAsync(cancellationToken);
			}
			return this.DoReadAsStringAsync(cancellationToken);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006F74 File Offset: 0x00005174
		[return: Nullable(new byte[] { 1, 2 })]
		internal async Task<string> DoReadAsStringAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadStringValueAsync(ReadType.ReadAsString, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (string)configuredTaskAwaiter.GetResult();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006FBF File Offset: 0x000051BF
		public JsonTextReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this._reader = reader;
			this._lineNumber = 1;
			this._safeAsync = base.GetType() == typeof(JsonTextReader);
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00006FFE File Offset: 0x000051FE
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00007006 File Offset: 0x00005206
		[Nullable(2)]
		public JsonNameTable PropertyNameTable
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000700F File Offset: 0x0000520F
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00007017 File Offset: 0x00005217
		[Nullable(2)]
		public IArrayPool<char> ArrayPool
		{
			[NullableContext(2)]
			get
			{
				return this._arrayPool;
			}
			[NullableContext(2)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._arrayPool = value;
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000702E File Offset: 0x0000522E
		private void EnsureBufferNotEmpty()
		{
			if (this._stringBuffer.IsEmpty)
			{
				this._stringBuffer = new StringBuffer(this._arrayPool, 1024);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007053 File Offset: 0x00005253
		private void SetNewLine(bool hasNextChar)
		{
			if (hasNextChar && this._chars[this._charPos] == '\n')
			{
				this._charPos++;
			}
			this.OnNewLine(this._charPos);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007083 File Offset: 0x00005283
		private void OnNewLine(int pos)
		{
			this._lineNumber++;
			this._lineStartPos = pos;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000709A File Offset: 0x0000529A
		private void ParseString(char quote, ReadType readType)
		{
			this._charPos++;
			this.ShiftBufferIfNeeded();
			this.ReadStringIntoBuffer(quote);
			this.ParseReadString(quote, readType);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000070C0 File Offset: 0x000052C0
		private void ParseReadString(char quote, ReadType readType)
		{
			base.SetPostValueState(true);
			switch (readType)
			{
			case ReadType.ReadAsInt32:
			case ReadType.ReadAsDecimal:
			case ReadType.ReadAsBoolean:
				return;
			case ReadType.ReadAsBytes:
			{
				byte[] array;
				Guid guid;
				if (this._stringReference.Length == 0)
				{
					array = CollectionUtils.ArrayEmpty<byte>();
				}
				else if (this._stringReference.Length == 36 && ConvertUtils.TryConvertGuid(this._stringReference.ToString(), out guid))
				{
					array = guid.ToByteArray();
				}
				else
				{
					array = Convert.FromBase64CharArray(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length);
				}
				base.SetToken(JsonToken.Bytes, array, false);
				return;
			}
			case ReadType.ReadAsString:
			{
				string text = this._stringReference.ToString();
				base.SetToken(JsonToken.String, text, false);
				this._quoteChar = quote;
				return;
			}
			}
			if (this._dateParseHandling != DateParseHandling.None)
			{
				DateParseHandling dateParseHandling;
				if (readType == ReadType.ReadAsDateTime)
				{
					dateParseHandling = DateParseHandling.DateTime;
				}
				else if (readType == ReadType.ReadAsDateTimeOffset)
				{
					dateParseHandling = DateParseHandling.DateTimeOffset;
				}
				else
				{
					dateParseHandling = this._dateParseHandling;
				}
				DateTimeOffset dateTimeOffset;
				if (dateParseHandling == DateParseHandling.DateTime)
				{
					DateTime dateTime;
					if (DateTimeUtils.TryParseDateTime(this._stringReference, base.DateTimeZoneHandling, base.DateFormatString, base.Culture, out dateTime))
					{
						base.SetToken(JsonToken.Date, dateTime, false);
						return;
					}
				}
				else if (DateTimeUtils.TryParseDateTimeOffset(this._stringReference, base.DateFormatString, base.Culture, out dateTimeOffset))
				{
					base.SetToken(JsonToken.Date, dateTimeOffset, false);
					return;
				}
			}
			base.SetToken(JsonToken.String, this._stringReference.ToString(), false);
			this._quoteChar = quote;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007245 File Offset: 0x00005445
		private static void BlockCopyChars(char[] src, int srcOffset, char[] dst, int dstOffset, int count)
		{
			Buffer.BlockCopy(src, srcOffset * 2, dst, dstOffset * 2, count * 2);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007258 File Offset: 0x00005458
		private void ShiftBufferIfNeeded()
		{
			int num = this._chars.Length;
			if ((double)(num - this._charPos) <= (double)num * 0.1 || num >= 1073741823)
			{
				int num2 = this._charsUsed - this._charPos;
				if (num2 > 0)
				{
					JsonTextReader.BlockCopyChars(this._chars, this._charPos, this._chars, 0, num2);
				}
				this._lineStartPos -= this._charPos;
				this._charPos = 0;
				this._charsUsed = num2;
				this._chars[this._charsUsed] = '\0';
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000072E7 File Offset: 0x000054E7
		private int ReadData(bool append)
		{
			return this.ReadData(append, 0);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000072F4 File Offset: 0x000054F4
		private void PrepareBufferForReadData(bool append, int charsRequired)
		{
			if (this._charsUsed + charsRequired >= this._chars.Length - 1)
			{
				if (append)
				{
					int num = this._chars.Length * 2;
					int num2 = Math.Max((num < 0) ? int.MaxValue : num, this._charsUsed + charsRequired + 1);
					char[] array = BufferUtils.RentBuffer(this._arrayPool, num2);
					JsonTextReader.BlockCopyChars(this._chars, 0, array, 0, this._chars.Length);
					BufferUtils.ReturnBuffer(this._arrayPool, this._chars);
					this._chars = array;
					return;
				}
				int num3 = this._charsUsed - this._charPos;
				if (num3 + charsRequired + 1 >= this._chars.Length)
				{
					char[] array2 = BufferUtils.RentBuffer(this._arrayPool, num3 + charsRequired + 1);
					if (num3 > 0)
					{
						JsonTextReader.BlockCopyChars(this._chars, this._charPos, array2, 0, num3);
					}
					BufferUtils.ReturnBuffer(this._arrayPool, this._chars);
					this._chars = array2;
				}
				else if (num3 > 0)
				{
					JsonTextReader.BlockCopyChars(this._chars, this._charPos, this._chars, 0, num3);
				}
				this._lineStartPos -= this._charPos;
				this._charPos = 0;
				this._charsUsed = num3;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007420 File Offset: 0x00005620
		private int ReadData(bool append, int charsRequired)
		{
			if (this._isEndOfFile)
			{
				return 0;
			}
			this.PrepareBufferForReadData(append, charsRequired);
			int num = this._chars.Length - this._charsUsed - 1;
			int num2 = this._reader.Read(this._chars, this._charsUsed, num);
			this._charsUsed += num2;
			if (num2 == 0)
			{
				this._isEndOfFile = true;
			}
			this._chars[this._charsUsed] = '\0';
			return num2;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007491 File Offset: 0x00005691
		private bool EnsureChars(int relativePosition, bool append)
		{
			return this._charPos + relativePosition < this._charsUsed || this.ReadChars(relativePosition, append);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000074B0 File Offset: 0x000056B0
		private bool ReadChars(int relativePosition, bool append)
		{
			if (this._isEndOfFile)
			{
				return false;
			}
			int num = this._charPos + relativePosition - this._charsUsed + 1;
			int num2 = 0;
			do
			{
				int num3 = this.ReadData(append, num - num2);
				if (num3 == 0)
				{
					break;
				}
				num2 += num3;
			}
			while (num2 < num);
			return num2 >= num;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000074F8 File Offset: 0x000056F8
		public override bool Read()
		{
			this.EnsureBuffer();
			for (;;)
			{
				switch (this._currentState)
				{
				case JsonReader.State.Start:
				case JsonReader.State.Property:
				case JsonReader.State.ArrayStart:
				case JsonReader.State.Array:
				case JsonReader.State.ConstructorStart:
				case JsonReader.State.Constructor:
					goto IL_4C;
				case JsonReader.State.ObjectStart:
				case JsonReader.State.Object:
					goto IL_53;
				case JsonReader.State.PostValue:
					if (this.ParsePostValue(false))
					{
						return true;
					}
					continue;
				case JsonReader.State.Finished:
					goto IL_65;
				}
				break;
			}
			goto IL_D1;
			IL_4C:
			return this.ParseValue();
			IL_53:
			return this.ParseObject();
			IL_65:
			if (!this.EnsureChars(0, false))
			{
				base.SetToken(JsonToken.None);
				return false;
			}
			this.EatWhitespace();
			if (this._isEndOfFile)
			{
				base.SetToken(JsonToken.None);
				return false;
			}
			if (this._chars[this._charPos] == '/')
			{
				this.ParseComment(true);
				return true;
			}
			throw JsonReaderException.Create(this, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			IL_D1:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000075F6 File Offset: 0x000057F6
		public override int? ReadAsInt32()
		{
			return (int?)this.ReadNumberValue(ReadType.ReadAsInt32);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007604 File Offset: 0x00005804
		public override DateTime? ReadAsDateTime()
		{
			return (DateTime?)this.ReadStringValue(ReadType.ReadAsDateTime);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007612 File Offset: 0x00005812
		[NullableContext(2)]
		public override string ReadAsString()
		{
			return (string)this.ReadStringValue(ReadType.ReadAsString);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007620 File Offset: 0x00005820
		[NullableContext(2)]
		public override byte[] ReadAsBytes()
		{
			this.EnsureBuffer();
			bool flag = false;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_23E;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_23E;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '\'')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_215;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_215;
							}
						}
						else
						{
							if (this.ReadNullChar())
							{
								break;
							}
							continue;
						}
					}
					else if (c != ' ')
					{
						if (c != '"' && c != '\'')
						{
							goto IL_215;
						}
						goto IL_FF;
					}
					this._charPos++;
					continue;
				}
				if (c <= '[')
				{
					if (c == ',')
					{
						this.ProcessValueComma();
						continue;
					}
					if (c == '/')
					{
						this.ParseComment(false);
						continue;
					}
					if (c == '[')
					{
						goto IL_175;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_1B0;
					}
					if (c == 'n')
					{
						goto IL_191;
					}
					if (c == '{')
					{
						this._charPos++;
						base.SetToken(JsonToken.StartObject);
						base.ReadIntoWrappedTypeObject();
						flag = true;
						continue;
					}
				}
				IL_215:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_22;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_FF:
			this.ParseString(c, ReadType.ReadAsBytes);
			byte[] array = (byte[])this.Value;
			if (flag)
			{
				base.ReaderReadAndAssert();
				if (this.TokenType != JsonToken.EndObject)
				{
					throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
				}
				base.SetToken(JsonToken.Bytes, array, false);
			}
			return array;
			IL_175:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return base.ReadArrayIntoByteArray();
			IL_191:
			this.HandleNull();
			return null;
			IL_1B0:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_22:
			throw this.CreateUnexpectedCharacterException(c);
			IL_23E:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000788C File Offset: 0x00005A8C
		[NullableContext(2)]
		private object ReadStringValue(ReadType readType)
		{
			this.EnsureBuffer();
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_2E1;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_2E1;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= 'I')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_2B8;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_2B8;
							}
						}
						else
						{
							if (this.ReadNullChar())
							{
								break;
							}
							continue;
						}
					}
					else
					{
						switch (c)
						{
						case ' ':
							break;
						case '!':
						case '#':
						case '$':
						case '%':
						case '&':
						case '(':
						case ')':
						case '*':
						case '+':
							goto IL_2B8;
						case '"':
						case '\'':
							goto IL_165;
						case ',':
							this.ProcessValueComma();
							continue;
						case '-':
							goto IL_175;
						case '.':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							goto IL_1A8;
						case '/':
							this.ParseComment(false);
							continue;
						default:
							if (c != 'I')
							{
								goto IL_2B8;
							}
							goto IL_224;
						}
					}
					this._charPos++;
					continue;
				}
				if (c <= ']')
				{
					if (c == 'N')
					{
						goto IL_22C;
					}
					if (c == ']')
					{
						goto IL_253;
					}
				}
				else
				{
					if (c == 'f')
					{
						goto IL_1D0;
					}
					if (c == 'n')
					{
						goto IL_234;
					}
					if (c == 't')
					{
						goto IL_1D0;
					}
				}
				IL_2B8:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_24;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_165:
			this.ParseString(c, readType);
			return this.FinishReadQuotedStringValue(readType);
			IL_175:
			if (this.EnsureChars(1, true) && this._chars[this._charPos + 1] == 'I')
			{
				return this.ParseNumberNegativeInfinity(readType);
			}
			this.ParseNumber(readType);
			return this.Value;
			IL_1A8:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			this.ParseNumber(ReadType.ReadAsString);
			return this.Value;
			IL_1D0:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			string text = ((c == 't') ? JsonConvert.True : JsonConvert.False);
			if (!this.MatchValueWithTrailingSeparator(text))
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.String, text);
			return text;
			IL_224:
			return this.ParseNumberPositiveInfinity(readType);
			IL_22C:
			return this.ParseNumberNaN(readType);
			IL_234:
			this.HandleNull();
			return null;
			IL_253:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_24:
			throw this.CreateUnexpectedCharacterException(c);
			IL_2E1:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007B9C File Offset: 0x00005D9C
		[NullableContext(2)]
		private object FinishReadQuotedStringValue(ReadType readType)
		{
			switch (readType)
			{
			case ReadType.ReadAsBytes:
			case ReadType.ReadAsString:
				return this.Value;
			case ReadType.ReadAsDateTime:
			{
				object obj = this.Value;
				if (obj is DateTime)
				{
					DateTime dateTime = (DateTime)obj;
					return dateTime;
				}
				return base.ReadDateTimeString((string)this.Value);
			}
			case ReadType.ReadAsDateTimeOffset:
			{
				object obj = this.Value;
				if (obj is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)obj;
					return dateTimeOffset;
				}
				return base.ReadDateTimeOffsetString((string)this.Value);
			}
			}
			throw new ArgumentOutOfRangeException("readType");
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007C40 File Offset: 0x00005E40
		private JsonReaderException CreateUnexpectedCharacterException(char c)
		{
			return JsonReaderException.Create(this, "Unexpected character encountered while parsing value: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007C60 File Offset: 0x00005E60
		public override bool? ReadAsBoolean()
		{
			this.EnsureBuffer();
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_2DF;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_2DF;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_2AE;
						case '\r':
							this.ProcessCarriageReturn(false);
							continue;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_2AE;
							case '"':
							case '\'':
								goto IL_158;
							case ',':
								this.ProcessValueComma();
								continue;
							case '-':
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_188;
							case '/':
								this.ParseComment(false);
								continue;
							default:
								goto IL_2AE;
							}
							break;
						}
						this._charPos++;
						continue;
					}
					if (this.ReadNullChar())
					{
						break;
					}
					continue;
				}
				else if (c <= 'f')
				{
					if (c == ']')
					{
						goto IL_241;
					}
					if (c == 'f')
					{
						goto IL_1DC;
					}
				}
				else
				{
					if (c == 'n')
					{
						goto IL_178;
					}
					if (c == 't')
					{
						goto IL_1DC;
					}
				}
				IL_2AE:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_18;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_158:
			this.ParseString(c, ReadType.Read);
			return base.ReadBooleanString(this._stringReference.ToString());
			IL_178:
			this.HandleNull();
			return null;
			IL_188:
			this.ParseNumber(ReadType.Read);
			object value = this.Value;
			bool flag;
			if (value is BigInteger)
			{
				BigInteger bigInteger = (BigInteger)value;
				flag = bigInteger != 0L;
			}
			else
			{
				flag = Convert.ToBoolean(this.Value, CultureInfo.InvariantCulture);
			}
			base.SetToken(JsonToken.Boolean, flag, false);
			return new bool?(flag);
			IL_1DC:
			bool flag2 = c == 't';
			string text = (flag2 ? JsonConvert.True : JsonConvert.False);
			if (!this.MatchValueWithTrailingSeparator(text))
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.Boolean, flag2);
			return new bool?(flag2);
			IL_241:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_18:
			throw this.CreateUnexpectedCharacterException(c);
			IL_2DF:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00007F6C File Offset: 0x0000616C
		private void ProcessValueComma()
		{
			this._charPos++;
			if (this._currentState != JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.Undefined);
				object obj = this.CreateUnexpectedCharacterException(',');
				this._charPos--;
				throw obj;
			}
			base.SetStateBasedOnCurrent();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00007FAC File Offset: 0x000061AC
		[NullableContext(2)]
		private object ReadNumberValue(ReadType readType)
		{
			this.EnsureBuffer();
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_250;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_250;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_227;
						case '\r':
							this.ProcessCarriageReturn(false);
							continue;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_227;
							case '"':
							case '\'':
								goto IL_142;
							case ',':
								this.ProcessValueComma();
								continue;
							case '-':
								goto IL_16A;
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_19D;
							case '/':
								this.ParseComment(false);
								continue;
							default:
								goto IL_227;
							}
							break;
						}
						this._charPos++;
						continue;
					}
					if (this.ReadNullChar())
					{
						break;
					}
					continue;
				}
				else if (c <= 'N')
				{
					if (c == 'I')
					{
						goto IL_162;
					}
					if (c == 'N')
					{
						goto IL_15A;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_1C2;
					}
					if (c == 'n')
					{
						goto IL_152;
					}
				}
				IL_227:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_17;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_142:
			this.ParseString(c, readType);
			return this.FinishReadQuotedNumber(readType);
			IL_152:
			this.HandleNull();
			return null;
			IL_15A:
			return this.ParseNumberNaN(readType);
			IL_162:
			return this.ParseNumberPositiveInfinity(readType);
			IL_16A:
			if (this.EnsureChars(1, true) && this._chars[this._charPos + 1] == 'I')
			{
				return this.ParseNumberNegativeInfinity(readType);
			}
			this.ParseNumber(readType);
			return this.Value;
			IL_19D:
			this.ParseNumber(readType);
			return this.Value;
			IL_1C2:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_17:
			throw this.CreateUnexpectedCharacterException(c);
			IL_250:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000822C File Offset: 0x0000642C
		[NullableContext(2)]
		private object FinishReadQuotedNumber(ReadType readType)
		{
			if (readType == ReadType.ReadAsInt32)
			{
				return base.ReadInt32String(this._stringReference.ToString());
			}
			if (readType == ReadType.ReadAsDecimal)
			{
				return base.ReadDecimalString(this._stringReference.ToString());
			}
			if (readType != ReadType.ReadAsDouble)
			{
				throw new ArgumentOutOfRangeException("readType");
			}
			return base.ReadDoubleString(this._stringReference.ToString());
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000082A8 File Offset: 0x000064A8
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			return (DateTimeOffset?)this.ReadStringValue(ReadType.ReadAsDateTimeOffset);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000082B6 File Offset: 0x000064B6
		public override decimal? ReadAsDecimal()
		{
			return (decimal?)this.ReadNumberValue(ReadType.ReadAsDecimal);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000082C4 File Offset: 0x000064C4
		public override double? ReadAsDouble()
		{
			return (double?)this.ReadNumberValue(ReadType.ReadAsDouble);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000082D4 File Offset: 0x000064D4
		private void HandleNull()
		{
			if (!this.EnsureChars(1, true))
			{
				this._charPos = this._charsUsed;
				throw base.CreateUnexpectedEndException();
			}
			if (this._chars[this._charPos + 1] == 'u')
			{
				this.ParseNull();
				return;
			}
			this._charPos += 2;
			throw this.CreateUnexpectedCharacterException(this._chars[this._charPos - 1]);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000833C File Offset: 0x0000653C
		private void ReadFinished()
		{
			if (this.EnsureChars(0, false))
			{
				this.EatWhitespace();
				if (this._isEndOfFile)
				{
					return;
				}
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				this.ParseComment(false);
			}
			base.SetToken(JsonToken.None);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000083AB File Offset: 0x000065AB
		private bool ReadNullChar()
		{
			if (this._charsUsed == this._charPos)
			{
				if (this.ReadData(false) == 0)
				{
					this._isEndOfFile = true;
					return true;
				}
			}
			else
			{
				this._charPos++;
			}
			return false;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000083DC File Offset: 0x000065DC
		private void EnsureBuffer()
		{
			if (this._chars == null)
			{
				this._chars = BufferUtils.RentBuffer(this._arrayPool, 1024);
				this._chars[0] = '\0';
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008408 File Offset: 0x00006608
		private void ReadStringIntoBuffer(char quote)
		{
			int num = this._charPos;
			int charPos = this._charPos;
			int num2 = this._charPos;
			this._stringBuffer.Position = 0;
			char c2;
			for (;;)
			{
				char c = this._chars[num++];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						if (c != '\n')
						{
							if (c == '\r')
							{
								this._charPos = num - 1;
								this.ProcessCarriageReturn(true);
								num = this._charPos;
							}
						}
						else
						{
							this._charPos = num - 1;
							this.ProcessLineFeed();
							num = this._charPos;
						}
					}
					else if (this._charsUsed == num - 1)
					{
						num--;
						if (this.ReadData(true) == 0)
						{
							break;
						}
					}
				}
				else if (c != '"' && c != '\'')
				{
					if (c == '\\')
					{
						this._charPos = num;
						if (!this.EnsureChars(0, true))
						{
							goto Block_10;
						}
						int num3 = num - 1;
						c2 = this._chars[num];
						num++;
						char c3;
						if (c2 <= '\\')
						{
							if (c2 <= '\'')
							{
								if (c2 != '"' && c2 != '\'')
								{
									goto Block_14;
								}
							}
							else if (c2 != '/')
							{
								if (c2 != '\\')
								{
									goto Block_16;
								}
								c3 = '\\';
								goto IL_28D;
							}
							c3 = c2;
						}
						else if (c2 <= 'f')
						{
							if (c2 != 'b')
							{
								if (c2 != 'f')
								{
									goto Block_19;
								}
								c3 = '\f';
							}
							else
							{
								c3 = '\b';
							}
						}
						else
						{
							if (c2 != 'n')
							{
								switch (c2)
								{
								case 'r':
									c3 = '\r';
									goto IL_28D;
								case 't':
									c3 = '\t';
									goto IL_28D;
								case 'u':
									this._charPos = num;
									c3 = this.ParseUnicode();
									if (StringUtils.IsLowSurrogate(c3))
									{
										c3 = '\ufffd';
									}
									else if (StringUtils.IsHighSurrogate(c3))
									{
										bool flag;
										do
										{
											flag = false;
											if (this.EnsureChars(2, true) && this._chars[this._charPos] == '\\' && this._chars[this._charPos + 1] == 'u')
											{
												char c4 = c3;
												this._charPos += 2;
												c3 = this.ParseUnicode();
												if (!StringUtils.IsLowSurrogate(c3))
												{
													if (StringUtils.IsHighSurrogate(c3))
													{
														c4 = '\ufffd';
														flag = true;
													}
													else
													{
														c4 = '\ufffd';
													}
												}
												this.EnsureBufferNotEmpty();
												this.WriteCharToBuffer(c4, num2, num3);
												num2 = this._charPos;
											}
											else
											{
												c3 = '\ufffd';
											}
										}
										while (flag);
									}
									num = this._charPos;
									goto IL_28D;
								}
								goto Block_21;
							}
							c3 = '\n';
						}
						IL_28D:
						this.EnsureBufferNotEmpty();
						this.WriteCharToBuffer(c3, num2, num3);
						num2 = num;
					}
				}
				else if (this._chars[num - 1] == quote)
				{
					goto Block_28;
				}
			}
			this._charPos = num;
			throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
			Block_10:
			throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
			Block_14:
			Block_16:
			Block_19:
			Block_21:
			this._charPos = num;
			throw JsonReaderException.Create(this, "Bad JSON escape sequence: {0}.".FormatWith(CultureInfo.InvariantCulture, "\\" + c2.ToString()));
			Block_28:
			this.FinishReadStringIntoBuffer(num - 1, charPos, num2);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000870C File Offset: 0x0000690C
		private void FinishReadStringIntoBuffer(int charPos, int initialPosition, int lastWritePosition)
		{
			if (initialPosition == lastWritePosition)
			{
				this._stringReference = new StringReference(this._chars, initialPosition, charPos - initialPosition);
			}
			else
			{
				this.EnsureBufferNotEmpty();
				if (charPos > lastWritePosition)
				{
					this._stringBuffer.Append(this._arrayPool, this._chars, lastWritePosition, charPos - lastWritePosition);
				}
				this._stringReference = new StringReference(this._stringBuffer.InternalBuffer, 0, this._stringBuffer.Position);
			}
			this._charPos = charPos + 1;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008784 File Offset: 0x00006984
		private void WriteCharToBuffer(char writeChar, int lastWritePosition, int writeToPosition)
		{
			if (writeToPosition > lastWritePosition)
			{
				this._stringBuffer.Append(this._arrayPool, this._chars, lastWritePosition, writeToPosition - lastWritePosition);
			}
			this._stringBuffer.Append(this._arrayPool, writeChar);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000087B8 File Offset: 0x000069B8
		private char ConvertUnicode(bool enoughChars)
		{
			if (!enoughChars)
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing Unicode escape sequence.");
			}
			int num;
			if (ConvertUtils.TryHexTextToInt(this._chars, this._charPos, this._charPos + 4, out num))
			{
				char c = Convert.ToChar(num);
				this._charPos += 4;
				return c;
			}
			throw JsonReaderException.Create(this, "Invalid Unicode escape sequence: \\u{0}.".FormatWith(CultureInfo.InvariantCulture, new string(this._chars, this._charPos, 4)));
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000882D File Offset: 0x00006A2D
		private char ParseUnicode()
		{
			return this.ConvertUnicode(this.EnsureChars(4, true));
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00008840 File Offset: 0x00006A40
		private void ReadNumberIntoBuffer()
		{
			int num = this._charPos;
			for (;;)
			{
				char c = this._chars[num];
				if (c == '\0')
				{
					this._charPos = num;
					if (this._charsUsed != num)
					{
						return;
					}
					if (this.ReadData(true) == 0)
					{
						break;
					}
				}
				else
				{
					if (this.ReadNumberCharIntoBuffer(c, num))
					{
						return;
					}
					num++;
				}
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000888C File Offset: 0x00006A8C
		private bool ReadNumberCharIntoBuffer(char currentChar, int charPos)
		{
			if (currentChar <= 'X')
			{
				switch (currentChar)
				{
				case '+':
				case '-':
				case '.':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
					break;
				case ',':
				case '/':
				case ':':
				case ';':
				case '<':
				case '=':
				case '>':
				case '?':
				case '@':
					goto IL_B0;
				default:
					if (currentChar != 'X')
					{
						goto IL_B0;
					}
					break;
				}
			}
			else
			{
				switch (currentChar)
				{
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
					break;
				default:
					if (currentChar != 'x')
					{
						goto IL_B0;
					}
					break;
				}
			}
			return false;
			IL_B0:
			this._charPos = charPos;
			if (char.IsWhiteSpace(currentChar) || currentChar == ',' || currentChar == '}' || currentChar == ']' || currentChar == ')' || currentChar == '/')
			{
				return true;
			}
			throw JsonReaderException.Create(this, "Unexpected character encountered while parsing number: {0}.".FormatWith(CultureInfo.InvariantCulture, currentChar));
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000898E File Offset: 0x00006B8E
		private void ClearRecentString()
		{
			this._stringBuffer.Position = 0;
			this._stringReference = default(StringReference);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000089A8 File Offset: 0x00006BA8
		private bool ParsePostValue(bool ignoreComments)
		{
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= ')')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_14C;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_14C;
							}
						}
						else
						{
							if (this._charsUsed != this._charPos)
							{
								this._charPos++;
								continue;
							}
							if (this.ReadData(false) == 0)
							{
								break;
							}
							continue;
						}
					}
					else if (c != ' ')
					{
						if (c != ')')
						{
							goto IL_14C;
						}
						goto IL_E2;
					}
					this._charPos++;
					continue;
				}
				if (c <= '/')
				{
					if (c == ',')
					{
						goto IL_10C;
					}
					if (c == '/')
					{
						this.ParseComment(!ignoreComments);
						if (!ignoreComments)
						{
							return true;
						}
						continue;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_CA;
					}
					if (c == '}')
					{
						goto IL_B2;
					}
				}
				IL_14C:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_167;
				}
				this._charPos++;
			}
			this._currentState = JsonReader.State.Finished;
			return false;
			IL_B2:
			this._charPos++;
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_CA:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_E2:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_10C:
			this._charPos++;
			base.SetStateBasedOnCurrent();
			return false;
			IL_167:
			if (base.SupportMultipleContent && this.Depth == 0)
			{
				base.SetStateBasedOnCurrent();
				return false;
			}
			throw JsonReaderException.Create(this, "After parsing a value an unexpected character was encountered: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008B50 File Offset: 0x00006D50
		private bool ParseObject()
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_BD;
						case '\r':
							this.ProcessCarriageReturn(false);
							continue;
						default:
							goto IL_BD;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						if (this.ReadData(false) == 0)
						{
							break;
						}
						continue;
					}
				}
				else if (c != ' ')
				{
					if (c == '/')
					{
						goto IL_8A;
					}
					if (c != '}')
					{
						goto IL_BD;
					}
					goto IL_72;
				}
				this._charPos++;
				continue;
				IL_BD:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_D8;
				}
				this._charPos++;
			}
			return false;
			IL_72:
			base.SetToken(JsonToken.EndObject);
			this._charPos++;
			return true;
			IL_8A:
			this.ParseComment(true);
			return true;
			IL_D8:
			return this.ParseProperty();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00008C3C File Offset: 0x00006E3C
		private bool ParseProperty()
		{
			char c = this._chars[this._charPos];
			char c2;
			if (c == '"' || c == '\'')
			{
				this._charPos++;
				c2 = c;
				this.ShiftBufferIfNeeded();
				this.ReadStringIntoBuffer(c2);
			}
			else
			{
				if (!this.ValidIdentifierChar(c))
				{
					throw JsonReaderException.Create(this, "Invalid property identifier character: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				c2 = '\0';
				this.ShiftBufferIfNeeded();
				this.ParseUnquotedProperty();
			}
			string text;
			if (this.PropertyNameTable != null)
			{
				text = this.PropertyNameTable.Get(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length);
				if (text == null)
				{
					text = this._stringReference.ToString();
				}
			}
			else
			{
				text = this._stringReference.ToString();
			}
			this.EatWhitespace();
			if (this._chars[this._charPos] != ':')
			{
				throw JsonReaderException.Create(this, "Invalid character after parsing property name. Expected ':' but got: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			base.SetToken(JsonToken.PropertyName, text);
			this._quoteChar = c2;
			this.ClearRecentString();
			return true;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008D82 File Offset: 0x00006F82
		private bool ValidIdentifierChar(char value)
		{
			return char.IsLetterOrDigit(value) || value == '_' || value == '$';
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00008D98 File Offset: 0x00006F98
		private void ParseUnquotedProperty()
		{
			int charPos = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_3B;
					}
					if (this.ReadData(true) == 0)
					{
						break;
					}
				}
				else if (this.ReadUnquotedPropertyReportIfDone(c, charPos))
				{
					return;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing unquoted property name.");
			IL_3B:
			this._stringReference = new StringReference(this._chars, charPos, this._charPos - charPos);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00008E08 File Offset: 0x00007008
		private bool ReadUnquotedPropertyReportIfDone(char currentChar, int initialPosition)
		{
			if (this.ValidIdentifierChar(currentChar))
			{
				this._charPos++;
				return false;
			}
			if (char.IsWhiteSpace(currentChar) || currentChar == ':')
			{
				this._stringReference = new StringReference(this._chars, initialPosition, this._charPos - initialPosition);
				return true;
			}
			throw JsonReaderException.Create(this, "Invalid JavaScript property identifier character: {0}.".FormatWith(CultureInfo.InvariantCulture, currentChar));
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00008E74 File Offset: 0x00007074
		private bool ParseValue()
		{
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= 'N')
				{
					if (c <= ' ')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_276;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								if (c != ' ')
								{
									goto IL_276;
								}
								break;
							}
							this._charPos++;
							continue;
						}
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						if (this.ReadData(false) == 0)
						{
							break;
						}
						continue;
					}
					else if (c <= '/')
					{
						if (c == '"')
						{
							goto IL_116;
						}
						switch (c)
						{
						case '\'':
							goto IL_116;
						case ')':
							goto IL_234;
						case ',':
							goto IL_22A;
						case '-':
							goto IL_1A3;
						case '/':
							goto IL_1D3;
						}
					}
					else
					{
						if (c == 'I')
						{
							goto IL_199;
						}
						if (c == 'N')
						{
							goto IL_18F;
						}
					}
				}
				else if (c <= 'f')
				{
					if (c == '[')
					{
						goto IL_1FB;
					}
					if (c == ']')
					{
						goto IL_212;
					}
					if (c == 'f')
					{
						goto IL_128;
					}
				}
				else if (c <= 't')
				{
					if (c == 'n')
					{
						goto IL_130;
					}
					if (c == 't')
					{
						goto IL_120;
					}
				}
				else
				{
					if (c == 'u')
					{
						goto IL_1DC;
					}
					if (c == '{')
					{
						goto IL_1E4;
					}
				}
				IL_276:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_291;
				}
				this._charPos++;
			}
			return false;
			IL_116:
			this.ParseString(c, ReadType.Read);
			return true;
			IL_120:
			this.ParseTrue();
			return true;
			IL_128:
			this.ParseFalse();
			return true;
			IL_130:
			if (this.EnsureChars(1, true))
			{
				char c2 = this._chars[this._charPos + 1];
				if (c2 == 'u')
				{
					this.ParseNull();
				}
				else
				{
					if (c2 != 'e')
					{
						throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
					}
					this.ParseConstructor();
				}
				return true;
			}
			this._charPos++;
			throw base.CreateUnexpectedEndException();
			IL_18F:
			this.ParseNumberNaN(ReadType.Read);
			return true;
			IL_199:
			this.ParseNumberPositiveInfinity(ReadType.Read);
			return true;
			IL_1A3:
			if (this.EnsureChars(1, true) && this._chars[this._charPos + 1] == 'I')
			{
				this.ParseNumberNegativeInfinity(ReadType.Read);
			}
			else
			{
				this.ParseNumber(ReadType.Read);
			}
			return true;
			IL_1D3:
			this.ParseComment(true);
			return true;
			IL_1DC:
			this.ParseUndefined();
			return true;
			IL_1E4:
			this._charPos++;
			base.SetToken(JsonToken.StartObject);
			return true;
			IL_1FB:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return true;
			IL_212:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_22A:
			base.SetToken(JsonToken.Undefined);
			return true;
			IL_234:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_291:
			if (char.IsNumber(c) || c == '-' || c == '.')
			{
				this.ParseNumber(ReadType.Read);
				return true;
			}
			throw this.CreateUnexpectedCharacterException(c);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009134 File Offset: 0x00007334
		private void ProcessLineFeed()
		{
			this._charPos++;
			this.OnNewLine(this._charPos);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009150 File Offset: 0x00007350
		private void ProcessCarriageReturn(bool append)
		{
			this._charPos++;
			this.SetNewLine(this.EnsureChars(1, append));
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009170 File Offset: 0x00007370
		private void EatWhitespace()
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c != '\0')
				{
					if (c != '\n')
					{
						if (c != '\r')
						{
							if (c != ' ' && !char.IsWhiteSpace(c))
							{
								return;
							}
							this._charPos++;
						}
						else
						{
							this.ProcessCarriageReturn(false);
						}
					}
					else
					{
						this.ProcessLineFeed();
					}
				}
				else if (this._charsUsed == this._charPos)
				{
					if (this.ReadData(false) == 0)
					{
						break;
					}
				}
				else
				{
					this._charPos++;
				}
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000091F0 File Offset: 0x000073F0
		private void ParseConstructor()
		{
			if (!this.MatchValueWithTrailingSeparator("new"))
			{
				throw JsonReaderException.Create(this, "Unexpected content while parsing JSON.");
			}
			this.EatWhitespace();
			int charPos = this._charPos;
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_51;
					}
					if (this.ReadData(true) == 0)
					{
						break;
					}
				}
				else
				{
					if (!char.IsLetterOrDigit(c))
					{
						goto IL_83;
					}
					this._charPos++;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing constructor.");
			IL_51:
			int num = this._charPos;
			this._charPos++;
			goto IL_F5;
			IL_83:
			if (c == '\r')
			{
				num = this._charPos;
				this.ProcessCarriageReturn(true);
			}
			else if (c == '\n')
			{
				num = this._charPos;
				this.ProcessLineFeed();
			}
			else if (char.IsWhiteSpace(c))
			{
				num = this._charPos;
				this._charPos++;
			}
			else
			{
				if (c != '(')
				{
					throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
				}
				num = this._charPos;
			}
			IL_F5:
			this._stringReference = new StringReference(this._chars, charPos, num - charPos);
			string text = this._stringReference.ToString();
			this.EatWhitespace();
			if (this._chars[this._charPos] != '(')
			{
				throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			this.ClearRecentString();
			base.SetToken(JsonToken.StartConstructor, text);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009380 File Offset: 0x00007580
		private void ParseNumber(ReadType readType)
		{
			this.ShiftBufferIfNeeded();
			char c = this._chars[this._charPos];
			int charPos = this._charPos;
			this.ReadNumberIntoBuffer();
			this.ParseReadNumber(readType, c, charPos);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000093B8 File Offset: 0x000075B8
		private void ParseReadNumber(ReadType readType, char firstChar, int initialPosition)
		{
			base.SetPostValueState(true);
			this._stringReference = new StringReference(this._chars, initialPosition, this._charPos - initialPosition);
			bool flag = char.IsDigit(firstChar) && this._stringReference.Length == 1;
			bool flag2 = firstChar == '0' && this._stringReference.Length > 1 && this._stringReference.Chars[this._stringReference.StartIndex + 1] != '.' && this._stringReference.Chars[this._stringReference.StartIndex + 1] != 'e' && this._stringReference.Chars[this._stringReference.StartIndex + 1] != 'E';
			object obj;
			JsonToken jsonToken;
			switch (readType)
			{
			case ReadType.Read:
			case ReadType.ReadAsInt64:
			{
				if (flag)
				{
					obj = (long)((ulong)firstChar - 48UL);
					jsonToken = JsonToken.Integer;
					goto IL_622;
				}
				if (flag2)
				{
					string text = this._stringReference.ToString();
					try
					{
						obj = (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text, 16) : Convert.ToInt64(text, 8));
					}
					catch (Exception ex)
					{
						throw this.ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, text), ex);
					}
					jsonToken = JsonToken.Integer;
					goto IL_622;
				}
				long num;
				ParseResult parseResult = ConvertUtils.Int64TryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num);
				if (parseResult == ParseResult.Success)
				{
					obj = num;
					jsonToken = JsonToken.Integer;
					goto IL_622;
				}
				if (parseResult != ParseResult.Overflow)
				{
					if (this._floatParseHandling == FloatParseHandling.Decimal)
					{
						decimal num2;
						parseResult = ConvertUtils.DecimalTryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num2);
						if (parseResult != ParseResult.Success)
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid decimal.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
						}
						obj = num2;
					}
					else
					{
						double num3;
						if (!double.TryParse(this._stringReference.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num3))
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
						}
						obj = num3;
					}
					jsonToken = JsonToken.Float;
					goto IL_622;
				}
				string text2 = this._stringReference.ToString();
				if (text2.Length > 380)
				{
					throw this.ThrowReaderError("JSON integer {0} is too large to parse.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
				}
				obj = JsonTextReader.BigIntegerParse(text2, CultureInfo.InvariantCulture);
				jsonToken = JsonToken.Integer;
				goto IL_622;
			}
			case ReadType.ReadAsInt32:
				if (flag)
				{
					obj = (int)(firstChar - '0');
				}
				else
				{
					if (flag2)
					{
						string text3 = this._stringReference.ToString();
						try
						{
							obj = (text3.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt32(text3, 16) : Convert.ToInt32(text3, 8));
							goto IL_27A;
						}
						catch (Exception ex2)
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid integer.".FormatWith(CultureInfo.InvariantCulture, text3), ex2);
						}
					}
					int num4;
					ParseResult parseResult2 = ConvertUtils.Int32TryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num4);
					if (parseResult2 == ParseResult.Success)
					{
						obj = num4;
					}
					else
					{
						if (parseResult2 == ParseResult.Overflow)
						{
							throw this.ThrowReaderError("JSON integer {0} is too large or small for an Int32.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
						}
						throw this.ThrowReaderError("Input string '{0}' is not a valid integer.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
					}
				}
				IL_27A:
				jsonToken = JsonToken.Integer;
				goto IL_622;
			case ReadType.ReadAsString:
			{
				string text4 = this._stringReference.ToString();
				if (flag2)
				{
					try
					{
						if (text4.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
						{
							Convert.ToInt64(text4, 16);
						}
						else
						{
							Convert.ToInt64(text4, 8);
						}
						goto IL_170;
					}
					catch (Exception ex3)
					{
						throw this.ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, text4), ex3);
					}
				}
				double num5;
				if (!double.TryParse(text4, NumberStyles.Float, CultureInfo.InvariantCulture, out num5))
				{
					throw this.ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
				}
				IL_170:
				jsonToken = JsonToken.String;
				obj = text4;
				goto IL_622;
			}
			case ReadType.ReadAsDecimal:
				if (flag)
				{
					obj = firstChar - 48m;
				}
				else
				{
					if (flag2)
					{
						string text5 = this._stringReference.ToString();
						try
						{
							obj = Convert.ToDecimal(text5.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text5, 16) : Convert.ToInt64(text5, 8));
							goto IL_35F;
						}
						catch (Exception ex4)
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid decimal.".FormatWith(CultureInfo.InvariantCulture, text5), ex4);
						}
					}
					decimal num6;
					if (ConvertUtils.DecimalTryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num6) != ParseResult.Success)
					{
						throw this.ThrowReaderError("Input string '{0}' is not a valid decimal.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
					}
					obj = num6;
				}
				IL_35F:
				jsonToken = JsonToken.Float;
				goto IL_622;
			case ReadType.ReadAsDouble:
				if (flag)
				{
					obj = (double)firstChar - 48.0;
				}
				else
				{
					if (flag2)
					{
						string text6 = this._stringReference.ToString();
						try
						{
							obj = Convert.ToDouble(text6.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text6, 16) : Convert.ToInt64(text6, 8));
							goto IL_437;
						}
						catch (Exception ex5)
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid double.".FormatWith(CultureInfo.InvariantCulture, text6), ex5);
						}
					}
					double num7;
					if (!double.TryParse(this._stringReference.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num7))
					{
						throw this.ThrowReaderError("Input string '{0}' is not a valid double.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
					}
					obj = num7;
				}
				IL_437:
				jsonToken = JsonToken.Float;
				goto IL_622;
			}
			throw JsonReaderException.Create(this, "Cannot read number value as type.");
			IL_622:
			this.ClearRecentString();
			base.SetToken(jsonToken, obj, false);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009A38 File Offset: 0x00007C38
		private JsonReaderException ThrowReaderError(string message, [Nullable(2)] Exception ex = null)
		{
			base.SetToken(JsonToken.Undefined, null, false);
			return JsonReaderException.Create(this, message, ex);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009A4C File Offset: 0x00007C4C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static object BigIntegerParse(string number, CultureInfo culture)
		{
			return BigInteger.Parse(number, culture);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00009A5C File Offset: 0x00007C5C
		private void ParseComment(bool setToken)
		{
			this._charPos++;
			if (!this.EnsureChars(1, false))
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing comment.");
			}
			bool flag;
			if (this._chars[this._charPos] == '*')
			{
				flag = false;
			}
			else
			{
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, "Error parsing comment. Expected: *, got {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				flag = true;
			}
			this._charPos++;
			int charPos = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= '\n')
				{
					if (c != '\0')
					{
						if (c == '\n')
						{
							if (flag)
							{
								goto Block_16;
							}
							this.ProcessLineFeed();
							continue;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						if (this.ReadData(true) == 0)
						{
							break;
						}
						continue;
					}
				}
				else if (c != '\r')
				{
					if (c == '*')
					{
						this._charPos++;
						if (!flag && this.EnsureChars(0, true) && this._chars[this._charPos] == '/')
						{
							goto Block_14;
						}
						continue;
					}
				}
				else
				{
					if (flag)
					{
						goto Block_15;
					}
					this.ProcessCarriageReturn(true);
					continue;
				}
				this._charPos++;
			}
			if (!flag)
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing comment.");
			}
			this.EndComment(setToken, charPos, this._charPos);
			return;
			Block_14:
			this.EndComment(setToken, charPos, this._charPos - 1);
			this._charPos++;
			return;
			Block_15:
			this.EndComment(setToken, charPos, this._charPos);
			return;
			Block_16:
			this.EndComment(setToken, charPos, this._charPos);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009C0F File Offset: 0x00007E0F
		private void EndComment(bool setToken, int initialPosition, int endPosition)
		{
			if (setToken)
			{
				base.SetToken(JsonToken.Comment, new string(this._chars, initialPosition, endPosition - initialPosition));
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009C2A File Offset: 0x00007E2A
		private bool MatchValue(string value)
		{
			return this.MatchValue(this.EnsureChars(value.Length - 1, true), value);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009C44 File Offset: 0x00007E44
		private bool MatchValue(bool enoughChars, string value)
		{
			if (!enoughChars)
			{
				this._charPos = this._charsUsed;
				throw base.CreateUnexpectedEndException();
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (this._chars[this._charPos + i] != value[i])
				{
					this._charPos += i;
					return false;
				}
			}
			this._charPos += value.Length;
			return true;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009CB4 File Offset: 0x00007EB4
		private bool MatchValueWithTrailingSeparator(string value)
		{
			return this.MatchValue(value) && (!this.EnsureChars(0, false) || this.IsSeparator(this._chars[this._charPos]) || this._chars[this._charPos] == '\0');
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009CF4 File Offset: 0x00007EF4
		private bool IsSeparator(char c)
		{
			if (c <= ')')
			{
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				case '\v':
				case '\f':
					goto IL_8C;
				default:
					if (c != ' ')
					{
						if (c != ')')
						{
							goto IL_8C;
						}
						if (base.CurrentState == JsonReader.State.Constructor || base.CurrentState == JsonReader.State.ConstructorStart)
						{
							return true;
						}
						return false;
					}
					break;
				}
				return true;
			}
			if (c <= '/')
			{
				if (c != ',')
				{
					if (c != '/')
					{
						goto IL_8C;
					}
					if (!this.EnsureChars(1, false))
					{
						return false;
					}
					char c2 = this._chars[this._charPos + 1];
					return c2 == '*' || c2 == '/';
				}
			}
			else if (c != ']' && c != '}')
			{
				goto IL_8C;
			}
			return true;
			IL_8C:
			if (char.IsWhiteSpace(c))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009D98 File Offset: 0x00007F98
		private void ParseTrue()
		{
			if (this.MatchValueWithTrailingSeparator(JsonConvert.True))
			{
				base.SetToken(JsonToken.Boolean, true);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing boolean value.");
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009DC1 File Offset: 0x00007FC1
		private void ParseNull()
		{
			if (this.MatchValueWithTrailingSeparator(JsonConvert.Null))
			{
				base.SetToken(JsonToken.Null);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing null value.");
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009DE4 File Offset: 0x00007FE4
		private void ParseUndefined()
		{
			if (this.MatchValueWithTrailingSeparator(JsonConvert.Undefined))
			{
				base.SetToken(JsonToken.Undefined);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing undefined value.");
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009E07 File Offset: 0x00008007
		private void ParseFalse()
		{
			if (this.MatchValueWithTrailingSeparator(JsonConvert.False))
			{
				base.SetToken(JsonToken.Boolean, false);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing boolean value.");
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009E30 File Offset: 0x00008030
		private object ParseNumberNegativeInfinity(ReadType readType)
		{
			return this.ParseNumberNegativeInfinity(readType, this.MatchValueWithTrailingSeparator(JsonConvert.NegativeInfinity));
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009E44 File Offset: 0x00008044
		private object ParseNumberNegativeInfinity(ReadType readType, bool matched)
		{
			if (matched)
			{
				if (readType != ReadType.Read)
				{
					if (readType == ReadType.ReadAsString)
					{
						base.SetToken(JsonToken.String, JsonConvert.NegativeInfinity);
						return JsonConvert.NegativeInfinity;
					}
					if (readType != ReadType.ReadAsDouble)
					{
						goto IL_4D;
					}
				}
				if (this._floatParseHandling == FloatParseHandling.Double)
				{
					base.SetToken(JsonToken.Float, double.NegativeInfinity);
					return double.NegativeInfinity;
				}
				IL_4D:
				throw JsonReaderException.Create(this, "Cannot read -Infinity value.");
			}
			throw JsonReaderException.Create(this, "Error parsing -Infinity value.");
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009EB5 File Offset: 0x000080B5
		private object ParseNumberPositiveInfinity(ReadType readType)
		{
			return this.ParseNumberPositiveInfinity(readType, this.MatchValueWithTrailingSeparator(JsonConvert.PositiveInfinity));
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009ECC File Offset: 0x000080CC
		private object ParseNumberPositiveInfinity(ReadType readType, bool matched)
		{
			if (matched)
			{
				if (readType != ReadType.Read)
				{
					if (readType == ReadType.ReadAsString)
					{
						base.SetToken(JsonToken.String, JsonConvert.PositiveInfinity);
						return JsonConvert.PositiveInfinity;
					}
					if (readType != ReadType.ReadAsDouble)
					{
						goto IL_4D;
					}
				}
				if (this._floatParseHandling == FloatParseHandling.Double)
				{
					base.SetToken(JsonToken.Float, double.PositiveInfinity);
					return double.PositiveInfinity;
				}
				IL_4D:
				throw JsonReaderException.Create(this, "Cannot read Infinity value.");
			}
			throw JsonReaderException.Create(this, "Error parsing Infinity value.");
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009F3D File Offset: 0x0000813D
		private object ParseNumberNaN(ReadType readType)
		{
			return this.ParseNumberNaN(readType, this.MatchValueWithTrailingSeparator(JsonConvert.NaN));
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009F54 File Offset: 0x00008154
		private object ParseNumberNaN(ReadType readType, bool matched)
		{
			if (matched)
			{
				if (readType != ReadType.Read)
				{
					if (readType == ReadType.ReadAsString)
					{
						base.SetToken(JsonToken.String, JsonConvert.NaN);
						return JsonConvert.NaN;
					}
					if (readType != ReadType.ReadAsDouble)
					{
						goto IL_4D;
					}
				}
				if (this._floatParseHandling == FloatParseHandling.Double)
				{
					base.SetToken(JsonToken.Float, double.NaN);
					return double.NaN;
				}
				IL_4D:
				throw JsonReaderException.Create(this, "Cannot read NaN value.");
			}
			throw JsonReaderException.Create(this, "Error parsing NaN value.");
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009FC8 File Offset: 0x000081C8
		public override void Close()
		{
			base.Close();
			if (this._chars != null)
			{
				BufferUtils.ReturnBuffer(this._arrayPool, this._chars);
				this._chars = null;
			}
			if (base.CloseInput)
			{
				TextReader reader = this._reader;
				if (reader != null)
				{
					reader.Close();
				}
			}
			this._stringBuffer.Clear(this._arrayPool);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A025 File Offset: 0x00008225
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000A028 File Offset: 0x00008228
		public int LineNumber
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start && this.LinePosition == 0 && this.TokenType != JsonToken.Comment)
				{
					return 0;
				}
				return this._lineNumber;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000A04B File Offset: 0x0000824B
		public int LinePosition
		{
			get
			{
				return this._charPos - this._lineStartPos;
			}
		}

		// Token: 0x040000C7 RID: 199
		private readonly bool _safeAsync;

		// Token: 0x040000C8 RID: 200
		private const char UnicodeReplacementChar = '\ufffd';

		// Token: 0x040000C9 RID: 201
		private const int MaximumJavascriptIntegerCharacterLength = 380;

		// Token: 0x040000CA RID: 202
		private const int LargeBufferLength = 1073741823;

		// Token: 0x040000CB RID: 203
		private readonly TextReader _reader;

		// Token: 0x040000CC RID: 204
		[Nullable(2)]
		private char[] _chars;

		// Token: 0x040000CD RID: 205
		private int _charsUsed;

		// Token: 0x040000CE RID: 206
		private int _charPos;

		// Token: 0x040000CF RID: 207
		private int _lineStartPos;

		// Token: 0x040000D0 RID: 208
		private int _lineNumber;

		// Token: 0x040000D1 RID: 209
		private bool _isEndOfFile;

		// Token: 0x040000D2 RID: 210
		private StringBuffer _stringBuffer;

		// Token: 0x040000D3 RID: 211
		private StringReference _stringReference;

		// Token: 0x040000D4 RID: 212
		[Nullable(2)]
		private IArrayPool<char> _arrayPool;
	}
}
