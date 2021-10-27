using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class PlayerStatistics : MonoBehaviour
{
    public static PlayerStatistics _playerStatistics;

    public GameObject _player;
    public Text _healthText;
    public Slider _healthSlider;

    public Text _goldText;

    public float _health;
    public float _maxHealth;

    public int _goldAmount;

    void Awake()
    {
        if(_playerStatistics != null)
        {
            Destroy(_playerStatistics);
        }
        else
        {
            _playerStatistics = this;
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        _health = _maxHealth;
        SetHealthUI();
    }
    public void DealDamage(float damage)
    {
        _health -= damage;
        CheckDeath();
        SetHealthUI();
    }

    private float CalculateHealthPercentage()
    {
        return _health / _maxHealth;
    }

    private void SetHealthUI()
    {
        _healthSlider.value = CalculateHealthPercentage();
        _healthText.text = $"{Mathf.Ceil(_health)} / {_maxHealth}";
    }

    private void CheckOverHeal()
    {
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    public void HealCharacter(float healingValue)
    {
        _health += healingValue;
        CheckOverHeal();
        SetHealthUI();
    }

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            Destroy(_player);
        }
    }
}
