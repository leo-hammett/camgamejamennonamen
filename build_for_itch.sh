#!/bin/bash

echo "========================================="
echo "     InGraved - Build for itch.io"
echo "========================================="

# Set paths
UNITY_PATH="/Applications/Unity/Hub/Editor/2022.3.62f1/Unity.app/Contents/MacOS/Unity"
PROJECT_PATH="/Users/leohammett/GithubClones/camgamejam/camgamejamennonamen"
BUILD_PATH="$PROJECT_PATH/Builds/WebGL"

# Create build directory
mkdir -p "$BUILD_PATH"

echo "Building WebGL..."
"$UNITY_PATH" -batchmode -quit \
    -projectPath "$PROJECT_PATH" \
    -buildTarget WebGL \
    -executeMethod BuildScript.PerformWebGLBuild \
    -logFile build.log

echo "Build complete! Files in: $BUILD_PATH"
echo ""
echo "Next steps:"
echo "1. Navigate to $BUILD_PATH"
echo "2. Zip the contents (index.html, Build/, TemplateData/)"
echo "3. Upload to itch.io"
echo ""
echo "Quick zip command:"
echo "cd $BUILD_PATH && zip -r InGraved_WebGL.zip index.html Build TemplateData"