using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.ComponentModel
{
	/// <summary>Throws an exception for a Win32 error code.</summary>
	// Token: 0x020005BB RID: 1467
	[SuppressUnmanagedCodeSecurity]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class Win32Exception : ExternalException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Win32Exception" /> class with the last Win32 error that occurred.</summary>
		// Token: 0x060036F9 RID: 14073 RVA: 0x000EF087 File Offset: 0x000ED287
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Win32Exception()
			: this(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Win32Exception" /> class with the specified error.</summary>
		/// <param name="error">The Win32 error code associated with this exception.</param>
		// Token: 0x060036FA RID: 14074 RVA: 0x000EF094 File Offset: 0x000ED294
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Win32Exception(int error)
			: this(error, Win32Exception.GetErrorMessage(error))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Win32Exception" /> class with the specified error and the specified detailed description.</summary>
		/// <param name="error">The Win32 error code associated with this exception.</param>
		/// <param name="message">A detailed description of the error.</param>
		// Token: 0x060036FB RID: 14075 RVA: 0x000EF0A3 File Offset: 0x000ED2A3
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Win32Exception(int error, string message)
			: base(message)
		{
			this.nativeErrorCode = error;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Win32Exception" /> class with the specified detailed description.</summary>
		/// <param name="message">A detailed description of the error.</param>
		// Token: 0x060036FC RID: 14076 RVA: 0x000EF0B3 File Offset: 0x000ED2B3
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Win32Exception(string message)
			: this(Marshal.GetLastWin32Error(), message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Win32Exception" /> class with the specified detailed description and the specified exception.</summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x060036FD RID: 14077 RVA: 0x000EF0C1 File Offset: 0x000ED2C1
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Win32Exception(string message, Exception innerException)
			: base(message, innerException)
		{
			this.nativeErrorCode = Marshal.GetLastWin32Error();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Win32Exception" /> class with the specified context and the serialization information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> associated with this exception.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the context of this exception.</param>
		// Token: 0x060036FE RID: 14078 RVA: 0x000EF0D6 File Offset: 0x000ED2D6
		protected Win32Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IntSecurity.UnmanagedCode.Demand();
			this.nativeErrorCode = info.GetInt32("NativeErrorCode");
		}

		/// <summary>Gets the Win32 error code associated with this exception.</summary>
		/// <returns>The Win32 error code associated with this exception.</returns>
		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x000EF0FB File Offset: 0x000ED2FB
		public int NativeErrorCode
		{
			get
			{
				return this.nativeErrorCode;
			}
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000EF104 File Offset: 0x000ED304
		private static bool TryGetErrorMessage(int error, StringBuilder sb, out string errorMsg)
		{
			errorMsg = "";
			int num = SafeNativeMethods.FormatMessage(12800, IntPtr.Zero, (uint)error, 0, sb, sb.Capacity + 1, null);
			if (num != 0)
			{
				int i;
				for (i = sb.Length; i > 0; i--)
				{
					char c = sb[i - 1];
					if (c > ' ' && c != '.')
					{
						break;
					}
				}
				errorMsg = sb.ToString(0, i);
			}
			else
			{
				if (Marshal.GetLastWin32Error() == 122)
				{
					return false;
				}
				errorMsg = "Unknown error (0x" + Convert.ToString(error, 16) + ")";
			}
			return true;
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000EF190 File Offset: 0x000ED390
		private static string GetErrorMessage(int error)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			string text;
			while (!Win32Exception.TryGetErrorMessage(error, stringBuilder, out text))
			{
				stringBuilder.Capacity *= 4;
				if (stringBuilder.Capacity >= 66560)
				{
					return "Unknown error (0x" + Convert.ToString(error, 16) + ")";
				}
			}
			return text;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and line number at which this <see cref="T:System.ComponentModel.Win32Exception" /> occurred.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06003702 RID: 14082 RVA: 0x000EF1E7 File Offset: 0x000ED3E7
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("NativeErrorCode", this.nativeErrorCode);
			base.GetObjectData(info, context);
		}

		// Token: 0x04002AA7 RID: 10919
		private readonly int nativeErrorCode;

		// Token: 0x04002AA8 RID: 10920
		private const int MaxAllowedBufferSize = 66560;
	}
}
