using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characterbehaviour : MonoBehaviour
{
    public GameObject mesh;
    public int id;
    public Vector3 pos;
    public GameManager _gm;
    public SpawnBehaviour _SpawnBehaviour;

    public int fightPercentage;
    public int matePercentage;


    public bool matchFound = false;
    public bool canCallFeelings = true;
    bool nearPartner = false;
    bool waitForMatch = false;

    public List<int> parentId = new List<int>();
    public List<int> childId = new List<int>();

    public GameObject match;

    public Quaternion destination;
    LayerMask player = 1 << 9;

    void Start()
    {
        matchFound = false;
        canCallFeelings = true;
        nearPartner = false;
        waitForMatch = false;

        destination = Quaternion.Euler(Random.onUnitSphere * 100);
    }

    void CollisionDetection()
    {
        Collider[] hitColliders = Physics.OverlapSphere(mesh.transform.position, 50, player);
        Debug.Log(hitColliders.Length);
        if (hitColliders.Length != 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].GetComponent<Characterbehaviour>().id != id && !matchFound)
                {
                    gameObject.layer = 10;
                    match = hitColliders[i].gameObject;
                    match.layer = 10;
                    match.GetComponent<Characterbehaviour>().match = gameObject;
                    matchFound = true;
                    match.GetComponent<Characterbehaviour>().matchFound = matchFound;
                }
            }
        }
    }
    void NearDetection()
    {
        if (_gm.character.Count > 1 && !waitForMatch)
        {
            for (int i = 0; i < _gm.character.Count; i++)
            {
                if (_gm.character[i] != null && !parentId.Contains(id) && !parentId.Contains(id))
                {
                    bool yesToSpawn = true;
                    if (parentId.Count != 0)
                    {
                        for (int j = 0; j < parentId.Count; j++)
                        {
                            if (id != parentId[j])
                            {
                                yesToSpawn = true;
                            }
                            else
                            {
                                yesToSpawn = false;
                            }
                        }
                    }
                    if (childId.Count != 0)
                    {
                        for (int j = 0; j < parentId.Count; j++)
                        {
                            if (id != childId[j])
                            {
                                yesToSpawn = true;
                            }
                            else
                            {
                                yesToSpawn = false;
                            }
                        }
                    }
                    if (yesToSpawn)
                    {
                        Characterbehaviour cb = _gm.character[i].GetComponent<Characterbehaviour>();
                        if (cb.id != id && !cb.matchFound && !cb.waitForMatch)
                        {
                            float angle = Quaternion.Angle(transform.rotation, _gm.character[i].transform.rotation);
                            //Debug.Log(angle);
                            if (angle < 30)
                            {
                                match = _gm.character[i].gameObject;
                                match.GetComponent<Characterbehaviour>().match = gameObject;
                                matchFound = true;
                                match.GetComponent<Characterbehaviour>().matchFound = matchFound;
                            }
                        }
                    }
                }
            }
        }
    }
    public IEnumerator WaitToMate()
    {
        canCallFeelings = false;
        yield return new WaitForSeconds(2f);
        matchFound = false;
        waitForMatch = true;
        yield return new WaitForSeconds(3f);
        Debug.Log("Mate1");
        nearPartner = false;
        canCallFeelings = true;
        waitForMatch = false;
    }

    void CharacterMechanics()
    {
        if (!matchFound)
        {
            //CollisionDetection();
            NearDetection();
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destination, 10 * Time.deltaTime);
            float angle = Quaternion.Angle(transform.rotation, destination);
            if (angle < 1)
            {
                destination = Quaternion.Euler(Random.onUnitSphere * 100);
            }
        }
        else if (matchFound != null && matchFound && !nearPartner)
        {
            matchFound = true;
            match.GetComponent<Characterbehaviour>().matchFound = matchFound;
            Debug.Log(gameObject.name);
            destination = match.gameObject.transform.rotation;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destination, 10 * Time.deltaTime);
            float angle = Quaternion.Angle(transform.rotation, destination);
            if (angle < 1)
            {
                match.gameObject.GetComponent<Characterbehaviour>().canCallFeelings = false;
                match.GetComponent<Characterbehaviour>().nearPartner = true;
                nearPartner = true;
            }
        }

        if (nearPartner && canCallFeelings)
        {
            _gm.Feelings(gameObject, match);
            StartCoroutine(WaitToMate());
            StartCoroutine(match.GetComponent<Characterbehaviour>().WaitToMate());
        }
    }
    void Update()
    {
        if (id <= 6 && _gm.canStart)
        {
            CharacterMechanics();
        }
        else if (id > 6)
        {
            CharacterMechanics();
        }

    }
}
