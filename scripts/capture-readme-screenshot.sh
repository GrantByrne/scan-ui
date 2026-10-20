#!/usr/bin/env bash
set -Eeuo pipefail

repo_root="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")/.." && pwd)"
project="$repo_root/src/ScanUi.App/ScanUi.App.csproj"
app="$repo_root/src/ScanUi.App/bin/Debug/net10.0/ScanUi.App"
screenshot="$repo_root/docs/images/scan-ui.png"

dotnet_command="${DOTNET:-}"
if [[ -z "$dotnet_command" ]]; then
    if command -v dotnet >/dev/null 2>&1; then
        dotnet_command="$(command -v dotnet)"
    elif [[ -x "$HOME/.dotnet/dotnet" ]]; then
        dotnet_command="$HOME/.dotnet/dotnet"
        export DOTNET_ROOT="$HOME/.dotnet"
    else
        printf 'The .NET 10 SDK is required to capture the app screenshot.\n' >&2
        exit 1
    fi
fi

if ! command -v spectacle >/dev/null 2>&1; then
    printf 'KDE Spectacle is required to capture the app window.\n' >&2
    exit 1
fi

if ! command -v qdbus6 >/dev/null 2>&1; then
    printf 'qdbus6 is required to focus the app window through KDE KWin.\n' >&2
    exit 1
fi

if [[ -z "${DISPLAY:-}${WAYLAND_DISPLAY:-}" ]]; then
    printf 'A graphical desktop session is required to capture the app window.\n' >&2
    exit 1
fi

mkdir -p "$(dirname -- "$screenshot")"
"$dotnet_command" build "$project" --configuration Debug

if [[ ! -x "$app" ]]; then
    printf 'The app executable was not produced at %s.\n' "$app" >&2
    exit 1
fi

kwin_script="$(mktemp --suffix=.js)"
kwin_plugin="scanui-screenshot-$$"
cat > "$kwin_script" <<'EOF'
const target = workspace.windowList().find(window => window.caption.includes("Folio — Document Studio"));
if (target) {
    workspace.activeWindow = target;
}
EOF

"$app" &
app_pid=$!
cleanup() {
    qdbus6 org.kde.KWin /Scripting unloadScript "$kwin_plugin" >/dev/null 2>&1 || true
    rm -f "$kwin_script"
    if kill -0 "$app_pid" 2>/dev/null; then
        kill "$app_pid" 2>/dev/null || true
        wait "$app_pid" 2>/dev/null || true
    fi
}
trap cleanup EXIT
trap 'exit 130' INT
trap 'exit 143' TERM

for ((second = 0; second < 8; second++)); do
    if ! kill -0 "$app_pid" 2>/dev/null; then
        wait "$app_pid" || true
        printf 'The ScanUi app exited before its window could be captured.\n' >&2
        exit 1
    fi
    sleep 1
done

qdbus6 org.kde.KWin /Scripting loadScript "$kwin_script" "$kwin_plugin" >/dev/null
qdbus6 org.kde.KWin /Scripting start >/dev/null
sleep 2


spectacle --background --activewindow --no-decoration --no-shadow --nonotify \
    --output "$screenshot"

if [[ ! -s "$screenshot" ]]; then
    printf 'Spectacle did not create the screenshot at %s.\n' "$screenshot" >&2
    exit 1
fi

printf 'Captured the ScanUi app window to %s.\n' "$screenshot"
