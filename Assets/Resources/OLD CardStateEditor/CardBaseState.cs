
using UnityEngine;

public abstract class CardBaseState
{
   public abstract void EnterState(CardStateManager card);
   public abstract void Exit(CardStateManager card);

   public abstract void Update(CardStateManager card);

}
