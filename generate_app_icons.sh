#!/bin/bash

# Source image
SOURCE="Assets/Graphics/Ethereal.png"
OUTPUT_DIR="Assets/AppIcons"

# First create a high-quality 1024x1024 base image
echo "Creating base 1024x1024 icon..."
sips -z 1024 1024 "$SOURCE" --out "$OUTPUT_DIR/Icon-1024.png"

# iOS App Icon sizes (required for App Store)
echo "Generating iOS icons..."
sips -z 1024 1024 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-AppStore-1024.png"
sips -z 180 180 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-iPhone-180.png"
sips -z 120 120 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-iPhone-120.png"
sips -z 167 167 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-iPad-167.png"
sips -z 152 152 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-iPad-152.png"
sips -z 76 76 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-iPad-76.png"

# Notification icons
sips -z 60 60 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-Notification-60.png"
sips -z 40 40 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-Notification-40.png"
sips -z 20 20 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-Notification-20.png"

# Settings icons
sips -z 87 87 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-Settings-87.png"
sips -z 58 58 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-Settings-58.png"
sips -z 29 29 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-Settings-29.png"

# Web/itch.io icon
sips -z 512 512 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-Web-512.png"
sips -z 256 256 "$OUTPUT_DIR/Icon-1024.png" --out "$OUTPUT_DIR/Icon-Web-256.png"

echo "All icons generated in $OUTPUT_DIR!"
echo ""
echo "iOS icons generated:"
ls -la "$OUTPUT_DIR"/*.png | awk '{print $NF}' | xargs -I {} basename {}
