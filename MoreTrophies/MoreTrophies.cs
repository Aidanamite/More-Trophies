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

namespace MoreTrophies
{
    public class Main : Mod
    {
        static List<Trophy> ItemsToAdd = new List<Trophy>
    {
        new Trophy(ItemManager.GetItemByName("Hammer"), null, "Hammer", null, new Vector3(0.1f, -0.3f, 0), new Vector3(-45, 90, 0), size: Size.Medium ),
        new Trophy(ItemManager.GetItemByName("Spear_Plank"), null, "Spear_Wood", null, Vector3.zero, new Vector3(-60, 90, 0), size: Size.Large ),
        new Trophy(ItemManager.GetItemByName("Spear_Scrap"), null, "Spear_Scrap", null, Vector3.zero, new Vector3(-60, 90, 0), size: Size.Large ),
        new Trophy(ItemManager.GetItemByName("Axe_Stone"), null, "Axe_Stone", null, new Vector3(0.2f, -0.4f, 0), new Vector3(-45, 90, 0), size: Size.Medium ),
        new Trophy(ItemManager.GetItemByName("Axe"), null, "Axe_Scrap", null, new Vector3(0.2f, -0.3f, 0), new Vector3(0, 0, 60), size: Size.Medium ),
        new Trophy(ItemManager.GetItemByName("Axe_Titanium"), null, "Axe_Titanium", null, new Vector3(0.3f, -0.3f, 0), new Vector3(-60, 90, 0), size: Size.Medium ),
        new Trophy(ItemManager.GetItemByName("PaintBrush"), null, null, x => x.name == "PaintBrush", new Vector3(-0.1f, -0.3f, 0), Vector3.zero, size: Size.Medium ),
        new Trophy(ItemManager.GetItemByName("Machete"), null, "Machete", null, new Vector3(0.2f, -0.2f, 0), new Vector3(60, -90, 0), size: Size.Medium ),
        new Trophy(ItemManager.GetItemByName("Hat_Pilot"), null, "Hat_Pilot_Remote", null, Vector3.zero, new Vector3(45, 0, 0) ),
        new Trophy(ItemManager.GetItemByName("Hat_Tiki"), null, "Hat_TikiMask_Remote", null, new Vector3(0, 0, 0.1f), new Vector3(0, 0, 0) ),
        new Trophy(ItemManager.GetItemByName("Hat_Captain"), null, "Hat_CaptainsHat_Remote", null, new Vector3(0, 0, 0.1f), new Vector3(90, 0, 0) ),
        new Trophy(ItemManager.GetItemByName("Hat_Mayor"), null, "Hat_MayorHat_Remote", null, new Vector3(0, 0, 0.05f), new Vector3(90, 0, 0) ),
        new Trophy(ItemManager.GetItemByName("Banana"), null, "Banana", null, new Vector3(0, -0.05f, 0), new Vector3(0, 90, 0) ),
        new Trophy(ItemManager.GetItemByName("Shear"), null, "Shear", null, new Vector3(0.01f, 0, 0.01f), new Vector3(-30, 90, 90) ),
        new Trophy(ItemManager.GetItemByName("Hat_Fishing"), null, "Hat_Fishing_Remote", null, new Vector3(0,0,0.07f), new Vector3(90,0,0), 0.67f ),
        new Trophy(ItemManager.GetItemByName("Hat_Sailor"), null, "Hat_Sailor_Remote", null, new Vector3(0,0,0.1f), new Vector3(30,0,0), 0.67f ),
        new Trophy(ItemManager.GetItemByName("Hat_Diving"), null, "Hat_Diving_Remote", null, new Vector3(0,0,0.1f), new Vector3(0,0,0), 0.67f ),
        new Trophy(ItemManager.GetItemByName("Hat_Construction"), null, "Hat_ConstructionHelmet_Remote", null, new Vector3(0,0,0.05f), new Vector3(90,0,0), 0.67f ),
        new Trophy(ItemManager.GetItemByName("Hat_Chef"), null, "Hat_Chef_Remote", null, new Vector3(0,0,0.1f), new Vector3(30,0,0), 0.67f ),
        new Trophy(ItemManager.GetItemByName("Hat_Glasses_Aviator"), null, "Hat_Glasses_Aviator_Remote", null, new Vector3(0,0,0), new Vector3(0,0,0) ),
        new Trophy(ItemManager.GetItemByName("Hat_Glasses_Disguise"), null, "Hat_Glasses_Disguise_Remote", null, new Vector3(0,0,0), new Vector3(0,0,0) ),
        new Trophy(ItemManager.GetItemByName("Hat_Pirate"), null, "Hat_Pirate_Remote", null, new Vector3(0,0,0), new Vector3(90,0,0), 0.67f ),
        new Trophy(ItemManager.GetItemByName("Cassette_Classical"), null, "Cassette_Classic", null, new Vector3(0,0,0), new Vector3(0,0,0), 0.5f ),
        new Trophy(ItemManager.GetItemByName("Cassette_EDM"), null, "Cassette_EDM", null, new Vector3(0,0,0), new Vector3(0,0,0), 0.5f ),
        new Trophy(ItemManager.GetItemByName("Cassette_Elevator"), null, "Cassette_Elevator", null, new Vector3(0,0,0), new Vector3(0,0,0), 0.5f ),
        new Trophy(ItemManager.GetItemByName("Cassette_Pop"), null, "Cassette_Pop", null, new Vector3(0,0,0), new Vector3(0,0,0), 0.5f ),
        new Trophy(ItemManager.GetItemByName("Cassette_Rock"), null, "Cassette_Rock", null, new Vector3(0,0,0), new Vector3(0,0,0), 0.5f ),
        new Trophy(ItemManager.GetItemByName("Cassette_TradingPost"), null, "Cassette_TradingPost", null, new Vector3(0,0,0), new Vector3(0,0,0), 0.5f ),
        new Trophy(ItemManager.GetItemByName("Shovel"), null, "Shovel", null, new Vector3(-0.7f,0.5f,0.1f), new Vector3(30,90,90), 0.67f, size: Size.Large ),
        new Trophy(ItemManager.GetItemByName("Sword_Titanium"), null, "Sword_Titanium", null, new Vector3(-0.35f,-0.2f,0), new Vector3(60,90,0), 0.67f, size: Size.Medium ),
        new Trophy(ItemManager.GetItemByName("NetGun"), null, "Model_NetGun", null, new Vector3(-0.15f,-0.25f,0.05f), new Vector3(-30,90,0), 0.67f, size: Size.Medium ),
        new Trophy(ItemManager.GetItemByName("FishingBait_Simple"), delegate {
            foreach (var h in Resources.FindObjectsOfTypeAll<FishingBaitHandler>())
                foreach (var c in h.baitConnections)
                    if (c.bait.UniqueName == "FishingBait_Simple")
                        return (c.bobberMaterial,c.bobberMesh);
            return default; }, null, null, new Vector3(0,0.05f,0), new Vector3(0,0,0) ),
        new Trophy(ItemManager.GetItemByName("FishingBait_Advanced"), delegate {
            foreach (var h in Resources.FindObjectsOfTypeAll<FishingBaitHandler>())
                foreach (var c in h.baitConnections)
                    if (c.bait.UniqueName == "FishingBait_Advanced")
                        return (c.bobberMaterial,c.bobberMesh);
            return default; }, null, null, new Vector3(0,0.05f,0), new Vector3(0,0,0) ),
        new Trophy(ItemManager.GetItemByName("FishingBait_Expert"), delegate {
            foreach (var h in Resources.FindObjectsOfTypeAll<FishingBaitHandler>())
                foreach (var c in h.baitConnections)
                    if (c.bait.UniqueName == "FishingBait_Expert")
                        return (c.bobberMaterial,c.bobberMesh);
            return default; }, null, null, new Vector3(0,0.2f,0.02f), new Vector3(0,0,0) )
    }; //csrun string str = ""; foreach (MeshRenderer mesh in Resources.FindObjectsOfTypeAll<MeshRenderer>()) if (mesh.name.Contains("NAME")) str += "\n" + mesh.name; Debug.Log(str);

