using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private Joystick _joystick;

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    private void Update()
    {
        _characterMovement.Move(_joystick.Direction);
    }
}
