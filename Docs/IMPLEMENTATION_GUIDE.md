# InGraved - Implementation Guide

## What Each System Should Do

### 🗺️ MapSystem
**Purpose**: Manage the tile grid and stone strengthening

**What it should do**:
1. **Track Stone Strength**
   - Maintain 2D array of float values (0.0 to 1.0)
   - 0.0 = soft earth (safe)
   - 1.0 = gravestone (deadly)

2. **Update Tiles from Generators**
   ```
   AddStrengthInRadius(center, radius, strength, falloff):
     for each tile in radius:
       distance = GetDistance(center, tile)
       if distance <= radius:
         falloffValue = falloffCurve.Evaluate(distance/radius)
         tile.strength += strength * falloffValue * deltaTime
         tile.strength = Min(1.0, tile.strength)
   ```

3. **Visualize Strength**
   ```
   UpdateTileVisuals():
     for each tile:
       band = Floor(tile.strength * 10)  // 0-9
       SetTileSprite(strengthBandTiles[band])
       if strength >= 1.0:
         MarkAsGravestone()
   ```

**Key Methods to Implement**:
- `AddStrengthInRadius()` - The core spreading mechanic
- `UpdateTileVisuals()` - Change tile appearance based on strength
- `IsGravestone()` - Check if player is touching death

---

### 🎯 GeneratorManager + StoneGenerator
**Purpose**: Spawn and control enemy generators

**What it should do**:

**GeneratorManager**:
1. **Spawn Generators**
   ```
   SpawnTimer countdown:
     if timer <= 0 and count < max:
       position = RandomPointInRing(player, minDist, maxDist)
       SpawnGenerator(position)
       timer = 1/spawnRate
   ```

2. **Check Encirclements**
   ```
   Each frame:
     if trail.IsClosed():
       for each generator:
         if IsInsidePolygon(generator.pos, trail):
           KillGenerator(generator)
           AddScore(100)
   ```

**StoneGenerator** (individual enemy):
1. **Chase Player**
   ```
   UpdateMovement(deltaTime):
     direction = (player.position - myPosition).normalized
     stoneHere = map.GetStrength(myPosition)
     speed = baseSpeed + (stoneHere * speedBonus)
     position += direction * speed * deltaTime
   ```

2. **Spread Stone**
   ```
   ApplyStrengthening(map):
     currentRadius = initialRadius + (aliveTime * growthRate)
     currentRadius = Min(currentRadius, maxRadius)
     map.AddStrengthInRadius(position, currentRadius, strengthPerSec, curve)
   ```

**Key Values**:
- Spawn 1 generator every 2 seconds
- Generators live until encircled
- Radius grows from 2 to 10 tiles over ~15 seconds
- Add 0.1 strength per second at center

---

### 🏃 PlayerController
**Purpose**: Handle player collision and death

**What it should do**:
1. **Check Gravestone Collision**
   ```
   CheckGravestoneCollision():
     myTile = map.WorldToTile(position)
     if map.GetStrength(myTile) >= 1.0:
       Kill()
       GameManager.EndGame()
   ```

2. **Work with Existing Movement.cs**
   - Movement.cs already handles input and motion
   - PlayerController just needs to check collision
   - Call CheckGravestoneCollision() every frame

**Integration**:
- Keep Movement.cs for movement
- PlayerController for game logic
- Both on same GameObject

---

### 🔄 TrailSystem
**Purpose**: Detect when trail encircles generators

**What it should do**:
1. **Track Trail Points**
   ```
   AddTrailPoint(position):
     if Distance(position, lastPoint) > minDistance:
       points.Add(position)
       if points.Count > maxPoints:
         points.RemoveAt(0)  // Remove oldest
   ```

2. **Detect Closed Loop**
   ```
   IsTrailClosed():
     if points.Count < 3: return false
     distance = Distance(points[0], points[Last])
     return distance < closureThreshold
   ```

3. **Work with EncirclementDetector**
   - EncirclementDetector has the polygon math
   - TrailSystem provides the points
   - GeneratorManager checks for kills

**Integration with Movement.cs**:
- Movement.cs already draws a trail
- Can either merge or run both
- TrailSystem needs the points for encirclement

---

### 📊 TelemetrySystem
**Purpose**: Visual feedback without text

**What it should do**:
1. **Danger Glow**
   ```
   UpdateDanger(playerPos):
     stoneThere = map.GetStrength(playerPos)
     nearbyGens = CountGeneratorsNear(playerPos, 5.0)
     danger = stoneThere + (nearbyGens * 0.2)
     screenEdge.color = Color.Lerp(clear, red, danger)
   ```

2. **Event Effects**
   - Generator spawn: Lightning strike prefab
   - Generator death: Explosion prefab
   - Encirclement: Success flash
   - Game over: Full screen red flash

**No Text Required**:
- Use color intensity for score
- Use pulse rate for danger
- Use particle count for multipliers

---

### 🎮 GameManager
**Purpose**: Coordinate everything

**What it should do**:
1. **Initialize Systems**
   ```
   StartGame():
     mapSystem.Initialize(mapConfig)
     generatorManager.Initialize(generatorConfig)
     playerController.Initialize(playerConfig)
     trailSystem.Initialize()
     score = 0
     timeAlive = 0
     state = Playing
   ```

