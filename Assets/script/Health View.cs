using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerViewInterface;

namespace PlayerView
{
    public class HealthView : MonoBehaviour, IHealthView
    {
        [SerializeField] private HealthBar healthBar;
        public string hitedAnimator;
        public string dieAnimator;

        public void HealthInit(int maxValue) => healthBar.SetMaxHealth(maxValue);
        
        public void HealthBar(int value) => healthBar.SetHealth(value);

    }

}

namespace PlayerViewInterface
{
    public interface IHealthView
    {
        public void HealthBar(int value);
        public void HealthInit(int maxHealth);
    }

}


