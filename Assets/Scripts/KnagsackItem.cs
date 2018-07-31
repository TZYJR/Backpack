using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnagsackItem : UIDragDropItem {

    private int startDepth;
    private TweenScale tweenScale;

    protected override void OnDragStart()
    {
        base.OnDragStart();
        if (this.transform.GetComponent<UISprite>())
        {
            UISprite m_tempSprite = this.transform.GetComponent<UISprite>();
            startDepth = m_tempSprite.depth;
            m_tempSprite.depth = 10;
        }
        tweenScale = this.GetComponent<TweenScale>();
        tweenScale.from = new Vector3(1, 1, 1);
        tweenScale.to = new Vector3(1.2f, 1.2f, 1.2f);
        tweenScale.duration = 0.1f;
        tweenScale.PlayForward();
    }


    protected override void OnDragEnd()
    {
        base.OnDragEnd();
        if (this.transform.GetComponent<UISprite>())
        {
            UISprite m_tempSprite = this.transform.GetComponent<UISprite>();
            m_tempSprite.depth=startDepth;
        }
    }



    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        print(surface);
        this.GetComponent<BoxCollider>().enabled = true;


        if (surface.tag=="Block")
        {
            if (surface.transform.childCount>0)
            {
                Transform m_icon = surface.transform.GetChild(0);
                m_icon.parent = this.transform.parent;
                this.transform.parent = surface.transform;

                m_icon.localPosition = Vector3.zero;
                this.transform.localPosition = Vector3.zero;
            }
            else
            {
                this.transform.parent = surface.transform; 
                this.transform.localPosition = Vector3.zero;
            }
        }
        else if (surface.tag == "Item")
        {
            Transform m_tempParent = surface.transform.parent;
            surface.transform.parent = this.transform.parent;
            this.transform.parent = m_tempParent;

            surface.transform.localPosition = Vector3.zero;
            this.transform.localPosition = Vector3.zero;
        }
        else
        {
            this.transform.localPosition = Vector3.zero;
        }

        tweenScale.ResetToBeginning();
        tweenScale = this.GetComponent<TweenScale>();
        tweenScale.to = new Vector3(1, 1, 1);
        tweenScale.from = new Vector3(1.2f, 1.2f, 1.2f);
        tweenScale.duration = 0.1f;
        tweenScale.PlayForward();


    }
}
