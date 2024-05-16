using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryItself : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        Invoke("Destroy", 1.5f);
    }


    void Destroy(){
        Destroy(gameObject);
    }
}
