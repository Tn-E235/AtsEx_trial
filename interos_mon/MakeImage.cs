using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IMakeImage{
    void make ();
    Bitmap get ();
    void Dispose ();
    bool isUpdate ();
}
