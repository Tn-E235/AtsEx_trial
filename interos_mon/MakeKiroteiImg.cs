using PITempCS.mon;
using System;
using System.Drawing;

public class MakeKiroteiImg {

    DWST_RECT_TEXT draw_inf;
	Bitmap bmp;
	Graphics g;
	bool update;
	double location;

    public MakeKiroteiImg (int x, int y, int size, Color color, String font, Color bg) {
        this.draw_inf = new DWST_RECT_TEXT(x, y, size, color, font, bg);
        this.bmp = new Bitmap(165, 30);
        this.g = Graphics.FromImage(this.bmp);
		this.location = -1.0d;
    }

	public void make (double i_location) {
		// 小数第一位まで一致する場合、更新しない
		if (Math.Round(i_location, 1) == Math.Round(this.location, 1)) {
			this.update = false;
			return;
		}
		this.g.FillRectangle(new SolidBrush(this.draw_inf.bg), 0, 0, this.bmp.Width, this.bmp.Height);
		this.g.DrawString(String.Format("{0,7:0.0} km", i_location/1000), this.draw_inf.font, new SolidBrush(this.draw_inf.color), 0, 0);
		this.location = i_location;
		this.update = true;
	}

	public Bitmap get () {
		return this.bmp;
	}

	public void dispose() {
		this.g.Dispose();
	}

	public bool isUpdate() {
		return this.update;
	}
}
