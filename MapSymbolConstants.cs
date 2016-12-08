using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MapObjects2;

namespace MapUtils
{
    class MapSymbolConstants
    {
        public static readonly uint WaterColorConstants = (uint)ColorTranslator.ToWin32(Color.FromArgb(153, 179, 204));

        public static readonly uint BaseColorConstants = (uint)ColorTranslator.ToWin32(Color.FromArgb(240, 237, 229));

        public static readonly uint BaseOutlineColorConstants = (uint)ColorTranslator.ToWin32(Color.FromArgb(178, 178, 178));

        public static readonly uint GreenColorConstants = (uint)ColorTranslator.ToWin32(Color.FromArgb(181, 210, 157));

        public static readonly uint HighWayColorConstants = (uint)ColorTranslator.ToWin32(Color.FromArgb(247, 184, 53));

        public static readonly uint NationalRoadColorConstants = (uint)ColorTranslator.ToWin32(Color.FromArgb(255, 255, 168));

        public static readonly uint StateRoadColorConstants = (uint)ColorTranslator.ToWin32(Color.FromArgb(255, 255, 168));

        public static readonly uint TownRoadColorConstants = (uint)ColorTranslator.ToWin32(Color.FromArgb(225, 225, 225));

        public static readonly uint CountryRoad06ColorConstants = (uint)ColorTranslator.ToWin32(Color.FromArgb(255, 255, 255));

        public static readonly uint CountryRoad08ColorConstants = (uint)ColorTranslator.ToWin32(Color.FromArgb(255, 255, 255));

        public static readonly uint LabelColorConstants = (uint)ColorTranslator.ToWin32(Color.FromArgb(80,80,80));

        public static readonly uint RoadBaseColorConstants = (uint)ColorTranslator.ToWin32(Color.FromArgb(200, 200, 200));

        public readonly stdole.StdFont HighwayLabelFont_L = new stdole.StdFont();

        public readonly stdole.StdFont HighwayLabelFont_S = new stdole.StdFont();

        public readonly stdole.StdFont PointLabelFont_L = new stdole.StdFont();

        public readonly stdole.StdFont PointLabelFont_S = new stdole.StdFont();

        public readonly stdole.StdFont AlertPointLabelFont_L = new stdole.StdFont();

        public readonly stdole.StdFont AlertPointLabelFont_S = new stdole.StdFont();

        public readonly stdole.StdFont CountryRoad06LabelFont_L = new stdole.StdFont();

        public readonly stdole.StdFont CountryRoad06LabelFont_S = new stdole.StdFont();

        public readonly stdole.StdFont CountryRoad08LabelFont_L = new stdole.StdFont();

        public readonly stdole.StdFont CountryRoad08LabelFont_S = new stdole.StdFont();

        public readonly stdole.StdFont OtherRoadLabelFont_L = new stdole.StdFont();

        public readonly stdole.StdFont OtherRoadLabelFont_S = new stdole.StdFont();

        public readonly IMoLabelPlacer HighWayLabelPlacer_L = new LabelPlacerClass();

        public readonly IMoLabelPlacer HighWayLabelPlacer_S = new LabelPlacerClass();

        public readonly IMoLabelPlacer PointLabelPlacer_L = new LabelPlacerClass();

        public readonly IMoLabelPlacer PointLabelPlacer_S = new LabelPlacerClass();

        public readonly IMoLabelPlacer AlertPointLabelPlacer_L = new LabelPlacerClass();

        public readonly IMoLabelPlacer AlertPointLabelPlacer_S = new LabelPlacerClass();

        public readonly IMoLabelPlacer CountryRoad06LabelPlacer_L = new LabelPlacerClass();

        public readonly IMoLabelPlacer CountryRoad06LabelPlacer_S = new LabelPlacerClass();

        public readonly IMoLabelPlacer CountryRoad08LabelPlacer_L = new LabelPlacerClass();

        public readonly IMoLabelPlacer CountryRoad08LabelPlacer_S = new LabelPlacerClass();

        public readonly IMoLabelPlacer OtherRoadLabelPlacer_L = new LabelPlacerClass();

        public readonly IMoLabelPlacer OtherRoadLabelPlacer_S = new LabelPlacerClass();


