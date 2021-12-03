using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextOS.Engine;
using System.Drawing;

namespace TextOS
{
    class Demo : Engine.Engine
    {
        Shape2D box1;
        Shape2D box2;
        Shape2D box3;
        Shape2D box4;
        Shape2D box5;

        public Demo() : base(new Vector2(615, 515), "Engine") { }

        public override void onLoad()
        {
            BackgroundColor = Color.Black;

            box1 = new Shape2D(new Vector2(10, 20), new Vector2(10, 10), "box1");
            box2 = new Shape2D(new Vector2(30, 15), new Vector2(20, 20), "box2");
            box3 = new Shape2D(new Vector2(60, 10), new Vector2(30, 30), "box3");
            box4 = new Shape2D(new Vector2(100, 15), new Vector2(20, 20), "box4");
            box5 = new Shape2D(new Vector2(130, 20), new Vector2(10, 10), "box5");
        }

        public override void onDraw()
        {

        }

        public override void onUpdate()
        {
            
        }
    }
}
