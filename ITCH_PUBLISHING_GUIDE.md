# Publishing InGraved to itch.io

## Quick Steps

### 1. Build in Unity
1. Open Unity
2. `File → Build Settings → WebGL`
3. Settings to use:
   - Compression: **Gzip** (works best on itch)
   - Texture Compression: **ETC2** 
   - Code Optimization: **Shorter Build Time** (for testing) or **Runtime Speed** (for release)
4. Click **Build** and select `Builds/WebGL` folder

### 2. Test Locally
```bash
cd Builds/WebGL
python3 -m http.server 8000
# Open http://localhost:8000 in browser
```

### 3. Prepare for itch.io
```bash
cd Builds/WebGL
zip -r InGraved.zip index.html Build TemplateData
```

### 4. Upload to itch.io
1. Go to [itch.io dashboard](https://itch.io/dashboard)
2. Create new project
3. Upload the ZIP file
4. Settings:
   - **Kind**: HTML
   - **Embed**: ✓ This file will be played in the browser
   - **Viewport**: 1920 x 1080
   - **Fullscreen**: ✓ Enable
   - **Mobile Friendly**: ✓ Enable

### 5. Game Page Settings
- **Title**: InGraved
- **Genre**: Action/Arcade
- **Tags**: `arcade`, `action`, `survival`, `web`
- **Description**: 
  ```
  Survive as long as you can in a world that turns to stone!
  
  Draw loops to capture enemies, avoid the spreading stone, 
  and see how long you can last.
  
  Controls: Move mouse to guide your character
  ```

## Troubleshooting

### If tiles don't show:
- Already fixed! Tiles are now in Resources folder

### If game doesn't load:
- Try Compression: **Disabled** in Build Settings
- Make sure to zip the contents, not the folder

### If performance is poor:
- Already optimized! WebGLSetup script handles this

## Cover Image Size
- itch.io recommends: **630 x 500px**
- Banner: **1920 x 620px** (optional)

## Test Link Format
Your game will be at: `https://[your-username].itch.io/ingraved`