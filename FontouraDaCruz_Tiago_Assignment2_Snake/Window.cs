using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Window : Form
    {
        private const int WIDTH = 30;
        private const int HEIGHT = 30;
        private const string SCORE_LABEL = "Score: {0}";
        private readonly Color backgroundColor = Color.Black;
        private readonly Game game;
        private readonly Bitmap gameBoard;
        private readonly Graphics gameGraphics;

        public Window()
        {
            InitializeComponent();
            this.ClientSize = new Size(WIDTH * Game.SQUARE_SIZE, HEIGHT * Game.SQUARE_SIZE);
            gameBoard = new Bitmap(WIDTH * Game.SQUARE_SIZE, HEIGHT * Game.SQUARE_SIZE);
            gameGraphics = Graphics.FromImage(gameBoard); 
            gameGraphics.PageUnit = GraphicsUnit.Pixel;
            ClientSize = new Size(gameBoard.Width, gameBoard.Height);
            game = new Game(WIDTH, HEIGHT);
            game.StartGame();
            UpdateScore();
            gameTimer.Interval = Game.SPEED;
            gameTimer.Start();            
        }

        private void UpdateScore()
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            // Method				:	UpdateScore()
            //
            // Method parameters	:	none
            //
            // Method return		:	void
            //
            // Synopsis				:   This method updates the score.						
            //							
            // Modifications		:
            //							Date			Developer				Notes
            //							----			---------				-----
            //							2022-11-21		Tiago   				none.
            //
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {
            scoreLbl.ForeColor = Color.White;
            scoreLbl.BackColor = Color.Black;           
            scoreLbl.Text = string.Format(SCORE_LABEL, game.GetScore());
        }

        private void OnTimerTick(object sender, EventArgs e)
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            // Method				:	OnTimerTick()
            //
            // Method parameters	:	object sender, EventArgs e
            //
            // Method return		:	void
            //
            // Synopsis				:   This method updates the game board components.						
            //							
            // Modifications		:
            //							Date			Developer				Notes
            //							----			---------				-----
            //							2022-11-21		Tiago   				none.
            //
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {
            game.Move();
            if (game.CheckFood())
            {
                UpdateScore();
            }
            if (game.GameOver())
            {
                gameTimer.Stop();                
            }
            Invalidate();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            // Method				:	OnKeyDown()
            //
            // Method parameters	:	object sender, EventArgs e
            //
            // Method return		:	void
            //
            // Synopsis				:   This method captures the keyboard buttons and sets the snake's direction.						
            //							
            // Modifications		:
            //							Date			Developer				Notes
            //							----			---------				-----
            //							2022-11-21		Tiago   				none.
            //
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    game.ChangeDirection(Direction.Left);
                    break;
                case Keys.Right:
                    game.ChangeDirection(Direction.Right);
                    break;
                case Keys.Up:
                    game.ChangeDirection(Direction.Up);
                    break;
                case Keys.Down:
                    game.ChangeDirection(Direction.Down);
                    break;
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            // Method				:	OnPaint()
            //
            // Method parameters	:	object sender, EventArgs e
            //
            // Method return		:	void
            //
            // Synopsis				:   This method draws everything inside the board.						
            //							
            // Modifications		:
            //							Date			Developer				Notes
            //							----			---------				-----
            //							2022-11-21		Tiago   				none.
            //
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {
            gameGraphics.Clear(backgroundColor);
            game.DrawGame(gameGraphics);
            e.Graphics.DrawImage(gameBoard, 0, 0);
        }
    }
}

