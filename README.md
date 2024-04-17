# LABGAT-Training-W3

3D "Overcooked"-like game project as a training for LABGAT - Game Programmer

The sources in this project are taken from [this playlist](https://youtu.be/AmGSEH7QcDg?si=JZjqXpeLNPSPCPGH)

## Key Features
- A fast paced casual game where you take orders, slice ingredients, and cook patties for the customer

## Unity Stuff
- Usage of the new input system and the option to rebind key
- Basic usage of post processing and other polishing elements
- Usage of Scriptable objects and prefab variants to make various game content & a scalable project architecture
- The use of basic shader graph for a moving graphic
- UIs elements such as screen space & world space, grid layout, and many more

## Coding Practices
- C# events. The use of events can make decoupling easier for scripts can communicate without directly referencing each other. Events also allow a more scalable architecture where we can just add or remove subscriber without the need to modify the publisher.
- Singleton, a commonly used pattern allowing easy access for a shared functionality of a class
- Interfaces. Interface gives an abstraction making the code more decoupled.
- Simple State Machine, used for managing various state of an object or a game state

---
itch.io: https://smtt.itch.io/labgat-week-3 (password: gat3)
