using AtsEx.PluginHost.Plugins;
using Mono.Collections.Generic;
using PITempCS.mon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace AtsExCsTemplate.VehiclePlugin {
    [PluginType (PluginType.VehiclePlugin)]
    public class Heisoku_Inf : AssemblyPluginBase {
        public Heisoku_Inf (PluginBuilder builder) : base(builder) {
        }

        public override void Dispose () {

        }

        public override TickResult Tick (TimeSpan elapsed) {
            return new VehiclePluginTickResult();
        }

        public int test() {
            return BveHacker.Scenario.Vehicle.Doors.AreAllClosingOrClosed ? 0 : 1;
        }

        //public List<BCST_HEISOKU> Get () {
        //    List<BCST_HEISOKU> list = new List<BCST_HEISOKU>();
        //    return list;
        //}

        //public struct BCST_HEISOKU {
        //    public int type;            // 閉塞種別[0:閉塞, 1:場内]
        //    public double location;     // 閉塞開始距離[m]
        //    public double distance;     // 閉塞長[m]
        //}
    }
}