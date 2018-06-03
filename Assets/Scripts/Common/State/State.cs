using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface State
{
    void Handle(StateContext context);
}

public class StateContext
{
    public State State { get; set; }
    public StateContext(State state)
    {
        State = state;
    }
    public void Request()
    {
        State.Handle(this);
    }
}