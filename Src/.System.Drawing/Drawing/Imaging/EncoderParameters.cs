﻿using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing.Imaging
{
	/// <summary>Encapsulates an array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</summary>
	// Token: 0x0200009A RID: 154
	public sealed class EncoderParameters : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameters" /> class that can contain the specified number of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</summary>
		/// <param name="count">An integer that specifies the number of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects that the <see cref="T:System.Drawing.Imaging.EncoderParameters" /> object can contain.</param>
		// Token: 0x06000945 RID: 2373 RVA: 0x00023982 File Offset: 0x00021B82
		public EncoderParameters(int count)
		{
			this.param = new EncoderParameter[count];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.EncoderParameters" /> class that can contain one <see cref="T:System.Drawing.Imaging.EncoderParameter" /> object.</summary>
		// Token: 0x06000946 RID: 2374 RVA: 0x00023996 File Offset: 0x00021B96
		public EncoderParameters()
		{
			this.param = new EncoderParameter[1];
		}

		/// <summary>Gets or sets an array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</summary>
		/// <returns>The array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects.</returns>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x000239AA File Offset: 0x00021BAA
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x000239B2 File Offset: 0x00021BB2
		public EncoderParameter[] Param
		{
			get
			{
				return this.param;
			}
			set
			{
				this.param = value;
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000239BC File Offset: 0x00021BBC
		internal IntPtr ConvertToMemory()
		{
			int num = Marshal.SizeOf(typeof(EncoderParameter));
			int num2 = this.param.Length;
			IntPtr intPtr;
			long num3;
			checked
			{
				intPtr = Marshal.AllocHGlobal(num2 * num + Marshal.SizeOf(typeof(IntPtr)));
				if (intPtr == IntPtr.Zero)
				{
					throw SafeNativeMethods.Gdip.StatusException(3);
				}
				Marshal.WriteIntPtr(intPtr, (IntPtr)num2);
				num3 = (long)intPtr + unchecked((long)Marshal.SizeOf(typeof(IntPtr)));
			}
			for (int i = 0; i < num2; i++)
			{
				Marshal.StructureToPtr(this.param[i], (IntPtr)(num3 + (long)(i * num)), false);
			}
			return intPtr;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00023A60 File Offset: 0x00021C60
		internal static EncoderParameters ConvertFromMemory(IntPtr memory)
		{
			if (memory == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			int num = Marshal.ReadIntPtr(memory).ToInt32();
			EncoderParameters encoderParameters = new EncoderParameters(num);
			int num2 = Marshal.SizeOf(typeof(EncoderParameter));
			long num3 = (long)memory + (long)Marshal.SizeOf(typeof(IntPtr));
			IntSecurity.UnmanagedCode.Assert();
			try
			{
				for (int i = 0; i < num; i++)
				{
					Guid guid = (Guid)UnsafeNativeMethods.PtrToStructure((IntPtr)((long)(i * num2) + num3), typeof(Guid));
					int num4 = Marshal.ReadInt32((IntPtr)((long)(i * num2) + num3 + 16L));
					EncoderParameterValueType encoderParameterValueType = (EncoderParameterValueType)Marshal.ReadInt32((IntPtr)((long)(i * num2) + num3 + 20L));
					IntPtr intPtr = Marshal.ReadIntPtr((IntPtr)((long)(i * num2) + num3 + 24L));
					encoderParameters.param[i] = new EncoderParameter(new Encoder(guid), num4, encoderParameterValueType, intPtr);
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return encoderParameters;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Imaging.EncoderParameters" /> object.</summary>
		// Token: 0x0600094B RID: 2379 RVA: 0x00023B7C File Offset: 0x00021D7C
		public void Dispose()
		{
			foreach (EncoderParameter encoderParameter in this.param)
			{
				if (encoderParameter != null)
				{
					encoderParameter.Dispose();
				}
			}
			this.param = null;
		}

		// Token: 0x04000883 RID: 2179
		private EncoderParameter[] param;
	}
}
