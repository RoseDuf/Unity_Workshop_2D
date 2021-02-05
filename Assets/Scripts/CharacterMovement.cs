using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private int m_Speed;

    private Animator m_Animator;
    
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        
    }

    void HandleInput()
    {

    }
}
