using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace GrassFPS.Synth
{
    /// <summary>
    /// Wrapper object for a single value of type <typeparamref name="T"/> that also exposes a boolean that allows the user to disable the application of the value.
    /// </summary>
    /// <remarks>
    /// See <see cref="ClassSetting{T}"/>, <see cref="StructSetting{T}"/>, &amp; <see cref="EnumSetting{T}"/> for ready-to-use classes that implement <see cref="ValueSetting{T}"/>.
    /// </remarks>
    /// <typeparam name="T">The value type to wrap.</typeparam>
    public abstract class ValueSetting<T> : IGetValueOrAlternative<T>
    {
        #region Constructor
        /// <summary>
        /// Creates a new <see cref="ValueSetting{T}"/> instance.
        /// </summary>
        /// <param name="enableProperty"><see cref="EnableProperty"/></param>
        /// <param name="value"><see cref="Value"/></param>
        public ValueSetting(bool enableProperty, T value)
        {
            EnableProperty = enableProperty;
            Value = value;
        }
        #endregion Constructor

        #region Operators
        public static implicit operator T(ValueSetting<T> valueSetting) => valueSetting.Value;
        #endregion Operators

        #region Fields
        /// <summary>
        /// When <see langword="true"/>, the setting is enabled; otherwise it is disabled. This setting's <see cref="Value"/> field is only applied to records when this is enabled.
        /// </summary>
        [Tooltip("This MUST be checked to apply the Value to records. When unchecked, the associated Value property is skipped, and no changes are made to the original value.")]
        public bool EnableProperty = false;
        /// <summary>
        /// The current value of this setting.
        /// </summary>
        public T Value;
        #endregion Fields

        #region Methods
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
        #endregion Methods
    }
}
