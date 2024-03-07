using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    private Animator _anim;

    [SerializeField] private Player _player;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        _anim.SetBool(IS_WALKING, _player.IsWalking());
    }

}