2. **Game Loop**
   ```
   Update():
     if state == Playing:
       timeAlive += deltaTime
       score += deltaTime * 1
       
       // Check lose condition
       if playerController.CheckGravestoneCollision(map):
         EndGame()
       
       // Check encirclements
       if trail.IsClosed():
         encircled = FindEncircled(trail, generators)
         foreach gen in encircled:
           KillGenerator(gen)
           score += 100
   ```

3. **Difficulty Scaling**
   ```
   UpdateDifficulty():
     if timeAlive > 30:
       multiplier = 1 + (timeAlive-30)/60
       multiplier = Min(multiplier, 3)
       generatorManager.SetSpawnRate(base * multiplier)
   ```

---

## Implementation Order

### Phase 1: Get Generators Working
1. ✅ Spawn generator prefabs randomly
2. ⬜ Make them move toward player
3. ⬜ Make them faster on stone

### Phase 2: Stone System
1. ⬜ Implement AddStrengthInRadius
2. ⬜ Create tile visuals (10 sprites/colors)
3. ⬜ Update tile colors based on strength

### Phase 3: Core Game Loop
1. ⬜ Detect trail closure
2. ⬜ Check if generators inside polygon
3. ⬜ Kill encircled generators
4. ⬜ Check gravestone collision → game over

### Phase 4: Polish
1. ⬜ Add spawn/death effects
2. ⬜ Implement danger glow
3. ⬜ Add difficulty scaling
4. ⬜ Tune all values

---

## Code Snippets to Help

### For AddStrengthInRadius (MapSystem)
```csharp
public void AddStrengthInRadius(Vector2 worldCenter, float radius, float strength, AnimationCurve falloffCurve)
{
    Vector2Int centerTile = WorldToTilePos(worldCenter);
    int radiusInTiles = Mathf.CeilToInt(radius / config.tileSize);
    
    for (int x = -radiusInTiles; x <= radiusInTiles; x++)
    {
        for (int y = -radiusInTiles; y <= radiusInTiles; y++)
        {
            int tileX = centerTile.x + x;
            int tileY = centerTile.y + y;
            
            if (!IsInBounds(tileX, tileY)) continue;
            
            Vector2 tileWorldPos = TileToWorldPos(new Vector2Int(tileX, tileY));
            float distance = Vector2.Distance(worldCenter, tileWorldPos);
            
            if (distance <= radius)
            {
                float normalizedDist = distance / radius;
                float falloff = falloffCurve?.Evaluate(1f - normalizedDist) ?? (1f - normalizedDist);
                AddTileStrength(tileX, tileY, strength * falloff * Time.deltaTime);
            }
        }
    }
}
```

### For Generator Movement (StoneGenerator)
```csharp
public void UpdateGenerator(float deltaTime, float currentStoneStrength)
{
    if (!IsAlive || target == null) return;
    
    AliveTime += deltaTime;
    
    // Grow radius over time
    CurrentRadius = Mathf.Min(
        config.initialRadius + (AliveTime * config.radiusGrowthRate),
        config.maxRadius
    );
    
    // Move toward player
    Vector2 toPlayer = ((Vector2)target.position - Position).normalized;
    float speed = config.baseSpeed + (currentStoneStrength * config.speedPerStoneStrength);
    
    Vector2 newVelocity = toPlayer * speed;
    Velocity = Vector2.Lerp(Velocity, newVelocity, config.chaseAggressiveness);
    
    transform.position += (Vector3)Velocity * deltaTime;
}
```

### For Encirclement Check (GeneratorManager)
```csharp
void CheckEncirclements()
{
    if (!trailSystem.IsTrailClosed()) return;
    
    var encircled = EncirclementDetector.FindEncircledGenerators(
        trailSystem.TrailPoints, 
        activeGenerators
    );
    
    foreach (var gen in encircled)
    {
        gameManager.OnGeneratorKilled(gen.Position);
        KillGenerator(gen, true);
    }
    
    if (encircled.Count > 0)
    {
        trailSystem.ClearTrail();  // Reset after successful encirclement
    }
}
```

---

## Testing Tips

### Quick Tests
1. **Stone Spread**: Set strengthPerSecond to 1.0 to see instant gravestones
2. **Encirclement**: Set generator speed to 0 to practice circling
3. **Difficulty**: Set spawnRate to 5.0 to stress test
4. **Death**: Set initial tile strength to 0.99 for quick death test

### Debug Helpers
```csharp
// In MapSystem - Visualize strength values
void OnDrawGizmos()
{
    if (tileStrengths == null) return;
    for (int x = 0; x < config.mapWidth; x++)
        for (int y = 0; y < config.mapHeight; y++)
        {
            Gizmos.color = Color.Lerp(Color.black, Color.white, tileStrengths[x,y]);
            Gizmos.DrawCube(TileToWorldPos(new Vector2Int(x,y)), Vector3.one * 0.8f);
        }
}
```

---

## Common Problems & Solutions

**Generators not chasing?**
- Check target is assigned
- Ensure player is tagged "Player"
- Verify chase aggressiveness > 0

**Stone not spreading?**
- Check ApplyStoneStrengthening is called
- Verify strengthPerSecond > 0
- Make sure map system is initialized

**Encirclement not working?**
- Trail must be closed (head near tail)
- Need at least 3 points
- Generator must be fully inside

**Instant death on start?**
- Initial tile strength should be 0
- Check spawn position isn't on stone
- Verify collision bounds

**Can't see stone changes?**
- Need different sprites/colors per band
- UpdateTileVisuals must be called
- Tilemap renderer must be visible