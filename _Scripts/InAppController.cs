using YG;
using UnityEngine;

public class InAppController : MonoBehaviour
{
    private void OnEnable()
	{
		YG2.onPurchaseSuccess += SuccessPurchased;
	}

	private void OnDisable()
	{
		YG2.onPurchaseSuccess -= SuccessPurchased;
	}

	private void SuccessPurchased(string id)
	{
		// Ваш код для обработки покупки, например:

		if (id == "adBlock")
		{
            YG2.saves.isAdBlock = true;
            YG2.StickyAdActivity(false);
        }
			
		YG2.SaveProgress();
	}

}
