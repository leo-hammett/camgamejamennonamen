# Steps to Fix "Old Version" iOS Build Issue

## Quick Fix Checklist:

### 1. In Unity:
- [ ] **File → Save Project** 
- [ ] **File → Save Scene** (Cmd+S)
- [ ] Close Unity completely

### 2. Clean Caches:
```bash
# Run these commands in Terminal
cd /Users/leohammett/GithubClones/camgamejam/camgamejamennonamen
rm -rf Library/BuildCache
rm -rf Library/il2cpp_cache  
rm -rf Temp/
rm -rf "IOS Build"  # Delete old build completely
```

### 3. Reopen Unity and Build Fresh:
1. Open Unity
2. **File → Build Settings**
3. Select **iOS**
4. Click **Player Settings**
5. Check version shows: **1.1** (just updated!)
6. Click **Build** (not Build And Run)
7. Create NEW folder: `IOS Build Fresh`
8. Let it build completely

### 4. In Xcode:
1. Open the NEW build folder
2. **Product → Clean Build Folder** (Shift+Cmd+K)
3. **Product → Build** (Cmd+B)

### 5. If STILL showing old version:
- Delete app from device/simulator
- In Xcode: **Window → Devices and Simulators**
- Select your device → find app → delete it
- Reinstall fresh

## Common Issues:
- **Unity Cloud Build**: Disable if enabled
- **Multiple Build Folders**: Make sure you're opening the right one
- **Addressables**: Clear addressables cache if using them

## Version is now: 1.1
The build number has been incremented to force a new build!