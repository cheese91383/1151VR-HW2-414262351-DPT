# 1151VR-HW2-414262351-DPT

### Controls

| Key | Action |
|---|---|
| `←` / `→` | Move left / right |
| `Space` | Jump |

### Game Rules

1. Start on the ground, jump across platforms of different heights, and reach the flag at the top right.
2. Touching a spike shows **GAME OVER**. Press Restart to try again.
3. Touching the flag makes a diamond pop out beside it, then **YOU WIN** appears. Press Play again to replay.
4. Coins on the way bounce up, spin, and disappear when touched.

### Development Process

1. **Create the project**
2. **Import assets**:
   - Place the character images (`charater1`, `charater2`), terrain tiles (`tile_xxxx`), and button images in `Assets/Sprites`, and the font in `Assets/Fonts`.
   - For every image, set Texture Type to `Sprite (2D and UI)` and **Filter Mode to Point (no filter)**.
   - Asset packs used:
     - [Pixel Platformer](https://kenney.nl/assets/pixel-platformer): character, terrain, background tiles, flag, diamond, coins, spikes
     - [UI Pack Pixel Adventure](https://kenney.nl/assets/ui-pack-pixel-adventure): button images
     - [Kenney Fonts](https://kenney.nl/assets/kenney-fonts): Kenney Future font
3. **Build the ground and background**:
   - `Ground`: a grass Sprite in Tiled draw mode with a `Box Collider 2D`, on the `Ground` layer.
   - Background: built with a Tilemap and Tile Palette from sky, cloud, and dark-fill tiles. The background tiles use a Pixels Per Unit of 18 so that one tile equals exactly one unit. The Tilemap's Order in Layer is negative, so it renders behind the game objects.
   - Trees are a separate Tilemap with a `Tilemap Collider 2D` on the `Ground` layer, so the player can stand on the leaves.
4. **Create the player**:
   - Add `Rigidbody2D` and `Collider2D`, and set the Tag to `Player`.
   - Add a child object `Groundtest` at the feet, used for ground detection.
   - Attach `PlayerController` and assign the idle / walking sprites, the ground check, and the `Ground` layer.
   - Apply the `NoFrictions` physics material (Friction = 0) so the player does not stick to walls.
5. **Make prefabs**: `Platform`, `coin1` / `coin2` (coins), and `Diamond`, saved in `Assets/Prefabs`.
6. **Level generation**: when the game starts, `LevelBuilder` uses `Instantiate` to spawn platforms and coins at the coordinates stored in the `platformPositions` and `coinPositions` arrays, and then enables the flag.
7. **Interaction scripts**:
   - `Spike`: a trigger that detects the player and calls `GameManager.GameOver()`.
   - `Coin`: after being touched, a coroutine moves the coin up in an arc and plays a frame animation, then calls `Destroy`.
   - `Goal`: when triggered, spawns a diamond, gives it an upward velocity, and calls `GameManager.Win()`.
   - `Diamond`: when it lands on the top surface of a `Ground` object, it becomes Static and stays in place.
8. **UI**: a Canvas containing `GameOverPanel`, `WinPanel`, and a Restart button (all hidden by default). `GameManager` controls showing them, pausing the game (`Time.timeScale`), and reloading the scene.

### Scripts

| Script | Purpose |
|---|---|
| `PlayerController.cs` | Horizontal movement, jumping, ground detection (`Physics2D.OverlapCircle`), swapping sprites between moving and idle, and flipping by direction |
| `LevelBuilder.cs` | Spawns platforms and coins from the array coordinates and enables the flag |
| `Coin.cs` | After the player touches it, a coroutine raises the coin and cycles through the `frames` sprites, then removes it |
| `Goal.cs` | Flag trigger: spawns the diamond and calls `Win()`; the `reached` flag prevents repeat triggers |
| `Diamond.cs` | Uses the collision normal to check for a landing from above, then zeroes the velocity and makes the body Static |
| `Spike.cs` | Trigger: calls `GameOver()` when the player touches it |
| `GameManager.cs` | Shows the Game Over / Win panels, pauses the game, and reloads the scene on Restart / Play again |

### Use of Vectors

| Script | Use |
|---|---|
| `PlayerController.cs` | Sets the player's velocity with a `Vector2`: horizontal speed when moving, vertical speed when jumping, and the other axis keeps its current value |
| `Goal.cs` | Adds `Vector3` values to compute the diamond's spawn position, and uses a `Vector2` to give the diamond an upward initial velocity |
| `Coin.cs` | Uses `Vector3.up` with addition and scalar multiplication to move the coin upward along an arc |
| `Diamond.cs` | Uses the collision contact normal to detect a landing from above, then sets the velocity to `Vector2.zero` |
| `LevelBuilder.cs` | Uses `Vector2` to represent the position of every platform and coin |

### Use of Arrays

**1. `Vector2[] platformPositions` (`LevelBuilder.cs`)**
Stores the coordinates of all platforms. `Start()` loops over the whole array and spawns one platform per element:

```csharp
for (int i = 0; i < platformPositions.Length; i++)
{
    Instantiate(platformPrefab, platformPositions[i], Quaternion.identity);
}
```

**2. `Vector2[] coinPositions` (`LevelBuilder.cs`)**
Stores the coin coordinates and spawns them with the same kind of loop.

**3. `Sprite[] frames` (`Coin.cs`)**
Stores the images for the coin's spinning animation. Once the coin is touched, the coroutine computes an index from the elapsed time and shows the images in the array in turn:

```csharp
int index = (int)(t / frameTime) % frames.Length;
sr.sprite = frames[index];
```

`% frames.Length` keeps the index within the array and makes it loop, which produces a continuous spinning effect.

`platformPositions` and `coinPositions` are **arrays of `Vector2`**: the array manages a set of coordinates in one place, each `Vector2` describes one coordinate, and `Instantiate` builds the level dynamically when the game starts.

### Links

- YouTube: https://www.youtube.com/watch?v=3CQoHfLvPxg
- GitHub: https://github.com/cheese91383/1151VR-HW2-414262351-DPT
