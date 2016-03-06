using UnityEngine;

public static class OpenWorldSpawnPoints {

    public static Coordinates southSpawn;

    static OpenWorldSpawnPoints()
    {
        southSpawn = new Coordinates() { x = 299, y = 158, z = 5 };
    }

    public class Coordinates
    {
        public int x;
        public int y;
        public int z;
    }
}