using System;
using System.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000063 RID: 99
	internal static class GeometryHelperMethods
	{
		// Token: 0x0600027D RID: 637 RVA: 0x00006D70 File Offset: 0x00004F70
		internal static void SendFigure(this GeometryLineString GeometryLineString, GeometryPipeline pipeline)
		{
			Util.CheckArgumentNull(GeometryLineString, "GeometryLineString");
			for (int i = 0; i < GeometryLineString.Points.Count; i++)
			{
				GeometryPoint geometryPoint = GeometryLineString.Points[i];
				GeometryPosition geometryPosition = new GeometryPosition(geometryPoint.X, geometryPoint.Y, geometryPoint.Z, geometryPoint.M);
				if (i == 0)
				{
					pipeline.BeginFigure(geometryPosition);
				}
				else
				{
					pipeline.LineTo(geometryPosition);
				}
			}
			if (GeometryLineString.Points.Count > 0)
			{
				pipeline.EndFigure();
			}
		}
	}
}
