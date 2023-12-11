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

namespace AtsExCsTemplate.VehiclePlugin {
    [PluginType(PluginType.VehiclePlugin)]
    internal class VehiclePluginMain : AssemblyPluginBase {
        public VehiclePluginMain (PluginBuilder builder) : base(builder) {
        }

        public override void Dispose () {
        }

        public override TickResult Tick (TimeSpan elapsed) {
            return new VehiclePluginTickResult();
        }

    }
}
