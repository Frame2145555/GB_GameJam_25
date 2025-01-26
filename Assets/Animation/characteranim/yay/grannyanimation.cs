using UnityEngine;

public class grannyanimation : celineanimation
{
    int count=0;
public void loopcount(){count++;}
    public void resetcount(){count=0;}

    void Update()
    {
        if (count==3)
        {
            anim.Play("");
        }

    }
    
}
