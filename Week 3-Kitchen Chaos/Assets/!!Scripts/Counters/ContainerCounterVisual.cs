using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter _container;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _container.OnPlayerGrabObject += ContainerOnPlayerGrabObject;
    }

    private void ContainerOnPlayerGrabObject(object sender, System.EventArgs e)
    {
        _anim.SetTrigger("OpenClose");
    }
}
