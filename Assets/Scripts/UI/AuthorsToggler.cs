using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthorsToggler : MonoBehaviour
{
    [SerializeField] private float pnlAppearDur = .2f;
    [SerializeField] private float imgBrandAppearDur = .1f;
    [SerializeField] private Image imgBrand;

    private const float LEFT_OFFSET = 78f;
    private Color imgBgInitColor;
    private Color imgBrandInitColor;
    private Image imgBackgr;
    private Sequence tweenIn;
    private Sequence tweenOut;

    public void Popup(bool toShow)
    {
        if (toShow) gameObject.SetActive(true);
        else tweenOut.Play();
    }

    private void Awake()
    {
        imgBackgr = GetComponent<Image>();
        imgBgInitColor = imgBackgr.color;
        imgBrandInitColor = imgBrand.color;

        MenuManager.SetImgTransparency(imgBackgr, imgBgInitColor.a, true);
        transform.localPosition += Vector3.left * LEFT_OFFSET;
        InitAnim();
    }

    private void InitAnim()
    {
        tweenIn = DOTween.Sequence()
            .Join(imgBackgr.DOColor(imgBgInitColor, pnlAppearDur))
            .Join(transform.DOLocalMoveX(LEFT_OFFSET, pnlAppearDur))
            .Join(imgBrand.DOFade(0, imgBrandAppearDur));

        tweenOut = DOTween.Sequence()
            .Join(imgBackgr.DOFade(0, pnlAppearDur))
            .Join(transform.DOLocalMoveX(-LEFT_OFFSET, pnlAppearDur))
            .Join(imgBrand.DOColor(imgBrandInitColor, imgBrandAppearDur))
            .OnComplete(() => gameObject.SetActive(false));
    }

    private void OnEnable()
    {
        tweenIn.Play();
    }
}
