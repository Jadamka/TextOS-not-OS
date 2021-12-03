using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace TextOS.Engine
{
    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }

    public abstract class Engine
    {
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title = "Title";
        private Canvas Window = null;
        private Thread GameLoopThread = null;

        private static List<Shape2D> AllShapes = new List<Shape2D>();

        public Color BackgroundColor = Color.Beige;

        public Engine(Vector2 ScreenSize, string Title)
        {
            Log.Info("Game is starting...");
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y);
            Window.Text = this.Title;
            Window.Paint += Renderer;

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        public static void RegisterShape(Shape2D shape)
        {
            AllShapes.Add(shape);
        }

        public static void UnRegisterShape(Shape2D shape)
        {
            AllShapes.Remove(shape);
        }

        void GameLoop()
        {
            onLoad();
            while (GameLoopThread.IsAlive)
            {
                try
                {
                    onDraw();   // drawing into the game
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    onUpdate(); // for movement and physics
                    Thread.Sleep(1);
                }
                catch
                {
                    Log.Error("Game has not been found...");
                }
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BackgroundColor);

            foreach (Shape2D shape in AllShapes)
            {
                g.FillRectangle(new SolidBrush(Color.Red), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
            }
        }

        public abstract void onLoad();
        public abstract void onUpdate();
        public abstract void onDraw();
    }
}
