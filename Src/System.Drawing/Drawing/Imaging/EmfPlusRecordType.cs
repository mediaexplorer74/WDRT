using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the methods available for use with a metafile to read and write graphic commands.</summary>
	// Token: 0x02000096 RID: 150
	public enum EmfPlusRecordType
	{
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000773 RID: 1907
		WmfRecordBase = 65536,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000774 RID: 1908
		WmfSetBkColor = 66049,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000775 RID: 1909
		WmfSetBkMode = 65794,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000776 RID: 1910
		WmfSetMapMode,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000777 RID: 1911
		WmfSetROP2,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000778 RID: 1912
		WmfSetRelAbs,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000779 RID: 1913
		WmfSetPolyFillMode,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400077A RID: 1914
		WmfSetStretchBltMode,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400077B RID: 1915
		WmfSetTextCharExtra,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400077C RID: 1916
		WmfSetTextColor = 66057,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400077D RID: 1917
		WmfSetTextJustification,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400077E RID: 1918
		WmfSetWindowOrg,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400077F RID: 1919
		WmfSetWindowExt,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000780 RID: 1920
		WmfSetViewportOrg,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000781 RID: 1921
		WmfSetViewportExt,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000782 RID: 1922
		WmfOffsetWindowOrg,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000783 RID: 1923
		WmfScaleWindowExt = 66576,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000784 RID: 1924
		WmfOffsetViewportOrg = 66065,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000785 RID: 1925
		WmfScaleViewportExt = 66578,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000786 RID: 1926
		WmfLineTo = 66067,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000787 RID: 1927
		WmfMoveTo,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000788 RID: 1928
		WmfExcludeClipRect = 66581,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000789 RID: 1929
		WmfIntersectClipRect,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400078A RID: 1930
		WmfArc = 67607,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400078B RID: 1931
		WmfEllipse = 66584,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400078C RID: 1932
		WmfFloodFill,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400078D RID: 1933
		WmfPie = 67610,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400078E RID: 1934
		WmfRectangle = 66587,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400078F RID: 1935
		WmfRoundRect = 67100,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000790 RID: 1936
		WmfPatBlt,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000791 RID: 1937
		WmfSaveDC = 65566,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000792 RID: 1938
		WmfSetPixel = 66591,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000793 RID: 1939
		WmfOffsetCilpRgn = 66080,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000794 RID: 1940
		WmfTextOut = 66849,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000795 RID: 1941
		WmfBitBlt = 67874,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000796 RID: 1942
		WmfStretchBlt = 68387,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000797 RID: 1943
		WmfPolygon = 66340,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000798 RID: 1944
		WmfPolyline,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000799 RID: 1945
		WmfEscape = 67110,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400079A RID: 1946
		WmfRestoreDC = 65831,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400079B RID: 1947
		WmfFillRegion = 66088,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400079C RID: 1948
		WmfFrameRegion = 66601,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400079D RID: 1949
		WmfInvertRegion = 65834,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400079E RID: 1950
		WmfPaintRegion,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400079F RID: 1951
		WmfSelectClipRegion,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007A0 RID: 1952
		WmfSelectObject,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007A1 RID: 1953
		WmfSetTextAlign,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007A2 RID: 1954
		WmfChord = 67632,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007A3 RID: 1955
		WmfSetMapperFlags = 66097,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007A4 RID: 1956
		WmfExtTextOut = 68146,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007A5 RID: 1957
		WmfSetDibToDev = 68915,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007A6 RID: 1958
		WmfSelectPalette = 66100,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007A7 RID: 1959
		WmfRealizePalette = 65589,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007A8 RID: 1960
		WmfAnimatePalette = 66614,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007A9 RID: 1961
		WmfSetPalEntries = 65591,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007AA RID: 1962
		WmfPolyPolygon = 66872,
		/// <summary>Increases or decreases the size of a logical palette based on the specified value.</summary>
		// Token: 0x040007AB RID: 1963
		WmfResizePalette = 65849,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007AC RID: 1964
		WmfDibBitBlt = 67904,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007AD RID: 1965
		WmfDibStretchBlt = 68417,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007AE RID: 1966
		WmfDibCreatePatternBrush = 65858,
		/// <summary>Copies the color data for a rectangle of pixels in a DIB to the specified destination rectangle.</summary>
		// Token: 0x040007AF RID: 1967
		WmfStretchDib = 69443,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007B0 RID: 1968
		WmfExtFloodFill = 66888,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007B1 RID: 1969
		WmfSetLayout = 65865,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007B2 RID: 1970
		WmfDeleteObject = 66032,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007B3 RID: 1971
		WmfCreatePalette = 65783,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007B4 RID: 1972
		WmfCreatePatternBrush = 66041,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007B5 RID: 1973
		WmfCreatePenIndirect = 66298,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007B6 RID: 1974
		WmfCreateFontIndirect,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007B7 RID: 1975
		WmfCreateBrushIndirect,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007B8 RID: 1976
		WmfCreateRegion = 67327,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007B9 RID: 1977
		EmfHeader = 1,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007BA RID: 1978
		EmfPolyBezier,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007BB RID: 1979
		EmfPolygon,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007BC RID: 1980
		EmfPolyline,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007BD RID: 1981
		EmfPolyBezierTo,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007BE RID: 1982
		EmfPolyLineTo,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007BF RID: 1983
		EmfPolyPolyline,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007C0 RID: 1984
		EmfPolyPolygon,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007C1 RID: 1985
		EmfSetWindowExtEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007C2 RID: 1986
		EmfSetWindowOrgEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007C3 RID: 1987
		EmfSetViewportExtEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007C4 RID: 1988
		EmfSetViewportOrgEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007C5 RID: 1989
		EmfSetBrushOrgEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007C6 RID: 1990
		EmfEof,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007C7 RID: 1991
		EmfSetPixelV,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007C8 RID: 1992
		EmfSetMapperFlags,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007C9 RID: 1993
		EmfSetMapMode,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007CA RID: 1994
		EmfSetBkMode,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007CB RID: 1995
		EmfSetPolyFillMode,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007CC RID: 1996
		EmfSetROP2,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007CD RID: 1997
		EmfSetStretchBltMode,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007CE RID: 1998
		EmfSetTextAlign,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007CF RID: 1999
		EmfSetColorAdjustment,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007D0 RID: 2000
		EmfSetTextColor,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007D1 RID: 2001
		EmfSetBkColor,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007D2 RID: 2002
		EmfOffsetClipRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007D3 RID: 2003
		EmfMoveToEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007D4 RID: 2004
		EmfSetMetaRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007D5 RID: 2005
		EmfExcludeClipRect,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007D6 RID: 2006
		EmfIntersectClipRect,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007D7 RID: 2007
		EmfScaleViewportExtEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007D8 RID: 2008
		EmfScaleWindowExtEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007D9 RID: 2009
		EmfSaveDC,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007DA RID: 2010
		EmfRestoreDC,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007DB RID: 2011
		EmfSetWorldTransform,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007DC RID: 2012
		EmfModifyWorldTransform,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007DD RID: 2013
		EmfSelectObject,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007DE RID: 2014
		EmfCreatePen,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007DF RID: 2015
		EmfCreateBrushIndirect,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007E0 RID: 2016
		EmfDeleteObject,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007E1 RID: 2017
		EmfAngleArc,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007E2 RID: 2018
		EmfEllipse,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007E3 RID: 2019
		EmfRectangle,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007E4 RID: 2020
		EmfRoundRect,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007E5 RID: 2021
		EmfRoundArc,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007E6 RID: 2022
		EmfChord,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007E7 RID: 2023
		EmfPie,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007E8 RID: 2024
		EmfSelectPalette,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007E9 RID: 2025
		EmfCreatePalette,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007EA RID: 2026
		EmfSetPaletteEntries,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007EB RID: 2027
		EmfResizePalette,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007EC RID: 2028
		EmfRealizePalette,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007ED RID: 2029
		EmfExtFloodFill,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007EE RID: 2030
		EmfLineTo,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007EF RID: 2031
		EmfArcTo,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007F0 RID: 2032
		EmfPolyDraw,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007F1 RID: 2033
		EmfSetArcDirection,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007F2 RID: 2034
		EmfSetMiterLimit,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007F3 RID: 2035
		EmfBeginPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007F4 RID: 2036
		EmfEndPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007F5 RID: 2037
		EmfCloseFigure,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007F6 RID: 2038
		EmfFillPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007F7 RID: 2039
		EmfStrokeAndFillPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007F8 RID: 2040
		EmfStrokePath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007F9 RID: 2041
		EmfFlattenPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007FA RID: 2042
		EmfWidenPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007FB RID: 2043
		EmfSelectClipPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007FC RID: 2044
		EmfAbortPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007FD RID: 2045
		EmfReserved069,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007FE RID: 2046
		EmfGdiComment,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040007FF RID: 2047
		EmfFillRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000800 RID: 2048
		EmfFrameRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000801 RID: 2049
		EmfInvertRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000802 RID: 2050
		EmfPaintRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000803 RID: 2051
		EmfExtSelectClipRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000804 RID: 2052
		EmfBitBlt,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000805 RID: 2053
		EmfStretchBlt,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000806 RID: 2054
		EmfMaskBlt,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000807 RID: 2055
		EmfPlgBlt,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000808 RID: 2056
		EmfSetDIBitsToDevice,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000809 RID: 2057
		EmfStretchDIBits,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400080A RID: 2058
		EmfExtCreateFontIndirect,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400080B RID: 2059
		EmfExtTextOutA,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400080C RID: 2060
		EmfExtTextOutW,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400080D RID: 2061
		EmfPolyBezier16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400080E RID: 2062
		EmfPolygon16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400080F RID: 2063
		EmfPolyline16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000810 RID: 2064
		EmfPolyBezierTo16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000811 RID: 2065
		EmfPolylineTo16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000812 RID: 2066
		EmfPolyPolyline16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000813 RID: 2067
		EmfPolyPolygon16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000814 RID: 2068
		EmfPolyDraw16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000815 RID: 2069
		EmfCreateMonoBrush,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000816 RID: 2070
		EmfCreateDibPatternBrushPt,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000817 RID: 2071
		EmfExtCreatePen,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000818 RID: 2072
		EmfPolyTextOutA,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000819 RID: 2073
		EmfPolyTextOutW,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400081A RID: 2074
		EmfSetIcmMode,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400081B RID: 2075
		EmfCreateColorSpace,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400081C RID: 2076
		EmfSetColorSpace,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400081D RID: 2077
		EmfDeleteColorSpace,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400081E RID: 2078
		EmfGlsRecord,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400081F RID: 2079
		EmfGlsBoundedRecord,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000820 RID: 2080
		EmfPixelFormat,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000821 RID: 2081
		EmfDrawEscape,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000822 RID: 2082
		EmfExtEscape,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000823 RID: 2083
		EmfStartDoc,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000824 RID: 2084
		EmfSmallTextOut,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000825 RID: 2085
		EmfForceUfiMapping,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000826 RID: 2086
		EmfNamedEscpae,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000827 RID: 2087
		EmfColorCorrectPalette,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000828 RID: 2088
		EmfSetIcmProfileA,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000829 RID: 2089
		EmfSetIcmProfileW,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400082A RID: 2090
		EmfAlphaBlend,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400082B RID: 2091
		EmfSetLayout,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400082C RID: 2092
		EmfTransparentBlt,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400082D RID: 2093
		EmfReserved117,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400082E RID: 2094
		EmfGradientFill,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400082F RID: 2095
		EmfSetLinkedUfis,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000830 RID: 2096
		EmfSetTextJustification,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000831 RID: 2097
		EmfColorMatchToTargetW,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000832 RID: 2098
		EmfCreateColorSpaceW,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000833 RID: 2099
		EmfMax = 122,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000834 RID: 2100
		EmfMin = 1,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000835 RID: 2101
		EmfPlusRecordBase = 16384,
		/// <summary>Indicates invalid data.</summary>
		// Token: 0x04000836 RID: 2102
		Invalid = 16384,
		/// <summary>Identifies a record that is the EMF+ header.</summary>
		// Token: 0x04000837 RID: 2103
		Header,
		/// <summary>Identifies a record that marks the last EMF+ record of a metafile.</summary>
		// Token: 0x04000838 RID: 2104
		EndOfFile,
		/// <summary>See <see cref="M:System.Drawing.Graphics.AddMetafileComment(System.Byte[])" />.</summary>
		// Token: 0x04000839 RID: 2105
		Comment,
		/// <summary>See <see cref="M:System.Drawing.Graphics.GetHdc" />.</summary>
		// Token: 0x0400083A RID: 2106
		GetDC,
		/// <summary>Marks the start of a multiple-format section.</summary>
		// Token: 0x0400083B RID: 2107
		MultiFormatStart,
		/// <summary>Marks a multiple-format section.</summary>
		// Token: 0x0400083C RID: 2108
		MultiFormatSection,
		/// <summary>Marks the end of a multiple-format section.</summary>
		// Token: 0x0400083D RID: 2109
		MultiFormatEnd,
		/// <summary>Marks an object.</summary>
		// Token: 0x0400083E RID: 2110
		Object,
		/// <summary>See <see cref="M:System.Drawing.Graphics.Clear(System.Drawing.Color)" />.</summary>
		// Token: 0x0400083F RID: 2111
		Clear,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.FillRectangles" /> methods.</summary>
		// Token: 0x04000840 RID: 2112
		FillRects,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawRectangles" /> methods.</summary>
		// Token: 0x04000841 RID: 2113
		DrawRects,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.FillPolygon" /> methods.</summary>
		// Token: 0x04000842 RID: 2114
		FillPolygon,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawLines" /> methods.</summary>
		// Token: 0x04000843 RID: 2115
		DrawLines,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.FillEllipse" /> methods.</summary>
		// Token: 0x04000844 RID: 2116
		FillEllipse,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawEllipse" /> methods.</summary>
		// Token: 0x04000845 RID: 2117
		DrawEllipse,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.FillPie" /> methods.</summary>
		// Token: 0x04000846 RID: 2118
		FillPie,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawPie" /> methods.</summary>
		// Token: 0x04000847 RID: 2119
		DrawPie,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawArc" /> methods.</summary>
		// Token: 0x04000848 RID: 2120
		DrawArc,
		/// <summary>See <see cref="M:System.Drawing.Graphics.FillRegion(System.Drawing.Brush,System.Drawing.Region)" />.</summary>
		// Token: 0x04000849 RID: 2121
		FillRegion,
		/// <summary>See <see cref="M:System.Drawing.Graphics.FillPath(System.Drawing.Brush,System.Drawing.Drawing2D.GraphicsPath)" />.</summary>
		// Token: 0x0400084A RID: 2122
		FillPath,
		/// <summary>See <see cref="M:System.Drawing.Graphics.DrawPath(System.Drawing.Pen,System.Drawing.Drawing2D.GraphicsPath)" />.</summary>
		// Token: 0x0400084B RID: 2123
		DrawPath,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.FillClosedCurve" /> methods.</summary>
		// Token: 0x0400084C RID: 2124
		FillClosedCurve,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawClosedCurve" /> methods.</summary>
		// Token: 0x0400084D RID: 2125
		DrawClosedCurve,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawCurve" /> methods.</summary>
		// Token: 0x0400084E RID: 2126
		DrawCurve,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawBeziers" /> methods.</summary>
		// Token: 0x0400084F RID: 2127
		DrawBeziers,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawImage" /> methods.</summary>
		// Token: 0x04000850 RID: 2128
		DrawImage,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawImage" /> methods.</summary>
		// Token: 0x04000851 RID: 2129
		DrawImagePoints,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawString" /> methods.</summary>
		// Token: 0x04000852 RID: 2130
		DrawString,
		/// <summary>See <see cref="P:System.Drawing.Graphics.RenderingOrigin" />.</summary>
		// Token: 0x04000853 RID: 2131
		SetRenderingOrigin,
		/// <summary>See <see cref="P:System.Drawing.Graphics.SmoothingMode" />.</summary>
		// Token: 0x04000854 RID: 2132
		SetAntiAliasMode,
		/// <summary>See <see cref="P:System.Drawing.Graphics.TextRenderingHint" />.</summary>
		// Token: 0x04000855 RID: 2133
		SetTextRenderingHint,
		/// <summary>See <see cref="P:System.Drawing.Graphics.TextContrast" />.</summary>
		// Token: 0x04000856 RID: 2134
		SetTextContrast,
		/// <summary>See <see cref="P:System.Drawing.Graphics.InterpolationMode" />.</summary>
		// Token: 0x04000857 RID: 2135
		SetInterpolationMode,
		/// <summary>See <see cref="P:System.Drawing.Graphics.PixelOffsetMode" />.</summary>
		// Token: 0x04000858 RID: 2136
		SetPixelOffsetMode,
		/// <summary>See <see cref="P:System.Drawing.Graphics.CompositingMode" />.</summary>
		// Token: 0x04000859 RID: 2137
		SetCompositingMode,
		/// <summary>See <see cref="P:System.Drawing.Graphics.CompositingQuality" />.</summary>
		// Token: 0x0400085A RID: 2138
		SetCompositingQuality,
		/// <summary>See <see cref="M:System.Drawing.Graphics.Save" />.</summary>
		// Token: 0x0400085B RID: 2139
		Save,
		/// <summary>See <see cref="M:System.Drawing.Graphics.Restore(System.Drawing.Drawing2D.GraphicsState)" />.</summary>
		// Token: 0x0400085C RID: 2140
		Restore,
		/// <summary>See <see cref="M:System.Drawing.Graphics.BeginContainer" /> methods.</summary>
		// Token: 0x0400085D RID: 2141
		BeginContainer,
		/// <summary>See <see cref="M:System.Drawing.Graphics.BeginContainer" /> methods.</summary>
		// Token: 0x0400085E RID: 2142
		BeginContainerNoParams,
		/// <summary>See <see cref="M:System.Drawing.Graphics.EndContainer(System.Drawing.Drawing2D.GraphicsContainer)" />.</summary>
		// Token: 0x0400085F RID: 2143
		EndContainer,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.TransformPoints" /> methods.</summary>
		// Token: 0x04000860 RID: 2144
		SetWorldTransform,
		/// <summary>See <see cref="M:System.Drawing.Graphics.ResetTransform" />.</summary>
		// Token: 0x04000861 RID: 2145
		ResetWorldTransform,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.MultiplyTransform" /> methods.</summary>
		// Token: 0x04000862 RID: 2146
		MultiplyWorldTransform,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.TransformPoints" /> methods.</summary>
		// Token: 0x04000863 RID: 2147
		TranslateWorldTransform,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.ScaleTransform" /> methods.</summary>
		// Token: 0x04000864 RID: 2148
		ScaleWorldTransform,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.RotateTransform" /> methods.</summary>
		// Token: 0x04000865 RID: 2149
		RotateWorldTransform,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.TransformPoints" /> methods.</summary>
		// Token: 0x04000866 RID: 2150
		SetPageTransform,
		/// <summary>See <see cref="M:System.Drawing.Graphics.ResetClip" />.</summary>
		// Token: 0x04000867 RID: 2151
		ResetClip,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.SetClip" /> methods.</summary>
		// Token: 0x04000868 RID: 2152
		SetClipRect,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.SetClip" /> methods.</summary>
		// Token: 0x04000869 RID: 2153
		SetClipPath,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.SetClip" /> methods.</summary>
		// Token: 0x0400086A RID: 2154
		SetClipRegion,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.TranslateClip" /> methods.</summary>
		// Token: 0x0400086B RID: 2155
		OffsetClip,
		/// <summary>Specifies a character string, a location, and formatting information.</summary>
		// Token: 0x0400086C RID: 2156
		DrawDriverString,
		/// <summary>Used internally.</summary>
		// Token: 0x0400086D RID: 2157
		Total,
		/// <summary>The maximum value for this enumeration.</summary>
		// Token: 0x0400086E RID: 2158
		Max = 16438,
		/// <summary>The minimum value for this enumeration.</summary>
		// Token: 0x0400086F RID: 2159
		Min = 16385
	}
}
