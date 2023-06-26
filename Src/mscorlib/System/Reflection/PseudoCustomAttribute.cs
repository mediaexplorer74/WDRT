using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020005DD RID: 1501
	internal static class PseudoCustomAttribute
	{
		// Token: 0x060045B3 RID: 17843
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSecurityAttributes(RuntimeModule module, int token, bool assembly, out object[] securityAttributes);

		// Token: 0x060045B4 RID: 17844 RVA: 0x00101DA4 File Offset: 0x000FFFA4
		[SecurityCritical]
		internal static void GetSecurityAttributes(RuntimeModule module, int token, bool assembly, out object[] securityAttributes)
		{
			PseudoCustomAttribute._GetSecurityAttributes(module.GetNativeHandle(), token, assembly, out securityAttributes);
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x00101DB4 File Offset: 0x000FFFB4
		[SecurityCritical]
		static PseudoCustomAttribute()
		{
			RuntimeType[] array = new RuntimeType[]
			{
				typeof(FieldOffsetAttribute) as RuntimeType,
				typeof(SerializableAttribute) as RuntimeType,
				typeof(MarshalAsAttribute) as RuntimeType,
				typeof(ComImportAttribute) as RuntimeType,
				typeof(NonSerializedAttribute) as RuntimeType,
				typeof(InAttribute) as RuntimeType,
				typeof(OutAttribute) as RuntimeType,
				typeof(OptionalAttribute) as RuntimeType,
				typeof(DllImportAttribute) as RuntimeType,
				typeof(PreserveSigAttribute) as RuntimeType,
				typeof(TypeForwardedToAttribute) as RuntimeType
			};
			PseudoCustomAttribute.s_pcasCount = array.Length;
			Dictionary<RuntimeType, RuntimeType> dictionary = new Dictionary<RuntimeType, RuntimeType>(PseudoCustomAttribute.s_pcasCount);
			for (int i = 0; i < PseudoCustomAttribute.s_pcasCount; i++)
			{
				dictionary[array[i]] = array[i];
			}
			PseudoCustomAttribute.s_pca = dictionary;
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x00101EC8 File Offset: 0x001000C8
		[SecurityCritical]
		[Conditional("_DEBUG")]
		private static void VerifyPseudoCustomAttribute(RuntimeType pca)
		{
			AttributeUsageAttribute attributeUsage = CustomAttribute.GetAttributeUsage(pca);
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x00101EDC File Offset: 0x001000DC
		internal static bool IsSecurityAttribute(RuntimeType type)
		{
			return type == (RuntimeType)typeof(SecurityAttribute) || type.IsSubclassOf(typeof(SecurityAttribute));
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x00101F08 File Offset: 0x00100108
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeType type, RuntimeType caType, bool includeSecCa, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
			{
				return new Attribute[0];
			}
			List<Attribute> list = new List<Attribute>();
			if (flag || caType == (RuntimeType)typeof(SerializableAttribute))
			{
				Attribute attribute = SerializableAttribute.GetCustomAttribute(type);
				if (attribute != null)
				{
					list.Add(attribute);
				}
			}
			if (flag || caType == (RuntimeType)typeof(ComImportAttribute))
			{
				Attribute attribute = ComImportAttribute.GetCustomAttribute(type);
				if (attribute != null)
				{
					list.Add(attribute);
				}
			}
			if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && !type.IsGenericParameter && type.GetElementType() == null)
			{
				if (type.IsGenericType)
				{
					type = (RuntimeType)type.GetGenericTypeDefinition();
				}
				object[] array;
				PseudoCustomAttribute.GetSecurityAttributes(type.Module.ModuleHandle.GetRuntimeModule(), type.MetadataToken, false, out array);
				if (array != null)
				{
					foreach (object obj in array)
					{
						if (caType == obj.GetType() || obj.GetType().IsSubclassOf(caType))
						{
							list.Add((Attribute)obj);
						}
					}
				}
			}
			count = list.Count;
			return list.ToArray();
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x0010208C File Offset: 0x0010028C
		[SecurityCritical]
		internal static bool IsDefined(RuntimeType type, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			int num;
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null) || PseudoCustomAttribute.IsSecurityAttribute(caType)) && (((flag || caType == (RuntimeType)typeof(SerializableAttribute)) && SerializableAttribute.IsDefined(type)) || ((flag || caType == (RuntimeType)typeof(ComImportAttribute)) && ComImportAttribute.IsDefined(type)) || ((flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && PseudoCustomAttribute.GetCustomAttributes(type, caType, true, out num).Length != 0));
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x0010214C File Offset: 0x0010034C
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeMethodInfo method, RuntimeType caType, bool includeSecCa, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
			{
				return new Attribute[0];
			}
			List<Attribute> list = new List<Attribute>();
			if (flag || caType == (RuntimeType)typeof(DllImportAttribute))
			{
				Attribute attribute = DllImportAttribute.GetCustomAttribute(method);
				if (attribute != null)
				{
					list.Add(attribute);
				}
			}
			if (flag || caType == (RuntimeType)typeof(PreserveSigAttribute))
			{
				Attribute attribute = PreserveSigAttribute.GetCustomAttribute(method);
				if (attribute != null)
				{
					list.Add(attribute);
				}
			}
			if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)))
			{
				object[] array;
				PseudoCustomAttribute.GetSecurityAttributes(method.Module.ModuleHandle.GetRuntimeModule(), method.MetadataToken, false, out array);
				if (array != null)
				{
					foreach (object obj in array)
					{
						if (caType == obj.GetType() || obj.GetType().IsSubclassOf(caType))
						{
							list.Add((Attribute)obj);
						}
					}
				}
			}
			count = list.Count;
			return list.ToArray();
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x00102298 File Offset: 0x00100498
		[SecurityCritical]
		internal static bool IsDefined(RuntimeMethodInfo method, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			int num;
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)) && (((flag || caType == (RuntimeType)typeof(DllImportAttribute)) && DllImportAttribute.IsDefined(method)) || ((flag || caType == (RuntimeType)typeof(PreserveSigAttribute)) && PreserveSigAttribute.IsDefined(method)) || ((flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && PseudoCustomAttribute.GetCustomAttributes(method, caType, true, out num).Length != 0));
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x00102350 File Offset: 0x00100550
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeParameterInfo parameter, RuntimeType caType, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)
			{
				return null;
			}
			Attribute[] array = new Attribute[PseudoCustomAttribute.s_pcasCount];
			if (flag || caType == (RuntimeType)typeof(InAttribute))
			{
				Attribute attribute = InAttribute.GetCustomAttribute(parameter);
				if (attribute != null)
				{
					Attribute[] array2 = array;
					int num = count;
					count = num + 1;
					array2[num] = attribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(OutAttribute))
			{
				Attribute attribute = OutAttribute.GetCustomAttribute(parameter);
				if (attribute != null)
				{
					Attribute[] array3 = array;
					int num = count;
					count = num + 1;
					array3[num] = attribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(OptionalAttribute))
			{
				Attribute attribute = OptionalAttribute.GetCustomAttribute(parameter);
				if (attribute != null)
				{
					Attribute[] array4 = array;
					int num = count;
					count = num + 1;
					array4[num] = attribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(MarshalAsAttribute))
			{
				Attribute attribute = MarshalAsAttribute.GetCustomAttribute(parameter);
				if (attribute != null)
				{
					Attribute[] array5 = array;
					int num = count;
					count = num + 1;
					array5[num] = attribute;
				}
			}
			return array;
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x00102478 File Offset: 0x00100678
		[SecurityCritical]
		internal static bool IsDefined(RuntimeParameterInfo parameter, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)) && (((flag || caType == (RuntimeType)typeof(InAttribute)) && InAttribute.IsDefined(parameter)) || ((flag || caType == (RuntimeType)typeof(OutAttribute)) && OutAttribute.IsDefined(parameter)) || ((flag || caType == (RuntimeType)typeof(OptionalAttribute)) && OptionalAttribute.IsDefined(parameter)) || ((flag || caType == (RuntimeType)typeof(MarshalAsAttribute)) && MarshalAsAttribute.IsDefined(parameter)));
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x00102560 File Offset: 0x00100760
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeAssembly assembly, RuntimeType caType, bool includeSecCa, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
			{
				return new Attribute[0];
			}
			List<Attribute> list = new List<Attribute>();
			if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)))
			{
				object[] array;
				PseudoCustomAttribute.GetSecurityAttributes(assembly.ManifestModule.ModuleHandle.GetRuntimeModule(), RuntimeAssembly.GetToken(assembly.GetNativeHandle()), true, out array);
				if (array != null)
				{
					foreach (object obj in array)
					{
						if (caType == obj.GetType() || obj.GetType().IsSubclassOf(caType))
						{
							list.Add((Attribute)obj);
						}
					}
				}
			}
			count = list.Count;
			return list.ToArray();
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x00102658 File Offset: 0x00100858
		[SecurityCritical]
		internal static bool IsDefined(RuntimeAssembly assembly, RuntimeType caType)
		{
			int num;
			return PseudoCustomAttribute.GetCustomAttributes(assembly, caType, true, out num).Length != 0;
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x00102673 File Offset: 0x00100873
		internal static Attribute[] GetCustomAttributes(RuntimeModule module, RuntimeType caType, out int count)
		{
			count = 0;
			return null;
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x00102679 File Offset: 0x00100879
		internal static bool IsDefined(RuntimeModule module, RuntimeType caType)
		{
			return false;
		}

		// Token: 0x060045C2 RID: 17858 RVA: 0x0010267C File Offset: 0x0010087C
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeFieldInfo field, RuntimeType caType, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)
			{
				return null;
			}
			Attribute[] array = new Attribute[PseudoCustomAttribute.s_pcasCount];
			if (flag || caType == (RuntimeType)typeof(MarshalAsAttribute))
			{
				Attribute attribute = MarshalAsAttribute.GetCustomAttribute(field);
				if (attribute != null)
				{
					Attribute[] array2 = array;
					int num = count;
					count = num + 1;
					array2[num] = attribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(FieldOffsetAttribute))
			{
				Attribute attribute = FieldOffsetAttribute.GetCustomAttribute(field);
				if (attribute != null)
				{
					Attribute[] array3 = array;
					int num = count;
					count = num + 1;
					array3[num] = attribute;
				}
			}
			if (flag || caType == (RuntimeType)typeof(NonSerializedAttribute))
			{
				Attribute attribute = NonSerializedAttribute.GetCustomAttribute(field);
				if (attribute != null)
				{
					Attribute[] array4 = array;
					int num = count;
					count = num + 1;
					array4[num] = attribute;
				}
			}
			return array;
		}

		// Token: 0x060045C3 RID: 17859 RVA: 0x00102774 File Offset: 0x00100974
		[SecurityCritical]
		internal static bool IsDefined(RuntimeFieldInfo field, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)) && (((flag || caType == (RuntimeType)typeof(MarshalAsAttribute)) && MarshalAsAttribute.IsDefined(field)) || ((flag || caType == (RuntimeType)typeof(FieldOffsetAttribute)) && FieldOffsetAttribute.IsDefined(field)) || ((flag || caType == (RuntimeType)typeof(NonSerializedAttribute)) && NonSerializedAttribute.IsDefined(field)));
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x00102838 File Offset: 0x00100A38
		[SecurityCritical]
		internal static Attribute[] GetCustomAttributes(RuntimeConstructorInfo ctor, RuntimeType caType, bool includeSecCa, out int count)
		{
			count = 0;
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			if (!flag && PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null && !PseudoCustomAttribute.IsSecurityAttribute(caType))
			{
				return new Attribute[0];
			}
			List<Attribute> list = new List<Attribute>();
			if (includeSecCa && (flag || PseudoCustomAttribute.IsSecurityAttribute(caType)))
			{
				object[] array;
				PseudoCustomAttribute.GetSecurityAttributes(ctor.Module.ModuleHandle.GetRuntimeModule(), ctor.MetadataToken, false, out array);
				if (array != null)
				{
					foreach (object obj in array)
					{
						if (caType == obj.GetType() || obj.GetType().IsSubclassOf(caType))
						{
							list.Add((Attribute)obj);
						}
					}
				}
			}
			count = list.Count;
			return list.ToArray();
		}

		// Token: 0x060045C5 RID: 17861 RVA: 0x0010292C File Offset: 0x00100B2C
		[SecurityCritical]
		internal static bool IsDefined(RuntimeConstructorInfo ctor, RuntimeType caType)
		{
			bool flag = caType == (RuntimeType)typeof(object) || caType == (RuntimeType)typeof(Attribute);
			int num;
			return (flag || !(PseudoCustomAttribute.s_pca.GetValueOrDefault(caType) == null)) && ((flag || PseudoCustomAttribute.IsSecurityAttribute(caType)) && PseudoCustomAttribute.GetCustomAttributes(ctor, caType, true, out num).Length != 0);
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x0010299C File Offset: 0x00100B9C
		internal static Attribute[] GetCustomAttributes(RuntimePropertyInfo property, RuntimeType caType, out int count)
		{
			count = 0;
			return null;
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x001029A2 File Offset: 0x00100BA2
		internal static bool IsDefined(RuntimePropertyInfo property, RuntimeType caType)
		{
			return false;
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x001029A5 File Offset: 0x00100BA5
		internal static Attribute[] GetCustomAttributes(RuntimeEventInfo e, RuntimeType caType, out int count)
		{
			count = 0;
			return null;
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x001029AB File Offset: 0x00100BAB
		internal static bool IsDefined(RuntimeEventInfo e, RuntimeType caType)
		{
			return false;
		}

		// Token: 0x04001C99 RID: 7321
		private static Dictionary<RuntimeType, RuntimeType> s_pca;

		// Token: 0x04001C9A RID: 7322
		private static int s_pcasCount;
	}
}
