using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SpawnBehaviour _SpawnBehaviour;
    public TouchBehaviour _tc;

    public List<GameObject> models = new List<GameObject>();
    public List<GameObject> character = new List<GameObject>();
    public List<GameObject> house = new List<GameObject>();

    public Slider slide;


    public int mate;
    public int fight;
    public int speed;

    public bool canStart = false;
    public bool switchControl;
    public bool switchControl1;

    public AudioSource construct;
    public AudioSource spawn;
    public AudioSource earth;
    public AudioSource death;
    public AudioSource baby;
    public AudioSource UI;

    float a = 0;

    public void Feelings(GameObject objA, GameObject objB)
    {
        Debug.Log(objA.name);
        Debug.Log(objB.name);

        mate = objA.GetComponent<Characterbehaviour>().matePercentage + objB.GetComponent<Characterbehaviour>().matePercentage;
        fight = objA.GetComponent<Characterbehaviour>().fightPercentage + objB.GetComponent<Characterbehaviour>().fightPercentage;


        if (mate > fight)
        {
            StartCoroutine(Spawn());
        }
        else if (mate < fight)
        {
            StartCoroutine(Die());
        }
        if (mate == fight)
        {
            int r = Random.Range(0, 2);
            if (r == 1)
            {
                StartCoroutine(Spawn());
            }
            else
            {
                StartCoroutine(Die());
            }
        }
        IEnumerator Spawn()
        {
            construct.Play();
            yield return new WaitForSeconds(1f);
            baby.Play();

            yield return new WaitForSeconds(1f);
            spawn.Play();

            GameObject mateHouse = Instantiate(house[Random.Range(0, 3)], objB.transform.position, objB.transform.rotation);
            mateHouse.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 51, 0);
            yield return new WaitForSeconds(2f);

            int rand = Random.Range(0, 2);
            GameObject gb = new GameObject();
            if (rand == 0)
            {
                gb = Instantiate(objA, objB.transform.position, objB.transform.rotation);
                gb.GetComponent<Characterbehaviour>().matePercentage = objA.GetComponent<Characterbehaviour>().matePercentage;
                gb.GetComponent<Characterbehaviour>().fightPercentage = objA.GetComponent<Characterbehaviour>().fightPercentage;
            }
            else
            {
                gb = Instantiate(objB, objB.transform.position, objB.transform.rotation);
                gb.GetComponent<Characterbehaviour>().matePercentage = objB.GetComponent<Characterbehaviour>().matePercentage;
                gb.GetComponent<Characterbehaviour>().fightPercentage = objB.GetComponent<Characterbehaviour>().fightPercentage;
            }

            gb.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 51, 0);
            gb.GetComponent<Characterbehaviour>()._SpawnBehaviour = _SpawnBehaviour;
            _SpawnBehaviour.spawnCount++;
            objA.GetComponent<Characterbehaviour>().childId.Add(_SpawnBehaviour.spawnCount);
            objB.GetComponent<Characterbehaviour>().childId.Add(_SpawnBehaviour.spawnCount);
            gb.GetComponent<Characterbehaviour>().id = _SpawnBehaviour.spawnCount;
            gb.GetComponent<Characterbehaviour>().parentId.Add(objA.GetComponent<Characterbehaviour>().id);
            gb.GetComponent<Characterbehaviour>().parentId.Add(objB.GetComponent<Characterbehaviour>().id);
            gb.GetComponent<Characterbehaviour>()._gm = GetComponent<GameManager>();
            character.Add(gb);
        }
        IEnumerator Die()
        {
            death.Play();
            yield return new WaitForSeconds(2f);
            Destroy(objB);
        }
    }
    public void Zoom()
    {
        a = slide.value * 80;
        Camera.main.GetComponent<Camera>().fieldOfView = a;
        switchControl1 = true;
        UI.Play();

    }
    public void canRotate()
    {
        switchControl = false;
        switchControl1 = false;
        UI.Play();
    }
    public void canSpawn()
    {
        switchControl = true;
        switchControl1 = false;
        UI.Play();
    }
    public void canZoom()
    {
        switchControl = false;
        switchControl1 = true;
        UI.Play();
    }

    void Update()
    {
        if(_SpawnBehaviour.spawnCount>5)
        {
            canStart = true;
        }
        if (character.Count != 0)
        {
            for (int i = 0; i < character.Count; i++)
            {
                if (character[i] == null)
                {
                    character.RemoveAt(i);
                }
            }
        }
        earth.Play();
    }
}
