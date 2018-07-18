using UnityEngine;
using UnityEngine.UI;

public class SceneRoot : MonoBehaviour
{
    public Camera Camera;

    public Bootstrap Bootstrap;
    public DragHandlePanel DragHandler;

    public Slider Slider;
    public Text FpsText;
    public Text CountText;

    private void Start()
    {
        Application.targetFrameRate = -1;

        this.Bootstrap.Initialize();
        this.DragHandler.Initialize(this.Camera);
    }

    private void Update()
    {
        this.Camera.fieldOfView = 75f - 74f * this.Slider.value;

        var fps = 1f / Time.smoothDeltaTime;
        if (float.IsInfinity(fps) || fps < 0)
        {
            return;
        }

        this.FpsText.text = fps.ToString("00.000");

        if (fps < 29)
        {
            return;
        }

        this.Bootstrap.CreateParticle(Mathf.FloorToInt(fps));
        this.CountText.text = this.Bootstrap.Count.ToString();
    }
}