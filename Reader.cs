namespace temp;

public class Reader {
    /// <summary>
    /// Read an integer from stdin.
    /// </summary>
    /// <param name="description">Description printed before stdin is read.</param>
    /// <param name="lb">Lower bound, inclusive</param>
    /// <param name="ub">Optional upper bound, exclusive</param>
    /// <param name="validator">Custom validator function. Returns true if the input is valid</param>
    /// <returns>
    /// Returns the entered integer once it is valid.
    /// An input is considered valid if:
    /// - It is an integer
    /// - It is larger than, or equal to the lower bound
    /// - It is less than the upper bound
    /// - It passes the validator
    /// </returns>
    public static int ReadInt(string description, int lb, int? ub, Func<int, bool>? validator) {
        Console.Write(description);
        int val;
        while (!int.TryParse(Console.ReadLine(), out val) || !(val >= lb) || !_EvalUb(ub, val) || !_EvalValidator(validator, val)) {
            Console.WriteLine($"Invalid input. Please enter a number <{lb}, {_UbText(ub)}] ");
        }

        return val;
    }

    /**
     * Get the range text for the upper bound.
     * Returns the infinity symbol if null, otherwhise returns the value.
     */
    private static string _UbText(int? ub) {
        if (ub == null) {
            return "\u221e";
        } else {
            return ub.ToString();
        }
    }

    /**
     * Evaluate the upper bounds check.
     * Always returns true if the upper bound is null.
     */
    private static bool _EvalUb(int? ub, int val) {
        if (ub == null) {
            return true;
        } else {
            // Using negation to make the bounds check more clears
            return !(ub < val);
        }
    }

    /**
     * Evaluate the validator function.
     * Always returns true if the validator is null.
     */
    private static bool _EvalValidator(Func<int, bool>? validator, int val) {
        if (validator == null) {
            return true;
        } else {
            return validator.Invoke(val);
        }
    } 
}