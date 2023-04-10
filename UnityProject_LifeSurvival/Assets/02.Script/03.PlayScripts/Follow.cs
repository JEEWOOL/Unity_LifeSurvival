using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            this.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(/*GameManager.Instance.player.transform.position*/GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().transform.position);
    }
}
