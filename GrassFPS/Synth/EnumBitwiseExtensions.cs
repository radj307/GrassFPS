using System;

namespace GrassFPS.Synth
{
    /// <summary>
    /// Extends <see cref="Enum"/>-based types with support for bitwise operations.<br/>
    /// These methods should <b>only be used on enums with <see cref="FlagsAttribute"/></b>.
    /// </summary>
    public static class EnumBitwiseExtensions
    {
        #region TypeCasting
        #region ToInt
        /// <summary>
        /// Converts an <see cref="Enum"/> value to <see cref="sbyte"/>.
        /// </summary> 
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="e">Enum value</param>
        /// <returns>The integral representation of <paramref name="e"/></returns>
        public static sbyte ToSByte<T>(this T e) where T : struct, Enum => Convert.ToSByte(Convert.ChangeType(e, e.GetTypeCode()));
        /// <summary>
        /// Converts an <see cref="Enum"/> value to <see cref="byte"/>.
        /// </summary> 
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="e">Enum value</param>
        /// <returns>The integral representation of <paramref name="e"/></returns>
        public static byte ToByte<T>(this T e) where T : struct, Enum => Convert.ToByte(Convert.ChangeType(e, e.GetTypeCode()));
        /// <summary>
        /// Converts an <see cref="Enum"/> value to <see cref="short"/>.
        /// </summary> 
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="e">Enum value</param>
        /// <returns>The integral representation of <paramref name="e"/></returns>
        public static short ToInt16<T>(this T e) where T : struct, Enum => Convert.ToInt16(Convert.ChangeType(e, e.GetTypeCode()));
        /// <summary>
        /// Converts an <see cref="Enum"/> value to <see cref="ushort"/>.
        /// </summary> 
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="e">Enum value</param>
        /// <returns>The integral representation of <paramref name="e"/></returns>
        public static ushort ToUInt16<T>(this T e) where T : struct, Enum => Convert.ToUInt16(Convert.ChangeType(e, e.GetTypeCode()));
        /// <summary>
        /// Converts an <see cref="Enum"/> value to <see cref="int"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="e">Enum value</param>
        /// <returns>The integral representation of <paramref name="e"/></returns>
        public static int ToInt32<T>(this T e) where T : struct, Enum => Convert.ToInt32(Convert.ChangeType(e, e.GetTypeCode()));
        /// <summary>
        /// Converts an <see cref="Enum"/> value to <see cref="uint"/>.
        /// </summary> 
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="e">Enum value</param>
        /// <returns>The integral representation of <paramref name="e"/></returns>
        public static uint ToUInt32<T>(this T e) where T : struct, Enum => Convert.ToUInt32(Convert.ChangeType(e, e.GetTypeCode()));
        /// <summary>
        /// Converts an <see cref="Enum"/> value to <see cref="long"/>.
        /// </summary> 
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="e">Enum value</param>
        /// <returns>The integral representation of <paramref name="e"/></returns>
        public static long ToInt64<T>(this T e) where T : struct, Enum => Convert.ToInt64(Convert.ChangeType(e, e.GetTypeCode()));
        /// <summary>
        /// Converts an <see cref="Enum"/> value to <see cref="ulong"/>.
        /// </summary> 
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="e">Enum value</param>
        /// <returns>The integral representation of <paramref name="e"/></returns>
        public static ulong ToUInt64<T>(this T e) where T : struct, Enum => Convert.ToUInt64(Convert.ChangeType(e, e.GetTypeCode()));
        #endregion ToInt

