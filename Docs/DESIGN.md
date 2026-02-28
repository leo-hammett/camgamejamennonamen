# InGraved - System Design Document

## Overview
InGraved is a mobile survival game where players dig through terrain while avoiding stone generators that harden the ground. The player must use their trail to encircle and kill generators before the map becomes too petrified.

## Architecture Philosophy
- **Grug Brain**: Keep it simple. If it can work simply, make it simple.
- **Separation of Concerns**: Each system handles one responsibility
- **Configuration-Driven**: All tunable parameters in ScriptableObjects
- **Interface-Based**: Systems communicate through interfaces for flexibility
- **Powerup-Ready**: Hook points left for future powerup implementation

## Core Systems

### 1. Game Manager (`GameManager.cs`)
**Responsibility**: Orchestrates all game systems and manages game state

**Key Functions**:
- Initializes and coordinates all subsystems
- Manages game state (Menu, Playing, Paused, GameOver)
- Handles scoring and time tracking
- Provides system references to other components

**Interactions**:
- Creates and initializes all other systems
- Polls systems for game-ending conditions
- Broadcasts game state changes

### 2. Map System (`MapSystem.cs`, `IMapSystem.cs`)
**Responsibility**: Manages the tile grid and stone strength values

**Key Functions**:
- Maintains 2D array of tile strengths (0.0 to 1.0)
- Converts between world and tile coordinates
- Updates tile visuals based on strength bands
- Applies stone strengthening from generators

**Interactions**:
- Read by Player for gravestone collision
- Modified by Generators to add stone strength
- Queried by Telemetry for danger visualization

**Data Flow**:
```
Generator -> AddStrengthInRadius() -> Tile Strengths -> UpdateTileVisuals() -> Tilemap
```

### 3. Generator System (`IGeneratorManager.cs`, `StoneGenerator.cs`)
**Responsibility**: Spawns and manages stone generators

**Key Functions**:
- Spawns generators at random positions
- Updates generator movement (chase player)
- Applies stone strengthening to map
- Handles generator death from encirclement

**Interactions**:
- Queries Player position for chasing
- Modifies Map tile strengths
- Checked by Trail system for encirclement

**Movement Algorithm**:
```
Speed = BaseSpeed + (StoneStrength * SpeedPerStone)
Direction = Lerp(CurrentDirection, ToPlayer, ChaseAggressiveness)
```

**Strengthening Algorithm**:
```
For each tile in radius:
    Distance = GetDistance(generator, tile)
    Falloff = FalloffCurve.Evaluate(Distance / Radius)
    Strength += StrengthPerSecond * Falloff * DeltaTime
```

### 4. Player System (`PlayerController.cs`, `IPlayerController.cs`)
**Responsibility**: Handles player movement and collision

**Key Functions**:
- Processes touch input for movement
- Checks collision with gravestones
- Maintains player state (alive/dead)

**Interactions**:
- Provides position to Trail system
- Checked by Map for gravestone collision
- Target for Generator chasing

**Input Processing**:
```
TouchDelta = TouchPosition - PlayerPosition
If (TouchDelta.magnitude > MinMoveDistance):
    TargetVelocity = TouchDelta.normalized * MoveSpeed
    Velocity = Lerp(Velocity, TargetVelocity, InputSmoothness)
```

### 5. Trail System (`TrailSystem.cs`, `EncirclementDetector.cs`)
**Responsibility**: Manages player trail and detects encirclements

**Key Functions**:
- Maintains list of trail points
- Detects when trail forms closed loop
- Identifies encircled generators
- Updates trail visual

**Interactions**:
- Follows Player position
- Checks Generators for encirclement
- Notifies GameManager of kills

**Encirclement Algorithm**:
```
1. Check if trail head is near trail tail (closed loop)
2. If closed, create polygon from trail points
3. For each generator:
   - Use ray casting to check if inside polygon
   - If inside, mark for destruction
```

