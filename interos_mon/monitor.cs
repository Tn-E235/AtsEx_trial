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
using SlimDX;

namespace AtsExCsTemplate.VehiclePlugin {
    [PluginType(PluginType.VehiclePlugin)]
    public class DXDynamicTextureTest : AssemblyPluginBase {
        private GDIHelper gdi;
        private Bitmap mon_R;
        private static TextureHandle txH;

        private const String DEFAULT_FONT_FAMILY = "VL ゴシック";// Cica", ""HGSｺﾞｼｯｸE"; //"KHドット秋葉原16";//  "JFドットjiskan16";
        private const String DEFAULT_DOT_FONT_FAMILY = "JFドットAyuゴシック20";//"JFドットjiskan16";
        private const int DEFAULT_FONT_SIZE = 22;
        private bool base_draw_f = true;

        BACKGROUND_COLOR bg_color;
        DrawGenzaiJikoku genzaoi_jikoku;

        private static string dllParentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public DXDynamicTextureTest (PluginBuilder builder) : base(builder) {
            BveHacker.ScenarioCreated += OnScenarioCreated;
            
            txH = TextureManager.Register("base.png", 2048, 1024);
            gdi = new GDIHelper(txH.Width, txH.Height);
            mon_R = new Bitmap(Path.Combine(dllParentPath, "./tex/base.png"));

            bg_color = new BACKGROUND_COLOR(0);
            genzaoi_jikoku = new DrawGenzaiJikoku(395, 2, DEFAULT_FONT_SIZE, Color.White, DEFAULT_FONT_FAMILY, bg_color.bg2);

        }

        public override void Dispose () {
            BveHacker.ScenarioCreated -= OnScenarioCreated;
            mon_R.Dispose();
        }


        private void OnScenarioCreated (ScenarioCreatedEventArgs e) {
        }

        unsafe public override TickResult Tick (TimeSpan elapsed) {
            if (!txH.IsCreated || !txH.HasEnoughTimePassed(100)) {
                return new VehiclePluginTickResult();
            }

            if (this.base_draw_f) {
                draw_base();
                this.base_draw_f = false;
            }

            // ドア
            for (int i = 0; i < Native.VehicleSpec.Cars - 1; ++i) {
                draw_door(i);
                draw_car_unit(i);
            }

            int cnt = 0;
            // 停車駅一覧[1]-[5]を表示
            StationList stations = BveHacker.Scenario.Route.Stations;
            // ドア状態
            int door = isAllDoorClosed() ? 0 : 1;
            for (int i = stations.CurrentIndex + door; i < stations.Count; ++i) {
                // 駅が無いとき
                if (i < 0) break;
                // 駅名を表示
                draw_station_name(cnt, ((Station)stations[i]).Name);
                // 到着時刻を表示
                draw_arrive_time(cnt, ((Station)stations[i]).ArrivalTime);
                // 出発時刻を表示
                draw_deperture_time(cnt, ((Station)stations[i]).DepertureTime);
                ++cnt;
                if (cnt == 5) break;
            }

            // 現在時刻
            //gdi.Graphics.DrawImage(genzaoi_jikoku.getGenzaiJikoku(Native.VehicleState.Time), 395, 2);
            //gdi = genzaoi_jikoku.Draw(gdi, Native.VehicleState.Time);
            genzaoi_jikoku.Draw(gdi, Native.VehicleState.Time);

            // 採時駅表示(着時刻および発時刻がある駅とする)
            draw_saiji_eki();

            // 終着駅表示
            draw_syucyaku_eki();

            txH.Update(gdi);
            //update = false;
            
            return new VehiclePluginTickResult();
        }

