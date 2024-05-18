using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubeTemplate;
    [SerializeField] private int _minCubesQuantity = 2;
    [SerializeField] private int _maxCubesQuantity = 7;
    [SerializeField] private bool _spawnOnStart = false;
    [SerializeField] private bool _fragmentOnSpawn = false;
    [SerializeField] private float _scaleFactor = 0.5f;

    private void Start()
    {
        if (_spawnOnStart)
        {
            SpawnCubes(transform.position);
        }
    }    

    public void SetFragmentOnSpawn(bool value)
    {
        _fragmentOnSpawn = value;
    }

    public void SetScaleFactor(float value)
    {
        _scaleFactor = value;
    }

    public List<Cube> SpawnCubes(Vector3 position)
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

    private int GetCubesQuantity()
    {
        return Random.Range(_minCubesQuantity, _maxCubesQuantity);
    }

    private Color RandomizeCubesColor()
    {
        return Random.ColorHSV();
    }

    private Cube SpawnCube(Vector3 position)
    {
        Color color = RandomizeCubesColor();
        Cube cube = Instantiate(_cubeTemplate, position, Quaternion.identity);
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material.color = color;

        if (_fragmentOnSpawn)
        {
            cube.transform.localScale *= _scaleFactor;
        }

        return cube;
    }    
}
