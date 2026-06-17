# CM2121 NatureCleanup — Project Review

**Unity 6000.3.3f1 | URP | Theme: UN SDG 12/15 (Recycling & Nature Awareness)**

---

## 1. Project Structure & Organisation

```
Assets/
  Input/                        PlayerInput.inputactions + auto-generated C#
  Scripts/
    PlayerState/                InputManager, PlayerMotor, PlayerLook
    Interaction/                (empty)
    Managers/                   (empty)
    Scoring/                    (empty)
    UI/                         (empty)
  Prefabs/
    Bins/                       Recycling_Trash_Bin.prefab
    Collectibles/               Bonsay, Dog_Plushie, Dog_Plushie2,
                                Plastic_Bottle, Vase_Plant, Vase_Pot_Plant
    Environment/                Stairs.prefab
    GameCharacter/              Player.prefab
    UI/                         (empty)
  Scenes/
    GameEnvironment.unity                       Terrain + Main Camera + Directional Light
    PlayerMovementScene.unity                   14 prefab roots (player, collectibles, bins, env)
  Art/
    Photogrammery/
      Blender/                  8 .blend source files (full scan pipeline)
      RawScans/                 8 scans each: .obj + .mtl + textures/ (3 JPGs each)
      CleanedScans/             8 cleaned .obj + .mtl pairs
      Processed/                (empty)
    Models/                     (empty)
    Textures/                   (empty)
    Materials/                  (empty)
  Audio/
    Ambient/                    (empty)
    Music/                      (empty)
    SFX/                        (empty)
  UI/                           (empty)
  VFX/                          (empty)
  Settings/                     PC/Mobile RP assets, Volume profiles, Global Settings
  Tree_Textures/                diffuse, normal_specular, shadow, translucency_gloss PNGs
```

---

## 2. What's Implemented

### 2.1 Player Controller (Working)
| Script | Purpose | Status |
|--------|---------|--------|
| `InputManager.cs` | Bridges `PlayerInput` actions to motor/look | Complete |
| `PlayerMotor.cs` | `CharacterController` movement, gravity, jump | Complete |
| `PlayerLook.cs` | Mouse look with clamped pitch, cursor lock | Complete |
| `PlayerInput.inputactions` | Input Action asset: Move (WASD/Arrows), Jump (Space/Enter), Sprint (Ctrl), Crouch (C), Equip (E), Release (Q), Look (Mouse), Shoot (LMB), Aim (RMB) | Complete |

All three player scripts are functional: move, look, jump work in `PlayerMovementScene.unity`.

### 2.2 Photogrammetry Pipeline (Strong)
- **8 raw scans** with source textures: Bonsay, Dog_Plushie, Dog_Plushie_2, Nature_Recycle_Bin, Plastic_Bottle, Stairs, Vase_Plant, Vase_Plant_Pot
- **8 cleaned scans** (obj+mtl) ready for import into Unity
- **8 Blender source files** matching each scan
- Processed/ folder reserved for final Unity-ready assets (empty)

### 2.3 Prefabs (Placeholder)
- All prefabs exist on disk (`Recycling_Trash_Bin`, collectibles, `Stairs`, `Player`) but are likely broken references since they depend on the imported FBX meshes which don't exist yet.

### 2.4 Scenes
- **`GameEnvironment.unity`**: Bare terrain with Main Camera and Directional Light. No textures/trees/grass painted. Has the Terrain component with a terrain data reference (guid `96f07a4d...`) that doesn't resolve.
- **`PlayerMovementScene.unity`**: 14 prefab instances including player, collectibles (Bonsay, Dog_Plushie, Plastic_Bottle, Vase_Plant, etc.), bins, stairs, UI elements. **The primary playable scene.**

