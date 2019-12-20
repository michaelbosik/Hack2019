using System;

namespace Enums {
    public enum SpriteNames {
        Astronaut, AlienDrone, Bullet, MedKit, Laser, RapidFire, Shotgun, Bounce
    }

    static class SpriteNamesMethods {
        public static string GetString(this SpriteNames spriteName) {
            if (spriteName == SpriteNames.Astronaut) {
                return spriteName.ToString();
            }
            return spriteName + "(Clone)";
        }
    }
}