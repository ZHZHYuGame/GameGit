using NUnit.Framework;
using UnityEngine.UI;
using UnityEngine;

[Category("Slider")]
public class SliderTests
{
    private Slider slider;
    private GameObject emptyGO;
<<<<<<< HEAD
=======
    private GameObject rootGO;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

    [SetUp]
    public void Setup()
    {
<<<<<<< HEAD
        var rootChildGO = new GameObject("root child");
        rootChildGO.AddComponent<Canvas>();
=======
        rootGO = new GameObject("root child");
        rootGO.AddComponent<Canvas>();
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

        var sliderGameObject = new GameObject("Slider");
        slider = sliderGameObject.AddComponent<Slider>();

        emptyGO = new GameObject("base", typeof(RectTransform));

<<<<<<< HEAD
        sliderGameObject.transform.SetParent(rootChildGO.transform);
        emptyGO.transform.SetParent(sliderGameObject.transform);
    }

=======
        sliderGameObject.transform.SetParent(rootGO.transform);
        emptyGO.transform.SetParent(sliderGameObject.transform);
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(rootGO);
    }

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
    [Test]
    public void SetSliderValueWithoutNotifyWillNotNotify()
    {
        slider.value = 0;

        bool calledOnValueChanged = false;

        slider.onValueChanged.AddListener(f => { calledOnValueChanged = true; });

        slider.SetValueWithoutNotify(1);

        Assert.IsTrue(slider.value == 1);
        Assert.IsFalse(calledOnValueChanged);
    }
}
