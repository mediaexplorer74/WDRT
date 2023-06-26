using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Media
{
	/// <summary>Controls playback of a sound from a .wav file.</summary>
	// Token: 0x020003A4 RID: 932
	[ToolboxItem(false)]
	[HostProtection(SecurityAction.LinkDemand, UI = true)]
	[Serializable]
	public class SoundPlayer : Component, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class.</summary>
		// Token: 0x060022A0 RID: 8864 RVA: 0x000A4D0B File Offset: 0x000A2F0B
		public SoundPlayer()
		{
			this.loadAsyncOperationCompleted = new SendOrPostCallback(this.LoadAsyncOperationCompleted);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class, and attaches the specified .wav file.</summary>
		/// <param name="soundLocation">The location of a .wav file to load.</param>
		/// <exception cref="T:System.UriFormatException">The URL value specified by <paramref name="soundLocation" /> cannot be resolved.</exception>
		// Token: 0x060022A1 RID: 8865 RVA: 0x000A4D47 File Offset: 0x000A2F47
		public SoundPlayer(string soundLocation)
			: this()
		{
			if (soundLocation == null)
			{
				soundLocation = string.Empty;
			}
			this.SetupSoundLocation(soundLocation);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class, and attaches the .wav file within the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> to a .wav file.</param>
		// Token: 0x060022A2 RID: 8866 RVA: 0x000A4D60 File Offset: 0x000A2F60
		public SoundPlayer(Stream stream)
			: this()
		{
			this.stream = stream;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		/// <exception cref="T:System.UriFormatException">The <see cref="P:System.Media.SoundPlayer.SoundLocation" /> specified in <paramref name="serializationInfo" /> cannot be resolved.</exception>
		// Token: 0x060022A3 RID: 8867 RVA: 0x000A4D70 File Offset: 0x000A2F70
		protected SoundPlayer(SerializationInfo serializationInfo, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in serializationInfo)
			{
				string name = serializationEntry.Name;
				if (!(name == "SoundLocation"))
				{
					if (!(name == "Stream"))
					{
						if (name == "LoadTimeout")
						{
							this.LoadTimeout = (int)serializationEntry.Value;
						}
					}
					else
					{
						this.stream = (Stream)serializationEntry.Value;
						if (this.stream.CanSeek)
						{
							this.stream.Seek(0L, SeekOrigin.Begin);
						}
					}
				}
				else
				{
					this.SetupSoundLocation((string)serializationEntry.Value);
				}
			}
		}

		/// <summary>Gets or sets the time, in milliseconds, in which the .wav file must load.</summary>
		/// <returns>The number of milliseconds to wait. The default is 10000 (10 seconds).</returns>
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x000A4E4A File Offset: 0x000A304A
		// (set) Token: 0x060022A5 RID: 8869 RVA: 0x000A4E52 File Offset: 0x000A3052
		public int LoadTimeout
		{
			get
			{
				return this.loadTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("LoadTimeout", value, SR.GetString("SoundAPILoadTimeout"));
				}
				this.loadTimeout = value;
			}
		}

		/// <summary>Gets or sets the file path or URL of the .wav file to load.</summary>
		/// <returns>The file path or URL from which to load a .wav file, or <see cref="F:System.String.Empty" /> if no file path is present. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x000A4E7C File Offset: 0x000A307C
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x000A4EBE File Offset: 0x000A30BE
		public string SoundLocation
		{
			get
			{
				if (this.uri != null && this.uri.IsFile)
				{
					new FileIOPermission(PermissionState.None)
					{
						AllFiles = FileIOPermissionAccess.PathDiscovery
					}.Demand();
				}
				return this.soundLocation;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.soundLocation.Equals(value))
				{
					return;
				}
				this.SetupSoundLocation(value);
				this.OnSoundLocationChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.IO.Stream" /> from which to load the .wav file.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> from which to load the .wav file, or <see langword="null" /> if no stream is available. The default is <see langword="null" />.</returns>
		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x000A4EEB File Offset: 0x000A30EB
		// (set) Token: 0x060022A9 RID: 8873 RVA: 0x000A4F03 File Offset: 0x000A3103
		public Stream Stream
		{
			get
			{
				if (this.uri != null)
				{
					return null;
				}
				return this.stream;
			}
			set
			{
				if (this.stream == value)
				{
					return;
				}
				this.SetupStream(value);
				this.OnStreamChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets a value indicating whether loading of a .wav file has successfully completed.</summary>
		/// <returns>
		///   <see langword="true" /> if a .wav file is loaded; <see langword="false" /> if a .wav file has not yet been loaded.</returns>
		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x000A4F21 File Offset: 0x000A3121
		public bool IsLoadCompleted
		{
			get
			{
				return this.isLoadCompleted;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Media.SoundPlayer" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Media.SoundPlayer" />.</returns>
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x000A4F29 File Offset: 0x000A3129
		// (set) Token: 0x060022AC RID: 8876 RVA: 0x000A4F31 File Offset: 0x000A3131
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Loads a .wav file from a stream or a Web resource using a new thread.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		// Token: 0x060022AD RID: 8877 RVA: 0x000A4F3C File Offset: 0x000A313C
		public void LoadAsync()
		{
			if (this.uri != null && this.uri.IsFile)
			{
				this.isLoadCompleted = true;
				FileInfo fileInfo = new FileInfo(this.uri.LocalPath);
				if (!fileInfo.Exists)
				{
					throw new FileNotFoundException(SR.GetString("SoundAPIFileDoesNotExist"), this.soundLocation);
				}
				this.OnLoadCompleted(new AsyncCompletedEventArgs(null, false, null));
				return;
			}
			else
			{
				if (this.copyThread != null && this.copyThread.ThreadState == ThreadState.Running)
				{
					return;
				}
				this.isLoadCompleted = false;
				this.streamData = null;
				this.currentPos = 0;
				this.asyncOperation = AsyncOperationManager.CreateOperation(null);
				this.LoadStream(false);
				return;
			}
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000A4FE7 File Offset: 0x000A31E7
		private void LoadAsyncOperationCompleted(object arg)
		{
			this.OnLoadCompleted((AsyncCompletedEventArgs)arg);
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000A4FF5 File Offset: 0x000A31F5
		private void CleanupStreamData()
		{
			this.currentPos = 0;
			this.streamData = null;
			this.isLoadCompleted = false;
			this.lastLoadException = null;
			this.doesLoadAppearSynchronous = false;
			this.copyThread = null;
			this.semaphore.Set();
		}

		/// <summary>Loads a sound synchronously.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		// Token: 0x060022B0 RID: 8880 RVA: 0x000A5030 File Offset: 0x000A3230
		public void Load()
		{
			if (!(this.uri != null) || !this.uri.IsFile)
			{
				this.LoadSync();
				return;
			}
			FileInfo fileInfo = new FileInfo(this.uri.LocalPath);
			if (!fileInfo.Exists)
			{
				throw new FileNotFoundException(SR.GetString("SoundAPIFileDoesNotExist"), this.soundLocation);
			}
			this.isLoadCompleted = true;
			this.OnLoadCompleted(new AsyncCompletedEventArgs(null, false, null));
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000A50A4 File Offset: 0x000A32A4
		private void LoadAndPlay(int flags)
		{
			if (string.IsNullOrEmpty(this.soundLocation) && this.stream == null)
			{
				SystemSounds.Beep.Play();
				return;
			}
			if (this.uri != null && this.uri.IsFile)
			{
				string localPath = this.uri.LocalPath;
				FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.Read, localPath);
				fileIOPermission.Demand();
				this.isLoadCompleted = true;
				SoundPlayer.IntSecurity.SafeSubWindows.Demand();
				System.ComponentModel.IntSecurity.UnmanagedCode.Assert();
				try
				{
					this.ValidateSoundFile(localPath);
					SoundPlayer.UnsafeNativeMethods.PlaySound(localPath, IntPtr.Zero, 2 | flags);
					return;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			this.LoadSync();
			SoundPlayer.ValidateSoundData(this.streamData);
			SoundPlayer.IntSecurity.SafeSubWindows.Demand();
			System.ComponentModel.IntSecurity.UnmanagedCode.Assert();
			try
			{
				SoundPlayer.UnsafeNativeMethods.PlaySound(this.streamData, IntPtr.Zero, 6 | flags);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000A519C File Offset: 0x000A339C
		private void LoadSync()
		{
			if (!this.semaphore.WaitOne(this.LoadTimeout, false))
			{
				if (this.copyThread != null)
				{
					this.copyThread.Abort();
				}
				this.CleanupStreamData();
				throw new TimeoutException(SR.GetString("SoundAPILoadTimedOut"));
			}
			if (this.streamData != null)
			{
				return;
			}
			if (this.uri != null && !this.uri.IsFile && this.stream == null)
			{
				WebPermission webPermission = new WebPermission(NetworkAccess.Connect, this.uri.AbsolutePath);
				webPermission.Demand();
				WebRequest webRequest = WebRequest.Create(this.uri);
				webRequest.Timeout = this.LoadTimeout;
				WebResponse response = webRequest.GetResponse();
				this.stream = response.GetResponseStream();
			}
			if (this.stream.CanSeek)
			{
				this.LoadStream(true);
			}
			else
			{
				this.doesLoadAppearSynchronous = true;
				this.LoadStream(false);
				if (!this.semaphore.WaitOne(this.LoadTimeout, false))
				{
					if (this.copyThread != null)
					{
						this.copyThread.Abort();
					}
					this.CleanupStreamData();
					throw new TimeoutException(SR.GetString("SoundAPILoadTimedOut"));
				}
				this.doesLoadAppearSynchronous = false;
				if (this.lastLoadException != null)
				{
					throw this.lastLoadException;
				}
			}
			this.copyThread = null;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000A52D4 File Offset: 0x000A34D4
		private void LoadStream(bool loadSync)
		{
			if (loadSync && this.stream.CanSeek)
			{
				int num = (int)this.stream.Length;
				this.currentPos = 0;
				this.streamData = new byte[num];
				this.stream.Read(this.streamData, 0, num);
				this.isLoadCompleted = true;
				this.OnLoadCompleted(new AsyncCompletedEventArgs(null, false, null));
				return;
			}
			this.semaphore.Reset();
			this.copyThread = new Thread(new ThreadStart(this.WorkerThread));
			this.copyThread.Start();
		}

		/// <summary>Plays the .wav file using a new thread, and loads the .wav file first if it has not been loaded.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The .wav header is corrupted; the file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> is not a PCM .wav file.</exception>
		// Token: 0x060022B4 RID: 8884 RVA: 0x000A5369 File Offset: 0x000A3569
		public void Play()
		{
			this.LoadAndPlay(1);
		}

		/// <summary>Plays the .wav file and loads the .wav file first if it has not been loaded.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The .wav header is corrupted; the file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> is not a PCM .wav file.</exception>
		// Token: 0x060022B5 RID: 8885 RVA: 0x000A5372 File Offset: 0x000A3572
		public void PlaySync()
		{
			this.LoadAndPlay(0);
		}

		/// <summary>Plays and loops the .wav file using a new thread, and loads the .wav file first if it has not been loaded.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The .wav header is corrupted; the file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> is not a PCM .wav file.</exception>
		// Token: 0x060022B6 RID: 8886 RVA: 0x000A537B File Offset: 0x000A357B
		public void PlayLooping()
		{
			this.LoadAndPlay(9);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000A5388 File Offset: 0x000A3588
		private static Uri ResolveUri(string partialUri)
		{
			Uri uri = null;
			try
			{
				uri = new Uri(partialUri);
			}
			catch (UriFormatException)
			{
			}
			if (uri == null)
			{
				try
				{
					uri = new Uri(Path.GetFullPath(partialUri));
				}
				catch (UriFormatException)
				{
				}
			}
			return uri;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000A53DC File Offset: 0x000A35DC
		private void SetupSoundLocation(string soundLocation)
		{
			if (this.copyThread != null)
			{
				this.copyThread.Abort();
				this.CleanupStreamData();
			}
			this.uri = SoundPlayer.ResolveUri(soundLocation);
			this.soundLocation = soundLocation;
			this.stream = null;
			if (this.uri == null)
			{
				if (!string.IsNullOrEmpty(soundLocation))
				{
					throw new UriFormatException(SR.GetString("SoundAPIBadSoundLocation"));
				}
			}
			else if (!this.uri.IsFile)
			{
				this.streamData = null;
				this.currentPos = 0;
				this.isLoadCompleted = false;
			}
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000A5464 File Offset: 0x000A3664
		private void SetupStream(Stream stream)
		{
			if (this.copyThread != null)
			{
				this.copyThread.Abort();
				this.CleanupStreamData();
			}
			this.stream = stream;
			this.soundLocation = string.Empty;
			this.streamData = null;
			this.currentPos = 0;
			this.isLoadCompleted = false;
			if (stream != null)
			{
				this.uri = null;
			}
		}

		/// <summary>Stops playback of the sound if playback is occurring.</summary>
		// Token: 0x060022BA RID: 8890 RVA: 0x000A54BB File Offset: 0x000A36BB
		public void Stop()
		{
			SoundPlayer.IntSecurity.SafeSubWindows.Demand();
			SoundPlayer.UnsafeNativeMethods.PlaySound(null, IntPtr.Zero, 64);
		}

		/// <summary>Occurs when a .wav file has been successfully or unsuccessfully loaded.</summary>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060022BB RID: 8891 RVA: 0x000A54D5 File Offset: 0x000A36D5
		// (remove) Token: 0x060022BC RID: 8892 RVA: 0x000A54E8 File Offset: 0x000A36E8
		public event AsyncCompletedEventHandler LoadCompleted
		{
			add
			{
				base.Events.AddHandler(SoundPlayer.EventLoadCompleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(SoundPlayer.EventLoadCompleted, value);
			}
		}

		/// <summary>Occurs when a new audio source path for this <see cref="T:System.Media.SoundPlayer" /> has been set.</summary>
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060022BD RID: 8893 RVA: 0x000A54FB File Offset: 0x000A36FB
		// (remove) Token: 0x060022BE RID: 8894 RVA: 0x000A550E File Offset: 0x000A370E
		public event EventHandler SoundLocationChanged
		{
			add
			{
				base.Events.AddHandler(SoundPlayer.EventSoundLocationChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(SoundPlayer.EventSoundLocationChanged, value);
			}
		}

		/// <summary>Occurs when a new <see cref="T:System.IO.Stream" /> audio source for this <see cref="T:System.Media.SoundPlayer" /> has been set.</summary>
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060022BF RID: 8895 RVA: 0x000A5521 File Offset: 0x000A3721
		// (remove) Token: 0x060022C0 RID: 8896 RVA: 0x000A5534 File Offset: 0x000A3734
		public event EventHandler StreamChanged
		{
			add
			{
				base.Events.AddHandler(SoundPlayer.EventStreamChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(SoundPlayer.EventStreamChanged, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Media.SoundPlayer.LoadCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> that contains the event data.</param>
		// Token: 0x060022C1 RID: 8897 RVA: 0x000A5548 File Offset: 0x000A3748
		protected virtual void OnLoadCompleted(AsyncCompletedEventArgs e)
		{
			AsyncCompletedEventHandler asyncCompletedEventHandler = (AsyncCompletedEventHandler)base.Events[SoundPlayer.EventLoadCompleted];
			if (asyncCompletedEventHandler != null)
			{
				asyncCompletedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Media.SoundPlayer.SoundLocationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060022C2 RID: 8898 RVA: 0x000A5578 File Offset: 0x000A3778
		protected virtual void OnSoundLocationChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[SoundPlayer.EventSoundLocationChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Media.SoundPlayer.StreamChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060022C3 RID: 8899 RVA: 0x000A55A8 File Offset: 0x000A37A8
		protected virtual void OnStreamChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[SoundPlayer.EventStreamChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000A55D8 File Offset: 0x000A37D8
		private void WorkerThread()
		{
			try
			{
				if (this.uri != null && !this.uri.IsFile && this.stream == null)
				{
					WebRequest webRequest = WebRequest.Create(this.uri);
					WebResponse response = webRequest.GetResponse();
					this.stream = response.GetResponseStream();
				}
				this.streamData = new byte[1024];
				int i = this.stream.Read(this.streamData, this.currentPos, 1024);
				int num = i;
				while (i > 0)
				{
					this.currentPos += i;
					if (this.streamData.Length < this.currentPos + 1024)
					{
						byte[] array = new byte[this.streamData.Length * 2];
						Array.Copy(this.streamData, array, this.streamData.Length);
						this.streamData = array;
					}
					i = this.stream.Read(this.streamData, this.currentPos, 1024);
					num += i;
				}
				this.lastLoadException = null;
			}
			catch (Exception ex)
			{
				this.lastLoadException = ex;
			}
			if (!this.doesLoadAppearSynchronous)
			{
				this.asyncOperation.PostOperationCompleted(this.loadAsyncOperationCompleted, new AsyncCompletedEventArgs(this.lastLoadException, false, null));
			}
			this.isLoadCompleted = true;
			this.semaphore.Set();
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000A5730 File Offset: 0x000A3930
		private unsafe void ValidateSoundFile(string fileName)
		{
			SoundPlayer.NativeMethods.MMCKINFO mmckinfo = new SoundPlayer.NativeMethods.MMCKINFO();
			SoundPlayer.NativeMethods.MMCKINFO mmckinfo2 = new SoundPlayer.NativeMethods.MMCKINFO();
			SoundPlayer.NativeMethods.WAVEFORMATEX waveformatex = null;
			IntPtr intPtr = SoundPlayer.UnsafeNativeMethods.mmioOpen(fileName, IntPtr.Zero, 65536);
			if (intPtr == IntPtr.Zero)
			{
				throw new FileNotFoundException(SR.GetString("SoundAPIFileDoesNotExist"), this.soundLocation);
			}
			try
			{
				mmckinfo.fccType = SoundPlayer.mmioFOURCC('W', 'A', 'V', 'E');
				if (SoundPlayer.UnsafeNativeMethods.mmioDescend(intPtr, mmckinfo, null, 32) != 0)
				{
					throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveFile", new object[] { this.soundLocation }));
				}
				while (SoundPlayer.UnsafeNativeMethods.mmioDescend(intPtr, mmckinfo2, mmckinfo, 0) == 0)
				{
					if (mmckinfo2.dwDataOffset + mmckinfo2.cksize > mmckinfo.dwDataOffset + mmckinfo.cksize)
					{
						throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
					}
					if (mmckinfo2.ckID == SoundPlayer.mmioFOURCC('f', 'm', 't', ' ') && waveformatex == null)
					{
						int num = mmckinfo2.cksize;
						if (num < Marshal.SizeOf(typeof(SoundPlayer.NativeMethods.WAVEFORMATEX)))
						{
							num = Marshal.SizeOf(typeof(SoundPlayer.NativeMethods.WAVEFORMATEX));
						}
						waveformatex = new SoundPlayer.NativeMethods.WAVEFORMATEX();
						byte[] array = new byte[num];
						if (SoundPlayer.UnsafeNativeMethods.mmioRead(intPtr, array, num) != num)
						{
							throw new InvalidOperationException(SR.GetString("SoundAPIReadError", new object[] { this.soundLocation }));
						}
						try
						{
							byte[] array2;
							byte* ptr;
							if ((array2 = array) == null || array2.Length == 0)
							{
								ptr = null;
							}
							else
							{
								ptr = &array2[0];
							}
							Marshal.PtrToStructure((IntPtr)((void*)ptr), waveformatex);
						}
						finally
						{
							byte[] array2 = null;
						}
					}
					SoundPlayer.UnsafeNativeMethods.mmioAscend(intPtr, mmckinfo2, 0);
				}
				if (waveformatex == null)
				{
					throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
				}
				if (waveformatex.wFormatTag != 1 && waveformatex.wFormatTag != 2 && waveformatex.wFormatTag != 3)
				{
					throw new InvalidOperationException(SR.GetString("SoundAPIFormatNotSupported"));
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SoundPlayer.UnsafeNativeMethods.mmioClose(intPtr, 0);
				}
			}
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000A5948 File Offset: 0x000A3B48
		private static void ValidateSoundData(byte[] data)
		{
			short num = -1;
			bool flag = false;
			if (data.Length < 12)
			{
				throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
			}
			if (data[0] != 82 || data[1] != 73 || data[2] != 70 || data[3] != 70)
			{
				throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
			}
			if (data[8] != 87 || data[9] != 65 || data[10] != 86 || data[11] != 69)
			{
				throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
			}
			int num2 = 12;
			int num3 = data.Length;
			while (!flag && num2 < num3 - 8)
			{
				if (data[num2] == 102 && data[num2 + 1] == 109 && data[num2 + 2] == 116 && data[num2 + 3] == 32)
				{
					flag = true;
					int num4 = SoundPlayer.BytesToInt(data[num2 + 7], data[num2 + 6], data[num2 + 5], data[num2 + 4]);
					int num5 = 16;
					if (num4 != num5)
					{
						int num6 = 18;
						if (num3 < num2 + 8 + num6 - 1)
						{
							throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
						}
						short num7 = SoundPlayer.BytesToInt16(data[num2 + 8 + num6 - 1], data[num2 + 8 + num6 - 2]);
						if ((int)num7 + num6 != num4)
						{
							throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
						}
					}
					if (num3 < num2 + 9)
					{
						throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
					}
					num = SoundPlayer.BytesToInt16(data[num2 + 9], data[num2 + 8]);
					num2 += num4 + 8;
				}
				else
				{
					num2 += 8 + SoundPlayer.BytesToInt(data[num2 + 7], data[num2 + 6], data[num2 + 5], data[num2 + 4]);
				}
			}
			if (!flag)
			{
				throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
			}
			if (num != 1 && num != 2 && num != 3)
			{
				throw new InvalidOperationException(SR.GetString("SoundAPIFormatNotSupported"));
			}
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000A5B0C File Offset: 0x000A3D0C
		private static short BytesToInt16(byte ch0, byte ch1)
		{
			int num = (int)ch1 | ((int)ch0 << 8);
			return (short)num;
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000A5B23 File Offset: 0x000A3D23
		private static int BytesToInt(byte ch0, byte ch1, byte ch2, byte ch3)
		{
			return SoundPlayer.mmioFOURCC((char)ch3, (char)ch2, (char)ch1, (char)ch0);
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000A5B30 File Offset: 0x000A3D30
		private static int mmioFOURCC(char ch0, char ch1, char ch2, char ch3)
		{
			int num = 0;
			num |= (int)ch0;
			num |= (int)((int)ch1 << 8);
			num |= (int)((int)ch2 << 16);
			return num | (int)((int)ch3 << 24);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x060022CA RID: 8906 RVA: 0x000A5B58 File Offset: 0x000A3D58
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (!string.IsNullOrEmpty(this.soundLocation))
			{
				info.AddValue("SoundLocation", this.soundLocation);
			}
			if (this.stream != null)
			{
				info.AddValue("Stream", this.stream);
			}
			info.AddValue("LoadTimeout", this.loadTimeout);
		}

		// Token: 0x04001F87 RID: 8071
		private const int blockSize = 1024;

		// Token: 0x04001F88 RID: 8072
		private const int defaultLoadTimeout = 10000;

		// Token: 0x04001F89 RID: 8073
		private Uri uri;

		// Token: 0x04001F8A RID: 8074
		private string soundLocation = string.Empty;

		// Token: 0x04001F8B RID: 8075
		private int loadTimeout = 10000;

		// Token: 0x04001F8C RID: 8076
		private object tag;

		// Token: 0x04001F8D RID: 8077
		private ManualResetEvent semaphore = new ManualResetEvent(true);

		// Token: 0x04001F8E RID: 8078
		private Thread copyThread;

		// Token: 0x04001F8F RID: 8079
		private int currentPos;

		// Token: 0x04001F90 RID: 8080
		private Stream stream;

		// Token: 0x04001F91 RID: 8081
		private bool isLoadCompleted;

		// Token: 0x04001F92 RID: 8082
		private Exception lastLoadException;

		// Token: 0x04001F93 RID: 8083
		private bool doesLoadAppearSynchronous;

		// Token: 0x04001F94 RID: 8084
		private byte[] streamData;

		// Token: 0x04001F95 RID: 8085
		private AsyncOperation asyncOperation;

		// Token: 0x04001F96 RID: 8086
		private readonly SendOrPostCallback loadAsyncOperationCompleted;

		// Token: 0x04001F97 RID: 8087
		private static readonly object EventLoadCompleted = new object();

		// Token: 0x04001F98 RID: 8088
		private static readonly object EventSoundLocationChanged = new object();

		// Token: 0x04001F99 RID: 8089
		private static readonly object EventStreamChanged = new object();

		// Token: 0x020007E1 RID: 2017
		private class IntSecurity
		{
			// Token: 0x060043AD RID: 17325 RVA: 0x0011CE32 File Offset: 0x0011B032
			private IntSecurity()
			{
			}

			// Token: 0x17000F50 RID: 3920
			// (get) Token: 0x060043AE RID: 17326 RVA: 0x0011CE3A File Offset: 0x0011B03A
			internal static CodeAccessPermission SafeSubWindows
			{
				get
				{
					if (SoundPlayer.IntSecurity.safeSubWindows == null)
					{
						SoundPlayer.IntSecurity.safeSubWindows = new UIPermission(UIPermissionWindow.SafeSubWindows);
					}
					return SoundPlayer.IntSecurity.safeSubWindows;
				}
			}

			// Token: 0x040034C4 RID: 13508
			private static volatile CodeAccessPermission safeSubWindows;
		}

		// Token: 0x020007E2 RID: 2018
		private class NativeMethods
		{
			// Token: 0x060043AF RID: 17327 RVA: 0x0011CE59 File Offset: 0x0011B059
			private NativeMethods()
			{
			}

			// Token: 0x040034C5 RID: 13509
			internal const int WAVE_FORMAT_PCM = 1;

			// Token: 0x040034C6 RID: 13510
			internal const int WAVE_FORMAT_ADPCM = 2;

			// Token: 0x040034C7 RID: 13511
			internal const int WAVE_FORMAT_IEEE_FLOAT = 3;

			// Token: 0x040034C8 RID: 13512
			internal const int MMIO_READ = 0;

			// Token: 0x040034C9 RID: 13513
			internal const int MMIO_ALLOCBUF = 65536;

			// Token: 0x040034CA RID: 13514
			internal const int MMIO_FINDRIFF = 32;

			// Token: 0x040034CB RID: 13515
			internal const int SND_SYNC = 0;

			// Token: 0x040034CC RID: 13516
			internal const int SND_ASYNC = 1;

			// Token: 0x040034CD RID: 13517
			internal const int SND_NODEFAULT = 2;

			// Token: 0x040034CE RID: 13518
			internal const int SND_MEMORY = 4;

			// Token: 0x040034CF RID: 13519
			internal const int SND_LOOP = 8;

			// Token: 0x040034D0 RID: 13520
			internal const int SND_PURGE = 64;

			// Token: 0x040034D1 RID: 13521
			internal const int SND_FILENAME = 131072;

			// Token: 0x040034D2 RID: 13522
			internal const int SND_NOSTOP = 16;

			// Token: 0x02000920 RID: 2336
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			internal class MMCKINFO
			{
				// Token: 0x04003D8D RID: 15757
				internal int ckID;

				// Token: 0x04003D8E RID: 15758
				internal int cksize;

				// Token: 0x04003D8F RID: 15759
				internal int fccType;

				// Token: 0x04003D90 RID: 15760
				internal int dwDataOffset;

				// Token: 0x04003D91 RID: 15761
				internal int dwFlags;
			}

			// Token: 0x02000921 RID: 2337
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			internal class WAVEFORMATEX
			{
				// Token: 0x04003D92 RID: 15762
				internal short wFormatTag;

				// Token: 0x04003D93 RID: 15763
				internal short nChannels;

				// Token: 0x04003D94 RID: 15764
				internal int nSamplesPerSec;

				// Token: 0x04003D95 RID: 15765
				internal int nAvgBytesPerSec;

				// Token: 0x04003D96 RID: 15766
				internal short nBlockAlign;

				// Token: 0x04003D97 RID: 15767
				internal short wBitsPerSample;

				// Token: 0x04003D98 RID: 15768
				internal short cbSize;
			}
		}

		// Token: 0x020007E3 RID: 2019
		private class UnsafeNativeMethods
		{
			// Token: 0x060043B0 RID: 17328 RVA: 0x0011CE61 File Offset: 0x0011B061
			private UnsafeNativeMethods()
			{
			}

			// Token: 0x060043B1 RID: 17329
			[DllImport("winmm.dll", CharSet = CharSet.Auto)]
			internal static extern bool PlaySound([MarshalAs(UnmanagedType.LPWStr)] string soundName, IntPtr hmod, int soundFlags);

			// Token: 0x060043B2 RID: 17330
			[DllImport("winmm.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			internal static extern bool PlaySound(byte[] soundName, IntPtr hmod, int soundFlags);

			// Token: 0x060043B3 RID: 17331
			[DllImport("winmm.dll", CharSet = CharSet.Auto)]
			internal static extern IntPtr mmioOpen(string fileName, IntPtr not_used, int flags);

			// Token: 0x060043B4 RID: 17332
			[DllImport("winmm.dll", CharSet = CharSet.Auto)]
			internal static extern int mmioAscend(IntPtr hMIO, SoundPlayer.NativeMethods.MMCKINFO lpck, int flags);

			// Token: 0x060043B5 RID: 17333
			[DllImport("winmm.dll", CharSet = CharSet.Auto)]
			internal static extern int mmioDescend(IntPtr hMIO, [MarshalAs(UnmanagedType.LPStruct)] SoundPlayer.NativeMethods.MMCKINFO lpck, [MarshalAs(UnmanagedType.LPStruct)] SoundPlayer.NativeMethods.MMCKINFO lcpkParent, int flags);

			// Token: 0x060043B6 RID: 17334
			[DllImport("winmm.dll", CharSet = CharSet.Auto)]
			internal static extern int mmioRead(IntPtr hMIO, [MarshalAs(UnmanagedType.LPArray)] byte[] wf, int cch);

			// Token: 0x060043B7 RID: 17335
			[DllImport("winmm.dll", CharSet = CharSet.Auto)]
			internal static extern int mmioClose(IntPtr hMIO, int flags);
		}
	}
}
