using ImageBitMapMoving.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ImageBitMapMoving.Helpers;

namespace ImageBitMapMoving
{
    public partial class Form1 : Form
    {
        public Form1 _form1;

        DirectionControl dc = new DirectionControl();
        Renderer render = new Renderer();
        Graphics g = null;
        Bitmap image = null;
        Bitmap bm = null;
        Random rand = new Random();
        Point p = new Point();
        Enum newDirectionEnum;
        public Form1()
        {
            InitializeComponent();
            //Inserted Code BEGIN=============
            CenterToScreen();
            SetStyle(ControlStyles.ResizeRedraw, true);

            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(600, 600);
            g = CreateGraphics();

            Timer animationTimer = new Timer();
            animationTimer.Interval = 50;
            animationTimer.Enabled = true;

            image = Resources.Bee_animation_1;

            bm = new Bitmap(image);
         

            p.X = rand.Next(1, 200);
            p.Y = rand.Next(1, 200);

            bm.MakeTransparent();
            g.DrawImage(bm, p.X, p.Y, 50, 50);
            //==========

            ImageBitMapMoving.Move[] enumValuesList = (ImageBitMapMoving.Move[])Enum.GetValues(typeof(ImageBitMapMoving.Move)).Cast<Move>();
                   
            int randVal = rand.Next(0, enumValuesList.Count());

            for (int i = 0; i < enumValuesList.Count(); i++ )
            {
                if(i==randVal)
                {
                    newDirectionEnum = enumValuesList[i];
                    break;
                }
            }

            //NOTE: The Timer will generate the movement
           
            //==EVENTS BEGIN=======
            animationTimer.Tick +=new EventHandler( animationTimer_Tick);
            this.timer1.Tick +=new EventHandler( timer1_Tick);
            this.Paint +=new PaintEventHandler( Form1_Paint);
            //==EVENTS END=========
            //Inserted Code END===============
        }
        int count = 0;
        void animationTimer_Tick(object sender, EventArgs e)
        {
            
            if (count < 6)
            {
                count++;
                bm = render.AnimateBees()[count];
                //This method forces the Parser to Jump to the Paint Event and do the painting 
                
                this.Refresh();
                //Refresh();
            }
             
           
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Blue, 5);
             e.Graphics.DrawRectangle(pen, 10, 10, 500, 500);

             Graphics g = e.Graphics;
           
             g.DrawImage(bm, p.X, p.Y, 50, 50);

             if (count == 5)
                 count = 0;
        }

       
        private void timer1_Tick(object sender, EventArgs e)
        {
            /*********
             * If we uncomment this block the speed of the flapping wings will
             * be the same as the speed of the movement of the Bee.
             * Because the timer is at a number of milliseconds for all the functions
            if (count < 6)
            {
                count++;
                bm = rend.AnimateBees()[count];
                //This method forces the Parser to Jump to the Paint Event and do the painting 
                Refresh();
            }
            **********/
            p = render.GenerateMovement(newDirectionEnum,p);
            this.Refresh();
            //Refresh();
            /*********BOUNDARIES*****************************
             We Constantly check the position of the Player
             In relation to the Boundaries.
             *a |---------------------------------|d
             *  |                                 |
             *  |                                 |
             *  |                                 |
             *  |                                 |
             *  |                                 |
             *  |                                 |
             * b|---------------------------------|c
             * Vector a=(x,y)
             * Vector b=(x,y+height)
             * Vector c=(x+width, y+height)
             * Vector d=(x+width, y) 
             * 
             * 
             * */

             dc = render.ControlBoundaries(p);

            if(dc.EnumDirectionToMove !=null && dc.PointLocationX !=0 && dc.PointLocationY !=0)
            {
                p.X = dc.PointLocationX;
                p.Y = dc.PointLocationY;
                newDirectionEnum = dc.EnumDirectionToMove;

            }
            
            this.Refresh();
            //Refresh();
        }


    }
}
