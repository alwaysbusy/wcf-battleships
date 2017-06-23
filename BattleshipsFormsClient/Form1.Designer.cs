namespace BattleshipsFormsClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnJoin = new System.Windows.Forms.Button();
            this.grpPlayer = new System.Windows.Forms.GroupBox();
            this.grpOpponent = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.tmrGame = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(12, 12);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(75, 23);
            this.btnJoin.TabIndex = 0;
            this.btnJoin.Text = "Join Game";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // grpPlayer
            // 
            this.grpPlayer.Location = new System.Drawing.Point(12, 41);
            this.grpPlayer.Name = "grpPlayer";
            this.grpPlayer.Size = new System.Drawing.Size(252, 265);
            this.grpPlayer.TabIndex = 1;
            this.grpPlayer.TabStop = false;
            this.grpPlayer.Text = "You";
            // 
            // grpOpponent
            // 
            this.grpOpponent.Location = new System.Drawing.Point(270, 41);
            this.grpOpponent.Name = "grpOpponent";
            this.grpOpponent.Size = new System.Drawing.Size(252, 265);
            this.grpOpponent.TabIndex = 2;
            this.grpOpponent.TabStop = false;
            this.grpOpponent.Text = "Opponent";
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(93, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start Game";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tmrGame
            // 
            this.tmrGame.Interval = 5000;
            this.tmrGame.Tick += new System.EventHandler(this.tmrGame_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 318);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.grpOpponent);
            this.Controls.Add(this.grpPlayer);
            this.Controls.Add(this.btnJoin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Battleships";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.GroupBox grpPlayer;
        private System.Windows.Forms.GroupBox grpOpponent;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer tmrGame;
    }
}

