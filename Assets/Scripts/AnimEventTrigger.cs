using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventTrigger : MonoBehaviour
{
    public void OnGetReadyAnimComplete ()
    {
        UIManager.instance.CountDownStart();
    }
}
