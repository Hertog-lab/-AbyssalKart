using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 20;

    private void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }
}