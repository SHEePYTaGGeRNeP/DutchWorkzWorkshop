using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Game
{
    public class HealthSystem : MonoBehaviour
    {
        [Header("DEBUG INFO")]
        [SerializeField]
        private int _maxHealth;
        public int MaxHealth
        {
            get => _maxHealth;
            private set
            {
                _maxHealth = value;
                if (CurrentHealth > _maxHealth)
                    CurrentHealth = _maxHealth;
                this.HealthChangedUnity?.Invoke(new HealthChangedEventArgs(_currentHealth, MaxHealth));
            }
        }

        [SerializeField]
        private int _currentHealth;
        public int CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                this._currentHealth = Mathf.Clamp(value, 0, MaxHealth);
                this.HealthChangedViaDelegate?.Invoke(_currentHealth, MaxHealth);
                this.HealthChanged?.Invoke(this, new HealthChangedEventArgs(_currentHealth, MaxHealth));
                this.HealthChangedUnity?.Invoke(new HealthChangedEventArgs(_currentHealth, MaxHealth));
            }
        }

        public delegate void HealthChangedDel(int currentHp, int maxHp);
        public event HealthChangedDel HealthChangedViaDelegate;
        public event EventHandler<HealthChangedEventArgs> HealthChanged;

        [Serializable]
        public class UnityHealthChangedEvent : UnityEvent<HealthChangedEventArgs> { }
        public UnityHealthChangedEvent HealthChangedUnity;

        public static HealthSystem CreateHealthSystem(GameObject go, int maxHealth) => CreateHealthSystem(go, maxHealth, maxHealth);
        public static HealthSystem CreateHealthSystem(GameObject go, int maxHealth, int currentHealth)
        {
            HealthSystem hs = go.AddComponent<HealthSystem>();
            hs.MaxHealth = maxHealth;
            hs.CurrentHealth = currentHealth;
            return hs;
        }

        private void Start()
        {
            this.HealthChangedUnity?.Invoke(new HealthChangedEventArgs(_currentHealth, MaxHealth));
        }

        public void Damage(int damage)
        {
            if (damage < 0)
                throw new ArgumentException($"Damage does not take a negative input ({damage})");
            CurrentHealth -= damage;
        }
        public void Heal(int heal)
        {
            if (heal < 0)
                throw new ArgumentException($"Heal does not take a negative input ({heal})");
            CurrentHealth += heal;
        }

        public void SetMaxHealth(int newValue)
        {
            if (newValue < 1)
                throw new ArgumentException($"Max health must be at least 1 ({newValue})");
            this.MaxHealth = newValue;
        }

    }
    public class HealthChangedEventArgs : EventArgs
    {
        public int CurrentHealth { get; }
        public int MaxHealth { get; }

        public HealthChangedEventArgs(int currentHealth, int maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }
    }
}
