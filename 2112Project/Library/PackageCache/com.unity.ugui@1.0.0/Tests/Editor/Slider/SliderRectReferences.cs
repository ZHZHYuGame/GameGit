using NUnit.Framework;
using UnityEngine.UI;
using UnityEngine;

[Category("Slider")]
<<<<<<< HEAD
public class SliderRectRefernces : TestBehaviourBase<UnityEngine.UI.Slider>
{
    private Slider slider;
    private GameObject emptyGO;

    [SetUp]
    public override void TestSetup()
    {
        base.TestSetup();

        var rootChildGO = new GameObject("root child");
        rootChildGO.AddComponent<Canvas>();
=======
public class SliderRectRefernces : Behaviour
{
    private Slider slider;
    private GameObject emptyGO;
    private GameObject rootGO;

    [SetUp]
    public void TestSetup()
    {
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
    public void AssigningSelfResultsInNullReferenceField()
    {
        slider.fillRect = (RectTransform)slider.transform;
        Assert.IsNull(slider.fillRect);

        slider.handleRect = (RectTransform)slider.transform;
        Assert.IsNull(slider.handleRect);
    }

    [Test]
    public void AssigningOtherObjectResultsInCorrectReferenceField()
    {
        slider.fillRect = (RectTransform)emptyGO.transform;
        Assert.IsNotNull(slider.fillRect);
        Assert.AreEqual(slider.fillRect, (RectTransform)emptyGO.transform);

        slider.handleRect = (RectTransform)emptyGO.transform;
        Assert.IsNotNull(slider.handleRect);
        Assert.AreEqual(slider.handleRect, (RectTransform)emptyGO.transform);
    }
}
