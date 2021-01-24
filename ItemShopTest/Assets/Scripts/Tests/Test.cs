using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class Test
    {


        public InventoryManager GenerateTestInventoryManager()
        {
            InventoryGenerator testGenerator = new GameObject().AddComponent<InventoryGenerator>();
            InventoryManager testManager = new GameObject().AddComponent<InventoryManager>();
            testManager.tag = "InventoryManager";
            testManager.GoldAmmount = new GameObject();
            testManager.GoldAmmount.AddComponent<Text>();
            testManager.playerInventory = ScriptableObject.CreateInstance<InventoryScript>();
            testManager.InventoryUI = new GameObject();


            return testManager;
        }

        [UnityTest]
        public IEnumerator TestWithEnumeratorPasses()
        {
            var testInventoryManager = GenerateTestInventoryManager();
            var testCounter = new GameObject().AddComponent<Counter>();
            BaseItem testItem = Resources.Load<BaseItem>("Assets/ScriptableObjects/Items/Pot.asset");

            testCounter.GetComponent<Counter>().SetItem(testItem);



            Assert.AreEqual(testCounter.Item, testItem);

            yield return null;
            
        }
    }
}
