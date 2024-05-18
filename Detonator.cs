using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _radius;
    [SerializeField] private float _explosionForce;

    private Camera _mainCamera;

    private int _leftMouseButton = 0;

    private float _splitChance = 1f;
    private float _splitFactor = 0.5f;
    private float _scaleFactor = 0.5f;

    private void Start()
    {
        _mainCamera = Camera.main;

        if (_cubeSpawner != null)
        {
            _cubeSpawner.SetFragmentOnSpawn(true);
            _cubeSpawner.SetScaleFactor(_scaleFactor);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out Cube hitCube))
                {
                    if (Random.value <= _splitChance)
                    {
                        Vector3 hitCubePosition = hitCube.transform.position;
                        Explode(hitCubePosition, _cubeSpawner.SpawnCubes(hitCubePosition));
                        Destroy(hitCube.gameObject);
                        _splitChance *= _splitFactor;
                    }
                    else
                    {
                        Destroy(hitCube.gameObject);
                    }
                }
            }
        }
    }

    private void Explode(Vector3 center, List<Cube> cubesToExplode)
    {
        foreach (Cube cube in cubesToExplode)
        {
            if (cube.TryGetComponent(out Rigidbody rigidBody))
            {
                rigidBody.AddExplosionForce(_explosionForce, center, _radius);
            }
        }
    }
}
