using System;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005F3 RID: 1523
	internal static class MdConstant
	{
		// Token: 0x06004690 RID: 18064 RVA: 0x00103F0C File Offset: 0x0010210C
		[SecurityCritical]
		public unsafe static object GetValue(MetadataImport scope, int token, RuntimeTypeHandle fieldTypeHandle, bool raw)
		{
			CorElementType corElementType = CorElementType.End;
			long num = 0L;
			int num2;
			string defaultValue = scope.GetDefaultValue(token, out num, out num2, out corElementType);
			RuntimeType runtimeType = fieldTypeHandle.GetRuntimeType();
			if (runtimeType.IsEnum && !raw)
			{
				long num3;
				switch (corElementType)
				{
				case CorElementType.Void:
					return DBNull.Value;
				case CorElementType.Char:
					num3 = (long)((ulong)(*(ushort*)(&num)));
					goto IL_C8;
				case CorElementType.I1:
					num3 = (long)(*(sbyte*)(&num));
					goto IL_C8;
				case CorElementType.U1:
					num3 = (long)((ulong)(*(byte*)(&num)));
					goto IL_C8;
				case CorElementType.I2:
					num3 = (long)(*(short*)(&num));
					goto IL_C8;
				case CorElementType.U2:
					num3 = (long)((ulong)(*(ushort*)(&num)));
					goto IL_C8;
				case CorElementType.I4:
					num3 = (long)(*(int*)(&num));
					goto IL_C8;
				case CorElementType.U4:
					num3 = (long)((ulong)(*(uint*)(&num)));
					goto IL_C8;
				case CorElementType.I8:
					num3 = num;
					goto IL_C8;
				case CorElementType.U8:
					num3 = num;
					goto IL_C8;
				}
				throw new FormatException(Environment.GetResourceString("Arg_BadLiteralFormat"));
				IL_C8:
				return RuntimeType.CreateEnum(runtimeType, num3);
			}
			if (!(runtimeType == typeof(DateTime)))
			{
				switch (corElementType)
				{
				case CorElementType.Void:
					return DBNull.Value;
				case CorElementType.Boolean:
					return *(int*)(&num) != 0;
				case CorElementType.Char:
					return (char)(*(ushort*)(&num));
				case CorElementType.I1:
					return *(sbyte*)(&num);
				case CorElementType.U1:
					return *(byte*)(&num);
				case CorElementType.I2:
					return *(short*)(&num);
				case CorElementType.U2:
					return *(ushort*)(&num);
				case CorElementType.I4:
					return *(int*)(&num);
				case CorElementType.U4:
					return *(uint*)(&num);
				case CorElementType.I8:
					return num;
				case CorElementType.U8:
					return (ulong)num;
				case CorElementType.R4:
					return *(float*)(&num);
				case CorElementType.R8:
					return *(double*)(&num);
				case CorElementType.String:
					if (defaultValue != null)
					{
						return defaultValue;
					}
					return string.Empty;
				case CorElementType.Class:
					return null;
				}
				throw new FormatException(Environment.GetResourceString("Arg_BadLiteralFormat"));
			}
			if (corElementType != CorElementType.Void)
			{
				long num4;
				if (corElementType != CorElementType.I8)
				{
					if (corElementType != CorElementType.U8)
					{
						throw new FormatException(Environment.GetResourceString("Arg_BadLiteralFormat"));
					}
					num4 = num;
				}
				else
				{
					num4 = num;
				}
				return new DateTime(num4);
			}
			return DBNull.Value;
		}
	}
}
