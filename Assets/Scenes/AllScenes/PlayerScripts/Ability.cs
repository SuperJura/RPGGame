using System;

[Serializable]
public class Ability
{
    public string Name { get; set; }
    public int IdAbility { get; set; }
    public int Damage { get; set; }
    public int ManaCost { get; set; }
    public int Range { get; set; }
    public float Cooldown { get; set; }
    public string Description { get; set; }
    public float CurrentCooldown { get; set; }

    //za boju ikonice
    private int red;
    public int Red
    {
        get
        {
            if (red == 0)
            {
                return 253;
            }
            return red;
        }
        set { red = value; }
    }

    private int green;
    public int Green
    {
        get 
        {
            if (green == 0)
            {
                return 255;
            }
            return green; 
        }
        set { green = value; }
    }

    private int blue;
    public int Blue
    {
        get
        {
            if (blue == 0)
            {
                return 114;
            }
            return blue;
        }
        set { blue = value; }
    }

    public int CalculateDamage(int playerDmg)
    {
        return Damage + playerDmg;
    }
}
