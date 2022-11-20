# YetAnotherSamplePlugin

YASP is an over-engineered Dalamud sample plugin. If you are new to plugin development, you probably want to start [here](https://github.com/goatcorp/SamplePlugin) instead. This is my attempt to abstract away boilerplate code and make writing unit tests easier.

## Features
- Uses [FlexConfig](https://github.com/kalilistic/FlexConfig) for plugin configuration.
- Uses [Dalamud.Loc](https://github.com/kalilistic/Dalamud.Loc) for Localization to support multiple languages.
- Uses [Dalamud.DrunkenToad](https://github.com/kalilistic/Dalamud.Loc) to avoid boilerplate code.
- Uses [DalamudPackager](https://github.com/goatcorp/DalamudPackager) to package the plugin for distribution.
- Uses multiple assemblies to allow for separation of concerns and easier testing.
- Contains unit tests for the services assembly with [xUnit](https://github.com/xunit/xunit).
- Retrieves game data from [Lumina](https://github.com/NotAdam/Lumina).
- Retrieves client state data from [Dalamud's ClientState API](https://goatcorp.github.io/Dalamud/api/Dalamud.Game.ClientState.html).
- Retrieves data from game addons using [Dalamud's GameGUI API](https://goatcorp.github.io/Dalamud/api/Dalamud.Game.Gui.html).
