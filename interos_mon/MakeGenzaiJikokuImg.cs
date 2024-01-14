using PITempCS.mon;
using System;
using System.Drawing;
using Zbx1425.DXDynamicTexture;

unsafe public class MakeGenzaiJikokuImg {
	private TimeSpan time;
    DWST_RECT_TEXT draw_inf;

    Bitmap bmp;
    Graphics g;

	public MakeGenzaiJikokuImg (int x, int y, int size, Color color, String font, Color bg) {
        this.draw_inf = new DWST_RECT_TEXT(x, y, size, color, font, bg);
        this.bmp = new Bitmap(210, 30);
        this.g = Graphics.FromImage(this.bmp);
    }

    public void Dispose() {
        this.g.Dispose();
    }

    public void Draw (GDIHelper i_gdi, TimeSpan i_time) {
        if (i_time.Hours != this.time.Hours) {
            i_gdi.Graphics.FillRectangle(new SolidBrush(this.draw_inf.bg), new Rectangle(this.draw_inf.x, this.draw_inf.y, 50, 30));
            i_gdi.Graphics.DrawString(String.Format("{0, 2}時", i_time.Hours),
                this.draw_inf.font, new SolidBrush(this.draw_inf.color), new Point(this.draw_inf.x, this.draw_inf.y));
        }
        if (i_time.Minutes != this.time.Minutes) {
            i_gdi.Graphics.FillRectangle(new SolidBrush(this.draw_inf.bg), new Rectangle(this.draw_inf.x+70, this.draw_inf.y, 50, 30));
            i_gdi.Graphics.DrawString(String.Format("{0, 2}分", i_time.Minutes),
                this.draw_inf.font, new SolidBrush(this.draw_inf.color), new Point(this.draw_inf.x+70, this.draw_inf.y));
        }
        if (i_time.Seconds != this.time.Seconds) {
            i_gdi.Graphics.FillRectangle(new SolidBrush(this.draw_inf.bg), new Rectangle(this.draw_inf.x+140, this.draw_inf.y, 50, 30));
            i_gdi.Graphics.DrawString(String.Format("{0, 2}秒", i_time.Seconds),
                this.draw_inf.font, new SolidBrush(this.draw_inf.color), new Point(this.draw_inf.x+140, this.draw_inf.y));
        }

        this.time = i_time;
    }

    public Bitmap get() {
        return this.bmp;
    }
    public void make (TimeSpan i_time) {
		
        if (i_time.Hours != this.time.Hours) {
			this.g.FillRectangle(new SolidBrush(this.draw_inf.bg), new Rectangle(0, 0, 70, 30));
			this.g.DrawString(String.Format("{0, 2}時", i_time.Hours), 
				this.draw_inf.font, new SolidBrush(this.draw_inf.color), new Point(0, 0));
        }
		if (i_time.Minutes != this.time.Minutes) {
            this.g.FillRectangle(new SolidBrush(this.draw_inf.bg), new Rectangle(70, 0, 70, 30));
            this.g.DrawString(String.Format("{0, 2}分", i_time.Minutes),
                this.draw_inf.font, new SolidBrush(this.draw_inf.color), new Point(70, 0));
        }
		if(i_time.Seconds != this.time.Seconds) {
            this.g.FillRectangle(new SolidBrush(this.draw_inf.bg), new Rectangle(140, 0, 70, 30));
            this.g.DrawString(String.Format("{0, 2}秒", i_time.Seconds),
                this.draw_inf.font, new SolidBrush(this.draw_inf.color), new Point(140, 0));
        }

		this.time = i_time; 
    }
}
