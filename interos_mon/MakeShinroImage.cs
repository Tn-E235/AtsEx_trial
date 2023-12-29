﻿using System;
using System.Drawing;

struct ShinroColor {
	public Color bg;		// 背景色
	public Color kido;		// 進路ベース
	public Color kaitsu;	// 開通進路
	public Color text;		// 文字色
	public Color jisya;		// 自車マーク
	public Color yajirushi;	// 矢印マーク
	public ShinroColor (int i) {
		this.bg = Color.Black;
		this.kido = Color.White;
		this.kaitsu = Color.Cyan;
		this.text = Color.Yellow;
		this.jisya = Color.White;
		this.yajirushi = Color.Red;
	}
}

struct DrawInfo {
	public int width;		// 表示エリアの幅
	public int height;		// 表示エリアの高さ
	public int track_width;	// 軌道回路の幅
	public int track_height;// 1000[m]の高さ
	public int yajirushi_h; // 矢印の高さ
	public int font_size;   // フォントサイズ
	public String font_name;// フォント名
	public Font font;		// フォント

	public DrawInfo (int i) {
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

struct ShinroInfo {
	public String name;		// 軌道回路名称
	public int blockID;     // ブロックID
	public int distance;	// 進路開通距離
	public ShinroInfo(int i) {
		this.name     = "";
		this.blockID  = 0;
		this.distance = 0;
	}
}


public class MakeShinroImage {
	Bitmap shinaro;
	Graphics g;
    int distance;
	ShinroColor color;
	ShinroInfo s_inf;
	DrawInfo d_inf;

	const int SHINRO_MAX = 1000;    // 開通進路表示距離
	public MakeShinroImage () {
		this.color = new ShinroColor(0);
		this.s_inf = new ShinroInfo(0);
		this.d_inf = new DrawInfo(0);
		this.shinaro = new Bitmap(this.d_inf.width, this.d_inf.height);
		this.g = Graphics.FromImage(this.shinaro);
		this.distance = 0;
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


    }

    public Bitmap GetShinroImage(int distatnce) {

		// 開通進路距離(描画換算)
		int d = distatnce >= SHINRO_MAX ? this.d_inf.track_height : distatnce * this.d_inf.track_height / SHINRO_MAX;
		// 描画更新判定
		if (d != this.distance) {
			// 軌道回路(ベース)
			g.FillRectangle(new SolidBrush(this.color.kido), new Rectangle(75, 0, this.d_inf.track_width, this.d_inf.track_height - d));
			// 開通進路描画
			g.FillRectangle(new SolidBrush(this.color.kaitsu), new Rectangle(75, this.d_inf.track_height - d, this.d_inf.track_width, d));
        }

		// 残距離表示
		g.FillRectangle(new SolidBrush(this.color.bg), new Rectangle(0, 205, this.d_inf.width, 20));
		g.DrawString(String.Format("停止限界{0, 4}m", distatnce), this.d_inf.font, new SolidBrush(this.color.text), new Point(0, 205));

        // 表示開通距離更新
        this.distance = d;
		this.s_inf.distance = distance;

        return this.shinaro;
	}
}
