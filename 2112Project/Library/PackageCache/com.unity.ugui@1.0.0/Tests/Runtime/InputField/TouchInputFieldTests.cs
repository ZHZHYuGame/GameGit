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
<<<<<<< HEAD
    [UnityPlatform(exclude = new RuntimePlatform[]
    {
        RuntimePlatform.Android /* case 1094042 */
    })]
=======
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
    public class TouchInputFieldTests : BaseInputFieldTests, IPrebuildSetup
    {
        protected const string kPrefabPath = "Assets/Resources/TouchInputFieldPrefab.prefab";

        public void Setup()
        {
#if UNITY_EDITOR
            CreateInputFieldAsset(kPrefabPath);
#endif
        }

        [SetUp]
        public void TestSetup()
        {
            m_PrefabRoot = UnityEngine.Object.Instantiate(Resources.Load("TouchInputFieldPrefab")) as GameObject;

            FieldInfo inputModule = typeof(EventSystem).GetField("m_CurrentInputModule", BindingFlags.NonPublic | BindingFlags.Instance);
            inputModule.SetValue(m_PrefabRoot.GetComponentInChildren<EventSystem>(), m_PrefabRoot.GetComponentInChildren<FakeInputModule>());
        }

        [TearDown]
        public void TearDown()
        {
            InputField inputField = m_PrefabRoot.GetComponentInChildren<InputField>();
            TouchScreenKeyboard.hideInput = false;
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

        protected const string kDefaultInputStr = "foobar";

        const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

        public struct CharValidationTestData
        {
            public string input, output;
            public InputField.CharacterValidation validation;

            public CharValidationTestData(string input, string output, InputField.CharacterValidation validation)
            {
                this.input = input;
                this.output = output;
                this.validation = validation;
            }

            public override string ToString()
            {
                // these won't properly show up if test runners UI if we don't replace it
                string input = this.input.Replace(kEmailSpecialCharacters, "specialchars");
                string output = this.output.Replace(kEmailSpecialCharacters, "specialchars");
                return string.Format("input={0}, output={1}, validation={2}", input, output, validation);
            }
        }

        [Test]
        [TestCase("*Azé09", "*Azé09", InputField.CharacterValidation.None)]
        [TestCase("*Azé09?.", "Az09", InputField.CharacterValidation.Alphanumeric)]
        [TestCase("Abc10x", "10", InputField.CharacterValidation.Integer)]
        [TestCase("-10", "-10", InputField.CharacterValidation.Integer)]
        [TestCase("10.0", "100", InputField.CharacterValidation.Integer)]
        [TestCase("10.0", "10.0", InputField.CharacterValidation.Decimal)]
        [TestCase(" -10.0x", "-10.0", InputField.CharacterValidation.Decimal)]
        [TestCase("10,0", "10,0", InputField.CharacterValidation.Decimal)]
        [TestCase(" -10,0x", "-10,0", InputField.CharacterValidation.Decimal)]
        [TestCase("A10,0 ", "10,0", InputField.CharacterValidation.Decimal)]
        [TestCase("A'a aaa  aaa", "A'a Aaa Aaa", InputField.CharacterValidation.Name)]
<<<<<<< HEAD
=======
        [TestCase("Unity-Editor", "Unity-Editor", InputField.CharacterValidation.Name)]
        [TestCase("Unity--Editor", "Unity-Editor", InputField.CharacterValidation.Name)]
        [TestCase("-UnityEditor", "Unityeditor", InputField.CharacterValidation.Name)]
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        [TestCase(" _JOHN*   (Doe)", "John Doe", InputField.CharacterValidation.Name)]
        [TestCase("johndoe@unity3d.com", "johndoe@unity3d.com", InputField.CharacterValidation.EmailAddress)]
        [TestCase(">john doe\\@unity3d.com", "johndoe@unity3d.com", InputField.CharacterValidation.EmailAddress)]
        [TestCase(kEmailSpecialCharacters + "@unity3d.com", kEmailSpecialCharacters + "@unity3d.com", InputField.CharacterValidation.EmailAddress)]
        public void HonorsCharacterValidationSettingsAssignment(string input, string output, InputField.CharacterValidation validation)
        {
            InputField inputField = m_PrefabRoot.GetComponentInChildren<InputField>();
            inputField.characterValidation = validation;
            inputField.text = input;
            Assert.AreEqual(output, inputField.text, string.Format("Failed character validation: input ={0}, output ={1}, validation ={2}",
                input.Replace(kEmailSpecialCharacters, "specialchars"),
                output.Replace(kEmailSpecialCharacters, "specialchars"),
                validation));
        }

        [UnityTest]
        [TestCase("*Azé09", "*Azé09", InputField.CharacterValidation.None, ExpectedResult = null)]
        [TestCase("*Azé09?.", "Az09", InputField.CharacterValidation.Alphanumeric, ExpectedResult = null)]
        [TestCase("Abc10x", "10", InputField.CharacterValidation.Integer, ExpectedResult = null)]
        [TestCase("-10", "-10", InputField.CharacterValidation.Integer, ExpectedResult = null)]
        [TestCase("10.0", "100", InputField.CharacterValidation.Integer, ExpectedResult = null)]
        [TestCase("10.0", "10.0", InputField.CharacterValidation.Decimal, ExpectedResult = null)]
        [TestCase(" -10.0x", "-10.0", InputField.CharacterValidation.Decimal, ExpectedResult = null)]
        [TestCase("10,0", "10,0", InputField.CharacterValidation.Decimal, ExpectedResult = null)]
        [TestCase(" -10,0x", "-10,0", InputField.CharacterValidation.Decimal, ExpectedResult = null)]
        [TestCase("A10,0 ", "10,0", InputField.CharacterValidation.Decimal, ExpectedResult = null)]
        [TestCase("A'a aaa  aaa", "A'a Aaa Aaa", InputField.CharacterValidation.Name, ExpectedResult = null)]
        [TestCase(" _JOHN*   (Doe)", "John Doe", InputField.CharacterValidation.Name, ExpectedResult = null)]
        [TestCase("johndoe@unity3d.com", "johndoe@unity3d.com", InputField.CharacterValidation.EmailAddress, ExpectedResult = null)]
        [TestCase(">john doe\\@unity3d.com", "johndoe@unity3d.com", InputField.CharacterValidation.EmailAddress, ExpectedResult = null)]
        [TestCase(kEmailSpecialCharacters + "@unity3d.com", kEmailSpecialCharacters + "@unity3d.com", InputField.CharacterValidation.EmailAddress, ExpectedResult = null)]
        public IEnumerator HonorsCharacterValidationSettingsTypingWithSelection(string input, string output, InputField.CharacterValidation validation)
        {
            if (!TouchScreenKeyboard.isSupported)
                yield break;
            InputField inputField = m_PrefabRoot.GetComponentInChildren<InputField>();
            BaseEventData eventData = new BaseEventData(m_PrefabRoot.GetComponentInChildren<EventSystem>());
            inputField.characterValidation = validation;
            inputField.text = input;

            inputField.OnSelect(eventData);
<<<<<<< HEAD
            yield return null;
=======

#if UNITY_GAMECORE && !UNITY_EDITOR
<<<<<<< HEAD
            // On Xbox, the onScreenKeyboard is going to constrain the application and make it go out of focus. 
=======
            // On Xbox, the onScreenKeyboard is going to constrain the application and make it go out of focus.
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
            // We need to wait for the application to go out of focus before we can close the onScreenKeyboard.
            while (Application.isFocused)
            {
                yield return null;
            }
#else
            yield return null;
#endif
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

            Assert.AreEqual(output, inputField.text, string.Format("Failed character validation: input ={0}, output ={1}, validation ={2}",
                input.Replace(kEmailSpecialCharacters, "specialchars"),
                output.Replace(kEmailSpecialCharacters, "specialchars"),
                validation));
<<<<<<< HEAD
=======

#if UNITY_GAMECORE && !UNITY_EDITOR
            // On Xbox, we then need to close onScreenKeyboard and wait for the application to be focused again.
            // If this is not done, it could have an impact on subsequent tests that require the application to be focused in order to function correctly.
            while (!Application.isFocused)
            {
                if (inputField.touchScreenKeyboard != null)
                {
                    inputField.touchScreenKeyboard.active = false;
                }
                yield return null;
            }
#endif
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        [Test]
        public void AssignmentAgainstCharacterLimit([Values("ABC", "abcdefghijkl")] string text)
        {
            InputField inputField = m_PrefabRoot.GetComponentInChildren<InputField>();
            // test assignment
            inputField.characterLimit = 5;
            inputField.text = text;
            Assert.AreEqual(text.Substring(0, Math.Min(text.Length, inputField.characterLimit)), inputField.text);
        }

        [Test] // regression test 793119
        public void AssignmentAgainstCharacterLimitWithContentType([Values("Abc", "Abcdefghijkl")] string text)
        {
            InputField inputField = m_PrefabRoot.GetComponentInChildren<InputField>();
            // test assignment
            inputField.characterLimit = 5;
            inputField.contentType = InputField.ContentType.Name;
            inputField.text = text;
            Assert.AreEqual(text.Substring(0, Math.Min(text.Length, inputField.characterLimit)), inputField.text);
        }

        [UnityTest]
        public IEnumerator SendsEndEditEventOnDeselect()
        {
            InputField inputField = m_PrefabRoot.GetComponentInChildren<InputField>();
            BaseEventData eventData = new BaseEventData(m_PrefabRoot.GetComponentInChildren<EventSystem>());
            inputField.OnSelect(eventData);
<<<<<<< HEAD
            yield return null;
=======

#if UNITY_GAMECORE && !UNITY_EDITOR
            while (Application.isFocused)
            {
                yield return null;
            }
#else
            yield return null;
#endif
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            var called = false;
            inputField.onEndEdit.AddListener((s) => { called = true; });

            inputField.OnDeselect(eventData);

<<<<<<< HEAD
=======
#if UNITY_GAMECORE && !UNITY_EDITOR
            while (!Application.isFocused)
            {
                if (inputField.touchScreenKeyboard != null)
                {
                    inputField.touchScreenKeyboard.active = false;
                }
                yield return null;
            }
#endif

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            Assert.IsTrue(called, "Expected invocation of onEndEdit");
        }

        [Test]
        public void StripsNullCharacters2()
        {
            InputField inputField = m_PrefabRoot.GetComponentInChildren<InputField>();
            inputField.text = "a\0b";
            Assert.AreEqual("ab", inputField.text, "\\0 characters should be stripped");
        }

        [UnityTest]
        public IEnumerator FocusOpensTouchScreenKeyboard()
        {
<<<<<<< HEAD
=======
            var isInPlaceEditingDisabled = typeof(TouchScreenKeyboard).GetProperty("disableInPlaceEditing",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            isInPlaceEditingDisabled.SetValue(null, true);

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (!TouchScreenKeyboard.isSupported)
                yield break;
            InputField inputField = m_PrefabRoot.GetComponentInChildren<InputField>();
            BaseEventData eventData = new BaseEventData(m_PrefabRoot.GetComponentInChildren<EventSystem>());
            inputField.OnSelect(eventData);
<<<<<<< HEAD
            yield return null;

            Assert.NotNull(inputField.touchScreenKeyboard, "Expect a keyboard to be opened");
=======

#if UNITY_GAMECORE && !UNITY_EDITOR
            while (Application.isFocused)
            {
                yield return null;
            }
#else
            yield return null;
#endif

            Assert.NotNull(inputField.touchScreenKeyboard, "Expect a keyboard to be opened");

#if UNITY_GAMECORE && !UNITY_EDITOR
            while (!Application.isFocused)
            {
                if (inputField.touchScreenKeyboard != null)
                {
                    inputField.touchScreenKeyboard.active = false;
                }
                yield return null;
            }
#endif
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        [UnityTest]
        public IEnumerator AssignsShouldHideInput()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                InputField inputField = m_PrefabRoot.GetComponentInChildren<InputField>();
                BaseEventData eventData = new BaseEventData(m_PrefabRoot.GetComponentInChildren<EventSystem>());

                inputField.shouldHideMobileInput = false;

                inputField.OnSelect(eventData);
                yield return null;

                Assert.IsFalse(inputField.shouldHideMobileInput);
                Assert.IsFalse(TouchScreenKeyboard.hideInput, "Expect TouchScreenKeyboard.hideInput to be set");
            }
        }
<<<<<<< HEAD
=======

        [UnityTest]
<<<<<<< HEAD
        [UnityPlatform(exclude = new[] { RuntimePlatform.IPhonePlayer, RuntimePlatform.PS4, RuntimePlatform.PS5 })]
=======
        [UnityPlatform(exclude = new[] { RuntimePlatform.IPhonePlayer, RuntimePlatform.PS4, RuntimePlatform.PS5, RuntimePlatform.VisionOS })] // disabled on visionOS due to UUM-61018
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        public IEnumerator IsTouchScreenKeyboardVisible()
        {
            if (!TouchScreenKeyboard.isSupported)
                yield break;

            InputField inputField = m_PrefabRoot.GetComponentInChildren<InputField>();
            BaseEventData eventData = new BaseEventData(m_PrefabRoot.GetComponentInChildren<EventSystem>());

            inputField.OnSelect(eventData);

#if UNITY_GAMECORE && !UNITY_EDITOR
            while (Application.isFocused)
            {
                yield return null;
            }
#else
            yield return null;
#endif

            Assert.IsTrue(TouchScreenKeyboard.visible);

            inputField.OnDeselect(eventData);

#if UNITY_GAMECORE && !UNITY_EDITOR
            while (!Application.isFocused)
            {
                if (inputField.touchScreenKeyboard != null)
                {
                    inputField.touchScreenKeyboard.active = false;
                }
                yield return null;
            }
#else
            yield return null;
#endif

            Assert.IsFalse(TouchScreenKeyboard.visible);
        }
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
    }
}
