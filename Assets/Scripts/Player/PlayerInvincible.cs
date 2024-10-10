using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerInvincible : MonoBehaviour
{
    private InvincibleController _invincibleController;

    [SerializeField] 
    private float _invincibleDuration;
     private void Awake()
    {
        _invincibleController = GetComponent<InvincibleController>();

    }
    public void StartInvincible()
    {
        _invincibleController.StartInvincible(_invincibleDuration);
    }
}
