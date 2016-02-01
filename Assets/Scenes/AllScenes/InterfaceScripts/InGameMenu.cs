using UnityEngine;

public class InGameMenu : MonoBehaviour {

    private Animator myAnimator;
    private CanvasGroup myCanvasGrp;

    public bool IsOpen
    {
        get { return myAnimator.GetBool("IsOpen"); }
        set { myAnimator.SetBool("IsOpen", value); }
    }

    public void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myCanvasGrp = GetComponent<CanvasGroup>();

        var rect = GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = new Vector2(0, 0);
    }

    public void Update()
    {
        if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            myCanvasGrp.blocksRaycasts = myCanvasGrp.interactable = false;
        }
        else
        {
            myCanvasGrp.blocksRaycasts = myCanvasGrp.interactable = true;
        }
    }

}