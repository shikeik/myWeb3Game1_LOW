using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Test
{
public class Test: MonoBehaviour
{
    public Dictionary<Type, A> alist = new Dictionary<Type, A>();

    public Test()
    {
        alist.Add(typeof(Aa), new Aa());
        alist.Add(typeof(Ab), new Ab());

        foreach (Type t in alist.Keys)
        {
            alist[t].say();
            if (alist[t] is Aa) ((Aa)alist[t]).hit();
        }

        A a = GetState(typeof(Aa));
        a.say();

        A ab = GetState<Ab>();
        ab.say();
    }
    public A GetState(Type t)
    {
        return alist[t];
    }
    private T GetState<T>()
    {
        return (T)(object)alist[typeof(T)];
    }
}

    public class A
    {
        public string name;
        public void say()
        {
            Debug.Log(""+name);
        }
    }
    public class Aa: A
    {
        public Aa() { name = "aaaaaaaa"; }
        public void hit() { Debug.Log("hit you."); }
    }
    public class Ab: A
    {
        public Ab() { name = "bbbbbbbbb"; }
    }
}