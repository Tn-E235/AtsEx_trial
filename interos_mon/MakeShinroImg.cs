using System;
using System.Drawing;

struct DWST_SHINRO_COLOR {
	public Color bg;		// 背景色
	public Color kido;		// 進路ベース
	public Color kaitsu;	// 開通進路
	public Color text;		// 文字色
	public Color jisya;		// 自車マーク
	public Color yajirushi;	// 矢印マーク
	public DWST_SHINRO_COLOR (int i) {
		this.bg = Color.Black;
		this.kido = Color.White;
		this.kaitsu = Color.Cyan;
		this.text = Color.Yellow;
		this.jisya = Color.White;
		this.yajirushi = Color.Red;
	}
}

struct DWST_SHINRO_INF {
	public int width;		// 表示エリアの幅
	public int height;		// 表示エリアの高さ
	public int track_width;	// 軌道回路の幅
	public int track_height;// 1000[m]の高さ
	public int yajirushi_h; // 矢印の高さ
	public int font_size;   // フォントサイズ
	public String font_name;// フォント名
	public Font font;		// フォント

	public DWST_SHINRO_INF (int i) {
		this.width = 200;
		this.height = 225;
		this.track_width = 20;
		this.track_height = 180;
		this.yajirushi_h = 20;
		this.font_size = 16;
		this.font_name = "VL ゴシック";
		this.font = new Font(this.font_name, this.font_size);
	}
}

struct SHINRO_INF {
	public String name;		// 軌道回路名称
	public int blockID;     // ブロックID
	public int distance;	// 進路開通距離
	public SHINRO_INF(int i) {
		this.name     = "";
		this.blockID  = 0;
		this.distance = 0;
	}
}


public class MakeShinroImage {
	Bitmap shinaro;
	Bitmap sankaku;
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

    public void make(int distatnce) {

		// 描画更新判定(残距離)
		if (distatnce == this.s_inf.distance) {
			this.update = false;
			return;
		}

		// 開通進路距離(描画換算)
		int d = distatnce >= SHINRO_MAX ? this.d_inf.track_height : distatnce * this.d_inf.track_height / SHINRO_MAX;

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
		g.DrawString(String.Format("停止限界{0, 4}m", distatnce), this.d_inf.font, new SolidBrush(this.color.text), new Point(0, 205));

        // 表示開通距離更新
        this.distance = d;
		this.s_inf.distance = distance;
		this.update = true;

        return;
	}
}
