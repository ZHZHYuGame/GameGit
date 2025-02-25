using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;

/*
 This test checks that a maskableGraphic within a RectMask2D will be properly clipped.
 Test for case (1013182 - [RectMask2D] Child gameObject is masked if there are less than 2 corners of GO that matches RectMask's x position)
*/
namespace UnityEngine.UI.Tests
{
    public class RectMask2DClipping : IPrebuildSetup
    {
        GameObject m_PrefabRoot;
<<<<<<< HEAD
=======
        GameObject m_CameraGO;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

        const string kPrefabPath = "Assets/Resources/Mask2DRectCullingPrefab.prefab";

        public void Setup()
        {
#if UNITY_EDITOR
            var rootGO = new GameObject("RootGO");
            var rootCanvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler));
            rootCanvasGO.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            rootCanvasGO.transform.SetParent(rootGO.transform);

<<<<<<< HEAD
            var maskGO = new GameObject("Mask", typeof(RectMask2D), typeof(RectTransform));
=======
            var maskGO = new GameObject("Mask", typeof(RectTransform), typeof(RectMask2D));
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            var maskTransform = maskGO.GetComponent<RectTransform>();
            maskTransform.SetParent(rootCanvasGO.transform);
            maskTransform.localPosition = Vector3.zero;
            maskTransform.sizeDelta = new Vector2(200, 200);
            maskTransform.localScale = Vector3.one;

<<<<<<< HEAD
            var imageGO = new GameObject("Image", typeof(ImageHook), typeof(RectTransform));
=======
            var imageGO = new GameObject("Image", typeof(RectTransform), typeof(ImageHook));
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            var imageTransform = imageGO.GetComponent<RectTransform>();
            imageTransform.SetParent(maskTransform);
            imageTransform.localPosition = new Vector3(-125, 0, 0);
            imageTransform.sizeDelta = new Vector2(100, 100);
            imageTransform.localScale = Vector3.one;

            if (!Directory.Exists("Assets/Resources/"))
                Directory.CreateDirectory("Assets/Resources/");

            UnityEditor.PrefabUtility.SaveAsPrefabAsset(rootGO, kPrefabPath);
            GameObject.DestroyImmediate(rootGO);

#endif
        }

        [SetUp]
        public void TestSetup()
        {
            m_PrefabRoot = Object.Instantiate(Resources.Load("Mask2DRectCullingPrefab")) as GameObject;
<<<<<<< HEAD
            new GameObject("Camera", typeof(Camera));
=======
            m_CameraGO = new GameObject("Camera", typeof(Camera));
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        [UnityTest]
        public IEnumerator Mask2DRect_CorrectClipping()
        {
            //root->canvas->mask->image
            RectTransform t = m_PrefabRoot.transform.GetChild(0).GetChild(0).GetChild(0) as RectTransform;
            CanvasRenderer cr = t.GetComponent<CanvasRenderer>();
            bool cull = false;
            for (int i = 0; i < 360; i += 45)
            {
                t.localEulerAngles = new Vector3(0, 0, i);
                cull |= cr.cull;
                yield return null;
            }

            Assert.IsFalse(cull);
        }

        [Test]
        public void Mask2DRect_NonZeroPaddingMasksProperly()
        {
            var mask = m_PrefabRoot.GetComponentInChildren<RectMask2D>();
            mask.padding = new Vector4(10, 10, 10, 10);

            Canvas.ForceUpdateCanvases();

            var image = m_PrefabRoot.GetComponentInChildren<ImageHook>();

            // The mask rect is 200, and padding of 10 all around makes it so its 180 in size.
            Assert.AreEqual(180, image.cachedClipRect.height);
            Assert.AreEqual(180, image.cachedClipRect.width);

            Assert.AreEqual(-90f, image.cachedClipRect.x);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(m_PrefabRoot);
<<<<<<< HEAD
=======
            GameObject.DestroyImmediate(m_CameraGO);
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
#if UNITY_EDITOR
            AssetDatabase.DeleteAsset(kPrefabPath);
#endif
        }
    }
}
