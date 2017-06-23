using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BattleshipsLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single, ConcurrencyMode =ConcurrencyMode.Single)]
    public class BattleshipsService : IBattleships
    {
        const int MAX_PLAYERS = 2;
        const int GRID_SIZE = 12;
        int players = 0;
        int[,,] grid = new int[MAX_PLAYERS + 1, GRID_SIZE, GRID_SIZE];  // 0=empty, 1=boat, 2=hit
        int currentPlayer = 0 - MAX_PLAYERS;

        public int Attack(int player, int x, int y)
        { // 0=Miss, 1=Hit, 2=Destroyed, 3=Win
            if (currentPlayer == player || currentPlayer <= 0) return -1;
            Console.WriteLine("Player " + player + " playing");
            int retVal = 0;
            switch(grid[player,x,y])
            {
                case 0:
                    currentPlayer = currentPlayer + 1;
                    if (currentPlayer > MAX_PLAYERS) currentPlayer -= MAX_PLAYERS;
                    Console.WriteLine("Miss");
                    return retVal;
                case 1:
                    retVal = 1;
                    grid[player, x, y] = 2;
                    break;
                case 2:
                    retVal = 1;
                    break;
            }
            Console.WriteLine("Hit");
            if(checkVertical(1, player, x, y) && checkVertical(-1, player, x, y) && checkHorizontal(1, player, x, y) && checkHorizontal(-1, player, x, y))
            {
                retVal = 3;
                int i = 0;
                while (i < GRID_SIZE && retVal == 3)
                {
                    int j = 0;
                    while(j < GRID_SIZE && retVal == 3)
                    {
                        if (grid[player, i, j] == 1) retVal = 2;
                        j++;
                    }
                    i++;
                }
            }
            if(retVal == 3)
            {
                Console.WriteLine("Player " + player + " has won");
                // Reset the game
                players = 0;
                currentPlayer = 0 - MAX_PLAYERS;
            }
            return retVal;
        }

        public int CurrentTurn()
        {
            if (currentPlayer <= 0) return 0;
            return currentPlayer;
        }

        public void CreateGrid(int player, int[] grid)
        {
            if (currentPlayer >= 0) return;
            for(int i = 0; i < GRID_SIZE; i++)
            {
                for(int j = 0; j < GRID_SIZE; j++)
                {
                    this.grid[player, i, j] = grid[(i * GRID_SIZE) + j];
                }
            }
            currentPlayer++;
            if (currentPlayer == 0) currentPlayer = 1;
        }

        public int AddPlayer()
        {
            if (players < MAX_PLAYERS)
            {
                players++;
                return players;
            }
            return 0;
        }

        public Tuple<int, int[]> GameState()
        {
            int[] grids = new int[GRID_SIZE * GRID_SIZE * MAX_PLAYERS];
            for(int player = 1; player <= MAX_PLAYERS; player++)
            {
                for(int x = 0; x < GRID_SIZE; x++)
                {
                    for(int y = 0; y < GRID_SIZE; y++)
                    {
                        grids[((GRID_SIZE * GRID_SIZE) * (player - 1)) + (x * GRID_SIZE) + y] = grid[player, x, y];
                    }
                }
            }
            return new Tuple<int, int[]>(currentPlayer, grids);
        }

        private bool checkVertical(int direction, int player, int x, int y)
        {
            x += direction;
            if (x < 0 || x >= GRID_SIZE) return true;
            switch(grid[player, x, y])
            {
                case 0:
                    return true;
                case 2:
                    return checkVertical(direction, player, x, y);
                default:
                    return false;
            }

        }

        private bool checkHorizontal(int direction, int player, int x, int y)
        {
            y += direction;
            if (y < 0 || y >= GRID_SIZE) return true;
            switch (grid[player, x, y])
            {
                case 0:
                    return true;
                case 2:
                    return checkVertical(direction, player, x, y);
                default:
                    return false;
            }

        }
    }
}
