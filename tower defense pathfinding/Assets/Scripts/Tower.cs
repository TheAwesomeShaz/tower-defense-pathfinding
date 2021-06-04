using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 1f;

    void Start()
    {

        StartCoroutine(BuildTower(buildDelay));
    }

    IEnumerator BuildTower(float buildDelay)
    {
        //set everything inactive
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }


        //set everything active
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);

            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (!bank)
        {
            return false;
        }

        if (bank.CurrentBalance >= cost)
        {

            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }

        //i.e none of the "if" conditions are caught 
        return false;

    }
}
