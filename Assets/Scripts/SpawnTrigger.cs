using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    bool canSpawn;
    List<Transform> spawnPointList=new List<Transform>();
    public GameObject enemyToSpawn;

    private void Start() {
        foreach(Transform t in transform){
            spawnPointList.Add(t);
        }
    }
    private void Update() {
        int i=0;
        //print("canSpawn value: " + canSpawn);
        if(canSpawn){
            Instantiate(enemyToSpawn, spawnPointList[0].transform,true);
            /*foreach(Transform s in spawnPointList){
                
                i++;
            }*/
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="PlayerCollider"){
            print("STO COLLIDENDO");
            canSpawn=true;    
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag=="PlayerCollider"){
            print("NON STO COLLIDENDO MANNAGGIA A DIO");
            canSpawn=false;
        }
    }
    
}
