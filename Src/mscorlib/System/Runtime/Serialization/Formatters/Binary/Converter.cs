﻿using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200079E RID: 1950
	internal sealed class Converter
	{
		// Token: 0x060054C4 RID: 21700 RVA: 0x0012DD4A File Offset: 0x0012BF4A
		private Converter()
		{
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x0012DD54 File Offset: 0x0012BF54
		internal static InternalPrimitiveTypeE ToCode(Type type)
		{
			InternalPrimitiveTypeE internalPrimitiveTypeE;
			if (type != null && !type.IsPrimitive)
			{
				if (type == Converter.typeofDateTime)
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.DateTime;
				}
				else if (type == Converter.typeofTimeSpan)
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.TimeSpan;
				}
				else if (type == Converter.typeofDecimal)
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.Decimal;
				}
				else
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.Invalid;
				}
			}
			else
			{
				internalPrimitiveTypeE = Converter.ToPrimitiveTypeEnum(Type.GetTypeCode(type));
			}
			return internalPrimitiveTypeE;
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x0012DDA4 File Offset: 0x0012BFA4
		internal static bool IsWriteAsByteArray(InternalPrimitiveTypeE code)
		{
			bool flag = false;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Char:
			case InternalPrimitiveTypeE.Double:
			case InternalPrimitiveTypeE.Int16:
			case InternalPrimitiveTypeE.Int32:
			case InternalPrimitiveTypeE.Int64:
			case InternalPrimitiveTypeE.SByte:
			case InternalPrimitiveTypeE.Single:
			case InternalPrimitiveTypeE.UInt16:
			case InternalPrimitiveTypeE.UInt32:
			case InternalPrimitiveTypeE.UInt64:
				flag = true;
				break;
			}
			return flag;
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x0012DE00 File Offset: 0x0012C000
		internal static int TypeLength(InternalPrimitiveTypeE code)
		{
			int num = 0;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				num = 1;
				break;
			case InternalPrimitiveTypeE.Byte:
				num = 1;
				break;
			case InternalPrimitiveTypeE.Char:
				num = 2;
				break;
			case InternalPrimitiveTypeE.Double:
				num = 8;
				break;
			case InternalPrimitiveTypeE.Int16:
				num = 2;
				break;
			case InternalPrimitiveTypeE.Int32:
				num = 4;
				break;
			case InternalPrimitiveTypeE.Int64:
				num = 8;
				break;
			case InternalPrimitiveTypeE.SByte:
				num = 1;
				break;
			case InternalPrimitiveTypeE.Single:
				num = 4;
				break;
			case InternalPrimitiveTypeE.UInt16:
				num = 2;
				break;
			case InternalPrimitiveTypeE.UInt32:
				num = 4;
				break;
			case InternalPrimitiveTypeE.UInt64:
				num = 8;
				break;
			}
			return num;
		}

		// Token: 0x060054C8 RID: 21704 RVA: 0x0012DE88 File Offset: 0x0012C088
		internal static InternalNameSpaceE GetNameSpaceEnum(InternalPrimitiveTypeE code, Type type, WriteObjectInfo objectInfo, out string typeName)
		{
			InternalNameSpaceE internalNameSpaceE = InternalNameSpaceE.None;
			typeName = null;
			if (code != InternalPrimitiveTypeE.Invalid)
			{
				switch (code)
				{
				case InternalPrimitiveTypeE.Boolean:
				case InternalPrimitiveTypeE.Byte:
				case InternalPrimitiveTypeE.Char:
				case InternalPrimitiveTypeE.Double:
				case InternalPrimitiveTypeE.Int16:
				case InternalPrimitiveTypeE.Int32:
				case InternalPrimitiveTypeE.Int64:
				case InternalPrimitiveTypeE.SByte:
				case InternalPrimitiveTypeE.Single:
				case InternalPrimitiveTypeE.TimeSpan:
				case InternalPrimitiveTypeE.DateTime:
				case InternalPrimitiveTypeE.UInt16:
				case InternalPrimitiveTypeE.UInt32:
				case InternalPrimitiveTypeE.UInt64:
					internalNameSpaceE = InternalNameSpaceE.XdrPrimitive;
					typeName = "System." + Converter.ToComType(code);
					break;
				case InternalPrimitiveTypeE.Decimal:
					internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					typeName = "System." + Converter.ToComType(code);
					break;
				}
			}
			if (internalNameSpaceE == InternalNameSpaceE.None && type != null)
			{
				if (type == Converter.typeofString)
				{
					internalNameSpaceE = InternalNameSpaceE.XdrString;
				}
				else if (objectInfo == null)
				{
					typeName = type.FullName;
					if (type.Assembly == Converter.urtAssembly)
					{
						internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					}
					else
					{
						internalNameSpaceE = InternalNameSpaceE.UrtUser;
					}
				}
				else
				{
					typeName = objectInfo.GetTypeFullName();
					if (objectInfo.GetAssemblyString().Equals(Converter.urtAssemblyString))
					{
						internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					}
					else
					{
						internalNameSpaceE = InternalNameSpaceE.UrtUser;
					}
				}
			}
			return internalNameSpaceE;
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x0012DF69 File Offset: 0x0012C169
		internal static Type ToArrayType(InternalPrimitiveTypeE code)
		{
			if (Converter.arrayTypeA == null)
			{
				Converter.InitArrayTypeA();
			}
			return Converter.arrayTypeA[(int)code];
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x0012DF84 File Offset: 0x0012C184
		private static void InitTypeA()
		{
			Type[] array = new Type[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = Converter.typeofBoolean;
			array[2] = Converter.typeofByte;
			array[3] = Converter.typeofChar;
			array[5] = Converter.typeofDecimal;
			array[6] = Converter.typeofDouble;
			array[7] = Converter.typeofInt16;
			array[8] = Converter.typeofInt32;
			array[9] = Converter.typeofInt64;
			array[10] = Converter.typeofSByte;
			array[11] = Converter.typeofSingle;
			array[12] = Converter.typeofTimeSpan;
			array[13] = Converter.typeofDateTime;
			array[14] = Converter.typeofUInt16;
			array[15] = Converter.typeofUInt32;
			array[16] = Converter.typeofUInt64;
			Converter.typeA = array;
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x0012E028 File Offset: 0x0012C228
		private static void InitArrayTypeA()
		{
			Type[] array = new Type[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = Converter.typeofBooleanArray;
			array[2] = Converter.typeofByteArray;
			array[3] = Converter.typeofCharArray;
			array[5] = Converter.typeofDecimalArray;
			array[6] = Converter.typeofDoubleArray;
			array[7] = Converter.typeofInt16Array;
			array[8] = Converter.typeofInt32Array;
			array[9] = Converter.typeofInt64Array;
			array[10] = Converter.typeofSByteArray;
			array[11] = Converter.typeofSingleArray;
			array[12] = Converter.typeofTimeSpanArray;
			array[13] = Converter.typeofDateTimeArray;
			array[14] = Converter.typeofUInt16Array;
			array[15] = Converter.typeofUInt32Array;
			array[16] = Converter.typeofUInt64Array;
			Converter.arrayTypeA = array;
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x0012E0CC File Offset: 0x0012C2CC
		internal static Type ToType(InternalPrimitiveTypeE code)
		{
			if (Converter.typeA == null)
			{
				Converter.InitTypeA();
			}
			return Converter.typeA[(int)code];
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x0012E0E8 File Offset: 0x0012C2E8
		internal static Array CreatePrimitiveArray(InternalPrimitiveTypeE code, int length)
		{
			Array array = null;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				array = new bool[length];
				break;
			case InternalPrimitiveTypeE.Byte:
				array = new byte[length];
				break;
			case InternalPrimitiveTypeE.Char:
				array = new char[length];
				break;
			case InternalPrimitiveTypeE.Decimal:
				array = new decimal[length];
				break;
			case InternalPrimitiveTypeE.Double:
				array = new double[length];
				break;
			case InternalPrimitiveTypeE.Int16:
				array = new short[length];
				break;
			case InternalPrimitiveTypeE.Int32:
				array = new int[length];
				break;
			case InternalPrimitiveTypeE.Int64:
				array = new long[length];
				break;
			case InternalPrimitiveTypeE.SByte:
				array = new sbyte[length];
				break;
			case InternalPrimitiveTypeE.Single:
				array = new float[length];
				break;
			case InternalPrimitiveTypeE.TimeSpan:
				array = new TimeSpan[length];
				break;
			case InternalPrimitiveTypeE.DateTime:
				array = new DateTime[length];
				break;
			case InternalPrimitiveTypeE.UInt16:
				array = new ushort[length];
				break;
			case InternalPrimitiveTypeE.UInt32:
				array = new uint[length];
				break;
			case InternalPrimitiveTypeE.UInt64:
				array = new ulong[length];
				break;
			}
			return array;
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x0012E1CC File Offset: 0x0012C3CC
		internal static bool IsPrimitiveArray(Type type, out object typeInformation)
		{
			typeInformation = null;
			bool flag = true;
			if (type == Converter.typeofBooleanArray)
			{
				typeInformation = InternalPrimitiveTypeE.Boolean;
			}
			else if (type == Converter.typeofByteArray)
			{
				typeInformation = InternalPrimitiveTypeE.Byte;
			}
			else if (type == Converter.typeofCharArray)
			{
				typeInformation = InternalPrimitiveTypeE.Char;
			}
			else if (type == Converter.typeofDoubleArray)
			{
				typeInformation = InternalPrimitiveTypeE.Double;
			}
			else if (type == Converter.typeofInt16Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int16;
			}
			else if (type == Converter.typeofInt32Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int32;
			}
			else if (type == Converter.typeofInt64Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int64;
			}
			else if (type == Converter.typeofSByteArray)
			{
				typeInformation = InternalPrimitiveTypeE.SByte;
			}
			else if (type == Converter.typeofSingleArray)
			{
				typeInformation = InternalPrimitiveTypeE.Single;
			}
			else if (type == Converter.typeofUInt16Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt16;
			}
			else if (type == Converter.typeofUInt32Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt32;
			}
			else if (type == Converter.typeofUInt64Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt64;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x0012E2D0 File Offset: 0x0012C4D0
		private static void InitValueA()
		{
			string[] array = new string[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = "Boolean";
			array[2] = "Byte";
			array[3] = "Char";
			array[5] = "Decimal";
			array[6] = "Double";
			array[7] = "Int16";
			array[8] = "Int32";
			array[9] = "Int64";
			array[10] = "SByte";
			array[11] = "Single";
			array[12] = "TimeSpan";
			array[13] = "DateTime";
			array[14] = "UInt16";
			array[15] = "UInt32";
			array[16] = "UInt64";
			Converter.valueA = array;
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x0012E374 File Offset: 0x0012C574
		internal static string ToComType(InternalPrimitiveTypeE code)
		{
			if (Converter.valueA == null)
			{
				Converter.InitValueA();
			}
			return Converter.valueA[(int)code];
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x0012E390 File Offset: 0x0012C590
		private static void InitTypeCodeA()
		{
			TypeCode[] array = new TypeCode[Converter.primitiveTypeEnumLength];
			array[0] = TypeCode.Object;
			array[1] = TypeCode.Boolean;
			array[2] = TypeCode.Byte;
			array[3] = TypeCode.Char;
			array[5] = TypeCode.Decimal;
			array[6] = TypeCode.Double;
			array[7] = TypeCode.Int16;
			array[8] = TypeCode.Int32;
			array[9] = TypeCode.Int64;
			array[10] = TypeCode.SByte;
			array[11] = TypeCode.Single;
			array[12] = TypeCode.Object;
			array[13] = TypeCode.DateTime;
			array[14] = TypeCode.UInt16;
			array[15] = TypeCode.UInt32;
			array[16] = TypeCode.UInt64;
			Converter.typeCodeA = array;
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x0012E400 File Offset: 0x0012C600
		internal static TypeCode ToTypeCode(InternalPrimitiveTypeE code)
		{
			if (Converter.typeCodeA == null)
			{
				Converter.InitTypeCodeA();
			}
			return Converter.typeCodeA[(int)code];
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x0012E41C File Offset: 0x0012C61C
		private static void InitCodeA()
		{
			Converter.codeA = new InternalPrimitiveTypeE[]
			{
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Boolean,
				InternalPrimitiveTypeE.Char,
				InternalPrimitiveTypeE.SByte,
				InternalPrimitiveTypeE.Byte,
				InternalPrimitiveTypeE.Int16,
				InternalPrimitiveTypeE.UInt16,
				InternalPrimitiveTypeE.Int32,
				InternalPrimitiveTypeE.UInt32,
				InternalPrimitiveTypeE.Int64,
				InternalPrimitiveTypeE.UInt64,
				InternalPrimitiveTypeE.Single,
				InternalPrimitiveTypeE.Double,
				InternalPrimitiveTypeE.Decimal,
				InternalPrimitiveTypeE.DateTime,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid
			};
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x0012E496 File Offset: 0x0012C696
		internal static InternalPrimitiveTypeE ToPrimitiveTypeEnum(TypeCode typeCode)
		{
			if (Converter.codeA == null)
			{
				Converter.InitCodeA();
			}
			return Converter.codeA[(int)typeCode];
		}

		// Token: 0x060054D5 RID: 21717 RVA: 0x0012E4B0 File Offset: 0x0012C6B0
		internal static object FromString(string value, InternalPrimitiveTypeE code)
		{
			object obj;
			if (code != InternalPrimitiveTypeE.Invalid)
			{
				obj = Convert.ChangeType(value, Converter.ToTypeCode(code), CultureInfo.InvariantCulture);
			}
			else
			{
				obj = value;
			}
			return obj;
		}

		// Token: 0x040026BA RID: 9914
		private static int primitiveTypeEnumLength = 17;

		// Token: 0x040026BB RID: 9915
		private static volatile Type[] typeA;

		// Token: 0x040026BC RID: 9916
		private static volatile Type[] arrayTypeA;

		// Token: 0x040026BD RID: 9917
		private static volatile string[] valueA;

		// Token: 0x040026BE RID: 9918
		private static volatile TypeCode[] typeCodeA;

		// Token: 0x040026BF RID: 9919
		private static volatile InternalPrimitiveTypeE[] codeA;

		// Token: 0x040026C0 RID: 9920
		internal static Type typeofISerializable = typeof(ISerializable);

		// Token: 0x040026C1 RID: 9921
		internal static Type typeofString = typeof(string);

		// Token: 0x040026C2 RID: 9922
		internal static Type typeofConverter = typeof(Converter);

		// Token: 0x040026C3 RID: 9923
		internal static Type typeofBoolean = typeof(bool);

		// Token: 0x040026C4 RID: 9924
		internal static Type typeofByte = typeof(byte);

		// Token: 0x040026C5 RID: 9925
		internal static Type typeofChar = typeof(char);

		// Token: 0x040026C6 RID: 9926
		internal static Type typeofDecimal = typeof(decimal);

		// Token: 0x040026C7 RID: 9927
		internal static Type typeofDouble = typeof(double);

		// Token: 0x040026C8 RID: 9928
		internal static Type typeofInt16 = typeof(short);

		// Token: 0x040026C9 RID: 9929
		internal static Type typeofInt32 = typeof(int);

		// Token: 0x040026CA RID: 9930
		internal static Type typeofInt64 = typeof(long);

		// Token: 0x040026CB RID: 9931
		internal static Type typeofSByte = typeof(sbyte);

		// Token: 0x040026CC RID: 9932
		internal static Type typeofSingle = typeof(float);

		// Token: 0x040026CD RID: 9933
		internal static Type typeofTimeSpan = typeof(TimeSpan);

		// Token: 0x040026CE RID: 9934
		internal static Type typeofDateTime = typeof(DateTime);

		// Token: 0x040026CF RID: 9935
		internal static Type typeofUInt16 = typeof(ushort);

		// Token: 0x040026D0 RID: 9936
		internal static Type typeofUInt32 = typeof(uint);

		// Token: 0x040026D1 RID: 9937
		internal static Type typeofUInt64 = typeof(ulong);

		// Token: 0x040026D2 RID: 9938
		internal static Type typeofObject = typeof(object);

		// Token: 0x040026D3 RID: 9939
		internal static Type typeofSystemVoid = typeof(void);

		// Token: 0x040026D4 RID: 9940
		internal static Assembly urtAssembly = Assembly.GetAssembly(Converter.typeofString);

		// Token: 0x040026D5 RID: 9941
		internal static string urtAssemblyString = Converter.urtAssembly.FullName;

		// Token: 0x040026D6 RID: 9942
		internal static Type typeofTypeArray = typeof(Type[]);

		// Token: 0x040026D7 RID: 9943
		internal static Type typeofObjectArray = typeof(object[]);

		// Token: 0x040026D8 RID: 9944
		internal static Type typeofStringArray = typeof(string[]);

		// Token: 0x040026D9 RID: 9945
		internal static Type typeofBooleanArray = typeof(bool[]);

		// Token: 0x040026DA RID: 9946
		internal static Type typeofByteArray = typeof(byte[]);

		// Token: 0x040026DB RID: 9947
		internal static Type typeofCharArray = typeof(char[]);

		// Token: 0x040026DC RID: 9948
		internal static Type typeofDecimalArray = typeof(decimal[]);

		// Token: 0x040026DD RID: 9949
		internal static Type typeofDoubleArray = typeof(double[]);

		// Token: 0x040026DE RID: 9950
		internal static Type typeofInt16Array = typeof(short[]);

		// Token: 0x040026DF RID: 9951
		internal static Type typeofInt32Array = typeof(int[]);

		// Token: 0x040026E0 RID: 9952
		internal static Type typeofInt64Array = typeof(long[]);

		// Token: 0x040026E1 RID: 9953
		internal static Type typeofSByteArray = typeof(sbyte[]);

		// Token: 0x040026E2 RID: 9954
		internal static Type typeofSingleArray = typeof(float[]);

		// Token: 0x040026E3 RID: 9955
		internal static Type typeofTimeSpanArray = typeof(TimeSpan[]);

		// Token: 0x040026E4 RID: 9956
		internal static Type typeofDateTimeArray = typeof(DateTime[]);

		// Token: 0x040026E5 RID: 9957
		internal static Type typeofUInt16Array = typeof(ushort[]);

		// Token: 0x040026E6 RID: 9958
		internal static Type typeofUInt32Array = typeof(uint[]);

		// Token: 0x040026E7 RID: 9959
		internal static Type typeofUInt64Array = typeof(ulong[]);

		// Token: 0x040026E8 RID: 9960
		internal static Type typeofMarshalByRefObject = typeof(MarshalByRefObject);
	}
}
