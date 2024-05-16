using System.Collections.Generic;
using UnityEngine;

public abstract class CubeSpawner : MonoBehaviour
{
    [SerializeField] protected Cube _cubeTemplate;

    [SerializeField] int _minCubesQuantity = 2;
    [SerializeField] int _maxCubesQuantity = 7;

    private int GetCubesQuantity()
    {
        return Random.Range(_minCubesQuantity, _maxCubesQuantity);
    }

    private Color RandomizeCubesColor()
    {
        return Random.ColorHSV();
    }

    protected virtual List<Cube> SpawnCubes(Vector3 position)
    {
        List<Cube> spawnedCubes = new List<Cube>();

        int cubesAmount = GetCubesQuantity();

        for (int i = 0; i < cubesAmount; i++)
        {
            Cube cube = SpawnCube(position);
            spawnedCubes.Add(cube);
        }

        return spawnedCubes;
    }

    protected virtual Cube SpawnCube(Vector3 position)
    {
        Color color = RandomizeCubesColor();
        Cube cube = Instantiate(_cubeTemplate, position, Quaternion.identity);
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material.color = color;
        return cube;
    }
}
