# ProceduralTerrainGeneration

### About
This project is being created as a hobby project with the goal to create huge worlds without unloading anything in view of the player and instead get rid of detail of the terrain as you get further away. After this is created it may be incorporated into a game with an undecided theme and style.

### How to use
- Add the scripts to your unity assets
- Create a new empty object in your scene with any name
- Drag the script into the new object you have created
- Change the settings of the script to your liking in the inspector

### Settings
- X Size: The number of squares created in the X axis
- Z Size: The number of squares created in the Z axis

# Version Changelogs

### 0.1.0.1
- Added ability to create terrain of any size with sudo-random heights for each vertex

### 0.2.0.2
- Added ability to create an array of vertices more detailed than before
- Unable to create mesh with new detailed vertex creation

### 0.2.1.3
- Fixed issue where, in NewTerrainGeneration, no mesh would be generated
