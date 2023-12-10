using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AtsEx.PluginHost;
using AtsEx.PluginHost.Handles;
using AtsEx.PluginHost.Plugins;

using BveTypes.ClassWrappers;

namespace AtsExCsTemplate.VehiclePlugin
{
    /// <summary>
    /// プラグインの本体
    /// </summary>
    [PluginType(PluginType.VehiclePlugin)]
    internal class VehiclePluginMain : AssemblyPluginBase {
        /// <summary>
        /// プラグインが読み込まれた時に呼ばれる
        /// 初期化を実装する
        /// </summary>
        /// <param name="builder"></param>
        public VehiclePluginMain (PluginBuilder builder) : base(builder) {
        }

        /// <summary>
        /// プラグインが解放されたときに呼ばれる
        /// 後処理を実装する
        /// </summary>
        public override void Dispose () {
        }

        // レバーサ位置を取得する
        int getReverser () {
            return (int)Native.Handles.Reverser.Position;
        }

        // 力行ノッチを取得する
        int getPowerNotch() {
            return Native.Handles.Power.Notch;
        }
        
        // 制動ノッチを取得する
        int getBrakeNotch () {
            return Native.Handles.Brake.Notch;
        }

        // 力行・制動ノッチを設定する
        HandleCommandSet setNotch(int p, int b) {
            return new HandleCommandSet(
                    Native.Handles.Power.GetCommandToSetNotchTo(p), 
                    Native.Handles.Brake.GetCommandToSetNotchTo(b), 
                    ReverserPositionCommandBase.Continue, 
                    ConstantSpeedCommand.Disable);
        }

        /// <summary>
        /// シナリオ読み込み中に毎フレーム呼び出される
        /// </summary>
        /// <param name="elapsed">前回フレームからの経過時間</param>
        public override TickResult Tick (TimeSpan elapsed) {
            UserVehicleLocationManager lm = BveHacker.Scenario.LocationManager;

            AtsEx.PluginHost.Handles.HandleSet h = Native.Handles;

            VehiclePluginTickResult ret = new VehiclePluginTickResult();

            // 自車速度[km/h]
            double speed = lm.SpeedMeterPerSecond*3.6;
            // 自車位置[m]
            double loction = lm.Location;
            // 力行ノッチ
            int p_notch = h.Power.Notch;
            // 制動ノッチ
            int b_notch = h.Brake.Notch;
            // レバーサ
            int teisoku = (int)h.Reverser.Position;

            if (speed > 10.0d) {
                ret.HandleCommandSet = setNotch(0, b_notch);
                lm.SetLocation(0, true);
            } else {
                ret.HandleCommandSet = setNotch(p_notch, b_notch);
            }

            return ret;
        }

        
    }
}
