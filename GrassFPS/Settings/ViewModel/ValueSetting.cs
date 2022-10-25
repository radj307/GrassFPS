using GrassFPS.Extensions;
using GrassFPS.Settings.Interfaces;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace GrassFPS.Settings.ViewModel
{
    /// <summary>
    /// Wrapper object for a single value of type <typeparamref name="T"/> that also exposes a boolean that allows the user to disable the application of the value.
    /// </summary>
    /// <typeparam name="T">The value type to wrap.</typeparam>
    public abstract class ValueSetting<T> : IGetValueOrAlternative<T>
    {
        public ValueSetting(bool enableProperty, T value)
        {
            EnableProperty = enableProperty;
            Value = value;
        }

        [Tooltip("This MUST be checked to apply the Value to records. When unchecked, the associated Value property is skipped, and no changes are made to the original value.")]
        public bool EnableProperty = false;
        public T Value;

        /// <summary>
        /// Gets the value of <see cref="Value"/> if it is <b>not</b> <see langword="null"/>.
        /// </summary>
        /// <param name="defaultValue">Some other value</param>
        /// <returns><see cref="Value"/> when <see cref="EnableProperty"/> is <see langword="true"/>; otherwise <paramref name="defaultValue"/>.</returns>
        public virtual T GetValueOrAlternative(T defaultValue, out bool changed)
        {
            T val = EnableProperty ? Value : defaultValue;
            changed = !defaultValue!.Equals(val);
            return val;
        }
    }
    public class ClassValueSetting<T> : ValueSetting<T> where T : class, new()
    {
        public ClassValueSetting() : base(false, new()) { }
        public ClassValueSetting(T? value) : base(value is not null, value ?? new()) { }
    }
    public class StructValueSetting<T> : ValueSetting<T> where T : struct
    {
        public StructValueSetting() : base(false, default) { }
        public StructValueSetting(T? value) : base(value is not null, value ?? default) { }
    }
    public class EnumValueSetting<T> : ValueSetting<T> where T : Enum
    {
        public EnumValueSetting() : base(false, 0.ToEnum<T>()) { }
        public EnumValueSetting(T? value) : base(value is not null, value ?? 0.ToEnum<T>()) { }
    }
}
