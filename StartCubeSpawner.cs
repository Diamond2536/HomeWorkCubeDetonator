using UnityEngine;

public class StartCubeSpawner : CubeSpawner
{
    private void Start()
    {
        Vector3 spawnerPosition = transform.position;
        SpawnCubes(spawnerPosition);
    }
}


