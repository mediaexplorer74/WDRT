using System;
using System.Collections;
using System.Globalization;
using System.IO;

namespace System.Management.Instrumentation
{
	// Token: 0x020000B1 RID: 177
	internal class CodeWriter
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0002260F File Offset: 0x0002080F
		public static explicit operator string(CodeWriter writer)
		{
			return writer.ToString();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00022618 File Offset: 0x00020818
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteCode(stringWriter);
			string text = stringWriter.ToString();
			stringWriter.Close();
			return text;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00022648 File Offset: 0x00020848
		private void WriteCode(TextWriter writer)
		{
			string text = new string(' ', this.depth * 4);
			foreach (object obj in this.children)
			{
				if (obj == null)
				{
					writer.WriteLine();
				}
				else if (obj is string)
				{
					writer.Write(text);
					writer.WriteLine(obj);
				}
				else
				{
					((CodeWriter)obj).WriteCode(writer);
				}
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000226D4 File Offset: 0x000208D4
		public CodeWriter AddChild(string name)
		{
			this.Line(name);
			this.Line("{");
			CodeWriter codeWriter = new CodeWriter();
			codeWriter.depth = this.depth + 1;
			this.children.Add(codeWriter);
			this.Line("}");
			return codeWriter;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00022720 File Offset: 0x00020920
		public CodeWriter AddChild(params string[] parts)
		{
			return this.AddChild(string.Concat(parts));
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00022730 File Offset: 0x00020930
		public CodeWriter AddChildNoIndent(string name)
		{
			this.Line(name);
			CodeWriter codeWriter = new CodeWriter();
			codeWriter.depth = this.depth + 1;
			this.children.Add(codeWriter);
			return codeWriter;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00022766 File Offset: 0x00020966
		public CodeWriter AddChild(CodeWriter snippet)
		{
			snippet.depth = this.depth;
			this.children.Add(snippet);
			return snippet;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00022782 File Offset: 0x00020982
		public void Line(string line)
		{
			this.children.Add(line);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00022791 File Offset: 0x00020991
		public void Line(params string[] parts)
		{
			this.Line(string.Concat(parts));
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0002279F File Offset: 0x0002099F
		public void Line()
		{
			this.children.Add(null);
		}

		// Token: 0x040004EA RID: 1258
		private int depth;

		// Token: 0x040004EB RID: 1259
		private ArrayList children = new ArrayList();
	}
}
