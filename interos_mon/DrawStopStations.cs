using AtsEx.PluginHost;
using BveTypes.ClassWrappers;
using PITempCS.mon;
using System;
using System.Drawing;
using Zbx1425.DXDynamicTexture;
using AtsEx.PluginHost.Plugins;

public struct STATION_INF {
	public string name;
	public int idx;
	public int position;
	public TimeSpan cyaku_jikoku;
	public TimeSpan hatsu_jikoku;
	public int bansen;
	public int location;
	public STATION_INF(int i_pos) {
		name = "未設定";
		idx = -1;
		position = i_pos;
		cyaku_jikoku = new TimeSpan(0);
		hatsu_jikoku = new TimeSpan(0);
		bansen = -1;
		location = -1;
	}
}
public class DrawStopStations {
	MakeStopStationImg[] imgs = new MakeStopStationImg[5];
    public DrawStopStations (){
		this.imgs[0] = new MakeStopStationImg(1);
        this.imgs[1] = new MakeStopStationImg(2);
        this.imgs[2] = new MakeStopStationImg(3);
        this.imgs[3] = new MakeStopStationImg(4);
        this.imgs[4] = new MakeStopStationImg(5);
    }

	public void draw(GDIHelper i_gdi, StationList i_stations, int i_idx) {
		int i = 0;
		foreach (MakeStopStationImg s in this.imgs) {
			if (i_stations.Count <= i_idx + i || i_idx + i < 0) {
                s.make(inf_set(null, -1, i));
            } else {
				s.make(inf_set((Station)i_stations[i_idx + i], i_idx, i));
			}
			if (this.imgs[i].isUpdate()) {
				i_gdi.Graphics.DrawImage(s.get(), 1025 - i * 100, 205);
			}
            ++i;
        }

    }

	public void dispose() {
		this.imgs[0].dispose();
        this.imgs[1].dispose();
        this.imgs[2].dispose();
        this.imgs[3].dispose();
        this.imgs[4].dispose();
    }

	private STATION_INF inf_set(Station i_station, int i_idx, int i_pos) {
        STATION_INF inf = new STATION_INF(0);
		inf.idx = i_idx;
		inf.position = i_pos;
		
		if (i_station == null || i_idx < 0) {
			return inf;
		}
		
		inf.name = i_station.Name;
		inf.cyaku_jikoku = i_station.ArrivalTime;
		inf.hatsu_jikoku = i_station.DepertureTime;
		inf.location = (int)i_station.Location;
		return inf;
    }
}

public class MakeStopStationImg {
	STATION_INF station_inf;
	Bitmap bmp;
	Graphics g;
	bool update;
	Font font_mm;
	Font font_ss;
	Font font_sta;
	BACKGROUND_COLOR bg_color;
	public MakeStopStationImg (int i) {
		this.bmp = new Bitmap(40, 200);
		this.g = Graphics.FromImage(this.bmp);
		this.station_inf = new STATION_INF(0);
		this.update = true;
		this.font_sta = new Font("VL ゴシック", 22);
		this.font_mm = new Font("VL ゴシック", 16);
		this.font_ss = new Font("VL ゴシック", 10);
		this.bg_color = new BACKGROUND_COLOR(0);
	}

	public void make (STATION_INF i_inf) {
		// 更新判定
		if (!update_check(i_inf)) {
			this.update = false;
			return;
		}
		g.FillRectangle(new SolidBrush(bg_color.bg1), new Rectangle(0, 0, this.bmp.Width, this.bmp.Height));
		draw_station_name(i_inf.name);
		draw_arrive_time(i_inf.cyaku_jikoku);
		draw_deperture_time(i_inf.hatsu_jikoku);
		this.update = true;
		this.station_inf = i_inf;
	}

	public Bitmap get () {
		return this.bmp;
	}

	public void dispose () {
		this.g.Dispose();
	}

	public bool isUpdate () {
		return this.update;
	}
	public int get_pos () {
		return this.station_inf.position;
	}

	private bool update_check(STATION_INF i_inf) {
		if (i_inf.name == this.station_inf.name 
				&& i_inf.idx == this.station_inf.idx 
				&& i_inf.cyaku_jikoku.TotalMilliseconds == this.station_inf.cyaku_jikoku.TotalMilliseconds
				&& i_inf.hatsu_jikoku.TotalMilliseconds == this.station_inf.hatsu_jikoku.TotalMilliseconds
				// && i_inf.bansen == this.station_inf.bansen
				// && i_inf.location == this.station_inf.location
        ) {
			return false;
		}
		return true;
	}

    public void draw_station_name (String i_name) {
        this.g.DrawString(cvt_station_name(i_name), font_sta, Brushes.White, 5, 0);
    }

    public void draw_arrive_time (TimeSpan i_time) {
        // 時刻未設定の場合は表示なし
        if (i_time.TotalMilliseconds < 0.1d) return;
		this.g.DrawString(String.Format("{0:00}", i_time.Minutes), font_mm, Brushes.White, 0, 118);
		this.g.DrawString(String.Format("{0:00}", i_time.Seconds), font_ss, Brushes.White, 25, 119);
    }

    public void draw_deperture_time (TimeSpan i_time) {
        // 時刻未設定の場合は表示なし
        if (i_time.TotalMilliseconds < 0.1d) return;
		this.g.DrawString(String.Format("{0:00}", i_time.Minutes), font_mm, Brushes.White, 0, 170);
		this.g.DrawString(String.Format("{0:00}", i_time.Seconds), font_ss, Brushes.White, 25, 171);
    }
    public String cvt_station_name (String i_name) {
        String str = "";
        int cnt = 0;
        foreach (char c in i_name) {
            str += c;
            str += "\n";
            if (3 < ++cnt) break;
        }
        return str;
    }
} 