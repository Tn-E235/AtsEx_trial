using SlimDX.D3DCompiler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MakeTeisyaEkiListImage : IMakeImage {
    Bitmap img;
    Graphics g;
    Boolean update;
    public MakeTeisyaEkiListImage() {
    
    }

    public void Dispose () {
        g.Dispose();
    }

    public Bitmap get () {
        return img;
    }

    public void make() {}
    public bool isUpdate () {
        return this.update;
    }
    public void make (int a) {
    
    }
}
