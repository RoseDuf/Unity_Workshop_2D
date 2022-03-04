using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] private Sprite m_FullHeartSprite;
    [SerializeField] private Sprite m_EmptyHeartSprite;

    [SerializeField] private GameObject m_HeartPrefab;
    [SerializeField] private int m_NumberOfHearts;
    private List<GameObject> m_HeartsList;

    //Need reference to player

    private void Start()
    {
        m_HeartsList = new List<GameObject>();
        //TODO: find player component

        for (int i = 0; i < m_NumberOfHearts; i++)
        {
            GameObject newHeart = Instantiate(m_HeartPrefab, transform) as GameObject;
            newHeart.GetComponent<Image>().sprite = m_FullHeartSprite;
            m_HeartsList.Add(newHeart);
        }
    }

    private void Update()
    {
        //TODO: Decrement hearts
    }

}
