using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x020009BF RID: 2495
	internal class EventProviderWriter
	{
		// Token: 0x060063BA RID: 25530 RVA: 0x0015453C File Offset: 0x0015273C
		public EventProviderWriter(ModuleBuilder OutputModule, string strDestTypeName, Type EventItfType, Type SrcItfType, Type SinkHelperType)
		{
			this.m_OutputModule = OutputModule;
			this.m_strDestTypeName = strDestTypeName;
			this.m_EventItfType = EventItfType;
			this.m_SrcItfType = SrcItfType;
			this.m_SinkHelperType = SinkHelperType;
		}

		// Token: 0x060063BB RID: 25531 RVA: 0x0015459C File Offset: 0x0015279C
		public Type Perform()
		{
			TypeBuilder typeBuilder = this.m_OutputModule.DefineType(this.m_strDestTypeName, TypeAttributes.Sealed, typeof(object), new Type[]
			{
				this.m_EventItfType,
				typeof(IDisposable)
			});
			FieldBuilder fieldBuilder = typeBuilder.DefineField("m_ConnectionPointContainer", typeof(IConnectionPointContainer), FieldAttributes.Private);
			FieldBuilder fieldBuilder2 = typeBuilder.DefineField("m_aEventSinkHelpers", typeof(ArrayList), FieldAttributes.Private);
			FieldBuilder fieldBuilder3 = typeBuilder.DefineField("m_ConnectionPoint", typeof(IConnectionPoint), FieldAttributes.Private);
			MethodBuilder methodBuilder = this.DefineInitSrcItfMethod(typeBuilder, this.m_SrcItfType, fieldBuilder2, fieldBuilder3, fieldBuilder);
			MethodInfo[] nonPropertyMethods = TCEAdapterGenerator.GetNonPropertyMethods(this.m_SrcItfType);
			for (int i = 0; i < nonPropertyMethods.Length; i++)
			{
				if (this.m_SrcItfType == nonPropertyMethods[i].DeclaringType)
				{
					MethodBuilder methodBuilder2 = this.DefineAddEventMethod(typeBuilder, nonPropertyMethods[i], this.m_SinkHelperType, fieldBuilder2, fieldBuilder3, methodBuilder);
					MethodBuilder methodBuilder3 = this.DefineRemoveEventMethod(typeBuilder, nonPropertyMethods[i], this.m_SinkHelperType, fieldBuilder2, fieldBuilder3);
				}
			}
			this.DefineConstructor(typeBuilder, fieldBuilder);
			MethodBuilder methodBuilder4 = this.DefineFinalizeMethod(typeBuilder, this.m_SinkHelperType, fieldBuilder2, fieldBuilder3);
			this.DefineDisposeMethod(typeBuilder, methodBuilder4);
			return typeBuilder.CreateType();
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x001546CC File Offset: 0x001528CC
		private MethodBuilder DefineAddEventMethod(TypeBuilder OutputTypeBuilder, MethodInfo SrcItfMethod, Type SinkHelperClass, FieldBuilder fbSinkHelperArray, FieldBuilder fbEventCP, MethodBuilder mbInitSrcItf)
		{
			FieldInfo field = SinkHelperClass.GetField("m_" + SrcItfMethod.Name + "Delegate");
			FieldInfo field2 = SinkHelperClass.GetField("m_dwCookie");
			ConstructorInfo constructor = SinkHelperClass.GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
			MethodInfo method = typeof(IConnectionPoint).GetMethod("Advise");
			Type[] array = new Type[] { typeof(object) };
			MethodInfo method2 = typeof(ArrayList).GetMethod("Add", array, null);
			MethodInfo method3 = typeof(Monitor).GetMethod("Enter", this.MonitorEnterParamTypes, null);
			array[0] = typeof(object);
			MethodInfo method4 = typeof(Monitor).GetMethod("Exit", array, null);
			Type[] array2 = new Type[] { field.FieldType };
			MethodBuilder methodBuilder = OutputTypeBuilder.DefineMethod("add_" + SrcItfMethod.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual, null, array2);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			Label label = ilgenerator.DefineLabel();
			LocalBuilder localBuilder = ilgenerator.DeclareLocal(SinkHelperClass);
			LocalBuilder localBuilder2 = ilgenerator.DeclareLocal(typeof(int));
			LocalBuilder localBuilder3 = ilgenerator.DeclareLocal(typeof(bool));
			ilgenerator.BeginExceptionBlock();
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldloca_S, localBuilder3);
			ilgenerator.Emit(OpCodes.Call, method3);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbEventCP);
			ilgenerator.Emit(OpCodes.Brtrue, label);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Call, mbInitSrcItf);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Newobj, constructor);
			ilgenerator.Emit(OpCodes.Stloc, localBuilder);
			ilgenerator.Emit(OpCodes.Ldc_I4_0);
			ilgenerator.Emit(OpCodes.Stloc, localBuilder2);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbEventCP);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder);
			ilgenerator.Emit(OpCodes.Castclass, typeof(object));
			ilgenerator.Emit(OpCodes.Ldloca, localBuilder2);
			ilgenerator.Emit(OpCodes.Callvirt, method);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder2);
			ilgenerator.Emit(OpCodes.Stfld, field2);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder);
			ilgenerator.Emit(OpCodes.Ldarg, 1);
			ilgenerator.Emit(OpCodes.Stfld, field);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbSinkHelperArray);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder);
			ilgenerator.Emit(OpCodes.Castclass, typeof(object));
			ilgenerator.Emit(OpCodes.Callvirt, method2);
			ilgenerator.Emit(OpCodes.Pop);
			ilgenerator.BeginFinallyBlock();
			Label label2 = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder3);
			ilgenerator.Emit(OpCodes.Brfalse_S, label2);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Call, method4);
			ilgenerator.MarkLabel(label2);
			ilgenerator.EndExceptionBlock();
			ilgenerator.Emit(OpCodes.Ret);
			return methodBuilder;
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x00154A24 File Offset: 0x00152C24
		private MethodBuilder DefineRemoveEventMethod(TypeBuilder OutputTypeBuilder, MethodInfo SrcItfMethod, Type SinkHelperClass, FieldBuilder fbSinkHelperArray, FieldBuilder fbEventCP)
		{
			FieldInfo field = SinkHelperClass.GetField("m_" + SrcItfMethod.Name + "Delegate");
			FieldInfo field2 = SinkHelperClass.GetField("m_dwCookie");
			Type[] array = new Type[] { typeof(int) };
			MethodInfo method = typeof(ArrayList).GetMethod("RemoveAt", array, null);
			PropertyInfo property = typeof(ArrayList).GetProperty("Item");
			MethodInfo getMethod = property.GetGetMethod();
			PropertyInfo property2 = typeof(ArrayList).GetProperty("Count");
			MethodInfo getMethod2 = property2.GetGetMethod();
			array[0] = typeof(Delegate);
			MethodInfo method2 = typeof(Delegate).GetMethod("Equals", array, null);
			MethodInfo method3 = typeof(Monitor).GetMethod("Enter", this.MonitorEnterParamTypes, null);
			array[0] = typeof(object);
			MethodInfo method4 = typeof(Monitor).GetMethod("Exit", array, null);
			MethodInfo method5 = typeof(IConnectionPoint).GetMethod("Unadvise");
			MethodInfo method6 = typeof(Marshal).GetMethod("ReleaseComObject");
			Type[] array2 = new Type[] { field.FieldType };
			MethodBuilder methodBuilder = OutputTypeBuilder.DefineMethod("remove_" + SrcItfMethod.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual, null, array2);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			LocalBuilder localBuilder = ilgenerator.DeclareLocal(typeof(int));
			LocalBuilder localBuilder2 = ilgenerator.DeclareLocal(typeof(int));
			LocalBuilder localBuilder3 = ilgenerator.DeclareLocal(SinkHelperClass);
			LocalBuilder localBuilder4 = ilgenerator.DeclareLocal(typeof(bool));
			Label label = ilgenerator.DefineLabel();
			Label label2 = ilgenerator.DefineLabel();
			Label label3 = ilgenerator.DefineLabel();
			Label label4 = ilgenerator.DefineLabel();
			ilgenerator.BeginExceptionBlock();
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldloca_S, localBuilder4);
			ilgenerator.Emit(OpCodes.Call, method3);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbSinkHelperArray);
			ilgenerator.Emit(OpCodes.Brfalse, label2);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbSinkHelperArray);
			ilgenerator.Emit(OpCodes.Callvirt, getMethod2);
			ilgenerator.Emit(OpCodes.Stloc, localBuilder);
			ilgenerator.Emit(OpCodes.Ldc_I4, 0);
			ilgenerator.Emit(OpCodes.Stloc, localBuilder2);
			ilgenerator.Emit(OpCodes.Ldc_I4, 0);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder);
			ilgenerator.Emit(OpCodes.Bge, label2);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbSinkHelperArray);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder2);
			ilgenerator.Emit(OpCodes.Callvirt, getMethod);
			ilgenerator.Emit(OpCodes.Castclass, SinkHelperClass);
			ilgenerator.Emit(OpCodes.Stloc, localBuilder3);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder3);
			ilgenerator.Emit(OpCodes.Ldfld, field);
			ilgenerator.Emit(OpCodes.Ldnull);
			ilgenerator.Emit(OpCodes.Beq, label3);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder3);
			ilgenerator.Emit(OpCodes.Ldfld, field);
			ilgenerator.Emit(OpCodes.Ldarg, 1);
			ilgenerator.Emit(OpCodes.Castclass, typeof(object));
			ilgenerator.Emit(OpCodes.Callvirt, method2);
			ilgenerator.Emit(OpCodes.Ldc_I4, 255);
			ilgenerator.Emit(OpCodes.And);
			ilgenerator.Emit(OpCodes.Ldc_I4, 0);
			ilgenerator.Emit(OpCodes.Beq, label3);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbSinkHelperArray);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder2);
			ilgenerator.Emit(OpCodes.Callvirt, method);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbEventCP);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder3);
			ilgenerator.Emit(OpCodes.Ldfld, field2);
			ilgenerator.Emit(OpCodes.Callvirt, method5);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder);
			ilgenerator.Emit(OpCodes.Ldc_I4, 1);
			ilgenerator.Emit(OpCodes.Bgt, label2);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbEventCP);
			ilgenerator.Emit(OpCodes.Call, method6);
			ilgenerator.Emit(OpCodes.Pop);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldnull);
			ilgenerator.Emit(OpCodes.Stfld, fbEventCP);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldnull);
			ilgenerator.Emit(OpCodes.Stfld, fbSinkHelperArray);
			ilgenerator.Emit(OpCodes.Br, label2);
			ilgenerator.MarkLabel(label3);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder2);
			ilgenerator.Emit(OpCodes.Ldc_I4, 1);
			ilgenerator.Emit(OpCodes.Add);
			ilgenerator.Emit(OpCodes.Stloc, localBuilder2);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder2);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder);
			ilgenerator.Emit(OpCodes.Blt, label);
			ilgenerator.MarkLabel(label2);
			ilgenerator.BeginFinallyBlock();
			Label label5 = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder4);
			ilgenerator.Emit(OpCodes.Brfalse_S, label5);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Call, method4);
			ilgenerator.MarkLabel(label5);
			ilgenerator.EndExceptionBlock();
			ilgenerator.Emit(OpCodes.Ret);
			return methodBuilder;
		}

		// Token: 0x060063BE RID: 25534 RVA: 0x00154FE8 File Offset: 0x001531E8
		private MethodBuilder DefineInitSrcItfMethod(TypeBuilder OutputTypeBuilder, Type SourceInterface, FieldBuilder fbSinkHelperArray, FieldBuilder fbEventCP, FieldBuilder fbCPC)
		{
			ConstructorInfo constructor = typeof(ArrayList).GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, new Type[0], null);
			byte[] array = new byte[16];
			Type[] array2 = new Type[] { typeof(byte[]) };
			ConstructorInfo constructor2 = typeof(Guid).GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, array2, null);
			MethodInfo method = typeof(IConnectionPointContainer).GetMethod("FindConnectionPoint");
			MethodBuilder methodBuilder = OutputTypeBuilder.DefineMethod("Init", MethodAttributes.Private, null, null);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			LocalBuilder localBuilder = ilgenerator.DeclareLocal(typeof(IConnectionPoint));
			LocalBuilder localBuilder2 = ilgenerator.DeclareLocal(typeof(Guid));
			LocalBuilder localBuilder3 = ilgenerator.DeclareLocal(typeof(byte[]));
			ilgenerator.Emit(OpCodes.Ldnull);
			ilgenerator.Emit(OpCodes.Stloc, localBuilder);
			array = SourceInterface.GUID.ToByteArray();
			ilgenerator.Emit(OpCodes.Ldc_I4, 16);
			ilgenerator.Emit(OpCodes.Newarr, typeof(byte));
			ilgenerator.Emit(OpCodes.Stloc, localBuilder3);
			for (int i = 0; i < 16; i++)
			{
				ilgenerator.Emit(OpCodes.Ldloc, localBuilder3);
				ilgenerator.Emit(OpCodes.Ldc_I4, i);
				ilgenerator.Emit(OpCodes.Ldc_I4, (int)array[i]);
				ilgenerator.Emit(OpCodes.Stelem_I1);
			}
			ilgenerator.Emit(OpCodes.Ldloca, localBuilder2);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder3);
			ilgenerator.Emit(OpCodes.Call, constructor2);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbCPC);
			ilgenerator.Emit(OpCodes.Ldloca, localBuilder2);
			ilgenerator.Emit(OpCodes.Ldloca, localBuilder);
			ilgenerator.Emit(OpCodes.Callvirt, method);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder);
			ilgenerator.Emit(OpCodes.Castclass, typeof(IConnectionPoint));
			ilgenerator.Emit(OpCodes.Stfld, fbEventCP);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Newobj, constructor);
			ilgenerator.Emit(OpCodes.Stfld, fbSinkHelperArray);
			ilgenerator.Emit(OpCodes.Ret);
			return methodBuilder;
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x00155230 File Offset: 0x00153430
		private void DefineConstructor(TypeBuilder OutputTypeBuilder, FieldBuilder fbCPC)
		{
			ConstructorInfo constructor = typeof(object).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[0], null);
			MethodAttributes methodAttributes = MethodAttributes.SpecialName | (constructor.Attributes & MethodAttributes.MemberAccessMask);
			MethodBuilder methodBuilder = OutputTypeBuilder.DefineMethod(".ctor", methodAttributes, null, new Type[] { typeof(object) });
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Call, constructor);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldarg, 1);
			ilgenerator.Emit(OpCodes.Castclass, typeof(IConnectionPointContainer));
			ilgenerator.Emit(OpCodes.Stfld, fbCPC);
			ilgenerator.Emit(OpCodes.Ret);
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x001552EC File Offset: 0x001534EC
		private MethodBuilder DefineFinalizeMethod(TypeBuilder OutputTypeBuilder, Type SinkHelperClass, FieldBuilder fbSinkHelper, FieldBuilder fbEventCP)
		{
			FieldInfo field = SinkHelperClass.GetField("m_dwCookie");
			PropertyInfo property = typeof(ArrayList).GetProperty("Item");
			MethodInfo getMethod = property.GetGetMethod();
			PropertyInfo property2 = typeof(ArrayList).GetProperty("Count");
			MethodInfo getMethod2 = property2.GetGetMethod();
			MethodInfo method = typeof(IConnectionPoint).GetMethod("Unadvise");
			MethodInfo method2 = typeof(Marshal).GetMethod("ReleaseComObject");
			MethodInfo method3 = typeof(Monitor).GetMethod("Enter", this.MonitorEnterParamTypes, null);
			Type[] array = new Type[] { typeof(object) };
			MethodInfo method4 = typeof(Monitor).GetMethod("Exit", array, null);
			MethodBuilder methodBuilder = OutputTypeBuilder.DefineMethod("Finalize", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual, null, null);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			LocalBuilder localBuilder = ilgenerator.DeclareLocal(typeof(int));
			LocalBuilder localBuilder2 = ilgenerator.DeclareLocal(typeof(int));
			LocalBuilder localBuilder3 = ilgenerator.DeclareLocal(SinkHelperClass);
			LocalBuilder localBuilder4 = ilgenerator.DeclareLocal(typeof(bool));
			ilgenerator.BeginExceptionBlock();
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldloca_S, localBuilder4);
			ilgenerator.Emit(OpCodes.Call, method3);
			Label label = ilgenerator.DefineLabel();
			Label label2 = ilgenerator.DefineLabel();
			Label label3 = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbEventCP);
			ilgenerator.Emit(OpCodes.Brfalse, label3);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbSinkHelper);
			ilgenerator.Emit(OpCodes.Callvirt, getMethod2);
			ilgenerator.Emit(OpCodes.Stloc, localBuilder);
			ilgenerator.Emit(OpCodes.Ldc_I4, 0);
			ilgenerator.Emit(OpCodes.Stloc, localBuilder2);
			ilgenerator.Emit(OpCodes.Ldc_I4, 0);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder);
			ilgenerator.Emit(OpCodes.Bge, label2);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbSinkHelper);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder2);
			ilgenerator.Emit(OpCodes.Callvirt, getMethod);
			ilgenerator.Emit(OpCodes.Castclass, SinkHelperClass);
			ilgenerator.Emit(OpCodes.Stloc, localBuilder3);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbEventCP);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder3);
			ilgenerator.Emit(OpCodes.Ldfld, field);
			ilgenerator.Emit(OpCodes.Callvirt, method);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder2);
			ilgenerator.Emit(OpCodes.Ldc_I4, 1);
			ilgenerator.Emit(OpCodes.Add);
			ilgenerator.Emit(OpCodes.Stloc, localBuilder2);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder2);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder);
			ilgenerator.Emit(OpCodes.Blt, label);
			ilgenerator.MarkLabel(label2);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbEventCP);
			ilgenerator.Emit(OpCodes.Call, method2);
			ilgenerator.Emit(OpCodes.Pop);
			ilgenerator.MarkLabel(label3);
			ilgenerator.BeginCatchBlock(typeof(Exception));
			ilgenerator.Emit(OpCodes.Pop);
			ilgenerator.BeginFinallyBlock();
			Label label4 = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder4);
			ilgenerator.Emit(OpCodes.Brfalse_S, label4);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Call, method4);
			ilgenerator.MarkLabel(label4);
			ilgenerator.EndExceptionBlock();
			ilgenerator.Emit(OpCodes.Ret);
			return methodBuilder;
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x001556C8 File Offset: 0x001538C8
		private void DefineDisposeMethod(TypeBuilder OutputTypeBuilder, MethodBuilder FinalizeMethod)
		{
			MethodInfo method = typeof(GC).GetMethod("SuppressFinalize");
			MethodBuilder methodBuilder = OutputTypeBuilder.DefineMethod("Dispose", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual, null, null);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Callvirt, FinalizeMethod);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Call, method);
			ilgenerator.Emit(OpCodes.Ret);
		}

		// Token: 0x04002CCD RID: 11469
		private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		// Token: 0x04002CCE RID: 11470
		private readonly Type[] MonitorEnterParamTypes = new Type[]
		{
			typeof(object),
			Type.GetType("System.Boolean&")
		};

		// Token: 0x04002CCF RID: 11471
		private ModuleBuilder m_OutputModule;

		// Token: 0x04002CD0 RID: 11472
		private string m_strDestTypeName;

		// Token: 0x04002CD1 RID: 11473
		private Type m_EventItfType;

		// Token: 0x04002CD2 RID: 11474
		private Type m_SrcItfType;

		// Token: 0x04002CD3 RID: 11475
		private Type m_SinkHelperType;
	}
}
