using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapObjects2;
using AxMapObjects2;
using System.Windows.Forms;
using System.Drawing;
using System.Transactions;
using System.IO;

namespace MapUtils
{
    class MapControl
    {
        /// <summary>
        /// 地图实例
        /// </summary>
        public AxMap map = null;

        private String dataBase = Environment.CurrentDirectory + "\\Map";

        private String mouseSymbol = "default";

        private MapSymbolConstants symbolConst = new MapSymbolConstants();
        /// <summary>
        /// 当前地图比例尺
        /// </summary>
        public double scale = 1000000;

        //private TextBox tb = null;

        private int clickedId;

        private DataConnection dc = new DataConnectionClass();

        //private MapLayer cityExtendFace = new MapLayerClass();
        private MapLayer cityExtendLine = new MapLayerClass();
        private MapLayer cityExtendPoint = new MapLayerClass();
        private MapLayer layer1 = new MapLayerClass();
        private MapLayer layer2 = new MapLayerClass();
        private MapLayer layer3 = new MapLayerClass();
        private MapLayer highway = new MapLayerClass();
        private MapLayer nationalRoad = new MapLayerClass();
        private MapLayer stateRoad = new MapLayerClass();
        private MapLayer townRoad = new MapLayerClass();
        private MapLayer countryRoad06 = new MapLayerClass();
        private MapLayer countryRoad06_1 = new MapLayerClass();
        private MapLayer countryRoad08 = new MapLayerClass();
        private MapLayer baseOutline = new MapLayerClass();
        private MapLayer properName = new MapLayerClass();
        private MapLayer factoryPoint = new MapLayerClass();
        private MapLayer LivingPoint = new MapLayerClass();
        private MapLayer townsName = new MapLayerClass();
        private MapLayer countyName = new MapLayerClass();
        private MapLayer businessPoint = new MapLayerClass();
        private MapLayer alertPoint = new MapLayerClass();

        //private Symbol cityExtendFaceSymbol = null;
        private Symbol cityExtendPointSymbol = null;
        private Symbol cityExtendLineSymbol = null;
        private Symbol base_symbol = null;
        private Symbol water_symbol = null;
        private Symbol green_symbol = null;
        private Symbol highway_symbol = null;
        private Symbol nationalRoad_symbol = null;
        private Symbol stateRoad_symbol = null;
        private Symbol townRoad_symbol = null;
        private Symbol countryRoad06_symbol = null;
        private Symbol countryRoad06_1_symbol = null;
        private Symbol countryRoad08_symbol = null;
        private Symbol baseOutline_symbol = null;
        private Symbol properName_symbol = null;
        private Symbol factoryPoint_symbol = null;
        private Symbol LivingPoint_symbol = null;
        private Symbol townsName_symbol = null;
        private Symbol countyName_symbol = null;
        private Symbol businessPoint_symbol = null;
        private Symbol alertPoints_symbol = null;

        public MapControl()
        {
            this.map = new AxMap();
        }

