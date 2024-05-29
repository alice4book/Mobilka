using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Spawner : MonoBehaviour
{
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public Transform spawnDest1;
    float x;
    int randNum;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(spawning());
    }
    public IEnumerator spawning()
    {
        while (true)
        {
            randNum = Random.Range(-300, 300);
            x = randNum / 100.0f;
            if(spawnDest1.position.x >= 3){
                spawnDest1.transform.position = new Vector3(0, spawnDest1.position.y, spawnDest1.position.z);
            }
            if (spawnDest1.position.x <= 3){
                spawnDest1.transform.position = new Vector3(0, spawnDest1.position.y, spawnDest1.position.z);
            }
            spawnDest1.transform.position = new Vector3(spawnDest1.position.x - x, spawnDest1.position.y, spawnDest1.position.z);
            yield return new WaitForSeconds(0.2f);
            randNum = Random.Range(1, 5);
            switch (randNum)
            {
                case 1:
                    Instantiate(obj1, spawnDest1.position, spawnDest1.rotation);
                    break;
                case 2:
                    Instantiate(obj2, spawnDest1.position, spawnDest1.rotation);
                    break;
                case 3:
                    Instantiate(obj3, spawnDest1.position, spawnDest1.rotation);
                    break;
                case 4:
                    Instantiate(obj4, spawnDest1.position, spawnDest1.rotation);
                    break;
            }
        }
    }
}
