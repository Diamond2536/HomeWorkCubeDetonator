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
                    if (Random.value <= _splitChance)
                    {
                        Vector3 hitCubePosition = hitCube.transform.position;
                        Explode(hitCubePosition, _fragmentCubeSpawner.SpawnFragmentedCubes(hitCubePosition));
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
        Collider[] colliders = Physics.OverlapSphere(center, _radius);

        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent(out Rigidbody rigidBody))
            {
                foreach (Cube cube in cubesToExplode)
                {
                    if (hit.gameObject == cube.gameObject)
                    {
                        rigidBody.AddExplosionForce(_explosionForce, center, _radius);
                    }
                }
            }
        }
    }
}
