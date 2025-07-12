using UnityEngine;

public class Data : MonoBehaviour
{
    public bool VFX = true;
    public bool music = true;
    public bool sound = true;
    
    public static Data Instance { get; private set; }  

    void Awake()
    {
        if (Instance != null && Instance != this) {  
            Destroy(this);  
        }  
        else {  
            Instance = this;  
            DontDestroyOnLoad(this);
        }
    }

    public void ChangeVFX()
    {
        VFX = !VFX;
    }
    public void ChangeMusic()
    {
        music = !music;
    }
    public void ChangeSound()
    {
        sound = !sound;
    }
}