        public MapControl(AxMap axMap)
        {
            this.map = axMap;
            //this.tb = textbox;
            this.map.ScrollBars = false;
            this.map.MouseDownEvent += new AxMapObjects2._DMapEvents_MouseDownEventHandler(this.Map_MouseDownEvent);
            this.map.MouseMoveEvent += new AxMapObjects2._DMapEvents_MouseMoveEventHandler(this.Map_MouseMoveEvent);
            this.map.BeforeLayerDraw += new AxMapObjects2._DMapEvents_BeforeLayerDrawEventHandler(this.Map_BeforeLayerDraw);
        }
        /// <summary>
        /// 鼠标单击事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Map_MouseDownEvent(object sender, AxMapObjects2._DMapEvents_MouseDownEvent e)
        {
           

            switch (this.mouseSymbol)
            {
                case "hand":
                    _MapBusinessPoint_ClickedEventArgs te = new _MapBusinessPoint_ClickedEventArgs(clickedId,e.x,e.y,e.button);
                    OnBusinessPointClickEvent(te);
                    break;
                default:
                    this.map.MousePointer = MousePointerConstants.moPanning;
                    this.map.Pan();
                    this.map.MousePointer = MousePointerConstants.moDefault;
                    _MapClickedEventArgs ea = new _MapClickedEventArgs(e.x, e.y, this.map.ToMapPoint(e.x, e.y));
                    OnMapClicked(ea);
                    break;
            }

            
        }
        /// <summary>
        /// 鼠标移动事件，用来处理鼠标指针的变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Map_MouseMoveEvent(object sender, AxMapObjects2._DMapEvents_MouseMoveEvent e)
        {
            if (businessPoint != null)
            {
                Recordset rest;
                MapObjects2.Point curp = map.ToMapPoint(e.x, e.y);
                rest = businessPoint.SearchByDistance(curp, (double)map.ToMapDistance(5f), "");
                if (rest.EOF != true)
                {
                    clickedId = Convert.ToInt32(rest.Fields.Item("Id").Value);
                    map.MousePointer = MousePointerConstants.moHotLink;
                    mouseSymbol = "hand";
                }
                else
                {
                    map.MousePointer = MousePointerConstants.moDefault;
                    mouseSymbol = "default";
                }
            }
            
        }
        /// <summary>
        /// 滚轮事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Map_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                mapZoomIn();
            }
            else
            {
                mapZoomout();
            }
        }
        /// <summary>
        /// 绘图前触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Map_BeforeLayerDraw(object sender, AxMapObjects2._DMapEvents_BeforeLayerDrawEvent e)
        {
            calScale();
            MapLayer lyr = (MapLayer)this.map.Layers.Item(e.index);    
            String name = lyr.Name;
            switch (name)
            {
                case "Highway.shp":
                    if (this.scale < 150000)
                    {
                        IMoLabelPlacer lp = (IMoLabelPlacer)lyr.Renderer;
                        if (lp.DefaultSymbol.Font.Size != symbolConst.HighwayLabelFont_L.Size)
                        {
                            lyr.Renderer = symbolConst.HighWayLabelPlacer_L;
                        }                    
                    }
                    else
                    {
                        IMoLabelPlacer lp = (IMoLabelPlacer)lyr.Renderer;
                        if (lp.DefaultSymbol.Font.Size != symbolConst.HighwayLabelFont_S.Size)
                        {
                            lyr.Renderer = symbolConst.HighWayLabelPlacer_S;
                        }       
                    }
                    break;
                case "businessPoint.shp":
                    if (this.scale < 150000)
                    {
                        if (this.scale < 25000)
                        {
                            lyr.Visible = true;
                            IMoLabelPlacer lp = (IMoLabelPlacer)lyr.Renderer;
                            if (lp.DefaultSymbol.Font.Size != symbolConst.HighwayLabelFont_L.Size)
                            {
                                lyr.Renderer = symbolConst.PointLabelPlacer_L;
                            }       
                        }
                        else
                        {
                            lyr.Visible = true;
                            lyr.Renderer = new LabelPlacerClass();
                            //IMoLabelPlacer lp = (IMoLabelPlacer)lyr.Renderer;
                            //if (lp.DefaultSymbol.Font.Size != symbolConst.HighwayLabelFont_S.Size)
                            //{
                            //    lyr.Renderer = symbolConst.PointLabelPlacer_S;
                            //}    
                        }                      
                    }
                    else
                    {
                        lyr.Visible = false;
                    }
                    break;
                case "CountryRoad06.shp":
                    if (this.scale < 20000)
                    {
                        lyr.Renderer = symbolConst.CountryRoad06LabelPlacer_S;
                        nationalRoad_symbol.Size = 8;
                        stateRoad_symbol.Size = 8;
                        townRoad_symbol.Size = 8;
                        countryRoad06_1.Visible = true;
                    }
                    else
                    {
                        lyr.Renderer = new LabelPlacerClass();
                        nationalRoad_symbol.Size = 6;
                        stateRoad_symbol.Size = 6;
                        townRoad_symbol.Size = 6;
                        countryRoad06_1.Visible = false;
                    }
                    break;
                case "CountryRoad08.shp":
                    if (this.scale < 15000)
                    {
                        lyr.Renderer = symbolConst.CountryRoad08LabelPlacer_S;
                        countryRoad06_symbol.Size = 8;
                        countryRoad06_1_symbol.Size = 10;
                        countryRoad08_symbol.Size = 6;
                    }
                    else
                    {
                        lyr.Renderer = new LabelPlacerClass();
                        countryRoad06_symbol.Size = 6;
                        countryRoad06_1_symbol.Size = 8;
                        countryRoad08_symbol.Size = 3;
                    }
                    break;
                case "Alerts.shp":
                    if (this.scale < 25000)
                    {
                        lyr.Renderer = new LabelPlacerClass();                       
                    }
                    else
                    {
                        lyr.Renderer = symbolConst.AlertPointLabelPlacer_S;
                    }
                    break;
                case "NationalRoad.shp":
                    if (this.scale < 150000)
                    {
                        nationalRoad.Renderer = symbolConst.OtherRoadLabelPlacer_S;
                        stateRoad.Renderer = symbolConst.OtherRoadLabelPlacer_S;
                        townRoad.Renderer = symbolConst.OtherRoadLabelPlacer_S;
                    }
                    else
                    {
                        nationalRoad.Renderer = new LabelPlacerClass();
                        stateRoad.Renderer = new LabelPlacerClass();
                        townRoad.Renderer = new LabelPlacerClass();
                    }
                    break;
                case "LivingPoint.shp":
                    if (this.scale < 15000)
                    {
                        townsName.Renderer = symbolConst.TownsNameLabelPlacer_L;
                        
                        LivingPoint.Visible = true;
                    }
                    else
                    {
                        townsName.Renderer = symbolConst.TownsNameLabelPlacer_S;
                        
                        LivingPoint.Visible = false;
                    }
                    break;
                case "FactoryPoint.shp":
                    if (this.scale < 10000)
                    {
                        factoryPoint.Visible = true;
                    }
                    else
                    {
                        factoryPoint.Visible = false;
                    }
                    break;
                case "ProperName.shp":
                    if (this.scale < 18000)
                    {
                        properName.Visible = true;
                    }
                    else
                    {
                        properName.Visible = false;
                    }
                    break;
                case "CountyName.shp":
                    if (this.scale < 70000)
                    {
                        countyName.Renderer = symbolConst.CountyNameLabelPlacer;
                    }
                    else
                    {
                        countyName.Renderer = new LabelPlacerClass();
                    }
                    break;

            }
            
            
        }
        /// <summary>
        /// 加载地图
        /// </summary>
        /// <param name="dataDirectory">地图文件所在文件夹路径</param>
        public void loadMap(String dataDirectory)
        {
            this.dataBase = dataDirectory;

            //restoreFile();
            //定义数据连接和图层
            #region
            
            
            #endregion
            dc.Database = this.dataBase+"\\CSX";                //指定连接位置
            restoreFile();
                if (dc.Connect())                           //连接
                {
                    //cityExtendFace.GeoDataset = dc.FindGeoDataset("cityExtendFace.shp");
                    cityExtendPoint.GeoDataset = dc.FindGeoDataset("cityExtendPoint.shp");
                    cityExtendLine.GeoDataset = dc.FindGeoDataset("cityExtendLine.shp");
                    layer1.GeoDataset = dc.FindGeoDataset("CSX.shp");
                    layer2.GeoDataset = dc.FindGeoDataset("Water.shp");
                    layer3.GeoDataset = dc.FindGeoDataset("Green.shp");
                    highway.GeoDataset = dc.FindGeoDataset("Highway.shp");
                    nationalRoad.GeoDataset = dc.FindGeoDataset("NationalRoad.shp");
                    stateRoad.GeoDataset = dc.FindGeoDataset("StateRoad.shp");
                    townRoad.GeoDataset = dc.FindGeoDataset("TownRoad.shp");
                    countryRoad06.GeoDataset = dc.FindGeoDataset("CountryRoad06.shp");
                    countryRoad06_1.GeoDataset = dc.FindGeoDataset("CountryRoad06_1.shp");
                    countryRoad08.GeoDataset = dc.FindGeoDataset("CountryRoad08.shp");
                    baseOutline.GeoDataset = dc.FindGeoDataset("BaseOutline.shp");
                    properName.GeoDataset = dc.FindGeoDataset("ProperName.shp");
                    factoryPoint.GeoDataset = dc.FindGeoDataset("FactoryPoint.shp");
                    LivingPoint.GeoDataset = dc.FindGeoDataset("LivingPoint.shp");
                    townsName.GeoDataset = dc.FindGeoDataset("TownsName.shp");
                    countyName.GeoDataset = dc.FindGeoDataset("CountyName.shp");
                    businessPoint.GeoDataset = dc.FindGeoDataset("businessPoint.shp");
                    alertPoint.GeoDataset = dc.FindGeoDataset("Alerts.shp");
                    businessPoint.Tag = "businessPoint";
                    //cityExtendFaceSymbol = cityExtendFace.Symbol;
                    cityExtendLineSymbol = cityExtendLine.Symbol;
                    cityExtendPointSymbol = cityExtendPoint.Symbol;
                    base_symbol = layer1.Symbol;
                    water_symbol = layer2.Symbol;
                    green_symbol = layer3.Symbol;
                    highway_symbol = highway.Symbol;
                    nationalRoad_symbol = nationalRoad.Symbol;
                    stateRoad_symbol = stateRoad.Symbol;
                    townRoad_symbol = townRoad.Symbol;
                    countryRoad06_symbol = countryRoad06.Symbol;
                    countryRoad06_1_symbol = countryRoad06_1.Symbol;
                    countryRoad08_symbol = countryRoad08.Symbol;
                    baseOutline_symbol = baseOutline.Symbol;
                    properName_symbol = properName.Symbol;
                    factoryPoint_symbol = factoryPoint.Symbol;
                    LivingPoint_symbol = LivingPoint.Symbol;
                    townsName_symbol = townsName.Symbol;
                    countyName_symbol = countyName.Symbol;
                    businessPoint_symbol = businessPoint.Symbol;
                    alertPoints_symbol = alertPoint.Symbol;
                    //设置符号样式
                    #region
                    //cityExtendFaceSymbol.SymbolType = SymbolTypeConstants.moFillSymbol;
                    //cityExtendFaceSymbol.Color = (uint)ColorTranslator.ToWin32(Color.FromArgb(255,255,255));

                    cityExtendPointSymbol.SymbolType = SymbolTypeConstants.moPointSymbol;
                    cityExtendPointSymbol.Outline = false;
                    cityExtendPointSymbol.Color = (uint)ColorTranslator.ToWin32(Color.FromArgb(255, 255, 255));

                    cityExtendLineSymbol.SymbolType = SymbolTypeConstants.moLineSymbol;
                    cityExtendLineSymbol.Color = MapSymbolConstants.BaseOutlineColorConstants;
                    cityExtendLineSymbol.Size = 1;

                    base_symbol.SymbolType = SymbolTypeConstants.moFillSymbol;
                    base_symbol.Color = MapSymbolConstants.BaseColorConstants;
                    base_symbol.Outline = false;
                    //base_symbol.OutlineColor = MapSymbolConstants.BaseOutlineColorConstants;

                    water_symbol.SymbolType = SymbolTypeConstants.moFillSymbol;
                    water_symbol.Color = MapSymbolConstants.WaterColorConstants;
                    water_symbol.Outline = false;

                    green_symbol.SymbolType = SymbolTypeConstants.moFillSymbol;
                    green_symbol.Color = MapSymbolConstants.GreenColorConstants;
                    green_symbol.Outline = false;

                    highway_symbol.SymbolType = SymbolTypeConstants.moLineSymbol;
                    highway_symbol.Style = 0;
                    highway_symbol.Color = MapSymbolConstants.HighWayColorConstants;
                    highway_symbol.Size = 4;

                    nationalRoad_symbol.SymbolType = SymbolTypeConstants.moLineSymbol;
                    nationalRoad_symbol.Style = 0;
                    nationalRoad_symbol.Color = MapSymbolConstants.NationalRoadColorConstants;
                    nationalRoad_symbol.Size = 6;

                    stateRoad_symbol.SymbolType = SymbolTypeConstants.moLineSymbol;
                    stateRoad_symbol.Style = 0;
                    stateRoad_symbol.Color = MapSymbolConstants.StateRoadColorConstants;
                    stateRoad_symbol.Size = 6;

                    townRoad_symbol.SymbolType = SymbolTypeConstants.moLineSymbol;
                    townRoad_symbol.Style = 0;
                    townRoad_symbol.Color = MapSymbolConstants.TownRoadColorConstants;
                    townRoad_symbol.Size = 6;

                    countryRoad06_symbol.SymbolType = SymbolTypeConstants.moLineSymbol;
                    countryRoad06_symbol.Style = 0;
                    countryRoad06_symbol.Color = MapSymbolConstants.CountryRoad06ColorConstants;
                    countryRoad06_symbol.Size = 6;

                    countryRoad06_1_symbol.SymbolType = SymbolTypeConstants.moLineSymbol;
                    countryRoad06_1_symbol.Style = 0;
                    countryRoad06_1_symbol.Color = MapSymbolConstants.RoadBaseColorConstants;
                    countryRoad06_1_symbol.Size = 8;

                    countryRoad08_symbol.SymbolType = SymbolTypeConstants.moLineSymbol;
                    countryRoad08_symbol.Style = 0;
                    countryRoad08_symbol.Color = MapSymbolConstants.CountryRoad08ColorConstants;
                    countryRoad08_symbol.Size = 3;

                    baseOutline_symbol.Color = MapSymbolConstants.BaseOutlineColorConstants;

                    properName_symbol.SymbolType = SymbolTypeConstants.moPointSymbol;
                    properName_symbol.Color = MapSymbolConstants.BaseColorConstants;
                    properName_symbol.Outline = false;

                    factoryPoint_symbol.SymbolType = SymbolTypeConstants.moPointSymbol;
                    factoryPoint_symbol.Style = 0;
                    factoryPoint_symbol.Color = (uint)ColorTranslator.ToWin32(Color.FromArgb(50, 50, 50));
                    factoryPoint_symbol.Size = 5;

                    LivingPoint_symbol.SymbolType = SymbolTypeConstants.moPointSymbol;
                    LivingPoint_symbol.Style = 0;
                    LivingPoint_symbol.Color = (uint)ColorTranslator.ToWin32(Color.FromArgb(0, 50, 255)); 
                    LivingPoint_symbol.Size = 5;

                    townsName_symbol.SymbolType = SymbolTypeConstants.moPointSymbol;
                    townsName_symbol.Style = 1;
                    townsName_symbol.Color = (uint)ColorTranslator.ToWin32(Color.FromArgb(255, 0, 0)); 
                    townsName_symbol.Size = 5;

                    countyName_symbol.SymbolType = SymbolTypeConstants.moPointSymbol;
                    countyName_symbol.Style = 0;
                    countyName_symbol.Color = (uint)ColorTranslator.ToWin32(Color.FromArgb(255, 0, 0));
                    countyName_symbol.Size = 7;

                    businessPoint.Visible = false;
                    countryRoad06_1.Visible = false;
                    CustomMarker cm = new CustomMarker(this.dataBase + "\\res\\drop_black_18dp.png");
                    businessPoint_symbol.Custom = cm;

                    alertPoints_symbol.SymbolType = SymbolTypeConstants.moPointSymbol;
                    alertPoints_symbol.Style = 0;
                    alertPoints_symbol.Size = 10;
                    alertPoints_symbol.Color = (uint)ColorTranslator.ToWin32(Color.FromArgb(255, 0, 0));
                    #endregion
                    //设置标注
                    #region
                    cityExtendPoint.Renderer = symbolConst.CityExtendPointPlacer;
                    highway.Renderer = symbolConst.HighWayLabelPlacer_S;
                    //nationalRoad.Renderer = symbolConst.OtherRoadLabelPlacer_S;
                    //stateRoad.Renderer = symbolConst.OtherRoadLabelPlacer_S;
                    //townRoad.Renderer = symbolConst.OtherRoadLabelPlacer_S;
                    properName.Renderer = symbolConst.ProperNameLabelPlacer;
                    factoryPoint.Renderer = symbolConst.FactoryPointLabelPlacer;
                    LivingPoint.Renderer = symbolConst.LivingPointLablePlacer;
                    businessPoint.Renderer = symbolConst.PointLabelPlacer_S;
                    countryRoad06.Renderer = symbolConst.CountryRoad06LabelPlacer_S;
                    countryRoad08.Renderer = symbolConst.CountryRoad08LabelPlacer_S;
                    alertPoint.Renderer = symbolConst.AlertPointLabelPlacer_S;
                    #endregion
                    //map.Layers.Add(cityExtendFace);
                    map.Layers.Add(cityExtendPoint);
                    map.Layers.Add(cityExtendLine);
                    map.Layers.Add(layer1);
                    map.Layers.Add(layer2);
                    map.Layers.Add(layer3);
                    map.Layers.Add(countryRoad08);
                    map.Layers.Add(countryRoad06_1);
                    map.Layers.Add(countryRoad06);
                    map.Layers.Add(townRoad);
                    map.Layers.Add(stateRoad);
                    map.Layers.Add(nationalRoad);
                    map.Layers.Add(highway);
                    map.Layers.Add(baseOutline);
                    map.Layers.Add(properName);
                    map.Layers.Add(factoryPoint);
                    map.Layers.Add(LivingPoint);
                    map.Layers.Add(townsName);
                    map.Layers.Add(countyName);
                    map.Layers.Add(businessPoint);
                    map.Layers.Add(alertPoint);
                    map.CenterAt(113.075, 28.249);
                    clearEvent();
                    map.Refresh();
                    //calScale();
                }
                return;
            
        }
        /// <summary>
        /// 加载地图，使用默认地图路径
        /// </summary>
        public void loadMap()
        {
            this.loadMap(this.dataBase);
        }
        /// <summary>
        /// 重新加载地图
        /// </summary>
        public void reload()
        {
            this.map.Layers.Clear();
            loadMap();
            this.map.Refresh();
        }

        /// <summary>
        /// 地图放大
        /// </summary>
        public void mapZoomIn()
        {
            if (this.scale >= 2000)
            {
                this.map.MousePointer = MapObjects2.MousePointerConstants.moZoomIn;
                MapObjects2.Rectangle myrc = new MapObjects2.Rectangle();
                myrc = map.Extent;
                myrc.ScaleRectangle(0.8);

                this.map.Extent = myrc;
                this.map.MousePointer = MapObjects2.MousePointerConstants.moDefault;
            }
            
            //calScale();
        }
        /// <summary>
        /// 地图放大
        /// </summary>
        /// <param name="factor">放大因子（0~1之间）</param>
        public void mapZoomIn(float factor)
        {
            if (this.scale >= 2000)
            {
                this.map.MousePointer = MapObjects2.MousePointerConstants.moZoomIn;
                MapObjects2.Rectangle myrc = new MapObjects2.Rectangle();
                myrc = map.Extent;
                if (factor > 0 && factor < 1)
                {
                    myrc.ScaleRectangle(factor);
                }
                else
                {
                    myrc.ScaleRectangle(0.8);
                }

                this.map.Extent = myrc;
                this.map.MousePointer = MapObjects2.MousePointerConstants.moDefault;
            }
            //calScale();
        }

        /// <summary>
        /// 地图缩小
        /// </summary>
        public void mapZoomout()
        {
            this.map.MousePointer = MapObjects2.MousePointerConstants.moZoomOut;
            MapObjects2.Rectangle myrc = new MapObjects2.Rectangle();
            myrc = map.Extent;
            myrc.ScaleRectangle(1.2);

            this.map.Extent = myrc;
            this.map.MousePointer = MapObjects2.MousePointerConstants.moDefault;
            //calScale();
        }
        /// <summary>
        /// 地图缩小
        /// </summary>
        /// <param name="factor">缩小因子（大于1）</param>
        public void mapZoomout(float factor)
        {
            this.map.MousePointer = MapObjects2.MousePointerConstants.moZoomOut;
            MapObjects2.Rectangle myrc = new MapObjects2.Rectangle();
            myrc = map.Extent;
            if (factor > 1)
            {
                myrc.ScaleRectangle(factor);
            }
            else
            {
                myrc.ScaleRectangle(1.2);
            }
           
            this.map.Extent = myrc;
            this.map.MousePointer = MapObjects2.MousePointerConstants.moDefault;
            //calScale();
        }
        public void mapFullExtent()
        {
            this.map.Extent = this.map.FullExtent;
        }
        //定义业务点单机事件委托
        /// <summary>
        /// 业务点单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void MapBusinessPoint_ClickedEventHandler(object sender, _MapBusinessPoint_ClickedEventArgs e);
        //声明事件对象
        /// <summary>
        /// 业务点单击事件
        /// </summary>
        public event MapBusinessPoint_ClickedEventHandler BusinessPointClickedEvent;
        //业务点单机事件触发方法
        public virtual void OnBusinessPointClickEvent(_MapBusinessPoint_ClickedEventArgs e)
        {
            if (BusinessPointClickedEvent != null)
            {
                BusinessPointClickedEvent(this, e);
            }
        }

        public delegate void MapScaleChangedEvnetHandler(object sender, _MapScaleChangedEventArgs e);
        /// <summary>
        /// 比例尺改变事件
        /// </summary>
        public event MapScaleChangedEvnetHandler ScaleChangedEvent;
        public virtual void OnScaleChanged(_MapScaleChangedEventArgs e)
        {
            if (ScaleChangedEvent != null)
            {
                ScaleChangedEvent(this, e);
            }
        }

        public delegate void MapClickEventHandler(object sender, _MapClickedEventArgs e);
        /// <summary>
        /// 地图非业务点点击事件
        /// </summary>
        public event MapClickEventHandler MapClickedEvent;
        public virtual void OnMapClicked(_MapClickedEventArgs e)
        {
            if (MapClickedEvent != null)
            {
                MapClickedEvent(this, e);
            }
        }
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="Ids">企业Id</param>
        public void loadEvent(int[] Ids)
        {
            if(businessPoint!=null&&alertPoint!=null){
                Recordset rest = null;
                Recordset set = alertPoint.Records;
                try
                {
                    while (set.EOF != true)
                    {
                        set.Delete();                       
                        set.MoveNext();
                    }
                    set.StopEditing();
                    for (int i = 0; i < Ids.Length; i++)
                    {
                        rest = businessPoint.SearchExpression("Id=" + Ids[i].ToString());
                        if (rest.EOF != true)
                        {
                            MapObjects2.Point pt = (MapObjects2.Point)rest.Fields.Item("shape").Value;
                            //mtl.AddEvent(pt, 0);
                            set.AddNew();
                            set.Fields.Item("shape").Value = pt;
                            set.Fields.Item("id").Value = rest.Fields.Item("id");
                            set.Fields.Item("name").Value = rest.Fields.Item("name");
                            //set.Fields.Item("info").Value = "sth \n sth";
                            set.Update();
                        }
                    }
                    set.StopEditing();
                }
                catch (Exception e)
                {
                    set.StopEditing();
                }
                map.Refresh();
            }
            
            //calScale();
        }
        /// <summary>
        /// 清除所有事件点
        /// </summary>
        public void clearEvent()
        {
            if (alertPoint != null)
            {
                Recordset set = alertPoint.Records;
                try
                {
                    while (set.EOF != true)
                    {
                        set.Delete();
                        set.MoveNext();
                    }
                    set.StopEditing();                  
                }
                catch (Exception e)
                {
                    set.StopEditing();
                }
                map.Refresh();
            }
        }
        /// <summary>
        /// 清除事件
        /// </summary>
        /// <param name="Ids"></param>
        public void clearEvent(int[] Ids)
        {
            if (alertPoint != null)
            {
                Recordset rest = alertPoint.Records;
                try
                {
                    for (int i = 0; i < Ids.Length; i++)
                    {
                        rest = alertPoint.SearchExpression("Id=" + Ids[i].ToString());
                        if (rest.EOF != true)
                        {
                            rest.Delete();
                        }
                        rest.StopEditing();
                    }                  
                }
                catch (Exception e)
                {
                    rest.StopEditing();
                }
                map.Refresh();
            }
        }
        /// <summary>
        /// 添加事件点
        /// </summary>
        /// <param name="Ids">企业Id</param>
        public void appendEvent(int[] Ids)
        {
            if (businessPoint != null && alertPoint!=null)
            {
                Recordset rest;
                Recordset set = alertPoint.Records;
                try
                {
                    for (int i = 0; i < Ids.Length; i++)
                    {
                        rest = businessPoint.SearchExpression("Id=" + Ids[i].ToString());
                        if (rest.EOF != true)
                        {
                            MapObjects2.Point pt = (MapObjects2.Point)rest.Fields.Item("shape").Value;
                            Recordset tempSet = alertPoint.SearchExpression("Id=" + Ids[i].ToString());
                            if (tempSet.EOF == true)
                            {
                                set.AddNew();
                                set.Fields.Item("shape").Value = pt;
                                set.Fields.Item("id").Value = rest.Fields.Item("id");
                                set.Fields.Item("name").Value = rest.Fields.Item("name");
                                //set.Fields.Item("info").Value = "sth \n sth";
                                set.Update();
                            }                            
                        }
                    }
                    set.StopEditing();               
                }
                catch (Exception e)
                {
                    set.StopEditing();
                }
                map.Refresh();
            }         
           
        }
        /// <summary>
        /// 闪烁企业
        /// </summary>
        /// <param name="Id">企业id</param>
        /// <param name="num">闪烁次数</param>
        public void flashPoint(int Id,short num)
        {
            if (businessPoint != null)
            {
                Recordset rest;
                rest = businessPoint.SearchExpression("Id=" + Id.ToString());
                if (rest.EOF != true)
                {
                    MapObjects2.Point pt = (MapObjects2.Point)rest.Fields.Item("shape").Value;
                    this.map.FlashShape(pt, num);
                }
            }
        }
        /// <summary>
        /// 闪烁事件点
        /// </summary>
        /// <param name="Id">企业id</param>
        /// <param name="num">闪烁次数</param>
        public void flashEventPoint(int Id, short num)
        {
            if (alertPoint != null)
            {
                Recordset rest;
                rest = alertPoint.SearchExpression("Id=" + Id.ToString());
                if (rest.EOF != true)
                {
                    MapObjects2.Point pt = (MapObjects2.Point)rest.Fields.Item("shape").Value;
                    this.map.FlashShape(pt, num);
                }
            }
        }
        /// <summary>
        /// 定位到点集
        /// </summary>
        /// <param name="Ids">点集Id数组</param>
        public void centerAt(int[] Ids)
        {
            Recordset rest;
            if (businessPoint != null)
            {
                List<double> xs = new List<double>();
                List<double> ys = new List<double>();
                for (int i = 0; i < Ids.Length; i++)
                {
                    rest = businessPoint.SearchExpression("Id=" + Ids[i].ToString());
                    if (rest.EOF != true)
                    {
                        MapObjects2.Point pt = (MapObjects2.Point)rest.Fields.Item("shape").Value;
                        xs.Add(pt.X);
                        ys.Add(pt.Y);
                    }
                }
                if (xs.Count != 0)
                {
                    if (xs.Count > 1)
                    {
                        double x1 = xs.Min();
                        double y1 = ys.Min();
                        double x2 = xs.Max();
                        double y2 = ys.Max();

                        MapObjects2.Rectangle myrc = new MapObjects2.Rectangle();
                        myrc.Top = y2;
                        myrc.Left = x1;
                        myrc.Right = x2;
                        myrc.Bottom = y1;
                        myrc.ScaleRectangle(1.2);
                        this.map.Extent = myrc;
                    }
                    else
                    {
                        MapObjects2.Rectangle myrc = new MapObjects2.Rectangle();
                        MapObjects2.Point pt = myrc.Center;
                        pt.X = xs[0];
                        pt.Y = ys[0];
                        myrc.Left = pt.X - 0.015;
                        myrc.Right = pt.X + 0.015;
                        myrc.Top = pt.Y + 0.015;
                        myrc.Bottom = pt.Y - 0.015;
                        this.map.Extent = myrc;
                    }
                }
            }
            //calScale();
        }
        /// <summary>
        /// 定位到地图上指定位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void centerAt(double x, double y)
        {
            MapObjects2.Rectangle myrc = new MapObjects2.Rectangle();
            MapObjects2.Point pt = myrc.Center;
            pt.X = x;
            pt.Y = y;
            myrc.Left = pt.X - 0.015;
            myrc.Right = pt.X + 0.015;
            myrc.Top = pt.Y + 0.015;
            myrc.Bottom = pt.Y - 0.015;
            this.map.Extent = myrc;
        }
        /// <summary>
        /// 计算当前比例尺
        /// </summary>
        private void calScale()
        {
            Graphics g = Graphics.FromHwnd((System.IntPtr)map.hWnd);
            double dLen1 = map.ToMapDistance((float)map.Width) * 102834.742580260897;
            double dLen2 = map.Width / g.DpiX * 2.54000508 / 100;
            this.scale = dLen1 / dLen2;

            _MapScaleChangedEventArgs args = new _MapScaleChangedEventArgs(this.scale);
            OnScaleChanged(args);
        }
        /// <summary>
        /// 获取当前所有业务点Id
        /// </summary>
        /// <returns>业务点Id列表</returns>
        public List<int> getCurrentIdArray()
        {
            List<int> result = new List<int>();
            if (businessPoint != null)
            {
                try
                {
                    Recordset rest = businessPoint.Records;
                    while (rest.EOF != true)
                    {
                        int id = (int)rest.Fields.Item("id").Value;
                        result.Add(id);
                        rest.MoveNext();
                    }
                }
                catch (Exception e)
                {
                    return new List<int>();
                }
               
            }
            return result;
        }
        /// <summary>
        /// 删除指定Id的业务点
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>是否删除成功</returns>
        public bool deletePoint(int Id)
        {
            if (businessPoint != null)
            {
                Recordset rest = businessPoint.SearchExpression("Id=" + Id.ToString());
                if (rest.EOF != true)
                {                    
                    try
                    {
                        rest.Delete();
                        rest.StopEditing();
                        this.map.Refresh();
                    }
                    catch (Exception e)
                    {
                        rest.StopEditing();
                        this.map.Refresh();
                    }
                }               
            }
            return checkFile();
            //return true;
        }
        public bool clearBusinessPoint()
        {
            if (businessPoint != null)
            {
                Recordset set = businessPoint.Records;
                try
                {
                    while (set.EOF != true)
                    {
                        set.Delete();
                        set.MoveNext();
                    }
                    set.StopEditing();
                }
                catch (Exception e)
                {
                    set.StopEditing();
                }
                map.Refresh();
            }
            return checkFile();
            //return true;
        }
        /// <summary>
        /// 根据Id获得地图业务点信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public _MapBussinessPoint getPointById(int Id)
        {
            _MapBussinessPoint point = null;
            if (businessPoint != null)
            {
                Recordset rest = businessPoint.SearchExpression("Id=" + Id.ToString());
                if (rest.EOF != true)
                {
                    point = new _MapBussinessPoint();
                    point.Id = Id;
                    point.Name = rest.Fields.Item("Name").Value.ToString();
                    point.X = Convert.ToDouble(rest.Fields.Item("X").Value);
                    point.Y = Convert.ToDouble(rest.Fields.Item("Y").Value);
                }
            }
            return point;
        }
        /// <summary>
        /// 更新业务点图层
        /// </summary>
        /// <param name="pointList">待更新的点列表</param>
        /// <returns>是否更新成功</returns>
        public bool updatePointLayer(List<_MapBussinessPoint> pointList)
        {
            if (businessPoint != null)
            {
                Recordset set = businessPoint.Records;
                foreach (_MapBussinessPoint p in pointList)
                {
                    if (p != null && p.Id > 0)
                    {
                        Recordset rest = businessPoint.SearchExpression("Id=" + p.Id.ToString());
                        try
                        {
                            if (rest.EOF == true)
                            {
                                if (p.X > 0 && p.Y > 0)
                                {
                                    MapObjects2.Point point = new MapObjects2.Point();
                                    point.X = p.X;
                                    point.Y = p.Y;

                                    set.AddNew();
                                    set.Fields.Item("Shape").Value = point;
                                    set.Fields.Item("Id").Value = p.Id;
                                    if (p.Name != null)
                                    {
                                        set.Fields.Item("Name").Value = p.Name;
                                    }
                                    set.Fields.Item("X").Value = p.X;
                                    set.Fields.Item("Y").Value = p.Y;
                                    set.Update();
                                }                               
                            }
                            else
                            {
                                rest.Edit();
                                if (p.X > 0 && p.Y > 0)
                                {
                                    MapObjects2.Point point = (MapObjects2.Point)rest.Fields.Item("Shape").Value;
                                    point.X = p.X;
                                    point.Y = p.Y;
                                    rest.Fields.Item("Shape").Value = point;
                                    rest.Fields.Item("X").Value = p.X;
                                    rest.Fields.Item("Y").Value = p.Y;
                                }
                                if (p.Name != null)
                                {
                                    rest.Fields.Item("Name").Value = p.Name;
                                }                                                               
                                rest.Update();
                            }
                            rest.StopEditing();
                            set.StopEditing();
                            this.map.Refresh();
                        }
                        catch (Exception e)
                        {
                            rest.StopEditing();
                            set.StopEditing();
                            this.map.Refresh();
                            return false;
                        }                      
                    }
                }
            }

            return checkFile();
            //return true;
        }

        private bool restoreFile()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    string aimPath = this.dataBase + "\\CSX";
                    if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    {
                        aimPath += Path.DirectorySeparatorChar;
                    }
                    string[] fileList = Directory.GetFileSystemEntries(aimPath, "businessPoint.*");
                    //foreach (string filePath in fileList)
                    //{
                    //    File.Delete(filePath);
                    //}
                    fileList = Directory.GetFileSystemEntries(aimPath, "Backup_businessPoint.*");
                    foreach (string filePath in fileList)
                    {
                        string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                        string fileExName = fileName.Substring(fileName.IndexOf(".") + 1);
                        File.Copy(filePath, aimPath + "businessPoint." + fileExName);
                    }
                    scope.Complete();
                    return false;
                }
                catch (Exception e)
                {
                    scope.Dispose();
                }
            }
            return true;
        }

        private bool checkFile()
        {
            if (businessPoint != null)
            {
                Recordset rest = businessPoint.SearchExpression("Id=-1");
                string aimPath = this.dataBase + "\\CSX";
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                {
                    aimPath += Path.DirectorySeparatorChar;
                }
                if (rest.EOF != true)
                {
                    return false;
                }
                //else
                //{
                //    using (TransactionScope scope = new TransactionScope())
                //    {
                //        try
                //        {
                //            string[] fileList = Directory.GetFileSystemEntries(aimPath, "Backup_businessPoint.*");
                //            foreach (string filePath in fileList)
                //            {
                //                File.Delete(filePath);
                //            }
                //            fileList = Directory.GetFileSystemEntries(aimPath, "businessPoint.*");
                //            foreach (string filePath in fileList)
                //            {
                //                string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                //                File.Copy(filePath, aimPath + "Backup_" + fileName);
                //            }
                //            scope.Complete();
                //            return true;
                //        }
                //        catch (Exception e)
                //        {
                //            scope.Dispose();
                //            return false;
                //        }
                //    }

                //}
            }
            return true;
        }
    }
    
    /// <summary>
    /// 业务点单击事件参数类
    /// </summary>
    public class _MapBusinessPoint_ClickedEventArgs : EventArgs
    {
        public readonly int Id;

        public readonly double X;

        public readonly double Y;

        public readonly int Button;
        public _MapBusinessPoint_ClickedEventArgs(int id,double x,double y,int button)
        {
            Id = id;
            X = x;
            Y = y;
            Button = button;
        }
    }

    public class _MapScaleChangedEventArgs : EventArgs
    {
        public readonly double Scale;

        public _MapScaleChangedEventArgs(double scale){
            Scale = scale;
        }
    }

    public class _MapClickedEventArgs:EventArgs{
        public readonly double X;
        public readonly double Y;
        public readonly double map_X;
        public readonly double map_Y;

        public _MapClickedEventArgs(double x,double y,MapObjects2.Point point){
            this.X=x;
            this.Y=y;
            this.map_X = point.X;
            this.map_Y = point.Y;
        }
    }

    public class _MapBussinessPoint
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public _MapBussinessPoint(int id, string name, double x, double y)
        {
            this.Id = id;
            this.Name = name;
            this.X = x;
            this.Y = y;
        }
        public _MapBussinessPoint()
        {
            this.Id = -1;
            this.X = -1;
            this.Y = -1;
        }
        public _MapBussinessPoint(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public _MapBussinessPoint(int id, double x, double y)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
        }
    }
}
