using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKick : MonoBehaviour
{
    [SerializeField] private float kickPowerMin;
    [SerializeField] private float kickPowerMax;
    [SerializeField] private float timeToChargeKick;
    [Space(10)]
    [SerializeField] private float attackRange;

    [Space(10)] [SerializeField] private Slider slider;


    private bool isChargingKick;
    private float currentChargeTime;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) isChargingKick = true;

        if (isChargingKick) currentChargeTime += Time.deltaTime;
        if (currentChargeTime > timeToChargeKick) currentChargeTime = timeToChargeKick;

        if (Input.GetMouseButtonUp(0))
        {
            isChargingKick = false;
            currentChargeTime = 0f;
            Kick();
        }
        
        UpdateUI();
    }

    private void Kick()
    {
        GameObject ball = null;

        Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward * attackRange, new Vector3(attackRange, attackRange, attackRange), transform.rotation);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Ball")
            {
                ball = collider.gameObject;
                break;
            }
        }
        if (ball == null) return;

        float power = Mathf.Lerp(kickPowerMin, kickPowerMax, currentChargeTime / timeToChargeKick);
        ball.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * power);
        currentChargeTime = 0f;
    }

    private void UpdateUI()
    {
        slider.value = currentChargeTime / timeToChargeKick;
    }
}
