using UnityEngine;
using UnityEngine.UI;

enum ToggleVariant
{
    VFX,
    Music,
    Sound
}
public class ToggleScript : MonoBehaviour
{
    [SerializeField] ToggleVariant m_ToggleVariant;
    
    private Data _data;
    private Toggle _toggle;

    private void Start()
    {
        _data = Data.Instance;
        _toggle = GetComponent<Toggle>();
        
        switch (m_ToggleVariant)
        {
            case ToggleVariant.VFX:
                _toggle.isOn = _data.VFX;
                break;
            case ToggleVariant.Music:
                _toggle.isOn = _data.music;
                break;
            case ToggleVariant.Sound:
                _toggle.isOn = _data.sound;
                break;
        }
    }

    public void Toggler()
    {
        switch (m_ToggleVariant)
        {
            case ToggleVariant.VFX:
                _data.VFX = _toggle.isOn;
                break;
            case ToggleVariant.Music:
                _data.music = _toggle.isOn;
                break;
            case ToggleVariant.Sound:
                _data.sound = _toggle.isOn;
                break;
        }
    }
}
