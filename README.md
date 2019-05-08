# Toy Robot Simulator
A C# solution for the `Toy Robot Simulator` problem, described [here](https://github.com/nandowalter-lm/toy_robot).

## Content
This repository contains a solution with three projects:
- `ToyRobotSimulatorCore`, a .NET Standard 1.0 class library, implementing the core features;
- `ToyRobotSimulatorCore.Tests`, a .NET Core 2.2 application, containing a xUnit-based test suite for ToyRobotSimulatorCore, with 50+ test cases;
- `ToyRobotSimulatorRunner`, a .NET Core 2.2 application, implementing a console-based interactive client for ToyRobotSimulatorCore.

The configuration files needed to build, run and test the solution using Visual Studio Code are provided in the `.vscode` folders.

## Other information
The solution was developed and tested with:
- [Visual Studio Code](https://code.visualstudio.com/) 1.33.1 with the [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp) 1.19.1;
- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core/2.2) 2.2.203;
- [xUnit.net](https://xunit.net/) 2.4.1 and the [Microsoft .NET Test Sdk](https://www.nuget.org/packages/Microsoft.NET.Test.SDK) 16.0.1;
- C# 7.3;
- Windows 10 1809.