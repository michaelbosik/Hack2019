using UnityEngine;

public class PowerUps : MonoBehaviour {
    // Unity objects
    public GameObject medkit_0;
    public GameObject shotgun_0;
    
    private static Player player;
    private static bool isShotgun;
    private static float timerShotgun;
    private static float durShotGun;

    void Start() {
        player = GameObject.Find("Astronaut").GetComponent<Player>();

        // Shotgun
        isShotgun = false;
        timerShotgun = 0;
        durShotGun = 0;
    }

    void Update() {
        float tDelta = Time.deltaTime;

        // Shotgun
        if (isShotgun) {
            timerShotgun += tDelta;
            if (timerShotgun > durShotGun) {
                player.setShotgun(false);
                isShotgun = false;
            }
        }
    }

    public GameObject rdmPowerUp() {
        GameObject[] powerUps = {medkit_0, shotgun_0};
        return powerUps[Random.Range(0, powerUps.Length)];
    }

    public static void shotGun(float duration) {
        player.setShotgun(true);
        isShotgun = true;
        timerShotgun = 0;
        durShotGun = duration;
    }
}
