using PITempCS.mon;
using System;
using System.Drawing;
using Zbx1425.DXDynamicTexture;
struct SHINRO_INF {
    public String name;     // 軌道回路名称
    public int blockID;     // ブロックID
    public double distance; // 進路開通距離
    public SHINRO_INF (int i) {
        this.name = "";
        this.blockID = 0;
        this.distance = 0.0;
    }
}
public class MakeShinroImage {
	Bitmap shinaro;
	Bitmap sankaku;
	Bitmap jisya;
	Graphics g;
    int distance;
	DWST_SHINRO_COLOR color;
	SHINRO_INF s_inf;
	DWST_SHINRO_INF d_inf;
	Boolean update;
	const int SHINRO_MAX = 1000;    // 開通進路表示距離

	public MakeShinroImage () {
		this.color = new DWST_SHINRO_COLOR(0);
		this.s_inf = new SHINRO_INF(0);
		this.d_inf = new DWST_SHINRO_INF(0);
		this.shinaro = new Bitmap(this.d_inf.width, this.d_inf.height);
		this.sankaku = new Bitmap(this.d_inf.track_width * 2, this.d_inf.track_height / 10);
        this.jisya = new Bitmap(this.d_inf.track_width * 2, this.d_inf.track_height / 10);
        this.g = Graphics.FromImage(this.shinaro);
		this.distance = 0;
        var sankaku_pp = new System.Drawing.Drawing2D.GraphicsPath();
		sankaku_pp.AddPolygon(new Point[] { 
				new Point(0, d_inf.track_height / 10), 
				new Point(d_inf.track_width * 2, d_inf.track_height / 10), 
				new Point(d_inf.track_width, 0) });
		Graphics sankaku_g = Graphics.FromImage(this.sankaku);
		sankaku_g.FillRectangle(new SolidBrush(color.bg), 0, 0, this.sankaku.Width, this.sankaku.Height);
		sankaku_g.FillPath(new SolidBrush(color.yajirushi), sankaku_pp);
		sankaku_g.Dispose();

        var jisya_pp = new System.Drawing.Drawing2D.GraphicsPath();
        jisya_pp.AddPolygon(new Point[] {
				new Point(                    0, d_inf.track_height / 10    ),
				new Point(d_inf.track_width * 2, d_inf.track_height / 10    ),
				new Point(d_inf.track_width * 2, d_inf.track_height / 10 / 2),
				new Point(d_inf.track_width    ,                           0),
				new Point(                    0, d_inf.track_height / 10 / 2)});
        Graphics jisya_g = Graphics.FromImage(this.jisya);
        jisya_g.FillRectangle(new SolidBrush(color.bg), 0, 0, this.jisya.Width, this.jisya.Height);
        jisya_g.FillPath(new SolidBrush(color.jisya), jisya_pp);
        jisya_g.Dispose();
        this.init();

    }

	void init() {
        // 背景塗りつぶし
        g.FillRectangle(new SolidBrush(this.color.bg), new Rectangle(0, 0, this.d_inf.width, this.d_inf.height));
		// 距離程表示[1000]
		g.DrawString("1000", this.d_inf.font, new SolidBrush(this.color.text), new Point(0, 0));
        // 距離程表示[ 500]	
        g.DrawString("500 ", this.d_inf.font, new SolidBrush(this.color.text), new Point(0, this.d_inf.track_height / 2 - this.d_inf.font_size / 2));
        // 距離程表示[   0]
        g.DrawString("0   ", this.d_inf.font, new SolidBrush(this.color.text), new Point(0, this.d_inf.track_height - this.d_inf.font_size));
		// 自車マーク表示
		g.DrawImage(this.jisya, 75 - this.d_inf.track_width / 2, this.d_inf.track_height);
		// 更新フラグ
		this.update = true;
    }
	
	public Bitmap get() {
		return this.shinaro;
	}

	public Boolean isUpdate() {
		return this.update;
	}

	public void Dispose() {
		this.g.Dispose();
	}

    public void make(double distatnce) {

		// 描画更新判定(残距離)
		if (distatnce == this.s_inf.distance) {
			this.update = false;
			return;
		}

		// 開通進路距離(描画換算)
		int d = (int)(distatnce >= SHINRO_MAX ? this.d_inf.track_height : distatnce * this.d_inf.track_height / SHINRO_MAX);
		if (d < 0) d = 0;

		// 描画更新判定(描画距離)
		if (d != this.distance) {

			// 軌道回路描画初期化
            g.FillRectangle(new SolidBrush(this.color.bg), new Rectangle(75 - this.d_inf.track_width / 2, 0, this.d_inf.track_width * 2, this.d_inf.track_height));
            // 軌道回路(ベース)
            g.FillRectangle(new SolidBrush(this.color.kido), new Rectangle(75, 0, this.d_inf.track_width, this.d_inf.track_height - d));
		
			// 開通境界赤三角表示
			if (900 <= distance && distance < 1000) {
				g.DrawImage(this.sankaku, 75 - this.d_inf.track_width / 2, this.sankaku.Height);
            } else if (distance < 900) {
                g.DrawImage(this.sankaku, 75 - this.d_inf.track_width / 2, this.d_inf.track_height - d - this.sankaku.Height);
            }
			// 開通進路描画
			g.FillRectangle(new SolidBrush(this.color.kaitsu), new Rectangle(75, this.d_inf.track_height - d, this.d_inf.track_width, d));
        
		}

		// 残距離表示
		g.FillRectangle(new SolidBrush(this.color.bg), new Rectangle(0, 205, this.d_inf.width, 20));
		g.DrawString(String.Format("停止限界{0,7:0.0}m", distatnce), this.d_inf.font, new SolidBrush(this.color.text), new Point(0, 205));

        // 表示開通距離更新
        this.distance = d;
		this.s_inf.distance = distance;
		this.update = true;

        return;
	}
}
