using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]

public class HealthBar : MonoBehaviour
{
    [Range(0,2)]
    [SerializeField] private float _transitionTime;
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;

    private float _slideTolerance = 0.01f;
    private Coroutine _slide;
    private WaitForFixedUpdate _wait;

    public void Initialize(Player player)
    {
        _player = player;
    }

    public void OnTakeDamage(int damage)
    {
        _player.TakeDamage(damage);
        OnHeathChanged();
    }

    public void OnTakeHealth(int health)
    {
        _player.TakeHealth(health);
        OnHeathChanged();
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

    private void Awake()
    {
        OnHeathChanged();
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
