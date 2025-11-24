using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;

public class HomeUI : BasePanelController
{
    [SerializeField] private RectTransform playerIllu;
    [SerializeField] private Image playerSkin;
    [SerializeField] private GameObject missionRedDot;

    public override void Show()
    {
        base.Show();
        playerIllu.sizeDelta = new Vector2(150, 300);
        playerIllu.DOSizeDelta(new Vector2(150, 750), 0.5f).SetEase(Ease.OutBack).SetUpdate(true);

       
    }


}
