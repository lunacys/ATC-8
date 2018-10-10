namespace ATC8.VirtualMachine
{
    public class NumberExpression : Expression
    {
        private double _value;

        public NumberExpression(double value)
        {
            _value = value;
        }

        public override double Evaluate()
        {
            return _value;
        }
    }
}