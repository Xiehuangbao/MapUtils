using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapUtils
{
    public partial class Form1 : Form
    {
        MapControl mc;
        public Form1()
        {
            InitializeComponent();
            mc = new MapControl(this.axMap1);
            mc.loadMap(Environment.CurrentDirectory + "\\Map");
            this.panel1.MouseWheel += new MouseEventHandler(mc.Map_MouseWheel);

            //业务点被点击事件
            mc.BusinessPointClickedEvent += new MapControl.MapBusinessPoint_ClickedEventHandler(eventTest);
            //比例尺改变事件
            mc.ScaleChangedEvent += new MapControl.MapScaleChangedEvnetHandler(showScale);
            //非业务点点击事件
            mc.MapClickedEvent += new MapControl.MapClickEventHandler(showXY);

            //mc.loadAlert(new int[] { 410020, 410011, 410010, 410005 });
            //mc.centerAt(new int[] { 410009, 410011, 410010, });
            //mc.centerAt(113.1445, 28.3842);
            //mc.mapFullExtent();

            //获取当前图层存在的所有点的Id
            List<int> ids = mc.getCurrentIdArray();

            MapObjects2.Point pt = this.axMap1.ToMapPoint(400f, 300f);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
        }

        public void eventTest(object sender, _MapBusinessPoint_ClickedEventArgs e)
        {
            MessageBox.Show(e.Id.ToString());
            MapObjects2.Point pt = axMap1.ToMapPoint((float)e.X,(float)e.Y);
            mc.centerAt(new int[] { e.Id });
            //axMap1.CenterAt(new int[] { e.Id });
            
            
        }
        public void showScale(object sender, _MapScaleChangedEventArgs e)
        {
            textBox1.Text = e.Scale.ToString();
        }

        public void showXY(object sender, _MapClickedEventArgs e)
        {
            this.textBox3.Text = e.map_X.ToString() + "," + e.map_Y.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mc.loadEvent(new int[] { Convert.ToInt32(this.textBox2.Text) });
            mc.centerAt(new int[] { Convert.ToInt32(this.textBox2.Text) });
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mc.clearEvent((new int[] { Convert.ToInt32(this.textBox2.Text) }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mc.appendEvent(new int[] { Convert.ToInt32(this.textBox2.Text) });
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mc.reload();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mc.flashPoint(Convert.ToInt32(this.textBox2.Text),5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //根据Id删除点
            mc.deletePoint(Convert.ToInt32(this.textBox2.Text));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _MapBussinessPoint point1 = new _MapBussinessPoint(410001,"企业1");
            _MapBussinessPoint point2 = new _MapBussinessPoint(410016, "企业333",113.091950,28.272890);
            List<_MapBussinessPoint> pl = new List<_MapBussinessPoint>();
            //pl.Add(point1);
            pl.Add(point2);
            //更新图层点，Id存在的则更新，Id不存在的则添加
            mc.updatePointLayer(pl);
            mc.centerAt(new int[] { 410016 });
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mc.clearBusinessPoint();
        }
    }
}
