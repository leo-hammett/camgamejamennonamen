# InGraved - Unity Setup Guide

## Prerequisites
- Unity 2021.3 LTS or newer (project uses URP)
- Git for cloning the repository

## Initial Setup Steps

### 1. Open the Project
1. Open Unity Hub
2. Click "Add" and navigate to the cloned project folder
3. Open the project (Unity will import all assets)

### 2. Create ScriptableObject Configurations
The game uses ScriptableObject configs for all tunable parameters. You need to create these:

#### Create Game Config (Master Config)
1. Right-click in `Assets/` folder
2. Select `Create > InGraved > Game Configuration`
3. Name it "GameConfig"
4. This will be your master config that references all others

#### Create Map Config
1. Right-click in `Assets/` folder
2. Select `Create > InGraved > Map Configuration`
3. Name it "MapConfig"
4. Set default values:
   - Map Width: 100
   - Map Height: 100
   - Tile Size: 1.0
   - Strength Band Count: 10
   - Create a color gradient for band colors (dark to light)

#### Create Generator Config
1. Right-click in `Assets/` folder
2. Select `Create > InGraved > Generator Configuration`
3. Name it "GeneratorConfig"
4. Set default values:
   - Base Spawn Rate: 0.5
   - Min/Max Spawn Distance: 10/30
   - Base Speed: 2.0
   - Initial Radius: 2.0
   - Radius Growth Rate: 0.5

#### Create Player Config
1. Right-click in `Assets/` folder
2. Select `Create > InGraved > Player Configuration`
3. Name it "PlayerConfig"
4. Set default values:
   - Move Speed: 5.0
   - Max Trail Points: 100
   - Trail Color: Cyan

#### Link Configs in GameConfig
1. Select your GameConfig asset
2. Drag the other configs into their respective slots:
   - Map Config → MapConfig
   - Generator Config → GeneratorConfig
   - Player Config → PlayerConfig

### 3. Scene Setup

#### Create Game Manager
1. Create empty GameObject: `GameObject > Create Empty`
2. Rename to "GameManager"
3. Add Component: `GameManager` script
4. Drag your GameConfig asset into the Game Config slot

#### Setup Tilemap (for the map)
1. Create Tilemap: `GameObject > 2D Object > Tilemap > Rectangular`
2. This creates Grid parent with Tilemap child
3. On the Tilemap GameObject:
   - Add Component: `MapSystem` script
   - Link the Tilemap component to the MapSystem's tilemap field

#### Setup Player
1. The existing scene should have a Circle object with Movement script
2. To integrate with new system:
   - Add Component: `PlayerController` script
   - Add Component: `TrailSystem` script
   - Tag it as "Player" (important for generators to find it)
3. The existing Movement script handles input, keep it for now

#### Setup Camera
1. The existing scene should have Main Camera with CameraFollow script
2. To integrate:
   - Add Component: `CameraController` script (optional, for new features)
   - Make sure CameraFollow target is set to the player Circle

#### Setup Generator Manager
1. Create empty GameObject: "GeneratorManager"
2. Add Component: `GeneratorManager` script
3. Link the GeneratorConfig to it

#### Setup Telemetry System (Visual Feedback)
1. Create Canvas: `GameObject > UI > Canvas`
2. Set Canvas Scaler to "Scale With Screen Size"
3. Create screen edge glow:
   - Right-click Canvas > `UI > Image`
   - Name it "ScreenEdgeGlow"
   - Set anchors to stretch full screen
   - Set color to semi-transparent red
   - Make it non-raycast target
4. On Canvas, add Component: `TelemetrySystem`
5. Link the ScreenEdgeGlow image to the telemetry system

### 4. Create Prefabs

#### Generator Prefab
1. Create empty GameObject in scene
2. Add Component: `StoneGenerator` script
3. Add a Sprite Renderer or simple colored square
4. Drag to `Assets/Prefabs/` folder to make prefab
5. Delete from scene
6. Link prefab to GeneratorConfig's Generator Prefab field

#### Effect Prefabs (Optional for now)
- Create simple particle systems or sprites for:
  - Spawn Warning (lightning effect)
  - Death Effect (explosion)
  - Pulse Effect (expanding ring)
- Save as prefabs and link to appropriate configs

### 5. Create Tile Assets for Map

1. Create a folder: `Assets/Tiles/`
2. Create tile assets for different strength levels:
   - Right-click > `Create > 2D > Tiles > Palette`
   - Create 10 different colored square sprites (or use one sprite with different colors)
   - Create TileBase assets for each strength band
3. Link these tiles to MapSystem's strengthBandTiles array

### 6. Input System Setup

The project uses the new Input System. The existing Movement.cs uses it for mouse input, which should work as-is.

### 7. Test Basic Setup

1. Press Play
2. You should see:
   - Player (circle) following mouse (from existing Movement script)
   - Trail rendering behind player (from existing Movement script)
   - Camera following player (from existing CameraFollow script)

### 8. Verify Systems are Working

In Play Mode, check:
1. **Console** - No errors (some warnings about TODOs are OK)
2. **Hierarchy** - All manager objects present
3. **Inspector** - All configs linked properly

## Common Issues & Solutions

### "NullReferenceException" Errors
- Make sure all ScriptableObject configs are created and linked
- Ensure GameManager has GameConfig assigned
- Check that prefabs are assigned in configs

### Generators Not Spawning
- Check GeneratorConfig has prefab assigned
- Verify spawn rate is > 0
- Check max alive generators is > 0

### Map Not Visible
- Ensure Tilemap has tile assets assigned
- Check MapSystem has strengthBandTiles array populated

### Trail Not Working
- Player object needs both Movement and TrailSystem components
- LineRenderer should be auto-created by Movement script

## Next Steps

Once basic setup is working:

1. **Implement Core Logic**
   - Fill in TODO sections in scripts
   - Start with MapSystem tile strengthening
   - Add generator AI movement
   - Implement encirclement detection

2. **Polish**
   - Add visual effects
   - Create proper sprites/tiles
   - Add sound effects
   - Implement telemetry visualization

3. **Tuning**
   - Adjust all ScriptableObject values
   - Playtest and balance
   - Add difficulty scaling

## Project Structure

```
Assets/
├── Scripts/
│   ├── Config/          # ScriptableObject definitions
│   ├── Core/           # GameManager and interfaces
│   ├── Map/            # Tile and map system
│   ├── Generators/     # Stone generator logic
│   ├── Player/         # Player controller
│   ├── Trail/          # Trail and encirclement
│   ├── UI/             # Camera and telemetry
│   ├── Movement.cs     # Existing mouse movement
│   └── CameraFollow.cs # Existing camera follow
├── Prefabs/            # Generator, effects prefabs
├── Tiles/              # Tile assets for map
├── Materials/          # Trail and visual materials
└── [Config Assets]     # Your created ScriptableObjects

Docs/
├── DESIGN.md          # System design document
└── SETUP_GUIDE.md     # This file
```

## Development Tips

- Use ScriptableObjects for ALL tuning - no magic numbers in code
- Test with very high spawn rates to quickly test encirclement
- Use Unity's Time.timeScale to slow down for debugging
- The existing Movement/CameraFollow scripts work well, integrate rather than replace
- Comment out TODO sections that cause errors until ready to implement

## Contact

For questions about the architecture, refer to DESIGN.md
For implementation details, check the docstrings in each script