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

namespace AtsExCsTemplate.VehiclePlugin {
    [PluginType(PluginType.VehiclePlugin)]
    public class DXDynamicTextureTest : AssemblyPluginBase {
        private GDIHelper gdi;
        private Bitmap mon_R;
        private static TextureHandle txH;

        private const String DEFAULT_FONT_FAMILY = "Cica";// "HGSｺﾞｼｯｸE"; //"KHドット秋葉原16";//  "JFドットjiskan16";
        private const String DEFAULT_DOT_FONT_FAMILY = "JFドットAyuゴシック20";//"JFドットjiskan16";
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

            if (txH.IsCreated && txH.HasEnoughTimePassed(10) && update) {

                Rectangle rc = new Rectangle(0, 0, 1280, 800);
                gdi.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(71, 80, 101)), rc);

                gdi.BeginGDI();
                gdi.EndGDI();
                
                txH.Update(gdi);
                update = false;
            }

            return new VehiclePluginTickResult();
        }
    }
}








