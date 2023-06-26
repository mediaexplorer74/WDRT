using System;
using System.Deployment.Internal.Isolation.Manifest;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000064 RID: 100
	internal class ApplicationContext
	{
		// Token: 0x060001EA RID: 490 RVA: 0x000084E1 File Offset: 0x000066E1
		internal ApplicationContext(IActContext a)
		{
			if (a == null)
			{
				throw new ArgumentNullException();
			}
			this._appcontext = a;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000084F9 File Offset: 0x000066F9
		public ApplicationContext(DefinitionAppId appid)
		{
			if (appid == null)
			{
				throw new ArgumentNullException();
			}
			this._appcontext = IsolationInterop.CreateActContext(appid._id);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000851B File Offset: 0x0000671B
		public ApplicationContext(ReferenceAppId appid)
		{
			if (appid == null)
			{
				throw new ArgumentNullException();
			}
			this._appcontext = IsolationInterop.CreateActContext(appid._id);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00008540 File Offset: 0x00006740
		public DefinitionAppId Identity
		{
			get
			{
				object obj;
				this._appcontext.GetAppId(out obj);
				return new DefinitionAppId(obj as IDefinitionAppId);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00008568 File Offset: 0x00006768
		public string BasePath
		{
			get
			{
				string text;
				this._appcontext.ApplicationBasePath(0U, out text);
				return text;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008584 File Offset: 0x00006784
		public string ReplaceStrings(string culture, string toreplace)
		{
			string text;
			this._appcontext.ReplaceStringMacros(0U, culture, toreplace, out text);
			return text;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000085A4 File Offset: 0x000067A4
		internal ICMS GetComponentManifest(DefinitionIdentity component)
		{
			object obj;
			this._appcontext.GetComponentManifest(0U, component._id, ref IsolationInterop.IID_ICMS, out obj);
			return obj as ICMS;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000085D0 File Offset: 0x000067D0
		internal string GetComponentManifestPath(DefinitionIdentity component)
		{
			object obj;
			this._appcontext.GetComponentManifest(0U, component._id, ref IsolationInterop.IID_IManifestInformation, out obj);
			string text;
			((IManifestInformation)obj).get_FullPath(out text);
			return text;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008604 File Offset: 0x00006804
		public string GetComponentPath(DefinitionIdentity component)
		{
			string text;
			this._appcontext.GetComponentPayloadPath(0U, component._id, out text);
			return text;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008628 File Offset: 0x00006828
		public DefinitionIdentity MatchReference(ReferenceIdentity TheRef)
		{
			object obj;
			this._appcontext.FindReferenceInContext(0U, TheRef._id, out obj);
			return new DefinitionIdentity(obj as IDefinitionIdentity);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00008654 File Offset: 0x00006854
		public EnumDefinitionIdentity Components
		{
			get
			{
				object obj;
				this._appcontext.EnumComponents(0U, out obj);
				return new EnumDefinitionIdentity(obj as IEnumDefinitionIdentity);
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000867A File Offset: 0x0000687A
		public void PrepareForExecution()
		{
			this._appcontext.PrepareForExecution(IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008694 File Offset: 0x00006894
		public ApplicationContext.ApplicationStateDisposition SetApplicationState(ApplicationContext.ApplicationState s)
		{
			uint num;
			this._appcontext.SetApplicationRunningState(0U, (uint)s, out num);
			return (ApplicationContext.ApplicationStateDisposition)num;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000086B4 File Offset: 0x000068B4
		public string StateLocation
		{
			get
			{
				string text;
				this._appcontext.GetApplicationStateFilesystemLocation(0U, UIntPtr.Zero, IntPtr.Zero, out text);
				return text;
			}
		}

		// Token: 0x040001AD RID: 429
		private IActContext _appcontext;

		// Token: 0x0200053F RID: 1343
		public enum ApplicationState
		{
			// Token: 0x040037F4 RID: 14324
			Undefined,
			// Token: 0x040037F5 RID: 14325
			Starting,
			// Token: 0x040037F6 RID: 14326
			Running
		}

		// Token: 0x02000540 RID: 1344
		public enum ApplicationStateDisposition
		{
			// Token: 0x040037F8 RID: 14328
			Undefined,
			// Token: 0x040037F9 RID: 14329
			Starting,
			// Token: 0x040037FA RID: 14330
			Starting_Migrated = 65537,
			// Token: 0x040037FB RID: 14331
			Running = 2,
			// Token: 0x040037FC RID: 14332
			Running_FirstTime = 131074
		}
	}
}
