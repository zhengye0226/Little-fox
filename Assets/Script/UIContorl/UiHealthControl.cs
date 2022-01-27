using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealthControl : MonoBehaviour
{
    // Start is called before the first frame update

    public Image UserBloodBar;

    private float BloodHP_Width;

    public static UiHealthControl instance
    {
        private set;
        get;
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        BloodHP_Width = UserBloodBar.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// �޸�Ѫ����ʾ
    /// </summary>
    /// <param name="fillPercent">Ѫ����ʾ�İٷֱ�</param>
    public void SetWidth(float fillPercent)
    {
        UserBloodBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, BloodHP_Width*fillPercent);
    }
}
