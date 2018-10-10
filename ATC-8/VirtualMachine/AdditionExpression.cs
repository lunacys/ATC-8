namespace ATC8.VirtualMachine
{
    public class AdditionExpression : Expression
    {
        private Expression _left;
        private Expression _right;

        public AdditionExpression(Expression left, Expression right)
        {
            _left = left;
            _right = right;
        }

        public override double Evaluate()
        {
            double left = _left.Evaluate();
            double right = _right.Evaluate();

            return left + right;
        }
    }
}