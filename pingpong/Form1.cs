using System;
using System.Drawing;
using System.Windows.Forms;

namespace pingpong
{
    public partial class Form1 : Form
    {
        GameEngine _gameEngine = GameEngine.Instance();

        Graphics Canvas;
        SolidBrush sb = new SolidBrush(Color.White);
        private static Font DrawFont = new Font("Arial", 40);

        public Form1()
        {
            InitializeComponent();
            canvas.BackColor = Color.Black;
            Canvas = canvas.CreateGraphics();
            _gameEngine.StartGame();
        }

        public void DrawIt()
        {    
            Canvas.Clear(Color.Black);
            Canvas.FillRectangle(sb, _gameEngine.Player1.Body);
            Canvas.FillRectangle(sb, _gameEngine.Player2.Body);
            Canvas.FillRectangle(new SolidBrush(Color.Red), _gameEngine.GameBall.Body);

            Canvas.DrawString(_gameEngine.Player1.Score.ToString(), DrawFont, sb, (int)(this.ClientRectangle.Width*0.2), 10);
            Canvas.DrawString(_gameEngine.Player2.Score.ToString(), DrawFont, sb, (int)(this.ClientRectangle.Width*0.8), 10);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            _gameEngine.KeyDown(e);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            _gameEngine.KeyUp(e);
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            DrawIt();
            _gameEngine.GameCycle();                        
        }
    }
}
