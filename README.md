# SuperSpinner

## 🎰 Overview
SuperSpinner is a Unity-based casino spinner game with network connectivity, reactive programming (UniRx), and professional architecture patterns.

## 🏗️ Architecture

### **Design Patterns**
- ✅ **Singleton Pattern** - AudioManager, SpinnerService
- ✅ **Dependency Injection** - Interface-based dependencies
- ✅ **Reactive Programming** - UniRx for state management
- ✅ **Async/Await** - Clean asynchronous operations
- ✅ **Interface Segregation** - IAudioManager, ISpinnerService

### **Project Structure**
```
Assets/
├── Scripts/
│   ├── Audio/              # Audio management
│   ├── Services/           # Network & API services
│   ├── UI/                 # UI components & screens
│   ├── Models/             # Data models
│   ├── Interfaces/         # Service interfaces
│   └── GameManager.cs      # Core game management
├── Tests/
│   └── EditMode/           # Unit tests
└── Resources/              # Audio, configs, prefabs
```

## 🚀 Features
- 🎡 Animated spinner reel with DOTween
- 🔊 Dynamic audio with pitch variation
- 🌐 RESTful API integration
- ⏱️ Network timeout handling (10s)
- 🎨 Reactive UI updates with UniRx
- 🧪 Unit tests
- 📱 Responsive UI animations

## 🧪 Testing

### **Run Tests**
1. Open `Window → General → Test Runner`
2. Click **Run All** for Edit Mode tests

### **Test Coverage**
- ✅ Service mocking
- ✅ Business logic validation
- ✅ Network timeout scenarios

## 🔧 Technologies
- **Unity** - Game engine
- **C# 9.0** - Programming language
- **.NET Standard 2.1** - Target framework
- **UniRx** - Reactive extensions
- **DOTween** - Animation library
- **NUnit** - Testing framework
- **UnityWebRequest** - Network API

## 📦 Dependencies
- UniRx (Reactive Extensions)
- DOTween (Animation)
- Unity Test Framework
- TextMeshPro

## 🛠️ Setup
1. Clone the repository
2. Open project in Unity 2021.3+
3. Install required packages via Package Manager
4. Configure API URL in `Resources/Configuration`
5. Run tests to verify setup

## 📦 Pre-Built Releases

Ready-to-run builds are available in the `Build/` folder:

### **Desktop Build** (`Build/Desktop/`)
- **Platform**: Windows (x64)
- **Executable**: `Super Spinner.exe`
- **Requirements**: Windows 10/11
- **Usage**: Run `Super Spinner.exe` to play on PC

### **Mobile Build** (`Build/Mobile/`)
- **Platform**: Android
- **File**: `SuperSpinner.apk`
- **Requirements**: Android 5.0+ (API Level 21+)
- **Installation**: 
  1. Transfer `.apk` to your Android device
  2. Enable "Install from Unknown Sources" in Settings
  3. Install and run the app

> **Note**: Debug information folders (`*_BurstDebugInformation_DoNotShip`, `*_BackUpThisFolder_ButDontShipItWithYourGame`) should not be distributed with final releases.

## 🌐 API Integration
The game connects to a backend API for:
- Fetching spinner prize values
- Requesting spin results
- Timeout: 10 seconds per request
- Auto-retry capability

## 📝 Code Quality
- ✅ XML documentation on all public methods
- ✅ Interface-based dependencies
- ✅ Null-safe operations
- ✅ Proper async/await patterns
- ✅ Memory leak prevention (UniRx AddTo)
- ✅ SOLID principles

## 🎯 Best Practices Implemented
- Dependency Injection for testability
- Reactive state management
- Timeout handling for network calls
- Proper resource disposal
- Separation of concerns
- Interface segregation

## 👤 Author
Ioannis Poriazis
