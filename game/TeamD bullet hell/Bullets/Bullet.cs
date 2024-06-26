﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TeamD_bullet_hell
{
    internal class Bullet
    {
        //screen size
        private int windowHeight;
        private int windowWidth;

        private Texture2D textureOfBullet;

        //the size and the postion 
        private Rectangle positionAndSize;

        //original for reuse
        private Rectangle positionOG;

        private double velocity;
        private float directionInDegrees;

        private double spawnTime;
        private double spawnTimer;


        //when shouldRemove = true remove the bullet
        private bool upDateTheBall;
        private bool outScreen;

        // direction represented by angle in radians
        private double angleInRadians = 0;

        //get button position and size reference
        public Rectangle Position
        {
            get { return positionAndSize; }
            set { positionAndSize = value; }
        }

        //get the original position
        public Rectangle OGPosition
        {
            get
            {
                return positionOG;
            }
        }
        
        //allow the bullet MAnager to check when to reset
        public bool UpDateTheBall { get { return upDateTheBall; } }
        public bool OutScreen { get { return outScreen; } }


        /// <summary>
        /// Pls use float for the direction because the method that convert degree into radius only take in float
        /// </summary>
        /// <param name="positionAndSize"></param>
        /// <param name="textureOfBullet"></param>
        /// <param name="velocity"></param>
        /// <param name="directionInDegrees"></param>
        /// <param name="windowHeight"></param>
        /// <param name="windowWidth"></param>
        public Bullet(float directionInDegrees, Rectangle positionAndSize, Texture2D textureOfBullet, double velocity,double spawnTime, int windowWidth, int windowHeight)
        {

            //Commenting all of this out in order to try putting the file IO in the constructor
            //It can be changed later if need be - Jarin 
            this.directionInDegrees = directionInDegrees;

            this.positionAndSize = positionAndSize;
            this.positionOG = new Rectangle (positionAndSize.X, positionAndSize.Y, positionAndSize.Width, positionAndSize.Height);

            this.textureOfBullet = textureOfBullet;
            this.velocity = velocity;
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
            this.spawnTime = spawnTime;
            

            //spawntimer will be set to time of how many time the player enter the game


            //convert the angle to radius for vector math NOOOOOOO-------
            angleInRadians = MathHelper.ToRadians(directionInDegrees);

            //when this is true remove the bullet
            upDateTheBall = false;
            outScreen = false;
        }
        /// <summary>
        /// reset the bulle
        /// </summary>
        public void Reset()
        {
            upDateTheBall = false;
            outScreen = false;
            this.positionAndSize =new Rectangle( positionOG.X, positionOG.Y, positionOG.Width, positionOG.Height);
        }

        public void Update(float currentGameTime)
        {
            spawnTimer = currentGameTime;

            // whe it reach the spawn time and the bullet is in the screen
            if (upDateTheBall== false&& outScreen == false)
            {
                //when reach the spawn time 
                if(spawnTimer>= spawnTime)
                {
                    upDateTheBall = true;
                }
            }
            else if (upDateTheBall == true)
            {
                Vector2 velocityVector = new Vector2((float)(velocity * Math.Cos(angleInRadians)), (float)(velocity * Math.Sin(angleInRadians)));

                //change the position over the time depend on the speed
                positionAndSize.X += (int)(velocityVector.X);
                positionAndSize.Y += (int)(velocityVector.Y);


                //mark the bullet to be removed if it move out side the screen
                if (positionAndSize.X < -300 || (positionAndSize.X + positionAndSize.Width) > windowWidth ||
                    positionAndSize.Y < -300 || (positionAndSize.Y + positionAndSize.Height) > windowWidth)
                {
                    outScreen = true;
                    upDateTheBall = false;

                    this.positionAndSize = new Rectangle(this.positionOG.X, this.positionOG.Y, this.positionOG.Width, this.positionOG.Height);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //only draw the ball when is update is ture
            
            if (upDateTheBall)
            {
                spriteBatch.Draw(textureOfBullet, positionAndSize, Color.White);
            }
           
        }

        
    }
}
