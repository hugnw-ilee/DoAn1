using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerView;
using CharactersModels;

namespace PlayerPresenter
{
    public class PlayerHealth : MonoBehaviour
    {
        private HealthModel healthModel;
        private HealthView healthView;
        private Animator anim; 

        private void Awake() {
            healthModel = GetComponent<Player>().healthModel;
            healthView = GetComponent<HealthView>();
            anim = GetComponent<Animator>();
        }

        void Start(){
            HealthInit();
            RestoreHealth();
        }

        public void GetHealth() => healthView.HealthBar(healthModel.curHealth);
        public void HealthInit() => healthView.HealthInit(healthModel.maxHealth);

        public void RestoreHealth()
        {
            healthModel.RestoreMax();
            GetHealth();
        }

        public void HealthPlus(int value)
        {
            healthModel.Plus(value);
            GetHealth();
        }

        public void HealthMinus(int value)
        {
            healthModel.Minus(value);
            anim.SetTrigger(healthView.hitedAnimator);
            GetHealth();
            if(DeathState()) anim.SetBool(healthView.dieAnimator,true);
        }
        

        public bool DeathState(){
            if(healthModel.curHealth <= 0){
                return true;
            }
            return false;
        }

        // Kiểm tra trạng thái va chạm
        private bool isCollidingWithEnemy = false; 
        private Coroutine damageCoroutine;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                isCollidingWithEnemy = true;
                damageCoroutine = StartCoroutine(DamageOverTime());
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy") && !isCollidingWithEnemy)
            {
                isCollidingWithEnemy = true;
                damageCoroutine = StartCoroutine(DamageOverTime());
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                isCollidingWithEnemy = false;
                if (damageCoroutine != null)
                {
                    StopCoroutine(damageCoroutine);
                }
            }
        }

        private IEnumerator DamageOverTime()
        {
            while (isCollidingWithEnemy)
            {
                HealthMinus(10);
                yield return new WaitForSeconds(0.5f);
            }
        }        
    }
}
