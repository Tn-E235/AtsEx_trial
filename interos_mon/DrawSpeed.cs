using PITempCS.mon;
using SlimDX.DirectSound;
using System;
using System.Drawing;

public class MakeSpeedImg {
	DWST_RECT_TEXT draw_inf;
	int speed;
    Bitmap bmp;
    Graphics g;
	bool update;

    public MakeSpeedImg(int x, int y, int size, Color color, String font, Color bg) {
		this.draw_inf = new DWST_RECT_TEXT(x, y, size, color, font, bg);
		this.speed = -1;
		this.update = false;
		this.bmp = new Bitmap(150, 30);
		this.g = Graphics.FromImage(this.bmp);
        this.g.DrawString("km/h", this.draw_inf.font, new SolidBrush(this.draw_inf.color), 4 * 16, 0);
    }

	public void Dispose () {
		this.g.Dispose();
	}

	public Bitmap get () {
		return this.bmp;
	}

	public bool isUpdate () {
		return this.update;
	}

	public void make (int i_speed) {
		
		// 描画更新判定
		if (this.speed == i_speed) {
			this.update = false;
			return;
		}

        this.g.FillRectangle(new SolidBrush(this.draw_inf.bg), new Rectangle(0, 0, 16 * 3 + 8, 30));
        this.draw(0, i_speed);
        this.draw(1, i_speed);
        this.draw(2, i_speed);
        this.speed = i_speed;
		this.update = true;
	}

	private void fill (int i_x) {
        this.g.FillRectangle(new SolidBrush(this.draw_inf.bg), new Rectangle(i_x*16+8, 0, 16, 30));
    }
	private void draw (int i_keta, int i_speed) {
		int num = 0;
		if (i_keta == 0) {
			num = i_speed / 100;
			if (i_speed < 100) return;
		} else if (i_keta == 1) {
			num = i_speed / 10 % 10;
            if (i_speed < 10) return;
        } else if (i_keta == 2) {
			num = i_speed % 10;
		} else {
			return;
		}
		this.g.DrawString(num.ToString(), this.draw_inf.font, new SolidBrush(this.draw_inf.color), i_keta*16, 0);
	}
}
