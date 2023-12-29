using PITempCS.mon;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using Zbx1425.DXDynamicTexture;

public class DrawSpeed {
	private DRAW_INF_SET draw_inf;
	int speed;

    public DrawSpeed(int x, int y, int size, Color color, String font, Color bg) {
		draw_inf = new DRAW_INF_SET(x, y, size, color, font, bg);
		this.speed = 0;
	}

	public void Draw (GDIHelper i_gdi, int i_speed) {
		if (this.speed == i_speed) {
			return;
		}

		if (this.speed / 100 != i_speed / 100) {
            i_gdi.Graphics.FillRectangle(new SolidBrush(this.draw_inf.bg), new Rectangle(this.draw_inf.x, this.draw_inf.y, 50, 30));
        }

		if (this.speed / 10 % 10 == i_speed / 10 % 10) {
            i_gdi.Graphics.FillRectangle(new SolidBrush(this.draw_inf.bg), new Rectangle(this.draw_inf.x, this.draw_inf.y, 50, 30));
        }

		if (this.speed % 10 == i_speed % 10) {
            i_gdi.Graphics.FillRectangle(new SolidBrush(this.draw_inf.bg), new Rectangle(this.draw_inf.x, this.draw_inf.y, 50, 30));
        }

		this.speed = i_speed;
	}
}
