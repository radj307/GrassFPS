using Mutagen.Bethesda.WPF.Reflection.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrassFPS.Synth
{
    #region FlagOperationType
    /// <summary>
    /// Defines possible operations to apply to any 2 arbitrary enum values.
    /// </summary>
    public enum FlagOperationType : byte
    {
        /// <summary>
        /// Enables the associated <see cref="FlagOperation{T}.Flag"/> flag.
        /// </summary>
        Enable = 1,
        /// <summary>
        /// Disables the associated <see cref="FlagOperation{T}.Flag"/> flag.
        /// </summary>
        Disable = 2,
        /// <summary>
        /// Deletes the previous flag flag, and overwrites it with the associated <see cref="FlagOperation{T}.Flag"/> flag.
        /// </summary>
        Overwrite = 3,
        /// <summary>
        /// Bitwise binary OR operator: <b>|</b>
        /// </summary>
        BitwiseOR = 4,
        /// <summary>
        /// Bitwise binary AND operator: <b>&amp;</b>
        /// </summary>
        BitwiseAND = 5,
        /// <summary>
        /// Bitwise binary XOR operator: <b>^</b>
        /// </summary>
        BitwiseXOR = 6,
        /// <summary>
        /// Bitwise unary NOT operator: <b>~</b>
        /// </summary>
        /// <remarks>
        /// Unlike the other enumerations of its type, <see cref="BitwiseNOT"/> is a <b>unary operator</b>, so it only accepts 1 input flag.
        /// </remarks>
        BitwiseNOT = 7,
    }
    #endregion FlagOperationType

    #region FlagSetting
    /// <summary>
    /// Provides a user-editable enum flag ViewModel.
    /// </summary>
    /// <typeparam name="T"><see cref="Enum"/> type.</typeparam>
    public class FlagSetting<T> : ICalculateValueFrom<T> where T : Enum
    {
        #region Constructors
        public FlagSetting() => FlagChanges = new();
        public FlagSetting(IEnumerable<Operation>? flagChanges) => FlagChanges = flagChanges?.ToList() ?? new();
        #endregion Constructors

        #region Operation
        /// <summary>
        /// Contains a single flag operation definition to be consumed by the ViewModel.
        /// </summary>
        /// <remarks>
        /// This is used by <see cref="FlagSetting{T}"/>.
        /// </remarks>
        public class Operation : ICalculateValueFrom<T>
        {
            #region Constructors
            public Operation(T flag) => Flag = flag;
            public Operation(FlagOperationType @operator, T flag) : this(flag) => Operator = @operator;
            public Operation() : this(FlagOperationType.Enable, default!) { }
            #endregion Constructors

            #region Properties
            [Tooltip($"Selects the flag to modify. This does nothing when the operator is set to '{nameof(FlagOperationType.BitwiseNOT)}'.")]
            public T Flag;
            /// <summary>
            /// Determines the operator to use in the expression.
            /// </summary>
            /// <remarks>
            /// <b>Default: <see cref="FlagOperationType.Enable"/></b>
            /// </remarks>
            [Tooltip($"Simple operators like '{nameof(FlagOperationType.Enable)}'/'{nameof(FlagOperationType.Disable)}'/'{nameof(FlagOperationType.Overwrite)}' are self-explanatory.\nFor Bitwise operators, the existing flag value is always on the left side of the operation while the 'Flag' value is always on the right side.\nThe BitwiseNOT operator never uses the 'Flag' value since it is a unary operator.")]
            public FlagOperationType Operator = FlagOperationType.Enable;
            internal int IntValue
            {
                get => Flag.ToInt();
                set => Flag = value.ToEnum<T>();
            }
            #endregion Properties

            #region GetOperationResult
            /// <summary>
            /// Calculates the result of the expression, given <paramref name="operatorType"/>, <paramref name="left"/>, &amp; <paramref name="right"/>.
            /// </summary>
            /// <param name="operatorType">The <see cref="FlagOperationType"/> to perform.</param>
            /// <param name="left">The left-side input for binary and unary operators.</param>
            /// <param name="right">The right-side input for binary operators.</param>
            /// <returns>The result of the operation.</returns>
            /// <exception cref="InvalidOperationException"><paramref name="operatorType"/> was set to an unexpected value. Expected values are any ONE of the <see cref="FlagOperationType"/> values.</exception>
            public static T GetOperationResult(FlagOperationType? operatorType, T left, T right) => operatorType switch
            {
                FlagOperationType.Disable => left.BitwiseAND(right.BitwiseNOT()),
                FlagOperationType.Enable or FlagOperationType.BitwiseOR => left.BitwiseOR(right),
                FlagOperationType.BitwiseAND => left.BitwiseAND(right),
                FlagOperationType.BitwiseXOR => left.BitwiseXOR(right),
                FlagOperationType.BitwiseNOT => left.BitwiseNOT(),
                FlagOperationType.Overwrite => right,
                0 or null => left,
                _ => throw new InvalidOperationException($"'{operatorType:G}' is an invalid value for enum type '{nameof(FlagOperationType)}'!")
            };
            #endregion GetOperationResult

            #region Methods
            public T CalculateValueFrom(T inputValue, out bool changed)
            {
                T val = GetOperationResult(Operator, inputValue, Flag);
                changed = !val.Equals(inputValue);
                return val;
            }
            #endregion Methods
        }
        #endregion Operation

        #region Fields
        [Tooltip("A list of changes to make to the current flag value.\n\n" +
            "Enable      Enables the specified Flag.\n" +
            "Disable     Disables the specified flag.\n" +
            "Overwrite   Overwrites the current flags with the specified flag, removing all other flags in the process.\n" +
            "BitwiseOR   Bitwise binary OR  operator `{{EXISTING_VALUE}} | {{Flag}}`\n" +
            "BitwiseAND  Bitwise binary AND operator `{{EXISTING_VALUE}} & {{Flag}}`\n" +
            "BitwiseXOR  Bitwise binary XOR operator `{{EXISTING_VALUE}} ^ {{Flag}}`\n" +
            "BitwiseNOT  Bitwise unary  NOT operator `~{{EXISTING_VALUE}}`")]
        public List<Operation> FlagChanges;
        #endregion Fields

        #region Methods
        public T ApplyFlagChangesTo(T input)
        {
            if (FlagChanges.Count.Equals(0))
                return input;

            T? val = input;

            FlagChanges.ForEach(a => val = a.CalculateValueFrom(val));

            return val;
        }
        public T CalculateValueFrom(T inputValue, out bool changed)
        {
            T? val = this.ApplyFlagChangesTo(inputValue);
            changed = !val.Equals(inputValue);
            return val;
        }
        #endregion Methods
    }
    #endregion FlagSetting
}