### 2.5 Rendering Pipeline
- **PC URP Asset**: Deferred rendering, SSAO, 4 shadow cascades, high-quality soft shadows, 2048 shadow atlas. Solid quality settings.
- **Mobile URP Asset**: Forward rendering, 0.8 render scale, single shadow cascade, 256 shadow atlas. Good for mobile targets.
- **Post-processing**: Volume profile includes Bloom, Color Adjustments, Tonemapping, Vignette, Motion Blur, Depth of Field, Chromatic Aberration, Lens Distortion, Film Grain.
- `UniversalRenderPipelineGlobalSettings.asset` references all URP resource assets.

---

## 3. What's Missing (Critical Gaps)

### 3.1 Art Assets — The Biggest Gap
| Category | Status | Impact |
|----------|--------|--------|
| 3D Models (FBX) | **None** | All prefabs have missing mesh references |
| Materials | **None** | No visual surface appearances defined |
| Textures (environment) | **None** | Terrain has no splat textures |
| Terrain trees/grass | **None** | Terrain data exists but no prototypes |
| Vegetation models | **None** | No tree/bush/grass models anywhere |

**Note:** `Tree_Textures/` has 4 PNG textures for tree shaders, but no tree models to apply them to. The Unity Terrain - URP Demo Scene package (free Asset Store) would provide all of this but is not installed.

### 3.2 Audio — Completely Missing
| Folder | Status |
|--------|--------|
| `Assets/Audio/Ambient/` | Empty |
| `Assets/Audio/Music/` | Empty |
| `Assets/Audio/SFX/` | Empty |

Zero audio files in the entire project.

### 3.3 Gameplay Scripts — Not Started
| Folder | Contents |
|--------|----------|
| `Scripts/Interaction/` | Empty — no pickup/equip/release logic |
| `Scripts/Managers/` | Empty — no GameManager, no game state |
| `Scripts/Scoring/` | Empty — no score tracking |
| `Scripts/UI/` | Empty — no HUD, menus, inventory UI |

**The input actions exist for Equip (E), Release (Q), Sprint (Ctrl), Crouch (C), Shoot (LMB), Aim (RMB)** but none of these are wired to any gameplay logic. Only Movement, Jump, and Look are connected.

### 3.4 UI — Not Started
`Assets/UI/` is empty. No canvas, no HUD, no menus, no interaction prompts.

### 3.5 VFX — Not Started
`Assets/VFX/` is empty. No particle systems, no environmental effects.

### 3.6 Build Settings — Wrong
`EditorBuildSettings.asset` references `SampleScene.unity` which no longer exists. It should reference `GameEnvironment.unity` and/or `PlayerMovementScene.unity`.

### 3.7 Prefabs — Not Connected to Scanned Assets
The clean scans in `CleanedScans/` (OBJ/MTL pairs) need to be:
1. Imported into Unity as proper assets
2. Converted to FBX or kept as OBJ with Unity Materials
3. Assigned to the matching prefabs

---

## 4. Summary

| Area | Status | Priority |
|------|--------|----------|
| Input System & Player Controls | **Done** | — |
| Player Movement (CharacterController) | **Done** | — |
| Camera & Mouse Look | **Done** | — |
| Photogrammetry Raw Scans | **Done** | — |
| Photogrammetry Cleaned Scans | **Done** | — |
| Blender Source Files | **Done** | — |
| URP Pipeline Config (PC/Mobile) | **Done** | — |
| Post-Processing Volume Profile | **Done** | — |
| Terrain in GameEnvironment | **Stub** (no textures/trees) | High |
| 3D Model Import (FBX/OBJ → Prefabs) | **Not done** | High |
| Materials & Textures | **Not done** | High |
| Gameplay Scripts (collect, score, game state) | **Not started** | High |
| Audio (any) | **Not started** | High |
| UI/HUD/Menus | **Not started** | High |
| VFX/Particles | **Not started** | Medium |
| Build Settings | **Broken** (wrong scene) | High |
| Unity Terrain - URP Demo Scene | **Not installed** | High |

---

## 5. Recommended Next Steps (Priority Order)

