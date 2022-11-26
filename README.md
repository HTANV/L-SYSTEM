# L‑SYSTEM

![GitHub stars](https://img.shields.io/github/stars/HTANV/L-SYSTEM?style=for-the-badge)
![GitHub forks](https://img.shields.io/github/forks/HTANV/L-SYSTEM?style=for-the-badge)
![GitHub last commit](https://img.shields.io/github/last-commit/HTANV/L-SYSTEM?style=for-the-badge)
![GitHub issues](https://img.shields.io/github/issues/HTANV/L-SYSTEM?style=for-the-badge)
![C#](https://img.shields.io/badge/Language-C%23-blue?style=for-the-badge&logo=csharp)
![Unity](https://img.shields.io/badge/Unity‑Project-black?style=for-the-badge&logo=unity)

---

## Overview

**L‑SYSTEM** is a Unity project that implements a **Lindenmayer System (L‑System)** — a procedural grammar used for generating natural forms such as fractal structures, plants, and recursive patterns.

L‑Systems are a powerful method for procedural content generation. They use a formal grammar of symbols, an initial axiom, and production rules to evolve patterns over iterative steps. Though traditionally applied in botany and fractals, they can be extended to many algorithmic generation systems in games and simulations. :contentReference[oaicite:1]{index=1}

This repository contains a Unity solution that demonstrates L‑System generation logic with C# workflows.

---

## What This Project Does

- Implements **Lindenmayer System logic** within a Unity C# project  
- Enables generation of recursive patterns using symbolic rules  
- Provides a framework to experiment with:
  - Axiom definitions
  - Production rules
  - Iteration counts  
- Useful for:
  - Procedural plants and trees
  - Fractal architectures
  - Algorithmic content generation in games
- Entire project developed in C# with Unity project files included :contentReference[oaicite:2]{index=2}

---

## Features

- 🔹 **Rule‑based procedural generation**
- 🔹 **Modular C# implementation**
- 🔹 Useful for **games, simulations, art tools**
- 🔹 Works inside a Unity project structure

---

## Repository Structure

```

L-SYSTEM/
├── Assets/
│   ├── Patterns/             # L‑System pattern definitions
│   ├── Scenes/               # Example scenes demonstrating generation
│   ├── Scripts/              # C# logic for L‑System generator
├── LSystem.sln               # Visual Studio solution
├── ProjectSettings/          # Unity project configuration
├── Packages/                 # Unity package manifest
└── README.md

````

> This layout assumes typical Unity project structure — adjust names if your assets are in subfolders.

---

## How It Works (Technical Summary)

An L‑System consists of:

1. **Axiom** – An initial string defining the start  
2. **Alphabet** – Collection of symbols used  
3. **Production Rules** – Mapping each symbol to transformation(s)  
4. **Iterations** – Number of times rules are applied to evolve the string

At each iteration, the system rewrites the string using production rules — creating increasingly complex structures. :contentReference[oaicite:3]{index=3}

In Unity, this logic can be used to spawn:
- Meshes or objects based on symbols  
- Branches, leaves, or repeated patterns  
- Artistic fractals and natural scenes

---

## Getting Started

> ⚠️ Make sure you have a compatible version of Unity installed (2020 LTS or later recommended).

1. **Clone the repository**
   ```bash
   git clone https://github.com/HTANV/L-SYSTEM.git
   ```

2. **Open in Unity Hub**

   * Launch Unity Hub
   * Add project
   * Select appropriate Unity Editor version

3. **Open Sample Scene**

   * Navigate to `Assets/Scenes`
   * Open a sample scene demonstrating the generator

4. **Run in Play Mode**

   * Press the Play button
   * Experiment with pattern rules and iterations

---

## Requirements

* **Unity Editor** – 2020 LTS or newer (recommended)
* **C#** scripting support
* Basic understanding of procedural generation and grammar systems

---

## Use Cases

* Procedural plant/tree generation
* Fractal art and generative scenes
* AI & simulation pipelines
* Game level procedural elements

---

## Notes

* No releases are published — this is a code‑first educational project. ([GitHub][1])
* Explore and modify the pattern rules to create custom procedural outputs.

---

## Contributing

Contributions are welcome! Suggestions:

* Add visual generators or rendering support
* Integrate GUI inspector tools to edit rules at runtime
* Add more example scenes
* Document pattern templates

Just open a pull request or issue.

---

## License

Check for an existing `LICENSE` file in the repo. If none exists, consider adding MIT or Apache‑2.0 for clarity.

---

## Related Resources

* **Lindenmayer systems (L‑Systems)** on GitHub topics — procedural graph grammars. ([GitHub][2])
