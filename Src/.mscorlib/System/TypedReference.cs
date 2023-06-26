using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	/// <summary>Describes objects that contain both a managed pointer to a location and a runtime representation of the type that may be stored at that location.</summary>
	// Token: 0x0200014D RID: 333
	[CLSCompliant(false)]
	[ComVisible(true)]
	[NonVersionable]
	public struct TypedReference
	{
		/// <summary>Makes a <see langword="TypedReference" /> for a field identified by a specified object and list of field descriptions.</summary>
		/// <param name="target">An object that contains the field described by the first element of <paramref name="flds" />.</param>
		/// <param name="flds">A list of field descriptions where each element describes a field that contains the field described by the succeeding element. Each described field must be a value type. The field descriptions must be <see langword="RuntimeFieldInfo" /> objects supplied by the type system.</param>
		/// <returns>A <see cref="T:System.TypedReference" /> for the field described by the last element of <paramref name="flds" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> or <paramref name="flds" /> is <see langword="null" />.  
		/// -or-  
		/// An element of <paramref name="flds" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flds" /> array has no elements.  
		///  -or-  
		///  An element of <paramref name="flds" /> is not a <see langword="RuntimeFieldInfo" /> object.  
		///  -or-  
		///  The <see cref="P:System.Reflection.FieldInfo.IsInitOnly" /> or <see cref="P:System.Reflection.FieldInfo.IsStatic" /> property of an element of <paramref name="flds" /> is <see langword="true" />.</exception>
		/// <exception cref="T:System.MissingMemberException">Parameter <paramref name="target" /> does not contain the field described by the first element of <paramref name="flds" />, or an element of <paramref name="flds" /> describes a field that is not contained in the field described by the succeeding element of <paramref name="flds" />.  
		///  -or-  
		///  The field described by an element of <paramref name="flds" /> is not a value type.</exception>
		// Token: 0x060014C6 RID: 5318 RVA: 0x0003D9DC File Offset: 0x0003BBDC
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe static TypedReference MakeTypedReference(object target, FieldInfo[] flds)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (flds == null)
			{
				throw new ArgumentNullException("flds");
			}
			if (flds.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayZeroError"));
			}
			IntPtr[] array = new IntPtr[flds.Length];
			RuntimeType runtimeType = (RuntimeType)target.GetType();
			for (int i = 0; i < flds.Length; i++)
			{
				RuntimeFieldInfo runtimeFieldInfo = flds[i] as RuntimeFieldInfo;
				if (runtimeFieldInfo == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeFieldInfo"));
				}
				if (runtimeFieldInfo.IsInitOnly || runtimeFieldInfo.IsStatic)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_TypedReferenceInvalidField"));
				}
				if (runtimeType != runtimeFieldInfo.GetDeclaringTypeInternal() && !runtimeType.IsSubclassOf(runtimeFieldInfo.GetDeclaringTypeInternal()))
				{
					throw new MissingMemberException(Environment.GetResourceString("MissingMemberTypeRef"));
				}
				RuntimeType runtimeType2 = (RuntimeType)runtimeFieldInfo.FieldType;
				if (runtimeType2.IsPrimitive)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_TypeRefPrimitve"));
				}
				if (i < flds.Length - 1 && !runtimeType2.IsValueType)
				{
					throw new MissingMemberException(Environment.GetResourceString("MissingMemberNestErr"));
				}
				array[i] = runtimeFieldInfo.FieldHandle.Value;
				runtimeType = runtimeType2;
			}
			TypedReference typedReference = default(TypedReference);
			TypedReference.InternalMakeTypedReference((void*)(&typedReference), target, array, runtimeType);
			return typedReference;
		}

		// Token: 0x060014C7 RID: 5319
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void InternalMakeTypedReference(void* result, object target, IntPtr[] flds, RuntimeType lastFieldType);

		/// <summary>Returns the hash code of this object.</summary>
		/// <returns>The hash code of this object.</returns>
		// Token: 0x060014C8 RID: 5320 RVA: 0x0003DB28 File Offset: 0x0003BD28
		public override int GetHashCode()
		{
			if (this.Type == IntPtr.Zero)
			{
				return 0;
			}
			return __reftype(this).GetHashCode();
		}

		/// <summary>Checks if this object is equal to the specified object.</summary>
		/// <param name="o">The object with which to compare the current object.</param>
		/// <returns>
		///   <see langword="true" /> if this object is equal to the specified object; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented.</exception>
		// Token: 0x060014C9 RID: 5321 RVA: 0x0003DB50 File Offset: 0x0003BD50
		public override bool Equals(object o)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NYI"));
		}

		/// <summary>Converts the specified <see langword="TypedReference" /> to an <see langword="Object" />.</summary>
		/// <param name="value">The <see langword="TypedReference" /> to be converted.</param>
		/// <returns>An <see cref="T:System.Object" /> converted from a <see langword="TypedReference" />.</returns>
		// Token: 0x060014CA RID: 5322 RVA: 0x0003DB61 File Offset: 0x0003BD61
		[SecuritySafeCritical]
		public unsafe static object ToObject(TypedReference value)
		{
			return TypedReference.InternalToObject((void*)(&value));
		}

		// Token: 0x060014CB RID: 5323
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern object InternalToObject(void* value);

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x0003DB6B File Offset: 0x0003BD6B
		internal bool IsNull
		{
			get
			{
				return this.Value.IsNull() && this.Type.IsNull();
			}
		}

		/// <summary>Returns the type of the target of the specified <see langword="TypedReference" />.</summary>
		/// <param name="value">The value whose target's type is to be returned.</param>
		/// <returns>The type of the target of the specified <see langword="TypedReference" />.</returns>
		// Token: 0x060014CD RID: 5325 RVA: 0x0003DB87 File Offset: 0x0003BD87
		public static Type GetTargetType(TypedReference value)
		{
			return __reftype(value);
		}

		/// <summary>Returns the internal metadata type handle for the specified <see langword="TypedReference" />.</summary>
		/// <param name="value">The <see langword="TypedReference" /> for which the type handle is requested.</param>
		/// <returns>The internal metadata type handle for the specified <see langword="TypedReference" />.</returns>
		// Token: 0x060014CE RID: 5326 RVA: 0x0003DB91 File Offset: 0x0003BD91
		public static RuntimeTypeHandle TargetTypeToken(TypedReference value)
		{
			return __reftype(value).TypeHandle;
		}

		/// <summary>Converts the specified value to a <see langword="TypedReference" />. This method is not supported.</summary>
		/// <param name="target">The target of the conversion.</param>
		/// <param name="value">The value to be converted.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060014CF RID: 5327 RVA: 0x0003DBA0 File Offset: 0x0003BDA0
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe static void SetTypedReference(TypedReference target, object value)
		{
			TypedReference.InternalSetTypedReference((void*)(&target), value);
		}

		// Token: 0x060014D0 RID: 5328
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void InternalSetTypedReference(void* target, object value);

		// Token: 0x040006EA RID: 1770
		private IntPtr Value;

		// Token: 0x040006EB RID: 1771
		private IntPtr Type;
	}
}
