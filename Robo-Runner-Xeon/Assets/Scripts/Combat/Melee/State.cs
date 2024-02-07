using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected float time { get; set; }
    protected float fixedtime { get; set; }
    protected float latetime { get; set; }

    public MeleeController meleeController;

    public virtual void OnEnter(MeleeController _meleeController)
    {
        meleeController = _meleeController;
    }


    public virtual void OnUpdate()
    {
        time += Time.deltaTime;
    }

    public virtual void OnFixedUpdate()
    {
        fixedtime += Time.deltaTime;
    }
    public virtual void OnLateUpdate()
    {
        latetime += Time.deltaTime;
    }

    public virtual void OnExit()
    {

    }

    #region Passthrough Methods

    protected static void Destroy(UnityEngine.Object obj)
    {
        UnityEngine.Object.Destroy(obj);
    }

    protected T GetComponent<T>() where T : Component { return meleeController.GetComponent<T>(); }

    protected Component GetComponent(System.Type type) { return meleeController.GetComponent(type); }

    protected Component GetComponent(string type) { return meleeController.GetComponent(type); }
    #endregion
}
