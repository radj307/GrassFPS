namespace GrassFPS.Synth
{
    /// <summary>
    /// Provides a toggleable value for a <see langword="struct"/> type.
    /// </summary>
    /// <remarks>
    /// <b>Use this for fundamental types like <see cref="int"/>, <see cref="float"/>, etc.</b>
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class StructSetting<T> : ValueSetting<T> where T : struct
    {
        #region Constructors
        /// <summary>
        /// Creates a new disabled <see cref="StructSetting{T}"/> instance with the default value.
        /// </summary>
        public StructSetting() : base(false, default) { }
        /// <summary>
        /// Creates a new enabled <see cref="StructSetting{T}"/> instance with the given <paramref name="value"/> if it isn't <see langword="null"/>; otherwise creates a new disabled instance with the default value.
        /// </summary>
        /// <param name="value">A starting value of type <typeparamref name="T"/>.<br/>When this is <see langword="null"/>, the base <see cref="ValueSetting{T}"/> starts disabled.</param>
        public StructSetting(T? value) : base(value is not null, value ?? default) { }
        #endregion Constructors

        #region Operators
        public static implicit operator StructSetting<T>(T? value) => new(value);
        #endregion Operators
    }
}
