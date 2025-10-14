

using UnityEngine;

public class BossBrain : MonoBehaviour
{
    public StateBoss state;
    public float mana;
    public Health health;
    public bool[] aliveBranchs;
    public int percent = 50;
    public int[] weigthBranches;
    public int forestDensity = 1000;

    public float GetMana()
    {
        return mana;
    }
    public float GetDistance()
    {
        return (transform.position - GameManager.Instance.player.position).magnitude;
    }
    public float GetHealh()
    {
        return health.health;
    }
    public int GetRandom(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    public void ChangeState(StateBoss SB)
    {
        state = SB;
    }
    public StateBoss TakeDesition()
    {
        if (GetDistance() < 2)
        {
            if (!aliveBranchs[0]) return StateBoss.basicAttack;
            if (GetMana() >= 10)
            {
                return (StateBoss.especialAttack);
            }
            else
            {
                return (StateBoss.basicAttack);
            }
        }
        else if (2 < GetDistance() && GetDistance() < 5)
        {
            if (!aliveBranchs[1]) return StateBoss.especialAttack;
            if (GetMana() >= 10)
            {
                return (StateBoss.especialAttack);
            }
            else
            {
                if (!aliveBranchs[2]) return StateBoss.follow;
                switch (GetRandom(0, 2))
                {
                    case 0:
                        return (StateBoss.idle);
                        break;

                    case 1:
                        return (StateBoss.follow);
                        break;

                    case 2:
                        return (StateBoss.patrol);
                        break;

                    default:
                        break;
                }
            }
        }
        else
        {
            if (!aliveBranchs[3]) return StateBoss.patrol;

            if (GetHealh() < 7)
            {
                return (StateBoss.patrol);
            }
            else if (GetHealh() >= 7)
            {
                return (StateBoss.follow);
            }
        }
        return StateBoss.idle;
    }
    public void InitArray()
    {
        aliveBranchs = new bool[4];
        for (int i = 0; i < aliveBranchs.Length; i++)
        {
            aliveBranchs[i] = true;
        }
    }
    public void RandomizeArray()
    {
        aliveBranchs = new bool[4];
        for (int i = 0; i < aliveBranchs.Length; i++)
        {
            int roll = Random.Range(0, 100);
            aliveBranchs[i] = roll >= percent;
        }
    }
    public StateBoss TakeDecitionFromForest() 
    { 
        weigthBranches = new int[5];
        for (int i = 0; i < forestDensity; i++)
        {
            RandomizeArray();
            int d = (int)TakeDesition();
            weigthBranches[d]++ ;
        }
        int m = 0;
        for (int i = 1; i < weigthBranches.Length; i++)
        {
            if (weigthBranches[i] > weigthBranches[m])
            {
                m = i; 
            }
        }
        switch (m)
        {
            case 0:
                return StateBoss.idle;
                case 1:
                    return StateBoss.follow;
                case 2:
                    return StateBoss.patrol;
                case 3:
                    return StateBoss.especialAttack;
                case 4:
                    return StateBoss.basicAttack;

            default:
                break;
        }
        return StateBoss.idle;

    }
    private void Start()
    {
        InitArray();
        Debug.Log(TakeDecitionFromForest().ToString());
    }
}
public enum StateBoss 
{
    idle = 0,
    follow = 1,
    patrol = 2,
    especialAttack = 3,
    basicAttack = 4
}