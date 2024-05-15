using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Exploder))]
public class Bomb : Figure
{
    private Exploder _exploder;
    private Coroutine _dissapierCorutin;
    private MeshRenderer _randerer;
    private Shader _shader;

    private void Awake()
    {
        _randerer = GetComponent<MeshRenderer>();
        _exploder = GetComponent<Exploder>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartDissapearCorutine();
        StartWaitCorutine();
    }

    protected override void WorkFinishing()
    {
        _exploder.StartExplosion();

        base.WorkFinishing();
    }


    private void StartDissapearCorutine()
    {
        if(_dissapierCorutin != null)
        {
            StopCoroutine(_dissapierCorutin);
        }

        _dissapierCorutin = StartCoroutine(Dissapearing());
    }

    private IEnumerator Dissapearing()
    {
        float startTime = Time.time;
        float normaliseTime;
        float alpha;
        Color materialColor;

        while (Time.time - startTime < _duration)
        {
            normaliseTime = (Time.time - startTime) / _duration;
            alpha = Mathf.Lerp(1, 0, normaliseTime);

            materialColor = _randerer.material.color;
            materialColor.a = alpha;
            _randerer.material.color = materialColor;

            yield return null;
        }
    }    
}
