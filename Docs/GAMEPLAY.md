# InGraved - Gameplay Documentation

## Game Concept
**InGraved** is a mobile survival game where you control a digging character trying to survive in a world that's gradually turning to stone. Enemy "Stone Generators" spawn randomly and harden the terrain around them. Your only defense is to encircle them with your trail to destroy them before the entire map becomes impassable gravestones.

## Core Gameplay Loop

### 1. **You Dig Through Terrain**
- Move your character by touching/clicking - character follows your finger/mouse
- Character automatically digs through soft terrain as it moves
- Character leaves a glowing trail behind showing where you've been

### 2. **Stone Generators Spawn and Chase You**
- Lightning strikes announce new generator spawns
- Generators are enemy entities that chase you
- They move faster when on harder stone (they surf on their own creation)
- Multiple generators can spawn - difficulty increases over time

### 3. **Terrain Hardens Into Stone**
- Each generator radiates a growing circle of influence
- Tiles within their radius gradually harden (0% → 100% strength)
- When multiple generator radii overlap, hardening accelerates
- Visual feedback: tiles change color from soft (dark) to hard (bright)

### 4. **Gravestones = Death**
- When a tile reaches 100% strength, it becomes a gravestone
- If your character touches a gravestone tile = GAME OVER
- Gravestones are permanent - they never soften

### 5. **Encircle Generators to Kill Them**
- Your trail persists for a limited time/length behind you
- If you loop your trail to create a closed circle with a generator inside = generator dies
- Dead generators explode and stop hardening terrain (but existing stone remains)
- Score points for each kill

## Detailed Mechanics

### Player Movement
- **Input**: Touch/click and hold - character moves toward finger/cursor
- **Speed**: Constant (not affected by terrain)
- **Trail**: 
  - Fixed maximum length (e.g., 100 points)
  - Older segments fade/disappear
  - Shows last ~5 seconds of movement
  - Cannot cross itself (would break the trail)

### Stone Generators
- **Spawning**:
  - Random positions, min/max distance from player
  - Lightning effect warns of spawn location
  - Spawn rate increases over time
  - Max number alive at once (e.g., 10)

- **Behavior**:
  - Always move toward player
  - Base speed + bonus speed based on stone strength under them
  - On soft ground: slow
  - On hard stone: fast (they surf on it!)

- **Stone Generation**:
  - Radius starts small (2 tiles)
  - Radius grows over time (up to max ~10 tiles)
  - Strength added per second with distance falloff
  - Center = strongest, edge = weakest
  - Multiple generators stack their effects

### Encirclement System
- **Detection**:
  - Check if trail head connects near trail tail
  - If yes, trail forms a polygon
  - Check which generators are inside polygon
  - Kill all encircled generators

- **Visual Feedback**:
  - Trail glows brighter when near closing
  - Success flash when encirclement completes
  - Explosion effect on killed generators

### Map & Tiles
- **Grid System**:
  - Fixed size map (e.g., 100x100 tiles)
  - Each tile has strength value (0.0 to 1.0)
  - Hard boundaries at edges

- **Strength Visualization**:
  - 10 visual bands (0-10%, 10-20%, etc.)
  - Color gradient from dark (soft) to bright (hard)
  - Special visual for gravestones (100%)

### Difficulty Progression
- **Time-Based Scaling**:
  - 0-30 seconds: Grace period, easy spawning
  - 30-60 seconds: Spawn rate increases
  - 60+ seconds: Maximum difficulty

- **Dynamic Difficulty**:
  - More generators = higher score multiplier
  - But also more danger!

## Telemetry (Visual Feedback)

### No Text - Pure Visual Communication

1. **Danger Indicator**:
   - Screen edges glow red based on nearby stone strength
   - Intensity = how close you are to gravestones
   - Pulse rate increases with danger

2. **Generator Spawning**:
   - Lightning strike effect at spawn point
   - Screen flash when new generator appears

3. **Encirclement Success**:
   - Explosion effect on killed generators
   - Screen flash in positive color (green/gold)
   - Ripple effect from kill location

4. **Score Feedback**:
   - Visual elements grow/pulse on score gain
   - Particle effects for big scores
   - No numbers - size/intensity shows magnitude

