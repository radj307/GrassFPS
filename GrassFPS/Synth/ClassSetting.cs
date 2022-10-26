namespace GrassFPS.Synth
{
    /// <summary>
    /// Provides a toggleable value for a <see langword="class"/> type.
    /// </summary>
    /// <typeparam name="T">Any class type that is default-constructible.</typeparam>
    public class ClassSetting<T> : ValueSetting<T> where T : class, new()
    {
        #region Constructors
        /// <summary>
        /// Creates a new disabled <see cref="ClassSetting{T}"/> instance with the default value.
        /// </summary>
        public ClassSetting() : base(false, new()) { }
        /// <summary>
        /// Creates a new enabled <see cref="ClassSetting{T}"/> instance with the given <paramref name="value"/> if it isn't <see langword="null"/>; otherwise creates a new disabled instance with the default value.
        /// </summary>
        /// <param name="value">A starting value of type <typeparamref name="T"/>.<br/>When this is <see langword="null"/>, the base <see cref="ValueSetting{T}"/> starts disabled.</param>
        public ClassSetting(T? value) : base(value is not null, value ?? new()) { }
        #endregion Constructors

        #region Operators
        public static implicit operator ClassSetting<T>(T? value) => new(value);
        #endregion Operators
    }
}
