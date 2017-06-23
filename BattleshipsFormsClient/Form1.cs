using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleshipsFormsClient.ServiceReference1;

namespace BattleshipsFormsClient
{
    public partial class Form1 : Form
    {
        BattleshipsClient client;
        const int GRID_SIZE = 12;
        int playerNum = 0;
        System.Windows.Forms.Button[] player;
        System.Windows.Forms.Button[] opponent;

        public Form1()
        {
            InitializeComponent();
            client = new BattleshipsClient();
        }

        ~Form1()
        {
            client.Close();
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            int res = client.AddPlayer();
            if (res != 0)
            {
                playerNum = res;
                startGameSetup();
            } else
            {
                MessageBox.Show("A game is already in progress, please try again later");
            }
        }

        private void startGameSetup()
        {
            btnJoin.Text = "Player " + playerNum;
            btnJoin.Enabled = false;

            player = new System.Windows.Forms.Button[GRID_SIZE * GRID_SIZE];
            for(int x = 0; x < GRID_SIZE; x++)
            {
                for (int y = 0; y < GRID_SIZE; y++)
                {
                    System.Windows.Forms.Button button = new System.Windows.Forms.Button();
                    button.Location = new System.Drawing.Point(6 + (20 * x), 19 + (20 * y));
                    button.Name = "playerButton_" + x + "_" + y;
                    button.Size = new System.Drawing.Size(20, 20);
                    button.TabIndex = 0;
                    button.Text = "";
                    button.UseVisualStyleBackColor = true;
                    button.Click += new System.EventHandler(this.playerPlace);
                    this.grpPlayer.Controls.Add(button);
                    player[(x * GRID_SIZE) + y] = button;
                }
            }

            opponent = new System.Windows.Forms.Button[GRID_SIZE * GRID_SIZE];
            for (int x = 0; x < GRID_SIZE; x++)
            {
                for (int y = 0; y < GRID_SIZE; y++)
                {
                    System.Windows.Forms.Button button = new System.Windows.Forms.Button();
                    button.Location = new System.Drawing.Point(6 + (20 * x), 19 + (20 * y));
                    button.Name = "opponentButton_" + x + "_" + y;
                    button.Size = new System.Drawing.Size(20, 20);
                    button.TabIndex = 0;
                    button.Text = "";
                    button.UseVisualStyleBackColor = true;
                    button.Enabled = false;
                    button.Click += new System.EventHandler(this.attackOpponent);
                    this.grpOpponent.Controls.Add(button);
                    opponent[(x * GRID_SIZE) + y] = button;
                }
            }
        }

        private void endGameSetup()
        {
            // Serialise where boats have been placed
            int[] grid = new int[GRID_SIZE * GRID_SIZE];
            for(int i = 0; i < GRID_SIZE * GRID_SIZE; i++)
            {
                if (player[i].BackColor == Color.Green)
                {
                    grid[i] = 1;
                } else
                {
                    grid[i] = 0;
                }
                player[i].Enabled = false;
            }

            client.CreateGrid(playerNum, grid);
        }

        private void playerPlace(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btnStart.Enabled = true;
            if (btn.BackColor != Color.Green)
            {
                btn.BackColor = Color.Green;
            }
            else
            {
                btn.BackColor = default(Color);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            endGameSetup();
            btnStart.Enabled = false;
            btnStart.Text = "Waiting";
            tmrGame.Start();
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            Tuple<int, int[]> res = client.GameState();
            int opponentOffset = 0, playerOffset = 0;
            if (playerNum == 2)
            {
                playerOffset = GRID_SIZE * GRID_SIZE;
            }
            else
            {
                opponentOffset = GRID_SIZE * GRID_SIZE;
            }

            for (int i = 0; i < GRID_SIZE * GRID_SIZE; i++)
            {
                switch (res.Item2[playerOffset + i])
                {
                    case 0:
                        player[i].BackColor = SystemColors.Control;
                        break;
                    case 1:
                        player[i].BackColor = Color.Green;
                        break;
                    case 2:
                        player[i].BackColor = Color.Yellow;
                        break;
                }
            }

            if (res.Item1 == playerNum)
            {
                // Our turn
                tmrGame.Stop();
                btnStart.Text = "Playing";

                for (int i = 0; i < GRID_SIZE * GRID_SIZE; i++)
                {
                    if(res.Item2[opponentOffset + i] != 2) opponent[i].Enabled = true;
                }
            } else if(res.Item1 > 0)
            {
                // Opponent turn
                btnStart.Text = "Opponent";
                for (int i = 0; i < GRID_SIZE * GRID_SIZE; i++) opponent[i].Enabled = false;
            } else
            {
                // Either not started or finished
                if(btnStart.Text != "Waiting")
                {
                    MessageBox.Show("Opponent has won");
                    endGame();
                }
            }
        }

        private void attackOpponent(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int x = Convert.ToInt32(btn.Name.Split('_')[1]), y = Convert.ToInt32(btn.Name.Split('_')[2]);
            int opponentNum = playerNum + 1;
            if (opponentNum > 2) opponentNum -= 2;
            int result = client.Attack(opponentNum, x, y);
            switch(result)
            {
                case 0:
                    btn.BackColor = Color.Black;
                    break;
                case 1:
                    btn.BackColor = Color.Yellow;
                    break;
                case 2:
                    btn.BackColor = Color.Red;
                    break;
                case 3:
                    btn.BackColor = Color.Red;
                    MessageBox.Show("Congratulations, you have won");
                    endGame();
                    return;
            }
            tmrGame.Start();
            tmrGame_Tick(null, null);
        }

        private void endGame()
        {
            for(int i = 0; i < GRID_SIZE * GRID_SIZE; i++)
            {
                grpPlayer.Controls.Remove(player[i]);
                player[i].Dispose();
                grpOpponent.Controls.Remove(opponent[i]);
                opponent[i].Dispose();
            }
            playerNum = 0;

            tmrGame.Stop();

            btnStart.Text = "Start Game";
            btnJoin.Text = "Join Game";
            btnJoin.Enabled = true;
        }
    }
}
;