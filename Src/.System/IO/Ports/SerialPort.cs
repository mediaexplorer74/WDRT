using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.IO.Ports
{
	/// <summary>Represents a serial port resource.</summary>
	// Token: 0x02000411 RID: 1041
	[MonitoringDescription("SerialPortDesc")]
	public class SerialPort : Component
	{
		/// <summary>Indicates that an error has occurred with a port represented by a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060026BA RID: 9914 RVA: 0x000B1ED4 File Offset: 0x000B00D4
		// (remove) Token: 0x060026BB RID: 9915 RVA: 0x000B1F0C File Offset: 0x000B010C
		[MonitoringDescription("SerialErrorReceived")]
		public event SerialErrorReceivedEventHandler ErrorReceived;

		/// <summary>Indicates that a non-data signal event has occurred on the port represented by the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060026BC RID: 9916 RVA: 0x000B1F44 File Offset: 0x000B0144
		// (remove) Token: 0x060026BD RID: 9917 RVA: 0x000B1F7C File Offset: 0x000B017C
		[MonitoringDescription("SerialPinChanged")]
		public event SerialPinChangedEventHandler PinChanged;

		/// <summary>Indicates that data has been received through a port represented by the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060026BE RID: 9918 RVA: 0x000B1FB4 File Offset: 0x000B01B4
		// (remove) Token: 0x060026BF RID: 9919 RVA: 0x000B1FEC File Offset: 0x000B01EC
		[MonitoringDescription("SerialDataReceived")]
		public event SerialDataReceivedEventHandler DataReceived;

		/// <summary>Gets the underlying <see cref="T:System.IO.Stream" /> object for a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream is in a .NET Compact Framework application and one of the following methods was called:  
		///  <see cref="M:System.IO.Stream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /><see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /><see cref="M:System.IO.Stream.EndRead(System.IAsyncResult)" /><see cref="M:System.IO.Stream.EndWrite(System.IAsyncResult)" />  
		///
		///  The .NET Compact Framework does not support the asynchronous model with base streams.</exception>
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x000B2021 File Offset: 0x000B0221
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Stream BaseStream
		{
			get
			{
				if (!this.IsOpen)
				{
					throw new InvalidOperationException(SR.GetString("BaseStream_Invalid_Not_Open"));
				}
				return this.internalSerialStream;
			}
		}

		/// <summary>Gets or sets the serial baud rate.</summary>
		/// <returns>The baud rate.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The baud rate specified is less than or equal to zero, or is greater than the maximum allowable baud rate for the device.</exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x000B2041 File Offset: 0x000B0241
		// (set) Token: 0x060026C2 RID: 9922 RVA: 0x000B2049 File Offset: 0x000B0249
		[Browsable(true)]
		[DefaultValue(9600)]
		[MonitoringDescription("BaudRate")]
		public int BaudRate
		{
			get
			{
				return this.baudRate;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("BaudRate", SR.GetString("ArgumentOutOfRange_NeedPosNum"));
				}
				if (this.IsOpen)
				{
					this.internalSerialStream.BaudRate = value;
				}
				this.baudRate = value;
			}
		}

		/// <summary>Gets or sets the break signal state.</summary>
		/// <returns>
		///   <see langword="true" /> if the port is in a break state; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x000B207F File Offset: 0x000B027F
		// (set) Token: 0x060026C4 RID: 9924 RVA: 0x000B20A4 File Offset: 0x000B02A4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool BreakState
		{
			get
			{
				if (!this.IsOpen)
				{
					throw new InvalidOperationException(SR.GetString("Port_not_open"));
				}
				return this.internalSerialStream.BreakState;
			}
			set
			{
				if (!this.IsOpen)
				{
					throw new InvalidOperationException(SR.GetString("Port_not_open"));
				}
				this.internalSerialStream.BreakState = value;
			}
		}

		/// <summary>Gets the number of bytes of data in the send buffer.</summary>
		/// <returns>The number of bytes of data in the send buffer.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x000B20CA File Offset: 0x000B02CA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int BytesToWrite
		{
			get
			{
				if (!this.IsOpen)
				{
					throw new InvalidOperationException(SR.GetString("Port_not_open"));
				}
				return this.internalSerialStream.BytesToWrite;
			}
		}

		/// <summary>Gets the number of bytes of data in the receive buffer.</summary>
		/// <returns>The number of bytes of data in the receive buffer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The port is not open.</exception>
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x000B20EF File Offset: 0x000B02EF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int BytesToRead
		{
			get
			{
				if (!this.IsOpen)
				{
					throw new InvalidOperationException(SR.GetString("Port_not_open"));
				}
				return this.internalSerialStream.BytesToRead + this.CachedBytesToRead;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060026C7 RID: 9927 RVA: 0x000B211B File Offset: 0x000B031B
		private int CachedBytesToRead
		{
			get
			{
				return this.readLen - this.readPos;
			}
		}

		/// <summary>Gets the state of the Carrier Detect line for the port.</summary>
		/// <returns>
		///   <see langword="true" /> if the carrier is detected; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060026C8 RID: 9928 RVA: 0x000B212A File Offset: 0x000B032A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CDHolding
		{
			get
			{
				if (!this.IsOpen)
				{
					throw new InvalidOperationException(SR.GetString("Port_not_open"));
				}
				return this.internalSerialStream.CDHolding;
			}
		}

		/// <summary>Gets the state of the Clear-to-Send line.</summary>
		/// <returns>
		///   <see langword="true" /> if the Clear-to-Send line is detected; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x000B214F File Offset: 0x000B034F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CtsHolding
		{
			get
			{
				if (!this.IsOpen)
				{
					throw new InvalidOperationException(SR.GetString("Port_not_open"));
				}
				return this.internalSerialStream.CtsHolding;
			}
		}

		/// <summary>Gets or sets the standard length of data bits per byte.</summary>
		/// <returns>The data bits length.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The data bits value is less than 5 or more than 8.</exception>
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x000B2174 File Offset: 0x000B0374
		// (set) Token: 0x060026CB RID: 9931 RVA: 0x000B217C File Offset: 0x000B037C
		[Browsable(true)]
		[DefaultValue(8)]
		[MonitoringDescription("DataBits")]
		public int DataBits
		{
			get
			{
				return this.dataBits;
			}
			set
			{
				if (value < 5 || value > 8)
				{
					throw new ArgumentOutOfRangeException("DataBits", SR.GetString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[] { 5, 8 }));
				}
				if (this.IsOpen)
				{
					this.internalSerialStream.DataBits = value;
				}
				this.dataBits = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether null bytes are ignored when transmitted between the port and the receive buffer.</summary>
		/// <returns>
		///   <see langword="true" /> if null bytes are ignored; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x000B21D9 File Offset: 0x000B03D9
		// (set) Token: 0x060026CD RID: 9933 RVA: 0x000B21E1 File Offset: 0x000B03E1
		[Browsable(true)]
		[DefaultValue(false)]
		[MonitoringDescription("DiscardNull")]
		public bool DiscardNull
		{
			get
			{
				return this.discardNull;
			}
			set
			{
				if (this.IsOpen)
				{
					this.internalSerialStream.DiscardNull = value;
				}
				this.discardNull = value;
			}
		}

		/// <summary>Gets the state of the Data Set Ready (DSR) signal.</summary>
		/// <returns>
		///   <see langword="true" /> if a Data Set Ready signal has been sent to the port; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x000B21FE File Offset: 0x000B03FE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool DsrHolding
		{
			get
			{
				if (!this.IsOpen)
				{
					throw new InvalidOperationException(SR.GetString("Port_not_open"));
				}
				return this.internalSerialStream.DsrHolding;
			}
		}

		/// <summary>Gets or sets a value that enables the Data Terminal Ready (DTR) signal during serial communication.</summary>
		/// <returns>
		///   <see langword="true" /> to enable Data Terminal Ready (DTR); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x000B2223 File Offset: 0x000B0423
		// (set) Token: 0x060026D0 RID: 9936 RVA: 0x000B2244 File Offset: 0x000B0444
		[Browsable(true)]
		[DefaultValue(false)]
		[MonitoringDescription("DtrEnable")]
		public bool DtrEnable
		{
			get
			{
				if (this.IsOpen)
				{
					this.dtrEnable = this.internalSerialStream.DtrEnable;
				}
				return this.dtrEnable;
			}
			set
			{
				if (this.IsOpen)
				{
					this.internalSerialStream.DtrEnable = value;
				}
				this.dtrEnable = value;
			}
		}

		/// <summary>Gets or sets the byte encoding for pre- and post-transmission conversion of text.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object. The default is <see cref="T:System.Text.ASCIIEncoding" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.Encoding" /> property was set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.Encoding" /> property was set to an encoding that is not <see cref="T:System.Text.ASCIIEncoding" />, <see cref="T:System.Text.UTF8Encoding" />, <see cref="T:System.Text.UTF32Encoding" />, <see cref="T:System.Text.UnicodeEncoding" />, one of the Windows single byte encodings, or one of the Windows double byte encodings.</exception>
		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x000B2261 File Offset: 0x000B0461
		// (set) Token: 0x060026D2 RID: 9938 RVA: 0x000B226C File Offset: 0x000B046C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Encoding")]
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Encoding");
				}
				if (!(value is ASCIIEncoding) && !(value is UTF8Encoding) && !(value is UnicodeEncoding) && !(value is UTF32Encoding) && ((value.CodePage >= 50000 && value.CodePage != 54936) || !(value.GetType().Assembly == typeof(string).Assembly)))
				{
					throw new ArgumentException(SR.GetString("NotSupportedEncoding", new object[] { value.WebName }), "value");
				}
				this.encoding = value;
				this.decoder = this.encoding.GetDecoder();
				this.maxByteCountForSingleChar = this.encoding.GetMaxByteCount(1);
				this.singleCharBuffer = null;
			}
		}

		/// <summary>Gets or sets the handshaking protocol for serial port transmission of data using a value from <see cref="T:System.IO.Ports.Handshake" />.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.Handshake" /> values. The default is <see langword="None" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value passed is not a valid value in the <see cref="T:System.IO.Ports.Handshake" /> enumeration.</exception>
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060026D3 RID: 9939 RVA: 0x000B2337 File Offset: 0x000B0537
		// (set) Token: 0x060026D4 RID: 9940 RVA: 0x000B233F File Offset: 0x000B053F
		[Browsable(true)]
		[DefaultValue(Handshake.None)]
		[MonitoringDescription("Handshake")]
		public Handshake Handshake
		{
			get
			{
				return this.handshake;
			}
			set
			{
				if (value < Handshake.None || value > Handshake.RequestToSendXOnXOff)
				{
					throw new ArgumentOutOfRangeException("Handshake", SR.GetString("ArgumentOutOfRange_Enum"));
				}
				if (this.IsOpen)
				{
					this.internalSerialStream.Handshake = value;
				}
				this.handshake = value;
			}
		}

		/// <summary>Gets a value indicating the open or closed status of the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> if the serial port is open; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.IsOpen" /> value passed is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.IsOpen" /> value passed is an empty string ("").</exception>
		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x000B2379 File Offset: 0x000B0579
		[Browsable(false)]
		public bool IsOpen
		{
			get
			{
				return this.internalSerialStream != null && this.internalSerialStream.IsOpen;
			}
		}

		/// <summary>Gets or sets the value used to interpret the end of a call to the <see cref="M:System.IO.Ports.SerialPort.ReadLine" /> and <see cref="M:System.IO.Ports.SerialPort.WriteLine(System.String)" /> methods.</summary>
		/// <returns>A value that represents the end of a line. The default is a line feed, <see cref="P:System.Environment.NewLine" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property value is empty.</exception>
		/// <exception cref="T:System.ArgumentNullException">The property value is <see langword="null" />.</exception>
		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x000B2390 File Offset: 0x000B0590
		// (set) Token: 0x060026D7 RID: 9943 RVA: 0x000B2398 File Offset: 0x000B0598
		[Browsable(false)]
		[DefaultValue("\n")]
		[MonitoringDescription("NewLine")]
		public string NewLine
		{
			get
			{
				return this.newLine;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("InvalidNullEmptyArgument", new object[] { "NewLine" }));
				}
				this.newLine = value;
			}
		}

		/// <summary>Gets or sets the parity-checking protocol.</summary>
		/// <returns>One of the enumeration values that represents the parity-checking protocol. The default is <see cref="F:System.IO.Ports.Parity.None" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.Parity" /> value passed is not a valid value in the <see cref="T:System.IO.Ports.Parity" /> enumeration.</exception>
		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x000B23D0 File Offset: 0x000B05D0
		// (set) Token: 0x060026D9 RID: 9945 RVA: 0x000B23D8 File Offset: 0x000B05D8
		[Browsable(true)]
		[DefaultValue(Parity.None)]
		[MonitoringDescription("Parity")]
		public Parity Parity
		{
			get
			{
				return this.parity;
			}
			set
			{
				if (value < Parity.None || value > Parity.Space)
				{
					throw new ArgumentOutOfRangeException("Parity", SR.GetString("ArgumentOutOfRange_Enum"));
				}
				if (this.IsOpen)
				{
					this.internalSerialStream.Parity = value;
				}
				this.parity = value;
			}
		}

		/// <summary>Gets or sets the byte that replaces invalid bytes in a data stream when a parity error occurs.</summary>
		/// <returns>A byte that replaces invalid bytes.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x000B2412 File Offset: 0x000B0612
		// (set) Token: 0x060026DB RID: 9947 RVA: 0x000B241A File Offset: 0x000B061A
		[Browsable(true)]
		[DefaultValue(63)]
		[MonitoringDescription("ParityReplace")]
		public byte ParityReplace
		{
			get
			{
				return this.parityReplace;
			}
			set
			{
				if (this.IsOpen)
				{
					this.internalSerialStream.ParityReplace = value;
				}
				this.parityReplace = value;
			}
		}

		/// <summary>Gets or sets the port for communications, including but not limited to all available COM ports.</summary>
		/// <returns>The communications port. The default is COM1.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.PortName" /> property was set to a value with a length of zero.  
		///  -or-  
		///  The <see cref="P:System.IO.Ports.SerialPort.PortName" /> property was set to a value that starts with "\\".  
		///  -or-  
		///  The port name was not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.PortName" /> property was set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is open.</exception>
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x000B2437 File Offset: 0x000B0637
		// (set) Token: 0x060026DD RID: 9949 RVA: 0x000B2440 File Offset: 0x000B0640
		[Browsable(true)]
		[DefaultValue("COM1")]
		[MonitoringDescription("PortName")]
		public string PortName
		{
			get
			{
				return this.portName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PortName");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("PortNameEmpty_String"), "PortName");
				}
				if (value.StartsWith("\\\\", StringComparison.Ordinal))
				{
					throw new ArgumentException(SR.GetString("Arg_SecurityException"), "PortName");
				}
				if (this.IsOpen)
				{
					throw new InvalidOperationException(SR.GetString("Cant_be_set_when_open", new object[] { "PortName" }));
				}
				this.portName = value;
			}
		}

		/// <summary>Gets or sets the size of the <see cref="T:System.IO.Ports.SerialPort" /> input buffer.</summary>
		/// <returns>The buffer size, in bytes. The default value is 4096; the maximum value is that of a positive int, or 2147483647.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize" /> value set is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize" /> property was set while the stream was open.</exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize" /> property was set to an odd integer value.</exception>
		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x000B24C8 File Offset: 0x000B06C8
		// (set) Token: 0x060026DF RID: 9951 RVA: 0x000B24D0 File Offset: 0x000B06D0
		[Browsable(true)]
		[DefaultValue(4096)]
		[MonitoringDescription("ReadBufferSize")]
		public int ReadBufferSize
		{
			get
			{
				return this.readBufferSize;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.IsOpen)
				{
					throw new InvalidOperationException(SR.GetString("Cant_be_set_when_open", new object[] { "value" }));
				}
				this.readBufferSize = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds before a time-out occurs when a read operation does not finish.</summary>
		/// <returns>The number of milliseconds before a time-out occurs when a read operation does not finish.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The read time-out value is less than zero and not equal to <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />.</exception>
		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x000B250E File Offset: 0x000B070E
		// (set) Token: 0x060026E1 RID: 9953 RVA: 0x000B2516 File Offset: 0x000B0716
		[Browsable(true)]
		[DefaultValue(-1)]
		[MonitoringDescription("ReadTimeout")]
		public int ReadTimeout
		{
			get
			{
				return this.readTimeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("ReadTimeout", SR.GetString("ArgumentOutOfRange_Timeout"));
				}
				if (this.IsOpen)
				{
					this.internalSerialStream.ReadTimeout = value;
				}
				this.readTimeout = value;
			}
		}

		/// <summary>Gets or sets the number of bytes in the internal input buffer before a <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event occurs.</summary>
		/// <returns>The number of bytes in the internal input buffer before a <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event is fired. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.ReceivedBytesThreshold" /> value is less than or equal to zero.</exception>
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060026E2 RID: 9954 RVA: 0x000B2550 File Offset: 0x000B0750
		// (set) Token: 0x060026E3 RID: 9955 RVA: 0x000B2558 File Offset: 0x000B0758
		[Browsable(true)]
		[DefaultValue(1)]
		[MonitoringDescription("ReceivedBytesThreshold")]
		public int ReceivedBytesThreshold
		{
			get
			{
				return this.receivedBytesThreshold;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("ReceivedBytesThreshold", SR.GetString("ArgumentOutOfRange_NeedPosNum"));
				}
				this.receivedBytesThreshold = value;
				if (this.IsOpen)
				{
					SerialDataReceivedEventArgs serialDataReceivedEventArgs = new SerialDataReceivedEventArgs(SerialData.Chars);
					this.CatchReceivedEvents(this, serialDataReceivedEventArgs);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the Request to Send (RTS) signal is enabled during serial communication.</summary>
		/// <returns>
		///   <see langword="true" /> to enable Request to Transmit (RTS); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.IO.Ports.SerialPort.RtsEnable" /> property was set or retrieved while the <see cref="P:System.IO.Ports.SerialPort.Handshake" /> property is set to the <see cref="F:System.IO.Ports.Handshake.RequestToSend" /> value or the <see cref="F:System.IO.Ports.Handshake.RequestToSendXOnXOff" /> value.</exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x000B259C File Offset: 0x000B079C
		// (set) Token: 0x060026E5 RID: 9957 RVA: 0x000B25BD File Offset: 0x000B07BD
		[Browsable(true)]
		[DefaultValue(false)]
		[MonitoringDescription("RtsEnable")]
		public bool RtsEnable
		{
			get
			{
				if (this.IsOpen)
				{
					this.rtsEnable = this.internalSerialStream.RtsEnable;
				}
				return this.rtsEnable;
			}
			set
			{
				if (this.IsOpen)
				{
					this.internalSerialStream.RtsEnable = value;
				}
				this.rtsEnable = value;
			}
		}

		/// <summary>Gets or sets the standard number of stopbits per byte.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.StopBits" /> values.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.StopBits" /> value is  <see cref="F:System.IO.Ports.StopBits.None" />.</exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x000B25DA File Offset: 0x000B07DA
		// (set) Token: 0x060026E7 RID: 9959 RVA: 0x000B25E2 File Offset: 0x000B07E2
		[Browsable(true)]
		[DefaultValue(StopBits.One)]
		[MonitoringDescription("StopBits")]
		public StopBits StopBits
		{
			get
			{
				return this.stopBits;
			}
			set
			{
				if (value < StopBits.One || value > StopBits.OnePointFive)
				{
					throw new ArgumentOutOfRangeException("StopBits", SR.GetString("ArgumentOutOfRange_Enum"));
				}
				if (this.IsOpen)
				{
					this.internalSerialStream.StopBits = value;
				}
				this.stopBits = value;
			}
		}

		/// <summary>Gets or sets the size of the serial port output buffer.</summary>
		/// <returns>The size of the output buffer. The default is 2048.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize" /> value is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize" /> property was set while the stream was open.</exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize" /> property was set to an odd integer value.</exception>
		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x000B261C File Offset: 0x000B081C
		// (set) Token: 0x060026E9 RID: 9961 RVA: 0x000B2624 File Offset: 0x000B0824
		[Browsable(true)]
		[DefaultValue(2048)]
		[MonitoringDescription("WriteBufferSize")]
		public int WriteBufferSize
		{
			get
			{
				return this.writeBufferSize;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.IsOpen)
				{
					throw new InvalidOperationException(SR.GetString("Cant_be_set_when_open", new object[] { "value" }));
				}
				this.writeBufferSize = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds before a time-out occurs when a write operation does not finish.</summary>
		/// <returns>The number of milliseconds before a time-out occurs. The default is <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />.</returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.WriteTimeout" /> value is less than zero and not equal to <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />.</exception>
		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060026EA RID: 9962 RVA: 0x000B2662 File Offset: 0x000B0862
		// (set) Token: 0x060026EB RID: 9963 RVA: 0x000B266A File Offset: 0x000B086A
		[Browsable(true)]
		[DefaultValue(-1)]
		[MonitoringDescription("WriteTimeout")]
		public int WriteTimeout
		{
			get
			{
				return this.writeTimeout;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("WriteTimeout", SR.GetString("ArgumentOutOfRange_WriteTimeout"));
				}
				if (this.IsOpen)
				{
					this.internalSerialStream.WriteTimeout = value;
				}
				this.writeTimeout = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified <see cref="T:System.ComponentModel.IContainer" /> object.</summary>
		/// <param name="container">An interface to a container.</param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x060026EC RID: 9964 RVA: 0x000B26A4 File Offset: 0x000B08A4
		public SerialPort(IContainer container)
		{
			container.Add(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class.</summary>
		// Token: 0x060026ED RID: 9965 RVA: 0x000B2768 File Offset: 0x000B0968
		public SerialPort()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name.</summary>
		/// <param name="portName">The port to use (for example, COM1).</param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x060026EE RID: 9966 RVA: 0x000B2825 File Offset: 0x000B0A25
		public SerialPort(string portName)
			: this(portName, 9600, Parity.None, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name and baud rate.</summary>
		/// <param name="portName">The port to use (for example, COM1).</param>
		/// <param name="baudRate">The baud rate.</param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x060026EF RID: 9967 RVA: 0x000B2836 File Offset: 0x000B0A36
		public SerialPort(string portName, int baudRate)
			: this(portName, baudRate, Parity.None, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name, baud rate, and parity bit.</summary>
		/// <param name="portName">The port to use (for example, COM1).</param>
		/// <param name="baudRate">The baud rate.</param>
		/// <param name="parity">One of the <see cref="P:System.IO.Ports.SerialPort.Parity" /> values.</param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x060026F0 RID: 9968 RVA: 0x000B2843 File Offset: 0x000B0A43
		public SerialPort(string portName, int baudRate, Parity parity)
			: this(portName, baudRate, parity, 8, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name, baud rate, parity bit, and data bits.</summary>
		/// <param name="portName">The port to use (for example, COM1).</param>
		/// <param name="baudRate">The baud rate.</param>
		/// <param name="parity">One of the <see cref="P:System.IO.Ports.SerialPort.Parity" /> values.</param>
		/// <param name="dataBits">The data bits value.</param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x060026F1 RID: 9969 RVA: 0x000B2850 File Offset: 0x000B0A50
		public SerialPort(string portName, int baudRate, Parity parity, int dataBits)
			: this(portName, baudRate, parity, dataBits, StopBits.One)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Ports.SerialPort" /> class using the specified port name, baud rate, parity bit, data bits, and stop bit.</summary>
		/// <param name="portName">The port to use (for example, COM1).</param>
		/// <param name="baudRate">The baud rate.</param>
		/// <param name="parity">One of the <see cref="P:System.IO.Ports.SerialPort.Parity" /> values.</param>
		/// <param name="dataBits">The data bits value.</param>
		/// <param name="stopBits">One of the <see cref="P:System.IO.Ports.SerialPort.StopBits" /> values.</param>
		/// <exception cref="T:System.IO.IOException">The specified port could not be found or opened.</exception>
		// Token: 0x060026F2 RID: 9970 RVA: 0x000B2860 File Offset: 0x000B0A60
		public SerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
		{
			this.PortName = portName;
			this.BaudRate = baudRate;
			this.Parity = parity;
			this.DataBits = dataBits;
			this.StopBits = stopBits;
		}

		/// <summary>Closes the port connection, sets the <see cref="P:System.IO.Ports.SerialPort.IsOpen" /> property to <see langword="false" />, and disposes of the internal <see cref="T:System.IO.Stream" /> object.</summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x060026F3 RID: 9971 RVA: 0x000B2942 File Offset: 0x000B0B42
		public void Close()
		{
			base.Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Ports.SerialPort" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		// Token: 0x060026F4 RID: 9972 RVA: 0x000B294A File Offset: 0x000B0B4A
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.IsOpen)
			{
				this.internalSerialStream.Flush();
				this.internalSerialStream.Close();
				this.internalSerialStream = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Discards data from the serial driver's receive buffer.</summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		// Token: 0x060026F5 RID: 9973 RVA: 0x000B297C File Offset: 0x000B0B7C
		public void DiscardInBuffer()
		{
			if (!this.IsOpen)
			{
				throw new InvalidOperationException(SR.GetString("Port_not_open"));
			}
			this.internalSerialStream.DiscardInBuffer();
			this.readPos = (this.readLen = 0);
		}

		/// <summary>Discards data from the serial driver's transmit buffer.</summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open" /> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close" /> method has been called.</exception>
		// Token: 0x060026F6 RID: 9974 RVA: 0x000B29BC File Offset: 0x000B0BBC
		public void DiscardOutBuffer()
		{
			if (!this.IsOpen)
			{
				throw new InvalidOperationException(SR.GetString("Port_not_open"));
			}
			this.internalSerialStream.DiscardOutBuffer();
		}

		/// <summary>Gets an array of serial port names for the current computer.</summary>
		/// <returns>An array of serial port names for the current computer.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The serial port names could not be queried.</exception>
		// Token: 0x060026F7 RID: 9975 RVA: 0x000B29E4 File Offset: 0x000B0BE4
		public static string[] GetPortNames()
		{
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			string[] array = null;
			RegistryPermission registryPermission = new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\HARDWARE\\DEVICEMAP\\SERIALCOMM");
			registryPermission.Assert();
			try
			{
				registryKey = Registry.LocalMachine;
				registryKey2 = registryKey.OpenSubKey("HARDWARE\\DEVICEMAP\\SERIALCOMM", false);
				if (registryKey2 != null)
				{
					string[] valueNames = registryKey2.GetValueNames();
					array = new string[valueNames.Length];
					for (int i = 0; i < valueNames.Length; i++)
					{
						array[i] = (string)registryKey2.GetValue(valueNames[i]);
					}
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
				CodeAccessPermission.RevertAssert();
			}
			if (array == null)
			{
				array = new string[0];
			}
			return array;
		}

		/// <summary>Opens a new serial port connection.</summary>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied to the port.  
		/// -or-
		///  The current process, or another process on the system, already has the specified COM port open either by a <see cref="T:System.IO.Ports.SerialPort" /> instance or in unmanaged code.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One or more of the properties for this instance are invalid. For example, the <see cref="P:System.IO.Ports.SerialPort.Parity" />, <see cref="P:System.IO.Ports.SerialPort.DataBits" />, or <see cref="P:System.IO.Ports.SerialPort.Handshake" /> properties are not valid values; the <see cref="P:System.IO.Ports.SerialPort.BaudRate" /> is less than or equal to zero; the <see cref="P:System.IO.Ports.SerialPort.ReadTimeout" /> or <see cref="P:System.IO.Ports.SerialPort.WriteTimeout" /> property is less than zero and is not <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout" />.</exception>
		/// <exception cref="T:System.ArgumentException">The port name does not begin with "COM".  
		/// -or-
		///  The file type of the port is not supported.</exception>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  
		/// -or-
		///  An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort" /> object were invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port on the current instance of the <see cref="T:System.IO.Ports.SerialPort" /> is already open.</exception>
		// Token: 0x060026F8 RID: 9976 RVA: 0x000B2A90 File Offset: 0x000B0C90
		public void Open()
		{
			if (this.IsOpen)
			{
				throw new InvalidOperationException(SR.GetString("Port_already_open"));
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			this.internalSerialStream = new SerialStream(this.portName, this.baudRate, this.parity, this.dataBits, this.stopBits, this.readTimeout, this.writeTimeout, this.handshake, this.dtrEnable, this.rtsEnable, this.discardNull, this.parityReplace);
			this.internalSerialStream.SetBufferSizes(this.readBufferSize, this.writeBufferSize);
			this.internalSerialStream.ErrorReceived += this.CatchErrorEvents;
			this.internalSerialStream.PinChanged += this.CatchPinChangedEvents;
			this.internalSerialStream.DataReceived += this.CatchReceivedEvents;
		}

		/// <summary>Reads a number of bytes from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer and writes those bytes into a byte array at the specified offset.</summary>
		/// <param name="buffer">The byte array to write the input to.</param>
		/// <param name="offset">The offset in <paramref name="buffer" /> at which to write the bytes.</param>
		/// <param name="count">The maximum number of bytes to read. Fewer bytes are read if <paramref name="count" /> is greater than the number of bytes in the input buffer.</param>
		/// <returns>The number of bytes read.</returns>
		/// <exception cref="T:System.ArgumentNullException">The buffer passed is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.TimeoutException">No bytes were available to read.</exception>
		// Token: 0x060026F9 RID: 9977 RVA: 0x000B2B70 File Offset: 0x000B0D70
		public int Read(byte[] buffer, int offset, int count)
		{
			if (!this.IsOpen)
			{
				throw new InvalidOperationException(SR.GetString("Port_not_open"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.GetString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			int num = 0;
			if (this.CachedBytesToRead >= 1)
			{
				num = Math.Min(this.CachedBytesToRead, count);
				Buffer.BlockCopy(this.inBuffer, this.readPos, buffer, offset, num);
				this.readPos += num;
				if (num == count)
				{
					if (this.readPos == this.readLen)
					{
						this.readPos = (this.readLen = 0);
					}
					return count;
				}
				if (this.BytesToRead == 0)
				{
					return num;
				}
			}
			this.readLen = (this.readPos = 0);
			int num2 = count - num;
			num += this.internalSerialStream.Read(buffer, offset + num, num2);
			this.decoder.Reset();
			return num;
		}

		/// <summary>Synchronously reads one character from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer.</summary>
		/// <returns>The character that was read.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open.</exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended.  
		/// -or-
		///  No character was available in the allotted time-out period.</exception>
		// Token: 0x060026FA RID: 9978 RVA: 0x000B2C92 File Offset: 0x000B0E92
		public int ReadChar()
		{
			if (!this.IsOpen)
			{
				throw new InvalidOperationException(SR.GetString("Port_not_open"));
			}
			return this.ReadOneChar(this.readTimeout);
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000B2CB8 File Offset: 0x000B0EB8
		private int ReadOneChar(int timeout)
		{
			int num = 0;
			if (this.decoder.GetCharCount(this.inBuffer, this.readPos, this.CachedBytesToRead) != 0)
			{
				int num2 = this.readPos;
				do
				{
					this.readPos++;
				}
				while (this.decoder.GetCharCount(this.inBuffer, num2, this.readPos - num2) < 1);
				try
				{
					this.decoder.GetChars(this.inBuffer, num2, this.readPos - num2, this.oneChar, 0);
				}
				catch
				{
					this.readPos = num2;
					throw;
				}
				return (int)this.oneChar[0];
			}
			if (timeout != 0)
			{
				int tickCount = Environment.TickCount;
				for (;;)
				{
					int num3;
					if (timeout == -1)
					{
						num3 = this.internalSerialStream.ReadByte(-1);
					}
					else
					{
						if (timeout - num < 0)
						{
							break;
						}
						num3 = this.internalSerialStream.ReadByte(timeout - num);
						num = Environment.TickCount - tickCount;
					}
					this.MaybeResizeBuffer(1);
					byte[] array = this.inBuffer;
					int num4 = this.readLen;
					this.readLen = num4 + 1;
					array[num4] = (byte)num3;
					if (this.decoder.GetCharCount(this.inBuffer, this.readPos, this.readLen - this.readPos) >= 1)
					{
						goto Block_8;
					}
				}
				throw new TimeoutException();
				Block_8:
				this.decoder.GetChars(this.inBuffer, this.readPos, this.readLen - this.readPos, this.oneChar, 0);
				this.readLen = (this.readPos = 0);
				return (int)this.oneChar[0];
			}
			int num5 = this.internalSerialStream.BytesToRead;
			if (num5 == 0)
			{
				num5 = 1;
			}
			this.MaybeResizeBuffer(num5);
			this.readLen += this.internalSerialStream.Read(this.inBuffer, this.readLen, num5);
			if (this.ReadBufferIntoChars(this.oneChar, 0, 1, false) == 0)
			{
				throw new TimeoutException();
			}
			return (int)this.oneChar[0];
		}

		/// <summary>Reads a number of characters from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer and writes them into an array of characters at a given offset.</summary>
		/// <param name="buffer">The character array to write the input to.</param>
		/// <param name="offset">The offset in <paramref name="buffer" /> at which to write the characters.</param>
		/// <param name="count">The maximum number of characters to read. Fewer characters are read if <paramref name="count" /> is greater than the number of characters in the input buffer.</param>
		/// <returns>The number of characters read.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the buffer.  
		/// -or-
		///  <paramref name="count" /> is 1 and there is a surrogate character in the buffer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> passed is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open.</exception>
		/// <exception cref="T:System.TimeoutException">No characters were available to read.</exception>
		// Token: 0x060026FC RID: 9980 RVA: 0x000B2E98 File Offset: 0x000B1098
		public int Read(char[] buffer, int offset, int count)
		{
			if (!this.IsOpen)
			{
				throw new InvalidOperationException(SR.GetString("Port_not_open"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.GetString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			return this.InternalRead(buffer, offset, count, this.readTimeout, false);
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000B2F30 File Offset: 0x000B1130
		private int InternalRead(char[] buffer, int offset, int count, int timeout, bool countMultiByteCharsAsOne)
		{
			if (count == 0)
			{
				return 0;
			}
			int tickCount = Environment.TickCount;
			int bytesToRead = this.internalSerialStream.BytesToRead;
			this.MaybeResizeBuffer(bytesToRead);
			this.readLen += this.internalSerialStream.Read(this.inBuffer, this.readLen, bytesToRead);
			int charCount = this.decoder.GetCharCount(this.inBuffer, this.readPos, this.CachedBytesToRead);
			if (charCount > 0)
			{
				return this.ReadBufferIntoChars(buffer, offset, count, countMultiByteCharsAsOne);
			}
			if (timeout == 0)
			{
				throw new TimeoutException();
			}
			int maxByteCount = this.Encoding.GetMaxByteCount(count);
			int num;
			for (;;)
			{
				this.MaybeResizeBuffer(maxByteCount);
				this.readLen += this.internalSerialStream.Read(this.inBuffer, this.readLen, maxByteCount);
				num = this.ReadBufferIntoChars(buffer, offset, count, countMultiByteCharsAsOne);
				if (num > 0)
				{
					break;
				}
				if (timeout != -1 && timeout - SerialPort.GetElapsedTime(Environment.TickCount, tickCount) <= 0)
				{
					goto Block_6;
				}
			}
			return num;
			Block_6:
			throw new TimeoutException();
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000B3024 File Offset: 0x000B1224
		private int ReadBufferIntoChars(char[] buffer, int offset, int count, bool countMultiByteCharsAsOne)
		{
			int num = Math.Min(count, this.CachedBytesToRead);
			DecoderReplacementFallback decoderReplacementFallback = this.encoding.DecoderFallback as DecoderReplacementFallback;
			if (this.encoding.IsSingleByte && this.encoding.GetMaxCharCount(num) == num && decoderReplacementFallback != null && decoderReplacementFallback.MaxCharCount == 1)
			{
				this.decoder.GetChars(this.inBuffer, this.readPos, num, buffer, offset);
				this.readPos += num;
				if (this.readPos == this.readLen)
				{
					this.readPos = (this.readLen = 0);
				}
				return num;
			}
			int num2 = 0;
			int num3 = 0;
			int num4 = this.readPos;
			do
			{
				int num5 = Math.Min(count - num3, this.readLen - this.readPos - num2);
				if (num5 <= 0)
				{
					break;
				}
				num2 += num5;
				num5 = this.readPos + num2 - num4;
				int charCount = this.decoder.GetCharCount(this.inBuffer, num4, num5);
				if (charCount > 0)
				{
					if (num3 + charCount > count && !countMultiByteCharsAsOne)
					{
						break;
					}
					int num6 = num5;
					do
					{
						num6--;
					}
					while (this.decoder.GetCharCount(this.inBuffer, num4, num6) == charCount);
					this.decoder.GetChars(this.inBuffer, num4, num6 + 1, buffer, offset + num3);
					num4 = num4 + num6 + 1;
				}
				num3 += charCount;
			}
			while (num3 < count && num2 < this.CachedBytesToRead);
			this.readPos = num4;
			if (this.readPos == this.readLen)
			{
				this.readPos = (this.readLen = 0);
			}
			return num3;
		}

		/// <summary>Synchronously reads one byte from the <see cref="T:System.IO.Ports.SerialPort" /> input buffer.</summary>
		/// <returns>The byte, cast to an <see cref="T:System.Int32" />, or -1 if the end of the stream has been read.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open.</exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended.  
		/// -or-
		///  No byte was read.</exception>
		// Token: 0x060026FF RID: 9983 RVA: 0x000B31B4 File Offset: 0x000B13B4
		public int ReadByte()
		{
			if (!this.IsOpen)
			{
				throw new InvalidOperationException(SR.GetString("Port_not_open"));
			}
			if (this.readLen != this.readPos)
			{
				byte[] array = this.inBuffer;
				int num = this.readPos;
				this.readPos = num + 1;
				return array[num];
			}
			this.decoder.Reset();
			return this.internalSerialStream.ReadByte();
		}

		/// <summary>Reads all immediately available bytes, based on the encoding, in both the stream and the input buffer of the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
		/// <returns>The contents of the stream and the input buffer of the <see cref="T:System.IO.Ports.SerialPort" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open.</exception>
		// Token: 0x06002700 RID: 9984 RVA: 0x000B3218 File Offset: 0x000B1418
		public string ReadExisting()
		{
			if (!this.IsOpen)
			{
				throw new InvalidOperationException(SR.GetString("Port_not_open"));
			}
			byte[] array = new byte[this.BytesToRead];
			if (this.readPos < this.readLen)
			{
				Buffer.BlockCopy(this.inBuffer, this.readPos, array, 0, this.CachedBytesToRead);
			}
			this.internalSerialStream.Read(array, this.CachedBytesToRead, array.Length - this.CachedBytesToRead);
			Decoder decoder = this.Encoding.GetDecoder();
			int charCount = decoder.GetCharCount(array, 0, array.Length);
			int num = array.Length;
			if (charCount == 0)
			{
				Buffer.BlockCopy(array, 0, this.inBuffer, 0, array.Length);
				this.readPos = 0;
				this.readLen = array.Length;
				return "";
			}
			do
			{
				decoder.Reset();
				num--;
			}
			while (decoder.GetCharCount(array, 0, num) == charCount);
			this.readPos = 0;
			this.readLen = array.Length - (num + 1);
			Buffer.BlockCopy(array, num + 1, this.inBuffer, 0, array.Length - (num + 1));
			return this.Encoding.GetString(array, 0, num + 1);
		}

		/// <summary>Reads up to the <see cref="P:System.IO.Ports.SerialPort.NewLine" /> value in the input buffer.</summary>
		/// <returns>The contents of the input buffer up to the first occurrence of a <see cref="P:System.IO.Ports.SerialPort.NewLine" /> value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open.</exception>
		/// <exception cref="T:System.TimeoutException">The operation did not complete before the time-out period ended.  
		/// -or-
		///  No bytes were read.</exception>
		// Token: 0x06002701 RID: 9985 RVA: 0x000B3324 File Offset: 0x000B1524
		public string ReadLine()
		{
			return this.ReadTo(this.NewLine);
		}

		/// <summary>Reads a string up to the specified <paramref name="value" /> in the input buffer.</summary>
		/// <param name="value">A value that indicates where the read operation stops.</param>
		/// <returns>The contents of the input buffer up to the specified <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentException">The length of the <paramref name="value" /> parameter is 0.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open.</exception>
		/// <exception cref="T:System.TimeoutException">The operation did not complete before the time-out period ended.</exception>
		// Token: 0x06002702 RID: 9986 RVA: 0x000B3334 File Offset: 0x000B1534
		public string ReadTo(string value)
		{
			if (!this.IsOpen)
			{
				throw new InvalidOperationException(SR.GetString("Port_not_open"));
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidNullEmptyArgument", new object[] { "value" }));
			}
			int tickCount = Environment.TickCount;
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			char c = value[value.Length - 1];
			int bytesToRead = this.internalSerialStream.BytesToRead;
			this.MaybeResizeBuffer(bytesToRead);
			this.readLen += this.internalSerialStream.Read(this.inBuffer, this.readLen, bytesToRead);
			int num2 = this.readPos;
			if (this.singleCharBuffer == null)
			{
				this.singleCharBuffer = new char[this.maxByteCountForSingleChar];
			}
			string text2;
			try
			{
				for (;;)
				{
					int num3;
					if (this.readTimeout == -1)
					{
						num3 = this.InternalRead(this.singleCharBuffer, 0, 1, this.readTimeout, true);
					}
					else
					{
						if (this.readTimeout - num < 0)
						{
							break;
						}
						int tickCount2 = Environment.TickCount;
						num3 = this.InternalRead(this.singleCharBuffer, 0, 1, this.readTimeout - num, true);
						num += Environment.TickCount - tickCount2;
					}
					stringBuilder.Append(this.singleCharBuffer, 0, num3);
					if (c == this.singleCharBuffer[num3 - 1] && stringBuilder.Length >= value.Length)
					{
						bool flag = true;
						for (int i = 2; i <= value.Length; i++)
						{
							if (value[value.Length - i] != stringBuilder[stringBuilder.Length - i])
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							goto Block_11;
						}
					}
				}
				throw new TimeoutException();
				Block_11:
				string text = stringBuilder.ToString(0, stringBuilder.Length - value.Length);
				if (this.readPos == this.readLen)
				{
					this.readPos = (this.readLen = 0);
				}
				text2 = text;
			}
			catch
			{
				byte[] bytes = this.encoding.GetBytes(stringBuilder.ToString());
				if (bytes.Length != 0)
				{
					int cachedBytesToRead = this.CachedBytesToRead;
					byte[] array = new byte[cachedBytesToRead];
					if (cachedBytesToRead > 0)
					{
						Buffer.BlockCopy(this.inBuffer, this.readPos, array, 0, cachedBytesToRead);
					}
					this.readPos = 0;
					this.readLen = 0;
					this.MaybeResizeBuffer(bytes.Length + cachedBytesToRead);
					Buffer.BlockCopy(bytes, 0, this.inBuffer, this.readLen, bytes.Length);
					this.readLen += bytes.Length;
					if (cachedBytesToRead > 0)
					{
						Buffer.BlockCopy(array, 0, this.inBuffer, this.readLen, cachedBytesToRead);
						this.readLen += cachedBytesToRead;
					}
				}
				throw;
			}
			return text2;
		}

		/// <summary>Writes the specified string to the serial port.</summary>
		/// <param name="text">The string for output.</param>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="text" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended.</exception>
		// Token: 0x06002703 RID: 9987 RVA: 0x000B35F4 File Offset: 0x000B17F4
		public void Write(string text)
		{
			if (!this.IsOpen)
			{
				throw new InvalidOperationException(SR.GetString("Port_not_open"));
			}
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (text.Length == 0)
			{
				return;
			}
			byte[] bytes = this.encoding.GetBytes(text);
			this.internalSerialStream.Write(bytes, 0, bytes.Length, this.writeTimeout);
		}

		/// <summary>Writes a specified number of characters to the serial port using data from a buffer.</summary>
		/// <param name="buffer">The character array that contains the data to write to the port.</param>
		/// <param name="offset">The zero-based byte offset in the <paramref name="buffer" /> parameter at which to begin copying bytes to the port.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> passed is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended.</exception>
		// Token: 0x06002704 RID: 9988 RVA: 0x000B3654 File Offset: 0x000B1854
		public void Write(char[] buffer, int offset, int count)
		{
			if (!this.IsOpen)
			{
				throw new InvalidOperationException(SR.GetString("Port_not_open"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			if (buffer.Length == 0)
			{
				return;
			}
			byte[] bytes = this.Encoding.GetBytes(buffer, offset, count);
			this.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Writes a specified number of bytes to the serial port using data from a buffer.</summary>
		/// <param name="buffer">The byte array that contains the data to write to the port.</param>
		/// <param name="offset">The zero-based byte offset in the <paramref name="buffer" /> parameter at which to begin copying bytes to the port.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> passed is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> or <paramref name="count" /> parameters are outside a valid region of the <paramref name="buffer" /> being passed. Either <paramref name="offset" /> or <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="count" /> is greater than the length of the <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended.</exception>
		// Token: 0x06002705 RID: 9989 RVA: 0x000B36F0 File Offset: 0x000B18F0
		public void Write(byte[] buffer, int offset, int count)
		{
			if (!this.IsOpen)
			{
				throw new InvalidOperationException(SR.GetString("Port_not_open"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", SR.GetString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			if (buffer.Length == 0)
			{
				return;
			}
			this.internalSerialStream.Write(buffer, offset, count, this.writeTimeout);
		}

		/// <summary>Writes the specified string and the <see cref="P:System.IO.Ports.SerialPort.NewLine" /> value to the output buffer.</summary>
		/// <param name="text">The string to write to the output buffer.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="text" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open.</exception>
		/// <exception cref="T:System.TimeoutException">The <see cref="M:System.IO.Ports.SerialPort.WriteLine(System.String)" /> method could not write to the stream.</exception>
		// Token: 0x06002706 RID: 9990 RVA: 0x000B3790 File Offset: 0x000B1990
		public void WriteLine(string text)
		{
			this.Write(text + this.NewLine);
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000B37A4 File Offset: 0x000B19A4
		private void CatchErrorEvents(object src, SerialErrorReceivedEventArgs e)
		{
			SerialErrorReceivedEventHandler errorReceived = this.ErrorReceived;
			SerialStream serialStream = this.internalSerialStream;
			if (errorReceived != null && serialStream != null)
			{
				SerialStream serialStream2 = serialStream;
				lock (serialStream2)
				{
					if (serialStream.IsOpen)
					{
						errorReceived(this, e);
					}
				}
			}
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000B3800 File Offset: 0x000B1A00
		private void CatchPinChangedEvents(object src, SerialPinChangedEventArgs e)
		{
			SerialPinChangedEventHandler pinChanged = this.PinChanged;
			SerialStream serialStream = this.internalSerialStream;
			if (pinChanged != null && serialStream != null)
			{
				SerialStream serialStream2 = serialStream;
				lock (serialStream2)
				{
					if (serialStream.IsOpen)
					{
						pinChanged(this, e);
					}
				}
			}
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000B385C File Offset: 0x000B1A5C
		private void CatchReceivedEvents(object src, SerialDataReceivedEventArgs e)
		{
			SerialDataReceivedEventHandler dataReceived = this.DataReceived;
			SerialStream serialStream = this.internalSerialStream;
			if (dataReceived != null && serialStream != null)
			{
				SerialStream serialStream2 = serialStream;
				lock (serialStream2)
				{
					bool flag2 = false;
					try
					{
						flag2 = serialStream.IsOpen && (SerialData.Eof == e.EventType || this.BytesToRead >= this.receivedBytesThreshold);
					}
					catch
					{
					}
					finally
					{
						if (flag2)
						{
							dataReceived(this, e);
						}
					}
				}
			}
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000B38FC File Offset: 0x000B1AFC
		private void CompactBuffer()
		{
			Buffer.BlockCopy(this.inBuffer, this.readPos, this.inBuffer, 0, this.CachedBytesToRead);
			this.readLen = this.CachedBytesToRead;
			this.readPos = 0;
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000B3930 File Offset: 0x000B1B30
		private void MaybeResizeBuffer(int additionalByteLength)
		{
			if (additionalByteLength + this.readLen <= this.inBuffer.Length)
			{
				return;
			}
			if (this.CachedBytesToRead + additionalByteLength <= this.inBuffer.Length / 2)
			{
				this.CompactBuffer();
				return;
			}
			int num = Math.Max(this.CachedBytesToRead + additionalByteLength, this.inBuffer.Length * 2);
			byte[] array = new byte[num];
			Buffer.BlockCopy(this.inBuffer, this.readPos, array, 0, this.CachedBytesToRead);
			this.readLen = this.CachedBytesToRead;
			this.readPos = 0;
			this.inBuffer = array;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000B39C0 File Offset: 0x000B1BC0
		private static int GetElapsedTime(int currentTickCount, int startTickCount)
		{
			int num = currentTickCount - startTickCount;
			if (num < 0)
			{
				return int.MaxValue;
			}
			return num;
		}

		/// <summary>Indicates that no time-out should occur.</summary>
		// Token: 0x040020F5 RID: 8437
		public const int InfiniteTimeout = -1;

		// Token: 0x040020F6 RID: 8438
		private const int defaultDataBits = 8;

		// Token: 0x040020F7 RID: 8439
		private const Parity defaultParity = Parity.None;

		// Token: 0x040020F8 RID: 8440
		private const StopBits defaultStopBits = StopBits.One;

		// Token: 0x040020F9 RID: 8441
		private const Handshake defaultHandshake = Handshake.None;

		// Token: 0x040020FA RID: 8442
		private const int defaultBufferSize = 1024;

		// Token: 0x040020FB RID: 8443
		private const string defaultPortName = "COM1";

		// Token: 0x040020FC RID: 8444
		private const int defaultBaudRate = 9600;

		// Token: 0x040020FD RID: 8445
		private const bool defaultDtrEnable = false;

		// Token: 0x040020FE RID: 8446
		private const bool defaultRtsEnable = false;

		// Token: 0x040020FF RID: 8447
		private const bool defaultDiscardNull = false;

		// Token: 0x04002100 RID: 8448
		private const byte defaultParityReplace = 63;

		// Token: 0x04002101 RID: 8449
		private const int defaultReceivedBytesThreshold = 1;

		// Token: 0x04002102 RID: 8450
		private const int defaultReadTimeout = -1;

		// Token: 0x04002103 RID: 8451
		private const int defaultWriteTimeout = -1;

		// Token: 0x04002104 RID: 8452
		private const int defaultReadBufferSize = 4096;

		// Token: 0x04002105 RID: 8453
		private const int defaultWriteBufferSize = 2048;

		// Token: 0x04002106 RID: 8454
		private const int maxDataBits = 8;

		// Token: 0x04002107 RID: 8455
		private const int minDataBits = 5;

		// Token: 0x04002108 RID: 8456
		private const string defaultNewLine = "\n";

		// Token: 0x04002109 RID: 8457
		private const string SERIAL_NAME = "\\Device\\Serial";

		// Token: 0x0400210A RID: 8458
		private int baudRate = 9600;

		// Token: 0x0400210B RID: 8459
		private int dataBits = 8;

		// Token: 0x0400210C RID: 8460
		private Parity parity;

		// Token: 0x0400210D RID: 8461
		private StopBits stopBits = StopBits.One;

		// Token: 0x0400210E RID: 8462
		private string portName = "COM1";

		// Token: 0x0400210F RID: 8463
		private Encoding encoding = Encoding.ASCII;

		// Token: 0x04002110 RID: 8464
		private Decoder decoder = Encoding.ASCII.GetDecoder();

		// Token: 0x04002111 RID: 8465
		private int maxByteCountForSingleChar = Encoding.ASCII.GetMaxByteCount(1);

		// Token: 0x04002112 RID: 8466
		private Handshake handshake;

		// Token: 0x04002113 RID: 8467
		private int readTimeout = -1;

		// Token: 0x04002114 RID: 8468
		private int writeTimeout = -1;

		// Token: 0x04002115 RID: 8469
		private int receivedBytesThreshold = 1;

		// Token: 0x04002116 RID: 8470
		private bool discardNull;

		// Token: 0x04002117 RID: 8471
		private bool dtrEnable;

		// Token: 0x04002118 RID: 8472
		private bool rtsEnable;

		// Token: 0x04002119 RID: 8473
		private byte parityReplace = 63;

		// Token: 0x0400211A RID: 8474
		private string newLine = "\n";

		// Token: 0x0400211B RID: 8475
		private int readBufferSize = 4096;

		// Token: 0x0400211C RID: 8476
		private int writeBufferSize = 2048;

		// Token: 0x0400211D RID: 8477
		private SerialStream internalSerialStream;

		// Token: 0x0400211E RID: 8478
		private byte[] inBuffer = new byte[1024];

		// Token: 0x0400211F RID: 8479
		private int readPos;

		// Token: 0x04002120 RID: 8480
		private int readLen;

		// Token: 0x04002121 RID: 8481
		private char[] oneChar = new char[1];

		// Token: 0x04002122 RID: 8482
		private char[] singleCharBuffer;
	}
}
