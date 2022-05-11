using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]

public class PlayerPresentor : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;
    [Range(0,2)]
    [SerializeField] private float _transitionTime;

    private Coroutine _slide;
    private WaitForFixedUpdate _wait;
    private float _slideTolerance = 0.001f;

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
        float targetValue = (float)_player.Health / (float)_player.MaxHealth;
        
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
