# App Icon Setup Guide

## Icons Generated from Ethereal Texture

All app icons have been generated in `Assets/AppIcons/` folder.

### For Unity (iOS Build):
1. Open Unity
2. Go to **File → Build Settings → iOS → Player Settings**
3. In **Icon** section, drag and drop:
   - **180x180**: `Icon-iPhone-180.png` → iPhone 180x180
   - **120x120**: `Icon-iPhone-120.png` → iPhone 120x120
   - **167x167**: `Icon-iPad-167.png` → iPad Pro 167x167
   - **152x152**: `Icon-iPad-152.png` → iPad 152x152
   - **76x76**: `Icon-iPad-76.png` → iPad 76x76
   - **1024x1024**: `Icon-AppStore-1024.png` → App Store 1024x1024

### For Xcode (After iOS Build):
1. After building for iOS in Unity
2. Open the Xcode project
3. Select **Unity-iPhone** target
4. Go to **App Icons and Launch Images**
5. Drag icons from `Assets/AppIcons/` to the corresponding slots

### For Web/itch.io:
- Use `Icon-Web-512.png` for itch.io cover image
- Use `Icon-Web-256.png` for smaller web icons

### Icon Files Generated:
```
Assets/AppIcons/
├── Icon-1024.png (Base high-res)
├── Icon-AppStore-1024.png (App Store)
├── Icon-iPhone-180.png (iPhone 3x)
├── Icon-iPhone-120.png (iPhone 2x)
├── Icon-iPad-167.png (iPad Pro)
├── Icon-iPad-152.png (iPad 2x)
├── Icon-iPad-76.png (iPad 1x)
├── Icon-Notification-60.png
├── Icon-Notification-40.png
├── Icon-Notification-20.png
├── Icon-Settings-87.png
├── Icon-Settings-58.png
├── Icon-Settings-29.png
├── Icon-Web-512.png
└── Icon-Web-256.png
```

### Manual Xcode Setup:
If Unity doesn't apply icons automatically:
1. In Xcode, select `Images.xcassets`
2. Click on `AppIcon`
3. Drag each PNG to its corresponding slot
4. Make sure all required sizes are filled

### Android Icons:
For Android builds in Unity:
1. Go to Player Settings → Icon
2. Use `Icon-Web-512.png` for Adaptive Icon Background
3. Use `Icon-Web-512.png` for Adaptive Icon Foreground
4. Or use `Icon-Web-256.png` for Legacy Icon

The Ethereal texture gives the app a unique stone/crystal appearance!