## Winning vs Losing

### Losing Conditions
- Touch a gravestone (100% strength tile) = instant death
- No timer - survive as long as possible

### Scoring System
- **Base Points**:
  - 100 points per generator killed
  - 1 point per second survived
  
- **Multipliers**:
  - More active generators = higher multiplier
  - Encircling multiple generators at once = bonus

### High Score Chase
- Game is endless - goal is highest score
- Difficulty keeps ramping up
- Eventually becomes impossible
- Leaderboard compares survival times and scores

## Strategic Depth

### Risk vs Reward
- **Safe Play**: Kill generators immediately
  - Lower score (fewer active = low multiplier)
  - Easier to manage
  - Slower stone spread

- **Risky Play**: Let generators accumulate
  - Higher score multiplier
  - Can encircle multiple at once
  - But stone spreads faster!

### Tactical Decisions
1. **When to Encircle**:
   - Wait for multiple generators to group up?
   - Or kill them one by one?

2. **Where to Move**:
   - Stay in soft areas (safer but generators spawn anywhere)
   - Or kite generators to already-hard areas?

3. **Trail Management**:
   - Short quick loops for single kills
   - Or long complex paths for multi-kills?

## Visual Style

### Aesthetics
- **Dark/Gothic**: Stone and grave theme
- **Glowing Trail**: Bright cyan/blue against dark background
- **Particle Effects**: Lightning, explosions, stone dust
- **Color Language**:
  - Blue/Cyan = Player/Safety
  - Red/Orange = Danger/Generators
  - Gray/White = Stone progression
  - Green/Gold = Success/Score

### Mobile-First Design
- **One-finger control**: Everything done with single touch
- **Clear Visual Hierarchy**: 
  - Player always visible
  - Generators stand out
  - Trail is bright and clear
- **No UI Clutter**: Information shown through world elements

## Planned Powerups (Future)

System has hooks for powerups but not implemented yet:

1. **Speed Boost**: Move faster temporarily
2. **Long Trail**: Trail lasts longer for easier encirclement
3. **Freeze**: All generators stop moving
4. **Bomb**: Clear stone in an area
5. **Shield**: Pass through gravestones briefly

## Technical Implementation Status

### Working Now (from existing code)
- ✅ Player follows mouse/touch
- ✅ Trail renders behind player
- ✅ Trail self-intersection detection
- ✅ Camera follows player

### Ready to Implement (stubs in place)
- ⏳ Stone generators spawning
- ⏳ Generators chasing player
- ⏳ Stone strength spreading
- ⏳ Tile visualization by strength
- ⏳ Encirclement killing generators
- ⏳ Gravestone collision
- ⏳ Score tracking
- ⏳ Danger telemetry

### TODO Priority Order
1. Get generators spawning and moving
2. Make them spread stone strength
3. Visualize stone on tiles
4. Detect encirclement → kill generators
5. Add gravestone collision → game over
6. Polish with effects and telemetry

## Tuning Guidelines

### Balance Goals
- First generator kill: ~10-15 seconds
- First death: 1-2 minutes for new players
- Skilled players: 3-5 minutes
- Expert players: 5-10 minutes

### Key Parameters (in ScriptableObjects)
- `baseSpawnRate`: 0.5/sec feels fair
- `maxAliveGenerators`: 10 creates pressure
- `strengthPerSecond`: 0.1 gives time to react
- `trailMaxPoints`: 100 allows one big loop
- `generatorBaseSpeed`: 2.0 (slower than player)
- `speedPerStoneStrength`: 3.0 (fast on stone!)

## Fun Factors

### What Makes It Fun
1. **Constant Pressure**: Generators never stop coming
2. **Clear Feedback**: You see stone spreading
3. **Big Moments**: Multi-generator encirclements
4. **Risk/Reward**: Greed vs safety
5. **Simple to Learn**: One-touch control
6. **Hard to Master**: Optimal path planning
7. **Quick Sessions**: 2-5 minute games

### Emotional Journey
1. **Start**: Calm, learning
2. **Early Game**: Confident, controlling
3. **Mid Game**: Pressure building
4. **Late Game**: Frantic survival
5. **Death**: "One more try!"