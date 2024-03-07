using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button _closeBtn;

    [Header("Key Code")]
    [SerializeField] private TextMeshProUGUI _upTxt;
    [SerializeField] private TextMeshProUGUI _leftTxt;
    [SerializeField] private TextMeshProUGUI _downTxt;
    [SerializeField] private TextMeshProUGUI _rightTxt;
    [SerializeField] private TextMeshProUGUI _interactTxt;
    [SerializeField] private TextMeshProUGUI _altTxt;
    [Header("Key Button")]
    [SerializeField] private Button _upBtn;
    [SerializeField] private Button _leftBtn;
    [SerializeField] private Button _downBtn;
    [SerializeField] private Button _rightBtn;
    [SerializeField] private Button _interactBtn;
    [SerializeField] private Button _altBtn;

    private void Awake()
    {
        Instance = this;
        _closeBtn.onClick.AddListener(() =>
        {
            Hide();
        });

        _upBtn.onClick.AddListener(() =>
       {
           Rebind(GameInput.BINDING.MoveUp);
       });
        _leftBtn.onClick.AddListener(() =>
        {
            Rebind(GameInput.BINDING.MoveLeft);
        });
        _downBtn.onClick.AddListener(() =>
        {
            Rebind(GameInput.BINDING.MoveDown);
        });
        _rightBtn.onClick.AddListener(() =>
        {
            Rebind(GameInput.BINDING.MoveRight);
        });
        _interactBtn.onClick.AddListener(() =>
        {
            Rebind(GameInput.BINDING.Interact);
        });
        _altBtn.onClick.AddListener(() =>
        {
            Rebind(GameInput.BINDING.Alt);
        });
    }
    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManagerOnGameUnpaused;

        UpdateVisual();

        Hide();
    }

    private void GameManagerOnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        _upTxt.text = GameInput.Instance.GetTextBinding(GameInput.BINDING.MoveUp);
        _leftTxt.text = GameInput.Instance.GetTextBinding(GameInput.BINDING.MoveLeft);
        _downTxt.text = GameInput.Instance.GetTextBinding(GameInput.BINDING.MoveDown);
        _rightTxt.text = GameInput.Instance.GetTextBinding(GameInput.BINDING.MoveRight);
        _interactTxt.text = GameInput.Instance.GetTextBinding(GameInput.BINDING.Interact);
        _altTxt.text = GameInput.Instance.GetTextBinding(GameInput.BINDING.Alt);
    }

    private void Rebind(GameInput.BINDING binding)
    {
        GameInput.Instance.SetBinding(binding, ()=> { UpdateVisual(); });
    }
}
