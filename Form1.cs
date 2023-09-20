using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Huetchenfahren
{
    public partial class Form1 : Form
    {
        int xRichtung = 0;
        int yRichtung = 0;
        int speedStreet = 10;
        int speed = 1;
        int lives = 3; 
        int timer = 0;
        int angle = 15;    //in Grad
        int conesPassed = 0;
        int conesToGo;
        int hitagain = 0;
        int timeMin = 0, timeSec = 0, timeHun = 0;
        int finishDistance;
        int numberCones;
        int xPixel = 0;
        int ypixel = 0;

        public Form1()
        {
            InitializeComponent();
            lblgameover.Visible = false;
            lblFinalTime.Visible = false;

        }
        private void rbtnLength_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLength.Checked)
            {
                txtLength.Enabled = true;
                txtNumberCones.Enabled = false;
            }
        }
        private void rbtnNumberCones_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnNumberCones.Checked)
            {
                txtLength.Enabled = false;
                txtNumberCones.Enabled = true;
            }
        }
        private void rbtnEndless_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnEndless.Checked)
            {
                txtLength.Enabled = false;
                txtNumberCones.Enabled = false;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbtnLength.Checked)
                {
                    finishDistance = Convert.ToInt32(txtLength.Text);
                    panSettings.Enabled = false;
                    numberCones = 100000;
                    lblConesPassed.Visible = false;
                }

                if (rbtnNumberCones.Checked)
                {
                    numberCones = Convert.ToInt32(txtNumberCones.Text);
                    panSettings.Enabled = false;
                    finishDistance = 100000;
                    lblFinish.Visible = false;
                    lblFinish.Visible = false;
                }

                if (rbtnEndless.Checked)
                {
                    finishline.Location = new Point(0, 700);
                    panSettings.Enabled = false;
                    lblConesPassed.Visible = false;
                    lblFinish.Visible = false;

                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Numbers Only!",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void keyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Oemcomma)
            {
               timLinks.Enabled = true;
               xPixel = car.Location.X - 70;
            }

            if (e.KeyCode == Keys.OemPeriod)
            {
                timRechts.Enabled = true;
                xPixel = car.Location.X + 70;
            }

            if (e.KeyCode == Keys.A)
            {
                if (speedStreet <= 50 && speed <= 10)
                {
                    speedStreet += 5;
                }

                timSpeedLineDecrease.Enabled = true;
            }

            if (e.KeyCode == Keys.Y)
            {
                if (speedStreet >= 5 && car.Top <= 840)
                {
                    speedStreet -= 1;
                }    
            }


            if (e.KeyCode == Keys.S && timer < 1)
            {

                timer = 4;
                timNitro.Enabled=true;
                timBoost.Enabled = true;
                ypixel = car.Location.Y - 80;
            }

            if (e.KeyCode == Keys.Space)
            {
                timStreet.Enabled = true;
                timStopwatch.Enabled = true;
                lblSpace.Visible = false;

                if(rbtnLength.Checked)
                {
                    lblFinish.Visible = true;
                    finishDistance = Convert.ToInt32(txtLength.Text);
                    panSettings.Enabled = false;
                    numberCones = 100000;
                    lblConesPassed.Visible = false;
                }

                if(rbtnNumberCones.Checked)
                {
                    lblConesPassed.Visible = true;
                    numberCones = Convert.ToInt32(txtNumberCones.Text);
                    panSettings.Enabled = false;
                    finishDistance = 100000;
                    lblFinish.Visible = false;
                   
                }

                if (rbtnEndless.Checked)
                {
                    finishline.Location = new Point(0, 700);
                    panSettings.Enabled = false;
                    lblConesPassed.Visible = false;
                    lblFinish.Visible = false;

                }

            }
        }
        private void timLinks_Tick(object sender, EventArgs e)
        {
            if (car.Left < 640 && car.Left > 10 && car.Top >= 100)
            {
                xRichtung = Convert.ToInt32(speedStreet * Math.Cos(angle * Math.PI / 180));
                yRichtung = Convert.ToInt32(speedStreet * Math.Sin(angle * Math.PI / 180));
                car.Left -= xRichtung;
                car.Top -= yRichtung;

            }
            if (car.Location.X <= xPixel)
            {
                timLinks.Enabled = false;
            }
        }
        private void timRechts_Tick(object sender, EventArgs e)
        {
            if (car.Left < 480 && car.Top >= 100)
            {
                xRichtung = Convert.ToInt32(speedStreet * Math.Cos(angle * Math.PI / 180)) * -1;
                yRichtung = Convert.ToInt32(speedStreet * Math.Sin(angle * Math.PI / 180));
                car.Left -= xRichtung;
                car.Top -= yRichtung;
            }

            if (car.Location.X >= xPixel)
            {
                timRechts.Enabled = false;
            }
        }
        private void timLives_Tick(object sender, EventArgs e)
        {
            gameover();
        }

        private void gameover()
        {
            if (car.Bounds.IntersectsWith(Huetchen1.Bounds) || car.Bounds.IntersectsWith(Huetchen2.Bounds))
            {
                if (hitagain <= 0)
                {
                    lives--;
                    speedStreet = speedStreet / 2;
                    hitagain = 2;
                }
            }
            if (((lives == 0) && (rbtnLength.Checked || rbtnNumberCones.Checked)) || (rbtnEndless.Checked && lives == 0))
            {
                timLives.Enabled = false;
                timStreet.Enabled = false;
                timStopwatch.Enabled = false;
                lblgameover.Visible = true;

                if(rbtnEndless.Checked && lives == 0)
                {
                    lblFinalTime.Visible = true;
                    lblFinalTime.Text = "Final Time: " + timeMin + "min." + timeSec + "sec." + timeHun + "cs.";
                    lblgameover.Visible = false;
                }

                DialogResult over = MessageBox.Show("Play Again?", "GAME OVER", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                switch (over)
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        car.Location = new Point(240, 500);
                        Huetchen1.Location = new Point(120, 135);
                        Huetchen2.Location = new Point(350, 50);
                        startline.Location = new Point(27, 446);
                        finishline.Location = new Point(30, -100);
                        lives = 3;
                        conesPassed = 0;
                        timeHun = 0;
                        timeSec = 0;
                        timeMin = 0;
                        timLives.Enabled = true;
                        lblFinalTime.Visible = false;
                        lblSpace.Visible = true;
                        speedStreet = 10;
                        panSettings.Enabled = true;
                        lblgameover.Visible = false;
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        this.Close();
                        break;
                }

            }

            switch (lives)
            {
                case 3:
                    Herz1.Visible = true;
                    Herz2.Visible = true;
                    Herz3.Visible = true;
                    break;
                case 2:
                    Herz3.Visible = false;
                    break;
                case 1:
                    Herz2.Visible = false;
                    break;
                case 0:
                    Herz1.Visible = false;
                    break;
            }
        }
        private void winner()
        {
            if (car.Bounds.IntersectsWith(finishline.Bounds))
                {
                timStreet.Enabled = false;
                timStopwatch.Enabled = false;
                lblFinalTime.Visible = true;

                lblFinalTime.Text = "Final Time: "+ timeMin + "min." + timeSec + "sec." + timeHun + "cs.";
                DialogResult finish = MessageBox.Show("Final Time: " + timeMin + "min " + timeSec + "sec " + timeHun + "cs " + "\n\n" +"Play Again?" , "Winner", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            
                switch (finish)
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        car.Location = new Point(240, 500);
                        finishline.Location = new Point(30, -100);
                        startline.Location = new Point(27, 446);
                        Huetchen1.Location = new Point(120, 135);
                        Huetchen2.Location = new Point(350, 50);
                        lives = 3;
                        timeHun = 0;
                        timeSec = 0;
                        timeMin = 0;
                        conesPassed = 0;
                        lblFinalTime.Visible = false;
                        lblSpace.Visible = true;
                        speedStreet = 10;
                        lblgameover.Visible = false;
                        panSettings.Enabled = true;
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        this.Close();
                        break;
                }

            }
        }

        private void timNitro_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = "Nitro in: " + timer.ToString();
            timer--;
            if (timer < 0)
            {
                timNitro.Enabled = false;
            }

        }

        private void timSpeedLineDecrease_Tick(object sender, EventArgs e)
        {
            if (speedStreet >= 5 && car.Top <= 840)
            {
                speedStreet-= 2;
            }
            else
            {
                timSpeedLineDecrease.Enabled = false;
            }

        }

        private void timStreet_Tick(object sender, EventArgs e)
        {
            int x, y;
            Random r = new Random();
            startline.Top += speedStreet;
            winner();
           

            if (Huetchen1.Top >= 600)
            {
                x = r.Next(37, 230);
                y = r.Next(-200, 0);
                Huetchen1.Location = new Point(x, y);
                conesPassed++;
            }
            else
            {
                Huetchen1.Top += speedStreet;
            }

            if (Huetchen2.Top >= 600)
            {
                x = r.Next(360, 470);
                y = r.Next(-200, 0);
                Huetchen2.Location = new Point(x, y);
                conesPassed++;
            }
            else
            {
                Huetchen2.Top += speedStreet;
            }

            if (pictureBox3.Top >= 625)
            {
                pictureBox3.Top = -290;
            }
            else
            {
                pictureBox3.Top += speedStreet;
            }
            
            if (pictureBox2.Top >= 625)
            {
                pictureBox2.Top = -290;
            }
            else
            {
                pictureBox2.Top += speedStreet;
            }

            if (pictureBox1.Top >= 625)
            {
                pictureBox1.Top = -290;
            }
            else
            {
                pictureBox1.Top += speedStreet;
            }

            if (pictureBox4.Top >= 625)
            {
                pictureBox4.Top = -290;
            }
            else
            {
                pictureBox4.Top += speedStreet;
            }

            if (speedStreet >= 5 && car.Top <= 500)
            {
                car.Top += 3;
            }

            conesToGo =  numberCones - conesPassed;

            if(numberCones>=conesPassed)
            {
                lblConesPassed.Text = "Cones to go: " + conesToGo.ToString();
            }

            if (finishDistance >= 0 || conesPassed != numberCones)
            {
                finishDistance -= speedStreet;

            }

            if (finishDistance <= 0 || conesPassed >= numberCones)
            {
                finishline.Top += speedStreet;
                lblFinish.Visible = false;
            }

            lblFinish.Text = "Finish in:" + finishDistance + "m";
           
        }

        private void timCollision_Tick(object sender, EventArgs e)
        {
            hitagain--;
        }      

     
        private void timBoost_Tick(object sender, EventArgs e)
        {
            car.Top -= 10;

            if (car.Location.Y <= ypixel || car.Top <= 100)
            {
                timBoost.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timStopwatch_Tick(object sender, EventArgs e)
        {
            stopwatch();
            if (timStopwatch.Enabled)
            {
                timeHun++;

                if (timeHun >= 100)
                {
                    timeSec++;
                    timeHun = 0;

                    if (timeSec >= 60)
                    {
                        timeMin++;
                        timeSec = 0;
                    }
                }
            }

        }
    
        private void stopwatch()
        {
            lblHun.Text = String.Format(".{0:00}", timeHun);
            lblSec.Text = String.Format(":{0:00}", timeSec);
            lblMin.Text = String.Format("{0:00}", timeMin);
        }
    }
}
