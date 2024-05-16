using UnityEngine;

public class StartCubeSpawner : CubeSpawner
{
    private void Start()
    {
        Vector3 spawnerPosition = transform.position;
        SpawnFragmentedCubes(spawnerPosition);
    }

    public void SpawnFragmentedCubes(Vector3 position)
    {
        base.SpawnCubes(position);
    }
}