        public void draw_base() {
            // 画面ベース
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg1), new Rectangle(0, 0, 1280, 800));
            // 画面上部
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg2), new Rectangle(0, 0, 300, 100));
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg2), new Rectangle(305, 0, 265, 100));
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg2), new Rectangle(575, 0, 705, 100));
            // 三本線
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg1), new Rectangle(800, 10, 320, 25));
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg1), new Rectangle(800, 40, 320, 25));
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg1), new Rectangle(800, 70, 320, 25));
            // 運転情報
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg3), new Rectangle(0, 100, 1280, 100));
            // 開通情報
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg2), new Rectangle(0, 395, 170, 25));
            // 転動防止
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg2), new Rectangle(0, 205, 200, 225));
            // 自車位置(横棒)
            gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), new Rectangle(650, 360, 395, 3));
            // 自車位置(マーク)

            // 車両状態帯
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg2), new Rectangle(0, 500, 1280, 100));

            // 下部ボタン帯
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg2), new Rectangle(0, 685, 1280, 115));
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg1), new Rectangle(630, 700, 495, 25));
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg1), new Rectangle(630, 730, 495, 25));
            gdi.Graphics.FillRectangle(new SolidBrush(bg_color.bg1), new Rectangle(630, 760, 495, 25));
            // 適当な文字
            gdi.Graphics.DrawString("INTEROSモニタプラグイン", new Font(DEFAULT_FONT_FAMILY, 20), Brushes.White, 630, 699);
            gdi.Graphics.DrawString("version 0.1", new Font(DEFAULT_FONT_FAMILY, 20), Brushes.White, 630, 729);
            gdi.Graphics.DrawString("制作＠Tn_E235", new Font(DEFAULT_FONT_FAMILY, 20), Brushes.White, 630, 759);

            // 下部button
            DrawButton buttunImg = new DrawButton();
            gdi.Graphics.DrawImage(buttunImg.getButtonImage(155, 65, 5), 955, 615);
            gdi.Graphics.DrawImage(buttunImg.getButtonImage(155, 65, 5), 1115, 615);

            gdi.Graphics.DrawImage(buttunImg.getButtonImage(150, 105, 5), 10, 690);
            gdi.Graphics.DrawImage(buttunImg.getButtonImage(150, 105, 5), 165, 690);
            gdi.Graphics.DrawImage(buttunImg.getButtonImage(150, 105, 5), 320, 690);
            gdi.Graphics.DrawImage(buttunImg.getButtonImage(150, 105, 5), 475, 690);
        }

        public void draw_station_name (int i_idx, String i_name) {
            Font drawFont = new Font(DEFAULT_FONT_FAMILY, DEFAULT_FONT_SIZE);
            gdi.Graphics.DrawString(cvt_station_name(i_name), drawFont, Brushes.White, 1030 - i_idx * 100, 205);
        }

        public void draw_saiji_eki () {

        }

        public void draw_syucyaku_eki () {
            StationList stations = BveHacker.Scenario.Route.Stations;
            if (stations.Count < 1) return;
            draw_station_name(6, ((Station)stations[stations.Count - 1]).Name);
            draw_arrive_time(6, ((Station)stations[stations.Count - 1]).ArrivalTime);
            draw_deperture_time(6, ((Station)stations[stations.Count - 1]).DepertureTime);
        }

        public void draw_arrive_time (int i_idx, TimeSpan i_time) {
            // 時刻未設定の場合は表示なし
            if (i_time.TotalMilliseconds < 0.1d) return;

            Font drawFont_M = new Font(DEFAULT_FONT_FAMILY, 16);
            Font drawFont_S = new Font(DEFAULT_FONT_FAMILY, 10);
            gdi.Graphics.DrawString(String.Format("{0:00}", i_time.Minutes), drawFont_M, Brushes.White, 1025 - i_idx * 100, 323);
            gdi.Graphics.DrawString(String.Format("{0:00}", i_time.Seconds), drawFont_S, Brushes.White, 1050 - i_idx * 100, 324);
        }

        public void draw_deperture_time (int i_idx, TimeSpan i_time) {
            // 時刻未設定の場合は表示なし
            if (i_time.TotalMilliseconds < 0.1d) return;

            Font drawFont_M = new Font(DEFAULT_FONT_FAMILY, 16);
            Font drawFont_S = new Font(DEFAULT_FONT_FAMILY, 10);
            gdi.Graphics.DrawString(String.Format("{0:00}", i_time.Minutes), drawFont_M, Brushes.White, 1025 - i_idx * 100, 375);
            gdi.Graphics.DrawString(String.Format("{0:00}", i_time.Seconds), drawFont_S, Brushes.White, 1050 - i_idx * 100, 376);
        }

        public void draw_door (int i_car) {
            Font drawFont = new Font(DEFAULT_FONT_FAMILY, 16);
            // 左右どちらかのドアがあいているとき、開表示とする
            if (!BveHacker.Scenario.Vehicle.Doors.GetSide(DoorSide.Left).CarDoors[i_car].IsOpen
                    && !BveHacker.Scenario.Vehicle.Doors.GetSide(DoorSide.Right).CarDoors[i_car].IsOpen) {
                gdi.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(365 + (i_car - 1) * 70, 510, 68, 30));
                gdi.Graphics.DrawString("閉", drawFont, Brushes.Black, 365 + (i_car - 1) * 70 + 20, 510 + 5);
            } else {
                gdi.Graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(365 + (i_car - 1) * 70, 510, 68, 30));
                gdi.Graphics.DrawString("開", drawFont, Brushes.White, 365 + (i_car - 1) * 70 + 20, 510 + 5);
            }
            gdi.Graphics.DrawRectangle(Pens.Blue, new Rectangle(365 + (i_car - 1) * 70, 510, 68, 30));
        }

        public void draw_car_unit (int i_car) {
            gdi.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(365 + (i_car - 1) * 70, 560, 68, 25));
            gdi.Graphics.DrawRectangle(new Pen(Color.Blue), new Rectangle(365 + (i_car - 1) * 70, 560, 68, 25));
        }

        public String cvt_station_name (String i_name) {
            String str = "";
            int cnt = 0;
            foreach (char c in i_name) {
                str += c;
                str += "\n";
                if (3 < ++cnt) break;
            }
            return str;
        }

        public int get_clm_first_eki () {
            return 0;
        }

        // すべてのドアが閉まっているか
        private Boolean isAllDoorClosed () {
            DoorSet doors = BveHacker.Scenario.Vehicle.Doors;

            foreach (CarDoor d in (doors.GetSide(DoorSide.Left)).CarDoors) {
                if (d.State == DoorState.Open) return false;
            }

            foreach (CarDoor d in (doors.GetSide(DoorSide.Right)).CarDoors) {
                if (d.State == DoorState.Open) return false;
            }

            return true;

        }
    }
}








