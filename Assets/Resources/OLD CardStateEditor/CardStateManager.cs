
using UnityEngine;

public class CardStateManager : MonoBehaviour
{
    CardBaseState currentCardState;
    CardTitleState cardTitleState = new CardTitleState();
    CardTypeState cardTypeState = new CardTypeState();
    CardColorState cardColorState = new CardColorState();
    CardDescriptionState cardDescriptionState = new CardDescriptionState();
    CardTagsState cardTagsState = new CardTagsState();
    CardSilhuetteState cardSilhuetteState = new CardSilhuetteState();
    CardIconsState cardIconsState = new CardIconsState();

    void Start()
    {
        currentCardState = cardTitleState;

        currentCardState.EnterState(this);
    }

    void Update()
    {
        
    }

    void SwitchState(CardBaseState state)
    {
        currentCardState.Exit(this);
        currentCardState = state;
        currentCardState.EnterState(this);
    }
}
