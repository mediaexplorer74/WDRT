using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	/// <summary>Provides a wrapper class for pointers.</summary>
	// Token: 0x02000617 RID: 1559
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class Pointer : ISerializable
	{
		// Token: 0x06004864 RID: 18532 RVA: 0x001083F9 File Offset: 0x001065F9
		private Pointer()
		{
		}

		// Token: 0x06004865 RID: 18533 RVA: 0x00108404 File Offset: 0x00106604
		[SecurityCritical]
		private Pointer(SerializationInfo info, StreamingContext context)
		{
			this._ptr = ((IntPtr)info.GetValue("_ptr", typeof(IntPtr))).ToPointer();
			this._ptrType = (RuntimeType)info.GetValue("_ptrType", typeof(RuntimeType));
		}

		/// <summary>Boxes the supplied unmanaged memory pointer and the type associated with that pointer into a managed <see cref="T:System.Reflection.Pointer" /> wrapper object. The value and the type are saved so they can be accessed from the native code during an invocation.</summary>
		/// <param name="ptr">The supplied unmanaged memory pointer.</param>
		/// <param name="type">The type associated with the <paramref name="ptr" /> parameter.</param>
		/// <returns>A pointer object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a pointer.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06004866 RID: 18534 RVA: 0x00108460 File Offset: 0x00106660
		[SecurityCritical]
		public unsafe static object Box(void* ptr, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsPointer)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBePointer"), "ptr");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBePointer"), "ptr");
			}
			return new Pointer
			{
				_ptr = ptr,
				_ptrType = runtimeType
			};
		}

		/// <summary>Returns the stored pointer.</summary>
		/// <param name="ptr">The stored pointer.</param>
		/// <returns>This method returns void.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is not a pointer.</exception>
		// Token: 0x06004867 RID: 18535 RVA: 0x001084D8 File Offset: 0x001066D8
		[SecurityCritical]
		public unsafe static void* Unbox(object ptr)
		{
			if (!(ptr is Pointer))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBePointer"), "ptr");
			}
			return ((Pointer)ptr)._ptr;
		}

		// Token: 0x06004868 RID: 18536 RVA: 0x00108502 File Offset: 0x00106702
		internal RuntimeType GetPointerType()
		{
			return this._ptrType;
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x0010850A File Offset: 0x0010670A
		[SecurityCritical]
		internal object GetPointerValue()
		{
			return (IntPtr)this._ptr;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name, fusion log, and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x0600486A RID: 18538 RVA: 0x0010851C File Offset: 0x0010671C
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_ptr", new IntPtr(this._ptr));
			info.AddValue("_ptrType", this._ptrType);
		}

		// Token: 0x04001E08 RID: 7688
		[SecurityCritical]
		private unsafe void* _ptr;

		// Token: 0x04001E09 RID: 7689
		private RuntimeType _ptrType;
	}
}
