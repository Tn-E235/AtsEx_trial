using AtsEx.PluginHost.Plugins;
using BveTypes.ClassWrappers;
using System;
using System.Drawing;
using Zbx1425.DXDynamicTexture;

[PluginType(PluginType.VehiclePlugin)]

public class DrawUnits : AssemblyPluginBase {
	bool update;
	public DrawUnits(PluginBuilder builder) : base(builder) {
	}

	public void draw(GDIHelper i_gdi) {
        // 更新判定
        // if (!this.isUpdate()) return;
        for (int i = 0; i < Native.VehicleSpec.Cars - 1; ++i) {
            i_gdi.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(365 + (i - 1) * 70, 560, 68, 25));
            i_gdi.Graphics.DrawRectangle(new Pen(Color.Blue), new Rectangle(365 + (i - 1) * 70, 560, 68, 25));
        }
    }

	public bool isUpdate() {
		return this.update;
	}
	public override void Dispose () {
    }

	public override TickResult Tick (TimeSpan elapsed) {
        return new VehiclePluginTickResult();
    }
}