        public static List<ItemModelConnection> addedConnections;
        public static Transform PrefabParent;
        Harmony harmony;
        public void Start()
        {
            PrefabParent = new GameObject("prefabParent").transform;
            DontDestroyOnLoad(PrefabParent.gameObject);
            PrefabParent.gameObject.SetActive(false);

            harmony = new Harmony("com.aidanamite.MoreTrophies");
            harmony.PatchAll();
            addedConnections = new List<ItemModelConnection>();
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
            harmony.UnpatchAll(harmony.Id);
            foreach (ItemObjectEnabler enabler in FindObjectsOfType<ItemObjectEnabler>())
            {
                var enablerConnections = Traverse.Create(enabler).Field<ItemModelConnection[]>("itemConnections");
                var connections = enablerConnections.Value.ToList();
                for (int i = addedConnections.Count - 1; i >= 0; i--)
                    if (connections.Contains(addedConnections[i]))
                    {
                        connections.Remove(addedConnections[i]);
                        Destroy(addedConnections[i].model);
                        addedConnections.RemoveAt(i);
                    }
                enablerConnections.Value = connections.ToArray();
                if (addedConnections.Count == 0)
                    break;
            }
            Destroy(PrefabParent.gameObject);
            Log("Mod has been unloaded!");
        }

