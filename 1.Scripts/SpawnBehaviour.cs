using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBehaviour : MonoBehaviour
{
    public GameObject createdCharacter;
    public GameObject newCharacter;

    public Camera cam;

    public GameManager _gm;
    public Characterbehaviour _cb;
    public RaycastHit hit;

    public string color;

    public Text totalPopulation;
    public Text totalCount;

    public int spawnCount = 0;
    public Vector3 touchPosition;


    public List<GameObject> character = new List<GameObject>();
    List<GameObject> spawnedCharacter= new List<GameObject>();

    int godmies = 0;
    int matePercent = 0;
    int fightPercent = 0;

    bool canSpawn = false;

    public AudioSource UI;

    // Update is called once per frame
    void Update()
    {
        SpawnCounter();
        totalPopulation.text = "Population :" + spawnCount;
        totalCount.text = "Godmies Alive :" + godmies;
        if (_gm.switchControl && (Input.GetMouseButtonDown(0)) && !_gm.switchControl1)
        {
            Spawn();
        }
    }
    void SpawnCounter()
    {
        if(spawnedCharacter.Count!=0)
        {
            for (int i=0; i< spawnedCharacter.Count;i++)
            {
                if(spawnedCharacter[i]== null)
                {
                    godmies--;
                    spawnedCharacter.RemoveAt(i);
                }
            }
        }
        //Debug.Log(spawnedCharacter.Count);
    }
    public void Spawn()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Ball" && spawnCount < 6 && canSpawn)
        {
            _gm.spawn.Play();
            godmies++;
            spawnCount++;
            touchPosition = hit.point;

            newCharacter = Instantiate(createdCharacter, touchPosition, hit.transform.rotation);

            newCharacter.GetComponent<Characterbehaviour>()._SpawnBehaviour = GetComponent<SpawnBehaviour>();
            newCharacter.GetComponent<Characterbehaviour>()._gm = _gm;
            newCharacter.GetComponent<Characterbehaviour>().id = spawnCount;
            _gm.character.Add(newCharacter);
            spawnedCharacter.Add(newCharacter);
            newCharacter.GetComponent<Characterbehaviour>().matePercentage = matePercent;
            newCharacter.GetComponent<Characterbehaviour>().fightPercentage = fightPercent;

            newCharacter.transform.LookAt(hit.transform.position);
            newCharacter.transform.Rotate(-90, 0, 0);

            newCharacter.transform.position = hit.transform.position;
            newCharacter.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 51, 0);

            canSpawn = false;
        }
    }
    public void Happy()
    {
        createdCharacter = character[0];
        matePercent = 100;
        fightPercent = 0;
        canSpawn = true;
        UI.Play();
    }
    public void Angry()
    {
        createdCharacter = character[1];
        matePercent = 0;
        fightPercent = 100;
        canSpawn = true;
        UI.Play();
    }
    public void Sad()
    {
        createdCharacter = character[2];
        matePercent = 75;
        fightPercent = 65;
        canSpawn = true;
        UI.Play();
    }
    public void Fear()
    {
        createdCharacter = character[3];
        matePercent = 25;
        fightPercent = 55;
        canSpawn = true;
        UI.Play();
    }

}
