# Marqi

Marqi is a configurable dashboard intended to run on a Raspberry Pi attached to a RGB LED matrix display. It supports web based simulation of a display for development purposes.

![Photo of Marqi running on a 32x64 RGB LED Matrix](/docs/img/photo.png)

## Features/Integrations

* Google Calendar
* Todoist
* OpenWeather
* Countdown timers
* BDF Font Drawing
* Image Drawing

## Configuration

Marqi uses the [default ASP.NET Core Configuration](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/) sources (environment variables, user secrets for development, `appsettings.json`, etc.)

| Configuration Key         | Type   | Description                                            | Default?     |
|---------------------------|--------|--------------------------------------------------------|--------------|
| EnableWebRGB              | bool   |  Enable/disable the web based RGB LED simulation       | `false`      |
| EnableRGB                 | bool   |  Enable/disable support for a physical RGB LED display | `false`      |
| Display:Columns           | int    |  Number of columns on the display                      | **Required** |
| Display:Rows              | int    |  Number of rows on the display                         | **Required** |
| Display:PixelSize         | int    |  Size of each pixel (when WebRGB is enabled)           | none         |
| OpenWeather:ApiKey        | string |  Api key for OpenWeather                               | none         |
| OpenWeather:Zip           | string |  Zip code to retrieve weather data for                 | none         |
| Todo:Token                | string |  Access token to retrieve Todoist tasks                | none         |
| Gcal:Calendars[]:Url      | string |  Direct `.ics` address of a calendar to retrieve       | none         |   
| Gcal:Calendars[]:HexColor | string |  Color code in hex to display this calendars evnets    | `"FF0000"`   | 

## Persistence

Marqi creates a SQLite db named `marqi.db` in the current directory to maintain certain state between restarts. Currently, timer state is persisted ot the db.

## RGB LED Matrix support

To date, Marqi has only been tested on a single 32x64 display.

See https://github.com/hzeller/rpi-rgb-led-matrix for details.