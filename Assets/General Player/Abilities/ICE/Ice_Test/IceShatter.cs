using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShatter : MonoBehaviour
{
    [SerializeField]
    GameObject attackRef;

    GameObject newAttack;

    [SerializeField]
    List<GameObject> phases = new List<GameObject>();


    public bool demonic = false;


    private void OnEnable() //this plays after AssignAbilityComponents.
    {
        Debug.Log("OnEnable");
    }

    public void Start()
    {
        if (demonic)
        {
            StartCoroutine(Duplicate());
            foreach (GameObject p in phases)
            {
                p.transform.localScale += new Vector3(0.4f, 0.4f, 0.4f);
            }
        }

        StartCoroutine(StartChain());
    }

    private IEnumerator Duplicate()
    {
        if (attackRef != null)
        {
            yield return new WaitForSeconds(0.5f);
            newAttack = Instantiate(attackRef, transform.position, transform.rotation);
            newAttack.transform.Rotate(0, -30f, 0, Space.Self);
            newAttack.transform.position = transform.position - (transform.right/2);
            newAttack = Instantiate(attackRef, transform.position, transform.rotation);
            newAttack.transform.Rotate(0, 30f, 0, Space.Self);
            newAttack.transform.position = transform.position + (transform.right/2);
        }
    }

    private IEnumerator StartChain()
    {
        phases[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        phases[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        phases[2].SetActive(true);

    }
}
