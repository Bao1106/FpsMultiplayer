using UnityEngine;

public class Destroy : MonoBehaviour {

public float destroyTime = 3;

void  Start (){
Destroy (gameObject, destroyTime);
}
void  Update (){
}
}