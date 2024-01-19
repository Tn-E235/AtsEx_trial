/* ============================================================================
   描画系構造体

    dwst.cs
============================================================================ */
namespace PITempCS.mon {
    using System;
    using System.Drawing;


    /* ----------------------------------------------------------------------
     * 開通進路表示
       ---------------------------------------------------------------------- */

    // 開通進路表示(色設定)
    public struct DWST_SHINRO_COLOR {
        public Color bg;            // 背景色
        public Color kido;          // 進路ベース
        public Color kaitsu;        // 開通進路
        public Color text;          // 文字色
        public Color jisya;         // 自車マーク
        public Color yajirushi;     // 矢印マーク
        public DWST_SHINRO_COLOR (int i) {
            this.bg        = Color.Black;
            this.kido      = Color.White;
            this.kaitsu    = Color.Cyan;
            this.text      = Color.Yellow;
            this.jisya     = Color.White;
            this.yajirushi = Color.Red;
        }
    }

    // 開通進路表示(サイズ設定)
    public struct DWST_SHINRO_INF {
        public int width;           // 表示エリアの幅
        public int height;          // 表示エリアの高さ
        public int track_width;     // 軌道回路の幅
        public int track_height;    // 1000[m]の高さ
        public int yajirushi_h;     // 矢印の高さ
        public int font_size;       // フォントサイズ
        public String font_name;    // フォント名
        public Font font;           // フォント

        public DWST_SHINRO_INF (int i) {
            this.width        = 200;
            this.height       = 225;
            this.track_width  =  20;
            this.track_height = 180;
            this.yajirushi_h  =  20;
            this.font_size    =  16;
            this.font_name    = "VL ゴシック";
            this.font = new Font(this.font_name, this.font_size);
        }
    }
}