        public MapSymbolConstants()
        {
            HighwayLabelFont_L.Bold = true;
            HighwayLabelFont_L.Name = "宋体";
            HighwayLabelFont_L.Size = 12;
            HighwayLabelFont_S.Bold = true;
            HighwayLabelFont_S.Name = "宋体";
            HighwayLabelFont_S.Size = 9;

            HighWayLabelPlacer_L.PlaceAbove = false;
            HighWayLabelPlacer_L.PlaceBelow = false;
            HighWayLabelPlacer_L.PlaceOn = true;
            HighWayLabelPlacer_L.AllowDuplicates = false;
            HighWayLabelPlacer_L.Field = "PathName";
            HighWayLabelPlacer_L.DefaultSymbol.Font = HighwayLabelFont_L;
            HighWayLabelPlacer_L.DefaultSymbol.Color = LabelColorConstants;

            HighWayLabelPlacer_S.PlaceAbove = false;
            HighWayLabelPlacer_S.PlaceBelow = false;
            HighWayLabelPlacer_S.PlaceOn = true;
            HighWayLabelPlacer_S.AllowDuplicates = false;
            HighWayLabelPlacer_S.Field = "PathName";
            HighWayLabelPlacer_S.DefaultSymbol.Font = HighwayLabelFont_S;
            HighWayLabelPlacer_S.DefaultSymbol.Color = LabelColorConstants;


            PointLabelFont_L.Name = "微软雅黑";
            PointLabelFont_L.Size = 12;
            PointLabelFont_S.Name = "微软雅黑";
            PointLabelFont_S.Size = 9;

            PointLabelPlacer_L.PlaceAbove = true;
            PointLabelPlacer_L.PlaceBelow = false;
            PointLabelPlacer_L.PlaceOn = false;
            PointLabelPlacer_L.SymbolHeight = 20;
            PointLabelPlacer_L.AllowDuplicates = false;
            PointLabelPlacer_L.Field = "Name";
            PointLabelPlacer_L.DefaultSymbol.Font = PointLabelFont_L;
            PointLabelPlacer_L.DefaultSymbol.Color = LabelColorConstants;

            PointLabelPlacer_S.PlaceAbove = true;
            PointLabelPlacer_S.PlaceBelow = false;
            PointLabelPlacer_S.PlaceOn = false;
            PointLabelPlacer_S.SymbolHeight = 15;
            PointLabelPlacer_S.AllowDuplicates = false;
            PointLabelPlacer_S.Field = "Name";
            PointLabelPlacer_S.DefaultSymbol.Font = PointLabelFont_S;
            PointLabelPlacer_S.DefaultSymbol.Color = LabelColorConstants;

            AlertPointLabelFont_L.Name = "微软雅黑";
            AlertPointLabelFont_L.Size = 12;
            AlertPointLabelFont_S.Name = "微软雅黑";
            AlertPointLabelFont_S.Size = 10;

            AlertPointLabelPlacer_L.PlaceAbove = true;
            AlertPointLabelPlacer_L.PlaceBelow = false;
            AlertPointLabelPlacer_L.PlaceOn = false;
            AlertPointLabelPlacer_L.SymbolHeight = 20;
            AlertPointLabelPlacer_L.AllowDuplicates = false;
            AlertPointLabelPlacer_L.Field = "Name";
            AlertPointLabelPlacer_L.DefaultSymbol.Font = AlertPointLabelFont_L;
            AlertPointLabelPlacer_L.DefaultSymbol.Color = LabelColorConstants;

            AlertPointLabelPlacer_S.PlaceAbove = true;
            AlertPointLabelPlacer_S.PlaceBelow = false;
            AlertPointLabelPlacer_S.PlaceOn = false;
            AlertPointLabelPlacer_S.SymbolHeight = 15;
            AlertPointLabelPlacer_S.AllowDuplicates = false;
            AlertPointLabelPlacer_S.Field = "Name";
            AlertPointLabelPlacer_S.DefaultSymbol.Font = AlertPointLabelFont_S;
            AlertPointLabelPlacer_S.DefaultSymbol.Color = LabelColorConstants;


            CountryRoad06LabelFont_L.Name = "Arial";
            CountryRoad06LabelFont_L.Size = 10;
            CountryRoad06LabelFont_S.Name = "Arial";
            CountryRoad06LabelFont_S.Size = 10;

            CountryRoad06LabelPlacer_L.PlaceAbove = false;
            CountryRoad06LabelPlacer_L.PlaceBelow = false;
            CountryRoad06LabelPlacer_L.PlaceOn = true;
            CountryRoad06LabelPlacer_L.AllowDuplicates = false;
            CountryRoad06LabelPlacer_L.Field = "PathName";
            CountryRoad06LabelPlacer_L.DefaultSymbol.Font = CountryRoad06LabelFont_L;
            CountryRoad06LabelPlacer_L.DefaultSymbol.Color = LabelColorConstants;

            CountryRoad06LabelPlacer_S.PlaceAbove = false;
            CountryRoad06LabelPlacer_S.PlaceBelow = false;
            CountryRoad06LabelPlacer_S.PlaceOn = true;
            CountryRoad06LabelPlacer_S.AllowDuplicates = false;
            CountryRoad06LabelPlacer_S.Field = "PathName";
            CountryRoad06LabelPlacer_S.DefaultSymbol.Font = CountryRoad06LabelFont_S;
            CountryRoad06LabelPlacer_S.DefaultSymbol.Color = LabelColorConstants;

            CountryRoad08LabelFont_L.Name = "Arial";
            CountryRoad08LabelFont_L.Size = 10;
            CountryRoad08LabelFont_S.Name = "Arial";
            CountryRoad08LabelFont_S.Size = 10;

            CountryRoad08LabelPlacer_L.PlaceAbove = false;
            CountryRoad08LabelPlacer_L.PlaceBelow = false;
            CountryRoad08LabelPlacer_L.PlaceOn = true;
            CountryRoad08LabelPlacer_L.AllowDuplicates = false;
            CountryRoad08LabelPlacer_L.Field = "PathName";
            CountryRoad08LabelPlacer_L.DefaultSymbol.Font = CountryRoad08LabelFont_L;
            CountryRoad08LabelPlacer_L.DefaultSymbol.Color = LabelColorConstants;

            CountryRoad08LabelPlacer_S.PlaceAbove = false;
            CountryRoad08LabelPlacer_S.PlaceBelow = false;
            CountryRoad08LabelPlacer_S.PlaceOn = true;
            CountryRoad08LabelPlacer_S.AllowDuplicates = false;
            CountryRoad08LabelPlacer_S.Field = "PathName";
            CountryRoad08LabelPlacer_S.DefaultSymbol.Font = CountryRoad08LabelFont_S;
            CountryRoad08LabelPlacer_S.DefaultSymbol.Color = LabelColorConstants;


            OtherRoadLabelFont_L.Name = "Arial";
            OtherRoadLabelFont_L.Size = 10;
            OtherRoadLabelFont_S.Name = "Arial";
            OtherRoadLabelFont_S.Size = 10;

            OtherRoadLabelPlacer_L.PlaceAbove = false;
            OtherRoadLabelPlacer_L.PlaceBelow = false;
            OtherRoadLabelPlacer_L.PlaceOn = true;
            OtherRoadLabelPlacer_L.AllowDuplicates = false;
            OtherRoadLabelPlacer_L.Field = "PathName";
            OtherRoadLabelPlacer_L.DefaultSymbol.Font = OtherRoadLabelFont_L;
            OtherRoadLabelPlacer_L.DefaultSymbol.Color = LabelColorConstants;

            OtherRoadLabelPlacer_S.PlaceAbove = false;
            OtherRoadLabelPlacer_S.PlaceBelow = false;
            OtherRoadLabelPlacer_S.PlaceOn = true;
            OtherRoadLabelPlacer_S.AllowDuplicates = false;
            OtherRoadLabelPlacer_S.Field = "PathName";
            OtherRoadLabelPlacer_S.DefaultSymbol.Font = OtherRoadLabelFont_S;
            OtherRoadLabelPlacer_S.DefaultSymbol.Color = LabelColorConstants;
        }


    }
}
