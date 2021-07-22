using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NonBlockingDropdown : TMP_Dropdown
{
    protected override GameObject CreateBlocker(Canvas rootCanvas)
    {
        return null;
    }
}
