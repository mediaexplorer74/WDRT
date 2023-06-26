using System;
using System.Security;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200047F RID: 1151
	[SecuritySafeCritical]
	internal class TraceLoggingDataCollector
	{
		// Token: 0x0600372B RID: 14123 RVA: 0x000D5ED8 File Offset: 0x000D40D8
		private TraceLoggingDataCollector()
		{
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000D5EE0 File Offset: 0x000D40E0
		public int BeginBufferedArray()
		{
			return DataCollector.ThreadInstance.BeginBufferedArray();
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000D5EEC File Offset: 0x000D40EC
		public void EndBufferedArray(int bookmark, int count)
		{
			DataCollector.ThreadInstance.EndBufferedArray(bookmark, count);
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000D5EFA File Offset: 0x000D40FA
		public TraceLoggingDataCollector AddGroup()
		{
			return this;
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000D5EFD File Offset: 0x000D40FD
		public unsafe void AddScalar(bool value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000D5F0D File Offset: 0x000D410D
		public unsafe void AddScalar(sbyte value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000D5F1D File Offset: 0x000D411D
		public unsafe void AddScalar(byte value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000D5F2D File Offset: 0x000D412D
		public unsafe void AddScalar(short value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000D5F3D File Offset: 0x000D413D
		public unsafe void AddScalar(ushort value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000D5F4D File Offset: 0x000D414D
		public unsafe void AddScalar(int value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000D5F5D File Offset: 0x000D415D
		public unsafe void AddScalar(uint value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000D5F6D File Offset: 0x000D416D
		public unsafe void AddScalar(long value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000D5F7D File Offset: 0x000D417D
		public unsafe void AddScalar(ulong value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000D5F8D File Offset: 0x000D418D
		public unsafe void AddScalar(IntPtr value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), IntPtr.Size);
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000D5FA1 File Offset: 0x000D41A1
		public unsafe void AddScalar(UIntPtr value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), UIntPtr.Size);
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000D5FB5 File Offset: 0x000D41B5
		public unsafe void AddScalar(float value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000D5FC5 File Offset: 0x000D41C5
		public unsafe void AddScalar(double value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x000D5FD5 File Offset: 0x000D41D5
		public unsafe void AddScalar(char value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000D5FE5 File Offset: 0x000D41E5
		public unsafe void AddScalar(Guid value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 16);
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000D5FF6 File Offset: 0x000D41F6
		public void AddBinary(string value)
		{
			DataCollector.ThreadInstance.AddBinary(value, (value == null) ? 0 : (value.Length * 2));
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000D6011 File Offset: 0x000D4211
		public void AddBinary(byte[] value)
		{
			DataCollector.ThreadInstance.AddBinary(value, (value == null) ? 0 : value.Length);
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000D6027 File Offset: 0x000D4227
		public void AddArray(bool[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000D603E File Offset: 0x000D423E
		public void AddArray(sbyte[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000D6055 File Offset: 0x000D4255
		public void AddArray(short[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000D606C File Offset: 0x000D426C
		public void AddArray(ushort[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000D6083 File Offset: 0x000D4283
		public void AddArray(int[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000D609A File Offset: 0x000D429A
		public void AddArray(uint[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000D60B1 File Offset: 0x000D42B1
		public void AddArray(long[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000D60C8 File Offset: 0x000D42C8
		public void AddArray(ulong[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000D60DF File Offset: 0x000D42DF
		public void AddArray(IntPtr[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, IntPtr.Size);
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000D60FA File Offset: 0x000D42FA
		public void AddArray(UIntPtr[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, UIntPtr.Size);
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x000D6115 File Offset: 0x000D4315
		public void AddArray(float[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x000D612C File Offset: 0x000D432C
		public void AddArray(double[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x000D6143 File Offset: 0x000D4343
		public void AddArray(char[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x000D615A File Offset: 0x000D435A
		public void AddArray(Guid[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 16);
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x000D6172 File Offset: 0x000D4372
		public void AddCustom(byte[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x04001875 RID: 6261
		internal static readonly TraceLoggingDataCollector Instance = new TraceLoggingDataCollector();
	}
}
