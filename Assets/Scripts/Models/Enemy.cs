using System;

public class Enemy
{
    public Guid ID { get; private set; }

    public Enemy()
    {
        ID = new Guid();
    }
}
