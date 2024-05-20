using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float _distanse = 10f;
    RaycastHit _hit;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out _hit, _distanse))
            {
                if(_hit.transform.tag == "doorsCapsul")
                {
                    Animator _anim = _hit.transform.GetComponent<Animator>();
                    _anim.SetBool("Open", !_anim.GetBool("Open"));
                }
            }
        }
    }
}
