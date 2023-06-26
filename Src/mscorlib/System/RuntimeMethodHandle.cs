﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
	/// <summary>
	///   <see cref="T:System.RuntimeMethodHandle" /> is a handle to the internal metadata representation of a method.</summary>
	// Token: 0x02000134 RID: 308
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct RuntimeMethodHandle : ISerializable
	{
		// Token: 0x06001238 RID: 4664 RVA: 0x000373FD File Offset: 0x000355FD
		internal static IRuntimeMethodInfo EnsureNonNullMethodInfo(IRuntimeMethodInfo method)
		{
			if (method == null)
			{
				throw new ArgumentNullException(null, Environment.GetResourceString("Arg_InvalidHandle"));
			}
			return method;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x00037414 File Offset: 0x00035614
		internal static RuntimeMethodHandle EmptyHandle
		{
			get
			{
				return default(RuntimeMethodHandle);
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0003742A File Offset: 0x0003562A
		internal RuntimeMethodHandle(IRuntimeMethodInfo method)
		{
			this.m_value = method;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00037433 File Offset: 0x00035633
		internal IRuntimeMethodInfo GetMethodInfo()
		{
			return this.m_value;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0003743B File Offset: 0x0003563B
		[SecurityCritical]
		private static IntPtr GetValueInternal(RuntimeMethodHandle rmh)
		{
			return rmh.Value;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00037444 File Offset: 0x00035644
		[SecurityCritical]
		private RuntimeMethodHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MethodBase methodBase = (MethodBase)info.GetValue("MethodObj", typeof(MethodBase));
			this.m_value = methodBase.MethodHandle.m_value;
			if (this.m_value == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data necessary to deserialize the field represented by this instance.</summary>
		/// <param name="info">The object to populate with serialization information.</param>
		/// <param name="context">(Reserved) The place to store and retrieve serialized data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <see cref="P:System.RuntimeMethodHandle.Value" /> is invalid.</exception>
		// Token: 0x0600123E RID: 4670 RVA: 0x000374A8 File Offset: 0x000356A8
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.m_value == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFieldState"));
			}
			MethodBase methodBase = RuntimeType.GetMethodBase(this.m_value);
			info.AddValue("MethodObj", methodBase, typeof(MethodBase));
		}

		/// <summary>Gets the value of this instance.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> that is the internal metadata representation of a method.</returns>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00037500 File Offset: 0x00035700
		public IntPtr Value
		{
			[SecurityCritical]
			get
			{
				if (this.m_value == null)
				{
					return IntPtr.Zero;
				}
				return this.m_value.Value.Value;
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001240 RID: 4672 RVA: 0x0003752E File Offset: 0x0003572E
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return ValueType.GetHashCodeOfPtr(this.Value);
		}

		/// <summary>Indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">A <see cref="T:System.Object" /> to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.RuntimeMethodHandle" /> and equal to the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001241 RID: 4673 RVA: 0x0003753C File Offset: 0x0003573C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is RuntimeMethodHandle && ((RuntimeMethodHandle)obj).Value == this.Value;
		}

		/// <summary>Indicates whether two instances of <see cref="T:System.RuntimeMethodHandle" /> are equal.</summary>
		/// <param name="left">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="left" /> is equal to the value of <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001242 RID: 4674 RVA: 0x0003756C File Offset: 0x0003576C
		[__DynamicallyInvokable]
		public static bool operator ==(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return left.Equals(right);
		}

		/// <summary>Indicates whether two instances of <see cref="T:System.RuntimeMethodHandle" /> are not equal.</summary>
		/// <param name="left">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="left" /> is unequal to the value of <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001243 RID: 4675 RVA: 0x00037576 File Offset: 0x00035776
		[__DynamicallyInvokable]
		public static bool operator !=(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return !left.Equals(right);
		}

		/// <summary>Indicates whether this instance is equal to a specified <see cref="T:System.RuntimeMethodHandle" />.</summary>
		/// <param name="handle">A <see cref="T:System.RuntimeMethodHandle" /> to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="handle" /> is equal to the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001244 RID: 4676 RVA: 0x00037583 File Offset: 0x00035783
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool Equals(RuntimeMethodHandle handle)
		{
			return handle.Value == this.Value;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00037597 File Offset: 0x00035797
		internal bool IsNullHandle()
		{
			return this.m_value == null;
		}

		// Token: 0x06001246 RID: 4678
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern IntPtr GetFunctionPointer(RuntimeMethodHandleInternal handle);

		/// <summary>Obtains a pointer to the method represented by this instance.</summary>
		/// <returns>A pointer to the method represented by this instance.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the necessary permission to perform this operation.</exception>
		// Token: 0x06001247 RID: 4679 RVA: 0x000375A4 File Offset: 0x000357A4
		[SecurityCritical]
		public IntPtr GetFunctionPointer()
		{
			IntPtr functionPointer = RuntimeMethodHandle.GetFunctionPointer(RuntimeMethodHandle.EnsureNonNullMethodInfo(this.m_value).Value);
			GC.KeepAlive(this.m_value);
			return functionPointer;
		}

		// Token: 0x06001248 RID: 4680
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CheckLinktimeDemands(IRuntimeMethodInfo method, RuntimeModule module, bool isDecoratedTargetSecurityTransparent);

		// Token: 0x06001249 RID: 4681
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool IsCAVisibleFromDecoratedType(RuntimeTypeHandle attrTypeHandle, IRuntimeMethodInfo attrCtor, RuntimeTypeHandle sourceTypeHandle, RuntimeModule sourceModule);

		// Token: 0x0600124A RID: 4682
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IRuntimeMethodInfo _GetCurrentMethod(ref StackCrawlMark stackMark);

		// Token: 0x0600124B RID: 4683 RVA: 0x000375D3 File Offset: 0x000357D3
		[SecuritySafeCritical]
		internal static IRuntimeMethodInfo GetCurrentMethod(ref StackCrawlMark stackMark)
		{
			return RuntimeMethodHandle._GetCurrentMethod(ref stackMark);
		}

		// Token: 0x0600124C RID: 4684
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodAttributes GetAttributes(RuntimeMethodHandleInternal method);

		// Token: 0x0600124D RID: 4685 RVA: 0x000375DC File Offset: 0x000357DC
		[SecurityCritical]
		internal static MethodAttributes GetAttributes(IRuntimeMethodInfo method)
		{
			MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(method.Value);
			GC.KeepAlive(method);
			return attributes;
		}

		// Token: 0x0600124E RID: 4686
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodImplAttributes GetImplAttributes(IRuntimeMethodInfo method);

		// Token: 0x0600124F RID: 4687
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ConstructInstantiation(IRuntimeMethodInfo method, TypeNameFormatFlags format, StringHandleOnStack retString);

		// Token: 0x06001250 RID: 4688 RVA: 0x000375FC File Offset: 0x000357FC
		[SecuritySafeCritical]
		internal static string ConstructInstantiation(IRuntimeMethodInfo method, TypeNameFormatFlags format)
		{
			string text = null;
			RuntimeMethodHandle.ConstructInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method), format, JitHelpers.GetStringHandleOnStack(ref text));
			return text;
		}

		// Token: 0x06001251 RID: 4689
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetDeclaringType(RuntimeMethodHandleInternal method);

		// Token: 0x06001252 RID: 4690 RVA: 0x00037620 File Offset: 0x00035820
		[SecuritySafeCritical]
		internal static RuntimeType GetDeclaringType(IRuntimeMethodInfo method)
		{
			RuntimeType declaringType = RuntimeMethodHandle.GetDeclaringType(method.Value);
			GC.KeepAlive(method);
			return declaringType;
		}

		// Token: 0x06001253 RID: 4691
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetSlot(RuntimeMethodHandleInternal method);

		// Token: 0x06001254 RID: 4692 RVA: 0x00037640 File Offset: 0x00035840
		[SecurityCritical]
		internal static int GetSlot(IRuntimeMethodInfo method)
		{
			int slot = RuntimeMethodHandle.GetSlot(method.Value);
			GC.KeepAlive(method);
			return slot;
		}

		// Token: 0x06001255 RID: 4693
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetMethodDef(IRuntimeMethodInfo method);

		// Token: 0x06001256 RID: 4694
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetName(RuntimeMethodHandleInternal method);

		// Token: 0x06001257 RID: 4695 RVA: 0x00037660 File Offset: 0x00035860
		[SecurityCritical]
		internal static string GetName(IRuntimeMethodInfo method)
		{
			string name = RuntimeMethodHandle.GetName(method.Value);
			GC.KeepAlive(method);
			return name;
		}

		// Token: 0x06001258 RID: 4696
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* _GetUtf8Name(RuntimeMethodHandleInternal method);

		// Token: 0x06001259 RID: 4697 RVA: 0x00037680 File Offset: 0x00035880
		[SecurityCritical]
		internal static Utf8String GetUtf8Name(RuntimeMethodHandleInternal method)
		{
			return new Utf8String(RuntimeMethodHandle._GetUtf8Name(method));
		}

		// Token: 0x0600125A RID: 4698
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool MatchesNameHash(RuntimeMethodHandleInternal method, uint hash);

		// Token: 0x0600125B RID: 4699
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object InvokeMethod(object target, object[] arguments, Signature sig, bool constructor);

		// Token: 0x0600125C RID: 4700 RVA: 0x0003768D File Offset: 0x0003588D
		[SecurityCritical]
		internal static INVOCATION_FLAGS GetSecurityFlags(IRuntimeMethodInfo handle)
		{
			return (INVOCATION_FLAGS)RuntimeMethodHandle.GetSpecialSecurityFlags(handle);
		}

		// Token: 0x0600125D RID: 4701
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetSpecialSecurityFlags(IRuntimeMethodInfo method);

		// Token: 0x0600125E RID: 4702
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void PerformSecurityCheck(object obj, RuntimeMethodHandleInternal method, RuntimeType parent, uint invocationFlags);

		// Token: 0x0600125F RID: 4703 RVA: 0x00037695 File Offset: 0x00035895
		[SecurityCritical]
		internal static void PerformSecurityCheck(object obj, IRuntimeMethodInfo method, RuntimeType parent, uint invocationFlags)
		{
			RuntimeMethodHandle.PerformSecurityCheck(obj, method.Value, parent, invocationFlags);
			GC.KeepAlive(method);
		}

		// Token: 0x06001260 RID: 4704
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SerializationInvoke(IRuntimeMethodInfo method, object target, SerializationInfo info, ref StreamingContext context);

		// Token: 0x06001261 RID: 4705
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _IsTokenSecurityTransparent(RuntimeModule module, int metaDataToken);

		// Token: 0x06001262 RID: 4706 RVA: 0x000376AC File Offset: 0x000358AC
		[SecurityCritical]
		internal static bool IsTokenSecurityTransparent(Module module, int metaDataToken)
		{
			return RuntimeMethodHandle._IsTokenSecurityTransparent(module.ModuleHandle.GetRuntimeModule(), metaDataToken);
		}

		// Token: 0x06001263 RID: 4707
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool _IsSecurityCritical(IRuntimeMethodInfo method);

		// Token: 0x06001264 RID: 4708 RVA: 0x000376CD File Offset: 0x000358CD
		[SecuritySafeCritical]
		internal static bool IsSecurityCritical(IRuntimeMethodInfo method)
		{
			return RuntimeMethodHandle._IsSecurityCritical(method);
		}

		// Token: 0x06001265 RID: 4709
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool _IsSecuritySafeCritical(IRuntimeMethodInfo method);

		// Token: 0x06001266 RID: 4710 RVA: 0x000376D5 File Offset: 0x000358D5
		[SecuritySafeCritical]
		internal static bool IsSecuritySafeCritical(IRuntimeMethodInfo method)
		{
			return RuntimeMethodHandle._IsSecuritySafeCritical(method);
		}

		// Token: 0x06001267 RID: 4711
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool _IsSecurityTransparent(IRuntimeMethodInfo method);

		// Token: 0x06001268 RID: 4712 RVA: 0x000376DD File Offset: 0x000358DD
		[SecuritySafeCritical]
		internal static bool IsSecurityTransparent(IRuntimeMethodInfo method)
		{
			return RuntimeMethodHandle._IsSecurityTransparent(method);
		}

		// Token: 0x06001269 RID: 4713
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetMethodInstantiation(RuntimeMethodHandleInternal method, ObjectHandleOnStack types, bool fAsRuntimeTypeArray);

		// Token: 0x0600126A RID: 4714 RVA: 0x000376E8 File Offset: 0x000358E8
		[SecuritySafeCritical]
		internal static RuntimeType[] GetMethodInstantiationInternal(IRuntimeMethodInfo method)
		{
			RuntimeType[] array = null;
			RuntimeMethodHandle.GetMethodInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method).Value, JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref array), true);
			GC.KeepAlive(method);
			return array;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00037718 File Offset: 0x00035918
		[SecuritySafeCritical]
		internal static RuntimeType[] GetMethodInstantiationInternal(RuntimeMethodHandleInternal method)
		{
			RuntimeType[] array = null;
			RuntimeMethodHandle.GetMethodInstantiation(method, JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref array), true);
			return array;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00037738 File Offset: 0x00035938
		[SecuritySafeCritical]
		internal static Type[] GetMethodInstantiationPublic(IRuntimeMethodInfo method)
		{
			RuntimeType[] array = null;
			RuntimeMethodHandle.GetMethodInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method).Value, JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref array), false);
			GC.KeepAlive(method);
			return array;
		}

		// Token: 0x0600126D RID: 4717
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasMethodInstantiation(RuntimeMethodHandleInternal method);

		// Token: 0x0600126E RID: 4718 RVA: 0x00037768 File Offset: 0x00035968
		[SecuritySafeCritical]
		internal static bool HasMethodInstantiation(IRuntimeMethodInfo method)
		{
			bool flag = RuntimeMethodHandle.HasMethodInstantiation(method.Value);
			GC.KeepAlive(method);
			return flag;
		}

		// Token: 0x0600126F RID: 4719
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodHandleInternal GetStubIfNeeded(RuntimeMethodHandleInternal method, RuntimeType declaringType, RuntimeType[] methodInstantiation);

		// Token: 0x06001270 RID: 4720
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodHandleInternal GetMethodFromCanonical(RuntimeMethodHandleInternal method, RuntimeType declaringType);

		// Token: 0x06001271 RID: 4721
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericMethodDefinition(RuntimeMethodHandleInternal method);

		// Token: 0x06001272 RID: 4722 RVA: 0x00037788 File Offset: 0x00035988
		[SecuritySafeCritical]
		internal static bool IsGenericMethodDefinition(IRuntimeMethodInfo method)
		{
			bool flag = RuntimeMethodHandle.IsGenericMethodDefinition(method.Value);
			GC.KeepAlive(method);
			return flag;
		}

		// Token: 0x06001273 RID: 4723
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsTypicalMethodDefinition(IRuntimeMethodInfo method);

		// Token: 0x06001274 RID: 4724
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetTypicalMethodDefinition(IRuntimeMethodInfo method, ObjectHandleOnStack outMethod);

		// Token: 0x06001275 RID: 4725 RVA: 0x000377A8 File Offset: 0x000359A8
		[SecuritySafeCritical]
		internal static IRuntimeMethodInfo GetTypicalMethodDefinition(IRuntimeMethodInfo method)
		{
			if (!RuntimeMethodHandle.IsTypicalMethodDefinition(method))
			{
				RuntimeMethodHandle.GetTypicalMethodDefinition(method, JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref method));
			}
			return method;
		}

		// Token: 0x06001276 RID: 4726
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void StripMethodInstantiation(IRuntimeMethodInfo method, ObjectHandleOnStack outMethod);

		// Token: 0x06001277 RID: 4727 RVA: 0x000377C0 File Offset: 0x000359C0
		[SecuritySafeCritical]
		internal static IRuntimeMethodInfo StripMethodInstantiation(IRuntimeMethodInfo method)
		{
			IRuntimeMethodInfo runtimeMethodInfo = method;
			RuntimeMethodHandle.StripMethodInstantiation(method, JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref runtimeMethodInfo));
			return runtimeMethodInfo;
		}

		// Token: 0x06001278 RID: 4728
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsDynamicMethod(RuntimeMethodHandleInternal method);

		// Token: 0x06001279 RID: 4729
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void Destroy(RuntimeMethodHandleInternal method);

		// Token: 0x0600127A RID: 4730
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Resolver GetResolver(RuntimeMethodHandleInternal method);

		// Token: 0x0600127B RID: 4731
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetCallerType(StackCrawlMarkHandle stackMark, ObjectHandleOnStack retType);

		// Token: 0x0600127C RID: 4732 RVA: 0x000377E0 File Offset: 0x000359E0
		[SecuritySafeCritical]
		internal static RuntimeType GetCallerType(ref StackCrawlMark stackMark)
		{
			RuntimeType runtimeType = null;
			RuntimeMethodHandle.GetCallerType(JitHelpers.GetStackCrawlMarkHandle(ref stackMark), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x0600127D RID: 4733
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodBody GetMethodBody(IRuntimeMethodInfo method, RuntimeType declaringType);

		// Token: 0x0600127E RID: 4734
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsConstructor(RuntimeMethodHandleInternal method);

		// Token: 0x0600127F RID: 4735
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern LoaderAllocator GetLoaderAllocator(RuntimeMethodHandleInternal method);

		// Token: 0x0400066B RID: 1643
		private IRuntimeMethodInfo m_value;
	}
}
