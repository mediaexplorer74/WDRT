using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001FD RID: 509
	internal abstract class SafeDeleteContext : SafeHandle
	{
		// Token: 0x06001328 RID: 4904 RVA: 0x00064A21 File Offset: 0x00062C21
		protected SafeDeleteContext()
			: base(IntPtr.Zero, true)
		{
			this._handle = default(SSPIHandle);
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x00064A3B File Offset: 0x00062C3B
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || this._handle.IsZero;
			}
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00064A52 File Offset: 0x00062C52
		public override string ToString()
		{
			return this._handle.ToString();
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00064A68 File Offset: 0x00062C68
		internal unsafe static int InitializeSecurityContext(SecurDll dll, ref SafeFreeCredentials inCredentials, ref SafeDeleteContext refContext, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer inSecBuffer, SecurityBuffer[] inSecBuffers, SecurityBuffer outSecBuffer, ref ContextFlags outFlags)
		{
			if (inCredentials == null)
			{
				throw new ArgumentNullException("inCredentials");
			}
			SecurityBufferDescriptor securityBufferDescriptor = null;
			if (inSecBuffer != null)
			{
				securityBufferDescriptor = new SecurityBufferDescriptor(1);
			}
			else if (inSecBuffers != null)
			{
				securityBufferDescriptor = new SecurityBufferDescriptor(inSecBuffers.Length);
			}
			SecurityBufferDescriptor securityBufferDescriptor2 = new SecurityBufferDescriptor(1);
			bool flag = (inFlags & ContextFlags.AllocateMemory) != ContextFlags.Zero;
			int num = -1;
			bool flag2 = true;
			if (refContext != null)
			{
				flag2 = refContext._handle.IsZero;
			}
			GCHandle[] array = null;
			GCHandle gchandle = default(GCHandle);
			SafeFreeContextBuffer safeFreeContextBuffer = null;
			try
			{
				gchandle = GCHandle.Alloc(outSecBuffer.token, GCHandleType.Pinned);
				SecurityBufferStruct[] array2 = new SecurityBufferStruct[(securityBufferDescriptor == null) ? 1 : securityBufferDescriptor.Count];
				try
				{
					SecurityBufferStruct[] array3;
					void* ptr;
					if ((array3 = array2) == null || array3.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = (void*)(&array3[0]);
					}
					if (securityBufferDescriptor != null)
					{
						securityBufferDescriptor.UnmanagedPointer = ptr;
						array = new GCHandle[securityBufferDescriptor.Count];
						for (int i = 0; i < securityBufferDescriptor.Count; i++)
						{
							SecurityBuffer securityBuffer = ((inSecBuffer != null) ? inSecBuffer : inSecBuffers[i]);
							if (securityBuffer != null)
							{
								array2[i].count = securityBuffer.size;
								array2[i].type = securityBuffer.type;
								if (securityBuffer.unmanagedToken != null)
								{
									array2[i].token = securityBuffer.unmanagedToken.DangerousGetHandle();
								}
								else if (securityBuffer.token == null || securityBuffer.token.Length == 0)
								{
									array2[i].token = IntPtr.Zero;
								}
								else
								{
									array[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
									array2[i].token = Marshal.UnsafeAddrOfPinnedArrayElement(securityBuffer.token, securityBuffer.offset);
								}
							}
						}
					}
					SecurityBufferStruct[] array4 = new SecurityBufferStruct[1];
					try
					{
						SecurityBufferStruct[] array5;
						void* ptr2;
						if ((array5 = array4) == null || array5.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = (void*)(&array5[0]);
						}
						securityBufferDescriptor2.UnmanagedPointer = ptr2;
						array4[0].count = outSecBuffer.size;
						array4[0].type = outSecBuffer.type;
						if (outSecBuffer.token == null || outSecBuffer.token.Length == 0)
						{
							array4[0].token = IntPtr.Zero;
						}
						else
						{
							array4[0].token = Marshal.UnsafeAddrOfPinnedArrayElement(outSecBuffer.token, outSecBuffer.offset);
						}
						if (flag)
						{
							safeFreeContextBuffer = SafeFreeContextBuffer.CreateEmptyHandle(dll);
						}
						if (dll == SecurDll.SECURITY)
						{
							if ((refContext == null || refContext.IsInvalid) && flag2)
							{
								refContext = new SafeDeleteContext_SECURITY();
							}
							if (targetName == null || targetName.Length == 0)
							{
								targetName = " ";
							}
							try
							{
								fixed (string text = targetName)
								{
									char* ptr3 = text;
									if (ptr3 != null)
									{
										ptr3 += RuntimeHelpers.OffsetToStringData / 2;
									}
									num = SafeDeleteContext.MustRunInitializeSecurityContext_SECURITY(ref inCredentials, flag2, (byte*)((targetName == " ") ? null : ptr3), inFlags, endianness, securityBufferDescriptor, refContext, securityBufferDescriptor2, ref outFlags, safeFreeContextBuffer);
									goto IL_2D9;
								}
							}
							finally
							{
								string text = null;
							}
							goto IL_2B6;
							IL_2D9:
							outSecBuffer.size = array4[0].count;
							outSecBuffer.type = array4[0].type;
							if (outSecBuffer.size > 0)
							{
								outSecBuffer.token = new byte[outSecBuffer.size];
								Marshal.Copy(array4[0].token, outSecBuffer.token, 0, outSecBuffer.size);
								return num;
							}
							outSecBuffer.token = null;
							return num;
						}
						IL_2B6:
						throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "SecurDll" }), "Dll");
					}
					finally
					{
						SecurityBufferStruct[] array5 = null;
					}
				}
				finally
				{
					SecurityBufferStruct[] array3 = null;
				}
			}
			finally
			{
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].IsAllocated)
						{
							array[j].Free();
						}
					}
				}
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (safeFreeContextBuffer != null)
				{
					safeFreeContextBuffer.Close();
				}
			}
			return num;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00064E80 File Offset: 0x00063080
		private unsafe static int MustRunInitializeSecurityContext_SECURITY(ref SafeFreeCredentials inCredentials, bool isContextAbsent, byte* targetName, ContextFlags inFlags, Endianness endianness, SecurityBufferDescriptor inputBuffer, SafeDeleteContext outContext, SecurityBufferDescriptor outputBuffer, ref ContextFlags attributes, SafeFreeContextBuffer handleTemplate)
		{
			int num = -2146893055;
			bool flag = false;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				inCredentials.DangerousAddRef(ref flag);
				outContext.DangerousAddRef(ref flag2);
			}
			catch (Exception ex)
			{
				if (flag)
				{
					inCredentials.DangerousRelease();
					flag = false;
				}
				if (flag2)
				{
					outContext.DangerousRelease();
					flag2 = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				SSPIHandle handle = inCredentials._handle;
				if (!flag)
				{
					inCredentials = null;
				}
				else if (flag && flag2)
				{
					SSPIHandle handle2 = outContext._handle;
					void* ptr = (handle2.IsZero ? null : ((void*)(&handle2)));
					isContextAbsent = ptr == null;
					long num2;
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.InitializeSecurityContextW(ref handle, ptr, targetName, inFlags, 0, endianness, inputBuffer, 0, ref outContext._handle, outputBuffer, ref attributes, out num2);
					if (outContext._EffectiveCredential != inCredentials && ((long)num & (long)((ulong)(-2147483648))) == 0L)
					{
						if (outContext._EffectiveCredential != null)
						{
							outContext._EffectiveCredential.DangerousRelease();
						}
						outContext._EffectiveCredential = inCredentials;
					}
					else
					{
						inCredentials.DangerousRelease();
					}
					outContext.DangerousRelease();
					if (handleTemplate != null)
					{
						handleTemplate.Set(((SecurityBufferStruct*)outputBuffer.UnmanagedPointer)->token);
						if (handleTemplate.IsInvalid)
						{
							handleTemplate.SetHandleAsInvalid();
						}
					}
				}
				if (isContextAbsent && ((long)num & (long)((ulong)(-2147483648))) != 0L)
				{
					outContext._handle.SetToInvalid();
				}
			}
			return num;
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00064FDC File Offset: 0x000631DC
		internal unsafe static int AcceptSecurityContext(SecurDll dll, ref SafeFreeCredentials inCredentials, ref SafeDeleteContext refContext, ContextFlags inFlags, Endianness endianness, SecurityBuffer inSecBuffer, SecurityBuffer[] inSecBuffers, SecurityBuffer outSecBuffer, ref ContextFlags outFlags)
		{
			if (inCredentials == null)
			{
				throw new ArgumentNullException("inCredentials");
			}
			SecurityBufferDescriptor securityBufferDescriptor = null;
			if (inSecBuffer != null)
			{
				securityBufferDescriptor = new SecurityBufferDescriptor(1);
			}
			else if (inSecBuffers != null)
			{
				securityBufferDescriptor = new SecurityBufferDescriptor(inSecBuffers.Length);
			}
			SecurityBufferDescriptor securityBufferDescriptor2 = new SecurityBufferDescriptor(1);
			bool flag = (inFlags & ContextFlags.AllocateMemory) != ContextFlags.Zero;
			int num = -1;
			bool flag2 = true;
			if (refContext != null)
			{
				flag2 = refContext._handle.IsZero;
			}
			GCHandle[] array = null;
			GCHandle gchandle = default(GCHandle);
			SafeFreeContextBuffer safeFreeContextBuffer = null;
			try
			{
				gchandle = GCHandle.Alloc(outSecBuffer.token, GCHandleType.Pinned);
				SecurityBufferStruct[] array2 = new SecurityBufferStruct[(securityBufferDescriptor == null) ? 1 : securityBufferDescriptor.Count];
				try
				{
					SecurityBufferStruct[] array3;
					void* ptr;
					if ((array3 = array2) == null || array3.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = (void*)(&array3[0]);
					}
					if (securityBufferDescriptor != null)
					{
						securityBufferDescriptor.UnmanagedPointer = ptr;
						array = new GCHandle[securityBufferDescriptor.Count];
						for (int i = 0; i < securityBufferDescriptor.Count; i++)
						{
							SecurityBuffer securityBuffer = ((inSecBuffer != null) ? inSecBuffer : inSecBuffers[i]);
							if (securityBuffer != null)
							{
								array2[i].count = securityBuffer.size;
								array2[i].type = securityBuffer.type;
								if (securityBuffer.unmanagedToken != null)
								{
									array2[i].token = securityBuffer.unmanagedToken.DangerousGetHandle();
								}
								else if (securityBuffer.token == null || securityBuffer.token.Length == 0)
								{
									array2[i].token = IntPtr.Zero;
								}
								else
								{
									array[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
									array2[i].token = Marshal.UnsafeAddrOfPinnedArrayElement(securityBuffer.token, securityBuffer.offset);
								}
							}
						}
					}
					SecurityBufferStruct[] array4 = new SecurityBufferStruct[1];
					try
					{
						SecurityBufferStruct[] array5;
						void* ptr2;
						if ((array5 = array4) == null || array5.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = (void*)(&array5[0]);
						}
						securityBufferDescriptor2.UnmanagedPointer = ptr2;
						array4[0].count = outSecBuffer.size;
						array4[0].type = outSecBuffer.type;
						if (outSecBuffer.token == null || outSecBuffer.token.Length == 0)
						{
							array4[0].token = IntPtr.Zero;
						}
						else
						{
							array4[0].token = Marshal.UnsafeAddrOfPinnedArrayElement(outSecBuffer.token, outSecBuffer.offset);
						}
						if (flag)
						{
							safeFreeContextBuffer = SafeFreeContextBuffer.CreateEmptyHandle(dll);
						}
						if (dll != SecurDll.SECURITY)
						{
							throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "SecurDll" }), "Dll");
						}
						if ((refContext == null || refContext.IsInvalid) && flag2)
						{
							refContext = new SafeDeleteContext_SECURITY();
						}
						num = SafeDeleteContext.MustRunAcceptSecurityContext_SECURITY(ref inCredentials, flag2, securityBufferDescriptor, inFlags, endianness, refContext, securityBufferDescriptor2, ref outFlags, safeFreeContextBuffer);
						outSecBuffer.size = array4[0].count;
						outSecBuffer.type = array4[0].type;
						if (outSecBuffer.size > 0)
						{
							outSecBuffer.token = new byte[outSecBuffer.size];
							Marshal.Copy(array4[0].token, outSecBuffer.token, 0, outSecBuffer.size);
						}
						else
						{
							outSecBuffer.token = null;
						}
					}
					finally
					{
						SecurityBufferStruct[] array5 = null;
					}
				}
				finally
				{
					SecurityBufferStruct[] array3 = null;
				}
			}
			finally
			{
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].IsAllocated)
						{
							array[j].Free();
						}
					}
				}
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (safeFreeContextBuffer != null)
				{
					safeFreeContextBuffer.Close();
				}
			}
			return num;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x000653A0 File Offset: 0x000635A0
		private unsafe static int MustRunAcceptSecurityContext_SECURITY(ref SafeFreeCredentials inCredentials, bool isContextAbsent, SecurityBufferDescriptor inputBuffer, ContextFlags inFlags, Endianness endianness, SafeDeleteContext outContext, SecurityBufferDescriptor outputBuffer, ref ContextFlags outFlags, SafeFreeContextBuffer handleTemplate)
		{
			int num = -2146893055;
			bool flag = false;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				inCredentials.DangerousAddRef(ref flag);
				outContext.DangerousAddRef(ref flag2);
			}
			catch (Exception ex)
			{
				if (flag)
				{
					inCredentials.DangerousRelease();
					flag = false;
				}
				if (flag2)
				{
					outContext.DangerousRelease();
					flag2 = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				SSPIHandle handle = inCredentials._handle;
				if (!flag)
				{
					inCredentials = null;
				}
				else if (flag && flag2)
				{
					SSPIHandle handle2 = outContext._handle;
					void* ptr = (handle2.IsZero ? null : ((void*)(&handle2)));
					isContextAbsent = ptr == null;
					long num2;
					num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.AcceptSecurityContext(ref handle, ptr, inputBuffer, inFlags, endianness, ref outContext._handle, outputBuffer, ref outFlags, out num2);
					if (outContext._EffectiveCredential != inCredentials && ((long)num & (long)((ulong)(-2147483648))) == 0L)
					{
						if (outContext._EffectiveCredential != null)
						{
							outContext._EffectiveCredential.DangerousRelease();
						}
						outContext._EffectiveCredential = inCredentials;
					}
					else
					{
						inCredentials.DangerousRelease();
					}
					outContext.DangerousRelease();
					if (handleTemplate != null)
					{
						handleTemplate.Set(((SecurityBufferStruct*)outputBuffer.UnmanagedPointer)->token);
						if (handleTemplate.IsInvalid)
						{
							handleTemplate.SetHandleAsInvalid();
						}
					}
				}
				if (isContextAbsent && ((long)num & (long)((ulong)(-2147483648))) != 0L)
				{
					outContext._handle.SetToInvalid();
				}
			}
			return num;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x000654F8 File Offset: 0x000636F8
		internal unsafe static int CompleteAuthToken(SecurDll dll, ref SafeDeleteContext refContext, SecurityBuffer[] inSecBuffers)
		{
			SecurityBufferDescriptor securityBufferDescriptor = new SecurityBufferDescriptor(inSecBuffers.Length);
			int num = -2146893055;
			GCHandle[] array = null;
			SecurityBufferStruct[] array2 = new SecurityBufferStruct[securityBufferDescriptor.Count];
			SecurityBufferStruct[] array3;
			void* ptr;
			if ((array3 = array2) == null || array3.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = (void*)(&array3[0]);
			}
			securityBufferDescriptor.UnmanagedPointer = ptr;
			array = new GCHandle[securityBufferDescriptor.Count];
			for (int i = 0; i < securityBufferDescriptor.Count; i++)
			{
				SecurityBuffer securityBuffer = inSecBuffers[i];
				if (securityBuffer != null)
				{
					array2[i].count = securityBuffer.size;
					array2[i].type = securityBuffer.type;
					if (securityBuffer.unmanagedToken != null)
					{
						array2[i].token = securityBuffer.unmanagedToken.DangerousGetHandle();
					}
					else if (securityBuffer.token == null || securityBuffer.token.Length == 0)
					{
						array2[i].token = IntPtr.Zero;
					}
					else
					{
						array[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
						array2[i].token = Marshal.UnsafeAddrOfPinnedArrayElement(securityBuffer.token, securityBuffer.offset);
					}
				}
			}
			SSPIHandle sspihandle = default(SSPIHandle);
			if (refContext != null)
			{
				sspihandle = refContext._handle;
			}
			try
			{
				if (dll == SecurDll.SECURITY)
				{
					if ((refContext == null || refContext.IsInvalid) && sspihandle.IsZero)
					{
						refContext = new SafeDeleteContext_SECURITY();
					}
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						refContext.DangerousAddRef(ref flag);
						goto IL_1FF;
					}
					catch (Exception ex)
					{
						if (flag)
						{
							refContext.DangerousRelease();
							flag = false;
						}
						if (!(ex is ObjectDisposedException))
						{
							throw;
						}
						goto IL_1FF;
					}
					finally
					{
						if (flag)
						{
							num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.CompleteAuthToken(sspihandle.IsZero ? null : ((void*)(&sspihandle)), securityBufferDescriptor);
							refContext.DangerousRelease();
						}
					}
				}
				throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "SecurDll" }), "Dll");
			}
			finally
			{
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].IsAllocated)
						{
							array[j].Free();
						}
					}
				}
			}
			IL_1FF:
			array3 = null;
			return num;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00065730 File Offset: 0x00063930
		internal unsafe static int ApplyControlToken(SecurDll dll, ref SafeDeleteContext refContext, SecurityBuffer[] inSecBuffers)
		{
			SecurityBufferDescriptor securityBufferDescriptor = new SecurityBufferDescriptor(inSecBuffers.Length);
			int num = -2146893055;
			GCHandle[] array = null;
			SecurityBufferStruct[] array2 = new SecurityBufferStruct[securityBufferDescriptor.Count];
			SecurityBufferStruct[] array3;
			void* ptr;
			if ((array3 = array2) == null || array3.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = (void*)(&array3[0]);
			}
			securityBufferDescriptor.UnmanagedPointer = ptr;
			array = new GCHandle[securityBufferDescriptor.Count];
			for (int i = 0; i < securityBufferDescriptor.Count; i++)
			{
				SecurityBuffer securityBuffer = inSecBuffers[i];
				if (securityBuffer != null)
				{
					array2[i].count = securityBuffer.size;
					array2[i].type = securityBuffer.type;
					if (securityBuffer.unmanagedToken != null)
					{
						array2[i].token = securityBuffer.unmanagedToken.DangerousGetHandle();
					}
					else if (securityBuffer.token == null || securityBuffer.token.Length == 0)
					{
						array2[i].token = IntPtr.Zero;
					}
					else
					{
						array[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
						array2[i].token = Marshal.UnsafeAddrOfPinnedArrayElement(securityBuffer.token, securityBuffer.offset);
					}
				}
			}
			SSPIHandle sspihandle = default(SSPIHandle);
			if (refContext != null)
			{
				sspihandle = refContext._handle;
			}
			try
			{
				if (dll == SecurDll.SECURITY)
				{
					if ((refContext == null || refContext.IsInvalid) && sspihandle.IsZero)
					{
						refContext = new SafeDeleteContext_SECURITY();
					}
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						refContext.DangerousAddRef(ref flag);
						goto IL_1FF;
					}
					catch (Exception ex)
					{
						if (flag)
						{
							refContext.DangerousRelease();
							flag = false;
						}
						if (!(ex is ObjectDisposedException))
						{
							throw;
						}
						goto IL_1FF;
					}
					finally
					{
						if (flag)
						{
							num = UnsafeNclNativeMethods.SafeNetHandles_SECURITY.ApplyControlToken(sspihandle.IsZero ? null : ((void*)(&sspihandle)), securityBufferDescriptor);
							refContext.DangerousRelease();
						}
					}
				}
				throw new ArgumentException(SR.GetString("net_invalid_enum", new object[] { "SecurDll" }), "Dll");
			}
			finally
			{
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].IsAllocated)
						{
							array[j].Free();
						}
					}
				}
			}
			IL_1FF:
			array3 = null;
			return num;
		}

		// Token: 0x0400153E RID: 5438
		private const string dummyStr = " ";

		// Token: 0x0400153F RID: 5439
		private static readonly byte[] dummyBytes = new byte[1];

		// Token: 0x04001540 RID: 5440
		internal SSPIHandle _handle;

		// Token: 0x04001541 RID: 5441
		protected SafeFreeCredentials _EffectiveCredential;
	}
}
