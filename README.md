# Unit Converter

A simple, clean unit conversion web app built with ASP.NET Core Razor Pages. Convert between common units of **Length**, **Weight**, and **Temperature** through an easy tabbed interface.

## Features

- Convert between multiple units in three categories:
  - **Length**: Meters, Feet, Inches, Kilometers, Miles
  - **Weight**: Kilograms, Grams, Pounds, Ounces
  - **Temperature**: Celsius, Fahrenheit, Kelvin
- Tab-based category switcher — no page reload framework needed, just simple query-string navigation
- Server-side validation with friendly error messages
- Styled with Tailwind CSS utility classes
- Precise decimal-based math (rounded to 3 decimal places)

## Tech Stack

- **ASP.NET Core Razor Pages**
- **C#**
- **Tailwind CSS** for some styling
- **Vanilla JavaScript** for tab interactivity

## How It Works

1. Choose a category (Length, Weight, or Temperature) using the tab buttons at the top.
2. Enter a value and select the units to convert **from** and **to**.
3. Click **Convert** to see the result.
4. Click **Reset** to start a new conversion.

Under the hood, each category has its own enum (`LengthUnit`, `WeightUnit`, `TemperatureUnit`). Length and weight conversions go through a common base unit (meters and kilograms respectively) using lookup dictionaries, while temperature conversions route through Celsius as an intermediate step.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (6.0 or later recommended)

### Running the App

```bash
# Clone the repository
git clone <your-repo-url>
cd UnitConverter

# Restore dependencies
dotnet restore

# Run the app
dotnet run
```

Then open your browser to the URL shown in the terminal.

## Roadmap / Possible Improvements

- [ ] Add validation messaging for mismatched category/unit combinations
- [ ] Add unit tests for conversion logic (xUnit)
- [ ] Support additional categories (e.g., Volume, Speed, Data Storage)
- [ ] Add a "swap units" button
- [ ] Persist last-used category/units between sessions

Project Idea By : https://roadmap.sh/projects/unit-converter
