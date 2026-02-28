# 🚀 Quick Setup Instructions to Run the Game

## Fixes Applied ✅
- Fixed deprecated API warning in SetupHelper.cs
- Fixed invalid GUIDs in GameConfig.asset
- Fixed invalid GUID in PlayerConfig.asset

## Steps to Run the Game in Unity:

### Option A: Automatic Setup (Recommended) 
1. **Open the SampleScene** 
   - In Unity, open `Assets/Scenes/SampleScene.unity`

2. **Run the Setup Helper**
   - Create an empty GameObject: `GameObject > Create Empty`
   - Name it "SetupHelper"
   - Add the SetupHelper script: `Add Component > SetupHelper`
   - In the Inspector, click the **"Auto Setup Scene"** button
   - Delete the SetupHelper object after setup

### Option B: Manual Setup
1. **Open SampleScene**: `Assets/Scenes/SampleScene.unity`

2. **Create Core GameObjects**:
   ```
   GameObject > Create Empty > "GameManager"
   - Add Component > GameManager
   - Drag GameConfig asset into the Config slot
   
   GameObject > Create Empty > "GeneratorManager"  
   - Add Component > GeneratorManager
   - It will auto-find the config from GameManager
   ```

3. **Setup Player** (find existing "Circle" object):
   - Add Component > PlayerController
   - Add Component > TrailSystem
   - Tag as "Player" (important!)

4. **Setup Map**:
   - GameObject > 2D Object > Tilemap > Rectangular
   - On the Tilemap child object: Add Component > MapSystem

5. **Setup UI Canvas**:
   - GameObject > UI > Canvas
   - Add Component > TelemetrySystem

### 3. Create Generator Prefab (Required!)
1. GameObject > Create Empty > Name it "StoneGenerator"
2. Add Component > StoneGenerator
3. Add Component > Sprite Renderer
4. Set the Sprite Renderer color to something visible (e.g., red)
5. Drag to `Assets/` folder to create a prefab
6. Delete from scene
7. Open `Assets/GeneratorConfig.asset` 
8. Drag your new prefab to the "Generator Prefab" field

### 4. Press Play! 🎮

## What You Should See:
- Player (circle) follows your mouse/touch
- Camera follows the player
- Stone generators spawn and chase you (if prefab is set)
- Trail renders behind player

## Common Issues:

### "NullReferenceException" errors
- Make sure GameConfig has all 3 configs linked (already fixed)
- Make sure GeneratorConfig has the prefab assigned

### Nothing spawns
- Check that GeneratorConfig has the generator prefab assigned (Step 3)

### Can't see anything
- Camera should be at Z = -10
- All game objects at Z = 0

## Next Steps for Development:
The game architecture is ready but needs gameplay implementation:

1. **StoneGenerator.cs** - Add movement logic (line 42)
2. **MapSystem.cs** - Implement tile strengthening 
3. **TrailSystem.cs** - Add encirclement detection
4. **PlayerController.cs** - Add collision detection

All TODO markers show exactly what needs implementation!