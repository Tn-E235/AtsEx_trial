using PITempCS.mon;
using System;
using System.Drawing;
using Zbx1425.DXDynamicTexture;

public class DrawStopStations {
	String name;
	TimeSpan cyaku_jikoku;
	TimeSpan hatsu_jikoku;
	int cyakuhatsu_bansen;
	int hatsu_bansen;
	STOP_EKI_INF eki_inf;

    public DrawStopStations (){
		this.name = "未設定";
		this.cyaku_jikoku = new TimeSpan(0);
		this.hatsu_jikoku = new TimeSpan(0);
	}
}

