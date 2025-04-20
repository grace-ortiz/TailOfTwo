using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private bool playerInside = false;
    public GameObject deactivated_bg;
    public GameObject deactivated_bg1;

    private void Start()
    {
        SetChildrenActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
            SetChildrenActive(true);
            if (deactivated_bg != null) deactivated_bg.SetActive(false);
            if (deactivated_bg1 != null) deactivated_bg1.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
            SetChildrenActive(false);
            if (deactivated_bg != null) deactivated_bg.SetActive(true);
            if (deactivated_bg1 != null) deactivated_bg1.SetActive(true);
        }
    }

    private void SetChildrenActive(bool state)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(state);
        }
    }
}