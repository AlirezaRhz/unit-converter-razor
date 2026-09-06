using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UnitConverter.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Dictionary<LengthUnit, decimal> LengthToMeters = new Dictionary<LengthUnit, decimal>()
        {
            [LengthUnit.Meters] = 1m,
            [LengthUnit.Feet] = 0.3048m,
            [LengthUnit.Inches] = 0.0254m,
            [LengthUnit.Kilometers] = 1000m,
            [LengthUnit.Miles] = 1609.344m
        };
        private readonly Dictionary<WeightUnit, decimal> WeightToKg = new Dictionary<WeightUnit, decimal>()
        {
            [WeightUnit.Kilograms] = 1m,
            [WeightUnit.Grams] = 0.001m,
            [WeightUnit.Pounds] = 0.45359237m,
            [WeightUnit.Ounces] = 0.028349523125m
        };
        public enum UnitCategory
        {
            Length,
            Weight,
            Temperature
        }
        public enum LengthUnit { Meters, Feet, Inches, Kilometers, Miles }
        public enum WeightUnit { Kilograms, Pounds, Ounces, Grams }
        public enum TemperatureUnit { Celsius, Fahrenheit, Kelvin }

        [BindProperty]
        [Required(ErrorMessage = "Please Enter A Value To Convert")]
        public decimal? InputValue { get; set; }

        [BindProperty(SupportsGet = true)]
        public UnitCategory Category { get; set; }

        public SelectList? FromUnitOptions { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please Enter A Unit To Convert From")]
        public string? FromUnit { get; set; }

        public SelectList? ToUnitOptions { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please Enter A Unit To Convert To")]
        public string? ToUnit { get; set; }

        public decimal? Result { get; set; }

        public void OnGet()
        {
            FromUnitOptions = BuildUnitOptions(Category);
            ToUnitOptions = BuildUnitOptions(Category);
        }

        public IActionResult OnPost()
        {
            FromUnitOptions = BuildUnitOptions(Category);
            ToUnitOptions = BuildUnitOptions(Category);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            CalculateConversion();

            return Page();
        }

        public SelectList BuildUnitOptions(UnitCategory category)
        {
            List<string> units = new List<string>();
            if (category == UnitCategory.Length)
            {
                foreach (var unit in Enum.GetNames(typeof(LengthUnit)))
                {
                    units.Add(unit);
                }
            }
            if (category == UnitCategory.Weight)
            {
                foreach (var unit in Enum.GetNames(typeof(WeightUnit)))
                {
                    units.Add(unit);
                }
            }
            if (category == UnitCategory.Temperature)
            {
                foreach (var unit in Enum.GetNames(typeof(TemperatureUnit)))
                {
                    units.Add(unit);
                }
            }
            SelectList selectListItems = new SelectList(units);
            return selectListItems;
        }

        public void CalculateConversion()
        {
            if (Category == UnitCategory.Length && Enum.TryParse<LengthUnit>(FromUnit, out var fromLengthUnit) && Enum.TryParse<LengthUnit>(ToUnit, out var toLengthUnit))
            {
                Result = ConvertLength(InputValue!.Value, fromLengthUnit, toLengthUnit);
            }
            else if (Category == UnitCategory.Weight && Enum.TryParse<WeightUnit>(FromUnit, out var fromWeightUnit) && Enum.TryParse<WeightUnit>(ToUnit, out var toWeightUnit))
            {
                Result = ConvertWeight(InputValue!.Value, fromWeightUnit, toWeightUnit);

            }
            else if (Category == UnitCategory.Temperature && Enum.TryParse<TemperatureUnit>(FromUnit, out var fromTempUnit) && Enum.TryParse<TemperatureUnit>(ToUnit, out var toTempUnit))
            {
                Result = ConvertTemperature(InputValue!.Value, fromTempUnit, toTempUnit);
            }
        }

        public decimal ConvertLength(decimal inputValue, LengthUnit fromUnit, LengthUnit convertingToUnit)
        {
            decimal toMeters = inputValue * LengthToMeters[fromUnit];
            decimal finalResult = toMeters / LengthToMeters[convertingToUnit];
            return Math.Round(finalResult, 3);
        }

        public decimal ConvertWeight(decimal inputValue, WeightUnit fromWeightUnit, WeightUnit toWeightUnit)
        {
            decimal toKg = inputValue * WeightToKg[fromWeightUnit];
            decimal finalResult = toKg / WeightToKg[toWeightUnit];
            return Math.Round(finalResult, 3);
        }

        public decimal ConvertTemperature(decimal inputValue, TemperatureUnit fromTempUnit, TemperatureUnit toTempUnit)
        {
            decimal inputToCelsius = inputValue;

            if (fromTempUnit == TemperatureUnit.Fahrenheit)
            {
                inputToCelsius = (inputToCelsius - 32) * (5m / 9m);
            }
            else if (fromTempUnit == TemperatureUnit.Kelvin)
            {
                inputToCelsius = inputToCelsius - 273.15m;
            }

            decimal result = inputToCelsius;
            // Now Convert from celcius to whatever the user wanted :
            if (toTempUnit == TemperatureUnit.Fahrenheit)
            {
                result = (inputToCelsius * 1.8m) + 32;
            }
            else if (toTempUnit == TemperatureUnit.Kelvin)
            {
                result = inputToCelsius + 273.15m;
            }

            return Math.Round(result, 3);
        }

    }
}
