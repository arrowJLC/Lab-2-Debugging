using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : BaseMenu
{
    public Button backButton;

    public override void InitState(MenuController context)
    {
        base.InitState(context);
        state = MenuController.MenuStates.Credits;

        backButton.onClick.AddListener(JumpBack);

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}