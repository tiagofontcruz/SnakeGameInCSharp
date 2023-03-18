using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Timer = System.Windows.Forms.Timer;

namespace Snake
{
    public class Game
    {
        public const int SQUARE_SIZE = 25;                                                                          //
        public const int SPEED = 300;                                                                               //
        private readonly int boardWidth;                                                                            //
        private readonly int boardHeight;                                                                           //
                                                                                                                    //
        private Direction Direction;                                                                                //
                                                                                                                    //
        private readonly Snake snake;                                                                               //
        private readonly Food food;                                                                                 //
        private readonly Random rand;                                                                               //
                                                                                                                    //
        private int foodScore;                                                                                      //
        private int[] axisX;                                                                                        //
        private int[] axisY;                                                                                        //
        private bool isRunning = false;                                                                             //
                                                                                                                    //
        public Game(int width, int height)                                                                          //
        {                                                                                                           //
            boardWidth = width;                                                                                     //
            boardHeight = height;                                                                                   //
            axisX = new int[boardWidth];                                                                            //
            axisY = new int[boardHeight];                                                                           //
            snake = new Snake(Brushes.Green);                                                                       //
            rand = new Random();                                                                                    //
            food = new Food(0, 0, Brushes.Yellow);                                                                  //
            StartGame();                                                                                            //
        }                                                                                                           //
                                                                                                                    //
        public void StartGame()                                                                                     //
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
	        // Method				:	startGame()
	        //
	        // Method parameters	:	none
	        //
	        // Method return		:	void
	        //
	        // Synopsis				:   This method starts the game.						
	        //							
	        // Modifications		:
	        //							Date			Developer				Notes
	        //							----			---------				-----
	        //							2022-11-21		Tiago   				none.
	        //
	        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {                                                                                                           //
            snake.BodyPieces = 4;                                                                                   //
            axisX[0] = 0;                                                                                           //
            axisY[0] = 0;                                                                                           //
            Direction = Direction.Down;                                                                             //
            loadFile();                                                                                             //
            FoodRespawn();                                                                                          //
            isRunning = true;                                                                                       //
        }                                                                                                           //
                                                                                                                    //
        public void DrawGame(Graphics g)                                                                            //
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
		    // Method				:	DrawGame()
		    //
		    // Method parameters	:	Graphics g
		    //
		    // Method return		:	void
		    //
		    // Synopsis				:   This method draws the game itself (snake, food and score).						
		    //							
		    // Modifications		:
		    //							Date			Developer				Notes
		    //							----			---------				-----
		    //							2022-11-21		Tiago   				none.
		    //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {                                                                                                           //
                food.DrawFood(g);                                                                                   //Draws the food.
                for (int index = 0; index < snake.BodyPieces; index++)                                              //Loops to draw each part of the snake.
                {                                                                                                   //
                    snake.DrawSnake(g, axisX[index], axisY[index], index);                                          //
                }                                                                                                   //
                                                                                                                    //
                if (!isRunning)                                                                                     //Checks if the game is not standing.
                {                                                                                                   //
                    var rect = new Rectangle(0, 0, boardWidth * Game.SQUARE_SIZE, boardHeight * Game.SQUARE_SIZE);  //
                    var format = new StringFormat(StringFormat.GenericDefault);                                     //
                    format.Alignment = StringAlignment.Center;                                                      //
                    format.LineAlignment = StringAlignment.Center;                                                  //
                    g.DrawString("GAME OVER", new Font("Arial", 25, FontStyle.Bold), Brushes.White, rect, format);  //Draws the Game Over screen.
                }                                                                                                   //
        }                                                                                                           //
                                                                                                                    //
        private void FoodRespawn()                                                                                  //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
		    // Method				:	FoodRespawn()
		    //
		    // Method parameters	:	none
		    //
		    // Method return		:	void
		    //
		    // Synopsis				:   This method generates the random position for the food appears.						
		    //							
		    // Modifications		:
		    //							Date			Developer				Notes
		    //							----			---------				-----
		    //							2022-11-21		Tiago   				none.
		    //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {                                                                                                           //
            food.axisX = rand.Next(0, boardWidth);                                                                  //
            food.axisY = rand.Next(0, boardHeight);                                                                 //
        }                                                                                                           //
                                                                                                                    //
        public void Move()                                                                                          //
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
		    // Method				:	Move()
		    //
		    // Method parameters	:	none
		    //
		    // Method return		:	void
		    //
		    // Synopsis				:   This method updates the snake position.						
		    //							
		    // Modifications		:
		    //							Date			Developer				Notes
		    //							----			---------				-----
		    //							2022-11-21		Tiago   				none.
		    //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {                                                                                                           //
            int index;                                                                                              //
            for (index = snake.BodyPieces; index > 0; index--)                                                      //Keeps updating the snake.
            {                                                                                                       //
                axisX[index] = axisX[index - 1];                                                                    //
                axisY[index] = axisY[index - 1];                                                                    //
            }                                                                                                       //
                                                                                                                    //
            if (Direction == Direction.Up)                                                                          //
            {                                                                                                       //
                axisY[0] -= 1;                                                                                      //Updates Y coordinate.
            }                                                                                                       //
            else if (Direction == Direction.Down)                                                                   //
            {                                                                                                       //
                axisY[0] += 1;                                                                                      //Updates Y coordinate.
            }                                                                                                       //
            else if (Direction == Direction.Left)                                                                   //
            {                                                                                                       //
                axisX[0] -= 1;                                                                                      //Updates X coordinate.
            }                                                                                                       //
            else if (Direction == Direction.Right)                                                                  //
            {                                                                                                       //
                axisX[0] += 1;                                                                                      //Updates X coordinate.
            }                                                                                                       //
        }                                                                                                           //
                                                                                                                    //
        public bool CheckFood()                                                                                     //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
		    // Method				:	CheckFood()
		    //
		    // Method parameters	:	none
		    //
		    // Method return		:	void
		    //
		    // Synopsis				:   This method checks collision between the snake's head and the food positions, add points and sets the respawn.						
		    //							
		    // Modifications		:
		    //							Date			Developer				Notes
		    //							----			---------				-----
		    //							2022-11-21		Tiago   				none.
		    //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {                                                                                                           //
            if (axisX[0] == food.axisX && axisY[0] == food.axisY)                                                   //Checks the collision.
            {                                                                                                       //
                snake.BodyPieces = snake.BodyPieces + 1;                                                            //Draws another piece of the snake.
                foodScore++;                                                                                        //Add points.
                FoodRespawn();                                                                                      //Repawns the food.
                return true;                                                                                        //
            }                                                                                                       //
            return false;                                                                                           //
        }                                                                                                           //
                                                                                                                    //
        public bool CheckCollisions()                                                                               //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
		    // Method				:	CheckCollisions()
		    //
		    // Method parameters	:	none
		    //
		    // Method return		:	void
		    //
		    // Synopsis				:   This method checks collision between the snake's and the board, also between the snake's head and body.						
		    //							
		    // Modifications		:
		    //							Date			Developer				Notes
		    //							----			---------				-----
		    //							2022-11-21		Tiago   				none.
		    //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {                                                                                                           //
            int index;                                                                                              //
            for (index = snake.BodyPieces - 1; index > 0; index--)                                                  //Checks if the head is going to collide with the body.
            {                                                                                                       //
                if (axisX[0] == axisX[index] && axisY[0] == axisY[index])                                           //
                {                                                                                                   //
                    isRunning = false;                                                                              //
                }                                                                                                   //
            }                                                                                                       //
                                                                                                                    //
            if (axisX[0] < 0)                                                                                       //Checks the collision on the edges.
            {                                                                                                       //
                isRunning = false;                                                                                  //
            }                                                                                                       //
            else if (axisX[0] > boardWidth)                                                                         //
            {                                                                                                       //
                isRunning = false;                                                                                  //
            }                                                                                                       //
            else if (axisY[0] < 0)                                                                                  //
            {                                                                                                       //
                isRunning = false;                                                                                  //
            }                                                                                                       //
            else if (axisY[0] > boardHeight)                                                                        //
            {                                                                                                       //
                isRunning = false;                                                                                  //
            }                                                                                                       //
                                                                                                                    //
            return isRunning;                                                                                       //
        }                                                                                                           //
                                                                                                                    //
        public bool GameOver()                                                                                      //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
		    // Method				:	GameOver()
		    //
		    // Method parameters	:	Graphics g
		    //
		    // Method return		:	void
		    //
		    // Synopsis				:   This method show the Game Over screen and saves the score into a file.						
		    //							
		    // Modifications		:
		    //							Date			Developer				Notes
		    //							----			---------				-----
		    //							2022-11-21		Tiago   				none.
		    //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {                                                                                                           //
            CheckCollisions();                                                                                      //
            saveFile();                                                                                             //
            return !isRunning;                                                                                      //
        }                                                                                                           //
                                                                                                                    //
        public void saveFile()                                                                                      //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
		    // Method				:	SaveFile()
		    //
		    // Method parameters	:	none
		    //
		    // Method return		:	void
		    //
		    // Synopsis				:   This method saves the score into a file.						
		    //							
		    // Modifications		:
		    //							Date			Developer				Notes
		    //							----			---------				-----
		    //							2022-11-21		Tiago   				none.
		    //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {                                                                                                           //
            try                                                                                                     //
            {                                                                                                       //                
                StreamWriter sw = new StreamWriter("D:\\snakeLastScore_CSharp.txt");                                //Passes the filepath and filename to the StreamWriter Constructor.
                sw.WriteLine(foodScore);                                                                            //Writes a line of text
                sw.Close();                                                                                         //
            }                                                                                                       //
            catch (Exception e)                                                                                     //
            {                                                                                                       //
                Console.WriteLine("Exception: " + e.Message);                                                       //Catches the block to handle the exception.
            }                                                                                                       //
            finally                                                                                                 //
            {                                                                                                       //
                Console.WriteLine("Executing finally block.");                                                      //
            }                                                                                                       //
        }                                                                                                           //
                                                                                                                    //
        public void loadFile()                                                                                      //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
		    // Method				:	LoadFile()
		    //
		    // Method parameters	:	none
		    //
		    // Method return		:	void
		    //
		    // Synopsis				:   This method load the score into the game.						
		    //							
		    // Modifications		:
		    //							Date			Developer				Notes
		    //							----			---------				-----
		    //							2022-11-21		Tiago   				none.
		    //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {                                                                                                           //
            String line;                                                                                            //
            try                                                                                                     //
            {                                                                                                       //
                //Pass the file path and file name to the StreamReader constructor                                  //
                StreamReader sr = new StreamReader("D:\\snakeLastScore_CSharp.txt");                                //Passes the file path and file name to the StreamReader constructor.
                line = sr.ReadLine();                                                                               //Reads the first line of text.
                foodScore = Int32.Parse(line);                                                                      //Continues to read until you reach end of file.
                sr.Close();                                                                                         //
                Console.ReadLine();                                                                                 //
            }                                                                                                       //
            catch (Exception e)                                                                                     //
            {                                                                                                       //
                Console.WriteLine("Exception: " + e.Message);                                                       //
            }                                                                                                       //
            finally                                                                                                 //
            {                                                                                                       //
                Console.WriteLine("Executing finally block.");                                                      //
            }                                                                                                       //
        }                                                                                                           //
                                                                                                                    //
        public void ChangeDirection(Direction direction)                                                            //
            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
		    // Method				:	ChangeDirection()
		    //
		    // Method parameters	:	Direction direction
		    //
		    // Method return		:	void
		    //
		    // Synopsis				:   This method stores temporarily the direction.						
		    //							
		    // Modifications		:
		    //							Date			Developer				Notes
		    //							----			---------				-----
		    //							2022-11-21		Tiago   				none.
		    //
		    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        {                                                                                                           //
            Direction = direction;                                                                                  //
        }                                                                                                           //
        
        //GETTER METHOD                                                                                                            
        public int GetScore()                                                                                       //
        {                                                                                                           //
            return foodScore;                                                                                       //
        }
    }
}
