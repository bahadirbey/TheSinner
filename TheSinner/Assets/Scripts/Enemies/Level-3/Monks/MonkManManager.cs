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
    public GameObject air;
    public GameObject fire;
    public GameObject earth;

    public GameObject earthPoint;

    public static bool earthActive;
    public static bool activateUltimate;
    private TakeDamage takeDamage;
    bool earthCreated;

    public static bool monkDefeated;
    void Start()
    {
        animator = GetComponent<Animator>();
        takeDamage = GetComponent<TakeDamage>();
        player = GameObject.FindGameObjectWithTag("Player");
        changeElementCd = startChangeElementCd;
        skillCd = startSkillCd;
    }


    void Update()
    {
        ChangeElement();
        ArrangeMeditationElement();

        if (takeDamage.currentHealth <= takeDamage.health * 3 / 5)
        {
            activateUltimate = true;
        }else if (takeDamage.currentHealth <= takeDamage.health * 1 / 5)
        {
            monkDefeated = true;
            //Load Scene or detroy all objects
        }
    }

    void ArrangeMeditationElement()
    {
        if (activateUltimate)
        {
            str = "ultimate";
            CheckMeditationChange(str);
            CreateElementAttack(str);
        }
        else if (meditationElement == 0)
        {
            str = "ice";
            CheckMeditationChange(str);
            CreateElementAttack(str);
        }
        else if (meditationElement == 1)
        {
            str = "air";
            CheckMeditationChange(str);
            CreateElementAttack(str);
        }
        else if (meditationElement == 2)
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
        if (activateUltimate)
        {

        }else if (changeElementCd <= 0)
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
            if (str == "ultimate")
            {
                if (player.GetComponent<PlayerMovement>().isGrounded)
                {
                    int random = Random.Range(0,2);
                    if (random < 1)
                    {
                        Instantiate(fire, new Vector2(player.transform.position.x, player.transform.position.y + 1f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(ice, new Vector2(player.transform.position.x, player.transform.position.y + .75f), Quaternion.identity);
                    }
                    skillCd = 3f;
                }
                if (!earthCreated)
                {
                    Instantiate(earth, earthPoint.transform.position, Quaternion.identity);
                    Instantiate(air, new Vector2(transform.position.x + 2f, transform.position.y + 1f), Quaternion.identity);
                    earthActive = true;
                    earthCreated = true;
                }
            }
            else if (str == "ice")
            {
                if (player.GetComponent<PlayerMovement>().isGrounded)
                {
                    Instantiate(ice, new Vector2(player.transform.position.x, player.transform.position.y + .75f), Quaternion.identity);
                    skillCd = 10f / 3f;
                }
            }
            else if (str == "air")
            {
                Instantiate(air, new Vector2(transform.position.x + 2f, transform.position.y + 1f), Quaternion.identity);
                skillCd = 10f;
            }
            else if (str == "earth")
            {
                earthActive = true;
                Instantiate(earth, earthPoint.transform.position, Quaternion.identity);
                skillCd = 10f;
            }
            else if (str == "fire")
            {
                earthActive = false;
                if (player.GetComponent<PlayerMovement>().isGrounded)
                {
                    Instantiate(fire, new Vector2(player.transform.position.x, player.transform.position.y + 1f), Quaternion.identity);
                    skillCd = 1f;
                }
            }
        }
        else
        {
            skillCd -= Time.deltaTime;
        }
    }
}
