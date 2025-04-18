using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private bool playerInside = false;

    private void Start()
    {
        SetChildrenActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            playerInside = true;
            SetChildrenActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            playerInside = false;
            SetChildrenActive(false);
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