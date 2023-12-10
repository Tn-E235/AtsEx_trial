using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

        //自車速度を取得する
        double getSpeed () {
            // 秒速[m/s]で取得されるので時速[km/h]に変換
            return BveHacker.Scenario.LocationManager.SpeedMeterPerSecond * 3.6;
        }

        // 自車位置を取得する
        double getLocation () {
            return BveHacker.Scenario.LocationManager.Location;
        }

        // レバーサ位置を取得する
        int getReverser () {
            return (int)Native.Handles.Reverser.Position;
        }

        // 力行ノッチを取得する
        int getPowerNotch () {
            return Native.Handles.Power.Notch;
        }

        // 制動ノッチを取得する
        int getBrakeNotch () {
            return Native.Handles.Brake.Notch;
        }

        // 自車位置を設定する(第一引数:自車位置[z座標]、第二引数:更新の有無？)
        void setLocation (double z, bool b) {
            BveHacker.Scenario.LocationManager.SetLocation(z, b);
        }

        // 駅一覧を取得する<リスト>
        StationList getStationList () {
            return BveHacker.Scenario.Route.Stations;
        }

        // 指定番号の駅情報を取得する(第一引数：0～駅数-1)
        Station getStation (int idx) {
            StationList ss = BveHacker.Scenario.Route.Stations;
            if (ss == null || idx < 0 || ss.Count <= idx) {
                return null;
            }
            return (Station)ss[idx];
        }

        // 指定番号駅の駅名称を取得する(第一引数：0～駅数-1)
        String getStationName (int idx) {
            Station s = getStation(idx);
            if (s == null) {
                return "none";
            }
            return s.Name;
        }

        // 指定駅番号の駅設置距離を取得する(第一引数：0～駅数-1)
        double getStationLocation (int idx) {
            Station s = getStation(idx);
            if (s == null) {
                return -1.0d;
            }
            return s.Location;
        }

        // 指定駅番号のドア開閉位置(左右)を取得する(第一引数：0～駅数-1)
        int getStationDoorOpenLR (int idx) {
            Station s = getStation(idx);
            if (s == null) {
                return 0;
            }
            return s.DoorSide;
        }

        // 指定駅番号の駅通過設定を取得する(第一引数：0～駅数-1)
        bool getStationDoor (int idx) {
            Station s = getStation(idx);
            if (s == null) {
                return false;
            }
            return s.Pass;
        }

        // 指定駅番号のオーバーラン位置を取得する(第一引数：0～駅数-1)
        double getStopLocation_a (int idx) {
            Station s = getStation(idx);
            if (s == null) return 0.0d;
            return s.MaxStopPosition;
        }

        // 指定駅番号のアンダーラン位置を取得する(第一引数：0～駅数-1)
        double getStopLocation_b (int idx) {
            Station s = getStation(idx);
            if (s == null) {
                return 0.0d;
            }
            return s.MinStopPosition;
        }

        // 指定駅番号の到着時刻を取得する
        TimeSpan getStationDepTime (int idx) {
            Station s = getStation(idx);
            if (s == null) {
                TimeSpan t = new TimeSpan();
                return t;
            }
            return s.DefaultTime;
        }

        // 指定駅番号の出発時刻を取得する
        TimeSpan getStationArrTime (int idx) {
            Station s = getStation(idx);
            if (s == null) {
                TimeSpan t = new TimeSpan();
                return t;
            }
            return s.ArrivalTime;
        }


        // 力行・制動ノッチを設定する
        HandleCommandSet setNotch (int p, int b) {
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
            // 自車情報(VehicleState)
            UserVehicleLocationManager lm = BveHacker.Scenario.LocationManager;
            // 本関数の返値
            VehiclePluginTickResult ret = new VehiclePluginTickResult();

            // 自車速度[km/h]
            double speed = getSpeed();
            // 自車位置[m]
            double loction = getLocation();
            // 力行ノッチ
            int p_notch = getPowerNotch();
            // 制動ノッチ
            int b_notch = getBrakeNotch();
            // レバーサ
            int reverser = getReverser();

            if (speed > 10.0d) {
                // ハンドル位置設定
                ret.HandleCommandSet = setNotch(0, b_notch);
                // 自車位置[Z座標]を変更
                setLocation(0, true);
            } else {
                // ハンドル位置設定
                ret.HandleCommandSet = setNotch(p_notch, b_notch);
            }

            return ret;
        }

        
    }
}
