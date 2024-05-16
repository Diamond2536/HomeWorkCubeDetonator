using System.Collections.Generic;
using UnityEngine;

public class FragmentCubeSpawner : CubeSpawner
{
    [SerializeField] private float _scaleFactor = 0.5f;

    protected override Cube SpawnCube(Vector3 position)
    {
        Cube cube = base.SpawnCube(position);
        cube.transform.localScale *= _scaleFactor;
        return cube;
    }

    public List<Cube> SpawnFragmentedCubes(Vector3 position)
    {
        List<Cube> spawnedCubes = SpawnCubes(position);
        return spawnedCubes;
    }
}



