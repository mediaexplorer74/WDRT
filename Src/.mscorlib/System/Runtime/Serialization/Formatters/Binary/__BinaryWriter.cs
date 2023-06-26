using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000779 RID: 1913
	internal sealed class __BinaryWriter
	{
		// Token: 0x06005399 RID: 21401 RVA: 0x001275A4 File Offset: 0x001257A4
		internal __BinaryWriter(Stream sout, ObjectWriter objectWriter, FormatterTypeStyle formatterTypeStyle)
		{
			this.sout = sout;
			this.formatterTypeStyle = formatterTypeStyle;
			this.objectWriter = objectWriter;
			this.m_nestedObjectCount = 0;
			this.dataWriter = new BinaryWriter(sout, Encoding.UTF8);
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x001275E4 File Offset: 0x001257E4
		internal void WriteBegin()
		{
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x001275E6 File Offset: 0x001257E6
		internal void WriteEnd()
		{
			this.dataWriter.Flush();
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x001275F3 File Offset: 0x001257F3
		internal void WriteBoolean(bool value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x00127601 File Offset: 0x00125801
		internal void WriteByte(byte value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x0012760F File Offset: 0x0012580F
		private void WriteBytes(byte[] value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x0012761D File Offset: 0x0012581D
		private void WriteBytes(byte[] byteA, int offset, int size)
		{
			this.dataWriter.Write(byteA, offset, size);
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x0012762D File Offset: 0x0012582D
		internal void WriteChar(char value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x0012763B File Offset: 0x0012583B
		internal void WriteChars(char[] value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x00127649 File Offset: 0x00125849
		internal void WriteDecimal(decimal value)
		{
			this.WriteString(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x0012765D File Offset: 0x0012585D
		internal void WriteSingle(float value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x0012766B File Offset: 0x0012586B
		internal void WriteDouble(double value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x00127679 File Offset: 0x00125879
		internal void WriteInt16(short value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x00127687 File Offset: 0x00125887
		internal void WriteInt32(int value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x00127695 File Offset: 0x00125895
		internal void WriteInt64(long value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x001276A3 File Offset: 0x001258A3
		internal void WriteSByte(sbyte value)
		{
			this.WriteByte((byte)value);
		}

		// Token: 0x060053A9 RID: 21417 RVA: 0x001276AD File Offset: 0x001258AD
		internal void WriteString(string value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060053AA RID: 21418 RVA: 0x001276BB File Offset: 0x001258BB
		internal void WriteTimeSpan(TimeSpan value)
		{
			this.WriteInt64(value.Ticks);
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x001276CA File Offset: 0x001258CA
		internal void WriteDateTime(DateTime value)
		{
			this.WriteInt64(value.ToBinaryRaw());
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x001276D9 File Offset: 0x001258D9
		internal void WriteUInt16(ushort value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x001276E7 File Offset: 0x001258E7
		internal void WriteUInt32(uint value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x001276F5 File Offset: 0x001258F5
		internal void WriteUInt64(ulong value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x00127703 File Offset: 0x00125903
		internal void WriteObjectEnd(NameInfo memberNameInfo, NameInfo typeNameInfo)
		{
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x00127708 File Offset: 0x00125908
		internal void WriteSerializationHeaderEnd()
		{
			MessageEnd messageEnd = new MessageEnd();
			messageEnd.Dump(this.sout);
			messageEnd.Write(this);
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x00127730 File Offset: 0x00125930
		internal void WriteSerializationHeader(int topId, int headerId, int minorVersion, int majorVersion)
		{
			SerializationHeaderRecord serializationHeaderRecord = new SerializationHeaderRecord(BinaryHeaderEnum.SerializedStreamHeader, topId, headerId, minorVersion, majorVersion);
			serializationHeaderRecord.Dump();
			serializationHeaderRecord.Write(this);
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x00127756 File Offset: 0x00125956
		internal void WriteMethodCall()
		{
			if (this.binaryMethodCall == null)
			{
				this.binaryMethodCall = new BinaryMethodCall();
			}
			this.binaryMethodCall.Dump();
			this.binaryMethodCall.Write(this);
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x00127784 File Offset: 0x00125984
		internal object[] WriteCallArray(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, object callContext, object[] properties)
		{
			if (this.binaryMethodCall == null)
			{
				this.binaryMethodCall = new BinaryMethodCall();
			}
			return this.binaryMethodCall.WriteArray(uri, methodName, typeName, instArgs, args, methodSignature, callContext, properties);
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x001277BC File Offset: 0x001259BC
		internal void WriteMethodReturn()
		{
			if (this.binaryMethodReturn == null)
			{
				this.binaryMethodReturn = new BinaryMethodReturn();
			}
			this.binaryMethodReturn.Dump();
			this.binaryMethodReturn.Write(this);
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x001277E8 File Offset: 0x001259E8
		internal object[] WriteReturnArray(object returnValue, object[] args, Exception exception, object callContext, object[] properties)
		{
			if (this.binaryMethodReturn == null)
			{
				this.binaryMethodReturn = new BinaryMethodReturn();
			}
			return this.binaryMethodReturn.WriteArray(returnValue, args, exception, callContext, properties);
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x00127810 File Offset: 0x00125A10
		internal void WriteObject(NameInfo nameInfo, NameInfo typeNameInfo, int numMembers, string[] memberNames, Type[] memberTypes, WriteObjectInfo[] memberObjectInfos)
		{
			this.InternalWriteItemNull();
			int num = (int)nameInfo.NIobjectId;
			string text;
			if (num < 0)
			{
				text = typeNameInfo.NIname;
			}
			else
			{
				text = nameInfo.NIname;
			}
			if (this.objectMapTable == null)
			{
				this.objectMapTable = new Hashtable();
			}
			ObjectMapInfo objectMapInfo = (ObjectMapInfo)this.objectMapTable[text];
			if (objectMapInfo != null && objectMapInfo.isCompatible(numMembers, memberNames, memberTypes))
			{
				if (this.binaryObject == null)
				{
					this.binaryObject = new BinaryObject();
				}
				this.binaryObject.Set(num, objectMapInfo.objectId);
				this.binaryObject.Write(this);
				return;
			}
			if (!typeNameInfo.NItransmitTypeOnObject)
			{
				if (this.binaryObjectWithMap == null)
				{
					this.binaryObjectWithMap = new BinaryObjectWithMap();
				}
				int num2 = (int)typeNameInfo.NIassemId;
				this.binaryObjectWithMap.Set(num, text, numMembers, memberNames, num2);
				this.binaryObjectWithMap.Dump();
				this.binaryObjectWithMap.Write(this);
				if (objectMapInfo == null)
				{
					this.objectMapTable.Add(text, new ObjectMapInfo(num, numMembers, memberNames, memberTypes));
					return;
				}
			}
			else
			{
				BinaryTypeEnum[] array = new BinaryTypeEnum[numMembers];
				object[] array2 = new object[numMembers];
				int[] array3 = new int[numMembers];
				int num2;
				for (int i = 0; i < numMembers; i++)
				{
					object obj = null;
					array[i] = BinaryConverter.GetBinaryTypeInfo(memberTypes[i], memberObjectInfos[i], null, this.objectWriter, out obj, out num2);
					array2[i] = obj;
					array3[i] = num2;
				}
				if (this.binaryObjectWithMapTyped == null)
				{
					this.binaryObjectWithMapTyped = new BinaryObjectWithMapTyped();
				}
				num2 = (int)typeNameInfo.NIassemId;
				this.binaryObjectWithMapTyped.Set(num, text, numMembers, memberNames, array, array2, array3, num2);
				this.binaryObjectWithMapTyped.Write(this);
				if (objectMapInfo == null)
				{
					this.objectMapTable.Add(text, new ObjectMapInfo(num, numMembers, memberNames, memberTypes));
				}
			}
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x001279C4 File Offset: 0x00125BC4
		internal void WriteObjectString(int objectId, string value)
		{
			this.InternalWriteItemNull();
			if (this.binaryObjectString == null)
			{
				this.binaryObjectString = new BinaryObjectString();
			}
			this.binaryObjectString.Set(objectId, value);
			this.binaryObjectString.Write(this);
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x001279F8 File Offset: 0x00125BF8
		[SecurityCritical]
		internal void WriteSingleArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound, Array array)
		{
			this.InternalWriteItemNull();
			int[] array2 = new int[] { length };
			int[] array3 = null;
			object obj = null;
			BinaryArrayTypeEnum binaryArrayTypeEnum;
			if (lowerBound == 0)
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
			}
			else
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.SingleOffset;
				array3 = new int[] { lowerBound };
			}
			int num;
			BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out obj, out num);
			if (this.binaryArray == null)
			{
				this.binaryArray = new BinaryArray();
			}
			this.binaryArray.Set((int)arrayNameInfo.NIobjectId, 1, array2, array3, binaryTypeInfo, obj, binaryArrayTypeEnum, num);
			long niobjectId = arrayNameInfo.NIobjectId;
			this.binaryArray.Write(this);
			if (Converter.IsWriteAsByteArray(arrayElemTypeNameInfo.NIprimitiveTypeEnum) && lowerBound == 0)
			{
				if (arrayElemTypeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Byte)
				{
					this.WriteBytes((byte[])array);
					return;
				}
				if (arrayElemTypeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Char)
				{
					this.WriteChars((char[])array);
					return;
				}
				this.WriteArrayAsBytes(array, Converter.TypeLength(arrayElemTypeNameInfo.NIprimitiveTypeEnum));
			}
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x00127AEC File Offset: 0x00125CEC
		[SecurityCritical]
		private void WriteArrayAsBytes(Array array, int typeLength)
		{
			this.InternalWriteItemNull();
			int num = array.Length * typeLength;
			int i = 0;
			if (this.byteBuffer == null)
			{
				this.byteBuffer = new byte[this.chunkSize];
			}
			while (i < array.Length)
			{
				int num2 = Math.Min(this.chunkSize / typeLength, array.Length - i);
				int num3 = num2 * typeLength;
				Buffer.InternalBlockCopy(array, i * typeLength, this.byteBuffer, 0, num3);
				this.WriteBytes(this.byteBuffer, 0, num3);
				i += num2;
			}
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x00127B6C File Offset: 0x00125D6C
		internal void WriteJaggedArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound)
		{
			this.InternalWriteItemNull();
			int[] array = new int[] { length };
			int[] array2 = null;
			object obj = null;
			int num = 0;
			BinaryArrayTypeEnum binaryArrayTypeEnum;
			if (lowerBound == 0)
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.Jagged;
			}
			else
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.JaggedOffset;
				array2 = new int[] { lowerBound };
			}
			BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out obj, out num);
			if (this.binaryArray == null)
			{
				this.binaryArray = new BinaryArray();
			}
			this.binaryArray.Set((int)arrayNameInfo.NIobjectId, 1, array, array2, binaryTypeInfo, obj, binaryArrayTypeEnum, num);
			long niobjectId = arrayNameInfo.NIobjectId;
			this.binaryArray.Write(this);
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x00127C0C File Offset: 0x00125E0C
		internal void WriteRectangleArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int rank, int[] lengthA, int[] lowerBoundA)
		{
			this.InternalWriteItemNull();
			BinaryArrayTypeEnum binaryArrayTypeEnum = BinaryArrayTypeEnum.Rectangular;
			object obj = null;
			int num = 0;
			BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out obj, out num);
			if (this.binaryArray == null)
			{
				this.binaryArray = new BinaryArray();
			}
			for (int i = 0; i < rank; i++)
			{
				if (lowerBoundA[i] != 0)
				{
					binaryArrayTypeEnum = BinaryArrayTypeEnum.RectangularOffset;
					break;
				}
			}
			this.binaryArray.Set((int)arrayNameInfo.NIobjectId, rank, lengthA, lowerBoundA, binaryTypeInfo, obj, binaryArrayTypeEnum, num);
			long niobjectId = arrayNameInfo.NIobjectId;
			this.binaryArray.Write(this);
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x00127CA5 File Offset: 0x00125EA5
		[SecurityCritical]
		internal void WriteObjectByteArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound, byte[] byteA)
		{
			this.InternalWriteItemNull();
			this.WriteSingleArray(memberNameInfo, arrayNameInfo, objectInfo, arrayElemTypeNameInfo, length, lowerBound, byteA);
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x00127CC0 File Offset: 0x00125EC0
		internal void WriteMember(NameInfo memberNameInfo, NameInfo typeNameInfo, object value)
		{
			this.InternalWriteItemNull();
			InternalPrimitiveTypeE niprimitiveTypeEnum = typeNameInfo.NIprimitiveTypeEnum;
			if (memberNameInfo.NItransmitTypeOnMember)
			{
				if (this.memberPrimitiveTyped == null)
				{
					this.memberPrimitiveTyped = new MemberPrimitiveTyped();
				}
				this.memberPrimitiveTyped.Set(niprimitiveTypeEnum, value);
				bool niisArrayItem = memberNameInfo.NIisArrayItem;
				this.memberPrimitiveTyped.Dump();
				this.memberPrimitiveTyped.Write(this);
				return;
			}
			if (this.memberPrimitiveUnTyped == null)
			{
				this.memberPrimitiveUnTyped = new MemberPrimitiveUnTyped();
			}
			this.memberPrimitiveUnTyped.Set(niprimitiveTypeEnum, value);
			bool niisArrayItem2 = memberNameInfo.NIisArrayItem;
			this.memberPrimitiveUnTyped.Dump();
			this.memberPrimitiveUnTyped.Write(this);
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x00127D60 File Offset: 0x00125F60
		internal void WriteNullMember(NameInfo memberNameInfo, NameInfo typeNameInfo)
		{
			this.InternalWriteItemNull();
			if (this.objectNull == null)
			{
				this.objectNull = new ObjectNull();
			}
			if (!memberNameInfo.NIisArrayItem)
			{
				this.objectNull.SetNullCount(1);
				this.objectNull.Dump();
				this.objectNull.Write(this);
				this.nullCount = 0;
			}
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x00127DB8 File Offset: 0x00125FB8
		internal void WriteMemberObjectRef(NameInfo memberNameInfo, int idRef)
		{
			this.InternalWriteItemNull();
			if (this.memberReference == null)
			{
				this.memberReference = new MemberReference();
			}
			this.memberReference.Set(idRef);
			bool niisArrayItem = memberNameInfo.NIisArrayItem;
			this.memberReference.Dump();
			this.memberReference.Write(this);
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x00127E08 File Offset: 0x00126008
		internal void WriteMemberNested(NameInfo memberNameInfo)
		{
			this.InternalWriteItemNull();
			bool niisArrayItem = memberNameInfo.NIisArrayItem;
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x00127E17 File Offset: 0x00126017
		internal void WriteMemberString(NameInfo memberNameInfo, NameInfo typeNameInfo, string value)
		{
			this.InternalWriteItemNull();
			bool niisArrayItem = memberNameInfo.NIisArrayItem;
			this.WriteObjectString((int)typeNameInfo.NIobjectId, value);
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x00127E34 File Offset: 0x00126034
		internal void WriteItem(NameInfo itemNameInfo, NameInfo typeNameInfo, object value)
		{
			this.InternalWriteItemNull();
			this.WriteMember(itemNameInfo, typeNameInfo, value);
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x00127E45 File Offset: 0x00126045
		internal void WriteNullItem(NameInfo itemNameInfo, NameInfo typeNameInfo)
		{
			this.nullCount++;
			this.InternalWriteItemNull();
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x00127E5B File Offset: 0x0012605B
		internal void WriteDelayedNullItem()
		{
			this.nullCount++;
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x00127E6B File Offset: 0x0012606B
		internal void WriteItemEnd()
		{
			this.InternalWriteItemNull();
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x00127E74 File Offset: 0x00126074
		private void InternalWriteItemNull()
		{
			if (this.nullCount > 0)
			{
				if (this.objectNull == null)
				{
					this.objectNull = new ObjectNull();
				}
				this.objectNull.SetNullCount(this.nullCount);
				this.objectNull.Dump();
				this.objectNull.Write(this);
				this.nullCount = 0;
			}
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x00127ECC File Offset: 0x001260CC
		internal void WriteItemObjectRef(NameInfo nameInfo, int idRef)
		{
			this.InternalWriteItemNull();
			this.WriteMemberObjectRef(nameInfo, idRef);
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x00127EDC File Offset: 0x001260DC
		internal void WriteAssembly(Type type, string assemblyString, int assemId, bool isNew)
		{
			this.InternalWriteItemNull();
			if (assemblyString == null)
			{
				assemblyString = string.Empty;
			}
			if (isNew)
			{
				if (this.binaryAssembly == null)
				{
					this.binaryAssembly = new BinaryAssembly();
				}
				this.binaryAssembly.Set(assemId, assemblyString);
				this.binaryAssembly.Dump();
				this.binaryAssembly.Write(this);
			}
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x00127F34 File Offset: 0x00126134
		internal void WriteValue(InternalPrimitiveTypeE code, object value)
		{
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				this.WriteBoolean(Convert.ToBoolean(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Byte:
				this.WriteByte(Convert.ToByte(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Char:
				this.WriteChar(Convert.ToChar(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Decimal:
				this.WriteDecimal(Convert.ToDecimal(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Double:
				this.WriteDouble(Convert.ToDouble(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Int16:
				this.WriteInt16(Convert.ToInt16(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Int32:
				this.WriteInt32(Convert.ToInt32(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Int64:
				this.WriteInt64(Convert.ToInt64(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.SByte:
				this.WriteSByte(Convert.ToSByte(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Single:
				this.WriteSingle(Convert.ToSingle(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.TimeSpan:
				this.WriteTimeSpan((TimeSpan)value);
				return;
			case InternalPrimitiveTypeE.DateTime:
				this.WriteDateTime((DateTime)value);
				return;
			case InternalPrimitiveTypeE.UInt16:
				this.WriteUInt16(Convert.ToUInt16(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.UInt32:
				this.WriteUInt32(Convert.ToUInt32(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.UInt64:
				this.WriteUInt64(Convert.ToUInt64(value, CultureInfo.InvariantCulture));
				return;
			}
			throw new SerializationException(Environment.GetResourceString("Serialization_TypeCode", new object[] { code.ToString() }));
		}

		// Token: 0x040025A5 RID: 9637
		internal Stream sout;

		// Token: 0x040025A6 RID: 9638
		internal FormatterTypeStyle formatterTypeStyle;

		// Token: 0x040025A7 RID: 9639
		internal Hashtable objectMapTable;

		// Token: 0x040025A8 RID: 9640
		internal ObjectWriter objectWriter;

		// Token: 0x040025A9 RID: 9641
		internal BinaryWriter dataWriter;

		// Token: 0x040025AA RID: 9642
		internal int m_nestedObjectCount;

		// Token: 0x040025AB RID: 9643
		private int nullCount;

		// Token: 0x040025AC RID: 9644
		internal BinaryMethodCall binaryMethodCall;

		// Token: 0x040025AD RID: 9645
		internal BinaryMethodReturn binaryMethodReturn;

		// Token: 0x040025AE RID: 9646
		internal BinaryObject binaryObject;

		// Token: 0x040025AF RID: 9647
		internal BinaryObjectWithMap binaryObjectWithMap;

		// Token: 0x040025B0 RID: 9648
		internal BinaryObjectWithMapTyped binaryObjectWithMapTyped;

		// Token: 0x040025B1 RID: 9649
		internal BinaryObjectString binaryObjectString;

		// Token: 0x040025B2 RID: 9650
		internal BinaryCrossAppDomainString binaryCrossAppDomainString;

		// Token: 0x040025B3 RID: 9651
		internal BinaryArray binaryArray;

		// Token: 0x040025B4 RID: 9652
		private byte[] byteBuffer;

		// Token: 0x040025B5 RID: 9653
		private int chunkSize = 4096;

		// Token: 0x040025B6 RID: 9654
		internal MemberPrimitiveUnTyped memberPrimitiveUnTyped;

		// Token: 0x040025B7 RID: 9655
		internal MemberPrimitiveTyped memberPrimitiveTyped;

		// Token: 0x040025B8 RID: 9656
		internal ObjectNull objectNull;

		// Token: 0x040025B9 RID: 9657
		internal MemberReference memberReference;

		// Token: 0x040025BA RID: 9658
		internal BinaryAssembly binaryAssembly;

		// Token: 0x040025BB RID: 9659
		internal BinaryCrossAppDomainAssembly crossAppDomainAssembly;
	}
}
