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
    /// 修改血量显示
    /// </summary>
    /// <param name="fillPercent">血量显示的百分比</param>
    public void SetWidth(float fillPercent)
    {
        UserBloodBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, BloodHP_Width*fillPercent);
    }
}
