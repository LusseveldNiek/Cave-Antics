using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUnloadArea : MonoBehaviour
{
    public GameObject areaToDisable;
    public GameObject areaToEnable;
    bool canToggle = true;

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            if (canToggle)
            {
                canToggle = false;
                StartCoroutine(WaitForFix());
                if (areaToDisable.activeInHierarchy)
                {
                    areaToDisable.SetActive(false);
                    areaToEnable.SetActive(true);
                }
                else
                {
                    areaToDisable.SetActive(true);
                    areaToEnable.SetActive(false);
                }
            }
        }
    }

    public IEnumerator WaitForFix()
    {
        yield return new WaitForSeconds(0.125f);
        canToggle = true;
    }
}
