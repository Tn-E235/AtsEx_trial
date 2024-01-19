using AtsEx.PluginHost.Plugins;
using BveTypes.ClassWrappers;
using System;
using System.Drawing;
using Zbx1425.DXDynamicTexture;

[PluginType(PluginType.VehiclePlugin)]

public class DrawDoors : AssemblyPluginBase {
    bool update;
    public DrawDoors (PluginBuilder builder) : base(builder) {
    }

    public void draw (GDIHelper i_gdi) {
        // 更新判定
        // if (!this.isUpdate()) return;
        Font drawFont = new Font("VL ゴシック", 16);
        for (int i = 0; i < Native.VehicleSpec.Cars - 1; ++i) {
            // 左右どちらかのドアがあいているとき、開表示とする
            if (!BveHacker.Scenario.Vehicle.Doors.GetSide(DoorSide.Left).CarDoors[i].IsOpen
                    && !BveHacker.Scenario.Vehicle.Doors.GetSide(DoorSide.Right).CarDoors[i].IsOpen) {
                i_gdi.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(365 + (i - 1) * 70, 510, 68, 30));
                i_gdi.Graphics.DrawString("閉", drawFont, Brushes.Black, 365 + (i - 1) * 70 + 20, 510 + 5);
            } else {
                i_gdi.Graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(365 + (i - 1) * 70, 510, 68, 30));
                i_gdi.Graphics.DrawString("開", drawFont, Brushes.White, 365 + (i - 1) * 70 + 20, 510 + 5);
            }
            i_gdi.Graphics.DrawRectangle(Pens.Blue, new Rectangle(365 + (i - 1) * 70, 510, 68, 30));
        }
    }

    public bool isUpdate () {
        return this.update;
    }
    public override void Dispose () {
    }

    public override TickResult Tick (TimeSpan elapsed) {
        return new VehiclePluginTickResult();
    }
}
