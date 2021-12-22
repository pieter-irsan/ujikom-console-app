namespace Praktek
{
    class Model
    {       
        private int quantity;
        private int variableCost;
        private int fixedCost;
        private int totalCost;

        private int marginalCost;
        private int averageFixedCost;
        private int averageVariableCost;
        private int averageTotalCost;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public int VariableCost
        {
            get { return variableCost; }
            set { variableCost = value; }
        }
        public int FixedCost
        {
            get { return fixedCost; }
            set { fixedCost = value; }
        }
        public int TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; }
        }

        public int MarginalCost
        {
            get { return marginalCost; }
            set { marginalCost = value; }
        }
        public int AverageFixedCost
        {
            get { return averageFixedCost; }
            set { averageFixedCost = value; }
        }
        public int AverageVariableCost
        {
            get { return averageVariableCost; }
            set { averageVariableCost = value; }
        }
        public int AverageTotalCost
        {
            get { return averageTotalCost; }
            set { averageTotalCost = value; }
        }
    }
}