### 6. Telemetry System (`TelemetrySystem.cs`, `ITelemetrySystem.cs`)
**Responsibility**: Provides textless visual feedback

**Key Functions**:
- Screen edge glow based on danger level
- Visual effects for events (spawn, death, encirclement)
- Score visualization without text

**Interactions**:
- Reads Player position and Map stone strength
- Triggered by GameManager for events
- Updates UI elements

**Danger Calculation**:
```
StoneAtPlayer = Map.GetStrengthAtWorldPos(PlayerPos)
NearbyGenerators = Generators.GetInRadius(PlayerPos, DangerRadius)
DangerLevel = StoneAtPlayer + (NearbyGenerators.Count * 0.1)
ScreenGlow.Color = DangerGradient.Evaluate(DangerLevel)
```

## Data Flow

### Frame Update Flow
```
GameManager.Update()
├── InputSystem.Process()
├── PlayerController.UpdateMovement()
├── TrailSystem.UpdateTrail()
├── GeneratorManager.UpdateGenerators()
│   ├── Generator.UpdateMovement()
│   └── Generator.ApplyStrengthening()
├── MapSystem.UpdateTileVisuals()
├── EncirclementDetector.CheckEncirclements()
├── TelemetrySystem.UpdateDanger()
└── CameraController.UpdatePosition()
```

### Encirclement Kill Flow
```
TrailSystem.IsTrailClosed()
└── EncirclementDetector.FindEncircledGenerators()
    └── GeneratorManager.KillGenerators()
        ├── Generator.PlayDeathEffect()
        ├── GameManager.AddScore()
        └── TelemetrySystem.ShowSuccess()
```

## Configuration Structure

All tunable parameters are stored in ScriptableObject configs:

- **GameConfig**: Master config with references to all others
- **MapConfig**: Map size, tile properties, visual bands
- **GeneratorConfig**: Spawn rates, movement, strengthening
- **PlayerConfig**: Movement speed, trail properties

This allows designers to tweak gameplay without code changes.

## Powerup Hook Points

The system includes hooks for future powerup implementation:

1. **Player Powerups**:
   - `ApplySpeedBoost()`: Temporary speed increase
   - `ApplyInvincibility()`: Temporary gravestone immunity

2. **Trail Powerups**:
   - `ApplyTrailEnhancement()`: Longer trail for easier encirclement

3. **Generator Powerups**:
   - `FreezeAllGenerators()`: Stop all generator movement
   - `SlowAllGenerators()`: Reduce generator speed

4. **Game Manager**:
   - `OnPowerupCollected()`: Central powerup handling

## Performance Considerations

1. **Tile Updates**: Only update changed tiles, batch visual updates
2. **Generator Spawning**: Use object pooling for generators
3. **Trail Points**: Limit trail length, simplify when possible
4. **Encirclement**: Only check when trail closes, use spatial partitioning
5. **Effects**: Pool visual effect objects

## Unity Integration

1. Create empty GameObjects for each system
2. Attach system MonoBehaviours
3. Create ScriptableObject assets for configs
4. Link configs in GameManager
5. Set up Tilemap with strength band tiles
6. Configure LineRenderer for trail
7. Set up UI Canvas for telemetry effects

## Build Order

1. Set up basic Unity scene with Tilemap
2. Implement MapSystem (tile management)
3. Implement PlayerController (movement only)
4. Add TrailSystem (visual only)
5. Implement StoneGenerator (movement and strengthening)
6. Add EncirclementDetector
7. Connect systems through GameManager
8. Add TelemetrySystem for feedback
9. Polish and tune with ScriptableObject configs

## Testing Approach

1. **Unit Tests**: Each system testable in isolation
2. **Integration Tests**: System interactions
3. **Playtesting**: Tune configs for fun gameplay
4. **Performance**: Profile on target devices

## Future Enhancements

- Powerup system implementation
- Multiple generator types
- Special tiles (slow zones, teleporters)
- Progression system
- Leaderboards
- Sound and haptics