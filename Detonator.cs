using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    private Camera _mainCamera;

    private int _leftMouseButton = 0;

    [SerializeField] private FragmentCubeSpawner _fragmentCubeSpawner;

    [SerializeField] private float _radius;
    [SerializeField] private float _explosionForce;

    private float _splitChance = 1f;
    private float _splitFactor = 0.5f;

    private List<Cube> _cubeList = new List<Cube>();

    private void Start()
    {
        _mainCamera = Camera.main;
        _fragmentCubeSpawner = FindObjectOfType<FragmentCubeSpawner>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Cube hitCube;

                if (hit.collider.TryGetComponent(out hitCube))
                {
                    Vector3 hitCubePosition = hitCube.transform.position;
                    var newCubes = _fragmentCubeSpawner.SpawnFragmentedCubes(hitCubePosition);
                    foreach (var cube in newCubes)
                    {
                        _cubeList.Add(cube);
                    }

                    Destroy(hitCube.gameObject);
                    Explode(hitCubePosition);

                    foreach (var cube in newCubes)
                    {
                        _cubeList.Remove(cube);
                    }

                    _splitChance *= _splitFactor;
                }
            }
        }
    }

    private void Explode(Vector3 center)
    {
        Collider[] colliders = Physics.OverlapSphere(center, _radius);

        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent(out Rigidbody rigidBody))
            {
                foreach (Cube cube in _cubeList)
                {
                    if (hit.gameObject == cube.gameObject)
                    {
                        if (Random.value <= _splitChance)
                        {
                            rigidBody.AddExplosionForce(_explosionForce, center, _radius);
                        }
                        else
                        {
                            Destroy(cube.gameObject);
                        }
                        break;
                    }
                }
            }
        }
    }
}
