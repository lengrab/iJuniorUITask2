using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [Range(0,2)]
    [SerializeField] private float _transitionTime;
    [SerializeField] private Player _player;

    private float _slideTolerance = 0.01f;
    private Slider _slider;
    private Coroutine _slide;
    private WaitForFixedUpdate _wait;

    public void Initialize(Player player)
    {
        _player = player;
        _player.HealthChanged.AddListener(OnHeathChanged);
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        OnHeathChanged();
    }

    private void OnEnable()
    {
        if (_player != null)
        {
            _player.HealthChanged.AddListener( OnHeathChanged);
        }
    }

    private void OnDisable()
    {
        if (_player != null)
        {
            _player.HealthChanged.RemoveListener(OnHeathChanged);
        }
    }

    private void OnHeathChanged()
    {
        float targetValue = _player.Health / (float)_player.MaxHealth;
        
        if (_slide != null)
        {
            StopCoroutine(_slide);
        }

        _slide = StartCoroutine(SlideTo(targetValue));
    }

    private IEnumerator SlideTo(float target)
    {
        float maxDelta = (1 / _transitionTime) * Mathf.Abs(target - _slider.value);
        bool isEquvivalent = false;

        while (isEquvivalent == false)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, target,  maxDelta * Time.deltaTime);

            if (Mathf.Abs(_slider.value - target) < _slideTolerance)
            {
                isEquvivalent = true;
                StopCoroutine(_slide);
            }
            yield return _wait;
        }
    }
}
