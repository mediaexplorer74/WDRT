using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005DC RID: 1500
	internal static class CustomAttribute
	{
		// Token: 0x0600458C RID: 17804 RVA: 0x00100D5C File Offset: 0x000FEF5C
		[SecurityCritical]
		internal static bool IsDefined(RuntimeType type, RuntimeType caType, bool inherit)
		{
			if (type.GetElementType() != null)
			{
				return false;
			}
			if (PseudoCustomAttribute.IsDefined(type, caType))
			{
				return true;
			}
			if (CustomAttribute.IsCustomAttributeDefined(type.GetRuntimeModule(), type.MetadataToken, caType))
			{
				return true;
			}
			if (!inherit)
			{
				return false;
			}
			type = type.BaseType as RuntimeType;
			while (type != null)
			{
				if (CustomAttribute.IsCustomAttributeDefined(type.GetRuntimeModule(), type.MetadataToken, caType, 0, inherit))
				{
					return true;
				}
				type = type.BaseType as RuntimeType;
			}
			return false;
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x00100DE0 File Offset: 0x000FEFE0
		[SecuritySafeCritical]
		internal static bool IsDefined(RuntimeMethodInfo method, RuntimeType caType, bool inherit)
		{
			if (PseudoCustomAttribute.IsDefined(method, caType))
			{
				return true;
			}
			if (CustomAttribute.IsCustomAttributeDefined(method.GetRuntimeModule(), method.MetadataToken, caType))
			{
				return true;
			}
			if (!inherit)
			{
				return false;
			}
			method = method.GetParentDefinition();
			while (method != null)
			{
				if (CustomAttribute.IsCustomAttributeDefined(method.GetRuntimeModule(), method.MetadataToken, caType, 0, inherit))
				{
					return true;
				}
				method = method.GetParentDefinition();
			}
			return false;
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x00100E47 File Offset: 0x000FF047
		[SecurityCritical]
		internal static bool IsDefined(RuntimeConstructorInfo ctor, RuntimeType caType)
		{
			return PseudoCustomAttribute.IsDefined(ctor, caType) || CustomAttribute.IsCustomAttributeDefined(ctor.GetRuntimeModule(), ctor.MetadataToken, caType);
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x00100E66 File Offset: 0x000FF066
		[SecurityCritical]
		internal static bool IsDefined(RuntimePropertyInfo property, RuntimeType caType)
		{
			return PseudoCustomAttribute.IsDefined(property, caType) || CustomAttribute.IsCustomAttributeDefined(property.GetRuntimeModule(), property.MetadataToken, caType);
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x00100E85 File Offset: 0x000FF085
		[SecurityCritical]
		internal static bool IsDefined(RuntimeEventInfo e, RuntimeType caType)
		{
			return PseudoCustomAttribute.IsDefined(e, caType) || CustomAttribute.IsCustomAttributeDefined(e.GetRuntimeModule(), e.MetadataToken, caType);
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x00100EA4 File Offset: 0x000FF0A4
		[SecurityCritical]
		internal static bool IsDefined(RuntimeFieldInfo field, RuntimeType caType)
		{
			return PseudoCustomAttribute.IsDefined(field, caType) || CustomAttribute.IsCustomAttributeDefined(field.GetRuntimeModule(), field.MetadataToken, caType);
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x00100EC3 File Offset: 0x000FF0C3
		[SecurityCritical]
		internal static bool IsDefined(RuntimeParameterInfo parameter, RuntimeType caType)
		{
			return PseudoCustomAttribute.IsDefined(parameter, caType) || CustomAttribute.IsCustomAttributeDefined(parameter.GetRuntimeModule(), parameter.MetadataToken, caType);
		}

		// Token: 0x06004593 RID: 17811 RVA: 0x00100EE2 File Offset: 0x000FF0E2
		[SecuritySafeCritical]
		internal static bool IsDefined(RuntimeAssembly assembly, RuntimeType caType)
		{
			return PseudoCustomAttribute.IsDefined(assembly, caType) || CustomAttribute.IsCustomAttributeDefined(assembly.ManifestModule as RuntimeModule, RuntimeAssembly.GetToken(assembly.GetNativeHandle()), caType);
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x00100F0B File Offset: 0x000FF10B
		[SecurityCritical]
		internal static bool IsDefined(RuntimeModule module, RuntimeType caType)
		{
			return PseudoCustomAttribute.IsDefined(module, caType) || CustomAttribute.IsCustomAttributeDefined(module, module.MetadataToken, caType);
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x00100F28 File Offset: 0x000FF128
		[SecurityCritical]
		internal static object[] GetCustomAttributes(RuntimeType type, RuntimeType caType, bool inherit)
		{
			if (type.GetElementType() != null)
			{
				if (!caType.IsValueType)
				{
					return CustomAttribute.CreateAttributeArrayHelper(caType, 0);
				}
				return EmptyArray<object>.Value;
			}
			else
			{
				if (type.IsGenericType && !type.IsGenericTypeDefinition)
				{
					type = type.GetGenericTypeDefinition() as RuntimeType;
				}
				int i = 0;
				Attribute[] customAttributes = PseudoCustomAttribute.GetCustomAttributes(type, caType, true, out i);
				if (!inherit || (caType.IsSealed && !CustomAttribute.GetAttributeUsage(caType).Inherited))
				{
					object[] customAttributes2 = CustomAttribute.GetCustomAttributes(type.GetRuntimeModule(), type.MetadataToken, i, caType, !CustomAttribute.AllowCriticalCustomAttributes(type));
					if (i > 0)
					{
						Array.Copy(customAttributes, 0, customAttributes2, customAttributes2.Length - i, i);
					}
					return customAttributes2;
				}
				List<object> list = new List<object>();
				bool flag = false;
				Type type2 = ((caType == null || caType.IsValueType || caType.ContainsGenericParameters) ? typeof(object) : caType);
				while (i > 0)
				{
					list.Add(customAttributes[--i]);
				}
				while (type != (RuntimeType)typeof(object) && type != null)
				{
					object[] customAttributes3 = CustomAttribute.GetCustomAttributes(type.GetRuntimeModule(), type.MetadataToken, 0, caType, flag, list, !CustomAttribute.AllowCriticalCustomAttributes(type));
					flag = true;
					for (int j = 0; j < customAttributes3.Length; j++)
					{
						list.Add(customAttributes3[j]);
					}
					type = type.BaseType as RuntimeType;
				}
				object[] array = CustomAttribute.CreateAttributeArrayHelper(type2, list.Count);
				Array.Copy(list.ToArray(), 0, array, 0, list.Count);
				return array;
			}
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x001010B0 File Offset: 0x000FF2B0
		private static bool AllowCriticalCustomAttributes(RuntimeType type)
		{
			if (type.IsGenericParameter)
			{
				MethodBase declaringMethod = type.DeclaringMethod;
				if (declaringMethod != null)
				{
					return CustomAttribute.AllowCriticalCustomAttributes(declaringMethod);
				}
				type = type.DeclaringType as RuntimeType;
			}
			return !type.IsSecurityTransparent || CustomAttribute.SpecialAllowCriticalAttributes(type);
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x001010F9 File Offset: 0x000FF2F9
		private static bool SpecialAllowCriticalAttributes(RuntimeType type)
		{
			return type != null && type.Assembly.IsFullyTrusted && RuntimeTypeHandle.IsEquivalentType(type);
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x00101119 File Offset: 0x000FF319
		private static bool AllowCriticalCustomAttributes(MethodBase method)
		{
			return !method.IsSecurityTransparent || CustomAttribute.SpecialAllowCriticalAttributes((RuntimeType)method.DeclaringType);
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x00101135 File Offset: 0x000FF335
		private static bool AllowCriticalCustomAttributes(RuntimeFieldInfo field)
		{
			return !field.IsSecurityTransparent || CustomAttribute.SpecialAllowCriticalAttributes((RuntimeType)field.DeclaringType);
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x00101151 File Offset: 0x000FF351
		private static bool AllowCriticalCustomAttributes(RuntimeParameterInfo parameter)
		{
			return CustomAttribute.AllowCriticalCustomAttributes(parameter.DefiningMethod);
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x00101160 File Offset: 0x000FF360
		[SecurityCritical]
		internal static object[] GetCustomAttributes(RuntimeMethodInfo method, RuntimeType caType, bool inherit)
		{
			if (method.IsGenericMethod && !method.IsGenericMethodDefinition)
			{
				method = method.GetGenericMethodDefinition() as RuntimeMethodInfo;
			}
			int i = 0;
			Attribute[] customAttributes = PseudoCustomAttribute.GetCustomAttributes(method, caType, true, out i);
			if (!inherit || (caType.IsSealed && !CustomAttribute.GetAttributeUsage(caType).Inherited))
			{
				object[] customAttributes2 = CustomAttribute.GetCustomAttributes(method.GetRuntimeModule(), method.MetadataToken, i, caType, !CustomAttribute.AllowCriticalCustomAttributes(method));
				if (i > 0)
				{
					Array.Copy(customAttributes, 0, customAttributes2, customAttributes2.Length - i, i);
				}
				return customAttributes2;
			}
			List<object> list = new List<object>();
			bool flag = false;
			Type type = ((caType == null || caType.IsValueType || caType.ContainsGenericParameters) ? typeof(object) : caType);
			while (i > 0)
			{
				list.Add(customAttributes[--i]);
			}
			while (method != null)
			{
				object[] customAttributes3 = CustomAttribute.GetCustomAttributes(method.GetRuntimeModule(), method.MetadataToken, 0, caType, flag, list, !CustomAttribute.AllowCriticalCustomAttributes(method));
				flag = true;
				for (int j = 0; j < customAttributes3.Length; j++)
				{
					list.Add(customAttributes3[j]);
				}
				method = method.GetParentDefinition();
			}
			object[] array = CustomAttribute.CreateAttributeArrayHelper(type, list.Count);
			Array.Copy(list.ToArray(), 0, array, 0, list.Count);
			return array;
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x001012A8 File Offset: 0x000FF4A8
		[SecuritySafeCritical]
		internal static object[] GetCustomAttributes(RuntimeConstructorInfo ctor, RuntimeType caType)
		{
			int num = 0;
			Attribute[] customAttributes = PseudoCustomAttribute.GetCustomAttributes(ctor, caType, true, out num);
			object[] customAttributes2 = CustomAttribute.GetCustomAttributes(ctor.GetRuntimeModule(), ctor.MetadataToken, num, caType, !CustomAttribute.AllowCriticalCustomAttributes(ctor));
			if (num > 0)
			{
				Array.Copy(customAttributes, 0, customAttributes2, customAttributes2.Length - num, num);
			}
			return customAttributes2;
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x001012F4 File Offset: 0x000FF4F4
		[SecuritySafeCritical]
		internal static object[] GetCustomAttributes(RuntimePropertyInfo property, RuntimeType caType)
		{
			int num = 0;
			Attribute[] customAttributes = PseudoCustomAttribute.GetCustomAttributes(property, caType, out num);
			bool flag = property.GetRuntimeModule().GetRuntimeAssembly().IsAllSecurityTransparent();
			object[] customAttributes2 = CustomAttribute.GetCustomAttributes(property.GetRuntimeModule(), property.MetadataToken, num, caType, flag);
			if (num > 0)
			{
				Array.Copy(customAttributes, 0, customAttributes2, customAttributes2.Length - num, num);
			}
			return customAttributes2;
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x00101348 File Offset: 0x000FF548
		[SecuritySafeCritical]
		internal static object[] GetCustomAttributes(RuntimeEventInfo e, RuntimeType caType)
		{
			int num = 0;
			Attribute[] customAttributes = PseudoCustomAttribute.GetCustomAttributes(e, caType, out num);
			bool flag = e.GetRuntimeModule().GetRuntimeAssembly().IsAllSecurityTransparent();
			object[] customAttributes2 = CustomAttribute.GetCustomAttributes(e.GetRuntimeModule(), e.MetadataToken, num, caType, flag);
			if (num > 0)
			{
				Array.Copy(customAttributes, 0, customAttributes2, customAttributes2.Length - num, num);
			}
			return customAttributes2;
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x0010139C File Offset: 0x000FF59C
		[SecuritySafeCritical]
		internal static object[] GetCustomAttributes(RuntimeFieldInfo field, RuntimeType caType)
		{
			int num = 0;
			Attribute[] customAttributes = PseudoCustomAttribute.GetCustomAttributes(field, caType, out num);
			object[] customAttributes2 = CustomAttribute.GetCustomAttributes(field.GetRuntimeModule(), field.MetadataToken, num, caType, !CustomAttribute.AllowCriticalCustomAttributes(field));
			if (num > 0)
			{
				Array.Copy(customAttributes, 0, customAttributes2, customAttributes2.Length - num, num);
			}
			return customAttributes2;
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x001013E8 File Offset: 0x000FF5E8
		[SecuritySafeCritical]
		internal static object[] GetCustomAttributes(RuntimeParameterInfo parameter, RuntimeType caType)
		{
			int num = 0;
			Attribute[] customAttributes = PseudoCustomAttribute.GetCustomAttributes(parameter, caType, out num);
			object[] customAttributes2 = CustomAttribute.GetCustomAttributes(parameter.GetRuntimeModule(), parameter.MetadataToken, num, caType, !CustomAttribute.AllowCriticalCustomAttributes(parameter));
			if (num > 0)
			{
				Array.Copy(customAttributes, 0, customAttributes2, customAttributes2.Length - num, num);
			}
			return customAttributes2;
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x00101434 File Offset: 0x000FF634
		[SecuritySafeCritical]
		internal static object[] GetCustomAttributes(RuntimeAssembly assembly, RuntimeType caType)
		{
			int num = 0;
			Attribute[] customAttributes = PseudoCustomAttribute.GetCustomAttributes(assembly, caType, true, out num);
			int token = RuntimeAssembly.GetToken(assembly.GetNativeHandle());
			bool flag = assembly.IsAllSecurityTransparent();
			object[] customAttributes2 = CustomAttribute.GetCustomAttributes(assembly.ManifestModule as RuntimeModule, token, num, caType, flag);
			if (num > 0)
			{
				Array.Copy(customAttributes, 0, customAttributes2, customAttributes2.Length - num, num);
			}
			return customAttributes2;
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x00101490 File Offset: 0x000FF690
		[SecuritySafeCritical]
		internal static object[] GetCustomAttributes(RuntimeModule module, RuntimeType caType)
		{
			int num = 0;
			Attribute[] customAttributes = PseudoCustomAttribute.GetCustomAttributes(module, caType, out num);
			bool flag = module.GetRuntimeAssembly().IsAllSecurityTransparent();
			object[] customAttributes2 = CustomAttribute.GetCustomAttributes(module, module.MetadataToken, num, caType, flag);
			if (num > 0)
			{
				Array.Copy(customAttributes, 0, customAttributes2, customAttributes2.Length - num, num);
			}
			return customAttributes2;
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x001014D8 File Offset: 0x000FF6D8
		[SecuritySafeCritical]
		internal static bool IsAttributeDefined(RuntimeModule decoratedModule, int decoratedMetadataToken, int attributeCtorToken)
		{
			return CustomAttribute.IsCustomAttributeDefined(decoratedModule, decoratedMetadataToken, null, attributeCtorToken, false);
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x001014E4 File Offset: 0x000FF6E4
		[SecurityCritical]
		private static bool IsCustomAttributeDefined(RuntimeModule decoratedModule, int decoratedMetadataToken, RuntimeType attributeFilterType)
		{
			return CustomAttribute.IsCustomAttributeDefined(decoratedModule, decoratedMetadataToken, attributeFilterType, 0, false);
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x001014F0 File Offset: 0x000FF6F0
		[SecurityCritical]
		private static bool IsCustomAttributeDefined(RuntimeModule decoratedModule, int decoratedMetadataToken, RuntimeType attributeFilterType, int attributeCtorToken, bool mustBeInheritable)
		{
			if (decoratedModule.Assembly.ReflectionOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyCA"));
			}
			CustomAttributeRecord[] customAttributeRecords = CustomAttributeData.GetCustomAttributeRecords(decoratedModule, decoratedMetadataToken);
			if (attributeFilterType != null)
			{
				MetadataImport metadataImport = decoratedModule.MetadataImport;
				Assembly assembly = null;
				foreach (CustomAttributeRecord customAttributeRecord in customAttributeRecords)
				{
					RuntimeType runtimeType;
					IRuntimeMethodInfo runtimeMethodInfo;
					bool flag;
					bool flag2;
					if (CustomAttribute.FilterCustomAttributeRecord(customAttributeRecord, metadataImport, ref assembly, decoratedModule, decoratedMetadataToken, attributeFilterType, mustBeInheritable, null, null, out runtimeType, out runtimeMethodInfo, out flag, out flag2))
					{
						return true;
					}
				}
			}
			else
			{
				foreach (CustomAttributeRecord customAttributeRecord2 in customAttributeRecords)
				{
					if (customAttributeRecord2.tkCtor == attributeCtorToken)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x001015A3 File Offset: 0x000FF7A3
		[SecurityCritical]
		private static object[] GetCustomAttributes(RuntimeModule decoratedModule, int decoratedMetadataToken, int pcaCount, RuntimeType attributeFilterType, bool isDecoratedTargetSecurityTransparent)
		{
			return CustomAttribute.GetCustomAttributes(decoratedModule, decoratedMetadataToken, pcaCount, attributeFilterType, false, null, isDecoratedTargetSecurityTransparent);
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x001015B4 File Offset: 0x000FF7B4
		[SecurityCritical]
		private unsafe static object[] GetCustomAttributes(RuntimeModule decoratedModule, int decoratedMetadataToken, int pcaCount, RuntimeType attributeFilterType, bool mustBeInheritable, IList derivedAttributes, bool isDecoratedTargetSecurityTransparent)
		{
			if (decoratedModule.Assembly.ReflectionOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyCA"));
			}
			MetadataImport metadataImport = decoratedModule.MetadataImport;
			CustomAttributeRecord[] customAttributeRecords = CustomAttributeData.GetCustomAttributeRecords(decoratedModule, decoratedMetadataToken);
			Type type = ((attributeFilterType == null || attributeFilterType.IsValueType || attributeFilterType.ContainsGenericParameters) ? typeof(object) : attributeFilterType);
			if (attributeFilterType == null && customAttributeRecords.Length == 0)
			{
				return CustomAttribute.CreateAttributeArrayHelper(type, 0);
			}
			object[] array = CustomAttribute.CreateAttributeArrayHelper(type, customAttributeRecords.Length);
			int num = 0;
			SecurityContextFrame securityContextFrame = default(SecurityContextFrame);
			securityContextFrame.Push(decoratedModule.GetRuntimeAssembly());
			Assembly assembly = null;
			for (int i = 0; i < customAttributeRecords.Length; i++)
			{
				object obj = null;
				CustomAttributeRecord customAttributeRecord = customAttributeRecords[i];
				IRuntimeMethodInfo runtimeMethodInfo = null;
				RuntimeType runtimeType = null;
				int num2 = 0;
				IntPtr intPtr = customAttributeRecord.blob.Signature;
				IntPtr intPtr2 = (IntPtr)((void*)((byte*)(void*)intPtr + customAttributeRecord.blob.Length));
				int num3 = (int)((long)((byte*)(void*)intPtr2 - (byte*)(void*)intPtr));
				bool flag;
				bool flag2;
				if (CustomAttribute.FilterCustomAttributeRecord(customAttributeRecord, metadataImport, ref assembly, decoratedModule, decoratedMetadataToken, attributeFilterType, mustBeInheritable, array, derivedAttributes, out runtimeType, out runtimeMethodInfo, out flag, out flag2))
				{
					if (runtimeMethodInfo != null)
					{
						RuntimeMethodHandle.CheckLinktimeDemands(runtimeMethodInfo, decoratedModule, isDecoratedTargetSecurityTransparent);
					}
					RuntimeConstructorInfo.CheckCanCreateInstance(runtimeType, flag2);
					if (flag)
					{
						obj = CustomAttribute.CreateCaObject(decoratedModule, runtimeMethodInfo, ref intPtr, intPtr2, out num2);
					}
					else
					{
						obj = RuntimeTypeHandle.CreateCaInstance(runtimeType, runtimeMethodInfo);
						if (num3 == 0)
						{
							num2 = 0;
						}
						else
						{
							if (Marshal.ReadInt16(intPtr) != 1)
							{
								throw new CustomAttributeFormatException();
							}
							intPtr = (IntPtr)((void*)((byte*)(void*)intPtr + 2));
							num2 = (int)Marshal.ReadInt16(intPtr);
							intPtr = (IntPtr)((void*)((byte*)(void*)intPtr + 2));
						}
					}
					for (int j = 0; j < num2; j++)
					{
						IntPtr signature = customAttributeRecord.blob.Signature;
						string text;
						bool flag3;
						RuntimeType runtimeType2;
						object obj2;
						CustomAttribute.GetPropertyOrFieldData(decoratedModule, ref intPtr, intPtr2, out text, out flag3, out runtimeType2, out obj2);
						try
						{
							if (flag3)
							{
								if (runtimeType2 == null && obj2 != null)
								{
									runtimeType2 = (RuntimeType)obj2.GetType();
									if (runtimeType2 == CustomAttribute.Type_RuntimeType)
									{
										runtimeType2 = CustomAttribute.Type_Type;
									}
								}
								RuntimePropertyInfo runtimePropertyInfo;
								if (runtimeType2 == null)
								{
									runtimePropertyInfo = runtimeType.GetProperty(text) as RuntimePropertyInfo;
								}
								else
								{
									runtimePropertyInfo = runtimeType.GetProperty(text, runtimeType2, Type.EmptyTypes) as RuntimePropertyInfo;
								}
								if (runtimePropertyInfo == null)
								{
									throw new CustomAttributeFormatException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString(flag3 ? "RFLCT.InvalidPropFail" : "RFLCT.InvalidFieldFail"), text));
								}
								RuntimeMethodInfo runtimeMethodInfo2 = runtimePropertyInfo.GetSetMethod(true) as RuntimeMethodInfo;
								if (runtimeMethodInfo2.IsPublic)
								{
									RuntimeMethodHandle.CheckLinktimeDemands(runtimeMethodInfo2, decoratedModule, isDecoratedTargetSecurityTransparent);
									runtimeMethodInfo2.UnsafeInvoke(obj, BindingFlags.Default, null, new object[] { obj2 }, null);
								}
							}
							else
							{
								RtFieldInfo rtFieldInfo = runtimeType.GetField(text) as RtFieldInfo;
								if (isDecoratedTargetSecurityTransparent)
								{
									RuntimeFieldHandle.CheckAttributeAccess(rtFieldInfo.FieldHandle, decoratedModule.GetNativeHandle());
								}
								rtFieldInfo.CheckConsistency(obj);
								rtFieldInfo.UnsafeSetValue(obj, obj2, BindingFlags.Default, Type.DefaultBinder, null);
							}
						}
						catch (Exception ex)
						{
							throw new CustomAttributeFormatException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString(flag3 ? "RFLCT.InvalidPropFail" : "RFLCT.InvalidFieldFail"), text), ex);
						}
					}
					if (!intPtr.Equals(intPtr2))
					{
						throw new CustomAttributeFormatException();
					}
					array[num++] = obj;
				}
			}
			securityContextFrame.Pop();
			if (num == customAttributeRecords.Length && pcaCount == 0)
			{
				return array;
			}
			object[] array2 = CustomAttribute.CreateAttributeArrayHelper(type, num + pcaCount);
			Array.Copy(array, 0, array2, 0, num);
			return array2;
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x00101954 File Offset: 0x000FFB54
		[SecurityCritical]
		private unsafe static bool FilterCustomAttributeRecord(CustomAttributeRecord caRecord, MetadataImport scope, ref Assembly lastAptcaOkAssembly, RuntimeModule decoratedModule, MetadataToken decoratedToken, RuntimeType attributeFilterType, bool mustBeInheritable, object[] attributes, IList derivedAttributes, out RuntimeType attributeType, out IRuntimeMethodInfo ctor, out bool ctorHasParameters, out bool isVarArg)
		{
			ctor = null;
			attributeType = null;
			ctorHasParameters = false;
			isVarArg = false;
			IntPtr signature = caRecord.blob.Signature;
			IntPtr intPtr = (IntPtr)((void*)((byte*)(void*)signature + caRecord.blob.Length));
			attributeType = decoratedModule.ResolveType(scope.GetParentToken(caRecord.tkCtor), null, null) as RuntimeType;
			if (!attributeFilterType.IsAssignableFrom(attributeType))
			{
				return false;
			}
			if (!CustomAttribute.AttributeUsageCheck(attributeType, mustBeInheritable, attributes, derivedAttributes))
			{
				return false;
			}
			if ((attributeType.Attributes & TypeAttributes.WindowsRuntime) == TypeAttributes.WindowsRuntime)
			{
				return false;
			}
			RuntimeAssembly runtimeAssembly = (RuntimeAssembly)attributeType.Assembly;
			RuntimeAssembly runtimeAssembly2 = (RuntimeAssembly)decoratedModule.Assembly;
			if (runtimeAssembly != lastAptcaOkAssembly && !RuntimeAssembly.AptcaCheck(runtimeAssembly, runtimeAssembly2))
			{
				return false;
			}
			lastAptcaOkAssembly = runtimeAssembly2;
			ConstArray methodSignature = scope.GetMethodSignature(caRecord.tkCtor);
			isVarArg = (methodSignature[0] & 5) > 0;
			ctorHasParameters = methodSignature[1] > 0;
			if (ctorHasParameters)
			{
				ctor = ModuleHandle.ResolveMethodHandleInternal(decoratedModule.GetNativeHandle(), caRecord.tkCtor);
			}
			else
			{
				ctor = attributeType.GetTypeHandleInternal().GetDefaultConstructor();
				if (ctor == null && !attributeType.IsValueType)
				{
					throw new MissingMethodException(".ctor");
				}
			}
			MetadataToken metadataToken = default(MetadataToken);
			if (decoratedToken.IsParamDef)
			{
				metadataToken = new MetadataToken(scope.GetParentToken(decoratedToken));
				metadataToken = new MetadataToken(scope.GetParentToken(metadataToken));
			}
			else if (decoratedToken.IsMethodDef || decoratedToken.IsProperty || decoratedToken.IsEvent || decoratedToken.IsFieldDef)
			{
				metadataToken = new MetadataToken(scope.GetParentToken(decoratedToken));
			}
			else if (decoratedToken.IsTypeDef)
			{
				metadataToken = decoratedToken;
			}
			else if (decoratedToken.IsGenericPar)
			{
				metadataToken = new MetadataToken(scope.GetParentToken(decoratedToken));
				if (metadataToken.IsMethodDef)
				{
					metadataToken = new MetadataToken(scope.GetParentToken(metadataToken));
				}
			}
			RuntimeTypeHandle runtimeTypeHandle = (metadataToken.IsTypeDef ? decoratedModule.ModuleHandle.ResolveTypeHandle(metadataToken) : default(RuntimeTypeHandle));
			return RuntimeMethodHandle.IsCAVisibleFromDecoratedType(attributeType.TypeHandle, ctor, runtimeTypeHandle, decoratedModule);
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x00101BA4 File Offset: 0x000FFDA4
		[SecurityCritical]
		private static bool AttributeUsageCheck(RuntimeType attributeType, bool mustBeInheritable, object[] attributes, IList derivedAttributes)
		{
			AttributeUsageAttribute attributeUsageAttribute = null;
			if (mustBeInheritable)
			{
				attributeUsageAttribute = CustomAttribute.GetAttributeUsage(attributeType);
				if (!attributeUsageAttribute.Inherited)
				{
					return false;
				}
			}
			if (derivedAttributes == null)
			{
				return true;
			}
			for (int i = 0; i < derivedAttributes.Count; i++)
			{
				if (derivedAttributes[i].GetType() == attributeType)
				{
					if (attributeUsageAttribute == null)
					{
						attributeUsageAttribute = CustomAttribute.GetAttributeUsage(attributeType);
					}
					return attributeUsageAttribute.AllowMultiple;
				}
			}
			return true;
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x00101C04 File Offset: 0x000FFE04
		[SecurityCritical]
		internal static AttributeUsageAttribute GetAttributeUsage(RuntimeType decoratedAttribute)
		{
			RuntimeModule runtimeModule = decoratedAttribute.GetRuntimeModule();
			MetadataImport metadataImport = runtimeModule.MetadataImport;
			CustomAttributeRecord[] customAttributeRecords = CustomAttributeData.GetCustomAttributeRecords(runtimeModule, decoratedAttribute.MetadataToken);
			AttributeUsageAttribute attributeUsageAttribute = null;
			foreach (CustomAttributeRecord customAttributeRecord in customAttributeRecords)
			{
				RuntimeType runtimeType = runtimeModule.ResolveType(metadataImport.GetParentToken(customAttributeRecord.tkCtor), null, null) as RuntimeType;
				if (!(runtimeType != (RuntimeType)typeof(AttributeUsageAttribute)))
				{
					if (attributeUsageAttribute != null)
					{
						throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Format_AttributeUsage"), runtimeType));
					}
					AttributeTargets attributeTargets;
					bool flag;
					bool flag2;
					CustomAttribute.ParseAttributeUsageAttribute(customAttributeRecord.blob, out attributeTargets, out flag, out flag2);
					attributeUsageAttribute = new AttributeUsageAttribute(attributeTargets, flag2, flag);
				}
			}
			if (attributeUsageAttribute == null)
			{
				return AttributeUsageAttribute.Default;
			}
			return attributeUsageAttribute;
		}

		// Token: 0x060045AB RID: 17835
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _ParseAttributeUsageAttribute(IntPtr pCa, int cCa, out int targets, out bool inherited, out bool allowMultiple);

		// Token: 0x060045AC RID: 17836 RVA: 0x00101CD4 File Offset: 0x000FFED4
		[SecurityCritical]
		private static void ParseAttributeUsageAttribute(ConstArray ca, out AttributeTargets targets, out bool inherited, out bool allowMultiple)
		{
			int num;
			CustomAttribute._ParseAttributeUsageAttribute(ca.Signature, ca.Length, out num, out inherited, out allowMultiple);
			targets = (AttributeTargets)num;
		}

		// Token: 0x060045AD RID: 17837
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern object _CreateCaObject(RuntimeModule pModule, IRuntimeMethodInfo pCtor, byte** ppBlob, byte* pEndBlob, int* pcNamedArgs);

		// Token: 0x060045AE RID: 17838 RVA: 0x00101CFC File Offset: 0x000FFEFC
		[SecurityCritical]
		private unsafe static object CreateCaObject(RuntimeModule module, IRuntimeMethodInfo ctor, ref IntPtr blob, IntPtr blobEnd, out int namedArgs)
		{
			byte* ptr = (byte*)(void*)blob;
			byte* ptr2 = (byte*)(void*)blobEnd;
			int num;
			object obj = CustomAttribute._CreateCaObject(module, ctor, &ptr, ptr2, &num);
			blob = (IntPtr)((void*)ptr);
			namedArgs = num;
			return obj;
		}

		// Token: 0x060045AF RID: 17839
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetPropertyOrFieldData(RuntimeModule pModule, byte** ppBlobStart, byte* pBlobEnd, out string name, out bool bIsProperty, out RuntimeType type, out object value);

		// Token: 0x060045B0 RID: 17840 RVA: 0x00101D34 File Offset: 0x000FFF34
		[SecurityCritical]
		private unsafe static void GetPropertyOrFieldData(RuntimeModule module, ref IntPtr blobStart, IntPtr blobEnd, out string name, out bool isProperty, out RuntimeType type, out object value)
		{
			byte* ptr = (byte*)(void*)blobStart;
			CustomAttribute._GetPropertyOrFieldData(module.GetNativeHandle(), &ptr, (byte*)(void*)blobEnd, out name, out isProperty, out type, out value);
			blobStart = (IntPtr)((void*)ptr);
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x00101D6C File Offset: 0x000FFF6C
		[SecuritySafeCritical]
		private static object[] CreateAttributeArrayHelper(Type elementType, int elementCount)
		{
			return (object[])Array.UnsafeCreateInstance(elementType, elementCount);
		}

		// Token: 0x04001C97 RID: 7319
		private static RuntimeType Type_RuntimeType = (RuntimeType)typeof(RuntimeType);

		// Token: 0x04001C98 RID: 7320
		private static RuntimeType Type_Type = (RuntimeType)typeof(Type);
	}
}
