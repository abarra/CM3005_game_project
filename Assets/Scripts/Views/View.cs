using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void Activate()
    {

    }

    public virtual void DeActivate()
    {
        
    }

    public virtual void CloseView()
    {
        UIManager.Instance.DeActivateView(this.gameObject.name);
    }

}
