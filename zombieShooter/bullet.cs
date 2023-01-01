using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// allow access to utilizing windows components (times, picture boxes, and their properties)

namespace zombieShooter
{
    internal class bullet
    {
        /**
         * bullet variables
         */
        // string: direction
        public string direction;

        // integer: speed, defaulted 20
        public int speed = 20;

        // create picture box component
        PictureBox Bullet = new PictureBox();

        // Timer: new timer for bullet
        System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();

        // integer: left bullet
        public int bulletLeft;

        // integer: top bullet
        public int bulletTop;

        // mkBullet: add bullet to game player, required to be called from main class
        public void mkBullet(Form form)
        {
            // set color white for bullet
            Bullet.BackColor = System.Drawing.Color.White; 

            // set size bullet to 5 pixel by 5 pixel
            Bullet.Size = new Size(5, 5); 
            
            // set tag to bullet
            Bullet.Tag = "bullet";

            // set bullet left
            Bullet.Left = bulletLeft;

            // set bullet right
            Bullet.Top = bulletTop;

            // bring bullet to front of other objects
            Bullet.BringToFront();

            // add bullet to screen
            form.Controls.Add(Bullet);

            // set timer interval to speed
            tm.Interval = speed;

            // assign timer with an event
            tm.Tick += new EventHandler(tm_Tick);

            // start timer
            tm.Start();
        }

        // tm_Tick: set timer for bullet
        public void tm_Tick(object sender, EventArgs e)
        {
            // if direction equals to left
            if(direction == "left")
            {
                // move bullet towards left of screen
                Bullet.Left -= speed;
            }

            // if direction equals right
            if(direction == "right")
            {
                // move bullet towards right of screen
                Bullet.Left += speed;
            }

            // if direction is up
            if(direction == "up")
            {
                // move bullet towards top of screen
                Bullet.Top -= speed;
            }

            // if direction is down
            if(direction == "down")
            {
                // move bullet bottom of screen
                Bullet.Top += speed;
            }

            // if bullet is less than 16 pixel to left or...
            // if bullet is more than 860 pixels to right or...
            // if bullet is 10 pixels from top or...
            // if bullet is 616 pixels to bottom
            if(Bullet.Left < 16 || Bullet.Left > 860 || Bullet.Top < 10 || Bullet.Top > 616)
            {
                // stop the timer
                tm.Stop();

                // dispose timer event and component from program
                tm.Dispose();

                // dispose bullet
                Bullet.Dispose();

                // nullify timer object
                tm = null;

                // nullify bullet objects
                Bullet = null;
            }
        }
    }
}
