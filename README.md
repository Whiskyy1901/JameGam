# Little Guy Clicker

A small clicker game I made in Unity for the jame gam game jam (Hack Club). The theme was "I'm just a little guy."

You start as a tiny red square. Click to gain mass, buy upgrades, and keep growing.
## How to play

- Click the big button to gain mass.
- Buy upgrades when you can afford them.
- Reach the last upgrade to see the ending.

## Built with

- Unity (2D)
- TextMeshPro for UI
- Sounds from Kenney's Interface Sounds pack (CC0)

No sprites in this one, just shapes and colors, since I only had a few hours.

{Art files were imported but never used}

## Scripts

- `ClickManager` - mass, clicks, upgrades
- `GrowthStages` - changes the size, colors, and camera zoom for each stage
- `EndingSequence` - the black hole ending and restart
- `UIFeedback` - button pop and greying out the upgrade button
- `SoundManager` - plays the sound effects

## Running it

Download latest build and extract and run the exe file.
## Known issues

- Balance is rough. A full run takes about 5 minutes.
- No save system.
