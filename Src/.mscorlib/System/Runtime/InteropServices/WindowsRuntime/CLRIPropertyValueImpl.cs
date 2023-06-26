using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009FE RID: 2558
	internal class CLRIPropertyValueImpl : IPropertyValue
	{
		// Token: 0x06006515 RID: 25877 RVA: 0x00159535 File Offset: 0x00157735
		internal CLRIPropertyValueImpl(PropertyType type, object data)
		{
			this._type = type;
			this._data = data;
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06006516 RID: 25878 RVA: 0x0015954C File Offset: 0x0015774C
		private static Tuple<Type, PropertyType>[] NumericScalarTypes
		{
			get
			{
				if (CLRIPropertyValueImpl.s_numericScalarTypes == null)
				{
					Tuple<Type, PropertyType>[] array = new Tuple<Type, PropertyType>[]
					{
						new Tuple<Type, PropertyType>(typeof(byte), PropertyType.UInt8),
						new Tuple<Type, PropertyType>(typeof(short), PropertyType.Int16),
						new Tuple<Type, PropertyType>(typeof(ushort), PropertyType.UInt16),
						new Tuple<Type, PropertyType>(typeof(int), PropertyType.Int32),
						new Tuple<Type, PropertyType>(typeof(uint), PropertyType.UInt32),
						new Tuple<Type, PropertyType>(typeof(long), PropertyType.Int64),
						new Tuple<Type, PropertyType>(typeof(ulong), PropertyType.UInt64),
						new Tuple<Type, PropertyType>(typeof(float), PropertyType.Single),
						new Tuple<Type, PropertyType>(typeof(double), PropertyType.Double)
					};
					CLRIPropertyValueImpl.s_numericScalarTypes = array;
				}
				return CLRIPropertyValueImpl.s_numericScalarTypes;
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x06006517 RID: 25879 RVA: 0x00159628 File Offset: 0x00157828
		public PropertyType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06006518 RID: 25880 RVA: 0x00159630 File Offset: 0x00157830
		public bool IsNumericScalar
		{
			get
			{
				return CLRIPropertyValueImpl.IsNumericScalarImpl(this._type, this._data);
			}
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x00159643 File Offset: 0x00157843
		public override string ToString()
		{
			if (this._data != null)
			{
				return this._data.ToString();
			}
			return base.ToString();
		}

		// Token: 0x0600651A RID: 25882 RVA: 0x0015965F File Offset: 0x0015785F
		public byte GetUInt8()
		{
			return this.CoerceScalarValue<byte>(PropertyType.UInt8);
		}

		// Token: 0x0600651B RID: 25883 RVA: 0x00159668 File Offset: 0x00157868
		public short GetInt16()
		{
			return this.CoerceScalarValue<short>(PropertyType.Int16);
		}

		// Token: 0x0600651C RID: 25884 RVA: 0x00159671 File Offset: 0x00157871
		public ushort GetUInt16()
		{
			return this.CoerceScalarValue<ushort>(PropertyType.UInt16);
		}

		// Token: 0x0600651D RID: 25885 RVA: 0x0015967A File Offset: 0x0015787A
		public int GetInt32()
		{
			return this.CoerceScalarValue<int>(PropertyType.Int32);
		}

		// Token: 0x0600651E RID: 25886 RVA: 0x00159683 File Offset: 0x00157883
		public uint GetUInt32()
		{
			return this.CoerceScalarValue<uint>(PropertyType.UInt32);
		}

		// Token: 0x0600651F RID: 25887 RVA: 0x0015968C File Offset: 0x0015788C
		public long GetInt64()
		{
			return this.CoerceScalarValue<long>(PropertyType.Int64);
		}

		// Token: 0x06006520 RID: 25888 RVA: 0x00159695 File Offset: 0x00157895
		public ulong GetUInt64()
		{
			return this.CoerceScalarValue<ulong>(PropertyType.UInt64);
		}

		// Token: 0x06006521 RID: 25889 RVA: 0x0015969E File Offset: 0x0015789E
		public float GetSingle()
		{
			return this.CoerceScalarValue<float>(PropertyType.Single);
		}

		// Token: 0x06006522 RID: 25890 RVA: 0x001596A7 File Offset: 0x001578A7
		public double GetDouble()
		{
			return this.CoerceScalarValue<double>(PropertyType.Double);
		}

		// Token: 0x06006523 RID: 25891 RVA: 0x001596B4 File Offset: 0x001578B4
		public char GetChar16()
		{
			if (this.Type != PropertyType.Char16)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "Char16" }), -2147316576);
			}
			return (char)this._data;
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x00159708 File Offset: 0x00157908
		public bool GetBoolean()
		{
			if (this.Type != PropertyType.Boolean)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "Boolean" }), -2147316576);
			}
			return (bool)this._data;
		}

		// Token: 0x06006525 RID: 25893 RVA: 0x0015975B File Offset: 0x0015795B
		public string GetString()
		{
			return this.CoerceScalarValue<string>(PropertyType.String);
		}

		// Token: 0x06006526 RID: 25894 RVA: 0x00159768 File Offset: 0x00157968
		public object GetInspectable()
		{
			if (this.Type != PropertyType.Inspectable)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "Inspectable" }), -2147316576);
			}
			return this._data;
		}

		// Token: 0x06006527 RID: 25895 RVA: 0x001597B6 File Offset: 0x001579B6
		public Guid GetGuid()
		{
			return this.CoerceScalarValue<Guid>(PropertyType.Guid);
		}

		// Token: 0x06006528 RID: 25896 RVA: 0x001597C0 File Offset: 0x001579C0
		public DateTimeOffset GetDateTime()
		{
			if (this.Type != PropertyType.DateTime)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "DateTime" }), -2147316576);
			}
			return (DateTimeOffset)this._data;
		}

		// Token: 0x06006529 RID: 25897 RVA: 0x00159814 File Offset: 0x00157A14
		public TimeSpan GetTimeSpan()
		{
			if (this.Type != PropertyType.TimeSpan)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "TimeSpan" }), -2147316576);
			}
			return (TimeSpan)this._data;
		}

		// Token: 0x0600652A RID: 25898 RVA: 0x00159868 File Offset: 0x00157A68
		[SecuritySafeCritical]
		public Point GetPoint()
		{
			if (this.Type != PropertyType.Point)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "Point" }), -2147316576);
			}
			return this.Unbox<Point>(IReferenceFactory.s_pointType);
		}

		// Token: 0x0600652B RID: 25899 RVA: 0x001598BC File Offset: 0x00157ABC
		[SecuritySafeCritical]
		public Size GetSize()
		{
			if (this.Type != PropertyType.Size)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "Size" }), -2147316576);
			}
			return this.Unbox<Size>(IReferenceFactory.s_sizeType);
		}

		// Token: 0x0600652C RID: 25900 RVA: 0x00159910 File Offset: 0x00157B10
		[SecuritySafeCritical]
		public Rect GetRect()
		{
			if (this.Type != PropertyType.Rect)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "Rect" }), -2147316576);
			}
			return this.Unbox<Rect>(IReferenceFactory.s_rectType);
		}

		// Token: 0x0600652D RID: 25901 RVA: 0x00159963 File Offset: 0x00157B63
		public byte[] GetUInt8Array()
		{
			return this.CoerceArrayValue<byte>(PropertyType.UInt8Array);
		}

		// Token: 0x0600652E RID: 25902 RVA: 0x00159970 File Offset: 0x00157B70
		public short[] GetInt16Array()
		{
			return this.CoerceArrayValue<short>(PropertyType.Int16Array);
		}

		// Token: 0x0600652F RID: 25903 RVA: 0x0015997D File Offset: 0x00157B7D
		public ushort[] GetUInt16Array()
		{
			return this.CoerceArrayValue<ushort>(PropertyType.UInt16Array);
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x0015998A File Offset: 0x00157B8A
		public int[] GetInt32Array()
		{
			return this.CoerceArrayValue<int>(PropertyType.Int32Array);
		}

		// Token: 0x06006531 RID: 25905 RVA: 0x00159997 File Offset: 0x00157B97
		public uint[] GetUInt32Array()
		{
			return this.CoerceArrayValue<uint>(PropertyType.UInt32Array);
		}

		// Token: 0x06006532 RID: 25906 RVA: 0x001599A4 File Offset: 0x00157BA4
		public long[] GetInt64Array()
		{
			return this.CoerceArrayValue<long>(PropertyType.Int64Array);
		}

		// Token: 0x06006533 RID: 25907 RVA: 0x001599B1 File Offset: 0x00157BB1
		public ulong[] GetUInt64Array()
		{
			return this.CoerceArrayValue<ulong>(PropertyType.UInt64Array);
		}

		// Token: 0x06006534 RID: 25908 RVA: 0x001599BE File Offset: 0x00157BBE
		public float[] GetSingleArray()
		{
			return this.CoerceArrayValue<float>(PropertyType.SingleArray);
		}

		// Token: 0x06006535 RID: 25909 RVA: 0x001599CB File Offset: 0x00157BCB
		public double[] GetDoubleArray()
		{
			return this.CoerceArrayValue<double>(PropertyType.DoubleArray);
		}

		// Token: 0x06006536 RID: 25910 RVA: 0x001599D8 File Offset: 0x00157BD8
		public char[] GetChar16Array()
		{
			if (this.Type != PropertyType.Char16Array)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "Char16[]" }), -2147316576);
			}
			return (char[])this._data;
		}

		// Token: 0x06006537 RID: 25911 RVA: 0x00159A30 File Offset: 0x00157C30
		public bool[] GetBooleanArray()
		{
			if (this.Type != PropertyType.BooleanArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "Boolean[]" }), -2147316576);
			}
			return (bool[])this._data;
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x00159A86 File Offset: 0x00157C86
		public string[] GetStringArray()
		{
			return this.CoerceArrayValue<string>(PropertyType.StringArray);
		}

		// Token: 0x06006539 RID: 25913 RVA: 0x00159A94 File Offset: 0x00157C94
		public object[] GetInspectableArray()
		{
			if (this.Type != PropertyType.InspectableArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "Inspectable[]" }), -2147316576);
			}
			return (object[])this._data;
		}

		// Token: 0x0600653A RID: 25914 RVA: 0x00159AEA File Offset: 0x00157CEA
		public Guid[] GetGuidArray()
		{
			return this.CoerceArrayValue<Guid>(PropertyType.GuidArray);
		}

		// Token: 0x0600653B RID: 25915 RVA: 0x00159AF8 File Offset: 0x00157CF8
		public DateTimeOffset[] GetDateTimeArray()
		{
			if (this.Type != PropertyType.DateTimeArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "DateTimeOffset[]" }), -2147316576);
			}
			return (DateTimeOffset[])this._data;
		}

		// Token: 0x0600653C RID: 25916 RVA: 0x00159B50 File Offset: 0x00157D50
		public TimeSpan[] GetTimeSpanArray()
		{
			if (this.Type != PropertyType.TimeSpanArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "TimeSpan[]" }), -2147316576);
			}
			return (TimeSpan[])this._data;
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x00159BA8 File Offset: 0x00157DA8
		[SecuritySafeCritical]
		public Point[] GetPointArray()
		{
			if (this.Type != PropertyType.PointArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "Point[]" }), -2147316576);
			}
			return this.UnboxArray<Point>(IReferenceFactory.s_pointType);
		}

		// Token: 0x0600653E RID: 25918 RVA: 0x00159C00 File Offset: 0x00157E00
		[SecuritySafeCritical]
		public Size[] GetSizeArray()
		{
			if (this.Type != PropertyType.SizeArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "Size[]" }), -2147316576);
			}
			return this.UnboxArray<Size>(IReferenceFactory.s_sizeType);
		}

		// Token: 0x0600653F RID: 25919 RVA: 0x00159C58 File Offset: 0x00157E58
		[SecuritySafeCritical]
		public Rect[] GetRectArray()
		{
			if (this.Type != PropertyType.RectArray)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[] { this.Type, "Rect[]" }), -2147316576);
			}
			return this.UnboxArray<Rect>(IReferenceFactory.s_rectType);
		}

		// Token: 0x06006540 RID: 25920 RVA: 0x00159CB0 File Offset: 0x00157EB0
		private T[] CoerceArrayValue<T>(PropertyType unboxType)
		{
			if (this.Type == unboxType)
			{
				return (T[])this._data;
			}
			Array array = this._data as Array;
			if (array == null)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this.Type,
					typeof(T).MakeArrayType().Name
				}), -2147316576);
			}
			PropertyType propertyType = this.Type - 1024;
			T[] array2 = new T[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				try
				{
					array2[i] = CLRIPropertyValueImpl.CoerceScalarValue<T>(propertyType, array.GetValue(i));
				}
				catch (InvalidCastException ex)
				{
					Exception ex2 = new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueArrayCoersion", new object[]
					{
						this.Type,
						typeof(T).MakeArrayType().Name,
						i,
						ex.Message
					}), ex);
					ex2.SetErrorCode(ex._HResult);
					throw ex2;
				}
			}
			return array2;
		}

		// Token: 0x06006541 RID: 25921 RVA: 0x00159DDC File Offset: 0x00157FDC
		private T CoerceScalarValue<T>(PropertyType unboxType)
		{
			if (this.Type == unboxType)
			{
				return (T)((object)this._data);
			}
			return CLRIPropertyValueImpl.CoerceScalarValue<T>(this.Type, this._data);
		}

		// Token: 0x06006542 RID: 25922 RVA: 0x00159E04 File Offset: 0x00158004
		private static T CoerceScalarValue<T>(PropertyType type, object value)
		{
			if (!CLRIPropertyValueImpl.IsCoercable(type, value) && type != PropertyType.Inspectable)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					type,
					typeof(T).Name
				}), -2147316576);
			}
			try
			{
				if (type == PropertyType.String && typeof(T) == typeof(Guid))
				{
					return (T)((object)Guid.Parse((string)value));
				}
				if (type == PropertyType.Guid && typeof(T) == typeof(string))
				{
					return (T)((object)((Guid)value).ToString("D", CultureInfo.InvariantCulture));
				}
				foreach (Tuple<Type, PropertyType> tuple in CLRIPropertyValueImpl.NumericScalarTypes)
				{
					if (tuple.Item1 == typeof(T))
					{
						return (T)((object)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture));
					}
				}
			}
			catch (FormatException)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					type,
					typeof(T).Name
				}), -2147316576);
			}
			catch (InvalidCastException)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					type,
					typeof(T).Name
				}), -2147316576);
			}
			catch (OverflowException)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueCoersion", new object[]
				{
					type,
					value,
					typeof(T).Name
				}), -2147352566);
			}
			IPropertyValue propertyValue = value as IPropertyValue;
			if (type == PropertyType.Inspectable && propertyValue != null)
			{
				if (typeof(T) == typeof(byte))
				{
					return (T)((object)propertyValue.GetUInt8());
				}
				if (typeof(T) == typeof(short))
				{
					return (T)((object)propertyValue.GetInt16());
				}
				if (typeof(T) == typeof(ushort))
				{
					return (T)((object)propertyValue.GetUInt16());
				}
				if (typeof(T) == typeof(int))
				{
					return (T)((object)propertyValue.GetUInt32());
				}
				if (typeof(T) == typeof(uint))
				{
					return (T)((object)propertyValue.GetUInt32());
				}
				if (typeof(T) == typeof(long))
				{
					return (T)((object)propertyValue.GetInt64());
				}
				if (typeof(T) == typeof(ulong))
				{
					return (T)((object)propertyValue.GetUInt64());
				}
				if (typeof(T) == typeof(float))
				{
					return (T)((object)propertyValue.GetSingle());
				}
				if (typeof(T) == typeof(double))
				{
					return (T)((object)propertyValue.GetDouble());
				}
			}
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
			{
				type,
				typeof(T).Name
			}), -2147316576);
		}

		// Token: 0x06006543 RID: 25923 RVA: 0x0015A1D8 File Offset: 0x001583D8
		private static bool IsCoercable(PropertyType type, object data)
		{
			return type == PropertyType.Guid || type == PropertyType.String || CLRIPropertyValueImpl.IsNumericScalarImpl(type, data);
		}

		// Token: 0x06006544 RID: 25924 RVA: 0x0015A1F0 File Offset: 0x001583F0
		private static bool IsNumericScalarImpl(PropertyType type, object data)
		{
			if (data.GetType().IsEnum)
			{
				return true;
			}
			foreach (Tuple<Type, PropertyType> tuple in CLRIPropertyValueImpl.NumericScalarTypes)
			{
				if (tuple.Item2 == type)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x0015A230 File Offset: 0x00158430
		[SecurityCritical]
		private unsafe T Unbox<T>(Type expectedBoxedType) where T : struct
		{
			if (this._data.GetType() != expectedBoxedType)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this._data.GetType(),
					expectedBoxedType.Name
				}), -2147316576);
			}
			T t = new T();
			fixed (byte* ptr = &JitHelpers.GetPinningHelper(this._data).m_data)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = (byte*)(void*)JitHelpers.UnsafeCastToStackPointer<T>(ref t);
				Buffer.Memcpy(ptr3, ptr2, Marshal.SizeOf<T>(t));
			}
			return t;
		}

		// Token: 0x06006546 RID: 25926 RVA: 0x0015A2BC File Offset: 0x001584BC
		[SecurityCritical]
		private unsafe T[] UnboxArray<T>(Type expectedArrayElementType) where T : struct
		{
			Array array = this._data as Array;
			if (array == null || this._data.GetType().GetElementType() != expectedArrayElementType)
			{
				throw new InvalidCastException(Environment.GetResourceString("InvalidCast_WinRTIPropertyValueElement", new object[]
				{
					this._data.GetType(),
					expectedArrayElementType.MakeArrayType().Name
				}), -2147316576);
			}
			T[] array2 = new T[array.Length];
			if (array2.Length != 0)
			{
				fixed (byte* ptr = &JitHelpers.GetPinningHelper(array).m_data)
				{
					byte* ptr2 = ptr;
					fixed (byte* ptr3 = &JitHelpers.GetPinningHelper(array2).m_data)
					{
						byte* ptr4 = ptr3;
						byte* ptr5 = (byte*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(array, 0);
						byte* ptr6 = (byte*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement<T>(array2, 0);
						Buffer.Memcpy(ptr6, ptr5, checked(Marshal.SizeOf(typeof(T)) * array2.Length));
					}
				}
			}
			return array2;
		}

		// Token: 0x04002D39 RID: 11577
		private PropertyType _type;

		// Token: 0x04002D3A RID: 11578
		private object _data;

		// Token: 0x04002D3B RID: 11579
		private static volatile Tuple<Type, PropertyType>[] s_numericScalarTypes;
	}
}
