using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Test
{
    public class Test2_Property : MonoBehaviour
    {
public int public_public { get; set; }
public int public_private { get; private set; }

private int public_private_2;
public int Public_private_2 { get => public_private_2; set => public_private_2 = value; }
public int Public_private_3
{
    get
    {
        return public_private_2;
    }
    set
    {
        public_private_2 = value;
    }
}

        public Test2_Property()
        {

        }

    }
}