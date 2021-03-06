﻿using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public float HorizontalInput
    {
        get
        {
            return Input.GetAxisRaw("Horizontal");
        }
    }

    public float VerticalInput
    {
        get
        {
            return Input.GetAxisRaw("Vertical");
        }
    }

    public float FireInput
    {
        get
        {
            return Input.GetAxisRaw("Fire1");
        }
    }
}
