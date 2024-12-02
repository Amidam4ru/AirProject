using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(AudioSource))]
public class EnemyHealth : MonoBehaviour
{
    static readonly int IsDead = Animator.StringToHash(nameof(IsDead));

    [SerializeField] private Color _damageColor = Color.red;
    [SerializeField] private float _colorChangeDuration = 0.5f;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private float _maxHealth = 1f;

    private Sprite _initialSprite;
    private float _currentHealth;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Collider2D _collider;
    private WaitForSeconds _delay;
    private Coroutine _dieCoroutine;
    private EnemyMover _enemyMover;
    private AudioSource _audioSource;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _delay = new WaitForSeconds(0.5f);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _enemyMover = GetComponent<EnemyMover>();
        _audioSource = GetComponent<AudioSource>();
        _initialSprite = _spriteRenderer.sprite;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _dieCoroutine = StartCoroutine(Die());
        }
        else
        {
            FlashRed();
        }
    }

    private void FlashRed()
    {
        Color originalColor = _spriteRenderer.color;

        _spriteRenderer.DOColor(_damageColor, _colorChangeDuration).OnKill(() =>
        {
            _spriteRenderer.DOColor(originalColor, _colorChangeDuration);
        });
    }

    private IEnumerator Die()
    {
        _enemyMover.enabled = false;
        _collider.enabled = false;

        _animator.SetBool(IsDead, true);
        _audioSource.PlayOneShot(_deathSound);

        yield return _delay;

        _spriteRenderer.enabled = false;
        _animator.Rebind();

        Died?.Invoke(transform.GetComponent<Enemy>());
    }

    private void OnEnable()
    {
        _enemyMover.enabled = true;
        _collider.enabled = true;

        _spriteRenderer.enabled = true;

        _spriteRenderer.sprite = _initialSprite;

        _currentHealth = _maxHealth;

        _animator.SetBool(IsDead, false);
    }
}
