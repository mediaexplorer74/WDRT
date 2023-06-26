using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the exception thrown when a component cannot be granted a license.</summary>
	// Token: 0x0200057D RID: 1405
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class LicenseException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class for the type of component that was denied a license.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component that was not granted a license.</param>
		// Token: 0x060033EC RID: 13292 RVA: 0x000E3AD0 File Offset: 0x000E1CD0
		public LicenseException(Type type)
			: this(type, null, SR.GetString("LicExceptionTypeOnly", new object[] { type.FullName }))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class for the type and the instance of the component that was denied a license.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component that was not granted a license.</param>
		/// <param name="instance">The instance of the component that was not granted a license.</param>
		// Token: 0x060033ED RID: 13293 RVA: 0x000E3AF3 File Offset: 0x000E1CF3
		public LicenseException(Type type, object instance)
			: this(type, null, SR.GetString("LicExceptionTypeAndInstance", new object[]
			{
				type.FullName,
				instance.GetType().FullName
			}))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class for the type and the instance of the component that was denied a license, along with a message to display.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component that was not granted a license.</param>
		/// <param name="instance">The instance of the component that was not granted a license.</param>
		/// <param name="message">The exception message to display.</param>
		// Token: 0x060033EE RID: 13294 RVA: 0x000E3B24 File Offset: 0x000E1D24
		public LicenseException(Type type, object instance, string message)
			: base(message)
		{
			this.type = type;
			this.instance = instance;
			base.HResult = -2146232063;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class for the type and the instance of the component that was denied a license, along with a message to display and the original exception thrown.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component that was not granted a license.</param>
		/// <param name="instance">The instance of the component that was not granted a license.</param>
		/// <param name="message">The exception message to display.</param>
		/// <param name="innerException">An <see cref="T:System.Exception" /> that represents the original exception.</param>
		// Token: 0x060033EF RID: 13295 RVA: 0x000E3B46 File Offset: 0x000E1D46
		public LicenseException(Type type, object instance, string message, Exception innerException)
			: base(message, innerException)
		{
			this.type = type;
			this.instance = instance;
			base.HResult = -2146232063;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class with the given <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x060033F0 RID: 13296 RVA: 0x000E3B6C File Offset: 0x000E1D6C
		protected LicenseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.type = (Type)info.GetValue("type", typeof(Type));
			this.instance = info.GetValue("instance", typeof(object));
		}

		/// <summary>Gets the type of the component that was not granted a license.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of component that was not granted a license.</returns>
		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x060033F1 RID: 13297 RVA: 0x000E3BBC File Offset: 0x000E1DBC
		public Type LicensedType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060033F2 RID: 13298 RVA: 0x000E3BC4 File Offset: 0x000E1DC4
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("type", this.type);
			info.AddValue("instance", this.instance);
			base.GetObjectData(info, context);
		}

		// Token: 0x040029AE RID: 10670
		private Type type;

		// Token: 0x040029AF RID: 10671
		private object instance;
	}
}
