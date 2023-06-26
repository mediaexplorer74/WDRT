using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x020003F5 RID: 1013
	[Serializable]
	internal class StackFrameHelper : IDisposable
	{
		// Token: 0x06003374 RID: 13172 RVA: 0x000C6B98 File Offset: 0x000C4D98
		public StackFrameHelper(Thread target)
		{
			this.targetThread = target;
			this.rgMethodBase = null;
			this.rgMethodHandle = null;
			this.rgiMethodToken = null;
			this.rgiOffset = null;
			this.rgiILOffset = null;
			this.rgAssemblyPath = null;
			this.rgLoadedPeAddress = null;
			this.rgiLoadedPeSize = null;
			this.rgInMemoryPdbAddress = null;
			this.rgiInMemoryPdbSize = null;
			this.dynamicMethods = null;
			this.rgFilename = null;
			this.rgiLineNumber = null;
			this.rgiColumnNumber = null;
			this.rgiLastFrameFromForeignExceptionStackTrace = null;
			this.iFrameCount = 0;
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000C6C24 File Offset: 0x000C4E24
		[SecuritySafeCritical]
		internal void InitializeSourceInfo(int iSkip, bool fNeedFileInfo, Exception exception)
		{
			StackTrace.GetStackFramesInternal(this, iSkip, fNeedFileInfo, exception);
			if (!fNeedFileInfo)
			{
				return;
			}
			if (!RuntimeFeature.IsSupported("PortablePdb"))
			{
				return;
			}
			if (StackFrameHelper.t_reentrancy > 0)
			{
				return;
			}
			StackFrameHelper.t_reentrancy++;
			try
			{
				if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
				{
					new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				}
				if (StackFrameHelper.s_getSourceLineInfo == null)
				{
					Type type = Type.GetType("System.Diagnostics.StackTraceSymbols, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false);
					if (type == null)
					{
						return;
					}
					MethodInfo methodInfo = type.GetMethod("GetSourceLineInfoWithoutCasAssert", new Type[]
					{
						typeof(string),
						typeof(IntPtr),
						typeof(int),
						typeof(IntPtr),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(string).MakeByRefType(),
						typeof(int).MakeByRefType(),
						typeof(int).MakeByRefType()
					});
					if (methodInfo == null)
					{
						methodInfo = type.GetMethod("GetSourceLineInfo", new Type[]
						{
							typeof(string),
							typeof(IntPtr),
							typeof(int),
							typeof(IntPtr),
							typeof(int),
							typeof(int),
							typeof(int),
							typeof(string).MakeByRefType(),
							typeof(int).MakeByRefType(),
							typeof(int).MakeByRefType()
						});
					}
					if (methodInfo == null)
					{
						return;
					}
					object obj = Activator.CreateInstance(type);
					StackFrameHelper.GetSourceLineInfoDelegate getSourceLineInfoDelegate = (StackFrameHelper.GetSourceLineInfoDelegate)methodInfo.CreateDelegate(typeof(StackFrameHelper.GetSourceLineInfoDelegate), obj);
					Interlocked.CompareExchange<StackFrameHelper.GetSourceLineInfoDelegate>(ref StackFrameHelper.s_getSourceLineInfo, getSourceLineInfoDelegate, null);
				}
				for (int i = 0; i < this.iFrameCount; i++)
				{
					if (this.rgiMethodToken[i] != 0)
					{
						StackFrameHelper.s_getSourceLineInfo(this.rgAssemblyPath[i], this.rgLoadedPeAddress[i], this.rgiLoadedPeSize[i], this.rgInMemoryPdbAddress[i], this.rgiInMemoryPdbSize[i], this.rgiMethodToken[i], this.rgiILOffset[i], out this.rgFilename[i], out this.rgiLineNumber[i], out this.rgiColumnNumber[i]);
					}
				}
			}
			catch
			{
			}
			finally
			{
				StackFrameHelper.t_reentrancy--;
			}
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000C6F0C File Offset: 0x000C510C
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000C6F10 File Offset: 0x000C5110
		[SecuritySafeCritical]
		public virtual MethodBase GetMethodBase(int i)
		{
			IntPtr intPtr = this.rgMethodHandle[i];
			if (intPtr.IsNull())
			{
				return null;
			}
			IRuntimeMethodInfo typicalMethodDefinition = RuntimeMethodHandle.GetTypicalMethodDefinition(new RuntimeMethodInfoStub(intPtr, this));
			return RuntimeType.GetMethodBase(typicalMethodDefinition);
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000C6F44 File Offset: 0x000C5144
		public virtual int GetOffset(int i)
		{
			return this.rgiOffset[i];
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000C6F4E File Offset: 0x000C514E
		public virtual int GetILOffset(int i)
		{
			return this.rgiILOffset[i];
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x000C6F58 File Offset: 0x000C5158
		public virtual string GetFilename(int i)
		{
			if (this.rgFilename != null)
			{
				return this.rgFilename[i];
			}
			return null;
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000C6F6C File Offset: 0x000C516C
		public virtual int GetLineNumber(int i)
		{
			if (this.rgiLineNumber != null)
			{
				return this.rgiLineNumber[i];
			}
			return 0;
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000C6F80 File Offset: 0x000C5180
		public virtual int GetColumnNumber(int i)
		{
			if (this.rgiColumnNumber != null)
			{
				return this.rgiColumnNumber[i];
			}
			return 0;
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000C6F94 File Offset: 0x000C5194
		public virtual bool IsLastFrameFromForeignExceptionStackTrace(int i)
		{
			return this.rgiLastFrameFromForeignExceptionStackTrace != null && this.rgiLastFrameFromForeignExceptionStackTrace[i];
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000C6FA8 File Offset: 0x000C51A8
		public virtual int GetNumberOfFrames()
		{
			return this.iFrameCount;
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000C6FB0 File Offset: 0x000C51B0
		public virtual void SetNumberOfFrames(int i)
		{
			this.iFrameCount = i;
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000C6FBC File Offset: 0x000C51BC
		[OnSerializing]
		[SecuritySafeCritical]
		private void OnSerializing(StreamingContext context)
		{
			this.rgMethodBase = ((this.rgMethodHandle == null) ? null : new MethodBase[this.rgMethodHandle.Length]);
			if (this.rgMethodHandle != null)
			{
				for (int i = 0; i < this.rgMethodHandle.Length; i++)
				{
					if (!this.rgMethodHandle[i].IsNull())
					{
						this.rgMethodBase[i] = RuntimeType.GetMethodBase(new RuntimeMethodInfoStub(this.rgMethodHandle[i], this));
					}
				}
			}
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000C7030 File Offset: 0x000C5230
		[OnSerialized]
		private void OnSerialized(StreamingContext context)
		{
			this.rgMethodBase = null;
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x000C703C File Offset: 0x000C523C
		[OnDeserialized]
		[SecuritySafeCritical]
		private void OnDeserialized(StreamingContext context)
		{
			this.rgMethodHandle = ((this.rgMethodBase == null) ? null : new IntPtr[this.rgMethodBase.Length]);
			if (this.rgMethodBase != null)
			{
				for (int i = 0; i < this.rgMethodBase.Length; i++)
				{
					if (this.rgMethodBase[i] != null)
					{
						this.rgMethodHandle[i] = this.rgMethodBase[i].MethodHandle.Value;
					}
				}
			}
			this.rgMethodBase = null;
		}

		// Token: 0x040016D6 RID: 5846
		[NonSerialized]
		private Thread targetThread;

		// Token: 0x040016D7 RID: 5847
		private int[] rgiOffset;

		// Token: 0x040016D8 RID: 5848
		private int[] rgiILOffset;

		// Token: 0x040016D9 RID: 5849
		private MethodBase[] rgMethodBase;

		// Token: 0x040016DA RID: 5850
		private object dynamicMethods;

		// Token: 0x040016DB RID: 5851
		[NonSerialized]
		private IntPtr[] rgMethodHandle;

		// Token: 0x040016DC RID: 5852
		private string[] rgAssemblyPath;

		// Token: 0x040016DD RID: 5853
		private IntPtr[] rgLoadedPeAddress;

		// Token: 0x040016DE RID: 5854
		private int[] rgiLoadedPeSize;

		// Token: 0x040016DF RID: 5855
		private IntPtr[] rgInMemoryPdbAddress;

		// Token: 0x040016E0 RID: 5856
		private int[] rgiInMemoryPdbSize;

		// Token: 0x040016E1 RID: 5857
		private int[] rgiMethodToken;

		// Token: 0x040016E2 RID: 5858
		private string[] rgFilename;

		// Token: 0x040016E3 RID: 5859
		private int[] rgiLineNumber;

		// Token: 0x040016E4 RID: 5860
		private int[] rgiColumnNumber;

		// Token: 0x040016E5 RID: 5861
		[OptionalField]
		private bool[] rgiLastFrameFromForeignExceptionStackTrace;

		// Token: 0x040016E6 RID: 5862
		private int iFrameCount;

		// Token: 0x040016E7 RID: 5863
		private static StackFrameHelper.GetSourceLineInfoDelegate s_getSourceLineInfo;

		// Token: 0x040016E8 RID: 5864
		[ThreadStatic]
		private static int t_reentrancy;

		// Token: 0x02000B7D RID: 2941
		// (Invoke) Token: 0x06006C74 RID: 27764
		private delegate void GetSourceLineInfoDelegate(string assemblyPath, IntPtr loadedPeAddress, int loadedPeSize, IntPtr inMemoryPdbAddress, int inMemoryPdbSize, int methodToken, int ilOffset, out string sourceFile, out int sourceLine, out int sourceColumn);
	}
}
