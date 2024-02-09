using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCombo : MonoBehaviour
{
   private MeleeController meleeController;

    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject Hiteffect;

    // Start is called before the first frame update
    void Start()
    {
        meleeController = GetComponent<MeleeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && meleeController.CurrentState.GetType() == typeof(IdleMeleeState))
        {
            meleeController.SetNextState(new GroundEntryState());
        }
    }
}
