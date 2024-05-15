using UnityEngine;

public class BombSpawner : FigureSpawner<Figure>
{
    [SerializeField] CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.FigureSpawned += OnCubeSpawn;
    }

    private void OnDisable()
    {
        _cubeSpawner.FigureSpawned -= OnCubeSpawn;
    }

    private void OnCubeSpawn(Figure cube)
    {
        cube.WorkDone += OnCubeDissapear;
    }

    private void OnCubeDissapear(Figure cube)
    {
        Spawn(cube.transform.position);
        cube.WorkDone -= OnCubeDissapear;
    }
}
