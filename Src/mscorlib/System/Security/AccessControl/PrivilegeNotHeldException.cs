using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Security.AccessControl
{
	/// <summary>The exception that is thrown when a method in the <see cref="N:System.Security.AccessControl" /> namespace attempts to enable a privilege that it does not have.</summary>
	// Token: 0x0200022B RID: 555
	[Serializable]
	public sealed class PrivilegeNotHeldException : UnauthorizedAccessException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> class.</summary>
		// Token: 0x06002011 RID: 8209 RVA: 0x00070EDE File Offset: 0x0006F0DE
		public PrivilegeNotHeldException()
			: base(Environment.GetResourceString("PrivilegeNotHeld_Default"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> class by using the specified privilege.</summary>
		/// <param name="privilege">The privilege that is not enabled.</param>
		// Token: 0x06002012 RID: 8210 RVA: 0x00070EF0 File Offset: 0x0006F0F0
		public PrivilegeNotHeldException(string privilege)
			: base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("PrivilegeNotHeld_Named"), privilege))
		{
			this._privilegeName = privilege;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> class by using the specified exception.</summary>
		/// <param name="privilege">The privilege that is not enabled.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the innerException parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06002013 RID: 8211 RVA: 0x00070F14 File Offset: 0x0006F114
		public PrivilegeNotHeldException(string privilege, Exception inner)
			: base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("PrivilegeNotHeld_Named"), privilege), inner)
		{
			this._privilegeName = privilege;
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x00070F39 File Offset: 0x0006F139
		internal PrivilegeNotHeldException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._privilegeName = info.GetString("PrivilegeName");
		}

		/// <summary>Gets the name of the privilege that is not enabled.</summary>
		/// <returns>The name of the privilege that the method failed to enable.</returns>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x00070F54 File Offset: 0x0006F154
		public string PrivilegeName
		{
			get
			{
				return this._privilegeName;
			}
		}

		/// <summary>Sets the <paramref name="info" /> parameter with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06002016 RID: 8214 RVA: 0x00070F5C File Offset: 0x0006F15C
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("PrivilegeName", this._privilegeName, typeof(string));
		}

		// Token: 0x04000B8F RID: 2959
		private readonly string _privilegeName;
	}
}