1. **Install Unity Terrain - URP Demo Scene** from Asset Store — gives terrain textures, trees, grass, demo scene
2. **Import cleaned OBJ scans** into Unity, create materials, assign to prefabs
3. **Wire up gameplay scripts** — collectible pickup (E), scoring, game manager
4. **Fix build settings** — add `PlayerMovementScene.unity` and/or `GameEnvironment.unity`
5. **Add audio** — ambient nature sounds, SFX for pickup/jump
6. **Build UI** — score display, interaction prompts, main menu
7. **Paint terrain** — apply textures and vegetation from the URP terrain package

---

## 6. Minimum Playable Demo — Setup Guide

New scripts created:

| Script | Path | Purpose |
|--------|------|---------|
| `WasteType.cs` | `Scripts/Scoring/` | Enum: Recyclable / NonRecyclable |
| `WasteItem.cs` | `Scripts/Interaction/` | Attached to each collectible — defines waste type |
| `BinZone.cs` | `Scripts/Scoring/` | Trigger zone on the bin — detects player |
| `ObjectInteractor.cs` | `Scripts/Interaction/` | On player — raycast pickup (E) and release (Q) |
| `GameManager.cs` | `Scripts/Managers/` | Score, 120-second timer, game-over state (singleton) |
| `GameUI.cs` | `Scripts/UI/` | Updates score/timer/prompt text (singleton) |
| `AudioManager.cs` | `Scripts/Managers/` | Placeholder SFX hooks — logs to console, ready for clips |
| `VFXManager.cs` | `Scripts/Managers/` | Placeholder VFX hooks — logs to console, ready for particles |

---

### 6.1 Player GameObject Setup

On the **Player** GameObject (the one with `PlayerMotor`, `PlayerLook`, `InputManager`):

1. Add `ObjectInteractor` component
2. Assign:
   - **Player Camera** → drag the Camera (child of Player)
   - **Hold Parent** → create an empty child GameObject named `HoldAnchor` positioned at `(0, 0, 0.5)` relative to camera, drag it here
   - **Pickup Range** → `3`
   - **Pickup Layer** → `Everything` (keep default -1)
3. Add a **Rigidbody** component:
   - **Use Gravity** → unchecked
   - **Is Kinematic** → checked
4. Make sure **Layer** is set to `Default`
5. Add a **Tag** called `Player` (create it if needed) and assign it

---

### 6.2 Collectible Items Setup

For each item (Bonsay, Dog_Plushie, Dog_Plushie2, Plastic_Bottle, etc.):

1. First, **import the cleaned OBJ scan** into the scene:
   - From `Assets/Art/Photogrammery/CleanedScans/ItemName/ItemName.obj`
   - Drag the `.obj` file into the scene or onto the existing Prefab
2. Add `WasteItem` component
3. Set:
   - **Waste Type** → `Recyclable` for `Plastic_Bottle` only; `NonRecyclable` for Dog_Plushie, Dog_Plushie2, Bonsay, Vase_Plant, Vase_Pot_Plant
   - **Item Name** → e.g. "Plastic Bottle", "Dog Plushie"
   - **Hold Position** → `(0, -0.3, 0.5)` (default is fine)
