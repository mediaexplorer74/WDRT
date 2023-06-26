using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;

namespace System.Resources
{
	/// <summary>Enumerates the resources in a binary resources (.resources) file by reading sequential resource name/value pairs.</summary>
	// Token: 0x02000397 RID: 919
	[ComVisible(true)]
	public sealed class ResourceReader : IResourceReader, IEnumerable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceReader" /> class for the specified named resource file.</summary>
		/// <param name="fileName">The path and name of the resource file to read. filename is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred.</exception>
		/// <exception cref="T:System.BadImageFormatException">The resource file has an invalid format. For example, the length of the file may be zero.</exception>
		// Token: 0x06002D60 RID: 11616 RVA: 0x000AD304 File Offset: 0x000AB504
		[SecuritySafeCritical]
		public ResourceReader(string fileName)
		{
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._store = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.RandomAccess, Path.GetFileName(fileName), false), Encoding.UTF8);
			try
			{
				this.ReadResources();
			}
			catch
			{
				this._store.Close();
				throw;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceReader" /> class for the specified stream.</summary>
		/// <param name="stream">The input stream for reading resources.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is not readable.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred while accessing <paramref name="stream" />.</exception>
		// Token: 0x06002D61 RID: 11617 RVA: 0x000AD378 File Offset: 0x000AB578
		[SecurityCritical]
		public ResourceReader(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"));
			}
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._store = new BinaryReader(stream, Encoding.UTF8);
			this._ums = stream as UnmanagedMemoryStream;
			this.ReadResources();
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000AD3E4 File Offset: 0x000AB5E4
		[SecurityCritical]
		internal ResourceReader(Stream stream, Dictionary<string, ResourceLocator> resCache)
		{
			this._resCache = resCache;
			this._store = new BinaryReader(stream, Encoding.UTF8);
			this._ums = stream as UnmanagedMemoryStream;
			this.ReadResources();
		}

		/// <summary>Releases all operating system resources associated with this <see cref="T:System.Resources.ResourceReader" /> object.</summary>
		// Token: 0x06002D63 RID: 11619 RVA: 0x000AD416 File Offset: 0x000AB616
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Resources.ResourceReader" /> class.</summary>
		// Token: 0x06002D64 RID: 11620 RVA: 0x000AD41F File Offset: 0x000AB61F
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000AD428 File Offset: 0x000AB628
		[SecuritySafeCritical]
		private void Dispose(bool disposing)
		{
			if (this._store != null)
			{
				this._resCache = null;
				if (disposing)
				{
					BinaryReader store = this._store;
					this._store = null;
					if (store != null)
					{
						store.Close();
					}
				}
				this._store = null;
				this._namePositions = null;
				this._nameHashes = null;
				this._ums = null;
				this._namePositionsPtr = null;
				this._nameHashesPtr = null;
			}
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000AD48C File Offset: 0x000AB68C
		[SecurityCritical]
		internal unsafe static int ReadUnalignedI4(int* p)
		{
			return (int)(*(byte*)p) | ((int)((byte*)p)[1] << 8) | ((int)((byte*)p)[2] << 16) | ((int)((byte*)p)[3] << 24);
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000AD4B4 File Offset: 0x000AB6B4
		private void SkipInt32()
		{
			this._store.BaseStream.Seek(4L, SeekOrigin.Current);
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000AD4CC File Offset: 0x000AB6CC
		private void SkipString()
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
			}
			this._store.BaseStream.Seek((long)num, SeekOrigin.Current);
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000AD50D File Offset: 0x000AB70D
		[SecuritySafeCritical]
		private int GetNameHash(int index)
		{
			if (this._ums == null)
			{
				return this._nameHashes[index];
			}
			return ResourceReader.ReadUnalignedI4(this._nameHashesPtr + index);
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000AD534 File Offset: 0x000AB734
		[SecuritySafeCritical]
		private int GetNamePosition(int index)
		{
			int num;
			if (this._ums == null)
			{
				num = this._namePositions[index];
			}
			else
			{
				num = ResourceReader.ReadUnalignedI4(this._namePositionsPtr + index);
			}
			if (num < 0 || (long)num > this._dataSectionOffset - this._nameSectionOffset)
			{
				throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameInvalidOffset", new object[] { num }));
			}
			return num;
		}

		/// <summary>Returns an enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</summary>
		/// <returns>An enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The reader has already been closed and cannot be accessed.</exception>
		// Token: 0x06002D6B RID: 11627 RVA: 0x000AD59B File Offset: 0x000AB79B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</summary>
		/// <returns>An enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The reader has been closed or disposed, and cannot be accessed.</exception>
		// Token: 0x06002D6C RID: 11628 RVA: 0x000AD5A3 File Offset: 0x000AB7A3
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this._resCache == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
			}
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000AD5C3 File Offset: 0x000AB7C3
		internal ResourceReader.ResourceEnumerator GetEnumeratorInternal()
		{
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000AD5CC File Offset: 0x000AB7CC
		internal int FindPosForResource(string name)
		{
			int num = FastResourceComparer.HashFunction(name);
			int i = 0;
			int num2 = this._numResources - 1;
			int num3 = -1;
			bool flag = false;
			while (i <= num2)
			{
				num3 = i + num2 >> 1;
				int nameHash = this.GetNameHash(num3);
				int num4;
				if (nameHash == num)
				{
					num4 = 0;
				}
				else if (nameHash < num)
				{
					num4 = -1;
				}
				else
				{
					num4 = 1;
				}
				if (num4 == 0)
				{
					flag = true;
					break;
				}
				if (num4 < 0)
				{
					i = num3 + 1;
				}
				else
				{
					num2 = num3 - 1;
				}
			}
			if (!flag)
			{
				return -1;
			}
			if (i != num3)
			{
				i = num3;
				while (i > 0 && this.GetNameHash(i - 1) == num)
				{
					i--;
				}
			}
			if (num2 != num3)
			{
				num2 = num3;
				while (num2 < this._numResources - 1 && this.GetNameHash(num2 + 1) == num)
				{
					num2++;
				}
			}
			lock (this)
			{
				int j = i;
				while (j <= num2)
				{
					this._store.BaseStream.Seek(this._nameSectionOffset + (long)this.GetNamePosition(j), SeekOrigin.Begin);
					if (this.CompareStringEqualsName(name))
					{
						int num5 = this._store.ReadInt32();
						if (num5 < 0 || (long)num5 >= this._store.BaseStream.Length - this._dataSectionOffset)
						{
							throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[] { num5 }));
						}
						return num5;
					}
					else
					{
						j++;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000AD740 File Offset: 0x000AB940
		[SecuritySafeCritical]
		private unsafe bool CompareStringEqualsName(string name)
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
			}
			if (this._ums == null)
			{
				byte[] array = new byte[num];
				int num2;
				for (int i = num; i > 0; i -= num2)
				{
					num2 = this._store.Read(array, num - i, i);
					if (num2 == 0)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted"));
					}
				}
				return FastResourceComparer.CompareOrdinal(array, num / 2, name) == 0;
			}
			byte* positionPointer = this._ums.PositionPointer;
			this._ums.Seek((long)num, SeekOrigin.Current);
			if (this._ums.Position > this._ums.Length)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameTooLong"));
			}
			return FastResourceComparer.CompareOrdinal(positionPointer, num, name) == 0;
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000AD80C File Offset: 0x000ABA0C
		[SecurityCritical]
		private unsafe string AllocateStringForNameIndex(int index, out int dataOffset)
		{
			long num = (long)this.GetNamePosition(index);
			int num2;
			byte[] array;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				num2 = this._store.Read7BitEncodedInt();
				if (num2 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
				}
				if (this._ums != null)
				{
					if (this._ums.Position > this._ums.Length - (long)num2)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesIndexTooLong", new object[] { index }));
					}
					char* positionPointer = (char*)this._ums.PositionPointer;
					string text = new string(positionPointer, 0, num2 / 2);
					this._ums.Position += (long)num2;
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[] { dataOffset }));
					}
					return text;
				}
				else
				{
					array = new byte[num2];
					int num3;
					for (int i = num2; i > 0; i -= num3)
					{
						num3 = this._store.Read(array, num2 - i, i);
						if (num3 == 0)
						{
							throw new EndOfStreamException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted_NameIndex", new object[] { index }));
						}
					}
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[] { dataOffset }));
					}
				}
			}
			return Encoding.Unicode.GetString(array, 0, num2);
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000ADA0C File Offset: 0x000ABC0C
		private object GetValueForNameIndex(int index)
		{
			long num = (long)this.GetNamePosition(index);
			object obj;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				this.SkipString();
				int num2 = this._store.ReadInt32();
				if (num2 < 0 || (long)num2 >= this._store.BaseStream.Length - this._dataSectionOffset)
				{
					throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[] { num2 }));
				}
				if (this._version == 1)
				{
					obj = this.LoadObjectV1(num2);
				}
				else
				{
					ResourceTypeCode resourceTypeCode;
					obj = this.LoadObjectV2(num2, out resourceTypeCode);
				}
			}
			return obj;
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000ADAD8 File Offset: 0x000ABCD8
		internal string LoadString(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			string text = null;
			int num = this._store.Read7BitEncodedInt();
			if (this._version == 1)
			{
				if (num == -1)
				{
					return null;
				}
				if (this.FindType(num) != typeof(string))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Type", new object[] { this.FindType(num).FullName }));
				}
				text = this._store.ReadString();
			}
			else
			{
				ResourceTypeCode resourceTypeCode = (ResourceTypeCode)num;
				if (resourceTypeCode != ResourceTypeCode.String && resourceTypeCode != ResourceTypeCode.Null)
				{
					string text2;
					if (resourceTypeCode < ResourceTypeCode.StartOfUserTypes)
					{
						text2 = resourceTypeCode.ToString();
					}
					else
					{
						text2 = this.FindType(resourceTypeCode - ResourceTypeCode.StartOfUserTypes).FullName;
					}
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Type", new object[] { text2 }));
				}
				if (resourceTypeCode == ResourceTypeCode.String)
				{
					text = this._store.ReadString();
				}
			}
			return text;
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000ADBC4 File Offset: 0x000ABDC4
		internal object LoadObject(int pos)
		{
			if (this._version == 1)
			{
				return this.LoadObjectV1(pos);
			}
			ResourceTypeCode resourceTypeCode;
			return this.LoadObjectV2(pos, out resourceTypeCode);
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000ADBEC File Offset: 0x000ABDEC
		internal object LoadObject(int pos, out ResourceTypeCode typeCode)
		{
			if (this._version == 1)
			{
				object obj = this.LoadObjectV1(pos);
				typeCode = ((obj is string) ? ResourceTypeCode.String : ResourceTypeCode.StartOfUserTypes);
				return obj;
			}
			return this.LoadObjectV2(pos, out typeCode);
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000ADC24 File Offset: 0x000ABE24
		internal object LoadObjectV1(int pos)
		{
			object obj;
			try
			{
				obj = this._LoadObjectV1(pos);
			}
			catch (EndOfStreamException ex)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), ex);
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), ex2);
			}
			return obj;
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x000ADC7C File Offset: 0x000ABE7C
		[SecuritySafeCritical]
		private object _LoadObjectV1(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			int num = this._store.Read7BitEncodedInt();
			if (num == -1)
			{
				return null;
			}
			RuntimeType runtimeType = this.FindType(num);
			if (runtimeType == typeof(string))
			{
				return this._store.ReadString();
			}
			if (runtimeType == typeof(int))
			{
				return this._store.ReadInt32();
			}
			if (runtimeType == typeof(byte))
			{
				return this._store.ReadByte();
			}
			if (runtimeType == typeof(sbyte))
			{
				return this._store.ReadSByte();
			}
			if (runtimeType == typeof(short))
			{
				return this._store.ReadInt16();
			}
			if (runtimeType == typeof(long))
			{
				return this._store.ReadInt64();
			}
			if (runtimeType == typeof(ushort))
			{
				return this._store.ReadUInt16();
			}
			if (runtimeType == typeof(uint))
			{
				return this._store.ReadUInt32();
			}
			if (runtimeType == typeof(ulong))
			{
				return this._store.ReadUInt64();
			}
			if (runtimeType == typeof(float))
			{
				return this._store.ReadSingle();
			}
			if (runtimeType == typeof(double))
			{
				return this._store.ReadDouble();
			}
			if (runtimeType == typeof(DateTime))
			{
				return new DateTime(this._store.ReadInt64());
			}
			if (runtimeType == typeof(TimeSpan))
			{
				return new TimeSpan(this._store.ReadInt64());
			}
			if (runtimeType == typeof(decimal))
			{
				int[] array = new int[4];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._store.ReadInt32();
				}
				return new decimal(array);
			}
			return this.DeserializeObject(num);
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000ADED4 File Offset: 0x000AC0D4
		internal object LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			object obj;
			try
			{
				obj = this._LoadObjectV2(pos, out typeCode);
			}
			catch (EndOfStreamException ex)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), ex);
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), ex2);
			}
			return obj;
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000ADF30 File Offset: 0x000AC130
		[SecuritySafeCritical]
		private object _LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			typeCode = (ResourceTypeCode)this._store.Read7BitEncodedInt();
			switch (typeCode)
			{
			case ResourceTypeCode.Null:
				return null;
			case ResourceTypeCode.String:
				return this._store.ReadString();
			case ResourceTypeCode.Boolean:
				return this._store.ReadBoolean();
			case ResourceTypeCode.Char:
				return (char)this._store.ReadUInt16();
			case ResourceTypeCode.Byte:
				return this._store.ReadByte();
			case ResourceTypeCode.SByte:
				return this._store.ReadSByte();
			case ResourceTypeCode.Int16:
				return this._store.ReadInt16();
			case ResourceTypeCode.UInt16:
				return this._store.ReadUInt16();
			case ResourceTypeCode.Int32:
				return this._store.ReadInt32();
			case ResourceTypeCode.UInt32:
				return this._store.ReadUInt32();
			case ResourceTypeCode.Int64:
				return this._store.ReadInt64();
			case ResourceTypeCode.UInt64:
				return this._store.ReadUInt64();
			case ResourceTypeCode.Single:
				return this._store.ReadSingle();
			case ResourceTypeCode.Double:
				return this._store.ReadDouble();
			case ResourceTypeCode.Decimal:
				return this._store.ReadDecimal();
			case ResourceTypeCode.DateTime:
			{
				long num = this._store.ReadInt64();
				return DateTime.FromBinary(num);
			}
			case ResourceTypeCode.TimeSpan:
			{
				long num2 = this._store.ReadInt64();
				return new TimeSpan(num2);
			}
			case ResourceTypeCode.ByteArray:
			{
				int num3 = this._store.ReadInt32();
				if (num3 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[] { num3 }));
				}
				if (this._ums == null)
				{
					if ((long)num3 > this._store.BaseStream.Length)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[] { num3 }));
					}
					return this._store.ReadBytes(num3);
				}
				else
				{
					if ((long)num3 > this._ums.Length - this._ums.Position)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[] { num3 }));
					}
					byte[] array = new byte[num3];
					int num4 = this._ums.Read(array, 0, num3);
					return array;
				}
				break;
			}
			case ResourceTypeCode.Stream:
			{
				int num5 = this._store.ReadInt32();
				if (num5 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[] { num5 }));
				}
				if (this._ums == null)
				{
					byte[] array2 = this._store.ReadBytes(num5);
					return new PinnedBufferMemoryStream(array2);
				}
				if ((long)num5 > this._ums.Length - this._ums.Position)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", new object[] { num5 }));
				}
				return new UnmanagedMemoryStream(this._ums.PositionPointer, (long)num5, (long)num5, FileAccess.Read, true);
			}
			}
			if (typeCode < ResourceTypeCode.StartOfUserTypes)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"));
			}
			int num6 = typeCode - ResourceTypeCode.StartOfUserTypes;
			return this.DeserializeObject(num6);
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000AE2B8 File Offset: 0x000AC4B8
		[SecurityCritical]
		private object DeserializeObject(int typeIndex)
		{
			RuntimeType runtimeType = this.FindType(typeIndex);
			if (this._safeToDeserialize == null)
			{
				this.InitSafeToDeserializeArray();
			}
			object obj;
			if (this._safeToDeserialize[typeIndex])
			{
				this._objFormatter.Binder = this._typeLimitingBinder;
				this._typeLimitingBinder.ExpectingToDeserialize(runtimeType);
				obj = this._objFormatter.UnsafeDeserialize(this._store.BaseStream, null);
			}
			else
			{
				this._objFormatter.Binder = null;
				obj = this._objFormatter.Deserialize(this._store.BaseStream);
			}
			if (obj.GetType() != runtimeType)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResType&SerBlobMismatch", new object[]
				{
					runtimeType.FullName,
					obj.GetType().FullName
				}));
			}
			return obj;
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000AE37C File Offset: 0x000AC57C
		[SecurityCritical]
		private void ReadResources()
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
			this._typeLimitingBinder = new ResourceReader.TypeLimitingDeserializationBinder();
			binaryFormatter.Binder = this._typeLimitingBinder;
			this._objFormatter = binaryFormatter;
			try
			{
				this._ReadResources();
			}
			catch (EndOfStreamException ex)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"), ex);
			}
			catch (IndexOutOfRangeException ex2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"), ex2);
			}
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x000AE400 File Offset: 0x000AC600
		[SecurityCritical]
		private unsafe void _ReadResources()
		{
			int num = this._store.ReadInt32();
			if (num != ResourceManager.MagicNumber)
			{
				throw new ArgumentException(Environment.GetResourceString("Resources_StreamNotValid"));
			}
			int num2 = this._store.ReadInt32();
			int num3 = this._store.ReadInt32();
			if (num3 < 0 || num2 < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
			if (num2 > 1)
			{
				this._store.BaseStream.Seek((long)num3, SeekOrigin.Current);
			}
			else
			{
				string text = this._store.ReadString();
				AssemblyName assemblyName = new AssemblyName(ResourceManager.MscorlibName);
				if (!ResourceManager.CompareNames(text, ResourceManager.ResReaderTypeName, assemblyName))
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_WrongResourceReader_Type", new object[] { text }));
				}
				this.SkipString();
			}
			int num4 = this._store.ReadInt32();
			if (num4 != 2 && num4 != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ResourceFileUnsupportedVersion", new object[] { 2, num4 }));
			}
			this._version = num4;
			this._numResources = this._store.ReadInt32();
			if (this._numResources < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
			int num5 = this._store.ReadInt32();
			if (num5 < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
			this._typeTable = new RuntimeType[num5];
			this._typeNamePositions = new int[num5];
			for (int i = 0; i < num5; i++)
			{
				this._typeNamePositions[i] = (int)this._store.BaseStream.Position;
				this.SkipString();
			}
			long position = this._store.BaseStream.Position;
			int num6 = (int)position & 7;
			if (num6 != 0)
			{
				for (int j = 0; j < 8 - num6; j++)
				{
					this._store.ReadByte();
				}
			}
			if (this._ums == null)
			{
				this._nameHashes = new int[this._numResources];
				for (int k = 0; k < this._numResources; k++)
				{
					this._nameHashes[k] = this._store.ReadInt32();
				}
			}
			else
			{
				if (((long)this._numResources & (long)((ulong)(-536870912))) != 0L)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
				}
				int num7 = 4 * this._numResources;
				this._nameHashesPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num7, SeekOrigin.Current);
				byte* positionPointer = this._ums.PositionPointer;
			}
			if (this._ums == null)
			{
				this._namePositions = new int[this._numResources];
				for (int l = 0; l < this._numResources; l++)
				{
					int num8 = this._store.ReadInt32();
					if (num8 < 0)
					{
						throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
					}
					this._namePositions[l] = num8;
				}
			}
			else
			{
				if (((long)this._numResources & (long)((ulong)(-536870912))) != 0L)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
				}
				int num9 = 4 * this._numResources;
				this._namePositionsPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num9, SeekOrigin.Current);
				byte* positionPointer2 = this._ums.PositionPointer;
			}
			this._dataSectionOffset = (long)this._store.ReadInt32();
			if (this._dataSectionOffset < 0L)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
			this._nameSectionOffset = this._store.BaseStream.Position;
			if (this._dataSectionOffset < this._nameSectionOffset)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
			}
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000AE78C File Offset: 0x000AC98C
		private RuntimeType FindType(int typeIndex)
		{
			if (typeIndex < 0 || typeIndex >= this._typeTable.Length)
			{
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_InvalidType"));
			}
			if (this._typeTable[typeIndex] == null)
			{
				long position = this._store.BaseStream.Position;
				try
				{
					this._store.BaseStream.Position = (long)this._typeNamePositions[typeIndex];
					string text = this._store.ReadString();
					this._typeTable[typeIndex] = (RuntimeType)Type.GetType(text, true);
				}
				finally
				{
					this._store.BaseStream.Position = position;
				}
			}
			return this._typeTable[typeIndex];
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000AE840 File Offset: 0x000ACA40
		[SecurityCritical]
		private void InitSafeToDeserializeArray()
		{
			this._safeToDeserialize = new bool[this._typeTable.Length];
			int i = 0;
			while (i < this._typeTable.Length)
			{
				long position = this._store.BaseStream.Position;
				string text;
				try
				{
					this._store.BaseStream.Position = (long)this._typeNamePositions[i];
					text = this._store.ReadString();
				}
				finally
				{
					this._store.BaseStream.Position = position;
				}
				RuntimeType runtimeType = (RuntimeType)Type.GetType(text, false);
				if (runtimeType == null)
				{
					AssemblyName assemblyName = null;
					string text2 = text;
					goto IL_E5;
				}
				if (!(runtimeType.BaseType == typeof(Enum)))
				{
					string text2 = runtimeType.FullName;
					AssemblyName assemblyName = new AssemblyName();
					RuntimeAssembly runtimeAssembly = (RuntimeAssembly)runtimeType.Assembly;
					assemblyName.Init(runtimeAssembly.GetSimpleName(), runtimeAssembly.GetPublicKey(), null, null, runtimeAssembly.GetLocale(), AssemblyHashAlgorithm.None, AssemblyVersionCompatibility.SameMachine, null, AssemblyNameFlags.PublicKey, null);
					goto IL_E5;
				}
				this._safeToDeserialize[i] = true;
				IL_11B:
				i++;
				continue;
				IL_E5:
				foreach (string text3 in ResourceReader.TypesSafeForDeserialization)
				{
					AssemblyName assemblyName;
					string text2;
					if (ResourceManager.CompareNames(text3, text2, assemblyName))
					{
						this._safeToDeserialize[i] = true;
					}
				}
				goto IL_11B;
			}
		}

		/// <summary>Retrieves the type name and data of a named resource from an open resource file or stream.</summary>
		/// <param name="resourceName">The name of a resource.</param>
		/// <param name="resourceType">When this method returns, contains a string that represents the type name of the retrieved resource. This parameter is passed uninitialized.</param>
		/// <param name="resourceData">When this method returns, contains a byte array that is the binary representation of the retrieved type. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="resourceName" /> does not exist.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="resourceName" /> has an invalid type.</exception>
		/// <exception cref="T:System.FormatException">The retrieved resource data is corrupt.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Resources.ResourceReader" /> object is not initialized, probably because it is closed.</exception>
		// Token: 0x06002D7E RID: 11646 RVA: 0x000AE98C File Offset: 0x000ACB8C
		public void GetResourceData(string resourceName, out string resourceType, out byte[] resourceData)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			if (this._resCache == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
			}
			int[] array = new int[this._numResources];
			int num = this.FindPosForResource(resourceName);
			if (num == -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ResourceNameNotExist", new object[] { resourceName }));
			}
			lock (this)
			{
				for (int i = 0; i < this._numResources; i++)
				{
					this._store.BaseStream.Position = this._nameSectionOffset + (long)this.GetNamePosition(i);
					int num2 = this._store.Read7BitEncodedInt();
					if (num2 < 0)
					{
						throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameInvalidOffset", new object[] { num2 }));
					}
					this._store.BaseStream.Position += (long)num2;
					int num3 = this._store.ReadInt32();
					if (num3 < 0 || (long)num3 >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", new object[] { num3 }));
					}
					array[i] = num3;
				}
				Array.Sort<int>(array);
				int num4 = Array.BinarySearch<int>(array, num);
				long num5 = ((num4 < this._numResources - 1) ? ((long)array[num4 + 1] + this._dataSectionOffset) : this._store.BaseStream.Length);
				int num6 = (int)(num5 - ((long)num + this._dataSectionOffset));
				this._store.BaseStream.Position = this._dataSectionOffset + (long)num;
				ResourceTypeCode resourceTypeCode = (ResourceTypeCode)this._store.Read7BitEncodedInt();
				if (resourceTypeCode < ResourceTypeCode.Null || resourceTypeCode >= ResourceTypeCode.StartOfUserTypes + this._typeTable.Length)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_InvalidType"));
				}
				resourceType = this.TypeNameFromTypeCode(resourceTypeCode);
				num6 -= (int)(this._store.BaseStream.Position - (this._dataSectionOffset + (long)num));
				byte[] array2 = this._store.ReadBytes(num6);
				if (array2.Length != num6)
				{
					throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted"));
				}
				resourceData = array2;
			}
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000AEBEC File Offset: 0x000ACDEC
		private string TypeNameFromTypeCode(ResourceTypeCode typeCode)
		{
			if (typeCode < ResourceTypeCode.StartOfUserTypes)
			{
				return "ResourceTypeCode." + typeCode.ToString();
			}
			int num = typeCode - ResourceTypeCode.StartOfUserTypes;
			long position = this._store.BaseStream.Position;
			string text;
			try
			{
				this._store.BaseStream.Position = (long)this._typeNamePositions[num];
				text = this._store.ReadString();
			}
			finally
			{
				this._store.BaseStream.Position = position;
			}
			return text;
		}

		// Token: 0x04001261 RID: 4705
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x04001262 RID: 4706
		private BinaryReader _store;

		// Token: 0x04001263 RID: 4707
		internal Dictionary<string, ResourceLocator> _resCache;

		// Token: 0x04001264 RID: 4708
		private long _nameSectionOffset;

		// Token: 0x04001265 RID: 4709
		private long _dataSectionOffset;

		// Token: 0x04001266 RID: 4710
		private int[] _nameHashes;

		// Token: 0x04001267 RID: 4711
		[SecurityCritical]
		private unsafe int* _nameHashesPtr;

		// Token: 0x04001268 RID: 4712
		private int[] _namePositions;

		// Token: 0x04001269 RID: 4713
		[SecurityCritical]
		private unsafe int* _namePositionsPtr;

		// Token: 0x0400126A RID: 4714
		private RuntimeType[] _typeTable;

		// Token: 0x0400126B RID: 4715
		private int[] _typeNamePositions;

		// Token: 0x0400126C RID: 4716
		private BinaryFormatter _objFormatter;

		// Token: 0x0400126D RID: 4717
		private int _numResources;

		// Token: 0x0400126E RID: 4718
		private UnmanagedMemoryStream _ums;

		// Token: 0x0400126F RID: 4719
		private int _version;

		// Token: 0x04001270 RID: 4720
		private bool[] _safeToDeserialize;

		// Token: 0x04001271 RID: 4721
		private ResourceReader.TypeLimitingDeserializationBinder _typeLimitingBinder;

		// Token: 0x04001272 RID: 4722
		private static readonly string[] TypesSafeForDeserialization = new string[]
		{
			"System.String[], mscorlib, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.DateTime[], mscorlib, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Bitmap, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Imaging.Metafile, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Point, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.PointF, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Size, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.SizeF, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Font, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Icon, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			"System.Drawing.Color, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Windows.Forms.Cursor, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.Padding, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.LinkArea, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ImageListStreamer, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListViewGroup, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListViewItem, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListViewItem+ListViewSubItem, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.ListViewItem+ListViewSubItem+SubItemStyle, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Windows.Forms.OwnerDrawPropertyBag, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
			"System.Windows.Forms.TreeNode, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089"
		};

		// Token: 0x02000B61 RID: 2913
		internal sealed class TypeLimitingDeserializationBinder : SerializationBinder
		{
			// Token: 0x17001248 RID: 4680
			// (get) Token: 0x06006C31 RID: 27697 RVA: 0x00177784 File Offset: 0x00175984
			// (set) Token: 0x06006C32 RID: 27698 RVA: 0x0017778C File Offset: 0x0017598C
			internal ObjectReader ObjectReader
			{
				get
				{
					return this._objectReader;
				}
				set
				{
					this._objectReader = value;
				}
			}

			// Token: 0x06006C33 RID: 27699 RVA: 0x00177795 File Offset: 0x00175995
			internal void ExpectingToDeserialize(RuntimeType type)
			{
				this._typeToDeserialize = type;
			}

			// Token: 0x06006C34 RID: 27700 RVA: 0x001777A0 File Offset: 0x001759A0
			[SecuritySafeCritical]
			public override Type BindToType(string assemblyName, string typeName)
			{
				AssemblyName assemblyName2 = new AssemblyName(assemblyName);
				bool flag = false;
				foreach (string text in ResourceReader.TypesSafeForDeserialization)
				{
					if (ResourceManager.CompareNames(text, typeName, assemblyName2))
					{
						flag = true;
						break;
					}
				}
				Type type = this.ObjectReader.FastBindToType(assemblyName, typeName);
				if (type.IsEnum)
				{
					flag = true;
				}
				if (flag)
				{
					return null;
				}
				throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResType&SerBlobMismatch", new object[]
				{
					this._typeToDeserialize.FullName,
					typeName
				}));
			}

			// Token: 0x04003447 RID: 13383
			private RuntimeType _typeToDeserialize;

			// Token: 0x04003448 RID: 13384
			private ObjectReader _objectReader;
		}

		// Token: 0x02000B62 RID: 2914
		internal sealed class ResourceEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006C36 RID: 27702 RVA: 0x0017782F File Offset: 0x00175A2F
			internal ResourceEnumerator(ResourceReader reader)
			{
				this._currentName = -1;
				this._reader = reader;
				this._dataPosition = -2;
			}

			// Token: 0x06006C37 RID: 27703 RVA: 0x00177850 File Offset: 0x00175A50
			public bool MoveNext()
			{
				if (this._currentName == this._reader._numResources - 1 || this._currentName == -2147483648)
				{
					this._currentIsValid = false;
					this._currentName = int.MinValue;
					return false;
				}
				this._currentIsValid = true;
				this._currentName++;
				return true;
			}

			// Token: 0x17001249 RID: 4681
			// (get) Token: 0x06006C38 RID: 27704 RVA: 0x001778AC File Offset: 0x00175AAC
			public object Key
			{
				[SecuritySafeCritical]
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
					}
					return this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
				}
			}

			// Token: 0x1700124A RID: 4682
			// (get) Token: 0x06006C39 RID: 27705 RVA: 0x00177922 File Offset: 0x00175B22
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x1700124B RID: 4683
			// (get) Token: 0x06006C3A RID: 27706 RVA: 0x0017792F File Offset: 0x00175B2F
			internal int DataPosition
			{
				get
				{
					return this._dataPosition;
				}
			}

			// Token: 0x1700124C RID: 4684
			// (get) Token: 0x06006C3B RID: 27707 RVA: 0x00177938 File Offset: 0x00175B38
			public DictionaryEntry Entry
			{
				[SecuritySafeCritical]
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
					}
					object obj = null;
					ResourceReader reader = this._reader;
					string text;
					lock (reader)
					{
						Dictionary<string, ResourceLocator> resCache = this._reader._resCache;
						lock (resCache)
						{
							text = this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
							ResourceLocator resourceLocator;
							if (this._reader._resCache.TryGetValue(text, out resourceLocator))
							{
								obj = resourceLocator.Value;
							}
							if (obj == null)
							{
								if (this._dataPosition == -1)
								{
									obj = this._reader.GetValueForNameIndex(this._currentName);
								}
								else
								{
									obj = this._reader.LoadObject(this._dataPosition);
								}
							}
						}
					}
					return new DictionaryEntry(text, obj);
				}
			}

			// Token: 0x1700124D RID: 4685
			// (get) Token: 0x06006C3C RID: 27708 RVA: 0x00177A68 File Offset: 0x00175C68
			public object Value
			{
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
					}
					return this._reader.GetValueForNameIndex(this._currentName);
				}
			}

			// Token: 0x06006C3D RID: 27709 RVA: 0x00177AD8 File Offset: 0x00175CD8
			public void Reset()
			{
				if (this._reader._resCache == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
				}
				this._currentIsValid = false;
				this._currentName = -1;
			}

			// Token: 0x04003449 RID: 13385
			private const int ENUM_DONE = -2147483648;

			// Token: 0x0400344A RID: 13386
			private const int ENUM_NOT_STARTED = -1;

			// Token: 0x0400344B RID: 13387
			private ResourceReader _reader;

			// Token: 0x0400344C RID: 13388
			private bool _currentIsValid;

			// Token: 0x0400344D RID: 13389
			private int _currentName;

			// Token: 0x0400344E RID: 13390
			private int _dataPosition;
		}
	}
}
