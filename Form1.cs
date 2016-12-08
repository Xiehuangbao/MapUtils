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
            mc.BusinessPointClickedEvent += new MapControl.MapBusinessPoint_ClickedEventHandler(eventTest);
            //mc.loadAlert(new int[] { 410020, 410011, 410010, 410005 });
            //mc.centerAt(new int[] { 410009, 410011, 410010, });
            //mc.centerAt(113.1445, 28.3842);
            //mc.mapFullExtent();
            mc.ScaleChangedEvent += new MapControl.MapScaleChangedEvnetHandler(showScale);
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
    }
}
