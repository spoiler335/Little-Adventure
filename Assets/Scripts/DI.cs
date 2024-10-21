using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DI
{
    public static readonly DI di = new DI();

    public InputManager input = new InputManager();

    public EconomyManager economy = new EconomyManager();

    private DI() { }
}
