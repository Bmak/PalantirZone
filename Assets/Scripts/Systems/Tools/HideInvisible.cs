using UnityEngine;

public class HideInvisible : MonoBehaviour
{
    public GameObject parent;

    private void OnBecameInvisible()
    {
        Log.Debug("HIDE " + parent.gameObject.name);
        //parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    private void OnBecameVisible()
    {
        Log.Debug("SHOW " + parent.gameObject.name);
        //parent.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
}
