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

    public class CentepedeSegment : ObjectInGame
    {
        public SegmentType segmentType;
        public CentepedeSegment nextSegment;
        public bool hit;

        public int xDirection;
        public int yDirection;


        public float pixelsToMoveEverySecond = 300f / 1000f;

        public void initialize(int x, int y, int width, int height, SegmentType st)
        {
            base.initialize(x, y, width, height);

            segmentType = st;
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

        public override void update(GameTime gameTime)
        {
            int prevX = x;
            int prevY = y;

            move(xDirection, 0,gameTime, pixelsToMoveEverySecond);

        }

        public static void update(GameTime gameTime, List<CentepedeSegment> segments) {
            foreach (CentepedeSegment segment in segments)
            {
                segment.update(gameTime);
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
                    startX += segmentWidth + 1;
                }

            }

            return centepede;
        }

    }
}
