using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown as a wrapper around the exception thrown by the class initializer. This class cannot be inherited.</summary>
	// Token: 0x0200014E RID: 334
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class TypeInitializationException : SystemException
	{
		// Token: 0x060014D1 RID: 5329 RVA: 0x0003DBAB File Offset: 0x0003BDAB
		private TypeInitializationException()
			: base(Environment.GetResourceString("TypeInitialization_Default"))
		{
			base.SetErrorCode(-2146233036);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0003DBC8 File Offset: 0x0003BDC8
		private TypeInitializationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233036);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeInitializationException" /> class with the default error message, the specified type name, and a reference to the inner exception that is the root cause of this exception.</summary>
		/// <param name="fullTypeName">The fully qualified name of the type that fails to initialize.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060014D3 RID: 5331 RVA: 0x0003DBDC File Offset: 0x0003BDDC
		[__DynamicallyInvokable]
		public TypeInitializationException(string fullTypeName, Exception innerException)
			: base(Environment.GetResourceString("TypeInitialization_Type", new object[] { fullTypeName }), innerException)
		{
			this._typeName = fullTypeName;
			base.SetErrorCode(-2146233036);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0003DC0B File Offset: 0x0003BE0B
		internal TypeInitializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._typeName = info.GetString("TypeName");
		}

		/// <summary>Gets the fully qualified name of the type that fails to initialize.</summary>
		/// <returns>The fully qualified name of the type that fails to initialize.</returns>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x0003DC26 File Offset: 0x0003BE26
		[__DynamicallyInvokable]
		public string TypeName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._typeName == null)
				{
					return string.Empty;
				}
				return this._typeName;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the type name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x060014D6 RID: 5334 RVA: 0x0003DC3C File Offset: 0x0003BE3C
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("TypeName", this.TypeName, typeof(string));
		}

		// Token: 0x040006EC RID: 1772
		private string _typeName;
	}
}
