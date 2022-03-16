
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using CS5410.CentepedeGame.ObjectsInGame;

namespace CS5410.CentepedeGame
{
    public enum collisionType { 
        ScreenTop,
        ScreenBottom,
        ScreenLeft,
        ScreenRight,
        PlayerMovermentTop,
    }



    public class Collider
    {
        GameModel m_model;

        Dictionary<collisionType, Func<ObjectInGame, bool>> collsionFunctionPairs; 

        public void initialize(GameModel gameModel)
        {
            m_model = gameModel;
            collsionFunctionPairs = new Dictionary<collisionType, Func<ObjectInGame, bool>>();
            collsionFunctionPairs.Add(collisionType.ScreenTop, ScreenTopCollision);
            collsionFunctionPairs.Add(collisionType.ScreenBottom, ScreenBottomCollision);
            collsionFunctionPairs.Add(collisionType.ScreenLeft, ScreenLeftCollision);
            collsionFunctionPairs.Add(collisionType.ScreenRight, ScreenRightCollision); 
        }

        public List<collisionType> screenBoundaryCollision(ObjectInGame toCheck) { 
            return checkCollision(toCheck, new List<collisionType> { collisionType.ScreenTop,collisionType.ScreenBottom,collisionType.ScreenLeft,collisionType.ScreenRight});
        }

        public List<collisionType> checkCollision(ObjectInGame toCheck, List<collisionType> collisionsToCheck) { 
            List<collisionType> collisions = new List<collisionType>();

            //calls associated function for each collision type
            foreach (collisionType collisionType in collisionsToCheck) {
                bool result = collsionFunctionPairs[collisionType](toCheck);
                if (result) { 
                    collisions.Add(collisionType);
                }
            }

            return collisions;
        }

        private bool ScreenTopCollision(ObjectInGame toCheck) {
            Rectangle collsionRect = toCheck.getBoundingBox();
            if (collsionRect.Y < 0) return true;
            return false;
        }
        private bool ScreenBottomCollision(ObjectInGame toCheck)
        {
            Rectangle collsionRect = toCheck.getBoundingBox();
            if(collsionRect.Y + collsionRect.Height > m_model.heightResolutionScaler) return true;
            return false;
        }
        private bool ScreenLeftCollision(ObjectInGame toCheck)
        {
            Rectangle collsionRect = toCheck.getBoundingBox();
            if (collsionRect.X < 0) return true;
            return false;
        }
        private bool ScreenRightCollision(ObjectInGame toCheck)
        {
            Rectangle collsionRect = toCheck.getBoundingBox();
            if (collsionRect.X + collsionRect.Width > m_model.widthResolutionScaler) return true;
            return false;
        }

        private bool collsion(ObjectInGame a, ObjectInGame b) {
            //if they arent the same object, collision can occur
            if (a != b) { 
                
            }

            return false;
        }

    }
}