using UnityEngine;

public class BossBrain : MonoBehaviour
{
    public StateBoss state;
    public float mana;
    public Health health;
    

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
        return Random.Range(min, max+1);
    }

    public void ChangeState(StateBoss SB)
    {
        state = SB;
    }
    public void TakeDesition()
    {
        if (GetDistance() < 2)
        {
            if (GetMana() >= 10)
            {
                ChangeState(StateBoss.especialAttack);
            }
            else
            {
                ChangeState(StateBoss.basicAttack);
            }
        }
        else if (2<GetDistance() && GetDistance() < 5)
        {
            if (GetMana() >= 10)
            {
                ChangeState(StateBoss.especialAttack);
            }
            else
            {
                switch (GetRandom(0,2))
                {
                    case 0:
                        ChangeState(StateBoss.idle);
                        break;

                        case 1:
                        ChangeState(StateBoss.follow);
                        break;

                        case 2:
                        ChangeState(StateBoss.patrol);
                            break;

                    default:
                        break;
                }
            }
        }
        else
        {
            if (GetHealh()<7)
            {
                ChangeState(StateBoss.patrol);
            }
            else if (GetHealh()>=7)
            {
                ChangeState(StateBoss.follow);
            }
        }
    }
}
public enum StateBoss 
{
    idle,
    follow,
    patrol,
    especialAttack,
    basicAttack
}