using System.Drawing;

namespace Snake
{
    public class Food
    {        
        private readonly Brush foodColor;

        //GETTERS AND SETTERS ********************************
        public int axisX { get; set; }
        public int axisY { get; set; }

        public Food(int x, int y, Brush color)
        {
            axisX = x;
            axisY = y;
            foodColor = color;
        }

        public void DrawFood(Graphics g)
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            // Method				:	DrawFood()
            //
            // Method parameters	:	Graphics g
            //
            // Method return		:	void
            //
            // Synopsis				:   This method draws the food on the screen.							
            //
            // Modifications		:
            //							Date			Developer				Notes
            //							----			---------				-----
            //							2022-11-21		Tiago   				none.
            //
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {
            g.FillEllipse(foodColor, axisX*Game.SQUARE_SIZE, axisY * Game.SQUARE_SIZE, Game.SQUARE_SIZE, Game.SQUARE_SIZE);            
        }
    }
}
