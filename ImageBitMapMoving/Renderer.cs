using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

//Extra Using Directives
using ImageBitMapMoving.Helpers;

namespace ImageBitMapMoving
{
    public class Renderer
    {       
       Random rand = new Random();
        public List<Bitmap> AnimateBees()
        {
            List<Bitmap> beesList = new List<Bitmap>();

            beesList.Add(ImageBitMapMoving.Properties.Resources.Bee_animation_1);
            beesList.Add(ImageBitMapMoving.Properties.Resources.Bee_animation_2);
            beesList.Add(ImageBitMapMoving.Properties.Resources.Bee_animation_3);
            beesList.Add(ImageBitMapMoving.Properties.Resources.Bee_animation_4);
            beesList.Add(ImageBitMapMoving.Properties.Resources.Bee_animation_3);
            beesList.Add(ImageBitMapMoving.Properties.Resources.Bee_animation_2);
      
            return beesList;
        }



        public Point GenerateMovement(Enum directionToMove,  Point p )
        {

            if (directionToMove.ToString().Equals("Up"))
            {
                p.Y = p.Y - 1;
                return p;             
            }
            if (directionToMove.ToString().Equals("Down"))
            {
                p.Y = p.Y + 1;
                return p;            
            }

            if (directionToMove.ToString().Equals("Left"))
            {
                p.X = p.X - 1;
                return p;
            }

            if (directionToMove.ToString().Equals("Right"))
            {
                p.X = p.X + 1;
                return p;
            }

            if (directionToMove.ToString().Equals("Degrees45DiagonalUpRight"))
            {
                p.X = p.X + 1;
                p.Y = p.Y - 1;
                return p;

            }
            if (directionToMove.ToString().Equals("Degrees45DiagonalUpLeft"))
            {
                p.X = p.X - 1;
                p.Y = p.Y - 1;
                return p;
            }
            if (directionToMove.ToString().Equals("Degrees45DiagonalDownRight"))
            {
                p.X = p.X + 1;
                p.Y = p.Y + 1;
                return p;
            }
            if (directionToMove.ToString().Equals("Degrees45DiagonalDownLeft"))
            {
                p.X = p.X - 1;
                p.Y = p.Y + 1;
                return p;
            }

            return p;
        }


        public DirectionControl ControlBoundaries(  Point p)
        {
            DirectionControl dc = new DirectionControl();
            
            //Case: Image Hits Top Limit
            //We are at the Top corner => We Move Back Down 
            if (p.Y < 10)
            {
                p.Y = p.Y + 5;

                dc.PointLocationY = p.Y;
                dc.PointLocationX = p.X;             
                //We Change Direction and We Indicate the Image hit the top
               dc.EnumDirectionToMove= ChangeDirection(ImageBitMapMoving.Move.Up);

               return dc;
            }
            //Case: Image Hits Bottom Limit
            //We are at the Bottom corner => We Move Back Up 
            if (p.Y > 450)
            {
                p.Y = p.Y - 5;
                dc.PointLocationY = p.Y;
                dc.PointLocationX = p.X;
                //We Change Direction and We Indicate the Image hit the bottom
                dc.EnumDirectionToMove = ChangeDirection(ImageBitMapMoving.Move.Down);
                return dc;
            }

            //Case: Image Hits Left Limit
            //We are at the left corner => We Move Back right
            if (p.X < 10)
            {
                p.X = p.X + 5;
                dc.PointLocationY = p.Y;
                dc.PointLocationX = p.X;              
                //We Change Direction and We Indicate the Image hit the left
                 dc.EnumDirectionToMove = ChangeDirection(ImageBitMapMoving.Move.Left);
                 return dc;
            }
            //Case: Image Hits Right Limit
            //We are at the right corner => We Move Back left
            if (p.X > 450)
            {
                p.X = p.X - 5;
                dc.PointLocationY = p.Y;
                dc.PointLocationX = p.X;          
                //We Change Direction and We Indicate the Image hit the right
                  dc.EnumDirectionToMove = ChangeDirection(ImageBitMapMoving.Move.Right);
                  return dc;
            }
            return dc;
        }



        private Enum ChangeDirection(Enum newDir)
        {
            //The new Direction Can contain the KEYWORKDS: UP, DOWN, LEFT or RIGHT
            Enum newDirectionEnum = newDir ;

            List<Enum> listDirection = new List<Enum>();
            var enumValuesList = Enum.GetValues(typeof(ImageBitMapMoving.Move)).Cast<Move>();
            foreach (var val in enumValuesList)
            {
                listDirection.Add(val);
            }
            for (int i = listDirection.Count() - 1; i >= 0; i--)
            {
                //We eliminate every Direction Enum that is related to the Initial
                //direction, leaving only the Opposite enums that refer to opposite
                //directions.
                if (listDirection[i].ToString().Contains(newDir.ToString()))
                {
                    listDirection.Remove(listDirection[i]);

                }
            }

            //With the Remaining list of Enum, we shuffle again to find another direction
            //to go.
            int result = rand.Next(0, listDirection.Count);


            if (listDirection[result].ToString().Equals("Up")) { newDirectionEnum = ImageBitMapMoving.Move.Up; }
            if (listDirection[result].ToString().Equals("Down")) { newDirectionEnum = ImageBitMapMoving.Move.Down; }
            if (listDirection[result].ToString().Equals("Left")) { newDirectionEnum = ImageBitMapMoving.Move.Left; }
            if (listDirection[result].ToString().Equals("Right")) { newDirectionEnum = ImageBitMapMoving.Move.Right; }


            if (listDirection[result].ToString().Equals("Degrees45DiagonalUpRight")) { newDirectionEnum = ImageBitMapMoving.Move.Degrees45DiagonalUpRight; }
            if (listDirection[result].ToString().Equals("Degrees45DiagonalUpLeft")) { newDirectionEnum = ImageBitMapMoving.Move.Degrees45DiagonalUpLeft; }
            if (listDirection[result].ToString().Equals("Degrees45DiagonalDownRight")) { newDirectionEnum = ImageBitMapMoving.Move.Degrees45DiagonalDownRight; }
            if (listDirection[result].ToString().Equals("Degrees45DiagonalDownLeft")) { newDirectionEnum = ImageBitMapMoving.Move.Degrees45DiagonalDownLeft; }

            return newDirectionEnum;
        }





    }


}
