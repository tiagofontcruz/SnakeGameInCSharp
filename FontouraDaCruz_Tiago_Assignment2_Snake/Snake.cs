using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;

namespace Snake
{
    public enum Direction
    {
        Down,
        Up,
        Right,
        Left
    }

    public class Snake
    {
        private int bodyPieces = 4;

        //GETTERS AND SETTERS ********************************
        public int BodyPieces
        {
            get { return bodyPieces; }   
            set { bodyPieces = value; }  
        }

        public Brush snakeColor;

        public Snake(Brush color)
        {
            snakeColor = color;
        }

        public void DrawSnake(Graphics g, int axisX, int axisY, int index)
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            // Method				:	DrawSnake()
            //
            // Method parameters	:	Graphics g, int axisX, int axisY, int index
            //
            // Method return		:	void
            //
            // Synopsis				:   This method draws the snake on the screen.							
            //							If the index is 0 it draws the head in red color. Otherwise draws in green.
            // Modifications		:
            //							Date			Developer				Notes
            //							----			---------				-----
            //							2022-11-21		Tiago   				none.
            //
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {
            if (index == 0)
            {
                snakeColor = Brushes.Red;
                g.FillRectangle(snakeColor, axisX * Game.SQUARE_SIZE, axisY * Game.SQUARE_SIZE, Game.SQUARE_SIZE, Game.SQUARE_SIZE);
            }
            else
            {
                snakeColor = Brushes.Green;
                g.FillRectangle(snakeColor, axisX * Game.SQUARE_SIZE, axisY * Game.SQUARE_SIZE, Game.SQUARE_SIZE, Game.SQUARE_SIZE);
            }
        }
    }
}