        #region ToEnum
        /// <summary>
        /// Converts from <see cref="byte"/> to the specified <see cref="Enum"/>-based type.
        /// </summary>
        /// <remarks>
        /// This method should <b>only be used when you know the resulting value will be valid!</b>
        /// </remarks>
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="i">Integral value</param>
        /// <returns>The enum representation of <paramref name="i"/></returns>
        public static T ToEnum<T>(this byte i) where T : struct, Enum => (T)Convert.ChangeType(i, Activator.CreateInstance<T>().GetTypeCode());
        /// <summary>
        /// Converts from <see cref="sbyte"/> to the specified <see cref="Enum"/>-based type.
        /// </summary>
        /// <remarks>
        /// This method should <b>only be used when you know the resulting value will be valid!</b>
        /// </remarks>
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="i">Integral value</param>
        /// <returns>The enum representation of <paramref name="i"/></returns>
        public static T ToEnum<T>(this sbyte i) where T : struct, Enum => (T)Convert.ChangeType(i, Activator.CreateInstance<T>().GetTypeCode());
        /// <summary>
        /// Converts from <see cref="short"/> to the specified <see cref="Enum"/>-based type.
        /// </summary>
        /// <remarks>
        /// This method should <b>only be used when you know the resulting value will be valid!</b>
        /// </remarks>
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="i">Integral value</param>
        /// <returns>The enum representation of <paramref name="i"/></returns>
        public static T ToEnum<T>(this short i) where T : struct, Enum => (T)Convert.ChangeType(i, Activator.CreateInstance<T>().GetTypeCode());
        /// <summary>
        /// Converts from <see cref="ushort"/> to the specified <see cref="Enum"/>-based type.
        /// </summary>
        /// <remarks>
        /// This method should <b>only be used when you know the resulting value will be valid!</b>
        /// </remarks>
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="i">Integral value</param>
        /// <returns>The enum representation of <paramref name="i"/></returns>
        public static T ToEnum<T>(this ushort i) where T : struct, Enum => (T)Convert.ChangeType(i, Activator.CreateInstance<T>().GetTypeCode());
        /// <summary>
        /// Converts from <see cref="int"/> to the specified <see cref="Enum"/>-based type.
        /// </summary>
        /// <remarks>
        /// This method should <b>only be used when you know the resulting value will be valid!</b>
        /// </remarks>
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="i">Integral value</param>
        /// <returns>The enum representation of <paramref name="i"/></returns>        public static T ToEnum<T>(this int i) where T : struct, Enum => (T)Convert.ChangeType(i, Activator.CreateInstance<T>().GetTypeCode());
        public static T ToEnum<T>(this int i) where T : struct, Enum => (T)Convert.ChangeType(i, Activator.CreateInstance<T>().GetTypeCode());
        /// <summary>
        /// Converts from <see cref="uint"/> to the specified <see cref="Enum"/>-based type.
        /// </summary>
        /// <remarks>
        /// This method should <b>only be used when you know the resulting value will be valid!</b>
        /// </remarks>
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="i">Integral value</param>
        /// <returns>The enum representation of <paramref name="i"/></returns>        public static T ToEnum<T>(this int i) where T : struct, Enum => (T)Convert.ChangeType(i, Activator.CreateInstance<T>().GetTypeCode());
        public static T ToEnum<T>(this uint i) where T : struct, Enum => (T)Convert.ChangeType(i, Activator.CreateInstance<T>().GetTypeCode());
        /// <summary>
        /// Converts from <see cref="long"/> to the specified <see cref="Enum"/>-based type.
        /// </summary>
        /// <remarks>
        /// This method should <b>only be used when you know the resulting value will be valid!</b>
        /// </remarks>
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="i">Integral value</param>
        /// <returns>The enum representation of <paramref name="i"/></returns>
        public static T ToEnum<T>(this long i) where T : struct, Enum => (T)Convert.ChangeType(i, Activator.CreateInstance<T>().GetTypeCode());
        /// <summary>
        /// Converts from <see cref="ulong"/> to the specified <see cref="Enum"/>-based type.
        /// </summary>
        /// <remarks>
        /// This method should <b>only be used when you know the resulting value will be valid!</b>
        /// </remarks>
        /// <typeparam name="T"><see cref="Enum"/>-based typename.</typeparam>
        /// <param name="i">Integral value</param>
        /// <returns>The enum representation of <paramref name="i"/></returns>
        public static T ToEnum<T>(this ulong i) where T : struct, Enum => (T)Convert.ChangeType(i, Activator.CreateInstance<T>().GetTypeCode());
        #endregion ToEnum
        #endregion TypeCasting

        #region Operators
        /// <summary>
        /// Performs a bitwise OR operation on two enum values of an arbitrary type.
        /// </summary>
        /// <remarks>
        /// <b>This method should only be used on enums with <see cref="FlagsAttribute"/></b>.
        /// </remarks>
        /// <typeparam name="T">Any <see cref="Enum"/> type.</typeparam>
        /// <param name="l">The left-side number in the operation.</param>
        /// <param name="r">The right-side number in the operation.</param>
        /// <returns>The result of the operation <b><paramref name="l"/> | <paramref name="r"/></b></returns>
        public static T BitwiseOR<T>(this T l, T r) where T : struct, Enum => (l.ToInt64() | r.ToInt64()).ToEnum<T>();
        /// <summary>
        /// Performs a bitwise AND operation on two enum values of an arbitrary type.
        /// </summary>
        /// <remarks>
        /// <b>This method should only be used on enums with <see cref="FlagsAttribute"/></b>.
        /// </remarks>
        /// <typeparam name="T">Any <see cref="Enum"/> type.</typeparam>
        /// <param name="l">The left-side number in the operation.</param>
        /// <param name="r">The right-side number in the operation.</param>
        /// <returns>The result of the operation <b><paramref name="l"/> &amp; <paramref name="r"/></b></returns>
        public static T BitwiseAND<T>(this T l, T r) where T : struct, Enum => (l.ToInt64() & r.ToInt64()).ToEnum<T>();
        /// <summary>
        /// Performs a bitwise XOR operation on two enum values of an arbitrary type.
        /// </summary>
        /// <remarks>
        /// <b>This method should only be used on enums with <see cref="FlagsAttribute"/></b>.
        /// </remarks>
        /// <typeparam name="T">Any <see cref="Enum"/> type.</typeparam>
        /// <param name="l">The left-side number in the operation.</param>
        /// <param name="r">The right-side number in the operation.</param>
        /// <returns>The result of the operation <b><paramref name="l"/> ^ <paramref name="r"/></b></returns>
        public static T BitwiseXOR<T>(this T l, T r) where T : struct, Enum => (l.ToInt64() ^ r.ToInt64()).ToEnum<T>();
        /// <summary>
        /// Performs a bitwise NOT operation on the given enum value of an arbitrary type.
        /// </summary>
        /// <remarks>
        /// <b>This method should only be used on enums with <see cref="FlagsAttribute"/></b>.
        /// </remarks>
        /// <typeparam name="T">Any <see cref="Enum"/> type.</typeparam>
        /// <param name="l">The number in the operation.</param>
        /// <returns>The result of the operation <b>~<paramref name="l"/></b></returns>
        public static T BitwiseNOT<T>(this T l) where T : struct, Enum => (~l.ToInt64()).ToEnum<T>();
        #endregion Operators
    }
}
