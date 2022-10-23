using GrassFPS.Extensions;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace GrassFPS.ViewModel
{
    public enum EnumFlagOperationType : byte
    {
        Enable,
        Disable,
        Overwrite,
        /// <summary>binary operand <b>|</b></summary>
        BitwiseOR,
        /// <summary>binary operand <b>&amp;</b></summary>
        BitwiseAND,
        /// <summary>binary operand <b>^</b></summary>
        BitwiseXOR,

        BitwiseNOT,
    }
    /// <summary>
    /// Contains a single flag operation definition to be consumed by the ViewModel.
    /// </summary>
    /// <remarks>
    /// This is used by <see cref="EnumFlagSetting{T}"/>.
    /// </remarks>
    public class EnumFlagOperation<T> : IGetValueOrAlternative<T> where T : Enum
    {
        #region Constructors
        public EnumFlagOperation(T value) => Flag = value;
        public EnumFlagOperation(EnumFlagOperationType action, T value) : this(value) => Operator = action;
        public EnumFlagOperation() : this(EnumFlagOperationType.Enable, default!) { }
        #endregion Constructors

        #region Properties
        [Tooltip($"Selects the flag to modify. This does nothing when the operator is set to '{nameof(EnumFlagOperationType.BitwiseNOT)}'.")]
        public T Flag;
        /// <summary>
        /// Determines the action that will be performed on the enum value.
        /// </summary>
        [Tooltip($"Simple operators like '{nameof(EnumFlagOperationType.Enable)}'/'{nameof(EnumFlagOperationType.Disable)}'/'{nameof(EnumFlagOperationType.Overwrite)}' are self-explanatory.\nFor Bitwise operators, the existing flag value is always on the left side of the operation while the 'Flag' value is always on the right side.\nThe BitwiseNOT operator never uses the 'Flag' value since it is a unary operator.")]
        public EnumFlagOperationType Operator = EnumFlagOperationType.Enable;

        internal int IntValue
        {
            get => Flag.ToInt();
            set => Flag = value.ToEnum<T>();
        }
        #endregion Properties

        public T GetValueOrAlternative(T inputValue, out bool changed)
        {
            var val = Operator switch
            {
                EnumFlagOperationType.Disable => inputValue.BitwiseAND(Flag.BitwiseNOT()),
                EnumFlagOperationType.Enable or EnumFlagOperationType.BitwiseOR => inputValue.BitwiseOR(Flag),
                EnumFlagOperationType.BitwiseAND => inputValue.BitwiseAND(Flag),
                EnumFlagOperationType.BitwiseXOR => inputValue.BitwiseXOR(Flag),
                _ => Flag,
            };
            changed = !val.Equals(inputValue);
            return val;
        }
    }
    /// <summary>
    /// Provides a user-editable enum flag ViewModel.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type.</typeparam>
    public class EnumFlagSetting<T> : IGetValueOrAlternative<T> where T : Enum
    {
        [Tooltip("This MUST be checked to apply the Flag Changes to records. When unchecked, the associated Flag Changes property is skipped, and no changes are made to the original value.")]
        public bool EnableProperty = false;
        [Tooltip("Flag changes are applied in sequential order. Actions that start with \"Bitwise\" are for advanced users.")]
        public List<EnumFlagOperation<T>> FlagChanges = new();

        public T GetValueOrAlternative(T inputValue, out bool changed)
        {
            var val = inputValue;

            foreach (var action in FlagChanges)
            {
                val = action.GetValueOrAlternative(val, out _);
            }

            changed = !val.Equals(inputValue);
            return val;
        }
    }
}
