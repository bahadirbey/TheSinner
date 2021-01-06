using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkManManager : MonoBehaviour
{
    private Animator animator;

    int meditationElement;

    float skillCd;
    public float startSkillCd;

    float changeElementCd;
    public float startChangeElementCd;

    bool checkMeditationChange;
    string str;

    private GameObject player;

    public GameObject ice;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        changeElementCd = startChangeElementCd;
        skillCd = startSkillCd;
    }

    
    void Update()
    {
        ChangeElement();
        ArrangeMeditationElement();
    }

    void ArrangeMeditationElement()
    {
        if (meditationElement == 0)
        {
            str = "ice";
            CheckMeditationChange(str);
            CreateElementAttack(str);
        }else if (meditationElement == 1)
        {
            str = "air";
            CheckMeditationChange(str);
            CreateElementAttack(str);
        }
        else if(meditationElement == 2)
        {
            str = "earth";
            CheckMeditationChange(str);
            CreateElementAttack(str);
        }
        else
        {
            str = "fire";
            CheckMeditationChange(str);
            CreateElementAttack(str);
        }
    }

    void ChangeElement()
    {
        if (changeElementCd <= 0)
        {
            checkMeditationChange = true;
            changeElementCd = startChangeElementCd;
            if (meditationElement <= 2)
            {
                meditationElement += 1;
            }
            else
            {
                meditationElement = 0;
            }
        }
        else
        {
            changeElementCd -= Time.deltaTime;
        }
    }

    void CheckMeditationChange(string str)
    {
        if (checkMeditationChange)
        {
            animator.SetBool("air", false);
            animator.SetBool("fire", false);
            animator.SetBool("earth", false);
            animator.SetBool("ice", false);
            animator.SetBool(str, true);
            checkMeditationChange = false;
        }
    }

    void CreateElementAttack(string str)
    {
        if (skillCd <= 0)
        {
            if (str == "ice")
            {
                if (player.GetComponent<PlayerMovement>().isGrounded)
                {
                    Instantiate(ice, new Vector2(player.transform.position.x, player.transform.position.y + .75f), Quaternion.identity);
                    skillCd = 3f;
                } 
            }else if (str == "air")
            {
                
            }else if (str == "earth")
            {

            }else if (str == "fire")
            {

            }
        }
        else
        {
            skillCd -= Time.deltaTime;
        }
    }
}
