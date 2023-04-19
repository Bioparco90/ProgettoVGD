using UnityEngine;

public class Weapon : MonoBehaviour
{
   public Vector3 idlePosition {get;set;} //Posizione di base
   public Vector3 aimPosition {get;set;}  //Posizione durante la mira
   public bool isAiming {get;set;} //True se il player sta mirando
   public float fireRate {get;set;} //Tempo che deve passare tra uno sparo e l'altro
   public float damage {get;set;} //Danno inflitto da uno sparo dell'arma
   public bool isActive; //True se l'arma è utilizzata dal player in quel momento
   //private int munizioni;
   public Weapon(Vector3 idlePosition, Vector3 aimPosition, float fireRate, float damage){
      this.idlePosition=idlePosition;
      this.aimPosition=aimPosition;
      this.fireRate=fireRate;
      this.damage=damage;
   }


   
}
