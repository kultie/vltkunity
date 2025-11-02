using UnityEngine;


public class AssetManager : MonoBehaviour
{
    private RuntimeAnimatorController splashAnimator;
    private RuntimeAnimatorController backgroundCharaterAnimator;
    private RuntimeAnimatorController characterAnimator;

    // Static singleton property.
    public static AssetManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    // Instance method, this method can be accessed through the //singleton instance
    public RuntimeAnimatorController getAnimatorSplash()
    {
        return splashAnimator;
    }

    public void setAnimatorSplash(RuntimeAnimatorController animator)
    {
        this.splashAnimator = animator;
    }



    public RuntimeAnimatorController getBackgroundCharaterAnimator()
    {
        return backgroundCharaterAnimator;
    }

    public void setBackgroundCharaterAnimator(RuntimeAnimatorController animator)
    {
        this.backgroundCharaterAnimator = animator;
    }

    public RuntimeAnimatorController getCharacterAnimator()
    {
        return characterAnimator;
    }

    public void setCharacterAnimator(RuntimeAnimatorController animator)
    {
        this.characterAnimator = animator;
    }
}
