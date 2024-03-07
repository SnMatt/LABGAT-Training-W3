using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _template;

    private void Awake()
    {
        _template.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManagerOnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliiveryManagerOnRecipeCompleted;

        UpdateVisual();
    }

    private void DeliveryManagerOnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliiveryManagerOnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        foreach (Transform child in _container)
        {
            if (child == _template) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO item in DeliveryManager.Instance.GetWaitingRecipeSOs())
        {
            Transform recipeTf = Instantiate(_template, _container);
            recipeTf.gameObject.SetActive(true);

            recipeTf.GetComponent<DeliveryManagerSingleUI>().SetRecipe(item);
        }
    }
}
