using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public Application app { get { return GameObject.FindObjectOfType<Application>(); } }
}

public class Application : StaticInstance<Application>
{
    public GameModel gameModel;
    public GameController gameController;
    public GameView gameView;
}
