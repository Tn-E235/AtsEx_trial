using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Zbx1425.DXDynamicTexture;

using AtsEx.PluginHost.Plugins;
using AtsEx.PluginHost;
using BveTypes.ClassWrappers;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using PITempCS.mon;

public class DrawButton {

    private BUTTON_COLOR btn_color_off = new BUTTON_COLOR(0);
    private BUTTON_COLOR btn_color_on  = new BUTTON_COLOR(1);

    public DrawButton () {
	}

    /* ---- ボタン画像取得 ------------------------------------------------- */
    public Bitmap getButtonImage (int w, int h, int r) {
        // ボタン画像生成
        Bitmap tmp_img = new Bitmap(w, h);
        Graphics tmp_g = Graphics.FromImage(tmp_img);
        // 外側(クリッピング)
        ClippingRoundRectangle(0, 0, w, h, r, Color.Red, tmp_g);
        // 左縦台形
        Point[] polyPoints_dai1 = {
                new Point(    0,         0),    // 左上
                new Point(r * 2,     r * 2),    // 右上
                new Point(r * 2, h - r * 2),    // 右下
                new Point(    0,         h)     // 左下
            };
        // 右縦台形
        Point[] polyPoints_dai2 = {
                new Point(w - r * 2,    r * 2),    // 左上
                new Point(        w,        0),    // 右上
                new Point(        w,        h),    // 右下
                new Point(w - r * 2, h - r * 2)    // 左下
            };
        // 上横台形
        Point[] polyPoints_dai3 = {
                new Point(        0,      0),    // 左上
                new Point(        w,      0),    // 右上
                new Point(w - r * 2,  r * 2),    // 右下
                new Point(    r * 2,  r * 2)     // 左下
            };
        // 下横台形
        Point[] polyPoints_dai4 = {
                new Point(    0 + r * 2, 0 + h - r * 2),    // 左上
                new Point(0 + w - r * 2, 0 + h - r * 2),    // 右上
                new Point(        0 + w,         0 + h),    // 右下
                new Point(        0,             0 + h)     // 左下
            };
        // 左上コーナー
        Point[] polyPoints_c1 = {
                new Point(    0,         0),    // 角
                new Point(    r,         0),    // 上
                new Point(r * 2,         r),    // 右 
                new Point(    r,     r * 2),    // 下
                new Point(    0,         r)     // 左
            };
        // 右上コーナー
        Point[] polyPoints_c2 = {
                new Point(        w,         0),    // 角
                new Point(    w - r,         0),    // 上
                new Point(w - r * 2,         r),    // 左 
                new Point(    w - r,     r * 2),    // 下
                new Point(        w,         r)     // 右
            };
        // 左下コーナー
        Point[] polyPoints_c3 = {
                new Point(    0,         h),    // 角
                new Point(    0,     h - r),    // 左
                new Point(    r, h - r * 2),    // 上
                new Point(r * 2,     h - r),    // 右 
                new Point(    r,         h)     // 下
            };
        // 右下コーナー
        Point[] polyPoints_c4 = {
                new Point(        w,         h),    // 角
                new Point(    w - r,         h),    // 下
                new Point(w - r * 2,      h -r),    // 左 
                new Point(    w - r, h - r * 2),    // 上
                new Point(        w,     h - r)     // 右
            };

        GraphicsPath sankaku1 = new GraphicsPath();
        GraphicsPath sankaku2 = new GraphicsPath();
        GraphicsPath sankaku3 = new GraphicsPath();
        GraphicsPath sankaku4 = new GraphicsPath();
        GraphicsPath corner1 = new GraphicsPath();
        GraphicsPath corner2 = new GraphicsPath();
        GraphicsPath corner3 = new GraphicsPath();
        GraphicsPath corner4 = new GraphicsPath();

        sankaku1.AddPolygon(polyPoints_dai1);
        sankaku2.AddPolygon(polyPoints_dai2);
        sankaku3.AddPolygon(polyPoints_dai3);
        sankaku4.AddPolygon(polyPoints_dai4);
        corner1.AddPolygon(polyPoints_c1);
        corner2.AddPolygon(polyPoints_c2);
        corner3.AddPolygon(polyPoints_c3);
        corner4.AddPolygon(polyPoints_c4);

        tmp_g.FillPath(new SolidBrush(btn_color_on.koumen),  sankaku1);
        tmp_g.FillPath(new SolidBrush(btn_color_on.kagemen), sankaku2);
        tmp_g.FillPath(new SolidBrush(btn_color_on.koumen),  sankaku3);
        tmp_g.FillPath(new SolidBrush(btn_color_on.kagemen), sankaku4);
        tmp_g.FillPath(new SolidBrush(btn_color_on.kado1),    corner1);
        tmp_g.FillPath(new SolidBrush(btn_color_on.kado2),    corner2);
        tmp_g.FillPath(new SolidBrush(btn_color_on.kado2),    corner3);
        //tmp_g.FillPath(btn_color.kado3,     corner4);

        // 内側
        DrawRoundRectangle(r, r, w - r * 2, h - r * 2, r, Color.FromArgb(48, 77, 135), tmp_g);
        //Font drawFont = new Font(DEFAULT_FONT_FAMILY, DEFAULT_FONT_SIZE);
        tmp_g.Dispose();
        //gdi.Graphics.DrawImage(tmp_img, x, y);
        return tmp_img;
    }

    /* ---- 角丸の四角形でクリッピング --------------------------------- */
    private void ClippingRoundRectangle (int x, int y, int w, int h, int r, Color c, Graphics g) {
        float a = (float)(4 * (1.41421356 - 1) / 3 * r);
        GraphicsPath path = new GraphicsPath();
        path.StartFigure();
        path.AddBezier(x, y + r, x, y + r - a, x + r - a, y, x + r, y);                                 // 左上
        path.AddBezier(x + w - r, y, x + w - r + a, y, x + w, y + r - a, x + w, y + r);                 // 右上
        path.AddBezier(x + w, y + h - r, x + w, y + h - r + a, x + w - r + a, y + h, x + w - r, y + h); // 右下
        path.AddBezier(x + r, y + h, x + r - a, y + h, x, y + h - r + a, x, y + h - r);                 // 左下
        path.CloseFigure();
        Region region = new Region(path);
        g.SetClip(region, CombineMode.Replace);
    }
    /* ---- [C#]角丸の矩形を描画する ----------------------------------- */
    /*        http://devlabo.blogspot.com/2010/01/c_16.html              */
    private void DrawRoundRectangle (int x, int y, int w, int h, int r, Color c, Graphics g) {
        float a = (float)(4 * (1.41421356 - 1) / 3 * r);

        GraphicsPath path = new GraphicsPath();
        path.StartFigure();
        path.AddBezier(x, y + r, x, y + r - a, x + r - a, y, x + r, y); /* 左上 */
        path.AddBezier(x + w - r, y, x + w - r + a, y, x + w, y + r - a, x + w, y + r); /* 右上 */
        path.AddBezier(x + w, y + h - r, x + w, y + h - r + a, x + w - r + a, y + h, x + w - r, y + h); /* 右下 */
        path.AddBezier(x + r, y + h, x + r - a, y + h, x, y + h - r + a, x, y + h - r); /* 左下 */
        path.CloseFigure();
        g.FillPath(new SolidBrush(c), path);
    }
}
