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
        //TODO: Instantiat Hearts
    }

    private void Update()
    {
        //TODO: Decrement hearts
    }

}
