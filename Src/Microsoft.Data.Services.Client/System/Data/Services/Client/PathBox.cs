using System;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace System.Data.Services.Client
{
	// Token: 0x020000B5 RID: 181
	internal class PathBox
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x000160A0 File Offset: 0x000142A0
		internal PathBox()
		{
			this.projectionPaths.Add(new StringBuilder());
			this.uriVersion = Util.DataServiceVersion1;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00016114 File Offset: 0x00014314
		internal IEnumerable<string> ProjectionPaths
		{
			get
			{
				return (from s in this.projectionPaths
					where s.Length > 0
					select s.ToString()).Distinct<string>();
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00016184 File Offset: 0x00014384
		internal IEnumerable<string> ExpandPaths
		{
			get
			{
				return (from s in this.expandPaths
					where s.Length > 0
					select s.ToString()).Distinct<string>();
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x000161E0 File Offset: 0x000143E0
		internal Version UriVersion
		{
			get
			{
				return this.uriVersion;
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000161E8 File Offset: 0x000143E8
		internal void PushParamExpression(ParameterExpression pe)
		{
			StringBuilder stringBuilder = this.projectionPaths.Last<StringBuilder>();
			this.basePaths.Add(pe, stringBuilder.ToString());
			this.projectionPaths.Remove(stringBuilder);
			this.parameterExpressions.Push(pe);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001622C File Offset: 0x0001442C
		internal void PopParamExpression()
		{
			this.parameterExpressions.Pop();
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001623A File Offset: 0x0001443A
		internal ParameterExpression ParamExpressionInScope
		{
			get
			{
				return this.parameterExpressions.Peek();
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00016248 File Offset: 0x00014448
		internal void StartNewPath()
		{
			StringBuilder stringBuilder = new StringBuilder(this.basePaths[this.ParamExpressionInScope]);
			PathBox.RemoveEntireEntityMarkerIfPresent(stringBuilder);
			this.expandPaths.Add(new StringBuilder(stringBuilder.ToString()));
			PathBox.AddEntireEntityMarker(stringBuilder);
			this.projectionPaths.Add(stringBuilder);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001629C File Offset: 0x0001449C
		internal void AppendPropertyToPath(PropertyInfo pi, Type convertedSourceType, DataServiceContext context)
		{
			bool flag = ClientTypeUtil.TypeOrElementTypeIsEntity(pi.PropertyType);
			string text = ((convertedSourceType == null) ? null : UriHelper.GetEntityTypeNameForUriAndValidateMaxProtocolVersion(convertedSourceType, context, ref this.uriVersion));
			if (flag)
			{
				if (convertedSourceType != null)
				{
					this.AppendToExpandPath(text);
				}
				this.AppendToExpandPath(pi.Name);
			}
			if (convertedSourceType != null)
			{
				this.AppendToProjectionPath(text, false);
			}
			StringBuilder stringBuilder = this.AppendToProjectionPath(pi.Name, false);
			if (flag)
			{
				PathBox.AddEntireEntityMarker(stringBuilder);
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001631C File Offset: 0x0001451C
		private StringBuilder AppendToProjectionPath(string name, bool replaceEntityMarkerIfPresent)
		{
			StringBuilder stringBuilder = this.projectionPaths.Last<StringBuilder>();
			bool flag = PathBox.RemoveEntireEntityMarkerIfPresent(stringBuilder);
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append('/');
			}
			stringBuilder.Append(name);
			if (flag && replaceEntityMarkerIfPresent)
			{
				PathBox.AddEntireEntityMarker(stringBuilder);
			}
			return stringBuilder;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00016364 File Offset: 0x00014564
		private void AppendToExpandPath(string name)
		{
			StringBuilder stringBuilder = this.expandPaths.Last<StringBuilder>();
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append('/');
			}
			stringBuilder.Append(name);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00016398 File Offset: 0x00014598
		private static bool RemoveEntireEntityMarkerIfPresent(StringBuilder sb)
		{
			bool flag = false;
			if (sb.Length > 0 && sb[sb.Length - 1] == '*')
			{
				sb.Remove(sb.Length - 1, 1);
				flag = true;
			}
			if (sb.Length > 0 && sb[sb.Length - 1] == '/')
			{
				sb.Remove(sb.Length - 1, 1);
			}
			return flag;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00016400 File Offset: 0x00014600
		private static void AddEntireEntityMarker(StringBuilder sb)
		{
			if (sb.Length > 0)
			{
				sb.Append('/');
			}
			sb.Append('*');
		}

		// Token: 0x04000320 RID: 800
		private const char EntireEntityMarker = '*';

		// Token: 0x04000321 RID: 801
		private readonly List<StringBuilder> projectionPaths = new List<StringBuilder>();

		// Token: 0x04000322 RID: 802
		private readonly List<StringBuilder> expandPaths = new List<StringBuilder>();

		// Token: 0x04000323 RID: 803
		private readonly Stack<ParameterExpression> parameterExpressions = new Stack<ParameterExpression>();

		// Token: 0x04000324 RID: 804
		private readonly Dictionary<ParameterExpression, string> basePaths = new Dictionary<ParameterExpression, string>(ReferenceEqualityComparer<ParameterExpression>.Instance);

		// Token: 0x04000325 RID: 805
		private Version uriVersion;
	}
}
