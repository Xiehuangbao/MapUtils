using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;

namespace MapUtils
{
    class CustomMarker : ESRI.MapObjects2.Custom.ICustomMarker
    {
        #region
        private IntPtr m_hdc;
        private System.Drawing.Graphics m_graphics;
        private Image image;
        //private Bitmap image;
        public CustomMarker(string fileName)
        {
            image = Image.FromFile(fileName);  
            //image = new Bitmap(fileName);
            m_graphics = Graphics.FromImage(image);
        }
        #endregion


        #region ICustomMarker 成员
        //绘制图片  
        void ESRI.MapObjects2.Custom.ICustomMarker.Draw(int hDC, int x, int y)
        {
            //calls drawing primitve to draw the symbol          
            int height = this.image.Height;
            int width = this.image.Width;
            //this.m_CustomStyle继承自CustomSym  
            //绘制图形  
            m_graphics.DrawImage(this.image, x - width / 2, y - height / 2);
        }
        //刷新m_graphics  
        void ESRI.MapObjects2.Custom.ICustomMarker.ResetDC(int hDC)
        {
            //Cleans up and re-establishes the original device context  
            if (m_graphics != null)
            {
                //m_graphics.ReleaseHdc(m_hdc);
                //m_graphics.ReleaseHdc(new IntPtr(hDC));
                m_graphics.Dispose();
                m_graphics = null;
            }
        }
        //设置m_graphics  
        void ESRI.MapObjects2.Custom.ICustomMarker.SetupDC(int hDC, double dpi, object pBaseSym)
        {
            //establishes the device context and sets up symbol characteristics  
            m_hdc = new IntPtr(hDC);

            //Get the graphics drawing surface  
            m_graphics = System.Drawing.Graphics.FromHdc(m_hdc);
        }

        #endregion  }  
    }
}
