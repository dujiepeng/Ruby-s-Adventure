using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2d;
    public int maxHealth = 5;
    public float speed = 3.0f;
    public float timeInvincible = 2.0f;

    public int health
    {
        get { return _currentHealth; }
    }
    
    private float _horizontal;
    private float _vertical;
    private int _currentHealth;
    private bool _inInvincible;
    private float _invInvincibleTimer;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal"); 
        _vertical = Input.GetAxis("Vertical");

        if (_inInvincible)
        {
            _invInvincibleTimer -= Time.deltaTime;
            if (_invInvincibleTimer < 0)
            {
                _inInvincible = false;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = _rigidbody2d.position;
        position.x = position.x + speed * _horizontal * Time.deltaTime;
        position.y = position.y + speed * _vertical * Time.deltaTime;
        _rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (_currentHealth <= 0)
            {
                return;
            }

            if (_inInvincible) return;
            
            _inInvincible = true;
            _invInvincibleTimer = timeInvincible;
            _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
            Debug.Log($"Health: {_currentHealth}/{maxHealth}");
        }
        else
        {
            _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
            Debug.Log($"Health: {_currentHealth}/{maxHealth}");
        }
    }
}
