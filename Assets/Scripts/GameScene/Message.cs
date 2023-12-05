using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro; 
public class Message : MonoBehaviour
{
    public Text myMessage; 
    void Start(){
        GetComponent<RectTransform>().SetAsFirstSibling();
    }
}
