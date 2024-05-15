using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(CubeCollisionHandler))]
public class Cube : Figure
{
    private CubeCollisionHandler _collisionHandler;
    private Renderer _renderer;
    private bool _isGroundTouched;
    private Color _baseColor;
    private Floor _lastFloor;

    private void Awake()
    {
        _collisionHandler = GetComponent<CubeCollisionHandler>();
        _renderer = GetComponent<Renderer>();
        _baseColor = _renderer.material.color;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _isGroundTouched = false;
        _lastFloor = null;
        _renderer.material.color = _baseColor;
        _collisionHandler.GroundTouched += OnTouchGround;
    }

    private void OnDisable()
    {
        _collisionHandler.GroundTouched -= OnTouchGround;
    }

    private void ChangeColor()
    {
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void OnTouchGround(Floor floor)
    {
        if (!_isGroundTouched)
        {
            _isGroundTouched = true;
            StartWaitCorutine();
        }

        if (_lastFloor != floor)
        {
            ChangeColor();
            _lastFloor = floor;
        }
    }
}
