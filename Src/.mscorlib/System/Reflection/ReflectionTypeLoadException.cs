using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	/// <summary>The exception that is thrown by the <see cref="M:System.Reflection.Module.GetTypes" /> method if any of the classes in a module cannot be loaded. This class cannot be inherited.</summary>
	// Token: 0x0200061C RID: 1564
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class ReflectionTypeLoadException : SystemException, ISerializable
	{
		// Token: 0x060048B9 RID: 18617 RVA: 0x00108CA3 File Offset: 0x00106EA3
		private ReflectionTypeLoadException()
			: base(Environment.GetResourceString("ReflectionTypeLoad_LoadFailed"))
		{
			base.SetErrorCode(-2146232830);
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x00108CC0 File Offset: 0x00106EC0
		private ReflectionTypeLoadException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232830);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ReflectionTypeLoadException" /> class with the given classes and their associated exceptions.</summary>
		/// <param name="classes">An array of type <see langword="Type" /> containing the classes that were defined in the module and loaded. This array can contain null reference (<see langword="Nothing" /> in Visual Basic) values.</param>
		/// <param name="exceptions">An array of type <see langword="Exception" /> containing the exceptions that were thrown by the class loader. The null reference (<see langword="Nothing" /> in Visual Basic) values in the <paramref name="classes" /> array line up with the exceptions in this <paramref name="exceptions" /> array.</param>
		// Token: 0x060048BB RID: 18619 RVA: 0x00108CD4 File Offset: 0x00106ED4
		[__DynamicallyInvokable]
		public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions)
			: base(null)
		{
			this._classes = classes;
			this._exceptions = exceptions;
			base.SetErrorCode(-2146232830);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ReflectionTypeLoadException" /> class with the given classes, their associated exceptions, and exception descriptions.</summary>
		/// <param name="classes">An array of type <see langword="Type" /> containing the classes that were defined in the module and loaded. This array can contain null reference (<see langword="Nothing" /> in Visual Basic) values.</param>
		/// <param name="exceptions">An array of type <see langword="Exception" /> containing the exceptions that were thrown by the class loader. The null reference (<see langword="Nothing" /> in Visual Basic) values in the <paramref name="classes" /> array line up with the exceptions in this <paramref name="exceptions" /> array.</param>
		/// <param name="message">A <see langword="String" /> describing the reason the exception was thrown.</param>
		// Token: 0x060048BC RID: 18620 RVA: 0x00108CF6 File Offset: 0x00106EF6
		[__DynamicallyInvokable]
		public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions, string message)
			: base(message)
		{
			this._classes = classes;
			this._exceptions = exceptions;
			base.SetErrorCode(-2146232830);
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x00108D18 File Offset: 0x00106F18
		internal ReflectionTypeLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._classes = (Type[])info.GetValue("Types", typeof(Type[]));
			this._exceptions = (Exception[])info.GetValue("Exceptions", typeof(Exception[]));
		}

		/// <summary>Gets the array of classes that were defined in the module and loaded.</summary>
		/// <returns>An array of type <see langword="Type" /> containing the classes that were defined in the module and loaded. This array can contain some <see langword="null" /> values.</returns>
		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060048BE RID: 18622 RVA: 0x00108D6D File Offset: 0x00106F6D
		[__DynamicallyInvokable]
		public Type[] Types
		{
			[__DynamicallyInvokable]
			get
			{
				return this._classes;
			}
		}

		/// <summary>Gets the array of exceptions thrown by the class loader.</summary>
		/// <returns>An array of type <see langword="Exception" /> containing the exceptions thrown by the class loader. The null values in the <see cref="P:System.Reflection.ReflectionTypeLoadException.Types" /> array of this instance line up with the exceptions in this array.</returns>
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060048BF RID: 18623 RVA: 0x00108D75 File Offset: 0x00106F75
		[__DynamicallyInvokable]
		public Exception[] LoaderExceptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this._exceptions;
			}
		}

		/// <summary>Provides an <see cref="T:System.Runtime.Serialization.ISerializable" /> implementation for serialized objects.</summary>
		/// <param name="info">The information and data needed to serialize or deserialize an object.</param>
		/// <param name="context">The context for the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see langword="info" /> is <see langword="null" />.</exception>
		// Token: 0x060048C0 RID: 18624 RVA: 0x00108D80 File Offset: 0x00106F80
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("Types", this._classes, typeof(Type[]));
			info.AddValue("Exceptions", this._exceptions, typeof(Exception[]));
		}

		// Token: 0x04001E1F RID: 7711
		private Type[] _classes;

		// Token: 0x04001E20 RID: 7712
		private Exception[] _exceptions;
	}
}
