/* ============================================================================
   カラーストラクチャ
============================================================================ */
namespace PITempCS.mon {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    /* ---- 一般フォント ------------------------------------------------------- */
    struct GENERAL_FONT {
        public String text_jp;
        public String text_en;
        public String num_han;
        public String num_zen;
        public GENERAL_FONT (int i) {
            text_jp = "Cica";
            text_en = "Cica";
            num_han = "Cica";
            num_zen = "Cica";
        }
        public GENERAL_FONT (String font) {
            text_jp = font;
            text_en = font;
            num_han = font;
            num_zen = font;
        }
        public GENERAL_FONT (String font1, String font2) {
            text_jp = font1;
            text_en = font1;
            num_han = font2;
            num_zen = font2;
        }
        public GENERAL_FONT (String font1, String font2, String font3, String font4) {
            text_jp = font1;
            text_en = font2;
            num_han = font3;
            num_zen = font4;
        }

    }

    /* ---- 一般フォントカラー ------------------------------------------------- */
    struct GENERAL_FONT_COLOR {

    }

    /* ---- 画面背景色 --------------------------------------------------------- */
    struct BACKGROUND_COLOR {
        public Brush bg1;
        public Brush bg2;
        public Brush bg3;
        public Brush bg4;
        public BACKGROUND_COLOR (int i) {
            bg1 = new SolidBrush(Color.FromArgb(71, 80, 101));
            bg2 = new SolidBrush(Color.FromArgb(0, 0, 0));
            bg3 = new SolidBrush(Color.FromArgb(53, 62, 83));
            bg4 = new SolidBrush(Color.FromArgb(37, 41, 51));
        }
        public BACKGROUND_COLOR (Brush b1, Brush b2, Brush b3, Brush b4) {
            bg1 = b1;
            bg2 = b2;
            bg3 = b3;
            bg4 = b4;
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

        public BUTTON_COLOR (int i) {
            body       = Color.FromArgb( 48,  77, 135);
            koumen     = Color.FromArgb(168, 184, 215);
            kagemen    = Color.FromArgb( 28,  35,  58);
            kado1      = Color.FromArgb(244, 244, 244);
            kado2      = Color.FromArgb(119, 139, 183);
            kado3      = Color.FromArgb(244, 244, 244);
            font_color = Color.FromArgb(255, 255, 255);
        }

        public BUTTON_COLOR (Color c1, Color c2, Color c3, Color c4, Color c5, Color c6, Color c7) {
            body = c1;
            koumen = c2;
            kagemen = c3;
            kado1 = c4;
            kado2 = c5;
            kado3 = c6;
            font_color = c7;
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




}