#!/bin/bash
# =============================================
#  Windows Shutdown Helper - Build Script
#  Uses local .NET 8 SDK from tools/dotnet/
#  Falls back to system dotnet if local not found
# =============================================

set -e

# --- Colors ---
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
CYAN='\033[0;36m'
WHITE='\033[1;37m'
NC='\033[0m'

# --- Paths ---
TOOLS_DIR="$(cd "$(dirname "$0")" && pwd)"
PROJECT_ROOT="$(dirname "$TOOLS_DIR")"
SOLUTION_FILE="$PROJECT_ROOT/Windows Shutdown Helper.sln"
LOCAL_DOTNET="$TOOLS_DIR/dotnet/dotnet"

# --- Defaults ---
CONFIGURATION="Release"
SKIP_RESTORE=false
CLEAN=false
INSTALL_SDK=false

# --- Parse Arguments ---
while [[ $# -gt 0 ]]; do
    case "$1" in
        --debug)
            CONFIGURATION="Debug"
            shift
            ;;
        --release)
            CONFIGURATION="Release"
            shift
            ;;
        --skip-restore)
            SKIP_RESTORE=true
            shift
            ;;
        --clean)
            CLEAN=true
            shift
            ;;
        --install-sdk)
            INSTALL_SDK=true
            shift
            ;;
        --help|-h)
            echo "Usage: ./create-build.sh [OPTIONS]"
            echo ""
            echo "Options:"
            echo "  --debug          Build in Debug configuration"
            echo "  --release        Build in Release configuration (default)"
            echo "  --skip-restore   Skip NuGet package restore"
            echo "  --clean          Clean build output before building"
            echo "  --install-sdk    Download .NET 8 SDK into tools/dotnet/"
            echo "  --help, -h       Show this help message"
            exit 0
            ;;
        *)
            echo -e "${RED}Unknown option: $1${NC}"
            echo "Use --help to see available options."
            exit 1
            ;;
    esac
done

step() {
    echo ""
    echo -e "${CYAN}=== $1 ===${NC}"
}

# ============================================================
#  MAIN
# ============================================================

echo ""
echo -e "${WHITE}============================================${NC}"
echo -e "${WHITE}  Windows Shutdown Helper - Build Script${NC}"
echo -e "${WHITE}============================================${NC}"
echo "  Configuration : $CONFIGURATION"
echo "  Project Root  : $PROJECT_ROOT"

# --- Step 0: Install SDK if requested ---
if [ "$INSTALL_SDK" = true ]; then
    step "Installing .NET 8 SDK into tools/dotnet/"
    curl -sSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh
    chmod +x /tmp/dotnet-install.sh
    /tmp/dotnet-install.sh --channel 8.0 --install-dir "$TOOLS_DIR/dotnet"
    rm -f /tmp/dotnet-install.sh
    echo -e "  ${GREEN}[OK] .NET 8 SDK installed.${NC}"
fi

# --- Step 1: Find dotnet ---
step "Checking .NET SDK"
DOTNET=""

# 1) Local SDK in tools/dotnet/
if [ -x "$LOCAL_DOTNET" ]; then
    DOTNET="$LOCAL_DOTNET"
    export DOTNET_ROOT="$TOOLS_DIR/dotnet"
    DOTNET_VER=$("$DOTNET" --version 2>&1)
    echo -e "  ${GREEN}[OK]${NC} Local SDK: dotnet $DOTNET_VER"
# 2) System dotnet
elif command -v dotnet &>/dev/null; then
    DOTNET="dotnet"
    DOTNET_VER=$(dotnet --version 2>&1)
    echo -e "  ${GREEN}[OK]${NC} System SDK: dotnet $DOTNET_VER"
else
    echo -e "  ${RED}[ERROR] dotnet SDK not found!${NC}"
    echo ""
    echo -e "  ${YELLOW}Option 1: Install locally into this project:${NC}"
    echo -e "  ${YELLOW}  ./create-build.sh --install-sdk${NC}"
    echo ""
    echo -e "  ${YELLOW}Option 2: Install system-wide:${NC}"
    echo -e "  ${YELLOW}  Ubuntu/Debian : sudo apt install dotnet-sdk-8.0${NC}"
    echo -e "  ${YELLOW}  Fedora        : sudo dnf install dotnet-sdk-8.0${NC}"
    echo -e "  ${YELLOW}  macOS         : brew install dotnet-sdk${NC}"
    echo ""
    exit 1
fi

# --- Step 2: Clean (optional) ---
if [ "$CLEAN" = true ]; then
    step "Cleaning Build Output"
    "$DOTNET" clean "$SOLUTION_FILE" -c "$CONFIGURATION" --nologo -v q
    echo -e "  ${GREEN}[OK] Clean complete.${NC}"
fi

# --- Step 3: Restore ---
if [ "$SKIP_RESTORE" = false ]; then
    step "Restoring NuGet Packages"
    "$DOTNET" restore "$SOLUTION_FILE"
    echo -e "  ${GREEN}[OK] Packages restored.${NC}"
fi

# --- Step 4: Inject Build Info ---
step "Injecting Build Info"
BUILD_INFO_FILE="$PROJECT_ROOT/src/BuildInfo.cs"
if [ -f "$BUILD_INFO_FILE" ]; then
    COMMIT_HASH=$(git -C "$PROJECT_ROOT" rev-parse --short=6 HEAD 2>/dev/null || true)
    if [ -n "$COMMIT_HASH" ]; then
        sed -i "s/public const string CommitId = \"dev\"/public const string CommitId = \"$COMMIT_HASH\"/" "$BUILD_INFO_FILE"
        echo -e "  ${GREEN}[OK] CommitId set to: $COMMIT_HASH${NC}"
    else
        echo -e "  ${YELLOW}[SKIP] Git not available, keeping default CommitId.${NC}"
    fi
else
    echo -e "  ${YELLOW}[SKIP] BuildInfo.cs not found.${NC}"
fi

# --- Step 5: Build ---
step "Building Solution ($CONFIGURATION)"
"$DOTNET" build "$SOLUTION_FILE" -c "$CONFIGURATION" --no-restore

# --- Done ---
echo ""
echo -e "${GREEN}============================================${NC}"
echo -e "${GREEN}  BUILD SUCCEEDED${NC}"
echo -e "${GREEN}============================================${NC}"
echo -e "${WHITE}  Output: bin/$CONFIGURATION/net8.0-windows/${NC}"
echo ""
