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

namespace AtsExCsTemplate.VehiclePlugin {
    [PluginType(PluginType.VehiclePlugin)]
    public class DXDynamicTextureTest : AssemblyPluginBase {
        private GDIHelper gdi;
        private Bitmap mon_R;
        private static TextureHandle txH;

        private const String DEFAULT_FONT_FAMILY = "Cica";// "HGSｺﾞｼｯｸE"; //"KHドット秋葉原16";//  "JFドットjiskan16";
        private const String DEFAULT_DOT_FONT_FAMILY = "JFドットAyuゴシック20";//"JFドットjiskan16";
        private const int DEFAULT_FONT_SIZE = 22;
        private const int MON_H = 800;
        private const int MON_W = 1280;
        private bool update = true;
        private static string dllParentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public DXDynamicTextureTest (PluginBuilder builder) : base(builder) {
            BveHacker.ScenarioCreated += OnScenarioCreated;
            txH = TextureManager.Register("base.png", 2048, 1024);
            gdi = new GDIHelper(txH.Width, txH.Height);
            mon_R = new Bitmap(Path.Combine(dllParentPath, "./tex/base.png"));
        }

        public override void Dispose () {
            BveHacker.ScenarioCreated -= OnScenarioCreated;
            mon_R.Dispose();
        }


        private void OnScenarioCreated (ScenarioCreatedEventArgs e) {
        }

        public override TickResult Tick (TimeSpan elapsed) {

            if (txH.IsCreated && txH.HasEnoughTimePassed(1000) && update) {
                Font drawFont = new Font(DEFAULT_FONT_FAMILY, DEFAULT_FONT_SIZE);

                // 画面ベース
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(71, 80, 101)), new Rectangle(0, 0, 1280, 800));
                // 画面上部
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0)), new Rectangle(0, 0, 300, 100));
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0)), new Rectangle(305, 0, 265, 100));
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0)), new Rectangle(575, 0, 705, 100));
                // 三本線
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(71, 80, 101)), new Rectangle(800, 10, 320, 25));
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(71, 80, 101)), new Rectangle(800, 40, 320, 25));
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(71, 80, 101)), new Rectangle(800, 70, 320, 25));
                // 運転情報
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(53, 62, 83)), new Rectangle(0, 100, 1280, 100));
                // 開通情報
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0)), new Rectangle(0, 395, 170, 25));
                // 転動防止
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0)), new Rectangle(0, 205, 200, 225));
                // 自車位置
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), new Rectangle(650, 360, 395, 3));
                // 車両状態帯
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0)), new Rectangle(0, 500, 1280, 100));
                // ドア
                for (int i = 0; i < Native.VehicleSpec.Cars - 1; ++i) {
                    draw_door(i);
                }
                // 下部ボタン帯
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0)), new Rectangle(0, 685, 1280, 115));
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(71, 80, 101)), new Rectangle(630, 700, 495, 25));
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(71, 80, 101)), new Rectangle(630, 730, 495, 25));
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(71, 80, 101)), new Rectangle(630, 760, 495, 25));
                // 適当な文字
                gdi.Graphics.DrawString("ＩＮＴＥＲＯＳモニタプラグイン", drawFont, Brushes.White, 630, 699);
                gdi.Graphics.DrawString("Ｖｅｒｓｉｏｎ＿０．１", drawFont, Brushes.White, 630, 729);
                gdi.Graphics.DrawString("制作＠Ｔｎ＿Ｅ２３５", drawFont, Brushes.White, 630, 759);
                // 下部button

                DrawButton buttunImg = new DrawButton();
                gdi.Graphics.DrawImage(buttunImg.getButtonImage(155, 65, 5), 955, 615);
                gdi.Graphics.DrawImage(buttunImg.getButtonImage(155, 65, 5), 1115, 615);

                gdi.Graphics.DrawImage(buttunImg.getButtonImage(150, 105, 5), 10, 690);
                gdi.Graphics.DrawImage(buttunImg.getButtonImage(150, 105, 5), 165, 690);
                gdi.Graphics.DrawImage(buttunImg.getButtonImage(150, 105, 5), 320, 690);
                gdi.Graphics.DrawImage(buttunImg.getButtonImage(150, 105, 5), 475, 690);

                gdi.BeginGDI();
                gdi.EndGDI();


                int cnt = 0;
                // 停車駅一覧[1]-[5]を表示
                StationList stations = BveHacker.Scenario.Route.Stations;
                for (int i = stations.CurrentIndex; i < stations.Count; ++i) {
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

                // 採時駅表示(着時刻および発時刻がある駅とする)
                draw_saiji_eki();

                // 終着駅表示
                draw_syucyaku_eki();

                txH.Update(gdi);
                //update = false;
            }

            return new VehiclePluginTickResult();
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
            draw_station_name(6, ((Station)stations[stations.Count-1]).Name);
            draw_arrive_time(6, ((Station)stations[stations.Count-1]).ArrivalTime);
            draw_deperture_time(6, ((Station)stations[stations.Count-1]).DepertureTime);
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
            // 左右どちらかのドアがあいているとき、開表示とする
            if (BveHacker.Scenario.Vehicle.Doors.GetSide(DoorSide.Left).CarDoors[i_car].State == DoorState.Close
                    && BveHacker.Scenario.Vehicle.Doors.GetSide(DoorSide.Right).CarDoors[i_car].State == DoorState.Close) {
                gdi.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(365 + (i_car - 1) * 70, 510, 68, 30));
            } else {
                gdi.Graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(365 + (i_car - 1) * 70, 510, 68, 30));
            }
            gdi.Graphics.DrawRectangle(Pens.Blue, new Rectangle(365 + (i_car - 1) * 70, 510, 68, 30));
            // 号車表示
        }


        public String cvt_station_name (String i_name) {
            String str = "";
            foreach (char c in i_name) {
                str += c;
                str += "\n";
            }
            return str;
        }

        

    }
}








