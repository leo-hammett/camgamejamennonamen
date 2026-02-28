# InGraved - Quick Start Guide

## Minimum Setup to Run (5 minutes)

### 1. Create Configs (Required!)
```
Right-click in Assets folder → Create → InGraved → ...
- Game Configuration → name "GameConfig"
- Map Configuration → name "MapConfig"  
- Generator Configuration → name "GeneratorConfig"
- Player Configuration → name "PlayerConfig"
```

**Link them:** Open GameConfig, drag other 3 configs into their slots

### 2. Scene Setup
```
1. GameObject → Create Empty → "GameManager"
   - Add Component → GameManager
   - Drag GameConfig into slot

2. Find "Circle" object (player):
   - Add Component → PlayerController
   - Add Component → TrailSystem
   - Tag as "Player"

3. GameObject → 2D Object → Tilemap → Rectangular
   - On Tilemap child: Add Component → MapSystem

4. GameObject → Create Empty → "GeneratorManager"  
   - Add Component → GeneratorManager
   - Drag GeneratorConfig into slot

5. GameObject → UI → Canvas
   - Add Component → TelemetrySystem
```

### 3. Create Generator Prefab
```
1. GameObject → Create Empty
2. Add Component → StoneGenerator
3. Add Component → Sprite Renderer (set color)
4. Drag to Assets/Prefabs/ folder
5. Delete from scene
6. Link to GeneratorConfig's Generator Prefab field
```

### 4. Press Play!
- Mouse movement should work (existing script)
- Trail should render (existing script)
- Generators should start spawning (if configs are linked)

## What Each System Does

| System | Purpose | Required? |
|--------|---------|-----------|
| **GameManager** | Orchestrates everything | YES |
| **MapSystem** | Manages tile strength | YES |
| **GeneratorManager** | Spawns/manages enemies | YES |
| **PlayerController** | Player logic | YES |
| **TrailSystem** | Trail + encirclement | YES |
| **TelemetrySystem** | Visual feedback | Optional |
| **Movement** (existing) | Mouse following | Keep it! |
| **CameraFollow** (existing) | Camera tracking | Keep it! |

## Config Quick Reference

### MapConfig
- `mapWidth/Height`: 100 (map size)
- `tileSize`: 1.0
- `strengthBandCount`: 10 (visual bands)

### GeneratorConfig  
- `baseSpawnRate`: 0.5 (per second)
- `baseSpeed`: 2.0
- `maxAliveGenerators`: 10
- **Must set**: generatorPrefab!

### PlayerConfig
- `moveSpeed`: 5.0  
- `maxTrailPoints`: 100
- `trailColor`: Cyan

### GameConfig
- Links to all other configs
- `pointsPerGeneratorKill`: 100

## Troubleshooting

**Nothing spawns?**
→ Check GeneratorConfig has prefab assigned

**NullReferenceException?**
→ GameManager needs GameConfig
→ GameConfig needs other 3 configs linked

**Can't see anything?**
→ Camera should be at Z = -10
→ Objects at Z = 0

## Implementation Priority

1. ✅ Get it running with stubs
2. ⬜ MapSystem.AddStrengthInRadius() 
3. ⬜ Generator movement (chase player)
4. ⬜ Encirclement detection
5. ⬜ Gravestone collision
6. ⬜ Visual polish

The game compiles and runs with stub implementations - fill in TODOs to make it playable!