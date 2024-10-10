using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleController : MonoBehaviour
{
    private HealthController _healthController;
    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
    }

    private IEnumerator InvincibleCoroutine(float invincibleDuration)
    {
        _healthController.IsInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        _healthController.IsInvincible = false;
    }

    public void StartInvincible(float invincibleDuration)
    {
        StartCoroutine(InvincibleCoroutine(invincibleDuration));
    }
}
