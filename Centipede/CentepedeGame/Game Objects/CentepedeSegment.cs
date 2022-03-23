using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame.ObjectsInGame
{
    public enum SegmentType { 
        Head,
        Body,
    }

    public enum CentepedeMode { 
        Normal,
        Poisoned
    }

    public class CentepedeSegment : ObjectInGame
    {
        public SegmentType segmentType;
        public CentepedeMode segmentMode;
        public CentepedeSegment nextSegment;
        public bool hit;

        public int xDirection;
        public int yDirection;


        public float pixelsToMoveEverySecond = 300f / 1000f;

        public void initialize(int x, int y, int width, int height, SegmentType st)
        {
            base.initialize(x, y, width, height);

            segmentType = st;
            segmentMode = CentepedeMode.Normal;
            nextSegment = null;
            hit = false;

            xDirection = -1;
            yDirection = 1;
        }

        //when split is called, sets the next segment to be of type head (game model should then replace this segment with a mushroom)
        public void split() {
            if (nextSegment != null)
            {
                nextSegment.segmentType = SegmentType.Head;
            }
        }

        public override void update(GameTime gameTime, Collider c)
        {
            if (!bulletCollision(gameTime, c))
            {
                if (segmentMode == CentepedeMode.Normal)
                {
                    moveCollisionOne(gameTime, c);
                }
                else
                {
                    moveCollisionTwo(gameTime, c); ;
                }
            }
            else { 
                this.hit = true;
            }

        }


        //first pass collsion and movement
        private bool moveCollisionOne( GameTime gameTime, Collider c) {
            int prevX = x;
            int prevY = y;
            move(xDirection, 0, gameTime, pixelsToMoveEverySecond);

            List<collisionType> boundaryCollisions = c.screenBoundaryCollision(this);
            boundaryCollisions.AddRange(c.checkCollision(this, new List<collisionType>() { collisionType.Mushroom, collisionType.poison }));
            if (boundaryCollisions.Count > 0)
            {
                if (boundaryCollisions.Contains(collisionType.poison))
                {
                    segmentMode = CentepedeMode.Poisoned;
                    yDirection = 1;
                }
                else
                {
                    if (horizontalCollision(boundaryCollisions))
                    {
                        x = prevX;
                        move(xDirection, 0, gameTime, pixelsToMoveEverySecond);
                    }

                    if (verticalCollision(boundaryCollisions))
                    {
                        y = prevY;
                        y += yDirection * height;
                    }

                    y += yDirection * height;
                }
                return true;
            }

            return false;   
        }

        private bool moveCollisionTwo(GameTime gameTime, Collider c)
        {
            int prevX = x;
            int prevY = y;
            move(0, yDirection, gameTime, pixelsToMoveEverySecond);

            List<collisionType> boundaryCollisions = c.checkCollision(this, new List<collisionType>() { collisionType.ScreenBottom, collisionType.ScreenTop});
            if (verticalCollision(boundaryCollisions))
            {
                if (verticalCollision(boundaryCollisions))
                {
                    y = prevY;
                    y += yDirection * height;
                }

                return true;
            }

            return false;
        }

        private bool verticalCollision(List<collisionType> boundaryCollisions)
        {
            //if segment has collided with the top of the screen or the bottom of the screen, change y direction and set mode back to normal
            if (boundaryCollisions.Contains(collisionType.ScreenTop) || boundaryCollisions.Contains(collisionType.ScreenBottom))
            {
                segmentMode = CentepedeMode.Normal;

                if (boundaryCollisions.Contains(collisionType.ScreenTop)){
                    yDirection = 1;
                }

                if (boundaryCollisions.Contains(collisionType.ScreenBottom))
                {
                    yDirection = -1;
                }

                return true;
            }
            return false;
        }
        private bool horizontalCollision(List<collisionType> boundaryCollisions)
        {
            //if segment has collided with the left or right of the screen, change y direction and set mode back to normal
            if (boundaryCollisions.Contains(collisionType.ScreenLeft) || boundaryCollisions.Contains(collisionType.ScreenRight) || boundaryCollisions.Contains(collisionType.Mushroom))
            {
                xDirection *= -1;
                return true;
            }

            return false;
        }

        public static void update(GameTime gameTime, List<CentepedeSegment> segments, Collider c) {
            foreach (CentepedeSegment segment in segments)
            {
                segment.update(gameTime, c);
            }
        }

       public static List<CentepedeSegment> generateCentepede(int startX, int startY, int segmentWidth, int segmentHeight, int numberOfSegments) { 
            List<CentepedeSegment> centepede = new List<CentepedeSegment>();

            CentepedeSegment prevSegment = null;
            SegmentType segmentType = SegmentType.Head;

            for (int i = 0; i < numberOfSegments; i++) { 
                CentepedeSegment curSegment = new CentepedeSegment();
                curSegment.initialize(startX, startY, segmentWidth, segmentHeight, segmentType);

                centepede.Add(curSegment);

                //update loop variables
                {
                    //set the previous segments next in line segment to the current segment for splitting updating
                    if (prevSegment != null)
                    {
                        prevSegment.nextSegment = curSegment;
                    }
                    prevSegment = curSegment;

                    //only the first segment is of type head
                    if (segmentType == SegmentType.Head)
                    {
                        segmentType = SegmentType.Body;
                    }

                    //set the postions
                    startX += segmentWidth + 5;
                }

            }

            return centepede;
        }
    }
}
