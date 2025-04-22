using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject current_bg;

    private void Start()
    {
        SetChildrenActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetChildrenActive(true);
            if (current_bg != null) current_bg.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetChildrenActive(false);
            if (current_bg != null) current_bg.SetActive(false);
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