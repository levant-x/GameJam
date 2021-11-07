using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AuthorsToggler : MonoBehaviour
{
    [SerializeField] protected float pnlAppearDur = .2f;
    [SerializeField] protected float imgBrandAppearDur = .1f;
    [SerializeField] protected Image imgBrand;

    protected const float LEFT_OFFSET = 78f;
    protected Image imgBackgr;
    protected Text[] labels;

    private float initXPos;
    private Color imgBgInitColor;
    private Color imgBrandInitColor;
    private Sequence tweenIn;
    private Sequence tweenOut;

    public virtual void Popup(bool toShow)
    {
        if (toShow) gameObject.SetActive(true);
        else
        {
            tweenOut.Restart();
            tweenOut.Play();
        }
    }

    private void Awake()
    {
        imgBackgr = GetComponent<Image>();
        imgBgInitColor = imgBackgr.color;
        imgBrandInitColor = imgBrand.color;
        labels = transform.GetComponentsInChildren<Text>();
        ApplyTween();
    }

    protected void ApplyTween()
    {
        MenuManager.SetImgTransparency(imgBackgr, imgBgInitColor.a, true);
        transform.localPosition += Vector3.left * LEFT_OFFSET;
        initXPos = transform.localPosition.x;
        InitAnim();
    }

    private void InitAnim()
    {
        var labelsIn = DOTween.Sequence();
        var labelsOut = DOTween.Sequence();
        foreach (var lbl in labels)
        {
            labelsIn.Join(lbl.DOColor(Color.white, pnlAppearDur));
            labelsOut.Join(lbl.DOFade(0, pnlAppearDur));
        }
        labelsIn.Pause().SetAutoKill(false);
        labelsOut.Pause().SetAutoKill(false);

        tweenIn = DOTween.Sequence()
            .Join(imgBackgr.DOColor(imgBgInitColor, pnlAppearDur))
            .Join(transform.DOLocalMoveX(initXPos + LEFT_OFFSET, pnlAppearDur))
            .Join(imgBrand.DOFade(0, imgBrandAppearDur))
            .Join(labelsIn)
            .Pause()
            .SetAutoKill(false); 

        tweenOut = DOTween.Sequence()
            .Join(imgBackgr.DOFade(0, pnlAppearDur))
            .Join(transform.DOLocalMoveX(initXPos, pnlAppearDur))
            .Join(labelsOut)
            .OnComplete(() =>
            {
                imgBrand.DOColor(imgBrandInitColor, imgBrandAppearDur)
                    .OnComplete(() => gameObject.SetActive(false));
            })            
            .Pause()
            .SetAutoKill(false);
    }

    protected virtual void OnEnable()
    {
        tweenIn.Restart();
        tweenIn.Play();
    }
}
