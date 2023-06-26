using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
	/// <summary>Represents a type using an internal metadata token.</summary>
	// Token: 0x02000130 RID: 304
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct RuntimeTypeHandle : ISerializable
	{
		// Token: 0x060011BB RID: 4539 RVA: 0x00036BBC File Offset: 0x00034DBC
		internal RuntimeTypeHandle GetNativeHandle()
		{
			RuntimeType type = this.m_type;
			if (type == null)
			{
				throw new ArgumentNullException(null, Environment.GetResourceString("Arg_InvalidHandle"));
			}
			return new RuntimeTypeHandle(type);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00036BF0 File Offset: 0x00034DF0
		internal RuntimeType GetTypeChecked()
		{
			RuntimeType type = this.m_type;
			if (type == null)
			{
				throw new ArgumentNullException(null, Environment.GetResourceString("Arg_InvalidHandle"));
			}
			return type;
		}

		// Token: 0x060011BD RID: 4541
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInstanceOfType(RuntimeType type, object o);

		// Token: 0x060011BE RID: 4542 RVA: 0x00036C20 File Offset: 0x00034E20
		[SecuritySafeCritical]
		internal unsafe static Type GetTypeHelper(Type typeStart, Type[] genericArgs, IntPtr pModifiers, int cModifiers)
		{
			Type type = typeStart;
			if (genericArgs != null)
			{
				type = type.MakeGenericType(genericArgs);
			}
			if (cModifiers > 0)
			{
				int* ptr = (int*)pModifiers.ToPointer();
				for (int i = 0; i < cModifiers; i++)
				{
					if ((byte)Marshal.ReadInt32((IntPtr)((void*)ptr), i * 4) == 15)
					{
						type = type.MakePointerType();
					}
					else if ((byte)Marshal.ReadInt32((IntPtr)((void*)ptr), i * 4) == 16)
					{
						type = type.MakeByRefType();
					}
					else if ((byte)Marshal.ReadInt32((IntPtr)((void*)ptr), i * 4) == 29)
					{
						type = type.MakeArrayType();
					}
					else
					{
						type = type.MakeArrayType(Marshal.ReadInt32((IntPtr)((void*)ptr), ++i * 4));
					}
				}
			}
			return type;
		}

		/// <summary>Indicates whether a <see cref="T:System.RuntimeTypeHandle" /> structure is equal to an object.</summary>
		/// <param name="left">A <see cref="T:System.RuntimeTypeHandle" /> structure to compare to <paramref name="right" />.</param>
		/// <param name="right">An object to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="right" /> is a <see cref="T:System.RuntimeTypeHandle" /> and is equal to <paramref name="left" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011BF RID: 4543 RVA: 0x00036CC3 File Offset: 0x00034EC3
		[__DynamicallyInvokable]
		public static bool operator ==(RuntimeTypeHandle left, object right)
		{
			return left.Equals(right);
		}

		/// <summary>Indicates whether an object and a <see cref="T:System.RuntimeTypeHandle" /> structure are equal.</summary>
		/// <param name="left">An object to compare to <paramref name="right" />.</param>
		/// <param name="right">A <see cref="T:System.RuntimeTypeHandle" /> structure to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is a <see cref="T:System.RuntimeTypeHandle" /> structure and is equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011C0 RID: 4544 RVA: 0x00036CCD File Offset: 0x00034ECD
		[__DynamicallyInvokable]
		public static bool operator ==(object left, RuntimeTypeHandle right)
		{
			return right.Equals(left);
		}

		/// <summary>Indicates whether a <see cref="T:System.RuntimeTypeHandle" /> structure is not equal to an object.</summary>
		/// <param name="left">A <see cref="T:System.RuntimeTypeHandle" /> structure to compare to <paramref name="right" />.</param>
		/// <param name="right">An object to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="right" /> is a <see cref="T:System.RuntimeTypeHandle" /> structure and is not equal to <paramref name="left" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011C1 RID: 4545 RVA: 0x00036CD7 File Offset: 0x00034ED7
		[__DynamicallyInvokable]
		public static bool operator !=(RuntimeTypeHandle left, object right)
		{
			return !left.Equals(right);
		}

		/// <summary>Indicates whether an object and a <see cref="T:System.RuntimeTypeHandle" /> structure are not equal.</summary>
		/// <param name="left">An object to compare to <paramref name="right" />.</param>
		/// <param name="right">A <see cref="T:System.RuntimeTypeHandle" /> structure to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is a <see cref="T:System.RuntimeTypeHandle" /> and is not equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011C2 RID: 4546 RVA: 0x00036CE4 File Offset: 0x00034EE4
		[__DynamicallyInvokable]
		public static bool operator !=(object left, RuntimeTypeHandle right)
		{
			return !right.Equals(left);
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00036CF1 File Offset: 0x00034EF1
		internal static RuntimeTypeHandle EmptyHandle
		{
			get
			{
				return new RuntimeTypeHandle(null);
			}
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060011C4 RID: 4548 RVA: 0x00036CF9 File Offset: 0x00034EF9
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (!(this.m_type != null))
			{
				return 0;
			}
			return this.m_type.GetHashCode();
		}

		/// <summary>Indicates whether the specified object is equal to the current <see cref="T:System.RuntimeTypeHandle" /> structure.</summary>
		/// <param name="obj">An object to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.RuntimeTypeHandle" /> structure and is equal to the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011C5 RID: 4549 RVA: 0x00036D18 File Offset: 0x00034F18
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is RuntimeTypeHandle && ((RuntimeTypeHandle)obj).m_type == this.m_type;
		}

		/// <summary>Indicates whether the specified <see cref="T:System.RuntimeTypeHandle" /> structure is equal to the current <see cref="T:System.RuntimeTypeHandle" /> structure.</summary>
		/// <param name="handle">The <see cref="T:System.RuntimeTypeHandle" /> structure to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="handle" /> is equal to the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011C6 RID: 4550 RVA: 0x00036D48 File Offset: 0x00034F48
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public bool Equals(RuntimeTypeHandle handle)
		{
			return handle.m_type == this.m_type;
		}

		/// <summary>Gets a handle to the type represented by this instance.</summary>
		/// <returns>A handle to the type represented by this instance.</returns>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00036D5C File Offset: 0x00034F5C
		public IntPtr Value
		{
			[SecurityCritical]
			get
			{
				if (!(this.m_type != null))
				{
					return IntPtr.Zero;
				}
				return this.m_type.m_handle;
			}
		}

		// Token: 0x060011C8 RID: 4552
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetValueInternal(RuntimeTypeHandle handle);

		// Token: 0x060011C9 RID: 4553 RVA: 0x00036D7D File Offset: 0x00034F7D
		internal RuntimeTypeHandle(RuntimeType type)
		{
			this.m_type = type;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00036D86 File Offset: 0x00034F86
		internal bool IsNullHandle()
		{
			return this.m_type == null;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00036D94 File Offset: 0x00034F94
		[SecuritySafeCritical]
		internal static bool IsPrimitive(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return (corElementType >= CorElementType.Boolean && corElementType <= CorElementType.R8) || corElementType == CorElementType.I || corElementType == CorElementType.U;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00036DC0 File Offset: 0x00034FC0
		[SecuritySafeCritical]
		internal static bool IsByRef(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.ByRef;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00036DDC File Offset: 0x00034FDC
		[SecuritySafeCritical]
		internal static bool IsPointer(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.Ptr;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00036DF8 File Offset: 0x00034FF8
		[SecuritySafeCritical]
		internal static bool IsArray(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.Array || corElementType == CorElementType.SzArray;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00036E18 File Offset: 0x00035018
		[SecuritySafeCritical]
		internal static bool IsSzArray(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.SzArray;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00036E34 File Offset: 0x00035034
		[SecuritySafeCritical]
		internal static bool HasElementType(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.Array || corElementType == CorElementType.SzArray || corElementType == CorElementType.Ptr || corElementType == CorElementType.ByRef;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00036E60 File Offset: 0x00035060
		[SecurityCritical]
		internal static IntPtr[] CopyRuntimeTypeHandles(RuntimeTypeHandle[] inHandles, out int length)
		{
			if (inHandles == null || inHandles.Length == 0)
			{
				length = 0;
				return null;
			}
			IntPtr[] array = new IntPtr[inHandles.Length];
			for (int i = 0; i < inHandles.Length; i++)
			{
				array[i] = inHandles[i].Value;
			}
			length = array.Length;
			return array;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00036EA8 File Offset: 0x000350A8
		[SecurityCritical]
		internal static IntPtr[] CopyRuntimeTypeHandles(Type[] inHandles, out int length)
		{
			if (inHandles == null || inHandles.Length == 0)
			{
				length = 0;
				return null;
			}
			IntPtr[] array = new IntPtr[inHandles.Length];
			for (int i = 0; i < inHandles.Length; i++)
			{
				array[i] = inHandles[i].GetTypeHandleInternal().Value;
			}
			length = array.Length;
			return array;
		}

		// Token: 0x060011D3 RID: 4563
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateInstance(RuntimeType type, bool publicOnly, bool noCheck, ref bool canBeCached, ref RuntimeMethodHandleInternal ctor, ref bool bNeedSecurityCheck);

		// Token: 0x060011D4 RID: 4564
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateCaInstance(RuntimeType type, IRuntimeMethodInfo ctor);

		// Token: 0x060011D5 RID: 4565
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object Allocate(RuntimeType type);

		// Token: 0x060011D6 RID: 4566
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateInstanceForAnotherGenericParameter(RuntimeType type, RuntimeType genericParameter);

		// Token: 0x060011D7 RID: 4567 RVA: 0x00036EF1 File Offset: 0x000350F1
		internal RuntimeType GetRuntimeType()
		{
			return this.m_type;
		}

		// Token: 0x060011D8 RID: 4568
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern CorElementType GetCorElementType(RuntimeType type);

		// Token: 0x060011D9 RID: 4569
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeAssembly GetAssembly(RuntimeType type);

		// Token: 0x060011DA RID: 4570
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeModule GetModule(RuntimeType type);

		/// <summary>Gets a handle to the module that contains the type represented by the current instance.</summary>
		/// <returns>A <see cref="T:System.ModuleHandle" /> structure representing a handle to the module that contains the type represented by the current instance.</returns>
		// Token: 0x060011DB RID: 4571 RVA: 0x00036EF9 File Offset: 0x000350F9
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public ModuleHandle GetModuleHandle()
		{
			return new ModuleHandle(RuntimeTypeHandle.GetModule(this.m_type));
		}

		// Token: 0x060011DC RID: 4572
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetBaseType(RuntimeType type);

		// Token: 0x060011DD RID: 4573
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern TypeAttributes GetAttributes(RuntimeType type);

		// Token: 0x060011DE RID: 4574
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetElementType(RuntimeType type);

		// Token: 0x060011DF RID: 4575
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CompareCanonicalHandles(RuntimeType left, RuntimeType right);

		// Token: 0x060011E0 RID: 4576
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetArrayRank(RuntimeType type);

		// Token: 0x060011E1 RID: 4577
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetToken(RuntimeType type);

		// Token: 0x060011E2 RID: 4578
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodHandleInternal GetMethodAt(RuntimeType type, int slot);

		// Token: 0x060011E3 RID: 4579 RVA: 0x00036F0B File Offset: 0x0003510B
		internal static RuntimeTypeHandle.IntroducedMethodEnumerator GetIntroducedMethods(RuntimeType type)
		{
			return new RuntimeTypeHandle.IntroducedMethodEnumerator(type);
		}

		// Token: 0x060011E4 RID: 4580
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeMethodHandleInternal GetFirstIntroducedMethod(RuntimeType type);

		// Token: 0x060011E5 RID: 4581
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNextIntroducedMethod(ref RuntimeMethodHandleInternal method);

		// Token: 0x060011E6 RID: 4582
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetFields(RuntimeType type, IntPtr* result, int* count);

		// Token: 0x060011E7 RID: 4583
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type[] GetInterfaces(RuntimeType type);

		// Token: 0x060011E8 RID: 4584
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetConstraints(RuntimeTypeHandle handle, ObjectHandleOnStack types);

		// Token: 0x060011E9 RID: 4585 RVA: 0x00036F14 File Offset: 0x00035114
		[SecuritySafeCritical]
		internal Type[] GetConstraints()
		{
			Type[] array = null;
			RuntimeTypeHandle.GetConstraints(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref array));
			return array;
		}

		// Token: 0x060011EA RID: 4586
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr GetGCHandle(RuntimeTypeHandle handle, GCHandleType type);

		// Token: 0x060011EB RID: 4587 RVA: 0x00036F36 File Offset: 0x00035136
		[SecurityCritical]
		internal IntPtr GetGCHandle(GCHandleType type)
		{
			return RuntimeTypeHandle.GetGCHandle(this.GetNativeHandle(), type);
		}

		// Token: 0x060011EC RID: 4588
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetNumVirtuals(RuntimeType type);

		// Token: 0x060011ED RID: 4589
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void VerifyInterfaceIsImplemented(RuntimeTypeHandle handle, RuntimeTypeHandle interfaceHandle);

		// Token: 0x060011EE RID: 4590 RVA: 0x00036F44 File Offset: 0x00035144
		[SecuritySafeCritical]
		internal void VerifyInterfaceIsImplemented(RuntimeTypeHandle interfaceHandle)
		{
			RuntimeTypeHandle.VerifyInterfaceIsImplemented(this.GetNativeHandle(), interfaceHandle.GetNativeHandle());
		}

		// Token: 0x060011EF RID: 4591
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetInterfaceMethodImplementationSlot(RuntimeTypeHandle handle, RuntimeTypeHandle interfaceHandle, RuntimeMethodHandleInternal interfaceMethodHandle);

		// Token: 0x060011F0 RID: 4592 RVA: 0x00036F58 File Offset: 0x00035158
		[SecuritySafeCritical]
		internal int GetInterfaceMethodImplementationSlot(RuntimeTypeHandle interfaceHandle, RuntimeMethodHandleInternal interfaceMethodHandle)
		{
			return RuntimeTypeHandle.GetInterfaceMethodImplementationSlot(this.GetNativeHandle(), interfaceHandle.GetNativeHandle(), interfaceMethodHandle);
		}

		// Token: 0x060011F1 RID: 4593
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsZapped(RuntimeType type);

		// Token: 0x060011F2 RID: 4594
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsDoNotForceOrderOfConstructorsSet();

		// Token: 0x060011F3 RID: 4595
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsComObject(RuntimeType type, bool isGenericCOM);

		// Token: 0x060011F4 RID: 4596
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsContextful(RuntimeType type);

		// Token: 0x060011F5 RID: 4597
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInterface(RuntimeType type);

		// Token: 0x060011F6 RID: 4598
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool _IsVisible(RuntimeTypeHandle typeHandle);

		// Token: 0x060011F7 RID: 4599 RVA: 0x00036F6D File Offset: 0x0003516D
		[SecuritySafeCritical]
		internal static bool IsVisible(RuntimeType type)
		{
			return RuntimeTypeHandle._IsVisible(new RuntimeTypeHandle(type));
		}

		// Token: 0x060011F8 RID: 4600
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsSecurityCritical(RuntimeTypeHandle typeHandle);

		// Token: 0x060011F9 RID: 4601 RVA: 0x00036F7A File Offset: 0x0003517A
		[SecuritySafeCritical]
		internal bool IsSecurityCritical()
		{
			return RuntimeTypeHandle.IsSecurityCritical(this.GetNativeHandle());
		}

		// Token: 0x060011FA RID: 4602
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsSecuritySafeCritical(RuntimeTypeHandle typeHandle);

		// Token: 0x060011FB RID: 4603 RVA: 0x00036F87 File Offset: 0x00035187
		[SecuritySafeCritical]
		internal bool IsSecuritySafeCritical()
		{
			return RuntimeTypeHandle.IsSecuritySafeCritical(this.GetNativeHandle());
		}

		// Token: 0x060011FC RID: 4604
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsSecurityTransparent(RuntimeTypeHandle typeHandle);

		// Token: 0x060011FD RID: 4605 RVA: 0x00036F94 File Offset: 0x00035194
		[SecuritySafeCritical]
		internal bool IsSecurityTransparent()
		{
			return RuntimeTypeHandle.IsSecurityTransparent(this.GetNativeHandle());
		}

		// Token: 0x060011FE RID: 4606
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasProxyAttribute(RuntimeType type);

		// Token: 0x060011FF RID: 4607
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsValueType(RuntimeType type);

		// Token: 0x06001200 RID: 4608
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ConstructName(RuntimeTypeHandle handle, TypeNameFormatFlags formatFlags, StringHandleOnStack retString);

		// Token: 0x06001201 RID: 4609 RVA: 0x00036FA4 File Offset: 0x000351A4
		[SecuritySafeCritical]
		internal string ConstructName(TypeNameFormatFlags formatFlags)
		{
			string text = null;
			RuntimeTypeHandle.ConstructName(this.GetNativeHandle(), formatFlags, JitHelpers.GetStringHandleOnStack(ref text));
			return text;
		}

		// Token: 0x06001202 RID: 4610
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* _GetUtf8Name(RuntimeType type);

		// Token: 0x06001203 RID: 4611 RVA: 0x00036FC7 File Offset: 0x000351C7
		[SecuritySafeCritical]
		internal static Utf8String GetUtf8Name(RuntimeType type)
		{
			return new Utf8String(RuntimeTypeHandle._GetUtf8Name(type));
		}

		// Token: 0x06001204 RID: 4612
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CanCastTo(RuntimeType type, RuntimeType target);

		// Token: 0x06001205 RID: 4613
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetDeclaringType(RuntimeType type);

		// Token: 0x06001206 RID: 4614
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IRuntimeMethodInfo GetDeclaringMethod(RuntimeType type);

		// Token: 0x06001207 RID: 4615
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetDefaultConstructor(RuntimeTypeHandle handle, ObjectHandleOnStack method);

		// Token: 0x06001208 RID: 4616 RVA: 0x00036FD4 File Offset: 0x000351D4
		[SecuritySafeCritical]
		internal IRuntimeMethodInfo GetDefaultConstructor()
		{
			IRuntimeMethodInfo runtimeMethodInfo = null;
			RuntimeTypeHandle.GetDefaultConstructor(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref runtimeMethodInfo));
			return runtimeMethodInfo;
		}

		// Token: 0x06001209 RID: 4617
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, StackCrawlMarkHandle stackMark, IntPtr pPrivHostBinder, bool loadTypeFromPartialName, ObjectHandleOnStack type);

		// Token: 0x0600120A RID: 4618 RVA: 0x00036FF6 File Offset: 0x000351F6
		internal static RuntimeType GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark, bool loadTypeFromPartialName)
		{
			return RuntimeTypeHandle.GetTypeByName(name, throwOnError, ignoreCase, reflectionOnly, ref stackMark, IntPtr.Zero, loadTypeFromPartialName);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0003700C File Offset: 0x0003520C
		[SecuritySafeCritical]
		internal static RuntimeType GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark, IntPtr pPrivHostBinder, bool loadTypeFromPartialName)
		{
			if (name != null && name.Length != 0)
			{
				RuntimeType runtimeType = null;
				RuntimeTypeHandle.GetTypeByName(name, throwOnError, ignoreCase, reflectionOnly, JitHelpers.GetStackCrawlMarkHandle(ref stackMark), pPrivHostBinder, loadTypeFromPartialName, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
				return runtimeType;
			}
			if (throwOnError)
			{
				throw new TypeLoadException(Environment.GetResourceString("Arg_TypeLoadNullStr"));
			}
			return null;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00037057 File Offset: 0x00035257
		internal static Type GetTypeByName(string name, ref StackCrawlMark stackMark)
		{
			return RuntimeTypeHandle.GetTypeByName(name, false, false, false, ref stackMark, false);
		}

		// Token: 0x0600120D RID: 4621
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetTypeByNameUsingCARules(string name, RuntimeModule scope, ObjectHandleOnStack type);

		// Token: 0x0600120E RID: 4622 RVA: 0x00037064 File Offset: 0x00035264
		[SecuritySafeCritical]
		internal static RuntimeType GetTypeByNameUsingCARules(string name, RuntimeModule scope)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			RuntimeType runtimeType = null;
			RuntimeTypeHandle.GetTypeByNameUsingCARules(name, scope.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x0600120F RID: 4623
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void GetInstantiation(RuntimeTypeHandle type, ObjectHandleOnStack types, bool fAsRuntimeTypeArray);

		// Token: 0x06001210 RID: 4624 RVA: 0x000370A0 File Offset: 0x000352A0
		[SecuritySafeCritical]
		internal RuntimeType[] GetInstantiationInternal()
		{
			RuntimeType[] array = null;
			RuntimeTypeHandle.GetInstantiation(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref array), true);
			return array;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000370C4 File Offset: 0x000352C4
		[SecuritySafeCritical]
		internal Type[] GetInstantiationPublic()
		{
			Type[] array = null;
			RuntimeTypeHandle.GetInstantiation(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref array), false);
			return array;
		}

		// Token: 0x06001212 RID: 4626
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void Instantiate(RuntimeTypeHandle handle, IntPtr* pInst, int numGenericArgs, ObjectHandleOnStack type);

		// Token: 0x06001213 RID: 4627 RVA: 0x000370E8 File Offset: 0x000352E8
		[SecurityCritical]
		internal unsafe RuntimeType Instantiate(Type[] inst)
		{
			int num;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(inst, out num);
			IntPtr[] array2;
			IntPtr* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			RuntimeType runtimeType = null;
			RuntimeTypeHandle.Instantiate(this.GetNativeHandle(), ptr, num, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			GC.KeepAlive(inst);
			return runtimeType;
		}

		// Token: 0x06001214 RID: 4628
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakeArray(RuntimeTypeHandle handle, int rank, ObjectHandleOnStack type);

		// Token: 0x06001215 RID: 4629 RVA: 0x00037138 File Offset: 0x00035338
		[SecuritySafeCritical]
		internal RuntimeType MakeArray(int rank)
		{
			RuntimeType runtimeType = null;
			RuntimeTypeHandle.MakeArray(this.GetNativeHandle(), rank, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x06001216 RID: 4630
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakeSZArray(RuntimeTypeHandle handle, ObjectHandleOnStack type);

		// Token: 0x06001217 RID: 4631 RVA: 0x0003715C File Offset: 0x0003535C
		[SecuritySafeCritical]
		internal RuntimeType MakeSZArray()
		{
			RuntimeType runtimeType = null;
			RuntimeTypeHandle.MakeSZArray(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x06001218 RID: 4632
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakeByRef(RuntimeTypeHandle handle, ObjectHandleOnStack type);

		// Token: 0x06001219 RID: 4633 RVA: 0x00037180 File Offset: 0x00035380
		[SecuritySafeCritical]
		internal RuntimeType MakeByRef()
		{
			RuntimeType runtimeType = null;
			RuntimeTypeHandle.MakeByRef(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x0600121A RID: 4634
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakePointer(RuntimeTypeHandle handle, ObjectHandleOnStack type);

		// Token: 0x0600121B RID: 4635 RVA: 0x000371A4 File Offset: 0x000353A4
		[SecurityCritical]
		internal RuntimeType MakePointer()
		{
			RuntimeType runtimeType = null;
			RuntimeTypeHandle.MakePointer(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			return runtimeType;
		}

		// Token: 0x0600121C RID: 4636
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool IsCollectible(RuntimeTypeHandle handle);

		// Token: 0x0600121D RID: 4637
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasInstantiation(RuntimeType type);

		// Token: 0x0600121E RID: 4638 RVA: 0x000371C6 File Offset: 0x000353C6
		internal bool HasInstantiation()
		{
			return RuntimeTypeHandle.HasInstantiation(this.GetTypeChecked());
		}

		// Token: 0x0600121F RID: 4639
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetGenericTypeDefinition(RuntimeTypeHandle type, ObjectHandleOnStack retType);

		// Token: 0x06001220 RID: 4640 RVA: 0x000371D4 File Offset: 0x000353D4
		[SecuritySafeCritical]
		internal static RuntimeType GetGenericTypeDefinition(RuntimeType type)
		{
			RuntimeType runtimeType = type;
			if (RuntimeTypeHandle.HasInstantiation(runtimeType) && !RuntimeTypeHandle.IsGenericTypeDefinition(runtimeType))
			{
				RuntimeTypeHandle.GetGenericTypeDefinition(runtimeType.GetTypeHandleInternal(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			}
			return runtimeType;
		}

		// Token: 0x06001221 RID: 4641
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericTypeDefinition(RuntimeType type);

		// Token: 0x06001222 RID: 4642
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericVariable(RuntimeType type);

		// Token: 0x06001223 RID: 4643 RVA: 0x00037206 File Offset: 0x00035406
		internal bool IsGenericVariable()
		{
			return RuntimeTypeHandle.IsGenericVariable(this.GetTypeChecked());
		}

		// Token: 0x06001224 RID: 4644
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGenericVariableIndex(RuntimeType type);

		// Token: 0x06001225 RID: 4645 RVA: 0x00037214 File Offset: 0x00035414
		[SecuritySafeCritical]
		internal int GetGenericVariableIndex()
		{
			RuntimeType typeChecked = this.GetTypeChecked();
			if (!RuntimeTypeHandle.IsGenericVariable(typeChecked))
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericParameter"));
			}
			return RuntimeTypeHandle.GetGenericVariableIndex(typeChecked);
		}

		// Token: 0x06001226 RID: 4646
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ContainsGenericVariables(RuntimeType handle);

		// Token: 0x06001227 RID: 4647 RVA: 0x00037246 File Offset: 0x00035446
		[SecuritySafeCritical]
		internal bool ContainsGenericVariables()
		{
			return RuntimeTypeHandle.ContainsGenericVariables(this.GetTypeChecked());
		}

		// Token: 0x06001228 RID: 4648
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool SatisfiesConstraints(RuntimeType paramType, IntPtr* pTypeContext, int typeContextLength, IntPtr* pMethodContext, int methodContextLength, RuntimeType toType);

		// Token: 0x06001229 RID: 4649 RVA: 0x00037254 File Offset: 0x00035454
		[SecurityCritical]
		internal unsafe static bool SatisfiesConstraints(RuntimeType paramType, RuntimeType[] typeContext, RuntimeType[] methodContext, RuntimeType toType)
		{
			int num;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeContext, out num);
			int num2;
			IntPtr[] array2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodContext, out num2);
			IntPtr[] array3;
			IntPtr* ptr;
			if ((array3 = array) == null || array3.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array3[0];
			}
			IntPtr[] array4;
			IntPtr* ptr2;
			if ((array4 = array2) == null || array4.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array4[0];
			}
			bool flag = RuntimeTypeHandle.SatisfiesConstraints(paramType, ptr, num, ptr2, num2, toType);
			GC.KeepAlive(typeContext);
			GC.KeepAlive(methodContext);
			return flag;
		}

		// Token: 0x0600122A RID: 4650
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr _GetMetadataImport(RuntimeType type);

		// Token: 0x0600122B RID: 4651 RVA: 0x000372D2 File Offset: 0x000354D2
		[SecurityCritical]
		internal static MetadataImport GetMetadataImport(RuntimeType type)
		{
			return new MetadataImport(RuntimeTypeHandle._GetMetadataImport(type), type);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x000372E0 File Offset: 0x000354E0
		[SecurityCritical]
		private RuntimeTypeHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			RuntimeType runtimeType = (RuntimeType)info.GetValue("TypeObj", typeof(RuntimeType));
			this.m_type = runtimeType;
			if (this.m_type == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data necessary to deserialize the type represented by the current instance.</summary>
		/// <param name="info">The object to be populated with serialization information.</param>
		/// <param name="context">(Reserved) The location where serialized data will be stored and retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <see cref="P:System.RuntimeTypeHandle.Value" /> is invalid.</exception>
		// Token: 0x0600122D RID: 4653 RVA: 0x0003733C File Offset: 0x0003553C
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.m_type == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFieldState"));
			}
			info.AddValue("TypeObj", this.m_type, typeof(RuntimeType));
		}

		// Token: 0x0600122E RID: 4654
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsEquivalentTo(RuntimeType rtType1, RuntimeType rtType2);

		// Token: 0x0600122F RID: 4655
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsEquivalentType(RuntimeType type);

		// Token: 0x0400065F RID: 1631
		private RuntimeType m_type;

		// Token: 0x02000AF8 RID: 2808
		internal struct IntroducedMethodEnumerator
		{
			// Token: 0x06006A5A RID: 27226 RVA: 0x0016F4F7 File Offset: 0x0016D6F7
			[SecuritySafeCritical]
			internal IntroducedMethodEnumerator(RuntimeType type)
			{
				this._handle = RuntimeTypeHandle.GetFirstIntroducedMethod(type);
				this._firstCall = true;
			}

			// Token: 0x06006A5B RID: 27227 RVA: 0x0016F50C File Offset: 0x0016D70C
			[SecuritySafeCritical]
			public bool MoveNext()
			{
				if (this._firstCall)
				{
					this._firstCall = false;
				}
				else if (this._handle.Value != IntPtr.Zero)
				{
					RuntimeTypeHandle.GetNextIntroducedMethod(ref this._handle);
				}
				return !(this._handle.Value == IntPtr.Zero);
			}

			// Token: 0x170011F7 RID: 4599
			// (get) Token: 0x06006A5C RID: 27228 RVA: 0x0016F564 File Offset: 0x0016D764
			public RuntimeMethodHandleInternal Current
			{
				get
				{
					return this._handle;
				}
			}

			// Token: 0x06006A5D RID: 27229 RVA: 0x0016F56C File Offset: 0x0016D76C
			public RuntimeTypeHandle.IntroducedMethodEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x040031F1 RID: 12785
			private bool _firstCall;

			// Token: 0x040031F2 RID: 12786
			private RuntimeMethodHandleInternal _handle;
		}
	}
}
