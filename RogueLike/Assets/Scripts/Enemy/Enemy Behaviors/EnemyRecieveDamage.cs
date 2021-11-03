using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRecieveDamage : MonoBehaviour
{
    public static EnemyRecieveDamage enemy;
    public string _nameOfEnemy;
    public float _health;
    public float _maxHealth;
    public bool _recievedDamage;

    public GameObject _healthBar;
    public Slider _healthBarSlider;

    public List<GameObject> _lootList;
    public int _chosenLootDrop;
    public GameObject _lootDrop;

    void Start()
    {
        _health = _maxHealth;
    }
    public void DealDamage(float damage)
    {
        _healthBar.SetActive(true);
        _health -= damage;
        CheckDeath();
        _healthBarSlider.value = CalculateHealthPercentage();
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
        _healthBarSlider.value = CalculateHealthPercentage();
    }

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            Debug.Log($"{_nameOfEnemy} has died");
            Destroy(gameObject);
            _chosenLootDrop = Random.Range(0, 1);
            Debug.Log(_chosenLootDrop);
            _lootDrop = _lootList[_chosenLootDrop];
            Instantiate(_lootDrop, transform.position, Quaternion.identity);
        }
    }

    private float CalculateHealthPercentage()
    {
        return _health / _maxHealth;
    }
}