        public static void ModifyBoard(TrophyHolder holder)
        {
            EnsureBlueprints();
            var enabler = Traverse.Create(holder).Field("itemObjectEnabler").GetValue<ItemObjectEnabler>();
            var size = Trophy.GetSize(enabler);
            var enablerConnections = Traverse.Create(enabler).Field<ItemModelConnection[]>("itemConnections");
            var connections = enablerConnections.Value.ToList();
            connections.AddRange(enablerConnections.Value);
            void CreatePrefab(Trophy trophy, GameObject sourceObject, Material material, Mesh mesh)
            {
                var model = new GameObject(trophy.Item.UniqueName);
                model.SetActive(false);
                model.transform.SetParent(PrefabParent, false);
                model.AddComponent<MeshRenderer>().material = material;
                model.AddComponent<MeshFilter>().mesh = mesh;
                model.transform.localPosition = new Vector3(0.03f, 0, 0.05f) + trophy.Position;
                model.transform.localRotation = trophy.Rotation;
                model.transform.localScale = trophy.Scale * 1.5f;
                trophy.AdditionalChanges?.Invoke(model, sourceObject);
                trophy.Prefab = model;
            }
            var c = connections.Where(x => ItemsToAdd.Any(y => y.Item.UniqueIndex == x.item.UniqueIndex)).ToDictionary(x => ItemsToAdd.Find(y => y.Item.UniqueIndex == x.item.UniqueIndex), x => x.model);
            foreach (Trophy trophy in ItemsToAdd)
                try
                {
                    if (trophy.Item)
                    {
                        var source = trophy;
                        while (source.Linked != null)
                            source = source.Linked;
                        if (size != source.Size)
                            continue;
                        if (!source.Prefab)
                        {
                            if (source.SourceModel != null)
                            {
                                var m = source.SourceModel();
                                CreatePrefab(source, null, m.Item1, m.Item2);
                            }
                            else
                            {
                                foreach (var renderer in Resources.FindObjectsOfTypeAll<Renderer>())
                                    if ((renderer is SkinnedMeshRenderer || renderer.GetComponent<MeshFilter>()) && (source.SourcePredicate?.Invoke(renderer) ?? (source.Source == renderer.name || source.Source == renderer.transform.parent?.name)))
                                    {
                                        CreatePrefab(source, renderer.gameObject, renderer.material, (renderer as SkinnedMeshRenderer)?.sharedMesh ?? renderer.GetComponent<MeshFilter>().mesh);
                                        break;
                                    }
                            }
                        }
                        if (source.Prefab)
                        {
                            var connection = new ItemModelConnection() { item = trophy.Item, model = c.TryGetValue(source, out var o) ? o : c[source] = Instantiate(source.Prefab, enabler.transform) };
                            if (trophy.Item.UniqueIndex == holder.GetCurrentItem()?.UniqueIndex)
                                connection.model.SetActive(true);
                            connections.Add(connection);
                            addedConnections.Add(connection);
                        }
                        else
                            Debug.Log($"{trophy.Item.UniqueName} is missing its prefab");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("[More Trophies]: " + e);
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
            void SetupBlueprint(GameObject newObj, GameObject oldObj)
            {
                newObj.AddComponent<BlueprintTrophy>();
                var b = newObj.AddComponent<BlueprintComponent>();
                var b2 = oldObj.transform.GetAnyComponentInParent<BlueprintComponent>();
                var c = Instantiate(oldObj.GetComponentInChildren<Canvas>(), newObj.transform);
                c.renderMode = RenderMode.WorldSpace;
                c.gameObject.layer = 0;
                b.SetNameText(c.transform.Find(b2.GetNameText().name).GetComponent<TextMeshProUGUI>());
                b.SetDescText(c.transform.Find(b2.GetDescText().name).GetComponent<TextMeshProUGUI>());
                b.SetBlueprintImage(c.transform.Find(b2.GetBlueprintImage().name).GetComponent<Image>());
                b.SetBlueprintConnections(b2.GetBlueprintConnections());
            }
            foreach (var i in ItemManager.GetAllItems())
                if (i.UniqueName.StartsWith("Blueprint_") && created.Add(i))
                    ItemsToAdd.AddRange(
                        baseLink == null
                        ? baseLink = new[]
                        {
                            new Trophy(i, null, "Blueprint", null, new Vector3(0,0,0.02f), default(Vector3), 1, size: Size.Small) { AdditionalChanges = SetupBlueprint },
                            new Trophy(i, null, "Blueprint", null, new Vector3(-0.12f,0,0.02f), default(Vector3), 2, size: Size.Medium) { AdditionalChanges = SetupBlueprint },
                            new Trophy(i, null, "Blueprint", null, new Vector3(-0.06f,0.1f,0.05f), default(Vector3), 3, size: Size.Large) { AdditionalChanges = SetupBlueprint }
                        }
                        : baseLink.Select(x => new Trophy(i, x))
                    );
        }

        public static T CreateObject<T>(Action<T> initialize = null)
        {
            var o = (T)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(T));
            initialize?.Invoke(o);
            return o;
        }
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
        public Item_Base Item { private set; get; }
        public string Source { private set; get; }
        public Func<(Material, Mesh)> SourceModel { private set; get; }
        public Predicate<Renderer> SourcePredicate { private set; get; }
        public Vector3 Position { private set; get; }
        public Quaternion Rotation { private set; get; }
        public Vector3 Scale { private set; get; }
        public Size Size { private set; get; }
        public Action<GameObject, GameObject> AdditionalChanges;

        public GameObject Prefab;
        public Trophy Linked { private set; get; }

        public Trophy(Item_Base item, Trophy linked)
        {
            Item = item;
            Linked = linked;
        }
        public Trophy(Item_Base item, Func<(Material, Mesh)> sourceModel, string source, Predicate<Renderer> sourcePredicate, Vector3 position, Quaternion rotation, Vector3 scale, Size size = Size.Small)
        {
            Item = item;
            Source = source;
            SourceModel = sourceModel;
            SourcePredicate = sourcePredicate;
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Size = size;
        }
        public Trophy(Item_Base item, Func<(Material, Mesh)> sourceModel, string source, Predicate<Renderer> sourcePredicate, Vector3 position, Vector3 rotation, Vector3 scale, Size size = Size.Small) : this(item, sourceModel, source, sourcePredicate, position, Quaternion.Euler(rotation), scale, size) { }
        public Trophy(Item_Base item, Func<(Material, Mesh)> sourceModel, string source, Predicate<Renderer> sourcePredicate, Vector3 position, Quaternion rotation, float scale = 1f, Size size = Size.Small) : this(item, sourceModel, source, sourcePredicate, position, rotation, Vector3.one * scale, size) { }
        public Trophy(Item_Base item, Func<(Material, Mesh)> sourceModel, string source, Predicate<Renderer> sourcePredicate, Vector3 position, Vector3 rotation, float scale = 1f, Size size = Size.Small) : this(item, sourceModel, source, sourcePredicate, position, Quaternion.Euler(rotation), Vector3.one * scale, size) { }


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
    }
}