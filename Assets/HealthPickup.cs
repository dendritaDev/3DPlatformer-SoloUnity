using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healAmount;
    public bool isFullHeal;

    public GameObject healthEffect;

    public int soundToPlay;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(healthEffect, PlayerController.instance.transform.position + new Vector3(0, 1, 0), PlayerController.instance.transform.rotation);
            AudioManager.instance.PlaySFX(soundToPlay);

            if (isFullHeal)
            {
                HealthManager.instance.ResetHealth();
            }
            else
            {
                HealthManager.instance.AddHealth(healAmount);
            }
        }
    }

}
