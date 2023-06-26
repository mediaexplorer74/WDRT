using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000106 RID: 262
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonReader : JsonReader
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00035B8A File Offset: 0x00033D8A
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x00035B92 File Offset: 0x00033D92
		[Obsolete("JsonNet35BinaryCompatibility will be removed in a future version of Json.NET.")]
		public bool JsonNet35BinaryCompatibility
		{
			get
			{
				return this._jsonNet35BinaryCompatibility;
			}
			set
			{
				this._jsonNet35BinaryCompatibility = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x00035B9B File Offset: 0x00033D9B
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x00035BA3 File Offset: 0x00033DA3
		public bool ReadRootValueAsArray
		{
			get
			{
				return this._readRootValueAsArray;
			}
			set
			{
				this._readRootValueAsArray = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00035BAC File Offset: 0x00033DAC
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x00035BB4 File Offset: 0x00033DB4
		public DateTimeKind DateTimeKindHandling
		{
			get
			{
				return this._dateTimeKindHandling;
			}
			set
			{
				this._dateTimeKindHandling = value;
			}
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00035BBD File Offset: 0x00033DBD
		public BsonReader(Stream stream)
			: this(stream, false, DateTimeKind.Local)
		{
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00035BC8 File Offset: 0x00033DC8
		public BsonReader(BinaryReader reader)
			: this(reader, false, DateTimeKind.Local)
		{
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00035BD3 File Offset: 0x00033DD3
		public BsonReader(Stream stream, bool readRootValueAsArray, DateTimeKind dateTimeKindHandling)
		{
			ValidationUtils.ArgumentNotNull(stream, "stream");
			this._reader = new BinaryReader(stream);
			this._stack = new List<BsonReader.ContainerContext>();
			this._readRootValueAsArray = readRootValueAsArray;
			this._dateTimeKindHandling = dateTimeKindHandling;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00035C0B File Offset: 0x00033E0B
		public BsonReader(BinaryReader reader, bool readRootValueAsArray, DateTimeKind dateTimeKindHandling)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this._reader = reader;
			this._stack = new List<BsonReader.ContainerContext>();
			this._readRootValueAsArray = readRootValueAsArray;
			this._dateTimeKindHandling = dateTimeKindHandling;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00035C3E File Offset: 0x00033E3E
		private string ReadElement()
		{
			this._currentElementType = this.ReadType();
			return this.ReadString();
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00035C54 File Offset: 0x00033E54
		public override bool Read()
		{
			bool flag2;
			try
			{
				bool flag;
				switch (this._bsonReaderState)
				{
				case BsonReader.BsonReaderState.Normal:
					flag = this.ReadNormal();
					break;
				case BsonReader.BsonReaderState.ReferenceStart:
				case BsonReader.BsonReaderState.ReferenceRef:
				case BsonReader.BsonReaderState.ReferenceId:
					flag = this.ReadReference();
					break;
				case BsonReader.BsonReaderState.CodeWScopeStart:
				case BsonReader.BsonReaderState.CodeWScopeCode:
				case BsonReader.BsonReaderState.CodeWScopeScope:
				case BsonReader.BsonReaderState.CodeWScopeScopeObject:
				case BsonReader.BsonReaderState.CodeWScopeScopeEnd:
					flag = this.ReadCodeWScope();
					break;
				default:
					throw JsonReaderException.Create(this, "Unexpected state: {0}".FormatWith(CultureInfo.InvariantCulture, this._bsonReaderState));
				}
				if (!flag)
				{
					base.SetToken(JsonToken.None);
					flag2 = false;
				}
				else
				{
					flag2 = true;
				}
			}
			catch (EndOfStreamException)
			{
				base.SetToken(JsonToken.None);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00035D00 File Offset: 0x00033F00
		public override void Close()
		{
			base.Close();
			if (base.CloseInput)
			{
				BinaryReader reader = this._reader;
				if (reader == null)
				{
					return;
				}
				reader.Close();
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00035D20 File Offset: 0x00033F20
		private bool ReadCodeWScope()
		{
			switch (this._bsonReaderState)
			{
			case BsonReader.BsonReaderState.CodeWScopeStart:
				base.SetToken(JsonToken.PropertyName, "$code");
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeCode;
				return true;
			case BsonReader.BsonReaderState.CodeWScopeCode:
				this.ReadInt32();
				base.SetToken(JsonToken.String, this.ReadLengthString());
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeScope;
				return true;
			case BsonReader.BsonReaderState.CodeWScopeScope:
			{
				if (base.CurrentState == JsonReader.State.PostValue)
				{
					base.SetToken(JsonToken.PropertyName, "$scope");
					return true;
				}
				base.SetToken(JsonToken.StartObject);
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeScopeObject;
				BsonReader.ContainerContext containerContext = new BsonReader.ContainerContext(BsonType.Object);
				this.PushContext(containerContext);
				containerContext.Length = this.ReadInt32();
				return true;
			}
			case BsonReader.BsonReaderState.CodeWScopeScopeObject:
			{
				bool flag = this.ReadNormal();
				if (flag && this.TokenType == JsonToken.EndObject)
				{
					this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeScopeEnd;
				}
				return flag;
			}
			case BsonReader.BsonReaderState.CodeWScopeScopeEnd:
				base.SetToken(JsonToken.EndObject);
				this._bsonReaderState = BsonReader.BsonReaderState.Normal;
				return true;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00035DFC File Offset: 0x00033FFC
		private bool ReadReference()
		{
			JsonReader.State currentState = base.CurrentState;
			if (currentState != JsonReader.State.Property)
			{
				if (currentState == JsonReader.State.ObjectStart)
				{
					base.SetToken(JsonToken.PropertyName, "$ref");
					this._bsonReaderState = BsonReader.BsonReaderState.ReferenceRef;
					return true;
				}
				if (currentState != JsonReader.State.PostValue)
				{
					throw JsonReaderException.Create(this, "Unexpected state when reading BSON reference: " + base.CurrentState.ToString());
				}
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceRef)
				{
					base.SetToken(JsonToken.PropertyName, "$id");
					this._bsonReaderState = BsonReader.BsonReaderState.ReferenceId;
					return true;
				}
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceId)
				{
					base.SetToken(JsonToken.EndObject);
					this._bsonReaderState = BsonReader.BsonReaderState.Normal;
					return true;
				}
				throw JsonReaderException.Create(this, "Unexpected state when reading BSON reference: " + this._bsonReaderState.ToString());
			}
			else
			{
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceRef)
				{
					base.SetToken(JsonToken.String, this.ReadLengthString());
					return true;
				}
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceId)
				{
					base.SetToken(JsonToken.Bytes, this.ReadBytes(12));
					return true;
				}
				throw JsonReaderException.Create(this, "Unexpected state when reading BSON reference: " + this._bsonReaderState.ToString());
			}
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00035F0C File Offset: 0x0003410C
		private bool ReadNormal()
		{
			switch (base.CurrentState)
			{
			case JsonReader.State.Start:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.Closed:
				return false;
			case JsonReader.State.Property:
				this.ReadType(this._currentElementType);
				return true;
			case JsonReader.State.ObjectStart:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.PostValue:
			{
				BsonReader.ContainerContext currentContext = this._currentContext;
				if (currentContext == null)
				{
					if (!base.SupportMultipleContent)
					{
						return false;
					}
				}
				else
				{
					int num = currentContext.Length - 1;
					if (currentContext.Position < num)
					{
						if (currentContext.Type == BsonType.Array)
						{
							this.ReadElement();
							this.ReadType(this._currentElementType);
							return true;
						}
						base.SetToken(JsonToken.PropertyName, this.ReadElement());
						return true;
					}
					else
					{
						if (currentContext.Position != num)
						{
							throw JsonReaderException.Create(this, "Read past end of current container context.");
						}
						if (this.ReadByte() != 0)
						{
							throw JsonReaderException.Create(this, "Unexpected end of object byte value.");
						}
						this.PopContext();
						if (this._currentContext != null)
						{
							this.MovePosition(currentContext.Length);
						}
						JsonToken jsonToken = ((currentContext.Type == BsonType.Object) ? JsonToken.EndObject : JsonToken.EndArray);
						base.SetToken(jsonToken);
						return true;
					}
				}
				break;
			}
			case JsonReader.State.Object:
			case JsonReader.State.Array:
				goto IL_145;
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
			case JsonReader.State.Error:
			case JsonReader.State.Finished:
				return false;
			default:
				goto IL_145;
			}
			JsonToken jsonToken2 = ((!this._readRootValueAsArray) ? JsonToken.StartObject : JsonToken.StartArray);
			BsonType bsonType = ((!this._readRootValueAsArray) ? BsonType.Object : BsonType.Array);
			base.SetToken(jsonToken2);
			BsonReader.ContainerContext containerContext = new BsonReader.ContainerContext(bsonType);
			this.PushContext(containerContext);
			containerContext.Length = this.ReadInt32();
			return true;
			IL_145:
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00036068 File Offset: 0x00034268
		private void PopContext()
		{
			this._stack.RemoveAt(this._stack.Count - 1);
			if (this._stack.Count == 0)
			{
				this._currentContext = null;
				return;
			}
			this._currentContext = this._stack[this._stack.Count - 1];
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x000360C0 File Offset: 0x000342C0
		private void PushContext(BsonReader.ContainerContext newContext)
		{
			this._stack.Add(newContext);
			this._currentContext = newContext;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000360D5 File Offset: 0x000342D5
		private byte ReadByte()
		{
			this.MovePosition(1);
			return this._reader.ReadByte();
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000360EC File Offset: 0x000342EC
		private void ReadType(BsonType type)
		{
			switch (type)
			{
			case BsonType.Number:
			{
				double num = this.ReadDouble();
				if (this._floatParseHandling == FloatParseHandling.Decimal)
				{
					base.SetToken(JsonToken.Float, Convert.ToDecimal(num, CultureInfo.InvariantCulture));
					return;
				}
				base.SetToken(JsonToken.Float, num);
				return;
			}
			case BsonType.String:
			case BsonType.Symbol:
				base.SetToken(JsonToken.String, this.ReadLengthString());
				return;
			case BsonType.Object:
			{
				base.SetToken(JsonToken.StartObject);
				BsonReader.ContainerContext containerContext = new BsonReader.ContainerContext(BsonType.Object);
				this.PushContext(containerContext);
				containerContext.Length = this.ReadInt32();
				return;
			}
			case BsonType.Array:
			{
				base.SetToken(JsonToken.StartArray);
				BsonReader.ContainerContext containerContext2 = new BsonReader.ContainerContext(BsonType.Array);
				this.PushContext(containerContext2);
				containerContext2.Length = this.ReadInt32();
				return;
			}
			case BsonType.Binary:
			{
				BsonBinaryType bsonBinaryType;
				byte[] array = this.ReadBinary(out bsonBinaryType);
				object obj = ((bsonBinaryType != BsonBinaryType.Uuid) ? array : new Guid(array));
				base.SetToken(JsonToken.Bytes, obj);
				return;
			}
			case BsonType.Undefined:
				base.SetToken(JsonToken.Undefined);
				return;
			case BsonType.Oid:
			{
				byte[] array2 = this.ReadBytes(12);
				base.SetToken(JsonToken.Bytes, array2);
				return;
			}
			case BsonType.Boolean:
			{
				bool flag = Convert.ToBoolean(this.ReadByte());
				base.SetToken(JsonToken.Boolean, flag);
				return;
			}
			case BsonType.Date:
			{
				DateTime dateTime = DateTimeUtils.ConvertJavaScriptTicksToDateTime(this.ReadInt64());
				DateTimeKind dateTimeKindHandling = this.DateTimeKindHandling;
				DateTime dateTime2;
				if (dateTimeKindHandling != DateTimeKind.Unspecified)
				{
					if (dateTimeKindHandling != DateTimeKind.Local)
					{
						dateTime2 = dateTime;
					}
					else
					{
						dateTime2 = dateTime.ToLocalTime();
					}
				}
				else
				{
					dateTime2 = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
				}
				base.SetToken(JsonToken.Date, dateTime2);
				return;
			}
			case BsonType.Null:
				base.SetToken(JsonToken.Null);
				return;
			case BsonType.Regex:
			{
				string text = this.ReadString();
				string text2 = this.ReadString();
				string text3 = "/" + text + "/" + text2;
				base.SetToken(JsonToken.String, text3);
				return;
			}
			case BsonType.Reference:
				base.SetToken(JsonToken.StartObject);
				this._bsonReaderState = BsonReader.BsonReaderState.ReferenceStart;
				return;
			case BsonType.Code:
				base.SetToken(JsonToken.String, this.ReadLengthString());
				return;
			case BsonType.CodeWScope:
				base.SetToken(JsonToken.StartObject);
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeStart;
				return;
			case BsonType.Integer:
				base.SetToken(JsonToken.Integer, (long)this.ReadInt32());
				return;
			case BsonType.TimeStamp:
			case BsonType.Long:
				base.SetToken(JsonToken.Integer, this.ReadInt64());
				return;
			default:
				throw new ArgumentOutOfRangeException("type", "Unexpected BsonType value: " + type.ToString());
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0003633C File Offset: 0x0003453C
		private byte[] ReadBinary(out BsonBinaryType binaryType)
		{
			int num = this.ReadInt32();
			binaryType = (BsonBinaryType)this.ReadByte();
			if (binaryType == BsonBinaryType.BinaryOld && !this._jsonNet35BinaryCompatibility)
			{
				num = this.ReadInt32();
			}
			return this.ReadBytes(num);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00036374 File Offset: 0x00034574
		private string ReadString()
		{
			this.EnsureBuffers();
			StringBuilder stringBuilder = null;
			int num = 0;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = num2;
				byte b;
				while (num3 < 128 && (b = this._reader.ReadByte()) > 0)
				{
					this._byteBuffer[num3++] = b;
				}
				num4 = num3 - num2;
				num += num4;
				if (num3 < 128 && stringBuilder == null)
				{
					break;
				}
				int lastFullCharStop = this.GetLastFullCharStop(num3 - 1);
				int chars = Encoding.UTF8.GetChars(this._byteBuffer, 0, lastFullCharStop + 1, this._charBuffer, 0);
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(256);
				}
				stringBuilder.Append(this._charBuffer, 0, chars);
				if (lastFullCharStop < num4 - 1)
				{
					num2 = num4 - lastFullCharStop - 1;
					Array.Copy(this._byteBuffer, lastFullCharStop + 1, this._byteBuffer, 0, num2);
				}
				else
				{
					if (num3 < 128)
					{
						goto Block_6;
					}
					num2 = 0;
				}
			}
			int chars2 = Encoding.UTF8.GetChars(this._byteBuffer, 0, num4, this._charBuffer, 0);
			this.MovePosition(num + 1);
			return new string(this._charBuffer, 0, chars2);
			Block_6:
			this.MovePosition(num + 1);
			return stringBuilder.ToString();
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00036494 File Offset: 0x00034694
		private string ReadLengthString()
		{
			int num = this.ReadInt32();
			this.MovePosition(num);
			string @string = this.GetString(num - 1);
			this._reader.ReadByte();
			return @string;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x000364C4 File Offset: 0x000346C4
		private string GetString(int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			this.EnsureBuffers();
			StringBuilder stringBuilder = null;
			int num = 0;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = ((length - num > 128 - num2) ? (128 - num2) : (length - num));
				num4 = this._reader.Read(this._byteBuffer, num2, num3);
				if (num4 == 0)
				{
					break;
				}
				num += num4;
				num4 += num2;
				if (num4 == length)
				{
					goto Block_4;
				}
				int lastFullCharStop = this.GetLastFullCharStop(num4 - 1);
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(length);
				}
				int chars = Encoding.UTF8.GetChars(this._byteBuffer, 0, lastFullCharStop + 1, this._charBuffer, 0);
				stringBuilder.Append(this._charBuffer, 0, chars);
				if (lastFullCharStop < num4 - 1)
				{
					num2 = num4 - lastFullCharStop - 1;
					Array.Copy(this._byteBuffer, lastFullCharStop + 1, this._byteBuffer, 0, num2);
				}
				else
				{
					num2 = 0;
				}
				if (num >= length)
				{
					goto Block_7;
				}
			}
			throw new EndOfStreamException("Unable to read beyond the end of the stream.");
			Block_4:
			int chars2 = Encoding.UTF8.GetChars(this._byteBuffer, 0, num4, this._charBuffer, 0);
			return new string(this._charBuffer, 0, chars2);
			Block_7:
			return stringBuilder.ToString();
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x000365DC File Offset: 0x000347DC
		private int GetLastFullCharStop(int start)
		{
			int i = start;
			int num = 0;
			while (i >= 0)
			{
				num = this.BytesInSequence(this._byteBuffer[i]);
				if (num == 0)
				{
					i--;
				}
				else
				{
					if (num != 1)
					{
						i--;
						break;
					}
					break;
				}
			}
			if (num == start - i)
			{
				return start;
			}
			return i;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00036620 File Offset: 0x00034820
		private int BytesInSequence(byte b)
		{
			if (b <= BsonReader.SeqRange1[1])
			{
				return 1;
			}
			if (b >= BsonReader.SeqRange2[0] && b <= BsonReader.SeqRange2[1])
			{
				return 2;
			}
			if (b >= BsonReader.SeqRange3[0] && b <= BsonReader.SeqRange3[1])
			{
				return 3;
			}
			if (b >= BsonReader.SeqRange4[0] && b <= BsonReader.SeqRange4[1])
			{
				return 4;
			}
			return 0;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0003667C File Offset: 0x0003487C
		private void EnsureBuffers()
		{
			if (this._byteBuffer == null)
			{
				this._byteBuffer = new byte[128];
			}
			if (this._charBuffer == null)
			{
				int maxCharCount = Encoding.UTF8.GetMaxCharCount(128);
				this._charBuffer = new char[maxCharCount];
			}
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x000366C5 File Offset: 0x000348C5
		private double ReadDouble()
		{
			this.MovePosition(8);
			return this._reader.ReadDouble();
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000366D9 File Offset: 0x000348D9
		private int ReadInt32()
		{
			this.MovePosition(4);
			return this._reader.ReadInt32();
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000366ED File Offset: 0x000348ED
		private long ReadInt64()
		{
			this.MovePosition(8);
			return this._reader.ReadInt64();
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00036701 File Offset: 0x00034901
		private BsonType ReadType()
		{
			this.MovePosition(1);
			return (BsonType)this._reader.ReadSByte();
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00036715 File Offset: 0x00034915
		private void MovePosition(int count)
		{
			this._currentContext.Position += count;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0003672A File Offset: 0x0003492A
		private byte[] ReadBytes(int count)
		{
			this.MovePosition(count);
			return this._reader.ReadBytes(count);
		}

		// Token: 0x0400040D RID: 1037
		private const int MaxCharBytesSize = 128;

		// Token: 0x0400040E RID: 1038
		private static readonly byte[] SeqRange1 = new byte[] { 0, 127 };

		// Token: 0x0400040F RID: 1039
		private static readonly byte[] SeqRange2 = new byte[] { 194, 223 };

		// Token: 0x04000410 RID: 1040
		private static readonly byte[] SeqRange3 = new byte[] { 224, 239 };

		// Token: 0x04000411 RID: 1041
		private static readonly byte[] SeqRange4 = new byte[] { 240, 244 };

		// Token: 0x04000412 RID: 1042
		private readonly BinaryReader _reader;

		// Token: 0x04000413 RID: 1043
		private readonly List<BsonReader.ContainerContext> _stack;

		// Token: 0x04000414 RID: 1044
		private byte[] _byteBuffer;

		// Token: 0x04000415 RID: 1045
		private char[] _charBuffer;

		// Token: 0x04000416 RID: 1046
		private BsonType _currentElementType;

		// Token: 0x04000417 RID: 1047
		private BsonReader.BsonReaderState _bsonReaderState;

		// Token: 0x04000418 RID: 1048
		private BsonReader.ContainerContext _currentContext;

		// Token: 0x04000419 RID: 1049
		private bool _readRootValueAsArray;

		// Token: 0x0400041A RID: 1050
		private bool _jsonNet35BinaryCompatibility;

		// Token: 0x0400041B RID: 1051
		private DateTimeKind _dateTimeKindHandling;

		// Token: 0x020001E2 RID: 482
		private enum BsonReaderState
		{
			// Token: 0x0400085D RID: 2141
			Normal,
			// Token: 0x0400085E RID: 2142
			ReferenceStart,
			// Token: 0x0400085F RID: 2143
			ReferenceRef,
			// Token: 0x04000860 RID: 2144
			ReferenceId,
			// Token: 0x04000861 RID: 2145
			CodeWScopeStart,
			// Token: 0x04000862 RID: 2146
			CodeWScopeCode,
			// Token: 0x04000863 RID: 2147
			CodeWScopeScope,
			// Token: 0x04000864 RID: 2148
			CodeWScopeScopeObject,
			// Token: 0x04000865 RID: 2149
			CodeWScopeScopeEnd
		}

		// Token: 0x020001E3 RID: 483
		private class ContainerContext
		{
			// Token: 0x06001043 RID: 4163 RVA: 0x000479A5 File Offset: 0x00045BA5
			public ContainerContext(BsonType type)
			{
				this.Type = type;
			}

			// Token: 0x04000866 RID: 2150
			public readonly BsonType Type;

			// Token: 0x04000867 RID: 2151
			public int Length;

			// Token: 0x04000868 RID: 2152
			public int Position;
		}
	}
}
