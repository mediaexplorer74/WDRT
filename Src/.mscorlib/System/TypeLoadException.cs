﻿using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when type-loading failures occur.</summary>
	// Token: 0x0200014F RID: 335
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class TypeLoadException : SystemException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TypeLoadException" /> class.</summary>
		// Token: 0x060014D7 RID: 5335 RVA: 0x0003DC61 File Offset: 0x0003BE61
		[__DynamicallyInvokable]
		public TypeLoadException()
			: base(Environment.GetResourceString("Arg_TypeLoadException"))
		{
			base.SetErrorCode(-2146233054);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeLoadException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060014D8 RID: 5336 RVA: 0x0003DC7E File Offset: 0x0003BE7E
		[__DynamicallyInvokable]
		public TypeLoadException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233054);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeLoadException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060014D9 RID: 5337 RVA: 0x0003DC92 File Offset: 0x0003BE92
		[__DynamicallyInvokable]
		public TypeLoadException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233054);
		}

		/// <summary>Gets the error message for this exception.</summary>
		/// <returns>The error message string.</returns>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0003DCA7 File Offset: 0x0003BEA7
		[__DynamicallyInvokable]
		public override string Message
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0003DCB8 File Offset: 0x0003BEB8
		[SecurityCritical]
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this.ClassName == null && this.ResourceId == 0)
				{
					this._message = Environment.GetResourceString("Arg_TypeLoadException");
					return;
				}
				if (this.AssemblyName == null)
				{
					this.AssemblyName = Environment.GetResourceString("IO_UnknownFileName");
				}
				if (this.ClassName == null)
				{
					this.ClassName = Environment.GetResourceString("IO_UnknownFileName");
				}
				string text = null;
				TypeLoadException.GetTypeLoadExceptionMessage(this.ResourceId, JitHelpers.GetStringHandleOnStack(ref text));
				this._message = string.Format(CultureInfo.CurrentCulture, text, this.ClassName, this.AssemblyName, this.MessageArg);
			}
		}

		/// <summary>Gets the fully qualified name of the type that causes the exception.</summary>
		/// <returns>The fully qualified type name.</returns>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x0003DD58 File Offset: 0x0003BF58
		[__DynamicallyInvokable]
		public string TypeName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.ClassName == null)
				{
					return string.Empty;
				}
				return this.ClassName;
			}
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0003DD6E File Offset: 0x0003BF6E
		[SecurityCritical]
		private TypeLoadException(string className, string assemblyName, string messageArg, int resourceId)
			: base(null)
		{
			base.SetErrorCode(-2146233054);
			this.ClassName = className;
			this.AssemblyName = assemblyName;
			this.MessageArg = messageArg;
			this.ResourceId = resourceId;
			this.SetMessageField();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeLoadException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is <see langword="null" />.</exception>
		// Token: 0x060014DE RID: 5342 RVA: 0x0003DDA8 File Offset: 0x0003BFA8
		protected TypeLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.ClassName = info.GetString("TypeLoadClassName");
			this.AssemblyName = info.GetString("TypeLoadAssemblyName");
			this.MessageArg = info.GetString("TypeLoadMessageArg");
			this.ResourceId = info.GetInt32("TypeLoadResourceID");
		}

		// Token: 0x060014DF RID: 5343
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetTypeLoadExceptionMessage(int resourceId, StringHandleOnStack retString);

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the class name, method name, resource ID, and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is <see langword="null" />.</exception>
		// Token: 0x060014E0 RID: 5344 RVA: 0x0003DE10 File Offset: 0x0003C010
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("TypeLoadClassName", this.ClassName, typeof(string));
			info.AddValue("TypeLoadAssemblyName", this.AssemblyName, typeof(string));
			info.AddValue("TypeLoadMessageArg", this.MessageArg, typeof(string));
			info.AddValue("TypeLoadResourceID", this.ResourceId);
		}

		// Token: 0x040006ED RID: 1773
		private string ClassName;

		// Token: 0x040006EE RID: 1774
		private string AssemblyName;

		// Token: 0x040006EF RID: 1775
		private string MessageArg;

		// Token: 0x040006F0 RID: 1776
		internal int ResourceId;
	}
}
