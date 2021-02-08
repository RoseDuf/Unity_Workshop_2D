using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] private Sprite m_FullHeart;
    [SerializeField] private Sprite m_EmptyHeart;

    private List<Transform> m_Hearts;
    private int m_NumberOfHearts;

    PlayerController player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        player.lifeCounter = m_NumberOfHearts;

        foreach (Transform heart in transform)
        {
            heart.GetComponent<Image>().sprite = m_FullHeart;
            m_Hearts.Add(heart);
        }
    }

    private void Update()
    {
        if (player.isHurt)
        {
            m_Hearts[player.lifeCounter].GetComponent<Image>().sprite = m_EmptyHeart;
        }
    }

}
