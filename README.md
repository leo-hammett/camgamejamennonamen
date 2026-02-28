# InGraved 🪦

A mobile survival game where you dig through terrain while stone generators chase you and harden the world into gravestones. Your only defense? Encircle them with your trail before it's too late!

## 🎮 How to Play

1. **Move**: Click/touch and hold - your character follows your finger
2. **Survive**: Avoid gravestones (100% hardened tiles) - they kill instantly  
3. **Fight Back**: Loop your trail around generators to destroy them
4. **Score**: Survive as long as possible and kill generators for points

The ground beneath you is constantly hardening. Stone generators spawn continuously and chase you, spreading their petrifying influence. When tiles reach 100% strength, they become deadly gravestones. Your glowing trail is your only weapon - use it to encircle and eliminate the generators!

## 📁 Documentation

- **[GAMEPLAY.md](Docs/GAMEPLAY.md)** - Complete game mechanics and rules
- **[QUICK_START.md](Docs/QUICK_START.md)** - 5-minute Unity setup
- **[SETUP_GUIDE.md](Docs/SETUP_GUIDE.md)** - Detailed Unity configuration
- **[IMPLEMENTATION_GUIDE.md](Docs/IMPLEMENTATION_GUIDE.md)** - Code implementation details
- **[DESIGN.md](Docs/DESIGN.md)** - System architecture

## 🚀 Quick Start

1. Open in Unity 2021.3+
2. Create ScriptableObject configs:
   - `Create > InGraved > Game/Map/Generator/Player Configuration`
3. Set up scene GameObjects:
   - GameManager (with GameConfig)
   - GeneratorManager (with GeneratorConfig)  
   - MapSystem on Tilemap
   - PlayerController on player
4. Create generator prefab with StoneGenerator script
5. Press Play!

## 🎯 Current Status

### ✅ Working
- Player mouse/touch movement (Movement.cs)
- Trail rendering with self-intersection
- Camera following (CameraFollow.cs)
- All system architecture in place

### 🚧 Ready to Implement (Stubs)
- Stone generator spawning/AI
- Tile strengthening system
- Encirclement detection
- Gravestone collision
- Score and telemetry

## 🏗️ Architecture

```
GameManager (orchestrator)
    ├── MapSystem (tile strength)
    ├── GeneratorManager (enemies)
    ├── PlayerController (collision)
    ├── TrailSystem (encirclement)
    └── TelemetrySystem (visual feedback)
```

All configuration through ScriptableObjects - no magic numbers!

## 🎨 Key Mechanics

- **Stone Spread**: Generators radiate strengthening in growing circles
- **Speed Scaling**: Generators move faster on hardened stone  
- **Encirclement**: Close your trail loop with enemies inside to kill them
- **Difficulty Ramp**: Spawn rate increases over time
- **Visual Telemetry**: No text - all feedback through colors and effects

## 🛠️ Development

1. Fill in TODO stubs in order:
   - Generator spawning/movement
   - Stone strengthening  
   - Encirclement detection
   - Collision and game over

2. Key parameters in ScriptableObjects:
   - `baseSpawnRate`: 0.5/sec
   - `strengthPerSecond`: 0.1  
   - `maxTrailPoints`: 100

3. Test values:
   - Set `strengthPerSecond` to 1.0 for instant gravestones
   - Set `generatorSpeed` to 0 for encirclement practice

## 📝 License

Game jam project - have fun!

---

*Built for mobile-first gameplay with single-touch controls. Difficulty scales from calm exploration to frantic survival. Games typically last 2-5 minutes.*