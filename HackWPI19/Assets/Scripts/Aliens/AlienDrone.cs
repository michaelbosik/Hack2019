namespace Aliens {
    public class AlienDrone : Alien {
        private const int deathPoints = 1;
        private const float alienVolume = 1f;

        protected override int getDeathPoints() {
            return deathPoints;
        }

        protected override void deathNoise() {
            alienManager.deathNoise(alienVolume);
        }
    }
}