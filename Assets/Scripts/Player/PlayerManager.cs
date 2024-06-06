using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject interactNotification;

    public void NotifyPlayer()
    {
        if (interactNotification != null)
        {
            interactNotification.SetActive(true);
        }
    }

    public void DeNotifyPlayer()
    {
        if (interactNotification != null)
        {
            interactNotification.SetActive(false);
        }
    }
}