using UnityEngine;
using System.Collections;

public class DisableAnimation : MonoBehaviour 
{
    private Animator _anim;

    void Awake()
    {
        _anim = this.GetComponent<Animator>();
    }

    public void Disable()
    {
        _anim.SetBool("ShowAchievements", false);
    }
	
}
