using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    //different healths
    public float baseHealth;
    public float split11Health;
    public float split12Health;
    public float split21Health;
    public float split22Health;
    public float split31Health;
    public float split32Health;

    //different Enemy stages
    public GameObject baseEnemy;
    public GameObject split11;
    public GameObject split12;
    public GameObject split21;
    public GameObject split22;
    public GameObject split31;
    public GameObject split32;

    public GameObject Split1;

    //rewards
    public GameObject reward1;
    public GameObject reward2;
    public GameObject reward3;
    public GameObject reward4;
    public void TakeDamage(int damage)
    {
        baseHealth -= damage;

        if (baseHealth <= 0)
        {
            Debug.Log("Split");
            //set smaller 2 to active then repeat in another if()
            Split1.transform.SetParent(null);
            //Split2.transform.parent = Split1.transform;
            baseEnemy.SetActive(false);

            split11.SetActive(true);
            split12.SetActive(true);
        }
    }

    public void TakeDamageSplit11(int damage)
    {
        split11Health -= damage;

        if (split11Health <= 0)
        {
            split21.transform.SetParent(null);
            split31.transform.SetParent(null);
            split11.SetActive(false);

            split21.SetActive(true);
            split31.SetActive(true);
            //CheckSplit1();
        }
    }
    public void TakeDamageSplit12(int damage)
    {
        split12Health -= damage;

        if (split12Health <= 0)
        {
            split22.transform.SetParent(null);
            split32.transform.SetParent(null);
            split12.SetActive(false);

            split22.SetActive(true);
            split32.SetActive(true);
            //CheckSplit1();
        }
    }

   /* public void CheckSplit1()
    {
        if (split11Health <= 0)
        {
            split21.SetActive(true);
            split31.SetActive(true);


        }
        if(split12Health <= 0)
        {
            split22.SetActive(true);
            split32.SetActive(true);
            //reward2.SetActive(false);
        }
    } */

    public void TakeDamageSplit21(int damage)
    {
        split21Health -= damage;

        if (split21Health <= 0)
        {
            reward1.transform.SetParent(null);
            split21.SetActive(false);

            reward1.SetActive(true);
            //CheckSplit2();
        }
    }

    public void TakeDamageSplit22(int damage)
    {
        split22Health -= damage;

        if (split22Health <= 0)
        {
            reward2.transform.SetParent(null);
            split22.SetActive(false);

            reward2.SetActive(true);
            //CheckSplit2();
        }
    }

   /* public void CheckSplit2()
    {
        if (split21Health <= 0)
        {
            reward1.SetActive(true);
        }
        if(split22Health <= 0)
        {
            reward2.SetActive(true);
        }
        //else
       // {
        //    reward1.SetActive(false);
        //    reward2.SetActive(false);
      //  }
    } */

    public void TakeDamageSplit31(int damage)
    {
        split31Health -= damage;

        if (split31Health <= 0)
        {
            reward3.transform.SetParent(null);
            split31.SetActive(false);

            reward3.SetActive(true);
            //CheckSplit3();
        }
    }

    public void TakeDamageSplit32(int damage)
    {
        split32Health -= damage;

        if (split32Health <= 0)
        {
            reward4.transform.SetParent(null);
            split32.SetActive(false);

            reward4.SetActive(true);
            //CheckSplit3();
        }
    }

   /* public void CheckSplit3()
    {
        if (split31Health <= 0)
        {
            reward3.SetActive(true);
        }
        if (split32Health <= 0)
        {
            reward4.SetActive(true);
        }
       // else
       // {
        //    reward3.SetActive(false);
         //   reward4.SetActive(false);
       // }
    } */
}
