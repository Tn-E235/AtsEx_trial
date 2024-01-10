using System;
using System.Drawing;
using Zbx1425.DXDynamicTexture;

unsafe public class MakeGenzaiJikokuImage {
    private int x;
    private int y;
    private int size;
	private Color color;
	private Font font;
	private Color bg;
	private TimeSpan time;

    Bitmap bmp;
    Graphics g;

    public MakeGenzaiJikokuImage() {
        this.x     = 0;
        this.y     = 0;
		this.size  = 16;
		this.color = Color.White;
		this.font  = new Font("VL ゴシック", this.size);
		this.bg    = Color.Black;
		this.time  = new TimeSpan();
        this.bmp   = new Bitmap(210, 30);
        this.g     = Graphics.FromImage(bmp);
    }

	public MakeGenzaiJikokuImage (int x, int y, int size, Color color, String font, Color bg) {
        this.x     = x;
        this.y     = y;
		this.size  = size;
		this.color = color;
		this.font  = new Font(font, this.size);
		this.bg    = bg;
		this.time  = new TimeSpan(0);
        this.bmp   = new Bitmap(210, this.size);
    }

    public void Dispose() {
        this.g.Dispose();
    }

    public void Draw (GDIHelper i_gdi, TimeSpan i_time) {
        if (i_time.Hours != this.time.Hours) {
            i_gdi.Graphics.FillRectangle(new SolidBrush(this.bg), new Rectangle(this.x, this.y, 50, 30));
            i_gdi.Graphics.DrawString(String.Format("{0, 2}時", i_time.Hours),
                this.font, new SolidBrush(this.color), new Point(this.x, this.y));
        }
        if (i_time.Minutes != this.time.Minutes) {
            i_gdi.Graphics.FillRectangle(new SolidBrush(this.bg), new Rectangle(this.x+70, this.y, 50, 30));
            i_gdi.Graphics.DrawString(String.Format("{0, 2}分", i_time.Minutes),
                this.font, new SolidBrush(this.color), new Point(this.x+70, this.y));
        }
        if (i_time.Seconds != this.time.Seconds) {
            i_gdi.Graphics.FillRectangle(new SolidBrush(this.bg), new Rectangle(this.x+140, this.y, 50, 30));
            i_gdi.Graphics.DrawString(String.Format("{0, 2}秒", i_time.Seconds),
                this.font, new SolidBrush(this.color), new Point(this.x+140, this.y));
        }

        this.time = i_time;
    }

    public Bitmap get() {
        return this.bmp;
    }
    public void make (TimeSpan i_time) {
		
        if (i_time.Hours != this.time.Hours) {
			this.g.FillRectangle(new SolidBrush(this.bg), new Rectangle(0, 0, 70, 30));
			this.g.DrawString(String.Format("{0, 2}時", i_time.Hours), 
				this.font, new SolidBrush(this.color), new Point(0, 0));
        }
		if (i_time.Minutes != this.time.Minutes) {
            this.g.FillRectangle(new SolidBrush(this.bg), new Rectangle(70, 0, 70, 30));
            this.g.DrawString(String.Format("{0, 2}分", i_time.Minutes),
                this.font, new SolidBrush(this.color), new Point(70, 0));
        }
		if(i_time.Seconds != this.time.Seconds) {
            this.g.FillRectangle(new SolidBrush(this.bg), new Rectangle(140, 0, 70, 30));
            this.g.DrawString(String.Format("{0, 2}秒", i_time.Seconds),
                this.font, new SolidBrush(this.color), new Point(140, 0));
        }

		this.time = i_time; 
    }
}