4. Add a **Collider** (Box Collider recommended, adjust to fit mesh):
   - **Is Trigger** → **checked** (so raycast and physics don't interfere)
5. Add a **Rigidbody**:
   - **Use Gravity** → checked
   - **Is Kinematic** → unchecked
6. Make the Prefab changes, then remove the scene instance and re-drop the prefab from the Project window

---

### 6.3 Bin (Nature_Recycle_Bin) Setup

1. Import the cleaned OBJ scan for `Nature_Recycle_Bin` into the scene
2. Add `BinZone` component
3. Add a **Box Collider** larger than the bin model (e.g., `(2, 2, 2)`):
   - **Is Trigger** → **checked**
4. Add a **Rigidbody**:
   - **Use Gravity** → unchecked
   - **Is Kinematic** → checked
5. Tag the bin GameObject with `Bin` (optional, not required by code)

---

### 6.4 UI Canvas Setup

1. Create a **Canvas** (right-click Hierarchy → UI → Canvas):
   - Render Mode: `Screen Space - Overlay`
2. Create four child **TextMeshPro - Text** objects (you'll be prompted to import TMP Essentials — click yes):

   | Text Object | Parent | Role | Font Size | Alignment | Suggested Position |
   |-------------|--------|------|-----------|-----------|-------------------|
   | `ScoreText` | Canvas | Display score | 36 | Top-Left | Anchors: top-left, Pos: (10, -10) |
   | `TimerText` | Canvas | Display timer | 36 | Top-Right | Anchors: top-right, Pos: (-10, -10) |
   | `PromptText` | Canvas | Interaction prompt | 24 | Bottom-Center | Anchors: bottom-center, Pos: (0, 50) |
   | `GameOverPanel` | Canvas | Dark overlay + final score | — | — | Full-screen Image (black, alpha 0.7) |

3. Add `GameUI` component to the Canvas (or a separate UI manager GameObject)
4. Assign the four text fields in the `GameUI` component slots:
   - **Score Text** → `ScoreText` GameObject
   - **Timer Text** → `TimerText` GameObject
   - **Prompt Text** → `PromptText` GameObject
   - **Game Over Panel** → a Panel GameObject with a child `FinalScoreText` (TextMeshPro), both initially disabled
   - **Final Score Text** → the TextMeshPro child of the Game Over Panel

5. For the **Game Over Panel**:
   - Create an Image (Panel) as child of Canvas
   - Name it `GameOverPanel`
   - Set color to black with alpha ~0.7
   - Uncheck it (disabled by default)
   - Add a child TextMeshPro called `FinalScoreText`, centered
   - Drag the Panel into `GameUI.GameOverPanel` and the TextMeshPro into `GameUI.FinalScoreText`

---

### 6.5 GameManager GameObject Setup

1. Create an empty GameObject named `GameManager`
2. Add `GameManager` component
3. Default settings are fine: 120-second timer, +10 for correct, -5 for wrong
4. Optionally add an **AudioSource** component (for future SFX assignment)

---

### 6.6 Required Tags

1. Open the **Tags & Layers** menu (Edit → Project Settings → Tags and Layers)
2. Create a tag called `Player`
3. Assign the Player GameObject this tag

---

### 6.7 Build Settings (Fix Before Building)

1. Open **File → Build Settings**
2. Remove `SampleScene` if present
3. Add `Assets/Scenes/PlayerMovementScene.unity`
4. Optionally add `GameEnvironment.unity` as scene 0

---

### 6.8 How to Test the Game Loop

1. Open `PlayerMovementScene.unity`
2. Press **Play**
3. Movement: **WASD / Arrow keys** to walk
4. Look around: **Mouse**
5. Walk up to the **Plastic_Bottle** — see "Press E to pick up Plastic Bottle" prompt
6. Press **E** — item attaches to camera view
7. Walk to the **Nature_Recycle_Bin** — prompt changes to "Press Q to release into bin"
8. Press **Q** — score shows **+10**, item disappears
9. Repeat with **Dog_Plushie** or **Dog_Plushie_2** — dropping in bin gives **-5**
10. Timer counts down from **2:00**
11. At **0:00**, Game Over panel appears with final score

---

### 6.9 How to Extend Later

- Add **audio clips** to `Assets/Audio/SFX/`, assign them in a real `AudioManager` that replaces the placeholder
- Create **Particle System** prefabs in `Assets/VFX/`, call `Instantiate()` in `VFXManager` methods
- Add more items: duplicate any prefab, change `WasteType`, import a new cleaned OBJ scan
- Adjust timer or scoring in the `GameManager` inspector
