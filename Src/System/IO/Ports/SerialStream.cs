using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Ports
{
	// Token: 0x02000415 RID: 1045
	internal sealed class SerialStream : Stream
	{
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06002713 RID: 10003 RVA: 0x000B39F4 File Offset: 0x000B1BF4
		// (remove) Token: 0x06002714 RID: 10004 RVA: 0x000B3A2C File Offset: 0x000B1C2C
		internal event SerialDataReceivedEventHandler DataReceived;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06002715 RID: 10005 RVA: 0x000B3A64 File Offset: 0x000B1C64
		// (remove) Token: 0x06002716 RID: 10006 RVA: 0x000B3A9C File Offset: 0x000B1C9C
		internal event SerialPinChangedEventHandler PinChanged;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06002717 RID: 10007 RVA: 0x000B3AD4 File Offset: 0x000B1CD4
		// (remove) Token: 0x06002718 RID: 10008 RVA: 0x000B3B0C File Offset: 0x000B1D0C
		internal event SerialErrorReceivedEventHandler ErrorReceived;

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x000B3B41 File Offset: 0x000B1D41
		public override bool CanRead
		{
			get
			{
				return this._handle != null;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x0600271A RID: 10010 RVA: 0x000B3B4C File Offset: 0x000B1D4C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x000B3B4F File Offset: 0x000B1D4F
		public override bool CanTimeout
		{
			get
			{
				return this._handle != null;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x000B3B5A File Offset: 0x000B1D5A
		public override bool CanWrite
		{
			get
			{
				return this._handle != null;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x000B3B65 File Offset: 0x000B1D65
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("NotSupported_UnseekableStream"));
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x000B3B76 File Offset: 0x000B1D76
		// (set) Token: 0x0600271F RID: 10015 RVA: 0x000B3B87 File Offset: 0x000B1D87
		public override long Position
		{
			get
			{
				throw new NotSupportedException(SR.GetString("NotSupported_UnseekableStream"));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NotSupported_UnseekableStream"));
			}
		}

		// Token: 0x170009AC RID: 2476
		// (set) Token: 0x06002720 RID: 10016 RVA: 0x000B3B98 File Offset: 0x000B1D98
		internal int BaudRate
		{
			set
			{
				if (value > 0 && (value <= this.commProp.dwMaxBaud || this.commProp.dwMaxBaud <= 0))
				{
					if ((long)value != (long)((ulong)this.dcb.BaudRate))
					{
						int baudRate = (int)this.dcb.BaudRate;
						this.dcb.BaudRate = (uint)value;
						if (!Microsoft.Win32.UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
						{
							this.dcb.BaudRate = (uint)baudRate;
							InternalResources.WinIOError();
						}
					}
					return;
				}
				if (this.commProp.dwMaxBaud == 0)
				{
					throw new ArgumentOutOfRangeException("baudRate", SR.GetString("ArgumentOutOfRange_NeedPosNum"));
				}
				throw new ArgumentOutOfRangeException("baudRate", SR.GetString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[]
				{
					0,
					this.commProp.dwMaxBaud
				}));
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002721 RID: 10017 RVA: 0x000B3C6A File Offset: 0x000B1E6A
		// (set) Token: 0x06002722 RID: 10018 RVA: 0x000B3C72 File Offset: 0x000B1E72
		public bool BreakState
		{
			get
			{
				return this.inBreak;
			}
			set
			{
				if (value)
				{
					if (!Microsoft.Win32.UnsafeNativeMethods.SetCommBreak(this._handle))
					{
						InternalResources.WinIOError();
					}
					this.inBreak = true;
					return;
				}
				if (!Microsoft.Win32.UnsafeNativeMethods.ClearCommBreak(this._handle))
				{
					InternalResources.WinIOError();
				}
				this.inBreak = false;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (set) Token: 0x06002723 RID: 10019 RVA: 0x000B3CAC File Offset: 0x000B1EAC
		internal int DataBits
		{
			set
			{
				if (value != (int)this.dcb.ByteSize)
				{
					byte byteSize = this.dcb.ByteSize;
					this.dcb.ByteSize = (byte)value;
					if (!Microsoft.Win32.UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.dcb.ByteSize = byteSize;
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009AF RID: 2479
		// (set) Token: 0x06002724 RID: 10020 RVA: 0x000B3D04 File Offset: 0x000B1F04
		internal bool DiscardNull
		{
			set
			{
				int dcbFlag = this.GetDcbFlag(11);
				if ((value && dcbFlag == 0) || (!value && dcbFlag == 1))
				{
					int num = dcbFlag;
					this.SetDcbFlag(11, value ? 1 : 0);
					if (!Microsoft.Win32.UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.SetDcbFlag(11, num);
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x000B3D5C File Offset: 0x000B1F5C
		// (set) Token: 0x06002726 RID: 10022 RVA: 0x000B3D78 File Offset: 0x000B1F78
		internal bool DtrEnable
		{
			get
			{
				int dcbFlag = this.GetDcbFlag(4);
				return dcbFlag == 1;
			}
			set
			{
				int dcbFlag = this.GetDcbFlag(4);
				this.SetDcbFlag(4, value ? 1 : 0);
				if (!Microsoft.Win32.UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
				{
					this.SetDcbFlag(4, dcbFlag);
					InternalResources.WinIOError();
				}
				if (!Microsoft.Win32.UnsafeNativeMethods.EscapeCommFunction(this._handle, value ? 5 : 6))
				{
					InternalResources.WinIOError();
				}
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (set) Token: 0x06002727 RID: 10023 RVA: 0x000B3DD4 File Offset: 0x000B1FD4
		internal Handshake Handshake
		{
			set
			{
				if (value != this.handshake)
				{
					Handshake handshake = this.handshake;
					int dcbFlag = this.GetDcbFlag(9);
					int dcbFlag2 = this.GetDcbFlag(2);
					int dcbFlag3 = this.GetDcbFlag(12);
					this.handshake = value;
					int num = ((this.handshake == Handshake.XOnXOff || this.handshake == Handshake.RequestToSendXOnXOff) ? 1 : 0);
					this.SetDcbFlag(9, num);
					this.SetDcbFlag(8, num);
					this.SetDcbFlag(2, (this.handshake == Handshake.RequestToSend || this.handshake == Handshake.RequestToSendXOnXOff) ? 1 : 0);
					if (this.handshake == Handshake.RequestToSend || this.handshake == Handshake.RequestToSendXOnXOff)
					{
						this.SetDcbFlag(12, 2);
					}
					else if (this.rtsEnable)
					{
						this.SetDcbFlag(12, 1);
					}
					else
					{
						this.SetDcbFlag(12, 0);
					}
					if (!Microsoft.Win32.UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.handshake = handshake;
						this.SetDcbFlag(9, dcbFlag);
						this.SetDcbFlag(8, dcbFlag);
						this.SetDcbFlag(2, dcbFlag2);
						this.SetDcbFlag(12, dcbFlag3);
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x000B3ED7 File Offset: 0x000B20D7
		internal bool IsOpen
		{
			get
			{
				return this._handle != null && !this.eventRunner.ShutdownLoop;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (set) Token: 0x06002729 RID: 10025 RVA: 0x000B3EF4 File Offset: 0x000B20F4
		internal Parity Parity
		{
			set
			{
				if ((byte)value != this.dcb.Parity)
				{
					byte parity = this.dcb.Parity;
					int dcbFlag = this.GetDcbFlag(1);
					byte errorChar = this.dcb.ErrorChar;
					int dcbFlag2 = this.GetDcbFlag(10);
					this.dcb.Parity = (byte)value;
					int num = ((this.dcb.Parity == 0) ? 0 : 1);
					this.SetDcbFlag(1, num);
					if (num == 1)
					{
						this.SetDcbFlag(10, (this.parityReplace != 0) ? 1 : 0);
						this.dcb.ErrorChar = this.parityReplace;
					}
					else
					{
						this.SetDcbFlag(10, 0);
						this.dcb.ErrorChar = 0;
					}
					if (!Microsoft.Win32.UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.dcb.Parity = parity;
						this.SetDcbFlag(1, dcbFlag);
						this.dcb.ErrorChar = errorChar;
						this.SetDcbFlag(10, dcbFlag2);
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (set) Token: 0x0600272A RID: 10026 RVA: 0x000B3FE8 File Offset: 0x000B21E8
		internal byte ParityReplace
		{
			set
			{
				if (value != this.parityReplace)
				{
					byte b = this.parityReplace;
					byte errorChar = this.dcb.ErrorChar;
					int dcbFlag = this.GetDcbFlag(10);
					this.parityReplace = value;
					if (this.GetDcbFlag(1) == 1)
					{
						this.SetDcbFlag(10, (this.parityReplace != 0) ? 1 : 0);
						this.dcb.ErrorChar = this.parityReplace;
					}
					else
					{
						this.SetDcbFlag(10, 0);
						this.dcb.ErrorChar = 0;
					}
					if (!Microsoft.Win32.UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.parityReplace = b;
						this.SetDcbFlag(10, dcbFlag);
						this.dcb.ErrorChar = errorChar;
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x000B40A0 File Offset: 0x000B22A0
		// (set) Token: 0x0600272C RID: 10028 RVA: 0x000B40C4 File Offset: 0x000B22C4
		public override int ReadTimeout
		{
			get
			{
				int readTotalTimeoutConstant = this.commTimeouts.ReadTotalTimeoutConstant;
				if (readTotalTimeoutConstant == -2)
				{
					return -1;
				}
				return readTotalTimeoutConstant;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("ReadTimeout", SR.GetString("ArgumentOutOfRange_Timeout"));
				}
				if (this._handle == null)
				{
					InternalResources.FileNotOpen();
				}
				int readTotalTimeoutConstant = this.commTimeouts.ReadTotalTimeoutConstant;
				int readIntervalTimeout = this.commTimeouts.ReadIntervalTimeout;
				int readTotalTimeoutMultiplier = this.commTimeouts.ReadTotalTimeoutMultiplier;
				if (value == 0)
				{
					this.commTimeouts.ReadTotalTimeoutConstant = 0;
					this.commTimeouts.ReadTotalTimeoutMultiplier = 0;
					this.commTimeouts.ReadIntervalTimeout = -1;
				}
				else if (value == -1)
				{
					this.commTimeouts.ReadTotalTimeoutConstant = -2;
					this.commTimeouts.ReadTotalTimeoutMultiplier = -1;
					this.commTimeouts.ReadIntervalTimeout = -1;
				}
				else
				{
					this.commTimeouts.ReadTotalTimeoutConstant = value;
					this.commTimeouts.ReadTotalTimeoutMultiplier = -1;
					this.commTimeouts.ReadIntervalTimeout = -1;
				}
				if (!Microsoft.Win32.UnsafeNativeMethods.SetCommTimeouts(this._handle, ref this.commTimeouts))
				{
					this.commTimeouts.ReadTotalTimeoutConstant = readTotalTimeoutConstant;
					this.commTimeouts.ReadTotalTimeoutMultiplier = readTotalTimeoutMultiplier;
					this.commTimeouts.ReadIntervalTimeout = readIntervalTimeout;
					InternalResources.WinIOError();
				}
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x000B41D4 File Offset: 0x000B23D4
		// (set) Token: 0x0600272E RID: 10030 RVA: 0x000B4204 File Offset: 0x000B2404
		internal bool RtsEnable
		{
			get
			{
				int dcbFlag = this.GetDcbFlag(12);
				if (dcbFlag == 2)
				{
					throw new InvalidOperationException(SR.GetString("CantSetRtsWithHandshaking"));
				}
				return dcbFlag == 1;
			}
			set
			{
				if (this.handshake == Handshake.RequestToSend || this.handshake == Handshake.RequestToSendXOnXOff)
				{
					throw new InvalidOperationException(SR.GetString("CantSetRtsWithHandshaking"));
				}
				if (value != this.rtsEnable)
				{
					int dcbFlag = this.GetDcbFlag(12);
					this.rtsEnable = value;
					if (value)
					{
						this.SetDcbFlag(12, 1);
					}
					else
					{
						this.SetDcbFlag(12, 0);
					}
					if (!Microsoft.Win32.UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.SetDcbFlag(12, dcbFlag);
						this.rtsEnable = !this.rtsEnable;
						InternalResources.WinIOError();
					}
					if (!Microsoft.Win32.UnsafeNativeMethods.EscapeCommFunction(this._handle, value ? 3 : 4))
					{
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (set) Token: 0x0600272F RID: 10031 RVA: 0x000B42AC File Offset: 0x000B24AC
		internal StopBits StopBits
		{
			set
			{
				byte b;
				if (value == StopBits.One)
				{
					b = 0;
				}
				else if (value == StopBits.OnePointFive)
				{
					b = 1;
				}
				else
				{
					b = 2;
				}
				if (b != this.dcb.StopBits)
				{
					byte stopBits = this.dcb.StopBits;
					this.dcb.StopBits = b;
					if (!Microsoft.Win32.UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
					{
						this.dcb.StopBits = stopBits;
						InternalResources.WinIOError();
					}
				}
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x000B4318 File Offset: 0x000B2518
		// (set) Token: 0x06002731 RID: 10033 RVA: 0x000B4338 File Offset: 0x000B2538
		public override int WriteTimeout
		{
			get
			{
				int writeTotalTimeoutConstant = this.commTimeouts.WriteTotalTimeoutConstant;
				if (writeTotalTimeoutConstant != 0)
				{
					return writeTotalTimeoutConstant;
				}
				return -1;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("WriteTimeout", SR.GetString("ArgumentOutOfRange_WriteTimeout"));
				}
				if (this._handle == null)
				{
					InternalResources.FileNotOpen();
				}
				int writeTotalTimeoutConstant = this.commTimeouts.WriteTotalTimeoutConstant;
				this.commTimeouts.WriteTotalTimeoutConstant = ((value == -1) ? 0 : value);
				if (!Microsoft.Win32.UnsafeNativeMethods.SetCommTimeouts(this._handle, ref this.commTimeouts))
				{
					this.commTimeouts.WriteTotalTimeoutConstant = writeTotalTimeoutConstant;
					InternalResources.WinIOError();
				}
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06002732 RID: 10034 RVA: 0x000B43B4 File Offset: 0x000B25B4
		internal bool CDHolding
		{
			get
			{
				int num = 0;
				if (!Microsoft.Win32.UnsafeNativeMethods.GetCommModemStatus(this._handle, ref num))
				{
					InternalResources.WinIOError();
				}
				return (128 & num) != 0;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x000B43E4 File Offset: 0x000B25E4
		internal bool CtsHolding
		{
			get
			{
				int num = 0;
				if (!Microsoft.Win32.UnsafeNativeMethods.GetCommModemStatus(this._handle, ref num))
				{
					InternalResources.WinIOError();
				}
				return (16 & num) != 0;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x000B4410 File Offset: 0x000B2610
		internal bool DsrHolding
		{
			get
			{
				int num = 0;
				if (!Microsoft.Win32.UnsafeNativeMethods.GetCommModemStatus(this._handle, ref num))
				{
					InternalResources.WinIOError();
				}
				return (32 & num) != 0;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x000B443C File Offset: 0x000B263C
		internal int BytesToRead
		{
			get
			{
				int num = 0;
				if (!Microsoft.Win32.UnsafeNativeMethods.ClearCommError(this._handle, ref num, ref this.comStat))
				{
					InternalResources.WinIOError();
				}
				return (int)this.comStat.cbInQue;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x000B4470 File Offset: 0x000B2670
		internal int BytesToWrite
		{
			get
			{
				int num = 0;
				if (!Microsoft.Win32.UnsafeNativeMethods.ClearCommError(this._handle, ref num, ref this.comStat))
				{
					InternalResources.WinIOError();
				}
				return (int)this.comStat.cbOutQue;
			}
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000B44A4 File Offset: 0x000B26A4
		internal SerialStream(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits, int readTimeout, int writeTimeout, Handshake handshake, bool dtrEnable, bool rtsEnable, bool discardNull, byte parityReplace)
		{
			int num = 1073741824;
			if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
			{
				num = 128;
				this.isAsync = false;
			}
			if (portName == null || !portName.StartsWith("COM", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(SR.GetString("Arg_InvalidSerialPort"), "portName");
			}
			SafeFileHandle safeFileHandle = Microsoft.Win32.UnsafeNativeMethods.CreateFile("\\\\.\\" + portName, -1073741824, 0, IntPtr.Zero, 3, num, IntPtr.Zero);
			if (safeFileHandle.IsInvalid)
			{
				InternalResources.WinIOError(portName);
			}
			try
			{
				int fileType = Microsoft.Win32.UnsafeNativeMethods.GetFileType(safeFileHandle);
				if (fileType != 2 && fileType != 0)
				{
					throw new ArgumentException(SR.GetString("Arg_InvalidSerialPort"), "portName");
				}
				this._handle = safeFileHandle;
				this.portName = portName;
				this.handshake = handshake;
				this.parityReplace = parityReplace;
				this.tempBuf = new byte[1];
				this.commProp = default(Microsoft.Win32.UnsafeNativeMethods.COMMPROP);
				int num2 = 0;
				if (!Microsoft.Win32.UnsafeNativeMethods.GetCommProperties(this._handle, ref this.commProp) || !Microsoft.Win32.UnsafeNativeMethods.GetCommModemStatus(this._handle, ref num2))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == 87 || lastWin32Error == 6)
					{
						throw new ArgumentException(SR.GetString("Arg_InvalidSerialPortExtended"), "portName");
					}
					InternalResources.WinIOError(lastWin32Error, string.Empty);
				}
				if (this.commProp.dwMaxBaud != 0 && baudRate > this.commProp.dwMaxBaud)
				{
					throw new ArgumentOutOfRangeException("baudRate", SR.GetString("Max_Baud", new object[] { this.commProp.dwMaxBaud }));
				}
				this.comStat = default(Microsoft.Win32.UnsafeNativeMethods.COMSTAT);
				this.dcb = default(Microsoft.Win32.UnsafeNativeMethods.DCB);
				this.InitializeDCB(baudRate, parity, dataBits, stopBits, discardNull);
				this.DtrEnable = dtrEnable;
				this.rtsEnable = this.GetDcbFlag(12) == 1;
				if (handshake != Handshake.RequestToSend && handshake != Handshake.RequestToSendXOnXOff)
				{
					this.RtsEnable = rtsEnable;
				}
				if (readTimeout == 0)
				{
					this.commTimeouts.ReadTotalTimeoutConstant = 0;
					this.commTimeouts.ReadTotalTimeoutMultiplier = 0;
					this.commTimeouts.ReadIntervalTimeout = -1;
				}
				else if (readTimeout == -1)
				{
					this.commTimeouts.ReadTotalTimeoutConstant = -2;
					this.commTimeouts.ReadTotalTimeoutMultiplier = -1;
					this.commTimeouts.ReadIntervalTimeout = -1;
				}
				else
				{
					this.commTimeouts.ReadTotalTimeoutConstant = readTimeout;
					this.commTimeouts.ReadTotalTimeoutMultiplier = -1;
					this.commTimeouts.ReadIntervalTimeout = -1;
				}
				this.commTimeouts.WriteTotalTimeoutMultiplier = 0;
				this.commTimeouts.WriteTotalTimeoutConstant = ((writeTimeout == -1) ? 0 : writeTimeout);
				if (!Microsoft.Win32.UnsafeNativeMethods.SetCommTimeouts(this._handle, ref this.commTimeouts))
				{
					InternalResources.WinIOError();
				}
				if (this.isAsync && !ThreadPool.BindHandle(this._handle))
				{
					throw new IOException(SR.GetString("IO_BindHandleFailed"));
				}
				Microsoft.Win32.UnsafeNativeMethods.SetCommMask(this._handle, 507);
				this.eventRunner = new SerialStream.EventLoopRunner(this);
				Thread thread = (LocalAppContextSwitches.DoNotCatchSerialStreamThreadExceptions ? new Thread(new ThreadStart(this.eventRunner.WaitForCommEvent)) : new Thread(new ThreadStart(this.eventRunner.SafelyWaitForCommEvent)));
				thread.IsBackground = true;
				thread.Start();
			}
			catch
			{
				safeFileHandle.Close();
				this._handle = null;
				throw;
			}
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000B47F4 File Offset: 0x000B29F4
		~SerialStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000B4824 File Offset: 0x000B2A24
		protected override void Dispose(bool disposing)
		{
			if (this._handle != null && !this._handle.IsInvalid)
			{
				try
				{
					this.eventRunner.endEventLoop = true;
					Thread.MemoryBarrier();
					bool flag = false;
					Microsoft.Win32.UnsafeNativeMethods.SetCommMask(this._handle, 0);
					if (!Microsoft.Win32.UnsafeNativeMethods.EscapeCommFunction(this._handle, 6))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if ((lastWin32Error == 5 || lastWin32Error == 22 || lastWin32Error == 1617) && !disposing)
						{
							flag = true;
						}
						else if (disposing)
						{
							InternalResources.WinIOError();
						}
					}
					if (!flag && !this._handle.IsClosed)
					{
						this.Flush();
					}
					this.eventRunner.waitCommEventWaitHandle.Set();
					if (!flag)
					{
						this.DiscardInBuffer();
						this.DiscardOutBuffer();
					}
					if (disposing && this.eventRunner != null)
					{
						this.eventRunner.eventLoopEndedSignal.WaitOne();
						this.eventRunner.eventLoopEndedSignal.Close();
						this.eventRunner.waitCommEventWaitHandle.Close();
					}
				}
				finally
				{
					if (disposing)
					{
						lock (this)
						{
							this._handle.Close();
							this._handle = null;
							goto IL_122;
						}
					}
					this._handle.Close();
					this._handle = null;
					IL_122:
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x000B4978 File Offset: 0x000B2B78
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (array.Length - offset < numBytes)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			int readTimeout = this.ReadTimeout;
			this.ReadTimeout = -1;
			IAsyncResult asyncResult;
			try
			{
				if (!this.isAsync)
				{
					asyncResult = base.BeginRead(array, offset, numBytes, userCallback, stateObject);
				}
				else
				{
					asyncResult = this.BeginReadCore(array, offset, numBytes, userCallback, stateObject);
				}
			}
			finally
			{
				this.ReadTimeout = readTimeout;
			}
			return asyncResult;
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000B4A3C File Offset: 0x000B2C3C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (this.inBreak)
			{
				throw new InvalidOperationException(SR.GetString("In_Break_State"));
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (array.Length - offset < numBytes)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			int writeTimeout = this.WriteTimeout;
			this.WriteTimeout = -1;
			IAsyncResult asyncResult;
			try
			{
				if (!this.isAsync)
				{
					asyncResult = base.BeginWrite(array, offset, numBytes, userCallback, stateObject);
				}
				else
				{
					asyncResult = this.BeginWriteCore(array, offset, numBytes, userCallback, stateObject);
				}
			}
			finally
			{
				this.WriteTimeout = writeTimeout;
			}
			return asyncResult;
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000B4B18 File Offset: 0x000B2D18
		internal void DiscardInBuffer()
		{
			if (!Microsoft.Win32.UnsafeNativeMethods.PurgeComm(this._handle, 10U))
			{
				InternalResources.WinIOError();
			}
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000B4B2E File Offset: 0x000B2D2E
		internal void DiscardOutBuffer()
		{
			if (!Microsoft.Win32.UnsafeNativeMethods.PurgeComm(this._handle, 5U))
			{
				InternalResources.WinIOError();
			}
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000B4B44 File Offset: 0x000B2D44
		public unsafe override int EndRead(IAsyncResult asyncResult)
		{
			if (!this.isAsync)
			{
				return base.EndRead(asyncResult);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = asyncResult as SerialStream.SerialStreamAsyncResult;
			if (serialStreamAsyncResult == null || serialStreamAsyncResult._isWrite)
			{
				InternalResources.WrongAsyncResult();
			}
			if (1 == Interlocked.CompareExchange(ref serialStreamAsyncResult._EndXxxCalled, 1, 0))
			{
				InternalResources.EndReadCalledTwice();
			}
			bool flag = false;
			WaitHandle waitHandle = serialStreamAsyncResult._waitHandle;
			if (waitHandle != null)
			{
				try
				{
					waitHandle.WaitOne();
					if (serialStreamAsyncResult._numBytes == 0 && this.ReadTimeout == -1 && serialStreamAsyncResult._errorCode == 0)
					{
						flag = true;
					}
				}
				finally
				{
					waitHandle.Close();
				}
			}
			NativeOverlapped* overlapped = serialStreamAsyncResult._overlapped;
			if (overlapped != null)
			{
				Overlapped.Free(overlapped);
			}
			if (serialStreamAsyncResult._errorCode != 0)
			{
				InternalResources.WinIOError(serialStreamAsyncResult._errorCode, this.portName);
			}
			if (flag)
			{
				throw new IOException(SR.GetString("IO_OperationAborted"));
			}
			return serialStreamAsyncResult._numBytes;
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000B4C28 File Offset: 0x000B2E28
		public unsafe override void EndWrite(IAsyncResult asyncResult)
		{
			if (!this.isAsync)
			{
				base.EndWrite(asyncResult);
				return;
			}
			if (this.inBreak)
			{
				throw new InvalidOperationException(SR.GetString("In_Break_State"));
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = asyncResult as SerialStream.SerialStreamAsyncResult;
			if (serialStreamAsyncResult == null || !serialStreamAsyncResult._isWrite)
			{
				InternalResources.WrongAsyncResult();
			}
			if (1 == Interlocked.CompareExchange(ref serialStreamAsyncResult._EndXxxCalled, 1, 0))
			{
				InternalResources.EndWriteCalledTwice();
			}
			WaitHandle waitHandle = serialStreamAsyncResult._waitHandle;
			if (waitHandle != null)
			{
				try
				{
					waitHandle.WaitOne();
				}
				finally
				{
					waitHandle.Close();
				}
			}
			NativeOverlapped* overlapped = serialStreamAsyncResult._overlapped;
			if (overlapped != null)
			{
				Overlapped.Free(overlapped);
			}
			if (serialStreamAsyncResult._errorCode != 0)
			{
				InternalResources.WinIOError(serialStreamAsyncResult._errorCode, this.portName);
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000B4CEC File Offset: 0x000B2EEC
		public override void Flush()
		{
			if (this._handle == null)
			{
				throw new ObjectDisposedException(SR.GetString("Port_not_open"));
			}
			Microsoft.Win32.UnsafeNativeMethods.FlushFileBuffers(this._handle);
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000B4D12 File Offset: 0x000B2F12
		public override int Read([In] [Out] byte[] array, int offset, int count)
		{
			return this.Read(array, offset, count, this.ReadTimeout);
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000B4D24 File Offset: 0x000B2F24
		internal int Read([In] [Out] byte[] array, int offset, int count, int timeout)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.GetString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			if (count == 0)
			{
				return 0;
			}
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			int num;
			if (this.isAsync)
			{
				IAsyncResult asyncResult = this.BeginReadCore(array, offset, count, null, null);
				num = this.EndRead(asyncResult);
			}
			else
			{
				int num2;
				num = this.ReadFileNative(array, offset, count, null, out num2);
				if (num == -1)
				{
					InternalResources.WinIOError();
				}
			}
			if (num == 0)
			{
				throw new TimeoutException();
			}
			return num;
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000B4DE6 File Offset: 0x000B2FE6
		public override int ReadByte()
		{
			return this.ReadByte(this.ReadTimeout);
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000B4DF4 File Offset: 0x000B2FF4
		internal int ReadByte(int timeout)
		{
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			int num;
			if (this.isAsync)
			{
				IAsyncResult asyncResult = this.BeginReadCore(this.tempBuf, 0, 1, null, null);
				num = this.EndRead(asyncResult);
			}
			else
			{
				int num2;
				num = this.ReadFileNative(this.tempBuf, 0, 1, null, out num2);
				if (num == -1)
				{
					InternalResources.WinIOError();
				}
			}
			if (num == 0)
			{
				throw new TimeoutException();
			}
			return (int)this.tempBuf[0];
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000B4E60 File Offset: 0x000B3060
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("NotSupported_UnseekableStream"));
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000B4E71 File Offset: 0x000B3071
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("NotSupported_UnseekableStream"));
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000B4E82 File Offset: 0x000B3082
		internal void SetBufferSizes(int readBufferSize, int writeBufferSize)
		{
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			if (!Microsoft.Win32.UnsafeNativeMethods.SetupComm(this._handle, readBufferSize, writeBufferSize))
			{
				InternalResources.WinIOError();
			}
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000B4EA5 File Offset: 0x000B30A5
		public override void Write(byte[] array, int offset, int count)
		{
			this.Write(array, offset, count, this.WriteTimeout);
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000B4EB8 File Offset: 0x000B30B8
		internal void Write(byte[] array, int offset, int count, int timeout)
		{
			if (this.inBreak)
			{
				throw new InvalidOperationException(SR.GetString("In_Break_State"));
			}
			if (array == null)
			{
				throw new ArgumentNullException("buffer", SR.GetString("ArgumentNull_Array"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (count == 0)
			{
				return;
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("count", SR.GetString("ArgumentOutOfRange_OffsetOut"));
			}
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			int num;
			if (this.isAsync)
			{
				IAsyncResult asyncResult = this.BeginWriteCore(array, offset, count, null, null);
				this.EndWrite(asyncResult);
				SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = asyncResult as SerialStream.SerialStreamAsyncResult;
				num = serialStreamAsyncResult._numBytes;
			}
			else
			{
				int num2;
				num = this.WriteFileNative(array, offset, count, null, out num2);
				if (num == -1)
				{
					if (num2 == 1121)
					{
						throw new TimeoutException(SR.GetString("Write_timed_out"));
					}
					InternalResources.WinIOError();
				}
			}
			if (num == 0)
			{
				throw new TimeoutException(SR.GetString("Write_timed_out"));
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000B4FC2 File Offset: 0x000B31C2
		public override void WriteByte(byte value)
		{
			this.WriteByte(value, this.WriteTimeout);
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000B4FD4 File Offset: 0x000B31D4
		internal void WriteByte(byte value, int timeout)
		{
			if (this.inBreak)
			{
				throw new InvalidOperationException(SR.GetString("In_Break_State"));
			}
			if (this._handle == null)
			{
				InternalResources.FileNotOpen();
			}
			this.tempBuf[0] = value;
			int num;
			if (this.isAsync)
			{
				IAsyncResult asyncResult = this.BeginWriteCore(this.tempBuf, 0, 1, null, null);
				this.EndWrite(asyncResult);
				SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = asyncResult as SerialStream.SerialStreamAsyncResult;
				num = serialStreamAsyncResult._numBytes;
			}
			else
			{
				int num2;
				num = this.WriteFileNative(this.tempBuf, 0, 1, null, out num2);
				if (num == -1)
				{
					if (Marshal.GetLastWin32Error() == 1121)
					{
						throw new TimeoutException(SR.GetString("Write_timed_out"));
					}
					InternalResources.WinIOError();
				}
			}
			if (num == 0)
			{
				throw new TimeoutException(SR.GetString("Write_timed_out"));
			}
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x000B508C File Offset: 0x000B328C
		private void InitializeDCB(int baudRate, Parity parity, int dataBits, StopBits stopBits, bool discardNull)
		{
			if (!Microsoft.Win32.UnsafeNativeMethods.GetCommState(this._handle, ref this.dcb))
			{
				InternalResources.WinIOError();
			}
			this.dcb.DCBlength = (uint)Marshal.SizeOf(this.dcb);
			this.dcb.BaudRate = (uint)baudRate;
			this.dcb.ByteSize = (byte)dataBits;
			switch (stopBits)
			{
			case StopBits.One:
				this.dcb.StopBits = 0;
				break;
			case StopBits.Two:
				this.dcb.StopBits = 2;
				break;
			case StopBits.OnePointFive:
				this.dcb.StopBits = 1;
				break;
			}
			this.dcb.Parity = (byte)parity;
			this.SetDcbFlag(1, (parity == Parity.None) ? 0 : 1);
			this.SetDcbFlag(0, 1);
			this.SetDcbFlag(2, (this.handshake == Handshake.RequestToSend || this.handshake == Handshake.RequestToSendXOnXOff) ? 1 : 0);
			this.SetDcbFlag(3, 0);
			this.SetDcbFlag(4, 0);
			this.SetDcbFlag(6, 0);
			this.SetDcbFlag(9, (this.handshake == Handshake.XOnXOff || this.handshake == Handshake.RequestToSendXOnXOff) ? 1 : 0);
			this.SetDcbFlag(8, (this.handshake == Handshake.XOnXOff || this.handshake == Handshake.RequestToSendXOnXOff) ? 1 : 0);
			if (parity != Parity.None)
			{
				this.SetDcbFlag(10, (this.parityReplace != 0) ? 1 : 0);
				this.dcb.ErrorChar = this.parityReplace;
			}
			else
			{
				this.SetDcbFlag(10, 0);
				this.dcb.ErrorChar = 0;
			}
			this.SetDcbFlag(11, discardNull ? 1 : 0);
			if (this.handshake == Handshake.RequestToSend || this.handshake == Handshake.RequestToSendXOnXOff)
			{
				this.SetDcbFlag(12, 2);
			}
			else if (this.GetDcbFlag(12) == 2)
			{
				this.SetDcbFlag(12, 0);
			}
			this.dcb.XonChar = 17;
			this.dcb.XoffChar = 19;
			this.dcb.XonLim = (this.dcb.XoffLim = (ushort)(this.commProp.dwCurrentRxQueue / 4));
			this.dcb.EofChar = 26;
			this.dcb.EvtChar = 26;
			if (!Microsoft.Win32.UnsafeNativeMethods.SetCommState(this._handle, ref this.dcb))
			{
				InternalResources.WinIOError();
			}
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000B52AC File Offset: 0x000B34AC
		internal int GetDcbFlag(int whichFlag)
		{
			uint num;
			if (whichFlag == 4 || whichFlag == 12)
			{
				num = 3U;
			}
			else if (whichFlag == 15)
			{
				num = 131071U;
			}
			else
			{
				num = 1U;
			}
			uint num2 = this.dcb.Flags & (num << whichFlag);
			return (int)(num2 >> whichFlag);
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000B52F0 File Offset: 0x000B34F0
		internal void SetDcbFlag(int whichFlag, int setting)
		{
			setting <<= whichFlag;
			uint num;
			if (whichFlag == 4 || whichFlag == 12)
			{
				num = 3U;
			}
			else if (whichFlag == 15)
			{
				num = 131071U;
			}
			else
			{
				num = 1U;
			}
			this.dcb.Flags = this.dcb.Flags & ~(num << whichFlag);
			this.dcb.Flags = this.dcb.Flags | (uint)setting;
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000B5348 File Offset: 0x000B3548
		private unsafe SerialStream.SerialStreamAsyncResult BeginReadCore(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = new SerialStream.SerialStreamAsyncResult();
			serialStreamAsyncResult._userCallback = userCallback;
			serialStreamAsyncResult._userStateObject = stateObject;
			serialStreamAsyncResult._isWrite = false;
			ManualResetEvent manualResetEvent = new ManualResetEvent(false);
			serialStreamAsyncResult._waitHandle = manualResetEvent;
			Overlapped overlapped = new Overlapped(0, 0, IntPtr.Zero, serialStreamAsyncResult);
			NativeOverlapped* ptr = overlapped.Pack(SerialStream.IOCallback, array);
			serialStreamAsyncResult._overlapped = ptr;
			int num = 0;
			int num2 = this.ReadFileNative(array, offset, numBytes, ptr, out num);
			if (num2 == -1 && num != 997)
			{
				if (num == 38)
				{
					InternalResources.EndOfFile();
				}
				else
				{
					InternalResources.WinIOError(num, string.Empty);
				}
			}
			return serialStreamAsyncResult;
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000B53DC File Offset: 0x000B35DC
		private unsafe SerialStream.SerialStreamAsyncResult BeginWriteCore(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = new SerialStream.SerialStreamAsyncResult();
			serialStreamAsyncResult._userCallback = userCallback;
			serialStreamAsyncResult._userStateObject = stateObject;
			serialStreamAsyncResult._isWrite = true;
			ManualResetEvent manualResetEvent = new ManualResetEvent(false);
			serialStreamAsyncResult._waitHandle = manualResetEvent;
			Overlapped overlapped = new Overlapped(0, 0, IntPtr.Zero, serialStreamAsyncResult);
			NativeOverlapped* ptr = overlapped.Pack(SerialStream.IOCallback, array);
			serialStreamAsyncResult._overlapped = ptr;
			int num = 0;
			int num2 = this.WriteFileNative(array, offset, numBytes, ptr, out num);
			if (num2 == -1 && num != 997)
			{
				if (num == 38)
				{
					InternalResources.EndOfFile();
				}
				else
				{
					InternalResources.WinIOError(num, string.Empty);
				}
			}
			return serialStreamAsyncResult;
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000B5470 File Offset: 0x000B3670
		private unsafe int ReadFileNative(byte[] bytes, int offset, int count, NativeOverlapped* overlapped, out int hr)
		{
			if (bytes.Length - offset < count)
			{
				throw new IndexOutOfRangeException(SR.GetString("IndexOutOfRange_IORaceCondition"));
			}
			if (bytes.Length == 0)
			{
				hr = 0;
				return 0;
			}
			int num = 0;
			int num2;
			fixed (byte[] array = bytes)
			{
				byte* ptr;
				if (bytes == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (this.isAsync)
				{
					num2 = Microsoft.Win32.UnsafeNativeMethods.ReadFile(this._handle, ptr + offset, count, IntPtr.Zero, overlapped);
				}
				else
				{
					num2 = Microsoft.Win32.UnsafeNativeMethods.ReadFile(this._handle, ptr + offset, count, out num, IntPtr.Zero);
				}
			}
			if (num2 == 0)
			{
				hr = Marshal.GetLastWin32Error();
				if (hr == 6)
				{
					this._handle.SetHandleAsInvalid();
				}
				return -1;
			}
			hr = 0;
			return num;
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000B5518 File Offset: 0x000B3718
		private unsafe int WriteFileNative(byte[] bytes, int offset, int count, NativeOverlapped* overlapped, out int hr)
		{
			if (bytes.Length - offset < count)
			{
				throw new IndexOutOfRangeException(SR.GetString("IndexOutOfRange_IORaceCondition"));
			}
			if (bytes.Length == 0)
			{
				hr = 0;
				return 0;
			}
			int num = 0;
			int num2;
			fixed (byte[] array = bytes)
			{
				byte* ptr;
				if (bytes == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (this.isAsync)
				{
					num2 = Microsoft.Win32.UnsafeNativeMethods.WriteFile(this._handle, ptr + offset, count, IntPtr.Zero, overlapped);
				}
				else
				{
					num2 = Microsoft.Win32.UnsafeNativeMethods.WriteFile(this._handle, ptr + offset, count, out num, IntPtr.Zero);
				}
			}
			if (num2 == 0)
			{
				hr = Marshal.GetLastWin32Error();
				if (hr == 6)
				{
					this._handle.SetHandleAsInvalid();
				}
				return -1;
			}
			hr = 0;
			return num;
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000B55C0 File Offset: 0x000B37C0
		private unsafe static void AsyncFSCallback(uint errorCode, uint numBytes, NativeOverlapped* pOverlapped)
		{
			Overlapped overlapped = Overlapped.Unpack(pOverlapped);
			SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = (SerialStream.SerialStreamAsyncResult)overlapped.AsyncResult;
			serialStreamAsyncResult._numBytes = (int)numBytes;
			serialStreamAsyncResult._errorCode = (int)errorCode;
			serialStreamAsyncResult._completedSynchronously = false;
			serialStreamAsyncResult._isComplete = true;
			ManualResetEvent waitHandle = serialStreamAsyncResult._waitHandle;
			if (waitHandle != null && !waitHandle.Set())
			{
				InternalResources.WinIOError();
			}
			AsyncCallback userCallback = serialStreamAsyncResult._userCallback;
			if (userCallback != null)
			{
				userCallback(serialStreamAsyncResult);
			}
		}

		// Token: 0x0400212A RID: 8490
		private const int errorEvents = 271;

		// Token: 0x0400212B RID: 8491
		private const int receivedEvents = 3;

		// Token: 0x0400212C RID: 8492
		private const int pinChangedEvents = 376;

		// Token: 0x0400212D RID: 8493
		private const int infiniteTimeoutConst = -2;

		// Token: 0x0400212E RID: 8494
		private string portName;

		// Token: 0x0400212F RID: 8495
		private byte parityReplace = 63;

		// Token: 0x04002130 RID: 8496
		private bool inBreak;

		// Token: 0x04002131 RID: 8497
		private bool isAsync = true;

		// Token: 0x04002132 RID: 8498
		private Handshake handshake;

		// Token: 0x04002133 RID: 8499
		private bool rtsEnable;

		// Token: 0x04002134 RID: 8500
		private Microsoft.Win32.UnsafeNativeMethods.DCB dcb;

		// Token: 0x04002135 RID: 8501
		private Microsoft.Win32.UnsafeNativeMethods.COMMTIMEOUTS commTimeouts;

		// Token: 0x04002136 RID: 8502
		private Microsoft.Win32.UnsafeNativeMethods.COMSTAT comStat;

		// Token: 0x04002137 RID: 8503
		private Microsoft.Win32.UnsafeNativeMethods.COMMPROP commProp;

		// Token: 0x04002138 RID: 8504
		private const int maxDataBits = 8;

		// Token: 0x04002139 RID: 8505
		private const int minDataBits = 5;

		// Token: 0x0400213A RID: 8506
		internal SafeFileHandle _handle;

		// Token: 0x0400213B RID: 8507
		internal SerialStream.EventLoopRunner eventRunner;

		// Token: 0x0400213C RID: 8508
		private byte[] tempBuf;

		// Token: 0x0400213D RID: 8509
		private static readonly IOCompletionCallback IOCallback = new IOCompletionCallback(SerialStream.AsyncFSCallback);

		// Token: 0x02000813 RID: 2067
		internal sealed class EventLoopRunner
		{
			// Token: 0x060044D9 RID: 17625 RVA: 0x0011FE8C File Offset: 0x0011E08C
			internal EventLoopRunner(SerialStream stream)
			{
				this.handle = stream._handle;
				this.streamWeakReference = new WeakReference(stream);
				this.callErrorEvents = new WaitCallback(this.CallErrorEvents);
				this.callReceiveEvents = new WaitCallback(this.CallReceiveEvents);
				this.callPinEvents = new WaitCallback(this.CallPinEvents);
				this.freeNativeOverlappedCallback = new IOCompletionCallback(this.FreeNativeOverlappedCallback);
				this.isAsync = stream.isAsync;
			}

			// Token: 0x17000F9F RID: 3999
			// (get) Token: 0x060044DA RID: 17626 RVA: 0x0011FF23 File Offset: 0x0011E123
			internal bool ShutdownLoop
			{
				get
				{
					return this.endEventLoop;
				}
			}

			// Token: 0x060044DB RID: 17627 RVA: 0x0011FF2C File Offset: 0x0011E12C
			internal unsafe void WaitForCommEvent()
			{
				int num = 0;
				bool flag = false;
				NativeOverlapped* ptr = null;
				while (!this.ShutdownLoop)
				{
					SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = null;
					if (this.isAsync)
					{
						serialStreamAsyncResult = new SerialStream.SerialStreamAsyncResult();
						serialStreamAsyncResult._userCallback = null;
						serialStreamAsyncResult._userStateObject = null;
						serialStreamAsyncResult._isWrite = false;
						serialStreamAsyncResult._numBytes = 2;
						serialStreamAsyncResult._waitHandle = this.waitCommEventWaitHandle;
						this.waitCommEventWaitHandle.Reset();
						Overlapped overlapped = new Overlapped(0, 0, this.waitCommEventWaitHandle.SafeWaitHandle.DangerousGetHandle(), serialStreamAsyncResult);
						ptr = overlapped.Pack(this.freeNativeOverlappedCallback, null);
					}
					try
					{
						fixed (int* ptr2 = &this.eventsOccurred)
						{
							int* ptr3 = ptr2;
							if (!Microsoft.Win32.UnsafeNativeMethods.WaitCommEvent(this.handle, ptr3, ptr))
							{
								int lastWin32Error = Marshal.GetLastWin32Error();
								if (lastWin32Error == 5 || lastWin32Error == 22 || lastWin32Error == 1617)
								{
									flag = true;
									break;
								}
								if (lastWin32Error == 997)
								{
									bool flag2 = this.waitCommEventWaitHandle.WaitOne();
									int lastWin32Error2;
									do
									{
										flag2 = Microsoft.Win32.UnsafeNativeMethods.GetOverlappedResult(this.handle, ptr, ref num, false);
										lastWin32Error2 = Marshal.GetLastWin32Error();
									}
									while (lastWin32Error2 == 996 && !this.ShutdownLoop && !flag2);
									if (!flag2 && (lastWin32Error2 == 996 || lastWin32Error2 == 87) && !this.ShutdownLoop)
									{
									}
								}
							}
						}
					}
					finally
					{
						int* ptr2 = null;
					}
					if (!this.ShutdownLoop)
					{
						this.CallEvents(this.eventsOccurred);
					}
					if (this.isAsync && Interlocked.Decrement(ref serialStreamAsyncResult._numBytes) == 0)
					{
						Overlapped.Free(ptr);
					}
				}
				if (flag)
				{
					this.endEventLoop = true;
					Overlapped.Free(ptr);
				}
				this.eventLoopEndedSignal.Set();
			}

			// Token: 0x060044DC RID: 17628 RVA: 0x001200C8 File Offset: 0x0011E2C8
			private unsafe void FreeNativeOverlappedCallback(uint errorCode, uint numBytes, NativeOverlapped* pOverlapped)
			{
				Overlapped overlapped = Overlapped.Unpack(pOverlapped);
				SerialStream.SerialStreamAsyncResult serialStreamAsyncResult = (SerialStream.SerialStreamAsyncResult)overlapped.AsyncResult;
				if (Interlocked.Decrement(ref serialStreamAsyncResult._numBytes) == 0)
				{
					Overlapped.Free(pOverlapped);
				}
			}

			// Token: 0x060044DD RID: 17629 RVA: 0x001200FC File Offset: 0x0011E2FC
			private void CallEvents(int nativeEvents)
			{
				if ((nativeEvents & 129) != 0)
				{
					int num = 0;
					if (!Microsoft.Win32.UnsafeNativeMethods.ClearCommError(this.handle, ref num, IntPtr.Zero))
					{
						this.endEventLoop = true;
						Thread.MemoryBarrier();
						return;
					}
					num &= 271;
					if (num != 0)
					{
						ThreadPool.QueueUserWorkItem(this.callErrorEvents, num);
					}
				}
				if ((nativeEvents & 376) != 0)
				{
					ThreadPool.QueueUserWorkItem(this.callPinEvents, nativeEvents);
				}
				if ((nativeEvents & 3) != 0)
				{
					ThreadPool.QueueUserWorkItem(this.callReceiveEvents, nativeEvents);
				}
			}

			// Token: 0x060044DE RID: 17630 RVA: 0x00120184 File Offset: 0x0011E384
			private void CallErrorEvents(object state)
			{
				int num = (int)state;
				SerialStream serialStream = (SerialStream)this.streamWeakReference.Target;
				if (serialStream == null)
				{
					return;
				}
				if (serialStream.ErrorReceived != null)
				{
					if ((num & 256) != 0)
					{
						serialStream.ErrorReceived(serialStream, new SerialErrorReceivedEventArgs(SerialError.TXFull));
					}
					if ((num & 1) != 0)
					{
						serialStream.ErrorReceived(serialStream, new SerialErrorReceivedEventArgs(SerialError.RXOver));
					}
					if ((num & 2) != 0)
					{
						serialStream.ErrorReceived(serialStream, new SerialErrorReceivedEventArgs(SerialError.Overrun));
					}
					if ((num & 4) != 0)
					{
						serialStream.ErrorReceived(serialStream, new SerialErrorReceivedEventArgs(SerialError.RXParity));
					}
					if ((num & 8) != 0)
					{
						serialStream.ErrorReceived(serialStream, new SerialErrorReceivedEventArgs(SerialError.Frame));
					}
				}
			}

			// Token: 0x060044DF RID: 17631 RVA: 0x00120234 File Offset: 0x0011E434
			private void CallReceiveEvents(object state)
			{
				int num = (int)state;
				SerialStream serialStream = (SerialStream)this.streamWeakReference.Target;
				if (serialStream == null)
				{
					return;
				}
				if (serialStream.DataReceived != null)
				{
					if ((num & 1) != 0)
					{
						serialStream.DataReceived(serialStream, new SerialDataReceivedEventArgs(SerialData.Chars));
					}
					if ((num & 2) != 0)
					{
						serialStream.DataReceived(serialStream, new SerialDataReceivedEventArgs(SerialData.Eof));
					}
				}
			}

			// Token: 0x060044E0 RID: 17632 RVA: 0x00120298 File Offset: 0x0011E498
			private void CallPinEvents(object state)
			{
				int num = (int)state;
				SerialStream serialStream = (SerialStream)this.streamWeakReference.Target;
				if (serialStream == null)
				{
					return;
				}
				if (serialStream.PinChanged != null)
				{
					if ((num & 8) != 0)
					{
						serialStream.PinChanged(serialStream, new SerialPinChangedEventArgs(SerialPinChange.CtsChanged));
					}
					if ((num & 16) != 0)
					{
						serialStream.PinChanged(serialStream, new SerialPinChangedEventArgs(SerialPinChange.DsrChanged));
					}
					if ((num & 32) != 0)
					{
						serialStream.PinChanged(serialStream, new SerialPinChangedEventArgs(SerialPinChange.CDChanged));
					}
					if ((num & 256) != 0)
					{
						serialStream.PinChanged(serialStream, new SerialPinChangedEventArgs(SerialPinChange.Ring));
					}
					if ((num & 64) != 0)
					{
						serialStream.PinChanged(serialStream, new SerialPinChangedEventArgs(SerialPinChange.Break));
					}
				}
			}

			// Token: 0x060044E1 RID: 17633 RVA: 0x00120350 File Offset: 0x0011E550
			internal void SafelyWaitForCommEvent()
			{
				try
				{
					this.WaitForCommEvent();
				}
				catch (ObjectDisposedException)
				{
				}
				catch (Exception ex)
				{
				}
			}

			// Token: 0x04003562 RID: 13666
			private WeakReference streamWeakReference;

			// Token: 0x04003563 RID: 13667
			internal ManualResetEvent eventLoopEndedSignal = new ManualResetEvent(false);

			// Token: 0x04003564 RID: 13668
			internal ManualResetEvent waitCommEventWaitHandle = new ManualResetEvent(false);

			// Token: 0x04003565 RID: 13669
			private SafeFileHandle handle;

			// Token: 0x04003566 RID: 13670
			private bool isAsync;

			// Token: 0x04003567 RID: 13671
			internal bool endEventLoop;

			// Token: 0x04003568 RID: 13672
			private int eventsOccurred;

			// Token: 0x04003569 RID: 13673
			private WaitCallback callErrorEvents;

			// Token: 0x0400356A RID: 13674
			private WaitCallback callReceiveEvents;

			// Token: 0x0400356B RID: 13675
			private WaitCallback callPinEvents;

			// Token: 0x0400356C RID: 13676
			private IOCompletionCallback freeNativeOverlappedCallback;
		}

		// Token: 0x02000814 RID: 2068
		internal sealed class SerialStreamAsyncResult : IAsyncResult
		{
			// Token: 0x17000FA0 RID: 4000
			// (get) Token: 0x060044E2 RID: 17634 RVA: 0x00120388 File Offset: 0x0011E588
			public object AsyncState
			{
				get
				{
					return this._userStateObject;
				}
			}

			// Token: 0x17000FA1 RID: 4001
			// (get) Token: 0x060044E3 RID: 17635 RVA: 0x00120390 File Offset: 0x0011E590
			public bool IsCompleted
			{
				get
				{
					return this._isComplete;
				}
			}

			// Token: 0x17000FA2 RID: 4002
			// (get) Token: 0x060044E4 RID: 17636 RVA: 0x00120398 File Offset: 0x0011E598
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return this._waitHandle;
				}
			}

			// Token: 0x17000FA3 RID: 4003
			// (get) Token: 0x060044E5 RID: 17637 RVA: 0x001203A0 File Offset: 0x0011E5A0
			public bool CompletedSynchronously
			{
				get
				{
					return this._completedSynchronously;
				}
			}

			// Token: 0x0400356D RID: 13677
			internal AsyncCallback _userCallback;

			// Token: 0x0400356E RID: 13678
			internal object _userStateObject;

			// Token: 0x0400356F RID: 13679
			internal bool _isWrite;

			// Token: 0x04003570 RID: 13680
			internal bool _isComplete;

			// Token: 0x04003571 RID: 13681
			internal bool _completedSynchronously;

			// Token: 0x04003572 RID: 13682
			internal ManualResetEvent _waitHandle;

			// Token: 0x04003573 RID: 13683
			internal int _EndXxxCalled;

			// Token: 0x04003574 RID: 13684
			internal int _numBytes;

			// Token: 0x04003575 RID: 13685
			internal int _errorCode;

			// Token: 0x04003576 RID: 13686
			internal unsafe NativeOverlapped* _overlapped;
		}
	}
}
