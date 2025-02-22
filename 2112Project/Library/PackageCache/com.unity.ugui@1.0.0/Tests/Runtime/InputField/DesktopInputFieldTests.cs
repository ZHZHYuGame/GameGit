using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Reflection;

namespace InputfieldTests
{
    public class DesktopInputFieldTests : BaseInputFieldTests, IPrebuildSetup
    {
        protected const string kPrefabPath = "Assets/Resources/DesktopInputFieldPrefab.prefab";

        public void Setup()
        {
#if UNITY_EDITOR
            CreateInputFieldAsset(kPrefabPath);
#endif
        }

        [SetUp]
        public virtual void TestSetup()
        {
            m_PrefabRoot = UnityEngine.Object.Instantiate(Resources.Load("DesktopInputFieldPrefab")) as GameObject;

            FieldInfo inputModule = typeof(EventSystem).GetField("m_CurrentInputModule", BindingFlags.NonPublic | BindingFlags.Instance);
            inputModule.SetValue(m_PrefabRoot.GetComponentInChildren<EventSystem>(), m_PrefabRoot.GetComponentInChildren<FakeInputModule>());
        }

        [TearDown]
        public virtual void TearDown()
        {
            GUIUtility.systemCopyBuffer = null;
            FontUpdateTracker.UntrackText(m_PrefabRoot.GetComponentInChildren<Text>());
            GameObject.DestroyImmediate(m_PrefabRoot);
        }

        [OneTimeTearDown]
        public void OnetimeTearDown()
        {
#if UNITY_EDITOR
            AssetDatabase.DeleteAsset(kPrefabPath);
#endif
        }

<<<<<<< HEAD
        [Test]
        public void FocusOnPointerClickWithLeftButton()
=======
        [UnityTest]
<<<<<<< HEAD
=======
        [UnityPlatform(exclude = new[] { RuntimePlatform.Switch })] // Currently InputField.ActivateInputFieldInternal calls Switch SoftwareKeyboard screen ; without user input or a command to close the SoftwareKeyboard this blocks the tests suite
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        public IEnumerator FocusOnPointerClickWithLeftButton()
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        {
            InputField inputField = m_PrefabRoot.GetComponentInChildren<InputField>();
            PointerEventData data = new PointerEventData(m_PrefabRoot.GetComponentInChildren<EventSystem>());
            data.button = PointerEventData.InputButton.Left;
            inputField.OnPointerClick(data);

            MethodInfo lateUpdate = typeof(InputField).GetMethod("LateUpdate", BindingFlags.NonPublic | BindingFlags.Instance);
            lateUpdate.Invoke(inputField, null);

<<<<<<< HEAD
            Assert.IsTrue(inputField.isFocused);
=======
#if UNITY_GAMECORE && !UNITY_EDITOR
            if (TouchScreenKeyboard.isSupported)
            {
                // On Xbox, the onScreenKeyboard is going to constrain the application and make it go out of focus. 
                // We need to wait for the application to go out of focus before we can close the onScreenKeyboard.
                while (Application.isFocused)
                {
                    yield return null;
                }
            }
#endif

            Assert.IsTrue(inputField.isFocused);

#if UNITY_GAMECORE && !UNITY_EDITOR
            // On Xbox, we then need to close onScreenKeyboard and wait for the application to be focused again.
            // If this is not done, it could have an impact on subsequent tests that require the application to be focused in order to function correctly.
            if (!TouchScreenKeyboard.isSupported || !TouchScreenKeyboard.visible)
            {
                yield break;
            }

            while (!Application.isFocused)
            {
                if (inputField.touchScreenKeyboard != null)
                {
                    inputField.touchScreenKeyboard.active = false;
                }
                yield return null;
            }
#else
            yield break;
#endif
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        [UnityTest]
        public IEnumerator DoesNotFocusOnPointerClickWithRightOrMiddleButton()
        {
            InputField inputField = m_PrefabRoot.GetComponentInChildren<InputField>();
            PointerEventData data = new PointerEventData(m_PrefabRoot.GetComponentInChildren<EventSystem>());
            data.button = PointerEventData.InputButton.Middle;
            inputField.OnPointerClick(data);
            yield return null;

            data.button = PointerEventData.InputButton.Right;
            inputField.OnPointerClick(data);
            yield return null;

            Assert.IsFalse(inputField.isFocused);
        }
    }
}
