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
        private static readonly Random Random = new Random();
        private GDIHelper GDIHelper;
        private static TextureHandle txH;
        private static Bitmap bmp;

        private static string dllParentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public DXDynamicTextureTest (PluginBuilder builder) : base(builder) {
            BveHacker.ScenarioCreated += OnScenarioCreated;
            txH = TextureManager.Register("img.png", 512, 512);
            GDIHelper = new GDIHelper(txH.Width, txH.Height);
            bmp = new Bitmap(Path.Combine(dllParentPath, "tex/img2.png"));
        }

        public override void Dispose () {
            BveHacker.ScenarioCreated -= OnScenarioCreated;
        }


        private void OnScenarioCreated (ScenarioCreatedEventArgs e) {
        }

        public override TickResult Tick (TimeSpan elapsed) {
            StationList stations = BveHacker.Scenario.Route.Stations;

            if (txH.HasEnoughTimePassed(10)) {
                GDIHelper.BeginGDI();
                GDIHelper.DrawImage(bmp, 0, 0);
                GDIHelper.EndGDI();

                Font drawFont = new Font("JFドットjiskan16", 16);

                GDIHelper.Graphics.DrawString(
                        "駅リスト",
                        drawFont,
                        Brushes.Green, 0, 0
                );

                for (int i = 0; i < stations.Count; ++i) {
                    GDIHelper.Graphics.DrawString(
                        (((Station)stations[i]).Name).ToString(),
                        drawFont,
                        Brushes.Green, 0, (i+1) * 20
                    );
                }

                txH.Update(GDIHelper);
                
                //MessageBox.Show("正しい値を入力してください。","エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
                
            }


            return new VehiclePluginTickResult();
        }
    }
}








