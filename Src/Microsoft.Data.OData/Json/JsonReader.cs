using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x0200016E RID: 366
	[DebuggerDisplay("{NodeType}: {Value}")]
	internal class JsonReader
	{
		// Token: 0x06000A4C RID: 2636 RVA: 0x0002164C File Offset: 0x0001F84C
		public JsonReader(TextReader reader, ODataFormat jsonFormat)
		{
			this.nodeType = JsonNodeType.None;
			this.nodeValue = null;
			this.reader = reader;
			this.characterBuffer = new char[2040];
			this.storedCharacterCount = 0;
			this.tokenStartIndex = 0;
			this.endOfInputReached = false;
			this.allowAnnotations = jsonFormat == ODataFormat.Json;
			this.supportAspNetDateTimeFormat = jsonFormat == ODataFormat.VerboseJson;
			this.scopes = new Stack<JsonReader.Scope>();
			this.scopes.Push(new JsonReader.Scope(JsonReader.ScopeType.Root));
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x000216D1 File Offset: 0x0001F8D1
		public virtual object Value
		{
			get
			{
				return this.nodeValue;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x000216D9 File Offset: 0x0001F8D9
		public virtual JsonNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x000216E1 File Offset: 0x0001F8E1
		public virtual string RawValue
		{
			get
			{
				return this.nodeRawValue;
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000216EC File Offset: 0x0001F8EC
		public virtual bool Read()
		{
			this.nodeValue = null;
			this.nodeRawValue = null;
			if (!this.SkipWhitespaces())
			{
				return this.EndOfInput();
			}
			JsonReader.Scope scope = this.scopes.Peek();
			bool flag = false;
			if (this.characterBuffer[this.tokenStartIndex] == ',')
			{
				flag = true;
				this.TryAppendJsonRawValue(this.characterBuffer[this.tokenStartIndex]);
				this.tokenStartIndex++;
				if (!this.SkipWhitespaces())
				{
					return this.EndOfInput();
				}
			}
			string text = null;
			switch (scope.Type)
			{
			case JsonReader.ScopeType.Root:
				if (flag)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Root));
				}
				if (scope.ValueCount > 0)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_MultipleTopLevelValues);
				}
				this.nodeType = this.ParseValue(out text);
				this.TryAppendJsonRawValue(text);
				break;
			case JsonReader.ScopeType.Array:
				if (flag && scope.ValueCount == 0)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Array));
				}
				if (this.characterBuffer[this.tokenStartIndex] == ']')
				{
					this.TryAppendJsonRawValue(this.characterBuffer[this.tokenStartIndex]);
					this.tokenStartIndex++;
					if (flag)
					{
						throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Array));
					}
					this.PopScope();
					this.nodeType = JsonNodeType.EndArray;
				}
				else
				{
					if (!flag && scope.ValueCount > 0)
					{
						throw JsonReaderExtensions.CreateException(Strings.JsonReader_MissingComma(JsonReader.ScopeType.Array));
					}
					this.nodeType = this.ParseValue(out text);
					this.TryAppendJsonRawValue(text);
				}
				break;
			case JsonReader.ScopeType.Object:
				if (flag && scope.ValueCount == 0)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Object));
				}
				if (this.characterBuffer[this.tokenStartIndex] == '}')
				{
					this.TryAppendJsonRawValue(this.characterBuffer[this.tokenStartIndex]);
					this.tokenStartIndex++;
					if (flag)
					{
						throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Object));
					}
					this.PopScope();
					this.nodeType = JsonNodeType.EndObject;
				}
				else
				{
					if (!flag && scope.ValueCount > 0)
					{
						throw JsonReaderExtensions.CreateException(Strings.JsonReader_MissingComma(JsonReader.ScopeType.Object));
					}
					this.nodeType = this.ParseProperty(out text);
					this.TryAppendJsonRawValue(text);
				}
				break;
			case JsonReader.ScopeType.Property:
				if (flag)
				{
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedComma(JsonReader.ScopeType.Property));
				}
				this.nodeType = this.ParseValue(out text);
				this.TryAppendJsonRawValue(text);
				break;
			default:
				throw JsonReaderExtensions.CreateException(Strings.General_InternalError(InternalErrorCodes.JsonReader_Read));
			}
			return true;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002195B File Offset: 0x0001FB5B
		private void TryAppendJsonRawValue(string rawValue)
		{
			this.nodeRawValue += rawValue;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002196F File Offset: 0x0001FB6F
		private void TryAppendJsonRawValue(char rawValue)
		{
			this.nodeRawValue += rawValue;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00021988 File Offset: 0x0001FB88
		private static bool IsWhitespaceCharacter(char character)
		{
			return character <= ' ' && (character == ' ' || character == '\t' || character == '\n' || character == '\r');
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000219A8 File Offset: 0x0001FBA8
		private static object TryParseDateTimePrimitiveValue(string stringValue)
		{
			if (!stringValue.StartsWith("/Date(", StringComparison.Ordinal) || !stringValue.EndsWith(")/", StringComparison.Ordinal))
			{
				return null;
			}
			string text = stringValue.Substring("/Date(".Length, stringValue.Length - ("/Date(".Length + ")/".Length));
			int num = text.IndexOfAny(new char[] { '+', '-' }, 1);
			if (num == -1)
			{
				long num2;
				if (long.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2))
				{
					return new DateTime(JsonValueUtils.JsonTicksToDateTimeTicks(num2), DateTimeKind.Utc);
				}
			}
			else
			{
				string text2 = text.Substring(num);
				text = text.Substring(0, num);
				long num3;
				int num4;
				if (long.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num3) && int.TryParse(text2, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num4))
				{
					return new DateTimeOffset(JsonValueUtils.JsonTicksToDateTimeTicks(num3), new TimeSpan(0, num4, 0));
				}
			}
			return null;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00021A94 File Offset: 0x0001FC94
		private JsonNodeType ParseValue(out string rawValue)
		{
			this.scopes.Peek().ValueCount++;
			char c = this.characterBuffer[this.tokenStartIndex];
			char c2 = c;
			if (c2 > '[')
			{
				if (c2 <= 'n')
				{
					if (c2 != 'f')
					{
						if (c2 != 'n')
						{
							goto IL_131;
						}
						this.nodeValue = this.ParseNullPrimitiveValue(out rawValue);
						goto IL_15D;
					}
				}
				else if (c2 != 't')
				{
					if (c2 == '{')
					{
						this.PushScope(JsonReader.ScopeType.Object);
						this.tokenStartIndex++;
						rawValue = c.ToString();
						return JsonNodeType.StartObject;
					}
					goto IL_131;
				}
				this.nodeValue = this.ParseBooleanPrimitiveValue(out rawValue);
				goto IL_15D;
			}
			if (c2 != '"' && c2 != '\'')
			{
				if (c2 == '[')
				{
					this.PushScope(JsonReader.ScopeType.Array);
					this.tokenStartIndex++;
					rawValue = c.ToString();
					return JsonNodeType.StartArray;
				}
			}
			else
			{
				bool flag;
				string text;
				rawValue = (text = this.ParseStringPrimitiveValue(out flag));
				this.nodeValue = text;
				rawValue = string.Format(CultureInfo.InvariantCulture, "{0}{1}{0}", new object[] { c, rawValue });
				if (!flag || !this.supportAspNetDateTimeFormat)
				{
					goto IL_15D;
				}
				object obj = JsonReader.TryParseDateTimePrimitiveValue((string)this.nodeValue);
				if (obj != null)
				{
					this.nodeValue = obj;
					goto IL_15D;
				}
				goto IL_15D;
			}
			IL_131:
			if (!char.IsDigit(c) && c != '-' && c != '.')
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedToken);
			}
			this.nodeValue = this.ParseNumberPrimitiveValue(out rawValue);
			IL_15D:
			this.TryPopPropertyScope();
			return JsonNodeType.PrimitiveValue;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00021C08 File Offset: 0x0001FE08
		private JsonNodeType ParseProperty(out string rawValue)
		{
			this.scopes.Peek().ValueCount++;
			this.PushScope(JsonReader.ScopeType.Property);
			this.nodeValue = this.ParseName(out rawValue);
			if (string.IsNullOrEmpty((string)this.nodeValue))
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_InvalidPropertyNameOrUnexpectedComma((string)this.nodeValue));
			}
			if (!this.SkipWhitespaces() || this.characterBuffer[this.tokenStartIndex] != ':')
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_MissingColon((string)this.nodeValue));
			}
			rawValue += ":";
			this.tokenStartIndex++;
			return JsonNodeType.Property;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00021CB8 File Offset: 0x0001FEB8
		private string ParseStringPrimitiveValue()
		{
			bool flag;
			return this.ParseStringPrimitiveValue(out flag);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00021CD0 File Offset: 0x0001FED0
		private string ParseStringPrimitiveValue(out bool hasLeadingBackslash)
		{
			hasLeadingBackslash = false;
			char c = this.characterBuffer[this.tokenStartIndex];
			this.tokenStartIndex++;
			StringBuilder stringBuilder = null;
			int num = 0;
			while (this.tokenStartIndex + num < this.storedCharacterCount || this.ReadInput())
			{
				char c2 = this.characterBuffer[this.tokenStartIndex + num];
				if (c2 == '\\')
				{
					if (num == 0 && stringBuilder == null)
					{
						hasLeadingBackslash = true;
					}
					if (stringBuilder == null)
					{
						if (this.stringValueBuilder == null)
						{
							this.stringValueBuilder = new StringBuilder();
						}
						else
						{
							this.stringValueBuilder.Length = 0;
						}
						stringBuilder = this.stringValueBuilder;
					}
					stringBuilder.Append(this.ConsumeTokenToString(num));
					num = 0;
					if (!this.EnsureAvailableCharacters(2))
					{
						throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\"));
					}
					c2 = this.characterBuffer[this.tokenStartIndex + 1];
					this.tokenStartIndex += 2;
					char c3 = c2;
					if (c3 <= '\\')
					{
						if (c3 <= '\'')
						{
							if (c3 != '"' && c3 != '\'')
							{
								goto IL_1E0;
							}
						}
						else if (c3 != '/' && c3 != '\\')
						{
							goto IL_1E0;
						}
						stringBuilder.Append(c2);
						continue;
					}
					if (c3 <= 'f')
					{
						if (c3 == 'b')
						{
							stringBuilder.Append('\b');
							continue;
						}
						if (c3 == 'f')
						{
							stringBuilder.Append('\f');
							continue;
						}
					}
					else
					{
						if (c3 == 'n')
						{
							stringBuilder.Append('\n');
							continue;
						}
						switch (c3)
						{
						case 'r':
							stringBuilder.Append('\r');
							continue;
						case 't':
							stringBuilder.Append('\t');
							continue;
						case 'u':
						{
							if (!this.EnsureAvailableCharacters(4))
							{
								throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\uXXXX"));
							}
							string text = this.ConsumeTokenToString(4);
							int num2;
							if (!int.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2))
							{
								throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\u" + text));
							}
							stringBuilder.Append((char)num2);
							continue;
						}
						}
					}
					IL_1E0:
					throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnrecognizedEscapeSequence("\\" + c2));
				}
				else
				{
					if (c2 == c)
					{
						string text2 = this.ConsumeTokenToString(num);
						this.tokenStartIndex++;
						if (stringBuilder != null)
						{
							stringBuilder.Append(text2);
							text2 = stringBuilder.ToString();
						}
						return text2;
					}
					num++;
				}
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedEndOfString);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00021F38 File Offset: 0x00020138
		private object ParseNullPrimitiveValue(out string rawValue)
		{
			string text = this.ParseName(out rawValue);
			if (!string.Equals(text, "null"))
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedToken(text));
			}
			return null;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00021F68 File Offset: 0x00020168
		private object ParseBooleanPrimitiveValue(out string rawValue)
		{
			string text = this.ParseName(out rawValue);
			if (string.Equals(text, "false"))
			{
				return false;
			}
			if (string.Equals(text, "true"))
			{
				return true;
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReader_UnexpectedToken(text));
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00021FB0 File Offset: 0x000201B0
		private object ParseNumberPrimitiveValue(out string rawValue)
		{
			int num = 1;
			while (this.tokenStartIndex + num < this.storedCharacterCount || this.ReadInput())
			{
				char c = this.characterBuffer[this.tokenStartIndex + num];
				if (!char.IsDigit(c) && c != '.' && c != 'E' && c != 'e' && c != '-' && c != '+')
				{
					break;
				}
				num++;
			}
			string text = this.ConsumeTokenToString(num);
			rawValue = text;
			int num2;
			if (int.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2))
			{
				return num2;
			}
			double num3;
			if (double.TryParse(text, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num3))
			{
				return num3;
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReader_InvalidNumberFormat(text));
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00022058 File Offset: 0x00020258
		private string ParseName(out string rawValue)
		{
			char c = this.characterBuffer[this.tokenStartIndex];
			if (c == '"' || c == '\'')
			{
				string text = this.ParseStringPrimitiveValue();
				rawValue = string.Format(CultureInfo.InvariantCulture, "{0}{1}{0}", new object[] { c, text });
				return text;
			}
			int num = 0;
			do
			{
				char c2 = this.characterBuffer[this.tokenStartIndex + num];
				if (c2 != '_' && !char.IsLetterOrDigit(c2) && c2 != '$' && (!this.allowAnnotations || (c2 != '.' && c2 != '@')))
				{
					break;
				}
				num++;
			}
			while (this.tokenStartIndex + num < this.storedCharacterCount || this.ReadInput());
			rawValue = this.ConsumeTokenToString(num);
			return rawValue;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002210D File Offset: 0x0002030D
		private bool EndOfInput()
		{
			if (this.scopes.Count > 1)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReader_EndOfInputWithOpenScope);
			}
			this.nodeType = JsonNodeType.EndOfInput;
			return false;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00022130 File Offset: 0x00020330
		private void PushScope(JsonReader.ScopeType newScopeType)
		{
			this.scopes.Push(new JsonReader.Scope(newScopeType));
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00022143 File Offset: 0x00020343
		private void PopScope()
		{
			this.scopes.Pop();
			this.TryPopPropertyScope();
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00022157 File Offset: 0x00020357
		private void TryPopPropertyScope()
		{
			if (this.scopes.Peek().Type == JsonReader.ScopeType.Property)
			{
				this.scopes.Pop();
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00022178 File Offset: 0x00020378
		private bool SkipWhitespaces()
		{
			for (;;)
			{
				if (this.tokenStartIndex >= this.storedCharacterCount)
				{
					if (!this.ReadInput())
					{
						return false;
					}
				}
				else
				{
					if (!JsonReader.IsWhitespaceCharacter(this.characterBuffer[this.tokenStartIndex]))
					{
						break;
					}
					this.tokenStartIndex++;
				}
			}
			return true;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000221B7 File Offset: 0x000203B7
		private bool EnsureAvailableCharacters(int characterCountAfterTokenStart)
		{
			while (this.tokenStartIndex + characterCountAfterTokenStart > this.storedCharacterCount)
			{
				if (!this.ReadInput())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x000221D8 File Offset: 0x000203D8
		private string ConsumeTokenToString(int characterCount)
		{
			string text = new string(this.characterBuffer, this.tokenStartIndex, characterCount);
			this.tokenStartIndex += characterCount;
			return text;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00022208 File Offset: 0x00020408
		private bool ReadInput()
		{
			if (this.endOfInputReached)
			{
				return false;
			}
			if (this.storedCharacterCount == this.characterBuffer.Length)
			{
				if (this.tokenStartIndex == this.storedCharacterCount)
				{
					this.tokenStartIndex = 0;
					this.storedCharacterCount = 0;
				}
				else if (this.tokenStartIndex > this.characterBuffer.Length - 64)
				{
					Array.Copy(this.characterBuffer, this.tokenStartIndex, this.characterBuffer, 0, this.storedCharacterCount - this.tokenStartIndex);
					this.storedCharacterCount -= this.tokenStartIndex;
					this.tokenStartIndex = 0;
				}
				else
				{
					int num = this.characterBuffer.Length * 2;
					char[] array = new char[num];
					Array.Copy(this.characterBuffer, 0, array, 0, this.characterBuffer.Length);
					this.characterBuffer = array;
				}
			}
			int num2 = this.reader.Read(this.characterBuffer, this.storedCharacterCount, this.characterBuffer.Length - this.storedCharacterCount);
			if (num2 == 0)
			{
				this.endOfInputReached = true;
				return false;
			}
			this.storedCharacterCount += num2;
			return true;
		}

		// Token: 0x040003C8 RID: 968
		private const int InitialCharacterBufferSize = 2040;

		// Token: 0x040003C9 RID: 969
		private const int MaxCharacterCountToMove = 64;

		// Token: 0x040003CA RID: 970
		private const string DateTimeFormatPrefix = "/Date(";

		// Token: 0x040003CB RID: 971
		private const string DateTimeFormatSuffix = ")/";

		// Token: 0x040003CC RID: 972
		private readonly TextReader reader;

		// Token: 0x040003CD RID: 973
		private readonly Stack<JsonReader.Scope> scopes;

		// Token: 0x040003CE RID: 974
		private readonly bool allowAnnotations;

		// Token: 0x040003CF RID: 975
		private readonly bool supportAspNetDateTimeFormat;

		// Token: 0x040003D0 RID: 976
		private bool endOfInputReached;

		// Token: 0x040003D1 RID: 977
		private char[] characterBuffer;

		// Token: 0x040003D2 RID: 978
		private int storedCharacterCount;

		// Token: 0x040003D3 RID: 979
		private int tokenStartIndex;

		// Token: 0x040003D4 RID: 980
		private JsonNodeType nodeType;

		// Token: 0x040003D5 RID: 981
		private object nodeValue;

		// Token: 0x040003D6 RID: 982
		private string nodeRawValue;

		// Token: 0x040003D7 RID: 983
		private StringBuilder stringValueBuilder;

		// Token: 0x0200016F RID: 367
		private enum ScopeType
		{
			// Token: 0x040003D9 RID: 985
			Root,
			// Token: 0x040003DA RID: 986
			Array,
			// Token: 0x040003DB RID: 987
			Object,
			// Token: 0x040003DC RID: 988
			Property
		}

		// Token: 0x02000170 RID: 368
		private sealed class Scope
		{
			// Token: 0x06000A65 RID: 2661 RVA: 0x00022318 File Offset: 0x00020518
			public Scope(JsonReader.ScopeType type)
			{
				this.type = type;
			}

			// Token: 0x17000270 RID: 624
			// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00022327 File Offset: 0x00020527
			// (set) Token: 0x06000A67 RID: 2663 RVA: 0x0002232F File Offset: 0x0002052F
			public int ValueCount { get; set; }

			// Token: 0x17000271 RID: 625
			// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00022338 File Offset: 0x00020538
			public JsonReader.ScopeType Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x040003DD RID: 989
			private readonly JsonReader.ScopeType type;
		}
	}
}
