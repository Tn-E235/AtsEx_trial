/* ============================================================================
   カラーストラクチャ
============================================================================ */
namespace PITempCS.mon {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    /* ---- 一般フォント ------------------------------------------------------- */
    struct FONT_INF {
        public String text_jp;
        public String text_en;
        public String num_han;
        public String num_zen;
        public int size;
        public Color color;
        public FONT_INF (int i) {
            this.text_jp = "VL ゴシック";
            this.text_en = "VL ゴシック";
            this.num_han = "VL ゴシック";
            this.num_zen = "VL ゴシック";
            this.size    = 22;
            this.color   = Color.White;
        }
        public FONT_INF (String font) {
            this.text_jp = font;
            this.text_en = font;
            this.num_han = font;
            this.num_zen = font;
            this.size    = 16;
            this.color   = Color.White;
        }
        public FONT_INF (String font1, String font2, int size) {
            this.text_jp = font1;
            this.text_en = font1;
            this.num_han = font2;
            this.num_zen = font2;
            this.size    = size;
            this.color   = Color.White;
        }
        public FONT_INF (String font1, String font2, String font3, String font4, int size, Color color) {
            this.text_jp = font1;
            this.text_en = font2;
            this.num_han = font3;
            this.num_zen = font4;
            this.size    = size;
            this.color   = color;
        }
    }

    /* ---- 一般フォントカラー ------------------------------------------------- */
    struct GENERAL_FONT_COLOR {

    }

    /* ---- 画面背景色 --------------------------------------------------------- */
    struct BACKGROUND_COLOR {
        public Color bg1;
        public Color bg2;
        public Color bg3;
        public Color bg4;
        public BACKGROUND_COLOR (int i) {
            bg1 = Color.FromArgb(71, 80, 101);
            bg2 = Color.FromArgb(0, 0, 0);
            bg3 = Color.FromArgb(53, 62, 83);
            bg4 = Color.FromArgb(37, 41, 51);
        }
        public BACKGROUND_COLOR (Color c1, Color c2, Color c3, Color c4) {
            bg1 = c1;
            bg2 = c2;
            bg3 = c3;
            bg4 = c4;
        }
    }

    /* ---- ボタン描画色 ------------------------------------------------------- */
    struct BUTTON_COLOR {
        public Color body;                          // 本体色
        public Color koumen;                        // 光面
        public Color kagemen;                       // 影面
        public Color kado1;                         // コーナー(光面)
        public Color kado2;                         // コーナー(影面)
        public Color kado3;                         // コーナー(影面)
        public Color font_color;                    // フォントカラー
        //public int font_size;                       // フォントサイズ
        //public Font font;                           // フォント

        public BUTTON_COLOR (int i) {
            this.body       = Color.FromArgb( 48,  77, 135);
            this.koumen     = Color.FromArgb(168, 184, 215);
            this.kagemen    = Color.FromArgb( 28,  35,  58);
            this.kado1      = Color.FromArgb(244, 244, 244);
            this.kado2      = Color.FromArgb(119, 139, 183);
            this.kado3      = Color.FromArgb(244, 244, 244);
            this.font_color = Color.FromArgb(255, 255, 255);
            //this.font_size  = 16;
            //this.font       = new Font("cica", font_size);
        }

        public BUTTON_COLOR (Color c1, Color c2, Color c3, Color c4, Color c5, Color c6, Color c7) {
            this.body       = c1;
            this.koumen     = c2;
            this.kagemen    = c3;
            this.kado1      = c4;
            this.kado2      = c5;
            this.kado3      = c6;
            this.font_color = c7;
            //this.font_size  = i;
            //this.font       = new Font(s, font_size);
        }

    }



    struct DRAW_INF_SET {
        public int x;
        public int y;
        public int size;
        public Color color;
        public Font font;
        public Color bg;
        public DRAW_INF_SET (int i) {
            this.x     = 0;
            this.y     = 0;
            this.size  = 16;
            this.color = Color.White;
            this.font  = new Font("VL ゴシック", this.size);
            this.bg    = Color.Black;
        }

        public DRAW_INF_SET (int x, int y, int size, Color color, String font, Color bg) {
            this.x = x;
            this.y = y;
            this.size = size;
            this.color = color;
            this.font = new Font(font, this.size);
            this.bg = bg;
        }
    }


    /* ---- 通過設定描画色 ----------------------------------------------------- */
    struct TSUKA_SETTEI {
        public Brush bg_color;
        public Brush font_color;
        public TSUKA_SETTEI (Brush bg, Brush font) {
            bg_color = bg;
            font_color = font;
        }
    }
    /* ---- ユニット描画色 ----------------------------------------------------- */
    struct UNIT_COLOR {
        public Brush[] train_color;                 // 列車描画色
        public Brush[] font_color;                  // フォントカラー
        public UNIT_COLOR (int i) {
            train_color    = new Brush[3];
            font_color     = new Brush[3];
            train_color[0] = Brushes.Gray;          // OFF
            train_color[1] = Brushes.SkyBlue;       // POWER
            train_color[2] = Brushes.Yellow;        // BRAKE
            font_color[0]  = Brushes.White;         // OFF
            font_color[1]  = Brushes.White;         // POWER
            font_color[2]  = Brushes.White;         // BRAKE
        }
    }
    /* ---- ドア描画色 --------------------------------------------------------- */
    struct DOOR_COLOR {
        public Brush[] door_color;                  // ドア描画色
        public Brush[] font_color;                  // フォントカラー
        public DOOR_COLOR (int i) {
            door_color = new Brush[3];
            font_color = new Brush[3];
            door_color[0] = Brushes.Gray;           // CLOSE
            door_color[1] = Brushes.Red;            // OPEN
            door_color[2] = Brushes.Yellow;         // BREAK
            font_color[0] = Brushes.Gray;           // CLOSE
            font_color[1] = Brushes.BlueViolet;     // OPEN
            font_color[2] = Brushes.Yellow;         // BREAK
        }
    }


    struct STOP_EKI_INF {
        public int idx;
        public string name;
        public TimeSpan cyaku_jikoku;
        public TimeSpan hatsu_jikoku;
        public int cyakuhatsu_bansen;
        public int hatsu_bansen;
        public STOP_EKI_INF(int i) {
            idx = 0;
            name = "未設定";
            cyaku_jikoku = new TimeSpan(0);
            hatsu_jikoku = new TimeSpan(0);
            cyakuhatsu_bansen = 0;
            hatsu_bansen = 0;
        }
    }

}