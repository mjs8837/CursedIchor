using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LevelGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void levelWidth_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void numEnemies_TextChanged(object sender, EventArgs e)
        {

        }

        private void levelName_TextChanged(object sender, EventArgs e)
        {

        }

        private void createLevel_Click(object sender, EventArgs e)
        {
            textLevelCreation();
        }

        private void textLevelCreation()
        {Random rng = new Random();

            StreamWriter output = null;
            string lvlName = null;
            int spawnEnemy = int.Parse(numEnemies.Text);

            lvlName = "..\\..\\..\\..\\Levels\\" + levelName.Text; //text file's name

            int height = 8;
            int width = int.Parse(levelWidth.Text);
            

            char[,] grid = new char[width, height]; //imaginary grid for where things are placed

            //characters that represents the object in space
            char empty = '-';   //represents nothing
            char enemy = 'X';   //represents an enemy
            char player = '@';  //represents the player 
            char door = 'D';    //represents the door

            //fills the grid with empty lines 
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grid[j, i] = empty;
                }
            }

            //loop that keeps going until all enemies are spawned on the grid
            while (spawnEnemy > 0)
            {
                int x = rng.Next(4, width - 4);   //placement of enemies in the x axis 
                                                  //can't spawn too close to the left because that is where the player is spawned
                                                  //can't spawn too close to the right because that is the door to the next level
                int y = rng.Next(0, height - 1);  //placement of enemies in the y axis 

                foreach(char element in grid)
                {
                    if(grid[x,y] != enemy) //checks to see if the current grid space is taken 
                    {
                        grid[x, y] = enemy; //puts an enemy on the grid
                        spawnEnemy--; //takes 1 enemy off the amount needed to spawn for the while loop
                    }
                }
            }

            //places the player on the grid
            grid[1, height / 2] = player;

            //places the door on the grid
            grid[width - 2, 3] = door;


            try
            {
                output = new StreamWriter(lvlName);

                //writes the dimension of the grid
                output.WriteLine(width + "," + height);

                //nested for loop to create the level
                for(int i = 0; i < height; i++)
                {
                    for(int j = 0; j < width; j++)
                    {
                        output.Write(grid[j, i]);
                    }
                    output.WriteLine();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if(output != null)
            {
                output.Close();
            }
        }
    }
}
