using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private List<Transform> doors;
    
    public void OpenDoor()
    {
        foreach (var door in doors)
        {
            door.gameObject.SetActive(false);
        }
    }
}
