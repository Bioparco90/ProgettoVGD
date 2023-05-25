using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject trigger;
    public GameObject spwanPoint;
    public bool canSpawn;
    
    public GameObject enemyPrefab;
    
    private void Start() {
        canSpawn=true;
    }
    void Update()
    {
        //TODO:
        //canSpawn viene settato da uno script attaccato al trigger 
        //(se il player entra in contatto con il trigger -> canSpawn diventa true)

        if(canSpawn){
            Instantiate(enemyPrefab,spwanPoint.transform);
        }
        
    }
}
