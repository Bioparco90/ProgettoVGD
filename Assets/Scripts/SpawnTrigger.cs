using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnTrigger : MonoBehaviour
{
    bool canSpawn;
    bool hasSpawned;
    List<Transform> spawnPointList=new List<Transform>();
    public GameObject enemyToSpawn;

    private void Start() {
        hasSpawned=false;
        foreach(Transform t in transform){
            spawnPointList.Add(t);
        }
    }
    private void Update() {
        int i=0;
        if(canSpawn && !hasSpawned){
            foreach(Transform s in spawnPointList){
                GameObject spawnedEnemy=Instantiate(enemyToSpawn, s.transform,true);
                NavMeshAgent agent = GameObject.FindGameObjectWithTag("Respawn").GetComponent<NavMeshAgent>();
                agent.enabled = false;
                agent.enabled = true;
                //spawnedEnemy.transform.localPosition=spawnPointList[i].transform.localPosition;
                i++;
            }
            hasSpawned=true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="PlayerCollider"){
            canSpawn=true;    
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag=="PlayerCollider"){
            canSpawn=false;
        }
    }
    
}
