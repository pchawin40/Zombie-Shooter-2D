namespace zombieShooter
{
    public partial class Form1 : Form
    {
        /*
         * game variables
         */
        bool goup; // boolean will be used for indicating player to go up the screen
        bool godown; // boolean will be used for indicating player to go down the screen
        bool goleft; // boolean will be used for indicating player to go left of the screen
        bool goright; // boolean will be used for indicating player to go right of the screen
        string facing = "up"; // used to guide bullets
        double playerHealth = 100; // double variable called player health
        int speed = 10; // integer for player's speed
        int ammo = 20; // integer for player's ammo at start of game
        int zombieSpeed = 2; // integer for zombie's speed
        int score = 0; // integer to hold player's score achieved through the game
        bool gameOver = false; // boolean used when game is finished (default as false)
        Random rnd = new Random(); // instance used to create randon number for game

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        // on key down event
        private void keyisdown(object sender, KeyEventArgs e)
        {
            // if game is over, do nothing
            if (gameOver) return;

            //? if left key is pressed, then...
            if(e.KeyCode == Keys.Left)
            {
                // change to go left
                goleft= true;

                // change facing to left
                facing = "left";

                // change player's image to left
                player.Image = Properties.Resources.left;
            }

            //? If right key is pressed, then...
            if(e.KeyCode == Keys.Right)
            {
                // change to go right
                goright = true;

                // change facing to right
                facing = "right";

                // change player's image to right
                player.Image = Properties.Resources.right;
            }

            //? If up key is pressed, then...
            if(e.KeyCode == Keys.Up)
            {
                // change to go up
                goup = true;

                // change facing to up
                facing = "up";

                // change player's image to up
                player.Image = Properties.Resources.up;
            }

            // if down key is pressed, then...
            if(e.KeyCode == Keys.Down)
            {
                // change to go down
                godown = true;

                // change facing to go down
                facing = "down";

                // change player's image to down
                player.Image = Properties.Resources.down;
            }
        }

        // on key up event
        private void keyisup(object sender, KeyEventArgs e)
        {
            // if game is over, do nothing
            if (gameOver) return;

            // if left key is up...
            if(e.KeyCode == Keys.Left)
            {
                // change go left to false
                goleft = false;
            }

            // if right key is right...
            if(e.KeyCode == Keys.Right)
            {
                // change go right to false
                goright = false;
            }

            // if up key is up...
            if(e.KeyCode == Keys.Up)
            {
                // change go up to false
                goup = false;
            }

            // if down key is up
            if(e.KeyCode == Keys.Down)
            {
                // change go down to false
                godown = false;
            }

            // if space key is up and ammo is greater than 0... 
            if(e.KeyCode == Keys.Space && ammo > 0)
            {
                // reduce ammo by 1 from total number
                ammo--;

                // invoke shoot function with passing facing string
                shoot(facing);

                // if ammo is less than 1
                if(ammo < 1)
                {
                    // invoke drop ammo function
                    DropAmmo();
                }
            }
        }

        // engine to run game
        private void gameEngine(object sender, EventArgs e)
        {
            // if player health is greater than 1...
            if(playerHealth > 1)
            {
                // assign progress bar to palyer's health integer
                progressBar1.Value = Convert.ToInt32(playerHealth);
            } 
            else
            {
                // if player health is below 1
                // show player's dead image
                player.Image = Properties.Resources.dead;
                
                // stop timer
                timer1.Stop();

                // change game over to true
                gameOver = true;
            }

            label1.Text = "   Ammo:  " + ammo; // show ammo amount on label 1
            label2.Text = "Kills: " + score; // show total kills on score

            // if player health is less than 20
            if(playerHealth < 20)
            {
                // change progress bar colours to red
                progressBar1.ForeColor = System.Drawing.Color.Red;
            }

            // if goleft is true and player is headed left, move player to left
            if(goleft && player.Left > 0)
            {
                player.Left -= speed;
            }
            
            // if moving right is true and player left + player width is less than 930 pixels
            if(goright && player.Left + player.Width < 1700)
            {
                // move player to right
                player.Left += speed;
            }

            if(goup && player.Top > 85)
            {
                // if moving top is true and player is 60 pixel more from top, move player to up
                player.Top -= speed;
            }

            // if moving down is true and player top and player height is less than 700 pixels
            // move player to down
            if(godown && player.Top + player.Height < 1400)
            {
                player.Top += speed;
            }

            // run first for each loop: x is a control and search for all controls in the loop
            foreach(Control x in this.Controls)
            {
                // if X is a picture box and X has a tag AMMO
                if(x is PictureBox && x.Tag == "ammo")
                {
                    if(x is PictureBox && x.Tag == "ammo")
                    {
                        // check X in hitting player picture box
                        // once player picks up ammo...
                        if(((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                        {
                            // remove ammo picture box
                            this.Controls.Remove(((PictureBox)x));

                            // dispose picture box completely from program
                            ((PictureBox)x).Dispose();
                            // add 5 ammo to integer
                            ammo += 5;
                        }
                    }
                }

                // if bullets hits 4 border of game and x is a picture box and has tag of bullet
                if(x is PictureBox && x.Tag == "bullet")
                {
                    // if bullet is less than 1 or...
                    // if bullet is more than 930 pixels to right or...
                    // if bullet is 10 pixels from top or...
                    // if bullet is 700 pixels to bottom
                    if(((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 930 || ((PictureBox)x).Top < 10 || ((PictureBox)x).Top > 700)
                    {
                        // remove bullets from display
                        this.Controls.Remove(((PictureBox)x));
                        // dispose bullet from program
                        ((PictureBox)x).Dispose();
                    }
                }

                // if player hits a zombie
                if(x is PictureBox && x.Tag == "zombie")
                {
                    // check bounds of player and zombie
                    if(((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        // if zombie hits player then decrease health by 1
                        playerHealth -= 1;
                    }

                    //? move zombie towards player picture box:
                    if(((PictureBox)x).Left > player.Left)
                    {
                        // move zombie towards left of player
                        ((PictureBox)x).Left -= zombieSpeed;
                        
                        // change zombie image to left
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }

                    if(((PictureBox)x).Top > player.Top)
                    {
                        // move zombie towards players top
                        ((PictureBox)x).Top -= zombieSpeed;

                        // change zombie picture to top pointing image
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }

                    if(((PictureBox)x).Left < player.Left)
                    {
                        // move zombie towards right of player
                        ((PictureBox)x).Left += zombieSpeed;

                        // change image to right image
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }

                    if(((PictureBox)x).Top < player.Top)
                    {
                        // move zombie towards bottom of player
                        ((PictureBox)x).Top += zombieSpeed;

                        // change image to down zombie
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }
                }

                // verify that zombie and bullet are different than each other and handle properly
                // when hitting each other's box
                foreach(Control j in this.Controls)
                {
                    if((j is PictureBox && j.Tag == "bullet") && (x is PictureBox && x.Tag == "zombie"))
                    {
                        // if statement that check if bullet hits zombie
                        if(x.Bounds.IntersectsWith(j.Bounds))
                        {
                            // increase kill score by 1
                            score++;

                            // remove bullet from screen
                            this.Controls.Remove(j);

                            // dispose bullet all together from program
                            j.Dispose();

                            // remove zombie from screen
                            this.Controls.Remove(x);

                            // dispose zombie from program
                            x.Dispose();

                            // invoke to make zombie functions to add another zombie to the game
                            makeZombies();
                        }
                    }
                }
            }
        }

        // Drop Ammo Function: used when user needs more ammo in the game
        private void DropAmmo()
        {
            // create new instance of picture box
            PictureBox ammo = new PictureBox();

            // assign ammo image to picture box
            ammo.Image = Properties.Resources.ammo_Image;

            // set size to auto size
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;

            // set location to random left
            ammo.Left = rnd.Next(10, 890);

            // set location to random top
            ammo.Top = rnd.Next(50, 600);

            // set tag to ammo
            ammo.Tag = "ammo";

            // add ammo picture box to screen
            this.Controls.Add(ammo);

            // bring to front
            ammo.BringToFront();

            // bring player to front
            player.BringToFront();
        }

        // Shoot Function: used when player is shooting in the game
        private void shoot(string direct)
        {
            // create new instance of bullet class
            bullet shoot = new bullet();

            // assign direction to bullet
            shoot.direction = direct;

            // place bullet to left half of player
            shoot.bulletLeft = player.Left + (player.Width / 2);

            // player bullet to top half of player
            shoot.bulletTop = player.Top + (player.Height / 2);

            // run function mkBullet from bullet class
            shoot.mkBullet(this);
        }

        // Zombies Function: used when more zombies are needed
        private void makeZombies()
        {
            // create new picture box called zombie
            PictureBox zombie = new PictureBox();

            // add tag to it called zombie
            zombie.Tag = "zombie";

            // default picture for zombie is zdown
            zombie.Image = Properties.Resources.zdown;

            // generate number between 0 and 900 and assign to new zombies left
            zombie.Left = rnd.Next(0, 900);

            // generate number between 0 and 800 and assign to new zombies top
            zombie.Top = rnd.Next(0, 800);

            // set auto size for new picture box
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;

            // add picture box to screen
            this.Controls.Add(zombie);

            // bring player to front
            player.BringToFront();
        }
    }
}