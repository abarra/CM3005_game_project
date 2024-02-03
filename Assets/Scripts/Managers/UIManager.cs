using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
    
    [SerializeField]
    private GameObject parentView;
   
    [SerializeField]
    private View[] viewList;

    private Dictionary<string, View> instantiatedViews;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        LoadViews();
    }
    

    public void ActivateView(string name)
    {
        Debug.Log($"Activating {name} view");

        if(instantiatedViews.TryGetValue(name, out View view))
        {
            view.gameObject.SetActive(true);
            view.Activate();
        }
    }

    public void DeActivateView(string name)
    {
        Debug.Log($"Deactivating {name} view");
        if(instantiatedViews.TryGetValue(name, out View view))
        {
            view.gameObject.SetActive(false);
            view.DeActivate();
        }
    }

    

    private void LoadViews()
    {
        instantiatedViews = new Dictionary<string, View>();

        foreach(var view in viewList)
        {
            var viewGameObject = Instantiate(view, parentView.transform);
            viewGameObject.name = view.name;
            viewGameObject.gameObject.SetActive(false);

            instantiatedViews.Add(viewGameObject.gameObject.name, viewGameObject);

        }
    }
}
