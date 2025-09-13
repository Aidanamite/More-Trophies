using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;
using System;
using RaftModLoader;
using HMLLibrary;
using System.Linq;
using TMPro;
using System.Reflection;
using UnityEngine.UI;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;
using System.Text;
using System.Globalization;

namespace MoreTrophies
{
    public class Main : Mod
    {
        static List<Trophy> ItemsToAdd = new List<Trophy>
        {
            new Trophy(ItemManager.GetItemByName("Hammer"), FindModel.ByRendererName("Hammer"), new Vector3(0.1f, -0.3f, 0), new Vector3(-45, 90, 0), 1.5f, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Spear_Plank"), FindModel.ByRendererName("Spear_Wood"), Vector3.zero, new Vector3(-60, 90, 0), 1.5f, Size.Large ),
            new Trophy(ItemManager.GetItemByName("Spear_Scrap"), FindModel.ByRendererName("Spear_Scrap"), Vector3.zero, new Vector3(-60, 90, 0), 1.5f, Size.Large ),
            new Trophy(ItemManager.GetItemByName("Axe_Stone"), FindModel.ByRendererName("Axe_Stone"), new Vector3(0.2f, -0.4f, 0), new Vector3(-45, 90, 0), 1.5f, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Axe"), FindModel.ByRendererName("Axe_Scrap"), new Vector3(0.2f, -0.3f, 0), new Vector3(0, 0, 60), 1.5f, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Axe_Titanium"), FindModel.ByRendererName("Axe_Titanium"), new Vector3(0.3f, -0.3f, 0), new Vector3(-60, 90, 0), 1.5f, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("PaintBrush"), FindModel.ByRendererName("PaintBrush", includeParent: false), new Vector3(-0.1f, -0.3f, 0), Vector3.zero, 1.5f, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Machete"), FindModel.ByRendererName("Machete"), new Vector3(-0.4f, -0.2f, 0), new Vector3(60, 90, 0), 1.5f, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Sword_Titanium"), FindModel.ByRendererName("Sword_Titanium"), new Vector3(-0.35f,-0.2f,0), new Vector3(60,90,0), size: Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Hat_Pilot"), FindModel.ByRendererName("Hat_Pilot_Remote"), Vector3.zero, new Vector3(45, 0, 0), 1.5f ),
            new Trophy(ItemManager.GetItemByName("Hat_Tiki"), FindModel.ByRendererName("Hat_TikiMask_Remote"), new Vector3(0, 0, 0.1f), new Vector3(0, 0, 0), 1.5f ),
            new Trophy(ItemManager.GetItemByName("Hat_Captain"), FindModel.ByRendererName("Hat_CaptainsHat_Remote"), new Vector3(0, 0, 0.1f), new Vector3(90, 0, 0), 1.5f ),
            new Trophy(ItemManager.GetItemByName("Hat_Mayor"), FindModel.ByRendererName("Hat_MayorHat_Remote"), new Vector3(0, 0, 0.05f), new Vector3(90, 0, 0), 1.5f ),
            new Trophy(ItemManager.GetItemByName("Banana"), FindModel.ByRendererName("Banana"), new Vector3(0, -0.05f, 0), new Vector3(0, 90, 0), 1.5f ),
            new Trophy(ItemManager.GetItemByName("Shear"), FindModel.ByRendererName("Shear"), new Vector3(0.01f, 0, 0.01f), new Vector3(-30, 90, 90), 1.5f ),
            new Trophy(ItemManager.GetItemByName("Hat_Fishing"), FindModel.ByRendererName("Hat_Fishing_Remote"), new Vector3(0,0,0.07f), new Vector3(90,0,0), 1.5f ),
            new Trophy(ItemManager.GetItemByName("Hat_Sailor"), FindModel.ByRendererName("Hat_Sailor_Remote"), new Vector3(0,0,0.1f), new Vector3(30,0,0) ),
            new Trophy(ItemManager.GetItemByName("Hat_Diving"), FindModel.ByRendererName("Hat_Diving_Remote"), new Vector3(0,0,0.1f), Vector3.zero ),
            new Trophy(ItemManager.GetItemByName("Hat_Construction"), FindModel.ByRendererName("Hat_ConstructionHelmet_Remote"), new Vector3(0,0,0.05f), new Vector3(90,0,0) ),
            new Trophy(ItemManager.GetItemByName("Hat_Chef"), FindModel.ByRendererName("Hat_Chef_Remote"), new Vector3(0,0,0.1f), new Vector3(30,0,0) ),
            new Trophy(ItemManager.GetItemByName("Hat_Glasses_Aviator"), FindModel.ByRendererName("Hat_Glasses_Aviator_Remote"), Vector3.zero, Vector3.zero, 1.5f ),
            new Trophy(ItemManager.GetItemByName("Hat_Glasses_Disguise"), FindModel.ByRendererName("Hat_Glasses_Disguise_Remote"), Vector3.zero, Vector3.zero, 1.5f ),
            new Trophy(ItemManager.GetItemByName("Hat_Pirate"), FindModel.ByRendererName("Hat_Pirate_Remote"), Vector3.zero, new Vector3(90,0,0) ),
            new Trophy(ItemManager.GetItemByName("Cassette_Classical"), FindModel.ByRendererName("Cassette_Classic"), Vector3.zero, Vector3.zero, 0.75f ),
            new Trophy(ItemManager.GetItemByName("Cassette_EDM"), FindModel.ByRendererName("Cassette_EDM"), Vector3.zero, Vector3.zero, 0.75f ),
            new Trophy(ItemManager.GetItemByName("Cassette_Elevator"), FindModel.ByRendererName("Cassette_Elevator"), Vector3.zero, Vector3.zero, 0.75f ),
            new Trophy(ItemManager.GetItemByName("Cassette_Pop"), FindModel.ByRendererName("Cassette_Pop"), Vector3.zero, Vector3.zero, 0.75f ),
            new Trophy(ItemManager.GetItemByName("Cassette_Rock"), FindModel.ByRendererName("Cassette_Rock"), Vector3.zero, Vector3.zero, 0.75f ),
            new Trophy(ItemManager.GetItemByName("Cassette_TradingPost"), FindModel.ByRendererName("Cassette_TradingPost"), Vector3.zero, Vector3.zero, 0.75f ),
            new Trophy(ItemManager.GetItemByName("Shovel"), FindModel.ByRendererName("Shovel"), new Vector3(-0.7f,0.5f,0.1f), new Vector3(30,90,90), size: Size.Large ),
            new Trophy(ItemManager.GetItemByName("NetGun"), FindModel.ByRendererName("Model_NetGun"), new Vector3(-0.15f,-0.25f,0.05f), new Vector3(-30,90,0), size: Size.Medium ),
            new Trophy(ItemManager.GetItemByName("FishingBait_Simple"), FindModel.ByDelegate(() => {
                foreach (var h in Resources.FindObjectsOfTypeAll<FishingBaitHandler>())
                    foreach (var c in h.baitConnections)
                        if (c.bait.UniqueName == "FishingBait_Simple")
                            return (null, c.bobberMesh, new[] { c.bobberMaterial });
                return default; }), new Vector3(0,0.05f,0), Vector3.zero, 1.5f ),
            new Trophy(ItemManager.GetItemByName("FishingBait_Advanced"), FindModel.ByDelegate(() => {
                foreach (var h in Resources.FindObjectsOfTypeAll<FishingBaitHandler>())
                    foreach (var c in h.baitConnections)
                        if (c.bait.UniqueName == "FishingBait_Advanced")
                            return (null, c.bobberMesh, new[] { c.bobberMaterial });
                return default; }), new Vector3(0,0.05f,0), Vector3.zero, 1.5f ),
            new Trophy(ItemManager.GetItemByName("FishingBait_Expert"), FindModel.ByDelegate(() => {
                foreach (var h in Resources.FindObjectsOfTypeAll<FishingBaitHandler>())
                    foreach (var c in h.baitConnections)
                        if (c.bait.UniqueName == "FishingBait_Expert")
                            return (null, c.bobberMesh, new[] { c.bobberMaterial });
                return default; }), new Vector3(0,0.2f,0.02f), Vector3.zero, 1.5f ),
            new Trophy(ItemManager.GetItemByName("Rope"), FindModel.ByRendererName("RopeBundle"), new Vector3(-0.15f,0.18f,0.04f), new Vector3(0, 90, 0), 1.5f, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Hook_Plastic"), FindModel.ByMeshName("Hook_Plastic"), new Vector3(-0.35f,-0.3f,0), new Vector3(45, 90, 0), 1.5f, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Hook_Scrap"), FindModel.ByMeshName("Hook_Scrap"), new Vector3(-0.35f,-0.3f,0), new Vector3(45, 90, 0), 1.5f, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Hook_Titanium"), FindModel.ByMeshName("HookPart.006_L"), new Vector3(-0.35f,-0.3f,0), new Vector3(45, 90, 0), 1.5f, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Raw_Beet"), FindModel.ByRendererName("Raw_Beet"), Vector3.zero, new Vector3(0, 0, 45) ),
            new Trophy(ItemManager.GetItemByName("Raw_Potato"), FindModel.ByRendererName("Raw_Potato"), Vector3.zero, new Vector3(90, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Raw_Shark"), FindModel.ByRendererName("Raw_Shark"), new Vector3(0, -0.03f, 0), new Vector3(90, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Raw_Drumstick"), FindModel.ByRendererName("Raw_Drumstick"), Vector3.zero, new Vector3(0, 0, -45), 1.5f ),
            new Trophy(ItemManager.GetItemByName("Raw_GenericMeat"), FindModel.ByRendererName("Raw_GenericMeat"), new Vector3(-0.02f, 0, 0), new Vector3(-30, 90, 90), 0.75f ),
            new Trophy(ItemManager.GetItemByName("Compass"), () =>
            {
                var obj = Resources.FindObjectsOfTypeAll<Handheld_Compass>().LastOrDefault();
                if (!obj)
                    return null;
                var comp = Instantiate(obj, PrefabParent, true);
                comp.gameObject.SetActive(false);
                comp.name = "Compass";
                comp.transform.localPosition = new Vector3(0.02f, -0.12f, 0.115f);
                comp.transform.localRotation = Quaternion.Euler(-18, 180, 0);
                comp.gameObject.SetLayerRecursivly(0);
                return comp.gameObject;
            }),
            new Trophy(ItemManager.GetItemByName("Bow"), FindModel.ByMeshName("Bow"), new Vector3(0.11f, 0.25f, 0), new Vector3(52, -92, 0), 100, Size.Large ),
            new Trophy(ItemManager.GetItemByName("FishingRod"), FindModel.ByRendererName("Cylinder.019"), new Vector3(0.6f, -0.5f, 0), new Vector3(0, 0, 51.6f), size: Size.Large ),
            new Trophy(ItemManager.GetItemByName("FishingRod_Metal"), FindModel.ByRendererName("Cylinder.039"), new Vector3(0.6f, -0.5f, 0), new Vector3(0, 0, 51.6f), size: Size.Large ),
            new Trophy(ItemManager.GetItemByName("SharkBait"), () =>
            {
                var obj = Resources.FindObjectsOfTypeAll<SharkBait>().LastOrDefault();
                if (!obj)
                    return null;
                var nobj = new GameObject("SharkBait");
                nobj.SetActive(false);
                nobj.transform.SetParent(PrefabParent,false);
                foreach (Transform child in obj.transform)
                    if (child.GetComponent<MeshRenderer>() ||child.GetComponent<SkinnedMeshRenderer>())
                        Instantiate(child,nobj.transform);
                nobj.transform.localScale = Vector3.one * 1.5f;
                nobj.transform.localRotation = Quaternion.Euler(-30, 0, 0);
                nobj.transform.localPosition = new Vector3(-0.08f, -0.15f, 0.17f);
                return nobj;
            }, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Arrow_Stone"), FindModel.ByRendererName("Bow_ArrowModel_Stone"), new Vector3(0.27f, 0.15f, 0), new Vector3(-30, 90, 0), size: Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Arrow_Metal"), FindModel.ByRendererName("Bow_ArrowModel_Metal"), new Vector3(0.27f, 0.15f, 0), new Vector3(-30, 90, 0), size: Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Arrow_Titanium"), FindModel.ByRendererName("Bow_ArrowModel_Titanium"), new Vector3(0.27f, 0.15f, 0), new Vector3(-60, -90, 0), size: Size.Medium ),
            new Trophy(ItemManager.GetItemByName("OxygenBottle"), FindModel.ByMeshName("OxygenBottle"), new Vector3(0, -0.74f, -1.46f), new Vector3(62, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Binoculars"), FindModel.ByRendererName("Binoculars"), new Vector3(0, 0.05f, 0.02f), new Vector3(90, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Battery"), FindModel.ByMeshName("Battery"), new Vector3(0, -0.04f, 0.05f), new Vector3(0, 90, 0) ),
            new Trophy(ItemManager.GetItemByName("Battery_Advanced"), FindModel.ByMeshName("Battery2"), new Vector3(0, -0.1f, 0.05f), new Vector3(0, 90, 180) ),
            new Trophy(ItemManager.GetItemByName("NetCanister"), FindModel.ByMeshName("NetCanister"), new Vector3(0, -0.04f, 0.03f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Watermelon"), FindModel.ByRendererName("Watermelon"), new Vector3(0, 0, 0.1f), new Vector3(0, 0, 90), 0.75f ),
            new Trophy(ItemManager.GetItemByName("Strawberry"), FindModel.ByRendererName("Strawberry"), new Vector3(-0.01f, -0.02f, 0.02f), new Vector3(90, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Egg"), FindModel.ByRendererName("Egg"), new Vector3(0, -0.01f, 0.03f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Berries_Red"), FindModel.ByRendererName("Berries"), new Vector3(-0.07f, -0.07f, 0.03f), new Vector3(0, 0, 0), 0.6f ),
            new Trophy(ItemManager.GetItemByName("Pineapple"), FindModel.ByRendererName("Pineapple"), new Vector3(0, 0.01f, 0.03f), new Vector3(0, 0, 0), 0.75f ),
            new Trophy(ItemManager.GetItemByName("Mango"), FindModel.ByRendererName("Mango"), new Vector3(0, -0.01f, 0.03f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Coconut"), FindModel.ByRendererName("Coconut"), new Vector3(0, -0.01f, 0.03f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Jar_Honey"), FindModel.ByMeshName("HoneyJar"), new Vector3(0, -0.02f, 0.02f), new Vector3(0, 0, 0), 0.9f ),
            new Trophy(ItemManager.GetItemByName("CaveMushroom"), FindModel.ByRendererName("Mushroom"), new Vector3(0, -0.06f, -0.03f), new Vector3(-55, 180, 0), 0.15f ),
            new Trophy(ItemManager.GetItemByName("HealingSalve"), FindModel.ByRendererName("HealingSalve"), new Vector3(0, 0, -0.01f), new Vector3(90, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("HealingSalve_Good"), FindModel.ByRendererName("HealingSalve_Good"), new Vector3(0, 0, -0.01f), new Vector3(90, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Chili"), FindModel.ByRendererName("Chili"), new Vector3(0, 0, -0.015f), new Vector3(0, -90, -90) ),
            new Trophy(ItemManager.GetItemByName("Juniper"), FindModel.ByRendererName("Juniper"), new Vector3(-0.03f, 0, -0.01f), new Vector3(0, -50, -90) ),
            new Trophy(ItemManager.GetItemByName("Turmeric"), FindModel.ByRendererName("Turmeric"), new Vector3(0, 0, -0.01f), new Vector3(0, -90, -90) ),
            new Trophy(ItemManager.GetItemByName("Flipper"), FindModel.ByMeshName("Flippers"), new Vector3(-0.1f, 0.05f, 0), new Vector3(90, 0, 0), size: Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Bucket"), FindModel.ByRendererName("model_bucket"), new Vector3(-0.13f, 0.08f, 0.2f), new Vector3(0, 0, 0), 1.5f, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Plank"), FindModel.ByRendererName("FL_Plank1"), new Vector3(-0.1f, 0, 0), new Vector3(0, 0, 30), 0.45f, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Thatch"), FindModel.ByRendererName("Pickup_Floating_Thatch 1"), new Vector3(-0.12f, 0, 0.03f), new Vector3(90, 0, 0), size: Size.Medium )
            { AdditionalChanges = (nobj,obj) => {
                //nobj.GetComponent<MeshRenderer>().sharedMaterial.ShowData();
                var rend = nobj.GetComponent<MeshRenderer>();
                rend.sharedMaterial = Instantiate(rend.sharedMaterial);
                rend.sharedMaterial.SetColor("_ShimmerColor",Color.black);
            } },
            new Trophy(ItemManager.GetItemByName("SweepNet"), () =>
            {
                var obj = Resources.FindObjectsOfTypeAll<MeshRenderer>().First(x => x.name == "SweepNet_Pole").transform.parent.gameObject;
                if (!obj)
                    return null;
                var nobj = Instantiate(obj,PrefabParent,false);
                nobj.name = "SweepNet";
                nobj.SetActive(false);
                nobj.SetLayerRecursivly(0);
                //nobj.transform.localScale = Vector3.one * 1.5f;
                nobj.transform.localRotation = Quaternion.Euler(-60, 90, 5);
                nobj.transform.localPosition = new Vector3(0.8f, -0.4f, 0.1f);
                return nobj;
            }, Size.Large ),
            new Trophy(ItemManager.GetItemByName("MetalDetector"), FindModel.ByRendererName("MetalDetector"), new Vector3(-0.3f, 0.17f, 0.1f), new Vector3(20, 30, 60), size: Size.Large ),
            new Trophy(ItemManager.GetItemByName("PlasticCup_Empty"), FindModel.ByRendererName("Plastic_Cup", includeParent: false), new Vector3(0.05f, 0.04f, 0.05f), new Vector3(0, 90, 0) ),
            new Trophy(ItemManager.GetItemByName("PlasticBottle_Empty"), FindModel.ByRendererName("PlasticBottle"), new Vector3(0, -0.15f, 0.01f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Canteen_Empty"), FindModel.ByRendererName("Canteen"), new Vector3(0, -0.17f, 0.02f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Placeable_Brick_Wet"), FindModel.ByRendererName("Placeable_Brick_Wet"), new Vector3(-0.01f, -0.02f, -0.04f), new Vector3(90, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Brick_Dry"), FindModel.ByDelegate(() =>
            {
                var obj = Resources.FindObjectsOfTypeAll<Brick_Wet>().LastOrDefault();
                if (!obj)
                    return default;
                var go = Traverse.Create(obj).Field("filter").GetValue<MeshFilter>().gameObject;
                return (go,Traverse.Create(obj).Field("dryMesh").GetValue<Mesh>(),go.GetComponent<MeshRenderer>().sharedMaterials);
            }), new Vector3(-0.01f, -0.02f, -0.04f), new Vector3(90, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Feather"), FindModel.ByRendererName("model_feather"), new Vector3(0, 0, -0.01f), new Vector3(0, 0, 20) ),
            new Trophy(ItemManager.GetItemByName("BioFuel"), FindModel.ByRendererName("BioFuel"), new Vector3(0, 0, 0.03f), new Vector3(0, 0, 0), 0.5f ),
            new Trophy(ItemManager.GetItemByName("Mystery_Package"), FindModel.ByRendererName("MysteryPackage"), new Vector3(0, -0.01f, -0.04f), new Vector3(90, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("SilverAlgae"), FindModel.ByMeshName("SilverAlgae_Table"), new Vector3(-0.01f, 0.1f, 0), new Vector3(80, 90, 90), 0.15f ),
            new Trophy(ItemManager.GetItemByName("HeadLight"), FindModel.ByRendererName("HeadLight_Remote"), new Vector3(0, 0, 0), new Vector3(10, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("HeadLight_Advanced"), FindModel.ByRendererName("HeadLightAdvanced_Remote"), new Vector3(0, 0, 0.02f), new Vector3(95, 5, 0) ),
            new Trophy(ItemManager.GetItemByName("ZiplineTool"), () =>
            {
                if (!FindModel.ByRendererName("ZiplineTool").Get(out var obj,out _, out _))
                    return null;
                var nobj = new GameObject("ZiplineTool");
                nobj.SetActive(false);
                nobj.transform.SetParent(PrefabParent,false);
                foreach (Transform child in obj.transform.parent)
                    if (child.GetComponent<MeshRenderer>() ||child.GetComponent<SkinnedMeshRenderer>())
                        Instantiate(child,nobj.transform);
                nobj.SetLayerRecursivly(0);
                nobj.transform.localPosition = new Vector3(-0.1f, -0.2f, 0.08f);
                return nobj;
            }, Size.Medium),
            new Trophy(ItemManager.GetItemByName("ZiplineTool_Electric"), () =>
            {
                if (!FindModel.ByRendererName("ElectricZiplineTool").Get(out var obj,out _, out _))
                    return null;
                var nobj = new GameObject("ZiplineTool_Electric");
                nobj.SetActive(false);
                nobj.transform.SetParent(PrefabParent,false);
                foreach (Transform child in obj.transform.parent)
                    if (child.GetComponent<MeshRenderer>() ||child.GetComponent<SkinnedMeshRenderer>())
                        Instantiate(child,nobj.transform);
                nobj.SetLayerRecursivly(0);
                nobj.transform.localPosition = new Vector3(-0.1f, -0.2f, 0.08f);
                return nobj;
            }, Size.Medium),
            new Trophy(ItemManager.GetItemByName("Seed_Banana"), FindModel.ByRendererName("Seed_Pine"), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Seed_Birch"), FindModel.ByRendererName("Seed_Pine"), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Seed_Flower_Black"), FindModel.ByRendererName("Seed_Pine"), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Seed_Flower_Blue"), FindModel.ByRendererName("Seed_Pine"), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Seed_Flower_Red"), FindModel.ByRendererName("Seed_Pine"), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Seed_Flower_White"), FindModel.ByRendererName("Seed_Pine"), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Seed_Flower_Yellow"), FindModel.ByRendererName("Seed_Pine"), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Seed_Mango"), FindModel.ByRendererName("Seed_Pine"), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Seed_Palm"), FindModel.ByRendererName("Seed_Pine"), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Seed_Pine"), FindModel.ByRendererName("Seed_Pine"), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Seed_Pineapple"), FindModel.ByRendererName("Seed_Pine"), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Seed_Strawberry"), FindModel.ByRendererName("Seed_Pine"), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Seed_Watermelon"), FindModel.ByRendererName("Seed_Pine"), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("SeaVine"), FindModel.ByRendererName("Raw_Vine"), new Vector3(0, 0, 0.02f), new Vector3(30, 90, 90), new Vector3(1, 0.3f, 1) ),
            new Trophy(ItemManager.GetItemByName("Trashcube"), FindModel.ByRendererName("TrashCube"), new Vector3(0, 0, 0.05f), new Vector3(90, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Backpack"), FindModel.ByMeshName("Backpack_Male"), new Vector3(-0.11f, -1.34f, -0.12f), new Vector3(-90, 180, 0), size: Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Backpack_Advanced"), FindModel.ByMeshName("Backpack2_Male"), new Vector3(-0.11f, -0.05f, 0.1f), new Vector3(-90, 180, 0), size: Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Paddle"), FindModel.ByRendererName("Paddle"), new Vector3(0, 0, 0.05f), new Vector3(1, 1, 55), size: Size.Large ),
            new Trophy(ItemManager.GetItemByName("Plastic"), FindModel.ByBrushAsset("plastic land",1), new Vector3(-0.14f, -0.1f, 0.05f), new Vector3(0, 0, 0), size: Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Stone"), FindModel.ByBrushAsset("stone", 1), new Vector3(0, 0, 0), new Vector3(0, 90, 90) ),
            new Trophy(ItemManager.GetItemByName("Scrap"), FindModel.ByBrushAsset("scrap", 0), new Vector3(-0.15f, -0.05f, 0), new Vector3(90, 0, 0), 0.8f, Size.Medium )
            { 
                AdditionalChanges = (nobj,obj) =>
                {
                    if (!FindModel.ByBrushAsset("scrap", 1).Get(out _, out var mesh, out var mat))
                        return;
                    var model = new GameObject("ScrapPart");
                    model.transform.SetParent(nobj.transform,false);
                    for (int i = 0; i < mat.Length; i++)
                        (mat[i] = Instantiate(mat[i])).SetColor("_ShimmerColor",Color.black);
                    model.AddComponent<MeshRenderer>().sharedMaterials = mat;
                    model.AddComponent<MeshFilter>().sharedMesh = mesh;
                    model.transform.localRotation = Quaternion.Euler(0,-60,180);
                    model.transform.localScale = Vector3.one * 0.8f;
                    model.transform.localPosition = new Vector3(0.1f,0.05f,0.1f);
                }
            },
            new Trophy(ItemManager.GetItemByName("HoneyComb"), FindModel.ByBrushAsset("honey", 0), new Vector3(-0.12f, -0.07f, 0.2f), new Vector3(0, 105, 0), size: Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Placeable_GiantClam"), FindModel.ByBrushAsset("giantclam", 0), new Vector3(-0.05f, -0.42f, 0.13f), new Vector3(0, 90, 0), 2, Size.Large ),
            new Trophy(ItemManager.GetItemByName("MetalOre"), FindModel.ByBrushAsset("iron", 0), new Vector3(-0.13f, -0.07f, 0.08f), new Vector3(0, 30, 0), size: Size.Medium ),
            new Trophy(ItemManager.GetItemByName("TitaniumOre"), FindModel.ByRendererName("Raw_Titanium"), new Vector3(-0.15f, -0.05f, 0.1f), new Vector3(0, 180, 30), size: Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Clay"), FindModel.ByBrushAsset("clay", 0), new Vector3(-0.11f, -0.09f, 0), new Vector3(-30, -90, 90), 0.8f, Size.Medium ),
            new Trophy(ItemManager.GetItemByName("CopperOre"), FindModel.ByBrushAsset("copper", 0), new Vector3(-0.125f, -0.05f, 0.1f), new Vector3(45, 90, 0), new Vector3(0.75f, 1, 1), Size.Medium ),
            new Trophy(ItemManager.GetItemByName("Claybowl_Empty"), FindModel.ByRendererName("Bowl_Empty"), new Vector3(0, -0.03f, 0.06f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Claybowl_CoconutChicken"), FindModel.ByRendererName("Bowl_Coconut_Chicken"), new Vector3(0, -0.03f, 0.08f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Claybowl_HeadBroth"), FindModel.ByRendererName("Bowl_Head_Broth"), new Vector3(0, -0.03f, 0.08f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Claybowl_HeartyStew"), FindModel.ByRendererName("Bowl_HeartyStew_DeathPrevention"), new Vector3(0, -0.03f, 0.08f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Claybowl_Leftover"), FindModel.ByRendererName("Bowl_Leftover"), new Vector3(0, -0.03f, 0.06f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Claybowl_SimpleFishStew"), FindModel.ByRendererName("Bowl_Simple_Fish_Stew"), new Vector3(0, -0.03f, 0.08f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("Claybowl_RootVegetableSoup"), FindModel.ByRendererName("Bowl_VegetableSoup"), new Vector3(0, -0.03f, 0.06f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("ClayPlate_BBQ"), FindModel.ByRendererName("Plate_BBQ"), new Vector3(0, -0.03f, 0.08f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("ClayPlate_CatfishDeluxe"), FindModel.ByRendererName("Plate_CatfishDeluxe_GroundSpeed"), new Vector3(0, -0.03f, 0.08f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("ClayPlate_MushroomOmelette"), FindModel.ByRendererName("Plate_Mushroom_Omelette"), new Vector3(0, -0.03f, 0.08f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("ClayPlate_SalmonSalad"), FindModel.ByRendererName("Plate_SalmonSalad Oxygen"), new Vector3(0, -0.03f, 0.08f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("ClayPlate_SharkDinner"), FindModel.ByRendererName("Plate_Shark_Dinner"), new Vector3(0, -0.03f, 0.08f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("ClayPlate_SteakWithJam"), FindModel.ByRendererName("Plate_SteakWithJam"), new Vector3(0, -0.03f, 0.08f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("ClayPlate_Sushi"), FindModel.ByRendererName("Plate_Sushi"), new Vector3(0, -0.03f, 0.08f), new Vector3(30, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("DrinkingGlass"), FindModel.ByRendererName("DrinkingGlass"), new Vector3(0, -0.15f, 0.06f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("DrinkingGlass_CoconutBeat"), FindModel.ByRendererName("DrinkingGlass_CoconutBeat"), new Vector3(0, -0.15f, 0.06f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("DrinkingGlass_Leftover"), FindModel.ByRendererName("DrinkingGlass_LeftOver"), new Vector3(0, -0.15f, 0.06f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("DrinkingGlass_Mangonana"), FindModel.ByRendererName("DrinkingGlass_Mangonana"), new Vector3(0, -0.15f, 0.06f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("DrinkingGlass_RedbeetShot"), FindModel.ByRendererName("DrinkingGlass_RedbeetShot MaxHealth"), new Vector3(0, -0.15f, 0.06f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("DrinkingGlass_RedMelon"), FindModel.ByRendererName("DrinkingGlass_RedMelon"), new Vector3(0, -0.15f, 0.06f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("DrinkingGlass_SilverSmoothie"), FindModel.ByRendererName("DrinkingGlass_SilverSmoothie"), new Vector3(0, -0.15f, 0.06f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("DrinkingGlass_SimpleSmoothie"), FindModel.ByRendererName("DrinkingGlass_SimpleSmoothie"), new Vector3(0, -0.15f, 0.06f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("DrinkingGlass_SpicyPineberry"), FindModel.ByRendererName("DrinkingGlass_SpicyPineberry WaterSpeed"), new Vector3(0, -0.15f, 0.06f), new Vector3(0, 0, 0) ),
            new Trophy(ItemManager.GetItemByName("DrinkingGlass_StrawberryColada"), FindModel.ByRendererName("DrinkingGlass_StrawberryColada"), new Vector3(0, -0.15f, 0.06f), new Vector3(0, 0, 0) ),

            new Trophy(ItemManager.GetItemByName("Color_Black"), FindModel.ByRendererName("Black_Complete", includeParent: false), new Vector3(-0.01f, -0.07f, 0.04f), new Vector3(0, 150, 0) )
            {
                AdditionalChanges = (nobj,obj) =>
                {
                    foreach (Transform child in obj.transform)
                        if (child.GetComponent<MeshRenderer>() || child.GetComponent<SkinnedMeshRenderer>())
                            Instantiate(child,nobj.transform).gameObject.SetLayerRecursivly(0);
                }
            },
            new Trophy(ItemManager.GetItemByName("Color_White"), FindModel.ByRendererName("White_Complete", includeParent: false), new Vector3(-0.01f, -0.07f, 0.04f), new Vector3(0, -50, 0) )
            {
                AdditionalChanges = (nobj,obj) =>
                {
                    foreach (Transform child in obj.transform)
                        if (child.GetComponent<MeshRenderer>() || child.GetComponent<SkinnedMeshRenderer>())
                            Instantiate(child,nobj.transform).gameObject.SetLayerRecursivly(0);
                }
            },
            new Trophy(ItemManager.GetItemByName("Color_Red"), FindModel.ByRendererName("Red_Complete", includeParent: false), new Vector3(-0.01f, -0.07f, 0.04f), new Vector3(0, -110, 0) )
            {
                AdditionalChanges = (nobj,obj) =>
                {
                    foreach (Transform child in obj.transform)
                        if (child.GetComponent<MeshRenderer>() || child.GetComponent<SkinnedMeshRenderer>())
                            Instantiate(child,nobj.transform).gameObject.SetLayerRecursivly(0);
                }
            },
            new Trophy(ItemManager.GetItemByName("Color_Yellow"), FindModel.ByRendererName("Yellow_Complete", includeParent: false), new Vector3(-0.01f, -0.07f, 0.04f), new Vector3(0, 180, 0) )
            {
                AdditionalChanges = (nobj,obj) =>
                {
                    foreach (Transform child in obj.transform)
                        if (child.GetComponent<MeshRenderer>() || child.GetComponent<SkinnedMeshRenderer>())
                            Instantiate(child,nobj.transform).gameObject.SetLayerRecursivly(0);
                }
            },
            new Trophy(ItemManager.GetItemByName("Color_Blue"), FindModel.ByRendererName("Blue_Complete", includeParent: false), new Vector3(-0.01f, -0.07f, 0.04f), new Vector3(0, 110, 0) )
            {
                AdditionalChanges = (nobj,obj) =>
                {
                    foreach (Transform child in obj.transform)
                        if (child.GetComponent<MeshRenderer>() || child.GetComponent<SkinnedMeshRenderer>())
                            Instantiate(child,nobj.transform).gameObject.SetLayerRecursivly(0);
                }
            }
        };
        // csrun string str = ""; var search = "scrap"; foreach (var mesh in Resources.FindObjectsOfTypeAll<Renderer>()) if (!mesh.GetComponentInParent<Landmark>() && (mesh.name.ToLower().Contains(search) || (mesh.transform.parent && mesh.transform.parent.name.ToLower().Contains(search)))) str += "\n" + mesh.transform.parent?.name + " >> " + mesh + " >> " + (mesh.GetComponent<MeshFilter>()?.sharedMesh ?? (mesh as SkinnedMeshRenderer)?.sharedMesh)?.name; Debug.Log(str);
        // csrun Debug.Log(Object.FindObjectsOfType<MeshRenderer>().First(x => x.transform.name == "SilverAlgae").transform.localRotation = Quaternion.Euler(0,0,0));
        // csrun foreach (var i in ItemManager.GetAllItems()) if (i.UniqueName.Contains("Zip")) Debug.Log(i.UniqueName);
        // csrun var obj = Resources.FindObjectsOfTypeAll<MeshRenderer>().First(x => x.name == "ZiplineTool_hook").transform.parent; var str = obj.name + " >> " + obj.GetComponents<Component>().Join(x => x.GetType().FullName)); foreach (Transform child in ) str += "\n - " + child.name + " >> " + child.GetComponents<Component>().Join(x => x.GetType().FullName)); Debug.Log(str);
        public static Transform PrefabParent;
        Harmony harmony;
        public void Start()
        {
            PrefabParent = new GameObject("prefabParent").transform;
            DontDestroyOnLoad(PrefabParent.gameObject);
            PrefabParent.gameObject.SetActive(false);

            harmony = new Harmony("com.aidanamite.MoreTrophies");
            harmony.PatchAll();
            foreach (TrophyHolder holder in FindObjectsOfType<TrophyHolder>())
                try
                {
                    ModifyBoard(holder);
                }
                catch (Exception e) { Debug.LogError(e); }
            Log("Mod has been loaded!");
        }

        public void OnModUnload()
        {
            harmony?.UnpatchAll(harmony.Id);
            foreach (ItemObjectEnabler enabler in Resources.FindObjectsOfTypeAll<ItemObjectEnabler>())
            {
                var enablerConnections = Traverse.Create(enabler).Field<ItemModelConnection[]>("itemConnections");
                var connections = enablerConnections.Value.ToList();
                var changed = false;
                for (int i = connections.Count - 1; i >= 0; i--)
                    if (connections[i] is CustomModelConnection)
                    {
                        changed = true;
                        Destroy(connections[i].model);
                        connections.RemoveAt(i);
                    }
                if (changed)
                    enablerConnections.Value = connections.ToArray();
            }
            while (ItemsToAdd.RemoveAll(x => x.GeneratePrefab == null && x.Source == null && !(x.Linked != null && ItemsToAdd.Contains(x.Linked))) > 0) ;
            Destroy(PrefabParent.gameObject);
            Log("Mod has been unloaded!");
        }
        static HashSet<string> cookedFailed = new HashSet<string>();
        public static void ModifyBoard(TrophyHolder holder)
        {
            EnsureBlueprints();
            var enabler = Traverse.Create(holder).Field("itemObjectEnabler").GetValue<ItemObjectEnabler>();
            var size = Trophy.GetSize(enabler);
            var enablerConnections = Traverse.Create(enabler).Field<ItemModelConnection[]>("itemConnections");
            var connections = enablerConnections.Value.ToList();
            var linkedLookup = new Dictionary<Trophy, GameObject>();
            var contains = new HashSet<string>();
            var held = holder.GetCurrentItem()?.UniqueIndex ?? -1;
            foreach (var connection in connections)
            {
                foreach (var i in ItemsToAdd)
                    if (i.Item.UniqueIndex == connection.item.UniqueIndex)
                    {
                        linkedLookup[i] = connection.model;
                        break;
                    }
                contains.Add(connection.item.UniqueName);
            }
            void CreatePrefab(Trophy trophy, GameObject sourceObject, Material[] material, Mesh mesh)
            {
                var model = new GameObject(trophy.Item.UniqueName);
                model.SetActive(false);
                model.transform.SetParent(PrefabParent, false);
                model.AddComponent<MeshRenderer>().sharedMaterials = material;
                model.AddComponent<MeshFilter>().sharedMesh = mesh;
                model.transform.localPosition = new Vector3(0.03f, 0, 0.05f) + trophy.Position;
                model.transform.localRotation = trophy.Rotation;
                model.transform.localScale = trophy.Scale;
                trophy.AdditionalChanges?.Invoke(model, sourceObject);
                trophy.Prefab = model;
            }
            void MaybeAddTrophy(Trophy trophy)
            {
                try
                {
                    if (trophy.Item && !contains.Contains(trophy.Item.UniqueName))
                    {
                        var source = trophy;
                        while (source.Linked != null)
                            source = source.Linked;
                        if (size != source.Size)
                            return;
                        if (!source.Prefab)
                        {
                            if (source.GeneratePrefab != null)
                                source.Prefab = source.GeneratePrefab();
                            else if (source.Source == null)
                                Debug.LogError($"[More Trophies]: {trophy.Item.UniqueName}#{trophy.Size} has no way to generate a prefab. This should never happen");
                            else if (source.Source.Get(out var go, out var mesh, out var mat))
                                CreatePrefab(source, go, mat, mesh);
                        }
                        if (source.Prefab)
                        {
                            if (!linkedLookup.TryGetValue(source, out var o))
                                (linkedLookup[source] = o = Instantiate(source.Prefab, enabler.transform)).name = source.Prefab.name;
                            var connection = new CustomModelConnection() { item = trophy.Item, model = o };
                            if (trophy.Item.UniqueIndex == held)
                                connection.model.SetActive(true);
                            connections.Add(connection);
                            contains.Add(trophy.Item.UniqueName);
                        }
                        else
                            Debug.Log($"[More Trophies]: {trophy.Item.UniqueName}#{trophy.Size} is missing its prefab");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"[More Trophies]: {trophy?.Item?.UniqueName}#{trophy?.Size}\n{e}");
                }
            }
            foreach (var trophy in ItemsToAdd)
                if (trophy.Item && !contains.Contains(trophy.Item.UniqueName))
                    MaybeAddTrophy(trophy);
            for (var i = connections.Count - 1; i >= 0; i--)
            {
                var connection = connections[i];
                if (connection.item && connection.item.settings_cookable.CookingResult?.item)
                {
                    var nItem = connection.item.settings_cookable.CookingResult.item;
                    if (contains.Contains(nItem.UniqueName))
                        continue;
                    var renderer = connection.model.GetComponent<Renderer>();
                    if (!(renderer is MeshRenderer || renderer is SkinnedMeshRenderer))
                        continue;
                    if (cookedFailed.Contains(nItem.UniqueName) || !FindModel.ByRendererName(nItem.UniqueName).Get(out var source, out var mesh, out var mat))
                    {
                        cookedFailed.Add(nItem.UniqueName);
                        continue;
                    }

                    var model = Instantiate(connection.model,PrefabParent);
                    model.SetActive(false);
                    model.name = nItem.UniqueName;
                    var rend = model.GetComponent<Renderer>();
                    rend.sharedMaterials = mat;
                    if (rend is SkinnedMeshRenderer skin)
                        skin.sharedMesh = mesh;
                    else
                        model.GetComponent<MeshFilter>().sharedMesh = mesh;
                    var trophy = new Trophy(nItem, model, size);
                    ItemsToAdd.Add(trophy);
                    MaybeAddTrophy(trophy);
                }
            }
            enablerConnections.Value = connections.ToArray();
        }

        public static Vector3 Vector3Multiply(Vector3 a, Vector3 b) => new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);

        public static GameObject RemoveComponents(GameObject gO)
        {
            foreach (var c in gO.GetComponents<Component>())
                if (!(c is Transform || c is MeshRenderer || c is MeshFilter))
                    DestroyImmediate(c);
            return gO;
        }

        public static void LogTree(Transform transform)
        {
            Debug.Log(GetLogTree(transform));
        }

        public static string GetLogTree(Transform transform, string prefix = " -")
        {
            var str = "\n" + prefix + transform.name;
            foreach (var obj in transform.GetComponents<Component>())
                str += ": " + obj.GetType().Name;
            foreach (Transform sub in transform)
                str += GetLogTree(sub, prefix + "--");
            return str;
        }

        static HashSet<Item_Base> created = new HashSet<Item_Base>();
        static Trophy[] baseLink;
        public static void EnsureBlueprints()
        {
            foreach (var i in ItemManager.GetAllItems())
                if (i.UniqueName.StartsWith("Blueprint_") && created.Add(i))
                {
                    if (baseLink != null)
                    {
                        ItemsToAdd.AddRange(baseLink.Select(x => new Trophy(i, x)));
                        continue;
                    }

                    var main = Resources.FindObjectsOfTypeAll<BlueprintComponent>().First();
                    var small = Instantiate(main, PrefabParent).gameObject;
                    small.name = "SmallBlueprint";
                    small.SetLayerRecursivly(0);
                    small.SetActive(false);
                    small.GetComponentInChildren<Canvas>(true).renderMode = RenderMode.WorldSpace;
                    small.transform.localPosition = new Vector3(0.03f, 0, 0.07f);
                    small.transform.localRotation = Quaternion.identity;
                    small.transform.localScale = Vector3.one * 1.5f;
                    var medium = Instantiate(small, PrefabParent);
                    medium.name = "MediumBlueprint";
                    medium.transform.localPosition = new Vector3(-0.09f, 0, 0.07f);
                    medium.transform.localScale = Vector3.one * 3;
                    var large = Instantiate(small, PrefabParent);
                    large.name = "LargeBlueprint";
                    large.transform.localPosition = new Vector3(-0.03f, 0.1f, 0.1f);
                    large.transform.localScale = Vector3.one * 4.5f;

                    ItemsToAdd.AddRange(baseLink = new[]
                    {
                        new Trophy(i, small, Size.Small),
                        new Trophy(i, medium, Size.Medium),
                        new Trophy(i, large, Size.Large)
                    });
                }
        }

        public static T CreateObject<T>(Action<T> initialize = null)
        {
            var o = (T)FormatterServices.GetUninitializedObject(typeof(T));
            initialize?.Invoke(o);
            return o;
        }

#if false // Debug Commands - Disabled for general release
        [ConsoleCommand("moretrophiesdebug.findallbyname")]
        static void MyCommand(string[] args) => findbynameshared(Resources.FindObjectsOfTypeAll<Renderer>(), args);

        [ConsoleCommand("moretrophiesdebug.findbyname")]
        static void MyCommand2(string[] args) => findbynameshared(FindObjectsOfType<Renderer>(), args);

        static void findbynameshared(Renderer[] collection, string[] args)
        {
            StringBuilder str = new StringBuilder();
            var search = args.Join(delimiter: " ").ToLowerInvariant();
            foreach (var mesh in collection)
                if (
                    (mesh is SkinnedMeshRenderer || mesh.GetComponent<MeshFilter>())
                    && !mesh.GetAnyComponentInParent<Landmark>()
                    && (
                        mesh.name.ToLower().Contains(search)
                        || (mesh.transform.parent && mesh.transform.parent.name.ToLowerInvariant().Contains(search))
                    )
                )
                {
                    str.Append("\n");
                    if (mesh.transform.parent)
                        str.AppendUnityObject(mesh.transform.parent,false)
                            .Append(" >> ");
                    str.AppendUnityObject(mesh)
                        .Append(" >> ")
                        .AppendUnityObject(mesh is SkinnedMeshRenderer skin ? skin.sharedMesh : mesh.GetComponent<MeshFilter>().sharedMesh, false);
                }
            Debug.Log(str.ToString());
        }

        [ConsoleCommand("moretrophiesdebug.adjusttrophy")]
        static void MyCommand3(string[] args)
        {
            var trophy = ItemsToAdd.Find(x => x.Item.UniqueName == args[0]);
            if (trophy == null)
            {
                Debug.LogError($"No custom trophy was found for {args[0]}");
                return;
            }
            if (!trophy.Prefab)
            {
                Debug.LogError($"Trophy prefab for {args[0]} has not been initialized");
                return;
            }
            bool LogTryParse(string val, out float value)
            {
                if (float.TryParse(val, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                    return true;
                Debug.LogError($"Could not parse {val} to float");
                return false;
            }
            if (args[1] == "getpos")
            {
                var val = trophy.Prefab.transform.localPosition;
                if (trophy.Source != null)
                    val -= new Vector3(0.03f, 0, 0.05f);
                Debug.Log($"{val.x}, {val.y}, {val.z}");
                return;
            }
            else if (args[1] == "getrot")
            {
                Debug.Log($"{trophy.Prefab.transform.localEulerAngles.x}, {trophy.Prefab.transform.localEulerAngles.y}, {trophy.Prefab.transform.localEulerAngles.z}");
                return;
            }
            else if (args[1] == "getscale")
            {
                Debug.Log($"{trophy.Prefab.transform.localScale.x}, {trophy.Prefab.transform.localScale.y}, {trophy.Prefab.transform.localScale.z}");
                return;
            }
            else if (args[1] == "setpos" || args[1] == "addpos" || args[1] == "setrot" || args[1] == "setscale")
            {
                if (LogTryParse(args[2], out var x) & LogTryParse(args[3], out var y) & LogTryParse(args[4], out var z))
                {
                    if (args[1] == "setrot")
                        trophy.Prefab.transform.localEulerAngles = new Vector3(x, y, z);
                    else if (args[1] == "setscale")
                        trophy.Prefab.transform.localScale = new Vector3(x, y, z);
                    else
                        trophy.Prefab.transform.localPosition = (args[1] == "addpos" ? trophy.Prefab.transform.localPosition : trophy.Source == null ? Vector3.zero : new Vector3(0.03f, 0, 0.05f)) + new Vector3(x, y, z);
                }
                else
                    return;
            }
            else
            {
                Debug.LogError($"Command {args[1]} not recognised");
                return;
            }
            foreach (var holder in Resources.FindObjectsOfTypeAll<TrophyHolder>())
                foreach (var connect in Traverse.Create(holder).Field("itemObjectEnabler").GetValue<ItemObjectEnabler>().GetObjectConnections())
                    if (connect.item.UniqueName == trophy.Item.UniqueName)
                    {
                        connect.model.transform.localPosition = trophy.Prefab.transform.localPosition;
                        connect.model.transform.localScale = trophy.Prefab.transform.localScale;
                        connect.model.transform.localRotation = trophy.Prefab.transform.localRotation;
                    }
        }

        [ConsoleCommand("moretrophiesdebug.finditembyname")]
        static void MyCommand4(string[] args)
        {
            var str = new StringBuilder();
            var arg = args[0];
            str.AppendJoin(ItemManager.GetAllItems(), x => x.UniqueName, "\n", x => x.UniqueName.Contains(arg));
            if (str.Length == 0)
                Debug.LogWarning("No items found");
            else
                Debug.Log(str.ToString());
        }

        [ConsoleCommand("moretrophiesdebug.testspawnrenderer")]
        static void MyCommand5(string[] args) => testspawnshared(FindModel.ByRendererName(args.Join(delimiter: " ")));

        [ConsoleCommand("moretrophiesdebug.testspawnmesh")]
        static void MyCommand6(string[] args) => testspawnshared(FindModel.ByMeshName(args.Join(delimiter: " ")));

        [ConsoleCommand("moretrophiesdebug.testspawnrendererparent")]
        static void MyCommand7(string[] args) => testspawnshared(FindModel.ByRendererName(args.Join(delimiter: " "),includeSelf: false));

        [ConsoleCommand("moretrophiesdebug.testspawnrendererself")]
        static void MyCommand8(string[] args) => testspawnshared(FindModel.ByRendererName(args.Join(delimiter: " "), includeParent: false));

        [ConsoleCommand("moretrophiesdebug.testspawnbrush")]
        static void MyCommand9(string[] args) => testspawnshared(FindModel.ByBrushAsset(args.Skip(1).Join(delimiter: " "), int.Parse(args[0])));

        static void testspawnshared(FindModel finder)
        {
            if (!finder.Get(out _, out var mesh, out var mat))
            {
                Debug.LogError("Nothing found");
                return;
            }
            var obj = new GameObject("testObj");
            Destroy(obj, 10);
            obj.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
            obj.AddComponent<MeshRenderer>().sharedMaterials = mat;
            obj.AddComponent<MeshFilter>().sharedMesh = mesh;
        }
#endif
    }

    public class BlueprintTrophy : MonoBehaviour
    {
        void OnEnable()
        {
            var holder = this.GetAnyComponentInParent<Block>().GetComponentInChildren<TrophyHolder>(true);
            var blueprint = GetComponent<BlueprintComponent>();
            if (blueprint && holder && holder.GetCurrentItem())
                blueprint.SetBlueprint(Main.CreateObject<Message_Item>(x => x.itemIndex = holder.GetCurrentItem().UniqueIndex));
        }
    }

    public class Trophy
    {
        public readonly Item_Base Item;
        public readonly FindModel Source;
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;
        public readonly Vector3 Scale;
        public readonly Size Size;
        public readonly Func<GameObject> GeneratePrefab;
        public Action<GameObject, GameObject> AdditionalChanges;


        public GameObject Prefab;
        public readonly Trophy Linked;

        public Trophy(Item_Base item, Trophy linked)
        {
            Item = item;
            Linked = linked;
        }
        public Trophy(Item_Base item, Func<GameObject> prefabGenerator, Size size = Size.Small)
        {
            Item = item;
            GeneratePrefab = prefabGenerator;
            Size = size;
        }
        public Trophy(Item_Base item, GameObject prefab, Size size = Size.Small)
        {
            Item = item;
            Prefab = prefab;
            Size = size;
        }
        public Trophy(Item_Base item, FindModel source, Vector3 position, Quaternion rotation, Vector3 scale, Size size = Size.Small)
        {
            Item = item;
            Source = source;
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Size = size;
        }
        public Trophy(Item_Base item, FindModel source, Vector3 position, Vector3 rotation, Vector3 scale, Size size = Size.Small) : this(item, source, position, Quaternion.Euler(rotation), scale, size) { }
        public Trophy(Item_Base item, FindModel source, Vector3 position, Quaternion rotation, float scale = 1f, Size size = Size.Small) : this(item, source, position, rotation, Vector3.one * scale, size) { }
        public Trophy(Item_Base item, FindModel source, Vector3 position, Vector3 rotation, float scale = 1f, Size size = Size.Small) : this(item, source, position, Quaternion.Euler(rotation), Vector3.one * scale, size) { }


        public static Size GetSize(ItemObjectEnabler obj)
        {
            foreach (ItemModelConnection connection in obj.GetObjectConnections())
            {
                if (connection.item.UniqueName == "Head_Shark")
                    return Size.Large;
                if (connection.item.UniqueName == "Raw_Salmon")
                    return Size.Medium;
                if (connection.item.UniqueName == "Raw_Tilapia")
                    return Size.Small;
            }

            return Size.None;
        }
    }
    public enum Size
    {
        None, Small, Medium, Large
    }
    public abstract class FindModel
    {
        public abstract bool Get(out GameObject source, out Mesh mesh, out Material[] material);

        public static IEnumerable<Renderer> AllMeshRenderers()
        {
            foreach (var renderer in Resources.FindObjectsOfTypeAll<Renderer>())
            {
                if (!cache.TryGetValue(renderer, out var state))
                    cache.Add(renderer, state = !renderer.transform.IsChildOf(Main.PrefabParent) && (renderer is SkinnedMeshRenderer || renderer.GetComponent<MeshFilter>()));
                if ((bool)state)
                    yield return renderer;
            }
            yield break;
        }
        static ConditionalWeakTable<Renderer,object> cache = new ConditionalWeakTable<Renderer, object>();

        public static void GetMesh(Renderer renderer, out GameObject source, out Mesh mesh, out Material[] material)
        {
            source = renderer.gameObject;
            material = renderer.sharedMaterials;
            mesh = (renderer as SkinnedMeshRenderer)?.sharedMesh ?? renderer.GetComponent<MeshFilter>().sharedMesh;
        }

        public abstract class CheckAll : FindModel
        {
            public override bool Get(out GameObject source, out Mesh mesh, out Material[] material)
            {
                foreach (var renderer in AllMeshRenderers())
                    if (Condition(renderer))
                    {
                        GetMesh(renderer, out source, out mesh, out material);
                        return true;
                    }
                source = null;
                mesh = null;
                material = null;
                return false;
            }
            public abstract bool Condition(Renderer renderer);
        }
        public static FindModel ByRendererName(string name, bool includeSelf = true, bool includeParent = true) => new _ByRendererName(name, includeSelf, includeParent);
        public class _ByRendererName : CheckAll
        {
            string name;
            bool includeSelf;
            bool includeParent;
            public _ByRendererName(string name, bool includeSelf = true, bool includeParent = true) { this.name = name; this.includeSelf = includeSelf; this.includeParent = includeParent; }
            public override bool Condition(Renderer renderer) => (includeSelf && name == renderer.name) || (includeParent && renderer.transform.parent && name == renderer.transform.parent.name);
        }
        public static FindModel ByMeshName(string name) => new _ByMeshName(name);
        public class _ByMeshName : CheckAll
        {
            string name;
            public _ByMeshName(string name) => this.name = name;
            public override bool Condition(Renderer renderer)
            {
                var mesh = renderer is SkinnedMeshRenderer skin ? skin.sharedMesh : renderer.GetComponent<MeshFilter>()?.sharedMesh;
                return mesh && mesh.name == name;
            }
        }
        public static FindModel ByPredicate(Predicate<Renderer> condition) => new _ByPredicate(condition);
        public class _ByPredicate : CheckAll
        {
            Predicate<Renderer> condition;
            public _ByPredicate(Predicate<Renderer> condition) => this.condition = condition;
            public override bool Condition(Renderer renderer) => condition(renderer);
        }

        public static FindModel ByDelegate(Func<(GameObject, Mesh, Material[])> func) => new _ByDelegate(func);
        public class _ByDelegate : FindModel
        {
            Func<(GameObject, Mesh, Material[])> func;
            public _ByDelegate(Func<(GameObject, Mesh, Material[])> func) => this.func = func;
            public override bool Get(out GameObject source, out Mesh mesh, out Material[] material)
            {
                var t = func();
                if (!t.Item2 || t.Item3 == null || t.Item3.Length == 0)
                {
                    source = null;
                    mesh = null;
                    material = null;
                    return false;
                }
                (source, mesh, material) = t;
                return true;
            }
        }

        public static FindModel ByBrushAsset(string asset, int prefabIndex) => new _ByBrushAsset(asset,prefabIndex);
        public class _ByBrushAsset : FindModel
        {
            string asset;
            int prefabIndex;
            public _ByBrushAsset(string asset, int prefabIndex) { this.asset = asset; this.prefabIndex = prefabIndex; }
            public override bool Get(out GameObject source, out Mesh mesh, out Material[] material)
            {
                source = null;
                mesh = null;
                material = null;
                if (prefabIndex < 0)
                    return false;
                var brush = Resources.Load<SO_Brush>(asset);
                if (!brush || brush.prefabs.Count == 0 || brush.prefabs.Count <= prefabIndex)
                    return false;
                var obj = brush.prefabs[prefabIndex];
                if (!obj)
                    return false;
                var renderer = obj.GetComponentsInChildren<Renderer>(true).FirstOrDefault(x => x && (x is SkinnedMeshRenderer || (x is MeshRenderer && x.GetComponent<MeshFilter>())));
                if (!renderer)
                    return false;
                source = obj.gameObject;
                material = renderer.sharedMaterials;
                for (int i = 0; i < material.Length; i++)
                if (material[i].HasProperty("_ShimmerColor") && material[i].GetColor("_ShimmerColor") != Color.black)
                {
                    material[i] = Object.Instantiate(material[i]);
                    material[i].SetColor("_ShimmerColor", Color.black);
                }
                mesh = renderer is SkinnedMeshRenderer skin ? skin.sharedMesh : renderer.GetComponent<MeshFilter>().sharedMesh;
                return true;
            }
        }
    }

    [HarmonyPatch(typeof(TrophyHolder), "Start")]
    public class Patch_TrophyHolder
    {
        static void Postfix(TrophyHolder __instance)
        {
            Main.ModifyBoard(__instance);
        }
    }

    [HarmonyPatch]
    public class Patch_SetActiveModel
    {
        static IEnumerable<MethodBase> TargetMethods() => typeof(ItemObjectEnabler).GetMethods(~BindingFlags.Default).Where(x => x.Name == "ActivateModel");

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator iL)
        {
            var code = instructions.ToList();
            var loc = iL.DeclareLocal(typeof(GameObject));
            code.InsertRange(0, new[]
            {
                new CodeInstruction(OpCodes.Ldnull),
                new CodeInstruction(OpCodes.Stloc, loc)
            });
            code.InsertRange(code.FindIndex(x => x.operand is MethodInfo m && m.Name == "SetActive"), new[]
            {
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldloca, loc),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Patch_SetActiveModel),nameof(OverrideSetActive)))
            });
            return code;
        }

        static bool OverrideSetActive(bool original, ItemModelConnection obj, ref GameObject memory)
        {
            if (!original)
                return memory && obj.model == memory;
            memory = obj.model;
            return original;
        }
    }

    [Serializable]
    public class CustomModelConnection : ItemModelConnection { }

    public static class ExtentionMethods
    {
        static FieldInfo _nameText = AccessTools.Field(typeof(BlueprintComponent), "nameText");
        static FieldInfo _descText = AccessTools.Field(typeof(BlueprintComponent), "descText");
        static FieldInfo _blueprintImage = AccessTools.Field(typeof(BlueprintComponent), "blueprintImage");
        static FieldInfo _blueprintConnections = AccessTools.Field(typeof(BlueprintComponent), "blueprintConnections");
        public static void SetNameText(this BlueprintComponent component, TextMeshProUGUI text) => _nameText.SetValue(component, text);
        public static TextMeshProUGUI GetNameText(this BlueprintComponent component) => (TextMeshProUGUI)_nameText.GetValue(component);
        public static void SetDescText(this BlueprintComponent component, TextMeshProUGUI text) => _descText.SetValue(component, text);
        public static TextMeshProUGUI GetDescText(this BlueprintComponent component) => (TextMeshProUGUI)_descText.GetValue(component);
        public static void SetBlueprintImage(this BlueprintComponent component, Image image) => _blueprintImage.SetValue(component, image);
        public static Image GetBlueprintImage(this BlueprintComponent component) => (Image)_blueprintImage.GetValue(component);
        public static void SetBlueprintConnections(this BlueprintComponent component, List<BlueprintConnection> connections) => _blueprintConnections.SetValue(component, connections);
        public static List<BlueprintConnection> GetBlueprintConnections(this BlueprintComponent component) => (List<BlueprintConnection>)_blueprintConnections.GetValue(component);

        public static T GetAnyComponentInParent<T>(this Component component) where T : Component
        {
            var t = component.transform;
            var c = t.GetComponent<T>();
            while (!c && t.parent)
            {
                t = t.parent;
                c = t.GetComponent<T>();
            }
            return c;
        }
        public static Quaternion TransformRotation(this Transform t, Quaternion localRotation) => t ? t.rotation * localRotation : localRotation;
        public static Quaternion InverseTransformRotation(this Transform t, Quaternion globalRotation) => t ? Quaternion.Inverse(t.rotation) * globalRotation : globalRotation;
        public static string GetPathFrom(this Transform child, Transform parent)
        {
            if (!child)
                throw new ArgumentNullException(nameof(child));
            if (child.parent == parent)
                return child.name;
            var str = new StringBuilder();
            str.Append(child.name);
            var curr = child.parent;
            while (curr && curr != parent)
            {
                str.Insert(0, parent.name + "/");
                curr = curr.parent;
            }
            if (parent && curr != parent)
                return null;
            return str.ToString();
        }

        public static void ShowData(this Material material)
        {
            var found = $" - \"{material.name}\" (shader: \"{material.shader.name}\")";
            for (int i = 0; i < material.shader.GetPropertyCount(); i++)
            {
                string t = material.shader.GetPropertyType(i).ToString();
                var n = material.shader.GetPropertyName(i);
                
                string value = null;
                switch (material.shader.GetPropertyType(i))
                {
                    case UnityEngine.Rendering.ShaderPropertyType.Texture:
                        var b = material.GetTexture(n);
                        t = material.shader.GetPropertyTextureDimension(i).ToString();
                        if (b != null)
                            value = b.name;
                        break;
                    case UnityEngine.Rendering.ShaderPropertyType.Range:
                        var range = material.shader.GetPropertyRangeLimits(i);
                        t = $"Range({range.x}, {range.y})";
                        value = material.GetFloat(n).ToString();
                        break;
                    case UnityEngine.Rendering.ShaderPropertyType.Vector:
                        var c = material.GetVector(n);
                        if (c == null)
                            t = "Unknown Vector";
                        else
                        {
                            t = c.GetType().FullName;
                            value = $"({c.x}, {c.y}, {c.z}, {c.w})";
                        }
                        break;
                    case UnityEngine.Rendering.ShaderPropertyType.Float:
                        value = material.GetFloat(n).ToString();
                        break;
                    case UnityEngine.Rendering.ShaderPropertyType.Color:
                        var v = material.GetColor(n);
                        value = $"({v.r}, {v.g}, {v.b}, {v.a})";
                        break;
                }
                found += $"\nProperty: {n} ({t})\nDescription: {material.shader.GetPropertyDescription(i)}" + (value == null ? "" : $"\nValue: {value}");
            }
            Debug.Log(found);
        }
        public static StringBuilder AppendJoin<X>(this StringBuilder builder, IEnumerable<X> items, Func<X, string> converter = null, string delimiter = ", ", Predicate<X> filter = null)
        {
            if (items == null)
                return builder;
            var flag = false;
            foreach (var item in items)
                if (filter == null || filter(item))
                {
                    if (flag)
                        builder.Append(delimiter);
                    else
                        flag = true;
                    if (converter != null)
                        builder.Append(converter(item));
                    else if (item != null)
                        builder.Append(item.ToString());
                }
            return builder;
        }
        public static string Join<X>(this IEnumerable<X> items, Func<X, string> converter = null, string delimiter = ", ", Predicate<X> filter = null)
            => items == null
                ? string.Empty
                : new StringBuilder().AppendJoin(items,converter,delimiter,filter).ToString();

        public static StringBuilder AppendUnityObject(this StringBuilder builder, Object obj, bool includeSpecificObjectType = true)
        {
            if ((object)obj == null)
                return builder.Append("null");
            if (obj == null)
                return builder.Append("destroyed");
            builder.Append(obj.name);
            if (includeSpecificObjectType)
                builder.Append(" (").Append(obj.GetType().FullName).Append(")");
            if (obj is GameObject go)
                obj = go.transform;
            if (obj is Component comp)
            {
                builder.Append(" [");
                bool flag = false;
                foreach (var c in comp.GetComponents<Component>())
                    if (c)
                    {
                        if (flag)
                            builder.Append(", ");
                        else
                            flag = true;
                        builder.Append(c.GetType().FullName);
                    }
                builder.Append("]");
            }
            return builder;
        }
    }
}